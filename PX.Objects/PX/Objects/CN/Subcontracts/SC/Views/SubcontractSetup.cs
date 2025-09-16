// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Views.SubcontractSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Views;

public class SubcontractSetup : PXSetup<POSetup>
{
  public SubcontractSetup(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    graph.Initialized += new PXGraphInitializedDelegate((object) this, __methodptr(Initialized));
  }

  private void Initialized(PXGraph graph)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    graph.Defaults[typeof (POSetup)] = new PXGraph.GetDefaultDelegate((object) new SubcontractSetup.\u003C\u003Ec__DisplayClass1_0()
    {
      \u003C\u003E4__this = this,
      baseHandler = graph.Defaults[typeof (POSetup)]
    }, __methodptr(\u003CInitialized\u003Eb__0));
  }

  private POSetup GetPurchaseOrderSetup(PXGraph.GetDefaultDelegate baseHandler)
  {
    POSetup poSetup = (POSetup) baseHandler.Invoke();
    return PXCache<POSetup>.GetExtension<PoSetupExt>(poSetup).IsSubcontractSetupSaved.GetValueOrDefault() ? poSetup : throw new PXSetupNotEnteredException<SubcontractSetup.SubcontractsPreferences>("The required configuration data is not entered on the {0} form.", Array.Empty<object>());
  }

  [PXPrimaryGraph(typeof (SubcontractSetupMaint))]
  [PXCacheName("Subcontracts Preferences")]
  private class SubcontractsPreferences : POSetup
  {
  }
}
