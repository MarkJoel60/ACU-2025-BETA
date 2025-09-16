// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BalancedARDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARRegister, LeftJoin<ARDocumentRelease.ARInvoice, On<ARDocumentRelease.ARInvoice.docType, Equal<ARRegister.docType>, And<ARDocumentRelease.ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoin<ARDocumentRelease.ARPayment, On<ARDocumentRelease.ARPayment.docType, Equal<ARRegister.docType>, And<ARDocumentRelease.ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>>))]
[Serializable]
public class BalancedARDocument : ARRegister
{
  [PXDBString(1, IsFixed = true, BqlField = typeof (ARRegister.status))]
  [PXUIField]
  [ARDocStatus.List]
  public override 
  #nullable disable
  string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (ARDocumentRelease.ARInvoice.invoiceNbr))]
  public virtual string InvoiceNbr { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (ARDocumentRelease.ARPayment.extRefNbr))]
  public virtual string ExtRefNbr { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Order Nbr.")]
  [PXFormula(typeof (IsNull<BalancedARDocument.invoiceNbr, BalancedARDocument.extRefNbr>))]
  public string CustomerRefNbr { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (ARDocumentRelease.ARPayment.paymentMethodID))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBBaseCury(BqlField = typeof (ARRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public override Decimal? OrigDocAmt { get; set; }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedARDocument.status>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedARDocument.invoiceNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedARDocument.extRefNbr>
  {
  }

  public abstract class customerRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BalancedARDocument.customerRefNbr>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BalancedARDocument.paymentMethodID>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BalancedARDocument.origDocAmt>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalancedARDocument.branchID>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.selected>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedARDocument.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalancedARDocument.refNbr>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BalancedARDocument.origModule>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.openDoc>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.voided>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BalancedARDocument.createdByID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BalancedARDocument.lastModifiedByID>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BalancedARDocument.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BalancedARDocument.isTaxSaved>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalancedARDocument.customerID>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalancedARDocument.adjCntr>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BalancedARDocument.isMigratedRecord>
  {
  }
}
