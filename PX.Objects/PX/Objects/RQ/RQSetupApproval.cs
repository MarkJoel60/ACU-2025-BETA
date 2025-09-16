// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSetupApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[Serializable]
public class RQSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  protected int? _ApprovalID;
  protected 
  #nullable disable
  string _Type;
  protected int? _AssignmentMapID;
  protected int? _AssignmentNotificationID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _IsActive;

  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID
  {
    get => this._ApprovalID;
    set => this._ApprovalID = value;
  }

  [PXDefault]
  [PXDBString(1, IsFixed = true)]
  [RQType.List]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXCheckUnique(new System.Type[] {typeof (RQSetupApproval.type)})]
  [PXSelector(typeof (SearchFor<EPAssignmentMap.assignmentMapID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPAssignmentMap.entityType, Equal<Switch<IBqlString, TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<Current<RQSetupApproval.type>, IBqlString>.IsEqual<RQType.requestItem>>, AssignmentMapType.AssignmentMapTypePurchaseRequestItem>, AssignmentMapType.AssignmentMapTypePurchaseRequisition>.When<BqlOperand<Current<RQSetupApproval.type>, IBqlString>.IsEqual<RQType.requisition>>.ElseNull>>>>>.And<BqlOperand<EPAssignmentMap.mapType, IBqlInt>.IsNotEqual<EPMapType.assignment>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  public virtual int? AssignmentMapID
  {
    get => this._AssignmentMapID;
    set => this._AssignmentMapID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Notification.notificationID), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Pending Approval Notification")]
  public virtual int? AssignmentNotificationID
  {
    get => this._AssignmentNotificationID;
    set => this._AssignmentNotificationID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<RQSetupApproval.type, Equal<RQType.requestItem>>, Current<RQSetup.requestApproval>, Current<RQSetup.requisitionApproval>>))]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSetupApproval.approvalID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSetupApproval.type>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQSetupApproval.lastModifiedDateTime>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQSetupApproval.isActive>
  {
  }
}
