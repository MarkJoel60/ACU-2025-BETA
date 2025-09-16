// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderAddLotSerialPanelExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class FSServiceOrderAddLotSerialPanelExt : 
  AddLotSerialPanelExtBase<ServiceOrderEntry, FSServiceOrder, FSSODet, FSSODetSplit>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXMergeAttributes]
  [PXDefault(typeof (FSServiceOrder.branchID))]
  protected virtual void _(
    Events.CacheAttached<AddLotSerialHeader.branchID> e)
  {
  }

  protected override FSSODet InsertLine(
    FSSODet newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    newLine.InventoryID = lotSerial.InventoryID;
    newLine.SiteID = lotSerial.SiteID;
    newLine.LocationID = lotSerial.LocationID;
    newLine.UOM = lotSerial.BaseUnit;
    newLine.TranDesc = lotSerial.Descr;
    return ((PXSelectBase<FSSODet>) this.Base.ServiceOrderDetails).Insert(newLine);
  }

  protected override FSSODetSplit InsertSplit(
    FSSODetSplit newSplit,
    FSSODet line,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    FSSODetSplit fssoDetSplit = GraphHelper.RowCast<FSSODetSplit>(((PXSelectBase) this.Base.Splits).Cache.Inserted).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (s =>
    {
      int? lineNbr1 = s.LineNbr;
      int? lineNbr2 = line.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue && s.LotSerialNbr == null;
    })).FirstOrDefault<FSSODetSplit>() ?? ((PXSelectBase<FSSODetSplit>) this.Base.Splits).Insert(newSplit);
    fssoDetSplit.InventoryID = lotSerial.InventoryID;
    fssoDetSplit.SiteID = lotSerial.SiteID;
    fssoDetSplit.LocationID = lotSerial.LocationID;
    fssoDetSplit.UOM = lotSerial.BaseUnit;
    fssoDetSplit.LotSerialNbr = lotSerial.LotSerialNbr;
    fssoDetSplit.Qty = lotSerial.QtySelected;
    fssoDetSplit.IsAllocated = new bool?(true);
    return ((PXSelectBase<FSSODetSplit>) this.Base.Splits).Update(fssoDetSplit);
  }

  protected override bool IsDocumentReadonly(FSServiceOrder document)
  {
    return !((PXSelectBase) this.Base.ServiceOrderDetails).AllowInsert;
  }
}
