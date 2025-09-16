// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorComponentIDServiceOrderAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorComponentIDServiceOrderAttribute : PXCustomSelectorAttribute
{
  private readonly Type CurrentTable;
  private readonly Type SourceTable;

  public FSSelectorComponentIDServiceOrderAttribute(Type currentTable, Type sourceTable)
    : base(typeof (FSModelTemplateComponent.componentID), new Type[3]
    {
      typeof (FSModelTemplateComponent.componentCD),
      typeof (FSModelComponent.optional),
      typeof (FSModelComponent.classID)
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
    fssoDet1 = (FSSODet) null;
    PXResultset<FSModelTemplateComponent> pxResultset = new PXResultset<FSModelTemplateComponent>();
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    foreach (object current in PXView.Currents)
    {
      if (current != null && current.GetType() == this.CurrentTable)
      {
        fssoDet1 = current as FSSODet;
        break;
      }
    }
    if (fssoDet1 == null && !(cach1.Current is FSSODet fssoDet1))
      return (IEnumerable) null;
    PXCache pxCache = cach1.Equals((object) cach2) ? cach1 : cach2;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(this._Graph, fssoDet1.InventoryID);
    if (fssoDet1.NewTargetEquipmentLineNbr != null)
    {
      foreach (object obj in pxCache.Cached)
      {
        FSSODet fssoDet2 = obj as FSSODet;
        if (fssoDet2.LineRef == fssoDet1.NewTargetEquipmentLineNbr)
        {
          nullable1 = fssoDet2.InventoryID;
          break;
        }
      }
    }
    else if (fssoDet1.SMEquipmentID.HasValue)
      nullable1 = SharedFunctions.GetEquipmentRow(this._Graph, fssoDet1.SMEquipmentID).InventoryID;
    if (!nullable1.HasValue)
      return (IEnumerable) null;
    int? itemClassId = (int?) inventoryItemRow?.ItemClassID;
    PXResultset<FSModelTemplateComponent> records;
    if (fssoDet1.EquipmentAction != "NO")
      records = PXSelectBase<FSModelTemplateComponent, PXSelectJoin<FSModelTemplateComponent, InnerJoin<FSModelComponent, On<FSModelTemplateComponent.componentID, Equal<FSModelComponent.componentID>>>, Where2<Where<FSModelComponent.active, Equal<True>, And<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>, And<Where<FSModelComponent.classID, Equal<Required<FSModelComponent.classID>>, Or<Required<FSModelComponent.classID>, IsNull>>>>>.Config>.Select(this._Graph, new object[3]
      {
        (object) nullable1,
        (object) itemClassId,
        (object) itemClassId
      });
    else
      records = PXSelectBase<FSModelTemplateComponent, PXSelectJoin<FSModelTemplateComponent, InnerJoin<FSModelComponent, On<FSModelTemplateComponent.componentID, Equal<FSModelComponent.componentID>>>, Where<FSModelComponent.active, Equal<True>, And<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>>.Config>.Select(this._Graph, new object[1]
      {
        (object) nullable1
      });
    return (IEnumerable) records;
  }
}
