namespace Zesty.Core.Entities
{
    public class HistoryItem
    {
        public User User { get; set; }
        public string Resource { get; set; }
        public string Text { get; set; }
        public string Actor { get; set; }
    }
}