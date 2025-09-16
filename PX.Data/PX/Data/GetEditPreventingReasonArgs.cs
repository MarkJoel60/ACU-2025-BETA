// Decompiled with JetBrains decompiler
// Type: PX.Data.GetEditPreventingReasonArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.ComponentModel;

#nullable disable
namespace PX.Data;

public sealed class GetEditPreventingReasonArgs : CancelEventArgs
{
  public PXGraph Graph { get; }

  public PXCache Cache { get; }

  public System.Type Field { get; }

  public object NewValue { get; }

  public object Row { get; }

  public bool IsRowDeleting { get; }

  public GetEditPreventingReasonArgs(
    PXCache cache,
    System.Type field,
    object row,
    object newValue,
    bool isRowDeleting = false)
  {
    this.Cache = cache;
    this.Graph = cache.Graph;
    this.Field = field;
    this.NewValue = newValue;
    this.Row = row;
    this.IsRowDeleting = isRowDeleting;
  }
}
