// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SOQuickPrepaymentInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>Parameters to create prepayment invoice</summary>
[PXVirtual]
[PXCacheName("SO Quick Prepayment Invoice")]
public class SOQuickPrepaymentInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the sales order.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Currency.CuryID" /> field.
  /// </value>
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated
  /// with the sales order.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.SOQuickPrepaymentInvoice.CuryInfoID" /> field.
  /// </value>
  [PXLong]
  [CurrencyInfo(ModuleCode = "AR")]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The unbilled balance of the sales order, which is used as a base for CuryPrepaymentAmt
  /// and PrepaymentPct calculation (in the currency of the document).
  /// </summary>
  [PXCurrency(typeof (SOQuickPrepaymentInvoice.curyInfoID), typeof (SOQuickPrepaymentInvoice.origDocAmt))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  /// <summary>
  /// The unbilled balance of the sales order, which is used as a base for PrepaymentAmt
  /// and PrepaymentPct calculation (in the base currency).
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  /// <summary>
  /// Percent of the Sales Order amount on which a PPI document will be created.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.00")]
  [PXUIField]
  public virtual Decimal? PrepaymentPct { get; set; }

  /// <summary>
  /// Part of the Sales Order amount on which a PPI document will be created.
  /// Given in the <see cref="T:PX.Objects.AR.SOQuickPrepaymentInvoice.curyID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (SOQuickPrepaymentInvoice.curyInfoID), typeof (SOQuickPrepaymentInvoice.prepaymentAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryPrepaymentAmt { get; set; }

  /// <summary>
  /// Part of the sales order amount on which a PPI document will be created.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? PrepaymentAmt { get; set; }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPrepaymentInvoice.curyID>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.origDocAmt>
  {
  }

  public abstract class prepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.prepaymentPct>
  {
  }

  public abstract class curyPrepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.curyPrepaymentAmt>
  {
  }

  public abstract class prepaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPrepaymentInvoice.prepaymentAmt>
  {
  }
}
