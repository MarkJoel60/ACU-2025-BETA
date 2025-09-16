// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPICountEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.PhysicalInventory;
using PX.Objects.RQ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

public class INPICountEntry : 
  PXGraph<INPICountEntry>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXAction<INPIHeader> save;
  public PXCancel<INPIHeader> Cancel;
  public PXFirst<INPIHeader> First;
  public PXPrevious<INPIHeader> Previous;
  public PXNext<INPIHeader> Next;
  public PXLast<INPIHeader> Last;
  public PXSelect<INPIHeader, Where<INPIHeader.status, Equal<INPIHdrStatus.counting>>> PIHeader;
  public PXFilter<PICountFilter> Filter;
  public INBarCodeItemLookup<INBarCodeItem> AddByBarCode;
  [PXImport(typeof (INPIHeader))]
  public PXSelectJoin<INPIDetail, InnerJoin<INPIHeader, On<INPIDetail.FK.PIHeader>, InnerJoin<InventoryItem, On<INPIDetail.FK.InventoryItem>, LeftJoin<INSubItem, On<INPIDetail.FK.SubItem>>>>, Where<INPIDetail.pIID, Equal<Current<INPIHeader.pIID>>, And<INPIDetail.inventoryID, IsNotNull, And<Where2<Where<Current<PICountFilter.startLineNbr>, IsNull, Or<INPIDetail.lineNbr, GreaterEqual<Current<PICountFilter.startLineNbr>>>>, And<Where<Current<PICountFilter.endLineNbr>, IsNull, Or<INPIDetail.lineNbr, LessEqual<Current<PICountFilter.endLineNbr>>>>>>>>>> PIDetail;
  public PXSetup<INSetup> Setup;
  public PXAction<INPIHeader> addLine;
  public PXAction<INPIHeader> addLine2;
  public int excelRowNumber = 2;
  public bool importHasError;

  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search<INPIHeader.pIID, Where<INPIHeader.status, Equal<INPIHdrStatus.counting>>, OrderBy<Desc<INPIHeader.pIID>>>), Filterable = true)]
  protected virtual void INPIHeader_PIID_CacheAttached(PXCache sender)
  {
  }

  public INPICountEntry()
  {
    ((PXSelectBase) this.PIHeader).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled<INPIHeader.descr>(((PXSelectBase) this.PIHeader).Cache, (object) null, false);
    ((PXSelectBase) this.PIDetail).WhereAndCurrent<PICountFilter>();
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.PIDetail).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INPIDetail.physicalQty>(((PXSelectBase) this.PIDetail).Cache, (object) null, true);
    ((PXSelectBase) this.PIDetail).View.Clear();
    ((PXSelectBase) this.PIDetail).GetAttribute<PXImportAttribute>().MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this.MappingPropertiesInit);
  }

  public void MappingPropertiesInit(
    object sender,
    PXImportAttribute.MappingPropertiesInitEventArgs e)
  {
    e.Names.Add(typeof (INPIDetail.inventoryID).Name);
    e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INPIDetail.inventoryID>(((PXSelectBase) this.PIDetail).Cache));
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      e.Names.Add(typeof (INPIDetail.subItemID).Name);
      e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INPIDetail.subItemID>(((PXSelectBase) this.PIDetail).Cache));
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
    {
      e.Names.Add(typeof (INPIDetail.locationID).Name);
      e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INPIDetail.locationID>(((PXSelectBase) this.PIDetail).Cache));
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
      return;
    e.Names.Add(typeof (INPIDetail.lotSerialNbr).Name);
    e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<INPIDetail.lotSerialNbr>(((PXSelectBase) this.PIDetail).Cache));
  }

  [PXUIField]
  [PXSaveButton]
  protected virtual IEnumerable Save(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton(Tooltip = "Add New Line")]
  public virtual IEnumerable AddLine(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<INBarCodeItem>) this.AddByBarCode).AskExt(INPICountEntry.\u003C\u003Ec.\u003C\u003E9__16_0 ?? (INPICountEntry.\u003C\u003Ec.\u003C\u003E9__16_0 = new PXView.InitializePanel((object) INPICountEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddLine\u003Eb__16_0)))) == 1 && this.AddByBarCode.VerifyRequired())
      this.UpdatePhysicalQty();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(Tooltip = "Add New Line")]
  public virtual IEnumerable AddLine2(PXAdapter adapter)
  {
    if (this.AddByBarCode.VerifyRequired())
      this.UpdatePhysicalQty();
    return adapter.Get();
  }

  protected virtual void INBarCodeItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.uOM>(((PXSelectBase) this.AddByBarCode).Cache, (object) null, false);
  }

  protected virtual void INBarCodeItem_ExpireDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    INPIDetail inpiDetail = PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXSelectReadonly<INPIDetail, Where<INPIDetail.pIID, Equal<Current<INPIHeader.pIID>>, And<INPIDetail.inventoryID, Equal<Current<INBarCodeItem.inventoryID>>, And<INPIDetail.lotSerialNbr, Equal<Current<INBarCodeItem.lotSerialNbr>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (inpiDetail == null)
      return;
    e.NewValue = (object) inpiDetail.ExpireDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INBarCodeItem_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    INBarCodeItem row = (INBarCodeItem) e.Row;
    if (row == null || !row.AutoAddLine.GetValueOrDefault() || !this.AddByBarCode.VerifyRequired(true))
      return;
    Decimal? qty = row.Qty;
    Decimal num = 0M;
    if (!(qty.GetValueOrDefault() > num & qty.HasValue))
      return;
    this.UpdatePhysicalQty();
  }

  private void UpdatePhysicalQty()
  {
    INBarCodeItem current = ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).Current;
    INPIDetail inpiDetail = this.UpdatePhysicalQty(PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXSelectReadonly<INPIDetail, Where<INPIDetail.pIID, Equal<Current<INPIHeader.pIID>>, And<INPIDetail.inventoryID, Equal<Current<INBarCodeItem.inventoryID>>, And<INPIDetail.subItemID, Equal<Current<INBarCodeItem.subItemID>>, And<INPIDetail.locationID, Equal<Current<INBarCodeItem.locationID>>, And<Where<INPIDetail.lotSerialNbr, IsNull, Or<INPIDetail.lotSerialNbr, Equal<Current<INBarCodeItem.lotSerialNbr>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())), (((PXSelectBase<INPIHeader>) this.PIHeader).Current.PIID, current.LocationID, current.InventoryID, current.SubItemID, current.LotSerialNbr, current.ExpireDate, current.BaseQty));
    current.Description = PXMessages.LocalizeFormatNoPrefixNLA("Item {0}{1} updated physical quantity {2} {3} line {4}.", new object[5]
    {
      (object) ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.inventoryID>(current).ToString().Trim(),
      ((PXSelectBase<INSetup>) this.Setup).Current.UseInventorySubItem.GetValueOrDefault() ? (object) (":" + ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.subItemID>(current)?.ToString()) : (object) string.Empty,
      ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.qty>(current),
      (object) current.UOM,
      (object) inpiDetail.LineNbr
    });
    this.AddByBarCode.Reset(true);
    ((PXSelectBase) this.AddByBarCode).View.RequestRefresh();
  }

  protected INPIDetail UpdatePhysicalQty(
    INPIDetail detail,
    (string PIID, int? LocationID, int? InventoryID, int? SubItemID, string LotSerialNbr, DateTime? ExpireDate, Decimal? DeltaBaseQty) item)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (detail == null)
      {
        INPIEntry instance = PXGraph.CreateInstance<INPIEntry>();
        ((PXSelectBase<INPIHeader>) instance.PIHeader).Current = PXResultset<INPIHeader>.op_Implicit(((PXSelectBase<INPIHeader>) instance.PIHeader).Search<INPIHeader.pIID>((object) item.PIID, Array.Empty<object>()));
        detail = new INPIDetail()
        {
          InventoryID = item.InventoryID
        };
        detail = PXCache<INPIDetail>.CreateCopy(((PXSelectBase<INPIDetail>) instance.PIDetail).Insert(detail));
        detail.SubItemID = item.SubItemID;
        detail.LocationID = item.LocationID;
        detail.LotSerialNbr = item.LotSerialNbr;
        detail = PXCache<INPIDetail>.CreateCopy(((PXSelectBase<INPIDetail>) instance.PIDetail).Update(detail));
        detail.PhysicalQty = item.DeltaBaseQty;
        detail.ExpireDate = item.ExpireDate;
        ((PXSelectBase<INPIDetail>) instance.PIDetail).Update(detail);
        ((PXAction) instance.Save).Press();
        object obj1 = ((PXSelectBase) instance.PIDetail).Cache.GetValue<INPIDetail.pIID>((object) detail);
        object obj2 = ((PXSelectBase) instance.PIDetail).Cache.GetValue<INPIDetail.lineNbr>((object) detail);
        ((PXSelectBase) this.PIHeader).View.RequestRefresh();
        ((PXSelectBase) this.PIDetail).Cache.Locate((IDictionary) new Dictionary<string, object>()
        {
          ["PIID"] = obj1,
          ["LineNbr"] = obj2
        });
      }
      else
      {
        detail = PXCache<INPIDetail>.CreateCopy(detail);
        detail.PhysicalQty = new Decimal?(detail.PhysicalQty.GetValueOrDefault() + item.DeltaBaseQty.GetValueOrDefault());
        detail = ((PXSelectBase<INPIDetail>) this.PIDetail).Update(detail);
      }
      transactionScope.Complete();
      return detail;
    }
  }

  protected virtual void INPIHeader_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INPIHeader row = (INPIHeader) e.Row;
    if (row == null)
      return;
    ((PXAction) this.addLine).SetEnabled(row.PIID != null);
    ((PXAction) this.addLine2).SetEnabled(row.PIID != null);
    INPIClass inpiClass = INPIClass.PK.Find((PXGraph) this, row.PIClassID);
    if (inpiClass == null)
      return;
    PXUIFieldAttribute.SetVisible<INPIDetail.bookQty>(((PXSelectBase) this.PIDetail).Cache, (object) null, !inpiClass.HideBookQty.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<INPIDetail.varQty>(((PXSelectBase) this.PIDetail).Cache, (object) null, !inpiClass.HideBookQty.GetValueOrDefault());
  }

  protected virtual void INPIHeader_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INBarCodeItem_SiteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<INPIHeader>) this.PIHeader).Current != null)
      e.NewValue = (object) ((PXSelectBase<INPIHeader>) this.PIHeader).Current.SiteID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INBarCodeItem_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
      return;
    if (((PXSelectBase<PICountFilter>) this.Filter).Current != null)
      e.NewValue = (object) ((PXSelectBase<PICountFilter>) this.Filter).Current.LocationID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INBarCodeItem_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    INBarCodeItem row = (INBarCodeItem) e.Row;
    if (e.NewValue == null)
      return;
    if (row == null)
      return;
    try
    {
      this.ValidatePIInventoryLocation((int?) e.NewValue, row.LocationID);
    }
    catch (PXSetPropertyException ex)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, new int?((int) e.NewValue));
      e.NewValue = (object) inventoryItem?.InventoryCD;
      throw ex;
    }
  }

  protected virtual void INBarCodeItem_LocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    INBarCodeItem row = (INBarCodeItem) e.Row;
    if (!(e.NewValue as int?).HasValue)
      return;
    if (row == null)
      return;
    try
    {
      this.ValidatePIInventoryLocation(row.InventoryID, (int?) e.NewValue);
    }
    catch (PXSetPropertyException ex)
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this, new int?((int) e.NewValue));
      e.NewValue = (object) inLocation?.LocationCD;
      throw ex;
    }
  }

  protected virtual void INPIDetail_PhysicalQty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    Decimal newValue1 = (Decimal) e.NewValue;
    INLotSerClass lsc_rec = this.SelectLotSerClass(((INPIDetail) e.Row).InventoryID);
    Decimal? newValue2 = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue2.GetValueOrDefault() < num & newValue2.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) 0
      });
    if (lsc_rec != null && this.LSRequired(lsc_rec) && lsc_rec.LotSerTrack == "S" && newValue1 != 0M && newValue1 != 1M)
      throw new PXSetPropertyException("Serial-numbered items should have physical quantity only 1 or 0.");
  }

  protected virtual void INPIDetail_PhysicalQty_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INPIDetail row = (INPIDetail) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    INPIDetail inpiDetail = row;
    Decimal? physicalQty = row.PhysicalQty;
    Decimal? bookQty = row.BookQty;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (physicalQty.HasValue & bookQty.HasValue ? new Decimal?(physicalQty.GetValueOrDefault() - bookQty.GetValueOrDefault()) : new Decimal?());
    pxCache.SetValue<INPIDetail.varQty>((object) inpiDetail, (object) local);
    sender.SetValue<INPIDetail.status>((object) row, row.PhysicalQty.HasValue ? (object) "E" : (object) "N");
  }

  protected virtual void INPIDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    INPIDetail newRow = (INPIDetail) e.NewRow;
    INPIDetail row = (INPIDetail) e.Row;
    if (newRow == null || row == null)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (!PXDatabase.Update<INPIDetail>(new PXDataFieldParam[6]
      {
        (PXDataFieldParam) new PXDataFieldAssign(typeof (INPIDetail.status).Name, (PXDbType) 22, (object) newRow.Status),
        (PXDataFieldParam) new PXDataFieldAssign(typeof (INPIDetail.physicalQty).Name, (PXDbType) 5, (object) newRow.PhysicalQty),
        (PXDataFieldParam) new PXDataFieldAssign(typeof (INPIDetail.varQty).Name, (PXDbType) 5, (object) newRow.VarQty),
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (INPIDetail.pIID).Name, (PXDbType) 22, (object) newRow.PIID),
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (INPIDetail.lineNbr).Name, (PXDbType) 8, (object) newRow.LineNbr),
        (PXDataFieldParam) new PXDataFieldRestrict(typeof (INPIDetail.Tstamp).Name, (PXDbType) 19, new int?(8), (object) newRow.tstamp, (PXComp) 5)
      }))
        throw new PXException("Another process has updated the '{0}' record. Your changes will be lost.", new object[1]
        {
          (object) typeof (INPIDetail).Name
        });
      PXDataFieldParam[] pxDataFieldParamArray = new PXDataFieldParam[3];
      string name1 = typeof (INPIHeader.totalPhysicalQty).Name;
      Decimal? nullable = newRow.PhysicalQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = row.PhysicalQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      // ISSUE: variable of a boxed type
      __Boxed<Decimal> local1 = (ValueType) (valueOrDefault1 - valueOrDefault2);
      pxDataFieldParamArray[0] = (PXDataFieldParam) new PXDataFieldAssign(name1, (PXDbType) 5, (object) local1)
      {
        Behavior = (PXDataFieldAssign.AssignBehavior) 1
      };
      string name2 = typeof (INPIHeader.totalVarQty).Name;
      nullable = newRow.VarQty;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      nullable = row.VarQty;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      // ISSUE: variable of a boxed type
      __Boxed<Decimal> local2 = (ValueType) (valueOrDefault3 - valueOrDefault4);
      pxDataFieldParamArray[1] = (PXDataFieldParam) new PXDataFieldAssign(name2, (PXDbType) 5, (object) local2)
      {
        Behavior = (PXDataFieldAssign.AssignBehavior) 1
      };
      pxDataFieldParamArray[2] = (PXDataFieldParam) new PXDataFieldRestrict(typeof (INPIHeader.pIID).Name, (PXDbType) 22, (object) newRow.PIID);
      PXDatabase.Update<INPIHeader>(pxDataFieldParamArray);
      transactionScope.Complete();
    }
    INPIHeader current1 = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    Decimal? nullable1 = current1.TotalPhysicalQty;
    Decimal? nullable2 = newRow.PhysicalQty;
    Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
    nullable2 = row.PhysicalQty;
    Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
    Decimal num1 = valueOrDefault5 - valueOrDefault6;
    Decimal? nullable3;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable3 = nullable2;
    }
    else
      nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num1);
    current1.TotalPhysicalQty = nullable3;
    INPIHeader current2 = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    nullable1 = current2.TotalVarQty;
    nullable2 = newRow.VarQty;
    Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
    nullable2 = row.VarQty;
    Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
    Decimal num2 = valueOrDefault7 - valueOrDefault8;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num2);
    current2.TotalVarQty = nullable4;
    ((PXSelectBase<INPIHeader>) this.PIHeader).Current.tstamp = newRow.tstamp = PXDatabase.SelectTimeStamp();
  }

  protected virtual void INPIDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.SetStatus(e.Row, (PXEntryStatus) 0);
    sender.IsDirty = false;
    ((PXSelectBase) this.PIDetail).View.RequestRefresh();
  }

  protected virtual void PICountFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is PICountFilter row))
      return;
    int? nullable = row.StartLineNbr;
    if (!nullable.HasValue)
      return;
    nullable = row.EndLineNbr;
    if (!nullable.HasValue)
      return;
    nullable = row.EndLineNbr;
    int? startLineNbr = row.StartLineNbr;
    if (!(nullable.GetValueOrDefault() < startLineNbr.GetValueOrDefault() & nullable.HasValue & startLineNbr.HasValue))
      return;
    row.EndLineNbr = row.StartLineNbr;
  }

  protected virtual INLotSerClass SelectLotSerClass(int? p_InventoryID)
  {
    if (!p_InventoryID.HasValue)
      return (INLotSerClass) null;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, p_InventoryID);
    return inventoryItem.LotSerClassID != null ? INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID) : (INLotSerClass) null;
  }

  protected virtual bool LSRequired(INLotSerClass lsc_rec)
  {
    return lsc_rec != null && lsc_rec.LotSerTrack != "N" && lsc_rec.LotSerAssign == "R";
  }

  protected virtual void ValidatePIInventoryLocation(int? inventoryID, int? locationID)
  {
    if (inventoryID.HasValue && locationID.HasValue && !new PILocksInspector(((PXSelectBase<INPIHeader>) this.PIHeader).Current.SiteID.Value).IsInventoryLocationIncludedInPI(inventoryID, locationID, ((PXSelectBase<INPIHeader>) this.PIHeader).Current.PIID))
      throw new PXSetPropertyException("Combination of selected Inventory Item and Warehouse Location is not allowed for this Physical Count.");
  }

  private object GetImportedValue<Field>(IDictionary values, bool isRequired) where Field : IBqlField
  {
    INPIDetail instance = (INPIDetail) ((PXSelectBase) this.PIDetail).Cache.CreateInstance();
    string displayName = PXUIFieldAttribute.GetDisplayName<Field>(((PXSelectBase) this.PIDetail).Cache);
    object obj = values.Contains((object) typeof (Field).Name) ? values[(object) typeof (Field).Name] : throw new PXException("Incorrect head in the file. Column \"{0}\" is mandatory", new object[1]
    {
      (object) displayName
    });
    ((PXSelectBase) this.PIDetail).Cache.RaiseFieldUpdating<Field>((object) instance, ref obj);
    return !isRequired || obj != null ? obj : throw new PXException("'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    });
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, ((PXSelectBase) this.PIDetail).View.Name, true) == 0)
    {
      PXCache cache = ((PXSelectBase) this.AddByBarCode).Cache;
      INBarCodeItem inBarCodeItem = ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).Current ?? (INBarCodeItem) cache.CreateInstance();
      try
      {
        cache.SetValueExt<INBarCodeItem.inventoryID>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.inventoryID>(values, true));
        if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
          cache.SetValueExt<INBarCodeItem.subItemID>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.subItemID>(values, true));
        if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
          cache.SetValueExt<INBarCodeItem.locationID>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.locationID>(values, true));
        if (PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
          cache.SetValueExt<INBarCodeItem.lotSerialNbr>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.lotSerialNbr>(values, false));
        cache.SetValueExt<INBarCodeItem.qty>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.physicalQty>(values, true));
        cache.SetValueExt<INBarCodeItem.autoAddLine>((object) inBarCodeItem, (object) false);
        cache.Update((object) inBarCodeItem);
        this.UpdatePhysicalQty();
      }
      catch (Exception ex)
      {
        PXTrace.WriteError("Row number {0}. Error message \"{1}\"", new object[2]
        {
          (object) this.excelRowNumber,
          (object) ex.Message
        });
        this.importHasError = true;
      }
      finally
      {
        ++this.excelRowNumber;
      }
    }
    return false;
  }

  public bool RowImporting(string viewName, object row) => false;

  public bool RowImported(string viewName, object row, object oldRow) => false;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    if (this.importHasError)
      throw new Exception("Import has some error. The list of incorrect records is recorded in the Trace.");
  }
}
