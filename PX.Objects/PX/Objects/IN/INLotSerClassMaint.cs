// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

public class INLotSerClassMaint : PXGraph<INLotSerClassMaint, INLotSerClass>
{
  private const string lotSerNumValueFieldName = "LotSerNumVal";
  public PXSelect<INLotSerClass> lotserclass;
  public PXSelect<INLotSerSegment, Where<INLotSerSegment.lotSerClassID, Equal<Current<INLotSerClass.lotSerClassID>>>> lotsersegments;
  public PXSelect<INLotSerClassLotSerNumVal, Where<INLotSerClassLotSerNumVal.lotSerClassID, Equal<Current<INLotSerClass.lotSerClassID>>>> lotSerNumVal;

  public INLotSerClassMaint()
  {
    ((PXSelectBase) this.lotserclass).Cache.Fields.Add("LotSerNumVal");
    PXGraph.FieldSelectingEvents fieldSelecting = ((PXGraph) this).FieldSelecting;
    Type type1 = typeof (INLotSerClass);
    INLotSerClassMaint lotSerClassMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) lotSerClassMaint1, __vmethodptr(lotSerClassMaint1, LotSerNumValueFieldSelecting));
    fieldSelecting.AddHandler(type1, "LotSerNumVal", pxFieldSelecting);
    PXGraph.FieldUpdatingEvents fieldUpdating = ((PXGraph) this).FieldUpdating;
    Type type2 = typeof (INLotSerClass);
    INLotSerClassMaint lotSerClassMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) lotSerClassMaint2, __vmethodptr(lotSerClassMaint2, LotSerNumValueFieldUpdating));
    fieldUpdating.AddHandler(type2, "LotSerNumVal", pxFieldUpdating);
  }

  private void SetParentChanged(PXCache sender, object Row)
  {
    INLotSerClass inLotSerClass = PXParentAttribute.SelectParent<INLotSerClass>(sender, Row);
    if (inLotSerClass == null || ((PXSelectBase) this.lotserclass).Cache.GetStatus((object) inLotSerClass) != null)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.lotserclass).Cache, (object) inLotSerClass, true);
  }

  protected virtual void INLotSerClass_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    INLotSerClass newRow = (INLotSerClass) e.NewRow;
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row != null)
    {
      if (newRow.LotSerAssign != row.LotSerAssign)
      {
        try
        {
          this.HasInventoryItemsInUse(newRow);
        }
        catch (PXSetPropertyException ex)
        {
          sender.RaiseExceptionHandling<INLotSerClass.lotSerAssign>((object) newRow, (object) row.LotSerAssign, (Exception) ex);
          ((CancelEventArgs) e).Cancel = true;
        }
      }
    }
    if (newRow.LotSerTrackExpiration.GetValueOrDefault() || !(newRow.LotSerIssueMethod == "E") || !(newRow.LotSerTrack != "N") || !(newRow.LotSerAssign != "U"))
      return;
    sender.RaiseExceptionHandling<INLotSerClass.lotSerIssueMethod>((object) newRow, (object) row.LotSerIssueMethod, (Exception) new PXSetPropertyException("Only classes with enabled Track Expiration can use Expiration Issue Method."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void HasInventoryItemsInUse(INLotSerClass item)
  {
    if (PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelectReadonly<InventoryItem, Where<InventoryItem.lotSerClassID, Equal<Required<InventoryItem.lotSerClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) item.LotSerClassID
    })) != null)
      throw new PXSetPropertyException("The assignment method cannot be changed for the lot/serial class because it is assigned to at least one item on the Stock Items (IN202500) form.");
  }

  protected virtual void INLotSerClass_LotSerTrack_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row != null && row.LotSerTrack == "N")
    {
      row.RequiredForDropship = new bool?(false);
      foreach (PXResult<INLotSerSegment> pxResult in ((PXSelectBase<INLotSerSegment>) this.lotsersegments).Select(Array.Empty<object>()))
        ((PXSelectBase<INLotSerSegment>) this.lotsersegments).Delete(PXResult<INLotSerSegment>.op_Implicit(pxResult));
    }
    if (!(row.LotSerTrack != "S") && row.AutoNextNbr.GetValueOrDefault())
      return;
    row.AutoSerialMaxCount = new int?(0);
  }

  protected virtual void INLotSerClass_IsManualAssignRequired_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row == null)
      return;
    bool? manualAssignRequired = row.IsManualAssignRequired;
    if (!manualAssignRequired.HasValue || e.NewValue == null)
      return;
    manualAssignRequired = row.IsManualAssignRequired;
    bool? newValue = (bool?) e.NewValue;
    if (manualAssignRequired.GetValueOrDefault() == newValue.GetValueOrDefault() & manualAssignRequired.HasValue == newValue.HasValue)
      return;
    this.HasOpenDocuments(sender, row, row.LotSerIssueMethod);
  }

  protected virtual void HasOpenDocuments(PXCache sender, INLotSerClass row, string oldValue)
  {
    using (IEnumerator<PXResult<InventoryItem>> enumerator = PXSelectBase<InventoryItem, PXSelectJoin<InventoryItem, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.inventoryID, Equal<InventoryItem.inventoryID>, And<SOShipLineSplit.confirmed, Equal<False>>>>, Where<InventoryItem.lotSerClassID, Equal<Required<INLotSerClass.lotSerClassID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.LotSerClassID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<InventoryItem, SOShipLineSplit> current = (PXResult<InventoryItem, SOShipLineSplit>) enumerator.Current;
        InventoryItem inventoryItem = PXResult<InventoryItem, SOShipLineSplit>.op_Implicit(current);
        SOShipLineSplit soShipLineSplit = PXResult<InventoryItem, SOShipLineSplit>.op_Implicit(current);
        throw new PXException(row.LotSerAssign == "R" ? "The issue method cannot be changed because the {0} shipment contains the {1} item with the serial number of this class. Process the shipment with the current settings or delete the shipment, and then change the settings" : "The value of the 'Auto-Generate Next Number' checkbox cannot be changed because the {0} shipment contains the {1} item with the serial number of this class. Process the shipment with the current settings or delete the shipment, and then change the settings", new object[2]
        {
          (object) soShipLineSplit.ShipmentNbr,
          (object) inventoryItem.InventoryCD
        });
      }
    }
  }

  protected virtual void INLotSerClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row == null)
      return;
    INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelectReadonly<INItemClass, Where<INItemClass.lotSerClassID, Equal<Required<INItemClass.lotSerClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.LotSerClassID
    }));
    InventoryItem inventoryItem = PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelectReadonly<InventoryItem, Where<InventoryItem.lotSerClassID, Equal<Required<InventoryItem.lotSerClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.LotSerClassID
    }));
    PXUIFieldAttribute.SetEnabled<INLotSerClass.lotSerTrack>(sender, (object) row, inItemClass == null && inventoryItem == null);
    bool flag = row.LotSerTrack != "N";
    ((PXSelectBase) this.lotsersegments).Cache.AllowInsert = flag;
    ((PXSelectBase) this.lotsersegments).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.lotsersegments).Cache.AllowDelete = flag;
    PXUIFieldAttribute.SetEnabled<INLotSerClass.lotSerTrackExpiration>(sender, (object) row, ((inItemClass != null ? 0 : (inventoryItem == null ? 1 : 0)) & (flag ? 1 : 0)) != 0);
    PXCache cache = ((PXSelectBase) this.lotSerNumVal).Cache;
    INLotSerClassLotSerNumVal current = ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Current;
    bool? nullable;
    int num1;
    if (flag)
    {
      nullable = row.LotSerNumShared;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<INLotSerClassLotSerNumVal.lotSerNumVal>(cache, (object) current, num1 != 0);
    PXUIFieldAttribute.SetEnabled<INLotSerClass.autoNextNbr>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<INLotSerClass.lotSerNumShared>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<INLotSerClass.requiredForDropship>(sender, (object) row, row.LotSerTrack != "N");
    PXCache pxCache = sender;
    INLotSerClass inLotSerClass = row;
    int num2;
    if (flag)
    {
      nullable = row.AutoNextNbr;
      if (nullable.GetValueOrDefault())
      {
        num2 = row.LotSerTrack == "S" ? 1 : 0;
        goto label_8;
      }
    }
    num2 = 0;
label_8:
    PXUIFieldAttribute.SetEnabled<INLotSerClass.autoSerialMaxCount>(pxCache, (object) inLotSerClass, num2 != 0);
    PXUIFieldAttribute.SetVisible<INLotSerClass.lotSerIssueMethod>(sender, (object) row, row.LotSerAssign == "R");
  }

  protected virtual void INLotSerSegment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.SetParentChanged(sender, e.Row);
  }

  protected virtual void INLotSerSegment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.SetParentChanged(sender, e.Row);
  }

  protected virtual void INLotSerSegment_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this.SetParentChanged(sender, e.Row);
  }

  protected virtual void INLotSerSegment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is INLotSerSegment row))
      return;
    if (row.SegmentType == "N")
      row.SegmentValue = ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Current?.LotSerNumVal;
    PXUIFieldAttribute.SetEnabled<INLotSerSegment.segmentValue>(sender, (object) row, row.SegmentType == "C" || row.SegmentType == "D");
  }

  protected virtual void LotSerNumValueFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Current = (INLotSerClassLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    e.ReturnState = ((PXSelectBase) this.lotSerNumVal).Cache.GetStateExt<INLotSerClassLotSerNumVal.lotSerNumVal>((object) ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Current);
  }

  protected virtual void LotSerNumValueFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row == null)
      return;
    string newValue = (string) e.NewValue;
    INLotSerClassLotSerNumVal classLotSerNumVal = (INLotSerClassLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).View.SelectSingleBound(new object[1]
    {
      (object) row
    }, Array.Empty<object>());
    string lotSerNumVal = classLotSerNumVal?.LotSerNumVal;
    if (sender.ObjectsEqual((object) lotSerNumVal, (object) newValue) || !(row.LotSerTrack != "N"))
      return;
    if (classLotSerNumVal == null)
      ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Insert(new INLotSerClassLotSerNumVal()
      {
        LotSerNumVal = newValue
      });
    else if (string.IsNullOrWhiteSpace(newValue))
    {
      ((PXSelectBase<INLotSerClassLotSerNumVal>) this.lotSerNumVal).Delete(classLotSerNumVal);
    }
    else
    {
      INLotSerClassLotSerNumVal copy = (INLotSerClassLotSerNumVal) ((PXSelectBase) this.lotSerNumVal).Cache.CreateCopy((object) classLotSerNumVal);
      copy.LotSerNumVal = newValue;
      ((PXSelectBase) this.lotSerNumVal).Cache.Update((object) copy);
    }
  }

  protected virtual void INLotSerClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    INLotSerClass row = (INLotSerClass) e.Row;
    if (row == null || e.Operation != 2 && e.Operation != 1 || !(row.LotSerTrack != "N") || !row.LotSerNumShared.GetValueOrDefault())
      return;
    PXStringState valueExt = (PXStringState) sender.GetValueExt((object) row, "LotSerNumVal");
    if (valueExt != null && ((PXFieldState) valueExt).Value != null)
      return;
    PXUIFieldAttribute.SetError<INLotSerClassLotSerNumVal.lotSerNumVal>(((PXSelectBase) this.lotSerNumVal).Cache, (object) null, ((Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) ((PXFieldState) valueExt).DisplayName
    })).Message);
  }

  public virtual void Persist()
  {
    foreach (INLotSerClass lsclass in ((PXSelectBase) this.lotserclass).Cache.Inserted)
    {
      lsclass.LotSerFormatStr = INLotSerialNbrAttribute.MakeFormatStr(((PXSelectBase) this.lotserclass).Cache, lsclass);
      if (lsclass.LotSerTrack != "N" && lsclass.AutoNextNbr.GetValueOrDefault())
      {
        int num = 0;
        PXView view = ((PXSelectBase) this.lotsersegments).View;
        object[] objArray1 = new object[1]
        {
          (object) lsclass
        };
        object[] objArray2 = Array.Empty<object>();
        foreach (INLotSerSegment inLotSerSegment in view.SelectMultiBound(objArray1, objArray2))
        {
          if (inLotSerSegment.SegmentType == "N")
            ++num;
        }
        if (num == 0)
          throw new PXException("'{0}' segment must be defined for lot/serial class.", new object[1]
          {
            (object) "Auto-Incremental Value"
          });
        if (num > 1)
          throw new PXException("Multiple '{0}' segments defined for lot/serial class.", new object[1]
          {
            (object) "Auto-Incremental Value"
          });
      }
    }
    foreach (INLotSerClass lsclass in ((PXSelectBase) this.lotserclass).Cache.Updated)
    {
      lsclass.LotSerFormatStr = INLotSerialNbrAttribute.MakeFormatStr(((PXSelectBase) this.lotserclass).Cache, lsclass);
      if (lsclass.LotSerTrack != "N" && lsclass.AutoNextNbr.GetValueOrDefault())
      {
        int num = 0;
        PXView view = ((PXSelectBase) this.lotsersegments).View;
        object[] objArray3 = new object[1]
        {
          (object) lsclass
        };
        object[] objArray4 = Array.Empty<object>();
        foreach (INLotSerSegment inLotSerSegment in view.SelectMultiBound(objArray3, objArray4))
        {
          if (inLotSerSegment.SegmentType == "N")
            ++num;
        }
        if (num == 0)
          throw new PXException("'{0}' segment must be defined for lot/serial class.", new object[1]
          {
            (object) "Auto-Incremental Value"
          });
        if (num > 1)
          throw new PXException("Multiple '{0}' segments defined for lot/serial class.", new object[1]
          {
            (object) "Auto-Incremental Value"
          });
      }
    }
    ((PXGraph) this).Persist();
  }
}
