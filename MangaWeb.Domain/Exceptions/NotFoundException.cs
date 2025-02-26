using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public abstract class NotFoundException : BaseException
{
    protected NotFoundException(string message)
        : base("Not Found", message)
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}
