// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DaylightSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class DaylightSelectorAttribute : PXCustomSelectorAttribute
{
  public DaylightSelectorAttribute()
    : base(typeof (Year.nbr), new Type[1]
    {
      typeof (Year.nbr)
    })
  {
  }

  public IEnumerable GetRecords()
  {
    int year = DateTime.Today.Year;
    int num = year - 30;
    int end = year + 30;
    for (int i = num; i < end; ++i)
      yield return (object) new Year() { Nbr = new int?(i) };
  }
}
