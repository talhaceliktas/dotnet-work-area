using Example_4.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Example_4.Binders
{
    public class SearchFilterBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(SearchFilter))
                return new BinderTypeModelBinder(typeof(SearchFilterBinder));

            return null;
        }
    }
}
