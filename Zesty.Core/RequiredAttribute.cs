using System;
namespace Zesty.Core
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredAttribute : System.Attribute
    {
        public RequiredAttribute()
        {
        }
    }
}
