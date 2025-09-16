// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Tools.DumpForDataContextExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Objects.Common.Tools;

public static class DumpForDataContextExtensions
{
  private static string[] _ignoredFields = new string[8]
  {
    "CreatedByID",
    "CreatedByScreenID",
    "CreatedDateTime",
    "LastModifiedByID",
    "LastModifiedByScreenID",
    "LastModifiedDateTime",
    "tstamp",
    "DeletedDatabaseRecord"
  };

  public static string DumpForDataContext(this object obj)
  {
    return obj.Dump(new Action<object, StringBuilder>(DumpForDataContextExtensions.DumpForDataContextForSingleObject));
  }

  private static void DumpForDataContextForSingleObject(object obj, StringBuilder sb)
  {
    if (obj == null)
      return;
    if (obj is PXResult pxResult)
    {
      List<string> values = new List<string>();
      StringBuilder[] stringBuilderArray = new StringBuilder[pxResult.TableCount];
      for (int index = 0; index < pxResult.TableCount; ++index)
      {
        values.Add(pxResult.GetItemType(index).Name);
        stringBuilderArray[index] = new StringBuilder();
        DumpForDataContextExtensions.DumpForDataContextForSingleObject(pxResult[index], stringBuilderArray[index]);
      }
      sb.AppendFormat("new PXResult<{0}>({1})", (object) string.Join(",", (IEnumerable<string>) values), (object) string.Join(",", (object[]) stringBuilderArray));
    }
    else
    {
      Type type = obj.GetType();
      PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
      sb.AppendFormat("new {0}()", (object) type.Name);
      sb.AppendLine();
      sb.AppendLine("{");
      foreach (PropertyInfo propertyInfo in properties)
      {
        object obj1 = propertyInfo.GetValue(obj, (object[]) null);
        if (obj1 != null && !((IEnumerable<string>) DumpForDataContextExtensions._ignoredFields).Contains<string>(propertyInfo.Name))
        {
          if (propertyInfo.PropertyType == typeof (string))
            sb.AppendFormat("{0} = \"{1}\"", (object) propertyInfo.Name, obj1);
          else if (propertyInfo.PropertyType == typeof (DateTime) || propertyInfo.PropertyType == typeof (DateTime?))
          {
            DateTime dateTime = (DateTime) obj1;
            sb.AppendFormat("{0} = new DateTime({1}, {2}, {3}, {4}, {5}, {6}, {7})", (object) propertyInfo.Name, (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute, (object) dateTime.Second, (object) dateTime.Millisecond);
          }
          else if (propertyInfo.PropertyType == typeof (Guid) || propertyInfo.PropertyType == typeof (Guid?))
          {
            Guid guid = (Guid) obj1;
            sb.AppendFormat("{0} = Guid.Parse(\"{1}\")", (object) propertyInfo.Name, (object) guid);
          }
          else if (propertyInfo.PropertyType == typeof (bool) || propertyInfo.PropertyType == typeof (bool?))
          {
            string str = (bool) obj1 ? "true" : "false";
            sb.AppendFormat("{0} = {1}", (object) propertyInfo.Name, (object) str);
          }
          else if (propertyInfo.PropertyType == typeof (Decimal) || propertyInfo.PropertyType == typeof (Decimal?))
            sb.AppendFormat("{0} = {1}m", (object) propertyInfo.Name, obj1);
          else
            sb.AppendFormat("{0} = {1}", (object) propertyInfo.Name, obj1);
          sb.AppendLine(",");
        }
      }
      sb.AppendLine("}");
    }
  }
}
