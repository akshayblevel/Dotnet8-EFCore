using System;
using System.Collections.Generic;

namespace Dotnet8_EFCore.Models;

public partial class State
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? CountryId { get; set; }

    public List<City> Cities { get; set; }
}
