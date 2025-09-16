// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanWarehousePath
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public class INScanWarehousePath : 
  WarehouseManagementSystem<
  #nullable disable
  INScanWarehousePath, INScanWarehousePath.Host>
{
  protected override bool UseQtyCorrection => false;

  public ScanPathHeader PathHeader => ScanHeaderExt.Get<ScanPathHeader>(this.Header);

  public ValueSetter<ScanHeader>.Ext<ScanPathHeader> PathSetter
  {
    get => this.HeaderSetter.With<ScanPathHeader>();
  }

  public int? NextPathIndex
  {
    get
    {
      ScanPathHeader pathHeader = this.PathHeader;
      int? nextPathIndex = pathHeader.NextPathIndex;
      int valueOrDefault = nextPathIndex.GetValueOrDefault();
      int num1;
      if (!nextPathIndex.HasValue)
      {
        int num2 = 1;
        pathHeader.NextPathIndex = new int?(num2);
        num1 = num2;
      }
      else
        num1 = valueOrDefault;
      return new int?(num1);
    }
    set
    {
      ValueSetter<ScanHeader>.Ext<ScanPathHeader> pathSetter = this.PathSetter;
      (^ref pathSetter).Set<int?>((Expression<Func<ScanPathHeader, int?>>) (h => h.NextPathIndex), value);
    }
  }

  [BorrowedNote(typeof (INSite), typeof (INSiteMaint))]
  protected virtual void _(Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  protected virtual IEnumerable Location()
  {
    PXResultset<INLocation> collection = PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLocation.siteID, IBqlInt>.IsEqual<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>.Order<By<BqlField<INLocation.pathPriority, IBqlInt>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc>>>.Config>.Select((PXGraph) ((PXGraphExtension<INScanWarehousePath.Host>) this).Base, Array.Empty<object>());
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultSorted = true;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    return (IEnumerable) pxDelegateResult;
  }

  protected override void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    ((PXSelectBase) ((PXGraphExtension<INScanWarehousePath.Host>) this).Base.location).AllowInsert = ((PXSelectBase) ((PXGraphExtension<INScanWarehousePath.Host>) this).Base.location).AllowDelete = ((PXSelectBase) ((PXGraphExtension<INScanWarehousePath.Host>) this).Base.location).AllowUpdate = false;
  }

  protected virtual IEnumerable<ScanMode<INScanWarehousePath>> CreateScanModes()
  {
    return (IEnumerable<ScanMode<INScanWarehousePath>>) new INScanWarehousePath.ScanPathMode[1]
    {
      new INScanWarehousePath.ScanPathMode()
    };
  }

  public class Host : INSiteMaint, ICaptionable
  {
    public INScanWarehousePath WMS => ((PXGraph) this).FindImplementation<INScanWarehousePath>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public sealed class ScanPathMode : 
    BarcodeDrivenStateMachine<INScanWarehousePath, INScanWarehousePath.Host>.ScanMode
  {
    public const string Value = "PATH";

    public virtual string Code => "PATH";

    public virtual string Description => "Scan Path";

    protected virtual IEnumerable<ScanState<INScanWarehousePath>> CreateStates()
    {
      yield return (ScanState<INScanWarehousePath>) new INScanWarehousePath.ScanPathMode.WarehouseState();
      yield return (ScanState<INScanWarehousePath>) new INScanWarehousePath.ScanPathMode.LocationState();
      yield return (ScanState<INScanWarehousePath>) new INScanWarehousePath.ScanPathMode.ConfirmState();
      yield return (ScanState<INScanWarehousePath>) new INScanWarehousePath.ScanPathMode.SetNextIndexState();
    }

    protected virtual IEnumerable<ScanTransition<INScanWarehousePath>> CreateTransitions()
    {
      return ((ScanMode<INScanWarehousePath>) this).StateFlow((Func<ScanStateFlow<INScanWarehousePath>.IFrom, IEnumerable<ScanTransition<INScanWarehousePath>>>) (flow => (IEnumerable<ScanTransition<INScanWarehousePath>>) flow.From<INScanWarehousePath.ScanPathMode.WarehouseState>().NextTo<INScanWarehousePath.ScanPathMode.LocationState>((Action<INScanWarehousePath>) null)));
    }

    protected virtual IEnumerable<ScanCommand<INScanWarehousePath>> CreateCommands()
    {
      yield return (ScanCommand<INScanWarehousePath>) new INScanWarehousePath.ScanPathMode.SetNextIndexCommand();
    }

    protected virtual IEnumerable<ScanRedirect<INScanWarehousePath>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<INScanWarehousePath>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<INScanWarehousePath>) this).Clear<INScanWarehousePath.ScanPathMode.SetNextIndexState>(fullReset);
      ((ScanMode<INScanWarehousePath>) this).Clear<INScanWarehousePath.ScanPathMode.WarehouseState>(fullReset);
      ((ScanMode<INScanWarehousePath>) this).Clear<INScanWarehousePath.ScanPathMode.LocationState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INScanWarehousePath.ScanPathMode.value>
    {
      public value()
        : base("PATH")
      {
      }
    }

    public sealed class WarehouseState : 
      WarehouseManagementSystem<INScanWarehousePath, INScanWarehousePath.Host>.WarehouseState
    {
      protected override bool UseDefaultWarehouse => false;

      protected override bool IsStateSkippable()
      {
        return base.IsStateSkippable() || ((ScanComponent<INScanWarehousePath>) this).Basis.SiteID.HasValue;
      }

      protected override void Apply(INSite site)
      {
        base.Apply(site);
        ((PXSelectBase<INSite>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.site).Current = site;
      }

      protected override void ClearState()
      {
        base.ClearState();
        ((PXSelectBase<INSite>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.site).Current = (INSite) null;
      }
    }

    public sealed class LocationState : 
      WarehouseManagementSystem<INScanWarehousePath, INScanWarehousePath.Host>.LocationState
    {
      protected override void Apply(INLocation location)
      {
        base.Apply(location);
        ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).Current = location;
      }

      protected override void ClearState()
      {
        base.ClearState();
        ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).Current = (INLocation) null;
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<INScanWarehousePath, INScanWarehousePath.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<INScanWarehousePath>) this).Basis.Localize("Confirm assignment of the {0} path index to the {1} location.", new object[2]
          {
            (object) ((ScanComponent<INScanWarehousePath>) this).Basis.NextPathIndex,
            (object) ((ScanComponent<INScanWarehousePath>) this).Basis.SightOf<INLocation.locationCD>((IBqlTable) ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).Current)
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).SetValueExt<INLocation.pathPriority>(((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).Current, (object) ((ScanComponent<INScanWarehousePath>) this).Basis.NextPathIndex);
        ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).UpdateCurrent();
        ((ScanComponent<INScanWarehousePath>) this).Basis.ReportInfo("{0} path index assigned to {1} location.", new object[2]
        {
          (object) ((ScanComponent<INScanWarehousePath>) this).Basis.NextPathIndex,
          (object) ((ScanComponent<INScanWarehousePath>) this).Basis.SightOf<INLocation.locationCD>((IBqlTable) ((PXSelectBase<INLocation>) ((ScanComponent<INScanWarehousePath>) this).Basis.Graph.location).Current)
        });
        INScanWarehousePath basis = ((ScanComponent<INScanWarehousePath>) this).Basis;
        int? nextPathIndex = basis.NextPathIndex;
        basis.NextPathIndex = nextPathIndex.HasValue ? new int?(nextPathIndex.GetValueOrDefault() + 1) : new int?();
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      [PXLocalizable]
      public abstract class Msg : 
        WarehouseManagementSystem<INScanWarehousePath, INScanWarehousePath.Host>.Msg
      {
        public const string Prompt = "Confirm assignment of the {0} path index to the {1} location.";
        public const string PathIndexAssignedToLocation = "{0} path index assigned to {1} location.";
      }
    }

    public sealed class SetNextIndexState : 
      BarcodeDrivenStateMachine<INScanWarehousePath, INScanWarehousePath.Host>.EntityState<ushort?>
    {
      public const string Value = "NIDX";

      public virtual string Code => "NIDX";

      protected virtual string StatePrompt => "Enter the new index of the next path.";

      protected virtual ushort? GetByBarcode(string barcode)
      {
        ushort result;
        return !ushort.TryParse(barcode, out result) ? new ushort?() : new ushort?(result);
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<INScanWarehousePath>) this).Basis.ReportError("The quantity format does not fit the locale settings.", Array.Empty<object>());
      }

      protected virtual void Apply(ushort? nextIndex)
      {
        INScanWarehousePath basis = ((ScanComponent<INScanWarehousePath>) this).Basis;
        ushort? nullable1 = nextIndex;
        int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        basis.NextPathIndex = nullable2;
      }

      protected virtual void ClearState()
      {
        ((ScanComponent<INScanWarehousePath>) this).Basis.NextPathIndex = new int?();
      }

      protected virtual void ReportSuccess(ushort? nextIndex)
      {
        ((ScanComponent<INScanWarehousePath>) this).Basis.ReportInfo("Index of next path set to {0}.", new object[1]
        {
          (object) nextIndex
        });
      }

      protected virtual void SetNextState()
      {
        ((ScanComponent<INScanWarehousePath>) this).Basis.SetScanState<INScanWarehousePath.ScanPathMode.LocationState>((string) null, Array.Empty<object>());
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        INScanWarehousePath.ScanPathMode.SetNextIndexState.value>
      {
        public value()
          : base("NIDX")
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg : 
        WarehouseManagementSystem<INScanWarehousePath, INScanWarehousePath.Host>.Msg
      {
        public const string Prompt = "Enter the new index of the next path.";
        public const string Ready = "Index of next path set to {0}.";
        public const string BadFormat = "The quantity format does not fit the locale settings.";
      }
    }

    public sealed class SetNextIndexCommand : 
      BarcodeDrivenStateMachine<INScanWarehousePath, INScanWarehousePath.Host>.ScanCommand
    {
      public const string Value = "NEXT";

      public virtual string Code => "NEXT";

      public virtual string ButtonName => "ScanNextPathIndex";

      public virtual string DisplayName => "Set Next Path Index";

      protected virtual bool IsEnabled
      {
        get
        {
          return !(((ScanComponent<INScanWarehousePath>) this).Basis.CurrentState is INScanWarehousePath.ScanPathMode.SetNextIndexState);
        }
      }

      protected virtual bool Process()
      {
        if (!((ScanCommand<INScanWarehousePath>) this).IsEnabled)
          return false;
        ((ScanComponent<INScanWarehousePath>) this).Basis.SetScanState<INScanWarehousePath.ScanPathMode.SetNextIndexState>((string) null, Array.Empty<object>());
        return true;
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        INScanWarehousePath.ScanPathMode.SetNextIndexCommand.value>
      {
        public value()
          : base("NEXT")
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Set Next Path Index";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<INScanWarehousePath>.Msg
    {
      public const string Description = "Scan Path";
    }
  }
}
