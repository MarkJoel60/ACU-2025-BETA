// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.FolderTag
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

[PXHidden]
public class FolderTag : Tag
{
  public new abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FolderTag.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<FolderTag.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<FolderTag.createdDateTime>
  {
  }

  public abstract class description : BqlType<IBqlString, string>.Field<FolderTag.description>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<IBqlGuid, Guid>.Field<FolderTag.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<IBqlString, string>.Field<FolderTag.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<FolderTag.lastModifiedDateTime>
  {
  }

  public new abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FolderTag.noteID>
  {
  }

  public new abstract class parentTagID : BqlType<IBqlGuid, Guid>.Field<FolderTag.parentTagID>
  {
  }

  public new abstract class tagCD : BqlType<IBqlString, string>.Field<FolderTag.tagCD>
  {
  }

  public new abstract class tagID : BqlType<IBqlGuid, Guid>.Field<FolderTag.tagID>
  {
  }

  public new abstract class Tstamp : BqlType<IBqlByteArray, byte[]>.Field<FolderTag.Tstamp>
  {
  }
}
