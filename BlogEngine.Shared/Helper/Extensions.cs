using BlogEngine.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Shared.Helper
{
    public static class Extensions
    {
        public static string GetValue(this Role type)
        {
            return GetValue<Role>(type);
        }

        private static string GetValue<T>(T type)
        {
            FieldInfo fi = type.GetType().GetField(type.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return type.ToString();
        }

    }
}
