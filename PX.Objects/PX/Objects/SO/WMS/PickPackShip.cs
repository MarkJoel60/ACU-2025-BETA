// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PickPackShip
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using PX.Objects.SM;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.SO.WMS;

public class PickPackShip : WarehouseManagementSystem<
#nullable disable
PickPackShip, PickPackShip.Host>
{
  public PXSetupOptional<SOPickPackShipSetup, Where<BqlOperand<
  #nullable enable
  SOPickPackShipSetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> Setup;
  public 
  #nullable disable
  PXAction<ScanHeader> ViewOrder;

  public Decimal BaseQty
  {
    get
    {
      return INUnitAttribute.ConvertToBase(((PXSelectBase) this.Graph.Transactions).Cache, this.InventoryID, this.UOM, this.Qty.GetValueOrDefault(), INPrecision.NOROUND);
    }
  }

  public virtual bool ExplicitConfirmation
  {
    get
    {
      return ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ExplicitLineConfirmation.GetValueOrDefault();
    }
  }

  public override bool DocumentIsEditable => base.DocumentIsEditable && !this.DocumentIsConfirmed;

  public virtual bool DocumentIsConfirmed
  {
    get
    {
      PX.Objects.SO.SOShipment shipment = this.Shipment;
      return shipment != null && shipment.Confirmed.GetValueOrDefault();
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.UseDefaultQty.GetValueOrDefault();
    }
  }

  protected override bool CanOverrideQty
  {
    get
    {
      if (base.CanOverrideQty || this.DocumentIsEditable && this.DefaultLotSerial && this.LotSerialTrack.IsTrackedSerial)
        return true;
      return this.DocumentIsEditable && this.IsTransfer && this.LotSerialTrack.IsTrackedSerial && this.LotSerialTrack.IsEnterable;
    }
  }

  public virtual bool DefaultLocation
  {
    get
    {
      return PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(((PXGraphExtension<PickPackShip.Host>) this).Base).DefaultLocationFromShipment.GetValueOrDefault();
    }
  }

  public virtual bool DefaultLotSerial
  {
    get
    {
      return PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(((PXGraphExtension<PickPackShip.Host>) this).Base).DefaultLotSerialFromShipment.GetValueOrDefault();
    }
  }

  public virtual bool HasPick
  {
    get => ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPickTab.GetValueOrDefault();
  }

  public virtual bool HasPack
  {
    get => ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPackTab.GetValueOrDefault();
  }

  public virtual bool CannotConfirmPartialShipments
  {
    get
    {
      return ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShortShipmentConfirmation == "F";
    }
  }

  public virtual bool PromptLocationForEveryLine
  {
    get
    {
      return ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.RequestLocationForEachItem.GetValueOrDefault();
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "View Order")]
  protected virtual IEnumerable viewOrder(PXAdapter adapter)
  {
    PX.Objects.SO.SOShipLineSplit current = (PX.Objects.SO.SOShipLineSplit) ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOShipLineSplit>((PXGraph) this.Graph)).Current;
    if (current == null)
      return adapter.Get();
    SOShipLine soShipLine = PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentNbr, Equal<BqlField<PX.Objects.SO.SOShipLineSplit.shipmentNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<SOShipLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOShipLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Graph, (object[]) new PX.Objects.SO.SOShipLineSplit[1]
    {
      current
    }, Array.Empty<object>()));
    if (soShipLine == null)
      return adapter.Get();
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>((object) soShipLine.OrigOrderType, (object) soShipLine.OrigOrderNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewOrder");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected override void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    if (this.DocumentIsConfirmed)
    {
      PXCache<PX.Objects.SO.SOShipLineSplit> cache = GraphHelper.Caches<PX.Objects.SO.SOShipLineSplit>((PXGraph) this.Graph);
      ((PXCache) cache).SetAllEditPermissions(false);
      PXCacheEx.AdjustUI((PXCache) cache, (object) null).ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    }
    if (string.IsNullOrEmpty(this.RefNbr))
      ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Graph.Document).Current = (PX.Objects.SO.SOShipment) null;
    else
      ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Graph.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<PickPackShip.Host>) this).Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) this.RefNbr, Array.Empty<object>()));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOPickPackShipUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current));
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOPickPackShipUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current));
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.lotSerialNbr> e)
  {
    if (e.Row == null || !e.Row.IsUnassigned.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.lotSerialNbr>>) e).ReturnValue = (object) "<UNASSIGNED>";
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOShipLineSplit> e)
  {
    if (e.Row == null || !e.Row.IsUnassigned.GetValueOrDefault())
      return;
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipLineSplit>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
  }

  [BorrowedNote(typeof (PX.Objects.SO.SOShipment), typeof (SOShipmentEntry))]
  protected virtual void _(PX.Data.Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  [PXMergeAttributes]
  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (PX.Objects.SO.SOShipment.shipmentNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<WMSScanHeader.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlOperand<InventoryMultiplicator.increase, IBqlShort>.When<BqlOperand<ScanHeader.mode, IBqlString>.IsEqual<PickPackShip.ReturnMode.value>>.Else<InventoryMultiplicator.decrease>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<WMSScanHeader.inventoryMultiplicator> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipLineSplit.lineNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SOShipLotSerialNbrAttribute), "ForceDisable", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipLineSplit.lotSerialNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SiteAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipLineSplit.siteID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SOLocationAvailAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipLineSplit.locationID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipLineSplit.qty> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (SearchFor<PX.Objects.SO.SOOrder.orderNbr>.Where<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<BqlField<SOShipLine.origOrderType, IBqlString>.FromCurrent>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.origOrderNbr> e)
  {
  }

  protected virtual ScanMode<PickPackShip> GetDefaultMode()
  {
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXViewOf<UserPreferences>.BasedOn<SelectFromBase<UserPreferences, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>.Config>.Select((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base, Array.Empty<object>()));
    DefaultPickPackShipModeByUser extension = userPreferences != null ? PXCacheEx.GetExtension<DefaultPickPackShipModeByUser>((IBqlTable) userPreferences) : (DefaultPickPackShipModeByUser) null;
    PickPackShip.PickMode defaultMode1 = this.ScanModes.OfType<PickPackShip.PickMode>().FirstOrDefault<PickPackShip.PickMode>();
    PickPackShip.PackMode defaultMode2 = this.ScanModes.OfType<PickPackShip.PackMode>().FirstOrDefault<PickPackShip.PackMode>();
    PickPackShip.ShipMode defaultMode3 = this.ScanModes.OfType<PickPackShip.ShipMode>().FirstOrDefault<PickPackShip.ShipMode>();
    PickPackShip.ReturnMode defaultMode4 = this.ScanModes.OfType<PickPackShip.ReturnMode>().FirstOrDefault<PickPackShip.ReturnMode>();
    if (extension?.PPSMode == "PICK" && ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPickTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode1;
    if (extension?.PPSMode == "PACK" && ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPackTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode2;
    if (extension?.PPSMode == "SHIP" && ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowShipTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode3;
    if (extension?.PPSMode == "CRTN" && ((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowReturningTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode4;
    if (((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPickTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode1;
    if (((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowPackTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode2;
    if (((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowShipTab.GetValueOrDefault())
      return (ScanMode<PickPackShip>) defaultMode3;
    return !((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShowReturningTab.GetValueOrDefault() ? base.GetDefaultMode() : (ScanMode<PickPackShip>) defaultMode4;
  }

  protected virtual IEnumerable<ScanMode<PickPackShip>> CreateScanModes()
  {
    yield return (ScanMode<PickPackShip>) new PickPackShip.PickMode();
    yield return (ScanMode<PickPackShip>) new PickPackShip.PackMode();
    yield return (ScanMode<PickPackShip>) new PickPackShip.ShipMode();
    yield return (ScanMode<PickPackShip>) new PickPackShip.ReturnMode();
  }

  public virtual PX.Objects.SO.SOShipment Shipment
  {
    get
    {
      return (PX.Objects.SO.SOShipment) PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.Find((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base, (PX.Objects.SO.SOShipment.shipmentNbr) ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<PickPackShip.Host>) this).Base.Document).Current, (PKFindOptions) 0);
    }
  }

  public virtual bool IsTransfer => this.Shipment?.ShipmentType == "T";

  public virtual IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> GetSplits(
    string shipmentNbr,
    bool includeUnassigned = false,
    Func<PX.Objects.SO.SOShipLineSplit, bool> processedSeparator = null)
  {
    IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> first = ((IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.SOShipLineSplit, PXViewOf<PX.Objects.SO.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.SOShipLineSplit.FK.ShipmentLine>>, FbqlJoins.Inner<INLocation>.On<BqlOperand<PX.Objects.SO.SOShipLineSplit.locationID, IBqlInt>.IsEqual<INLocation.locationID>>>>.Where<BqlOperand<PX.Objects.SO.SOShipLineSplit.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base, new object[1]
    {
      (object) shipmentNbr
    })).AsEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit>>().Cast<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>();
    IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> pxResults;
    if (includeUnassigned)
    {
      IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> second = ((IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit, PXViewOf<PX.Objects.SO.Unassigned.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.Unassigned.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.Unassigned.SOShipLineSplit.FK.ShipmentLine>>, FbqlJoins.Inner<INLocation>.On<BqlOperand<PX.Objects.SO.Unassigned.SOShipLineSplit.locationID, IBqlInt>.IsEqual<INLocation.locationID>>>>.Where<BqlOperand<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base, new object[1]
      {
        (object) shipmentNbr
      })).AsEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>().Cast<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>>().Select<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>, PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>((Func<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>, PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>) (r => new PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>(MakeAssigned(PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>.op_Implicit(r)), PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>.op_Implicit(r), PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLine, INLocation>.op_Implicit(r))));
      pxResults = first.Concat<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>(second);
    }
    else
      pxResults = first;
    IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> source1;
    IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> source2;
    if (processedSeparator == null)
    {
      PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>[] pxResultArray = Array.Empty<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>();
      source1 = pxResults;
      source2 = (IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>) pxResultArray;
    }
    else
      (source2, source1) = EnumerableExtensions.DisuniteBy<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>(pxResults, (Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, bool>) (s => processedSeparator(((PXResult) s).GetItem<PX.Objects.SO.SOShipLineSplit>())));
    List<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>> splits = new List<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>();
    splits.AddRange((IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>) source1.OrderBy<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>) (r =>
    {
      if (!(((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShipmentLocationOrdering == "PICK"))
        return ((PXResult) r).GetItem<INLocation>().PathPriority;
      short? pickPriority = ((PXResult) r).GetItem<INLocation>().PickPriority;
      return !pickPriority.HasValue ? new int?() : new int?((int) pickPriority.GetValueOrDefault());
    })).ThenBy<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, bool>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, bool>) (r =>
    {
      bool? isUnassigned = ((PXResult) r).GetItem<PX.Objects.SO.SOShipLineSplit>().IsUnassigned;
      bool flag = false;
      return isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue;
    })).ThenBy<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.SO.SOShipLineSplit>().InventoryID)).ThenBy<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, string>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, string>) (r => ((PXResult) r).GetItem<PX.Objects.SO.SOShipLineSplit>().LotSerialNbr)));
    splits.AddRange((IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>) source2.OrderByDescending<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>) (r =>
    {
      if (!(((PXSelectBase<SOPickPackShipSetup>) this.Setup).Current.ShipmentLocationOrdering == "PICK"))
        return ((PXResult) r).GetItem<INLocation>().PathPriority;
      short? pickPriority = ((PXResult) r).GetItem<INLocation>().PickPriority;
      return !pickPriority.HasValue ? new int?() : new int?((int) pickPriority.GetValueOrDefault());
    })).ThenByDescending<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.SO.SOShipLineSplit>().InventoryID)).ThenByDescending<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, string>((Func<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>, string>) (r => ((PXResult) r).GetItem<PX.Objects.SO.SOShipLineSplit>().LotSerialNbr)));
    return (IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine, INLocation>>) splits;

    static PX.Objects.SO.SOShipLineSplit MakeAssigned(PX.Objects.SO.Unassigned.SOShipLineSplit unassignedSplit)
    {
      return PropertyTransfer.Transfer<PX.Objects.SO.Unassigned.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit>(unassignedSplit, new PX.Objects.SO.SOShipLineSplit());
    }
  }

  public virtual bool IsLocationMissing(
    PXSelectBase<PX.Objects.SO.SOShipLineSplit> splitView,
    INLocation location,
    out Validation error)
  {
    if (((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splitView.SelectMain(Array.Empty<object>())).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (t =>
    {
      int? locationId1 = t.LocationID;
      int? locationId2 = location.LocationID;
      return !(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue);
    })))
    {
      error = Validation.Fail("{0} location not listed in shipment.", new object[1]
      {
        (object) location.LocationCD
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool IsItemMissing(
    PXSelectBase<PX.Objects.SO.SOShipLineSplit> splitView,
    PXResult<INItemXRef, PX.Objects.IN.InventoryItem> item,
    out Validation error)
  {
    INItemXRef inItemXref;
    PX.Objects.IN.InventoryItem inventoryItem1;
    item.Deconstruct(ref inItemXref, ref inventoryItem1);
    PX.Objects.IN.InventoryItem inventoryItem = inventoryItem1;
    if (((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splitView.SelectMain(Array.Empty<object>())).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (t =>
    {
      int? inventoryId1 = t.InventoryID;
      int? inventoryId2 = inventoryItem.InventoryID;
      return !(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue);
    })))
    {
      error = Validation.Fail("{0} item not listed in shipment.", new object[1]
      {
        (object) inventoryItem.InventoryCD
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool IsLotSerialMissing(
    PXSelectBase<PX.Objects.SO.SOShipLineSplit> splitView,
    string lotSerialNbr,
    out Validation error)
  {
    if (!this.LotSerialTrack.IsEnterable && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splitView.SelectMain(Array.Empty<object>())).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (t => !string.Equals(t.LotSerialNbr, lotSerialNbr, StringComparison.OrdinalIgnoreCase))))
    {
      error = Validation.Fail("{0} lot or serial number not listed in shipment.", new object[1]
      {
        (object) lotSerialNbr
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public void EnsureAssignedSplitEditing(PX.Objects.SO.SOShipLineSplit split)
  {
    if (split.IsUnassigned.GetValueOrDefault())
      throw new InvalidOperationException("Unassigned splits should not be edited directly by WMS screen");
  }

  [Obsolete]
  public virtual string GetCommandOrShipmentOnlyPrompt()
  {
    return this.Get<PickPackShip.CommandOrShipmentOnlyState.Logic>().GetPromptForCommandOrShipmentOnly();
  }

  public virtual bool HasNonStockLinesWithEmptyLocation(PX.Objects.SO.SOShipment shipment, out Validation error)
  {
    if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOShipLine.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.kitItem, IBqlBool>.IsEqual<False>>>, And<BqlOperand<SOShipLine.locationID, IBqlInt>.IsNull>>>.And<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this), new object[1]
    {
      (object) shipment.ShipmentNbr
    })) != null)
    {
      error = Validation.Fail("The {0} shipment cannot be processed on the Pick, Pack, and Ship (SO302020) form because it contains a non-stock item with an empty location.", new object[1]
      {
        (object) shipment.ShipmentNbr
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool HasIncompleteLinesBy<TQtyField>() where TQtyField : class, IBqlField, IImplement<IBqlDecimal>
  {
    return ((IQueryable<PXResult<PX.Objects.SO.SOLine>>) PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.OrderLine>>, FbqlJoins.Inner<PX.Objects.SO.SOShipLineSplit>.On<PX.Objects.SO.SOShipLineSplit.FK.ShipmentLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.SameAsCurrent>, And<BqlOperand<Mult<PX.Objects.SO.SOShipLineSplit.qty, BqlOperand<PX.Objects.SO.SOLine.completeQtyMin, IBqlDecimal>.Divide<decimal100>>, IBqlDecimal>.IsGreater<TQtyField>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.shipComplete, Equal<SOShipComplete.shipComplete>>>>>.Or<BqlOperand<PX.Objects.SO.SOLine.shipComplete, IBqlString>.IsEqual<SOShipComplete.shipComplete>>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this), (object[]) new PX.Objects.SO.SOShipment[1]
    {
      this.Shipment
    }, Array.Empty<object>())).Any<PXResult<PX.Objects.SO.SOLine>>();
  }

  protected virtual void LogScan(ScanHeader headerBefore, ScanHeader headerAfter)
  {
    base.LogScan(headerBefore, headerAfter);
    if (headerBefore.Barcode.StartsWith("@"))
      return;
    this.UpdateWorkLogOnLogScan(this.Graph.WorkLogExt, ((PXSelectBase<ScanInfo>) this.Info).Current.MessageType == "ERR");
    if (!((PXCache) GraphHelper.Caches<SOShipmentProcessedByUser>((PXGraph) this.Graph)).IsDirty)
      return;
    this.Graph.WorkLogExt.PersistWorkLog();
  }

  protected virtual void UpdateWorkLogOnLogScan(SOShipmentEntry.WorkLog workLogger, bool isError)
  {
    if (this.Shipment == null)
      return;
    string jobType;
    if (this.CurrentMode is PickPackShip.PackMode)
    {
      jobType = this.HasPick ? "PACK" : "PPCK";
    }
    else
    {
      if (!(this.CurrentMode is PickPackShip.PickMode) && !(this.CurrentMode is PickPackShip.ReturnMode))
        return;
      jobType = "PICK";
    }
    workLogger.LogScanFor(this.Shipment.ShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), jobType, isError);
  }

  public virtual void InjectLocationDeactivationOnDefaultLocationOption(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => !basis.DefaultLocation), false, new RelativeInject?());
  }

  public virtual void InjectLotSerialDeactivationOnDefaultLotSerialOption(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lsState,
    bool isEntranceAllowed)
  {
    ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, string>) lsState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis =>
    {
      if (!basis.DefaultLotSerial || basis.Remove.GetValueOrDefault())
        return true;
      return isEntranceAllowed && basis.LotSerialTrack.IsEnterable;
    }), false, new RelativeInject?());
    ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, string>) lsState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => basis.SelectedLotSerialClass.With<INLotSerClass, bool>((Func<INLotSerClass, bool>) (it => it.LotSerAssign == "U")).Implies(!basis.IsTransfer)), false, new RelativeInject?());
  }

  public virtual void InjectLocationSkippingOnPromptLocationForEveryLineOption(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.IsStateSkippable).ByDisjoin((Func<PickPackShip, bool>) (basis => !basis.PromptLocationForEveryLine && basis.LocationID.HasValue), false, new RelativeInject?());
  }

  public virtual void InjectItemAbsenceHandlingByLocation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) inventoryState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) => AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(basis.TryProcessBy<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Done : AbsenceHandling.Skipped)), new RelativeInject?());
  }

  public virtual void InjectLocationPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState,
    Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>> viewSelector)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<INLocation, Validation>.AsAppendable) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.Validate).ByAppend((Func<Validation, INLocation, Validation>) ((basis, location) => !basis.IsLocationMissing(viewSelector(basis), location, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public virtual void InjectItemPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState,
    Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>> viewSelector)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>.AsAppendable) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.Validate).ByAppend((Func<Validation, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>) ((basis, item) => !basis.IsItemMissing(viewSelector(basis), item, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public virtual void InjectLotSerialPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerailState,
    Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>> viewSelector)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfFunc<string, Validation>.AsAppendable) ((EntityState<PickPackShip, string>) lotSerailState).Intercept.Validate).ByAppend((Func<Validation, string, Validation>) ((basis, lotSerialNbr) => !basis.IsLotSerialMissing(viewSelector(basis), lotSerialNbr, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public sealed class PackMode : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "PACK";

    public virtual string Code => "PACK";

    public virtual string Description => "Pack";

    protected virtual bool IsModeActive()
    {
      return ((PXSelectBase<SOPickPackShipSetup>) ((ScanMode<PickPackShip>) this).Basis.Setup).Current.ShowPackTab.GetValueOrDefault();
    }

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      PickPackShip.PackMode packMode = this;
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.ShipmentState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN),
        IsForIssue = true,
        IsForTransfer = ((ScanMode<PickPackShip>) packMode).Basis.IsTransfer,
        SuppressModuleItemStatusCheck = true
      };
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.ConfirmState();
      yield return (ScanState<PickPackShip>) new PickPackShip.CommandOrShipmentOnlyState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.StartState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.DimensionsState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.CompleteState();
      if (!((ScanMode<PickPackShip>) packMode).Basis.HasPick)
      {
        yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
        yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState()
        {
          IsForIssue = true,
          IsForTransfer = ((ScanMode<PickPackShip>) packMode).Basis.IsTransfer
        };
      }
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (basis => basis.HasPick)).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (separatePicking => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) separatePicking.From<PickPackShip.PackMode.ShipmentState>().NextTo<PickPackShip.PackMode.BoxState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (packOnly => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) packOnly.From<PickPackShip.PackMode.ShipmentState>().NextTo<PickPackShip.PackMode.BoxState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))))).Concat<ScanTransition<PickPackShip>>(((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.PackMode.BoxConfirming.StartState>().NextTo<PickPackShip.PackMode.BoxConfirming.WeightState>((Action<PickPackShip>) null)).NextTo<PickPackShip.PackMode.BoxConfirming.DimensionsState>((Action<PickPackShip>) null)).NextTo<PickPackShip.PackMode.BoxConfirming.CompleteState>((Action<PickPackShip>) null))));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new PickPackShip.PackMode.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.PackMode.ConfirmPackageCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ConfirmShipmentCommand();
    }

    protected virtual IEnumerable<ScanQuestion<PickPackShip>> CreateQuestions()
    {
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion();
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState.SkipScalesQuestion();
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.ShipmentState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxConfirming.WeightState>(true);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxConfirming.DimensionsState>(true);
      if (fullReset)
        this.Get<PickPackShip.PackMode.Logic>().PackageLineNbrUI = new int?();
      if (((ScanMode<PickPackShip>) this).Basis.HasPick)
        return;
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset || ((ScanMode<PickPackShip>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PickPackShip.PackMode.value>
    {
      public value()
        : base("PACK")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.SOShipLineSplit.FK.ShipmentLine>>>.Order<By<BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.shipmentNbr, IBqlString>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.isUnassigned, IBqlBool>.Desc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.lineNbr, IBqlInt>.Asc>>, 
      #nullable disable
      PX.Objects.SO.SOShipLineSplit>.View PickedForPack;
      public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.SO.SOShipLineSplit>.View Packed;
      public FbqlSelect<SelectFromBase<SOPackageDetailEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      SOPackageDetailEx.shipmentNbr, 
      #nullable disable
      Equal<BqlField<
      #nullable enable
      WMSScanHeader.refNbr, IBqlString>.FromCurrent>>>>>.And<
      #nullable disable
      BqlOperand<
      #nullable enable
      SOPackageDetailEx.lineNbr, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PackScanHeader.packageLineNbrUI, IBqlInt>.FromCurrent.NoDefault>>>, 
      #nullable disable
      SOPackageDetailEx>.View ShownPackage;
      public PXAction<ScanHeader> ReviewPack;

      public PackScanHeader PackHeader
      {
        get => ScanHeaderExt.Get<PackScanHeader>(this.Basis.Header) ?? new PackScanHeader();
      }

      public ValueSetter<ScanHeader>.Ext<PackScanHeader> PackSetter
      {
        get => this.Basis.HeaderSetter.With<PackScanHeader>();
      }

      public int? PackageLineNbr
      {
        get => this.PackHeader.PackageLineNbr;
        set
        {
          ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter = this.PackSetter;
          (^ref packSetter).Set<int?>((Expression<Func<PackScanHeader, int?>>) (h => h.PackageLineNbr), value);
        }
      }

      public int? PackageLineNbrUI
      {
        get => this.PackHeader.PackageLineNbrUI;
        set
        {
          ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter = this.PackSetter;
          (^ref packSetter).Set<int?>((Expression<Func<PackScanHeader, int?>>) (h => h.PackageLineNbrUI), value);
        }
      }

      protected virtual IEnumerable pickedForPack()
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        pxDelegateResult.IsResultSorted = true;
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) this.Basis.GetSplits(this.Basis.RefNbr, true, (Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? packedQty = s.PackedQty;
          Decimal? qty = s.Qty;
          return packedQty.GetValueOrDefault() >= qty.GetValueOrDefault() & packedQty.HasValue & qty.HasValue;
        })));
        return (IEnumerable) pxDelegateResult;
      }

      protected virtual IEnumerable packed()
      {
        if (this.Basis.Header == null)
          return (IEnumerable) Enumerable.Empty<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>>();
        return (IEnumerable) ((IEnumerable<SOShipLineSplitPackage>) ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).SelectMain(new object[2]
        {
          (object) this.Basis.RefNbr,
          (object) this.PackageLineNbrUI
        })).SelectMany((Func<SOShipLineSplitPackage, IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>>>) (link => (IEnumerable<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.PickedForPack).Select(Array.Empty<object>()).Cast<PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>>()), (link, split) => new
        {
          link = link,
          split = split
        }).Where(_param1 =>
        {
          if (PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>.op_Implicit(_param1.split).ShipmentNbr == _param1.link.ShipmentNbr)
          {
            int? lineNbr = PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>.op_Implicit(_param1.split).LineNbr;
            int? shipmentLineNbr = _param1.link.ShipmentLineNbr;
            if (lineNbr.GetValueOrDefault() == shipmentLineNbr.GetValueOrDefault() & lineNbr.HasValue == shipmentLineNbr.HasValue)
            {
              int? splitLineNbr = PXResult<PX.Objects.SO.SOShipLineSplit, SOShipLine>.op_Implicit(_param1.split).SplitLineNbr;
              int? shipmentSplitLineNbr = _param1.link.ShipmentSplitLineNbr;
              return splitLineNbr.GetValueOrDefault() == shipmentSplitLineNbr.GetValueOrDefault() & splitLineNbr.HasValue == shipmentSplitLineNbr.HasValue;
            }
          }
          return false;
        }).Select(_param1 => _param1.split);
      }

      [PXButton]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewPack(PXAdapter adapter)
      {
        this.PackageLineNbrUI = new int?();
        return adapter.Get();
      }

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        if (e.Row == null)
          return;
        EnumerableExtensions.Consume<PXCache>(EnumerableExtensions.Modify<PXCache>((IEnumerable<PXCache>) new PXCache[2]
        {
          ((PXSelectBase) ((PXGraphExtension<PickPackShip.Host>) this).Base.Packages).Cache,
          ((PXSelectBase) ((PXGraphExtension<PickPackShip.Host>) this).Base.PackageDetailExt.PackageDetailSplit).Cache
        }, (Action<PXCache>) (c => c.AllowInsert = c.AllowUpdate = c.AllowDelete = !this.Basis.DocumentIsConfirmed)));
        ((PXAction) this.ReviewPack).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile && e.Row.Mode == "PACK");
      }

      public virtual void InjectExpireDateForPackDeactivationOnAlreadyEnteredLot(
        WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState)
      {
        ((MethodInterceptor<EntityState<PickPackShip, DateTime?>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, DateTime?>) expireDateState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => basis.SelectedLotSerialClass?.LotSerAssign == "U" && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) basis.Get<PickPackShip.PackMode.Logic>().PickedForPack).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (t =>
        {
          if (t.IsUnassigned.GetValueOrDefault())
            return true;
          if (!(t.LotSerialNbr == basis.LotSerialNbr))
            return false;
          Decimal? packedQty = t.PackedQty;
          Decimal num = 0M;
          return packedQty.GetValueOrDefault() == num & packedQty.HasValue;
        }))), false, new RelativeInject?());
      }

      public virtual void InjectItemAbsenceHandlingByBox(
        WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState)
      {
        ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) =>
        {
          bool? nullable = basis.Get<PickPackShip.PackMode.Logic>().TryAutoConfirmCurrentPackageAndLoadNext(barcode);
          bool flag = false;
          return AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(nullable.GetValueOrDefault() == flag & nullable.HasValue ? AbsenceHandling.Skipped : AbsenceHandling.Done);
        }), new RelativeInject?());
      }

      public virtual void InjectItemPromptForPackageConfirm(
        WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState)
      {
        ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.StatePrompt).ByOverride((Func<PickPackShip, Func<string>, string>) ((basis, base_StatePrompt) => basis.Get<PickPackShip.PackMode.Logic>().With<PickPackShip.PackMode.Logic, string>((Func<PickPackShip.PackMode.Logic, string>) (mode =>
        {
          if (basis.Remove.GetValueOrDefault() || !mode.CanConfirmPackage)
            return (string) null;
          return !basis.HasActive<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>() ? "Confirm the package, or scan the next item." : "Confirm the package, or scan the next item or the next location.";
        })) ?? base_StatePrompt()), new RelativeInject?());
      }

      [PXOverride]
      public ScanState<PickPackShip> DecorateScanState(
        ScanState<PickPackShip> original,
        Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
      {
        ScanState<PickPackShip> scanState = base_DecorateScanState(original);
        if (((ScanComponent<PickPackShip>) scanState).ModeCode == "PACK")
        {
          switch (scanState)
          {
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState:
              this.Basis.InjectLocationDeactivationOnDefaultLocationOption(locationState);
              this.Basis.InjectLocationSkippingOnPromptLocationForEveryLineOption(locationState);
              this.Basis.InjectLocationPresenceValidation(locationState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryItemState:
              if (!this.Basis.HasPick)
                this.Basis.InjectItemAbsenceHandlingByLocation(inventoryItemState);
              this.InjectItemPromptForPackageConfirm(inventoryItemState);
              this.InjectItemAbsenceHandlingByBox(inventoryItemState);
              this.Basis.InjectItemPresenceValidation(inventoryItemState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerialState:
              this.Basis.InjectLotSerialPresenceValidation(lotSerialState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              this.Basis.InjectLotSerialDeactivationOnDefaultLotSerialOption(lotSerialState, !this.Basis.HasPick);
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState:
              this.InjectExpireDateForPackDeactivationOnAlreadyEnteredLot(expireDateState);
              break;
          }
        }
        return scanState;

        static PXSelectBase<PX.Objects.SO.SOShipLineSplit> viewSelector(PickPackShip basis)
        {
          return (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) basis.Get<PickPackShip.PackMode.Logic>().PickedForPack;
        }
      }

      /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
      [PXOverride]
      public void OnBeforeFullClear(Action base_OnBeforeFullClear)
      {
        base_OnBeforeFullClear();
        if (!(this.Basis.CurrentMode is PickPackShip.PackMode) || this.Basis.RefNbr == null || !this.Graph.WorkLogExt.SuspendFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), this.Basis.HasPick ? "PACK" : "PPCK"))
          return;
        this.Graph.WorkLogExt.PersistWorkLog();
      }

      [Obsolete]
      public virtual string GetCommandOrShipmentOnlyPrompt(
        Func<string> base_GetCommandOrShipmentOnlyPrompt)
      {
        return base_GetCommandOrShipmentOnlyPrompt();
      }

      public virtual bool ShowPackTab(ScanHeader row) => this.Basis.HasPack && row.Mode == "PACK";

      public virtual bool CanPack
      {
        get
        {
          return !this.Basis.HasPick ? ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.PickedForPack).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? packedQty = s.PackedQty;
            Decimal? qty = s.Qty;
            return packedQty.GetValueOrDefault() < qty.GetValueOrDefault() & packedQty.HasValue & qty.HasValue;
          })) : ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.PickedForPack).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? packedQty = s.PackedQty;
            Decimal? pickedQty = s.PickedQty;
            return packedQty.GetValueOrDefault() < pickedQty.GetValueOrDefault() & packedQty.HasValue & pickedQty.HasValue;
          }));
        }
      }

      public virtual bool CanConfirmPackage
      {
        get
        {
          return this.Basis.RefNbr != null && this.HasConfirmableBoxes && !this.HasSingleAutoPackage(this.Basis.RefNbr, out SOPackageDetailEx _) && this.PackageLineNbr.HasValue && this.SelectedPackage != null && !this.IsPackageEmpty(this.SelectedPackage);
        }
      }

      public virtual bool IsPackageEmpty(SOPackageDetailEx package)
      {
        return ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Select(new object[2]
        {
          (object) package.ShipmentNbr,
          (object) package.LineNbr
        }).Count == 0;
      }

      public SOPackageDetailEx SelectedPackage
      {
        get
        {
          return PXResultset<SOPackageDetailEx>.op_Implicit(((PXSelectBase<SOPackageDetailEx>) this.Graph.Packages).Search<SOPackageDetailEx.lineNbr>((object) this.PackageLineNbr, Array.Empty<object>()));
        }
      }

      public virtual bool HasConfirmableBoxes
      {
        get
        {
          return ((IEnumerable<SOPackageDetailEx>) ((PXSelectBase<SOPackageDetailEx>) this.Graph.Packages).SelectMain(Array.Empty<object>())).Any<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (p =>
          {
            bool? confirmed = p.Confirmed;
            bool flag = false;
            return confirmed.GetValueOrDefault() == flag & confirmed.HasValue && !this.IsPackageEmpty(p);
          }));
        }
      }

      public virtual bool HasSingleAutoPackage(string shipmentNbr, out SOPackageDetailEx package)
      {
        if (PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
        {
          SOPackageDetailEx[] array = GraphHelper.RowCast<SOPackageDetailEx>((IEnumerable) PXSelectBase<SOPackageDetailEx, PXViewOf<SOPackageDetailEx>.BasedOn<SelectFromBase<SOPackageDetailEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOPackageDetailEx.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
          {
            (object) shipmentNbr
          })).ToArray<SOPackageDetailEx>();
          if (array.Length == 1 && array[0].PackageType == "A")
          {
            package = array[0];
            return true;
          }
          if (((IEnumerable<SOPackageDetailEx>) array).Any<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (p => p.PackageType == "A")))
            throw new PXInvalidOperationException("The {0} shipment cannot be processed in Pack mode because it has two or more packages assigned.", new object[1]
            {
              (object) shipmentNbr
            });
        }
        package = (SOPackageDetailEx) null;
        return false;
      }

      public virtual bool? TryAutoConfirmCurrentPackageAndLoadNext(string boxBarcode)
      {
        bool? remove = this.Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && PX.Objects.CS.CSBox.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), boxBarcode) != null)
        {
          if (!this.Basis.Get<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>().TryAutoConfirm())
            return new bool?();
          if (this.Basis.TryProcessBy<PickPackShip.PackMode.BoxState>(boxBarcode, (StateSubstitutionRule) 26))
            return new bool?(true);
        }
        return new bool?(false);
      }

      public class AlterCommandOrShipmentOnlyStatePrompt : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.CommandOrShipmentOnlyState.Logic>
      {
        /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.CommandOrShipmentOnlyState.Logic.GetPromptForCommandOrShipmentOnly" />
        [PXOverride]
        public virtual string GetPromptForCommandOrShipmentOnly(
          Func<string> base_GetPromptForCommandOrShipmentOnly)
        {
          if (((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentMode is PickPackShip.PackMode)
          {
            PickPackShip.PackMode.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.Logic>();
            if (logic != null && logic.CanConfirmPackage)
              return "Confirm the package.";
          }
          return base_GetPromptForCommandOrShipmentOnly();
        }
      }
    }

    public sealed class ShipmentState : PickPackShip.ShipmentState
    {
      protected virtual Validation Validate(PX.Objects.SO.SOShipment shipment)
      {
        if (shipment.Operation != "I")
          return Validation.Fail("The {0} shipment cannot be packed because it has the {1} operation.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.operation>((IBqlTable) shipment)
          });
        if (shipment.Status != "N")
          return Validation.Fail("The {0} shipment cannot be packed because it has the {1} status.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
          });
        IEnumerable<PX.Objects.SO.SOShipLineSplit> source = GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>((IEnumerable) ((ScanComponent<PickPackShip>) this).Basis.GetSplits(shipment.ShipmentNbr, true)).AsEnumerable<PX.Objects.SO.SOShipLineSplit>();
        if (((ScanComponent<PickPackShip>) this).Basis.HasPick && source.All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal num = 0M;
          return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
        })))
          return Validation.Fail("The {0} shipment cannot be packed because the items have not been picked.", new object[1]
          {
            (object) shipment.ShipmentNbr
          });
        Validation error;
        if (((ScanComponent<PickPackShip>) this).Basis.HasNonStockLinesWithEmptyLocation(shipment, out error))
          return error;
        this.Get<PickPackShip.PackMode.Logic>().HasSingleAutoPackage(shipment.ShipmentNbr, out SOPackageDetailEx _);
        return Validation.Ok;
      }

      protected override void ReportSuccess(PX.Objects.SO.SOShipment shipment)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment loaded and ready to be packed.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
      }

      protected virtual void SetNextState()
      {
        PickPackShip.PackMode.Logic logic = ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.PackMode.Logic>();
        if (((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault() || logic.CanPack || logic.HasConfirmableBoxes)
        {
          ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) this).SetNextState();
        }
        else
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<PickPackShip>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<PickPackShip>) this).Basis.Localize("{0} shipment packed.", new object[1]
            {
              (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
        }
      }

      [PXLocalizable]
      public new abstract class Msg : PickPackShip.ShipmentState.Msg
      {
        public new const string Ready = "{0} shipment loaded and ready to be packed.";
        public const string InvalidStatus = "The {0} shipment cannot be packed because it has the {1} status.";
        public const string InvalidOperation = "The {0} shipment cannot be packed because it has the {1} operation.";
        public const string ShouldBePickedFirst = "The {0} shipment cannot be packed because the items have not been picked.";
      }
    }

    public sealed class BoxState : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<PX.Objects.CS.CSBox>
    {
      public const string Value = "BOX";

      public PickPackShip.PackMode.Logic Mode => this.Get<PickPackShip.PackMode.Logic>();

      public virtual string Code => "BOX";

      protected virtual string StatePrompt => "Scan the box.";

      protected virtual bool IsStateSkippable()
      {
        return ((EntityState<PickPackShip, PX.Objects.CS.CSBox>) this).IsStateSkippable() || this.Mode.PackageLineNbr.HasValue;
      }

      protected virtual void OnTakingOver()
      {
        SOPackageDetailEx package;
        if (!this.Mode.HasSingleAutoPackage(((ScanComponent<PickPackShip>) this).Basis.RefNbr, out package))
          return;
        this.Mode.PackageLineNbr = package.LineNbr;
        this.Mode.PackageLineNbrUI = package.LineNbr;
        ((PXSelectBase<SOPackageDetailEx>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).Current = package;
        ((ScanState<PickPackShip>) this).MoveToNextState();
      }

      protected virtual PX.Objects.CS.CSBox GetByBarcode(string barcode)
      {
        return PX.Objects.CS.CSBox.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), barcode);
      }

      protected virtual void Apply(PX.Objects.CS.CSBox box)
      {
        SOPackageDetailEx soPackageDetailEx = ((IEnumerable<SOPackageDetailEx>) ((PXSelectBase<SOPackageDetailEx>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).SelectMain(Array.Empty<object>())).FirstOrDefault<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (p =>
        {
          if (!string.Equals(p.BoxID.Trim(), box.BoxID.Trim(), StringComparison.OrdinalIgnoreCase))
            return false;
          bool? confirmed = p.Confirmed;
          bool flag = false;
          return confirmed.GetValueOrDefault() == flag & confirmed.HasValue;
        }));
        if (soPackageDetailEx == null)
        {
          SOPackageDetailEx instance = (SOPackageDetailEx) ((PXSelectBase) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).Cache.CreateInstance();
          instance.BoxID = box.BoxID;
          instance.ShipmentNbr = ((ScanComponent<PickPackShip>) this).Basis.RefNbr;
          soPackageDetailEx = ((PXSelectBase<SOPackageDetailEx>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).Insert(instance);
          ((PXAction) ((ScanComponent<PickPackShip>) this).Basis.Save).Press();
        }
        this.Mode.PackageLineNbr = soPackageDetailEx.LineNbr;
        this.Mode.PackageLineNbrUI = soPackageDetailEx.LineNbr;
        ((PXSelectBase<SOPackageDetailEx>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).Current = soPackageDetailEx;
      }

      protected virtual void ClearState()
      {
        this.Mode.PackageLineNbr = new int?();
        ((PXSelectBase<SOPackageDetailEx>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Packages).Current = (SOPackageDetailEx) null;
      }

      protected virtual void ReportSuccess(PX.Objects.CS.CSBox entity)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} box selected.", new object[1]
        {
          (object) entity.BoxID
        });
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportError("{0} box not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual void SetNextState()
      {
        if (!((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault() && !this.Mode.CanPack)
        {
          ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
          if (this.Mode.CanConfirmPackage)
            return;
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment packed.", new object[1]
          {
            (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
          });
        }
        else
          ((EntityState<PickPackShip, PX.Objects.CS.CSBox>) this).SetNextState();
      }

      public class value : BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PickPackShip.PackMode.BoxState.value>
      {
        public value()
          : base("BOX")
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the box.";
        public const string Ready = "{0} box selected.";
        public const string Missing = "{0} box not found.";
      }
    }

    public static class BoxConfirming
    {
      public sealed class StartState : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.MediatorState
      {
        public const string Value = "BCS";

        public virtual string Code => "BCS";

        protected virtual bool IsStateActive()
        {
          return this.Get<PickPackShip.PackMode.Logic>().CanConfirmPackage;
        }

        protected virtual void Apply()
        {
          ((ScanComponent<PickPackShip>) this).Basis.Clear<PickPackShip.PackMode.BoxConfirming.WeightState>(true);
          ((ScanComponent<PickPackShip>) this).Basis.Clear<PickPackShip.PackMode.BoxConfirming.DimensionsState>(true);
        }

        protected virtual void SetNextState()
        {
          if (this.Get<PickPackShip.PackMode.Logic>().HasSingleAutoPackage(((ScanComponent<PickPackShip>) this).Basis.RefNbr, out SOPackageDetailEx _))
          {
            ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} {1}", new object[2]
            {
              (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<PickPackShip>) this).Basis.Info).Current.Message,
              (object) ((ScanComponent<PickPackShip>) this).Basis.Localize("{0} shipment packed.", new object[1]
              {
                (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
              })
            });
            ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
          }
          else
            ((MediatorState<PickPackShip>) this).SetNextState();
        }

        public class value : 
          BqlType<
          #nullable enable
          IBqlString, string>.Constant<
          #nullable disable
          PickPackShip.PackMode.BoxConfirming.StartState.value>
        {
          public value()
            : base("BCS")
          {
          }
        }
      }

      public sealed class WeightState : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<Decimal?>
      {
        public const string Value = "BWGT";

        public PickPackShip.PackMode.BoxConfirming.WeightState.Logic This
        {
          get => this.Get<PickPackShip.PackMode.BoxConfirming.WeightState.Logic>();
        }

        public PickPackShip.PackMode.Logic Mode => this.Get<PickPackShip.PackMode.Logic>();

        public virtual string Code => "BWGT";

        protected virtual string StatePrompt => "Enter the actual total weight of the package.";

        protected virtual void OnTakingOver()
        {
          if (!this.This.TryPrepareWeightAndSkipInputFor(this.Mode.SelectedPackage))
            return;
          ((ScanState<PickPackShip>) this).MoveToNextState();
        }

        protected virtual void OnDismissing()
        {
          ((ScanComponent<PickPackShip>) this).Basis.RevokeQuestion<PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion>();
        }

        protected virtual Decimal? GetByBarcode(string barcode)
        {
          Decimal result;
          return !Decimal.TryParse(barcode, out result) ? new Decimal?() : new Decimal?(result);
        }

        protected virtual void ReportMissing(string barcode)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportError("The quantity format does not fit the locale settings.", Array.Empty<object>());
        }

        protected virtual Validation Validate(Decimal? value)
        {
          Validation validation;
          if (((ScanComponent<PickPackShip>) this).Basis.HasFault<Decimal?>(value, new Func<Decimal?, Validation>(((EntityState<PickPackShip, Decimal?>) this).Validate), ref validation))
            return validation;
          string str;
          return !((ScanComponent<PickPackShip>) this).Basis.IsValid<SOPackageDetail.weight, SOPackageDetailEx>(this.Mode.SelectedPackage, (object) value.Value, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
        }

        protected virtual void Apply(Decimal? value)
        {
          this.This.Weight = new Decimal?(value.Value);
        }

        protected virtual void ClearState()
        {
          PickPackShip.PackMode.BoxConfirming.WeightState.Logic logic1 = this.This;
          PickPackShip.PackMode.BoxConfirming.WeightState.Logic logic2 = this.This;
          logic1.Weight = new Decimal?();
          DateTime? nullable = new DateTime?();
          logic2.LastWeighingTime = nullable;
        }

        protected virtual void ReportSuccess(Decimal? value)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("Once the package is confirmed, it will have the following weight: {0} {1}.", new object[2]
          {
            (object) value.Value,
            (object) this.Mode.SelectedPackage.WeightUOM
          });
        }

        public class value : 
          BqlType<
          #nullable enable
          IBqlString, string>.Constant<
          #nullable disable
          PickPackShip.PackMode.BoxConfirming.WeightState.value>
        {
          public value()
            : base("BWGT")
          {
          }
        }

        public sealed class SkipQuestion : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanQuestion
        {
          public PickPackShip.PackMode.BoxConfirming.WeightState.Logic State
          {
            get => this.Get<PickPackShip.PackMode.BoxConfirming.WeightState.Logic>();
          }

          public virtual string Code => "SKIPWEIGHT";

          protected virtual string GetPrompt() => "To skip the weighing, click OK.";

          protected virtual void Confirm()
          {
            if (!this.State.TryUsePreparedWeightFor(this.State.Target.SelectedPackage, true) || !(((ScanComponent<PickPackShip>) this).Basis.CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState))
              return;
            ((ScanComponent<PickPackShip>) this).Basis.DispatchNext((string) null, Array.Empty<object>());
          }

          protected virtual void Reject()
          {
          }

          [PXLocalizable]
          public abstract class Msg
          {
            public const string Prompt = "To skip the weighing, click OK.";
          }
        }

        public sealed class SkipScalesQuestion : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanQuestion
        {
          public PickPackShip.PackMode.BoxConfirming.WeightState.Logic State
          {
            get => this.Get<PickPackShip.PackMode.BoxConfirming.WeightState.Logic>();
          }

          public virtual string Code => "SKIPSCALES";

          protected virtual string GetPrompt()
          {
            return "Put the package on the scale and click OK. To skip the weighing, click OK without using the scale.";
          }

          protected virtual void Confirm()
          {
            SOPackageDetailEx selectedPackage = this.State.Target.SelectedPackage;
            DateTime? modifiedDateTime = this.State.SelectedScales.LastModifiedDateTime;
            DateTime? lastWeighingTime = this.State.LastWeighingTime;
            if ((modifiedDateTime.HasValue == lastWeighingTime.HasValue ? (modifiedDateTime.HasValue ? (modifiedDateTime.GetValueOrDefault() == lastWeighingTime.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              if (!this.State.TryUsePreparedWeightFor(selectedPackage) || !(((ScanComponent<PickPackShip>) this).Basis.CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState))
                return;
              ((ScanComponent<PickPackShip>) this).Basis.DispatchNext((string) null, Array.Empty<object>());
            }
            else
            {
              if (!this.State.ProcessScales(selectedPackage) || !(((ScanComponent<PickPackShip>) this).Basis.CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState))
                return;
              ((ScanComponent<PickPackShip>) this).Basis.DispatchNext((string) null, Array.Empty<object>());
            }
          }

          protected virtual void Reject()
          {
          }

          [PXLocalizable]
          public abstract class Msg
          {
            public const string Prompt = "Put the package on the scale and click OK. To skip the weighing, click OK without using the scale.";
          }
        }

        public class Logic : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.Logic>
        {
          public PXSetupOptional<CommonSetup> CommonSetupUOM;

          public virtual double ScaleWeightValiditySeconds => 30.0;

          public SMScale SelectedScales
          {
            get
            {
              Guid? scaleDeviceId = PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(this.Graph).ScaleDeviceID;
              ((PXCache) GraphHelper.Caches<SMScale>((PXGraph) this.Graph)).ClearQueryCache();
              return SMScale.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), scaleDeviceId, (PKFindOptions) 0);
            }
          }

          public Decimal? Weight
          {
            get => this.Target.PackHeader.Weight;
            set
            {
              ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter = this.Target.PackSetter;
              (^ref packSetter).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Weight), value);
            }
          }

          public DateTime? LastWeighingTime
          {
            get => this.Target.PackHeader.LastWeighingTime;
            set
            {
              ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter = this.Target.PackSetter;
              (^ref packSetter).Set<DateTime?>((Expression<Func<PackScanHeader, DateTime?>>) (h => h.LastWeighingTime), value);
            }
          }

          public virtual bool TryUsePreparedWeightFor(
            SOPackageDetailEx package,
            bool explicitConfirmation = false)
          {
            if (!explicitConfirmation && ((PXSelectBase<SOPickPackShipSetup>) ((PickPackShip) this.Basis).Setup).Current.ConfirmEachPackageWeight.GetValueOrDefault())
              return false;
            if (!this.CanSkipInputFor(package))
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("The package does not have a predefined weight.", Array.Empty<object>());
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion>();
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.WeightState.SkipScalesQuestion>();
              return false;
            }
            Validation validation;
            if (!this.Weight.HasValue || !((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).HasFault<Decimal?>(this.Weight, (Func<Decimal?, Validation?>) (w => ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).TryValidate<Decimal?>(w).By<PickPackShip.PackMode.BoxConfirming.WeightState>()), ref validation))
              return true;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError(((Validation) ref validation).Message, ((Validation) ref validation).MessageArgs);
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion>();
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.WeightState.SkipScalesQuestion>();
            return false;
          }

          public virtual bool TryPrepareWeightAndSkipInputFor(SOPackageDetailEx package)
          {
            Decimal? weight = package.Weight;
            Decimal num = 0M;
            this.Weight = new Decimal?(weight.GetValueOrDefault() == num & weight.HasValue ? this.AutoCalculateBoxWeightBasedOnItems(package) : package.Weight.Value);
            if (PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis)).UseScale.GetValueOrDefault() && !this.ProcessScales(package))
              return false;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo("The {0} package is ready to be confirmed. The calculated weight is {1} {2}.", new object[3]
            {
              (object) package.BoxID,
              (object) this.Weight,
              (object) package.WeightUOM
            });
            if (!((PXSelectBase<SOPickPackShipSetup>) ((PickPackShip) this.Basis).Setup).Current.ConfirmEachPackageWeight.GetValueOrDefault())
              return this.CanSkipInputFor(package);
            if (this.CanSkipInputFor(package))
              this.AskToSkipFor(package);
            return false;
          }

          protected virtual bool CanSkipInputFor(SOPackageDetailEx package)
          {
            return EnumerableExtensions.IsNotIn<Decimal?>(this.Weight, new Decimal?(), new Decimal?(0M)) || EnumerableExtensions.IsNotIn<Decimal?>(package.Weight, new Decimal?(), new Decimal?(0M));
          }

          protected virtual void AskToSkipFor(SOPackageDetailEx package)
          {
            Validation validation;
            if (((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).HasFault<Decimal?>(this.Weight, (Func<Decimal?, Validation?>) (w => ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).TryValidate<Decimal?>(w).By<PickPackShip.PackMode.BoxConfirming.WeightState>()), ref validation))
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Warn<PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion>("The {0} package is ready to be confirmed. The calculated weight is {1} {2}.", new object[3]
              {
                (object) package.BoxID,
                (object) this.Weight,
                (object) package.WeightUOM
              });
            else
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Ask<PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion>("The {0} package is ready to be confirmed. The calculated weight is {1} {2}.", new object[3]
              {
                (object) package.BoxID,
                (object) this.Weight,
                (object) package.WeightUOM
              });
          }

          protected virtual Decimal AutoCalculateBoxWeightBasedOnItems(SOPackageDetailEx package)
          {
            Decimal d = ((Decimal?) PX.Objects.CS.CSBox.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), package.BoxID)?.BoxWeight).GetValueOrDefault();
            PXSelect<SOShipLineSplitPackage, Where<SOShipLineSplitPackage.shipmentNbr, Equal<Optional<SOPackageDetail.shipmentNbr>>, And<SOShipLineSplitPackage.packageLineNbr, Equal<Optional<SOPackageDetail.lineNbr>>>>> packageDetailSplit = this.Graph.PackageDetailExt.PackageDetailSplit;
            object[] objArray = new object[2]
            {
              (object) package.ShipmentNbr,
              (object) package.LineNbr
            };
            foreach (SOShipLineSplitPackage lineSplitPackage in ((PXSelectBase<SOShipLineSplitPackage>) packageDetailSplit).SelectMain(objArray))
            {
              PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), lineSplitPackage.InventoryID);
              Decimal num1 = d;
              Decimal? nullable = inventoryItem.BaseWeight;
              Decimal valueOrDefault1 = nullable.GetValueOrDefault();
              nullable = lineSplitPackage.BasePackedQty;
              Decimal valueOrDefault2 = nullable.GetValueOrDefault();
              Decimal num2 = valueOrDefault1 * valueOrDefault2;
              d = num1 + num2;
            }
            return Math.Round(d, 4);
          }

          public virtual bool ProcessScales(SOPackageDetailEx package)
          {
            SMScale selectedScales = this.SelectedScales;
            if (selectedScales == null)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("{0} scale not found.", new object[1]
              {
                (object) ""
              });
              return false;
            }
            DateTime serverTime = this.GetServerTime();
            DateTime? modifiedDateTime = selectedScales.LastModifiedDateTime;
            this.LastWeighingTime = new DateTime?(modifiedDateTime.Value);
            modifiedDateTime = selectedScales.LastModifiedDateTime;
            if (modifiedDateTime.Value.AddHours(1.0) < serverTime)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("The system could not retrieve the weighing result from the {0} scale. Check if the scale is connected to the working station with DeviceHub.", new object[1]
              {
                (object) selectedScales.ScaleID
              });
              return false;
            }
            if (selectedScales.LastWeight.GetValueOrDefault() == 0M)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Warn<PickPackShip.PackMode.BoxConfirming.WeightState.SkipScalesQuestion>("The system could not retrieve the weighing result from the {0} scale. Ensure that items are placed on the scale.", new object[1]
              {
                (object) selectedScales.ScaleID
              });
              return false;
            }
            modifiedDateTime = selectedScales.LastModifiedDateTime;
            if (modifiedDateTime.Value.AddSeconds(this.ScaleWeightValiditySeconds) < serverTime)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("The weighing result on the {0} scale is more than {1} seconds old. Remove the package from the scale and weigh it again.", new object[2]
              {
                (object) selectedScales.ScaleID,
                (object) this.ScaleWeightValiditySeconds
              });
              return false;
            }
            SMScaleWeightConversion extension = PXCacheEx.GetExtension<SMScaleWeightConversion>((IBqlTable) selectedScales);
            if (extension == null || extension.CompanyUOM == null)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("Default values for weight UOM and volume UOM are not specified on the Companies (CS101500) form.", Array.Empty<object>());
              return false;
            }
            if ((extension != null ? (!extension.CompanyLastWeight.HasValue ? 1 : 0) : 1) != 0)
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("No rule for converting the {0} unit of measure to the {1} unit of measure has been set up on the Units of Measure (CS203500) form.", new object[2]
              {
                (object) selectedScales.UOM,
                (object) extension.CompanyUOM
              });
              return false;
            }
            this.Weight = new Decimal?(extension.CompanyLastWeight.GetValueOrDefault());
            return true;
          }

          protected virtual DateTime GetServerTime()
          {
            DateTime dateTime1;
            DateTime dateTime2;
            PXDatabase.SelectDate(ref dateTime1, ref dateTime2);
            return PXTimeZoneInfo.ConvertTimeFromUtc(dateTime2, LocaleInfo.GetTimeZone());
          }

          [PXOverride]
          public ScanCommand<PickPackShip> DecorateScanCommand(
            ScanCommand<PickPackShip> original,
            Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
          {
            ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
            if (!(scanCommand is PickPackShip.PackMode.ConfirmPackageCommand confirmPackageCommand))
              return scanCommand;
            ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfPredicate) ((ScanCommand<PickPackShip>) confirmPackageCommand).Intercept.IsEnabled).ByConjoin((Func<PickPackShip, bool>) (basis => !(basis.CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState)), false, new RelativeInject?());
            return scanCommand;
          }
        }

        public class AlterComplete : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>
        {
          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.ApplyChanges(PX.Objects.SO.SOPackageDetailEx)" />
          [PXOverride]
          public void ApplyChanges(
            SOPackageDetailEx package,
            Action<SOPackageDetailEx> base_ApplyChanges)
          {
            base_ApplyChanges(package);
            PickPackShip.PackMode.BoxConfirming.WeightState.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.BoxConfirming.WeightState.Logic>();
            if (!EnumerableExtensions.IsNotIn<Decimal?>(logic.Weight, new Decimal?(), new Decimal?(0M)))
              return;
            package.Weight = new Decimal?(Math.Round(logic.Weight.Value, 4));
          }

          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.TryForwardProcessing" />
          [PXOverride]
          public bool TryForwardProcessing(Func<bool> base_TryForwardProcessing)
          {
            return (!(((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState) || this.TryForward()) && base_TryForwardProcessing() && (!(((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentState is PickPackShip.PackMode.BoxConfirming.WeightState) || this.TryForward());
          }

          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.ClearStates" />
          [PXOverride]
          public void ClearStates(Action base_ClearStates)
          {
            base_ClearStates();
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Clear<PickPackShip.PackMode.BoxConfirming.WeightState>(true);
          }

          protected virtual bool TryForward()
          {
            PickPackShip.PackMode.BoxConfirming.WeightState.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.BoxConfirming.WeightState.Logic>();
            if (!logic.TryUsePreparedWeightFor(logic.Target.SelectedPackage))
              return false;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).DispatchNext((string) null, Array.Empty<object>());
            return true;
          }
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string Prompt = "Enter the actual total weight of the package.";
          public const string BadFormat = "The quantity format does not fit the locale settings.";
          public const string Success = "Once the package is confirmed, it will have the following weight: {0} {1}.";
          public const string CalculatedWeight = "The {0} package is ready to be confirmed. The calculated weight is {1} {2}.";
          public const string NoSkip = "The package does not have a predefined weight.";
          public const string ScaleMissing = "{0} scale not found.";
          public const string ScaleDisconnected = "The system could not retrieve the weighing result from the {0} scale. Check if the scale is connected to the working station with DeviceHub.";
          public const string ScaleTimeout = "The weighing result on the {0} scale is more than {1} seconds old. Remove the package from the scale and weigh it again.";
          public const string ScaleNoBox = "The system could not retrieve the weighing result from the {0} scale. Ensure that items are placed on the scale.";
        }
      }

      public sealed class DimensionsState : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<(Decimal L, Decimal W, Decimal H)?>
      {
        public const string Value = "BDIM";

        public PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic This
        {
          get => this.Get<PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic>();
        }

        public PickPackShip.PackMode.Logic Mode => this.Get<PickPackShip.PackMode.Logic>();

        public virtual string Code => "BDIM";

        protected virtual string StatePrompt
        {
          get
          {
            return "Enter the actual length, width, and height of the package. Use a space as a separator.";
          }
        }

        protected virtual bool IsStateSkippable()
        {
          bool? packageDimensions = ((PXSelectBase<SOPickPackShipSetup>) ((ScanComponent<PickPackShip>) this).Basis.Setup).Current.ConfirmEachPackageDimensions;
          bool flag = false;
          if (packageDimensions.GetValueOrDefault() == flag & packageDimensions.HasValue || this.Mode.SelectedPackage == null)
            return true;
          PX.Objects.CS.CSBox csBox = PX.Objects.CS.CSBox.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), this.Mode.SelectedPackage.BoxID);
          return csBox == null || !csBox.AllowOverrideDimension.GetValueOrDefault();
        }

        protected virtual void OnTakingOver()
        {
          if (!this.This.TryPrepareDimensionsAndSkipInputFor(this.Mode.SelectedPackage))
            return;
          ((ScanState<PickPackShip>) this).MoveToNextState();
        }

        protected virtual void OnDismissing()
        {
          ((ScanComponent<PickPackShip>) this).Basis.RevokeQuestion<PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion>();
        }

        protected virtual (Decimal L, Decimal W, Decimal H)? GetByBarcode(string barcode)
        {
          string[] strArray = barcode.Trim().Split(' ');
          if (strArray.Length < 3)
            return new (Decimal, Decimal, Decimal)?();
          string str1;
          string str2;
          string str3;
          ArrayDeconstruct.Deconstruct<string>(strArray, ref str1, ref str2, ref str3);
          string s1 = str1;
          string s2 = str2;
          string s3 = str3;
          Decimal num;
          ref Decimal local = ref num;
          Decimal result1;
          Decimal result2;
          return Decimal.TryParse(s1, out local) && Decimal.TryParse(s2, out result1) && Decimal.TryParse(s3, out result2) ? new (Decimal, Decimal, Decimal)?((num, result1, result2)) : new (Decimal, Decimal, Decimal)?();
        }

        protected virtual void ReportMissing(string barcode)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportError("The format of the entered string is incorrect. The string should contain three numeric dimensions separated by a space. Example: 31.2 20 13.5", Array.Empty<object>());
        }

        protected virtual Validation Validate((Decimal L, Decimal W, Decimal H)? value)
        {
          Validation validation;
          if (((ScanComponent<PickPackShip>) this).Basis.HasFault<(Decimal, Decimal, Decimal)?>(value, new Func<(Decimal, Decimal, Decimal)?, Validation>(((EntityState<PickPackShip, (Decimal, Decimal, Decimal)?>) this).Validate), ref validation))
            return validation;
          SOPackageDetailEx selectedPackage = this.Mode.SelectedPackage;
          string str1;
          if (!((ScanComponent<PickPackShip>) this).Basis.IsValid<SOPackageDetail.length, SOPackageDetailEx>(selectedPackage, (object) value.Value.L, ref str1))
            return Validation.Fail(str1, Array.Empty<object>());
          string str2;
          if (!((ScanComponent<PickPackShip>) this).Basis.IsValid<SOPackageDetail.width, SOPackageDetailEx>(selectedPackage, (object) value.Value.W, ref str2))
            return Validation.Fail(str2, Array.Empty<object>());
          string str3;
          return !((ScanComponent<PickPackShip>) this).Basis.IsValid<SOPackageDetail.height, SOPackageDetailEx>(selectedPackage, (object) value.Value.H, ref str3) ? Validation.Fail(str3, Array.Empty<object>()) : Validation.Ok;
        }

        protected virtual void Apply((Decimal L, Decimal W, Decimal H)? value)
        {
          this.This.Dimensions = new (Decimal, Decimal, Decimal)?(value.Value);
        }

        protected virtual void ClearState()
        {
          this.This.Dimensions = new (Decimal, Decimal, Decimal)?();
        }

        protected virtual void ReportSuccess((Decimal L, Decimal W, Decimal H)? value)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("Once the package is confirmed, it will have the following dimensions: {0} x {1} x {2} {3}.", new object[4]
          {
            (object) value.Value.L,
            (object) value.Value.W,
            (object) value.Value.H,
            (object) this.Mode.SelectedPackage.LinearUOM
          });
        }

        public class value : 
          BqlType<
          #nullable enable
          IBqlString, string>.Constant<
          #nullable disable
          PickPackShip.PackMode.BoxConfirming.DimensionsState.value>
        {
          public value()
            : base("BDIM")
          {
          }
        }

        public sealed class SkipQuestion : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanQuestion
        {
          public PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic State
          {
            get => this.Get<PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic>();
          }

          public virtual string Code => "SKIPDIMENSIONS";

          protected virtual string GetPrompt() => "To use the default dimensions, click OK.";

          protected virtual void Confirm()
          {
            if (!this.State.TryUsePreparedDimensionsFor(this.State.Target.SelectedPackage, true) || !(((ScanComponent<PickPackShip>) this).Basis.CurrentState is PickPackShip.PackMode.BoxConfirming.DimensionsState))
              return;
            ((ScanComponent<PickPackShip>) this).Basis.DispatchNext((string) null, Array.Empty<object>());
          }

          protected virtual void Reject()
          {
          }

          [PXLocalizable]
          public abstract class Msg
          {
            public const string Prompt = "To use the default dimensions, click OK.";
          }
        }

        public class Logic : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.Logic>
        {
          public (Decimal L, Decimal W, Decimal H)? Dimensions
          {
            get
            {
              if (EnumerableExtensions.IsIn<Decimal?>(new Decimal?(), this.Target.PackHeader.Length, this.Target.PackHeader.Width, this.Target.PackHeader.Height))
                return new (Decimal, Decimal, Decimal)?();
              Decimal? nullable = this.Target.PackHeader.Length;
              Decimal num1 = nullable.Value;
              nullable = this.Target.PackHeader.Width;
              Decimal num2 = nullable.Value;
              Decimal num3 = this.Target.PackHeader.Height.Value;
              return new (Decimal, Decimal, Decimal)?((num1, num2, num3));
            }
            set
            {
              if (!value.HasValue)
              {
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter1 = this.Target.PackSetter;
                (^ref packSetter1).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Length), new Decimal?());
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter2 = this.Target.PackSetter;
                (^ref packSetter2).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Width), new Decimal?());
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter3 = this.Target.PackSetter;
                (^ref packSetter3).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Height), new Decimal?());
              }
              else
              {
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter4 = this.Target.PackSetter;
                (^ref packSetter4).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Length), new Decimal?(value.Value.L));
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter5 = this.Target.PackSetter;
                (^ref packSetter5).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Width), new Decimal?(value.Value.W));
                ValueSetter<ScanHeader>.Ext<PackScanHeader> packSetter6 = this.Target.PackSetter;
                (^ref packSetter6).Set<Decimal?>((Expression<Func<PackScanHeader, Decimal?>>) (h => h.Height), new Decimal?(value.Value.H));
              }
            }
          }

          public virtual bool TryUsePreparedDimensionsFor(
            SOPackageDetailEx package,
            bool explicitConfirmation = false)
          {
            if (!explicitConfirmation && ((PXSelectBase<SOPickPackShipSetup>) ((PickPackShip) this.Basis).Setup).Current.ConfirmEachPackageDimensions.GetValueOrDefault())
              return false;
            if (!this.CanSkipInputFor(package))
            {
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError("The package does not have predefined dimensions.", Array.Empty<object>());
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion>();
              return false;
            }
            Validation validation;
            if (!this.Dimensions.HasValue || !((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).HasFault<(Decimal, Decimal, Decimal)?>(this.Dimensions, (Func<(Decimal, Decimal, Decimal)?, Validation?>) (dims => ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).TryValidate<(Decimal, Decimal, Decimal)?>(dims).By<PickPackShip.PackMode.BoxConfirming.DimensionsState>()), ref validation))
              return true;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportError(((Validation) ref validation).Message, ((Validation) ref validation).MessageArgs);
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).RevokeQuestion<PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion>();
            return false;
          }

          public virtual bool TryPrepareDimensionsAndSkipInputFor(SOPackageDetailEx package)
          {
            Decimal? nullable = package.Length;
            Decimal valueOrDefault1 = nullable.GetValueOrDefault();
            nullable = package.Width;
            Decimal valueOrDefault2 = nullable.GetValueOrDefault();
            nullable = package.Height;
            Decimal valueOrDefault3 = nullable.GetValueOrDefault();
            this.Dimensions = new (Decimal, Decimal, Decimal)?((valueOrDefault1, valueOrDefault2, valueOrDefault3));
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo("The {0} package is ready to be confirmed. It has the following default dimensions: {1} x {2} x {3} {4}.", new object[5]
            {
              (object) package.BoxID,
              (object) this.Dimensions.Value.L,
              (object) this.Dimensions.Value.W,
              (object) this.Dimensions.Value.H,
              (object) package.LinearUOM
            });
            if (!((PXSelectBase<SOPickPackShipSetup>) ((PickPackShip) this.Basis).Setup).Current.ConfirmEachPackageDimensions.GetValueOrDefault())
              return this.CanSkipInputFor(package);
            if (this.CanSkipInputFor(package))
              this.AskToSkipFor(package);
            return false;
          }

          protected virtual bool CanSkipInputFor(SOPackageDetailEx package)
          {
            if (EnumerableExtensions.IsNotIn<(Decimal, Decimal, Decimal)?>(this.Dimensions, new (Decimal, Decimal, Decimal)?(), new (Decimal, Decimal, Decimal)?((0M, 0M, 0M))))
              return true;
            return EnumerableExtensions.IsNotIn<Decimal?>(package.Length, new Decimal?(), new Decimal?(0M)) && EnumerableExtensions.IsNotIn<Decimal?>(package.Width, new Decimal?(), new Decimal?(0M)) && EnumerableExtensions.IsNotIn<Decimal?>(package.Height, new Decimal?(), new Decimal?(0M));
          }

          protected virtual void AskToSkipFor(SOPackageDetailEx package)
          {
            Validation validation;
            if (((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).HasFault<(Decimal, Decimal, Decimal)?>(this.Dimensions, (Func<(Decimal, Decimal, Decimal)?, Validation?>) (dims => ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).TryValidate<(Decimal, Decimal, Decimal)?>(dims).By<PickPackShip.PackMode.BoxConfirming.DimensionsState>()), ref validation))
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Warn<PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion>("The {0} package is ready to be confirmed. It has the following default dimensions: {1} x {2} x {3} {4}.", new object[5]
              {
                (object) package.BoxID,
                (object) this.Dimensions.Value.L,
                (object) this.Dimensions.Value.W,
                (object) this.Dimensions.Value.H,
                (object) package.LinearUOM
              });
            else
              ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Ask<PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion>("The {0} package is ready to be confirmed. It has the following default dimensions: {1} x {2} x {3} {4}.", new object[5]
              {
                (object) package.BoxID,
                (object) this.Dimensions.Value.L,
                (object) this.Dimensions.Value.W,
                (object) this.Dimensions.Value.H,
                (object) package.LinearUOM
              });
          }

          [PXOverride]
          public ScanCommand<PickPackShip> DecorateScanCommand(
            ScanCommand<PickPackShip> original,
            Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
          {
            ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
            if (!(scanCommand is PickPackShip.PackMode.ConfirmPackageCommand confirmPackageCommand))
              return scanCommand;
            ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfPredicate) ((ScanCommand<PickPackShip>) confirmPackageCommand).Intercept.IsEnabled).ByConjoin((Func<PickPackShip, bool>) (basis => !(basis.CurrentState is PickPackShip.PackMode.BoxConfirming.DimensionsState)), false, new RelativeInject?());
            return scanCommand;
          }
        }

        [PXUIField(DisplayName = "Dimensions (L x W x H)")]
        public class PackageDimensionsCombined : 
          PXFieldAttachedTo<SOPackageDetailEx>.By<PickPackShip.Host>.AsString.Named<PickPackShip.PackMode.BoxConfirming.DimensionsState.PackageDimensionsCombined>
        {
          public override string GetValue(SOPackageDetailEx Row)
          {
            return $"{Row.Length} x {Row.Width} x {Row.Height} {Row.LinearUOM}";
          }
        }

        public class AlterComplete : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>
        {
          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.ApplyChanges(PX.Objects.SO.SOPackageDetailEx)" />
          [PXOverride]
          public void ApplyChanges(
            SOPackageDetailEx package,
            Action<SOPackageDetailEx> base_ApplyChanges)
          {
            base_ApplyChanges(package);
            PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic>();
            if (!EnumerableExtensions.IsNotIn<(Decimal, Decimal, Decimal)?>(logic.Dimensions, new (Decimal, Decimal, Decimal)?(), new (Decimal, Decimal, Decimal)?((0M, 0M, 0M))))
              return;
            package.Length = new Decimal?(Math.Round(logic.Dimensions.Value.L, 4));
            package.Width = new Decimal?(Math.Round(logic.Dimensions.Value.W, 4));
            package.Height = new Decimal?(Math.Round(logic.Dimensions.Value.H, 4));
          }

          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.TryForwardProcessing" />
          [PXOverride]
          public bool TryForwardProcessing(Func<bool> base_TryForwardProcessing)
          {
            return (!(((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentState is PickPackShip.PackMode.BoxConfirming.DimensionsState) || this.TryForward()) && base_TryForwardProcessing() && (!(((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentState is PickPackShip.PackMode.BoxConfirming.DimensionsState) || this.TryForward());
          }

          /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.BoxConfirming.CompleteState.Logic.ClearStates" />
          [PXOverride]
          public void ClearStates(Action base_ClearStates)
          {
            base_ClearStates();
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Clear<PickPackShip.PackMode.BoxConfirming.DimensionsState>(true);
          }

          protected virtual bool TryForward()
          {
            PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.BoxConfirming.DimensionsState.Logic>();
            if (!logic.TryUsePreparedDimensionsFor(logic.Target.SelectedPackage))
              return false;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).DispatchNext((string) null, Array.Empty<object>());
            return true;
          }
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string Prompt = "Enter the actual length, width, and height of the package. Use a space as a separator.";
          public const string BadFormat = "The format of the entered string is incorrect. The string should contain three numeric dimensions separated by a space. Example: 31.2 20 13.5";
          public const string Success = "Once the package is confirmed, it will have the following dimensions: {0} x {1} x {2} {3}.";
          public const string NoSkip = "The package does not have predefined dimensions.";
          public const string CalculatedDimensions = "The {0} package is ready to be confirmed. It has the following default dimensions: {1} x {2} x {3} {4}.";
          public const string PackageDimensionsCombined = "Dimensions (L x W x H)";
        }
      }

      public sealed class CompleteState : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.MediatorState
      {
        public const string Value = "BCC";

        public virtual string Code => "BCC";

        protected virtual void Apply()
        {
          this.Get<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>().Call<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>((Action<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>) (state => state.SettleAndConfirmPackage(state.Target.SelectedPackage)));
        }

        protected virtual void SetNextState()
        {
          ((ScanComponent<PickPackShip>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
        }

        public class value : 
          BqlType<
          #nullable enable
          IBqlString, string>.Constant<
          #nullable disable
          PickPackShip.PackMode.BoxConfirming.CompleteState.value>
        {
          public value()
            : base("BCC")
          {
          }
        }

        public class Logic : 
          BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.Logic>
        {
          public virtual bool TryAutoConfirm()
          {
            if (!((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).HasActive<PickPackShip.PackMode.BoxConfirming.StartState>())
              return true;
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).SetScanState<PickPackShip.PackMode.BoxConfirming.StartState>((string) null, Array.Empty<object>());
            if (this.Target.SelectedPackage.Confirmed.GetValueOrDefault())
              return true;
            if (!this.TryForwardProcessing())
              return false;
            this.SettleAndConfirmPackage(this.Target.SelectedPackage);
            return true;
          }

          public virtual void SettleAndConfirmPackage(SOPackageDetailEx package)
          {
            this.ApplyChanges(package);
            package.Confirmed = new bool?(true);
            ((PXSelectBase<SOPackageDetailEx>) this.Graph.Packages).Update(package);
            ((PXAction) ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Save).Press();
            this.ClearStates();
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Reset(false);
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo("Package confirmed.", Array.Empty<object>());
          }

          protected virtual bool TryForwardProcessing() => true;

          protected virtual void ApplyChanges(SOPackageDetailEx package)
          {
          }

          protected virtual void ClearStates()
          {
            ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Clear<PickPackShip.PackMode.BoxState>(true);
          }
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string Success = "Package confirmed.";
        }
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<PickPackShip>) this).Basis.Localize("Confirm packing {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<PickPackShip>) this).Basis.Qty,
            (object) ((ScanComponent<PickPackShip>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<PickPackShip.PackMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        protected PickPackShip.PackMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<PickPackShip.PackMode.Logic>();
        }

        public virtual FlowStatus Confirm()
        {
          FlowStatus flowStatus = FlowStatus.Fail(this.Basis.Remove.GetValueOrDefault() ? "No items to remove from shipment." : "No items to pack.", Array.Empty<object>());
          if (!this.Mode.PackageLineNbr.HasValue)
            return flowStatus;
          SOPackageDetailEx packageDetail = this.Mode.SelectedPackage;
          if (this.Basis.InventoryID.HasValue)
          {
            Decimal? qty1 = this.Basis.Qty;
            Decimal num1 = 0M;
            if (!(qty1.GetValueOrDefault() == num1 & qty1.HasValue))
            {
              IEnumerable<PX.Objects.SO.SOShipLineSplit> splitsToPack = this.GetSplitsToPack();
              if (!splitsToPack.Any<PX.Objects.SO.SOShipLineSplit>())
              {
                FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
                return ((FlowStatus) ref withModeReset).WithPostAction(new Action(KeepPackageSelection));
              }
              Decimal baseQty = this.Basis.BaseQty;
              string str = this.Basis.SightOf<WMSScanHeader.inventoryID>();
              bool? remove = this.Basis.Remove;
              Decimal? nullable1;
              int num2;
              if (!remove.GetValueOrDefault())
              {
                nullable1 = splitsToPack.Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (s =>
                {
                  Decimal? nullable2 = this.TargetQty(s);
                  Decimal? packedQty = s.PackedQty;
                  return !(nullable2.HasValue & packedQty.HasValue) ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() - packedQty.GetValueOrDefault());
                }));
                Decimal num3 = baseQty;
                num2 = nullable1.GetValueOrDefault() < num3 & nullable1.HasValue ? 1 : 0;
              }
              else
              {
                Decimal? nullable3 = splitsToPack.Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (s => s.PackedQty));
                Decimal num4 = baseQty;
                Decimal? nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num4) : new Decimal?();
                Decimal num5 = 0M;
                num2 = nullable4.GetValueOrDefault() < num5 & nullable4.HasValue ? 1 : 0;
              }
              if (num2 != 0)
              {
                remove = this.Basis.Remove;
                return FlowStatus.Fail(remove.GetValueOrDefault() ? "The packed quantity cannot be negative." : "The packed quantity cannot be greater than the quantity in the shipment lines with this item.", new object[3]
                {
                  (object) str,
                  (object) this.Basis.Qty,
                  (object) this.Basis.UOM
                });
              }
              remove = this.Basis.Remove;
              Decimal val2 = Sign.op_Multiply(Sign.MinusIf(remove.GetValueOrDefault()), baseQty);
              foreach (PX.Objects.SO.SOShipLineSplit split in splitsToPack)
              {
                remove = this.Basis.Remove;
                Decimal num6;
                if (!remove.GetValueOrDefault())
                {
                  nullable1 = this.TargetQty(split);
                  Decimal num7 = nullable1.Value;
                  nullable1 = split.PackedQty;
                  Decimal num8 = nullable1.Value;
                  num6 = Math.Min(num7 - num8, val2);
                }
                else
                {
                  nullable1 = split.PackedQty;
                  num6 = -Math.Min(nullable1.Value, -val2);
                }
                Decimal qty2 = num6;
                if (!this.PackSplit(split, packageDetail, qty2))
                {
                  remove = this.Basis.Remove;
                  return FlowStatus.Fail(remove.GetValueOrDefault() ? "The packed quantity cannot be negative." : "The packed quantity cannot be greater than the quantity in the shipment lines with this item.", new object[3]
                  {
                    (object) str,
                    (object) this.Basis.Qty,
                    (object) this.Basis.UOM
                  });
                }
                val2 -= qty2;
                if (val2 == 0M)
                  break;
              }
              if (this.Mode.IsPackageEmpty(packageDetail))
              {
                ((PXSelectBase<SOPackageDetailEx>) this.Basis.Graph.Packages).Delete(packageDetail);
                this.Basis.Clear<PickPackShip.PackMode.BoxState>(true);
                this.Mode.PackageLineNbrUI = new int?();
              }
              else
                this.Mode.PackageLineNbrUI = this.Mode.PackageLineNbr;
              this.EnsureShipmentUserLinkForPack();
              this.Basis.ReportInfo(this.Basis.Remove.GetValueOrDefault() ? "{0} x {1} {2} removed from shipment." : "{0} x {1} {2} added to shipment.", new object[3]
              {
                (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
                (object) this.Basis.Qty,
                (object) this.Basis.UOM
              });
              return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
            }
          }
          return flowStatus;

          void KeepPackageSelection() => this.Mode.PackageLineNbr = packageDetail.LineNbr;
        }

        public virtual Decimal? TargetQty(PX.Objects.SO.SOShipLineSplit split)
        {
          if (this.Basis.HasPick)
            return split.PickedQty;
          Decimal? qty = split.Qty;
          Decimal qtyThreshold = this.Basis.Graph.GetQtyThreshold(split);
          return !qty.HasValue ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() * qtyThreshold);
        }

        protected virtual bool IsSelectedSplit(PX.Objects.SO.SOShipLineSplit split)
        {
          int? inventoryId1 = split.InventoryID;
          int? inventoryId2 = this.Basis.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = split.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
            {
              int? siteId1 = split.SiteID;
              int? siteId2 = this.Basis.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              {
                int? locationId = split.LocationID;
                int? nullable = this.Basis.LocationID ?? split.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
                  return string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
              }
            }
          }
          return false;
        }

        public virtual IEnumerable<PX.Objects.SO.SOShipLineSplit> GetSplitsToPack()
        {
          bool locationIsRequired = this.Basis.HasActive<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>();
          return (IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = r.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
              {
                bool? nullable1 = r.IsUnassigned;
                if (!nullable1.GetValueOrDefault())
                {
                  nullable1 = r.HasGeneratedLotSerialNbr;
                  if (!nullable1.GetValueOrDefault() && !string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                    goto label_8;
                }
                int num1 = locationIsRequired ? 1 : 0;
                int? locationId = r.LocationID;
                int? nullable2 = this.Basis.LocationID ?? r.LocationID;
                int num2 = locationId.GetValueOrDefault() == nullable2.GetValueOrDefault() & locationId.HasValue == nullable2.HasValue ? 1 : 0;
                if ((num1 != 0).Implies(num2 != 0))
                {
                  nullable1 = this.Basis.Remove;
                  if (!nullable1.GetValueOrDefault())
                  {
                    Decimal? nullable3 = this.TargetQty(r);
                    Decimal? packedQty = r.PackedQty;
                    return nullable3.GetValueOrDefault() > packedQty.GetValueOrDefault() & nullable3.HasValue & packedQty.HasValue;
                  }
                  Decimal? packedQty1 = r.PackedQty;
                  Decimal num3 = 0M;
                  return packedQty1.GetValueOrDefault() > num3 & packedQty1.HasValue;
                }
              }
            }
label_8:
            return false;
          })).With<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(new Func<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(this.PrioritizeSplits));
        }

        public virtual IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit> PrioritizeSplits(
          IEnumerable<PX.Objects.SO.SOShipLineSplit> splits)
        {
          if (!this.Basis.HasPick)
          {
            PX.Objects.SO.SOShipment shipment = this.Basis.Shipment;
            int num1;
            if (shipment == null)
            {
              num1 = 0;
            }
            else
            {
              bool? pickedViaWorksheet = shipment.PickedViaWorksheet;
              bool flag = false;
              num1 = pickedViaWorksheet.GetValueOrDefault() == flag & pickedViaWorksheet.HasValue ? 1 : 0;
            }
            if (num1 != 0)
              return splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
              {
                bool? isUnassigned = split.IsUnassigned;
                bool flag1 = false;
                if (!(isUnassigned.GetValueOrDefault() == flag1 & isUnassigned.HasValue))
                  return false;
                bool? generatedLotSerialNbr = split.HasGeneratedLotSerialNbr;
                bool flag2 = false;
                return generatedLotSerialNbr.GetValueOrDefault() == flag2 & generatedLotSerialNbr.HasValue;
              })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
              {
                if (!this.Basis.Remove.GetValueOrDefault())
                {
                  Decimal? qty = split.Qty;
                  Decimal? pickedQty = split.PickedQty;
                  return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
                }
                Decimal? pickedQty1 = split.PickedQty;
                Decimal num2 = 0M;
                return pickedQty1.GetValueOrDefault() > num2 & pickedQty1.HasValue;
              })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.IsNullOrEmpty(split.LotSerialNbr))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
              {
                Decimal? qty = split.Qty;
                Decimal? pickedQty2 = split.PickedQty;
                if (!(qty.GetValueOrDefault() > pickedQty2.GetValueOrDefault() & qty.HasValue & pickedQty2.HasValue) && !this.Basis.Remove.GetValueOrDefault())
                  return false;
                Decimal? pickedQty3 = split.PickedQty;
                Decimal num3 = 0M;
                return pickedQty3.GetValueOrDefault() > num3 & pickedQty3.HasValue;
              })).ThenByDescending<PX.Objects.SO.SOShipLineSplit, Decimal?>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (split =>
              {
                Sign sign = Sign.MinusIf(this.Basis.Remove.GetValueOrDefault());
                Decimal? qty = split.Qty;
                Decimal? pickedQty = split.PickedQty;
                Decimal? nullable = qty.HasValue & pickedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault()) : new Decimal?();
                return !nullable.HasValue ? new Decimal?() : new Decimal?(Sign.op_Multiply(sign, nullable.GetValueOrDefault()));
              }));
          }
          return splits.OrderBy<PX.Objects.SO.SOShipLineSplit, int>((Func<PX.Objects.SO.SOShipLineSplit, int>) (split => 0));
        }

        public virtual bool PackSplit(
          PX.Objects.SO.SOShipLineSplit split,
          SOPackageDetailEx package,
          Decimal qty)
        {
          bool? nullable1;
          Decimal? nullable2;
          if (this.Basis.HasPick)
          {
            this.Basis.EnsureAssignedSplitEditing(split);
          }
          else
          {
            nullable1 = split.IsUnassigned;
            if (nullable1.GetValueOrDefault())
            {
              PX.Objects.SO.SOShipLineSplit soShipLineSplit1 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).SelectMain(Array.Empty<object>())).FirstOrDefault<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
              {
                int? lineNbr1 = s.LineNbr;
                int? lineNbr2 = split.LineNbr;
                if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
                {
                  bool? isUnassigned = s.IsUnassigned;
                  bool flag = false;
                  if (isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                    return this.IsSelectedSplit(s);
                }
                return false;
              }));
              if (soShipLineSplit1 == null)
              {
                PX.Objects.SO.SOShipLineSplit copy = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(split);
                copy.SplitLineNbr = new int?();
                copy.LotSerialNbr = this.Basis.LotSerialNbr;
                copy.ExpireDate = this.Basis.ExpireDate;
                copy.Qty = new Decimal?(qty);
                copy.PickedQty = new Decimal?(qty);
                copy.PackedQty = new Decimal?(0M);
                copy.IsUnassigned = new bool?(false);
                copy.PlanID = new long?();
                split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Insert(copy);
              }
              else
              {
                PX.Objects.SO.SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
                Decimal? qty1 = soShipLineSplit2.Qty;
                Decimal num = qty;
                soShipLineSplit2.Qty = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + num) : new Decimal?();
                soShipLineSplit1.ExpireDate = this.Basis.ExpireDate;
                split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(soShipLineSplit1);
              }
            }
            else
            {
              nullable1 = split.HasGeneratedLotSerialNbr;
              if (nullable1.GetValueOrDefault())
              {
                PX.Objects.SO.SOShipLineSplit copy1 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(split);
                Decimal? qty2 = copy1.Qty;
                Decimal num1 = qty;
                if (qty2.GetValueOrDefault() == num1 & qty2.HasValue)
                {
                  ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Delete(copy1);
                }
                else
                {
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit3 = copy1;
                  Decimal? qty3 = soShipLineSplit3.Qty;
                  Decimal num2 = qty;
                  soShipLineSplit3.Qty = qty3.HasValue ? new Decimal?(qty3.GetValueOrDefault() - num2) : new Decimal?();
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit4 = copy1;
                  nullable2 = soShipLineSplit4.PickedQty;
                  Decimal num3 = Math.Min(qty, copy1.PickedQty.Value);
                  soShipLineSplit4.PickedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num3) : new Decimal?();
                  ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(copy1);
                }
                PX.Objects.SO.SOShipLineSplit soShipLineSplit5 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).SelectMain(Array.Empty<object>())).FirstOrDefault<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
                {
                  int? lineNbr3 = s.LineNbr;
                  int? lineNbr4 = split.LineNbr;
                  if (lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue)
                  {
                    bool? generatedLotSerialNbr = s.HasGeneratedLotSerialNbr;
                    bool flag = false;
                    if (generatedLotSerialNbr.GetValueOrDefault() == flag & generatedLotSerialNbr.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                      return this.IsSelectedSplit(s);
                  }
                  return false;
                }));
                if (soShipLineSplit5 == null)
                {
                  PX.Objects.SO.SOShipLineSplit copy2 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(split);
                  copy2.SplitLineNbr = new int?();
                  copy2.LotSerialNbr = this.Basis.LotSerialNbr;
                  if (this.Basis.ExpireDate.HasValue)
                    copy2.ExpireDate = this.Basis.ExpireDate;
                  copy2.Qty = new Decimal?(qty);
                  copy2.PickedQty = new Decimal?(qty);
                  copy2.PackedQty = new Decimal?(0M);
                  copy2.PlanID = new long?();
                  split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Insert(copy2);
                  split.HasGeneratedLotSerialNbr = new bool?(false);
                  split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(split);
                }
                else
                {
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit6 = soShipLineSplit5;
                  nullable2 = soShipLineSplit6.Qty;
                  Decimal num4 = qty;
                  soShipLineSplit6.Qty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit7 = soShipLineSplit5;
                  nullable2 = soShipLineSplit7.PickedQty;
                  Decimal num5 = qty;
                  soShipLineSplit7.PickedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num5) : new Decimal?();
                  if (this.Basis.ExpireDate.HasValue)
                    soShipLineSplit5.ExpireDate = this.Basis.ExpireDate;
                  split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(soShipLineSplit5);
                }
              }
            }
          }
          SOShipLineSplitPackage lineSplitPackage1 = ((IEnumerable<SOShipLineSplitPackage>) ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).SelectMain(new object[2]
          {
            (object) package.ShipmentNbr,
            (object) package.LineNbr
          })).FirstOrDefault<SOShipLineSplitPackage>((Func<SOShipLineSplitPackage, bool>) (t =>
          {
            if (t.ShipmentNbr == split.ShipmentNbr)
            {
              int? shipmentLineNbr = t.ShipmentLineNbr;
              int? nullable3 = split.LineNbr;
              if (shipmentLineNbr.GetValueOrDefault() == nullable3.GetValueOrDefault() & shipmentLineNbr.HasValue == nullable3.HasValue)
              {
                nullable3 = t.ShipmentSplitLineNbr;
                int? splitLineNbr = split.SplitLineNbr;
                return nullable3.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & nullable3.HasValue == splitLineNbr.HasValue;
              }
            }
            return false;
          }));
          if (qty < 0M)
          {
            if (lineSplitPackage1 != null)
            {
              Decimal? packedQty1 = lineSplitPackage1.PackedQty;
              Decimal num6 = qty;
              nullable2 = packedQty1.HasValue ? new Decimal?(packedQty1.GetValueOrDefault() + num6) : new Decimal?();
              Decimal num7 = 0M;
              if (!(nullable2.GetValueOrDefault() < num7 & nullable2.HasValue))
              {
                if (!this.Basis.HasPick && this.Basis.LotSerialTrack.IsEnterable)
                {
                  nullable1 = this.Basis.SelectedLotSerialClass.AutoNextNbr;
                  if (nullable1.GetValueOrDefault())
                  {
                    Decimal? packedQty2 = split.PackedQty;
                    Decimal num8 = qty;
                    nullable2 = packedQty2.HasValue ? new Decimal?(packedQty2.GetValueOrDefault() + num8) : new Decimal?();
                    Decimal num9 = 0M;
                    if (nullable2.GetValueOrDefault() == num9 & nullable2.HasValue)
                    {
                      split.HasGeneratedLotSerialNbr = new bool?(true);
                      split = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(split);
                    }
                  }
                  else
                  {
                    PX.Objects.SO.SOShipLineSplit soShipLineSplit = split;
                    nullable2 = soShipLineSplit.Qty;
                    Decimal num10 = qty;
                    soShipLineSplit.Qty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num10) : new Decimal?();
                    nullable2 = split.Qty;
                    Decimal num11 = 0M;
                    split = !(nullable2.GetValueOrDefault() == num11 & nullable2.HasValue) ? ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Update(split) : ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.PickedForPack).Delete(split);
                  }
                }
                Decimal? packedQty3 = lineSplitPackage1.PackedQty;
                Decimal num12 = qty;
                nullable2 = packedQty3.HasValue ? new Decimal?(packedQty3.GetValueOrDefault() + num12) : new Decimal?();
                Decimal num13 = 0M;
                if (nullable2.GetValueOrDefault() > num13 & nullable2.HasValue)
                {
                  SOShipLineSplitPackage lineSplitPackage2 = lineSplitPackage1;
                  nullable2 = lineSplitPackage2.PackedQty;
                  Decimal num14 = qty;
                  lineSplitPackage2.PackedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num14) : new Decimal?();
                  ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Update(lineSplitPackage1);
                }
                else
                {
                  Decimal? packedQty4 = lineSplitPackage1.PackedQty;
                  Decimal num15 = qty;
                  nullable2 = packedQty4.HasValue ? new Decimal?(packedQty4.GetValueOrDefault() + num15) : new Decimal?();
                  Decimal num16 = 0M;
                  if (nullable2.GetValueOrDefault() == num16 & nullable2.HasValue)
                    ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Delete(lineSplitPackage1);
                }
                package.Confirmed = new bool?(false);
                ((PXSelectBase<SOPackageDetailEx>) this.Graph.Packages).Update(package);
                goto label_34;
              }
            }
            return false;
          }
          if (lineSplitPackage1 == null)
          {
            SOShipLineSplitPackage instance = (SOShipLineSplitPackage) ((PXSelectBase) ((PXGraphExtension<PickPackShip.Host>) this).Base.PackageDetailExt.PackageDetailSplit).Cache.CreateInstance();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            PXFieldVerifying pxFieldVerifying = PickPackShip.PackMode.ConfirmState.Logic.\u003C\u003Ec.\u003C\u003E9__10_3 ?? (PickPackShip.PackMode.ConfirmState.Logic.\u003C\u003Ec.\u003C\u003E9__10_3 = new PXFieldVerifying((object) PickPackShip.PackMode.ConfirmState.Logic.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPackSplit\u003Eb__10_3)));
            ((PXGraph) this.Graph).FieldVerifying.AddHandler<SOShipLineSplitPackage.shipmentSplitLineNbr>(pxFieldVerifying);
            instance.ShipmentSplitLineNbr = split.SplitLineNbr;
            instance.PackedQty = new Decimal?(qty);
            SOShipLineSplitPackage lineSplitPackage3 = ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Insert(instance);
            ((PXGraph) this.Graph).FieldVerifying.RemoveHandler<SOShipLineSplitPackage.shipmentSplitLineNbr>(pxFieldVerifying);
            lineSplitPackage3.ShipmentNbr = split.ShipmentNbr;
            lineSplitPackage3.ShipmentLineNbr = split.LineNbr;
            lineSplitPackage3.PackageLineNbr = package.LineNbr;
            lineSplitPackage3.InventoryID = split.InventoryID;
            lineSplitPackage3.SubItemID = split.SubItemID;
            lineSplitPackage3.LotSerialNbr = split.LotSerialNbr;
            lineSplitPackage3.UOM = split.UOM;
            ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Update(lineSplitPackage3);
          }
          else
          {
            SOShipLineSplitPackage lineSplitPackage4 = lineSplitPackage1;
            nullable2 = lineSplitPackage4.PackedQty;
            Decimal num = qty;
            lineSplitPackage4.PackedQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num) : new Decimal?();
            ((PXSelectBase<SOShipLineSplitPackage>) this.Graph.PackageDetailExt.PackageDetailSplit).Update(lineSplitPackage1);
          }
