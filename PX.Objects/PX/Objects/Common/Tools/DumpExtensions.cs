// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Tools.DumpExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Objects.Common.Tools;

public static class DumpExtensions
{
  private static string[] ignoredFields = new string[9]
  {
    "NoteID",
    "CreatedByID",
    "CreatedByScreenID",
    "CreatedDateTime",
    "LastModifiedByID",
    "LastModifiedByScreenID",
    "LastModifiedDateTime",
    "tstamp",
    "AssignDate"
  };
  private const string Indent = "    ";

  public static string Dump(this object obj, Action<object, StringBuilder> dumpSingleObject)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(obj.ToString());
    if (obj is IEnumerable enumerable)
    {
      stringBuilder.AppendLine("{");
      foreach (object obj1 in enumerable)
      {
        dumpSingleObject(obj1, stringBuilder);
        stringBuilder.Append(",");
      }
      stringBuilder.AppendLine("}");
    }
    else
      dumpSingleObject(obj, stringBuilder);
    return stringBuilder.ToString();
  }

  public static string Dump(this object obj)
  {
    return obj.Dump((Action<object, StringBuilder>) ((ob, sb) => ob.DumpForSingleObject(sb)));
  }

  public static string DumpForSingleObject(
    this object obj,
    StringBuilder sb,
    int getFullImageStackLevel = -1)
  {
    ++getFullImageStackLevel;
    PropertyInfo[] array = ((IEnumerable<PropertyInfo>) obj.GetType().GetProperties()).OrderBy<PropertyInfo, string>((Func<PropertyInfo, string>) (pi => pi.Name)).ToArray<PropertyInfo>();
    string indent1 = DumpExtensions.GetIndent(getFullImageStackLevel);
    string indent2 = DumpExtensions.GetIndent(getFullImageStackLevel + 1);
    sb.AppendLine();
    sb.AppendLine(indent1 + "{");
    foreach (PropertyInfo propertyInfo1 in array)
    {
      PropertyInfo propertyInfo = propertyInfo1;
      if (!((IEnumerable<string>) DumpExtensions.ignoredFields).Any<string>((Func<string, bool>) (fieldName => propertyInfo.Name.Contains(fieldName))))
      {
        object obj1 = propertyInfo.GetValue(obj, (object[]) null);
        if (obj1 != null)
        {
          if (obj1.IsComplex())
            obj1 = (object) obj1.DumpForSingleObject(sb, getFullImageStackLevel + 1);
          if (obj1 is string)
            obj1 = (object) $"\"{obj1}\"";
          if (obj1 is Decimal || obj1 is Decimal?)
            obj1 = (object) ((Decimal) obj1).ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
          obj1 = (object) "null";
        sb.AppendLine($"{indent2}{propertyInfo.Name}: {obj1}");
      }
    }
    sb.AppendLine(indent1 + "}");
    return sb.ToString();
  }

  private static string GetIndent(int level)
  {
    return string.Join(string.Empty, Enumerable.Repeat<string>("    ", level));
  }

  public static string DumpAsTable<TItem>(this IReadOnlyCollection<TItem> items, PXCache cache)
  {
    PropertyInfo[] array1 = ((IEnumerable<PropertyInfo>) typeof (TItem).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (info => !((IEnumerable<string>) DumpExtensions.ignoredFields).Contains<string>(info.Name))).ToArray<PropertyInfo>();
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    foreach (PropertyInfo propertyInfo in array1)
    {
      int num = 0;
      if (typeof (Decimal?).IsAssignableFrom(propertyInfo.PropertyType))
        num = Decimal.MinValue.ToString((IFormatProvider) CultureInfo.InvariantCulture).Length;
      else if (typeof (int?).IsAssignableFrom(propertyInfo.PropertyType))
        num = int.MinValue.ToString((IFormatProvider) CultureInfo.InvariantCulture).Length;
      else if (typeof (bool?).IsAssignableFrom(propertyInfo.PropertyType))
      {
        num = 5;
      }
      else
      {
        if (!(propertyInfo.PropertyType == typeof (string)))
          throw new Exception("Unexpected type");
        PXDBStringAttribute pxdbStringAttribute = cache.GetAttributes(propertyInfo.Name).OfType<PXDBStringAttribute>().SingleOrDefault<PXDBStringAttribute>();
        if (pxdbStringAttribute != null)
        {
          num = pxdbStringAttribute.Length;
        }
        else
        {
          PXStringAttribute pxStringAttribute = cache.GetAttributes(propertyInfo.Name).OfType<PXStringAttribute>().SingleOrDefault<PXStringAttribute>();
          if (pxStringAttribute != null)
            num = pxStringAttribute.Length;
        }
      }
      dictionary[propertyInfo.Name] = num;
    }
    KeyValuePair<string, int>[] array2 = dictionary.Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>) (kvp => kvp.Value == 0)).ToArray<KeyValuePair<string, int>>();
    string[] array3 = ((IEnumerable<PropertyInfo>) array1).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (info => info.Name)).ToArray<string>();
    if (!((IEnumerable<KeyValuePair<string, int>>) array2).Any<KeyValuePair<string, int>>())
      return items.DumpAsTable<TItem>(cache, array3, dictionary);
    foreach (TItem obj in (IEnumerable<TItem>) items)
    {
      foreach (KeyValuePair<string, int> keyValuePair in array2)
      {
        int? length = cache.GetValue((object) obj, keyValuePair.Key)?.ToString().Length;
        int? nullable = length;
        int num = dictionary[keyValuePair.Key];
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          dictionary[keyValuePair.Key] = length.Value;
      }
    }
    return items.DumpAsTable<TItem>(cache, array3, dictionary);
  }

  private static string DumpAsTable<TItem>(
    this IEnumerable<TItem> items,
    PXCache cache,
    string[] origFieldList,
    Dictionary<string, int> maxValueLengths)
  {
    Dictionary<string, int> dictionary = new Dictionary<string, int>((IDictionary<string, int>) maxValueLengths);
    StringBuilder stringBuilder = new StringBuilder();
    List<string> list = cache.BqlKeys.Select<Type, string>((Func<Type, string>) (t => t.Name.Capitalize())).ToList<string>();
    list.AddRange(((IEnumerable<string>) origFieldList).Except<string>((IEnumerable<string>) list));
    stringBuilder.Append("|");
    foreach (string key in list)
    {
      if (key.Length > dictionary[key])
        dictionary[key] = key.Length;
      stringBuilder.Append(key.PadRight(dictionary[key], ' '));
      stringBuilder.Append("|");
    }
    stringBuilder.AppendLine();
    foreach (TItem obj1 in items)
    {
      stringBuilder.Append("|");
      foreach (string key in list)
      {
        object obj2 = cache.GetValue((object) obj1, key);
        stringBuilder.Append((obj2?.ToString() ?? string.Empty).PadRight(dictionary[key], ' '));
        stringBuilder.Append("|");
      }
      stringBuilder.AppendLine();
    }
    return stringBuilder.ToString();
  }
}
