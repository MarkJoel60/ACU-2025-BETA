// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN.PhysicalInventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class INPIEntry : 
  INPIController,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public 
  #nullable disable
  PXAction<INPIHeader> Insert;
  public PXCopyPasteAction<INPIHeader> CopyPaste;
  public PXDelete<INPIHeader> Delete;
  public PXFirst<INPIHeader> First;
  public PXPrevious<INPIHeader> Previous;
  public PXNext<INPIHeader> Next;
  public PXLast<INPIHeader> Last;
  [PXFilterable(new Type[] {})]
  [PXImport(typeof (INPIHeader))]
  public FbqlSelect<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<InventoryItem>.On<INPIDetail.FK.InventoryItem>>, FbqlJoins.Left<INSubItem>.On<INPIDetail.FK.SubItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INPIDetail.pIID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INPIHeader.pIID, IBqlString>.AsOptional>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INPIDetail.inventoryID, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  InventoryItem.inventoryID, IBqlInt>.IsNotNull>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INPIDetail.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  INPIDetail>.View PIDetail;
  public FbqlSelect<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INPIDetail.pIID>.IsRelatedTo<INPIHeader.pIID>.AsSimpleKey.WithTablesOf<INPIHeader, INPIDetail>, INPIHeader, INPIDetail>.SameAsCurrent>, INPIDetail>.View PIDetailPure;
  public FbqlSelect<SelectFromBase<INPIHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INPIHeader.pIID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INPIHeader.pIID, IBqlString>.FromCurrent>>, 
  #nullable disable
  INPIHeader>.View PIHeaderInfo;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.INSetup, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.INSetup>.View INSetup;
  public PXSetup<INSite>.Where<BqlOperand<
  #nullable enable
  INSite.siteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INPIHeader.siteID, IBqlInt>.FromCurrent>> insite;
  public 
  #nullable disable
  PXFilter<PIGeneratorSettings> GeneratorSettings;
  public INBarCodeItemLookup<INBarCodeItem> AddByBarCode;
  public PXSetup<PX.Objects.IN.INSetup> Setup;
  public PXAction<INPIHeader> addLine;
  public PXAction<INPIHeader> addLine2;
  public bool DisableCostCalculation;
  private bool skipRecalcTotals;
  public int excelRowNumber = 2;
  public bool importHasError;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXSelector(typeof (Search2<INPIHeader.pIID, InnerJoin<INSite, On<INPIHeader.FK.Site>>, Where<MatchUserFor<INSite>>, OrderBy<Desc<INPIHeader.pIID>>>), Filterable = true)]
  protected void _(Events.CacheAttached<INPIHeader.pIID> e)
  {
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INPIClass.pIClassID, Where<INPIClass.method, Equal<PIMethod.fullPhysicalInventory>>>))]
  [PXDefault]
  protected virtual void _(
    Events.CacheAttached<PIGeneratorSettings.pIClassID> e)
  {
  }

  public INPIEntry()
  {
    PXDefaultAttribute.SetPersistingCheck<PIGeneratorSettings.randomItemsLimit>(((PXSelectBase) this.GeneratorSettings).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PIGeneratorSettings.lastCountPeriod>(((PXSelectBase) this.GeneratorSettings).Cache, (object) null, (PXPersistingCheck) 2);
  }

  [PXInsertButton]
  [PXUIField]
  protected virtual IEnumerable insert(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).AskExt(INPIEntry.\u003C\u003Ec.\u003C\u003E9__18_0 ?? (INPIEntry.\u003C\u003Ec.\u003C\u003E9__18_0 = new PXView.InitializePanel((object) INPIEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cinsert\u003Eb__18_0)))) == 1 && this.GeneratorSettings.VerifyRequired())
    {
      PIGenerator instance = PXGraph.CreateInstance<PIGenerator>();
      ((PXSelectBase<PIGeneratorSettings>) instance.GeneratorSettings).Current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
      instance.CalcPIRows(true);
      if (((PXSelectBase<INPIHeader>) instance.piheader).Current != null)
      {
        ((PXGraph) this).Clear();
        return (IEnumerable) ((PXSelectBase<INPIHeader>) this.PIHeader).Search<INPIHeader.pIID>((object) ((PXSelectBase<INPIHeader>) instance.piheader).Current.PIID, Array.Empty<object>());
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(Tooltip = "Add New Line")]
  public virtual IEnumerable AddLine(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<INBarCodeItem>) this.AddByBarCode).AskExt(INPIEntry.\u003C\u003Ec.\u003C\u003E9__20_0 ?? (INPIEntry.\u003C\u003Ec.\u003C\u003E9__20_0 = new PXView.InitializePanel((object) INPIEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddLine\u003Eb__20_0)))) == 1 && this.AddByBarCode.VerifyRequired())
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

  protected virtual void _(Events.RowSelected<INBarCodeItem> e)
  {
    PXUIFieldAttribute.SetEnabled<INBarCodeItem.uOM>(((PXSelectBase) this.AddByBarCode).Cache, (object) null, false);
  }

  protected virtual void _(
    Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.expireDate> e)
  {
    INPIDetail inpiDetail = PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<BqlField<INPIHeader.pIID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INPIDetail.inventoryID, IBqlInt>.IsEqual<BqlField<INBarCodeItem.inventoryID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<BqlField<INBarCodeItem.lotSerialNbr, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (inpiDetail == null)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.expireDate>, INBarCodeItem, object>) e).NewValue = (object) inpiDetail.ExpireDate;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.expireDate>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.siteID> e)
  {
    if (((PXSelectBase<INPIHeader>) this.PIHeader).Current != null)
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.siteID>, INBarCodeItem, object>) e).NewValue = (object) ((PXSelectBase<INPIHeader>) this.PIHeader).Current.SiteID;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<INBarCodeItem, INBarCodeItem.siteID>>) e).Cancel = true;
  }

  protected virtual void _(Events.RowUpdated<INBarCodeItem> e)
  {
    if (e.Row == null || !e.Row.AutoAddLine.GetValueOrDefault() || !this.AddByBarCode.VerifyRequired(true))
      return;
    Decimal? qty = e.Row.Qty;
    Decimal num = 0M;
    if (!(qty.GetValueOrDefault() > num & qty.HasValue))
      return;
    this.UpdatePhysicalQty();
  }

  private void UpdatePhysicalQty()
  {
    INBarCodeItem current = ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).Current;
    INPIDetail inpiDetail1 = PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<BqlField<INPIHeader.pIID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INPIDetail.inventoryID, IBqlInt>.IsEqual<BqlField<INBarCodeItem.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INPIDetail.subItemID, IBqlInt>.IsEqual<BqlField<INBarCodeItem.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INPIDetail.locationID, IBqlInt>.IsEqual<BqlField<INBarCodeItem.locationID, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.lotSerialNbr, IsNull>>>>.Or<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<BqlField<INBarCodeItem.lotSerialNbr, IBqlString>.FromCurrent>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    INPIDetail copy1;
    if (inpiDetail1 == null)
    {
      INPIDetail copy2 = PXCache<INPIDetail>.CreateCopy(((PXSelectBase<INPIDetail>) this.PIDetail).Insert(new INPIDetail()
      {
        InventoryID = current.InventoryID
      }));
      copy2.SubItemID = current.SubItemID;
      copy2.LocationID = current.LocationID;
      copy2.LotSerialNbr = current.LotSerialNbr;
      copy1 = PXCache<INPIDetail>.CreateCopy(((PXSelectBase<INPIDetail>) this.PIDetail).Update(copy2));
      copy1.PhysicalQty = current.BaseQty;
      copy1.ExpireDate = current.ExpireDate;
    }
    else
    {
      copy1 = PXCache<INPIDetail>.CreateCopy(inpiDetail1);
      copy1.PhysicalQty = new Decimal?(copy1.PhysicalQty.GetValueOrDefault() + current.BaseQty.GetValueOrDefault());
    }
    if (!string.IsNullOrEmpty(current.ReasonCode))
      copy1.ReasonCode = current.ReasonCode;
    INPIDetail inpiDetail2 = ((PXSelectBase<INPIDetail>) this.PIDetail).Update(copy1);
    current.Description = PXMessages.LocalizeFormatNoPrefixNLA("Item {0}{1} updated physical quantity {2} {3} line {4}.", new object[5]
    {
      (object) ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.inventoryID>(current).ToString().Trim(),
      ((PXSelectBase<PX.Objects.IN.INSetup>) this.Setup).Current.UseInventorySubItem.GetValueOrDefault() ? (object) (":" + ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.subItemID>(current)?.ToString()) : (object) string.Empty,
      ((PXSelectBase<INBarCodeItem>) this.AddByBarCode).GetValueExt<INBarCodeItem.qty>(current),
      (object) current.UOM,
      (object) inpiDetail2.LineNbr
    });
    this.AddByBarCode.Reset(true);
    ((PXSelectBase) this.AddByBarCode).View.RequestRefresh();
  }

  protected virtual void _(Events.RowSelected<INPIHeader> e)
  {
    if (e.Row == null || ((PXGraph) this).IsContractBasedAPI)
      return;
    ((PXSelectBase) this.PIHeader).Cache.AllowDelete = EnumerableExtensions.IsNotIn<string>(e.Row.Status, "R", "C");
    ((PXSelectBase) this.PIHeader).Cache.AllowUpdate = ((PXSelectBase) this.PIDetail).Cache.AllowInsert = ((PXSelectBase) this.PIDetail).Cache.AllowDelete = ((PXSelectBase) this.PIDetail).Cache.AllowUpdate = EnumerableExtensions.IsIn<string>(e.Row.Status, "N", "E");
    ((PXAction) this.addLine).SetEnabled(((PXSelectBase) this.PIDetail).Cache.AllowUpdate);
  }

  protected virtual void _(Events.RowDeleting<INPIHeader> e)
  {
    if (e.Row == null)
      return;
    this.CreatePILocksManager().UnlockInventory();
  }

  protected virtual void _(Events.RowSelected<INPIDetail> e)
  {
    if (e.Row == null)
      return;
    INLotSerClass lsc_rec = this.SelectLotSerClass(e.Row.InventoryID);
    bool flag1 = e.Row.LineType != "N";
    bool flag2 = flag1 & this.LSRequired(lsc_rec);
    bool? nullable;
    int num1;
    if (flag2)
    {
      nullable = lsc_rec.LotSerTrackExpiration;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag3 = num1 != 0;
    INPIDetail row = e.Row;
    int num2;
    if (row == null)
    {
      num2 = 0;
    }
    else
    {
      Decimal? varQty = row.VarQty;
      Decimal num3 = 0M;
      num2 = varQty.GetValueOrDefault() > num3 & varQty.HasValue ? 1 : 0;
    }
    bool flag4 = num2 != 0;
    bool flag5 = ((PXSelectBase<INPIHeader>) this.PIHeader).Current.Status == "E";
    PXUIFieldAttribute.SetEnabled<INPIDetail.inventoryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<INPIDetail.subItemID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<INPIDetail.locationID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<INPIDetail.lotSerialNbr>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<INPIDetail.expireDate>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<INPIDetail.physicalQty>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, this.AreKeysFieldsEntered(e.Row));
    PXUIFieldAttribute.SetEnabled<INPIDetail.unitCost>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag5 & flag4);
    PXUIFieldAttribute.SetEnabled<INPIDetail.manualCost>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache, (object) e.Row, flag5 & flag4);
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INPIDetail>>) e).Cache;
    nullable = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current.PIUseTags;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INPIDetail.tagNumber>(cache, (object) null, num4 != 0);
  }

  public virtual Decimal GetBookQty(INPIDetail detail)
  {
    return !this.LSRequired(detail.InventoryID) ? ((Decimal?) INLocationStatus.PK.Find((PXGraph) this, detail.InventoryID, detail.SubItemID, detail.SiteID, detail.LocationID)?.QtyActual).GetValueOrDefault() : ((Decimal?) INLotSerialStatus.PK.Find((PXGraph) this, detail.InventoryID, detail.SubItemID, detail.SiteID, detail.LocationID, detail.LotSerialNbr)?.QtyActual).GetValueOrDefault();
  }

  protected virtual void _(Events.RowInserting<INPIDetail> e)
  {
    if (e.Row == null)
      return;
    e.Row.BookQty = new Decimal?(this.GetBookQty(e.Row));
  }

  protected virtual void _(Events.RowUpdating<INPIDetail> e)
  {
    if (e.NewRow == null || ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<INPIDetail>>) e).Cache.ObjectsEqual<INPIDetail.inventoryID, INPIDetail.siteID, INPIDetail.subItemID, INPIDetail.locationID, INPIDetail.lotSerialNbr>((object) e.Row, (object) e.NewRow))
      return;
    e.NewRow.BookQty = new Decimal?(this.GetBookQty(e.NewRow));
  }

  protected virtual void _(Events.RowDeleting<INPIDetail> e)
  {
    if (e.Row.LineType != "U" && e.ExternalCall)
      throw new PXException("Unable to delete line, just manually added line can be deleted.");
  }

  protected virtual void _(
    Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID>, INPIDetail, object>) e).NewValue == null)
      return;
    if (e.Row == null)
      return;
    try
    {
      this.ValidateDuplicate(e.Row.Status, (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID>, INPIDetail, object>) e).NewValue, e.Row.SubItemID, e.Row.LocationID, e.Row.LotSerialNbr, e.Row.LineNbr);
      this.ValidatePIInventoryLocation((int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID>, INPIDetail, object>) e).NewValue, e.Row.LocationID);
    }
    catch (PXSetPropertyException ex)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, new int?((int) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID>, INPIDetail, object>) e).NewValue));
      ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.inventoryID>, INPIDetail, object>) e).NewValue = (object) inventoryItem?.InventoryCD;
      throw ex;
    }
  }

  protected virtual void _(
    Events.FieldVerifying<INPIDetail, INPIDetail.subItemID> e)
  {
    if (!(((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.subItemID>, INPIDetail, object>) e).NewValue as int?).HasValue || e.Row == null)
      return;
    if (!PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      return;
    try
    {
      this.ValidateDuplicate(e.Row.Status, e.Row.InventoryID, (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.subItemID>, INPIDetail, object>) e).NewValue, e.Row.LocationID, e.Row.LotSerialNbr, e.Row.LineNbr);
    }
    catch (PXSetPropertyException ex)
    {
      INSubItem inSubItem = INSubItem.PK.Find((PXGraph) this, new int?((int) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.subItemID>, INPIDetail, object>) e).NewValue));
      ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.subItemID>, INPIDetail, object>) e).NewValue = (object) inSubItem?.SubItemCD;
      throw ex;
    }
  }

  protected virtual void _(
    Events.FieldVerifying<INPIDetail, INPIDetail.locationID> e)
  {
    if (!(((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.locationID>, INPIDetail, object>) e).NewValue as int?).HasValue)
      return;
    if (e.Row == null)
      return;
    try
    {
      this.ValidateDuplicate(e.Row.Status, e.Row.InventoryID, e.Row.SubItemID, (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.locationID>, INPIDetail, object>) e).NewValue, e.Row.LotSerialNbr, e.Row.LineNbr);
      this.ValidatePIInventoryLocation(e.Row.InventoryID, (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.locationID>, INPIDetail, object>) e).NewValue);
    }
    catch (PXSetPropertyException ex)
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this, new int?((int) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.locationID>, INPIDetail, object>) e).NewValue));
      ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.locationID>, INPIDetail, object>) e).NewValue = (object) inLocation?.LocationCD;
      throw ex;
    }
  }

  protected virtual void _(
    Events.FieldVerifying<INPIDetail, INPIDetail.lotSerialNbr> e)
  {
    if (e.Row == null || !(((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.lotSerialNbr>, INPIDetail, object>) e).NewValue is string newValue))
      return;
    if (!string.IsNullOrEmpty(newValue) && char.IsWhiteSpace(newValue[0]))
      throw new PXSetPropertyException("The value in the Lot/Serial Number column cannot have leading spaces.");
    this.ValidateDuplicate(e.Row.Status, e.Row.InventoryID, e.Row.SubItemID, e.Row.LocationID, newValue, e.Row.LineNbr);
  }

  protected virtual void _(
    Events.FieldVerifying<INPIDetail, INPIDetail.physicalQty> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.physicalQty>, INPIDetail, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((Events.FieldVerifyingBase<Events.FieldVerifying<INPIDetail, INPIDetail.physicalQty>, INPIDetail, object>) e).NewValue;
    INLotSerClass lsc_rec = this.SelectLotSerClass(e.Row.InventoryID);
    if (newValue < 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) 0
      });
    if (lsc_rec != null && this.LSRequired(lsc_rec) && lsc_rec.LotSerTrack == "S" && newValue != 0M && newValue != 1M)
      throw new PXSetPropertyException("Serial-numbered items should have physical quantity only 1 or 0.");
  }

  protected virtual void _(Events.RowPersisting<INPIDetail> e)
  {
    bool? nullable;
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && e.Row.LineType != "N" && e.Row.Status == "E")
    {
      INPIEntry.CheckDefault<INPIDetail.inventoryID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache, e.Row);
      INPIEntry.CheckDefault<INPIDetail.subItemID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache, e.Row);
      INPIEntry.CheckDefault<INPIDetail.locationID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache, e.Row);
      INLotSerClass lsc_rec = this.SelectLotSerClass(e.Row.InventoryID);
      if (this.LSRequired(lsc_rec))
      {
        INPIEntry.CheckDefault<INPIDetail.lotSerialNbr>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache, e.Row);
        nullable = lsc_rec.LotSerTrackExpiration;
        if (nullable.GetValueOrDefault())
          INPIEntry.CheckDefault<INPIDetail.expireDate>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache, e.Row);
      }
    }
    if (e.Row == null || ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<INPIDetail>>) e).Cache.GetStatus((object) e.Row) != 2)
      return;
    PX.Objects.IN.INSetup inSetup1 = PXResultset<PX.Objects.IN.INSetup>.op_Implicit(((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Select(Array.Empty<object>()));
    nullable = inSetup1.PIUseTags;
    if (!nullable.GetValueOrDefault())
      return;
    PX.Objects.IN.INSetup inSetup2 = inSetup1;
    int? piLastTagNumber = inSetup2.PILastTagNumber;
    inSetup2.PILastTagNumber = piLastTagNumber.HasValue ? new int?(piLastTagNumber.GetValueOrDefault() + 1) : new int?();
    e.Row.TagNumber = inSetup1.PILastTagNumber;
    ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Update(inSetup1);
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID> e)
  {
    if (e.Row == null || ((PXSelectBase) this.PIHeader).Cache.Current == null)
      return;
    if (e.Row.InventoryID.HasValue)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID>>) e).Cache.SetDefaultExt<INPIDetail.subItemID>((object) e.Row);
    else
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID>>) e).Cache.SetValue<INPIDetail.subItemID>((object) e.Row, (object) null);
    if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID>>) e).Cache.SetValue<INPIDetail.locationID>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID>>) e).Cache.SetValue<INPIDetail.lotSerialNbr>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.inventoryID>>) e).Cache.SetValueExt<INPIDetail.physicalQty>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.subItemID> e)
  {
    if (((PXSelectBase) this.PIHeader).Cache.Current == null || e.Row == null)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.subItemID>>) e).Cache.SetValueExt<INPIDetail.physicalQty>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.locationID> e)
  {
    if (((PXSelectBase) this.PIHeader).Cache.Current == null || e.Row == null || e.Row.LocationID.HasValue)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.locationID>>) e).Cache.SetValueExt<INPIDetail.physicalQty>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.lotSerialNbr> e)
  {
    if (((PXSelectBase) this.PIHeader).Cache.Current == null || e.Row == null || !string.IsNullOrWhiteSpace(e.Row.LotSerialNbr))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.lotSerialNbr>>) e).Cache.SetValueExt<INPIDetail.physicalQty>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty> e)
  {
    if (e.Row == null)
      return;
    if (this.AreKeysFieldsEntered(e.Row))
    {
      Decimal? nullable1 = e.Row.PhysicalQty;
      if (nullable1.HasValue)
      {
        nullable1 = e.Row.PhysicalQty;
        Decimal? nullable2 = e.Row.BookQty;
        Decimal num1 = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?()).Value;
        ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValue<INPIDetail.varQty>((object) e.Row, (object) num1);
        ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValue<INPIDetail.status>((object) e.Row, (object) "E");
        if (((PXSelectBase<INPIHeader>) this.PIHeader).Current?.Status != "E")
          return;
        if (num1 <= 0M && e.Row.ManualCost.GetValueOrDefault())
          ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValueExt<INPIDetail.manualCost>((object) e.Row, (object) false);
        Decimal? nullable3;
        if (((Events.FieldUpdatedBase<Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>, INPIDetail, object>) e).OldValue == null)
        {
          nullable1 = new Decimal?();
          nullable3 = nullable1;
        }
        else
        {
          nullable1 = (Decimal?) ((Events.FieldUpdatedBase<Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>, INPIDetail, object>) e).OldValue;
          nullable2 = e.Row.BookQty;
          nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        }
        Decimal? nullable4 = nullable3;
        if (!(num1 >= 0M))
          return;
        if (((Events.FieldUpdatedBase<Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>, INPIDetail, object>) e).OldValue != null)
        {
          nullable2 = nullable4;
          Decimal num2 = 0M;
          if (!(nullable2.GetValueOrDefault() < num2 & nullable2.HasValue))
            goto label_16;
        }
        this.DefaultDebitLineCost(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache, e.Row);
label_16:
        PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache;
        INPIDetail row = e.Row;
        nullable2 = e.Row.UnitCost;
        Decimal num3 = num1;
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable5 = nullable1;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() * num3);
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) nullable5;
        cache.SetValueExt<INPIDetail.extVarCost>((object) row, (object) local);
        return;
      }
    }
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValue<INPIDetail.manualCost>((object) e.Row, (object) false);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValue<INPIDetail.varQty>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValueExt<INPIDetail.unitCost>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValueExt<INPIDetail.extVarCost>((object) e.Row, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.physicalQty>>) e).Cache.SetValue<INPIDetail.status>((object) e.Row, (object) "N");
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.manualCost> e)
  {
    INPIDetail row = e.Row;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      bool? manualCost = row.ManualCost;
      bool flag = false;
      num = manualCost.GetValueOrDefault() == flag & manualCost.HasValue ? 1 : 0;
    }
    if (num == 0 || !(bool) ((Events.FieldUpdatedBase<Events.FieldUpdated<INPIDetail, INPIDetail.manualCost>, INPIDetail, object>) e).OldValue)
      return;
    this.DefaultDebitLineCost(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.manualCost>>) e).Cache, e.Row);
  }

  protected virtual void _(
    Events.FieldUpdating<INPIDetail, INPIDetail.unitCost> e)
  {
    if (e.Row == null)
      return;
    Decimal? varQty = e.Row.VarQty;
    Decimal num = 0M;
    if (!(varQty.GetValueOrDefault() > num & varQty.HasValue) || ((Events.FieldUpdatingBase<Events.FieldUpdating<INPIDetail, INPIDetail.unitCost>>) e).NewValue != null)
      return;
    ((Events.FieldUpdatingBase<Events.FieldUpdating<INPIDetail, INPIDetail.unitCost>>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    Events.FieldUpdated<INPIDetail, INPIDetail.unitCost> e)
  {
    if (e.Row == null)
      return;
    Decimal? nullable1 = e.Row.VarQty;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue) || !((Events.FieldUpdatedBase<Events.FieldUpdated<INPIDetail, INPIDetail.unitCost>>) e).ExternalCall || e.Row.IsCostDefaulting.GetValueOrDefault())
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.unitCost>>) e).Cache.SetValue<INPIDetail.manualCost>((object) e.Row, (object) true);
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INPIDetail, INPIDetail.unitCost>>) e).Cache;
    INPIDetail row = e.Row;
    nullable1 = e.Row.UnitCost;
    Decimal? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = e.Row.VarQty;
      if (nullable1.HasValue)
      {
        nullable1 = e.Row.UnitCost;
        Decimal num2 = nullable1.Value;
        nullable1 = e.Row.VarQty;
        Decimal num3 = nullable1.Value;
        nullable2 = new Decimal?(num2 * num3);
        goto label_7;
      }
    }
    nullable1 = new Decimal?();
    nullable2 = nullable1;
