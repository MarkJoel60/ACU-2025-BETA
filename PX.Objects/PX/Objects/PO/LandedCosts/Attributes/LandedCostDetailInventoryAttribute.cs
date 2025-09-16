// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.Attributes.LandedCostDetailInventoryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts.Attributes;

public class LandedCostDetailInventoryAttribute : PXDimensionSelectorAttribute
{
  public LandedCostDetailInventoryAttribute()
    : base("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), new Type[2]
    {
      typeof (PX.Objects.IN.InventoryItem.inventoryCD),
      typeof (PX.Objects.IN.InventoryItem.descr)
    })
  {
    this.DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr);
    ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new LandedCostDetailInventoryAttribute.CustomSelector();
  }

  public class CustomSelector : PXCustomSelectorAttribute
  {
    public CustomSelector()
      : base(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), new Type[2]
      {
        typeof (PX.Objects.IN.InventoryItem.inventoryCD),
        typeof (PX.Objects.IN.InventoryItem.descr)
      })
    {
      ((PXSelectorAttribute) this).SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD);
      ((PXSelectorAttribute) this).DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr);
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    }

    protected virtual IEnumerable GetRecords()
    {
      int?[] array = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) PXSelectBase<POLandedCostReceiptLine, PXSelect<POLandedCostReceiptLine, Where<POLandedCostReceiptLine.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostReceiptLine.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>>.Config>.Select(this._Graph, Array.Empty<object>())).Select<POLandedCostReceiptLine, int?>((Func<POLandedCostReceiptLine, int?>) (t => t.InventoryID)).Distinct<int?>().ToArray<int?>();
      if (!((IEnumerable<int?>) array).Any<int?>())
        return (IEnumerable) new List<PX.Objects.IN.InventoryItem>();
      return (IEnumerable) GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectReadonly<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, In<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(this._Graph, new object[1]
      {
        (object) array
      }));
    }

    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      PX.Objects.IN.InventoryItem inventoryItem1 = (PX.Objects.IN.InventoryItem) null;
      if (e.NewValue != null)
      {
        PX.Objects.IN.InventoryItem inventoryItem2;
        if (!(e.NewValue is int))
          inventoryItem2 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
          {
            e.NewValue
          }));
        else
          inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, (int?) e.NewValue);
        inventoryItem1 = inventoryItem2;
      }
      if (inventoryItem1 != null)
      {
        e.NewValue = (object) inventoryItem1.InventoryID;
        ((CancelEventArgs) e).Cancel = true;
      }
      else if (e.NewValue != null)
        throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", new object[2]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName,
          e.NewValue
        }));
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      object returnValue = e.ReturnValue;
      e.ReturnValue = (object) null;
      ((PXSelectorAttribute) this).FieldSelecting(sender, e);
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, (int?) returnValue);
      if (inventoryItem != null)
      {
        e.ReturnValue = (object) inventoryItem.InventoryCD;
      }
      else
      {
        if (e.Row == null)
          return;
        e.ReturnValue = (object) null;
      }
    }
  }
}
