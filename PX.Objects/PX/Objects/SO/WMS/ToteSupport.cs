// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.ToteSupport
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
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO.WMS;

public class ToteSupport : WorksheetPicking.ScanExtension
{
  public static bool IsActive() => WorksheetPicking.ScanExtension.IsActiveBase();

  public 
  #nullable disable
  ToteScanHeader ToteHeader
  {
    get => ScanHeaderExt.Get<ToteScanHeader>(this.Basis.Header) ?? new ToteScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<ToteScanHeader> ToteSetter
  {
    get => this.Basis.HeaderSetter.With<ToteScanHeader>();
  }

  public bool? AddNewTote
  {
    get => this.ToteHeader.AddNewTote;
    set
    {
      ValueSetter<ScanHeader>.Ext<ToteScanHeader> toteSetter = this.ToteSetter;
      (^ref toteSetter).Set<bool?>((Expression<Func<ToteScanHeader, bool?>>) (h => h.AddNewTote), value);
    }
  }

  public int? ToteID
  {
    get => this.ToteHeader.ToteID;
    set
    {
      ValueSetter<ScanHeader>.Ext<ToteScanHeader> toteSetter = this.ToteSetter;
      (^ref toteSetter).Set<int?>((Expression<Func<ToteScanHeader, int?>>) (h => h.ToteID), value);
    }
  }

  public ISet<int> PreparedForPackToteIDs => (ISet<int>) this.ToteHeader.PreparedForPackToteIDs;

  public string ShipmentJustAssignedWithTote { get; set; }

  public virtual bool AllowMultipleTotesPerShipment
  {
    get
    {
      return ((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.AllowMultipleTotesPerShipment.GetValueOrDefault();
    }
  }

  public virtual bool CanAddNewTote
  {
    get
    {
      return this.AllowMultipleTotesPerShipment && this.WSBasis.CanWSPick && this.NoEmptyTotes && !this.HasAnotherToAssign;
    }
  }

  public virtual bool NoEmptyTotes
  {
    get
    {
      return ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).GroupBy<SOPickerListEntry, int?>((Func<SOPickerListEntry, int?>) (s => s.ToteID)).All<IGrouping<int?, SOPickerListEntry>>((Func<IGrouping<int?, SOPickerListEntry>, bool>) (tote => tote.Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
      {
        Decimal? pickedQty = s.PickedQty;
        Decimal num = 0M;
        return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue || s.ForceCompleted.GetValueOrDefault();
      }))));
    }
  }

  public virtual bool IsToteSelectionNeeded => false;

  public virtual bool HasAnotherToAssign => this.NextShipmentWithoutTote != null;

  public virtual SOPickerToShipmentLink NextShipmentWithoutTote
  {
    get
    {
      return ((IEnumerable<SOPickerToShipmentLink>) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SelectMain(Array.Empty<object>())).FirstOrDefault<SOPickerToShipmentLink>((Func<SOPickerToShipmentLink, bool>) (s =>
      {
        int? toteId = s.ToteID;
        int num = 0;
        return toteId.GetValueOrDefault() == num & toteId.HasValue;
      }));
    }
  }

  public virtual bool HasAnotherToPrepare
  {
    get => this.TotesToPrepareForPack.Length - this.PreparedForPackToteIDs.Count > 0;
  }

