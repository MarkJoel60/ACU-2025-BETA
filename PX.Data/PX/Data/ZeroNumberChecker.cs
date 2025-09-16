// Decompiled with JetBrains decompiler
// Type: PX.Data.ZeroNumberChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class ZeroNumberChecker
{
  /// <exclude />
  public static object GetZeroValue(object val)
  {
    if (val == null)
      return (object) null;
    switch (System.Type.GetTypeCode(val.GetType()))
    {
      case TypeCode.Double:
        return (object) 0.0;
      case TypeCode.Decimal:
        return (object) 0.0M;
      default:
        return (object) 0;
    }
  }

  /// <exclude />
  public static bool IsZeroValue(object val)
  {
    return val != null && val.Equals(ZeroNumberChecker.GetZeroValue(val));
  }
}
