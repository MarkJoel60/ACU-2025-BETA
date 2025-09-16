// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSetupInvoiceApproval
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
using PX.Objects.AR;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>The settings for approval of SO invoices.</summary>
[PXCacheName("SO Invoice Approval")]
public class SOSetupInvoiceApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  /// <summary>
  /// Specifies (if set to <c>true</c>) that the approval map is applied to documents of the <see cref="P:PX.Objects.SO.SOSetupInvoiceApproval.DocType" /> type.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Specifies the document type to which the approval map is applied.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault("INV")]
  [ARDocType.SOEntryList]
  [PXFieldDescription]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>The surrogate identifier of the record.</summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  /// <summary>
  /// Specifies the assignment map that will be used to walk an SO Invoice through the approval process.
  /// </summary>
  [PXDBInt]
  [PXDefault]
  [PXCheckUnique(new Type[] {typeof (SOSetupInvoiceApproval.docType)})]
  [PXSelector(typeof (SearchFor<EPAssignmentMap.assignmentMapID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPAssignmentMap.entityType, Equal<FullNameOf<PX.Objects.AR.ARInvoice>>>>>>.And<BqlOperand<EPAssignmentMap.graphType, IBqlString>.IsEqual<FullNameOf<SOInvoiceEntry>>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  public virtual int? AssignmentMapID { get; set; }

  /// <summary>
  /// Specifies the pending approval notification that will be send to an approver.
  /// </summary>
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
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <exclude />
  public class PK : PrimaryKeyOf<SOSetupInvoiceApproval>.By<SOSetupInvoiceApproval.approvalID>
  {
    public static SOSetupInvoiceApproval Find(
      PXGraph graph,
      int? approvalID,
      PKFindOptions options = 0)
    {
      return (SOSetupInvoiceApproval) PrimaryKeyOf<SOSetupInvoiceApproval>.By<SOSetupInvoiceApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  /// <exclude />
  public static class FK
  {
    public class ApprovalMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<SOSetupInvoiceApproval>.By<SOSetupInvoiceApproval.assignmentMapID>
    {
    }

    public class PendingApprovalNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<SOSetupInvoiceApproval>.By<SOSetupInvoiceApproval.assignmentNotificationID>
    {
    }
  }

  public class EPSettings : 
    EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetupInvoiceApproval.isActive>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOSetupInvoiceApproval.docType>
  {
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSetupInvoiceApproval.approvalID>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetupInvoiceApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetupInvoiceApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOSetupInvoiceApproval.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOSetupInvoiceApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetupInvoiceApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetupInvoiceApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOSetupInvoiceApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetupInvoiceApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetupInvoiceApproval.lastModifiedDateTime>
  {
  }
}
