using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace _02_Views.Util;

public class CustomViewEngine : IViewEngine {
    public ViewEngineResult GetView(string? executingFilePath, string viewPath, bool isMainPage) {
        return ViewEngineResult.NotFound(viewPath, new string[] { });
    }
    public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage) {
        string viewPath = $"Views/Engine/{viewName}.cshtml"; ;
        if (string.IsNullOrEmpty(viewName)) {
            viewPath = $"Views/Engine/{context.RouteData.Values["action"]}.cshtml";
        }

        if (File.Exists(viewPath)) {
            return ViewEngineResult.Found(viewPath, new CustomView(viewPath));
        }
        else {
            return ViewEngineResult.NotFound(viewName, new string[] { viewPath });
        }
    }
}
