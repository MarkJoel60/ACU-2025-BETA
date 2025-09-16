// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ReportNotePrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable enable
namespace PX.Objects.PM;

public class ReportNotePrimaryGraphAttribute : PXPrimaryGraphBaseAttribute
{
  protected readonly string NoteFieldName;

  public ReportNotePrimaryGraphAttribute(string noteFieldName)
  {
    this.NoteFieldName = noteFieldName;
  }

  public virtual Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    Type preferedType)
  {
    Type type1 = cache.Graph.GetType();
    if (row == null)
      return type1;
    Guid? nullable = cache.GetValue(row, this.NoteFieldName) as Guid?;
    if (!nullable.HasValue)
      return type1;
    EntityHelper entityHelper = new EntityHelper(cache.Graph);
    object entityRow = entityHelper.GetEntityRow(nullable, true);
    if (entityRow == null)
      return type1;
    Type type2 = entityHelper.GetPrimaryGraph(entityRow, checkRights)?.GetType();
    if (type2 != (Type) null)
      row = entityRow;
    return (object) type2 != null ? type2 : type1;
  }
}
