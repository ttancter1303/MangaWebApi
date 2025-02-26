using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class UnauthorizedAccessException : BaseException
{
    public UnauthorizedAccessException(string resourceName)
        : base(
            title: "Unauthorized Access",
            message: $"You do not have permission to access this resource: {resourceName}."
        )
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
}