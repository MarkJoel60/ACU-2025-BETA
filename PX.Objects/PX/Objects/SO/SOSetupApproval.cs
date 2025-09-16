// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSetupApproval
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
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.SO;

/// <summary>The settings for approval of SO Orders.</summary>
[PXCacheName("SO Approval")]
public class SOSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  /// <summary>
  /// Specifies (if set to <c>true</c>) that the approval map is applied to orders of the <see cref="P:PX.Objects.SO.SOSetupApproval.OrderType" /> type.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Specifies the order type to which the approval map is applied.
  /// </summary>
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXFieldDescription]
  [SOSetupApproval.ApprovableOrderTypeSelector]
  [PXUIField]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  /// <summary>The surrogate identifier of the record.</summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  /// <summary>
  /// Specifies the assignment map that will be used to walk an SO Order through the approval process.
  /// </summary>
  [PXDBInt]
  [PXDefault]
  [PXCheckUnique(new Type[] {typeof (SOSetupApproval.orderType)})]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeSalesOrder>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
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

  public class PK : PrimaryKeyOf<SOSetupApproval>.By<SOSetupApproval.approvalID>
  {
    public static SOSetupApproval Find(PXGraph graph, int? approvalID, PKFindOptions options = 0)
    {
      return (SOSetupApproval) PrimaryKeyOf<SOSetupApproval>.By<SOSetupApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOSetupApproval>.By<SOSetupApproval.orderType>
    {
    }

    public class ApprovalMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<SOSetupApproval>.By<SOSetupApproval.assignmentMapID>
    {
    }

    public class PendingApprovalNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<SOSetupApproval>.By<SOSetupApproval.assignmentNotificationID>
    {
    }
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetupApproval.isActive>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOSetupApproval.orderType>
  {
  }

  public class ApprovableOrderTypeSelectorAttribute : PXCustomSelectorAttribute
  {
    public ApprovableOrderTypeSelectorAttribute()
      : base(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<KeysRelation<Field<SOOrderTypeOperation.orderType>.IsRelatedTo<SOOrderType.orderType>.AsSimpleKey.WithTablesOf<SOOrderType, SOOrderTypeOperation>, SOOrderType, SOOrderTypeOperation>.And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOrderType.defaultOperation>>>>>))
    {
    }

    protected virtual IEnumerable GetRecords()
    {
      FbqlSelect<SelectFromBase<SOOrderType, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderTypeOperation>.On<KeysRelation<Field<SOOrderTypeOperation.orderType>.IsRelatedTo<SOOrderType.orderType>.AsSimpleKey.WithTablesOf<SOOrderType, SOOrderTypeOperation>, SOOrderType, SOOrderTypeOperation>.And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOrderType.defaultOperation>>>>>, SOOrderType>.View view = new FbqlSelect<SelectFromBase<SOOrderType, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderTypeOperation>.On<KeysRelation<Field<SOOrderTypeOperation.orderType>.IsRelatedTo<SOOrderType.orderType>.AsSimpleKey.WithTablesOf<SOOrderType, SOOrderTypeOperation>, SOOrderType, SOOrderTypeOperation>.And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOrderType.defaultOperation>>>>>, SOOrderType>.View(this._Graph);
      if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
        ((PXSelectBase<SOOrderType>) view).WhereAnd<Where<BqlOperand<SOOrderTypeOperation.iNDocType, IBqlString>.IsNotEqual<INTranType.transfer>>>();
      return (IEnumerable) ((PXSelectBase<SOOrderType>) view).Select(Array.Empty<object>());
    }
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSetupApproval.approvalID>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetupApproval.lastModifiedDateTime>
  {
  }
}
