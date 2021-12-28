using System;
using System.Collections.Generic;

namespace CustomSerializer
{
    public class PropertyAttribute : Attribute, IComparer<PropertyAttribute>, IComparable<PropertyAttribute>
    {
        public int Position { get; private set; }
        public int Length { get; private set; }
        public int Precision { get; private set; }
        public Padding Padding { get; }

        public PropertyAttribute(int position)
        {
            Position = position;
            Padding = Padding.Right;
        }

        public PropertyAttribute(int position, int length, int precision = 0, Padding padding = Padding.Right)
            : this(position)
        {
            Length = length;
            Padding = padding;
            Precision = precision;
        }

        public int Compare(PropertyAttribute x, PropertyAttribute y)
        {
            return x.Position.CompareTo(y.Position);
        }

        public int CompareTo(PropertyAttribute other)
        {
            return Position.CompareTo(other.Position);
        }

        public void SetPosition(int position)
        {
            Position = position;
        }

        public void SetLength(int length)
        {
            Length = length;
        }
    }
}
