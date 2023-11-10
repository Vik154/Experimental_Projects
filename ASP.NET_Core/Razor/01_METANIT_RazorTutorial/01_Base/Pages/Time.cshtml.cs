using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class TimeModel : PageModel {
    public string Message { get; private set; }
    public TimeModel(ITimeService timeService) => Message = $"Time: {timeService.Time}";

    /* ������ ����������� ������������ ������ � ����� ������. 
     * � � ���� ������ ��� ������������� ���������� �� � �����������,
     * ��������� ��� �������� ����� ���� �������� � ��� �����, ������� �� ����������.
     * ��� �������� ����������� � ����� ����������� ������� [FromServices]:
     
       public void OnGet([FromServices] ITimeService timeService) {
            Message = $"Time: {timeService.Time}";
       }
     */
}
