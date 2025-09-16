// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorComponentIDAppointmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorComponentIDAppointmentAttribute : PXCustomSelectorAttribute
{
  private readonly Type CurrentTable;
  private readonly Type SourceTable;

  public FSSelectorComponentIDAppointmentAttribute(Type currentTable, Type sourceTable)
    : base(typeof (FSModelTemplateComponent.componentID), new Type[4]
    {
      typeof (FSModelTemplateComponent.componentCD),
      typeof (FSModelTemplateComponent.descr),
      typeof (FSModelTemplateComponent.optional),
      typeof (FSModelTemplateComponent.classID)
    })
  {
    this.CurrentTable = currentTable;
    this.SourceTable = sourceTable;
    ((PXSelectorAttribute) this).SubstituteKey = typeof (FSModelTemplateComponent.componentCD);
    ((PXSelectorAttribute) this).CacheGlobal = false;
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCache cach1 = this._Graph.Caches[this.CurrentTable];
    PXCache cach2 = this._Graph.Caches[this.SourceTable];
    fsAppointmentDet1 = (FSAppointmentDet) null;
    PXResultset<FSModelTemplateComponent> pxResultset = new PXResultset<FSModelTemplateComponent>();
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    foreach (object current in PXView.Currents)
    {
      if (current != null && current.GetType() == this.CurrentTable)
      {
        fsAppointmentDet1 = current as FSAppointmentDet;
        break;
      }
    }
    if (fsAppointmentDet1 == null && !(cach1.Current is FSAppointmentDet fsAppointmentDet1))
      return (IEnumerable) null;
    PXCache pxCache = cach1.Equals((object) cach2) ? cach1 : cach2;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(this._Graph, fsAppointmentDet1.InventoryID);
    if (fsAppointmentDet1.NewTargetEquipmentLineNbr != null)
    {
      foreach (object obj in pxCache.Cached)
      {
        FSAppointmentDet fsAppointmentDet2 = obj as FSAppointmentDet;
        if (fsAppointmentDet2.LineRef == fsAppointmentDet1.NewTargetEquipmentLineNbr)
        {
          nullable1 = fsAppointmentDet2.InventoryID;
          break;
        }
      }
    }
    else if (fsAppointmentDet1.SMEquipmentID.HasValue)
    {
      FSEquipment equipmentRow = SharedFunctions.GetEquipmentRow(this._Graph, fsAppointmentDet1.SMEquipmentID);
      if (equipmentRow != null)
        nullable1 = equipmentRow.InventoryID;
    }
    PXResultset<FSModelTemplateComponent> records1 = new PXResultset<FSModelTemplateComponent>();
    if (!nullable1.HasValue)
      return (IEnumerable) records1;
    int? itemClassId = (int?) inventoryItemRow?.ItemClassID;
    PXResultset<FSModelTemplateComponent> records2;
    if (fsAppointmentDet1.EquipmentAction != "NO")
      records2 = PXSelectBase<FSModelTemplateComponent, PXSelectJoin<FSModelTemplateComponent, LeftJoin<FSModelComponent, On<FSModelTemplateComponent.componentID, Equal<FSModelComponent.componentID>>>, Where2<Where<FSModelComponent.active, Equal<True>, And<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>, And<Where<FSModelComponent.classID, Equal<Required<FSModelComponent.classID>>, Or<Required<FSModelComponent.classID>, IsNull>>>>>.Config>.Select(this._Graph, new object[3]
      {
        (object) nullable1,
        (object) itemClassId,
        (object) itemClassId
      });
    else
      records2 = PXSelectBase<FSModelTemplateComponent, PXSelectJoin<FSModelTemplateComponent, InnerJoin<FSModelComponent, On<FSModelTemplateComponent.componentID, Equal<FSModelComponent.componentID>>>, Where<FSModelComponent.active, Equal<True>, And<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>>.Config>.Select(this._Graph, new object[1]
      {
        (object) nullable1
      });
    return (IEnumerable) records2;
  }
}
