using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Models.Users
{
    public class UpdateRoleViewModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
