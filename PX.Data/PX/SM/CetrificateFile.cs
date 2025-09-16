// Decompiled with JetBrains decompiler
// Type: PX.SM.CetrificateFile
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[CertificateFile]
[Serializable]
public class CetrificateFile : Certificate
{
  private Guid? _FileID;

  [PXDBGuid(false, BqlField = typeof (NoteDoc.fileID))]
  public Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  public abstract class fileID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  CetrificateFile.fileID>
  {
  }
}
