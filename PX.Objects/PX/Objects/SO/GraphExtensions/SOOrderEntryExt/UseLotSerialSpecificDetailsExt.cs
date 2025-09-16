// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.UseLotSerialSpecificDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.DAC;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(typeof (SOOrderLineSplittingExtension))]
public abstract class UseLotSerialSpecificDetailsExt : 
  PXGraphExtension<SOOrderLineSplittingExtension, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row.Hold.GetValueOrDefault() || e.Row.Cancelled.GetValueOrDefault() || EnumerableExtensions.IsIn<string>(e.Row.Behavior, "QT", "BL"))
      return;
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOLine line = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
      if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.GetStatus((object) line) == null)
        this.ValidateLineLotSerials(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, line);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if ((e.Operation & 3) == 3)
      return;
    this.ValidateLineLotSerials(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache, e.Row);
  }

  protected virtual void SOLine_LotSerialNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.SO.SOLine row = (PX.Objects.SO.SOLine) e.Row;
    if (row == null || row.LotSerialNbr == null)
      return;
    INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(row.InventoryID));
    if ((inLotSerClass != null ? (inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.SetDefaultExt<PX.Objects.SO.SOLine.curyUnitPrice>((object) row);
    INItemLotSerialAttributesHeader attributesHeader = INItemLotSerialAttributesHeader.PK.Find(sender.Graph, row.InventoryID, row.LotSerialNbr);
    if (attributesHeader == null)
      return;
    sender.SetValueExt<PX.Objects.SO.SOLine.tranDesc>((object) row, (object) attributesHeader.Descr);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.ForceLineSingleLotSerialPopulation(System.Nullable{System.Int32})" />.
  /// Returns true if the item class for the inventory item has checked Specify Lot/Serial Price and Description checkbox.
  /// </summary>
  [PXOverride]
  public bool ForceLineSingleLotSerialPopulation(int? inventoryID)
  {
    return ((bool?) PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(inventoryID))?.UseLotSerSpecificDetails).GetValueOrDefault();
  }

  /// <summary>
  /// Checks validity of lot/serial numbers specified in the line and its splits for items with enabled Specify Lot/Serial Price and Description in lot/serial class. Raises exception handling if necessary.
  /// </summary>
  private void ValidateLineLotSerials(PXCache cache, PX.Objects.SO.SOLine line)
  {
    if (EnumerableExtensions.IsIn<string>(line.Behavior, "QT", "BL") || !string.IsNullOrEmpty(line.LotSerialNbr))
      return;
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass1;
    this.ReadInventoryItem(line.InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass1);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    INLotSerClass inLotSerClass2 = inLotSerClass1;
    if ((inLotSerClass2 != null ? (!inLotSerClass2.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.Hold;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 == 0)
    {
      PX.Objects.SO.SOOrder current2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.Cancelled;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 1;
    bool flag = num2 != 0;
    string str = (string) null;
    PXView view = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View;
    object[] objArray1 = new object[1]{ (object) line };
    object[] objArray2 = Array.Empty<object>();
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in view.SelectMultiBound(objArray1, objArray2))
    {
      if (string.IsNullOrWhiteSpace(soLineSplit.LotSerialNbr))
      {
        if (!flag)
          UseLotSerialSpecificDetailsExt.RaiseLineLotSerialException(cache, line, "The sales order cannot be saved because a lot or serial number is not specified for the {0} item. Specify the number, or clear the Specify Lot/Serial Price and Description check box for the lot or serial class of the item on the Lot/Serial Classes (IN207000) form.", inventoryItem2.InventoryCD);
        else
          continue;
      }
      if (str == null)
        str = soLineSplit.LotSerialNbr;
      if (str != soLineSplit.LotSerialNbr)
        UseLotSerialSpecificDetailsExt.RaiseLineLotSerialException(cache, line, "Multiple lot or serial numbers cannot be specified for a single line with the {0} item because the Specify Lot/Serial Price and Description check box is selected for the lot or serial class of this item on the Lot/Serial Classes (IN207000) form. Add a separate line for each lot or serial number.", inventoryItem2.InventoryCD);
    }
    if (string.IsNullOrWhiteSpace(str) && !flag)
      UseLotSerialSpecificDetailsExt.RaiseLineLotSerialException(cache, line, "The sales order cannot be saved because a lot or serial number is not specified for the {0} item. Specify the number, or clear the Specify Lot/Serial Price and Description check box for the lot or serial class of the item on the Lot/Serial Classes (IN207000) form.", inventoryItem2.InventoryCD);
    if (!line.AutoCreateIssueLine.GetValueOrDefault() || flag)
      return;
    UseLotSerialSpecificDetailsExt.RaiseLineLotSerialException(cache, line, "The order cannot be saved because entering a lot or serial number is required for a line with the {0} item. To save the order, clear the Auto Create Issue check box in the line.", inventoryItem2.InventoryCD);
  }

  /// <summary>
  /// Gets <see cref="T:PX.Objects.IN.InventoryItem" /> combined with <see cref="T:PX.Objects.IN.INLotSerClass" /> for specified inventoryID.
  /// </summary>
  [PXProtectedAccess(null)]
  protected abstract PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID);

  private static void RaiseLineLotSerialException(
    PXCache cache,
    PX.Objects.SO.SOLine line,
    string message,
    string inventoryCD)
  {
    inventoryCD = inventoryCD.TrimEnd();
    if (cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.lotSerialNbr>((object) line, (object) null, (Exception) new PXSetPropertyException<PX.Objects.SO.SOLine.lotSerialNbr>(message, new object[1]
    {
      (object) inventoryCD
    })))
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLine.lotSerialNbr).Name, (object) null, message, new object[1]
      {
        (object) inventoryCD
      });
  }
}
