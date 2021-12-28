using System;
using System.IO;

namespace CustomSerializer
{
    public class DateFormatter : CustFormatter<DateTime>
    {
        private const string Format = "yyyyMMdd";

        protected override void WriteFile(StringWriter writer, DateTime obj, object properties)
        {
            writer.Write(obj.ToString(Format));
        }
    }
}
