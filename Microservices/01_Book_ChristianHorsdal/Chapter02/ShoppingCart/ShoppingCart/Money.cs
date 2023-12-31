﻿namespace ShoppingCart.ShoppingCart;

public class Money {
    public string Currency { get; }
    public decimal Amount { get; }

    public Money(string currency, decimal amount) =>
        (Currency, Amount) = (currency, amount);
}
