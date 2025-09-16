// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemPlanSyncOnly`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ItemPlanSyncOnly<TGraph, TItemPlanSource> : 
  ItemPlanBase<TGraph, TItemPlanSource>
  where TGraph : PXGraph
  where TItemPlanSource : class, IItemPlanSource, IBqlTable, new()
{
  public override void Initialize()
  {
    base.Initialize();
    if (this.Base.Views.Caches.Contains(typeof (TItemPlanSource)))
      return;
    this.Base.Views.Caches.Add(typeof (TItemPlanSource));
  }
}
