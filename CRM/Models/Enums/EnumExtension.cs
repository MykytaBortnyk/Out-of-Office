using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CRM.Models.Enums
{
    public static class EnumExtension
    {

        /// <summary>
        /// Deprecated in future virsion of FluentIU. Issue: <see href="https://github.com/microsoft/fluentui-blazor/issues/2300"/>
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue) =>
            enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            .GetName();
    }
}
