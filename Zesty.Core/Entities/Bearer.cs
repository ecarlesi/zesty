using System;
namespace Zesty.Core.Entities
{
    public class Bearer
    {
        public long Exp { get; set; }
        public Entities.User User { get; set; }
    }
}
