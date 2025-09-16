// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.GraphExtensions.CrossSellAssistance`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.RelatedItems.DAC;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems.GraphExtensions;

public class CrossSellAssistance<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  [PXCopyPasteHiddenFields(new Type[] {})]
  public FbqlSelect<SelectFromBase<INRelatedInventoryUserFeedback, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INRelatedInventoryUserFeedback.inventoryID, 
  #nullable disable
  Equal<P.AsInt>>>>>.And<BqlOperand<
  #nullable enable
  INRelatedInventoryUserFeedback.relatedInventoryID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>, INRelatedInventoryUserFeedback>.View Feedback;

  public bool SkipFeedback { get; set; }

  protected virtual void _(
    Events.FieldUpdated<INRelatedInventory, INRelatedInventory.acceptedMLSuggestion> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) e.NewValue;
    if (!newValue.HasValue)
      return;
    newValue = (bool?) e.NewValue;
    bool? isActive = e.Row.IsActive;
    if (newValue.GetValueOrDefault() == isActive.GetValueOrDefault() & newValue.HasValue == isActive.HasValue)
      return;
    ((PXCache) GraphHelper.Caches<INRelatedInventory>((PXGraph) this.Base)).SetValueExt<INRelatedInventory.isActive>((object) e.Row, e.NewValue);
  }

  protected virtual void _(Events.RowUpdated<INRelatedInventory> e)
  {
    if (this.SkipFeedback || !e.Row.CreatedByPossibleRelatedItem.GetValueOrDefault() || !(e.Row.Relation == "CSELL"))
      return;
    bool? acceptedMlSuggestion1 = e.OldRow.AcceptedMLSuggestion;
    bool? acceptedMlSuggestion2 = e.Row.AcceptedMLSuggestion;
    if (acceptedMlSuggestion1.GetValueOrDefault() == acceptedMlSuggestion2.GetValueOrDefault() & acceptedMlSuggestion1.HasValue == acceptedMlSuggestion2.HasValue)
      return;
    INRelatedInventoryUserFeedback inventoryUserFeedback = PXResultset<INRelatedInventoryUserFeedback>.op_Implicit(((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Select(new object[2]
    {
      (object) e.Row.InventoryID,
      (object) e.Row.RelatedInventoryID
    }));
    acceptedMlSuggestion2 = e.Row.AcceptedMLSuggestion;
    if (acceptedMlSuggestion2.GetValueOrDefault())
    {
      if (inventoryUserFeedback == null)
      {
        ((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Insert(new INRelatedInventoryUserFeedback()
        {
          InventoryID = e.Row.InventoryID,
          RelatedInventoryID = e.Row.RelatedInventoryID,
          IsCrossSell = new bool?(true)
        });
      }
      else
      {
        inventoryUserFeedback.IsCrossSell = new bool?(true);
        ((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Update(inventoryUserFeedback);
      }
    }
    else
    {
      if (inventoryUserFeedback == null)
        return;
      ((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Delete(inventoryUserFeedback);
    }
  }

  protected virtual void _(Events.RowDeleted<INRelatedInventory> e)
  {
    if (this.SkipFeedback || !e.Row.CreatedByPossibleRelatedItem.GetValueOrDefault() || !(e.Row.Relation == "CSELL"))
      return;
    INRelatedInventoryUserFeedback inventoryUserFeedback = PXResultset<INRelatedInventoryUserFeedback>.op_Implicit(((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Select(new object[2]
    {
      (object) e.Row.InventoryID,
      (object) e.Row.RelatedInventoryID
    }));
    if (inventoryUserFeedback == null)
    {
      ((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Insert(new INRelatedInventoryUserFeedback()
      {
        InventoryID = e.Row.InventoryID,
        RelatedInventoryID = e.Row.RelatedInventoryID,
        IsCrossSell = new bool?(false)
      });
    }
    else
    {
      inventoryUserFeedback.IsCrossSell = new bool?(false);
      ((PXSelectBase<INRelatedInventoryUserFeedback>) this.Feedback).Update(inventoryUserFeedback);
    }
  }
}
