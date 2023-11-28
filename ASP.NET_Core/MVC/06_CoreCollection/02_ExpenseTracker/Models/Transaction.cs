﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_ExpenseTracker.Models;

public class Transaction {

    [Key]
    public int TransactionId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, выберите категорию.")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Сумма должна быть больше 0.")]
    public int Amount { get; set; }

    [Column(TypeName = "nvarchar(75)")]
    public string? Note { get; set; }

    public DateTime Date {  get; set; } = DateTime.Now;

    [NotMapped]
    public string? CategoryTitleWithIcon => Category == null ? "" : Category.Icon + " " + Category.Title;

    [NotMapped]
    public string? FormattedAmount => 
        ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
}
