// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt.AddLotSerialPanelExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt;

public class AddLotSerialPanelExt : INRegisterAddLotSerialPanelExtBase<INAdjustmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected override INTran InsertLine(
    INTran newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    newLine.InventoryID = lotSerial.InventoryID;
    newLine.SiteID = lotSerial.SiteID;
    newLine.UOM = lotSerial.BaseUnit;
    newLine.LocationID = lotSerial.LocationID;
    newLine.TranDesc = lotSerial.Descr;
    newLine.LotSerialNbr = lotSerial.LotSerialNbr;
    newLine.Qty = lotSerial.QtySelected;
    return this.Base.INTranDataMember.Insert(newLine);
  }

  protected override INTranSplit InsertSplit(
    INTranSplit newSplit,
    INTran line,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    return this.Base.INTranSplitDataMember.Current;
  }
}
