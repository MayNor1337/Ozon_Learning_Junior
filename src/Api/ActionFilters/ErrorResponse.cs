using System.Net;

namespace Api.ActionFilters;

public record ErrorResponse(HttpStatusCode StatusCodes, string Message)
{
    public override string ToString()
    {
        return $"{{ StatusCodes = {StatusCodes}, Message = {Message} }}";
    }
}