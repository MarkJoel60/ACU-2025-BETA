// Decompiled with JetBrains decompiler
// Type: PX.Data.DataSourceTypeProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class DataSourceTypeProvider
{
  internal const char _TYPE_NAME_SEPARATOR = ',';
  private static readonly Dictionary<string, bool> _checkedTypes = new Dictionary<string, bool>();
  private static readonly object _syncObj = new object();

  [PXInternalUseOnly]
  public static string GetTypeName(string types)
  {
    string[] strArray1;
    if (!string.IsNullOrEmpty(types))
      strArray1 = types.Split(',');
    else
      strArray1 = new string[0];
    string[] strArray2 = strArray1;
    if (strArray2.Length != 0)
    {
      lock (DataSourceTypeProvider._syncObj)
      {
        foreach (string str in strArray2)
        {
          string typeName = str.Trim();
          if (!string.IsNullOrEmpty(typeName))
          {
            bool flag;
            if (DataSourceTypeProvider._checkedTypes.TryGetValue(typeName, out flag))
            {
              if (flag)
                return typeName;
            }
            else
            {
              flag = PXBuildManager.GetType(typeName, false) != (System.Type) null;
              DataSourceTypeProvider._checkedTypes.Add(typeName, flag);
              if (flag)
                return typeName;
            }
          }
        }
      }
    }
    return (string) null;
  }
}
