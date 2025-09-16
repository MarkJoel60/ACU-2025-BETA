// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXDBBaseQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class PXDBBaseQuantityAttribute : PXDBQuantityAttribute
{
  internal override ConversionInfo ReadConversionInfo(PXCache cache, object data)
  {
    ConversionInfo conversionInfo = base.ReadConversionInfo(cache, data);
    if (conversionInfo?.Conversion != null && conversionInfo.Conversion.FromUnit != conversionInfo.Conversion.ToUnit)
    {
      INUnit copy = PXCache<INUnit>.CreateCopy(conversionInfo.Conversion);
      copy.UnitMultDiv = conversionInfo.Conversion.UnitMultDiv == "M" ? "D" : "M";
      conversionInfo = new ConversionInfo(copy, conversionInfo.Inventory);
    }
    return conversionInfo;
  }

  public PXDBBaseQuantityAttribute()
  {
  }

  public PXDBBaseQuantityAttribute(Type keyField, Type resultField)
    : base(keyField, resultField)
  {
  }
}
