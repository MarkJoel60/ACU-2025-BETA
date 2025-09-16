// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitSpecMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class INKitSpecMaint : PXGraph<
#nullable disable
INKitSpecMaint, INKitSpecHdr>
{
  public PXSelect<INKitSpecHdr> Hdr;
  public PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>>>, OrderBy<Asc<INKitSpecStkDet.kitInventoryID, Asc<INKitSpecStkDet.revisionID, Asc<INKitSpecStkDet.lineNbr>>>>> StockDet;
  public PXSelect<INKitSpecNonStkDet, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>>>, OrderBy<Asc<INKitSpecNonStkDet.kitInventoryID, Asc<INKitSpecNonStkDet.revisionID, Asc<INKitSpecNonStkDet.lineNbr>>>>> NonStockDet;

  public INKitSpecMaint()
  {
    ((PXGraph) this).OnBeforePersist += new Action<PXGraph>(this.OnBeforeGraphPersist);
  }

  private void OnBeforeGraphPersist(PXGraph obj)
  {
    if (!this.IsNonStockKitSpecificationChanged())
      return;
    this.ThrowIfNonstockKitIsInUse(((PXSelectBase<INKitSpecHdr>) this.Hdr).Current);
  }

  protected virtual void _(Events.RowDeleting<INKitSpecHdr> e)
  {
    this.ThrowIfNonstockKitIsInUse(e.Row);
  }

  protected virtual bool IsNonStockKitSpecificationChanged()
  {
    if (((PXSelectBase<INKitSpecHdr>) this.Hdr).Current == null || ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current.IsStock.GetValueOrDefault() || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Hdr).Cache.GetStatus((object) ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current), (PXEntryStatus) 2, (PXEntryStatus) 4))
      return false;
    if (!((PXSelectBase) this.Hdr).Cache.ObjectsEqual<INKitSpecHdr.isActive, INKitSpecHdr.allowCompAddition>((object) ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current, (object) (INKitSpecHdr) ((PXSelectBase) this.Hdr).Cache.GetOriginal((object) ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current)))
      return true;
    PXCache<INKitSpecStkDet> stkDetailsCache = GraphHelper.Caches<INKitSpecStkDet>((PXGraph) this);
    PXCache<INKitSpecNonStkDet> nonstkDetailsCache = GraphHelper.Caches<INKitSpecNonStkDet>((PXGraph) this);
    int num1 = NonGenericIEnumerableExtensions.Any_(((PXCache) stkDetailsCache).Inserted) ? 1 : (NonGenericIEnumerableExtensions.Any_(((PXCache) nonstkDetailsCache).Inserted) ? 1 : 0);
    bool flag = NonGenericIEnumerableExtensions.Any_(((PXCache) stkDetailsCache).Deleted) || NonGenericIEnumerableExtensions.Any_(((PXCache) nonstkDetailsCache).Deleted);
    Lazy<bool> lazy1 = new Lazy<bool>((Func<bool>) (() => stkDetailsCache.Rows.Updated.Any<INKitSpecStkDet>((Func<INKitSpecStkDet, bool>) (detail => !((PXCache) stkDetailsCache).ObjectsEqual<INKitSpecStkDet.compInventoryID, INKitSpecStkDet.allowSubstitution, INKitSpecStkDet.uOM, INKitSpecStkDet.dfltCompQty, INKitSpecStkDet.allowQtyVariation, INKitSpecStkDet.minCompQty, INKitSpecStkDet.maxCompQty>((object) detail, (object) stkDetailsCache.GetOriginal(detail))))));
    Lazy<bool> lazy2 = new Lazy<bool>((Func<bool>) (() => nonstkDetailsCache.Rows.Updated.Any<INKitSpecNonStkDet>((Func<INKitSpecNonStkDet, bool>) (detail => !((PXCache) nonstkDetailsCache).ObjectsEqual<INKitSpecNonStkDet.compInventoryID, INKitSpecNonStkDet.uOM, INKitSpecNonStkDet.dfltCompQty, INKitSpecNonStkDet.allowQtyVariation, INKitSpecNonStkDet.minCompQty, INKitSpecNonStkDet.maxCompQty>((object) detail, (object) nonstkDetailsCache.GetOriginal(detail))))));
    int num2 = flag ? 1 : 0;
    return (num1 | num2) != 0 || lazy1.Value || lazy2.Value;
  }

  protected virtual void ThrowIfNonstockKitIsInUse(INKitSpecHdr header)
  {
    INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) header.KitInventoryID
    }));
    PXResultset<INItemPlan> pxResultset = PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) header.KitInventoryID
    });
    INItemPlan inItemPlan2 = inItemPlan1 ?? PXResultset<INItemPlan>.op_Implicit(pxResultset);
    if (inItemPlan2 != null)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, header.KitInventoryID);
      EntityHelper entityHelper = new EntityHelper((PXGraph) this);
      throw new PXException("The specification cannot be edited or deleted because the {0} non-stock kit is used in the following document: {1} {2}. To correct the specification, process the document to completion or delete it.", new object[3]
      {
        (object) inventoryItem.InventoryCD.TrimEnd(),
        (object) entityHelper.GetFriendlyEntityName(inItemPlan2.RefNoteID),
        (object) entityHelper.GetEntityRowID(inItemPlan2.RefNoteID, ", ")
      });
    }
  }

  protected virtual void INKitSpecHdr_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is INKitSpecHdr row))
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.KitInventoryID);
    if (inventoryItem == null)
      return;
    PXUIFieldAttribute.SetEnabled<INKitSpecHdr.kitSubItemID>(sender, (object) row, inventoryItem.StkItem.GetValueOrDefault());
  }

  protected virtual void INKitSpecHdr_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row is INKitSpecHdr row && e.Operation == 2 && !InventoryItem.PK.Find((PXGraph) this, row.KitInventoryID).StkItem.GetValueOrDefault() && PXResultset<INKitSpecHdr>.op_Implicit(PXSelectBase<INKitSpecHdr, PXSelectReadonly<INKitSpecHdr, Where<INKitSpecHdr.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null && sender.RaiseExceptionHandling<INKitSpecHdr.revisionID>(e.Row, (object) row.RevisionID, (Exception) new PXSetPropertyException("Non-Stock kit can contain only one revision.")))
      throw new PXRowPersistingException(typeof (INKitSpecHdr.revisionID).Name, (object) null, "Non-Stock kit can contain only one revision.");
  }

  protected virtual void INKitSpecStkDet_CompInventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is INKitSpecStkDet row))
      return;
    if (((PXSelectBase<INKitSpecStkDet>) new PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>, And<INKitSpecStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>, And<INKitSpecStkDet.compSubItemID, Equal<Required<INKitSpecStkDet.compSubItemID>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      e.NewValue,
      (object) row.CompSubItemID
    }).Count > 0)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
      this.RaiseOnKitNotUniqueException(e, new PXSetPropertyException("Component Item must be unique for the given Kit accross Component ID and Subitem combinations.")
      {
        ErrorValue = (object) inventoryItem?.InventoryCD
      });
    }
    InventoryItem inventoryItem1 = InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current.KitInventoryID);
    if (inventoryItem1 == null)
      return;
    INLotSerClass inLotSerClass1 = INLotSerClass.PK.Find((PXGraph) this, inventoryItem1.LotSerClassID) ?? new INLotSerClass();
    InventoryItem inventoryItem2 = InventoryItem.PK.Find((PXGraph) this, (int?) e.NewValue);
    if (inventoryItem2 == null)
      return;
    INLotSerClass inLotSerClass2 = INLotSerClass.PK.Find((PXGraph) this, inventoryItem2.LotSerClassID) ?? new INLotSerClass();
    if (inventoryItem1.StkItem.GetValueOrDefault() && inLotSerClass1.LotSerTrack != "S" && inLotSerClass2.LotSerTrack == "S")
      this.RaiseSNComponentInSNKitException(e, new PXSetPropertyException("Serial-numbered components are allowed only in serial-numbered kits")
      {
        ErrorValue = (object) inventoryItem2.InventoryCD
      });
    bool? nullable = inventoryItem1.StkItem;
    if (!nullable.GetValueOrDefault())
    {
      nullable = inLotSerClass2.IsManualAssignRequired;
      if (nullable.GetValueOrDefault())
        this.RaiseUnassignedComponentInKitException(e, new PXSetPropertyException("Components with the 'When-Used' assignment method and 'User-Enterable' issue method are not allowed in non-stock kits")
        {
          ErrorValue = (object) inventoryItem2.InventoryCD
        });
    }
    nullable = inventoryItem1.StkItem;
    if (nullable.GetValueOrDefault())
      return;
    nullable = inventoryItem1.NonStockShip;
    if (!nullable.GetValueOrDefault())
    {
      PXSetPropertyException<INKitSpecStkDet.compInventoryID> propertyException = new PXSetPropertyException<INKitSpecStkDet.compInventoryID>("The item cannot be added to the kit specification because it can contain only non-stock items that do not require shipping.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) inventoryItem2.InventoryCD;
      throw propertyException;
    }
  }

  protected virtual void INKitSpecStkDet_UOM_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is INKitSpecStkDet row))
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, inventoryItem?.LotSerClassID);
    if (inLotSerClass == null || !(inLotSerClass.LotSerTrack == "S") || string.Equals(inventoryItem.BaseUnit, (string) e.NewValue, StringComparison.InvariantCultureIgnoreCase))
      return;
    this.RaiseSerialTrackedComponentIsNotInBaseUnitException(e, new PXSetPropertyException("You can add serial tracked components with only a base UOM ('{0}') to the kit specification.", new object[1]
    {
      (object) inventoryItem.BaseUnit
    })
    {
      ErrorValue = e.NewValue
    });
  }

  /// <summary>
  ///  Raised during the INKitSpecStkDet_CompInventoryID verification if component Item is not unique for the given Kit accross Component ID and Subitem combinations.
  /// </summary>
  public virtual void RaiseOnKitNotUniqueException(
    PXFieldVerifyingEventArgs e,
    PXSetPropertyException ex)
  {
    throw ex;
  }

  /// <summary>
  ///  Raised during the INKitSpecStkDet_CompInventoryID verification if component is not SerialNumbered and Kit is SerialNumbered.
  /// </summary>
  public virtual void RaiseSNComponentInSNKitException(
    PXFieldVerifyingEventArgs e,
    PXSetPropertyException ex)
  {
    throw ex;
  }

  /// <summary>
  /// Raised during the INKitSpecStkDet_CompInventoryID verification if component's LotSerialClass.IsUnassigned property is True
  /// </summary>
  public virtual void RaiseUnassignedComponentInKitException(
    PXFieldVerifyingEventArgs e,
    PXSetPropertyException ex)
  {
    throw ex;
  }

  /// <summary>
  /// Raised during the INKitSpecStkDet_UOM_FieldVerifying verification if component is serial numbered and the UOM used is not the Base Unit.
  /// </summary>
  public virtual void RaiseSerialTrackedComponentIsNotInBaseUnitException(
    PXFieldVerifyingEventArgs e,
    PXSetPropertyException ex)
  {
    throw ex;
  }

  protected virtual void INKitSpecStkDet_CompSubItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is INKitSpecStkDet row))
      return;
    if (((PXSelectBase<INKitSpecStkDet>) new PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>, And<INKitSpecStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>, And<INKitSpecStkDet.compSubItemID, Equal<Required<INKitSpecStkDet.compSubItemID>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) row.CompInventoryID,
      e.NewValue
    }).Count > 0)
    {
      INSubItem inSubItem = INSubItem.PK.Find((PXGraph) this, (int?) e.NewValue);
      throw new PXSetPropertyException("Component Item must be unique for the given Kit accross Component ID and Subitem combinations.")
      {
        ErrorValue = (object) inSubItem?.SubItemCD
      };
    }
  }

  protected virtual void INKitSpecStkDet_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (e.NewRow is INKitSpecStkDet newRow && newRow.AllowQtyVariation.GetValueOrDefault())
    {
      Decimal? nullable1 = newRow.MinCompQty;
      Decimal? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = newRow.DfltCompQty;
        nullable2 = newRow.MinCompQty;
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          goto label_5;
      }
      nullable2 = newRow.MaxCompQty;
      if (!nullable2.HasValue)
        return;
      nullable2 = newRow.DfltCompQty;
      nullable1 = newRow.MaxCompQty;
      if (!(nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
        return;
label_5:
      throw new PXSetPropertyException("Component Qty should be between Min. and Max. Qty.");
    }
  }

  protected virtual void INKitSpecStkDet_CompInventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is INKitSpecStkDet row))
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
    if (inventoryItem == null)
      return;
    row.UOM = inventoryItem.BaseUnit;
  }

  protected virtual void INKitSpecStkDet_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is INKitSpecStkDet row) || (e.Operation & 3) == 3)
      return;
    if (((PXSelectBase<INKitSpecStkDet>) new PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>, And<INKitSpecStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>, And<INKitSpecStkDet.compSubItemID, Equal<Required<INKitSpecStkDet.compSubItemID>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) row.CompInventoryID,
      (object) row.CompSubItemID
    }).Count <= 1)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
    if (sender.RaiseExceptionHandling<INKitSpecStkDet.compInventoryID>(e.Row, (object) inventoryItem.InventoryCD, (Exception) new PXException("Component Item must be unique for the given Kit accross Component ID and Subitem combinations.")))
      throw new PXRowPersistingException(typeof (INKitSpecStkDet.compInventoryID).Name, (object) inventoryItem.InventoryCD, "Component Item must be unique for the given Kit accross Component ID and Subitem combinations.");
  }

  protected virtual void INKitSpecNonStkDet_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is INKitSpecNonStkDet newRow))
      return;
    if (newRow.AllowQtyVariation.GetValueOrDefault())
    {
      Decimal? nullable1 = newRow.MinCompQty;
      Decimal? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = newRow.DfltCompQty;
        nullable2 = newRow.MinCompQty;
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          goto label_6;
      }
      nullable2 = newRow.MaxCompQty;
      if (nullable2.HasValue)
      {
        nullable2 = newRow.DfltCompQty;
        nullable1 = newRow.MaxCompQty;
        if (!(nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
          goto label_7;
      }
      else
        goto label_7;
label_6:
      throw new PXSetPropertyException(typeof (INKitSpecNonStkDet.dfltCompQty).Name, new object[2]
      {
        null,
        (object) "Component Qty should be between Min. and Max. Qty."
      });
    }
label_7:
    int? kitInventoryId = newRow.KitInventoryID;
    int? compInventoryId = newRow.CompInventoryID;
    if (kitInventoryId.GetValueOrDefault() == compInventoryId.GetValueOrDefault() & kitInventoryId.HasValue == compInventoryId.HasValue)
      throw new PXSetPropertyException(typeof (INKitSpecNonStkDet.compInventoryID).Name, new object[2]
      {
        null,
        (object) "Kit May Not Include Itself As Component Part"
      });
  }

  protected virtual void INKitSpecNonStkDet_CompInventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is INKitSpecNonStkDet row))
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
    if (inventoryItem == null)
      return;
    row.UOM = inventoryItem.BaseUnit;
  }

  protected virtual void INKitSpecNonStkDet_CompInventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is INKitSpecNonStkDet))
      return;
    InventoryItem inventoryItem1 = InventoryItem.PK.Find((PXGraph) this, (int?) e.NewValue);
    if (((PXSelectBase<INKitSpecNonStkDet>) new PXSelect<INKitSpecNonStkDet, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>, And<INKitSpecNonStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>>>>>((PXGraph) this)).Select(new object[1]
    {
      e.NewValue
    }).Count > 0)
      throw new PXSetPropertyException("Component Item must be unique for the given Kit.")
      {
        ErrorValue = (object) inventoryItem1?.InventoryCD
      };
    if (inventoryItem1 == null)
      return;
    InventoryItem inventoryItem2 = InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INKitSpecHdr>) this.Hdr).Current.KitInventoryID);
    bool? nullable = inventoryItem2.StkItem;
    if (nullable.GetValueOrDefault())
      return;
    nullable = inventoryItem2.NonStockShip;
    if (nullable.GetValueOrDefault())
      return;
    nullable = inventoryItem1.NonStockShip;
    if (nullable.GetValueOrDefault())
    {
      PXSetPropertyException<INKitSpecNonStkDet.compInventoryID> propertyException = new PXSetPropertyException<INKitSpecNonStkDet.compInventoryID>("The item cannot be added to the kit specification because it can contain only non-stock items that do not require shipping.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) inventoryItem1.InventoryCD;
      throw propertyException;
    }
  }

  protected virtual void INKitSpecNonStkDet_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is INKitSpecNonStkDet row) || (e.Operation & 3) == 3)
      return;
    if (((PXSelectBase<INKitSpecNonStkDet>) new PXSelect<INKitSpecNonStkDet, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<INKitSpecNonStkDet.revisionID, Equal<Current<INKitSpecHdr.revisionID>>, And<INKitSpecNonStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) row.CompInventoryID
    }).Count <= 1)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, row.CompInventoryID);
    if (sender.RaiseExceptionHandling<INKitSpecNonStkDet.compInventoryID>(e.Row, (object) inventoryItem.InventoryCD, (Exception) new PXException("Component Item must be unique for the given Kit.")))
      throw new PXRowPersistingException(typeof (INKitSpecNonStkDet.compInventoryID).Name, (object) inventoryItem.InventoryCD, "Component Item must be unique for the given Kit.");
  }

  [NonStockItem(DisplayName = "Component ID")]
  [PXDefault]
  [PXRestrictor(typeof (Where<InventoryItem.inventoryID, NotEqual<Current<INKitSpecHdr.kitInventoryID>>>), "Non-stock kit can't using as its own component", new Type[] {})]
  [PXRestrictor(typeof (Where<InventoryItem.kitItem, Equal<boolFalse>>), "It is not allowed to add non-stock kits as components to a stock kit or to a  non-stock kit.", new Type[] {})]
  protected virtual void INKitSpecNonStkDet_CompInventoryID_CacheAttached(PXCache sender)
  {
  }
}
