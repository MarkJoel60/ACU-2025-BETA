// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.InventoryItemLookup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Common.GS1;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public class InventoryItemLookup : 
  BarcodeDrivenStateMachine<
  #nullable disable
  InventoryItemLookup, InventoryItemLookup.Host>
{
  public PXAction<ScanHeader> ReviewAvailability;
  public PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ItemLookupScanHeader.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly InventoryItem;
  public 
  #nullable disable
  PXSetupOptional<INScanSetup, Where<BqlOperand<
  #nullable enable
  INScanSetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> Setup;

  protected 
  #nullable disable
  InventoryItemLookup.GS1Support GS1Ext
  {
    get => ((PXGraph) this.Graph).FindImplementation<InventoryItemLookup.GS1Support>();
  }

  public ItemLookupScanHeader ItemHeader => ScanHeaderExt.Get<ItemLookupScanHeader>(this.Header);

  public ValueSetter<ScanHeader>.Ext<ItemLookupScanHeader> ItemSetter
  {
    get => this.HeaderSetter.With<ItemLookupScanHeader>();
  }

  public int? SiteID
  {
    get => this.ItemHeader.SiteID;
    set
    {
      ValueSetter<ScanHeader>.Ext<ItemLookupScanHeader> itemSetter = this.ItemSetter;
      (^ref itemSetter).Set<int?>((Expression<Func<ItemLookupScanHeader, int?>>) (h => h.SiteID), value);
    }
  }

  public int? InventoryID
  {
    get => this.ItemHeader.InventoryID;
    set
    {
      ValueSetter<ScanHeader>.Ext<ItemLookupScanHeader> itemSetter = this.ItemSetter;
      (^ref itemSetter).Set<int?>((Expression<Func<ItemLookupScanHeader, int?>>) (h => h.InventoryID), value);
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Review")]
  protected virtual IEnumerable reviewAvailability(PXAdapter adapter) => adapter.Get();

  [BorrowedNote(typeof (PX.Objects.IN.InventoryItem), typeof (InventorySummaryEnq))]
  protected virtual void _(Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  protected virtual void _(Events.RowSelected<InventorySummaryEnqFilter> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<InventorySummaryEnqFilter>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(
    Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.expandByLotSerialNbr> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.expandByLotSerialNbr>, InventorySummaryEnqFilter, object>) e).NewValue = (object) PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>();
  }

  protected virtual void _(
    Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.siteID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.siteID>, InventorySummaryEnqFilter, object>) e).NewValue = (object) this.SiteID ?? ((Events.FieldDefaultingBase<Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.siteID>, InventorySummaryEnqFilter, object>) e).NewValue;
  }

  protected virtual void _(
    Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.inventoryID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.inventoryID>, InventorySummaryEnqFilter, object>) e).NewValue = (object) this.InventoryID ?? ((Events.FieldDefaultingBase<Events.FieldDefaulting<InventorySummaryEnqFilter, InventorySummaryEnqFilter.inventoryID>, InventorySummaryEnqFilter, object>) e).NewValue;
  }

  protected virtual void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    ((PXAction) this.ReviewAvailability).SetVisible(((PXGraph) ((PXGraphExtension<InventoryItemLookup.Host>) this).Base).IsMobile);
  }

  protected virtual IEnumerable<ScanMode<InventoryItemLookup>> CreateScanModes()
  {
    return (IEnumerable<ScanMode<InventoryItemLookup>>) new ScanMode<InventoryItemLookup>[1]
    {
      (ScanMode<InventoryItemLookup>) (((MethodInterceptor<ScanMode<InventoryItemLookup>, InventoryItemLookup>.OfAction<bool>) ((ScanMode<InventoryItemLookup>) ((MethodInterceptor<ScanMode<InventoryItemLookup>, InventoryItemLookup>.OfFunc<IEnumerable<ScanRedirect<InventoryItemLookup>>>) ((ScanMode<InventoryItemLookup>) ((MethodInterceptor<ScanMode<InventoryItemLookup>, InventoryItemLookup>.OfFunc<IEnumerable<ScanState<InventoryItemLookup>>>) ((ScanMode<InventoryItemLookup>) new ScanMode<InventoryItemLookup>.Simple("ITEM", "Item Lookup")).Intercept.CreateStates).ByReplace((Func<IEnumerable<ScanState<InventoryItemLookup>>>) (() => (IEnumerable<ScanState<InventoryItemLookup>>) new ScanState<InventoryItemLookup>[2]
      {
        (ScanState<InventoryItemLookup>) new InventoryItemLookup.WarehouseState(),
        (ScanState<InventoryItemLookup>) new InventoryItemLookup.InventoryItemState()
      }), new RelativeInject?())).Intercept.CreateRedirects).ByReplace((Func<IEnumerable<ScanRedirect<InventoryItemLookup>>>) (() => AllWMSRedirects.CreateFor<InventoryItemLookup>()), new RelativeInject?())).Intercept.ResetMode).ByReplace((Action<InventoryItemLookup, bool>) ((basis, fullReset) =>
      {
        basis.Clear<InventoryItemLookup.WarehouseState>(fullReset);
        basis.Clear<InventoryItemLookup.InventoryItemState>(true);
      }), new RelativeInject?()) ? 1 : 0)
    };
  }

  public class Host : InventorySummaryEnq, ICaptionable
  {
    public InventoryItemLookup WMS => ((PXGraph) this).FindImplementation<InventoryItemLookup>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public class GS1Support : GS1BarcodeSupport<InventoryItemLookup, InventoryItemLookup.Host>
  {
    protected virtual IEnumerable<BarcodeComponentApplicationStep> GetBarcodeComponentApplicationSteps()
    {
      return (IEnumerable<BarcodeComponentApplicationStep>) new BarcodeComponentApplicationStep[2]
      {
        new BarcodeComponentApplicationStep("ITEM", Codes.GTIN.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true),
        new BarcodeComponentApplicationStep("ITEM", Codes.Content.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true)
      };
    }
  }

  public sealed class WarehouseState : PX.Objects.IN.WarehouseState<InventoryItemLookup>
  {
    protected override int? SiteID
    {
      get => ((ScanComponent<InventoryItemLookup>) this).Basis.SiteID;
      set => ((ScanComponent<InventoryItemLookup>) this).Basis.SiteID = value;
    }

    private void SetFilteringSite(int? siteID)
    {
      ((PXSelectBase) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<InventorySummaryEnqFilter.siteID>((object) ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Current, (object) siteID);
      ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).UpdateCurrent();
    }

    protected virtual Validation Validate(INSite site)
    {
      string str;
      return !((ScanComponent<InventoryItemLookup>) this).Basis.IsValid<ItemLookupScanHeader.siteID>((object) site.SiteID, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }

    protected override void Apply(INSite site)
    {
      base.Apply(site);
      this.SetFilteringSite(site.SiteID);
    }

    protected override void ClearState()
    {
      base.ClearState();
      this.SetFilteringSite(new int?());
    }

    protected virtual void SetNextState()
    {
      ((ScanComponent<InventoryItemLookup>) this).Basis.SetScanState<InventoryItemLookup.InventoryItemState>((string) null, Array.Empty<object>());
    }

    protected override bool UseDefaultWarehouse
    {
      get
      {
        return ((PXSelectBase<INScanSetup>) ((ScanComponent<InventoryItemLookup>) this).Basis.Setup).Current.DefaultWarehouse.GetValueOrDefault();
      }
    }
  }

  public sealed class InventoryItemState : 
    BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>.EntityState<PX.Objects.IN.InventoryItem>
  {
    public const string Value = "ITEM";

    public virtual string Code => "ITEM";

    protected virtual string StatePrompt => "Scan the item.";

    protected virtual PX.Objects.IN.InventoryItem GetByBarcode(string barcode)
    {
      PX.Objects.IN.InventoryItem byBarcode1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemXRef>.On<INItemXRef.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemXRef.alternateID, Equal<P.AsString>>>>, And<BqlOperand<INItemXRef.alternateType, IBqlString>.IsIn<INAlternateType.barcode, INAlternateType.gIN>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>.Order<By<BqlField<INItemXRef.alternateType, IBqlString>.Asc>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>.op_Implicit((BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>) ((ScanComponent<InventoryItemLookup>) this).Basis), new object[1]
      {
        (object) barcode
      }));
      if (byBarcode1 == null)
      {
        PX.Objects.IN.InventoryItem byBarcode2 = PX.Objects.IN.InventoryItem.UK.Find(BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>.op_Implicit((BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>) ((ScanComponent<InventoryItemLookup>) this).Basis), barcode);
        if (byBarcode2 != null && EnumerableExtensions.IsNotIn<string>(byBarcode2.ItemStatus, "IN", "DE"))
          return byBarcode2;
      }
      return byBarcode1;
    }

    protected virtual AbsenceHandling.Of<PX.Objects.IN.InventoryItem> HandleAbsence(string barcode)
    {
      return ((ScanComponent<InventoryItemLookup>) this).Basis.TryProcessBy<InventoryItemLookup.WarehouseState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PX.Objects.IN.InventoryItem>.op_Implicit(AbsenceHandling.Done) : ((EntityState<InventoryItemLookup, PX.Objects.IN.InventoryItem>) this).HandleAbsence(barcode);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<InventoryItemLookup>) this).Basis.Reporter.Error("{0} item not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void Apply(PX.Objects.IN.InventoryItem entity)
    {
      ((ScanComponent<InventoryItemLookup>) this).Basis.InventoryID = entity.InventoryID;
      ((ScanComponent<InventoryItemLookup>) this).Basis.NoteID = entity.NoteID;
      ((PXSelectBase) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<InventorySummaryEnqFilter.inventoryID>((object) ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Current, (object) entity.InventoryID);
      ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).UpdateCurrent();
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<InventoryItemLookup>) this).Basis.InventoryID = new int?();
      ((ScanComponent<InventoryItemLookup>) this).Basis.NoteID = new Guid?();
      ((PXSelectBase) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Cache.SetValueExt<InventorySummaryEnqFilter.inventoryID>((object) ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).Current, (object) null);
      ((PXSelectBase<InventorySummaryEnqFilter>) ((ScanComponent<InventoryItemLookup>) this).Basis.Graph.Filter).UpdateCurrent();
    }

    protected virtual void ReportSuccess(PX.Objects.IN.InventoryItem entity)
    {
      ((ScanComponent<InventoryItemLookup>) this).Basis.Reporter.Info("{0} item selected.", new object[1]
      {
        (object) entity.InventoryCD.Trim()
      });
    }

    protected virtual void SetNextState()
    {
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      InventoryItemLookup.InventoryItemState.value>
    {
      public value()
        : base("ITEM")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the item.";
      public const string Ready = "{0} item selected.";
      public const string Missing = "{0} item not found.";
    }
  }

  public sealed class RedirectFrom<TForeignBasis> : 
    BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "ITEM";

    public virtual string DisplayName => "Item Lookup";

    public virtual bool IsPossible => PXAccess.FeatureInstalled<FeaturesSet.wMSInventory>();
  }

  [PXLocalizable]
  public abstract class Msg : 
    BarcodeDrivenStateMachine<InventoryItemLookup, InventoryItemLookup.Host>.Msg
  {
    public const string ModeDescription = "Item Lookup";
  }
}
