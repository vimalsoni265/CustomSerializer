using System;

namespace CustomSerializer
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class FormatterAttribute : Attribute
    {
        private readonly Type _formatterType;
        public Type FormatterType => _formatterType;
        public Type BaseType => this.BaseType;

        public FormatterAttribute(Type convertorType)
        {
            _formatterType = convertorType ?? throw new ArgumentNullException(nameof(convertorType));
        }
    }
}
