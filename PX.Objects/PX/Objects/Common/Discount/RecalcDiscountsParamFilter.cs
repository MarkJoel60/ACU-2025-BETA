// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.RecalcDiscountsParamFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.Discount;

/// <summary>Recalculate Prices and Discounts filter</summary>
[Serializable]
public class RecalcDiscountsParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string CurrentLine = "LNE";
  public const string AllLines = "ALL";

  [PXDBString(3, IsFixed = true)]
  [PXDefault("ALL")]
  [PXStringList(new string[] {"LNE", "ALL"}, new string[] {"Current Line", "All Lines"})]
  [PXUIField(DisplayName = "Recalculate")]
  public virtual string RecalcTarget { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Set Current Unit Prices", Visible = true)]
  public virtual bool? RecalcUnitPrices { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (RecalcDiscountsParamFilter.recalcUnitPrices))]
  [PXFormula(typeof (Switch<Case<Where<RecalcDiscountsParamFilter.recalcUnitPrices, Equal<False>>, False>, RecalcDiscountsParamFilter.overrideManualPrices>))]
  [PXUIField(DisplayName = "Override Manual Prices", Visible = true)]
  public virtual bool? OverrideManualPrices { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Discounts")]
  public virtual bool? RecalcDiscounts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (RecalcDiscountsParamFilter.recalcDiscounts))]
  [PXFormula(typeof (Switch<Case<Where<RecalcDiscountsParamFilter.recalcDiscounts, Equal<False>>, False>, RecalcDiscountsParamFilter.overrideManualDiscounts>))]
  [PXUIField(DisplayName = "Override Manual Line Discounts")]
  public virtual bool? OverrideManualDiscounts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (RecalcDiscountsParamFilter.recalcDiscounts))]
  [PXFormula(typeof (Switch<Case<Where<RecalcDiscountsParamFilter.recalcDiscounts, Equal<False>>, False>, RecalcDiscountsParamFilter.overrideManualDocGroupDiscounts>))]
  [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
  public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (RecalcDiscountsParamFilter.recalcDiscounts))]
  [PXFormula(typeof (Switch<Case<Where<RecalcDiscountsParamFilter.recalcDiscounts, Equal<False>>, False>, RecalcDiscountsParamFilter.calcDiscountsOnLinesWithDisabledAutomaticDiscounts>))]
  [PXUIField(DisplayName = "Include Lines With Disabled Automatic Discounts")]
  public virtual bool? CalcDiscountsOnLinesWithDisabledAutomaticDiscounts { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? UseRecalcFilter { get; set; }

  public abstract class recalcTarget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.recalcTarget>
  {
  }

  public abstract class recalcUnitPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.recalcUnitPrices>
  {
  }

  public abstract class overrideManualPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.overrideManualPrices>
  {
  }

  public abstract class recalcDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.recalcDiscounts>
  {
  }

  public abstract class overrideManualDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.overrideManualDiscounts>
  {
  }

  public abstract class overrideManualDocGroupDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.overrideManualDocGroupDiscounts>
  {
  }

  public abstract class calcDiscountsOnLinesWithDisabledAutomaticDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.calcDiscountsOnLinesWithDisabledAutomaticDiscounts>
  {
  }

  public abstract class useRecalcFilter : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RecalcDiscountsParamFilter.useRecalcFilter>
  {
  }
}
