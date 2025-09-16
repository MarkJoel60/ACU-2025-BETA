// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APSetupApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("AP Approval Preferences")]
public class APSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  protected 
  #nullable disable
  string _DocType;

  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<EPAssignmentMap, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.invoice>>>>, PX.Data.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPInvoice>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.creditAdj>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPInvoice>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.debitAdj>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPInvoice>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.prepaymentRequest>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPInvoice>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.quickCheck>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPQuickCheck>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.check>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPPayment>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.prepayment>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPPayment>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APSetupApproval.docType>, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeAPInvoice>>>>, EPAssignmentMap>.SearchFor<EPAssignmentMap.assignmentMapID>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  public virtual int? AssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Notification.notificationID), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Pending Approval Notification")]
  public virtual int? AssignmentNotificationID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("INV")]
  [APDocType.APApprovalDocTypeList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  [PXFieldDescription]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  public class PK : PrimaryKeyOf<APSetupApproval>.By<APSetupApproval.approvalID>
  {
    public static APSetupApproval Find(PXGraph graph, int? approvalID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APSetupApproval>.By<APSetupApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  public static class FK
  {
    public class PendingApprovalNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<APSetupApproval>.By<APSetupApproval.assignmentNotificationID>
    {
    }
  }

  public class EPSetting : 
    EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>
  {
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APSetupApproval.approvalID>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APSetupApproval.lastModifiedDateTime>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APSetupApproval.isActive>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APSetupApproval.docType>
  {
  }
}
