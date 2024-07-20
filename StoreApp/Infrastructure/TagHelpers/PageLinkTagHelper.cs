using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Models;

namespace StoreApp.Infrastructure.TagHelpers
{
    // Bu sınıf, özel bir TagHelper olan PageLinkTagHelper sınıfını tanımlar.
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        // URL yardımcılarını oluşturmak için kullanılan IUrlHelperFactory nesnesi.
        private readonly IUrlHelperFactory _urlHelperFactory;

        // Görünüm bağlamını alır ve ayarlar. Bu, HTML yardımcılarının görünümdeki bilgileri almasını sağlar.
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        // Sayfalama modelini temsil eder.
        public Pagination PageModel { get; set; }

        // Sayfa eylemini temsil eden bir string. Sayfa numarasına göre URL'yi oluşturmak için kullanılır.
        public string? PageAction { get; set; }

        // Sayfa sınıflarının etkin olup olmadığını belirler.
        public bool PageClassesEnabled { get; set; } = false;

        // Genel sayfa sınıfı.
        public string PageClass { get; set; } = string.Empty;

        // Normal sayfa sınıfı.
        public string PageClassNormal { get; set; } = string.Empty;

        // Seçili sayfa sınıfı.
        public string PageClassSelected { get; set; } = string.Empty;

        // Yapıcı metot, IUrlHelperFactory nesnesini alır.
        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        // TagHelper'ın HTML çıktısını işlemesini sağlar.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // ViewContext ve PageModel null değilse işlemi gerçekleştir.
            if (ViewContext != null && PageModel != null)
            {
                // URL yardımcı nesnesini al.
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                // Bir <div> elementi oluştur.
                TagBuilder result = new TagBuilder("div");

                // Toplam sayfa sayısı kadar döngü oluştur.
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    // Her bir sayfa için bir <a> elementi oluştur.
                    TagBuilder tag = new TagBuilder("a");

                    // <a> elementinin href özniteliğini ayarla.
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { PageNumber = i });

                    // Sayfa sınıfları etkinse, sınıfları ayarla.
                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    // <a> elementinin içeriğini sayfa numarası olarak ayarla.
                    tag.InnerHtml.Append(i.ToString());

                    // <div> elementine <a> elementini ekle.
                    result.InnerHtml.AppendHtml(tag);
                }

                // TagHelper çıktısına oluşturulan <div> içeriğini ekle.
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
