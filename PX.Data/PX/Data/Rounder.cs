// Decompiled with JetBrains decompiler
// Type: PX.Data.Rounder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class Rounder
{
  public static object Round(object val, int digits)
  {
    if (val == null || digits < -1 || digits > 15)
      return (object) null;
    if (val is int || val is string || digits == -1)
      return val;
    switch (System.Type.GetTypeCode(val.GetType()))
    {
      case TypeCode.Double:
        return (object) System.Math.Round((double) val, digits, MidpointRounding.AwayFromZero);
      case TypeCode.Decimal:
        return (object) System.Math.Round((Decimal) val, digits, MidpointRounding.AwayFromZero);
      default:
        throw new PXArgumentException();
    }
  }
}
