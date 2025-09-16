// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.CopiedNoteIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <exclude />
public class CopiedNoteIDAttribute : PXNoteAttribute
{
  protected Type _entityType;

  public Type EntityType
  {
    get => this._entityType;
    set => this._entityType = value;
  }

  public Type GraphType { get; set; }

  public CopiedNoteIDAttribute(Type entityType)
  {
    this._entityType = entityType;
    this._ForceRetain = true;
  }

  [Obsolete("The constructor is obsolete. Use the constructor without \"searches\" parameter. This CopiedNoteIDAttribute constructor is exactly the same. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public CopiedNoteIDAttribute(Type entityType, params Type[] searches)
    : base(searches)
  {
    this._entityType = entityType;
    this._ForceRetain = true;
  }

  protected virtual string GetEntityType(PXCache cache, Guid? noteId) => this._entityType.FullName;

  protected virtual string GetGraphType(PXGraph graph)
  {
    return this.GraphType != (Type) null ? this.GraphType.FullName : base.GetGraphType(graph);
  }
}
