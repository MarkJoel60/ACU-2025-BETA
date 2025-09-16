// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorCatalogueMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PO;

public class POVendorCatalogueMaint : 
  PXGraph<POVendorCatalogueMaint>,
  PXImportAttribute.IPXPrepareItems
{
  public PXSelect<VendorLocation> BAccount;
  public PXSave<VendorLocation> Save;
  public PXAction<VendorLocation> cancel;
  public PXFirst<VendorLocation> First;
  public PXPrevious<VendorLocation> Prev;
  public PXNext<VendorLocation> Next;
  public PXLast<VendorLocation> Last;
  public PXAction<VendorLocation> ShowVendorPrices;
  [PXImport(typeof (VendorLocation))]
  public POVendorInventorySelect<POVendorInventory, LeftJoin<PX.Objects.IN.InventoryItem, On<POVendorInventory.FK.InventoryItem>, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POVendorInventory.vendorID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<POVendorInventory.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<POVendorInventory.vendorLocationID>>>>>>, Where<POVendorInventory.vendorID, Equal<Current<VendorLocation.bAccountID>>, And<Where<POVendorInventory.vendorLocationID, Equal<Current<VendorLocation.locationID>>, Or<POVendorInventory.vendorLocationID, IsNull>>>>, VendorLocation> VendorCatalogue;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSelect<APVendorPrice> VendorPrice;
  public PXSetup<POSetup> posetup;
  public PXSetup<INSetup> insetup;
  private readonly string _inventoryID;
  private readonly string _subItemID;
  private readonly string _uOM;
  private readonly string _recordID;

  [Inventory(Filterable = true)]
  protected virtual void POVendorInventory_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (VendorLocation.bAccountID))]
  protected virtual void POVendorInventory_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (VendorLocation.locationID))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorLocation.bAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Vendor Location", Visible = false)]
  protected virtual void POVendorInventory_VendorLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "All Locations")]
  [PXDBCalced(typeof (Switch<Case<Where<POVendorInventory.vendorLocationID, IsNull>, True>, False>), typeof (bool))]
  protected virtual void POVendorInventory_AllLocations_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POVendorInventory.inventoryID>>>>))]
  [PXFormula(typeof (Default<POVendorInventory.inventoryID>))]
  [INUnit(typeof (POVendorInventory.inventoryID))]
  [PXCheckUnique(new System.Type[] {typeof (POVendorInventory.vendorID), typeof (POVendorInventory.vendorLocationID), typeof (POVendorInventory.inventoryID), typeof (POVendorInventory.subItemID)}, IgnoreNulls = false, ClearOnDuplicate = false)]
  protected virtual void POVendorInventory_PurchaseUnit_CacheAttached(PXCache sender)
  {
  }

  public POVendorCatalogueMaint()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXSelectBase) this.BAccount).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetVisible<VendorLocation.curyID>(((PXSelectBase) this.BAccount).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    this._inventoryID = ((PXSelectBase) this.VendorCatalogue).Cache.GetField(typeof (POVendorInventory.inventoryID));
    this._subItemID = ((PXSelectBase) this.VendorCatalogue).Cache.GetField(typeof (POVendorInventory.subItemID));
    this._uOM = ((PXSelectBase) this.VendorCatalogue).Cache.GetField(typeof (POVendorInventory.purchaseUnit));
    this._recordID = ((PXSelectBase) this.VendorCatalogue).Cache.GetField(typeof (POVendorInventory.recordID));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(POVendorCatalogueMaint.\u003C\u003Ec.\u003C\u003E9__22_0 ?? (POVendorCatalogueMaint.\u003C\u003Ec.\u003C\u003E9__22_0 = new PXFieldDefaulting((object) POVendorCatalogueMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__22_0))));
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    POVendorCatalogueMaint vendorCatalogueMaint1 = this;
    VendorLocation current = ((PXSelectBase<VendorLocation>) vendorCatalogueMaint1.BAccount).Current;
    if (current != null && adapter.Searches.Length == 2 && adapter.Searches[0] != null && current.AcctCD != null && current.AcctCD.Trim() != adapter.Searches[0].ToString().Trim())
    {
      POVendorCatalogueMaint vendorCatalogueMaint2 = vendorCatalogueMaint1;
      object[] objArray = new object[1]
      {
        adapter.Searches[0]
      };
      adapter.Searches[1] = (object) PXResult<BAccountR, PX.Objects.CR.Standalone.Location>.op_Implicit((PXResult<BAccountR, PX.Objects.CR.Standalone.Location>) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.acctCD, Equal<Required<BAccountR.acctCD>>>>.Config>.SelectWindowed((PXGraph) vendorCatalogueMaint2, 0, 1, objArray))).LocationCD;
    }
    foreach (VendorLocation vendorLocation in ((PXAction) new PXCancel<VendorLocation>((PXGraph) vendorCatalogueMaint1, nameof (Cancel))).Press(adapter))
      yield return (object) vendorLocation;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable showVendorPrices(PXAdapter adapter)
  {
    if (((PXSelectBase<VendorLocation>) this.BAccount).Current != null)
    {
      APVendorPriceMaint instance = PXGraph.CreateInstance<APVendorPriceMaint>();
      APVendorPriceFilter copy = PXCache<APVendorPriceFilter>.CreateCopy(((PXSelectBase<APVendorPriceFilter>) instance.Filter).Current);
      copy.VendorID = ((PXSelectBase<VendorLocation>) this.BAccount).Current.BAccountID;
      if (((PXSelectBase<POVendorInventory>) this.VendorCatalogue).Current != null)
        copy.InventoryID = ((PXSelectBase<POVendorInventory>) this.VendorCatalogue).Current.InventoryID;
      ((PXSelectBase<APVendorPriceFilter>) instance.Filter).Update(copy);
      ((PXSelectBase<APVendorPriceFilter>) instance.Filter).Select(Array.Empty<object>());
      throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Prices");
    }
    return adapter.Get();
  }

  protected virtual void VendorLocation_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void VendorLocation_CuryID_FieldSelecting(
    PXCache sedner,
    PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue != null)
      return;
    e.ReturnValue = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? (e.Row is VendorLocation row ? (object) row.BaseCuryID : (object) (string) null) : (object) ((PXGraph) this).Accessinfo.BaseCuryID;
  }

  protected virtual void POVendorInventory_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    POVendorInventory row = (POVendorInventory) e.Row;
    if (row == null || ((PXSelectBase<VendorLocation>) this.BAccount).Current == null)
      return;
    row.CuryID = ((PXSelectBase<VendorLocation>) this.BAccount).Current.CuryID ?? ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
  }

  protected virtual void POVendorInventory_AllLocations_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POVendorInventory row = (POVendorInventory) e.Row;
    if (e.NewValue == null || !(bool) e.NewValue)
      return;
    if (PXResultset<POVendorInventory>.op_Implicit(PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Current<POVendorInventory.inventoryID>>, And<POVendorInventory.subItemID, Equal<Current<POVendorInventory.subItemID>>, And<POVendorInventory.purchaseUnit, Equal<Current<POVendorInventory.purchaseUnit>>, And<POVendorInventory.vendorID, Equal<Current<POVendorInventory.vendorID>>, And<POVendorInventory.vendorLocationID, IsNull, And<POVendorInventory.recordID, NotEqual<Current<POVendorInventory.recordID>>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, (object[]) null)) != null)
      throw new PXSetPropertyException("Unable to propagate selected item for all locations there's another one.");
  }

  protected virtual void POVendorInventory_AllLocations_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POVendorInventory row = (POVendorInventory) e.Row;
    if (row.AllLocations.GetValueOrDefault())
      row.VendorLocationID = new int?();
    else
      row.VendorLocationID = ((PXSelectBase<VendorLocation>) this.BAccount).Current.LocationID;
  }

  protected virtual void POVendorInventory_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    POVendorInventory row = (POVendorInventory) e.Row;
    if (row == null)
      return;
    int? nullable = row.InventoryID;
    if (!nullable.HasValue)
      return;
    nullable = row.SubItemID;
    if (!nullable.HasValue || row.PurchaseUnit == null)
      return;
    ((PXSelectBase) this.VendorCatalogue).View.RequestRefresh();
  }

  protected virtual void POVendorInventory_InventoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "VendorCatalogue", true) == 0)
    {
      string str1 = (string) values[(object) this._inventoryID];
      PXResult<PX.Objects.IN.InventoryItem, INSubItem> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INSubItem>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectReadonly2<PX.Objects.IN.InventoryItem, LeftJoin<INSubItem, On<INSubItem.subItemCD, Equal<Required<INSubItem.subItemCD>>, Or<Where<Required<INSubItem.subItemCD>, IsNull, And<INSubItem.subItemID, Equal<PX.Objects.IN.InventoryItem.defaultSubItemID>>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryCD, Equal<Required<PX.Objects.IN.InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        values[(object) this._subItemID],
        values[(object) this._subItemID],
        (object) str1
      }));
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(pxResult);
      INSubItem inSubItem = PXResult<PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(pxResult);
      if (inventoryItem != null && inventoryItem.InventoryID.HasValue)
      {
        string str2 = (string) values[(object) this._uOM] ?? inventoryItem.PurchaseUnit;
        if (values[(object) this._subItemID] == null)
          values[(object) this._subItemID] = (object) inSubItem.SubItemCD;
        PXResultset<POVendorInventory> pxResultset;
        if (!inSubItem.SubItemID.HasValue)
          pxResultset = PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, Where<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>, And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.purchaseUnit, Equal<Required<POVendorInventory.purchaseUnit>>, And<POVendorInventory.subItemID, IsNull>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
          {
            (object) ((PXSelectBase<VendorLocation>) this.BAccount).Current.BAccountID,
            (object) ((PXSelectBase<VendorLocation>) this.BAccount).Current.LocationID,
            (object) inventoryItem.InventoryID,
            (object) str2
          });
        else
          pxResultset = PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, Where<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>, And<POVendorInventory.vendorLocationID, Equal<Required<POVendorInventory.vendorLocationID>>, And<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.purchaseUnit, Equal<Required<POVendorInventory.purchaseUnit>>, And<Where<POVendorInventory.subItemID, Equal<Required<POVendorInventory.subItemID>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[5]
          {
            (object) ((PXSelectBase<VendorLocation>) this.BAccount).Current.BAccountID,
            (object) ((PXSelectBase<VendorLocation>) this.BAccount).Current.LocationID,
            (object) inventoryItem.InventoryID,
            (object) str2,
            (object) inSubItem.SubItemID
          });
        POVendorInventory poVendorInventory = PXResultset<POVendorInventory>.op_Implicit(pxResultset);
        if (poVendorInventory != null)
        {
          if (values[(object) this._uOM] == null)
            values[(object) this._uOM] = (object) poVendorInventory.PurchaseUnit;
          if (keys.Contains((object) this._recordID))
          {
            keys[(object) this._recordID] = (object) poVendorInventory.RecordID;
            values[(object) this._recordID] = (object) poVendorInventory.RecordID;
          }
        }
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }
}
