// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYMappingNote
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Api;

public class PXSYMappingNote : PXNoteAttribute
{
  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
  }

  protected override string GetGraphType(PXGraph graph) => typeof (SYProviderMaint).FullName;

  protected override string GetEntityType(PXCache cache, Guid? noteId)
  {
    return typeof (SYProvider).FullName;
  }
}
