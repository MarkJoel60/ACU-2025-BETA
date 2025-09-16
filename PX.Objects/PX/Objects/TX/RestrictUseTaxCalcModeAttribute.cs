// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.RestrictUseTaxCalcModeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public class RestrictUseTaxCalcModeAttribute : RestrictTaxCalcModeAttribute
{
  protected override string _restrictedCalcMode => "T";

  protected override bool _enableCalcModeField => true;

  protected override Type _ConditionSelect
  {
    get
    {
      return typeof (Select2<TaxZoneDet, InnerJoin<Tax, On<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, And<Tax.taxID, Equal<TaxZoneDet.taxID>>>>, Where<Tax.taxType, Equal<CSTaxType.use>>>);
    }
  }

  public RestrictUseTaxCalcModeAttribute(Type TaxZoneID, Type TaxCalcMode)
    : base(TaxZoneID, TaxCalcMode)
  {
  }

  public RestrictUseTaxCalcModeAttribute(Type TaxZoneID, Type TaxCalcMode, Type OrigModule)
    : base(TaxZoneID, TaxCalcMode, OrigModule)
  {
  }
}
