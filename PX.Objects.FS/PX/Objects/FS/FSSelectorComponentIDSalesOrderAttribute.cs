// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorComponentIDSalesOrderAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorComponentIDSalesOrderAttribute : PXCustomSelectorAttribute
{
  public FSSelectorComponentIDSalesOrderAttribute()
    : base(typeof (FSModelTemplateComponent.componentID), new Type[3]
    {
      typeof (FSModelTemplateComponent.componentCD),
      typeof (FSModelComponent.optional),
      typeof (FSModelComponent.classID)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (FSModelTemplateComponent.componentCD);
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCache cach = this._Graph.Caches[typeof (PX.Objects.SO.SOLine)];
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    object obj = (object) null;
    foreach (object current in PXView.Currents)
    {
      if (current != null && current.GetType() == typeof (PX.Objects.SO.SOLine))
      {
        obj = current;
        break;
      }
    }
    if (obj == null)
      obj = cach.Current;
    PX.Objects.SO.SOLine soLine1 = (PX.Objects.SO.SOLine) obj;
    FSxSOLine extension = PXCache<PX.Objects.SO.SOLine>.GetExtension<FSxSOLine>(soLine1);
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(this._Graph, soLine1.InventoryID);
    if (extension.NewEquipmentLineNbr.HasValue)
    {
      PX.Objects.SO.SOLine soLine2 = PX.Objects.SO.SOLine.PK.Find(this._Graph, soLine1.OrderType, soLine1.OrderNbr, extension.NewEquipmentLineNbr, (PKFindOptions) 1);
      if (soLine2 != null)
        nullable1 = soLine2.InventoryID;
    }
    else if (extension.SMEquipmentID.HasValue)
      nullable1 = SharedFunctions.GetEquipmentRow(this._Graph, extension.SMEquipmentID).InventoryID;
    PXResultset<FSModelTemplateComponent> records1 = new PXResultset<FSModelTemplateComponent>();
    if (!nullable1.HasValue)
      return (IEnumerable) records1;
    int? itemClassId = inventoryItemRow.ItemClassID;
    PXResultset<FSModelTemplateComponent> records2;
    if (extension.EquipmentAction != "NO")
      records2 = PXSelectBase<FSModelTemplateComponent, PXSelectJoin<FSModelTemplateComponent, InnerJoin<FSModelComponent, On<FSModelTemplateComponent.componentID, Equal<FSModelComponent.componentID>>>, Where<FSModelComponent.active, Equal<True>, And<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>, And<FSModelComponent.classID, Equal<Required<FSModelComponent.classID>>>>>>.Config>.Select(this._Graph, new object[2]
      {
        (object) nullable1,
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
