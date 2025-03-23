using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MangaWeb.Domain.Abstractions;
using MangaWeb.Domain.Abstractions.ApplicationServices;
using MangaWeb.Domain.Abstractions.InfrastructureServices;
using MangaWeb.Domain.Entities;
using MangaWeb.Domain.Exceptions;
using MangaWeb.Domain.Models.Commons;
using MangaWeb.Domain.Models.Users;
using MangaWeb.Domain.Utility;

using MangaWeb.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MangaWeb.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IGenericRepository<Permission, Guid> _permissionRepository;
        private readonly IGenericRepository<RolePermission, Guid> _rolePermissionRepository;
        private readonly IJwtTokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IGenericRepository<Permission, Guid> permissionRepository,
            IHttpContextAccessor httpContextAccessor,
            IJwtTokenService tokenService, IGenericRepository<RolePermission, Guid> rolePermissionRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionRepository = permissionRepository;
            _tokenService = tokenService;
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        #region Common
        public async Task<AuthorizedResponseModel> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                throw new UserException.PasswordNotCorrectException();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var jwtToken = _tokenService.GenerateAccessToken(claims);

            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Items["Token"] = jwtToken;
            }

            Console.WriteLine($" Token Generated: {jwtToken}");

            return new AuthorizedResponseModel { JwtToken = jwtToken };
        }


        public async Task<string?> GetUserNameByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            return user.UserName;
        }


        public async Task<UserProfileModel> GetUserProfile(string userName)
        {

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }
            var result = new UserProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            if (!user.IsSystemUser)
            {
                return result;
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles == null || !userRoles.Any())
            {
                return result;
            }
            var roles = _roleManager.Roles;
            var permissions = _permissionRepository.FindAll();

            var rolePermissions = _rolePermissionRepository.FindAll();
            var userPermission =
                from r in roles
                join rp in rolePermissions on r.Id equals rp.RoleId
                select new { rp.PermissionCode, r.Name };

            var filerPermissions = userPermission.Where(s => userRoles.Contains(s.Name)).Select(x => x.PermissionCode).ToList();
            result.Permissions = filerPermissions.ToList().DistinctBy(s => s).ToList();
            return result;
        }

        public async Task<ResponseResult> UpdateUserInfo(UpdateUserInfoViewModel model, UserProfileModel currentUser)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == currentUser.UserId);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ResponseResult.Success("Update user profile success");
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);
                throw new UserException.HandleUserException(errors);
            }
        }

        public async Task<ResponseResult> ChangePassword(ChangePasswordViewModel model, UserProfileModel currentUser)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(s => s.Id == currentUser.UserId);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }


            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return ResponseResult.Success("Update user profile success");
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);
                throw new UserException.HandleUserException(errors);
            }
        }


        #endregion

        #region User

        public async Task<ResponseResult> RegisterUser(RegisterUserViewModel model)
        {
            var user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNummber,
                IsSystemUser = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                return ResponseResult.Success();
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);
                throw new UserException.HandleUserException(errors);
            }
        }




        #endregion

        #region  System_Users
        public async Task<ResponseResult> RegisterSystemUser(RegisterUserViewModel model)
        {
            var user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNummber,
                IsSystemUser = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var roleAssignResult = await _userManager.AddToRoleAsync(user, "admin");
                if (!roleAssignResult.Succeeded)
                {
                    var roleErrors = JsonConvert.SerializeObject(roleAssignResult.Errors);
                    throw new UserException.HandleUserException($"Không thể gán role Admin: {roleErrors}");
                }
                return ResponseResult.Success();
            }
            else
            {
                var errors = JsonConvert.SerializeObject(result.Errors);
                throw new UserException.HandleUserException(errors);
            }
        }

        public async Task<ResponseResult> AssignRoles(AssignRolesViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            var result = await _userManager.AddToRolesAsync(user, model.RoleNames);
            if (result.Succeeded)
            {
                return ResponseResult.Success("Assign roles to user success");
            }
            var errors = JsonConvert.SerializeObject(result.Errors);
            throw new UserException.HandleUserException(errors);
        }

        public async Task<ResponseResult> RemoveRoles(RemoveRolesViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            var result = await _userManager.RemoveFromRolesAsync(user, model.RoleNames);
            if (result.Succeeded)
            {
                return ResponseResult.Success();
            }
            var errors = JsonConvert.SerializeObject(result.Errors);
            throw new UserException.HandleUserException(errors);
        }

        public async Task<ResponseResult> AssignPermissions(AssignPermissionsViewModel model)

        {
            var permissions = model.Permissions.Where(s => s.IsInRole).Select(s => new RolePermission()
            {
                Id = Guid.NewGuid(),
                RoleId = model.RoleId,
                PermissionCode = s.PermissionCode,
            }).ToList();
            var currentPermissions = await _rolePermissionRepository.FindAll(s => s.RoleId == model.RoleId).ToListAsync();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _rolePermissionRepository.RemoveMultiple(currentPermissions);
                _rolePermissionRepository.AddRange(permissions);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return ResponseResult.Success();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackAsync();
                throw new UserException.HandleUserException("Somethings went wrong");
            }
        }

        public async Task<PageResult<UserViewModel>> GetUsers(UserSearchQuery query)
        {
            var result = new PageResult<UserViewModel>() { CurrentPage = query.PageIndex };
            var users = _userManager.Users.Where(s => s.IsSystemUser == query.IsSystemUser);
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                users = users.Where(s => s.UserName.Contains(query.Keyword)
                                    || s.Email.Contains(query.Keyword)
                                    || s.PhoneNumber.Contains(query.Keyword));
            }
            result.TotalCount = await users.CountAsync();
            result.Data = await users.Select(s => new UserViewModel
            {
                UserId = s.Id,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                UserName = s.UserName,
            }).ToListAsync();
            return result;
        }

        public async Task<PageResult<RoleViewModel>> GetRoles(RoleSearchQuery query)
        {
            var result = new PageResult<RoleViewModel>() { CurrentPage = query.PageIndex };
            var roles = _roleManager.Roles;
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                roles = roles.Where(s => s.Name.Contains(query.Keyword));
            }
            result.TotalCount = await roles.CountAsync();

            result.Data = await roles.Select
                 (s => new RoleViewModel
                 {
                     RoleId = s.Id,
                     RoleName = s.Name
                 }).ToListAsync();
            return result;
        }

        public async Task<RoleViewModel> GetRoleDetail(Guid roleId)
        {
            var roles = _roleManager.Roles;
            var permissions = await _permissionRepository.FindAll().ToListAsync();
            var rolePermission = _rolePermissionRepository.FindAll();

            var role = roles.FirstOrDefault(s => s.Id == roleId);
            if (role == null)
            {
                throw new UserException.RoleNotFoundException();
            }

            var permissionCodesInRole = await rolePermission.Where(s => s.RoleId == roleId).Select(x => x.PermissionCode).ToListAsync();
            var permissionViewModels = permissions.Select(s => new PermissionViewModel
            {
                IsInRole = permissionCodesInRole.Contains(s.Code),
                PermissionName = s.Name,
                PermissionCode = s.Code,
                ParentPermissionCode = s.ParentCode
            }).ToList();

            var usersInRole = (await _userManager.GetUsersInRoleAsync(role.Name)).Select(s => new UserViewModel
            {
                UserId = s.Id,
                PhoneNumber = s.PhoneNumber,
                UserName = s.UserName,
                Email = s.Email
            }).ToList();
            var result = new RoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = permissionViewModels,
                UsersInRole = usersInRole
            };
            return result;

        }

        public async Task<ResponseResult> CreateRole(CreateRoleViewModel model)

        {
            var role = new AppRole()
            {
                Name = model.RoleName
            };
            await _roleManager.CreateAsync(role);
            return ResponseResult.Success();
        }

        public async Task<ResponseResult> UpdateRole(UpdateRoleViewModel model)

        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role == null)
            {
                throw new UserException.RoleNotFoundException();
            }
            role.Name = model.RoleName;
            await _roleManager.UpdateAsync(role);
            return ResponseResult.Success();
        }

        public async Task<ResponseResult> DeleteRole(Guid roleId)

        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                throw new UserException.RoleNotFoundException();
            }

            await _roleManager.DeleteAsync(role);
            return ResponseResult.Success();
        }

        public async Task<ResponseResult> DeleteUser(Guid userId)

        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserException.UserNotFoundException();
            }

            await _userManager.DeleteAsync(user);
            return ResponseResult.Success();
        }

        #endregion

        #region Seeding Data
        public async Task<bool> InitializeUserAdminAsync()
        {
            var userAdmin = _configuration.GetSection("UserAdmin");
            if (userAdmin != null)
            {
                var user = await _userManager.FindByNameAsync(userAdmin["UserName"]);
                if (user == null)
                {
                    var createUser = new AppUser()
                    {
                        UserName = userAdmin["UserName"],
                        Email = userAdmin["Email"],
                        IsSystemUser = true
                    };

                    var createUserResult = await _userManager.CreateAsync(createUser, userAdmin["Password"]);

                    if (!createUserResult.Succeeded)
                    {
                        return false;
                    }

                    var adminRole = new AppRole() { Name = userAdmin["Role"] };
                    var createRoleResult = await _roleManager.CreateAsync(adminRole);
                    if (!createRoleResult.Succeeded)
                    {
                        return false;
                    }

                    var assignRoleResult = await _userManager.AddToRoleAsync(createUser, adminRole.Name);
                    if (!assignRoleResult.Succeeded)
                    {
                        return false;
                    }

                    var listPermissions = new List<Permission>
                    {
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ROLE_PERMISSION,
                            Name = CommonConstants.Permissions.ROLE_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_ROLE_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_ROLE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.ROLE_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_ROLE_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_ROLE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.ROLE_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_ROLE_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_ROLE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.ROLE_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_ROLE_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_ROLE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.ROLE_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.USER_PERMISSION,
                            Name = CommonConstants.Permissions.USER_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_USER_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_USER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.USER_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_USER_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_USER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.USER_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_USER_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_USER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.USER_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_USER_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_USER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.USER_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.MANGA_PERMISSION,
                            Name = CommonConstants.Permissions.MANGA_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_MANGA_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_MANGA_PERMISSION,
                            ParentCode = CommonConstants.Permissions.MANGA_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_MANGA_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_MANGA_PERMISSION,
                            ParentCode = CommonConstants.Permissions.MANGA_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_MANGA_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_MANGA_PERMISSION,
                            ParentCode = CommonConstants.Permissions.MANGA_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_MANGA_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_MANGA_PERMISSION,
                            ParentCode = CommonConstants.Permissions.MANGA_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Name = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_IMAGE_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_IMAGE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_IMAGE_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_IMAGE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_IMAGE_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_IMAGE_PERMISSION,
                            ParentCode = CommonConstants.Permissions.IMAGE_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Name = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_CHAPTER_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_CHAPTER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION,
                            ParentCode = CommonConstants.Permissions.CHAPTER_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Name = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION,
                            ParentCode = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_AUTHOR_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_AUTHOR_PERMISSION,
                            ParentCode = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION,
                            ParentCode = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION,
                            ParentCode = CommonConstants.Permissions.AUTHOR_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.TAG_PERMISSION,
                            Name = CommonConstants.Permissions.TAG_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_TAG_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_TAG_PERMISSION,
                            ParentCode = CommonConstants.Permissions.TAG_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_TAG_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_TAG_PERMISSION,
                            ParentCode = CommonConstants.Permissions.TAG_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_TAG_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_TAG_PERMISSION,
                            ParentCode = CommonConstants.Permissions.TAG_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_TAG_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_TAG_PERMISSION,
                            ParentCode = CommonConstants.Permissions.TAG_PERMISSION,
                            Index = 5
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Name = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Index = 1
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.VIEW_REVIEW_PERMISSION,
                            Name = CommonConstants.Permissions.VIEW_REVIEW_PERMISSION,
                            ParentCode = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Index = 2
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.ADD_REVIEW_PERMISSION,
                            Name = CommonConstants.Permissions.ADD_REVIEW_PERMISSION,
                            ParentCode = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Index = 3
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION,
                            Name = CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION,
                            ParentCode = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Index = 4
                        },
                        new Permission()
                        {
                            Id = Guid.NewGuid(),
                            Code = CommonConstants.Permissions.DELETE_REVIEW_PERMISSION,
                            Name = CommonConstants.Permissions.DELETE_REVIEW_PERMISSION,
                            ParentCode = CommonConstants.Permissions.REVIEW_PERMISSION,
                            Index = 5
                        }
                    };



                    var rolesPermissons = listPermissions.Select(s => new RolePermission()
                    {
                        Id = Guid.NewGuid(),
                        RoleId = adminRole.Id,
                        PermissionCode = s.Code,
                    }).ToList();

                    bool assignPermissionResult = true;

                    try
                    {
                        await _unitOfWork.BeginTransactionAsync();
                        _permissionRepository.AddRange(listPermissions);
                        _rolePermissionRepository.AddRange(rolesPermissons);
                        await _unitOfWork.SaveChangesAsync();
                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        await _unitOfWork.RollbackAsync();
                        assignPermissionResult = false;
                    }

                    if (!assignPermissionResult)
                    {
                        return false;
                    }
                    return true;
                }
            }

            else
            {
                return false;
            }

            return false;
        }



        #endregion
    }
}
