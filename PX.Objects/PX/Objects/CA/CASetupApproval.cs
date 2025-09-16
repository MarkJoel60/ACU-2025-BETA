// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASetupApproval
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
namespace PX.Objects.CA;

/// <summary>
/// The map of persons that approve documents in cash management.
/// </summary>
[PXCacheName("CA Approval Preferences")]
[Serializable]
public class CASetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  /// <summary>
  /// The type of entity that should be assigned to the approval.
  /// The entity type depends on the form where the entity has been created and can be one of the following:
  /// <list type="bullet">
  /// <item><description>A cash transaction for the approval of cash transactions created on the Cash Transactions (CA304000) form</description></item>
  /// <item><description>A reconciliation statement for the approval of reconciliation statements created on the Reconciliation Statements (CA302000) form</description></item>
  /// </list>
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Type")]
  public virtual 
  #nullable disable
  string GraphType { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, In3<AssignmentMapType.AssignmentMapTypeCashTransaction, AssignmentMapType.AssignmentMapTypeReconciliationStatements>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>, And<EPAssignmentMap.graphType, Equal<Current<CASetupApproval.graphType>>>>>>), DescriptionField = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  [PXCheckUnique(new Type[] {})]
  public virtual int? AssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Notification.notificationID), DescriptionField = typeof (Notification.name))]
  [PXUIField(DisplayName = "Pending Approval Notification")]
  public virtual int? AssignmentNotificationID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

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

  public class PK : PrimaryKeyOf<CASetupApproval>.By<CASetupApproval.approvalID>
  {
    public static CASetupApproval Find(PXGraph graph, int? approvalID, PKFindOptions options = 0)
    {
      return (CASetupApproval) PrimaryKeyOf<CASetupApproval>.By<CASetupApproval.approvalID>.FindBy(graph, (object) approvalID, options);
    }
  }

  public static class FK
  {
    public class AssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<CASetupApproval>.By<CASetupApproval.assignmentMapID>
    {
    }

    public class Notification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<CASetupApproval>.By<CASetupApproval.assignmentNotificationID>
    {
    }
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASetupApproval.approvalID>
  {
  }

  public abstract class graphType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASetupApproval.graphType>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetupApproval.assignmentNotificationID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetupApproval.isActive>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CASetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CASetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASetupApproval.lastModifiedDateTime>
  {
  }
}
