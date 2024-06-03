using System;
using System.Collections.Generic;

namespace Dotnet8_EFCore.Models;

public partial class Country
{
    public Country()
    {
        states = new List<State>();
    }
    public long Id { get; set; }

    public string? Shortname { get; set; }

    public string? Name { get; set; }

    public int? Phonecode { get; set; }

    public List<State> states { get; set; }
}
