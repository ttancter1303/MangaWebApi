using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class MangaAlreadyExistsException : BaseException
{
    public MangaAlreadyExistsException(string mangaTitle)
        : base(
            title: "Manga Already Exists",
            message: $"A manga with the title '{mangaTitle}' already exists."
        )
    {
        StatusCode = StatusCodes.Status409Conflict;
    }
}