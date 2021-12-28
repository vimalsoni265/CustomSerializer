using System;
using System.IO;

namespace CustomSerializer
{
    public class DefaultFormatter : CustFormatter<object>
    {
        protected override void WriteFile(StringWriter writer, object obj, object properties)
        {
            var property = properties as PropertyAttribute;

            if (obj.ToString().Length > property.Length)
            {
                throw new FormatException("Property length is grater than define formatting length");
            }

            switch (property.Padding)
            {
                case Padding.Left:
                    writer.Write(obj.ToString().PadLeft(property.Length));
                    break;
                case Padding.Right:
                    writer.Write(obj.ToString().PadRight(property.Length));
                    break;
            }
        }
    }
}
