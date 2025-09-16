// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterAddLotSerialPanelExtBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.DAC.Projections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class INRegisterAddLotSerialPanelExtBase<TGraph> : 
  AddLotSerialPanelExtBase<TGraph, INRegister, INTran, INTranSplit>
  where TGraph : INRegisterEntryBase
{
  protected override bool IsDocumentReadonly(INRegister document)
  {
    return !((PXSelectBase) this.Base.INTranDataMember).AllowInsert;
  }

  protected override INTran InsertLine(
    INTran newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    newLine.InventoryID = lotSerial.InventoryID;
    newLine.SiteID = lotSerial.SiteID;
    newLine.UOM = lotSerial.BaseUnit;
    newLine.LocationID = lotSerial.LocationID;
    newLine.TranDesc = lotSerial.Descr;
    return this.Base.INTranDataMember.Insert(newLine);
  }

  protected override INTranSplit InsertSplit(
    INTranSplit newSplit,
    INTran line,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    INTranSplit inTranSplit1 = this.Base.INTranSplitDataMember.Insert(newSplit);
    inTranSplit1.LotSerialNbr = lotSerial.LotSerialNbr;
    INTranSplit inTranSplit2 = this.Base.INTranSplitDataMember.Update(inTranSplit1);
    inTranSplit2.Qty = lotSerial.QtySelected;
    return this.Base.INTranSplitDataMember.Update(inTranSplit2);
  }
}
