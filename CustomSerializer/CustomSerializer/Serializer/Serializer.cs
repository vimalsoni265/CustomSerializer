using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CustomSerializer.Serializer
{
    public static class Serizalizer
    {
        private static ConcurrentDictionary<Type, Lazy<CustFormatter>> s_concurrentDictionary = new ConcurrentDictionary<Type, Lazy<CustFormatter>>();

        public static string Serialize<T>(T obj)
            where T : class
        {
            var result = new StringWriter();
            Serialize(result, obj);
            return result.ToString();
        }

        private static void Serialize(StringWriter result, object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object", "serializer unable to serialize null object");
            }

            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var sortedProperties = new SortedList<PropertyAttribute, PropertyInfo>();

            if (!(type.GetCustomAttribute(typeof(SerializableAttribute), true) is SerializableAttribute))
            {
                return;
            }

            foreach (var propertyInfo in properties)
            {
                var customPropertyAttribute = propertyInfo.GetCustomAttribute(typeof(PropertyAttribute), true) as PropertyAttribute;
                if (customPropertyAttribute != null)
                {
                    sortedProperties.Add(customPropertyAttribute, propertyInfo);
                }
            }

            foreach (var sortedPropertyInfo in sortedProperties)
            {
                var formatterAttribute = sortedPropertyInfo.Value.GetCustomAttribute(typeof(FormatterAttribute), true) as FormatterAttribute;
                CustFormatter formatter = null;

                if (formatterAttribute != null)
                {
                    formatter = GetCustFormatter(formatterAttribute.FormatterType);
                }
                else
                {
                    var classFormatterAttribute = type.GetCustomAttributes(typeof(FormatterAttribute), true).OfType<FormatterAttribute>();
                    foreach (var formatterType in classFormatterAttribute)
                    {
                        if (formatterType.BaseType.GetGenericArguments().First() == sortedPropertyInfo.Value.PropertyType)
                        {
                            formatter = GetCustFormatter(formatterType.FormatterType);
                            break;
                        }
                    }

                    if (formatter == null)
                    {
                        var serializableAttribute = sortedPropertyInfo.Value.PropertyType.GetCustomAttribute(typeof(SerializableAttribute), true) as SerializableAttribute;
                        if (serializableAttribute != null)
                        {
                            var currentObject = sortedPropertyInfo.Value.GetValue(obj);
                            if (currentObject == null)
                            {
                                currentObject = Activator.CreateInstance(sortedPropertyInfo.Value.PropertyType);
                            }
                            Serialize(result, currentObject);
                            continue;
                        }
                        else
                        {
                            formatter = GetCustFormatter(typeof(DefaultFormatter));
                        }

                    }
                }

                var propertyValue = sortedPropertyInfo.Value.GetValue(obj);
                var nullableObject = Nullable.GetUnderlyingType(sortedPropertyInfo.Value.PropertyType);
                if (propertyValue == null && nullableObject == null)
                {
                    propertyValue = Activator.CreateInstance(typeof(string), new object[] { string.Empty.ToCharArray() });
                }
                formatter.Write(result, propertyValue, sortedPropertyInfo.Key);
            }
        }

        private static CustFormatter GetCustFormatter(Type formatterType)
        {
            ValidateType(formatterType);
            return s_concurrentDictionary.GetOrAdd(formatterType, x => new Lazy<CustFormatter>(() => { return Activator.CreateInstance(formatterType) as CustFormatter; })).Value;
        }

        private static void ValidateType(Type formatterType)
        {
            if (formatterType == null)
            {
                throw new ArgumentNullException(nameof(formatterType));
            }
        }
    }
}