label_7:
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable2;
    cache.SetValueExt<INPIDetail.extVarCost>((object) row, (object) local);
  }

  protected virtual void _(Events.RowUpdated<INPIDetail> e)
  {
    int updatedLinesCount = 0;
    if (this.IsCostCalculationEnabled())
    {
      Decimal? physicalQty = e.OldRow.PhysicalQty;
      if (!physicalQty.HasValue && this.AreKeysFieldsEntered(e.Row))
      {
        ICollection<INPIDetail> relatedLines = this.GetRelatedLines(e.Row);
        this.UpdateRelatedLinesCost(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INPIDetail>>) e).Cache, (IEnumerable<INPIDetail>) relatedLines, ref updatedLinesCount);
        if (updatedLinesCount > 0 || this.AreTotalsAffected(e.OldRow, e.Row))
          this.RecalcTotals();
        if (updatedLinesCount <= 0)
          return;
        ((PXSelectBase) this.PIDetail).View.RequestRefresh();
        return;
      }
      physicalQty = e.Row.PhysicalQty;
      if (!physicalQty.HasValue && this.AreKeysFieldsEntered(e.OldRow))
      {
        ICollection<INPIDetail> relatedLines = this.GetRelatedLines(e.OldRow);
        this.UpdateRelatedLinesCost(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INPIDetail>>) e).Cache, (IEnumerable<INPIDetail>) relatedLines, ref updatedLinesCount);
        if (updatedLinesCount > 0 || this.AreTotalsAffected(e.OldRow, e.Row))
          this.RecalcTotals();
        if (updatedLinesCount <= 0)
          return;
        ((PXSelectBase) this.PIDetail).View.RequestRefresh();
        return;
      }
      if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INPIDetail>>) e).Cache.ObjectsEqual<INPIDetail.inventoryID, INPIDetail.subItemID, INPIDetail.locationID, INPIDetail.lotSerialNbr>((object) e.Row, (object) e.OldRow) && this.AreKeysFieldsEntered(e.OldRow))
      {
        ICollection<INPIDetail> relatedLines = this.GetRelatedLines(e.OldRow);
        this.UpdateRelatedLinesCost(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INPIDetail>>) e).Cache, (IEnumerable<INPIDetail>) relatedLines, ref updatedLinesCount);
      }
      if (this.AreKeysFieldsEntered(e.Row))
      {
        ICollection<INPIDetail> relatedLines = this.GetRelatedLines(e.Row);
        this.UpdateRelatedLinesCost(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INPIDetail>>) e).Cache, (IEnumerable<INPIDetail>) relatedLines, ref updatedLinesCount);
      }
    }
    if (updatedLinesCount > 0 || this.AreTotalsAffected(e.OldRow, e.Row))
      this.RecalcTotals();
    if (updatedLinesCount <= 0)
      return;
    ((PXSelectBase) this.PIDetail).View.RequestRefresh();
  }

  protected virtual void _(Events.RowDeleted<INPIDetail> e)
  {
    if (((PXSelectBase<INPIHeader>) this.PIHeader).Current == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.PIHeader).Cache.GetStatus((object) ((PXSelectBase<INPIHeader>) this.PIHeader).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    Decimal? varQty;
    if (this.IsCostCalculationEnabled() && this.AreKeysFieldsEntered(e.Row))
    {
      varQty = e.Row.VarQty;
      if (!(varQty.GetValueOrDefault() == 0M))
      {
        ICollection<INPIDetail> relatedLines = this.GetRelatedLines(e.Row);
        this.UpdateRelatedLinesCost(((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<INPIDetail>>) e).Cache, (IEnumerable<INPIDetail>) relatedLines);
        this.RecalcTotals();
        ((PXSelectBase) this.PIDetail).View.RequestRefresh();
        return;
      }
    }
    varQty = e.Row.VarQty;
    if (!(varQty.GetValueOrDefault() != 0M))
      return;
    this.RecalcTotals();
    ((PXSelectBase) this.PIDetail).View.RequestRefresh();
  }

  protected virtual void _(Events.RowPersisting<PX.Objects.IN.INSetup> e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
      return;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.IN.INSetup.iNTransitAcctID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<PX.Objects.IN.INSetup>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.IN.INSetup.iNTransitSubID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<PX.Objects.IN.INSetup>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
  }

  protected virtual void _(
    Events.FieldUpdated<PIGeneratorSettings, PIGeneratorSettings.pIClassID> e)
  {
    if (e.Row == null)
      return;
    INPIClass inpiClass = (INPIClass) PXSelectorAttribute.Select<PIGeneratorSettings.pIClassID>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PIGeneratorSettings, PIGeneratorSettings.pIClassID>>) e).Cache, (object) e.Row);
    if (inpiClass == null)
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (INPIClass)];
    foreach (string field in (List<string>) ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PIGeneratorSettings, PIGeneratorSettings.pIClassID>>) e).Cache.Fields)
    {
      if (string.Compare(field, typeof (PIGeneratorSettings.pIClassID).Name, true) != 0 && cach.Fields.Contains(field))
        ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PIGeneratorSettings, PIGeneratorSettings.pIClassID>>) e).Cache.SetValuePending((object) e.Row, field, cach.GetValueExt((object) inpiClass, field));
    }
  }

  public virtual bool IsCostCalculationEnabled()
  {
    INPIHeader current = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    return !this.DisableCostCalculation && current?.Status == "E";
  }

  private static void CheckDefault<Field>(PXCache sender, INPIDetail row) where Field : IBqlField
  {
    string name = typeof (Field).Name;
    if (sender.GetValue<Field>((object) row) != null)
      return;
    if (sender.RaiseExceptionHandling(name, (object) row, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{name}]"
    }))))
      throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) name
      });
  }

  protected virtual void ValidatePIInventoryLocation(int? inventoryID, int? locationID)
  {
    if (inventoryID.HasValue && locationID.HasValue && !new PILocksInspector(((PXSelectBase<INPIHeader>) this.PIHeader).Current.SiteID.Value).IsInventoryLocationIncludedInPI(inventoryID, locationID, ((PXSelectBase<INPIHeader>) this.PIHeader).Current.PIID))
      throw new PXSetPropertyException("Combination of selected Inventory Item and Warehouse Location is not allowed for this Physical Count.");
  }

  private void ValidateDuplicate(
    string status,
    int? inventoryID,
    int? subItemID,
    int? locationID,
    string lotSerialNbr,
    int? lineNbr)
  {
    if (!inventoryID.HasValue || !subItemID.HasValue || !locationID.HasValue)
      return;
    foreach (PXResult<INPIDetail> pxResult in PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<BqlField<INPIHeader.pIID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INPIDetail.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INPIDetail.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INPIDetail.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INPIDetail.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) inventoryID,
      (object) subItemID,
      (object) locationID,
      (object) lineNbr
    }))
    {
      INPIDetail inpiDetail = PXResult<INPIDetail>.op_Implicit(pxResult);
      if (string.Equals((inpiDetail.LotSerialNbr ?? "").Trim(), (lotSerialNbr ?? "").Trim(), StringComparison.OrdinalIgnoreCase))
        throw new PXSetPropertyException("This Combination Is Used Already in Line Nbr. {0}", new object[1]
        {
          (object) inpiDetail.LineNbr
        });
    }
    if (string.IsNullOrEmpty(lotSerialNbr))
      return;
    INLotSerClass inLotSerClass = this.SelectLotSerClass(inventoryID);
    if (!(inLotSerClass.LotSerTrack == "S") || !(inLotSerClass.LotSerAssign == "R"))
      return;
    INPIDetail inpiDetail1 = PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<BqlField<INPIHeader.pIID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INPIDetail.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INPIDetail.lineType, IBqlString>.IsEqual<INPIDetLineType.userEntered>>>>.And<BqlOperand<INPIDetail.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) inventoryID,
      (object) lotSerialNbr,
      (object) lineNbr
    }));
    if (inpiDetail1 != null)
      throw new PXSetPropertyException("This  Serial Number Is Used Already in Line Nbr. {0}", new object[1]
      {
        (object) inpiDetail1.LineNbr
      });
    if (!(status == "N"))
      return;
    INLotSerialStatus inLotSerialStatus = PXResultset<INLotSerialStatus>.op_Implicit(PXSelectBase<INLotSerialStatus, PXViewOf<INLotSerialStatus>.BasedOn<SelectFromBase<INLotSerialStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatus.siteID, Equal<BqlField<INPIHeader.siteID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatus.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatus.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INLotSerialStatus.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) inventoryID,
      (object) lotSerialNbr
    }));
    if (inLotSerialStatus == null)
      return;
    if (PXResultset<INPIDetail>.op_Implicit(PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<BqlField<INPIHeader.pIID, IBqlString>.FromCurrent>>>>, And<BqlOperand<INPIDetail.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INPIDetail.locationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INPIDetail.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) inventoryID,
      (object) inLotSerialStatus.LocationID,
      (object) lotSerialNbr,
      (object) lineNbr
    })) == null)
      throw new PXSetPropertyException("This Serial Number Is Used Already for the item");
  }

  protected virtual bool LSRequired(int? p_InventoryID)
  {
    return this.LSRequired(this.SelectLotSerClass(p_InventoryID));
  }

  protected virtual bool LSRequired(INLotSerClass lsc_rec)
  {
    return lsc_rec != null && lsc_rec.LotSerTrack != "N" && lsc_rec.LotSerAssign == "R";
  }

  protected virtual INLotSerClass SelectLotSerClass(int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return (INLotSerClass) null;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, inventoryID);
    return inventoryItem.LotSerClassID != null ? INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID) : (INLotSerClass) null;
  }

  protected virtual bool AreKeysFieldsEntered(INPIDetail detail)
  {
    if (detail == null)
      return false;
    bool flag = !this.LSRequired(detail.InventoryID) || !string.IsNullOrWhiteSpace(detail.LotSerialNbr);
    int? nullable = detail.InventoryID;
    int num1;
    if (nullable.HasValue)
    {
      nullable = detail.SubItemID;
      if (nullable.HasValue)
      {
        nullable = detail.LocationID;
        num1 = nullable.HasValue ? 1 : 0;
        goto label_6;
      }
    }
    num1 = 0;
label_6:
    int num2 = flag ? 1 : 0;
    return (num1 & num2) != 0;
  }

  protected virtual string GetDetailsGroupingKey(INPIDetail detail)
  {
    if (!this.AreKeysFieldsEntered(detail))
      throw new InvalidOperationException();
    List<string> stringList = new List<string>(4);
    int? nullable = detail.InventoryID;
    stringList.Add(nullable.Value.ToString());
    nullable = detail.SubItemID;
    stringList.Add(nullable.Value.ToString());
    List<string> values = stringList;
    InventoryItem parent = (InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.inventoryID>.FindParent((PXGraph) this, (INPIDetail.inventoryID) detail, (PKFindOptions) 0);
    INLocation inLocation = INLocation.PK.Find((PXGraph) this, new int?(detail.LocationID.Value));
    if (EnumerableExtensions.IsIn<string>(parent.ValMethod, "A", "F") && inLocation.IsCosted.GetValueOrDefault())
      values.Add(detail.LocationID.Value.ToString());
    else if (parent.ValMethod == "S")
      values.Add(detail.LotSerialNbr);
    return string.Join("-", (IEnumerable<string>) values);
  }

  protected virtual ICollection<INPIDetail> GetRelatedLines(INPIDetail detail)
  {
    if (!this.AreKeysFieldsEntered(detail))
      throw new InvalidOperationException();
    if (!detail.VarQty.HasValue)
      return (ICollection<INPIDetail>) new List<INPIDetail>();
    List<object> objectList = new List<object>();
    FbqlSelect<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INPIDetail.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INPIDetail.pIID, IBqlString>.IsEqual<P.AsString>>>, INPIDetail>.View view = new FbqlSelect<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INPIDetail.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INPIDetail.pIID, IBqlString>.IsEqual<P.AsString>>>, INPIDetail>.View((PXGraph) this);
    objectList.Add((object) detail.InventoryID);
    objectList.Add((object) detail.SubItemID);
    objectList.Add((object) detail.PIID);
    InventoryItem parent = (InventoryItem) PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INPIDetail>.By<INPIDetail.inventoryID>.FindParent((PXGraph) this, (INPIDetail.inventoryID) detail, (PKFindOptions) 0);
    INLocation inLocation = INLocation.PK.Find((PXGraph) this, new int?(detail.LocationID.Value));
    if (EnumerableExtensions.IsIn<string>(parent.ValMethod, "A", "F"))
    {
      if (inLocation != null && inLocation.IsCosted.GetValueOrDefault())
      {
        ((PXSelectBase<INPIDetail>) view).WhereAnd<Where<BqlOperand<INPIDetail.locationID, IBqlInt>.IsEqual<P.AsInt>>>();
        objectList.Add((object) detail.LocationID);
      }
      else
        ((PXSelectBase<INPIDetail>) view).Join<InnerJoin<INLocation, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.locationID, Equal<INPIDetail.locationID>>>>>.And<BqlOperand<INLocation.isCosted, IBqlBool>.IsEqual<False>>>>>();
    }
    else if (parent.ValMethod == "S")
    {
      ((PXSelectBase<INPIDetail>) view).WhereAnd<Where<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>();
      objectList.Add((object) detail.LotSerialNbr);
    }
    List<INPIDetail> list = GraphHelper.RowCast<INPIDetail>((IEnumerable) ((PXSelectBase<INPIDetail>) view).Select(objectList.ToArray())).Where<INPIDetail>((Func<INPIDetail, bool>) (line => this.AreKeysFieldsEntered(line) && line.VarQty.HasValue)).ToList<INPIDetail>();
    if (inLocation == null && list.FirstOrDefault<INPIDetail>((Func<INPIDetail, bool>) (line =>
    {
      int? lineNbr1 = line.LineNbr;
      int? lineNbr2 = detail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })) == null)
      list.Add(detail);
    return (ICollection<INPIDetail>) list;
  }

  protected virtual IEnumerable<INPIEntry.ProjectedTranRec> UpdateRelatedLinesCost(
    PXCache detailCache,
    IEnumerable<INPIDetail> relatedLines,
    bool adjustmentCreation = false,
    bool forseDebitLinesRecalculation = false)
  {
    int updatedLinesCount = 0;
    return this.UpdateRelatedLinesCost(detailCache, relatedLines, ref updatedLinesCount, adjustmentCreation, forseDebitLinesRecalculation = false);
  }

  protected virtual IEnumerable<INPIEntry.ProjectedTranRec> UpdateRelatedLinesCost(
    PXCache detailCache,
    IEnumerable<INPIDetail> relatedLines,
    ref int updatedLinesCount,
    bool adjustmentCreation = false,
    bool forseDebitLinesRecalculation = false)
  {
    List<INPIDetail> inpiDetailList = new List<INPIDetail>();
    List<INPIDetail> creditLines = new List<INPIDetail>();
    foreach (INPIDetail relatedLine in relatedLines)
    {
      if (this.AreKeysFieldsEntered(relatedLine))
      {
        Decimal? nullable = relatedLine.VarQty;
        if (nullable.HasValue)
        {
          nullable = relatedLine.VarQty;
          Decimal num1 = 0M;
          if (nullable.GetValueOrDefault() >= num1 & nullable.HasValue)
          {
            if (!forseDebitLinesRecalculation)
            {
              nullable = relatedLine.UnitCost;
              if (nullable.HasValue)
              {
                nullable = relatedLine.ExtVarCost;
                if (nullable.HasValue)
                  goto label_10;
              }
            }
            this.DefaultDebitLineCost(detailCache, relatedLine, ref updatedLinesCount);
          }
label_10:
          nullable = relatedLine.VarQty;
          Decimal num2 = 0M;
          if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
          {
            nullable = relatedLine.VarQty;
            Decimal num3 = 0M;
            if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
            {
              inpiDetailList.Add(relatedLine);
              continue;
            }
            creditLines.Add(relatedLine);
            continue;
          }
          continue;
        }
      }
      throw new InvalidOperationException();
    }
    Decimal additionalDebitQty = 0M;
    Decimal additionalDebitExtCost = 0M;
    List<INPIEntry.ProjectedTranRec> projectedTranRecList1 = new List<INPIEntry.ProjectedTranRec>();
    foreach (INPIDetail inpiDetail in inpiDetailList)
    {
      Decimal num4 = additionalDebitQty;
      Decimal? nullable = inpiDetail.VarQty;
      Decimal num5 = nullable.Value;
      additionalDebitQty = num4 + num5;
      Decimal num6 = additionalDebitExtCost;
      nullable = inpiDetail.UnitCost;
      Decimal num7 = nullable.Value;
      nullable = inpiDetail.VarQty;
      Decimal num8 = nullable.Value;
      Decimal num9 = num7 * num8;
      additionalDebitExtCost = num6 + num9;
      List<INPIEntry.ProjectedTranRec> projectedTranRecList2 = projectedTranRecList1;
      INPIDetail detail = inpiDetail;
      nullable = new Decimal?();
      Decimal? tranUnitCost = nullable;
      nullable = new Decimal?();
      Decimal? tranQty = nullable;
      nullable = new Decimal?();
      Decimal? tranTotalCost = nullable;
      int? invAcctID = new int?();
      int? invSubID = new int?();
      INPIEntry.ProjectedTranRec projectedTran = this.CreateProjectedTran(detail, false, tranUnitCost, tranQty, tranTotalCost, invAcctID: invAcctID, invSubID: invSubID);
      projectedTranRecList2.Add(projectedTran);
    }
    if (creditLines.Count == 0)
      return (IEnumerable<INPIEntry.ProjectedTranRec>) projectedTranRecList1;
    projectedTranRecList1.AddRange(this.UpdateCreditLinesCost(detailCache, (IEnumerable<INPIDetail>) creditLines, additionalDebitQty, additionalDebitExtCost, adjustmentCreation, ref updatedLinesCount));
    return (IEnumerable<INPIEntry.ProjectedTranRec>) projectedTranRecList1;
  }

  protected virtual void DefaultDebitLineCost(PXCache detailCache, INPIDetail debitLine)
  {
    int updatedLinesCount = 0;
    this.DefaultDebitLineCost(detailCache, debitLine, ref updatedLinesCount);
  }

  protected virtual void DefaultDebitLineCost(
    PXCache detailCache,
    INPIDetail debitLine,
    ref int updatedLinesCount)
  {
    if (this.AreKeysFieldsEntered(debitLine))
    {
      Decimal? nullable1 = debitLine.VarQty;
      if (nullable1.HasValue)
      {
        if (debitLine.ManualCost.GetValueOrDefault())
          return;
        debitLine.IsCostDefaulting = new bool?(true);
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, new int?(debitLine.InventoryID.Value));
        INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this, debitLine.InventoryID, debitLine.SiteID);
        INSite inSite = INSite.PK.Find((PXGraph) this, debitLine.SiteID);
        Decimal num1;
        if (inventoryItem.ValMethod == "T")
        {
          InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem.InventoryID, inSite.BaseCuryID);
          Decimal? nullable2;
          if (inItemSite == null)
          {
            nullable1 = new Decimal?();
            nullable2 = nullable1;
          }
          else
            nullable2 = inItemSite.StdCost;
          nullable1 = nullable2;
          Decimal valueOrDefault;
          if (!(nullable1.GetValueOrDefault() != 0M))
          {
            Decimal? nullable3;
            if (itemCurySettings == null)
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
            }
            else
              nullable3 = itemCurySettings.StdCost;
            nullable1 = nullable3;
            valueOrDefault = nullable1.GetValueOrDefault();
          }
          else
          {
            Decimal? nullable4;
            if (inItemSite == null)
            {
              nullable1 = new Decimal?();
              nullable4 = nullable1;
            }
            else
              nullable4 = inItemSite.StdCost;
            nullable1 = nullable4;
            valueOrDefault = nullable1.Value;
          }
          num1 = valueOrDefault;
        }
        else
        {
          INItemCost inItemCost = INItemCost.PK.Find((PXGraph) this, debitLine.InventoryID, inSite.BaseCuryID);
          if (inventoryItem.ValMethod == "S")
          {
            Decimal lastSpecificLayer = this.GetCostFromLastSpecificLayer(debitLine);
            Decimal num2;
            if (!(lastSpecificLayer != 0M))
            {
              Decimal? nullable5;
              if (inItemSite == null)
              {
                nullable1 = new Decimal?();
                nullable5 = nullable1;
              }
              else
                nullable5 = inItemSite.LastCost;
              nullable1 = nullable5;
              if (!(nullable1.GetValueOrDefault() != 0M))
              {
                Decimal? nullable6;
                if (inItemCost == null)
                {
                  nullable1 = new Decimal?();
                  nullable6 = nullable1;
                }
                else
                  nullable6 = inItemCost.LastCost;
                nullable1 = nullable6;
                num2 = nullable1.GetValueOrDefault();
              }
              else
              {
                Decimal? nullable7;
                if (inItemSite == null)
                {
                  nullable1 = new Decimal?();
                  nullable7 = nullable1;
                }
                else
                  nullable7 = inItemSite.LastCost;
                nullable1 = nullable7;
                num2 = nullable1.Value;
              }
            }
            else
              num2 = lastSpecificLayer;
            num1 = num2;
          }
          else
          {
            string str = inventoryItem.ValMethod == "A" ? inSite.AvgDefaultCost : inSite.FIFODefaultCost;
            Decimal valueOrDefault;
            if (str == "A")
            {
              Decimal? nullable8;
              if (inItemSite == null)
              {
                nullable1 = new Decimal?();
                nullable8 = nullable1;
              }
              else
                nullable8 = inItemSite.AvgCost;
              nullable1 = nullable8;
              if (nullable1.GetValueOrDefault() > 0M)
              {
                Decimal? nullable9;
                if (inItemSite == null)
                {
                  nullable1 = new Decimal?();
                  nullable9 = nullable1;
                }
                else
                  nullable9 = inItemSite.AvgCost;
                nullable1 = nullable9;
                valueOrDefault = nullable1.Value;
                goto label_62;
              }
            }
            Decimal? nullable10;
            if (inItemSite == null)
            {
              nullable1 = new Decimal?();
              nullable10 = nullable1;
            }
            else
              nullable10 = inItemSite.LastCost;
            nullable1 = nullable10;
            if (!(nullable1.GetValueOrDefault() > 0M))
            {
              if (str == "A")
              {
                Decimal? nullable11;
                if (inItemCost == null)
                {
                  nullable1 = new Decimal?();
                  nullable11 = nullable1;
                }
                else
                  nullable11 = inItemCost.AvgCost;
                nullable1 = nullable11;
                if (nullable1.GetValueOrDefault() > 0M)
                {
                  nullable1 = inItemCost.AvgCost;
                  valueOrDefault = nullable1.Value;
                  goto label_62;
                }
              }
              Decimal? nullable12;
              if (inItemCost == null)
              {
                nullable1 = new Decimal?();
                nullable12 = nullable1;
              }
              else
                nullable12 = inItemCost.LastCost;
              nullable1 = nullable12;
              valueOrDefault = nullable1.GetValueOrDefault();
            }
            else
            {
              Decimal? nullable13;
              if (inItemSite == null)
              {
                nullable1 = new Decimal?();
                nullable13 = nullable1;
              }
              else
                nullable13 = inItemSite.LastCost;
              nullable1 = nullable13;
              valueOrDefault = nullable1.Value;
            }
label_62:
            num1 = valueOrDefault;
          }
        }
        nullable1 = debitLine.ExtVarCost;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        detailCache.SetValueExt<INPIDetail.unitCost>((object) debitLine, (object) num1);
        PXCache pxCache = detailCache;
        INPIDetail inpiDetail = debitLine;
        Decimal num3 = PXDBPriceCostAttribute.Round(num1);
        nullable1 = debitLine.VarQty;
        Decimal num4 = nullable1.Value;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal> local = (ValueType) (num3 * num4);
        pxCache.SetValueExt<INPIDetail.extVarCost>((object) inpiDetail, (object) local);
        debitLine.IsCostDefaulting = new bool?(false);
        GraphHelper.MarkUpdated(detailCache, (object) debitLine, true);
        nullable1 = debitLine.ExtVarCost;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        if (!(Math.Abs(valueOrDefault1 - valueOrDefault2) > 0.000001M))
          return;
        ++updatedLinesCount;
        return;
      }
    }
    throw new InvalidOperationException();
  }

  protected Decimal GetCostFromLastSpecificLayer(INPIDetail specificDetail)
  {
    INCostStatus inCostStatus = PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostSubItemXRef>.On<BqlOperand<INCostSubItemXRef.costSubItemID, IBqlInt>.IsEqual<INCostStatus.costSubItemID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsGreaterEqual<decimal0>>>, And<BqlOperand<INCostSubItemXRef.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INCostStatus.valMethod, IBqlString>.IsEqual<INValMethod.specific>>>, And<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INCostStatus.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>.Order<By<BqlField<INCostStatus.receiptDate, IBqlDateTime>.Desc, BqlField<INCostStatus.receiptNbr, IBqlString>.Desc>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) specificDetail.InventoryID,
      (object) specificDetail.SubItemID,
      (object) specificDetail.SiteID,
      (object) specificDetail.LotSerialNbr
    }));
    if (inCostStatus != null)
    {
      Decimal? nullable = inCostStatus.QtyOnHand;
      if (!(nullable.GetValueOrDefault() == 0M))
      {
        nullable = inCostStatus.TotalCost;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        nullable = inCostStatus.QtyOnHand;
        Decimal num = nullable.Value;
        return valueOrDefault / num;
      }
    }
    return 0M;
  }

  protected virtual IEnumerable<INPIEntry.CostLayerInfo> ReadCostLayers(INPIDetail detail)
  {
    return (IEnumerable<INPIEntry.CostLayerInfo>) GraphHelper.RowCast<INCostStatus>((IEnumerable) PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostSubItemXRef>.On<BqlOperand<INCostSubItemXRef.costSubItemID, IBqlInt>.IsEqual<INCostStatus.costSubItemID>>>, FbqlJoins.Inner<INLocation>.On<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INCostStatus.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>, And<BqlOperand<INCostSubItemXRef.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.costSiteID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.valMethod, In3<INValMethod.standard, INValMethod.specific>>>>>.Or<BqlOperand<INLocation.isCosted, IBqlBool>.IsEqual<False>>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.costSiteID, Equal<P.AsInt>>>>>.And<Not<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.valMethod, In3<INValMethod.standard, INValMethod.specific>>>>>.Or<BqlOperand<INLocation.isCosted, IBqlBool>.IsEqual<False>>>>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.lotSerialNbr, Equal<P.AsString>>>>, Or<BqlOperand<INCostStatus.lotSerialNbr, IBqlString>.IsNull>>>.Or<BqlOperand<INCostStatus.lotSerialNbr, IBqlString>.IsEqual<Empty>>>>>.Config>.Select((PXGraph) this, new object[6]
    {
      (object) detail.LocationID,
      (object) detail.InventoryID,
      (object) detail.SubItemID,
      (object) detail.SiteID,
      (object) detail.LocationID,
      (object) detail.LotSerialNbr
    })).AsEnumerable<INCostStatus>().Select<INCostStatus, INPIEntry.CostLayerInfo>((Func<INCostStatus, INPIEntry.CostLayerInfo>) (layer => new INPIEntry.CostLayerInfo(layer, "N"))).ToList<INPIEntry.CostLayerInfo>();
  }

  protected virtual IComparer<INPIEntry.CostLayerInfo> GetCostLayerComparer(
    INItemSiteSettings itemSite)
  {
    return (IComparer<INPIEntry.CostLayerInfo>) new CostLayerComparer(itemSite);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  protected virtual IEnumerable<INPIEntry.ProjectedTranRec> UpdateCreditLinesCost(
    PXCache detailCache,
    IEnumerable<INPIDetail> creditLines,
    Decimal additionalDebitQty,
    Decimal additionalDebitExtCost,
    bool adjustmentCreation)
  {
    int updatedLinesCount = 0;
    return this.UpdateCreditLinesCost(detailCache, creditLines, additionalDebitQty, additionalDebitExtCost, adjustmentCreation, ref updatedLinesCount);
  }

  protected virtual IEnumerable<INPIEntry.ProjectedTranRec> UpdateCreditLinesCost(
    PXCache detailCache,
    IEnumerable<INPIDetail> creditLines,
    Decimal additionalDebitQty,
    Decimal additionalDebitExtCost,
    bool adjustmentCreation,
    ref int updatedLinesCount)
  {
    INPIDetail detail = creditLines.First<INPIDetail>();
    Decimal additionalUnitCost = this.GetAdditionalUnitCost(detail, additionalDebitQty, additionalDebitExtCost);
    INItemSiteSettings itemSiteSettings = INItemSiteSettings.PK.Find((PXGraph) this, detail.InventoryID, detail.SiteID);
    List<INPIEntry.CostLayerInfo> list = this.ReadCostLayers(detail).ToList<INPIEntry.CostLayerInfo>();
    list.Sort(this.GetCostLayerComparer(itemSiteSettings));
    List<INPIEntry.ProjectedTranRec> projectedTranRecList = new List<INPIEntry.ProjectedTranRec>();
    foreach (INPIDetail inpiDetail in (IEnumerable<INPIDetail>) creditLines.OrderBy<INPIDetail, int>((Func<INPIDetail, int>) (l => l.LineNbr.Value)))
    {
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = 0M;
      Decimal val2 = -inpiDetail.VarQty.Value;
      foreach (INPIEntry.CostLayerInfo costLayerInfo in list)
      {
        Decimal intersectionQty = this.GetIntersectionQty(costLayerInfo, inpiDetail);
        if (!(intersectionQty == 0M))
        {
          Decimal num4 = Math.Min(intersectionQty, val2);
          Decimal num5 = -num4;
          Decimal num6;
          Decimal num7;
          if (num4 == costLayerInfo.RestQty)
          {
            num7 = num6 = -costLayerInfo.RestCost;
          }
          else
          {
            num7 = -costLayerInfo.RestCost * num4 / costLayerInfo.RestQty;
            num6 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num7);
          }
          num1 += num4;
          num2 += -num7;
          val2 -= num4;
          costLayerInfo.RestQty -= num4;
          costLayerInfo.RestCost -= -num6;
          INPIEntry.ProjectedTranRec projectedTran = this.CreateProjectedTran(costLayerInfo, inpiDetail, itemSiteSettings);
          projectedTran.UnitCost = new Decimal?(PXDBPriceCostAttribute.Round(num7 / num5));
          projectedTran.VarQtyPortion = num5;
          projectedTran.VarCostPortion = num6;
          projectedTran.AcctID = costLayerInfo.CostLayer.AccountID;
          projectedTran.SubID = costLayerInfo.CostLayer.SubID;
          projectedTranRecList.Add(projectedTran);
          if (val2 == 0M)
            break;
        }
      }
      list = list.Where<INPIEntry.CostLayerInfo>((Func<INPIEntry.CostLayerInfo, bool>) (layer => layer.RestQty != 0M)).ToList<INPIEntry.CostLayerInfo>();
      if (val2 > 0M)
      {
        if (additionalDebitQty >= val2)
        {
          Decimal num8 = -val2;
          Decimal num9 = -(additionalUnitCost * val2);
          Decimal num10 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, num9);
          additionalDebitQty -= val2;
          num1 += val2;
          num2 += -num9;
          projectedTranRecList.Add(this.CreateProjectedTran(inpiDetail, true, new Decimal?(additionalUnitCost), new Decimal?(num8), new Decimal?(num10)));
        }
        else
        {
          if (adjustmentCreation)
          {
            if (INLocation.PK.Find((PXGraph) this, inpiDetail.LocationID).IsCosted.GetValueOrDefault())
              throw new PXException("Unable to create adjustment for line '{0}'. Insufficient Qty. On Hand for item '{1} {2}' in warehouse '{3} {4}'.", new object[5]
              {
                (object) inpiDetail.LineNbr,
                detailCache.GetValueExt<INPIDetail.inventoryID>((object) inpiDetail),
                detailCache.GetValueExt<INPIDetail.subItemID>((object) inpiDetail),
                detailCache.GetValueExt<INPIDetail.siteID>((object) inpiDetail),
                detailCache.GetValueExt<INPIDetail.locationID>((object) inpiDetail)
              });
            throw new PXException("Unable to create adjustment for line '{0}'. Insufficient Qty. On Hand for item '{1} {2}' in warehouse '{3}'.", new object[4]
            {
              (object) inpiDetail.LineNbr,
              detailCache.GetValueExt<INPIDetail.inventoryID>((object) inpiDetail),
              detailCache.GetValueExt<INPIDetail.subItemID>((object) inpiDetail),
              detailCache.GetValueExt<INPIDetail.siteID>((object) inpiDetail)
            });
          }
          if (num1 > 0M)
          {
            Decimal num11 = PXDBPriceCostAttribute.Round(num2 / num1);
            num3 += num11 * val2;
          }
        }
      }
      Decimal valueOrDefault1 = inpiDetail.ExtVarCost.GetValueOrDefault();
      detailCache.SetValueExt<INPIDetail.unitCost>((object) inpiDetail, (object) (num1 != 0M ? num2 / num1 : 0M));
      detailCache.SetValueExt<INPIDetail.extVarCost>((object) inpiDetail, (object) -(num2 + num3));
      GraphHelper.MarkUpdated(detailCache, (object) inpiDetail, true);
      Decimal valueOrDefault2 = inpiDetail.ExtVarCost.GetValueOrDefault();
      if (Math.Abs(valueOrDefault1 - valueOrDefault2) > 0.000001M)
        ++updatedLinesCount;
    }
    return (IEnumerable<INPIEntry.ProjectedTranRec>) projectedTranRecList;
  }

  protected virtual Decimal GetIntersectionQty(INPIEntry.CostLayerInfo costLayer, INPIDetail line)
  {
    return costLayer.RestQty;
  }

  protected virtual INPIEntry.ProjectedTranRec CreateProjectedTran(
    INPIEntry.CostLayerInfo costLayerInfo,
    INPIDetail line,
    INItemSiteSettings itemSiteSettings)
  {
    INPIEntry.ProjectedTranRec projectedTran = this.CreateProjectedTran(line, true);
    projectedTran.OrigRefNbr = itemSiteSettings.ValMethod == "F" ? costLayerInfo.CostLayer.ReceiptNbr : (string) null;
    return projectedTran;
  }

  private INPIEntry.ProjectedTranRec CreateProjectedTran(
    INPIDetail detail,
    bool adjNotReceipt,
    Decimal? tranUnitCost = null,
    Decimal? tranQty = null,
    Decimal? tranTotalCost = null,
    string receiptNbr = null,
    int? invAcctID = null,
    int? invSubID = null)
  {
    INPIEntry.ProjectedTranRec projectedTran = new INPIEntry.ProjectedTranRec();
    projectedTran.AdjNotReceipt = adjNotReceipt;
    projectedTran.OrigRefNbr = receiptNbr;
    projectedTran.LineNbr = detail.LineNbr.Value;
    projectedTran.ManualCost = detail.ManualCost;
    projectedTran.UnitCost = tranUnitCost ?? detail.UnitCost;
    Decimal? nullable1 = tranQty;
    Decimal? nullable2;
    Decimal valueOrDefault1;
    if (!nullable1.HasValue)
    {
      nullable2 = detail.VarQty;
      valueOrDefault1 = nullable2.GetValueOrDefault();
    }
    else
      valueOrDefault1 = nullable1.GetValueOrDefault();
    projectedTran.VarQtyPortion = valueOrDefault1;
    nullable1 = tranTotalCost;
    Decimal valueOrDefault2;
    if (!nullable1.HasValue)
    {
      nullable2 = detail.ExtVarCost;
      valueOrDefault2 = nullable2.GetValueOrDefault();
    }
    else
      valueOrDefault2 = nullable1.GetValueOrDefault();
    projectedTran.VarCostPortion = valueOrDefault2;
    projectedTran.AcctID = invAcctID;
    projectedTran.SubID = invSubID;
    projectedTran.InventoryID = detail.InventoryID;
    projectedTran.SubItemID = detail.SubItemID;
    projectedTran.LocationID = detail.LocationID;
    projectedTran.LotSerialNbr = detail.LotSerialNbr;
    projectedTran.ReasonCode = detail.ReasonCode;
    projectedTran.ExpireDate = detail.ExpireDate;
    return projectedTran;
  }

  protected virtual Decimal GetAdditionalUnitCost(
    INPIDetail detail,
    Decimal additionalDebitQty,
    Decimal additionalDebitExtCost)
  {
    Decimal additionalUnitCost = additionalDebitQty != 0M ? PXDBPriceCostAttribute.Round(additionalDebitExtCost / additionalDebitQty) : 0M;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, detail.InventoryID);
    if (!(inventoryItem.ValMethod == "T"))
      return additionalUnitCost;
    INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this, detail.InventoryID, detail.SiteID);
    INSite inSite = INSite.PK.Find((PXGraph) this, detail.SiteID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem.InventoryID, inSite.BaseCuryID);
    return !(((Decimal?) inItemSite?.StdCost).GetValueOrDefault() != 0M) ? (Decimal?) itemCurySettings?.StdCost ?? additionalUnitCost : inItemSite.StdCost.Value;
  }

  protected virtual List<INPIEntry.ProjectedTranRec> RecalcDemandCost(
    bool adjustmentCreation = false,
    bool forseDebitLinesRecalculation = false)
  {
    if ((INPIHeader) ((PXSelectBase) this.PIHeader).Cache.Current == null || !this.IsCostCalculationEnabled())
      return new List<INPIEntry.ProjectedTranRec>();
    List<INPIEntry.ProjectedTranRec> projectedTranRecList = new List<INPIEntry.ProjectedTranRec>();
    foreach (IGrouping<string, INPIDetail> relatedLines in GraphHelper.RowCast<INPIDetail>((IEnumerable) ((IEnumerable<PXResult<INPIDetail>>) ((PXSelectBase<INPIDetail>) this.PIDetailPure).Select(Array.Empty<object>())).AsEnumerable<PXResult<INPIDetail>>()).Where<INPIDetail>((Func<INPIDetail, bool>) (detail => this.AreKeysFieldsEntered(detail) && detail.Status == "E")).GroupBy<INPIDetail, string>(new Func<INPIDetail, string>(this.GetDetailsGroupingKey)))
      projectedTranRecList.AddRange(this.UpdateRelatedLinesCost(((PXSelectBase) this.PIDetail).Cache, (IEnumerable<INPIDetail>) relatedLines, adjustmentCreation, forseDebitLinesRecalculation));
    return projectedTranRecList;
  }

  protected virtual void RecalcTotals()
  {
    if (this.skipRecalcTotals)
      return;
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    foreach (PXResult<INPIDetail> pxResult in ((PXSelectBase<INPIDetail>) this.PIDetailPure).Select(Array.Empty<object>()))
    {
      INPIDetail inpiDetail = PXResult<INPIDetail>.op_Implicit(pxResult);
      if (inpiDetail != null && !(inpiDetail.Status == "S"))
      {
        Decimal num4 = num3;
        Decimal? nullable = inpiDetail.PhysicalQty;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        num3 = num4 + valueOrDefault1;
        Decimal num5 = num1;
        nullable = inpiDetail.VarQty;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num1 = num5 + valueOrDefault2;
        Decimal num6 = num2;
        nullable = inpiDetail.FinalExtVarCost;
        Decimal num7 = nullable ?? inpiDetail.ExtVarCost.GetValueOrDefault();
        num2 = num6 + num7;
      }
    }
    INPIHeader current = (INPIHeader) ((PXSelectBase) this.PIHeader).Cache.Current;
    if (current == null)
      return;
    current.TotalPhysicalQty = new Decimal?(num3);
    current.TotalVarQty = new Decimal?(num1);
    current.TotalVarCost = new Decimal?(num2);
    ((PXSelectBase<INPIHeader>) this.PIHeader).Update(current);
  }

  protected virtual bool AreTotalsAffected(INPIDetail oldDetail, INPIDetail newDetail)
  {
    if (oldDetail == null && newDetail == null)
      return false;
    return oldDetail == null || newDetail == null || !((PXSelectBase) this.PIDetail).Cache.ObjectsEqual<INPIDetail.physicalQty, INPIDetail.varQty, INPIDetail.finalExtVarCost, INPIDetail.extVarCost>((object) oldDetail, (object) newDetail);
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    this.skipRecalcTotals = true;
    this.DisableCostCalculation = true;
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
        {
          cache.SetValueExt<INBarCodeItem.lotSerialNbr>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.lotSerialNbr>(values, false));
          cache.SetValueExt<INBarCodeItem.expireDate>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.expireDate>(values, false));
        }
        cache.SetValueExt<INBarCodeItem.qty>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.physicalQty>(values, true));
        cache.SetValueExt<INBarCodeItem.autoAddLine>((object) inBarCodeItem, (object) false);
        cache.SetValueExt<INBarCodeItem.reasonCode>((object) inBarCodeItem, this.GetImportedValue<INPIDetail.reasonCode>(values, false));
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

  private object GetImportedValue<Field>(IDictionary values, bool isRequired) where Field : IBqlField
  {
    INPIDetail instance = (INPIDetail) ((PXSelectBase) this.PIDetail).Cache.CreateInstance();
    string displayName = PXUIFieldAttribute.GetDisplayName<Field>(((PXSelectBase) this.PIDetail).Cache);
    if (!values.Contains((object) typeof (Field).Name) & isRequired)
      throw new PXException("Incorrect head in the file. Column \"{0}\" is mandatory", new object[1]
      {
        (object) displayName
      });
    object obj = values[(object) typeof (Field).Name];
    ((PXSelectBase) this.PIDetail).Cache.RaiseFieldUpdating<Field>((object) instance, ref obj);
    return !isRequired || obj != null ? obj : throw new PXException("'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    });
  }

  public void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this.skipRecalcTotals = false;
    this.DisableCostCalculation = false;
    this.RecalcDemandCost();
    this.RecalcTotals();
    if (this.importHasError)
      throw new Exception("Import has some error. The list of incorrect records is recorded in the Trace.");
  }

  protected struct CostStatusSupplInfoRec
  {
    public Decimal ProjectedQty;
    public Decimal ProjectedCost;
  }

  public class CostLayerInfo
  {
    public INCostStatus CostLayer { get; private set; }

    public string CostLayerType { get; private set; }

    public Decimal RestQty { get; set; }

    public Decimal RestCost { get; set; }

    public CostLayerInfo(INCostStatus layer, string layerType)
    {
      this.CostLayer = layer;
      this.CostLayerType = layerType;
      this.RestQty = layer.QtyOnHand.Value;
      this.RestCost = layer.TotalCost.Value;
    }
  }

  public class ProjectedTranRec
  {
    public bool AdjNotReceipt;
    public bool? ManualCost;
    public int LineNbr;
    public string UOM;
    public Decimal? UnitCost;
    public Decimal VarQtyPortion;
    public Decimal VarCostPortion;
    public int? AcctID;
    public int? SubID;
    public int? ProjectID;
    public int? TaskID;
    public int? CostCenterID;
    public int? InventoryID;
    public int? SubItemID;
    public int? LocationID;
    public string LotSerialNbr;
    public DateTime? ExpireDate;
    public string OrigRefNbr;
    public string ReasonCode;
    public bool IsSpecialOrder;
    public string SOOrderType;
    public string SOOrderNbr;
    public int? SOOrderLineNbr;
  }
}
