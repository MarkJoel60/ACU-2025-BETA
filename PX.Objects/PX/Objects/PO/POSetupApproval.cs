// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POSetupApproval
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Approval")]
[Serializable]
public class POSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  protected int? _ApprovalID;
  protected 
  #nullable disable
  string _OrderType;
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
  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXCheckUnique(new Type[] {typeof (POSetupApproval.orderType)})]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypePurchaseOrder>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
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
  [PXDefault(typeof (Search<POSetup.orderRequestApproval>))]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  public class PK : PrimaryKeyOf<POSetupApproval>.By<POSetupApproval.approvalID>
  {
    public static POSetupApproval Find(PXGraph graph, int? approvalID, PKFindOptions options = 0)
    {
      return (POSetupApproval) PrimaryKeyOf<POSetupApproval>.By<POSetupApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  public static class FK
  {
    public class ApprovalMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<POSetupApproval>.By<POSetupApproval.assignmentMapID>
    {
    }

    public class PendingApprovalNotification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<POSetupApproval>.By<POSetupApproval.assignmentNotificationID>
    {
    }
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POSetupApproval.approvalID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POSetupApproval.orderType>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POSetupApproval.lastModifiedDateTime>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POSetupApproval.isActive>
  {
  }
}
