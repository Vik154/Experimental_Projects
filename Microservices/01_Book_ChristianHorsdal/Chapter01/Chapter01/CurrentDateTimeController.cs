using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chapter01;

public class CurrentDateTimeController : ControllerBase {

    [HttpGet("/")]
    public async Task<IActionResult> Get() => Content(DateTime.Now.ToLongDateString());
    // public object Get() => DateTime.UtcNow;
}
