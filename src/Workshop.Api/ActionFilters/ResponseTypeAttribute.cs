using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Workshop.Api.ActionFilters;

public class ResponseTypeAttribute : ProducesResponseTypeAttribute
{
    public ResponseTypeAttribute(int statusCode) 
        : base(typeof(ErrorResponse), statusCode)
    {
    }
}