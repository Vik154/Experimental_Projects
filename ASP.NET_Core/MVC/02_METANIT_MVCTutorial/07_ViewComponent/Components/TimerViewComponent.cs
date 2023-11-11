namespace _07_ViewComponent.Components;

public class TimerViewComponent {
    public string Invoke() => $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
}
