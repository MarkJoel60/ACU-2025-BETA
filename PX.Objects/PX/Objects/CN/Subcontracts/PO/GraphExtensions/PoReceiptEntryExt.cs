// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.GraphExtensions.PoReceiptEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.GraphExtensions;

public class PoReceiptEntryExt : PXGraphExtension<POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<SOLineSplit.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<POOrderFilter.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLineS.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POOrderEntry.POOrderS.orderNbr> e)
  {
  }
}
