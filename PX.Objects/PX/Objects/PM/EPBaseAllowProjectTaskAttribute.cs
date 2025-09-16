// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.EPBaseAllowProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class EPBaseAllowProjectTaskAttribute : PXEntityAttribute
{
  protected Type projectIDField;
  protected string module;

  public EPBaseAllowProjectTaskAttribute(Type projectID)
  {
    this.projectIDField = !(projectID == (Type) null) ? projectID : throw new ArgumentNullException(nameof (projectID));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROTASK", BqlCommand.Compose(new Type[7]
    {
      typeof (Search<,>),
      typeof (PMTask.taskID),
      typeof (Where<,>),
      typeof (PMTask.projectID),
      typeof (Equal<>),
      typeof (Optional<>),
      projectID
    }), typeof (PMTask.taskCD), new Type[3]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.description),
      typeof (PMTask.status)
    })
    {
      DescriptionField = typeof (PMTask.description),
      DescriptionDisplayName = "Project Task Description",
      ValidComboRequired = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    int? nullable = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal);
    int? projectID = (int?) sender.GetValue(e.Row, this.projectIDField.Name);
    PMProject pmProject = PMProject.PK.Find(sender.Graph, projectID);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || nullable.HasValue || !(pmProject.BaseType == "P"))
      return;
    if (sender.RaiseExceptionHandling(this.FieldName, e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{this.FieldName}]"
    })))
      throw new PXRowPersistingException(this.FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) this.FieldName
      });
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType1 = sender.GetItemType();
    string name = this.projectIDField.Name;
    EPBaseAllowProjectTaskAttribute projectTaskAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) projectTaskAttribute1, __vmethodptr(projectTaskAttribute1, OnProjectUpdated));
    fieldUpdated.AddHandler(itemType1, name, pxFieldUpdated);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType2 = sender.GetItemType();
    EPBaseAllowProjectTaskAttribute projectTaskAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) projectTaskAttribute2, __vmethodptr(projectTaskAttribute2, RowPersisting));
    rowPersisting.AddHandler(itemType2, pxRowPersisting);
  }

  protected PMTask GetDefaultTask(PXGraph graph, int? projectID)
  {
    if (!projectID.HasValue)
      return (PMTask) null;
    return PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.isDefault, Equal<True>>>>.Config>.Select(graph, new object[1]
    {
      (object) projectID
    }));
  }

  protected virtual void OnProjectUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj = (object) (sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as string) ?? ((PXFieldState) (sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as PXSegmentedState)).Value;
    object projectID = sender.GetValue(e.Row, this.projectIDField.Name);
    if (obj != null)
    {
      if (obj != PXCache.NotSetValue)
      {
        try
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select(sender.Graph, new object[2]
          {
            projectID,
            obj
          }));
          if (pmTask != null && !ProjectDefaultAttribute.IsNonProject((int?) projectID))
          {
            sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) pmTask.TaskID);
            return;
          }
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) this.GetDefaultTask(sender.Graph, (int?) projectID)?.TaskCD);
          return;
        }
        catch (PXException ex)
        {
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          return;
        }
      }
    }
    sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) (int?) this.GetDefaultTask(sender.Graph, (int?) projectID)?.TaskID);
  }
}
