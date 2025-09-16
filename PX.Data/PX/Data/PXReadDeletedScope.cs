// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReadDeletedScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public sealed class PXReadDeletedScope : IDisposable
{
  private bool prevReadDeleted;
  private bool prevReadOnlyDeleted;

  public PXReadDeletedScope(bool onlyDeleted = false)
  {
    this.prevReadDeleted = PXDatabase.ReadDeleted;
    this.prevReadOnlyDeleted = PXDatabase.ReadOnlyDeleted;
    PXDatabase.ReadDeleted = true;
    PXDatabase.ReadOnlyDeleted = onlyDeleted;
  }

  void IDisposable.Dispose()
  {
    PXDatabase.ReadDeleted = this.prevReadDeleted;
    PXDatabase.ReadOnlyDeleted = this.prevReadOnlyDeleted;
  }
}
