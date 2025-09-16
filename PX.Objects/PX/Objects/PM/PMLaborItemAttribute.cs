// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMLaborItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField(DisplayName = "Labor Item")]
public class PMLaborItemAttribute : PXEntityAttribute, IPXFieldDefaultingSubscriber
{
  protected Type projectField;
  protected Type earningTypeField;
  protected Type employeeSearch;

  public PMLaborItemAttribute(Type project, Type earningType, Type employeeSearch)
  {
    this.projectField = project;
    this.earningTypeField = earningType;
    this.employeeSearch = employeeSearch;
    PXDimensionSelectorAttribute selectorAttribute = new PXDimensionSelectorAttribute("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).IndexOf((PXEventSubscriberAttribute) selectorAttribute);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    EPEmployee employee = (EPEmployee) null;
    if (this.employeeSearch != (Type) null)
    {
      BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
      {
        this.employeeSearch
      });
      employee = new PXView(sender.Graph, false, instance).SelectSingle(Array.Empty<object>()) as EPEmployee;
    }
    if (employee == null || !(this.projectField != (Type) null) || !(this.earningTypeField != (Type) null))
      return;
    int? projectID = (int?) sender.GetValue(e.Row, this.projectField.Name);
    string earningType = (string) sender.GetValue(e.Row, this.earningTypeField.Name);
    int? nullable = (int?) sender.GetValue(e.Row, this.FieldName);
    if (sender.Graph.IsImportFromExcel && nullable.HasValue)
      e.NewValue = (object) nullable;
    else
      e.NewValue = (object) this.GetDefaultLaborItem(sender.Graph, employee, earningType, projectID);
  }

  public virtual int? GetDefaultLaborItem(
    PXGraph graph,
    EPEmployee employee,
    string earningType,
    int? projectID)
  {
    if (employee == null)
      return new int?();
    int? defaultLaborItem = new int?();
    if (ProjectDefaultAttribute.IsProject(graph, projectID))
      defaultLaborItem = EPContractRate.GetProjectLaborClassID(graph, projectID.Value, employee.BAccountID.Value, earningType);
    if (!defaultLaborItem.HasValue)
      defaultLaborItem = EPEmployeeClassLaborMatrix.GetLaborClassID(graph, employee.BAccountID, earningType);
    if (!defaultLaborItem.HasValue)
      defaultLaborItem = employee.LabourItemID;
    return defaultLaborItem;
  }
}
