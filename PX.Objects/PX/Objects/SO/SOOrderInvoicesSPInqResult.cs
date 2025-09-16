// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderInvoicesSPInqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// The DAC that represent the result line of the invoices for the sales orders side panel inquiry of the sales orders form.
/// </summary>
[PXCacheName("SO Order Invoices Inquiry Result")]
public class SOOrderInvoicesSPInqResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.SO.SOInvoice.DocType" />
  [PXString(3, IsKey = true, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOInvoice.RefNbr" />
  [PXString(15, IsUnicode = true, IsKey = true)]
  [PXSelector(typeof (Search<SOInvoice.refNbr, Where<SOInvoice.docType, Equal<Current<SOOrderInvoicesSPInqResult.docType>>>>))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public virtual string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.Status" />
  [PXString(1, IsFixed = true)]
  [ARDocStatus.List]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.DocDate" />
  [PXDate]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? DocDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CustomerID" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), Filterable = true)]
  public virtual int? CustomerID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.DueDate" />
  [PXDate]
  [PXUIField(DisplayName = "Due Date")]
  public virtual DateTime? DueDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.FinPeriodID" />
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryOrigDocAmt" />
  [PXCury(typeof (SOOrderInvoicesSPInqResult.curyID))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryDocBal" />
  [PXCury(typeof (SOOrderInvoicesSPInqResult.curyID))]
  [PXUIField(DisplayName = "Balance")]
  public virtual Decimal? CuryDocBal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryID" />
  [PXString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderType" />
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  public virtual string OrderType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderNbr" />
  [PXString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOOrderInvoicesSPInqResult.orderType>>>>))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderInvoicesSPInqResult.refNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderInvoicesSPInqResult.status>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.docDate>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.customerID>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.dueDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.finPeriodID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.curyOrigDocAmt>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.curyDocBal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderInvoicesSPInqResult.curyID>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPInqResult.orderNbr>
  {
  }
}
