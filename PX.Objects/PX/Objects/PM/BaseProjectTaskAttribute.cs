// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BaseProjectTaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Task Selector that displays all Tasks for the given Project. Task Field is Disabled if a Non-Project is selected; otherwise mandatory.
/// Task Selector always work in pair with Project Selector. When the Project Selector displays a valid Project Task field becomes mandatory.
/// When This selector is used in pair with <see cref="!:ActiveProjectOrContractAttribute" /> and a Contract is selected Task is no longer mandatory.
/// If Completed Task is selected an error will be displayed - Completed Task cannot be used in DataEntry.
/// </summary>
[PXDBInt]
[PXInt]
[PXUIField]
public class BaseProjectTaskAttribute : 
  PXEntityAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  private readonly Type projectIDField;
  private readonly Type baseTypeField;
  private readonly Type nonProjectField;
  private readonly string module;
  protected int _ModuleRestrictorAttrIndex = -1;
  protected Type _visibleInModule;
  protected IBqlCreator checkMandatoryCondition;

  public Type NeedTaskValidationField { get; set; }

  public virtual Type CheckMandatoryCondition
  {
    get => this.checkMandatoryCondition?.GetType();
    set => this.checkMandatoryCondition = PXFormulaAttribute.InitFormula(value);
  }

  public bool AllowCompleted { get; set; }

  public bool AllowCanceled { get; set; }

  public bool AllowInactive { get; set; } = true;

  public bool SuppressVerify { get; set; }

  public bool AlwaysVisibleIfProjectType { get; set; }

  public BaseProjectTaskAttribute(Type projectID)
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

  public BaseProjectTaskAttribute(Type projectID, Type baseType, Type nonProject)
    : this(projectID)
  {
    if (baseType == (Type) null)
      throw new ArgumentNullException(nameof (baseType));
    if (nonProject == (Type) null)
      throw new ArgumentNullException(nameof (nonProject));
    this.baseTypeField = baseType;
    this.nonProjectField = nonProject;
  }

  public BaseProjectTaskAttribute(Type projectID, string Module)
    : this(projectID)
  {
    this.module = !string.IsNullOrEmpty(Module) ? Module : throw new ArgumentNullException("Source");
    if (Module != null && Module.Length == 2)
    {
      switch (Module[0])
      {
        case 'A':
          switch (Module)
          {
            case "AP":
              this._visibleInModule = typeof (PMTask.visibleInAP);
              break;
            case "AR":
              this._visibleInModule = typeof (PMTask.visibleInAR);
              break;
            default:
              goto label_18;
          }
          break;
        case 'C':
          switch (Module)
          {
            case "CA":
              this._visibleInModule = typeof (PMTask.visibleInCA);
              break;
            case "CR":
              this._visibleInModule = typeof (PMTask.visibleInCR);
              break;
            default:
              goto label_18;
          }
          break;
        case 'G':
          if (Module == "GL")
          {
            this._visibleInModule = typeof (PMTask.visibleInGL);
            break;
          }
          goto label_18;
        case 'I':
          if (Module == "IN")
          {
            this._visibleInModule = typeof (PMTask.visibleInIN);
            break;
          }
          goto label_18;
        case 'P':
          if (Module == "PO")
          {
            this._visibleInModule = typeof (PMTask.visibleInPO);
            break;
          }
          goto label_18;
        case 'S':
          if (Module == "SO")
          {
            this._visibleInModule = typeof (PMTask.visibleInSO);
            break;
          }
          goto label_18;
        default:
          goto label_18;
      }
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[3]
      {
        typeof (Where<,>),
        this._visibleInModule,
        typeof (Equal<True>)
      }), PXMessages.LocalizeFormatNoPrefixNLA("Project Task '{0}' is invisible in {1} module.", new object[2]
      {
        (object) "{0}",
        (object) PXMessages.LocalizeNoPrefix(this.module)
      }), new Type[1]{ typeof (PMTask.taskCD) }));
      this._ModuleRestrictorAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
      return;
    }
