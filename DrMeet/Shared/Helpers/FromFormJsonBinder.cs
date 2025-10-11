using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DrMeet.Api.Shared.Helpers
{
    public class FromFormJsonBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if (string.IsNullOrEmpty(value)) { bindingContext.Result = ModelBindingResult.Success(default(T)); return Task.CompletedTask; }
            try { var result = System.Text.Json.JsonSerializer.Deserialize<T>(value); bindingContext.Result = ModelBindingResult.Success(result); } catch (Exception ex) { bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message); }
            return Task.CompletedTask;
        }
    }
}
