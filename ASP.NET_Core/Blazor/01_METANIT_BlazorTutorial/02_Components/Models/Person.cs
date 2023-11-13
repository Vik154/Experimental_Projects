namespace _02_Components.Models;

public class Person {
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public DateTime Day { get; set; } = DateTime.Now;
    public bool Working { get; set; }
    public string Subject { get; set; } = "";
    public string[] Courses { get; set; } = new string[0];
}
