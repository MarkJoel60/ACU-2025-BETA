// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.ScaleWeightConversionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.SM;

public class ScaleWeightConversionExt : PXGraphExtension<ScaleMaint>
{
  protected virtual void _(Events.RowSelected<SMScale> e)
  {
    if (e.Row == null || ((PXGraph) this.Base).IsImport)
      return;
    this.RaiseConversionError(e.Row);
  }

  protected virtual void RaiseConversionError(SMScale scale)
  {
    SMScaleWeightConversion extension = PXCacheEx.GetExtension<SMScaleWeightConversion>((IBqlTable) scale);
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    if (extension == null || extension.CompanyUOM == null)
      propertyException1 = new PXSetPropertyException("Default values for weight UOM and volume UOM are not specified on the Companies (CS101500) form.");
    else if (scale.LastWeight.HasValue && !extension.CompanyLastWeight.HasValue)
      propertyException2 = new PXSetPropertyException("No rule for converting the {0} unit of measure to the {1} unit of measure has been set up on the Units of Measure (CS203500) form.", new object[2]
      {
        (object) scale.UOM,
        (object) extension.CompanyUOM
      });
    ((PXSelectBase) this.Base.Scale).Cache.RaiseExceptionHandling<SMScaleWeightConversion.companyUOM>((object) scale, (object) extension.CompanyUOM, (Exception) propertyException1);
    ((PXSelectBase) this.Base.Scale).Cache.RaiseExceptionHandling<SMScaleWeightConversion.companyLastWeight>((object) scale, (object) extension.CompanyLastWeight, (Exception) propertyException2);
  }
}
