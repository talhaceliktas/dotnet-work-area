using Example_4.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Example_4.Binders
{
    public class SearchFilterBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // 1. Kutumuzu oluşturuyoruz. İçinde zaten Page=1 ve PageSize=10 hazır bekliyor!
            SearchFilter searchFilter = new SearchFilter();
            // 2. Keyword Okuma
            var keyword = bindingContext.ValueProvider.GetValue("keyword").FirstValue;
            if (!string.IsNullOrEmpty(keyword))
            {
                searchFilter.Keyword = keyword;
            }

            // 3. Page Okuma (Ünlem YOK!)
            var pageValue = bindingContext.ValueProvider.GetValue("page").FirstValue;
            // EĞER boş değilse VE başarıyla sayıya çevrilebiliyorsa:
            if (!string.IsNullOrEmpty(pageValue) && int.TryParse(pageValue, out int sayfaNo))
            {
                // Modeli güncelle. (Çevrilemezse hiçbir şey yapmıyoruz, Page zaten 1 kalıyor)
                searchFilter.Page = sayfaNo;
            }

            // 4. PageSize Okuma ve Sınırlandırma (Clamp)
            var pageSizeValue = bindingContext.ValueProvider.GetValue("pageSize").FirstValue;
            if (!string.IsNullOrEmpty(pageSizeValue) && int.TryParse(pageSizeValue, out int limit))
            {
                // Görev: "5 ile 50 arasında sınırlayın"
                if (limit > 50) limit = 50; 
                else if (limit < 5) limit = 5; 

                searchFilter.PageSize = limit;
            }

            bindingContext.Result = ModelBindingResult.Success(searchFilter);
            return Task.CompletedTask;
        }
    }
}