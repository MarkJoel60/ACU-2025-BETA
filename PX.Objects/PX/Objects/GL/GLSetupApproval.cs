// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLSetupApproval
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
namespace PX.Objects.GL;

/// <summary>The settings for approval of GL Transactions.</summary>
[PXCacheName("GL Approval Preferences")]
[Serializable]
public class GLSetupApproval : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssignedMap
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ApprovalID { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXSelector(typeof (SearchFor<EPAssignmentMap.assignmentMapID>.In<SelectFromBase<EPAssignmentMap, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPAssignmentMap.entityType, IBqlString>.IsEqual<AssignmentMapType.AssignmentMapTypeGLBatch>>>), DescriptionField = typeof (EPAssignmentMap.name), SubstituteKey = typeof (EPAssignmentMap.name))]
  [PXUIField(DisplayName = "Approval Map")]
  [PXCheckUnique(new Type[] {typeof (GLSetupApproval.batchType)})]
  public virtual int? AssignmentMapID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Notification.notificationID), DescriptionField = typeof (Notification.name), SubstituteKey = typeof (Notification.name))]
  [PXUIField(DisplayName = "Pending Approval Notification")]
  public virtual int? AssignmentNotificationID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the approval map is applied to documents of the <see cref="!:DocType" /> type.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Specifies the batch type to which the approval map is applied.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault("H")]
  [BatchTypeCode.List]
  [PXUIField]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string BatchType { get; set; }

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
  public class PK : PrimaryKeyOf<GLSetupApproval>.By<GLSetupApproval.approvalID>
  {
    public static GLSetupApproval Find(PXGraph graph, int? ApprovalID, PKFindOptions options = 0)
    {
      return (GLSetupApproval) PrimaryKeyOf<GLSetupApproval>.By<GLSetupApproval.approvalID>.FindBy(graph, (object) ApprovalID, options);
    }
  }

  public class EPSetting : 
    EPApprovalSettings<GLSetupApproval, GLSetupApproval.batchType, BatchTypeCode, BatchStatus.hold, BatchStatus.pendingApproval, BatchStatus.rejected>
  {
  }

  public abstract class approvalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLSetupApproval.approvalID>
  {
  }

  public abstract class assignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLSetupApproval.assignmentMapID>
  {
  }

  public abstract class assignmentNotificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLSetupApproval.assignmentNotificationID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLSetupApproval.isActive>
  {
  }

  public abstract class batchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLSetupApproval.batchType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLSetupApproval.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLSetupApproval.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetupApproval.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLSetupApproval.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLSetupApproval.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLSetupApproval.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLSetupApproval.lastModifiedDateTime>
  {
  }
}
