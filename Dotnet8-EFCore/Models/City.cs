using System;
using System.Collections.Generic;

namespace Dotnet8_EFCore.Models;

public partial class City
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? StateId { get; set; }
}
