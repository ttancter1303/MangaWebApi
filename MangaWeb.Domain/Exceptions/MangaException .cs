using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaWeb.Domain.Exceptions
{
    public class MangaException : Exception
    {
        public MangaException(string message) : base(message) { }

        public class CreateMangaException : MangaException
        {
            public CreateMangaException(string mangaTitle)
                : base($"Failed to create manga: {mangaTitle}") { }
        }
    }
}
