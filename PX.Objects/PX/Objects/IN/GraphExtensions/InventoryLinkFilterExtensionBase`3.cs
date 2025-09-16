// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryLinkFilterExtensionBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.GraphExtensions.Abstract;
using System.Collections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class InventoryLinkFilterExtensionBase<TGraph, TGraphFilter, TGraphFilterInventoryID> : 
  EntityLinkFilterExtensionBase<TGraph, TGraphFilter, TGraphFilterInventoryID, InventoryLinkFilter, InventoryLinkFilter.inventoryID, int?>
  where TGraph : PXGraph, PXImportAttribute.IPXPrepareItems, PXImportAttribute.IPXProcess
  where TGraphFilter : class, IBqlTable, new()
  where TGraphFilterInventoryID : class, IBqlField, IImplement<IBqlInt>
{
  [PXVirtualDAC]
  [PXImport]
  [PXReadOnlyView]
  public PXSelect<InventoryLinkFilter> SelectedItems;
  public PXAction<TGraphFilter> SelectItems;

  protected override string EntityViewName => "SelectedItems";

  public IEnumerable selectedItems() => (IEnumerable) this.GetEntities();

  [PXButton]
  [PXUIField(DisplayName = "List")]
  public virtual void selectItems()
  {
    ((PXSelectBase<InventoryLinkFilter>) this.SelectedItems).AskExt();
  }

  public abstract class AttachedInventoryDescription<TSelf> : 
    InventoryLinkFilter.AttachedInventoryDescription<TSelf, TGraph>
    where TSelf : PXFieldAttachedTo<InventoryLinkFilter>.By<TGraph>.AsString
  {
  }
}
