using System;
using System.Collections.Generic;
using System.Text;

namespace CustomSerializer.Test
{
    public class RegularClass
    {
        public string Property1 { get; set; }
        public double Property2 { get; set; }
    }

    [Serializable]
    public class ClassA
    {
        [Property(position:1, length:2)]
        public string Property1 { get; set; }

        [Property(position: 1, length: 2)]
        public string Property2 { get; set; }
    }

    [Serializable]
    public class ClassB
    {
        [Property(position: 1, length: 2)]
        public string Property1 { get; set; }

        [Property(position: 2, length: 2)]
        public string Property2 { get; set; }
    }

    [Serializable]
    public class ClassC
    {
        [Property(position: 1, length: 2)]
        public string Property1 { get; set; }

        [Property(position: 2, length: 5)]
        public double Property2 { get; set; }
    }

    [Serializable]
    public class ClassD
    {
        [Property(position: 3, length: 2)]
        public string Property1 { get; set; }

        [Property(position: 2, length: 5)]
        public double Property2 { get; set; }

        [Property(position: 1, length: 8)]
        [Formatter(typeof(DateFormatter))]
        public DateTime Property3 { get; set; }
    }
}
