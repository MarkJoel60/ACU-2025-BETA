// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSBillHistory
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Field Service Billing History")]
[Serializable]
public class FSBillHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IFSRelatedDoc
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false, Visible = false)]
  [FSPostBatch]
  public virtual int? BatchID { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false)]
  [FSSelectorSrvOrdType]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSBillHistory.srvOrdType>>>>))]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.srvOrdType, Equal<Current<FSBillHistory.srvOrdType>>>>))]
  public virtual string AppointmentRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Service Contract Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSServiceContract.refNbr>))]
  public virtual string ServiceContractRefNbr { get; set; }

  [PXDBString(4)]
  [FSEntityType.List]
  [PXUIField(DisplayName = "Origin Entity Type", Enabled = false, Visible = false)]
  public virtual string ParentEntityType { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Origin Doc Type", Enabled = false)]
  public virtual string ParentDocType { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Origin Doc Nbr.", Enabled = false)]
  public virtual string ParentRefNbr { get; set; }

  [PXDBString(4)]
  [FSEntityType.List]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false)]
  public virtual string ChildEntityType { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXUIField(DisplayName = "Created Doc. Type", Enabled = false)]
  public virtual string ChildDocType { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Created Doc. Nbr.", Enabled = false)]
  public virtual string ChildRefNbr { get; set; }

  [FSRelatedDocument(typeof (FSBillHistory), typeof (FSBillHistory.childEntityType), typeof (FSBillHistory.childDocType), typeof (FSBillHistory.childRefNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string ChildDocLink { get; set; }

  [FSRelatedDocument(typeof (FSBillHistory), typeof (FSBillHistory.parentEntityType), typeof (FSBillHistory.parentDocType), typeof (FSBillHistory.parentRefNbr))]
  [PXUIField(DisplayName = "Origin Doc. Ref. Nbr.", Enabled = false)]
  public virtual string ParentDocLink { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string ChildDocDesc { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? ChildDocDate { get; set; }

  [PXString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string ChildDocStatus { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the related billing document deleted or not.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? IsChildDocDeleted { get; set; }

  [PXDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? ChildAmount { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<FSContractPeriod.contractPeriodID>), SubstituteKey = typeof (FSContractPeriod.billingPeriod))]
  [PXUIField(DisplayName = "Service Contract Billing Period", Enabled = false)]
  public virtual int? ServiceContractPeriodID { get; set; }

  [PXString(1, IsUnicode = true)]
  [ListField_Status_ContractPeriod.List]
  [PXUIField(DisplayName = "Contract Period Status", Enabled = false)]
  public virtual string ContractPeriodStatus { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  [FSEntityType.List]
  [PXFormula(typeof (Switch<Case<Where<FSBillHistory.appointmentRefNbr, IsNotNull>, FSEntityType.appointment, Case<Where<FSBillHistory.serviceOrderRefNbr, IsNotNull>, FSEntityType.serviceOrder, Case<Where<FSBillHistory.serviceContractRefNbr, IsNotNull>, FSEntityType.serviceContract>>>, Null>))]
  [PXUIField(DisplayName = "Svc. Doc. Type", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual string RelatedDocumentType { get; set; }

  [FSRelatedDocument(typeof (FSBillHistory))]
  [PXUIField(DisplayName = "Svc. Ref. Nbr.", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual string RelatedDocument { get; set; }

  public int? ServiceOrderLineNbr => new int?();

  public int? AppointmentLineNbr => new int?();

  public class PK : PrimaryKeyOf<FSBillHistory>.By<FSBillHistory.recordID>
  {
    public static FSBillHistory Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (FSBillHistory) PrimaryKeyOf<FSBillHistory>.By<FSBillHistory.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<FSBillHistory>.By<FSBillHistory.srvOrdType, FSBillHistory.serviceOrderRefNbr, FSBillHistory.appointmentRefNbr, FSBillHistory.parentEntityType, FSBillHistory.parentDocType, FSBillHistory.parentRefNbr, FSBillHistory.childEntityType, FSBillHistory.childDocType, FSBillHistory.childRefNbr>
  {
    public static FSBillHistory FindDirty(
      PXGraph graph,
      string srvOrdType,
      string serviceOrderRefNbr,
      string appointmentRefNbr,
      string parentEntityType,
      string parentDocType,
      string parentRefNbr,
      string childEntityType,
      string childDocType,
      string childRefNbr)
    {
      return PXResultset<FSBillHistory>.op_Implicit(PXSelectBase<FSBillHistory, PXSelect<FSBillHistory, Where2<Where<FSBillHistory.srvOrdType, Equal<Required<FSBillHistory.srvOrdType>>, Or<Where<Required<FSBillHistory.srvOrdType>, IsNull, And<FSBillHistory.srvOrdType, IsNull>>>>, And2<Where<FSBillHistory.serviceOrderRefNbr, Equal<Required<FSBillHistory.serviceOrderRefNbr>>, Or<Where<Required<FSBillHistory.serviceOrderRefNbr>, IsNull, And<FSBillHistory.serviceOrderRefNbr, IsNull>>>>, And2<Where<FSBillHistory.appointmentRefNbr, Equal<Required<FSBillHistory.appointmentRefNbr>>, Or<Where<Required<FSBillHistory.appointmentRefNbr>, IsNull, And<FSBillHistory.appointmentRefNbr, IsNull>>>>, And2<Where<FSBillHistory.parentEntityType, Equal<Required<FSBillHistory.parentEntityType>>, Or<Where<Required<FSBillHistory.parentEntityType>, IsNull, And<FSBillHistory.parentEntityType, IsNull>>>>, And2<Where<FSBillHistory.parentDocType, Equal<Required<FSBillHistory.parentDocType>>, Or<Where<Required<FSBillHistory.parentDocType>, IsNull, And<FSBillHistory.parentDocType, IsNull>>>>, And2<Where<FSBillHistory.parentRefNbr, Equal<Required<FSBillHistory.parentRefNbr>>, Or<Where<Required<FSBillHistory.parentRefNbr>, IsNull, And<FSBillHistory.parentRefNbr, IsNull>>>>, And2<Where<FSBillHistory.childEntityType, Equal<Required<FSBillHistory.childEntityType>>, Or<Where<Required<FSBillHistory.childEntityType>, IsNull, And<FSBillHistory.childEntityType, IsNull>>>>, And2<Where<FSBillHistory.childDocType, Equal<Required<FSBillHistory.childDocType>>, Or<Where<Required<FSBillHistory.childDocType>, IsNull, And<FSBillHistory.childDocType, IsNull>>>>, And<Where<FSBillHistory.childRefNbr, Equal<Required<FSBillHistory.childRefNbr>>, Or<Where<Required<FSBillHistory.childRefNbr>, IsNull, And<FSBillHistory.childRefNbr, IsNull>>>>>>>>>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[18]
      {
        (object) srvOrdType,
        (object) srvOrdType,
        (object) serviceOrderRefNbr,
        (object) serviceOrderRefNbr,
        (object) appointmentRefNbr,
        (object) appointmentRefNbr,
        (object) parentEntityType,
        (object) parentEntityType,
        (object) parentDocType,
        (object) parentDocType,
        (object) parentRefNbr,
        (object) parentRefNbr,
        (object) childEntityType,
        (object) childEntityType,
        (object) childDocType,
        (object) childDocType,
        (object) childRefNbr,
        (object) childRefNbr
      }));
    }
  }

  public static class FK
  {
    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<ARTran>.By<FSBillHistory.srvOrdType, FSBillHistory.appointmentRefNbr>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<ARTran>.By<FSBillHistory.srvOrdType, FSBillHistory.serviceOrderRefNbr>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<ARTran>.By<FSBillHistory.serviceContractRefNbr>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBillHistory.recordID>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBillHistory.batchID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.srvOrdType>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.serviceOrderRefNbr>
  {
  }

  public abstract class appointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.appointmentRefNbr>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.serviceContractRefNbr>
  {
  }

  public abstract class parentEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.parentEntityType>
  {
  }

  public abstract class parentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.parentDocType>
  {
  }

  public abstract class parentRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.parentRefNbr>
  {
  }

  public abstract class childEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.childEntityType>
  {
    public abstract class Values : FSEntityType
    {
    }
  }

  public abstract class childDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.childDocType>
  {
  }

  public abstract class childRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.childRefNbr>
  {
  }

  public abstract class childDocLink : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.childDocLink>
  {
  }

  public abstract class parentDocLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.parentDocLink>
  {
  }

  public abstract class childDocDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillHistory.childDocDesc>
  {
  }

  public abstract class childDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBillHistory.childDocDate>
  {
  }

  public abstract class childDocStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.childDocStatus>
  {
  }

  public abstract class isChildDocDeleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSBillHistory.isChildDocDeleted>
  {
  }

  public abstract class childAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSBillHistory.childAmount>
  {
  }

  public abstract class serviceContractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSBillHistory.serviceContractPeriodID>
  {
  }

  public abstract class contractPeriodStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.contractPeriodStatus>
  {
    public abstract class Values : ListField_Status_ContractPeriod
    {
    }
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSBillHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBillHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSBillHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBillHistory.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSBillHistory.Tstamp>
  {
  }

  public abstract class relatedDocumentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.relatedDocumentType>
  {
    public abstract class Values : FSEntityType
    {
    }
  }

  public abstract class relatedDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillHistory.relatedDocument>
  {
  }
}
