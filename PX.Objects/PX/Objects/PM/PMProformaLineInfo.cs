// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaLineInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Virtual Table used in Report</summary>
[PXCacheName("Proforma Line Info for AIA Report")]
public class PMProformaLineInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Gets or sets Proforma Reference Number.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>Gets or sets Proforma Line Number</summary>
  [PXDBInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  /// <summary>Gets or sets PreviousQty</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Previous Qty")]
  public Decimal? PreviousQty { get; set; }

  /// <summary>Gets or sets ChangeOrder Amount to date</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Amount To Date")]
  public virtual Decimal? ChangeOrderAmountToDate { get; set; }

  /// <summary>Gets or sets ChangeOrder Quantity to date</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Change Order Quantity To Date")]
  public virtual Decimal? ChangeOrderQtyToDate { get; set; }

  /// <summary>
  /// Gets or sets completed percent for lines with Quantity progress billing base
  /// </summary>
  [PXDecimal]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QuantityBaseCompleterdPct { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProformaLineInfo.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProformaLineInfo.lineNbr>
  {
  }

  public abstract class previousQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineInfo.previousQty>
  {
  }

  public abstract class changeOrderAmountToDate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineInfo.changeOrderAmountToDate>
  {
  }

  public abstract class changeOrderQtyToDate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineInfo.changeOrderQtyToDate>
  {
  }

  public abstract class quantityBaseCompleterdPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProformaLineInfo.quantityBaseCompleterdPct>
  {
  }
}
