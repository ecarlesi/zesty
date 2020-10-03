using System;
namespace Zesty.Core.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool LeftToRight { get; set; }
    }
}
