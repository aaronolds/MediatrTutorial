﻿namespace MediatrTutorial.Domain;

using System;

public class Customer {
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public DateTime RegistrationDate { get; set; }
}