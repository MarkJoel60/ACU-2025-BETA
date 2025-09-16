// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.PostingBatchDetail
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSPostRegister, LeftJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSPostRegister.srvOrdType>, And<FSAppointment.refNbr, Equal<FSPostRegister.refNbr>>>, LeftJoin<FSServiceOrder, On<Where2<Where<FSServiceOrder.srvOrdType, Equal<FSPostRegister.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSPostRegister.refNbr>>>, Or<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.billCustomerID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAddress.postalCode>>, LeftJoin<FSGeoZone, On<FSGeoZone.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>>>>>>>))]
[Serializable]
public class PostingBatchDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (FSPostRegister.batchID))]
  [PXParent(typeof (Select<FSPostBatch, Where<FSPostBatch.batchID, Equal<Current<PostingBatchDetail.batchID>>>>))]
  [PXDBDefault(typeof (FSPostBatch.batchID))]
  [PXUIField(DisplayName = "Batch ID")]
  public virtual int? BatchID { get; set; }

  [PXDBInt(BqlField = typeof (FSPostDet.postDetID))]
  public virtual int? PostDetID { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (FSPostRegister.postedTO))]
  public virtual 
  #nullable disable
  string PostedTO { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = ">aaa", BqlField = typeof (FSPostRegister.postDocType))]
  public virtual string PostDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSPostRegister.postRefNbr))]
  public virtual string PostRefNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SO>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through Sales Order")]
  public virtual bool? SOPosted { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SO>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "Sales Order Type")]
  public virtual string SOOrderType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SO>>, PostingBatchDetail.postRefNbr>, Null>))]
  [PXUIField(DisplayName = "Sales Order Nbr.")]
  public virtual string SOOrderNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Sales Order Line Nbr.")]
  public virtual int? SOLineNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AR>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through AR")]
  public virtual bool? ARPosted { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AR>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "AR Document Type")]
  public virtual string ARDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AR>>, PostingBatchDetail.postRefNbr>, Null>))]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
  public virtual string ARRefNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "AR Line Nbr.")]
  public virtual int? ARLineNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AP>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through AP")]
  public virtual bool? APPosted { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AP>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "AP Document Type")]
  public virtual string APDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AP Reference Nbr.")]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.AP>>, PostingBatchDetail.postRefNbr>, Null>))]
  public virtual string APRefNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "AP Line Nbr.")]
  public virtual int? APLineNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.IN>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through IN")]
  public virtual bool? INPosted { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.IN>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "IN Document Type")]
  public virtual string INDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.IN>>, PostingBatchDetail.postRefNbr>, Null>))]
  [PXUIField(DisplayName = "IN Reference Nbr.")]
  public virtual string INRefNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "IN Line Nbr.")]
  public virtual int? INLineNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SI>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through SO Invoice")]
  public virtual bool? SOInvPosted { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SI>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "SO Invoice Document Type")]
  public virtual string SOInvDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo.SI>>, PostingBatchDetail.postRefNbr>, Null>))]
  [PXUIField(DisplayName = "SO Invoice Ref. Nbr.")]
  public virtual string SOInvRefNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "SO Invoice Line Nbr.")]
  public virtual int? SOInvLineNbr { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo_CreateInvoice.PM>>, True>, False>))]
  [PXUIField(DisplayName = "Invoiced through PM")]
  public virtual bool? PMPosted { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo_CreateInvoice.PM>>, PostingBatchDetail.postDocType>, Null>))]
  [PXUIField(DisplayName = "PM Document Type")]
  public virtual string PMDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUnboundDefault(typeof (Switch<Case<Where<PostingBatchDetail.postedTO, Equal<ListField_PostTo_CreateInvoice.PM>>, PostingBatchDetail.postRefNbr>, Null>))]
  [PXUIField(DisplayName = "PM Reference Nbr.")]
  public virtual string PMRefNbr { get; set; }

  [PXLong]
  [PXUIField(DisplayName = "PM Tran ID")]
  public virtual long? PMTranID { get; set; }

  [PXString(15)]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
  public virtual string Mem_DocNbr
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return this.APRefNbr;
      if (this.ARPosted.GetValueOrDefault())
        return this.ARRefNbr;
      if (this.INPosted.GetValueOrDefault())
        return this.INRefNbr;
      if (this.SOPosted.GetValueOrDefault())
        return this.SOOrderNbr;
      if (this.SOInvPosted.GetValueOrDefault())
        return this.SOInvRefNbr;
      return this.PMPosted.GetValueOrDefault() ? this.PMRefNbr : string.Empty;
    }
  }

  [PXString(3)]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string Mem_DocType
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return this.APDocType;
      if (this.ARPosted.GetValueOrDefault())
        return this.ARDocType;
      if (this.INPosted.GetValueOrDefault())
        return this.INDocType;
      if (this.SOPosted.GetValueOrDefault())
        return this.SOOrderType;
      if (this.SOInvPosted.GetValueOrDefault())
        return this.SOInvDocType;
      return this.PMPosted.GetValueOrDefault() ? this.PMDocType : string.Empty;
    }
  }

  [PXString(2)]
  [PXUIField(DisplayName = "Document", Enabled = false)]
  public virtual string Mem_PostedIn
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return "AP";
      if (this.ARPosted.GetValueOrDefault())
        return "AR";
      if (this.INPosted.GetValueOrDefault())
        return "IN";
      if (this.SOPosted.GetValueOrDefault())
        return "SO";
      if (this.SOInvPosted.GetValueOrDefault())
        return "SI";
      return this.PMPosted.GetValueOrDefault() ? "PM" : string.Empty;
    }
  }

  [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof (FSServiceOrder.srvOrdType))]
  [PXUIField]
  [FSSelectorSrvOrdType]
  public virtual string SrvOrdType { get; set; }

  [PXUnboundKey]
  [PXDBString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSServiceOrder.customerID>>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (FSServiceOrder.refNbr), typeof (PX.Objects.CR.BAccount.acctName), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  public virtual string AppointmentRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceOrder.refNbr))]
  [PXUIField]
  [FSSelectorSORefNbr]
  public virtual string SORefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.billCustomerID))]
  [PXUIField(DisplayName = "Billing Customer ID")]
  [FSSelectorCustomer]
  public virtual int? BillCustomerID { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time", BqlField = typeof (FSAppointment.actualDateTimeBegin))]
  [PXUIField]
  public virtual DateTime? ActualDateTimeBegin { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time", BqlField = typeof (FSAppointment.actualDateTimeEnd))]
  [PXUIField]
  public virtual DateTime? ActualDateTimeEnd { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSServiceOrder.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true, BqlField = typeof (FSGeoZone.geoZoneCD))]
  [PXUIField]
  [PXSelector(typeof (FSGeoZone.geoZoneCD))]
  public virtual string GeoZoneCD { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.sOID))]
  [PXUIField(Enabled = false, Visible = false, DisplayName = "Service Order ID")]
  public virtual int? SOID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  public virtual int? AppointmentID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote(BqlField = typeof (FSAppointment.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXDefault]
  [PXFieldDescription]
  [PXUIField]
  public virtual string AcctName { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Invoice Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr>))]
  public virtual string InvoiceRefNbr { get; set; }

  public class PK : 
    PrimaryKeyOf<PostingBatchDetail>.By<PostingBatchDetail.batchID, PostingBatchDetail.postDetID>
  {
    public static PostingBatchDetail Find(
      PXGraph graph,
      int? batchID,
      int? postDetID,
      PKFindOptions options = 0)
    {
      return (PostingBatchDetail) PrimaryKeyOf<PostingBatchDetail>.By<PostingBatchDetail.batchID, PostingBatchDetail.postDetID>.FindBy(graph, (object) batchID, (object) postDetID, options);
    }
  }

  public static class FK
  {
    public class PostBatch : 
      PrimaryKeyOf<FSPostBatch>.By<FSPostBatch.batchNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.batchID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.sOOrderType, PostingBatchDetail.sOOrderNbr>
    {
    }

    public class SOOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.sOOrderType, PostingBatchDetail.sOOrderNbr, PostingBatchDetail.sOLineNbr>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.arDocType, PostingBatchDetail.arRefNbr>
    {
    }

    public class ARInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.arDocType, PostingBatchDetail.arRefNbr, PostingBatchDetail.aRLineNbr>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.apDocType, PostingBatchDetail.apRefNbr>
    {
    }

    public class APInvoiceLine : 
      PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.apDocType, PostingBatchDetail.apRefNbr, PostingBatchDetail.aPLineNbr>
    {
    }

    public class INIssue : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.iNDocType, PostingBatchDetail.iNRefNbr>
    {
    }

    public class INIssueLine : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.iNDocType, PostingBatchDetail.iNRefNbr, PostingBatchDetail.iNLineNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.sOInvDocType, PostingBatchDetail.sOInvRefNbr>
    {
    }

    public class SOInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.sOInvDocType, PostingBatchDetail.sOInvRefNbr, PostingBatchDetail.sOInvLineNbr>
    {
    }

    public class PMRegisterLine : 
      PrimaryKeyOf<PMTran>.By<PMTran.tranID>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.pMTranID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.srvOrdType, PostingBatchDetail.appointmentRefNbr>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.srvOrdType, PostingBatchDetail.sORefNbr>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.billCustomerID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.branchLocationID>
    {
    }

    public class GeoZone : 
      PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneCD>.ForeignKeyOf<PostingBatchDetail>.By<PostingBatchDetail.geoZoneCD>
    {
    }
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.batchID>
  {
  }

  public abstract class postDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.postDetID>
  {
  }

  public abstract class postedTO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.postedTO>
  {
  }

  public abstract class postDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.postDocType>
  {
  }

  public abstract class postRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.postRefNbr>
  {
  }

  public abstract class sOPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PostingBatchDetail.sOPosted>
  {
  }

  public abstract class sOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.sOLineNbr>
  {
  }

  public abstract class aRPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PostingBatchDetail.aRPosted>
  {
  }

  public abstract class arDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.arDocType>
  {
  }

  public abstract class arRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.arRefNbr>
  {
  }

  public abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.aRLineNbr>
  {
  }

  public abstract class aPPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PostingBatchDetail.aPPosted>
  {
  }

  public abstract class apDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.apDocType>
  {
  }

  public abstract class apRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.apRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.aPLineNbr>
  {
  }

  public abstract class iNPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PostingBatchDetail.iNPosted>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.iNRefNbr>
  {
  }

  public abstract class iNLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.iNLineNbr>
  {
  }

  public abstract class sOInvPosted : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvDocType : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvRefNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class pMPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PostingBatchDetail.pMPosted>
  {
  }

  public abstract class pMDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.pMDocType>
  {
  }

  public abstract class pMRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.pMRefNbr>
  {
  }

  public abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PostingBatchDetail.pMTranID>
  {
  }

  public abstract class mem_DocNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.mem_DocNbr>
  {
  }

  public abstract class mem_DocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.mem_DocType>
  {
  }

  public abstract class mem_PostedIn : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.mem_PostedIn>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.srvOrdType>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.appointmentRefNbr>
  {
  }

  public abstract class sORefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.sORefNbr>
  {
  }

  public abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PostingBatchDetail.billCustomerID>
  {
  }

  public abstract class actualDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PostingBatchDetail.actualDateTimeBegin>
  {
  }

  public abstract class actualDateTimeEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PostingBatchDetail.actualDateTimeEnd>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PostingBatchDetail.branchLocationID>
  {
  }

  public abstract class geoZoneCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.geoZoneCD>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PostingBatchDetail.docDesc>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.sOID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PostingBatchDetail.appointmentID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PostingBatchDetail.noteID>
  {
  }

  public abstract class invoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PostingBatchDetail.invoiceRefNbr>
  {
  }
}
