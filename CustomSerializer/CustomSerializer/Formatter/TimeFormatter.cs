using System;
using System.IO;

namespace CustomSerializer
{
    public class TimeFormatter : CustFormatter<DateTime>
    {
        private const string Format = "hhmmss";

        protected override void WriteFile(StringWriter writer, DateTime obj, object properties)
        {
            writer.Write(obj.ToString(Format));
        }
    }
}
