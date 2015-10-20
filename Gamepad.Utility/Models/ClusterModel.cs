using System.Collections.Generic;

namespace Gamepad.Utility.Models
{
    public class Cluster<T>
    {
        public IEnumerable<T> List { get; set; }

        public int CountAll { get; set; }
    }
}
