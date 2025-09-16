// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.RestrictWithholdingTaxCalcModeFromPOAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2019R2.")]
public class RestrictWithholdingTaxCalcModeFromPOAttribute : RestrictWithholdingTaxCalcModeAttribute
{
  public RestrictWithholdingTaxCalcModeFromPOAttribute(Type TaxZoneID, Type TaxCalcMode)
    : base(TaxZoneID, TaxCalcMode)
  {
  }

  public RestrictWithholdingTaxCalcModeFromPOAttribute(
    Type TaxZoneID,
    Type TaxCalcMode,
    Type OrigModule)
    : base(TaxZoneID, TaxCalcMode, OrigModule)
  {
  }

  protected override bool CheckCondition(PXCache sender, object row)
  {
    return base.CheckCondition(sender, row) && (string) sender.GetValue(row, this._OrigModule.Name) != "PO";
  }
}
