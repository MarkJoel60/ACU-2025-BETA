// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.WarehouseManagementSystem`2
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
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public abstract class WarehouseManagementSystem<TSelf, TGraph> : 
  BarcodeDrivenStateMachine<
  #nullable disable
  TSelf, TGraph>
  where TSelf : WarehouseManagementSystem<TSelf, TGraph>
  where TGraph : PXGraph, new()
{
  public PXAction<ScanHeader> Review;

  public static bool IsActiveBase() => PXAccess.FeatureInstalled<FeaturesSet.advancedFulfillment>();

  protected WarehouseManagementSystem<TSelf, TGraph>.QtySupport QtyExt
  {
    get => this.Graph.FindImplementation<WarehouseManagementSystem<TSelf, TGraph>.QtySupport>();
  }

  protected WarehouseManagementSystem<TSelf, TGraph>.GS1Support GS1Ext
  {
    get => this.Graph.FindImplementation<WarehouseManagementSystem<TSelf, TGraph>.GS1Support>();
  }

  public WMSScanHeader WMSHeader
  {
    get => ScanHeaderExt.Get<WMSScanHeader>(this.Header) ?? new WMSScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<WMSScanHeader> WMSSetter
  {
    get => this.HeaderSetter.With<WMSScanHeader>();
  }

  public string RefNbr
  {
    get => this.WMSHeader.RefNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<string>((Expression<Func<WMSScanHeader, string>>) (h => h.RefNbr), value);
    }
  }

  public int? SiteID
  {
    get => this.WMSHeader.SiteID;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<int?>((Expression<Func<WMSScanHeader, int?>>) (h => h.SiteID), value);
    }
  }

  public int? LocationID
  {
    get => this.WMSHeader.LocationID;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<int?>((Expression<Func<WMSScanHeader, int?>>) (h => h.LocationID), value);
    }
  }

  public int? InventoryID
  {
    get => this.WMSHeader.InventoryID;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<int?>((Expression<Func<WMSScanHeader, int?>>) (h => h.InventoryID), value);
    }
  }

  public int? SubItemID
  {
    get => this.WMSHeader.SubItemID;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<int?>((Expression<Func<WMSScanHeader, int?>>) (h => h.SubItemID), value);
    }
  }

  public string UOM
  {
    get => this.WMSHeader.UOM;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<string>((Expression<Func<WMSScanHeader, string>>) (h => h.UOM), value);
    }
  }

  public Decimal? Qty
  {
    get => this.WMSHeader.Qty;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<Decimal?>((Expression<Func<WMSScanHeader, Decimal?>>) (h => h.Qty), value);
    }
  }

  public Decimal? BaseQty => this.WMSHeader.BaseQty;

  public string LotSerialNbr
  {
    get => this.WMSHeader.LotSerialNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<string>((Expression<Func<WMSScanHeader, string>>) (h => h.LotSerialNbr), value);
    }
  }

  public DateTime? ExpireDate
  {
    get => this.WMSHeader.ExpireDate;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<DateTime?>((Expression<Func<WMSScanHeader, DateTime?>>) (h => h.ExpireDate), value);
    }
  }

  public bool? Remove
  {
    get => this.WMSHeader.Remove;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<bool?>((Expression<Func<WMSScanHeader, bool?>>) (h => h.Remove), value);
    }
  }

  public DateTime? TranDate
  {
    get => this.WMSHeader.TranDate;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<DateTime?>((Expression<Func<WMSScanHeader, DateTime?>>) (h => h.TranDate), value);
    }
  }

  public string TranType
  {
    get => this.WMSHeader.TranType;
    set
    {
      ValueSetter<ScanHeader>.Ext<WMSScanHeader> wmsSetter = this.WMSSetter;
      (^ref wmsSetter).Set<string>((Expression<Func<WMSScanHeader, string>>) (h => h.TranType), value);
    }
  }

  public INSite SelectedSite => INSite.PK.Find((PXGraph) this.Graph, this.SiteID);

  public INLocation SelectedLocation => INLocation.PK.Find((PXGraph) this.Graph, this.LocationID);

  public InventoryItem SelectedInventoryItem
  {
    get => InventoryItem.PK.Find((PXGraph) this.Graph, this.InventoryID);
  }

  public INLotSerClass SelectedLotSerialClass
  {
    get => this.GetLotSerialClassOf(this.SelectedInventoryItem);
  }

  public LSConfig LotSerialTrack
  {
    get
    {
      return new LSConfig(this.SelectedLotSerialClass, this.TranType, ScanHeaderExt.Get<WMSScanHeader>(this.Header).InventoryMultiplicator);
    }
  }

  protected abstract bool UseQtyCorrection { get; }

  protected virtual bool CanOverrideQty
  {
    get => this.DocumentIsEditable && !this.LotSerialTrack.IsTrackedSerial;
  }

  public virtual bool DocumentLoaded => this.RefNbr != null;

  public virtual bool DocumentIsEditable => this.DocumentLoaded;

  protected virtual string DocumentIsNotEditableMessage
  {
    get => "The document has become unavailable for editing. Contact your manager.";
  }

  [PXButton]
  [PXUIField(DisplayName = "Review")]
  protected virtual IEnumerable review(PXAdapter adapter) => adapter.Get();

  protected virtual void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    ((PXAction) this.Review).SetVisible(((PXGraphExtension<TGraph>) this).Base.IsMobile);
  }

  public INLotSerClass GetLotSerialClassOf(InventoryItem inventoryItem)
  {
    return inventoryItem.With<InventoryItem, INLotSerClass>((Func<InventoryItem, INLotSerClass>) (ii => !ii.StkItem.GetValueOrDefault() ? this.DefaultLotSerialClass : INLotSerClass.PK.Find((PXGraph) this.Graph, ii.LotSerClassID)));
  }

  [Obsolete("Use the LotSerialTrack.IsTracked property instead.")]
  public bool ItemHasLotSerial => this.LotSerialTrack.IsTracked;

  [Obsolete("Use the LotSerialTrack.HasExpiration property instead.")]
  public bool ItemHasExpireDate => this.LotSerialTrack.HasExpiration;

  [Obsolete("Use the LotSerialTrack.IsEnterable property instead.")]
  public virtual bool IsEnterableLotSerial(bool isForIssue, bool isForTransfer)
  {
    if (!isForTransfer)
      return this.IsEnterableLotSerial(isForIssue);
    return isForIssue && this.SelectedLotSerialClass.With<INLotSerClass, bool>((Func<INLotSerClass, bool>) (it => it.LotSerIssueMethod == "U"));
  }

  [Obsolete("Use the LotSerialTrack.IsEnterable property instead.")]
  public virtual bool IsEnterableLotSerial(bool isForIssue)
  {
    return !isForIssue ? this.SelectedLotSerialClass.With<INLotSerClass, bool>((Func<INLotSerClass, bool>) (it => it.LotSerAssign == "R")) : this.SelectedLotSerialClass.With<INLotSerClass, bool>((Func<INLotSerClass, bool>) (it => it.LotSerAssign == "U" || it.LotSerIssueMethod == "U"));
  }

  protected virtual int? DefaultSiteID => UserPreferenceExt.GetDefaultSite((PXGraph) this.Graph);

  protected virtual INLotSerClass DefaultLotSerialClass
  {
    get
    {
      return new INLotSerClass()
      {
        LotSerTrack = "N",
        LotSerAssign = "R",
        LotSerTrackExpiration = new bool?(false),
        AutoNextNbr = new bool?(true)
      };
    }
  }

  public DateTime? EnsureExpireDateDefault()
  {
    return LSSelect.ExpireDateByLot((PXGraph) this.Graph, this.GetLSMaster(), (ILSMaster) null);
  }

  public ILSMaster GetLSMaster()
  {
    return (ILSMaster) new LSMasterDummy()
    {
      SiteID = this.SiteID,
      LocationID = this.LocationID,
      InventoryID = this.InventoryID,
      SubItemID = this.SubItemID,
      LotSerialNbr = this.LotSerialNbr,
      ExpireDate = this.ExpireDate,
      UOM = this.UOM,
      Qty = this.Qty,
      TranDate = this.TranDate,
      TranType = this.TranType,
      InvtMult = ScanHeaderExt.Get<WMSScanHeader>(this.Header).InventoryMultiplicator
    };
  }

  protected virtual ScanMode<TSelf> LateDecorateScanMode(ScanMode<TSelf> original)
  {
    ScanMode<TSelf> mode = base.LateDecorateScanMode(original);
    WarehouseManagementSystem<TSelf, TGraph>.RemoveCommand.InterceptResetMode(mode);
    return mode;
  }

  protected virtual bool CanHandleScan(string barcode)
  {
    if (barcode.StartsWith("@") || barcode.StartsWith("*") || !EnumerableExtensions.IsNotIn<string>(this.Header.ScanState, "RNBR", "NONE") || !this.DocumentLoaded || this.DocumentIsEditable)
      return true;
    this.Graph.Clear();
    this.Graph.SelectTimeStamp();
    this.ReportError(this.DocumentIsNotEditableMessage, Array.Empty<object>());
    return false;
  }

  public abstract class QtySupport : BarcodeQtySupport<TSelf, TGraph>
  {
    public virtual bool UseQtyCorrection => this.Basis.UseQtyCorrection;

    public virtual bool CanOverrideQty => base.CanOverrideQty && this.Basis.CanOverrideQty;

    public virtual bool IsMandatoryQtyInput
    {
      get
      {
        if (!(((PXSelectBase<ScanHeader>) this.Basis.HeaderView).Current.PrevScanState != "QTY"))
          return false;
        InventoryItem selectedInventoryItem = this.Basis.SelectedInventoryItem;
        return selectedInventoryItem != null && selectedInventoryItem.WeightItem.GetValueOrDefault();
      }
    }
  }

  public abstract class GS1Support : GS1BarcodeSupport<TSelf, TGraph>
  {
    protected virtual IEnumerable<BarcodeComponentApplicationStep> GetBarcodeComponentApplicationSteps()
    {
      return (IEnumerable<BarcodeComponentApplicationStep>) new BarcodeComponentApplicationStep[6]
      {
        new BarcodeComponentApplicationStep("ITEM", Codes.GTIN.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true),
        new BarcodeComponentApplicationStep("ITEM", Codes.Content.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true),
        new BarcodeComponentApplicationStep("LTSR", Codes.BatchLot.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true),
        new BarcodeComponentApplicationStep("LTSR", Codes.Serial.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).String), true),
        new BarcodeComponentApplicationStep("EXPD", Codes.BestBeforeDate.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).Date.Value.ToString()), true),
        new BarcodeComponentApplicationStep("EXPD", Codes.ExpireDate.Code, (Func<ParseResult, string>) (data => ((ParseResult) ref data).Date.Value.ToString()), true)
      };
    }
  }

  public abstract class RefNbrState<TDocument> : 
    BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<TDocument>
  {
    public const string Value = "RNBR";

    public virtual string Code => "RNBR";

    protected virtual bool IsStateSkippable()
    {
      return ((ScanComponent<TSelf>) this).Basis.RefNbr != null && !((ScanComponent<TSelf>) this).Basis.Header.ProcessingSucceeded.GetValueOrDefault();
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      WarehouseManagementSystem<TSelf, TGraph>.RefNbrState<TDocument>.value>
    {
      public value()
        : base("RNBR")
      {
      }
    }
  }

  public abstract class WarehouseState : PX.Objects.IN.WarehouseState<TSelf>
  {
    protected sealed override int? SiteID
    {
      get => ((ScanComponent<TSelf>) this).Basis.SiteID;
      set => ((ScanComponent<TSelf>) this).Basis.SiteID = value;
    }

    protected virtual Validation Validate(INSite site)
    {
      string str;
      return !((ScanComponent<TSelf>) this).Basis.IsValid<WMSScanHeader.siteID>((object) site.SiteID, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }
  }

  public class LocationState : BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<INLocation>
  {
    public const string Value = "LOCN";

    public virtual string Code => "LOCN";

    protected virtual string StatePrompt => "Scan the location.";

    protected virtual bool IsStateActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>();
    }

    protected virtual INLocation GetByBarcode(string barcode)
    {
      return PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<P.AsInt>>>>>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<TSelf, TGraph>.op_Implicit((BarcodeDrivenStateMachine<TSelf, TGraph>) ((ScanComponent<TSelf>) this).Basis), new object[2]
      {
        (object) ((ScanComponent<TSelf>) this).Basis.SiteID,
        (object) barcode
      }));
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Error("{0} location not found in {1} warehouse.", new object[2]
      {
        (object) barcode,
        (object) ((ScanComponent<TSelf>) this).Basis.SelectedSite.SiteCD
      });
    }

    protected virtual Validation Validate(INLocation location)
    {
      if (location.Active.GetValueOrDefault())
        return Validation.Ok;
      return Validation.Fail("Location '{0}' is inactive", new object[1]
      {
        (object) location.LocationCD
      });
    }

    protected virtual void Apply(INLocation location)
    {
      ((ScanComponent<TSelf>) this).Basis.LocationID = location.LocationID;
    }

    protected virtual void ReportSuccess(INLocation location)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("{0} location selected.", new object[1]
      {
        (object) location.LocationCD
      });
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<TSelf>) this).Basis.LocationID = new int?();
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      WarehouseManagementSystem<TSelf, TGraph>.LocationState.value>
    {
      public value()
        : base("LOCN")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the location.";
      public const string Ready = "{0} location selected.";
      public const string Missing = "{0} location not found in {1} warehouse.";
      public const string NotSet = "Location not selected.";
    }
  }

  public class InventoryItemState : 
    BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<PXResult<INItemXRef, InventoryItem>>
  {
    public const string Value = "ITEM";

    public InventoryItemState()
    {
      ((MethodInterceptor<EntityState<TSelf, PXResult<INItemXRef, InventoryItem>>, TSelf>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((EntityState<TSelf, PXResult<INItemXRef, InventoryItem>>) this).Intercept.HandleAbsence).ByOverride(new Func<string, Func<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>(this.TryHandleByLotSerialNbr), new RelativeInject?((RelativeInject) 1));
    }

    public bool IsForIssue { get; set; }

    public bool IsForTransfer { get; set; }

    public bool SuppressModuleItemStatusCheck { get; set; }

    public INPrimaryAlternateType? AlternateType { get; set; }

    public string DefaultUOM(InventoryItem inventoryItem)
    {
      if (((ScanComponent<TSelf>) this).Basis.GetLotSerialClassOf(inventoryItem)?.LotSerTrack == "S")
        return inventoryItem.BaseUnit;
      if (this.AlternateType.GetValueOrDefault() == INPrimaryAlternateType.CPN)
        return inventoryItem.SalesUnit;
      INPrimaryAlternateType? alternateType = this.AlternateType;
      INPrimaryAlternateType primaryAlternateType = INPrimaryAlternateType.VPN;
      return !(alternateType.GetValueOrDefault() == primaryAlternateType & alternateType.HasValue) ? inventoryItem.BaseUnit : inventoryItem.PurchaseUnit;
    }

    public virtual string Code => "ITEM";

    protected virtual string StatePrompt
    {
      get
      {
        if (!((ScanComponent<TSelf>) this).Basis.InventoryID.HasValue || !((ScanComponent<TSelf>) this).Basis.HasActive<WarehouseManagementSystem<TSelf, TGraph>.LotSerialState>())
          return "Scan the item.";
        return ((ScanComponent<TSelf>) this).Basis.Localize("Scan another item or the next lot or serial number of the {0} item.", new object[1]
        {
          (object) ((ScanComponent<TSelf>) this).Basis.SightOf<WMSScanHeader.inventoryID>()
        });
      }
    }

    protected virtual PXResult<INItemXRef, InventoryItem> GetByBarcode(string barcode)
    {
      return this.ReadItemByBarcode(barcode, this.AlternateType);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Error("{0} item not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual Validation Validate(PXResult<INItemXRef, InventoryItem> entity)
    {
      INItemXRef inItemXref1;
      InventoryItem inventoryItem1;
      entity.Deconstruct(ref inItemXref1, ref inventoryItem1);
      INItemXRef inItemXref2 = inItemXref1;
      InventoryItem inventoryItem2 = inventoryItem1;
      string str = inItemXref2.UOM ?? this.DefaultUOM(inventoryItem2);
      INLotSerClass lotSerialClassOf = ((ScanComponent<TSelf>) this).Basis.GetLotSerialClassOf(inventoryItem2);
      if (lotSerialClassOf.LotSerTrack == "S" && !this.IsForTransfer && (this.IsForIssue ? (lotSerialClassOf.LotSerAssign == "U" ? 1 : 0) : (lotSerialClassOf.LotSerAssign == "R" ? 1 : 0)) != 0 && str != inventoryItem2.BaseUnit)
        return Validation.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
      if (!this.SuppressModuleItemStatusCheck)
      {
        INPrimaryAlternateType? alternateType = this.AlternateType;
        if (alternateType.GetValueOrDefault() == INPrimaryAlternateType.CPN && inventoryItem2.ItemStatus == "NS")
          return Validation.Fail("The {0} item cannot be scanned because it has the {1} status.", new object[2]
          {
            (object) inventoryItem2.InventoryCD,
            (object) ((ScanComponent<TSelf>) this).Basis.SightOf<InventoryItem.itemStatus>((IBqlTable) inventoryItem2)
          });
        alternateType = this.AlternateType;
        INPrimaryAlternateType primaryAlternateType = INPrimaryAlternateType.VPN;
        if (alternateType.GetValueOrDefault() == primaryAlternateType & alternateType.HasValue && inventoryItem2.ItemStatus == "NP")
          return Validation.Fail("The {0} item cannot be scanned because it has the {1} status.", new object[2]
          {
            (object) inventoryItem2.InventoryCD,
            (object) ((ScanComponent<TSelf>) this).Basis.SightOf<InventoryItem.itemStatus>((IBqlTable) inventoryItem2)
          });
      }
      if (!EnumerableExtensions.IsIn<string>(inventoryItem2.ItemStatus, "IN", "DE"))
        return Validation.Ok;
      return Validation.Fail("The {0} item cannot be scanned because it has the {1} status.", new object[2]
      {
        (object) inventoryItem2.InventoryCD,
        (object) ((ScanComponent<TSelf>) this).Basis.SightOf<InventoryItem.itemStatus>((IBqlTable) inventoryItem2)
      });
    }

    protected virtual void Apply(PXResult<INItemXRef, InventoryItem> entity)
    {
      INItemXRef inItemXref1;
      InventoryItem inventoryItem1;
      entity.Deconstruct(ref inItemXref1, ref inventoryItem1);
      INItemXRef inItemXref2 = inItemXref1;
      InventoryItem inventoryItem2 = inventoryItem1;
      ((ScanComponent<TSelf>) this).Basis.InventoryID = inItemXref2.InventoryID;
      ((ScanComponent<TSelf>) this).Basis.SubItemID = inItemXref2.SubItemID;
      WarehouseManagementSystem<TSelf, TGraph>.QtySupport qtySupport = ((ScanComponent<TSelf>) this).Basis.Get<WarehouseManagementSystem<TSelf, TGraph>.QtySupport>();
      if ((qtySupport != null ? (!qtySupport.IsUOMSetAutomatically.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      ((ScanComponent<TSelf>) this).Basis.UOM = inItemXref2.UOM ?? this.DefaultUOM(inventoryItem2);
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<TSelf>) this).Basis.InventoryID = new int?();
      ((ScanComponent<TSelf>) this).Basis.SubItemID = new int?();
      ((ScanComponent<TSelf>) this).Basis.UOM = (string) null;
    }

    protected virtual void ReportSuccess(PXResult<INItemXRef, InventoryItem> entity)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("{0} item selected.", new object[1]
      {
        (object) ((PXResult) entity).GetItem<InventoryItem>().InventoryCD.Trim()
      });
    }

    protected PXResult<INItemXRef, InventoryItem> ReadItemByBarcode(
      string barcode,
      INPrimaryAlternateType? additionalAlternateType = null)
    {
      PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INItemXRef.FK.InventoryItem>>>.Where<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>.Order<By<BqlField<INItemXRef.alternateType, IBqlString>.Asc>>>.ReadOnly readOnly = new PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<INItemXRef.FK.InventoryItem>>>.Where<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>.Order<By<BqlField<INItemXRef.alternateType, IBqlString>.Asc>>>.ReadOnly(BarcodeDrivenStateMachine<TSelf, TGraph>.op_Implicit((BarcodeDrivenStateMachine<TSelf, TGraph>) ((ScanComponent<TSelf>) this).Basis));
      if (additionalAlternateType.GetValueOrDefault() == INPrimaryAlternateType.CPN)
      {
        ((PXSelectBase<INItemXRef>) readOnly).WhereAnd<Where<BqlOperand<INItemXRef.alternateType, IBqlString>.IsIn<INAlternateType.barcode, INAlternateType.gIN, INAlternateType.cPN>>>();
      }
      else
      {
        INPrimaryAlternateType? nullable = additionalAlternateType;
        INPrimaryAlternateType primaryAlternateType = INPrimaryAlternateType.VPN;
        if (nullable.GetValueOrDefault() == primaryAlternateType & nullable.HasValue)
          ((PXSelectBase<INItemXRef>) readOnly).WhereAnd<Where<BqlOperand<INItemXRef.alternateType, IBqlString>.IsIn<INAlternateType.barcode, INAlternateType.gIN, INAlternateType.vPN>>>();
        else
          ((PXSelectBase<INItemXRef>) readOnly).WhereAnd<Where<BqlOperand<INItemXRef.alternateType, IBqlString>.IsIn<INAlternateType.barcode, INAlternateType.gIN>>>();
      }
      PXResult<INItemXRef, InventoryItem> pxResult = ((IEnumerable<PXResult<INItemXRef>>) ((PXSelectBase<INItemXRef>) readOnly).Select(new object[1]
      {
        (object) barcode
      })).AsEnumerable<PXResult<INItemXRef>>().OrderByDescending<PXResult<INItemXRef>, bool>((Func<PXResult<INItemXRef>, bool>) (r => EnumerableExtensions.IsIn<string>(((PXResult) r).GetItem<INItemXRef>().AlternateType, "BAR", "GIN"))).Cast<PXResult<INItemXRef, InventoryItem>>().FirstOrDefault<PXResult<INItemXRef, InventoryItem>>();
      if (pxResult == null || PXResult<INItemXRef, InventoryItem>.op_Implicit(pxResult) == null)
        pxResult = this.ReadItemById(barcode, additionalAlternateType);
      return pxResult;
    }

    private PXResult<INItemXRef, InventoryItem> ReadItemById(
      string barcode,
      INPrimaryAlternateType? additionalAlternateType = null)
    {
      InventoryItem inventoryItem = InventoryItem.UK.Find(BarcodeDrivenStateMachine<TSelf, TGraph>.op_Implicit((BarcodeDrivenStateMachine<TSelf, TGraph>) ((ScanComponent<TSelf>) this).Basis), barcode);
      if (inventoryItem == null)
        return (PXResult<INItemXRef, InventoryItem>) null;
      INItemXRef inItemXref = new INItemXRef()
      {
        InventoryID = inventoryItem.InventoryID,
        AlternateType = "BAR",
        AlternateID = barcode
      };
      object obj;
      ((PXCache) GraphHelper.Caches<INItemXRef>((PXGraph) ((ScanComponent<TSelf>) this).Basis.Graph)).RaiseFieldDefaulting<INItemXRef.subItemID>((object) inItemXref, ref obj);
      inItemXref.SubItemID = (int?) obj;
      return new PXResult<INItemXRef, InventoryItem>(inItemXref, inventoryItem);
    }

    private AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>> TryHandleByLotSerialNbr(
      string barcode,
      Func<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>> base_HandleAbsence)
    {
      AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>> of = base_HandleAbsence(barcode);
      return !of.IsHandled && ((ScanComponent<TSelf>) this).Basis.InventoryID.HasValue && ((ScanComponent<TSelf>) this).Basis.TryProcessBy<WarehouseManagementSystem<TSelf, TGraph>.LotSerialState>(barcode, (StateSubstitutionRule) 26) ? AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>.op_Implicit(AbsenceHandling.Done) : of;
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      WarehouseManagementSystem<TSelf, TGraph>.InventoryItemState.value>
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
      public const string PromptWithLotSerialNbr = "Scan another item or the next lot or serial number of the {0} item.";
      public const string Ready = "{0} item selected.";
      public const string Missing = "{0} item not found.";
      public const string NotSet = "Item not selected.";
      public const string SerialItemNotComplexQty = "Serialized items can be processed only with the base UOM and the 1.00 quantity.";
      public const string InvalidItemStatus = "The {0} item cannot be scanned because it has the {1} status.";
    }
  }

  public class LotSerialState : BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<string>
  {
    public const string Value = "LTSR";

    public virtual string Code => "LTSR";

    protected virtual string StatePrompt => "Scan the lot or serial number.";

    protected virtual bool IsStateActive()
    {
      return ((ScanComponent<TSelf>) this).Basis.LotSerialTrack.IsTracked;
    }

    protected virtual string GetByBarcode(string barcode) => barcode.Trim();

    protected virtual Validation Validate(string lotSerial)
    {
      string str;
      return !((ScanComponent<TSelf>) this).Basis.IsValid<WMSScanHeader.lotSerialNbr>((object) lotSerial, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }

    protected virtual void Apply(string lotSerial)
    {
      ((ScanComponent<TSelf>) this).Basis.LotSerialNbr = lotSerial;
    }

    protected virtual void ReportSuccess(string lotSerial)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("{0} lot or serial number selected.", new object[1]
      {
        (object) lotSerial
      });
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<TSelf>) this).Basis.LotSerialNbr = (string) null;
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      WarehouseManagementSystem<TSelf, TGraph>.LotSerialState.value>
    {
      public value()
        : base("LTSR")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the lot or serial number.";
      public const string Ready = "{0} lot or serial number selected.";
      public const string NotSet = "Lot or serial number not selected.";
    }
  }

  public class ExpireDateState : BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<DateTime?>
  {
    public const string Value = "EXPD";

    public bool IsForIssue { get; set; }

    public bool IsForTransfer { get; set; }

    public virtual string Code => "EXPD";

    protected virtual string StatePrompt => "Scan the expiration date of the lot or serial number.";

    protected virtual bool IsStateActive()
    {
      bool? remove = ((ScanComponent<TSelf>) this).Basis.Remove;
      bool flag = false;
      return remove.GetValueOrDefault() == flag & remove.HasValue && ((ScanComponent<TSelf>) this).Basis.LotSerialTrack.With<LSConfig, bool>((Func<LSConfig, bool>) (ls => ls.HasExpiration && ls.IsEnterable));
    }

    protected virtual DateTime? GetByBarcode(string barcode)
    {
      DateTime result;
      return !DateTime.TryParse(barcode.Trim(), out result) ? new DateTime?() : new DateTime?(result);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Error("The date format does not fit the locale settings.", Array.Empty<object>());
    }

    protected virtual Validation Validate(DateTime? expireDate)
    {
      string str;
      return !((ScanComponent<TSelf>) this).Basis.IsValid<WMSScanHeader.expireDate>((object) expireDate, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }

    protected virtual void Apply(DateTime? expireDate)
    {
      ((ScanComponent<TSelf>) this).Basis.ExpireDate = expireDate;
    }

    protected virtual void ReportSuccess(DateTime? expireDate)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("Expiration date set to {0:d}.", new object[1]
      {
        (object) expireDate
      });
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<TSelf>) this).Basis.ExpireDate = new DateTime?();
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      WarehouseManagementSystem<TSelf, TGraph>.ExpireDateState.value>
    {
      public value()
        : base("EXPD")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the expiration date of the lot or serial number.";
      public const string Ready = "Expiration date set to {0:d}.";
      public const string BadFormat = "The date format does not fit the locale settings.";
      public const string NotSet = "Expiration date not selected.";
    }
  }

  public class RemoveCommand : BarcodeDrivenStateMachine<TSelf, TGraph>.ScanCommand
  {
    public virtual string Code => "REMOVE";

    public virtual string ButtonName => "scanRemove";

    public virtual string DisplayName => "Remove";

    protected virtual bool IsEnabled
    {
      get
      {
        bool? remove = ((ScanComponent<TSelf>) this).Basis.Remove;
        bool flag = false;
        return remove.GetValueOrDefault() == flag & remove.HasValue && ((ScanComponent<TSelf>) this).Basis.DocumentLoaded && ((ScanComponent<TSelf>) this).Basis.DocumentIsEditable;
      }
    }

    protected virtual bool Process()
    {
      ((ScanComponent<TSelf>) this).Basis.Reset(false);
      ((ScanComponent<TSelf>) this).Basis.Remove = new bool?(true);
      ((ScanComponent<TSelf>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("Remove mode activated.", Array.Empty<object>());
      return true;
    }

    public static void InterceptResetMode(ScanMode<TSelf> mode)
    {
      if (!mode.Commands.OfType<WarehouseManagementSystem<TSelf, TGraph>.RemoveCommand>().Any<WarehouseManagementSystem<TSelf, TGraph>.RemoveCommand>())
        return;
      ((MethodInterceptor<ScanMode<TSelf>, TSelf>.OfAction<bool>) mode.Intercept.ResetMode).ByAppend((Action<TSelf, bool>) ((basis, fullReset) => basis.Remove = new bool?(false)), new RelativeInject?());
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Remove";
      public const string RemoveMode = "Remove mode activated.";
    }
  }

  [PXLocalizable]
  public abstract class Msg : BarcodeDrivenStateMachine<TSelf, TGraph>.Msg
  {
    public const string DocumentIsNotEditable = "The document has become unavailable for editing. Contact your manager.";
  }
}