label_34:
          return true;
        }

        public virtual void EnsureShipmentUserLinkForPack()
        {
          this.Graph.WorkLogExt.EnsureFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), this.Basis.HasPick ? "PACK" : "PPCK");
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm packing {0} x {1} {2}.";
        public const string NothingToPack = "No items to pack.";
        public const string NothingToRemove = "No items to remove from shipment.";
        public const string InventoryAdded = "{0} x {1} {2} added to shipment.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from shipment.";
        public const string BoxCanNotPack = "The packed quantity cannot be greater than the quantity in the shipment lines with this item.";
        public const string BoxCanNotUnpack = "The packed quantity cannot be negative.";
      }
    }

    public sealed class ConfirmPackageCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      public PickPackShip.PackMode.Logic Mode => this.Get<PickPackShip.PackMode.Logic>();

      public virtual string Code => "PACKAGE*CONFIRM";

      public virtual string ButtonName => "scanConfirmPackage";

      public virtual string DisplayName => "Confirm Package";

      protected virtual bool IsEnabled => this.Mode.CanConfirmPackage;

      protected virtual bool Process()
      {
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState<PickPackShip.PackMode.BoxConfirming.StartState>((string) null, Array.Empty<object>());
        return true;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Confirm Package";
      }
    }

    public sealed class RemoveCommand : 
      WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand
    {
      protected override bool IsEnabled
      {
        get => base.IsEnabled && this.Get<PickPackShip.PackMode.Logic>().HasConfirmableBoxes;
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<PickPackShip>.SetMode<PickPackShip.PackMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "PACK";

      public virtual string DisplayName => "Pack";

      private string RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>() ? 1 : 0;
          SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && pickPackShipSetup != null && pickPackShipSetup.ShowPackTab.GetValueOrDefault();
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis && basis.RefNbr != null && !basis.DocumentIsConfirmed)
        {
          Validation? nullable = ((ScanMode<PickPackShip>) basis.FindMode<PickPackShip.PackMode>()).TryValidate<PX.Objects.SO.SOShipment>(basis.Shipment).By<PickPackShip.PackMode.ShipmentState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis) || !(basis.CurrentMode.Code != "CRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string Description = "Pack";
      public const string PackedQtyPerBox = "Packed Qty.";
      public const string Completed = "{0} shipment packed.";
      public const string CannotBePacked = "The {0} shipment cannot be processed in Pack mode because it has two or more packages assigned.";
      public const string BoxConfirmPrompt = "Confirm the package.";
      public const string BoxConfirmOrContinuePrompt = "Confirm the package, or scan the next item.";
      public const string BoxConfirmOrContinuePromptNoPick = "Confirm the package, or scan the next item or the next location.";
    }

    [PXUIField(Visible = false)]
    public class ShowPack : 
      PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.PackMode.ShowPack>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(this.Base.WMS.Get<PickPackShip.PackMode.Logic>().ShowPackTab(row));
      }
    }

    [PXUIField(DisplayName = "Packed Qty.")]
    public class PackedQtyPerBox : 
      PXFieldAttachedTo<PX.Objects.SO.SOShipLineSplit>.By<PickPackShip.Host>.AsDecimal.Named<PickPackShip.PackMode.PackedQtyPerBox>
    {
      public override Decimal? GetValue(PX.Objects.SO.SOShipLineSplit row)
      {
        return new Decimal?(((Decimal?) ((IEnumerable<SOShipLineSplitPackage>) ((PXSelectBase<SOShipLineSplitPackage>) this.Base.PackageDetailExt.PackageDetailSplit).SelectMain(new object[2]
        {
          (object) this.Base.WMS.RefNbr,
          (object) this.Base.WMS.Get<PickPackShip.PackMode.Logic>().PackageLineNbrUI
        })).FirstOrDefault<SOShipLineSplitPackage>((Func<SOShipLineSplitPackage, bool>) (t =>
        {
          int? shipmentSplitLineNbr = t.ShipmentSplitLineNbr;
          int? splitLineNbr = row.SplitLineNbr;
          return shipmentSplitLineNbr.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & shipmentSplitLineNbr.HasValue == splitLineNbr.HasValue;
        }))?.PackedQty).GetValueOrDefault());
      }
    }
  }

  public sealed class PickMode : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "PICK";

    public virtual string Code => "PICK";

    public virtual string Description => "Pick";

    protected virtual bool IsModeActive()
    {
      return ((PXSelectBase<SOPickPackShipSetup>) ((ScanMode<PickPackShip>) this).Basis.Setup).Current.ShowPickTab.GetValueOrDefault();
    }

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      PickPackShip.PickMode pickMode = this;
      yield return (ScanState<PickPackShip>) new PickPackShip.PickMode.ShipmentState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN),
        IsForIssue = true,
        IsForTransfer = ((ScanMode<PickPackShip>) pickMode).Basis.IsTransfer,
        SuppressModuleItemStatusCheck = true
      };
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState()
      {
        IsForIssue = true,
        IsForTransfer = ((ScanMode<PickPackShip>) pickMode).Basis.IsTransfer
      };
      yield return (ScanState<PickPackShip>) new PickPackShip.PickMode.ConfirmState();
      yield return (ScanState<PickPackShip>) new PickPackShip.CommandOrShipmentOnlyState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.PickMode.ShipmentState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null)));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.PickMode.ConfirmShipmentPickingCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ConfirmShipmentCommand();
    }

    protected virtual IEnumerable<ScanQuestion<PickPackShip>> CreateQuestions()
    {
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PickMode.ReopenPickingQuestion();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      int num;
      if (fullReset)
      {
        if (((ScanMode<PickPackShip>) this).Basis.IsWithinReset)
        {
          PX.Objects.SO.SOShipment shipment = ((ScanMode<PickPackShip>) this).Basis.Shipment;
          num = shipment != null ? (shipment.Picked.GetValueOrDefault() ? 1 : 0) : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PickMode.ShipmentState>(num != 0);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset || ((ScanMode<PickPackShip>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PickPackShip.PickMode.value>
    {
      public value()
        : base("PICK")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.SOShipLineSplit.FK.ShipmentLine>>>.Order<By<BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.shipmentNbr, IBqlString>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.isUnassigned, IBqlBool>.Desc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.lineNbr, IBqlInt>.Asc>>, 
      #nullable disable
      PX.Objects.SO.SOShipLineSplit>.View Picked;
      public PXAction<ScanHeader> ReviewPick;

      protected virtual IEnumerable picked()
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        pxDelegateResult.IsResultSorted = true;
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) this.Basis.GetSplits(this.Basis.RefNbr, true, (Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal? qty = s.Qty;
          return pickedQty.GetValueOrDefault() >= qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue;
        })));
        return (IEnumerable) pxDelegateResult;
      }

      [PXButton]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewPick(PXAdapter adapter) => adapter.Get();

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        ((PXAction) this.ReviewPick).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile && e.Row?.Mode == "PICK");
      }

      public virtual void InjectExpireDateForPickDeactivationOnAlreadyEnteredLot(
        WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState)
      {
        ((MethodInterceptor<EntityState<PickPackShip, DateTime?>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, DateTime?>) expireDateState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => basis.SelectedLotSerialClass?.LotSerIssueMethod != "U" && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) basis.Get<PickPackShip.PickMode.Logic>().Picked).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (t =>
        {
          if (t.IsUnassigned.GetValueOrDefault())
            return true;
          if (!string.Equals(t.LotSerialNbr, basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            return false;
          Decimal? pickedQty = t.PickedQty;
          Decimal num = 0M;
          return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
        }))), false, new RelativeInject?());
      }

      [PXOverride]
      public ScanState<PickPackShip> DecorateScanState(
        ScanState<PickPackShip> original,
        Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
      {
        ScanState<PickPackShip> scanState = base_DecorateScanState(original);
        if (((ScanComponent<PickPackShip>) scanState).ModeCode == "PICK")
        {
          switch (scanState)
          {
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState:
              this.Basis.InjectLocationDeactivationOnDefaultLocationOption(locationState);
              this.Basis.InjectLocationSkippingOnPromptLocationForEveryLineOption(locationState);
              this.Basis.InjectLocationPresenceValidation(locationState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryItemState:
              this.Basis.InjectItemAbsenceHandlingByLocation(inventoryItemState);
              this.Basis.InjectItemPresenceValidation(inventoryItemState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerialState:
              this.Basis.InjectLotSerialPresenceValidation(lotSerialState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              this.Basis.InjectLotSerialDeactivationOnDefaultLotSerialOption(lotSerialState, true);
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState:
              this.InjectExpireDateForPickDeactivationOnAlreadyEnteredLot(expireDateState);
              break;
          }
        }
        return scanState;

        static PXSelectBase<PX.Objects.SO.SOShipLineSplit> viewSelector(PickPackShip basis)
        {
          return (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) basis.Get<PickPackShip.PickMode.Logic>().Picked;
        }
      }

      /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
      [PXOverride]
      public void OnBeforeFullClear(Action base_OnBeforeFullClear)
      {
        base_OnBeforeFullClear();
        if (!(this.Basis.CurrentMode is PickPackShip.PickMode) || this.Basis.RefNbr == null || !this.Graph.WorkLogExt.SuspendFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK"))
          return;
        this.Graph.WorkLogExt.PersistWorkLog();
      }

      /// Overrides <see cref="P:PX.Objects.SO.WMS.PickPackShip.DocumentIsEditable" />
      [PXOverride]
      public bool get_DocumentIsEditable(Func<bool> base_DocumentIsEditable)
      {
        if (!base_DocumentIsEditable())
          return false;
        int num1 = this.Basis.CurrentMode is PickPackShip.PickMode ? 1 : 0;
        PX.Objects.SO.SOShipment shipment = this.Basis.Shipment;
        int num2 = shipment != null ? (!shipment.Picked.GetValueOrDefault() ? 1 : 0) : 1;
        return (num1 != 0).Implies(num2 != 0);
      }

      public virtual bool ShowPickTab(ScanHeader row) => this.Basis.HasPick && row.Mode == "PICK";

      public virtual bool CanPick
      {
        get
        {
          if (!((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Picked).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? pickedQty = s.PickedQty;
            Decimal? qty = s.Qty;
            return pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue;
          })))
            return false;
          PX.Objects.SO.SOShipment shipment = this.Basis.Shipment;
          return shipment == null || !shipment.Picked.GetValueOrDefault();
        }
      }
    }

    public sealed class ShipmentState : PickPackShip.ShipmentState
    {
      protected virtual Validation Validate(PX.Objects.SO.SOShipment shipment)
      {
        Validation error;
        return shipment.Operation != "I" ? Validation.Fail("The {0} shipment cannot be picked because it has the {1} operation.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.operation>((IBqlTable) shipment)
        }) : (shipment.Status != "N" ? Validation.Fail("The {0} shipment cannot be picked because it has the {1} status.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
        }) : (((ScanComponent<PickPackShip>) this).Basis.HasNonStockLinesWithEmptyLocation(shipment, out error) ? error : Validation.Ok));
      }

      protected override void ReportSuccess(PX.Objects.SO.SOShipment shipment)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment loaded and ready to be picked.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
      }

      protected virtual void SetNextState()
      {
        bool? remove = ((ScanComponent<PickPackShip>) this).Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && !((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.PickMode.Logic>().CanPick)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<PickPackShip>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<PickPackShip>) this).Basis.Localize("{0} shipment picked.", new object[1]
            {
              (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
          PX.Objects.SO.SOShipment shipment = ((ScanComponent<PickPackShip>) this).Basis.Shipment;
          if ((shipment != null ? (shipment.Picked.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return;
          ((ScanComponent<PickPackShip>) this).Basis.Warn<PickPackShip.PickMode.ReopenPickingQuestion>("The {0} shipment has already been picked.", new object[1]
          {
            (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
          });
        }
        else
          ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) this).SetNextState();
      }

      [PXLocalizable]
      public new abstract class Msg : PickPackShip.ShipmentState.Msg
      {
        public new const string Ready = "{0} shipment loaded and ready to be picked.";
        public const string InvalidStatus = "The {0} shipment cannot be picked because it has the {1} status.";
        public const string InvalidOperation = "The {0} shipment cannot be picked because it has the {1} operation.";
        public const string AlreadyPicked = "The {0} shipment has already been picked.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<PickPackShip>) this).Basis.Localize("Confirm picking {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<PickPackShip>) this).Basis.Qty,
            (object) ((ScanComponent<PickPackShip>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<PickPackShip.PickMode.ConfirmState.Logic>().ConfirmPicked();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        protected PickPackShip.PickMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<PickPackShip.PickMode.Logic>();
        }

        public virtual FlowStatus ConfirmPicked()
        {
          Decimal restDeltaQty = Sign.op_Multiply(Sign.MinusIf(this.Basis.Remove.GetValueOrDefault()), this.Basis.BaseQty);
          bool hasSuitableSplits = false;
          if (restDeltaQty == 0M)
            return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          bool? nullable;
          if (!this.Basis.LotSerialTrack.IsTrackedSerial)
          {
            nullable = this.Basis.SelectedInventoryItem.WeightItem;
            if (!nullable.GetValueOrDefault())
            {
              IEnumerable<PX.Objects.SO.SOShipLineSplit> splitsToPick = this.GetSplitsToPick().Select<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit>) (s =>
              {
                hasSuitableSplits = true;
                return s;
              }));
              FlowStatus flowStatus1 = this.PickAllSplits(splitsToPick, ref restDeltaQty, false);
              nullable = ((FlowStatus) ref flowStatus1).IsError;
              bool flag1 = false;
              if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
                return flowStatus1;
              if (restDeltaQty != 0M)
              {
                nullable = this.Basis.Remove;
                bool flag2 = false;
                if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
                {
                  nullable = this.Basis.SelectedInventoryItem.DecimalBaseUnit;
                  if (nullable.GetValueOrDefault())
                  {
                    FlowStatus flowStatus2 = this.PickAllSplits(splitsToPick, ref restDeltaQty, true);
                    nullable = ((FlowStatus) ref flowStatus2).IsError;
                    bool flag3 = false;
                    if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
                      return flowStatus2;
                    goto label_17;
                  }
                  goto label_17;
                }
                goto label_17;
              }
              goto label_17;
            }
          }
          PX.Objects.SO.SOShipLineSplit soShipLineSplit = this.GetSplitsToPick().FirstOrDefault<PX.Objects.SO.SOShipLineSplit>();
          if (soShipLineSplit != null)
          {
            hasSuitableSplits = true;
            nullable = this.Basis.Remove;
            bool flag4 = false;
            Decimal threshold = !(nullable.GetValueOrDefault() == flag4 & nullable.HasValue) || this.Basis.LotSerialTrack.IsTrackedSerial ? 1M : this.Graph.GetQtyThreshold(soShipLineSplit);
            nullable = this.Basis.Remove;
            Decimal num = nullable.GetValueOrDefault() ? -Math.Min(soShipLineSplit.PickedQty.Value, -restDeltaQty) : Math.Min(soShipLineSplit.Qty.Value * threshold - soShipLineSplit.PickedQty.Value, restDeltaQty);
            if (this.Basis.LotSerialTrack.IsTrackedSerial && EnumerableExtensions.IsNotIn<Decimal>(num, 1M, -1M))
              return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
            FlowStatus flowStatus = this.PickSplit(soShipLineSplit, restDeltaQty, threshold);
            nullable = ((FlowStatus) ref flowStatus).IsError;
            bool flag5 = false;
            if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
              return flowStatus;
            restDeltaQty -= num;
          }
label_17:
          if (!hasSuitableSplits)
          {
            nullable = this.Basis.Remove;
            FlowStatus flowStatus = FlowStatus.Fail(nullable.GetValueOrDefault() ? "No items to remove from shipment." : "No items to pick.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          if (Math.Abs(restDeltaQty) > 0M)
          {
            nullable = this.Basis.Remove;
            FlowStatus flowStatus = FlowStatus.Fail(nullable.GetValueOrDefault() ? "The picked quantity cannot be negative." : "The picked quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
            flowStatus = ((FlowStatus) ref flowStatus).WithModeReset;
            return ((FlowStatus) ref flowStatus).WithChangesDiscard;
          }
          this.EnsureShipmentUserLinkForPick();
          PickPackShip basis = this.Basis;
          nullable = this.Basis.Remove;
          string str = nullable.GetValueOrDefault() ? "{0} x {1} {2} removed from shipment." : "{0} x {1} {2} added to shipment.";
          object[] objArray = new object[3]
          {
            (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          };
          basis.ReportInfo(str, objArray);
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        public virtual FlowStatus PickAllSplits(
          IEnumerable<PX.Objects.SO.SOShipLineSplit> splitsToPick,
          ref Decimal restDeltaQty,
          bool withThresholds)
        {
          foreach (PX.Objects.SO.SOShipLineSplit soShipLineSplit in splitsToPick)
          {
            bool? nullable1;
            Decimal num1;
            if (withThresholds)
            {
              nullable1 = this.Basis.Remove;
              bool flag = false;
              if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
              {
                num1 = this.Graph.GetQtyThreshold(soShipLineSplit);
                goto label_6;
              }
            }
            num1 = 1M;
label_6:
            Decimal threshold = num1;
            nullable1 = this.Basis.Remove;
            Decimal? nullable2;
            Decimal num2;
            if (!nullable1.GetValueOrDefault())
            {
              nullable2 = soShipLineSplit.Qty;
              Decimal num3 = nullable2.Value * threshold;
              nullable2 = soShipLineSplit.PickedQty;
              Decimal num4 = nullable2.Value;
              num2 = Math.Min(num3 - num4, restDeltaQty);
            }
            else
            {
              nullable2 = soShipLineSplit.PickedQty;
              num2 = -Math.Min(nullable2.Value, -restDeltaQty);
            }
            Decimal deltaQty = num2;
            FlowStatus flowStatus = this.PickSplit(soShipLineSplit, deltaQty, threshold);
            nullable1 = ((FlowStatus) ref flowStatus).IsError;
            bool flag1 = false;
            if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
              return ((FlowStatus) ref flowStatus).WithChangesDiscard;
            restDeltaQty -= deltaQty;
            if (restDeltaQty == 0M)
              break;
          }
          return FlowStatus.Ok;
        }

        public virtual FlowStatus PickSplit(
          PX.Objects.SO.SOShipLineSplit pickedSplit,
          Decimal deltaQty,
          Decimal threshold)
        {
          bool flag = false;
          Decimal? nullable1;
          Decimal? nullable2;
          Decimal? nullable3;
          if (deltaQty < 0M)
          {
            nullable1 = pickedSplit.PickedQty;
            Decimal num1 = deltaQty;
            Decimal? nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
            Decimal num2 = 0M;
            if (nullable4.GetValueOrDefault() < num2 & nullable4.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be negative.", Array.Empty<object>());
            nullable2 = pickedSplit.PickedQty;
            Decimal num3 = deltaQty;
            Decimal? nullable5 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
            nullable1 = pickedSplit.PackedQty;
            if (nullable5.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable5.HasValue & nullable1.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be less than the already packed quantity.", Array.Empty<object>());
          }
          else
          {
            nullable2 = pickedSplit.PickedQty;
            Decimal num4 = deltaQty;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
            nullable2 = pickedSplit.Qty;
            Decimal num5 = threshold;
            nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num5) : new Decimal?();
            if (nullable1.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
            if (!string.Equals(pickedSplit.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && this.Basis.LotSerialTrack.IsEnterable)
            {
              if (!this.SetLotSerialNbrAndQty(pickedSplit, deltaQty))
                return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
              flag = true;
            }
          }
          if (!flag)
          {
            this.Basis.EnsureAssignedSplitEditing(pickedSplit);
            PX.Objects.SO.SOShipLineSplit soShipLineSplit = pickedSplit;
            nullable3 = soShipLineSplit.PickedQty;
            Decimal num6 = deltaQty;
            Decimal? nullable6;
            if (!nullable3.HasValue)
            {
              nullable1 = new Decimal?();
              nullable6 = nullable1;
            }
            else
              nullable6 = new Decimal?(nullable3.GetValueOrDefault() + num6);
            soShipLineSplit.PickedQty = nullable6;
            if (deltaQty < 0M && this.Basis.LotSerialTrack.IsEnterable)
            {
              nullable2 = pickedSplit.PickedQty;
              Decimal num7 = deltaQty;
              nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num7) : new Decimal?();
              nullable1 = pickedSplit.Qty;
              if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
                pickedSplit.Qty = pickedSplit.PickedQty;
              nullable1 = pickedSplit.Qty;
              Decimal num8 = 0M;
              if (nullable1.GetValueOrDefault() == num8 & nullable1.HasValue)
                ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.Picked).Delete(pickedSplit);
              else
                ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.Picked).Update(pickedSplit);
            }
            else
              ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.Picked).Update(pickedSplit);
          }
          return FlowStatus.Ok;
        }

        public virtual bool IsSelectedSplit(PX.Objects.SO.SOShipLineSplit split)
        {
          int? inventoryId1 = split.InventoryID;
          int? inventoryId2 = this.Basis.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = split.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
            {
              int? siteId1 = split.SiteID;
              int? siteId2 = this.Basis.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              {
                int? locationId = split.LocationID;
                int? nullable = this.Basis.LocationID ?? split.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
                {
                  if (string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                    return true;
                  bool? remove = this.Basis.Remove;
                  bool flag = false;
                  if (!(remove.GetValueOrDefault() == flag & remove.HasValue))
                    return false;
                  if (this.Basis.SelectedLotSerialClass.LotSerAssign == "U")
                    return true;
                  if (!(this.Basis.SelectedLotSerialClass.LotSerIssueMethod == "U"))
                    return false;
                  Decimal? packedQty = split.PackedQty;
                  Decimal num = 0M;
                  return packedQty.GetValueOrDefault() == num & packedQty.HasValue;
                }
              }
            }
          }
          return false;
        }

        public virtual bool SetLotSerialNbrAndQty(PX.Objects.SO.SOShipLineSplit pickedSplit, Decimal qty)
        {
          PXSelectBase<PX.Objects.SO.SOShipLineSplit> picked = (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.Picked;
          Decimal? pickedQty1 = pickedSplit.PickedQty;
          Decimal num1 = 0M;
          if (pickedQty1.GetValueOrDefault() == num1 & pickedQty1.HasValue)
          {
            bool? isUnassigned = pickedSplit.IsUnassigned;
            bool flag = false;
            if (isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue)
            {
              LSConfig lotSerialTrack = this.Basis.LotSerialTrack;
              if (lotSerialTrack.IsTrackedSerial && this.Basis.SelectedLotSerialClass.LotSerIssueMethod == "U")
              {
                PX.Objects.SO.SOShipLineSplit soShipLineSplit1 = PXResultset<PX.Objects.SO.SOShipLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipLineSplit, PXViewOf<PX.Objects.SO.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<PX.Objects.SO.SOShipLineSplit.FK.OriginalLineSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipLineSplit.shipmentNbr, Equal<BqlField<WMSScanHeader.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
                {
                  (object) this.Basis.LotSerialNbr
                }));
                if (soShipLineSplit1 == null)
                {
                  pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit2 = pickedSplit;
                  Decimal? pickedQty2 = soShipLineSplit2.PickedQty;
                  Decimal num2 = qty;
                  soShipLineSplit2.PickedQty = pickedQty2.HasValue ? new Decimal?(pickedQty2.GetValueOrDefault() + num2) : new Decimal?();
                  pickedSplit = picked.Update(pickedSplit);
                  goto label_39;
                }
                if (string.Equals(soShipLineSplit1.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                  return false;
                PX.Objects.SO.SOShipLineSplit copy1 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(soShipLineSplit1);
                PX.Objects.SO.SOShipLineSplit copy2 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(pickedSplit);
                soShipLineSplit1.Qty = new Decimal?(0M);
                soShipLineSplit1.LotSerialNbr = this.Basis.LotSerialNbr;
                PX.Objects.SO.SOShipLineSplit soShipLineSplit3 = picked.Update(soShipLineSplit1);
                soShipLineSplit3.Qty = copy1.Qty;
                PX.Objects.SO.SOShipLineSplit soShipLineSplit4 = soShipLineSplit3;
                Decimal? pickedQty3 = copy2.PickedQty;
                Decimal num3 = qty;
                Decimal? nullable = pickedQty3.HasValue ? new Decimal?(pickedQty3.GetValueOrDefault() + num3) : new Decimal?();
                soShipLineSplit4.PickedQty = nullable;
                soShipLineSplit3.ExpireDate = copy2.ExpireDate;
                picked.Update(soShipLineSplit3);
                pickedSplit.Qty = new Decimal?(0M);
                pickedSplit.LotSerialNbr = copy1.LotSerialNbr;
                pickedSplit = picked.Update(pickedSplit);
                pickedSplit.Qty = copy2.Qty;
                pickedSplit.PickedQty = copy1.PickedQty;
                pickedSplit.ExpireDate = copy1.ExpireDate;
                pickedSplit = picked.Update(pickedSplit);
                goto label_39;
              }
              pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
              lotSerialTrack = this.Basis.LotSerialTrack;
              if (lotSerialTrack.HasExpiration && this.Basis.ExpireDate.HasValue)
                pickedSplit.ExpireDate = this.Basis.ExpireDate;
              PX.Objects.SO.SOShipLineSplit soShipLineSplit = pickedSplit;
              Decimal? pickedQty4 = soShipLineSplit.PickedQty;
              Decimal num4 = qty;
              soShipLineSplit.PickedQty = pickedQty4.HasValue ? new Decimal?(pickedQty4.GetValueOrDefault() + num4) : new Decimal?();
              pickedSplit = picked.Update(pickedSplit);
              goto label_39;
            }
          }
          bool? isUnassigned1 = pickedSplit.IsUnassigned;
          LSConfig lotSerialTrack1;
          PX.Objects.SO.SOShipLineSplit soShipLineSplit5;
          if (!isUnassigned1.GetValueOrDefault())
          {
            lotSerialTrack1 = this.Basis.LotSerialTrack;
            if (!lotSerialTrack1.IsTrackedLot)
            {
              soShipLineSplit5 = (PX.Objects.SO.SOShipLineSplit) null;
              goto label_15;
            }
          }
          soShipLineSplit5 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) picked.SelectMain(Array.Empty<object>())).FirstOrDefault<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            int? lineNbr1 = s.LineNbr;
            int? lineNbr2 = pickedSplit.LineNbr;
            if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            {
              bool? isUnassigned2 = s.IsUnassigned;
              bool flag = false;
              if (isUnassigned2.GetValueOrDefault() == flag & isUnassigned2.HasValue)
              {
                int? locationId = s.LocationID;
                int? nullable = this.Basis.LocationID ?? pickedSplit.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                  return this.IsSelectedSplit(s);
              }
            }
            return false;
          }));
label_15:
          PX.Objects.SO.SOShipLineSplit soShipLineSplit6 = soShipLineSplit5;
          isUnassigned1 = pickedSplit.IsUnassigned;
          bool flag1 = false;
          int num5;
          if (isUnassigned1.GetValueOrDefault() == flag1 & isUnassigned1.HasValue)
          {
            lotSerialTrack1 = this.Basis.LotSerialTrack;
            num5 = lotSerialTrack1.IsTrackedLot ? 1 : 0;
          }
          else
            num5 = 0;
          bool suppress = num5 != 0;
          if (soShipLineSplit6 != null)
          {
            PX.Objects.SO.SOShipLineSplit soShipLineSplit7 = soShipLineSplit6;
            Decimal? pickedQty5 = soShipLineSplit7.PickedQty;
            Decimal num6 = qty;
            soShipLineSplit7.PickedQty = pickedQty5.HasValue ? new Decimal?(pickedQty5.GetValueOrDefault() + num6) : new Decimal?();
            Decimal? pickedQty6 = soShipLineSplit6.PickedQty;
            Decimal? qty1 = soShipLineSplit6.Qty;
            if (pickedQty6.GetValueOrDefault() > qty1.GetValueOrDefault() & pickedQty6.HasValue & qty1.HasValue)
              soShipLineSplit6.Qty = soShipLineSplit6.PickedQty;
            using (this.Graph.LineSplittingExt.SuppressedModeScope(suppress))
              picked.Update(soShipLineSplit6);
          }
          else
          {
            PX.Objects.SO.SOShipLineSplit copy = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(pickedSplit);
            copy.SplitLineNbr = new int?();
            copy.PlanID = new long?();
            copy.LotSerialNbr = this.Basis.LotSerialNbr;
            Decimal? qty2 = pickedSplit.Qty;
            Decimal num7 = qty;
            Decimal? nullable = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - num7) : new Decimal?();
            Decimal num8 = 0M;
            if (!(nullable.GetValueOrDefault() > num8 & nullable.HasValue))
            {
              isUnassigned1 = pickedSplit.IsUnassigned;
              if (!isUnassigned1.GetValueOrDefault())
              {
                copy.Qty = pickedSplit.Qty;
                copy.PickedQty = pickedSplit.PickedQty;
                copy.PackedQty = pickedSplit.PackedQty;
                goto label_30;
              }
            }
            copy.Qty = new Decimal?(qty);
            copy.PickedQty = new Decimal?(qty);
            copy.PackedQty = new Decimal?(0M);
            copy.IsUnassigned = new bool?(false);
label_30:
            using (this.Graph.LineSplittingExt.SuppressedModeScope(suppress))
              picked.Insert(copy);
          }
          bool? isUnassigned3 = pickedSplit.IsUnassigned;
          bool flag2 = false;
          if (isUnassigned3.GetValueOrDefault() == flag2 & isUnassigned3.HasValue)
          {
            Decimal? qty3 = pickedSplit.Qty;
            Decimal num9 = 0M;
            if (qty3.GetValueOrDefault() <= num9 & qty3.HasValue)
            {
              pickedSplit = picked.Delete(pickedSplit);
            }
            else
            {
              PX.Objects.SO.SOShipLineSplit soShipLineSplit8 = pickedSplit;
              Decimal? qty4 = soShipLineSplit8.Qty;
              Decimal num10 = qty;
              soShipLineSplit8.Qty = qty4.HasValue ? new Decimal?(qty4.GetValueOrDefault() - num10) : new Decimal?();
              pickedSplit = picked.Update(pickedSplit);
            }
          }
label_39:
          return true;
        }

        [Obsolete("Use the GetSplitsToPick method instead.")]
        public virtual PX.Objects.SO.SOShipLineSplit GetPickedSplit()
        {
          return this.GetSplitsToPick().FirstOrDefault<PX.Objects.SO.SOShipLineSplit>();
        }

        public virtual IEnumerable<PX.Objects.SO.SOShipLineSplit> GetSplitsToPick()
        {
          return (IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Mode.Picked).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOShipLineSplit>(new Func<PX.Objects.SO.SOShipLineSplit, bool>(this.IsSelectedSplit)).With<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(new Func<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(this.PrioritizeSplits));
        }

        public virtual IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit> PrioritizeSplits(
          IEnumerable<PX.Objects.SO.SOShipLineSplit> splits)
        {
          if (this.Basis.Remove.GetValueOrDefault())
            return splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
            {
              Decimal? pickedQty = split.PickedQty;
              Decimal num = 0M;
              return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
            })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenBy<PX.Objects.SO.SOShipLineSplit, Decimal?>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (split =>
            {
              Decimal? qty = split.Qty;
              Decimal? pickedQty = split.PickedQty;
              return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
            }));
          return this.Basis.LotSerialTrack.IsTrackedSerial ? splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? pickedQty = split.PickedQty;
            Decimal num = 0M;
            return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
          })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => split.IsUnassigned.GetValueOrDefault())) : splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
          })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.IsNullOrEmpty(split.LotSerialNbr))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? pickedQty = split.PickedQty;
            Decimal num = 0M;
            return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
          })).ThenByDescending<PX.Objects.SO.SOShipLineSplit, Decimal?>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (split =>
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
          }));
        }

        public virtual void EnsureShipmentUserLinkForPick()
        {
          this.Graph.WorkLogExt.EnsureFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK");
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm picking {0} x {1} {2}.";
        public const string NothingToPick = "No items to pick.";
        public const string NothingToRemove = "No items to remove from shipment.";
        public const string Overpicking = "The picked quantity cannot be greater than the quantity in the shipment line.";
        public const string Underpicking = "The picked quantity cannot be negative.";
        public const string UnderpickingByPack = "The picked quantity cannot be less than the already packed quantity.";
        public const string InventoryAdded = "{0} x {1} {2} added to shipment.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from shipment.";
      }
    }

    public sealed class ConfirmShipmentPickingCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      public virtual string Code => "CONFIRM*PICK";

      public virtual string ButtonName => "scanConfirmShipmentPicking";

      public virtual string DisplayName => "Confirm Picking";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.PickMode.ConfirmShipmentPickingCommand.Logic>().ConfirmShipmentPicking();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        public virtual bool ConfirmShipmentPicking()
        {
          if (!this.CanConfirmPicking())
            return true;
          this.Basis.SaveChanges();
          this.Basis.WaitFor<PX.Objects.SO.SOShipment>((Action<PickPackShip, PX.Objects.SO.SOShipment>) ((basis, doc) => PickPackShip.PickMode.ConfirmShipmentPickingCommand.Logic.ConfirmPickingHandler(doc.ShipmentNbr))).WithDescription("Marking the {0} shipment as picked.", new object[1]
          {
            (object) this.Basis.RefNbr
          }).ActualizeDataBy((Func<PickPackShip, PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment>) ((basis, doc) => (PX.Objects.SO.SOShipment) PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), (PX.Objects.SO.SOShipment.shipmentNbr) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.ISuccessProcessor>) (x => x.Say("The shipment has been successfully marked as picked.", Array.Empty<object>()).ChangeStateTo<PickPackShip.PickMode.ShipmentState>())).OnFail((Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor>) (x => x.Say("The shipment could not be marked as picked.", Array.Empty<object>()))).BeginAwait(this.Basis.Shipment);
          return true;
        }

        protected static void ConfirmPickingHandler(string shipmentNbr)
        {
          ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).FindImplementation<PickPackShip.PickMode.ConfirmShipmentPickingCommand.PickingConfirmation>().ConfirmPickedQtyAndMarkShipmentPicked(shipmentNbr);
        }

        protected virtual bool CanConfirmPicking()
        {
          PX.Objects.SO.SOShipLineSplit[] source = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Basis.Get<PickPackShip.PickMode.Logic>().Picked).SelectMain(Array.Empty<object>());
          if (((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? pickedQty = s.PickedQty;
            Decimal num = 0M;
            return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
          })))
          {
            this.Basis.ReportError("The shipment cannot be marked as picked because no items have been picked.", Array.Empty<object>());
            return false;
          }
          if (((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType != "WRN" && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? pickedQty = s.PickedQty;
            Decimal? qty = s.Qty;
            Decimal minQtyThreshold = this.Basis.Graph.GetMinQtyThreshold(s);
            Decimal? nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * minQtyThreshold) : new Decimal?();
            return pickedQty.GetValueOrDefault() < nullable.GetValueOrDefault() & pickedQty.HasValue & nullable.HasValue;
          })))
          {
            if (this.Basis.CannotConfirmPartialShipments)
              this.Basis.ReportError("The shipment cannot be marked as picked because it is not complete.", Array.Empty<object>());
            else
              this.Basis.ReportWarning("The shipment is incomplete and should not be marked as picked. Do you want to finish picking the shipment?", Array.Empty<object>());
            return false;
          }
          if (!this.Basis.HasIncompleteLinesBy<PX.Objects.SO.SOShipLineSplit.pickedQty>())
            return true;
          this.Basis.ReportError("The shipment cannot be marked as picked because it is not complete.", Array.Empty<object>());
          return false;
        }
      }

      public class PickingConfirmation : PXGraphExtension<SOShipmentEntry>
      {
        public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>();

        public virtual void ConfirmPickedQtyAndMarkShipmentPicked(string shipmentNbr)
        {
          NonStockKitSpecHelper stockKitSpecHelper = new NonStockKitSpecHelper((PXGraph) this.Base);
          Func<int, bool> RequireShipping = Func.Memorize<int, bool>((Func<int, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(inventoryID)).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (item => item.StkItem.GetValueOrDefault() || item.NonStockShip.GetValueOrDefault()))));
          PXSelectBase<PX.Objects.SO.SOShipment> document = (PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document;
          PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) this.Base.Transactions;
          PXSelectBase<PX.Objects.SO.SOShipLineSplit> splits = (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Base.splits;
          document.Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) shipmentNbr
          }));
          Decimal num1 = 0M;
          foreach (PXResult<SOShipLine> pxResult in transactions.Select(Array.Empty<object>()))
          {
            SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult);
            transactions.Current = soShipLine;
            Decimal num2;
            if (stockKitSpecHelper.IsNonStockKit(transactions.Current.InventoryID))
            {
              Dictionary<int, Decimal> dictionary1 = EnumerableExtensions.ToDictionary<int, Decimal>(stockKitSpecHelper.GetNonStockKitSpec(transactions.Current.InventoryID.Value).Where<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, bool>) (pair => RequireShipping(pair.Key))));
              Dictionary<int, Decimal> dictionary2 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splits.SelectMain(Array.Empty<object>())).GroupBy<PX.Objects.SO.SOShipLineSplit, int>((Func<PX.Objects.SO.SOShipLineSplit, int>) (r => r.InventoryID.Value)).ToDictionary<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, int, Decimal>((Func<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, int>) (g => g.Key), (Func<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, Decimal>) (g => g.Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal>) (s => s.PickedQty.GetValueOrDefault()))));
              num2 = dictionary1.Keys.Count<int>() == 0 || dictionary1.Keys.Except<int>((IEnumerable<int>) dictionary2.Keys).Count<int>() > 0 ? 0M : dictionary2.Join<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) dictionary1, (Func<KeyValuePair<int, Decimal>, int>) (split => split.Key), (Func<KeyValuePair<int, Decimal>, int>) (spec => spec.Key), (Func<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, Decimal>) ((split, spec) =>
              {
                KeyValuePair<int, Decimal> keyValuePair = split;
                Decimal d1 = keyValuePair.Value;
                keyValuePair = spec;
                Decimal d2 = keyValuePair.Value;
                return Math.Floor(Decimal.Divide(d1, d2));
              })).Min();
            }
            else
              num2 = INUnitAttribute.ConvertFromBase(((PXSelectBase) transactions).Cache, transactions.Current.InventoryID, transactions.Current.UOM, ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splits.SelectMain(Array.Empty<object>())).Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal>) (s => s.PickedQty.GetValueOrDefault())), INPrecision.NOROUND);
            GraphHelper.MarkUpdated(((PXSelectBase) transactions).Cache, (object) soShipLine, true);
            Decimal? pickedQty = transactions.Current.PickedQty;
            transactions.Current.PickedQty = new Decimal?(num2);
            ((PXSelectBase) transactions).Cache.RaiseFieldUpdated<SOShipLine.pickedQty>((object) transactions.Current, (object) pickedQty);
            num1 += num2;
          }
          GraphHelper.MarkUpdated(((PXSelectBase) document).Cache, (object) document.Current, true);
          Decimal? pickedQty1 = document.Current.PickedQty;
          document.Current.PickedQty = new Decimal?(num1);
          ((PXSelectBase) document).Cache.RaiseFieldUpdated<PX.Objects.SO.SOShipment.pickedQty>((object) document.Current, (object) pickedQty1);
          bool? picked = document.Current.Picked;
          document.Current.Picked = new bool?(true);
          ((PXSelectBase) document).Cache.RaiseFieldUpdated<PX.Objects.SO.SOShipment.picked>((object) document.Current, (object) picked);
          ((PXAction) this.Base.Save).Press();
        }

        public virtual void ReopenPickedShipment(string shipmentNbr)
        {
          PXSelectBase<PX.Objects.SO.SOShipment> document = (PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document;
          PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) this.Base.Transactions;
          document.Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) shipmentNbr
          }));
          foreach (PXResult<SOShipLine> pxResult in transactions.Select(Array.Empty<object>()))
          {
            SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult);
            transactions.Current = soShipLine;
            GraphHelper.MarkUpdated(((PXSelectBase) transactions).Cache, (object) soShipLine, true);
            Decimal? pickedQty = transactions.Current.PickedQty;
            transactions.Current.PickedQty = new Decimal?(0M);
            ((PXSelectBase) transactions).Cache.RaiseFieldUpdated<SOShipLine.pickedQty>((object) transactions.Current, (object) pickedQty);
          }
          GraphHelper.MarkUpdated(((PXSelectBase) document).Cache, (object) document.Current, true);
          Decimal? pickedQty1 = document.Current.PickedQty;
          document.Current.PickedQty = new Decimal?(0M);
          ((PXSelectBase) document).Cache.RaiseFieldUpdated<PX.Objects.SO.SOShipment.pickedQty>((object) document.Current, (object) pickedQty1);
          bool? picked = document.Current.Picked;
          document.Current.Picked = new bool?(false);
          ((PXSelectBase) document).Cache.RaiseFieldUpdated<PX.Objects.SO.SOShipment.picked>((object) document.Current, (object) picked);
          ((PXAction) this.Base.Save).Press();
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Confirm Picking";
        public const string InProcess = "Marking the {0} shipment as picked.";
        public const string Success = "The shipment has been successfully marked as picked.";
        public const string Fail = "The shipment could not be marked as picked.";
        public const string PickingCannotBeConfirmed = "The shipment cannot be marked as picked because no items have been picked.";
        public const string PickingCannotBeConfirmedInPart = "The shipment cannot be marked as picked because it is not complete.";
        public const string PickingShouldNotBeConfirmedInPart = "The shipment is incomplete and should not be marked as picked. Do you want to finish picking the shipment?";
      }
    }

    public sealed class ReopenPickingQuestion : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanQuestion
    {
      public virtual string Code => "REOPENPICK";

      protected virtual string GetPrompt() => "To continue picking the current shipment, click OK.";

      protected virtual void Confirm()
      {
        string refNbr = ((ScanComponent<PickPackShip>) this).Basis.RefNbr;
        ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).FindImplementation<PickPackShip.PickMode.ConfirmShipmentPickingCommand.PickingConfirmation>().ReopenPickedShipment(((ScanComponent<PickPackShip>) this).Basis.RefNbr);
        ((PXGraph) ((ScanComponent<PickPackShip>) this).Basis.Graph).Clear();
        ((ScanComponent<PickPackShip>) this).Basis.TryProcessBy<PickPackShip.PickMode.ShipmentState>(refNbr, (StateSubstitutionRule) (int) byte.MaxValue);
      }

      protected virtual void Reject()
      {
        ((ScanComponent<PickPackShip>) this).Basis.Clear<PickPackShip.PickMode.ShipmentState>(true);
      }

      [PXLocalizable]
      public static class Msg
      {
        public const string Prompt = "To continue picking the current shipment, click OK.";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<PickPackShip>.SetMode<PickPackShip.PickMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "PICK";

      public virtual string DisplayName => "Pick";

      private string RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>() ? 1 : 0;
          SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && pickPackShipSetup != null && pickPackShipSetup.ShowPickTab.GetValueOrDefault();
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis && basis.RefNbr != null && !basis.DocumentIsConfirmed)
        {
          Validation? nullable = ((ScanMode<PickPackShip>) basis.FindMode<PickPackShip.PickMode>()).TryValidate<PX.Objects.SO.SOShipment>(basis.Shipment).By<PickPackShip.PickMode.ShipmentState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis) || !(basis.CurrentMode.Code != "CRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string Description = "Pick";
      public const string Completed = "{0} shipment picked.";
    }

    [PXUIField(Visible = false)]
    public class ShowPick : 
      PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.PickMode.ShowPick>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(this.Base.WMS.Get<PickPackShip.PickMode.Logic>().ShowPickTab(row));
      }
    }
  }

  public sealed class ReturnMode : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "CRTN";

    public virtual string Code => "CRTN";

    public virtual string Description => "Return";

    protected virtual bool IsModeActive()
    {
      return ((PXSelectBase<SOPickPackShipSetup>) ((ScanMode<PickPackShip>) this).Basis.Setup).Current.ShowReturningTab.GetValueOrDefault();
    }

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      yield return (ScanState<PickPackShip>) new PickPackShip.ReturnMode.ShipmentState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN),
        IsForIssue = false
      };
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState();
      yield return (ScanState<PickPackShip>) new PickPackShip.ReturnMode.ConfirmState();
      yield return (ScanState<PickPackShip>) new PickPackShip.CommandOrShipmentOnlyState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.ReturnMode.ShipmentState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ConfirmShipmentCommand();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.ReturnMode.ShipmentState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset || ((ScanMode<PickPackShip>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PickPackShip.ReturnMode.value>
    {
      public value()
        : base("CRTN")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<PX.Objects.SO.SOShipLineSplit.FK.ShipmentLine>>>.Order<By<BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.shipmentNbr, IBqlString>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.isUnassigned, IBqlBool>.Desc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.SO.SOShipLineSplit.lineNbr, IBqlInt>.Asc>>, 
      #nullable disable
      PX.Objects.SO.SOShipLineSplit>.View Returned;
      public PXAction<ScanHeader> ReviewReturn;

      protected virtual IEnumerable returned()
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        pxDelegateResult.IsResultSorted = true;
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) this.Basis.GetSplits(this.Basis.RefNbr, true, (Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal? qty = s.Qty;
          return pickedQty.GetValueOrDefault() >= qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue;
        })));
        return (IEnumerable) pxDelegateResult;
      }

      [PXButton]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewReturn(PXAdapter adapter) => adapter.Get();

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        ((PXAction) this.ReviewReturn).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile && e.Row?.Mode == "CRTN");
      }

      [PXOverride]
      public ScanState<PickPackShip> DecorateScanState(
        ScanState<PickPackShip> original,
        Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
      {
        ScanState<PickPackShip> scanState = base_DecorateScanState(original);
        if (((ScanComponent<PickPackShip>) scanState).ModeCode == "CRTN")
        {
          switch (scanState)
          {
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState:
              this.Basis.InjectLocationDeactivationOnDefaultLocationOption(locationState);
              this.Basis.InjectLocationSkippingOnPromptLocationForEveryLineOption(locationState);
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState:
              this.Basis.InjectItemPresenceValidation(itemState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
            case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerailState:
              this.Basis.InjectLotSerialPresenceValidation(lotSerailState, new Func<PickPackShip, PXSelectBase<PX.Objects.SO.SOShipLineSplit>>(viewSelector));
              break;
          }
        }
        return scanState;

        static PXSelectBase<PX.Objects.SO.SOShipLineSplit> viewSelector(PickPackShip basis)
        {
          return (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) basis.Get<PickPackShip.ReturnMode.Logic>().Returned;
        }
      }

      /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
      [PXOverride]
      public void OnBeforeFullClear(Action base_OnBeforeFullClear)
      {
        base_OnBeforeFullClear();
        if (!(this.Basis.CurrentMode is PickPackShip.ReturnMode) || this.Basis.RefNbr == null || !this.Graph.WorkLogExt.SuspendFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK"))
          return;
        this.Graph.WorkLogExt.PersistWorkLog();
      }

      public virtual bool ShowReturnTab(ScanHeader row)
      {
        return ((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.ShowReturningTab.GetValueOrDefault() && row.Mode == "CRTN";
      }

      public virtual bool CanReturn
      {
        get
        {
          return ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Returned).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            Decimal? pickedQty = s.PickedQty;
            Decimal? qty = s.Qty;
            return pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue;
          }));
        }
      }
    }

    public sealed class ShipmentState : PickPackShip.ShipmentState
    {
      protected virtual AbsenceHandling.Of<PX.Objects.SO.SOShipment> HandleAbsence(string barcode)
      {
        PX.Objects.SO.SOShipment soShipment = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<PX.Objects.SO.SOShipment.FK.Site>>, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<PX.Objects.SO.SOOrderShipment.FK.Shipment>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.SO.SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<PX.Objects.SO.SOShipment.status, IBqlString>.IsEqual<SOShipmentStatus.open>>>, And<BqlOperand<PX.Objects.SO.SOShipment.operation, IBqlString>.IsEqual<SOOperation.receipt>>>, And<BqlOperand<PX.Objects.SO.SOOrder.customerRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipment.siteID, Equal<BqlField<WMSScanHeader.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<Current2<WMSScanHeader.siteID>, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[1]
        {
          (object) barcode
        }));
        return soShipment != null ? AbsenceHandling.ReplaceWith<PX.Objects.SO.SOShipment>(soShipment) : ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) this).HandleAbsence(barcode);
      }

      protected virtual Validation Validate(PX.Objects.SO.SOShipment shipment)
      {
        if (shipment.Operation != "R")
          return Validation.Fail("The {0} shipment cannot be returned because it has the {1} operation.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.operation>((IBqlTable) shipment)
          });
        if (!(shipment.Status != "N"))
          return Validation.Ok;
        return Validation.Fail("The {0} shipment cannot be returned because it has the {1} status.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
        });
      }

      protected override void ReportSuccess(PX.Objects.SO.SOShipment shipment)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment loaded and ready to be returned.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
      }

      protected virtual void SetNextState()
      {
        bool? remove = ((ScanComponent<PickPackShip>) this).Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && !((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.ReturnMode.Logic>().CanReturn)
        {
          ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<PickPackShip>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<PickPackShip>) this).Basis.Localize("{0} shipment returned.", new object[1]
            {
              (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
        }
        else
          ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) this).SetNextState();
      }

      [PXLocalizable]
      public new abstract class Msg : PickPackShip.ShipmentState.Msg
      {
        public new const string Ready = "{0} shipment loaded and ready to be returned.";
        public const string InvalidStatus = "The {0} shipment cannot be returned because it has the {1} status.";
        public const string InvalidOperation = "The {0} shipment cannot be returned because it has the {1} operation.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<PickPackShip>) this).Basis.Localize("Confirm returning {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<PickPackShip>) this).Basis.Qty,
            (object) ((ScanComponent<PickPackShip>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.ReturnMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        protected PickPackShip.ReturnMode.Logic ModeLogic { get; private set; }

        public virtual void Initialize()
        {
          this.ModeLogic = this.Basis.Get<PickPackShip.ReturnMode.Logic>();
        }

        public virtual FlowStatus Confirm()
        {
          Decimal restDeltaQty = Sign.op_Multiply(Sign.MinusIf(this.Basis.Remove.GetValueOrDefault()), this.Basis.BaseQty);
          bool hasSuitableSplits = false;
          if (restDeltaQty == 0M)
            return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          bool? nullable;
          if (!this.Basis.LotSerialTrack.IsTrackedSerial)
          {
            nullable = this.Basis.SelectedInventoryItem.WeightItem;
            if (!nullable.GetValueOrDefault())
            {
              IEnumerable<PX.Objects.SO.SOShipLineSplit> splitsToReturn = this.GetSplitsToReturn().Select<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit>) (s =>
              {
                hasSuitableSplits = true;
                return s;
              }));
              FlowStatus flowStatus1 = this.ReturnAllSplits(splitsToReturn, ref restDeltaQty, false);
              nullable = ((FlowStatus) ref flowStatus1).IsError;
              bool flag1 = false;
              if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
                return flowStatus1;
              if (restDeltaQty != 0M)
              {
                nullable = this.Basis.Remove;
                bool flag2 = false;
                if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
                {
                  nullable = this.Basis.SelectedInventoryItem.DecimalBaseUnit;
                  if (nullable.GetValueOrDefault())
                  {
                    FlowStatus flowStatus2 = this.ReturnAllSplits(splitsToReturn, ref restDeltaQty, true);
                    nullable = ((FlowStatus) ref flowStatus2).IsError;
                    bool flag3 = false;
                    if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
                      return flowStatus2;
                    goto label_17;
                  }
                  goto label_17;
                }
                goto label_17;
              }
              goto label_17;
            }
          }
          PX.Objects.SO.SOShipLineSplit soShipLineSplit = this.GetSplitsToReturn().FirstOrDefault<PX.Objects.SO.SOShipLineSplit>();
          if (soShipLineSplit != null)
          {
            hasSuitableSplits = true;
            nullable = this.Basis.Remove;
            bool flag4 = false;
            Decimal num1 = !(nullable.GetValueOrDefault() == flag4 & nullable.HasValue) || this.Basis.LotSerialTrack.IsTrackedSerial ? 1M : this.Graph.GetQtyThreshold(soShipLineSplit);
            nullable = this.Basis.Remove;
            Decimal num2 = nullable.GetValueOrDefault() ? -Math.Min(soShipLineSplit.PickedQty.Value, -restDeltaQty) : Math.Min(soShipLineSplit.Qty.Value * num1 - soShipLineSplit.PickedQty.Value, restDeltaQty);
            if (this.Basis.LotSerialTrack.IsTrackedSerial && EnumerableExtensions.IsNotIn<Decimal>(num2, 1M, -1M))
              return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
            FlowStatus flowStatus = this.ReturnSplit(soShipLineSplit, restDeltaQty, this.Graph.GetQtyThreshold(soShipLineSplit));
            nullable = ((FlowStatus) ref flowStatus).IsError;
            bool flag5 = false;
            if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
              return flowStatus;
            restDeltaQty -= num2;
          }
label_17:
          if (!hasSuitableSplits)
          {
            nullable = this.Basis.Remove;
            FlowStatus flowStatus = FlowStatus.Fail(nullable.GetValueOrDefault() ? "No items to remove from shipment." : "No items to return.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          if (Math.Abs(restDeltaQty) > 0M)
          {
            nullable = this.Basis.Remove;
            FlowStatus flowStatus = FlowStatus.Fail(nullable.GetValueOrDefault() ? "The returned quantity cannot be negative." : "The returned quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
            flowStatus = ((FlowStatus) ref flowStatus).WithModeReset;
            return ((FlowStatus) ref flowStatus).WithChangesDiscard;
          }
          this.EnsureShipmentUserLinkForReturn();
          PickPackShip basis = this.Basis;
          nullable = this.Basis.Remove;
          string str = nullable.GetValueOrDefault() ? "{0} x {1} {2} removed from return." : "{0} x {1} {2} added to return.";
          object[] objArray = new object[3]
          {
            (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          };
          basis.ReportInfo(str, objArray);
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        public virtual FlowStatus ReturnAllSplits(
          IEnumerable<PX.Objects.SO.SOShipLineSplit> splitsToReturn,
          ref Decimal restDeltaQty,
          bool withThresholds)
        {
          foreach (PX.Objects.SO.SOShipLineSplit soShipLineSplit in splitsToReturn)
          {
            bool? nullable1;
            Decimal num1;
            if (withThresholds)
            {
              nullable1 = this.Basis.Remove;
              bool flag = false;
              if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
              {
                num1 = this.Graph.GetQtyThreshold(soShipLineSplit);
                goto label_6;
              }
            }
            num1 = 1M;
label_6:
            Decimal threshold = num1;
            nullable1 = this.Basis.Remove;
            Decimal? nullable2;
            Decimal num2;
            if (!nullable1.GetValueOrDefault())
            {
              nullable2 = soShipLineSplit.Qty;
              Decimal num3 = nullable2.Value * threshold;
              nullable2 = soShipLineSplit.PickedQty;
              Decimal num4 = nullable2.Value;
              num2 = Math.Min(num3 - num4, restDeltaQty);
            }
            else
            {
              nullable2 = soShipLineSplit.PickedQty;
              num2 = -Math.Min(nullable2.Value, -restDeltaQty);
            }
            Decimal deltaQty = num2;
            FlowStatus flowStatus = this.ReturnSplit(soShipLineSplit, deltaQty, threshold);
            nullable1 = ((FlowStatus) ref flowStatus).IsError;
            bool flag1 = false;
            if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
              return ((FlowStatus) ref flowStatus).WithChangesDiscard;
            restDeltaQty -= deltaQty;
            if (restDeltaQty == 0M)
              break;
          }
          return FlowStatus.Ok;
        }

        public virtual FlowStatus ReturnSplit(
          PX.Objects.SO.SOShipLineSplit returnedSplit,
          Decimal deltaQty,
          Decimal threshold)
        {
          bool flag = false;
          Decimal? nullable1;
          Decimal? nullable2;
          if (deltaQty < 0M)
          {
            nullable1 = returnedSplit.PickedQty;
            Decimal num1 = deltaQty;
            Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
            Decimal num2 = 0M;
            if (nullable3.GetValueOrDefault() < num2 & nullable3.HasValue)
              return FlowStatus.Fail("The returned quantity cannot be negative.", Array.Empty<object>());
          }
          else
          {
            Decimal? nullable4 = returnedSplit.PickedQty;
            Decimal num3 = deltaQty;
            nullable2 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num3) : new Decimal?();
            nullable4 = returnedSplit.Qty;
            Decimal num4 = threshold;
            nullable1 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * num4) : new Decimal?();
            if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
              return FlowStatus.Fail("The returned quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
            if (!string.Equals(returnedSplit.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && this.Basis.LotSerialTrack.IsEnterable)
            {
              if (!this.SetLotSerialNbrAndQty(returnedSplit, deltaQty))
                return FlowStatus.Fail("The returned quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
              flag = true;
            }
          }
          if (!flag)
          {
            this.Basis.EnsureAssignedSplitEditing(returnedSplit);
            if (deltaQty > 0M && this.Basis.LocationID.HasValue)
              returnedSplit.LocationID = this.Basis.LocationID;
            PX.Objects.SO.SOShipLineSplit soShipLineSplit = returnedSplit;
            nullable1 = soShipLineSplit.PickedQty;
            Decimal num = deltaQty;
            Decimal? nullable5;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable5 = nullable2;
            }
            else
              nullable5 = new Decimal?(nullable1.GetValueOrDefault() + num);
            soShipLineSplit.PickedQty = nullable5;
            ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.ModeLogic.Returned).Update(returnedSplit);
          }
          return FlowStatus.Ok;
        }

        public virtual bool IsSelectedSplit(PX.Objects.SO.SOShipLineSplit split)
        {
          int? inventoryId1 = split.InventoryID;
          int? inventoryId2 = this.Basis.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = split.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
            {
              int? siteId1 = split.SiteID;
              int? siteId2 = this.Basis.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
                return string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            }
          }
          return false;
        }

        public virtual bool SetLotSerialNbrAndQty(PX.Objects.SO.SOShipLineSplit pickedSplit, Decimal qty)
        {
          PXSelectBase<PX.Objects.SO.SOShipLineSplit> returned = (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.ModeLogic.Returned;
          Decimal? pickedQty1 = pickedSplit.PickedQty;
          Decimal num1 = 0M;
          if (pickedQty1.GetValueOrDefault() == num1 & pickedQty1.HasValue)
          {
            bool? isUnassigned = pickedSplit.IsUnassigned;
            bool flag1 = false;
            if (isUnassigned.GetValueOrDefault() == flag1 & isUnassigned.HasValue)
            {
              LSConfig lotSerialTrack = this.Basis.LotSerialTrack;
              if (lotSerialTrack.IsTrackedSerial && this.Basis.SelectedLotSerialClass.LotSerIssueMethod == "U")
              {
                PX.Objects.SO.SOShipLineSplit soShipLineSplit1 = PXResultset<PX.Objects.SO.SOShipLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipLineSplit, PXViewOf<PX.Objects.SO.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<PX.Objects.SO.SOShipLineSplit.FK.OriginalLineSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipLineSplit.shipmentNbr, Equal<BqlField<WMSScanHeader.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
                {
                  (object) this.Basis.LotSerialNbr
                }));
                if (soShipLineSplit1 == null)
                {
                  bool? remove = this.Basis.Remove;
                  bool flag2 = false;
                  if (remove.GetValueOrDefault() == flag2 & remove.HasValue)
                    pickedSplit.LocationID = this.Basis.LocationID;
                  pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
                  PX.Objects.SO.SOShipLineSplit soShipLineSplit2 = pickedSplit;
                  Decimal? pickedQty2 = soShipLineSplit2.PickedQty;
                  Decimal num2 = qty;
                  soShipLineSplit2.PickedQty = pickedQty2.HasValue ? new Decimal?(pickedQty2.GetValueOrDefault() + num2) : new Decimal?();
                  pickedSplit = returned.Update(pickedSplit);
                  goto label_45;
                }
                if (string.Equals(soShipLineSplit1.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                  return false;
                PX.Objects.SO.SOShipLineSplit copy1 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(soShipLineSplit1);
                PX.Objects.SO.SOShipLineSplit copy2 = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(pickedSplit);
                soShipLineSplit1.Qty = new Decimal?(0M);
                soShipLineSplit1.LotSerialNbr = this.Basis.LotSerialNbr;
                PX.Objects.SO.SOShipLineSplit soShipLineSplit3 = returned.Update(soShipLineSplit1);
                soShipLineSplit3.Qty = copy1.Qty;
                PX.Objects.SO.SOShipLineSplit soShipLineSplit4 = soShipLineSplit3;
                Decimal? pickedQty3 = copy2.PickedQty;
                Decimal num3 = qty;
                Decimal? nullable = pickedQty3.HasValue ? new Decimal?(pickedQty3.GetValueOrDefault() + num3) : new Decimal?();
                soShipLineSplit4.PickedQty = nullable;
                soShipLineSplit3.ExpireDate = copy2.ExpireDate;
                returned.Update(soShipLineSplit3);
                pickedSplit.Qty = new Decimal?(0M);
                bool? remove1 = this.Basis.Remove;
                bool flag3 = false;
                if (remove1.GetValueOrDefault() == flag3 & remove1.HasValue)
                  pickedSplit.LocationID = this.Basis.LocationID;
                pickedSplit.LotSerialNbr = copy1.LotSerialNbr;
                pickedSplit = returned.Update(pickedSplit);
                pickedSplit.Qty = copy2.Qty;
                pickedSplit.PickedQty = copy1.PickedQty;
                pickedSplit.ExpireDate = copy1.ExpireDate;
                pickedSplit = returned.Update(pickedSplit);
                goto label_45;
              }
              bool? remove2 = this.Basis.Remove;
              bool flag4 = false;
              if (remove2.GetValueOrDefault() == flag4 & remove2.HasValue)
                pickedSplit.LocationID = this.Basis.LocationID;
              pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
              lotSerialTrack = this.Basis.LotSerialTrack;
              if (lotSerialTrack.HasExpiration && this.Basis.ExpireDate.HasValue)
                pickedSplit.ExpireDate = this.Basis.ExpireDate;
              PX.Objects.SO.SOShipLineSplit soShipLineSplit = pickedSplit;
              Decimal? pickedQty4 = soShipLineSplit.PickedQty;
              Decimal num4 = qty;
              soShipLineSplit.PickedQty = pickedQty4.HasValue ? new Decimal?(pickedQty4.GetValueOrDefault() + num4) : new Decimal?();
              pickedSplit = returned.Update(pickedSplit);
              goto label_45;
            }
          }
          bool? isUnassigned1 = pickedSplit.IsUnassigned;
          LSConfig lotSerialTrack1;
          PX.Objects.SO.SOShipLineSplit soShipLineSplit5;
          if (!isUnassigned1.GetValueOrDefault())
          {
            lotSerialTrack1 = this.Basis.LotSerialTrack;
            if (!lotSerialTrack1.IsTrackedLot)
            {
              soShipLineSplit5 = (PX.Objects.SO.SOShipLineSplit) null;
              goto label_21;
            }
          }
          soShipLineSplit5 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) returned.SelectMain(Array.Empty<object>())).FirstOrDefault<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
          {
            int? lineNbr1 = s.LineNbr;
            int? lineNbr2 = pickedSplit.LineNbr;
            if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            {
              bool? isUnassigned2 = s.IsUnassigned;
              bool flag = false;
              if (isUnassigned2.GetValueOrDefault() == flag & isUnassigned2.HasValue)
              {
                int? locationId = s.LocationID;
                int? nullable = this.Basis.LocationID ?? pickedSplit.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                  return this.IsSelectedSplit(s);
              }
            }
            return false;
          }));
label_21:
          PX.Objects.SO.SOShipLineSplit soShipLineSplit6 = soShipLineSplit5;
          isUnassigned1 = pickedSplit.IsUnassigned;
          bool flag5 = false;
          int num5;
          if (isUnassigned1.GetValueOrDefault() == flag5 & isUnassigned1.HasValue)
          {
            lotSerialTrack1 = this.Basis.LotSerialTrack;
            num5 = lotSerialTrack1.IsTrackedLot ? 1 : 0;
          }
          else
            num5 = 0;
          bool suppress = num5 != 0;
          if (soShipLineSplit6 != null)
          {
            PX.Objects.SO.SOShipLineSplit soShipLineSplit7 = soShipLineSplit6;
            Decimal? pickedQty5 = soShipLineSplit7.PickedQty;
            Decimal num6 = qty;
            soShipLineSplit7.PickedQty = pickedQty5.HasValue ? new Decimal?(pickedQty5.GetValueOrDefault() + num6) : new Decimal?();
            Decimal? pickedQty6 = soShipLineSplit6.PickedQty;
            Decimal? qty1 = soShipLineSplit6.Qty;
            if (pickedQty6.GetValueOrDefault() > qty1.GetValueOrDefault() & pickedQty6.HasValue & qty1.HasValue)
              soShipLineSplit6.Qty = soShipLineSplit6.PickedQty;
            using (this.Graph.LineSplittingExt.SuppressedModeScope(suppress))
              returned.Update(soShipLineSplit6);
          }
          else
          {
            PX.Objects.SO.SOShipLineSplit copy = PXCache<PX.Objects.SO.SOShipLineSplit>.CreateCopy(pickedSplit);
            copy.SplitLineNbr = new int?();
            copy.PlanID = new long?();
            copy.LotSerialNbr = this.Basis.LotSerialNbr;
            Decimal? qty2 = pickedSplit.Qty;
            Decimal num7 = qty;
            Decimal? nullable = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - num7) : new Decimal?();
            Decimal num8 = 0M;
            if (!(nullable.GetValueOrDefault() > num8 & nullable.HasValue))
            {
              isUnassigned1 = pickedSplit.IsUnassigned;
              if (!isUnassigned1.GetValueOrDefault())
              {
                copy.Qty = pickedSplit.Qty;
                copy.PickedQty = pickedSplit.PickedQty;
                copy.PackedQty = pickedSplit.PackedQty;
                goto label_36;
              }
            }
            copy.Qty = new Decimal?(qty);
            copy.PickedQty = new Decimal?(qty);
            copy.PackedQty = new Decimal?(0M);
            copy.IsUnassigned = new bool?(false);
label_36:
            using (this.Graph.LineSplittingExt.SuppressedModeScope(suppress))
              returned.Insert(copy);
          }
          bool? isUnassigned3 = pickedSplit.IsUnassigned;
          bool flag6 = false;
          if (isUnassigned3.GetValueOrDefault() == flag6 & isUnassigned3.HasValue)
          {
            Decimal? qty3 = pickedSplit.Qty;
            Decimal num9 = 0M;
            if (qty3.GetValueOrDefault() <= num9 & qty3.HasValue)
            {
              pickedSplit = returned.Delete(pickedSplit);
            }
            else
            {
              PX.Objects.SO.SOShipLineSplit soShipLineSplit8 = pickedSplit;
              Decimal? qty4 = soShipLineSplit8.Qty;
              Decimal num10 = qty;
              soShipLineSplit8.Qty = qty4.HasValue ? new Decimal?(qty4.GetValueOrDefault() - num10) : new Decimal?();
              pickedSplit = returned.Update(pickedSplit);
            }
          }
