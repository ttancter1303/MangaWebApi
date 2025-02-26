using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class MangaNotFoundException : BaseException
{
    public MangaNotFoundException(int mangaId)
        : base(
            title: "Manga Not Found",
            message: $"Manga with ID {mangaId} was not found."
        )
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}