// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.SMScaleWeightConversion
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SM;

/// <exclude />
public sealed class SMScaleWeightConversion : PXCacheExtension<
#nullable disable
SMScale>
{
  [INUnboundUnit(DisplayName = "UOM", Enabled = false)]
  [PXFormula(typeof (BqlField<CommonSetup.weightUOM, IBqlString>.FromSetup))]
  public string CompanyUOM { get; set; }

  [INUnitConvert(typeof (SMScale.lastWeight), typeof (SMScale.uOM), typeof (SMScaleWeightConversion.companyUOM), DisplayName = "Last Weight", Enabled = false)]
  public Decimal? CompanyLastWeight { get; set; }

  public abstract class companyUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScaleWeightConversion.companyUOM>
  {
  }

  public abstract class companyLastWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SMScaleWeightConversion.companyLastWeight>
  {
  }
}
