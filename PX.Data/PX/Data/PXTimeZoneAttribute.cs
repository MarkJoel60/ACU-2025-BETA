// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTimeZoneAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class PXTimeZoneAttribute(bool allowEmpty = true) : PXStringListAttribute(allowEmpty ? EnumerableExtensions.Prepend<string>(PXTimeZoneAttribute._values, string.Empty) : PXTimeZoneAttribute._values, allowEmpty ? EnumerableExtensions.Prepend<string>(PXTimeZoneAttribute._labels, string.Empty) : PXTimeZoneAttribute._labels)
{
  private static readonly string[] _values;
  private static readonly string[] _labels;

  public override bool IsLocalizable => false;

  static PXTimeZoneAttribute()
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    List<PXTimeZoneInfo> pxTimeZoneInfoList = new List<PXTimeZoneInfo>((IEnumerable<PXTimeZoneInfo>) PXTimeZoneInfo.GetSystemTimeZones());
    pxTimeZoneInfoList.Sort((Comparison<PXTimeZoneInfo>) ((x, y) =>
    {
      int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
      if (num == 0)
        num = x.DisplayName.CompareTo(y.DisplayName);
      return num;
    }));
    foreach (PXTimeZoneInfo pxTimeZoneInfo in pxTimeZoneInfoList)
    {
      stringList1.Add(pxTimeZoneInfo.Id);
      stringList2.Add(pxTimeZoneInfo.DisplayName);
    }
    PXTimeZoneAttribute._values = stringList1.ToArray();
    PXTimeZoneAttribute._labels = stringList2.ToArray();
  }
}
