using System;
namespace Zesty.Core.Entities
{
    public class Domain
    {
        public Guid Id { get; set; }
        public Guid ParentDomainId { get; set; }
        public string Name { get; set; }
    }
}
