﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Models.Users
{
    public class AssignRolesViewModel
    {
        public string UserName { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