label_18:
    throw new ArgumentOutOfRangeException("Source", (object) Module, "ProjectTaskAttribute does not support the given module.");
  }

  public BaseProjectTaskAttribute(Type projectID, Type Module)
    : this(projectID)
  {
    Type type = !(Module == (Type) null) ? BqlCommand.Compose(new Type[45]
    {
      typeof (Where<,,>),
      typeof (PMTask.visibleInGL),
      typeof (Equal<>),
      typeof (True),
      typeof (And<,,>),
      typeof (Optional<>),
      Module,
      typeof (Equal<>),
      typeof (BatchModule.moduleGL),
      typeof (Or<,,>),
      typeof (PMTask.visibleInAR),
      typeof (Equal<>),
      typeof (True),
      typeof (And<,,>),
      typeof (Optional<>),
      Module,
      typeof (Equal<>),
      typeof (BatchModule.moduleAR),
      typeof (Or<,,>),
      typeof (PMTask.visibleInAP),
      typeof (Equal<>),
      typeof (True),
      typeof (And<,,>),
      typeof (Optional<>),
      Module,
      typeof (Equal<>),
      typeof (BatchModule.moduleAP),
      typeof (Or<,,>),
      typeof (PMTask.visibleInCA),
      typeof (Equal<>),
      typeof (True),
      typeof (And<,,>),
      typeof (Optional<>),
      Module,
      typeof (Equal<>),
      typeof (BatchModule.moduleCA),
      typeof (Or<,,>),
      typeof (PMTask.visibleInIN),
      typeof (Equal<>),
      typeof (True),
      typeof (And<,>),
      typeof (Optional<>),
      Module,
      typeof (Equal<>),
      typeof (BatchModule.moduleIN)
    }) : throw new ArgumentNullException("Source");
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PMTask.isCompleted, NotEqual<True>>), "Project Task '{0}' is completed.", new Type[1]
    {
      typeof (PMTask.taskCD)
    }));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(type, "Project Task '{0}' is invisible.", new Type[1]
    {
      typeof (PMTask.taskCD)
    }));
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation == 3)
      return;
    int? nullable1 = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal);
    int? nullable2 = (int?) sender.GetValue(e.Row, this.projectIDField.Name);
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, (object) nullable2, Array.Empty<object>()));
    if (pmProject == null)
      return;
    bool? nullable3 = pmProject.NonProject;
    if (nullable3.GetValueOrDefault() || nullable1.HasValue || !(pmProject.BaseType == "P"))
      return;
    bool flag = true;
    if (this.checkMandatoryCondition != null)
    {
      bool? nullable4 = new bool?();
      object obj = (object) null;
      BqlFormula.Verify(sender, e.Row, this.checkMandatoryCondition, ref nullable4, ref obj);
      nullable3 = obj as bool?;
      flag = nullable3.GetValueOrDefault();
    }
    else if (this.NeedTaskValidationField != (Type) null)
    {
      nullable3 = (bool?) sender.GetValue(e.Row, this.NeedTaskValidationField.Name);
      flag = nullable3.GetValueOrDefault();
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

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.SuppressVerify || e.NewValue == null || !(e.NewValue is int))
      return;
    object projectId = sender.GetValue(e.Row, this.projectIDField.Name);
    if (this.CanPostToInactiveTask())
      return;
    BaseProjectTaskAttribute.VerifyTaskState(sender, projectId, e.NewValue, this.AllowCompleted, this.AllowCanceled, this.AllowInactive);
  }

  public static void VerifyTaskIsActive(PXCache sender, object projectId, object taskId)
  {
    BaseProjectTaskAttribute.VerifyTaskState(sender, projectId, taskId);
  }

  public static void VerifyTaskState(
    PXCache sender,
    object projectId,
    object taskId,
    bool allowCompleted = false,
    bool allowCanceled = false,
    bool allowInactive = false)
  {
    if (projectId == null || taskId == null)
      return;
    PMTask pmTask = PMTask.PK.Find(sender.Graph, (int?) projectId, (int?) taskId);
    if (pmTask == null)
      return;
    bool? nullable;
    if (!allowCompleted)
    {
      nullable = pmTask.IsCompleted;
      if (nullable.GetValueOrDefault())
        goto label_11;
    }
    if (!allowCanceled)
    {
      nullable = pmTask.IsCancelled;
      if (nullable.GetValueOrDefault())
        goto label_11;
    }
    int num;
    if (!allowInactive)
    {
      nullable = pmTask.IsActive;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
      goto label_12;
    }
    num = 0;
    goto label_12;
label_11:
    num = 1;
label_12:
    if (num == 0)
      return;
    nullable = pmTask.IsCancelled;
    if (nullable.GetValueOrDefault())
    {
      PXTaskIsCanceledException canceledException = new PXTaskIsCanceledException(pmTask.ProjectID, pmTask.TaskID, "To be able to use project tasks with the Completed, Canceled, or In Planning status for data entry, the user must have the Project Accountant role assigned.");
      canceledException.ErrorValue = (object) pmTask.TaskCD;
      throw canceledException;
    }
    nullable = pmTask.IsCompleted;
    if (nullable.GetValueOrDefault())
    {
      PXTaskIsCompletedException completedException = new PXTaskIsCompletedException(pmTask.ProjectID, pmTask.TaskID, "To be able to use project tasks with the Completed, Canceled, or In Planning status for data entry, the user must have the Project Accountant role assigned.");
      completedException.ErrorValue = (object) pmTask.TaskCD;
      throw completedException;
    }
    PXTaskIsInactiveException inactiveException = new PXTaskIsInactiveException(pmTask.ProjectID, pmTask.TaskID, "To be able to use project tasks with the Completed, Canceled, or In Planning status for data entry, the user must have the Project Accountant role assigned.");
    inactiveException.ErrorValue = (object) pmTask.TaskCD;
    throw inactiveException;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXFieldState returnState = e.ReturnState as PXFieldState;
    bool? nullable = new bool?(true);
    if (returnState == null)
      return;
    if (!this.AlwaysVisibleIfProjectType)
      returnState.Visible = ProjectAttribute.IsPMVisible(this.module);
    if (e.Row == null)
      return;
    bool flag1 = ((int?) sender.GetValue(e.Row, this.projectIDField.Name)).HasValue;
    bool flag2;
    bool flag3;
    if (this.nonProjectField == (Type) null && this.baseTypeField == (Type) null)
    {
      PMProject pmProject = PMProject.PK.Find(sender.Graph, (int?) sender.GetValue(e.Row, this.projectIDField.Name));
      flag1 = pmProject != null;
      flag2 = pmProject == null || !pmProject.NonProject.GetValueOrDefault();
      flag3 = pmProject?.BaseType == "P";
      if (this.AlwaysVisibleIfProjectType && !returnState.Visible)
        returnState.Visible = flag3;
    }
    else
    {
      flag2 = !((bool?) sender.GetValue(e.Row, this.nonProjectField.Name)).GetValueOrDefault();
      flag3 = (string) sender.GetValue(e.Row, this.baseTypeField.Name) == "P";
    }
    if (this.NeedTaskValidationField != (Type) null)
      nullable = (bool?) sender.GetValue(e.Row, this.NeedTaskValidationField.Name);
    returnState.Enabled = nullable.GetValueOrDefault() & flag1 & flag2 & flag3;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType1 = sender.GetItemType();
    string name = this.projectIDField.Name;
    BaseProjectTaskAttribute projectTaskAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) projectTaskAttribute1, __vmethodptr(projectTaskAttribute1, OnProjectUpdated));
    fieldUpdated.AddHandler(itemType1, name, pxFieldUpdated);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType2 = sender.GetItemType();
    BaseProjectTaskAttribute projectTaskAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) projectTaskAttribute2, __vmethodptr(projectTaskAttribute2, RowPersisting));
    rowPersisting.AddHandler(itemType2, pxRowPersisting);
    if (this._ModuleRestrictorAttrIndex == -1 || string.IsNullOrEmpty(this.module))
      return;
    ((PXRestrictorAttribute) ((PXAggregateAttribute) this)._Attributes[this._ModuleRestrictorAttrIndex]).Message = PXMessages.LocalizeFormat("Project Task '{0}' is invisible in {1} module.", new object[2]
    {
      (object) "{0}",
      (object) PXUIFieldAttribute.GetDisplayName(sender.Graph.Caches[typeof (PMTask)], this._visibleInModule.Name)
    });
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
              else
                sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
            }
            else if (valuePending == PXCache.NotSetValue && sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null)
              sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
          }
          if (obj3 == null)
          {
            sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) (int?) this.GetDefaultTask(sender.Graph, (int?) projectID)?.TaskID);
            return;
          }
          if (ProjectDefaultAttribute.IsNonProject((int?) projectID) && !sender.Graph.IsImportFromExcel)
            return;
          sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj3);
          return;
        }
        catch (PXTaskSetPropertyException ex) when (
        {
          // ISSUE: unable to correctly present filter
          int num;
          switch (ex)
          {
            case PXTaskIsCompletedException _:
            case PXTaskIsCanceledException _:
              num = 1;
              break;
            default:
              num = ex is PXTaskIsInactiveException ? 1 : 0;
              break;
          }
          if ((uint) num > 0U)
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
          throw;
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

  protected virtual bool CanPostToInactiveTask()
  {
    return !string.IsNullOrEmpty(PredefinedRoles.ProjectAccountant) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.ProjectAccountant);
  }

  public static void CheckPermissionForInactiveTask(PMTask task)
  {
    if (task == null || (string.IsNullOrEmpty(PredefinedRoles.ProjectAccountant) ? 0 : (PXContext.PXIdentity.User.IsInRole(PredefinedRoles.ProjectAccountant) ? 1 : 0)) != 0)
      return;
    bool? isActive = task.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXException("To be able to use project tasks with the Completed, Canceled, or In Planning status for data entry, the user must have the Project Accountant role assigned.");
  }
}
