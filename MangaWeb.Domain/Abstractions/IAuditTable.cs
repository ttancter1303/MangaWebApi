﻿using MangaWeb.Domain.Enums;

namespace MangaWeb.Domain.Abstractions
{
    internal interface IAuditTable
    {
        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public EntityStatus Status { get; set; }
    }
}
