// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCostProjectionClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CN.ProjectAccounting;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a cost projection class.
/// Cost projection classes are used to define the budget detail level of
/// <see cref="T:PX.Objects.PM.PMCostProjection">cost projection revisions</see>.
/// The records of this type are created and edited through the Cost Projection Classes (PM203500) form
/// (which corresponds to the <see cref="T:PX.Objects.CN.ProjectAccounting.CostProjectionClassMaint" /> graph).
/// </summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Cost Projection Class")]
[PXPrimaryGraph(typeof (CostProjectionClassMaint))]
[Serializable]
public class PMCostProjectionClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;

  /// <summary>The unique identifier of the cost projection class.</summary>
  [PXReferentialIntegrityCheck]
  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string ClassID { get; set; }

  /// <summary>The description of the cost projection class.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the given class can be used in projects.
  /// </summary>
  [PXUIField(DisplayName = "Active")]
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that a cost projection revision prepared for the project includes the cost task level of detail.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Cost Task")]
  public virtual bool? TaskID { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that a cost projection revision prepared for the project includes the account group level of detail.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Account Group")]
  public virtual bool? AccountGroupID { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that a cost projection revision prepared for the project includes the cost code level of detail.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public virtual bool? CostCodeID { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that a cost projection revision prepared for the project includes the inventory item level of detail.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual bool? InventoryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<PMCostProjectionClass>.By<PMCostProjectionClass.classID>
  {
    public static PMCostProjectionClass Find(PXGraph graph, string classID, PKFindOptions options = 0)
    {
      return (PMCostProjectionClass) PrimaryKeyOf<PMCostProjectionClass>.By<PMCostProjectionClass.classID>.FindBy(graph, (object) classID, options);
    }
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMCostProjectionClass.classID>
  {
    public const int Length = 30;
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionClass.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionClass.isActive>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionClass.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionClass.accountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMCostProjectionClass.costCodeID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCostProjectionClass.inventoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMCostProjectionClass.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMCostProjectionClass.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMCostProjectionClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMCostProjectionClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMCostProjectionClass.lastModifiedDateTime>
  {
  }
}
