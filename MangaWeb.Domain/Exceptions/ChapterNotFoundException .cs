using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class ChapterNotFoundException : BaseException
{
    public ChapterNotFoundException(Guid chapterId)  
        : base(
            title: "Chapter Not Found",
            message: $"Chapter with ID {chapterId} was not found."
        )
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}
