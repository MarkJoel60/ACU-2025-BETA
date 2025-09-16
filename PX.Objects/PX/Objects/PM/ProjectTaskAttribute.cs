// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Displays all Tasks for the given Project. Task is mandatory if Project is set.
/// </summary>
[PXDBInt]
[PXUIField]
public class ProjectTaskAttribute : 
  PXEntityAttribute,
  IPXRowPersistingSubscriber,
  IPXFieldSelectingSubscriber
{
  public const string DisplayNameText = "Project Task";
  public const string DimensionName = "PROTASK";
  private Type projectIDField;
  private string module;
  protected IBqlCreator checkMandatoryCondition;

  /// <summary>
  /// If True allows TaskID to be null if ProjectID is a Contract.
  /// </summary>
  public bool AllowNullIfContract { get; set; }

  public bool AllowNull { get; set; }

  /// <summary>
  /// Field is always enebled even if project is null or invalid.
  /// </summary>
  public bool AlwaysEnabled { get; set; }

  [Obsolete("AC-298206")]
  public bool DefaultActiveTask { get; set; }

  public bool DefaultNotClosedTask { get; set; }

  public bool SkipDefaultTask { get; set; }

  public virtual Type CheckMandatoryCondition
  {
    get => this.checkMandatoryCondition?.GetType();
    set => this.checkMandatoryCondition = PXFormulaAttribute.InitFormula(value);
  }

  public ProjectTaskAttribute(Type projectID)
    : this(projectID, (Type) null)
  {
    this.Filterable = true;
  }

  public ProjectTaskAttribute(Type projectID, Type Where)
  {
    this.projectIDField = !(projectID == (Type) null) ? projectID : throw new ArgumentNullException(nameof (projectID));
    Type type = BqlCommand.Compose(new Type[7]
    {
      typeof (Search<,>),
      typeof (PMTask.taskID),
      typeof (Where<,>),
      typeof (PMTask.projectID),
      typeof (Equal<>),
      typeof (Current<>),
      projectID
    });
    if (Where != (Type) null)
      type = BqlCommand.Compose(new Type[9]
      {
        typeof (Search<,>),
        typeof (PMTask.taskID),
        typeof (Where<,,>),
        typeof (PMTask.projectID),
        typeof (Equal<>),
        typeof (Current<>),
        projectID,
        typeof (And<>),
        Where
      });
    PXDimensionSelectorAttribute selectorAttribute = (PXDimensionSelectorAttribute) new PXDimensionSelector.WithCachingByCompositeKeyAttribute("PROTASK", type, typeof (Field<>.IsRelatedTo<>).MakeGenericType(projectID, typeof (PMTask.projectID)), typeof (PMTask.taskCD), new Type[3]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.description),
      typeof (PMTask.status)
    });
    selectorAttribute.DescriptionField = typeof (PMTask.description);
    selectorAttribute.DescriptionDisplayName = "Project Task Description";
    selectorAttribute.ValidComboRequired = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  public ProjectTaskAttribute(Type projectID, string Module)
  {
    if (projectID == (Type) null)
      throw new ArgumentNullException(nameof (projectID));
    if (string.IsNullOrEmpty(Module))
      throw new ArgumentNullException("Source");
    this.projectIDField = projectID;
    this.module = Module;
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
              goto label_22;
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
              goto label_22;
          }
          break;
        case 'G':
          if (Module == "GL")
          {
            type = typeof (PMTask.visibleInGL);
            break;
          }
          goto label_22;
        case 'I':
          if (Module == "IN")
          {
            type = typeof (PMTask.visibleInIN);
            break;
          }
          goto label_22;
        case 'P':
          if (Module == "PO")
          {
            type = typeof (PMTask.visibleInPO);
            break;
          }
          goto label_22;
        case 'S':
          if (Module == "SO")
          {
            type = typeof (PMTask.visibleInSO);
            break;
          }
          goto label_22;
        case 'T':
          if (Module == "TA")
          {
            type = typeof (PMTask.visibleInTA);
            break;
          }
          goto label_22;
        default:
          goto label_22;
      }
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROTASK", BqlCommand.Compose(new Type[10]
      {
        typeof (Search<,>),
        typeof (PMTask.taskID),
        typeof (Where<,,>),
        type,
        typeof (Equal<True>),
        typeof (And<,>),
        typeof (PMTask.projectID),
        typeof (Equal<>),
        typeof (Current<>),
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
      return;
    }
