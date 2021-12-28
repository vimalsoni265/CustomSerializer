using System;
using System.IO;

namespace CustomSerializer
{
    public class DoubleFormatter : CustFormatter<double>
    {
        protected override void WriteFile(StringWriter writer, double obj, object properties)
        {
            var property = properties as PropertyAttribute;
            var wholeNumber = property.Length - property.Precision;
            var decimalPlaces = property.Precision;

            if (Convert.ToInt32(obj).ToString().Length > wholeNumber)
            {
                throw new ArithmeticException($"Value {obj} will be exceeded desire length of {property.Length}");
            }

            var result = obj
                .ToString($"{new string('#', wholeNumber)}.{new string('#', decimalPlaces)}")
                .Replace(".", string.Empty)
                .PadLeft(property.Length, '0');

            writer.Write(result);
        }
    }
}
