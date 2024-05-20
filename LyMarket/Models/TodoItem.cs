using LyMarket.Common;
using LyMarket.Enums;

namespace LyMarket.Models;

public class TodoItem : BaseEntity
{
    public string Title { get; set; }

    public bool IsComplete { get; set; } = false;

    public PriorityLevel Priority { get; set; } = PriorityLevel.None;
}
