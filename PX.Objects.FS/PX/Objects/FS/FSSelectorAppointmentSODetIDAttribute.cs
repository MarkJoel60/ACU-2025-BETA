// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorAppointmentSODetIDAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorAppointmentSODetIDAttribute : PXSelectorAttribute
{
  public FSSelectorAppointmentSODetIDAttribute()
    : base(typeof (Search2<FSAppointmentDet.lineRef, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>>, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.lineRef, IsNotNull>>>>), new Type[5]
    {
      typeof (FSAppointmentDet.lineRef),
      typeof (FSAppointmentDet.lineType),
      typeof (FSAppointmentDet.status),
      typeof (FSAppointmentDet.inventoryID),
      typeof (FSAppointmentDet.tranDesc)
    })
  {
    this.SubstituteKey = typeof (FSAppointmentDet.lineRef);
    this.DescriptionField = typeof (PX.Objects.IN.InventoryItem.inventoryCD);
    this.DirtyRead = true;
  }

  public FSSelectorAppointmentSODetIDAttribute(Type whereType)
    : base(BqlCommand.Compose(new Type[7]
    {
      typeof (Search2<,,>),
      typeof (FSAppointmentDet.lineRef),
      typeof (LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>>),
      typeof (Where2<,>),
      typeof (Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentDet.lineRef, IsNotNull, And<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, Or<FSAppointmentDet.lineType, Equal<FSLineType.NonStockItem>>>>>>),
      typeof (And<>),
      whereType
    }), new Type[5]
    {
      typeof (FSAppointmentDet.lineRef),
      typeof (FSAppointmentDet.lineType),
      typeof (FSAppointmentDet.status),
      typeof (FSAppointmentDet.inventoryID),
      typeof (FSAppointmentDet.tranDesc)
    })
  {
    this.SubstituteKey = typeof (FSAppointmentDet.lineRef);
    this.DescriptionField = typeof (PX.Objects.IN.InventoryItem.inventoryCD);
    this.DirtyRead = true;
  }

  public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object returnValue = e.ReturnValue;
    e.ReturnValue = (object) null;
    this.FieldSelecting(sender, e);
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
    if (CurrentCacheExists(typeof (FSAppointment)))
      fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<Where<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
      {
        returnValue
      }));
    else if (CurrentCacheExists(typeof (FSAppointmentLog)))
      fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointmentLog.docType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointmentLog.docRefNbr>>, And<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
      {
        returnValue
      }));
    if (fsAppointmentDet != null)
    {
      e.ReturnValue = (object) fsAppointmentDet.LineRef;
    }
    else
    {
      if (e.Row == null)
        return;
      e.ReturnValue = (object) null;
    }

    bool CurrentCacheExists(Type cacheType) => sender.Graph.Caches[cacheType].Current != null;
  }
}
