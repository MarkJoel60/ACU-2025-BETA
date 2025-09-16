// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuartetOfTheYearAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Attribute for quartet of the year.</summary>
public sealed class PMQuartetOfTheYearAttribute(Type dateField) : PMDatePartAttribute(dateField)
{
  protected override object GetDatePartValue(DateTime dt)
  {
    return (object) PMQuartetOfTheYearAttribute.GetQuarterByDate(dt);
  }

  public static int GetQuarterByDate(DateTime dt) => (dt.Month - 1) / 3 + 1;
}
