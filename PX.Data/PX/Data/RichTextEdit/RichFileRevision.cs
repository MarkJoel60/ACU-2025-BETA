// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.RichFileRevision
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.RichTextEdit;

/// <exclude />
[Serializable]
public class RichFileRevision : UploadFileRevision
{
  public override 
  #nullable disable
  byte[] BlobData
  {
    get => (byte[]) null;
    set
    {
    }
  }

  public override byte[] Data
  {
    get => (byte[]) null;
    set
    {
    }
  }

  /// <exclude />
  public new abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RichFileRevision.fileID>
  {
  }

  public new abstract class fileRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RichFileRevision.fileRevisionID>
  {
  }

  /// <exclude />
  public new abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RichFileRevision.comment>
  {
  }
}
