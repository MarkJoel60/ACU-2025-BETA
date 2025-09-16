// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXRestrictor(typeof (Where<PMUnion.isActive, Equal<True>>), "The {0} union local is inactive.", new Type[] {typeof (PMUnion.unionID)})]
[PXDBString(15, IsUnicode = true)]
[PXUIField(DisplayName = "Union Local", FieldClass = "Construction")]
public class PMUnionAttribute : PXEntityAttribute, IPXFieldDefaultingSubscriber
{
  protected Type projectField;
  protected Type employeeSearch;

  public PMUnionAttribute(Type project, Type employeeSearch)
  {
    this.projectField = project;
    this.employeeSearch = employeeSearch;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (Search<PMUnion.unionID>), new Type[1]
    {
      this.DescriptionField = typeof (PMUnion.description)
    }));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    EPEmployee epEmployee = (EPEmployee) null;
    if (this.employeeSearch != (Type) null)
    {
      BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
      {
        this.employeeSearch
      });
      epEmployee = new PXView(sender.Graph, false, instance).SelectSingle(Array.Empty<object>()) as EPEmployee;
    }
    if (epEmployee == null || string.IsNullOrEmpty(epEmployee.UnionID))
      return;
    HashSet<string> stringSet = new HashSet<string>();
    if (this.projectField != (Type) null)
    {
      int? projectID = (int?) sender.GetValue(e.Row, this.projectField.Name);
      if (ProjectDefaultAttribute.IsProject(sender.Graph, projectID))
      {
        foreach (PXResult<PMProjectUnion> pxResult in ((PXSelectBase<PMProjectUnion>) new PXSelect<PMProjectUnion, Where<PMProjectUnion.projectID, Equal<Required<PMProjectUnion.projectID>>>>(sender.Graph)).Select(new object[1]
        {
          (object) projectID
        }))
        {
          PMProjectUnion pmProjectUnion = PXResult<PMProjectUnion>.op_Implicit(pxResult);
          stringSet.Add(pmProjectUnion.UnionID);
        }
      }
    }
    if (stringSet.Count != 0 && !stringSet.Contains(epEmployee.UnionID))
      return;
    e.NewValue = (object) epEmployee.UnionID;
  }
}
