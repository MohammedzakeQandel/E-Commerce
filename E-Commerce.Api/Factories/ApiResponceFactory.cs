using Microsoft.AspNetCore.Mvc;
using Shared.ErroreModel;

namespace E_Commerce.Api.Factories
{
    public class ApiResponceFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            // context => errors, key [Field]
            // context.ModelState = <string, ModelStateEntry>
            // string ==> name of the field
            // ModelStateEntry ==> Errors ==> Error Messages

            var errors = context.ModelState
                .Where(error => error.Value?.Errors.Any() == true)
                .Select(error => new ValidationError
                {
                    Field = error.Key,
                    Errors = error.Value?.Errors
                        .Select(error => error.ErrorMessage)
                        .ToList() ?? new List<string>()
                })
                .ToList();

            var response = new ValidationErrorResponse
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One or more validation errors happened"
            };

            return new BadRequestObjectResult(response);
        }
    }
}
