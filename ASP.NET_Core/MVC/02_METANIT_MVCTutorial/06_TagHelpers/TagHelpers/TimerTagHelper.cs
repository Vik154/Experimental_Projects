using Microsoft.AspNetCore.Razor.TagHelpers;

namespace _06_TagHelpers.TagHelpers;

public class TimerTagHelper : TagHelper {
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";
        output.Content.SetContent($"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}");
    }
}
