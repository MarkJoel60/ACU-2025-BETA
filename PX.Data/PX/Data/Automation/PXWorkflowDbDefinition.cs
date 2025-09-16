// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.PXWorkflowDbDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation.State;

#nullable disable
namespace PX.Data.Automation;

internal sealed class PXWorkflowDbDefinition : 
  IPrefetchable<IPXPageIndexingService>,
  IPXCompanyDependent
{
  private static readonly string SLOT_KEY = "PXWorkflowDbDefinition.Screens";
  private IScreenMap _screens = (IScreenMap) new SimpleScreenMap();

  public IScreenMap Screens
  {
    get
    {
      IScreenMap slot = PXContext.GetSlot<IScreenMap>(PXWorkflowDbDefinition.SLOT_KEY);
      if (slot != null)
        return slot;
      PXContext.SetSlot<IScreenMap>(PXWorkflowDbDefinition.SLOT_KEY, this._screens);
      return this._screens;
    }
  }

  void IPrefetchable<IPXPageIndexingService>.Prefetch(IPXPageIndexingService pageIndexingService)
  {
    this._screens = (IScreenMap) new LazyScreenMap(pageIndexingService);
  }
}
