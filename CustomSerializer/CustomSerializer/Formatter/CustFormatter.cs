using System.IO;

namespace CustomSerializer
{
    public abstract class CustFormatter
    {
        public abstract void Write(StringWriter writer, object obj, object properties);
    }

    public abstract class CustFormatter<T> : CustFormatter
    {
        public sealed override void Write(StringWriter writer, object obj, object properties)
        {
            WriteFile(writer, (T)obj, properties);
        }

        protected abstract void WriteFile(StringWriter writer, T obj, object properties);
    }
}
