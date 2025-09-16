// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryLinkFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;

#nullable enable
namespace PX.Objects.IN.GraphExtensions;

/// <exclude />
[PXCacheName("Inventory List Filter")]
public class InventoryLinkFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Inventory(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  InventoryLinkFilter.inventoryID>
  {
  }

  [PXUIField(DisplayName = "Description", Enabled = false)]
  public abstract class AttachedInventoryDescription<TSelf, TGraph> : 
    PXFieldAttachedTo<InventoryLinkFilter>.By<TGraph>.AsString.Named<TSelf>
    where TSelf : PXFieldAttachedTo<InventoryLinkFilter>.By<TGraph>.AsString
    where TGraph : PXGraph
  {
    public override string GetValue(InventoryLinkFilter Row)
    {
      return InventoryItem.PK.Find((PXGraph) this.Base, (int?) Row?.InventoryID)?.Descr;
    }
  }
}
