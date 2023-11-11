using System.ComponentModel.DataAnnotations;

namespace _08_MetaData.Models;

public class Person {

    [Required]
    public string? Name { get; set; }

    [Required]
    public int Age { get; set; }
}
