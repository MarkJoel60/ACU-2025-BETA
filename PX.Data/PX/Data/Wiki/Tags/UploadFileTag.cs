// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.UploadFileTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

/// <summary>A relation between a tag and a user file.</summary>
[PXCacheName("File Tag")]
public class UploadFileTag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier of the user file.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "File ID", Enabled = false)]
  public virtual Guid? FileID { get; set; }

  /// <summary>The unique identifier of the tag.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? TagID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string? LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<UploadFileTag>.By<UploadFileTag.fileID, UploadFileTag.tagID>
  {
    public static UploadFileTag Find(
      PXGraph graph,
      Guid fileID,
      Guid tagID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UploadFileTag>.By<UploadFileTag.fileID, UploadFileTag.tagID>.FindBy(graph, (object) fileID, (object) tagID, options);
    }
  }

  public abstract class fileID : BqlType<IBqlGuid, Guid>.Field<UploadFileTag.fileID>
  {
  }

  public abstract class tagID : BqlType<IBqlGuid, Guid>.Field<UploadFileTag.tagID>
  {
  }

  public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<UploadFileTag.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<UploadFileTag.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<UploadFileTag.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<UploadFileTag.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<UploadFileTag.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<UploadFileTag.lastModifiedDateTime>
  {
  }
}
