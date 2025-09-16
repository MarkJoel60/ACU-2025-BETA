// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CN.Common.Descriptor.Attributes;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

/// <summary>
/// Represents a compliance document.
/// The records of this type are created and edited through the Compliance Management (CL401000) form
/// (which corresponds to the <see cref="T:PX.Objects.CN.Compliance.CL.Graphs.ComplianceDocumentEntry" /> graph)
/// and through many other forms that contain the <b>Compliance</b> tab.
/// </summary>
[PXCacheName("Compliance Document")]
[Serializable]
public class ComplianceDocument : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXUIField]
  [PXDBIdentity(IsKey = true)]
  public virtual int? ComplianceDocumentID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Required { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Received { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CreationDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXFormula(typeof (IIf<Where<BqlOperand<ComplianceDocument.received, IBqlBool>.IsEqual<True>>, IIf<Where<BqlOperand<Current<ComplianceDocument.receivedDate>, IBqlDateTime>.IsNull>, BqlField<AccessInfo.businessDate, IBqlDateTime>.FromCurrent, BqlField<ComplianceDocument.receivedDate, IBqlDateTime>.FromCurrent>, Null>))]
  [PXUIField]
  public virtual DateTime? ReceivedDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? SentDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField]
  public virtual Decimal? Limit { get; set; }

  [PXDBInt]
  [PXDefault(typeof (SearchFor<ComplianceAttributeType.complianceAttributeTypeID>.Where<BqlOperand<ComplianceAttributeType.type, IBqlString>.IsEqual<BqlField<FilterRow.valueSt, IBqlString>.FromCurrent>>))]
  [PXSelector(typeof (Search<ComplianceAttributeType.complianceAttributeTypeID, Where<ComplianceAttributeType.type, NotEqual<ComplianceDocumentType.status>>>), SubstituteKey = typeof (ComplianceAttributeType.type))]
  [PXUIField]
  [ComplianceDocumentType]
  public virtual int? DocumentType { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<ComplianceAttribute.attributeId, Where<ComplianceAttribute.type, Equal<Current<ComplianceDocument.documentType>>>>), SubstituteKey = typeof (ComplianceAttribute.value))]
  [ComplianceDocumentLienWaiverType]
  [PXUIField]
  public virtual int? DocumentTypeValue { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<ComplianceAttribute.attributeId, InnerJoin<ComplianceAttributeType, On<ComplianceAttributeType.complianceAttributeTypeID, Equal<ComplianceAttribute.type>>>, Where<ComplianceAttributeType.type, Equal<ComplianceDocumentType.status>>>), SubstituteKey = typeof (ComplianceAttribute.value))]
  [PXUIField]
  public virtual int? Status { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string MethodSent { get; set; }

  [PXDBInt]
  [PXDimensionSelector("PROJECT", typeof (Search<PMProject.contractID, Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>), typeof (PMProject.contractCD), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.customerID), typeof (PMProject.description), typeof (PMProject.status)}, DescriptionField = typeof (PMProject.description))]
  [PXUIField]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (ComplianceDocument.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>))]
  public virtual int? CostTaskID { get; set; }

  [ProjectTask(typeof (ComplianceDocument.projectID), typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>))]
  public virtual int? RevenueTaskID { get; set; }

  [PXDBInt]
  [CostCodeDimensionSelector(typeof (PMCostCode.costCodeID))]
  [PXUIField]
  public virtual int? CostCodeID { get; set; }

  [Customer]
  [ComplianceDocumentCustomer]
  public virtual int? CustomerID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<PX.Objects.AR.Customer.acctName, Where<PX.Objects.AR.Customer.bAccountID, Equal<ComplianceDocument.customerID>>>))]
  public virtual string CustomerName { get; set; }

  [Vendor]
  [ComplianceDocumentVendor]
  public virtual int? VendorID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<PX.Objects.AP.Vendor.acctName, Where<PX.Objects.AP.Vendor.bAccountID, Equal<ComplianceDocument.vendorID>>>))]
  public virtual string VendorName { get; set; }

  [Vendor]
  [ComplianceDocumentSecondaryVendor]
  public virtual int? SecondaryVendorID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<PX.Objects.AP.Vendor.acctName, Where<PX.Objects.AP.Vendor.bAccountID, Equal<ComplianceDocument.secondaryVendorID>>>))]
  public virtual string SecondaryVendorName { get; set; }

  [FieldDescriptionForDynamicColumns]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PX.Objects.PO.POOrder))]
  [PXUIField]
  public virtual Guid? PurchaseOrder { get; set; }

  [PXDBInt]
  [ComplianceDocumentPurchaseOrderLineSelector]
  [PXUIField]
  [DependsOnField(typeof (ComplianceDocument.purchaseOrder))]
  public virtual int? PurchaseOrderLineItem { get; set; }

  [PXDBString(15)]
  [PXSelector(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>>, OrderBy<Desc<PX.Objects.PO.POOrder.orderNbr>>>), new Type[] {typeof (PX.Objects.PO.POOrder.orderNbr), typeof (PX.Objects.PO.POOrder.vendorRefNbr), typeof (PX.Objects.PO.POOrder.orderDate), typeof (PX.Objects.PO.POOrder.status), typeof (PX.Objects.PO.POOrder.vendorID), typeof (PX.Objects.PO.POOrder.vendorID_Vendor_acctName), typeof (PX.Objects.PO.POOrder.vendorLocationID), typeof (PX.Objects.PO.POOrder.curyID), typeof (PX.Objects.PO.POOrder.curyOrderTotal)}, Filterable = true, Headers = new string[] {"Subcontract Nbr.", "Vendor Ref.", "Date", "Status", "Vendor", "Vendor Name", "Location", "Currency", "Subcontract Total"})]
  [PXUIField]
  [SubcontractLink]
  public virtual string Subcontract { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<PX.Objects.PO.POLine.lineNbr, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>>>>, Where<PX.Objects.PO.POOrder.orderNbr, Equal<Current<ComplianceDocument.subcontract>>>>), new Type[] {typeof (PX.Objects.PO.POLine.lineNbr), typeof (PX.Objects.PO.POLine.branchID), typeof (PX.Objects.PO.POLine.inventoryID), typeof (PX.Objects.PO.POLine.lineType), typeof (PX.Objects.PO.POLine.tranDesc), typeof (PX.Objects.PO.POLine.orderQty), typeof (PX.Objects.PO.POLine.curyUnitCost)})]
  [PXUIField]
  [DependsOnField(typeof (ComplianceDocument.subcontract))]
  public virtual int? SubcontractLineItem { get; set; }

  [PXDBString(15)]
  [PXSelector(typeof (Search3<PMChangeOrder.refNbr, InnerJoin<PX.Objects.AR.Customer, On<PMChangeOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<PMChangeOrder.refNbr>>>), new Type[] {typeof (PMChangeOrder.refNbr), typeof (PMChangeOrder.projectID), typeof (PMChangeOrder.status), typeof (PMChangeOrder.projectNbr), typeof (PMChangeOrder.date), typeof (PMChangeOrder.completionDate)}, Filterable = true)]
  [PXUIField]
  public virtual string ChangeOrderNumber { get; set; }

  [FieldDescriptionForDynamicColumns]
  [ComplianceDocumentInvoice]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PX.Objects.AR.ARInvoice))]
  [PXUIField]
  public virtual Guid? InvoiceID { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search2<PX.Objects.AR.ARInvoice.origDocAmt, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.refNoteId, Equal<PX.Objects.AR.ARInvoice.noteID>>>, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.invoiceID>>>))]
  [PXUIField]
  public virtual Decimal? InvoiceAmount { get; set; }

  [FieldDescriptionForDynamicColumns]
  [ComplianceDocumentBill]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PX.Objects.AP.APInvoice))]
  [PXUIField]
  public virtual Guid? BillID { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search2<PX.Objects.AP.APInvoice.origDocAmt, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.refNoteId, Equal<PX.Objects.AP.APInvoice.noteID>>>, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.billID>>>))]
  [PXUIField]
  public virtual Decimal? BillAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField]
  public virtual Decimal? LienWaiverAmount { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string SponsorOrganization { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string CertificateNumber { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string InsuranceCompany { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [ComplianceDocumentPolicyUnique]
  public virtual string Policy { get; set; }

  [PXInt]
  [PXDBScalar(typeof (Search<ComplianceAttributeType.complianceAttributeTypeID, Where<ComplianceAttributeType.type, Equal<ComplianceDocumentType.insurance>>>))]
  public virtual int? InsuranceDocumentTypeId { get; set; }

  [PXDBString(10)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  public virtual string ApPaymentMethodID { get; set; }

  [PXDBString(10)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  public virtual string ArPaymentMethodID { get; set; }

  [AccountAny]
  public virtual int? AccountID { get; set; }

  [FieldDescriptionForDynamicColumns]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PX.Objects.AP.APPayment))]
  [ComplianceDocumentCheck]
  [PXUIField]
  public virtual Guid? ApCheckID { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search2<PX.Objects.AP.APPayment.extRefNbr, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.refNoteId, Equal<PX.Objects.AP.APPayment.noteID>>>, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.apCheckId>>>))]
  public virtual string CheckNumber { get; set; }

  [FieldDescriptionForDynamicColumns]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PX.Objects.AR.ARPayment))]
  [PXUIField]
  public virtual Guid? ArPaymentID { get; set; }

  [FieldDescriptionForDynamicColumns]
  [PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentRefNote(typeof (PMRegister))]
  [PXUIField]
  public virtual Guid? ProjectTransactionID { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? DateIssued { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? ThroughDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? ReceiveDate { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXUIField]
  public virtual DateTime? PaymentDate { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string ReceivedBy { get; set; }

  [PXDBString(10)]
  [PXDefault("AP Bill")]
  [PXUIField]
  [ComplianceDocumentSourceType]
  public virtual string SourceType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsRequiredJointCheck { get; set; }

  [Vendor]
  [JointVendorRequired]
  public virtual int? JointVendorInternalId { get; set; }

  [PXDBString(30)]
  [JointVendorRequired]
  [PXUIField(DisplayName = "Joint Payee")]
  public virtual string JointVendorExternalName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? LinkToPayment { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField]
  public virtual Decimal? JointAmount { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string JointRelease { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? JointReleaseReceived { get; set; }

  [PXBool]
  [UiInformationField]
  [PXUnboundDefault(typeof (IIf<BqlOperand<ComplianceDocument.expirationDate, IBqlDateTime>.IsLess<BqlField<AccessInfo.businessDate, IBqlDateTime>.FromCurrent>, True, False>))]
  [PXFormula(typeof (Default<ComplianceDocument.expirationDate>))]
  [PXUIField(DisplayName = "Expired", IsReadOnly = true)]
  public virtual bool? IsExpired { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Processed", Enabled = false)]
  public virtual bool? IsProcessed { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Voided")]
  public virtual bool? IsVoided { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Created Automatically", Enabled = false)]
  public virtual bool? IsCreatedAutomatically { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Lien Notice Amount")]
  public virtual Decimal? LienNoticeAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Joint Lien Notice Amount")]
  public virtual Decimal? JointLienNoticeAmount { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Received from Joint Payee")]
  public virtual bool? IsReceivedFromJointVendor { get; set; }

  [PXDBDate(PreserveTime = false)]
  [PXFormula(typeof (IIf<Where<BqlOperand<ComplianceDocument.isReceivedFromJointVendor, IBqlBool>.IsEqual<True>>, IIf<Where<BqlOperand<Current<ComplianceDocument.jointReceivedDate>, IBqlDateTime>.IsNull>, BqlField<AccessInfo.businessDate, IBqlDateTime>.FromCurrent, BqlField<ComplianceDocument.jointReceivedDate, IBqlDateTime>.FromCurrent>, Null>))]
  [PXUIField(DisplayName = "Joint Payee Received Date")]
  public virtual DateTime? JointReceivedDate { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Joint Payee Lien Waiver Amount")]
  public virtual Decimal? JointLienWaiverAmount { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedById { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenId { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedById { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenId { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXBool]
  public virtual bool? SkipInit { get; set; }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ComplianceDocument.tstamp>
  {
  }

  public abstract class skipInit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.skipInit>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.selected>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.createdByID>
  {
  }

  public abstract class linkToPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComplianceDocument.linkToPayment>
  {
  }

  public abstract class jointAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.jointAmount>
  {
  }

  public abstract class jointRelease : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.jointRelease>
  {
  }

  public abstract class jointReleaseReceived : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComplianceDocument.jointReleaseReceived>
  {
  }

  public abstract class complianceDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.complianceDocumentID>
  {
  }

  public abstract class complianceDocumentIdForReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.complianceDocumentIdForReport>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.required>
  {
  }

  public abstract class received : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.received>
  {
  }

  public abstract class creationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.creationDate>
  {
  }

  public abstract class receivedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.receivedDate>
  {
  }

  public abstract class sentDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ComplianceDocument.sentDate>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.expirationDate>
  {
  }

  public abstract class limit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ComplianceDocument.limit>
  {
  }

  public abstract class documentType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.documentType>
  {
  }

  public abstract class documentTypeValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.documentTypeValue>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.status>
  {
  }

  public abstract class methodSent : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocument.methodSent>
  {
  }

  public abstract class costTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.costTaskID>
  {
  }

  public abstract class revenueTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.revenueTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.costCodeID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.customerID>
  {
  }

  public abstract class customerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.customerName>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.projectID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.vendorID>
  {
  }

  public abstract class vendorName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocument.vendorName>
  {
  }

  public abstract class secondaryVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.secondaryVendorID>
  {
  }

  public abstract class secondaryVendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.secondaryVendorName>
  {
  }

  public abstract class purchaseOrderLineItem : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.purchaseOrderLineItem>
  {
  }

  public abstract class purchaseOrder : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocument.purchaseOrder>
  {
  }

  public abstract class subcontract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.subcontract>
  {
  }

  public abstract class subcontractLineItem : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.subcontractLineItem>
  {
  }

  public abstract class changeOrderNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.changeOrderNumber>
  {
  }

  public abstract class invoiceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocument.invoiceID>
  {
  }

  public abstract class invoiceAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.invoiceAmount>
  {
  }

  public abstract class billID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocument.billID>
  {
  }

  public abstract class billAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.billAmount>
  {
  }

  public abstract class lienWaiverAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.lienWaiverAmount>
  {
  }

  public abstract class sponsorOrganization : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.sponsorOrganization>
  {
  }

  public abstract class certificateNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.certificateNumber>
  {
  }

  public abstract class insuranceCompany : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.insuranceCompany>
  {
  }

  public abstract class policy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocument.policy>
  {
  }

  public abstract class insuranceDocumentTypeId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.insuranceDocumentTypeId>
  {
  }

  public abstract class apPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.apPaymentMethodID>
  {
  }

  public abstract class arPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.arPaymentMethodID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocument.accountID>
  {
  }

  public abstract class apCheckId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocument.apCheckId>
  {
  }

  public abstract class checkNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.checkNumber>
  {
  }

  public abstract class arPaymentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocument.arPaymentID>
  {
  }

  public abstract class projectTransactionID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocument.projectTransactionID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.receiptDate>
  {
  }

  public abstract class dateIssued : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.dateIssued>
  {
  }

  public abstract class throughDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.throughDate>
  {
  }

  public abstract class receiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.receiveDate>
  {
  }

  public abstract class paymentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.paymentDate>
  {
  }

  public abstract class receivedBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocument.receivedBy>
  {
  }

  public abstract class sourceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocument.sourceType>
  {
  }

  public abstract class isRequiredJointCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComplianceDocument.isRequiredJointCheck>
  {
  }

  public abstract class jointVendorInternalId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocument.jointVendorInternalId>
  {
  }

  public abstract class jointVendorExternalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocument.jointVendorExternalName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocument.noteID>
  {
  }

  public abstract class isExpired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.isExpired>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.lastModifiedDateTime>
  {
  }

  public abstract class isProcessed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.isProcessed>
  {
  }

  public abstract class isVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComplianceDocument.isVoided>
  {
  }

  public abstract class isCreatedAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComplianceDocument.isCreatedAutomatically>
  {
  }

  public abstract class lienNoticeAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.lienNoticeAmount>
  {
  }

  public abstract class jointLienNoticeAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.jointLienNoticeAmount>
  {
  }

  public abstract class isReceivedFromJointVendor : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComplianceDocument.isReceivedFromJointVendor>
  {
  }

  public abstract class jointReceivedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.jointReceivedDate>
  {
  }

  public abstract class jointLienWaiverAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocument.jointLienWaiverAmount>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocument.createdDateTime>
  {
  }

  public class complianceClassId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ComplianceDocument.complianceClassId>
  {
    public complianceClassId()
      : base("COMPLIANCE")
    {
    }
  }

  public class typeName : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ComplianceDocument.typeName>
  {
    public typeName()
      : base(typeof (ComplianceDocument).FullName)
    {
    }
  }
}
