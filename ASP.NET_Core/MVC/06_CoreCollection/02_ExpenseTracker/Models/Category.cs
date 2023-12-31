﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_ExpenseTracker.Models;

public class Category {

    [Key]
    public int CategoryId { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    [Required(ErrorMessage = "Требуется название.")]
    public string Title { get; set; }

    [Column(TypeName = "nvarchar(5)")]
    public string Icon { get; set; } = "";

    [Column(TypeName = "nvarchar(10)")]
    public string Type { get; set; } = "Расход";

    [NotMapped]
    public string? TitleWithIcon => Icon + " " + Title;
}
