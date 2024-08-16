using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Gets the name of the action being executed.
        var action = context.RouteData.Values["action"];
        // Gets the name of the controller handling the request
        var controller = context.RouteData.Values["controller"];
        // Extracts the DTO parameter that we send to the POST and PUT actions
        var param = context.ActionArguments
                           .SingleOrDefault(x => x.Value != null && x.Value.GetType().Name.Contains("Dto"))
                           .Value;

        if (param == null)
        {
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // This method can be left empty if no action is needed after the action executes
    }
}