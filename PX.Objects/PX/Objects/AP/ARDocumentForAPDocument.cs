// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ARDocumentForAPDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<PX.Objects.AR.ARInvoice, InnerJoin<ARDocumentCustomer, On<ARDocumentCustomer.customerID, Equal<PX.Objects.AR.ARInvoice.customerID>>, InnerJoin<ARDocumentVendor, On<ARDocumentVendor.branchID, Equal<PX.Objects.AR.ARInvoice.branchID>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<PX.Objects.AR.ARInvoice.projectID>>, LeftJoin<IntercompanyBill, On<IntercompanyBill.intercompanyInvoiceNoteID, Equal<PX.Objects.AR.ARInvoice.noteID>>>>>>, Where<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.voided, Equal<False>, And<PX.Objects.AR.ARInvoice.isMigratedRecord, Equal<False>, And<PX.Objects.AR.ARInvoice.isRetainageDocument, Equal<False>, And<PX.Objects.AR.ARInvoice.retainageApply, Equal<False>, And<PX.Objects.AR.ARInvoice.docType, In3<ARDocType.invoice, ARDocType.creditMemo, ARDocType.debitMemo>, And2<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>>.Or<BqlOperand<PX.Objects.AR.ARInvoice.pendingPPD, IBqlBool>.IsNotEqual<True>>>, And<PX.Objects.AR.ARInvoice.masterRefNbr, PX.Data.IsNull, And<PX.Objects.AR.ARInvoice.installmentNbr, PX.Data.IsNull, And<IntercompanyBill.refNbr, PX.Data.IsNull, And<PX.Objects.AR.ARInvoice.isHiddenInIntercompanySales, NotEqual<True>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
[DebuggerDisplay("DocType = {DocType}, RefNbr = {RefNbr}")]
[Serializable]
public class ARDocumentForAPDocument : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDefault]
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARInvoice.docType))]
  [ARInvoiceType.List]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (PX.Objects.AR.ARInvoice.branchID))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The status of the document.
  /// The value of the field is determined by the values of the status flags,
  /// such as <see cref="!:Hold" />, <see cref="!:Released" />, <see cref="!:Voided" />, <see cref="!:Scheduled" />.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
  /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARInvoice.status))]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  [ARDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARInvoice.docDate))]
  [PXUIField(DisplayName = "Doc. Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? DocDate { get; set; }

  /// <summary>
  /// <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARRegister.DocDate" /> belongs, but can be overriden by user.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (PX.Objects.AR.ARInvoice.finPeriodID))]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual string FinPeriodID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARInvoice.customerID))]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "Purchaser ID")]
  public virtual int? CustomerID { get; set; }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, BqlField = typeof (ARDocumentCustomer.customerCD))]
  [PXUIField(DisplayName = "Purchaser", Visibility = PXUIVisibility.Visible)]
  public virtual string CustomerCD { get; set; }

  [PXDBInt(BqlField = typeof (ARDocumentVendor.vendorID))]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, DisplayName = "Seller ID")]
  public virtual int? VendorID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (ARDocumentVendor.vendorCD))]
  [PXUIField(DisplayName = "Seller", Visibility = PXUIVisibility.Visible)]
  public virtual string VendorCD { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt))]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBString(5, BqlField = typeof (PX.Objects.AR.ARInvoice.curyID))]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.Visible)]
  public virtual string CuryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.docDesc))]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
  public virtual string DocDesc { get; set; }

  /// <summary>
  /// The original reference number or ID assigned by the customer to the customer document.
  /// </summary>
  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.invoiceNbr))]
  [PXUIField(DisplayName = "Customer Order Nbr.", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual string InvoiceNbr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Terms">Credit Terms</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AR.Customer.TermsID">credit terms</see> that are selected for the <see cref="P:PX.Objects.AP.ARDocumentForAPDocument.CustomerID">customer</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CS.Terms.TermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.termsID))]
  [PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  [ARTermsSelector]
  [Terms(typeof (PX.Objects.AR.ARInvoice.docDate), typeof (PX.Objects.AR.ARInvoice.dueDate), typeof (PX.Objects.AR.ARInvoice.discDate), typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt), typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt), typeof (PX.Objects.AR.ARInvoice.curyTaxTotal), typeof (PX.Objects.AR.ARInvoice.branchID))]
  public virtual string TermsID { get; set; }

  /// <summary>The due date of the document.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARInvoice.dueDate))]
  [PXUIField(DisplayName = "Due Date", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual System.DateTime? DueDate { get; set; }

  /// <summary>
  /// The date when the cash discount can be taken in accordance with the <see cref="P:PX.Objects.AR.ARInvoice.TermsID">credit terms</see>.
  /// </summary>
  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARInvoice.discDate))]
  [PXUIField(DisplayName = "Cash Discount Date", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual System.DateTime? DiscDate { get; set; }

  /// <summary>
  /// The open balance of the document.
  /// Given in the <see cref="P:PX.Objects.AP.ARDocumentForAPDocument.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBDecimal(BqlField = typeof (PX.Objects.AR.ARInvoice.curyDiscBal))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual Decimal? CuryDocBal { get; set; }

  /// <summary>
  /// The total amount of tax associated with the document.
  /// Given in the <see cref="P:PX.Objects.AP.ARDocumentForAPDocument.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBDecimal(BqlField = typeof (PX.Objects.AR.ARInvoice.curyTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The cash discount entered for the document.
  /// Given in the <see cref="P:PX.Objects.AP.ARDocumentForAPDocument.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBDecimal(BqlField = typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt))]
  [PXUIField(DisplayName = "Cash Discount", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  public virtual Decimal? CuryOrigDiscAmt { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARInvoice.projectID))]
  [PXUIField(DisplayName = "Project ID", Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual int? ProjectID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PMProject.contractCD))]
  [PXUIField(DisplayName = "Project", Visibility = PXUIVisibility.Visible)]
  public virtual string ProjectCD { get; set; }

  [PXDBGuid(false, BqlField = typeof (IntercompanyBill.intercompanyInvoiceNoteID))]
  [PXUIField(DisplayName = "AP Document", FieldClass = "InterBranch", Enabled = false)]
  [DocumentSelector(typeof (SearchFor<APInvoice.noteID>.In<SelectFrom<APInvoice>>), SubstituteKey = typeof (APInvoice.documentKey), SelectorMode = PXSelectorMode.TextModeReadonly | PXSelectorMode.DisplayModeValue)]
  public virtual Guid? IntercompanyAPDocumentNoteID { get; set; }

  /// <inheritdoc cref="T:PX.Objects.AR.ARInvoice.noteID" />
  [PXNote(BqlField = typeof (PX.Objects.AR.ARInvoice.noteID))]
  public virtual Guid? NoteID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDocumentForAPDocument.selected>
  {
  }

  /// <exclude />
  public class PK : 
    PrimaryKeyOf<ARDocumentForAPDocument>.By<ARDocumentForAPDocument.docType, ARDocumentForAPDocument.refNbr>
  {
    public static ARDocumentForAPDocument Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<ARDocumentForAPDocument>.By<ARDocumentForAPDocument.docType, ARDocumentForAPDocument.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.refNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentForAPDocument.branchID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.status>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ARDocumentForAPDocument.docDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentForAPDocument.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentForAPDocument.customerID>
  {
  }

  public abstract class customerCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentForAPDocument.customerCD>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentForAPDocument.vendorID>
  {
  }

  public abstract class vendorCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentForAPDocument.vendorCD>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDocumentForAPDocument.curyOrigDocAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.curyID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.docDesc>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentForAPDocument.invoiceNbr>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDocumentForAPDocument.termsID>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ARDocumentForAPDocument.dueDate>
  {
  }

  public abstract class discDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ARDocumentForAPDocument.discDate>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDocumentForAPDocument.curyDocBal>
  {
  }

  public abstract class curyTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDocumentForAPDocument.curyTaxTotal>
  {
  }

  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDocumentForAPDocument.curyOrigDiscAmt>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDocumentForAPDocument.projectID>
  {
  }

  public abstract class projectCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentForAPDocument.projectCD>
  {
  }

  public abstract class intercompanyAPDocumentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARDocumentForAPDocument.intercompanyAPDocumentNoteID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDocumentForAPDocument.noteID>
  {
  }
}
