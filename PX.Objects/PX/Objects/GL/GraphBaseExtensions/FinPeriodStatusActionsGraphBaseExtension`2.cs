// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GraphBaseExtensions.FinPeriodStatusActionsGraphBaseExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods;
using System.Collections;

#nullable disable
namespace PX.Objects.GL.GraphBaseExtensions;

public class FinPeriodStatusActionsGraphBaseExtension<TGraph, TYear> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TYear : class, IBqlTable, IFinYear, new()
{
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXMenuAction<TYear> ActionsMenu;
  public PXAction<TYear> Open;
  public PXAction<TYear> Close;
  public PXAction<TYear> Lock;
  public PXAction<TYear> Deactivate;
  public PXAction<TYear> Reopen;
  public PXAction<TYear> Unlock;

  protected virtual void _(Events.RowSelected<TYear> e)
  {
    bool closedPeriod = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<TYear>>) e).Cache.Graph.GetService<IFinPeriodUtils>().CanPostToClosedPeriod();
    ((PXAction) this.Reopen).SetEnabled(closedPeriod);
    ((PXAction) this.Unlock).SetEnabled(closedPeriod);
  }

  private void RedirectToStatusProcessing(string action)
  {
    TYear current = (TYear) ((PXCache) GraphHelper.Caches<TYear>((PXGraph) this.Base)).Current;
    FinPeriodStatusProcess instance = PXGraph.CreateInstance<FinPeriodStatusProcess>();
    FinPeriodStatusProcess.FinPeriodStatusProcessParameters processParameters1 = new FinPeriodStatusProcess.FinPeriodStatusProcessParameters();
    int? organizationId = current.OrganizationID;
    int num = 0;
    processParameters1.OrganizationID = organizationId.GetValueOrDefault() == num & organizationId.HasValue ? new int?() : current.OrganizationID;
    processParameters1.Action = action;
    FinPeriodStatusProcess.FinPeriodStatusProcessParameters processParameters2 = processParameters1;
    FinPeriodStatusProcess.FinPeriodStatusProcessParameters processParameters3 = GraphHelper.InitNewRow<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>(GraphHelper.Caches<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>((PXGraph) instance), processParameters2);
    if (FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.GetDirection(action) == FinPeriodStatusProcess.FinPeriodStatusProcessParameters.action.Direction.Direct)
    {
      processParameters3.ToYear = current.Year;
      processParameters3.ToYear = string.Compare(processParameters3.ToYear, processParameters3.FirstYear) >= 0 ? processParameters3.ToYear : processParameters3.FirstYear;
      processParameters3.ToYear = string.Compare(processParameters3.ToYear, processParameters3.LastYear) <= 0 ? processParameters3.ToYear : processParameters3.LastYear;
    }
    else
    {
      processParameters3.FromYear = current.Year;
      processParameters3.FromYear = string.Compare(processParameters3.FromYear, processParameters3.FirstYear) >= 0 ? processParameters3.FromYear : processParameters3.FirstYear;
      processParameters3.FromYear = string.Compare(processParameters3.FromYear, processParameters3.LastYear) <= 0 ? processParameters3.FromYear : processParameters3.LastYear;
    }
    ((PXSelectBase<FinPeriodStatusProcess.FinPeriodStatusProcessParameters>) instance.Filter).Current = processParameters3;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable open(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Open");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable close(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Close");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable @lock(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Lock");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable deactivate(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Deactivate");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable reopen(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Reopen");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable unlock(PXAdapter adapter)
  {
    this.RedirectToStatusProcessing("Unlock");
    return adapter.Get();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    this.AddMenuActions();
  }

  public virtual void AddMenuActions()
  {
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Open);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Close);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Lock);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Deactivate);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Reopen);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.Unlock);
  }
}
