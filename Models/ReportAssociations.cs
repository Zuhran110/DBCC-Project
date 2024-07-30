namespace WpfApp6.Models;

public static class ReportAssociations
{
    public static Dictionary<ReportB, (Project project, Store store)> ReportMap { get; } = new Dictionary<ReportB, (Project project, Store store)>();
}