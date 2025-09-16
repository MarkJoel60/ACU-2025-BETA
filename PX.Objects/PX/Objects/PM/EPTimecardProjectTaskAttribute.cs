// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.EPTimecardProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXUIField]
public class EPTimecardProjectTaskAttribute : PXEntityAttribute, IPXFieldSelectingSubscriber
{
  private readonly Type projectIDField;
  private readonly string module;

  public bool AllowNull { get; set; }

  public EPTimecardProjectTaskAttribute(Type projectID)
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
    this.Filterable = true;
  }

  public EPTimecardProjectTaskAttribute(Type projectID, string Module)
    : this(projectID)
  {
    this.module = !string.IsNullOrEmpty(Module) ? Module : throw new ArgumentNullException("Source");
    if (Module != null && Module.Length == 2)
    {
      Type type;
      switch (Module[0])
      {
        case 'A':
          switch (Module)
          {
            case "AP":
              type = typeof (PMTask.visibleInAP);
              break;
            case "AR":
              type = typeof (PMTask.visibleInAR);
              break;
            default:
              goto label_20;
          }
          break;
        case 'C':
          switch (Module)
          {
            case "CA":
              type = typeof (PMTask.visibleInCA);
              break;
            case "CR":
              type = typeof (PMTask.visibleInCR);
              break;
            default:
              goto label_20;
          }
          break;
        case 'G':
          if (Module == "GL")
          {
            type = typeof (PMTask.visibleInGL);
            break;
          }
          goto label_20;
        case 'I':
          if (Module == "IN")
          {
            type = typeof (PMTask.visibleInIN);
            break;
          }
          goto label_20;
        case 'P':
          if (Module == "PO")
          {
            type = typeof (PMTask.visibleInPO);
            break;
          }
          goto label_20;
        case 'S':
          if (Module == "SO")
          {
            type = typeof (PMTask.visibleInSO);
            break;
          }
          goto label_20;
        case 'T':
          if (Module == "TA")
          {
            type = typeof (PMTask.visibleInTA);
            break;
          }
          goto label_20;
        default:
          goto label_20;
      }
      this.Filterable = true;
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[3]
      {
        typeof (Where<,>),
        type,
        typeof (Equal<True>)
      }), PXMessages.LocalizeFormatNoPrefixNLA("Project Task '{0}' is invisible in {1} module.", new object[2]
      {
        (object) "{0}",
        (object) PXMessages.LocalizeNoPrefix(this.module)
      }), new Type[1]{ typeof (PMTask.taskCD) }));
      return;
    }
label_20:
    throw new ArgumentOutOfRangeException("Source", (object) Module, "ProjectTaskAttribute does not support the given module.");
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation == 3 || this.AllowNull)
      return;
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

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.Visible = ProjectAttribute.IsPMVisible(this.module);
    if (e.Row == null)
      return;
    if (!(PXSelectorAttribute.Select(sender, e.Row, this.projectIDField.Name) is PMProject pmProject))
    {
      int? nullable = (int?) sender.GetValue(e.Row, this.projectIDField.Name);
      pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, (object) nullable, Array.Empty<object>()));
    }
    returnState.Enabled = pmProject != null && !pmProject.NonProject.GetValueOrDefault() && pmProject.BaseType == "P";
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType1 = sender.GetItemType();
    string name = this.projectIDField.Name;
    EPTimecardProjectTaskAttribute projectTaskAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) projectTaskAttribute1, __vmethodptr(projectTaskAttribute1, OnProjectUpdated));
    fieldUpdated.AddHandler(itemType1, name, pxFieldUpdated);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType2 = sender.GetItemType();
    EPTimecardProjectTaskAttribute projectTaskAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) projectTaskAttribute2, __vmethodptr(projectTaskAttribute2, RowPersisting));
    rowPersisting.AddHandler(itemType2, pxRowPersisting);
  }

  protected PMTask GetDefaultTask(PXCache cache, string fieldName, object row, int? projectID)
  {
    if (!projectID.HasValue)
      return (PMTask) null;
    object taskID;
    cache.RaiseFieldDefaulting(fieldName, row, ref taskID);
    return taskID == null ? (PMTask) null : PMTask.PK.Find(cache.Graph, projectID, taskID as int?);
  }

  protected virtual void OnProjectUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj1 = (object) (sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as string) ?? ((PXFieldState) (sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as PXSegmentedState)).Value;
    object projectID = sender.GetValue(e.Row, this.projectIDField.Name);
    if (obj1 != null)
    {
      if (obj1 != PXCache.NotSetValue)
      {
        try
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select(sender.Graph, new object[2]
          {
            projectID,
            obj1
          }));
          if (pmTask != null && !ProjectDefaultAttribute.IsNonProject((int?) projectID))
          {
            sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) pmTask.TaskID);
            object obj2 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
            int? taskId = pmTask.TaskID;
            int? nullable = (int?) obj2;
            if (taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue)
              return;
            sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
            sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
            return;
          }
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) this.GetDefaultTask(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row, (int?) projectID)?.TaskCD);
          return;
        }
        catch (PXException ex)
        {
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          return;
        }
      }
    }
    sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) (int?) this.GetDefaultTask(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row, (int?) projectID)?.TaskID);
  }
}
