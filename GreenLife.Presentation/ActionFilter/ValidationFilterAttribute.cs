using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GreenLife.Presentation.ActionFilter
{

    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidationFilterAttribute(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments.SingleOrDefault(x => x.Value != null && x.Value.GetType().Name.Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action} ");
                return;
            }

            var validator = _validatorFactory.GetValidator(param.GetType());
            if (validator != null)
            {
                var validationResult = validator.Validate((IValidationContext)param);
                if (!validationResult.IsValid)
                {
                    context.Result = new BadRequestObjectResult(validationResult.Errors.Select(e => e.ErrorMessage));
                    return;
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }


    //public class ValidationFilterAttribute : IActionFilter
    //{
    //    public ValidationFilterAttribute()
    //    { }
    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        var action = context.RouteData.Values["action"];
    //        var controller = context.RouteData.Values["controller"];
    //        var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
    //        if (param is null)
    //        {
    //            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action} ");
    //            return;
    //        }
    //        if (!context.ModelState.IsValid)
    //            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    //    }
    //    public void OnActionExecuted(ActionExecutedContext context) { }
    //}
}
