using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class TimeModel : PageModel {
    public string Message { get; private set; }
    public TimeModel(ITimeService timeService) => Message = $"Time: {timeService.Time}";

    /* Иногда зависимость используется только в одном методе. 
     * И в этом случае нет необходимости передавать ее в конструктор,
     * поскольку она напрямую может быть внедрена в сам метод, который ее использует.
     * Для передачи зависимости в метод применяется атрибут [FromServices]:
     
       public void OnGet([FromServices] ITimeService timeService) {
            Message = $"Time: {timeService.Time}";
       }
     */
}
