// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.SubcontractLinkAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class SubcontractLinkAttribute : PXEventSubscriberAttribute
{
  public virtual void CacheAttached(PXCache cache)
  {
    string str = $"{cache.GetItemType().Name}${this._FieldName}$Link";
    // ISSUE: method pointer
    cache.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(SubcontractLinkAttribute.GetDacOfPrimaryView(cache)), (object) cache.Graph, (object) str, (object) new PXButtonDelegate((object) this, __methodptr(ViewSubcontract)), (object) SubcontractLinkAttribute.GetEventSubscriberAttributes());
    cache.Graph.Actions[str].SetVisible(false);
  }

  private IEnumerable ViewSubcontract(PXAdapter adapter)
  {
    PXCache cach = adapter.View.Graph.Caches[this.BqlTable];
    object orderNbr = cach.GetValue(cach.Current, this._FieldName);
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = this.GetPoOrder(adapter.View.Graph, orderNbr);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  private POOrder GetPoOrder(PXGraph graph, object orderNbr)
  {
    return ((PXSelectBase<POOrder>) new PXSelect<POOrder, Where<POOrder.orderType, Equal<POOrderType.regularSubcontract>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>(graph)).SelectSingle(new object[1]
    {
      orderNbr
    });
  }

  private static PXEventSubscriberAttribute[] GetEventSubscriberAttributes()
  {
    return new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        MapEnableRights = (PXCacheRights) 1
      }
    };
  }

  private static Type GetDacOfPrimaryView(PXCache cache)
  {
    return !((Dictionary<string, PXView>) cache.Graph.Views).ContainsKey(cache.Graph.PrimaryView) ? cache.BqlTable : cache.Graph.Views[cache.Graph.PrimaryView].GetItemType();
  }
}
