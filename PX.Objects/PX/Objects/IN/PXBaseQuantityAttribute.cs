// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXBaseQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class PXBaseQuantityAttribute : PXQuantityAttribute
{
  internal override INUnit ReadConversion(PXCache cache, object data)
  {
    INUnit inUnit = base.ReadConversion(cache, data);
    if (inUnit != null && inUnit.RecordID.HasValue)
    {
      inUnit = PXCache<INUnit>.CreateCopy(inUnit);
      inUnit.UnitMultDiv = inUnit.UnitMultDiv == "M" ? "D" : "M";
    }
    return inUnit;
  }

  public PXBaseQuantityAttribute()
  {
  }

  public PXBaseQuantityAttribute(Type keyField, Type resultField)
    : base(keyField, resultField)
  {
  }
}
