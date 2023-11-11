using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers;

public class TimeController : Controller {
   
    readonly ITimeService timeService;
    public TimeController(ITimeService timeServ) => timeService = timeServ;
    public string Index() => timeService.Time;
}
