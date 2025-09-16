// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.BorrowedNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>
/// Allows a virtual object to borrow the note ID of another entity and use it to attach files.
/// </summary>
public class BorrowedNoteAttribute : PXNoteAttribute
{
  public Type TargetEntityType { get; }

  public Type TargetGraphType { get; }

  public BorrowedNoteAttribute(Type targetEntityType, Type targetGraphType)
  {
    this.TargetEntityType = targetEntityType;
    this.TargetGraphType = targetGraphType;
  }

  protected virtual string GetEntityType(PXCache cache, Guid? noteId)
  {
    return this.TargetEntityType?.FullName ?? base.GetEntityType(cache, noteId);
  }

  protected virtual string GetGraphType(PXGraph graph)
  {
    return this.TargetGraphType?.FullName ?? base.GetGraphType(graph);
  }
}
