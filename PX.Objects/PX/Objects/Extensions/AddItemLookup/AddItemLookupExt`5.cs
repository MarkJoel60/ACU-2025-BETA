// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.AddItemLookup.AddItemLookupExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System.Collections;

#nullable disable
namespace PX.Objects.Extensions.AddItemLookup;

public abstract class AddItemLookupExt<TGraph, TDocument, TItemInfo, TItemFilter, TAddItemParameters> : 
  AddItemLookupBaseExt<TGraph, TDocument, TItemInfo, TItemFilter>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TItemInfo : class, IPXSelectable, IBqlTable, new()
  where TItemFilter : INSiteStatusFilter, IBqlTable, new()
  where TAddItemParameters : class, IBqlTable, new()
{
  [PXCopyPasteHiddenView]
  public PXFilter<TAddItemParameters> addItemParameters;
  public PXAction<TDocument> addAllItems;

  protected override IEnumerable ShowItemsHandler(PXAdapter adapter)
  {
    return this.ItemInfo.AskExt() == WebDialogResult.OK ? this.AddSelectedItems(adapter) : adapter.Get();
  }

  [PXUIField(DisplayName = "Add All", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable AddAllItems(PXAdapter adapter) => this.AddAllItemsHandler(adapter);

  protected abstract IEnumerable AddAllItemsHandler(PXAdapter adapter);
}
