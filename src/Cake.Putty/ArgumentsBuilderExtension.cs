using Cake.Core;
using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Cake.Putty
{
    /// <summary>
    /// Arguments builder
    /// </summary>
    public static class ArgumentsBuilderExtension
    {
        /// <summary>
        /// Appends all arguments from <paramref name="settings"/> and <paramref name="arguments"/>.
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <param name="arguments"></param>
        /// <param name="commands"></param>
        public static void AppendAll<TSettings>(this ProcessArgumentBuilder builder, IList<string> commands, TSettings settings, IList<string> arguments)
            where TSettings: AutoToolSettings, new()
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }
            if (settings == null)
            {
                settings = new TSettings();
            }
            foreach (var property in typeof(TSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (string argument in GetArgumentFromProperty(property, settings))
                {
                    if (!string.IsNullOrEmpty(argument))
                    {
                        builder.Append(argument);
                    }
                }
            }
            if (commands?.Count > 0 )
            {
                foreach (string command in commands)
                {
                    builder.Append(command);
                }
            }
            if (arguments != null)
            {
                foreach (string argument in arguments)
                {
                    builder.Append(argument);
                }
            }
        }

        /// <summary>
        /// Checks whether a type is nullable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets and processes <paramref name="property"/> value from <paramref name="settings"/>.
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="property"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetArgumentFromProperty<TSettings>(PropertyInfo property, TSettings settings)
            where TSettings : AutoToolSettings, new()
        {
            if (property.PropertyType == typeof(bool))
            {
                if (IsNullableType(property.PropertyType))
                {
                    yield return GetArgumentFromNullableBoolProperty(property, (bool?)property.GetValue(settings));
                }
                else
                {
                    yield return GetArgumentFromBoolProperty(property, (bool)property.GetValue(settings));
                }
            }
            else if (property.PropertyType == typeof(int?))
            {
                yield return GetArgumentFromNullableIntProperty(property, (int?)property.GetValue(settings));
            }
            else if (property.PropertyType.IsEnum || IsNullableType(property.PropertyType) && property.PropertyType.GenericTypeArguments[0].IsEnum)
            {
                yield return GetArgumentFromEnumProperty(property, property.GetValue(settings));
            }
            else if (property.PropertyType == typeof(string))
            {
                yield return GetArgumentFromStringProperty(property, (string)property.GetValue(settings));
            }
            else if (property.PropertyType == typeof(FilePath))
            {
                var filePath = (FilePath)property.GetValue(settings);
                string value = filePath?.FullPath;
                yield return GetArgumentFromStringProperty(property, value);
            }
            else if (property.PropertyType == typeof(string[]))
            {
                foreach (string arg in GetArgumentFromStringArrayProperty(property, (string[])property.GetValue(settings)))
                {
                    yield return arg;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromBoolProperty(PropertyInfo property, bool value)
        {
            return value ? $"-{GetPropertyName(property)}" : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableBoolProperty(PropertyInfo property, bool? value)
        {
            if (value.HasValue)
            {
                BoolParameterAttribute attribute = property.GetCustomAttribute<BoolParameterAttribute>();
                if (attribute == null)
                {
                    throw new Exception($"{property.Name} isn't attributed with BoolParameterAttribute");
                }
                string parameterValue = value.Value ? attribute.OnTrue: attribute.OnFalse;
                return "-" + parameterValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get arguments for a (nullable) enum.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromEnumProperty(PropertyInfo property, object value)
        {
            return value != null ? $"-{GetEnumName(property.PropertyType, value)}" : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromNullableIntProperty(PropertyInfo property, int? value)
        {
            return value.HasValue ? $"-{GetPropertyName(property)} {value.Value}" : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetArgumentFromStringArrayProperty(PropertyInfo property, string[] values)
        {
            if (values != null)
            {
                foreach (string value in values)
                {
                    yield return GetArgumentFromStringProperty(property, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetArgumentFromStringProperty(PropertyInfo property, string value)
        {
            return !string.IsNullOrEmpty(value) ? $"-{GetPropertyName(property)} {value}" : null;
        }

        /// <summary>
        /// Retrieves property name from <see cref="ParameterAttribute"/>.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyName(PropertyInfo property)
        {
            ParameterAttribute attribute = property.GetCustomAttribute<ParameterAttribute>();
            if (attribute == null)
            {
                throw new Exception($"{property.Name} isn't attributed with ParameterAttribute");
            }
            return attribute.Name;
        }
        /// <summary>
        /// Retrieves enum name from <see cref="ParameterAttribute"/>.
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumName(Type sourceType, object value)
        {
            Type enumType = IsNullableType(sourceType) ? sourceType.GenericTypeArguments[0] : sourceType;
            var member = enumType.GetMember(Convert.ToString(value)).Single();

            ParameterAttribute attribute = member.GetCustomAttribute<ParameterAttribute>();
            if (attribute == null)
            {
                throw new Exception($"{member.Name} isn't attributed with ParameterAttribute");
            }
            return attribute.Name;
        }
    }
}
