// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.Tag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

/// <summary>A tag for classification of user files.</summary>
[PXCacheName("Tag")]
[PXPrimaryGraph(typeof (TagMaint))]
public class Tag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier of the tag.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? TagID { get; set; }

  /// <summary>A tag name defined by a user.</summary>
  [PXDBString(32 /*0x20*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXCheckUniqueTag]
  [PXUIField(DisplayName = "Tag Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  public virtual string? TagCD { get; set; }

  /// <summary>The identifier of the parent tag.</summary>
  [PXDBGuid(false)]
  [PXDefault("11111111-1111-1111-1111-111111111111")]
  [PXParent(typeof (Select<Tag, Where<Tag.tagID, Equal<Current<Tag.parentTagID>>>>))]
  [PXUIField(DisplayName = "Parent Tag", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? ParentTagID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[]? tstamp { get; set; }

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

  public class PK : PrimaryKeyOf<Tag>.By<Tag.tagID>
  {
    public static Tag Find(PXGraph graph, Guid tagID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Tag>.By<Tag.tagID>.FindBy(graph, (object) tagID, options);
    }
  }

  public class UK : PrimaryKeyOf<Tag>.By<Tag.tagCD>
  {
    public static Tag Find(PXGraph graph, string tagCD, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Tag>.By<Tag.tagCD>.FindBy(graph, (object) tagCD, options);
    }
  }

  public abstract class tagID : BqlType<IBqlGuid, Guid>.Field<Tag.tagID>
  {
  }

  public abstract class tagCD : BqlType<IBqlString, string>.Field<Tag.tagCD>
  {
  }

  public abstract class parentTagID : BqlType<IBqlGuid, Guid>.Field<Tag.parentTagID>
  {
  }

  public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<Tag.noteID>
  {
  }

  public abstract class Tstamp : BqlType<IBqlByteArray, byte[]>.Field<Tag.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<Tag.createdByID>
  {
  }

  public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<Tag.createdByScreenID>
  {
  }

  public abstract class createdDateTime : BqlType<IBqlDateTime, System.DateTime>.Field<Tag.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<Tag.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<Tag.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<Tag.lastModifiedDateTime>
  {
  }
}
