namespace WpfApp6.Models;

public class SharedData
{
    private static readonly Lazy<SharedData> _instance = new Lazy<SharedData>(() => new SharedData());
    public static SharedData Instance => _instance.Value;

    public List<ReportB> Reports
    {
        get; set;
    }

    public Dictionary<ReportB, (Project Project, Store Store)> ReportAssociations
    {
        get; set;
    }

    private SharedData()
    {
        Reports = new List<ReportB>();
        ReportAssociations = new Dictionary<ReportB, (Project Project, Store Store)>();
    }
}