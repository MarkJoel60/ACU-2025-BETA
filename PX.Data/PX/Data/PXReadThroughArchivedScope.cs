// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReadThroughArchivedScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public sealed class PXReadThroughArchivedScope : IDisposable
{
  private bool prevReadThroughArchived;
  private bool prevReadOnlyArchived;

  public PXReadThroughArchivedScope()
    : this(false)
  {
  }

  public PXReadThroughArchivedScope(bool onlyArchived)
  {
    this.prevReadThroughArchived = PXDatabase.ReadThroughArchived;
    this.prevReadOnlyArchived = PXDatabase.ReadOnlyArchived;
    PXDatabase.ReadThroughArchived = true;
    if (!onlyArchived)
      return;
    PXDatabase.ReadOnlyArchived = true;
  }

  void IDisposable.Dispose()
  {
    PXDatabase.ReadThroughArchived = this.prevReadThroughArchived;
    PXDatabase.ReadOnlyArchived = this.prevReadOnlyArchived;
  }
}
