﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CleanArchitechture.Application.Common.Extensions;

public static class UtilityExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var member = enumValue
             .GetType()
             .GetMember(enumValue.ToString())
             .FirstOrDefault();

        var displayAttribute = member?
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        return displayAttribute?.GetName() ?? nameof(enumValue);
    }

    public static string SplitWordByUppper(this string value)
    {
        return Regex.Replace(value, "(\\B[A-Z])", " $1").Trim();
    }

    public static IEnumerable<string> GetColumnNameFromClass(Type model)
    {
        PropertyInfo[] properties = model.GetProperties();

        return properties.Select(prop => prop.Name);
    }

    public static List<SelectListModel<int>> GetActiveInactiveSelectList() =>
        [
            new() {Id = 1, Name = "Active", Severity = "success"},
            new() {Id = 0, Name = "Inactive", Severity = "danger"}
        ];
}

