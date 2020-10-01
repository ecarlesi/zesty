using System;
namespace Zesty.Core.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Label { get; set; }
        public bool IsPublic { get; set; }
        public bool RequireToken { get; set; }
    }
}
