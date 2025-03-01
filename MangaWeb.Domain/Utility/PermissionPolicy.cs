using Microsoft.AspNetCore.Authorization;

namespace MangaWeb.Domain.Utility
{
    public static class PermissionPolicy
    {
        public static void AddPermissionPolicies(AuthorizationOptions options)
        {
            // User Permissions
            options.AddPolicy(CommonConstants.Permissions.USER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.USER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_USER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_USER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_USER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_USER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_USER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_USER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_USER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_USER_PERMISSION));

            // Role Permissions
            options.AddPolicy(CommonConstants.Permissions.ROLE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ROLE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_ROLE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_ROLE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_ROLE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_ROLE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_ROLE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_ROLE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_ROLE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_ROLE_PERMISSION));

            // Manga Permissions
            options.AddPolicy(CommonConstants.Permissions.MANGA_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.MANGA_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_MANGA_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_MANGA_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_MANGA_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_MANGA_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_MANGA_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_MANGA_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_MANGA_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_MANGA_PERMISSION));

            // Chapter Permissions
            options.AddPolicy(CommonConstants.Permissions.CHAPTER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.CHAPTER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_CHAPTER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_CHAPTER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_CHAPTER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_CHAPTER_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_CHAPTER_PERMISSION));

            // Author Permissions
            options.AddPolicy(CommonConstants.Permissions.AUTHOR_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.AUTHOR_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_AUTHOR_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_AUTHOR_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_AUTHOR_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_AUTHOR_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_AUTHOR_PERMISSION));

            // Tag Permissions
            options.AddPolicy(CommonConstants.Permissions.TAG_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.TAG_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_TAG_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_TAG_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_TAG_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_TAG_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_TAG_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_TAG_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_TAG_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_TAG_PERMISSION));

            // Review Permissions
            options.AddPolicy(CommonConstants.Permissions.REVIEW_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.REVIEW_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_REVIEW_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_REVIEW_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_REVIEW_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_REVIEW_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_REVIEW_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_REVIEW_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_REVIEW_PERMISSION));

            // Image Permissions
            options.AddPolicy(CommonConstants.Permissions.IMAGE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.IMAGE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.ADD_IMAGE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.ADD_IMAGE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.UPDATE_IMAGE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.DELETE_IMAGE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.DELETE_IMAGE_PERMISSION));
            options.AddPolicy(CommonConstants.Permissions.VIEW_IMAGE_PERMISSION, policy =>
                policy.RequireClaim("Permission", CommonConstants.Permissions.VIEW_IMAGE_PERMISSION));
        }
    }
}