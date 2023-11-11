using Microsoft.AspNetCore.Mvc;

namespace _07_ViewComponent.Components;

public class Header : ViewComponent {
    public async Task<string> InvokeAsync() {
        using (StreamReader reader = new StreamReader("Files/header.txt"))
            return await reader.ReadToEndAsync();
    }
}
