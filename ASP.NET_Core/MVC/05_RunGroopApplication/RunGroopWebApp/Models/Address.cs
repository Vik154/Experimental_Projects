using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models;

/// <summary> POCO - object </summary>
public class Address {
    
    [Key]
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
