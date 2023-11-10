using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class RoutesModel : PageModel {

    /// <summary> 
    /// ��� ��������� ���������� �������� �� �������� Razor 
    /// � � �� ������ ����������� ������� RouteData.Values.
    /// � ������� ����� - �������� ��������� ����� �������� ��� ��������: 
    /// </summary>
    public void OnGet() => Id = RouteData.Values["id"];
    
    public object? Id { get; private set; }
}
