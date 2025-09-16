// Decompiled with JetBrains decompiler
// Type: PX.Data.INotable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Represents a document that has a link to the <see cref="T:PX.Data.Note" /> record,
/// which means that a document may have additional notes.
/// </summary>
public interface INotable
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Note">Note</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  Guid? NoteID { get; set; }
}
