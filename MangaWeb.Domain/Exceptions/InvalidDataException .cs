using Microsoft.AspNetCore.Http;

namespace MangaWeb.Domain.Exceptions;

public class InvalidDataException : BaseException
{
    public InvalidDataException(string fieldName)
        : base(
            title: "Invalid Data",
            message: $"The provided data for '{fieldName}' is invalid."
        )
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }
}