label_45:
          return true;
        }

        [Obsolete("Use the GetSplitsToReturn method instead.")]
        public virtual PX.Objects.SO.SOShipLineSplit GetSplitToReturn()
        {
          return this.GetSplitsToReturn().FirstOrDefault<PX.Objects.SO.SOShipLineSplit>();
        }

        public virtual IEnumerable<PX.Objects.SO.SOShipLineSplit> GetSplitsToReturn()
        {
          return (IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.ModeLogic.Returned).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOShipLineSplit>(new Func<PX.Objects.SO.SOShipLineSplit, bool>(this.IsSelectedSplit)).With<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(new Func<IEnumerable<PX.Objects.SO.SOShipLineSplit>, IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit>>(this.PrioritizeSplits));
        }

        public virtual IOrderedEnumerable<PX.Objects.SO.SOShipLineSplit> PrioritizeSplits(
          IEnumerable<PX.Objects.SO.SOShipLineSplit> splits)
        {
          if (this.Basis.Remove.GetValueOrDefault())
            return splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
            {
              Decimal? pickedQty = split.PickedQty;
              Decimal num = 0M;
              return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
            })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenBy<PX.Objects.SO.SOShipLineSplit, Decimal?>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (split =>
            {
              Decimal? qty = split.Qty;
              Decimal? pickedQty = split.PickedQty;
              return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
            }));
          return this.Basis.LotSerialTrack.IsTrackedSerial ? splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? pickedQty = split.PickedQty;
            Decimal num = 0M;
            return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
          })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => split.IsUnassigned.GetValueOrDefault())) : splits.OrderByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
          })).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split => string.IsNullOrEmpty(split.LotSerialNbr))).ThenByAccordanceTo<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (split =>
          {
            Decimal? pickedQty = split.PickedQty;
            Decimal num = 0M;
            return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
          })).ThenByDescending<PX.Objects.SO.SOShipLineSplit, Decimal?>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (split =>
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
          }));
        }

        public virtual void EnsureShipmentUserLinkForReturn()
        {
          this.Graph.WorkLogExt.EnsureFor(this.Basis.RefNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK");
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm returning {0} x {1} {2}.";
        public const string NothingToReturn = "No items to return.";
        public const string NothingToRemove = "No items to remove from shipment.";
        public const string Overreturning = "The returned quantity cannot be greater than the quantity in the shipment line.";
        public const string Underreturning = "The returned quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added to return.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from return.";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<PickPackShip>.SetMode<PickPackShip.ReturnMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "SORETURN";

      public virtual string DisplayName => "SO Return";

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>() ? 1 : 0;
          SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && pickPackShipSetup != null && pickPackShipSetup.ShowReturningTab.GetValueOrDefault();
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "SO Return";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string Description = "Return";
      public const string Completed = "{0} shipment returned.";
    }

    [PXUIField(Visible = false)]
    public class ShowReturn : 
      PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.ReturnMode.ShowReturn>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(this.Base.WMS.Get<PickPackShip.ReturnMode.Logic>().ShowReturnTab(row));
      }
    }
  }

  public sealed class ShipMode : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "SHIP";

    public virtual string Code => "SHIP";

    public virtual string Description => "Ship";

    protected virtual bool IsModeActive()
    {
      return ((PXSelectBase<SOPickPackShipSetup>) ((ScanMode<PickPackShip>) this).Basis.Setup).Current.ShowShipTab.GetValueOrDefault();
    }

    protected virtual ScanState<PickPackShip> GetDefaultState()
    {
      return ((ScanMode<PickPackShip>) this).Basis.RefNbr != null ? ((ScanMode<PickPackShip>) this).FindState("NONE") : ((ScanMode<PickPackShip>) this).GetDefaultState();
    }

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      yield return (ScanState<PickPackShip>) new PickPackShip.ShipMode.ShipmentState();
      yield return (ScanState<PickPackShip>) new PickPackShip.CommandOrShipmentOnlyState();
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ShipMode.RefreshRatesCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ShipMode.GetLabelsCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.ConfirmShipmentCommand();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.ShipMode.ShipmentState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PickPackShip.ShipMode.value>
    {
      public value()
        : base("SHIP")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        if (!(e.Row?.Mode == "SHIP"))
          return;
        ((PXAction) this.Basis.ScanConfirm).SetVisible(false);
      }

      public virtual bool ShowShipTab(ScanHeader row)
      {
        return ((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.ShowShipTab.GetValueOrDefault() && row.Mode == "SHIP";
      }
    }

    public class CarrierRatesLogic : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      protected virtual void ClearCarrierRates()
      {
        ((PXSelectBase) this.Graph.CarrierRatesExt.CarrierRates).Cache.Clear();
      }

      protected virtual void _(PX.Data.Events.RowInserted<SOPackageDetailEx> e)
      {
        this.ClearCarrierRates();
      }

      protected virtual void _(PX.Data.Events.RowUpdated<SOPackageDetailEx> e)
      {
        this.ClearCarrierRates();
      }

      protected virtual void _(PX.Data.Events.RowDeleted<SOPackageDetailEx> e)
      {
        this.ClearCarrierRates();
      }
    }

    public sealed class ShipmentState : PickPackShip.ShipmentState
    {
      private bool _needToRefreshRates;

      protected virtual Validation Validate(PX.Objects.SO.SOShipment shipment)
      {
        if (shipment.Operation != "I")
          return Validation.Fail("The {0} shipment cannot be processed in Ship mode because it has the {1} operation.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.operation>((IBqlTable) shipment)
          });
        if (!(shipment.Status != "N"))
          return Validation.Ok;
        return Validation.Fail("The {0} shipment cannot be processed in Ship mode because it has the {1} status.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
        });
      }

      protected override void Apply(PX.Objects.SO.SOShipment shipment)
      {
        this._needToRefreshRates = false;
        string refNbr = ((ScanComponent<PickPackShip>) this).Basis.RefNbr;
        base.Apply(shipment);
        if (!EnumerableExtensions.IsNotIn<string>(((ScanComponent<PickPackShip>) this).Basis.RefNbr, (string) null, refNbr) || ((ScanComponent<PickPackShip>) this).Basis.Header.Barcode.StartsWith("@"))
          return;
        this._needToRefreshRates = true;
      }

      protected override void ClearState()
      {
        base.ClearState();
        this._needToRefreshRates = false;
      }

      protected override void ReportSuccess(PX.Objects.SO.SOShipment shipment)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment loaded and ready to be shipped.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
      }

      protected virtual void SetNextState()
      {
        if (this._needToRefreshRates)
          ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.ShipMode.RefreshRatesCommand.Logic>().UpdateRates();
        this._needToRefreshRates = false;
      }

      [PXLocalizable]
      public new abstract class Msg : PickPackShip.ShipmentState.Msg
      {
        public new const string Ready = "{0} shipment loaded and ready to be shipped.";
        public const string InvalidStatus = "The {0} shipment cannot be processed in Ship mode because it has the {1} status.";
        public const string InvalidOperation = "The {0} shipment cannot be processed in Ship mode because it has the {1} operation.";
      }
    }

    public sealed class GetLabelsCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      public virtual string Code => "GET*LABELS";

      public virtual string ButtonName => "scanGetLabels";

      public virtual string DisplayName => "Get Return Labels";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        return this.Get<PickPackShip.ShipMode.GetLabelsCommand.Logic>().GetLabels();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        public virtual bool GetLabels()
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          PickPackShip.ShipMode.GetLabelsCommand.Logic.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new PickPackShip.ShipMode.GetLabelsCommand.Logic.\u003C\u003Ec__DisplayClass0_0();
          ((PXAction) this.Basis.Save).Press();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass00.clone = GraphHelper.Clone<PickPackShip.Host>(this.Graph);
          // ISSUE: reference to a compiler-generated field
          cDisplayClass00.refNbr = this.Basis.RefNbr;
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this.Basis.Graph, new PXToggleAsyncDelegate((object) cDisplayClass00, __methodptr(\u003CGetLabels\u003Eb__0)));
          return true;
        }
      }
    }

    public sealed class RefreshRatesCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      public virtual string Code => "REFRESH*RATES";

      public virtual string ButtonName => "scanRefreshRates";

      public virtual string DisplayName => "Refresh Rates";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        return this.Get<PickPackShip.ShipMode.RefreshRatesCommand.Logic>().PerformRatesRefresh();
      }

      public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
      {
        public virtual bool PerformRatesRefresh()
        {
          if (!string.IsNullOrEmpty(this.Basis.RefNbr))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec__DisplayClass0_0();
            ((PXAction) this.Basis.Save).Press();
            // ISSUE: reference to a compiler-generated field
            cDisplayClass00.clone = GraphHelper.Clone<PickPackShip.Host>(this.Graph);
            // ISSUE: method pointer
            PXLongOperation.StartOperation((PXGraph) this.Graph, new PXToggleAsyncDelegate((object) cDisplayClass00, __methodptr(\u003CPerformRatesRefresh\u003Eb__0)));
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            ((PXGraph) this.Basis.Graph).RowSelected.AddHandler<SOCarrierRate>(PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec.\u003C\u003E9__0_1 ?? (PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec.\u003C\u003E9__0_1 = new PXRowSelected((object) PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPerformRatesRefresh\u003Eb__0_1))));
          }
          return true;
        }

        public static void UpdateRates(PickPackShip.Host graph)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new PickPackShip.ShipMode.RefreshRatesCommand.Logic.\u003C\u003Ec__DisplayClass1_0();
          // ISSUE: reference to a compiler-generated field
          cDisplayClass10.carrierRateErrors = new Dictionary<SOCarrierRate, PXSetPropertyException>();
          try
          {
            // ISSUE: method pointer
            ((PXGraph) graph).ExceptionHandling.AddHandler<SOCarrierRate.method>(new PXExceptionHandling((object) cDisplayClass10, __methodptr(\u003CUpdateRates\u003Eg__saveCarrierRateError\u007C0)));
            graph.CarrierRatesExt.UpdateRates();
          }
          finally
          {
            // ISSUE: method pointer
            ((PXGraph) graph).ExceptionHandling.RemoveHandler<SOCarrierRate.method>(new PXExceptionHandling((object) cDisplayClass10, __methodptr(\u003CUpdateRates\u003Eg__saveCarrierRateError\u007C0)));
          }
          PXCache<SOCarrierRate> pxCache = GraphHelper.Caches<SOCarrierRate>((PXGraph) graph);
          // ISSUE: reference to a compiler-generated field
          foreach (KeyValuePair<SOCarrierRate, PXSetPropertyException> carrierRateError in cDisplayClass10.carrierRateErrors)
          {
            SOCarrierRate key = carrierRateError.Key;
            PXSetPropertyException propertyException = new PXSetPropertyException(((Exception) carrierRateError.Value).Message, (PXErrorLevel) 4)
            {
              ErrorValue = (object) key.Amount
            };
            ((PXCache) pxCache).RaiseExceptionHandling<SOCarrierRate.amount>((object) key, (object) key.Amount, (Exception) propertyException);
          }
        }

        public virtual void UpdateRates()
        {
          if (PXResultset<SOPackageDetailEx>.op_Implicit(((PXSelectBase<SOPackageDetailEx>) this.Basis.Graph.Packages).SelectWindowed(0, 1, Array.Empty<object>())) == null)
            return;
          try
          {
            this.Basis.Graph.CarrierRatesExt.UpdateRates();
          }
          catch (PXException ex)
          {
            this.Basis.ReportError(ex.MessageNoPrefix, Array.Empty<object>());
          }
        }
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<PickPackShip>.SetMode<PickPackShip.ShipMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "SHIP";

      public virtual string DisplayName => "Ship";

      private string RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          if (((ScanComponent<TForeignBasis>) this).Basis.Graph.IsMobile)
            return false;
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>() ? 1 : 0;
          SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && pickPackShipSetup != null && pickPackShipSetup.ShowShipTab.GetValueOrDefault();
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis && basis.RefNbr != null && !basis.DocumentIsConfirmed)
        {
          Validation? nullable = ((ScanMode<PickPackShip>) basis.FindMode<PickPackShip.ShipMode>()).TryValidate<PX.Objects.SO.SOShipment>(basis.Shipment).By<PickPackShip.ShipMode.ShipmentState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is PickPackShip basis) || !(basis.CurrentMode.Code != "CRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
        SOPackageDetailEx package;
        if ((!basis.Get<PickPackShip.PackMode.Logic>().HasSingleAutoPackage(basis.RefNbr, out package) ? 0 : (!package.Confirmed.GetValueOrDefault() ? 1 : 0)) != 0)
        {
          package.Confirmed = new bool?(true);
          ((PXSelectBase<SOPackageDetailEx>) basis.Graph.Packages).Update(package);
          ((PXSelectBase<PX.Objects.SO.SOShipment>) basis.Graph.Document).Current.IsPackageValid = new bool?(true);
          ((PXSelectBase<PX.Objects.SO.SOShipment>) basis.Graph.Document).UpdateCurrent();
          basis.Reset(false);
          basis.SaveChanges();
        }
        basis.Get<PickPackShip.ShipMode.RefreshRatesCommand.Logic>().UpdateRates();
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string Description = "Ship";
    }

    [PXUIField(Visible = false)]
    public class ShowShip : 
      PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.ShipMode.ShowShip>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(this.Base.WMS.Get<PickPackShip.ShipMode.Logic>().ShowShipTab(row));
      }
    }
  }

  public class Host : SOShipmentEntry, ICaptionable
  {
    public PickPackShip WMS => ((PXGraph) this).FindImplementation<PickPackShip>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.QtySupport
  {
  }

  public new class GS1Support : WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.GS1Support
  {
  }

  public class UserSetup : 
    PXUserSetup<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, SOPickPackShipUserSetup.userID>
  {
  }

  public abstract class ShipmentState : 
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RefNbrState<PX.Objects.SO.SOShipment>
  {
    protected virtual string StatePrompt => "Scan the shipment number.";

    protected virtual PX.Objects.SO.SOShipment GetByBarcode(string barcode)
    {
      return PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<PX.Objects.SO.SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.SO.SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipment.shipmentNbr, Equal<P.AsString>>>>, And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[1]
      {
        (object) barcode
      }));
    }

    protected virtual void Apply(PX.Objects.SO.SOShipment shipment)
    {
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Document).Current = shipment;
      ((ScanComponent<PickPackShip>) this).Basis.RefNbr = shipment.ShipmentNbr;
      ((ScanComponent<PickPackShip>) this).Basis.SiteID = shipment.SiteID;
      ((ScanComponent<PickPackShip>) this).Basis.TranDate = shipment.ShipDate;
      ((ScanComponent<PickPackShip>) this).Basis.TranType = shipment.ShipmentType == "T" ? "TRX" : (shipment.Operation == "R" ? "RET" : "III");
      ((ScanComponent<PickPackShip>) this).Basis.NoteID = shipment.NoteID;
    }

    protected virtual void ClearState()
    {
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Document).Current = (PX.Objects.SO.SOShipment) null;
      ((ScanComponent<PickPackShip>) this).Basis.RefNbr = (string) null;
      ((ScanComponent<PickPackShip>) this).Basis.SiteID = new int?();
      ((ScanComponent<PickPackShip>) this).Basis.TranDate = new DateTime?();
      ((ScanComponent<PickPackShip>) this).Basis.TranType = (string) null;
      ((ScanComponent<PickPackShip>) this).Basis.NoteID = new Guid?();
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportError("{0} shipment not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void ReportSuccess(PX.Objects.SO.SOShipment shipment)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} shipment loaded and ready to be processed.", new object[1]
      {
        (object) shipment.ShipmentNbr
      });
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the shipment number.";
      public const string Ready = "{0} shipment loaded and ready to be processed.";
      public const string Missing = "{0} shipment not found.";
      public const string Invalid = "The {0} shipment cannot be processed because it has the {1} status.";
    }
  }

  public sealed class CommandOrShipmentOnlyState : CommandOnlyStateBase<PickPackShip>
  {
    public virtual void MoveToNextState()
    {
    }

    public virtual string Prompt
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.CommandOrShipmentOnlyState.Logic>().GetPromptForCommandOrShipmentOnly();
      }
    }

    public virtual bool Process(string barcode)
    {
      if (((ScanComponent<PickPackShip>) this).Basis.TryProcessBy<PickPackShip.ShipmentState>(barcode, (StateSubstitutionRule) 1))
      {
        ((ScanComponent<PickPackShip>) this).Basis.Clear<PickPackShip.ShipmentState>(true);
        ((ScanComponent<PickPackShip>) this).Basis.Reset(false);
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState<PickPackShip.ShipmentState>((string) null, Array.Empty<object>());
        ((ScanState<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.CurrentMode.FindState<PickPackShip.ShipmentState>(false)).Process(barcode);
        return true;
      }
      ((ScanComponent<PickPackShip>) this).Basis.Reporter.Error(((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.CommandOrShipmentOnlyState.Logic>().GetErrorForCommandOrShipmentOnly(), Array.Empty<object>());
      return false;
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      public virtual string GetPromptForCommandOrShipmentOnly()
      {
        return "Use any command or scan the next shipment number to continue.";
      }

      public virtual string GetErrorForCommandOrShipmentOnly()
      {
        return "Only commands or a shipment number can be used to continue.";
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string UseCommandOrShipmentToContinue = "Use any command or scan the next shipment number to continue.";
      public const string OnlyCommandsAndShipmentsAreAllowed = "Only commands or a shipment number can be used to continue.";
    }
  }

  public sealed class ConfirmShipmentCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "CONFIRM*SHIPMENT";

    public virtual string ButtonName => "scanConfirmShipment";

    public virtual string DisplayName => "Confirm Shipment";

    protected virtual bool IsEnabled
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
    }

    protected virtual bool Process()
    {
      return ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.ConfirmShipmentCommand.Logic>().ConfirmShipment(!((ScanComponent<PickPackShip>) this).Basis.HasPick && !((ScanComponent<PickPackShip>) this).Basis.HasPack);
    }

    public class Logic : BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension
    {
      public virtual bool ConfirmShipment(bool confirmAsIs)
      {
        if (!this.CanConfirm(confirmAsIs))
          return true;
        PickPackShip.PackMode.Logic logic = this.Basis.Get<PickPackShip.PackMode.Logic>();
        SOPackageDetailEx selectedPackage = logic.SelectedPackage;
        int num;
        if (selectedPackage == null)
        {
          num = 0;
        }
        else
        {
          bool? confirmed = selectedPackage.Confirmed;
          bool flag = false;
          num = confirmed.GetValueOrDefault() == flag & confirmed.HasValue ? 1 : 0;
        }
        if (num != 0 && !this.Basis.Get<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>().TryAutoConfirm())
          return true;
        int? packageLineNbr = logic.PackageLineNbr;
        this.Basis.Reset(false);
        this.Basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
        logic.PackageLineNbr = packageLineNbr;
        SOPackageDetailEx autoPackageToConfirm = (SOPackageDetailEx) null;
        if (!confirmAsIs && EnumerableExtensions.IsIn<string>(this.Basis.Header.Mode, "PACK", "SHIP"))
          logic.HasSingleAutoPackage(this.Basis.RefNbr, out autoPackageToConfirm);
        string refNbr = this.Basis.RefNbr;
        SOPickPackShipSetup current = ((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current;
        SOPickPackShipUserSetup userSetup = PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis));
        SOPickPackShipSetup setup = current;
        this.Basis.SaveChanges();
        this.Basis.AwaitFor<PX.Objects.SO.SOShipment>((Func<PickPackShip, PX.Objects.SO.SOShipment, CancellationToken, System.Threading.Tasks.Task>) ((basis, doc, ct) => PickPackShip.ConfirmShipmentCommand.Logic.ConfirmShipmentHandler(doc.ShipmentNbr, confirmAsIs, setup, userSetup, autoPackageToConfirm, ct))).WithDescription("Confirmation of {0} shipment in progress.", new object[1]
        {
          (object) this.Basis.RefNbr
        }).ActualizeDataBy((Func<PickPackShip, PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment>) ((basis, doc) => (PX.Objects.SO.SOShipment) PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), (PX.Objects.SO.SOShipment.shipmentNbr) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.ISuccessProcessor>) (x => x.Say("Shipment successfully confirmed.", Array.Empty<object>()).ChangeStateTo<PickPackShip.ShipmentState>())).OnFail((Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor>) (x => x.Say("Shipment not confirmed.", Array.Empty<object>()))).BeginAwait(this.Basis.Shipment);
        return true;
      }

      protected static System.Threading.Tasks.Task ConfirmShipmentHandler(
        string shipmentNbr,
        bool confirmAsIs,
        SOPickPackShipSetup setup,
        SOPickPackShipUserSetup userSetup,
        SOPackageDetailEx autoPackageToConfirm,
        CancellationToken cancellationToken)
      {
        return ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).FindImplementation<PickPackShip.ConfirmShipmentCommand.PickPackShipShipmentConfirmation>().ApplyPickedQtyAndConfirmShipment(shipmentNbr, confirmAsIs, setup, userSetup, autoPackageToConfirm, cancellationToken);
      }

      protected virtual bool CanConfirm(bool confirmAsIs)
      {
        return confirmAsIs || (!this.Basis.HasPick || this.CanConfirmPicked()) && (!this.Basis.HasPack || this.CanConfirmPacked());
      }

      protected virtual bool CanConfirmPicked()
      {
        PX.Objects.SO.SOShipLineSplit[] source = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Basis.Get<PickPackShip.PickMode.Logic>().Picked).SelectMain(Array.Empty<object>());
        if (((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal num = 0M;
          return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
        })))
        {
          this.Basis.ReportError("The shipment cannot be confirmed because no items have been picked.", Array.Empty<object>());
          return false;
        }
        if (((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType != "WRN" && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal? qty = s.Qty;
          Decimal minQtyThreshold = this.Basis.Graph.GetMinQtyThreshold(s);
          Decimal? nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * minQtyThreshold) : new Decimal?();
          return pickedQty.GetValueOrDefault() < nullable.GetValueOrDefault() & pickedQty.HasValue & nullable.HasValue;
        })))
        {
          if (this.Basis.CannotConfirmPartialShipments)
            this.Basis.ReportError("The shipment cannot be confirmed because at least one line has not been processed to completion.", Array.Empty<object>());
          else
            this.Basis.ReportWarning("At least one line has not been processed to completion. Do you want to confirm the shipment?", Array.Empty<object>());
          return false;
        }
        if (!this.Basis.HasIncompleteLinesBy<PX.Objects.SO.SOShipLineSplit.pickedQty>())
          return true;
        this.Basis.ReportError("The shipment cannot be confirmed because at least one line has not been processed to completion.", Array.Empty<object>());
        return false;
      }

      protected virtual bool CanConfirmPacked()
      {
        PX.Objects.SO.SOShipLineSplit[] source = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Basis.Get<PickPackShip.PackMode.Logic>().PickedForPack).SelectMain(Array.Empty<object>());
        if (((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).All<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? packedQty = s.PackedQty;
          Decimal num = 0M;
          return packedQty.GetValueOrDefault() == num & packedQty.HasValue;
        })))
          return true;
        if (((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType != "WRN" && ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) source).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          Decimal? packedQty = s.PackedQty;
          Decimal? qty = s.Qty;
          Decimal minQtyThreshold = this.Basis.Graph.GetMinQtyThreshold(s);
          Decimal? nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * minQtyThreshold) : new Decimal?();
          return packedQty.GetValueOrDefault() < nullable.GetValueOrDefault() & packedQty.HasValue & nullable.HasValue;
        })))
        {
          if (this.Basis.CannotConfirmPartialShipments)
            this.Basis.ReportError("The shipment cannot be confirmed because at least one line has not been processed to completion.", Array.Empty<object>());
          else
            this.Basis.ReportWarning("At least one line has not been processed to completion. Do you want to confirm the shipment?", Array.Empty<object>());
          return false;
        }
        if (!this.Basis.HasIncompleteLinesBy<PX.Objects.SO.SOShipLineSplit.packedQty>())
          return true;
        this.Basis.ReportError("The shipment cannot be confirmed because at least one line has not been processed to completion.", Array.Empty<object>());
        return false;
      }

      [Obsolete("Use the PickPackShip.HasIncompleteLinesBy method instead.")]
      protected virtual bool HasIncompleteLinesBy<TQtyField>() where TQtyField : class, IBqlField, IImplement<IBqlDecimal>
      {
        return this.Basis.HasIncompleteLinesBy<TQtyField>();
      }
    }

    public class PickPackShipShipmentConfirmation : PXGraphExtension<SOShipmentEntry>
    {
      public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>();

      public virtual async System.Threading.Tasks.Task ApplyPickedQtyAndConfirmShipment(
        string shipmentNbr,
        bool confirmAsIs,
        SOPickPackShipSetup setup,
        SOPickPackShipUserSetup userSetup,
        SOPackageDetailEx autoPackageToConfirm,
        CancellationToken cancellationToken)
      {
        PickPackShip.ConfirmShipmentCommand.PickPackShipShipmentConfirmation shipmentConfirmation = this;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentConfirmation.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) shipmentConfirmation.Base, new object[1]
            {
              (object) shipmentNbr
            }));
            shipmentConfirmation.CloseShipmentUserLinks(shipmentNbr);
            shipmentConfirmation.ApplyPickedQty(confirmAsIs, setup);
            shipmentConfirmation.HandleCarts();
            if (((PXGraph) shipmentConfirmation.Base).IsDirty)
            {
              Decimal? shipmentQty = ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentConfirmation.Base.Document).Current.ShipmentQty;
              Decimal num = 0M;
              if (shipmentQty.GetValueOrDefault() == num & shipmentQty.HasValue)
                throw new PXException("Unable to confirm zero shipment {0}.", new object[1]
                {
                  (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentConfirmation.Base.Document).Current.ShipmentNbr
                });
            }
            shipmentConfirmation.HandlePackages(confirmAsIs, setup, autoPackageToConfirm);
            if (((PXGraph) shipmentConfirmation.Base).IsDirty)
              ((PXAction) shipmentConfirmation.Base.Save).Press();
            shipmentConfirmation.TryUseExternalConfirmation();
            ((PXAction) ((PXGraph) shipmentConfirmation.Base).GetExtension<ConfirmShipmentExtension>().confirmShipmentAction).Press();
            ((PXGraph) shipmentConfirmation.Base).Clear();
            ((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentConfirmation.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) shipmentConfirmation.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
            transactionScope.Complete((PXGraph) shipmentConfirmation.Base);
          }
          catch (PXBaseRedirectException ex)
          {
            transactionScope.Complete((PXGraph) shipmentConfirmation.Base);
            throw;
          }
        }
        int num1 = await shipmentConfirmation.TryPrintShipmentForms(userSetup, cancellationToken) ? 1 : 0;
      }

      protected virtual void CloseShipmentUserLinks(string shipmentNbr)
      {
        this.Base.WorkLogExt?.CloseFor(shipmentNbr);
      }

      protected virtual void ApplyPickedQty(bool confirmAsIs, SOPickPackShipSetup setup)
      {
        NonStockKitSpecHelper stockKitSpecHelper = new NonStockKitSpecHelper((PXGraph) this.Base);
        Func<int, bool> RequireShipping = Func.Memorize<int, bool>((Func<int, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(inventoryID)).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (item => item.StkItem.GetValueOrDefault() || item.NonStockShip.GetValueOrDefault()))));
        if (confirmAsIs)
          return;
        bool? nullable = setup.ShowPickTab;
        if (!nullable.GetValueOrDefault())
        {
          nullable = setup.ShowPackTab;
          if (!nullable.GetValueOrDefault())
            return;
        }
        PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) this.Base.Transactions;
        PXSelectBase<PX.Objects.SO.SOShipLineSplit> splits = (PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Base.splits;
        foreach (PXResult<SOShipLine> pxResult1 in transactions.Select(Array.Empty<object>()))
        {
          SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult1);
          transactions.Current = soShipLine;
          Decimal num1 = 0M;
          Decimal num2;
          if (stockKitSpecHelper.IsNonStockKit(soShipLine.InventoryID))
          {
            Dictionary<int, Decimal> dictionary1 = EnumerableExtensions.ToDictionary<int, Decimal>(stockKitSpecHelper.GetNonStockKitSpec(soShipLine.InventoryID.Value).Where<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, bool>) (pair => RequireShipping(pair.Key))));
            Dictionary<int, Decimal> dictionary2 = ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) splits.SelectMain(Array.Empty<object>())).GroupBy<PX.Objects.SO.SOShipLineSplit, int>((Func<PX.Objects.SO.SOShipLineSplit, int>) (r => r.InventoryID.Value)).ToDictionary<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, int, Decimal>((Func<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, int>) (g => g.Key), (Func<IGrouping<int, PX.Objects.SO.SOShipLineSplit>, Decimal>) (g => g.Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal>) (s => GetNewQty(s)))));
            num2 = dictionary1.Keys.Count<int>() == 0 || dictionary1.Keys.Except<int>((IEnumerable<int>) dictionary2.Keys).Count<int>() > 0 ? 0M : dictionary2.Join<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) dictionary1, (Func<KeyValuePair<int, Decimal>, int>) (split => split.Key), (Func<KeyValuePair<int, Decimal>, int>) (spec => spec.Key), (Func<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, Decimal>) ((split, spec) =>
            {
              KeyValuePair<int, Decimal> keyValuePair = split;
              Decimal d1 = keyValuePair.Value;
              keyValuePair = spec;
              Decimal d2 = keyValuePair.Value;
              return Math.Floor(Decimal.Divide(d1, d2));
            })).Min();
          }
          else
          {
            using (new UpdateIfFieldsChangedScope().AppendContext(typeof (SOShipLine.locationID)))
            {
              foreach (PXResult<PX.Objects.SO.SOShipLineSplit> pxResult2 in splits.Select(Array.Empty<object>()))
              {
                PX.Objects.SO.SOShipLineSplit soShipLineSplit = PXResult<PX.Objects.SO.SOShipLineSplit>.op_Implicit(pxResult2);
                splits.Current = soShipLineSplit;
                Decimal newQty = GetNewQty(splits.Current);
                Decimal num3 = newQty;
                Decimal? qty = splits.Current.Qty;
                Decimal valueOrDefault1 = qty.GetValueOrDefault();
                if (!(num3 == valueOrDefault1 & qty.HasValue))
                {
                  splits.Current.Qty = new Decimal?(newQty);
                  splits.UpdateCurrent();
                }
                qty = splits.Current.Qty;
                Decimal num4 = 0M;
                if (!(qty.GetValueOrDefault() == num4 & qty.HasValue))
                {
                  Decimal num5 = num1;
                  qty = splits.Current.Qty;
                  Decimal valueOrDefault2 = qty.GetValueOrDefault();
                  num1 = num5 + valueOrDefault2;
                }
              }
            }
            num2 = INUnitAttribute.ConvertFromBase(((PXSelectBase) transactions).Cache, transactions.Current.InventoryID, transactions.Current.UOM, num1, INPrecision.NOROUND);
          }
          transactions.Current.Qty = new Decimal?(num2);
          transactions.UpdateCurrent();
          PXSelectBase<SOSetup> sosetup = (PXSelectBase<SOSetup>) this.Base.sosetup;
          Decimal? qty1 = transactions.Current.Qty;
          Decimal num6 = 0M;
          if (qty1.GetValueOrDefault() == num6 & qty1.HasValue)
          {
            bool? addAllToShipment = sosetup.Current.AddAllToShipment;
            bool flag = false;
            if (addAllToShipment.GetValueOrDefault() == flag & addAllToShipment.HasValue)
              transactions.DeleteCurrent();
          }
        }

        Decimal GetNewQty(PX.Objects.SO.SOShipLineSplit split)
        {
          if (setup.ShowPickTab.GetValueOrDefault())
            return split.PickedQty.GetValueOrDefault();
          Decimal? nullable = split.PickedQty;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = split.PackedQty;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          return Math.Max(valueOrDefault1, valueOrDefault2);
        }
      }

      protected virtual void HandleCarts()
      {
        foreach (PXResult<SOCartShipment> pxResult in PXSelectBase<SOCartShipment, PXViewOf<SOCartShipment>.BasedOn<SelectFromBase<SOCartShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOCartShipment.shipmentNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
        {
          SOCartShipment soCartShipment = PXResult<SOCartShipment>.op_Implicit(pxResult);
          GraphHelper.Caches<SOCartShipment>((PXGraph) this.Base).Delete(soCartShipment);
        }
      }

      protected virtual void HandlePackages(
        bool confirmAsIs,
        SOPickPackShipSetup setup,
        SOPackageDetailEx autoPackageToConfirm)
      {
        if (!confirmAsIs && (setup.ShowPickTab.GetValueOrDefault() || setup.ShowPackTab.GetValueOrDefault()))
        {
          foreach (SOPackageDetailEx soPackageDetailEx in ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).SelectMain(Array.Empty<object>()))
          {
            if (soPackageDetailEx.PackageType == "M")
            {
              if (((PXSelectBase<SOShipLineSplitPackage>) this.Base.PackageDetailExt.PackageDetailSplit).Select(new object[2]
              {
                (object) soPackageDetailEx.ShipmentNbr,
                (object) soPackageDetailEx.LineNbr
              }).Count == 0)
                ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Delete(soPackageDetailEx);
            }
          }
        }
        if (autoPackageToConfirm != null)
        {
          bool? confirmed = autoPackageToConfirm.Confirmed;
          bool flag = false;
          if (confirmed.GetValueOrDefault() == flag & confirmed.HasValue)
          {
            autoPackageToConfirm.Confirmed = new bool?(true);
            ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Update(autoPackageToConfirm);
          }
        }
        if (PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
        {
          SOPackageDetailEx[] source = ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).SelectMain(Array.Empty<object>());
          if (confirmAsIs)
          {
            foreach (SOPackageDetailEx soPackageDetailEx in ((IEnumerable<SOPackageDetailEx>) source).Where<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (x => !x.Confirmed.GetValueOrDefault())))
            {
              soPackageDetailEx.Confirmed = new bool?(true);
              ((PXSelectBase) this.Base.Packages).Cache.Update((object) soPackageDetailEx);
            }
          }
          bool? isPackageValid = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.IsPackageValid;
          bool flag = false;
          if (isPackageValid.GetValueOrDefault() == flag & isPackageValid.HasValue && ((IEnumerable<SOPackageDetailEx>) source).Any<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (p => p.PackageType == "A")))
          {
            ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.IsPackageValid = new bool?(true);
            ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).UpdateCurrent();
          }
        }
        if (!((PXGraph) this.Base).IsDirty)
          return;
        ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.IsPackageValid = new bool?(true);
        ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).UpdateCurrent();
        ((PXAction) this.Base.Save).Press();
      }

      protected virtual void TryUseExternalConfirmation()
      {
        PX.Objects.CS.Carrier carrier;
        if (this.UseExternalShippingApplication(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, out carrier))
          throw new PXRedirectToUrlException($"../../Frames/ShipmentAppLauncher.html?ShipmentApplicationType={carrier.ShippingApplicationType}&ShipmentNbr={((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipmentNbr}", (PXBaseRedirectException.WindowMode) 3, true, string.Empty);
      }

      public virtual async Task<bool> TryPrintShipmentForms(
        SOPickPackShipUserSetup userSetup,
        CancellationToken cancellationToken)
      {
        PickPackShip.ConfirmShipmentCommand.PickPackShipShipmentConfirmation shipmentConfirmation = this;
        bool anyPrinted = false;
        if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
        {
          LabelsPrinting labelsPrintingExt = ((PXGraph) shipmentConfirmation.Base).GetExtension<LabelsPrinting>();
          if (userSetup.PrintShipmentLabels.GetValueOrDefault())
          {
            try
            {
              await labelsPrintingExt.PrintCarrierLabels(cancellationToken);
            }
            catch (PXBaseRedirectException ex)
            {
            }
          }
          if (userSetup.PrintCommercialInvoices.GetValueOrDefault())
          {
            try
            {
              await labelsPrintingExt.PrintCommercInvoices(cancellationToken);
              anyPrinted = true;
            }
            catch (PXBaseRedirectException ex)
            {
            }
          }
          if (userSetup.PrintShipmentConfirmation.GetValueOrDefault())
          {
            // ISSUE: reference to a compiler-generated method
            BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.WithSuppressedRedirects(new Action(shipmentConfirmation.\u003CTryPrintShipmentForms\u003Eb__7_0));
            anyPrinted = true;
          }
          labelsPrintingExt = (LabelsPrinting) null;
        }
        return anyPrinted;
      }

      protected virtual bool UseExternalShippingApplication(
        PX.Objects.SO.SOShipment shipment,
        out PX.Objects.CS.Carrier carrier)
      {
        carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, shipment.ShipVia);
        return !((PXGraph) this.Base).IsMobile && carrier != null && carrier.IsExternalShippingApplication.GetValueOrDefault();
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Confirm Shipment";
      public const string InProcess = "Confirmation of {0} shipment in progress.";
      public const string Success = "Shipment successfully confirmed.";
      public const string Fail = "Shipment not confirmed.";
      public const string ShipmentCannotBeConfirmed = "The shipment cannot be confirmed because no items have been picked.";
      public const string ShipmentCannotBeConfirmedNoPacked = "The shipment cannot be confirmed because no items have been packed.";
      public const string ShipmentCannotBeConfirmedInPart = "The shipment cannot be confirmed because at least one line has not been processed to completion.";
      public const string ShipmentShouldNotBeConfirmedInPart = "At least one line has not been processed to completion. Do you want to confirm the shipment?";
    }
  }

  public sealed class ConfirmShipmentAsIsCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "CONFIRM*SHIPMENT*ALL";

    public virtual string ButtonName => "scanConfirmShipmentAll";

    public virtual string DisplayName => "Confirm Shipment As Is";

    protected virtual bool IsEnabled
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
    }

    protected virtual bool Process()
    {
      return ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.ConfirmShipmentCommand.Logic>().ConfirmShipment(true);
    }

    [PXLocalizable]
    public abstract class Msg : PickPackShip.ConfirmShipmentCommand.Msg
    {
      public new const string DisplayName = "Confirm Shipment As Is";
    }
  }

  [PXLocalizable]
  public new abstract class Msg : WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.Msg
  {
    public const string ShipmentIsNotEditable = "The document has become unavailable for editing. Contact your manager.";
    public const string InventoryMissingInShipment = "{0} item not listed in shipment.";
    public const string LocationMissingInShipment = "{0} location not listed in shipment.";
    public const string LotSerialMissingInShipment = "{0} lot or serial number not listed in shipment.";
    public const string ShipmentContainsNonStockItemWithEmptyLocation = "The {0} shipment cannot be processed on the Pick, Pack, and Ship (SO302020) form because it contains a non-stock item with an empty location.";
  }

  public static class FieldAttached
  {
    public abstract class To<TTable> : PXFieldAttachedTo<TTable>.By<PickPackShip.Host> where TTable : class, IBqlTable, new()
    {
    }

    [PXUIField(DisplayName = "Matched")]
    public class Fits : 
      PXFieldAttachedTo<PX.Objects.SO.SOShipLineSplit>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.FieldAttached.Fits>
    {
      public override bool? GetValue(PX.Objects.SO.SOShipLineSplit row)
      {
        bool flag = true;
        int? nullable1;
        if (this.Base.WMS.LocationID.HasValue)
        {
          int num1 = flag ? 1 : 0;
          nullable1 = this.Base.WMS.LocationID;
          int? locationId = row.LocationID;
          int num2 = nullable1.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable1.HasValue == locationId.HasValue ? 1 : 0;
          flag = (num1 & num2) != 0;
        }
        if (this.Base.WMS.InventoryID.HasValue)
        {
          int num3 = flag ? 1 : 0;
          int? nullable2 = this.Base.WMS.InventoryID;
          nullable1 = row.InventoryID;
          int num4;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = this.Base.WMS.SubItemID;
            nullable2 = row.SubItemID;
            num4 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
          }
          else
            num4 = 0;
          flag = (num3 & num4) != 0;
        }
        if (this.Base.WMS.LotSerialNbr != null)
        {
          int num5 = flag ? 1 : 0;
          int num6;
          if (!string.Equals(this.Base.WMS.LotSerialNbr, row.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
          {
            if (this.Base.WMS.Header.Mode == "PICK" && this.Base.WMS.LotSerialTrack.IsEnterable)
            {
              Decimal? pickedQty = row.PickedQty;
              Decimal num7 = 0M;
              num6 = pickedQty.GetValueOrDefault() == num7 & pickedQty.HasValue ? 1 : 0;
            }
            else
              num6 = 0;
          }
          else
            num6 = 1;
          flag = (num5 & num6) != 0;
        }
        return new bool?(flag);
      }
    }

    [PXUIField(Visible = false)]
    public class ShowLog : 
      PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<PickPackShip.FieldAttached.ShowLog>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<SOPickPackShipSetup>) this.Base.WMS.Setup).Current.ShowScanLogTab.GetValueOrDefault());
      }
    }
  }
}
