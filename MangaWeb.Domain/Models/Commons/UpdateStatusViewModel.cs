using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Models.Commons
{
    public class UpdateStatusViewModel
    {
        public EntityStatus Status { get; set; }
        public Guid Id { get; set; }
    }
}
