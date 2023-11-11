using Microsoft.AspNetCore.Mvc;

namespace _07_ViewComponent.Components;

public class UsersListViewComponent : ViewComponent {

    List<string> users = new List<string> { "Tom", "Tim", "Bob", "Sam" };

    public IViewComponentResult Invoke() => View(users);
}
