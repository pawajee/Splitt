using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Duc.Splitt.Common.Extensions
{
    public static class UtilityExtensions 
    {
        private static bool IsArabic { get; set; }

        // Method to get either 'TitleArabic' or 'TitleEnglish' based on 'IsArabic' flag
        public static string Lang(this object obj)
        {
            // Check for null object
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "The object cannot be null.");
            }

            // Use reflection to get the property names dynamically
            string propertyName = IsArabic ? "TitleArabic" : "TitleEnglish";

            // Get the property using reflection
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);

            // Check if the property exists
            if (propertyInfo == null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist on the type '{obj.GetType().Name}'.");
            }

            // Return the value of the property
            var value = propertyInfo.GetValue(obj)?.ToString(); // Get the value and convert to string

            return value;
        }
        public static string Lang(this object obj, bool isArabic)
        {
            IsArabic = isArabic;
            return Lang(obj);
        }
    }
}
