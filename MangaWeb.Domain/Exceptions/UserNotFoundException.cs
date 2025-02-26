using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class UserNotFoundException : BaseException
{
    public UserNotFoundException(int userId)
        : base(
            title: "User Not Found",
            message: $"User with ID {userId} was not found."
        )
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}