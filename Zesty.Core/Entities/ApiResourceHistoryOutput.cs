namespace Zesty.Core.Entities
{
    public class ApiResourceHistoryOutput
    {
        public ApiResourceHistoryPolicy ResourceHistoryPolicy { get; set; }
        public HistoryItem Item { get; set; }
    }
}