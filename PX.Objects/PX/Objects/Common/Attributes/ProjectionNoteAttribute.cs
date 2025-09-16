// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.ProjectionNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class ProjectionNoteAttribute : PXNoteAttribute
{
  protected Type EntityType { get; set; }

  public ProjectionNoteAttribute(Type entityType) => this.EntityType = entityType;

  protected virtual bool IsVirtualTable(Type table) => false;

  protected virtual string GetEntityType(PXCache cache, Guid? noteId) => this.EntityType.FullName;
}
