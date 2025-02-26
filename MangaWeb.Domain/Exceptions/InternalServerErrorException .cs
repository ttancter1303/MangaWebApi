using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class InternalServerErrorException : BaseException
{
    public InternalServerErrorException()
        : base(
            title: "Internal Server Error",
            message: "An unexpected error occurred on the server."
        )
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}