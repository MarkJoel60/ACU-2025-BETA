// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Groups of projects for restricting user access via row-level security.
/// </summary>
[PXCacheName("Project Group")]
[PXPrimaryGraph(typeof (PMProjectGroupMaint))]
[Serializable]
public class PMProjectGroup : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  /// <summary>The unique string identifier of the project group.</summary>
  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PXReferentialIntegrityCheck]
  public virtual 
  #nullable disable
  string ProjectGroupID { get; set; }

  /// <summary>The description of the project group.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// Is project group active or not.
  /// User cannot change project group of any project to inactive one.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// An unbound field used in the user interface (PM102000)
  /// to include the project group into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// System field used for restrictions via row-level security.
  /// </summary>
  [PXDBGroupMask(HideFromEntityTypesList = true)]
  public virtual byte[] GroupMask { get; set; }

  public abstract class projectGroupID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectGroup.projectGroupID>
  {
    public const int Length = 15;
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectGroup.description>
  {
    public const int Length = 255 /*0xFF*/;
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProjectGroup.isActive>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMProjectGroup.included>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProjectGroup.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMProjectGroup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectGroup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectGroup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMProjectGroup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProjectGroup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMProjectGroup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProjectGroup.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMProjectGroup.groupMask>
  {
  }

  public class PK : PrimaryKeyOf<PMProjectGroup>.By<PMProjectGroup.projectGroupID>
  {
    public static PMProjectGroup Find(PXGraph graph, string projectGroupID)
    {
      return (PMProjectGroup) PrimaryKeyOf<PMProjectGroup>.By<PMProjectGroup.projectGroupID>.FindBy(graph, (object) projectGroupID, (PKFindOptions) 0);
    }
  }
}
