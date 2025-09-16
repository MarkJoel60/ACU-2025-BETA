// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.StoragePlaceLookup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public class StoragePlaceLookup : 
  BarcodeDrivenStateMachine<
  #nullable disable
  StoragePlaceLookup, StoragePlaceLookup.Host>
{
  public PXAction<ScanHeader> ReviewAvailability;
  public PXSetupOptional<INScanSetup, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>> Setup;

  public StorageLookupScanHeader StorageHeader
  {
    get => ScanHeaderExt.Get<StorageLookupScanHeader>(this.Header);
  }

  public ValueSetter<ScanHeader>.Ext<StorageLookupScanHeader> StorageSetter
  {
    get => this.HeaderSetter.With<StorageLookupScanHeader>();
  }

  public int? SiteID
  {
    get => this.StorageHeader.SiteID;
    set
    {
      ValueSetter<ScanHeader>.Ext<StorageLookupScanHeader> storageSetter = this.StorageSetter;
      (^ref storageSetter).Set<int?>((Expression<Func<StorageLookupScanHeader, int?>>) (h => h.SiteID), value);
    }
  }

  public int? StorageID
  {
    get => this.StorageHeader.StorageID;
    set
    {
      ValueSetter<ScanHeader>.Ext<StorageLookupScanHeader> storageSetter = this.StorageSetter;
      (^ref storageSetter).Set<int?>((Expression<Func<StorageLookupScanHeader, int?>>) (h => h.StorageID), value);
    }
  }

  public bool? IsCart
  {
    get => this.StorageHeader.IsCart;
    set
    {
      ValueSetter<ScanHeader>.Ext<StorageLookupScanHeader> storageSetter = this.StorageSetter;
      (^ref storageSetter).Set<bool?>((Expression<Func<StorageLookupScanHeader, bool?>>) (h => h.IsCart), value);
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Review")]
  protected virtual IEnumerable reviewAvailability(PXAdapter adapter) => adapter.Get();

  protected virtual void _(
    Events.RowUpdated<StoragePlaceEnq.StoragePlaceFilter> e)
  {
    ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<StoragePlaceEnq.StoragePlaceFilter>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(
    Events.RowInserted<StoragePlaceEnq.StoragePlaceFilter> e)
  {
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<StoragePlaceEnq.StoragePlaceFilter>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    ((PXAction) this.ReviewAvailability).SetVisible(((PXGraph) ((PXGraphExtension<StoragePlaceLookup.Host>) this).Base).IsMobile);
  }

  protected virtual IEnumerable<ScanMode<StoragePlaceLookup>> CreateScanModes()
  {
    return (IEnumerable<ScanMode<StoragePlaceLookup>>) new ScanMode<StoragePlaceLookup>[1]
    {
      (ScanMode<StoragePlaceLookup>) (((MethodInterceptor<ScanMode<StoragePlaceLookup>, StoragePlaceLookup>.OfAction<bool>) ((ScanMode<StoragePlaceLookup>) ((MethodInterceptor<ScanMode<StoragePlaceLookup>, StoragePlaceLookup>.OfFunc<IEnumerable<ScanRedirect<StoragePlaceLookup>>>) ((ScanMode<StoragePlaceLookup>) ((MethodInterceptor<ScanMode<StoragePlaceLookup>, StoragePlaceLookup>.OfFunc<IEnumerable<ScanCommand<StoragePlaceLookup>>>) ((ScanMode<StoragePlaceLookup>) ((MethodInterceptor<ScanMode<StoragePlaceLookup>, StoragePlaceLookup>.OfFunc<IEnumerable<ScanState<StoragePlaceLookup>>>) ((ScanMode<StoragePlaceLookup>) new ScanMode<StoragePlaceLookup>.Simple("STOR", "Storage Lookup")).Intercept.CreateStates).ByReplace((Func<IEnumerable<ScanState<StoragePlaceLookup>>>) (() => (IEnumerable<ScanState<StoragePlaceLookup>>) new ScanState<StoragePlaceLookup>[2]
      {
        (ScanState<StoragePlaceLookup>) new StoragePlaceLookup.WarehouseState(),
        (ScanState<StoragePlaceLookup>) new StoragePlaceLookup.StorageState()
      }), new RelativeInject?())).Intercept.CreateCommands).ByReplace((Func<IEnumerable<ScanCommand<StoragePlaceLookup>>>) (() => (IEnumerable<ScanCommand<StoragePlaceLookup>>) new ScanCommand<StoragePlaceLookup>[1]
      {
        (ScanCommand<StoragePlaceLookup>) new StoragePlaceLookup.SwitchLotSerialViewCommand()
      }), new RelativeInject?())).Intercept.CreateRedirects).ByReplace((Func<IEnumerable<ScanRedirect<StoragePlaceLookup>>>) (() => AllWMSRedirects.CreateFor<StoragePlaceLookup>()), new RelativeInject?())).Intercept.ResetMode).ByReplace((Action<StoragePlaceLookup, bool>) ((basis, fullReset) =>
      {
        basis.Clear<StoragePlaceLookup.WarehouseState>(fullReset);
        basis.Clear<StoragePlaceLookup.StorageState>(true);
      }), new RelativeInject?()) ? 1 : 0)
    };
  }

  public class Host : StoragePlaceEnq, ICaptionable
  {
    public StoragePlaceLookup WMS => ((PXGraph) this).FindImplementation<StoragePlaceLookup>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public sealed class WarehouseState : PX.Objects.IN.WarehouseState<StoragePlaceLookup>
  {
    protected override int? SiteID
    {
      get => ((ScanComponent<StoragePlaceLookup>) this).Basis.SiteID;
      set => ((ScanComponent<StoragePlaceLookup>) this).Basis.SiteID = value;
    }

    protected virtual Validation Validate(INSite site)
    {
      string str;
      return !((ScanComponent<StoragePlaceLookup>) this).Basis.IsValid<StorageLookupScanHeader.siteID>((object) site.SiteID, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }

    protected virtual void SetNextState()
    {
      ((ScanComponent<StoragePlaceLookup>) this).Basis.SetScanState<StoragePlaceLookup.StorageState>((string) null, Array.Empty<object>());
    }

    protected override bool UseDefaultWarehouse
    {
      get
      {
        return ((PXSelectBase<INScanSetup>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Setup).Current.DefaultWarehouse.GetValueOrDefault();
      }
    }
  }

  public sealed class StorageState : 
    BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>.EntityState<StoragePlace>
  {
    public const string Value = "STOR";

    public virtual string Code => "STOR";

    protected virtual string StatePrompt => StoragePlaceLookup.StorageState.Msg.Prompt;

    protected virtual StoragePlace GetByBarcode(string barcode)
    {
      return PXResultset<StoragePlace>.op_Implicit(PXSelectBase<StoragePlace, PXViewOf<StoragePlace>.BasedOn<SelectFromBase<StoragePlace, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<StoragePlace.siteID, Equal<BqlField<StorageLookupScanHeader.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<StoragePlace.storageCD, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>.op_Implicit((BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>) ((ScanComponent<StoragePlaceLookup>) this).Basis), new object[1]
      {
        (object) barcode
      }));
    }

    protected virtual AbsenceHandling.Of<StoragePlace> HandleAbsence(string barcode)
    {
      return ((ScanComponent<StoragePlaceLookup>) this).Basis.TryProcessBy<StoragePlaceLookup.WarehouseState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<StoragePlace>.op_Implicit(AbsenceHandling.Done) : ((EntityState<StoragePlaceLookup, StoragePlace>) this).HandleAbsence(barcode);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<StoragePlaceLookup>) this).Basis.Reporter.Error("{0} storage not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual Validation Validate(StoragePlace storage)
    {
      if (storage.Active.GetValueOrDefault())
        return Validation.Ok;
      return Validation.Fail("Location '{0}' is inactive", new object[1]
      {
        (object) storage.StorageCD
      });
    }

    protected virtual void Apply(StoragePlace storage)
    {
      ((ScanComponent<StoragePlaceLookup>) this).Basis.StorageID = storage.StorageID;
      ((ScanComponent<StoragePlaceLookup>) this).Basis.IsCart = storage.IsCart;
      ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Insert();
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.siteID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) storage.SiteID);
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.storageType>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, storage.IsCart.GetValueOrDefault() ? (object) "C" : (object) "L");
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.locationID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) (storage.IsCart.GetValueOrDefault() ? new int?() : storage.StorageID));
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.cartID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) (storage.IsCart.GetValueOrDefault() ? storage.StorageID : new int?()));
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.IsDirty = false;
      ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).UpdateCurrent();
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<StoragePlaceLookup>) this).Basis.StorageID = new int?();
      ((ScanComponent<StoragePlaceLookup>) this).Basis.IsCart = new bool?(false);
      if (((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current == null)
        return;
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.siteID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) null);
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.locationID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) null);
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.cartID>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) null);
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.IsDirty = false;
      ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).UpdateCurrent();
    }

    protected virtual void ReportSuccess(StoragePlace storage)
    {
      ((ScanComponent<StoragePlaceLookup>) this).Basis.Reporter.Info("{0} storage selected.", new object[1]
      {
        (object) storage.StorageCD
      });
    }

    protected virtual void SetNextState()
    {
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    StoragePlaceLookup.StorageState.value>
    {
      public value()
        : base("STOR")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string PromptNoCart = "Scan the location.";
      public const string PromptWithCart = "Scan the location or cart.";
      public const string Ready = "{0} storage selected.";
      public const string Missing = "{0} storage not found.";

      public static string Prompt
      {
        get
        {
          return !PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>() ? "Scan the location." : "Scan the location or cart.";
        }
      }
    }
  }

  public sealed class SwitchLotSerialViewCommand : 
    BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>.ScanCommand
  {
    public virtual string Code => "SWITCH*LS";

    public virtual string ButtonName => "scanSwitchLotSerialView";

    public virtual string DisplayName => "Switch Lot/Serial View";

    protected virtual bool IsEnabled => true;

    protected virtual bool Process()
    {
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<StoragePlaceEnq.StoragePlaceFilter.expandByLotSerialNbr>((object) ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current, (object) !((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Current.ExpandByLotSerialNbr.GetValueOrDefault());
      ((PXSelectBase) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).Cache.IsDirty = false;
      ((PXSelectBase<StoragePlaceEnq.StoragePlaceFilter>) ((ScanComponent<StoragePlaceLookup>) this).Basis.Graph.Filter).UpdateCurrent();
      return true;
    }

    [PXLocalizable]
    public class Msg
    {
      public const string DisplayName = "Switch Lot/Serial View";
    }
  }

  public sealed class RedirectFrom<TForeignBasis> : 
    BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "STORAGE";

    public virtual string DisplayName => "Storage Lookup";

    public virtual bool IsPossible => PXAccess.FeatureInstalled<FeaturesSet.wMSInventory>();
  }

  [PXLocalizable]
  public abstract class Msg : 
    BarcodeDrivenStateMachine<StoragePlaceLookup, StoragePlaceLookup.Host>.Msg
  {
    public const string ModeDescription = "Storage Lookup";
  }
}