label_22:
    throw new ArgumentOutOfRangeException("Source", (object) Module, "ProjectTaskAttribute does not support the given module.");
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    int? projectID = (int?) sender.GetValue(e.Row, this.projectIDField.Name);
    int? nullable1 = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal);
    if (!projectID.HasValue || ProjectDefaultAttribute.IsNonProject(projectID) || nullable1.HasValue || e.Operation == 3 || this.AllowNull || this.AllowNullIfContract && PMProject.PK.Find(sender.Graph, projectID).BaseType == "C")
      return;
    bool flag = true;
    if (this.checkMandatoryCondition != null)
    {
      bool? nullable2 = new bool?();
      object obj = (object) null;
      BqlFormula.Verify(sender, e.Row, this.checkMandatoryCondition, ref nullable2, ref obj);
      flag = (obj as bool?).GetValueOrDefault();
    }
    if (!flag)
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
    if (e.ReturnState is PXFieldState returnState && e.Row != null && !this.AlwaysEnabled)
    {
      pmProject = (PMProject) null;
      if (e.Row != null)
      {
        object obj = e.Row;
        PXCache pxCache = sender;
        Type itemType = BqlCommand.GetItemType(this.projectIDField);
        if (sender.GetItemType() != itemType)
        {
          pxCache = sender.Graph.Caches[itemType];
          obj = pxCache.Current;
        }
        if (!(PXSelectorAttribute.Select(pxCache, obj, this.projectIDField.Name) is PMProject pmProject))
        {
          int? nullable = (int?) pxCache.GetValue(obj, this.projectIDField.Name);
          pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, (object) nullable, Array.Empty<object>()));
        }
      }
      returnState.Enabled = pmProject != null && (pmProject.BaseType == "P" || pmProject.BaseType == "R") && !pmProject.NonProject.GetValueOrDefault() || sender.Graph.IsImport && !sender.Graph.IsMobile;
    }
    returnState.Visible = ProjectAttribute.IsPMVisible(this.module);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string name = this.projectIDField.Name;
    ProjectTaskAttribute projectTaskAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) projectTaskAttribute, __vmethodptr(projectTaskAttribute, OnProjectUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  protected PMTask GetDefaultTask(PXGraph graph, int? projectID)
  {
    if (!projectID.HasValue || ProjectDefaultAttribute.IsNonProject(projectID) || this.SkipDefaultTask)
      return (PMTask) null;
    PMTask defaultTask;
    if (this.DefaultNotClosedTask)
      defaultTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PMTask.isDefault, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PMTask.isCancelled, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<PMTask.isCompleted, IBqlBool>.IsNotEqual<True>>>>.Config>.Select(graph, new object[1]
      {
        (object) projectID
      }));
    else
      defaultTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMTask.isDefault, IBqlBool>.IsEqual<True>>>>.Config>.Select(graph, new object[1]
      {
        (object) projectID
      }));
    return defaultTask;
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
          PMTask pmTask1 = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select(sender.Graph, new object[2]
          {
            projectID,
            obj1
          }));
          if (pmTask1 != null && !ProjectDefaultAttribute.IsNonProject((int?) projectID))
          {
            sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) pmTask1.TaskID);
            object obj2 = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
            int? taskId = pmTask1.TaskID;
            int? nullable = (int?) obj2;
            if (taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue)
              return;
            PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null);
            sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
            return;
          }
          object obj3 = (object) null;
          if (ProjectDefaultAttribute.IsNonProject((int?) projectID))
          {
            object valuePending = sender.GetValuePending(e.Row, this.projectIDField.Name);
            if (valuePending != null && valuePending != PXCache.NotSetValue)
            {
              PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>>>.Config>.Select(sender.Graph, new object[1]
              {
                valuePending
              }));
              if (pmProject != null && !pmProject.NonProject.GetValueOrDefault())
              {
                PMTask pmTask2 = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select(sender.Graph, new object[2]
                {
                  (object) pmProject.ContractID,
                  obj1
                }));
                if (pmTask2 != null)
                  obj3 = (object) pmTask2.TaskCD;
              }
            }
            else if (valuePending == PXCache.NotSetValue && sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null)
              sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          }
          if (obj3 == null)
          {
            if (sender.Graph.IsImportFromExcel)
              return;
            sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) this.GetDefaultTask(sender.Graph, (int?) projectID)?.TaskCD);
            return;
          }
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj3);
          return;
        }
        catch (PXException ex)
        {
          if (sender.Graph.UnattendedMode)
            throw ex;
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          return;
        }
      }
    }
    sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) (int?) this.GetDefaultTask(sender.Graph, (int?) projectID)?.TaskID);
  }
}