  public virtual INTote[] TotesToPrepareForPack
  {
    get
    {
      return GraphHelper.RowCast<INTote>((IEnumerable) PXSelectBase<INTote, PXViewOf<INTote>.BasedOn<SelectFromBase<INTote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerToShipmentLink>.On<SOPickerToShipmentLink.FK.Tote>>>.Where<BqlOperand<SOPickerToShipmentLink.shipmentNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), Array.Empty<object>())).ToArray<INTote>();
    }
  }

  public virtual string GetShipmentToAddToteTo() => this.Basis.RefNbr;

  public virtual bool TryAssignTotesFromCart(string barcode)
  {
    INCart inCart = PXResultset<INCart>.op_Implicit(PXSelectBase<INCart, PXViewOf<INCart>.BasedOn<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<INCart.FK.Site>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCart.siteID, Equal<BqlField<WMSScanHeader.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INCart.cartCD, IBqlString>.IsEqual<P.AsString>>>>.And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
    {
      (object) barcode
    }));
    if (inCart == null)
      return false;
    SOPickerToShipmentLink[] pickerToShipmentLinkArray = ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SelectMain(Array.Empty<object>());
    if (((IEnumerable<SOPickerToShipmentLink>) pickerToShipmentLinkArray).Any<SOPickerToShipmentLink>((Func<SOPickerToShipmentLink, bool>) (s =>
    {
      int? toteId = s.ToteID;
      int num = 0;
      return !(toteId.GetValueOrDefault() == num & toteId.HasValue);
    })))
    {
      this.Basis.Reporter.Error("Totes from the {0} cart cannot be auto assigned to the pick list because it already has manual assignments.", new object[1]
      {
        (object) inCart.CartCD
      });
      return true;
    }
    if (((IQueryable<PXResult<SOPickerToShipmentLink>>) PXSelectBase<SOPickerToShipmentLink, PXViewOf<SOPickerToShipmentLink>.BasedOn<SelectFromBase<SOPickerToShipmentLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPickerToShipmentLink.FK.Worksheet>>, FbqlJoins.Inner<INTote>.On<SOPickerToShipmentLink.FK.Tote>>, FbqlJoins.Inner<INCart>.On<INTote.FK.Cart>>, FbqlJoins.Inner<PX.Objects.SO.SOShipment>.On<SOPickerToShipmentLink.FK.Shipment>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, Equal<SOPickingWorksheet.worksheetType.wave>>>>, And<BqlOperand<PX.Objects.SO.SOShipment.confirmed, IBqlBool>.IsEqual<False>>>, And<BqlOperand<INCart.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INCart.cartID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[2]
    {
      (object) inCart.SiteID,
      (object) inCart.CartID
    })).Any<PXResult<SOPickerToShipmentLink>>())
    {
      this.Basis.Reporter.Error("The {0} cart is already in use.", new object[1]
      {
        (object) inCart.CartCD
      });
      return true;
    }
    INTote[] array = KeysRelation<CompositeKey<Field<INTote.siteID>.IsRelatedTo<INCart.siteID>, Field<INTote.assignedCartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INTote>, INCart, INTote>.SelectChildren(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), inCart).Where<INTote>((Func<INTote, bool>) (t => t.Active.GetValueOrDefault())).ToArray<INTote>();
    if (pickerToShipmentLinkArray.Length > array.Length)
    {
      this.Basis.Reporter.Error("There are not enough active totes in the {0} cart to assign them to all of the shipments of the pick list.", new object[1]
      {
        (object) inCart.CartCD
      });
      return true;
    }
    foreach ((SOPickerToShipmentLink, INTote) tuple in ((IEnumerable<SOPickerToShipmentLink>) pickerToShipmentLinkArray).Zip<SOPickerToShipmentLink, INTote, (SOPickerToShipmentLink, INTote)>((IEnumerable<INTote>) array, (Func<SOPickerToShipmentLink, INTote, (SOPickerToShipmentLink, INTote)>) ((link, tote) => (link, tote))))
      this.AssignTote(tuple.Item1, tuple.Item2);
    ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current.CartID = inCart.CartID;
    ((PXSelectBase<SOPicker>) this.WSBasis.Picker).UpdateCurrent();
    this.Basis.SaveChanges();
    PPSCartSupport ppsCartSupport = this.Basis.Get<PPSCartSupport>();
    if (ppsCartSupport != null)
      ppsCartSupport.CartID = inCart.CartID;
    this.Basis.DispatchNext("The {0} first totes from the {1} cart were automatically assigned to the shipments of the pick list.", new object[2]
    {
      (object) pickerToShipmentLinkArray.Length,
      (object) inCart.CartCD
    });
    return true;
  }

  public virtual void AssignTote(INTote tote)
  {
    if (this.AddNewTote.GetValueOrDefault())
    {
      if (!this.AllowMultipleTotesPerShipment)
        throw new InvalidOperationException();
      this.ShipmentJustAssignedWithTote = this.GetShipmentToAddToteTo();
      if (this.ShipmentJustAssignedWithTote == null)
        throw new InvalidOperationException();
      this.AssignTote(new SOPickerToShipmentLink()
      {
        WorksheetNbr = this.WSBasis.WorksheetNbr,
        PickerNbr = this.WSBasis.PickerNbr,
        ShipmentNbr = this.ShipmentJustAssignedWithTote,
        SiteID = this.Basis.SiteID
      }, tote);
      this.AddNewTote = new bool?(false);
    }
    else
    {
      SOPickerToShipmentLink shipmentWithoutTote = this.NextShipmentWithoutTote;
      this.ShipmentJustAssignedWithTote = shipmentWithoutTote != null ? shipmentWithoutTote.ShipmentNbr : throw new InvalidOperationException();
      this.AssignTote(shipmentWithoutTote, tote);
      this.WSBasis.AssignUser();
    }
    this.Basis.SaveChanges();
  }

  public virtual void AssignTote(SOPickerToShipmentLink link, INTote tote)
  {
    SOPickerToShipmentLink pickerToShipmentLink = ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Locate(link);
    if (pickerToShipmentLink != null)
    {
      SOPickerToShipmentLink copy = PXCache<SOPickerToShipmentLink>.CreateCopy(pickerToShipmentLink);
      copy.ToteID = tote.ToteID;
      ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Delete(pickerToShipmentLink);
      ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Insert(copy);
    }
    else
    {
      link.ToteID = tote.ToteID;
      ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Insert(link);
    }
    foreach (PXResult<SOPickerListEntry> pxResult in ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.shipmentNbr>>((object[]) new string[1]
    {
      link.ShipmentNbr
    }, Array.Empty<object>()))
    {
      SOPickerListEntry split = PXResult<SOPickerListEntry>.op_Implicit(pxResult);
      Decimal? pickedQty = split.PickedQty;
      Decimal? qty = split.Qty;
      if (pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue)
        this.MoveSplitRestQtyToAnotherTote(split, tote.ToteID);
    }
  }

  public virtual SOPickerListEntry EnsureSplitQtyInTote(
    SOPickerListEntry pickedSplit,
    Decimal deltaQty)
  {
    if (pickedSplit != null && this.ToteID.HasValue && deltaQty > 0M)
    {
      int? toteId1 = pickedSplit.ToteID;
      int? toteId2 = this.ToteID;
      Decimal? nullable1;
      Decimal? nullable2;
      if (!(toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue))
      {
        Decimal? pickedQty = pickedSplit.PickedQty;
        Decimal num = deltaQty;
        nullable1 = pickedQty.HasValue ? new Decimal?(pickedQty.GetValueOrDefault() + num) : new Decimal?();
        nullable2 = pickedSplit.Qty;
        if (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        {
          pickedSplit = this.MoveSplitRestQtyToAnotherTote(pickedSplit, this.ToteID);
          goto label_8;
        }
      }
      int? toteId3 = pickedSplit.ToteID;
      toteId1 = this.ToteID;
      if (toteId3.GetValueOrDefault() == toteId1.GetValueOrDefault() & toteId3.HasValue == toteId1.HasValue)
      {
        Decimal? pickedQty = pickedSplit.PickedQty;
        Decimal num1 = deltaQty;
        nullable2 = pickedQty.HasValue ? new Decimal?(pickedQty.GetValueOrDefault() + num1) : new Decimal?();
        nullable1 = pickedSplit.Qty;
        if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
        {
          SOPickerListEntry split = pickedSplit;
          Decimal num2 = deltaQty;
          nullable1 = pickedSplit.Qty;
          Decimal num3 = nullable1.Value;
          nullable1 = pickedSplit.PickedQty;
          Decimal num4 = nullable1.Value;
          Decimal num5 = num3 - num4;
          Decimal qtyToBorrow = num2 - num5;
          if (this.TryBorrowMissingQtyFromSimilarSplitInAnotherTote(split, qtyToBorrow))
            pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Locate(pickedSplit);
        }
      }
    }
label_8:
    return pickedSplit;
  }

  public virtual bool TryRemoveTote(INTote tote)
  {
    return this.TryRemoveToteOf(PXResultset<SOPickerToShipmentLink>.op_Implicit(((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Search<SOPickerToShipmentLink.toteID>((object) tote.ToteID, Array.Empty<object>())));
  }

  public virtual bool TryRemoveToteOf(SOPickerToShipmentLink link)
  {
    SOPickerToShipmentLink pickerToShipmentLink = GraphHelper.RowCast<SOPickerToShipmentLink>((IEnumerable) ((IEnumerable<PXResult<SOPickerToShipmentLink>>) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SearchAll<Asc<SOPickerToShipmentLink.shipmentNbr>>((object[]) new string[1]
    {
      link.ShipmentNbr
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOPickerToShipmentLink>>()).Where<SOPickerToShipmentLink>((Func<SOPickerToShipmentLink, bool>) (l =>
    {
      int? toteId1 = l.ToteID;
      int? toteId2 = link.ToteID;
      return !(toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue);
    })).OrderByDescending<SOPickerToShipmentLink, DateTime?>((Func<SOPickerToShipmentLink, DateTime?>) (l => l.LastModifiedDateTime)).FirstOrDefault<SOPickerToShipmentLink>();
    if (pickerToShipmentLink == null)
      return false;
    SOPickerListEntry[] array1 = GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) ((IEnumerable<PXResult<SOPickerListEntry>>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.shipmentNbr, Asc<SOPickerListEntry.toteID>>>(new object[2]
    {
      (object) link.ShipmentNbr,
      (object) link.ToteID
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOPickerListEntry>>()).ToArray<SOPickerListEntry>();
    SOPickerListEntry[] array2 = GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) ((IEnumerable<PXResult<SOPickerListEntry>>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.shipmentNbr, Asc<SOPickerListEntry.toteID>>>(new object[2]
    {
      (object) pickerToShipmentLink.ShipmentNbr,
      (object) pickerToShipmentLink.ToteID
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOPickerListEntry>>()).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (r => !r.ForceCompleted.GetValueOrDefault())).ToArray<SOPickerListEntry>();
    foreach (SOPickerListEntry soPickerListEntry1 in array1)
    {
      SOPickerListEntry entry = soPickerListEntry1;
      Decimal? nullable = entry.PickedQty;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        throw new InvalidOperationException();
      SOPickerListEntry soPickerListEntry2 = ((IEnumerable<SOPickerListEntry>) array2).FirstOrDefault<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (ps =>
      {
        if (this.WSBasis.AreSplitsSimilar(ps, entry))
        {
          bool? isUnassigned1 = ps.IsUnassigned;
          bool? isUnassigned2 = entry.IsUnassigned;
          if (isUnassigned1.GetValueOrDefault() == isUnassigned2.GetValueOrDefault() & isUnassigned1.HasValue == isUnassigned2.HasValue)
          {
            bool? generatedLotSerialNbr1 = ps.HasGeneratedLotSerialNbr;
            bool? generatedLotSerialNbr2 = entry.HasGeneratedLotSerialNbr;
            return generatedLotSerialNbr1.GetValueOrDefault() == generatedLotSerialNbr2.GetValueOrDefault() & generatedLotSerialNbr1.HasValue == generatedLotSerialNbr2.HasValue;
          }
        }
        return false;
      }));
      if (soPickerListEntry2 != null)
      {
        SOPickerListEntry soPickerListEntry3 = soPickerListEntry2;
        nullable = soPickerListEntry3.Qty;
        Decimal? qty = entry.Qty;
        soPickerListEntry3.Qty = nullable.HasValue & qty.HasValue ? new Decimal?(nullable.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
        ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Delete(entry);
        ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(soPickerListEntry2);
      }
      else
      {
        entry.ToteID = pickerToShipmentLink.ToteID;
        ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(entry);
      }
    }
    ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Delete(link);
    return true;
  }

  public virtual INTote GetProperTote()
  {
    return this.GetToteForPickListEntry(this.WSBasis.GetEntriesToPick().FirstOrDefault<SOPickerListEntry>());
  }

  public virtual INTote GetToteForPickListEntry(SOPickerListEntry entry, bool certain = false)
  {
    if (entry == null)
      return (INTote) null;
    if (certain)
      return INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), entry.SiteID, entry.ToteID);
    PXResultset<INTote> pxResultset = PXSelectBase<INTote, PXViewOf<INTote>.BasedOn<SelectFromBase<INTote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerToShipmentLink>.On<SOPickerToShipmentLink.FK.Tote>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerToShipmentLink.worksheetNbr, Equal<BqlField<SOPickerListEntry.worksheetNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOPickerToShipmentLink.pickerNbr, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.pickerNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<SOPickerToShipmentLink.shipmentNbr, IBqlString>.IsEqual<BqlField<SOPickerListEntry.shipmentNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
    {
      (object) entry
    }, Array.Empty<object>());
    return pxResultset.Count == 1 ? PXResultset<INTote>.op_Implicit(pxResultset) : (INTote) null;
  }

  public virtual SOPickerListEntry MoveSplitRestQtyToAnotherTote(
    SOPickerListEntry split,
    int? toteID)
  {
    SOPickerToShipmentLink pickerToShipmentLink = PXResultset<SOPickerToShipmentLink>.op_Implicit(((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Search<SOPickerToShipmentLink.toteID>((object) toteID, Array.Empty<object>()));
    if (pickerToShipmentLink == null || pickerToShipmentLink.ShipmentNbr != split.ShipmentNbr)
      return split;
    Decimal? pickedQty1 = split.PickedQty;
    Decimal num = 0M;
    if (pickedQty1.GetValueOrDefault() == num & pickedQty1.HasValue)
    {
      split.ToteID = toteID;
      return ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(split);
    }
    SOPickerListEntry copy1 = PXCache<SOPickerListEntry>.CreateCopy(split);
    copy1.EntryNbr = new int?();
    copy1.Qty = copy1.PickedQty;
    SOPickerListEntry copy2 = PXCache<SOPickerListEntry>.CreateCopy(split);
    copy2.EntryNbr = new int?();
    SOPickerListEntry soPickerListEntry = copy2;
    Decimal? qty = split.Qty;
    Decimal? pickedQty2 = split.PickedQty;
    Decimal? nullable = qty.HasValue & pickedQty2.HasValue ? new Decimal?(qty.GetValueOrDefault() - pickedQty2.GetValueOrDefault()) : new Decimal?();
    soPickerListEntry.Qty = nullable;
    copy2.PickedQty = new Decimal?(0M);
    copy2.ToteID = toteID;
    ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Delete(split);
    ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Insert(copy1);
    return ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Insert(copy2);
  }

  public virtual bool TryBorrowMissingQtyFromSimilarSplitInAnotherTote(
    SOPickerListEntry split,
    Decimal qtyToBorrow)
  {
    SOPickerListEntry soPickerListEntry1 = ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (r =>
    {
      if (this.WSBasis.AreSplitsSimilar(r, split))
      {
        int? toteId1 = r.ToteID;
        int? toteId2 = split.ToteID;
        if (!(toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue))
        {
          Decimal? qty = r.Qty;
          Decimal? pickedQty = r.PickedQty;
          Decimal? nullable = qty.HasValue & pickedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault()) : new Decimal?();
          Decimal num = qtyToBorrow;
          return nullable.GetValueOrDefault() >= num & nullable.HasValue;
        }
      }
      return false;
    })).FirstOrDefault<SOPickerListEntry>();
    if (soPickerListEntry1 == null)
      return false;
    Decimal? pickedQty1 = soPickerListEntry1.PickedQty;
    Decimal num1 = 0M;
    if (pickedQty1.GetValueOrDefault() == num1 & pickedQty1.HasValue)
    {
      Decimal num2 = qtyToBorrow;
      Decimal? qty = soPickerListEntry1.Qty;
      Decimal valueOrDefault = qty.GetValueOrDefault();
      if (num2 == valueOrDefault & qty.HasValue)
      {
        ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Delete(soPickerListEntry1);
        goto label_5;
      }
    }
    SOPickerListEntry soPickerListEntry2 = soPickerListEntry1;
    Decimal? qty1 = soPickerListEntry2.Qty;
    Decimal num3 = qtyToBorrow;
    soPickerListEntry2.Qty = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() - num3) : new Decimal?();
    ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(soPickerListEntry1);
label_5:
    SOPickerListEntry soPickerListEntry3 = split;
    qty1 = soPickerListEntry3.Qty;
    Decimal num4 = qtyToBorrow;
    soPickerListEntry3.Qty = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + num4) : new Decimal?();
    split = ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(split);
    return true;
  }

  public virtual void InjectRemoveMovesToRemoveFromTote(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove)
  {
    ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfFunc<bool>) ((ScanCommand<PickPackShip>) remove).Intercept.Process).ByOverride((Func<PickPackShip, Func<bool>, bool>) ((basis, base_Process) =>
    {
      int num = base_Process() ? 1 : 0;
      if (!basis.Get<ToteSupport>().IsToteSelectionNeeded)
        return num != 0;
      basis.SetScanState<ToteSupport.SelectToteState>((string) null, Array.Empty<object>());
      return num != 0;
    }), new RelativeInject?());
  }

  public virtual void InjectRemoveDisableWhenAssignTote(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove)
  {
    ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfPredicate) ((ScanCommand<PickPackShip>) remove).Intercept.IsEnabled).ByConjoin((Func<PickPackShip, bool>) (basis => !(basis.CurrentState is ToteSupport.AssignToteState)), false, new RelativeInject?());
  }

  public virtual void InjectShipmentAbsenceHandlingByTote(
    PickPackShip.PackMode.ShipmentState packShipment)
  {
    int? initialToteID = new int?();
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfAction) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfAction<PX.Objects.SO.SOShipment>) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) packShipment).Intercept.StatePrompt).ByReplace((Func<string>) (() => "Scan the shipment or tote number."), new RelativeInject?())).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.SO.SOShipment>, string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>) ((basis, barcode) =>
    {
      PXResult<PX.Objects.SO.SOShipment, SOPickerToShipmentLink, INTote> pxResult = (PXResult<PX.Objects.SO.SOShipment, SOPickerToShipmentLink, INTote>) PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerToShipmentLink>.On<SOPickerToShipmentLink.FK.Shipment>>, FbqlJoins.Inner<INTote>.On<SOPickerToShipmentLink.FK.Tote>>, FbqlJoins.Inner<SOPicker>.On<SOPickerToShipmentLink.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTote.toteCD, Equal<P.AsString>>>>, And<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsIn<SOPickingWorksheet.worksheetType.wave, SOPickingWorksheet.worksheetType.single>>>, And<BqlOperand<SOPicker.confirmed, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOShipment.picked, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOShipment.confirmed, IBqlBool>.IsEqual<False>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), new object[1]
      {
        (object) barcode
      }));
      if (pxResult == null)
        return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Skipped);
      initialToteID = ((PXResult) pxResult).GetItem<INTote>().ToteID;
      return AbsenceHandling.ReplaceWith<PX.Objects.SO.SOShipment>(((PXResult) pxResult).GetItem<PX.Objects.SO.SOShipment>());
    }), new RelativeInject?())).Intercept.Apply).ByAppend((Action<PickPackShip, PX.Objects.SO.SOShipment>) ((basis, shipment) =>
    {
      ToteSupport toteSupport = basis.Get<ToteSupport>();
      toteSupport.PreparedForPackToteIDs.Clear();
      if (!initialToteID.HasValue)
        return;
      toteSupport.PreparedForPackToteIDs?.Add(initialToteID.Value);
    }), new RelativeInject?())).Intercept.SetNextState).ByOverride((Action<PickPackShip, Action>) ((basis, base_SetNextState) =>
    {
      if (basis.Get<ToteSupport>().HasAnotherToPrepare)
        basis.SetScanState<ToteSupport.PrepareToteForPackState>((string) null, Array.Empty<object>());
      else
        base_SetNextState();
    }), new RelativeInject?((RelativeInject) 0));
  }

  public virtual void InjectPackPrepareTotesState(PickPackShip.PackMode pack)
  {
    ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfAction<bool>) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanState<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) pack).Intercept.CreateStates).ByAppend((Func<IEnumerable<ScanState<PickPackShip>>>) (() => (IEnumerable<ScanState<PickPackShip>>) new ToteSupport.PrepareToteForPackState[1]
    {
      new ToteSupport.PrepareToteForPackState()
    }), new RelativeInject?())).Intercept.ResetMode).ByAppend((Action<PickPackShip, bool>) ((basis, fullReset) => basis.Clear<ToteSupport.PrepareToteForPackState>(fullReset && !basis.IsWithinReset)), new RelativeInject?());
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.IsSelectedSplit(PX.Objects.SO.SOPickerListEntry)" />
  [PXOverride]
  public bool IsSelectedSplit(
    SOPickerListEntry split,
    Func<SOPickerListEntry, bool> base_IsSelectedSplit)
  {
    if (!base_IsSelectedSplit(split))
      return false;
    int? toteId = split.ToteID;
    int? nullable = this.ToteID ?? split.ToteID;
    return toteId.GetValueOrDefault() == nullable.GetValueOrDefault() & toteId.HasValue == nullable.HasValue;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ConfirmSplit(PX.Objects.SO.SOPickerListEntry,System.Decimal,System.Decimal)" />
  [PXOverride]
  public FlowStatus ConfirmSplit(
    SOPickerListEntry pickedSplit,
    Decimal deltaQty,
    Decimal threshold,
    Func<SOPickerListEntry, Decimal, Decimal, FlowStatus> base_ConfirmSplit)
  {
    pickedSplit = this.EnsureSplitQtyInTote(pickedSplit, deltaQty);
    bool? nullable1;
    int? nullable2;
    if (pickedSplit != null)
    {
      nullable1 = this.Basis.Remove;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && this.ToteID.HasValue)
      {
        int? toteId = pickedSplit.ToteID;
        nullable2 = this.ToteID;
        if (!(toteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & toteId.HasValue == nullable2.HasValue))
        {
          string str = GraphHelper.RowCast<SOPickerToShipmentLink>((IEnumerable) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SearchAll<Asc<SOPickerToShipmentLink.shipmentNbr>>(new object[1]
          {
            (object) pickedSplit.ShipmentNbr
          }, Array.Empty<object>())).Select<SOPickerToShipmentLink, string>((Func<SOPickerToShipmentLink, string>) (link => INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.Basis.SiteID, link.ToteID).ToteCD)).With<IEnumerable<string>, string>((Func<IEnumerable<string>, string>) (tcds => string.Join(", ", tcds)));
          return FlowStatus.Fail("The {0} tote is not assigned to the {1} shipment. Available totes: {2}.", new object[3]
          {
            (object) INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.Basis.SiteID, this.ToteID).ToteCD,
            (object) pickedSplit.ShipmentNbr,
            (object) str
          });
        }
      }
    }
    FlowStatus flowStatus = base_ConfirmSplit(pickedSplit, deltaQty, threshold);
    nullable1 = ((FlowStatus) ref flowStatus).IsError;
    bool flag1 = false;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue && deltaQty < 0M)
    {
      Decimal? pickedQty1 = pickedSplit.PickedQty;
      Decimal num1 = 0M;
      if (pickedQty1.GetValueOrDefault() == num1 & pickedQty1.HasValue)
      {
        int? toteId = pickedSplit.ToteID;
        nullable2 = new int?();
        int? nullable3 = nullable2;
        int? nullable4 = new int?(0);
        if (EnumerableExtensions.IsNotIn<int?>(toteId, nullable3, nullable4))
        {
          if (GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) ((IEnumerable<PXResult<SOPickerListEntry>>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.toteID>>(new object[1]
          {
            (object) pickedSplit.ToteID
          }, Array.Empty<object>())).AsEnumerable<PXResult<SOPickerListEntry>>()).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
          {
            Decimal? pickedQty2 = s.PickedQty;
            Decimal num2 = 0M;
            return pickedQty2.GetValueOrDefault() == num2 & pickedQty2.HasValue;
          })))
            this.TryRemoveTote(INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.Basis.SiteID, pickedSplit.ToteID));
        }
      }
    }
    return flowStatus;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ReportSplitConfirmed(PX.Objects.SO.SOPickerListEntry)" />
  [PXOverride]
  public void ReportSplitConfirmed(
    SOPickerListEntry pickedSplit,
    Action<SOPickerListEntry> base_ReportSplitConfirmed)
  {
    INTote forPickListEntry = this.GetToteForPickListEntry(pickedSplit, true);
    if (forPickListEntry != null)
      this.Basis.ReportInfo(this.Basis.Remove.GetValueOrDefault() ? "{0} x {1} {2} removed from {3} tote." : "{0} x {1} {2} added to {3} tote.", new object[4]
      {
        (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
        (object) this.Basis.Qty,
        (object) this.Basis.UOM,
        (object) forPickListEntry.ToteCD
      });
    else
      base_ReportSplitConfirmed(pickedSplit);
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (!(scanState is PickPackShip.PackMode.ShipmentState packShipment))
      return scanState;
    this.InjectShipmentAbsenceHandlingByTote(packShipment);
    return scanState;
  }

  [PXOverride]
  public ScanMode<PickPackShip> DecorateScanMode(
    ScanMode<PickPackShip> original,
    Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
  {
    ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
    if (!(scanMode is PickPackShip.PackMode pack))
      return scanMode;
    this.InjectPackPrepareTotesState(pack);
    return scanMode;
  }

  public abstract class ToteState : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<INTote>
  {
    public WorksheetPicking WSBasis => this.Get<WorksheetPicking>();

    public ToteSupport ToteBasis => this.Get<ToteSupport>();

    protected virtual INTote GetByBarcode(string barcode)
    {
      return INTote.UK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), ((ScanComponent<PickPackShip>) this).Basis.SiteID, barcode);
    }

    protected virtual Validation Validate(INTote tote)
    {
      bool? active = tote.Active;
      bool flag = false;
      if (!(active.GetValueOrDefault() == flag & active.HasValue))
        return ((EntityState<PickPackShip, INTote>) this).Validate(tote);
      return Validation.Fail("{0} tote inactive.", new object[1]
      {
        (object) tote.ToteCD
      });
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<PickPackShip>) this).Basis.Reporter.Error("{0} tote not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void ReportSuccess(INTote tote)
    {
      ((ScanComponent<PickPackShip>) this).Basis.Reporter.Info("{0} tote selected.", new object[1]
      {
        (object) tote.ToteCD
      });
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Ready = "{0} tote selected.";
      public const string Missing = "{0} tote not found.";
      public const string Inactive = "{0} tote inactive.";
    }
  }

  public sealed class AssignToteState : ToteSupport.ToteState
  {
    public const string Value = "ASST";

    public virtual string Code => "ASST";

    protected virtual string StatePrompt
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Localize("Scan the tote for the {0} shipment.", new object[1]
        {
          (object) this.ToteBasis.With<ToteSupport, string>((Func<ToteSupport, string>) (ts =>
          {
            if (ts.AddNewTote.GetValueOrDefault())
              return ts.GetShipmentToAddToteTo();
            return ts.NextShipmentWithoutTote?.ShipmentNbr;
          }))
        });
      }
    }

    protected virtual bool IsStateSkippable()
    {
      return this.ToteBasis.With<ToteSupport, bool>((Func<ToteSupport, bool>) (ts => !ts.AddNewTote.GetValueOrDefault() && ts.NextShipmentWithoutTote == null));
    }

    protected virtual AbsenceHandling.Of<INTote> HandleAbsence(string barcode)
    {
      return !this.ToteBasis.AddNewTote.GetValueOrDefault() && this.ToteBasis.TryAssignTotesFromCart(barcode) ? AbsenceHandling.Of<INTote>.op_Implicit(AbsenceHandling.Done) : ((EntityState<PickPackShip, INTote>) this).HandleAbsence(barcode);
    }

    protected override Validation Validate(INTote tote)
    {
      Validation validation;
      if (((ScanComponent<PickPackShip>) this).Basis.HasFault<INTote>(tote, new Func<INTote, Validation>(((ToteSupport.ToteState) this).Validate), ref validation))
        return validation;
      if (tote.AssignedCartID.HasValue)
        return Validation.Fail("The {0} tote cannot be used separately from the cart.", new object[1]
        {
          (object) tote.ToteCD
        });
      if (!((IQueryable<PXResult<SOPickerToShipmentLink>>) PXSelectBase<SOPickerToShipmentLink, PXViewOf<SOPickerToShipmentLink>.BasedOn<SelectFromBase<SOPickerToShipmentLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTote>.On<SOPickerToShipmentLink.FK.Tote>>, FbqlJoins.Inner<PX.Objects.SO.SOShipment>.On<SOPickerToShipmentLink.FK.Shipment>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTote.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INTote.toteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<PX.Objects.SO.SOShipment.confirmed, IBqlBool>.IsEqual<False>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[2]
      {
        (object) tote.SiteID,
        (object) tote.ToteID
      })).Any<PXResult<SOPickerToShipmentLink>>())
        return Validation.Ok;
      return Validation.Fail("The {0} tote cannot be selected because it has already been assigned to another shipment.", new object[1]
      {
        (object) tote.ToteCD
      });
    }

    protected virtual void Apply(INTote tote) => this.ToteBasis.AssignTote(tote);

    protected virtual void ClearState() => this.ToteBasis.AddNewTote = new bool?(false);

    protected override void ReportSuccess(INTote tote)
    {
      ((ScanComponent<PickPackShip>) this).Basis.Reporter.Info("{0} tote selected for {1} shipment.", new object[2]
      {
        (object) tote.ToteCD,
        (object) this.ToteBasis.ShipmentJustAssignedWithTote
      });
    }

    protected virtual void SetNextState()
    {
      if (this.ToteBasis.HasAnotherToAssign)
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState<ToteSupport.AssignToteState>((string) null, Array.Empty<object>());
      else
        ((EntityState<PickPackShip, INTote>) this).SetNextState();
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ToteSupport.AssignToteState.value>
    {
      public value()
        : base("ASST")
      {
      }
    }

    [PXLocalizable]
    public new abstract class Msg : ToteSupport.ToteState.Msg
    {
      public const string Prompt = "Scan the tote for the {0} shipment.";
      public new const string Ready = "{0} tote selected for {1} shipment.";
      public const string CannotBeUsedSeparatly = "The {0} tote cannot be used separately from the cart.";
      public const string Busy = "The {0} tote cannot be selected because it has already been assigned to another shipment.";
    }
  }

  public sealed class SelectToteState : ToteSupport.ToteState
  {
    public const string Value = "TOTE";

    public virtual string Code => "TOTE";

    protected virtual string StatePrompt
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault() ? "Scan the tote from which you want to remove the items." : "Scan the tote to which you want to add the items.";
      }
    }

    protected virtual bool IsStateActive()
    {
      return ((EntityState<PickPackShip, INTote>) this).IsStateActive() && ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Select(Array.Empty<object>()).Count > 1;
    }

    protected virtual bool IsStateSkippable()
    {
      return !((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault() && this.ToteBasis.GetProperTote() != null;
    }

    protected override Validation Validate(INTote tote)
    {
      Validation validation;
      if (((ScanComponent<PickPackShip>) this).Basis.HasFault<INTote>(tote, new Func<INTote, Validation>(((ToteSupport.ToteState) this).Validate), ref validation))
        return validation;
      if (PXResultset<SOPickerToShipmentLink>.op_Implicit(((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Search<SOPickerToShipmentLink.toteID>((object) tote.ToteID, Array.Empty<object>())) != null)
        return Validation.Ok;
      return Validation.Fail("The {0} tote is not assigned to the current pick list. Available totes: {1}.", new object[2]
      {
        (object) tote.ToteCD,
        (object) ((IEnumerable<SOPickerToShipmentLink>) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SelectMain(Array.Empty<object>())).Select<SOPickerToShipmentLink, int?>((Func<SOPickerToShipmentLink, int?>) (link => link.ToteID)).Select<int?, string>((Func<int?, string>) (tid => INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), ((ScanComponent<PickPackShip>) this).Basis.SiteID, tid).ToteCD)).With<IEnumerable<string>, string>((Func<IEnumerable<string>, string>) (tcds => string.Join(", ", tcds)))
      });
    }

    protected virtual void Apply(INTote tote) => this.ToteBasis.ToteID = tote.ToteID;

    protected virtual void ClearState() => this.ToteBasis.ToteID = new int?();

    protected virtual void SetNextState()
    {
      if (((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault())
        ((ScanComponent<PickPackShip>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      else
        ((EntityState<PickPackShip, INTote>) this).SetNextState();
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ToteSupport.SelectToteState.value>
    {
      public value()
        : base("TOTE")
      {
      }
    }

    [PXLocalizable]
    public new abstract class Msg : ToteSupport.ToteState.Msg
    {
      public const string PromptAdd = "Scan the tote to which you want to add the items.";
      public const string PromptRemove = "Scan the tote from which you want to remove the items.";
      public const string MissingAssigned = "The {0} tote is not assigned to the current pick list. Available totes: {1}.";
    }
  }

  public sealed class PrepareToteForPackState : ToteSupport.ToteState
  {
    public const string Value = "PRET";

    public virtual string Code => "PRET";

    protected virtual string StatePrompt
    {
      get
      {
        return this.ToteBasis.With<ToteSupport, string>((Func<ToteSupport, string>) (ts => ts.Basis.Localize("Scan the {0} tote.", new object[1]
        {
          (object) ((IEnumerable<INTote>) ts.TotesToPrepareForPack).FirstOrDefault<INTote>((Func<INTote, bool>) (tote => !ts.PreparedForPackToteIDs.Contains(tote.ToteID.Value))).ToteCD
        })));
      }
    }

    protected virtual string StateInstructions
    {
      get
      {
        return this.ToteBasis.With<ToteSupport, string>((Func<ToteSupport, string>) (ts => ts.Basis.Localize("{0} of {1} totes scanned for the {2} shipment.", new object[3]
        {
          (object) ts.PreparedForPackToteIDs.Count,
          (object) ts.TotesToPrepareForPack.Length,
          (object) ts.Basis.RefNbr
        })));
      }
    }

    protected virtual void OnTakingOver()
    {
      bool? totesPerShipment = ((PXSelectBase<SOPickPackShipSetup>) ((ScanComponent<PickPackShip>) this).Basis.Setup).Current.AllowMultipleTotesPerShipment;
      bool flag = false;
      if (!(totesPerShipment.GetValueOrDefault() == flag & totesPerShipment.HasValue) || this.ToteBasis.TotesToPrepareForPack.Length != 1)
        return;
      this.ToteBasis.PreparedForPackToteIDs.Add(this.ToteBasis.TotesToPrepareForPack[0].ToteID.Value);
      ((ScanState<PickPackShip>) this).MoveToNextState();
    }

    protected override Validation Validate(INTote tote)
    {
      Validation validation;
      if (((ScanComponent<PickPackShip>) this).Basis.HasFault<INTote>(tote, new Func<INTote, Validation>(((ToteSupport.ToteState) this).Validate), ref validation))
        return validation;
      if (!((IEnumerable<INTote>) this.ToteBasis.TotesToPrepareForPack).Select<INTote, int?>((Func<INTote, int?>) (t => t.ToteID)).Contains<int?>(tote.ToteID))
        return Validation.Fail("The {0} tote is not assigned to the {1} shipment.", new object[2]
        {
          (object) tote.ToteCD,
          (object) ((ScanComponent<PickPackShip>) this).Basis.RefNbr
        });
      if (!this.ToteBasis.PreparedForPackToteIDs.Contains(tote.ToteID.Value))
        return Validation.Ok;
      return Validation.Fail("The {0} tote has already been scanned.", new object[1]
      {
        (object) tote.ToteCD
      });
    }

    protected virtual void Apply(INTote tote)
    {
      this.ToteBasis.PreparedForPackToteIDs.Add(tote.ToteID.Value);
    }

    protected virtual void ClearState() => this.ToteBasis.PreparedForPackToteIDs.Clear();

    protected override void ReportSuccess(INTote tote)
    {
      this.ToteBasis.Call<ToteSupport>((Action<ToteSupport>) (ts => ts.Basis.Reporter.Info("{0} tote selected.", new object[4]
      {
        (object) tote.ToteCD,
        (object) ts.PreparedForPackToteIDs.Count,
        (object) ts.TotesToPrepareForPack.Length,
        (object) ts.Basis.RefNbr
      })));
    }

    protected virtual void SetNextState()
    {
      if (this.ToteBasis.HasAnotherToPrepare)
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState<ToteSupport.PrepareToteForPackState>((string) null, Array.Empty<object>());
      else
        ((ScanComponent<PickPackShip>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ToteSupport.PrepareToteForPackState.value>
    {
      public value()
        : base("PRET")
      {
      }
    }

    [PXLocalizable]
    public new abstract class Msg : ToteSupport.ToteState.Msg
    {
      public const string Prompt = "Scan the {0} tote.";
      public const string Instruction = "{0} of {1} totes scanned for the {2} shipment.";
      public const string WrongTote = "The {0} tote is not assigned to the {1} shipment.";
      public const string AlreadyPreparedTote = "The {0} tote has already been scanned.";
    }
  }

  public sealed class AddToteCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "ADD*TOTE";

    public virtual string ButtonName => "scanAddTote";

    public virtual string DisplayName => "Add Tote";

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable && ((ScanComponent<PickPackShip>) this).Basis.Get<ToteSupport>().With<ToteSupport, bool>((Func<ToteSupport, bool>) (ts => !ts.AddNewTote.GetValueOrDefault() && ts.CanAddNewTote));
      }
    }

    protected virtual bool Process()
    {
      ((ScanComponent<PickPackShip>) this).Basis.Reset(false);
      ((ScanComponent<PickPackShip>) this).Basis.Get<ToteSupport>().AddNewTote = new bool?(true);
      ((ScanComponent<PickPackShip>) this).Basis.SetScanState<ToteSupport.AssignToteState>((string) null, Array.Empty<object>());
      return true;
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Add Tote";
    }
  }

  public class AlterCommandOrShipmentOnlyStatePromptError : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.CommandOrShipmentOnlyState.Logic>
  {
    public static bool IsActive() => ToteSupport.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.CommandOrShipmentOnlyState.Logic.GetPromptForCommandOrShipmentOnly" />
    [PXOverride]
    public string GetPromptForCommandOrShipmentOnly(
      Func<string> base_GetPromptForCommandOrShipmentOnly)
    {
      return "Use any command or scan the next shipment or tote number to continue.";
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.CommandOrShipmentOnlyState.Logic.GetErrorForCommandOrShipmentOnly" />
    [PXOverride]
    public string GetErrorForCommandOrShipmentOnly(
      Func<string> base_GetErrorForCommandOrShipmentOnly)
    {
      return "Only commands, a shipment number, or a tote can be used to continue.";
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string ToteAlreadyAssignedCannotAssignCart = "Totes from the {0} cart cannot be auto assigned to the pick list because it already has manual assignments.";
    public const string TotesAreNotEnoughInCart = "There are not enough active totes in the {0} cart to assign them to all of the shipments of the pick list.";
    public const string TotesFromCartAreAssigned = "The {0} first totes from the {1} cart were automatically assigned to the shipments of the pick list.";
    public const string MissingToteAssignedToShipment = "The {0} tote is not assigned to the {1} shipment. Available totes: {2}.";
    public const string InventoryAddedToTote = "{0} x {1} {2} added to {3} tote.";
    public const string InventoryRemovedFromTote = "{0} x {1} {2} removed from {3} tote.";
    public const string UseCommandOrShipmentOrToteToContinue = "Use any command or scan the next shipment or tote number to continue.";
    public const string OnlyCommandsAndShipmentsOrToteAreAllowed = "Only commands, a shipment number, or a tote can be used to continue.";
    public const string PromptShipmentOrTote = "Scan the shipment or tote number.";
  }
}
