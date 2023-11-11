﻿using Microsoft.AspNetCore.Mvc;

namespace _02_Views.Controllers;

public class HomeController : Controller {

    /// <summary> View(): для генерации ответа используется представление,
    /// которое по имени совпадает с вызывающим методом, если явно не указано иное</summary>
    public IActionResult Index() {
        return View();
    }

    public IActionResult Test() {
        return View("About");
    }
}
