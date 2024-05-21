using LyMarket.Common;

namespace LyMarket.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Image { get; set; }
}
