// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.RQ.GraphExtensions.RqRequisitionEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.RQ;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.RQ.GraphExtensions;

public class RqRequisitionEntryExt : PXGraphExtension<RQRequisitionEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Only Purchase Orders are allowed.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.pONbr> e)
  {
  }
}
