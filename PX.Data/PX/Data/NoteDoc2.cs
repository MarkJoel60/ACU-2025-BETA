// Decompiled with JetBrains decompiler
// Type: PX.Data.NoteDoc2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

public class NoteDoc2 : NoteDoc
{
  public new abstract class noteID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  NoteDoc2.noteID>
  {
  }

  public new abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NoteDoc2.fileID>
  {
  }
}
