// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSetupApproval
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
using PX.Objects.AR.Standalone;
using PX.Objects.EP;
using PX.Objects.SO;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>The settings for approval of AR documents.</summary>
public class ARSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXSelector(typeof (SearchFor<EPAssignmentMap.assignmentMapID>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ARSetupApproval.docType>, Equal<ARDocType.refund>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<FullNameOf<ARPayment>>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ARSetupApproval.docType>, Equal<ARDocType.cashReturn>>>>>.And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<FullNameOf<ARCashSale>>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ARSetupApproval.docType>, In3<ARDocType.invoice, ARDocType.prepaymentInvoice, ARDocType.creditMemo, ARDocType.debitMemo>>>>, And<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<FullNameOf<ARInvoice>>>>>.And<BqlOperand<EPAssignmentMap.graphType, IBqlString>.IsNotEqual<FullNameOf<SOInvoiceEntry>>>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  [PXCheckUnique(new Type[] {typeof (ARSetupApproval.docType)})]
  public virtual int? AssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Notification.notificationID), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Pending Approval Notification")]
  public virtual int? AssignmentNotificationID { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

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

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the approval map is applied to documents of the <see cref="P:PX.Objects.AR.ARSetupApproval.DocType" /> type.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Specifies the document type to which the approval map is applied.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault("REF")]
  [ARDocType.ARApprovalDocTypeList]
  [PXUIField]
  [PXFieldDescription]
  public virtual string DocType { get; set; }

  /// <exclude />
  public class PK : PrimaryKeyOf<ARSetupApproval>.By<ARSetupApproval.approvalID>
  {
    public static ARSetupApproval Find(PXGraph graph, int? ApprovalID, PKFindOptions options = 0)
    {
      return (ARSetupApproval) PrimaryKeyOf<ARSetupApproval>.By<ARSetupApproval.approvalID>.FindBy(graph, (object) ApprovalID, options);
    }
  }

  public class EPSetting : 
    EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>
  {
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSetupApproval.approvalID>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSetupApproval.lastModifiedDateTime>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSetupApproval.isActive>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSetupApproval.docType>
  {
  }
}
