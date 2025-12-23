using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class Skill : ComponentBase
{
    [Parameter]
    public required string Name { get; set; }

    [Parameter]
    public required int StartYear { get; set; }

    [Parameter]
    public int? EndYear { get; set; }

    public int Duration => EndYear == null
        ? DateTime.Now.Year - StartYear
        : EndYear.Value - StartYear;

    public string DurationStr => EndYear == null
        ? $"{Duration}+ year{(Duration == 1 ? string.Empty : "s")}"
        : $"{Duration} year{(Duration == 1 ? string.Empty : "s")}";

    public string TimeframeStr => EndYear == null
        ? $"{StartYear}->"
        : $"{StartYear}-{EndYear}";
}
