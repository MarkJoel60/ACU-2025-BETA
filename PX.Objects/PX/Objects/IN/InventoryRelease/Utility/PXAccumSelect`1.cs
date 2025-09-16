// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Utility.PXAccumSelect`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Utility;

public class PXAccumSelect<Table> : PXSelect<Table> where Table : class, IBqlTable, new()
{
  public virtual Table Insert(Table item)
  {
    return ((PXSelectBase<Table>) this).Insert(item) ?? ((PXSelectBase<Table>) this).Locate(item);
  }

  public PXAccumSelect(PXGraph graph)
    : base(graph)
  {
  }

  public PXAccumSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
