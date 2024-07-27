namespace WpfApp6.Models;

public class SharedData
{
    private static SharedData _instance;
    public static SharedData Instance => _instance ??= new SharedData();

    public List<ReportB> Reports
    {
        get; set;
    }

    private SharedData()
    {
        Reports = new List<ReportB>();
    }
}