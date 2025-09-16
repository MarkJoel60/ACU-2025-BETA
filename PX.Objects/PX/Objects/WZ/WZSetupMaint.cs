// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.WZ;

public class WZSetupMaint : PXGraph<WZSetupMaint>
{
  public PXSelect<WZSetup> Setup;
  public PXAction<WZSetup> enableWizards;
  public PXAction<WZSetup> disableWizards;

  protected virtual IEnumerable setup()
  {
    PXCache cache = ((PXSelectBase) this.Setup).Cache;
    PXResultset<WZSetup> pxResultset = PXSelectBase<WZSetup, PXSelect<WZSetup>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>());
    if (pxResultset.Count == 0)
    {
      WZSetup wzSetup = (WZSetup) cache.Insert((object) new WZSetup());
      cache.IsDirty = false;
      pxResultset.Add(new PXResult<WZSetup>(wzSetup));
    }
    else if (cache.Current == null)
      cache.SetStatus((object) PXResultset<WZSetup>.op_Implicit(pxResultset), (PXEntryStatus) 0);
    return (IEnumerable) pxResultset;
  }

  public virtual IEnumerable EnableWizards(PXAdapter adapter)
  {
    WZSetup wzSetup = PXResultset<WZSetup>.op_Implicit(((PXSelectBase<WZSetup>) this.Setup).Select(Array.Empty<object>()));
    wzSetup.WizardsStatus = new bool?(true);
    ((PXSelectBase<WZSetup>) this.Setup).Update(wzSetup);
    ((PXGraph) this).Actions.PressSave();
    PXSiteMap.Provider.Clear();
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    foreach (PXResult<WZScenario> pxResult in PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.nodeID, Equal<Required<WZScenario.nodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) Guid.Empty
    }))
    {
      WZScenario wzScenario = PXResult<WZScenario>.op_Implicit(pxResult);
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = wzScenario;
      ((PXAction) instance.activateScenarioWithoutRefresh).Press();
    }
    return adapter.Get();
  }

  public virtual IEnumerable DisableWizards(PXAdapter adapter)
  {
    WZSetup wzSetup = PXResultset<WZSetup>.op_Implicit(((PXSelectBase<WZSetup>) this.Setup).Select(Array.Empty<object>()));
    wzSetup.WizardsStatus = new bool?(false);
    ((PXSelectBase<WZSetup>) this.Setup).Update(wzSetup);
    ((PXGraph) this).Actions.PressSave();
    PXSiteMap.Provider.Clear();
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    foreach (PXResult<WZScenario> pxResult in PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.status, Equal<Required<WZScenario.status>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) "AC"
    }))
    {
      WZScenario wzScenario = PXResult<WZScenario>.op_Implicit(pxResult);
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = wzScenario;
      ((PXAction) instance.completeScenarioWithoutRefresh).Press();
    }
    return adapter.Get();
  }
}
