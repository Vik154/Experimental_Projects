using Microsoft.AspNetCore.Razor.TagHelpers;

namespace _06_TagHelpers.TagHelpers;

public class SummaryTagHelper : TagHelper {
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";
        // получаем вложенный контекст из дочерних tag-хелперов
        var target = await output.GetChildContentAsync();
        var content = "<h3>Общая информация</h3>" + target.GetContent();
        output.Content.SetHtmlContent(content);
    }
}
