using System.ComponentModel;
using System.Reflection;

namespace CARGA.Utils.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<TEnum>(TEnum value) where TEnum : Enum
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (fieldInfo != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }
        
        public static Dictionary<int, string> ToDictionary<TEnum>() where TEnum : Enum
        {
            var enumType = typeof(TEnum);

            var result = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(enumType))
            {
                int intValue = (int)value;
                string name = GetDescription<TEnum>((TEnum)value);
                result[intValue] = name;
            }

            return result;
        }
    }
}
