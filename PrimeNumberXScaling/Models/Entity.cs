using System;
using System.Collections.Generic;

namespace Models
{
    public class Entity
    {
        public int? InstanceId { get; set; }
        public DateTime? Time { get; set; }
        public List<int> Numbers { get; set; }
    }
}
