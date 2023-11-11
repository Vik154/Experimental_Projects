using Microsoft.AspNetCore.Razor.TagHelpers;

namespace _06_TagHelpers.TagHelpers;

public class DateTagHelper : TagHelper {
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";
        output.Content.SetContent($"Текущая дата: {DateTime.Now.ToString("dd/mm/yyyy")}");
    }
}
