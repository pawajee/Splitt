using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Duc.Splitt.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
                ? (T)attributes[0]
                : null;
        }
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static string GetDescriptionValue(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionWithValueAttribute>();
            return attribute == null ? value.ToString() : attribute.Value;
        }
        public static T GetValue<T>(this Enum enumObj)
        {
            return (T)Convert.ChangeType(enumObj, enumObj.GetTypeCode());
        }

        public static string GetSizeInMemory(this long bytesize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = Convert.ToDouble(bytesize);
            int order = 0;
            while (len >= 1024D && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return string.Format(CultureInfo.CurrentCulture, "{0:0.##} {1}", len, sizes[order]);
        }
    }

    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        readonly ResourceManager _resourceManager;
        readonly string _resourceKey;
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resourceManager = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string description = _resourceManager.GetString(_resourceKey);
                return string.IsNullOrWhiteSpace(description) ? $"[[{_resourceKey}]]" : description;
            }
        }
    }

    [AttributeUsageAttribute(AttributeTargets.All)]
    public class DescriptionWithValueAttribute : DescriptionAttribute
    {
        public DescriptionWithValueAttribute(string description, string value) : base(description)
        {
            this.Value = value;
        }
        public string Value { get; private set; }
    }
}
