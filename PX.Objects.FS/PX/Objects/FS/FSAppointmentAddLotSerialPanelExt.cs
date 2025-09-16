// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentAddLotSerialPanelExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.FS;

public class FSAppointmentAddLotSerialPanelExt : 
  AddLotSerialPanelExtBase<AppointmentEntry, FSAppointment, FSAppointmentDet, FSApptLineSplit>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  [PXMergeAttributes]
  [PXDefault(typeof (FSAppointment.branchID))]
  protected virtual void _(
    Events.CacheAttached<AddLotSerialHeader.branchID> e)
  {
  }

  protected override FSAppointmentDet InsertLine(
    FSAppointmentDet newLine,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    newLine.InventoryID = lotSerial.InventoryID;
    newLine.SiteID = lotSerial.SiteID;
    newLine.LocationID = lotSerial.LocationID;
    newLine.UOM = lotSerial.BaseUnit;
    newLine.TranDesc = lotSerial.Descr;
    newLine.EstimatedQty = lotSerial.QtySelected;
    return ((PXSelectBase<FSAppointmentDet>) this.Base.AppointmentDetails).Insert(newLine);
  }

  protected override FSApptLineSplit InsertSplit(
    FSApptLineSplit newSplit,
    FSAppointmentDet line,
    INItemLotSerialAttributesHeaderSelected lotSerial)
  {
    FSApptLineSplit fsApptLineSplit = ((PXSelectBase<FSApptLineSplit>) this.Base.Splits).Insert(newSplit);
    fsApptLineSplit.InventoryID = lotSerial.InventoryID;
    fsApptLineSplit.SiteID = lotSerial.SiteID;
    fsApptLineSplit.LocationID = lotSerial.LocationID;
    fsApptLineSplit.UOM = lotSerial.BaseUnit;
    fsApptLineSplit.LotSerialNbr = lotSerial.LotSerialNbr;
    fsApptLineSplit.Qty = lotSerial.QtySelected;
    return ((PXSelectBase<FSApptLineSplit>) this.Base.Splits).Update(fsApptLineSplit);
  }

  protected override bool IsDocumentReadonly(FSAppointment document)
  {
    return !((PXSelectBase) this.Base.AppointmentDetails).AllowInsert;
  }
}
