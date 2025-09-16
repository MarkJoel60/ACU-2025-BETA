// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.Readonly
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.ProjectDefinition.Attributes;
using PX.Data.ProjectDefinition.Workflow;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.WorkflowAPI;

public static class Readonly
{
  public const string NextState = "@N";
  public const string ParentNextState = "@P";
  public const string SequenceStateType = "S";
  public const string OnSequenceLeavingTriggerName = "@OnSequenceLeaving";

  public class ActionCategory
  {
    public string CategoryName { get; set; }

    public string DisplayName { get; set; }

    public Placement Placement { get; set; }

    public string After { get; set; }

    internal static Readonly.ActionCategory From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ActionCategory category)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.ActionCategory()
      {
        CategoryName = category.CategoryName,
        DisplayName = category.DisplayName ?? category.CategoryName,
        Placement = category.Placement,
        After = category.After
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ActionCategory To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.ActionCategory()
      {
        CategoryName = this.CategoryName,
        DisplayName = this.DisplayName,
        Placement = this.Placement,
        After = this.After
      };
    }

    public void CopyTo(AUWorkflowCategory row)
    {
      row.CategoryName = this.CategoryName;
      row.DisplayName = this.DisplayName;
      row.Placement = new byte?((byte) this.Placement);
      row.After = this.After;
      row.IsSystem = new bool?(true);
    }
  }

  public class ActionDefinition
  {
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DataMember { get; set; }

    public Readonly.Condition DisableCondition { get; set; }

    public Readonly.Condition HideCondition { get; set; }

    public string After { get; set; }

    public Placement PlacementInCategory { get; set; }

    public string AfterInMenu { get; set; }

    public FolderType? ParentFolder { get; set; }

    public bool? DisablePersist { get; set; }

    public bool CreateNewAction { get; set; }

    public bool BatchMode { get; set; }

    public PXCacheRights? MapEnableRights { get; set; }

    public PXCacheRights? MapViewRights { get; set; }

    public string Form { get; private set; }

    public string MassProcessingScreen { get; private set; }

    public Readonly.NavigationDefinition Navigation { get; private set; }

    public string Category { get; set; }

    public bool? ExposeToMobile { get; private set; }

    public IReadOnlyCollection<Readonly.Assignment> FieldAssignments { get; private set; }

    public IReadOnlyCollection<Readonly.Assignment> ParameterAssignments { get; private set; }

    public bool? DisplayOnMainToolbar { get; private set; }

    public bool? IsLockedOnToolbar { get; private set; }

    public bool? IgnoresArchiveDisabling { get; private set; }

    internal static Readonly.ActionDefinition From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ActionDefinition action)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.ActionDefinition()
      {
        Name = action.Name,
        DisplayName = action.DisplayName,
        HideCondition = action.HideCondition?.AsReadonly(),
        DisableCondition = action.DisableCondition?.AsReadonly(),
        ParentFolder = action.ParentFolder,
        CreateNewAction = action.CreateNewAction,
        DisablePersist = action.DisablePersist,
        DataMember = action.GetDataType().FullName,
        After = action.After,
        PlacementInCategory = action.PlacementInCategory,
        AfterInMenu = action.AfterInMenu,
        Form = action.Form,
        MassProcessingScreen = action.MassProcessingScreen,
        BatchMode = action.BatchMode,
        MapEnableRights = action.MapEnableRights,
        MapViewRights = action.MapViewRights,
        Category = action.Category,
        ExposeToMobile = action.ExposeToMobile,
        Navigation = action.Navigation?.AsReadonly(),
        FieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) action.FieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>(),
        ParameterAssignments = (IReadOnlyCollection<Readonly.Assignment>) action.ParameterAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>(),
        DisplayOnMainToolbar = action.DisplayOnMainToolbar,
        IsLockedOnToolbar = action.IsLockedOnToolbar,
        IgnoresArchiveDisabling = action.IgnoresArchiveDisabling
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ActionDefinition To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.ActionDefinition actionDefinition = new BoundedTo<TGraph, TPrimary>.ActionDefinition();
      actionDefinition.Name = this.Name;
      actionDefinition.DisplayName = this.DisplayName;
      actionDefinition.HideCondition = this.HideCondition?.To<TGraph, TPrimary>();
      actionDefinition.DisableCondition = this.DisableCondition?.To<TGraph, TPrimary>();
      actionDefinition.ParentFolder = this.ParentFolder;
      actionDefinition.CreateNewAction = this.CreateNewAction;
      actionDefinition.DisablePersist = this.DisablePersist;
      actionDefinition.After = this.After;
      actionDefinition.PlacementInCategory = this.PlacementInCategory;
      actionDefinition.AfterInMenu = this.AfterInMenu;
      actionDefinition.Form = this.Form;
      actionDefinition.MassProcessingScreen = this.MassProcessingScreen;
      actionDefinition.BatchMode = this.BatchMode;
      actionDefinition.MapEnableRights = this.MapEnableRights;
      actionDefinition.MapViewRights = this.MapViewRights;
      actionDefinition.Category = this.Category;
      actionDefinition.ExposeToMobile = this.ExposeToMobile;
      actionDefinition.Navigation = this.Navigation?.To<TGraph, TPrimary>();
      actionDefinition.FieldAssignments = this.FieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      actionDefinition.ParameterAssignments = this.ParameterAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      actionDefinition.DisplayOnMainToolbar = this.DisplayOnMainToolbar;
      actionDefinition.IsLockedOnToolbar = this.IsLockedOnToolbar;
      actionDefinition.IgnoresArchiveDisabling = this.IgnoresArchiveDisabling;
      return actionDefinition;
    }

    public void CopyTo(AUScreenActionBaseState row)
    {
      PXSpecialButtonType? nullable1 = this.Convert(this.ParentFolder);
      row.DisplayName = this.DisplayName;
      row.ActionName = this.Name;
      AUScreenActionBaseState screenActionBaseState1 = row;
      PXSpecialButtonType? nullable2 = nullable1;
      int? nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
      screenActionBaseState1.MenuFolderType = nullable3;
      row.IsTopLevel = this.DisplayOnMainToolbar;
      row.IsLockedOnToolbar = this.IsLockedOnToolbar;
      row.IgnoresArchiveDisabling = this.IgnoresArchiveDisabling;
      AUScreenActionBaseState screenActionBaseState2 = row;
      Readonly.Condition disableCondition1 = this.DisableCondition;
      string str1;
      if ((disableCondition1 != null ? (disableCondition1.Constant.HasValue ? 1 : 0) : 0) == 0)
      {
        Readonly.Condition disableCondition2 = this.DisableCondition;
        if (disableCondition2 == null)
        {
          str1 = (string) null;
        }
        else
        {
          Guid? id = disableCondition2.Id;
          ref Guid? local = ref id;
          str1 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
        }
      }
      else
        str1 = this.DisableCondition?.Constant.ToString();
      screenActionBaseState2.DisableCondition = str1;
      AUScreenActionBaseState screenActionBaseState3 = row;
      Readonly.Condition hideCondition1 = this.HideCondition;
      string str2;
      if ((hideCondition1 != null ? (hideCondition1.Constant.HasValue ? 1 : 0) : 0) == 0)
      {
        Readonly.Condition hideCondition2 = this.HideCondition;
        if (hideCondition2 == null)
        {
          str2 = (string) null;
        }
        else
        {
          Guid? id = hideCondition2.Id;
          ref Guid? local = ref id;
          str2 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
        }
      }
      else
        str2 = this.HideCondition?.Constant.ToString();
      screenActionBaseState3.HideCondition = str2;
      row.DataMember = this.DataMember;
      row.After = this.After;
      row.PlacementInCategory = new byte?((byte) this.PlacementInCategory);
      row.AfterInMenu = this.AfterInMenu;
      row.Form = this.Form;
      row.MassProcessingScreen = this.MassProcessingScreen;
      row.BatchMode = new bool?(this.BatchMode);
      row.DisablePersist = this.DisablePersist;
      AUScreenActionBaseState screenActionBaseState4 = row;
      PXCacheRights? nullable4 = this.MapEnableRights;
      byte? nullable5 = nullable4.HasValue ? new byte?((byte) nullable4.GetValueOrDefault()) : new byte?();
      screenActionBaseState4.MapEnableRights = nullable5;
      AUScreenActionBaseState screenActionBaseState5 = row;
      nullable4 = this.MapViewRights;
      byte? nullable6 = nullable4.HasValue ? new byte?((byte) nullable4.GetValueOrDefault()) : new byte?();
      screenActionBaseState5.MapViewRights = nullable6;
      row.Category = this.Category;
      row.ExposedToMobile = this.ExposeToMobile;
      row.IsActive = new bool?(true);
      row.IsSystem = new bool?(true);
    }

    private PXSpecialButtonType? Convert(FolderType? folder)
    {
      if (!folder.HasValue)
        return new PXSpecialButtonType?();
      FolderType? nullable = folder;
      FolderType folderType1 = FolderType.ToolbarFolder;
      if (nullable.GetValueOrDefault() == folderType1 & nullable.HasValue)
        return new PXSpecialButtonType?(PXSpecialButtonType.ToolbarFolder);
      nullable = folder;
      FolderType folderType2 = FolderType.InquiriesFolder;
      if (nullable.GetValueOrDefault() == folderType2 & nullable.HasValue)
        return new PXSpecialButtonType?(PXSpecialButtonType.InquiriesFolder);
      nullable = folder;
      FolderType folderType3 = FolderType.ReportsFolder;
      if (nullable.GetValueOrDefault() == folderType3 & nullable.HasValue)
        return new PXSpecialButtonType?(PXSpecialButtonType.ReportsFolder);
      nullable = folder;
      FolderType folderType4 = FolderType.ActionsFolder;
      if (nullable.GetValueOrDefault() == folderType4 & nullable.HasValue)
        return new PXSpecialButtonType?(PXSpecialButtonType.ActionsFolder);
      throw new ArgumentException();
    }

    public IEnumerable<AUScreenNavigationParameterState> InitNavigation(
      AUScreenNavigationActionState newAction)
    {
      newAction.DestinationScreenID = this.Navigation.NavigationScreen;
      newAction.WindowMode = PXWindowModeAttribute.FromMode(this.Navigation.WindowMode);
      newAction.ActionType = this.Navigation.ActionType;
      newAction.Icon = this.Navigation.IconName;
      if (this.Navigation.WindowMode == PXBaseRedirectException.WindowMode.Layer)
        newAction.MenuFolderType = new int?(23);
      List<AUScreenNavigationParameterState> navigationParameterStateList1 = new List<AUScreenNavigationParameterState>();
      foreach (Readonly.NavigationParameter assignment in (IEnumerable<Readonly.NavigationParameter>) this.Navigation.Assignments)
      {
        List<AUScreenNavigationParameterState> navigationParameterStateList2 = navigationParameterStateList1;
        AUScreenNavigationParameterState navigationParameterState = new AUScreenNavigationParameterState();
        navigationParameterState.ScreenID = newAction.ScreenID;
        navigationParameterState.ActionName = newAction.ActionName;
        navigationParameterState.FieldName = assignment.FieldName;
        navigationParameterState.IsFromSchema = new bool?(assignment.IsFromScheme);
        navigationParameterState.Value = assignment.Value?.ToString();
        navigationParameterState.IsActive = new bool?(true);
        navigationParameterState.IsSystem = new bool?(true);
        navigationParameterStateList2.Add(navigationParameterState);
      }
      return (IEnumerable<AUScreenNavigationParameterState>) navigationParameterStateList1;
    }

    public void Publish(PXSystemWorkflowContainer dest, AUScreenActionBaseState parent)
    {
      int num1 = 0;
      foreach (Readonly.Assignment fieldAssignment in (IEnumerable<Readonly.Assignment>) this.FieldAssignments)
      {
        AUWorkflowActionUpdateField row1 = new AUWorkflowActionUpdateField()
        {
          ScreenID = parent.ScreenID,
          ActionName = parent.ActionName,
          StateActionFieldLineNbr = new int?(num1++)
        };
        AUWorkflowActionUpdateField row2 = row1;
        fieldAssignment.CopyTo(row2);
        dest.Insert<AUWorkflowActionUpdateField>(row1);
      }
      int num2 = 0;
      foreach (Readonly.Assignment parameterAssignment in (IEnumerable<Readonly.Assignment>) this.ParameterAssignments)
      {
        AUWorkflowActionParam row3 = new AUWorkflowActionParam()
        {
          ScreenID = parent.ScreenID,
          ActionName = parent.ActionName,
          StateActionParamLineNbr = new int?(num2++)
        };
        AUWorkflowActionParam row4 = row3;
        parameterAssignment.CopyTo(row4);
        dest.Insert<AUWorkflowActionParam>(row3);
      }
    }
  }

  public class ActionSequence
  {
    public string PrevActionName { get; set; }

    public string NextActionName { get; set; }

    public bool StopOnError { get; set; }

    public Readonly.Condition Condition { get; set; }

    public IReadOnlyCollection<Readonly.Assignment> FieldAssignments { get; private set; }

    internal static Readonly.ActionSequence From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ActionSequence actionSequence)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.ActionSequence()
      {
        FieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) actionSequence.FieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>(),
        PrevActionName = actionSequence.PrevActionName,
        NextActionName = actionSequence.NextActionName,
        StopOnError = actionSequence.StopOnError,
        Condition = actionSequence.Condition?.AsReadonly()
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ActionSequence To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.ActionSequence()
      {
        FieldAssignments = this.FieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>(),
        PrevActionName = this.PrevActionName,
        NextActionName = this.NextActionName,
        StopOnError = this.StopOnError,
        Condition = this.Condition?.To<TGraph, TPrimary>()
      };
    }

    public void CopyTo(AUWorkflowActionSequence row)
    {
      row.PrevActionName = this.PrevActionName;
      row.NextActionName = this.NextActionName;
      row.StopOnError = new bool?(this.StopOnError);
      row.IsActive = new bool?(true);
      row.Condition = this.Condition?.Name ?? "True";
      row.IsSystem = new bool?(true);
    }

    public void Publish(PXSystemWorkflowContainer dest, AUWorkflowActionSequence parent)
    {
      foreach (Readonly.Assignment fieldAssignment in (IEnumerable<Readonly.Assignment>) this.FieldAssignments)
      {
        AUWorkflowActionSequenceFormFieldValue row1 = new AUWorkflowActionSequenceFormFieldValue()
        {
          ScreenID = parent.ScreenID,
          PrevActionName = parent.PrevActionName,
          NextActionName = parent.NextActionName,
          Condition = parent.Condition
        };
        AUWorkflowActionSequenceFormFieldValue row2 = row1;
        fieldAssignment.CopyTo(row2);
        dest.Insert<AUWorkflowActionSequenceFormFieldValue>(row1);
      }
    }
  }

  public class ActionState
  {
    public string Name { get; private set; }

    public Readonly.Condition AutoRun { get; private set; }

    public bool DuplicatedInToolbar { get; private set; }

    public bool ValidateNewAction { get; set; }

    public ActionConnotation Connotation { get; set; }

    internal static Readonly.ActionState From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ActionState actionState)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.ActionState()
      {
        Name = actionState.Name,
        DuplicatedInToolbar = actionState.DuplicatedInToolbar,
        AutoRun = actionState.AutoRun?.AsReadonly(),
        ValidateNewAction = actionState.ValidateNewAction,
        Connotation = actionState.Connotation
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ActionState To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.ActionState()
      {
        Name = this.Name,
        DuplicatedInToolbar = this.DuplicatedInToolbar,
        AutoRun = this.AutoRun?.To<TGraph, TPrimary>(),
        ValidateNewAction = this.ValidateNewAction,
        Connotation = this.Connotation
      };
    }

    internal void CopyTo(AUWorkflowStateAction dest)
    {
      dest.ActionName = this.Name;
      dest.IsActive = new bool?(true);
      dest.IsDisabled = new bool?(false);
      dest.IsHide = new bool?(false);
      dest.AutoRun = this.AutoRun?.Name ?? "False";
      dest.Connotation = this.Connotation == ActionConnotation.None ? (string) null : this.Connotation.ToString();
      dest.IsTopLevel = new bool?(this.DuplicatedInToolbar);
      dest.IsSystem = new bool?(true);
    }
  }

  public class ArchivingRule
  {
    internal ArchivingRule()
    {
    }

    public System.Type Primary { get; private set; }

    public System.Type Table { get; private set; }

    public ArchivingReferStrategy ReferStrategy { get; private set; }

    public System.Type FK { get; private set; }

    public System.Type Select { get; private set; }

    public bool IsParentToPrimary { get; private set; }

    internal static Readonly.ArchivingRule From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ArchivingRule rule)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.ArchivingRule()
      {
        Primary = rule.Primary,
        Table = rule.Table,
        ReferStrategy = rule.ReferStrategy,
        FK = rule.FK,
        Select = rule.Select,
        IsParentToPrimary = rule.IsParentToPrimary
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ArchivingRule To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.ArchivingRule()
      {
        Primary = this.Primary,
        Table = this.Table,
        ReferStrategy = this.ReferStrategy,
        FK = this.FK,
        Select = this.Select,
        IsParentToPrimary = this.IsParentToPrimary
      };
    }

    internal static Readonly.ArchivingRule From(AUArchivingRule row)
    {
      Readonly.ArchivingRule archivingRule = new Readonly.ArchivingRule();
      archivingRule.Primary = row.PrimaryType != null ? PXBuildManager.GetType(row.PrimaryType, false) : (System.Type) null;
      archivingRule.Table = row.TableType != null ? PXBuildManager.GetType(row.TableType, false) : (System.Type) null;
      archivingRule.ReferStrategy = (ArchivingReferStrategy) row.ReferStrategy.Value;
      archivingRule.FK = row.FKType != null ? PXBuildManager.GetType(row.FKType, false) : (System.Type) null;
      archivingRule.Select = row.SelectType != null ? PXBuildManager.GetType(row.SelectType, false) : (System.Type) null;
      bool? isParentToPrimary = row.IsParentToPrimary;
      bool flag = true;
      archivingRule.IsParentToPrimary = isParentToPrimary.GetValueOrDefault() == flag & isParentToPrimary.HasValue;
      return archivingRule;
    }

    public void CopyTo(AUArchivingRule row)
    {
      row.PrimaryType = this.Primary.FullName;
      row.TableType = this.Table.FullName;
      row.ReferStrategy = new int?((int) this.ReferStrategy);
      row.FKType = this.FK?.FullName;
      row.SelectType = this.Select?.FullName;
      row.IsParentToPrimary = new bool?(this.IsParentToPrimary);
      row.IsActive = new bool?(true);
      row.IsSystem = new bool?(true);
    }
  }

  public class Assignment
  {
    internal Assignment()
    {
    }

    public bool IsActive { get; private set; }

    public bool IsField { get; private set; }

    public string LeftOperandName { get; private set; }

    public object RightOperandValue { get; private set; }

    public bool IsFromScheme { get; set; } = true;

    internal static Readonly.Assignment From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.Assignment assignment)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      Readonly.Assignment assignment1 = new Readonly.Assignment()
      {
        IsActive = assignment.IsActive,
        IsField = assignment.IsField,
        LeftOperandName = assignment.LeftOperandName,
        RightOperandValue = assignment.RightOperandValue,
        IsFromScheme = assignment.IsFromScheme
      };
      if (assignment1.IsField)
        assignment1.LeftOperandName = PXSystemWorkflows.ResolveFieldName(typeof (TPrimary), assignment1.LeftOperandName);
      return assignment1;
    }

    internal BoundedTo<TGraph, TPrimary>.Assignment To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.Assignment()
      {
        IsActive = this.IsActive,
        IsField = this.IsField,
        LeftOperandName = this.LeftOperandName,
        RightOperandValue = this.RightOperandValue,
        IsFromScheme = this.IsFromScheme
      };
    }

    public void CopyTo(AUWorkflowTransitionField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    internal void CopyTo(AUWorkflowOnEnterStateField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    internal void CopyTo(AUWorkflowOnLeaveStateField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowStateActionField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowStateActionParam row)
    {
      row.Parameter = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowActionUpdateField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowHandlerUpdateField row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowActionParam row)
    {
      row.Parameter = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }

    public void CopyTo(AUWorkflowActionSequenceFormFieldValue row)
    {
      row.FieldName = this.LeftOperandName;
      row.Value = Convert.ToString(this.RightOperandValue);
      row.IsActive = new bool?(true);
      row.IsFromScheme = new bool?(this.IsFromScheme);
      row.IsSystem = new bool?(true);
    }
  }

  public abstract class BaseCompositeState : Readonly.BaseFlowStep
  {
    public IReadOnlyCollection<Readonly.BaseFlowStep> States { get; protected set; }
  }

  public abstract class BaseFlowStep : IOrderableWorkflowElement
  {
    public string Identifier { get; protected set; }

    public string NextStateId { get; protected set; }

    public string Description { get; protected set; }

    public Readonly.Condition SkipCondition { get; protected set; }

    public MoveObjectInCollection MoveBefore { get; protected set; }

    public string NearKey { get; protected set; }

    public string Key { get; protected set; }

    public IReadOnlyCollection<Readonly.FieldState> FieldStates { get; protected set; }

    public IReadOnlyCollection<Readonly.ActionState> Actions { get; protected set; }

    public IReadOnlyCollection<Readonly.WorkflowEventHandler> EventHandlers { get; protected set; }

    public IReadOnlyCollection<Readonly.Assignment> OnEnterFieldAssignments { get; protected set; }

    public IReadOnlyCollection<Readonly.Assignment> OnLeaveFieldAssignments { get; protected set; }

    protected internal BaseFlowStep()
    {
    }

    public abstract BoundedTo<TGraph, TPrimary>.BaseFlowStep To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new();

    public virtual void CopyTo(AUWorkflowState row)
    {
      row.IsActive = new bool?(true);
      row.Identifier = this.Identifier;
      row.Description = this.Description;
      row.NextState = this.NextStateId;
      Readonly.Condition skipCondition = this.SkipCondition;
      if ((skipCondition != null ? (skipCondition.Id.HasValue ? 1 : 0) : 0) != 0)
        row.SkipConditionID = this.SkipCondition.Id;
      row.IsSystem = new bool?(true);
    }

    public virtual void Publish(PXSystemWorkflowContainer dest, AUWorkflowState parent)
    {
      int num1 = 0;
      foreach (Readonly.ActionState action in (IEnumerable<Readonly.ActionState>) this.Actions)
      {
        if (action.ValidateNewAction)
        {
          HashSet<IBqlTable> uniqueItems = dest.UniqueItems;
          AUScreenNavigationActionState navigationActionState = new AUScreenNavigationActionState();
          navigationActionState.ScreenID = parent.ScreenID;
          navigationActionState.ActionName = action.Name;
          if (!uniqueItems.Contains((IBqlTable) navigationActionState))
            throw new ArgumentException($"Action state in state {parent.Identifier} in workflow {parent.WorkflowGUID} defined for new action {action.Name} that does not exists.");
        }
        AUWorkflowStateAction workflowStateAction = new AUWorkflowStateAction()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateName = parent.Identifier,
          StateActionLineNbr = new int?(num1++)
        };
        action.CopyTo(workflowStateAction);
        dest.Insert<AUWorkflowStateAction>(workflowStateAction);
      }
      int num2 = 0;
      foreach (Readonly.FieldState fieldState in (IEnumerable<Readonly.FieldState>) this.FieldStates)
      {
        AUWorkflowStateProperty row1 = new AUWorkflowStateProperty()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateName = parent.Identifier,
          StatePropertyLineNbr = new int?(num2++)
        };
        AUWorkflowStateProperty row2 = row1;
        fieldState.CopyTo(row2);
        dest.Insert<AUWorkflowStateProperty>(row1);
        HashSet<IBqlTable> uniqueItems = dest.UniqueItems;
        AUWorkflowStateProperty equalValue = new AUWorkflowStateProperty();
        equalValue.ScreenID = row1.ScreenID;
        equalValue.WorkflowGUID = row1.WorkflowGUID;
        IBqlTable bqlTable;
        ref IBqlTable local = ref bqlTable;
        if (uniqueItems.TryGetValue((IBqlTable) equalValue, out local))
          row1.FlowLevelDefaults = (AUWorkflowStateProperty) bqlTable;
      }
      int num3 = 0;
      foreach (Readonly.WorkflowEventHandler eventHandler in (IEnumerable<Readonly.WorkflowEventHandler>) this.EventHandlers)
      {
        AUWorkflowStateEventHandler row = new AUWorkflowStateEventHandler()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateName = parent.Identifier,
          StateHandlerLineNbr = new int?(num3++)
        };
        AUWorkflowStateEventHandler dest1 = row;
        eventHandler.CopyTo(dest1);
        dest.Insert<AUWorkflowStateEventHandler>(row);
      }
      int num4 = 0;
      foreach (Readonly.Assignment enterFieldAssignment in (IEnumerable<Readonly.Assignment>) this.OnEnterFieldAssignments)
      {
        AUWorkflowOnEnterStateField row3 = new AUWorkflowOnEnterStateField()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateName = parent.Identifier,
          OnEnterStateFieldLineNbr = new int?(num4++)
        };
        AUWorkflowOnEnterStateField row4 = row3;
        enterFieldAssignment.CopyTo(row4);
        dest.Insert<AUWorkflowOnEnterStateField>(row3);
      }
      int num5 = 0;
      foreach (Readonly.Assignment leaveFieldAssignment in (IEnumerable<Readonly.Assignment>) this.OnLeaveFieldAssignments)
      {
        AUWorkflowOnLeaveStateField row5 = new AUWorkflowOnLeaveStateField()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateName = parent.Identifier,
          OnLeaveStateFieldLineNbr = new int?(num5++)
        };
        AUWorkflowOnLeaveStateField row6 = row5;
        leaveFieldAssignment.CopyTo(row6);
        dest.Insert<AUWorkflowOnLeaveStateField>(row5);
      }
    }
  }

  public class Condition
  {
    public Func<IBqlTable, bool> Lambda;

    internal Condition()
    {
    }

    public string Name { get; private set; }

    public Guid? Id { get; private set; }

    public IBqlUnary BqlExpression { get; private set; }

    public bool? Constant { get; private set; }

    public INamedCondition NamedCondition { get; internal set; }

    internal static Readonly.Condition From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.Condition condition)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.Condition()
      {
        BqlExpression = condition.BqlExpression,
        Lambda = condition.Lambda,
        Constant = condition.Constant,
        Name = condition.Name,
        NamedCondition = condition.NamedCondition,
        Id = PXSystemWorkflows.GuidFromString(condition.Name)
      };
    }

    internal BoundedTo<TGraph, TPrimary>.Condition To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.Condition((BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions) null)
      {
        BqlExpression = this.BqlExpression,
        Lambda = this.Lambda,
        Constant = this.Constant,
        Name = this.Name,
        NamedCondition = this.NamedCondition
      };
    }
  }

  public class DynamicFieldState
  {
    public System.Type Table { get; set; }

    public string FieldName { get; set; }

    public Readonly.Condition DisableCondition { get; set; }

    public Readonly.Condition HideCondition { get; set; }

    public Readonly.Condition RequiredCondition { get; set; }

    public string DisplayName { get; set; }

    public bool SkipExistenceCheck { get; set; }

    public Dictionary<string, ComboBoxItemsModification> ComboBoxValuesModifications { get; set; }

    public bool IsFromSchema { get; set; }

    public string DefaultValue { get; set; }

    internal static Readonly.DynamicFieldState From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.DynamicFieldState fieldState)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.DynamicFieldState()
      {
        Table = fieldState.Table,
        FieldName = fieldState.FieldName,
        DisableCondition = fieldState.Disabled?.AsReadonly(),
        HideCondition = fieldState.Hidden?.AsReadonly(),
        RequiredCondition = fieldState.IsRequired?.AsReadonly(),
        ComboBoxValuesModifications = fieldState.ComboBoxValuesModifications,
        DisplayName = fieldState.DisplayName,
        SkipExistenceCheck = fieldState.SkipExistenceCheck,
        IsFromSchema = fieldState.IsFromSchema,
        DefaultValue = fieldState.DefaultValue
      };
    }

    internal BoundedTo<TGraph, TPrimary>.DynamicFieldState To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.DynamicFieldState()
      {
        Table = this.Table,
        FieldName = this.FieldName,
        Disabled = this.DisableCondition?.To<TGraph, TPrimary>(),
        Hidden = this.HideCondition?.To<TGraph, TPrimary>(),
        IsRequired = this.RequiredCondition?.To<TGraph, TPrimary>(),
        ComboBoxValuesModifications = this.ComboBoxValuesModifications,
        DisplayName = this.DisplayName,
        SkipExistenceCheck = this.SkipExistenceCheck,
        IsFromSchema = this.IsFromSchema,
        DefaultValue = this.DefaultValue
      };
    }

    public void CopyTo(AUScreenFieldState row)
    {
      row.TableName = this.Table?.FullName;
      row.FieldName = PXSystemWorkflows.ResolveFieldName(this.Table, this.FieldName, this.SkipExistenceCheck);
      AUScreenFieldState screenFieldState1 = row;
      Readonly.Condition disableCondition1 = this.DisableCondition;
      bool? constant;
      int num1;
      if (disableCondition1 == null)
      {
        num1 = 0;
      }
      else
      {
        constant = disableCondition1.Constant;
        num1 = constant.HasValue ? 1 : 0;
      }
      string str1;
      if (num1 == 0)
      {
        Guid? nullable = PXSystemWorkflows.GuidFromString(this.DisableCondition?.Name);
        ref Guid? local = ref nullable;
        str1 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
      }
      else
      {
        Readonly.Condition disableCondition2 = this.DisableCondition;
        if (disableCondition2 == null)
        {
          str1 = (string) null;
        }
        else
        {
          constant = disableCondition2.Constant;
          str1 = constant.ToString();
        }
      }
      screenFieldState1.DisableCondition = str1;
      AUScreenFieldState screenFieldState2 = row;
      Readonly.Condition hideCondition1 = this.HideCondition;
      int num2;
      if (hideCondition1 == null)
      {
        num2 = 0;
      }
      else
      {
        constant = hideCondition1.Constant;
        num2 = constant.HasValue ? 1 : 0;
      }
      string str2;
      if (num2 == 0)
      {
        Guid? nullable = PXSystemWorkflows.GuidFromString(this.HideCondition?.Name);
        ref Guid? local = ref nullable;
        str2 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
      }
      else
      {
        Readonly.Condition hideCondition2 = this.HideCondition;
        if (hideCondition2 == null)
        {
          str2 = (string) null;
        }
        else
        {
          constant = hideCondition2.Constant;
          str2 = constant.ToString();
        }
      }
      screenFieldState2.HideCondition = str2;
      AUScreenFieldState screenFieldState3 = row;
      Readonly.Condition requiredCondition1 = this.RequiredCondition;
      int num3;
      if (requiredCondition1 == null)
      {
        num3 = 0;
      }
      else
      {
        constant = requiredCondition1.Constant;
        num3 = constant.HasValue ? 1 : 0;
      }
      string str3;
      if (num3 == 0)
      {
        Guid? nullable = PXSystemWorkflows.GuidFromString(this.RequiredCondition?.Name);
        ref Guid? local = ref nullable;
        str3 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
      }
      else
      {
        Readonly.Condition requiredCondition2 = this.RequiredCondition;
        if (requiredCondition2 == null)
        {
          str3 = (string) null;
        }
        else
        {
          constant = requiredCondition2.Constant;
          str3 = constant.ToString();
        }
      }
      screenFieldState3.RequiredCondition = str3;
      row.DisplayName = this.DisplayName;
      row.IsSystem = new bool?(true);
      row.IsActive = new bool?(true);
      row.IsFromSchema = new bool?(this.IsFromSchema);
      row.DefaultValue = this.DefaultValue;
      if (!this.ComboBoxValuesModifications.Any<KeyValuePair<string, ComboBoxItemsModification>>())
        return;
      row.ComboBoxValues = string.Join(";", this.ComboBoxValuesModifications.Select<KeyValuePair<string, ComboBoxItemsModification>, string>((Func<KeyValuePair<string, ComboBoxItemsModification>, string>) (it => it.Value.ToString())));
    }
  }

  public class FieldState
  {
    internal FieldState()
    {
    }

    public System.Type Table { get; private set; }

    public string FieldName { get; private set; }

    public bool IsDisabled { get; private set; }

    public bool IsHidden { get; private set; }

    public bool IsRequired { get; private set; }

    public bool AllFields { get; set; }

    public string DefaultValue { get; private set; }

    public string ComboBoxValues { get; private set; }

    public bool IsFromScheme { get; set; } = true;

    internal static Readonly.FieldState From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.FieldState fieldState)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.FieldState()
      {
        Table = fieldState.Table,
        FieldName = fieldState.FieldName,
        IsDisabled = fieldState.IsDisabled,
        IsHidden = fieldState.IsHidden,
        IsRequired = fieldState.IsRequired,
        DefaultValue = fieldState.DefaultValue,
        ComboBoxValues = fieldState.ComboBoxValues,
        AllFields = fieldState.AllFields
      };
    }

    internal BoundedTo<TGraph, TPrimary>.FieldState To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.FieldState()
      {
        Table = this.Table,
        FieldName = this.FieldName,
        IsDisabled = this.IsDisabled,
        IsHidden = this.IsHidden,
        IsRequired = this.IsRequired,
        DefaultValue = this.DefaultValue,
        ComboBoxValues = this.ComboBoxValues,
        AllFields = this.AllFields
      };
    }

    public void CopyTo(AUWorkflowStateProperty row)
    {
      row.IsActive = new bool?(true);
      row.ObjectName = this.Table?.FullName;
      row.FieldName = !string.IsNullOrEmpty(this.FieldName) ? PXSystemWorkflows.ResolveFieldName(this.Table, this.FieldName) : (this.AllFields ? "<All Fields>" : "<Table>");
      row.IsDisabled = new bool?(this.IsDisabled);
      row.IsHide = new bool?(this.IsHidden);
      row.IsRequired = new bool?(this.IsRequired);
      row.DefaultValue = this.DefaultValue;
      row.ComboBoxValues = this.ComboBoxValues;
      row.IsSystem = new bool?(true);
    }
  }

  public class FlowState : Readonly.BaseFlowStep
  {
    protected internal FlowState()
    {
    }

    public bool IsActive { get; protected set; }

    public bool IsInitial { get; protected set; }

    /// <summary>
    /// If true, the state is non persistent and an entity in the state cannot be saved.
    /// </summary>
    protected internal bool IsNonPersistent { get; protected set; }

    internal static Readonly.FlowState From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.FlowState flowState)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      Readonly.FlowState flowState1 = new Readonly.FlowState();
      flowState1.IsInitial = flowState.IsInitial;
      flowState1.IsNonPersistent = flowState.IsNonPersistent;
      flowState1.Identifier = flowState.Identifier;
      flowState1.Description = flowState.Description;
      flowState1.NextStateId = flowState.NextStateId;
      flowState1.SkipCondition = flowState.SkipCondition?.AsReadonly();
      flowState1.FieldStates = (IReadOnlyCollection<Readonly.FieldState>) flowState.FieldStates.Select<BoundedTo<TGraph, TPrimary>.FieldState, Readonly.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, Readonly.FieldState>) (f => f.AsReadonly())).ToArray<Readonly.FieldState>();
      flowState1.Actions = (IReadOnlyCollection<Readonly.ActionState>) flowState.Actions.Select<BoundedTo<TGraph, TPrimary>.ActionState, Readonly.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, Readonly.ActionState>) (f => f.AsReadonly())).ToArray<Readonly.ActionState>();
      flowState1.EventHandlers = (IReadOnlyCollection<Readonly.WorkflowEventHandler>) flowState.EventHandlers.Select<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler, Readonly.WorkflowEventHandler>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler, Readonly.WorkflowEventHandler>) (f => f.AsReadonly())).ToArray<Readonly.WorkflowEventHandler>();
      flowState1.OnEnterFieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) flowState.OnEnterFieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>();
      flowState1.OnLeaveFieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) flowState.OnLeaveFieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>();
      return flowState1;
    }

    public override BoundedTo<TGraph, TPrimary>.BaseFlowStep To<TGraph, TPrimary>()
    {
      BoundedTo<TGraph, TPrimary>.FlowState flowState = new BoundedTo<TGraph, TPrimary>.FlowState();
      flowState.IsInitial = this.IsInitial;
      flowState.IsNonPersistent = this.IsNonPersistent;
      flowState.Identifier = this.Identifier;
      flowState.Description = this.Description;
      flowState.NextStateId = this.NextStateId;
      flowState.SkipCondition = this.SkipCondition?.To<TGraph, TPrimary>();
      flowState.FieldStates = this.FieldStates.Select<Readonly.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>((Func<Readonly.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.FieldState>();
      flowState.Actions = this.Actions.Select<Readonly.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>((Func<Readonly.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ActionState>();
      flowState.EventHandlers = this.EventHandlers.Select<Readonly.WorkflowEventHandler, BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>((Func<Readonly.WorkflowEventHandler, BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>();
      flowState.OnEnterFieldAssignments = this.OnEnterFieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      flowState.OnLeaveFieldAssignments = this.OnLeaveFieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      return (BoundedTo<TGraph, TPrimary>.BaseFlowStep) flowState;
    }

    public override void CopyTo(AUWorkflowState row)
    {
      base.CopyTo(row);
      row.IsInitial = new bool?(this.IsInitial);
      row.IsNonPersistent = this.IsNonPersistent;
    }
  }

  public class Form
  {
    internal Form()
    {
    }

    public string Name { get; private set; }

    public string DisplayName { get; private set; }

    public int ColumnsCount { get; private set; }

    public string DacType { get; set; }

    public bool MapAllFields { get; set; }

    public IReadOnlyCollection<Readonly.FormField> Fields { get; private set; }

    internal static Readonly.Form From<TGraph, TPrimary>(BoundedTo<TGraph, TPrimary>.Form form)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.Form()
      {
        Name = form.Name,
        DisplayName = form.DisplayName,
        ColumnsCount = form.ColumnsCount,
        DacType = form.DacType,
        MapAllFields = form.MapAllFields,
        Fields = (IReadOnlyCollection<Readonly.FormField>) form.Fields.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>().Select<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, Readonly.FormField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, Readonly.FormField>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.FormField>()
      };
    }

    internal BoundedTo<TGraph, TPrimary>.Form To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.Form()
      {
        Name = this.Name,
        DisplayName = this.DisplayName,
        ColumnsCount = this.ColumnsCount,
        Fields = this.Fields.Select<Readonly.FormField, BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<Readonly.FormField, BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>()
      };
    }

    internal void CopyTo(AUWorkflowForm dest)
    {
      dest.FormName = this.Name;
      dest.DisplayName = this.DisplayName;
      dest.Columns = new int?(this.ColumnsCount);
      dest.DacType = this.DacType;
      dest.IsSystem = new bool?(true);
      dest.IsActive = new bool?(true);
    }
  }

  public class FormField
  {
    internal const string ComboBoxValuesSourceSourceStateCode = "S";
    internal const string ComboBoxValuesSourceTargetStateCode = "T";
    internal const string ComboBoxValuesSourceExplicitValuesCode = "E";

    internal FormField()
    {
    }

    public bool IsActive { get; private set; }

    public string SchemaField { get; private set; }

    public string Name { get; private set; }

    public string DisplayName { get; private set; }

    public string DefaultValue { get; private set; }

    public Readonly.Condition RequiredCondition { get; private set; }

    public Readonly.Condition HideCondition { get; private set; }

    public int ColumnSpan { get; private set; }

    public string ControlSize { get; private set; }

    public string ComboBoxValues { get; private set; }

    public ComboBoxValuesSource ComboBoxValuesSource { get; private set; }

    public bool IsFromScheme { get; private set; } = true;

    public string ComboboxAndDefaultSourceField { get; private set; }

    public DefaultValueSource DefaultValueSource { get; private set; }

    internal static Readonly.FormField From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.FormField formField)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.FormField()
      {
        IsActive = formField.IsActive,
        SchemaField = formField.SchemaField,
        Name = formField.Name,
        DisplayName = formField.DisplayName,
        DefaultValue = formField.DefaultValue,
        RequiredCondition = formField.RequiredCondition?.AsReadonly(),
        HideCondition = formField.HideCondition?.AsReadonly(),
        ColumnSpan = formField.ColumnSpan,
        ControlSize = formField.ControlSize,
        ComboBoxValues = formField.ComboBoxValues,
        ComboBoxValuesSource = formField.ComboBoxValuesSource,
        IsFromScheme = formField.IsFromScheme
      };
    }

    internal BoundedTo<TGraph, TPrimary>.FormField To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.FormField()
      {
        IsActive = this.IsActive,
        SchemaField = this.SchemaField,
        Name = this.Name,
        DisplayName = this.DisplayName,
        DefaultValue = this.DefaultValue,
        RequiredCondition = this.RequiredCondition?.To<TGraph, TPrimary>(),
        HideCondition = this.HideCondition?.To<TGraph, TPrimary>(),
        ColumnSpan = this.ColumnSpan,
        ControlSize = this.ControlSize,
        ComboBoxValues = this.ComboBoxValues,
        ComboBoxValuesSource = this.ComboBoxValuesSource,
        IsFromScheme = this.IsFromScheme
      };
    }

    internal static Readonly.FormField From(
      string dacName,
      string dacField,
      PXWorkflowFormFieldLayoutAttribute workflowFormFieldLayoutAttribute,
      PXWorkflowFormBehaviorAttribute workflowFormBehaviorAttribute)
    {
      return new Readonly.FormField()
      {
        IsActive = true,
        SchemaField = $"{dacName}.{dacField}",
        Name = dacField,
        ColumnSpan = workflowFormFieldLayoutAttribute != null ? workflowFormFieldLayoutAttribute.ColumnSpan : 0,
        ControlSize = workflowFormFieldLayoutAttribute?.ControlSize,
        ComboBoxValuesSource = workflowFormBehaviorAttribute != null ? workflowFormBehaviorAttribute.ComboBoxValuesSource : ComboBoxValuesSource.SourceState,
        DefaultValueSource = workflowFormBehaviorAttribute != null ? workflowFormBehaviorAttribute.DefaultValueSource : DefaultValueSource.ExplicitValue,
        ComboboxAndDefaultSourceField = workflowFormBehaviorAttribute?.ComboboxAndDefaultSourceField != (System.Type) null ? $"{workflowFormBehaviorAttribute.ComboboxAndDefaultSourceField.DeclaringType?.ToString()}.{workflowFormBehaviorAttribute.ComboboxAndDefaultSourceField.Name}" : (string) null,
        IsFromScheme = true
      };
    }

    internal static Readonly.FormField From(
      Readonly.FormField primaryField,
      Readonly.FormField overrides)
    {
      return new Readonly.FormField()
      {
        IsActive = primaryField.IsActive,
        SchemaField = primaryField.SchemaField,
        Name = primaryField.Name,
        DisplayName = overrides.DisplayName ?? primaryField.DisplayName,
        DefaultValue = overrides.DefaultValue ?? primaryField.DefaultValue,
        RequiredCondition = overrides.RequiredCondition ?? primaryField.RequiredCondition,
        HideCondition = overrides.HideCondition ?? primaryField.HideCondition,
        ColumnSpan = overrides.ColumnSpan > 0 ? overrides.ColumnSpan : primaryField.ColumnSpan,
        ControlSize = overrides.ControlSize ?? primaryField.ControlSize,
        ComboBoxValues = overrides.ComboBoxValues ?? primaryField.ComboBoxValues,
        ComboBoxValuesSource = overrides.ComboBoxValuesSource,
        IsFromScheme = primaryField.IsFromScheme
      };
    }

    internal void CopyTo(AUWorkflowFormField dest)
    {
      dest.IsActive = new bool?(true);
      dest.FromScheme = new bool?(this.IsFromScheme);
      dest.SchemaField = this.SchemaField;
      dest.FieldName = this.Name;
      dest.DisplayName = this.DisplayName;
      dest.DefaultValue = this.DefaultValue;
      AUWorkflowFormField workflowFormField1 = dest;
      Readonly.Condition requiredCondition1 = this.RequiredCondition;
      string str1;
      if ((requiredCondition1 != null ? (requiredCondition1.Constant.HasValue ? 1 : 0) : 0) == 0)
      {
        Readonly.Condition requiredCondition2 = this.RequiredCondition;
        if (requiredCondition2 == null)
        {
          str1 = (string) null;
        }
        else
        {
          Guid? id = requiredCondition2.Id;
          ref Guid? local = ref id;
          str1 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
        }
      }
      else
        str1 = this.RequiredCondition?.Constant.ToString();
      workflowFormField1.RequiredCondition = str1;
      AUWorkflowFormField workflowFormField2 = dest;
      Readonly.Condition hideCondition1 = this.HideCondition;
      string str2;
      if ((hideCondition1 != null ? (hideCondition1.Constant.HasValue ? 1 : 0) : 0) == 0)
      {
        Readonly.Condition hideCondition2 = this.HideCondition;
        if (hideCondition2 == null)
        {
          str2 = (string) null;
        }
        else
        {
          Guid? id = hideCondition2.Id;
          ref Guid? local = ref id;
          str2 = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
        }
      }
      else
        str2 = this.HideCondition?.Constant.ToString();
      workflowFormField2.HideCondition = str2;
      dest.ColumnSpan = new int?(this.ColumnSpan);
      dest.ControlSize = this.ControlSize;
      dest.ComboBoxValues = this.ComboBoxValues;
      dest.IsSystem = new bool?(true);
      switch (this.ComboBoxValuesSource)
      {
        case ComboBoxValuesSource.SourceState:
          dest.ComboBoxValuesSource = "S";
          break;
        case ComboBoxValuesSource.TargetState:
          dest.ComboBoxValuesSource = "T";
          break;
        default:
          dest.ComboBoxValuesSource = "E";
          break;
      }
      dest.ComboboxAndDefaultSourceField = this.ComboboxAndDefaultSourceField;
      switch (this.DefaultValueSource)
      {
        case DefaultValueSource.SourceState:
          dest.DefaultValueSource = "S";
          break;
        case DefaultValueSource.TargetState:
          dest.DefaultValueSource = "T";
          break;
        default:
          dest.DefaultValueSource = "E";
          break;
      }
    }
  }

  public class NavigationDefinition
  {
    public string NavigationScreen { get; private set; }

    public PXBaseRedirectException.WindowMode WindowMode { get; private set; }

    public string IconName { get; private set; }

    public IReadOnlyCollection<Readonly.NavigationParameter> Assignments { get; private set; }

    public string ActionType { get; private set; }

    internal static Readonly.NavigationDefinition From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.NavigationDefinition navigationDefinition)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.NavigationDefinition()
      {
        NavigationScreen = navigationDefinition.NavigationScreen,
        WindowMode = navigationDefinition.WindowMode,
        ActionType = navigationDefinition.ActionType,
        Assignments = (IReadOnlyCollection<Readonly.NavigationParameter>) navigationDefinition.Assignments.Select<BoundedTo<TGraph, TPrimary>.NavigationParameter, Readonly.NavigationParameter>((Func<BoundedTo<TGraph, TPrimary>.NavigationParameter, Readonly.NavigationParameter>) (fa => fa.AsReadonly())).ToArray<Readonly.NavigationParameter>(),
        IconName = navigationDefinition.IconName
      };
    }

    internal BoundedTo<TGraph, TPrimary>.NavigationDefinition To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.NavigationDefinition()
      {
        NavigationScreen = this.NavigationScreen,
        WindowMode = this.WindowMode,
        ActionType = this.ActionType,
        Assignments = this.Assignments.Select<Readonly.NavigationParameter, BoundedTo<TGraph, TPrimary>.NavigationParameter>((Func<Readonly.NavigationParameter, BoundedTo<TGraph, TPrimary>.NavigationParameter>) (fa => fa.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.NavigationParameter>(),
        IconName = this.IconName
      };
    }
  }

  public class NavigationParameter
  {
    public string FieldName { get; private set; }

    public object Value { get; private set; }

    public bool IsFromScheme { get; private set; } = true;

    internal static Readonly.NavigationParameter From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.NavigationParameter navigationParameter)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.NavigationParameter()
      {
        Value = navigationParameter.Value,
        FieldName = navigationParameter.FieldName,
        IsFromScheme = navigationParameter.IsFromScheme
      };
    }

    internal BoundedTo<TGraph, TPrimary>.NavigationParameter To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.NavigationParameter()
      {
        Value = this.Value,
        FieldName = this.FieldName,
        IsFromScheme = this.IsFromScheme
      };
    }
  }

  public class ScreenConfiguration
  {
    internal ScreenConfiguration()
    {
    }

    public string StateIdentifier { get; private set; }

    public string FlowIdentifier { get; private set; }

    public bool AllowUserToChange { get; private set; }

    public string FlowSubIdentifier { get; private set; }

    public bool AllowUserToChangeSubType { get; private set; }

    public bool AllowWorkflowCustomization { get; private set; }

    public IReadOnlyCollection<Readonly.DynamicFieldState> GlobalFieldStates { get; private set; }

    public IReadOnlyCollection<Readonly.ActionDefinition> Actions { get; private set; }

    public IReadOnlyCollection<Readonly.ActionCategory> Categories { get; private set; }

    public IReadOnlyCollection<Readonly.WorkflowEventHandlerDefinition> Handlers { get; private set; }

    public IReadOnlyCollection<Readonly.ActionSequence> ActionSequences { get; private set; }

    public IReadOnlyCollection<Readonly.ArchivingRule> ArchivingRules { get; private set; }

    public IReadOnlyCollection<Readonly.Form> Forms { get; private set; }

    public IReadOnlyCollection<Readonly.Workflow> Flows { get; private set; }

    public IReadOnlyCollection<Readonly.Condition> SharedConditions { get; private set; }

    internal static Readonly.ScreenConfiguration From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration screen)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      string fieldName1 = screen.StateIdentifier;
      string fieldName2 = screen.FlowIdentifier;
      string fieldName3 = screen.FlowSubIdentifier;
      try
      {
        fieldName1 = PXSystemWorkflows.ResolveFieldName(typeof (TPrimary), fieldName1);
        fieldName2 = PXSystemWorkflows.ResolveFieldName(typeof (TPrimary), fieldName2);
        fieldName3 = PXSystemWorkflows.ResolveFieldName(typeof (TPrimary), fieldName3);
      }
      catch (PXException ex)
      {
        PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<System.Type>((Exception) ex, "The system has failed to initialize the workflow. CacheType:{CacheType}", typeof (TPrimary));
        throw;
      }
      catch (Exception ex)
      {
        PXTrace.Logger.ForSystemEvents("System", "System_WorkflowCouldNotAccessCache").ForCurrentCompanyContext().Warning<System.Type>(ex, "The system could not access the cache when searching for information about the fields used in the workflow. CacheType:{CacheType}", typeof (TPrimary));
      }
      Readonly.ActionCategory[] array = Readonly.ScreenConfiguration.SortCategories((IEnumerable<Readonly.ActionCategory>) screen.Categories.Select<BoundedTo<TGraph, TPrimary>.ActionCategory, Readonly.ActionCategory>((Func<BoundedTo<TGraph, TPrimary>.ActionCategory, Readonly.ActionCategory>) (c => c.AsReadonly())).ToList<Readonly.ActionCategory>()).ToArray<Readonly.ActionCategory>();
      for (int index = 1; index < array.Length; ++index)
      {
        if (!PlacementHelper.HasPlacement(array[index].Placement, array[index].After))
        {
          array[index].Placement = Placement.After;
          array[index].After = array[index - 1].CategoryName;
        }
      }
      return new Readonly.ScreenConfiguration()
      {
        StateIdentifier = fieldName1,
        FlowIdentifier = fieldName2,
        FlowSubIdentifier = fieldName3,
        AllowUserToChange = screen.AllowUserToChange,
        AllowUserToChangeSubType = screen.AllowUserToChangeSubType,
        AllowWorkflowCustomization = screen.AllowWorkflowCustomization,
        GlobalFieldStates = (IReadOnlyCollection<Readonly.DynamicFieldState>) screen.GlobalFieldStates.Select<BoundedTo<TGraph, TPrimary>.DynamicFieldState, Readonly.DynamicFieldState>((Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState, Readonly.DynamicFieldState>) (fs => fs.AsReadonly())).ToArray<Readonly.DynamicFieldState>(),
        Actions = (IReadOnlyCollection<Readonly.ActionDefinition>) screen.Actions.Select<BoundedTo<TGraph, TPrimary>.ActionDefinition, Readonly.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, Readonly.ActionDefinition>) (a => a.AsReadonly())).ToArray<Readonly.ActionDefinition>(),
        Categories = (IReadOnlyCollection<Readonly.ActionCategory>) array,
        Handlers = (IReadOnlyCollection<Readonly.WorkflowEventHandlerDefinition>) screen.Handlers.Select<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition, Readonly.WorkflowEventHandlerDefinition>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition, Readonly.WorkflowEventHandlerDefinition>) (a => a.AsReadonly())).ToArray<Readonly.WorkflowEventHandlerDefinition>(),
        ActionSequences = (IReadOnlyCollection<Readonly.ActionSequence>) screen.ActionSequences.Select<BoundedTo<TGraph, TPrimary>.ActionSequence, Readonly.ActionSequence>((Func<BoundedTo<TGraph, TPrimary>.ActionSequence, Readonly.ActionSequence>) (a => a.AsReadonly())).ToArray<Readonly.ActionSequence>(),
        ArchivingRules = (IReadOnlyCollection<Readonly.ArchivingRule>) screen.ArchivingRules.Select<BoundedTo<TGraph, TPrimary>.ArchivingRule, Readonly.ArchivingRule>((Func<BoundedTo<TGraph, TPrimary>.ArchivingRule, Readonly.ArchivingRule>) (r => r.AsReadonly())).ToArray<Readonly.ArchivingRule>(),
        Forms = (IReadOnlyCollection<Readonly.Form>) screen.Forms.Select<BoundedTo<TGraph, TPrimary>.Form, Readonly.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, Readonly.Form>) (f => f.AsReadonly())).ToArray<Readonly.Form>(),
        Flows = (IReadOnlyCollection<Readonly.Workflow>) screen.Flows.Select<BoundedTo<TGraph, TPrimary>.Workflow, Readonly.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, Readonly.Workflow>) (f => f.AsReadonly())).ToArray<Readonly.Workflow>(),
        SharedConditions = (IReadOnlyCollection<Readonly.Condition>) screen.SharedConditions.Select<BoundedTo<TGraph, TPrimary>.ISharedCondition, Readonly.Condition>((Func<BoundedTo<TGraph, TPrimary>.ISharedCondition, Readonly.Condition>) (c => ((BoundedTo<TGraph, TPrimary>.Condition) c).AsReadonly())).ToArray<Readonly.Condition>()
      };
    }

    internal BoundedTo<TGraph, TPrimary>.ScreenConfiguration To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      string stateIdentifier = this.StateIdentifier;
      string flowIdentifier = this.FlowIdentifier;
      string flowSubIdentifier = this.FlowSubIdentifier;
      return new BoundedTo<TGraph, TPrimary>.ScreenConfiguration()
      {
        StateIdentifier = stateIdentifier,
        FlowIdentifier = flowIdentifier,
        FlowSubIdentifier = flowSubIdentifier,
        AllowUserToChange = this.AllowUserToChange,
        AllowUserToChangeSubType = this.AllowUserToChangeSubType,
        AllowWorkflowCustomization = this.AllowWorkflowCustomization,
        GlobalFieldStates = this.GlobalFieldStates.Select<Readonly.DynamicFieldState, BoundedTo<TGraph, TPrimary>.DynamicFieldState>((Func<Readonly.DynamicFieldState, BoundedTo<TGraph, TPrimary>.DynamicFieldState>) (fs => fs.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.DynamicFieldState>(),
        Actions = this.Actions.Select<Readonly.ActionDefinition, BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<Readonly.ActionDefinition, BoundedTo<TGraph, TPrimary>.ActionDefinition>) (a => a.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ActionDefinition>(),
        Categories = this.Categories.Select<Readonly.ActionCategory, BoundedTo<TGraph, TPrimary>.ActionCategory>((Func<Readonly.ActionCategory, BoundedTo<TGraph, TPrimary>.ActionCategory>) (c => c.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ActionCategory>(),
        Handlers = this.Handlers.Select<Readonly.WorkflowEventHandlerDefinition, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>((Func<Readonly.WorkflowEventHandlerDefinition, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>) (a => a.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>(),
        ActionSequences = this.ActionSequences.Select<Readonly.ActionSequence, BoundedTo<TGraph, TPrimary>.ActionSequence>((Func<Readonly.ActionSequence, BoundedTo<TGraph, TPrimary>.ActionSequence>) (a => a.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ActionSequence>(),
        ArchivingRules = this.ArchivingRules.Select<Readonly.ArchivingRule, BoundedTo<TGraph, TPrimary>.ArchivingRule>((Func<Readonly.ArchivingRule, BoundedTo<TGraph, TPrimary>.ArchivingRule>) (r => r.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ArchivingRule>(),
        Forms = this.Forms.Select<Readonly.Form, BoundedTo<TGraph, TPrimary>.Form>((Func<Readonly.Form, BoundedTo<TGraph, TPrimary>.Form>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.Form>(),
        Flows = this.Flows.Select<Readonly.Workflow, BoundedTo<TGraph, TPrimary>.Workflow>((Func<Readonly.Workflow, BoundedTo<TGraph, TPrimary>.Workflow>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.Workflow>(),
        SharedConditions = this.SharedConditions.Select<Readonly.Condition, BoundedTo<TGraph, TPrimary>.ISharedCondition>((Func<Readonly.Condition, BoundedTo<TGraph, TPrimary>.ISharedCondition>) (c => (BoundedTo<TGraph, TPrimary>.ISharedCondition) c.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ISharedCondition>()
      };
    }

    private static IEnumerable<Readonly.ActionCategory> SortCategories(
      IEnumerable<Readonly.ActionCategory> categories)
    {
      return PlacementHelper.SortBeforeAfter<Readonly.ActionCategory>(categories, (Func<Readonly.ActionCategory, string>) (cat => cat.CategoryName), (Func<Readonly.ActionCategory, Placement>) (cat => cat.Placement), (Func<Readonly.ActionCategory, string>) (cat => cat.After));
    }

    internal void Publish(PXSystemWorkflowContainer dest, string screenID)
    {
      AUScreenActionBaseState screenActionBaseState = (AUScreenActionBaseState) null;
      foreach (Readonly.Condition sharedCondition in (IEnumerable<Readonly.Condition>) this.SharedConditions)
      {
        AUScreenConditionState screenConditionState = new AUScreenConditionState()
        {
          ScreenID = screenID,
          ConditionName = sharedCondition.Name,
          ConditionID = sharedCondition.Id
        };
        screenConditionState.InternalImplementation = (IWorkflowCondition) dest.RegisterCondition(sharedCondition, (IBqlTable) screenConditionState, sharedCondition.Name);
        dest.Insert<AUScreenConditionState>(screenConditionState);
      }
      foreach (Readonly.DynamicFieldState globalFieldState in (IEnumerable<Readonly.DynamicFieldState>) this.GlobalFieldStates)
      {
        AUScreenFieldState screenFieldState = new AUScreenFieldState()
        {
          ScreenID = screenID
        };
        AUScreenFieldState row = screenFieldState;
        globalFieldState.CopyTo(row);
        dest.Insert<AUScreenFieldState>(screenFieldState);
        dest.RegisterFieldState(screenFieldState);
      }
      foreach (Readonly.Form form1 in (IEnumerable<Readonly.Form>) this.Forms)
      {
        Readonly.Form form = form1;
        AUWorkflowForm auWorkflowForm = new AUWorkflowForm()
        {
          Screen = screenID
        };
        form.CopyTo(auWorkflowForm);
        List<Readonly.FormField> formFieldList = new List<Readonly.FormField>();
        if (form.DacType != null && form.MapAllFields)
        {
          System.Type type = PXBuildManager.GetType(form.DacType, false);
          if (Attribute.GetCustomAttribute((MemberInfo) type, typeof (PXWorkflowFormLayoutAttribute)) is PXWorkflowFormLayoutAttribute customAttribute)
          {
            if (string.IsNullOrEmpty(auWorkflowForm.DisplayName))
              auWorkflowForm.DisplayName = customAttribute.Prompt;
            if (auWorkflowForm.Columns.HasValue)
            {
              int? columns = auWorkflowForm.Columns;
              int num = 0;
              if (!(columns.GetValueOrDefault() == num & columns.HasValue) || customAttribute.ColumnsCount <= 0)
                goto label_23;
            }
            auWorkflowForm.Columns = new int?(customAttribute.ColumnsCount);
          }
label_23:
          PXCache formCache = dest.Graph.Caches[type];
          formFieldList = formCache.BqlFields.Select<System.Type, Readonly.FormField>((Func<System.Type, Readonly.FormField>) (it => Readonly.FormField.From(form.DacType, it.Name, formCache.GetAttributesOfType<PXWorkflowFormFieldLayoutAttribute>((object) null, it.Name).FirstOrDefault<PXWorkflowFormFieldLayoutAttribute>(), formCache.GetAttributesOfType<PXWorkflowFormBehaviorAttribute>((object) null, it.Name).FirstOrDefault<PXWorkflowFormBehaviorAttribute>()))).ToList<Readonly.FormField>();
        }
        dest.Insert<AUWorkflowForm>(auWorkflowForm);
        foreach (Readonly.FormField field in (IEnumerable<Readonly.FormField>) form.Fields)
        {
          Readonly.FormField formField = field;
          int index = formFieldList.FindIndex((Predicate<Readonly.FormField>) (f => string.Equals(f.Name, formField.Name, StringComparison.OrdinalIgnoreCase)));
          if (index >= 0)
            formFieldList[index] = Readonly.FormField.From(formFieldList[index], formField);
          else
            formFieldList.Add(formField);
        }
        int num1 = 0;
        foreach (Readonly.FormField formField in formFieldList)
        {
          AUWorkflowFormField row = new AUWorkflowFormField()
          {
            Screen = screenID,
            FormName = auWorkflowForm.FormName,
            LineNumber = new int?(num1++)
          };
          AUWorkflowFormField dest1 = row;
          formField.CopyTo(dest1);
          dest.Insert<AUWorkflowFormField>(row);
        }
      }
      foreach (Readonly.ActionDefinition action in (IEnumerable<Readonly.ActionDefinition>) this.Actions)
      {
        if (action.CreateNewAction)
        {
          AUScreenNavigationActionState navigationActionState1 = new AUScreenNavigationActionState();
          navigationActionState1.ScreenID = screenID;
          navigationActionState1.WindowMode = "S";
          AUScreenNavigationActionState navigationActionState2 = navigationActionState1;
          action.CopyTo((AUScreenActionBaseState) navigationActionState2);
          if (navigationActionState2.After == null)
            navigationActionState2.After = screenActionBaseState?.ActionName ?? "Last";
          if (action.Navigation != null)
          {
            IEnumerable<AUScreenNavigationParameterState> source = action.InitNavigation(navigationActionState2);
            dest.InsertRange<AUScreenNavigationParameterState>((IList<AUScreenNavigationParameterState>) source.ToList<AUScreenNavigationParameterState>());
          }
          dest.Insert<AUScreenNavigationActionState>(navigationActionState2);
          screenActionBaseState = (AUScreenActionBaseState) navigationActionState2;
          action.Publish(dest, (AUScreenActionBaseState) navigationActionState2);
        }
        else
        {
          AUScreenActionState screenActionState1 = new AUScreenActionState();
          screenActionState1.ScreenID = screenID;
          AUScreenActionState screenActionState2 = screenActionState1;
          action.CopyTo((AUScreenActionBaseState) screenActionState2);
          if (screenActionState2.After == null)
            screenActionState2.After = screenActionBaseState?.ActionName ?? "Last";
          dest.Insert<AUScreenActionState>(screenActionState2);
          screenActionBaseState = (AUScreenActionBaseState) screenActionState2;
          action.Publish(dest, (AUScreenActionBaseState) screenActionState2);
        }
      }
      foreach (Readonly.ActionCategory category in (IEnumerable<Readonly.ActionCategory>) this.Categories)
      {
        AUWorkflowCategory row1 = new AUWorkflowCategory()
        {
          ScreenID = screenID
        };
        AUWorkflowCategory row2 = row1;
        category.CopyTo(row2);
        dest.Insert<AUWorkflowCategory>(row1);
      }
      foreach (Readonly.WorkflowEventHandlerDefinition handler in (IEnumerable<Readonly.WorkflowEventHandlerDefinition>) this.Handlers)
      {
        AUWorkflowHandler auWorkflowHandler = new AUWorkflowHandler()
        {
          ScreenID = screenID
        };
        handler.CopyTo(auWorkflowHandler);
        dest.Insert<AUWorkflowHandler>(auWorkflowHandler);
        handler.Publish(dest, auWorkflowHandler);
      }
      int num2 = 0;
      foreach (Readonly.ActionSequence actionSequence in (IEnumerable<Readonly.ActionSequence>) this.ActionSequences)
      {
        AUWorkflowActionSequence workflowActionSequence = new AUWorkflowActionSequence()
        {
          ScreenID = screenID,
          LineNbr = new int?(num2++)
        };
        actionSequence.CopyTo(workflowActionSequence);
        dest.Insert<AUWorkflowActionSequence>(workflowActionSequence);
        actionSequence.Publish(dest, workflowActionSequence);
      }
      foreach (Readonly.ArchivingRule archivingRule in (IEnumerable<Readonly.ArchivingRule>) this.ArchivingRules)
      {
        AUArchivingRule row3 = new AUArchivingRule()
        {
          ScreenID = screenID
        };
        AUArchivingRule row4 = row3;
        archivingRule.CopyTo(row4);
        dest.Insert<AUArchivingRule>(row3);
      }
      int num3 = 0;
      foreach (Readonly.Workflow flow in (IEnumerable<Readonly.Workflow>) this.Flows)
      {
        AUWorkflow auWorkflow = new AUWorkflow()
        {
          ScreenID = screenID,
          LineNbr = new int?(num3++)
        };
        flow.CopyTo(auWorkflow);
        dest.Insert<AUWorkflow>(auWorkflow);
        flow.Publish(dest, auWorkflow);
      }
    }

    internal void CopyTo(AUWorkflowDefinition dest)
    {
      dest.StateField = this.StateIdentifier;
      dest.FlowTypeField = this.FlowIdentifier;
      dest.EnableWorkflowIDField = new bool?(this.AllowUserToChange);
      dest.FlowSubTypeField = this.FlowSubIdentifier;
      dest.EnableWorkflowSubTypeField = new bool?(this.AllowUserToChangeSubType);
      dest.AllowWorkflowCustomization = new bool?(this.AllowWorkflowCustomization);
    }
  }

  public class Sequence : Readonly.BaseCompositeState
  {
    internal static Readonly.Sequence From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.Sequence sequence)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      Readonly.Sequence sequence1 = new Readonly.Sequence();
      sequence1.Identifier = sequence.Identifier;
      sequence1.Description = sequence.Description;
      sequence1.NextStateId = sequence.NextStateId;
      sequence1.SkipCondition = sequence.SkipCondition?.AsReadonly();
      sequence1.FieldStates = (IReadOnlyCollection<Readonly.FieldState>) sequence.FieldStates.Select<BoundedTo<TGraph, TPrimary>.FieldState, Readonly.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, Readonly.FieldState>) (f => f.AsReadonly())).ToArray<Readonly.FieldState>();
      sequence1.Actions = (IReadOnlyCollection<Readonly.ActionState>) sequence.Actions.Select<BoundedTo<TGraph, TPrimary>.ActionState, Readonly.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, Readonly.ActionState>) (f => f.AsReadonly())).ToArray<Readonly.ActionState>();
      sequence1.EventHandlers = (IReadOnlyCollection<Readonly.WorkflowEventHandler>) sequence.EventHandlers.Select<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler, Readonly.WorkflowEventHandler>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler, Readonly.WorkflowEventHandler>) (f => f.AsReadonly())).ToArray<Readonly.WorkflowEventHandler>();
      sequence1.States = (IReadOnlyCollection<Readonly.BaseFlowStep>) sequence.States.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>().Select<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, Readonly.BaseFlowStep>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, Readonly.BaseFlowStep>) (f => f.GetResult().AsReadonly())).ToArray<Readonly.BaseFlowStep>();
      sequence1.OnEnterFieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) sequence.OnEnterFieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>();
      sequence1.OnLeaveFieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) sequence.OnLeaveFieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>();
      return sequence1;
    }

    public override BoundedTo<TGraph, TPrimary>.BaseFlowStep To<TGraph, TPrimary>()
    {
      BoundedTo<TGraph, TPrimary>.Sequence sequence = new BoundedTo<TGraph, TPrimary>.Sequence();
      sequence.Identifier = this.Identifier;
      sequence.Description = this.Description;
      sequence.NextStateId = this.NextStateId;
      sequence.SkipCondition = this.SkipCondition?.To<TGraph, TPrimary>();
      sequence.FieldStates = this.FieldStates.Select<Readonly.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>((Func<Readonly.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.FieldState>();
      sequence.Actions = this.Actions.Select<Readonly.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>((Func<Readonly.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.ActionState>();
      sequence.EventHandlers = this.EventHandlers.Select<Readonly.WorkflowEventHandler, BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>((Func<Readonly.WorkflowEventHandler, BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>) (f => f.To<TGraph, TPrimary>())).ToList<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>();
      sequence.States = this.States.Select<Readonly.BaseFlowStep, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<Readonly.BaseFlowStep, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>) (f => f.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>();
      sequence.OnEnterFieldAssignments = this.OnEnterFieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      sequence.OnLeaveFieldAssignments = this.OnLeaveFieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      return (BoundedTo<TGraph, TPrimary>.BaseFlowStep) sequence;
    }

    public override void CopyTo(AUWorkflowState row)
    {
      base.CopyTo(row);
      row.IsInitial = new bool?(false);
      row.StateType = "S";
    }

    public override void Publish(PXSystemWorkflowContainer dest, AUWorkflowState parent)
    {
      base.Publish(dest, parent);
      int num = 0;
      foreach (Readonly.BaseFlowStep state in (IEnumerable<Readonly.BaseFlowStep>) this.States)
      {
        AUWorkflowState auWorkflowState = new AUWorkflowState()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateLineNbr = new int?(num++),
          ParentState = parent.Identifier
        };
        state.CopyTo(auWorkflowState);
        dest.Insert<AUWorkflowState>(auWorkflowState);
        state.Publish(dest, auWorkflowState);
      }
    }
  }

  public class Transition
  {
    internal Transition()
    {
    }

    public bool IsActive { get; private set; }

    public string Name { get; private set; }

    public string SourceIdentifier { get; private set; }

    public string TargetIdentifier { get; private set; }

    public string Action { get; private set; }

    public bool IsTriggeredOnEventHandler { get; set; }

    public Readonly.Condition Condition { get; private set; }

    public bool DisablePersist { get; set; }

    public bool ValidateNewAction { get; set; }

    public IReadOnlyCollection<Readonly.Assignment> FieldAssignments { get; private set; }

    internal static Readonly.Transition From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.Transition transition)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.Transition()
      {
        IsActive = transition.IsActive,
        Name = transition.Name,
        SourceIdentifier = transition.SourceIdentifier,
        TargetIdentifier = transition.TargetIdentifier,
        Action = transition.Action,
        Condition = transition.Condition?.AsReadonly(),
        DisablePersist = transition.DisablePersist,
        FieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) transition.FieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>(),
        IsTriggeredOnEventHandler = transition.IsTriggeredOnEventHandler,
        ValidateNewAction = transition.ValidateNewAction
      };
    }

    internal BoundedTo<TGraph, TPrimary>.Transition To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.Transition()
      {
        IsActive = this.IsActive,
        Name = this.Name,
        SourceIdentifier = this.SourceIdentifier,
        TargetIdentifier = this.TargetIdentifier,
        Action = this.Action,
        Condition = this.Condition?.To<TGraph, TPrimary>(),
        DisablePersist = this.DisablePersist,
        FieldAssignments = this.FieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>(),
        IsTriggeredOnEventHandler = this.IsTriggeredOnEventHandler,
        ValidateNewAction = this.ValidateNewAction
      };
    }

    public void CopyTo(AUWorkflowTransition row)
    {
      row.FromStateName = this.SourceIdentifier;
      row.TargetStateName = this.TargetIdentifier;
      row.DisplayName = this.Name;
      Readonly.Condition condition = this.Condition;
      if ((condition != null ? (condition.Id.HasValue ? 1 : 0) : 0) != 0)
        row.ConditionID = this.Condition.Id;
      row.ActionName = this.Action;
      row.TransitionID = PXSystemWorkflows.GuidFromString(this.Name);
      row.DisablePersist = new bool?(this.DisablePersist);
      row.IsActive = new bool?(true);
      row.IsSystem = new bool?(true);
      row.TriggeredBy = new int?(this.IsTriggeredOnEventHandler ? 2 : 1);
    }

    public void Publish(PXSystemWorkflowContainer dest, AUWorkflowTransition parent)
    {
      int num = 0;
      foreach (Readonly.Assignment fieldAssignment in (IEnumerable<Readonly.Assignment>) this.FieldAssignments)
      {
        AUWorkflowTransitionField row1 = new AUWorkflowTransitionField()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          TransitionID = parent.TransitionID,
          TransitionFieldLineNbr = new int?(num++)
        };
        AUWorkflowTransitionField row2 = row1;
        fieldAssignment.CopyTo(row2);
        dest.Insert<AUWorkflowTransitionField>(row1);
      }
    }
  }

  public class Workflow
  {
    public const string DEFAULT_WORKFLOW = "DEFAULT";

    internal Workflow()
    {
    }

    public bool IsActive { get; private set; }

    public string FlowID { get; private set; }

    public string FlowSubID { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<Readonly.BaseFlowStep> States { get; private set; }

    public IReadOnlyCollection<Readonly.Transition> Transitions { get; private set; }

    public IReadOnlyCollection<Readonly.Sequence> Sequences { get; private set; }

    internal static Readonly.Workflow From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.Workflow flow)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.Workflow()
      {
        IsActive = flow.IsActive,
        FlowID = flow.FlowID,
        FlowSubID = flow.FlowSubID,
        Description = flow.Description,
        States = (IReadOnlyCollection<Readonly.BaseFlowStep>) flow.States.Select<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, Readonly.BaseFlowStep>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, Readonly.BaseFlowStep>) (f => f.GetResult().AsReadonly())).ToArray<Readonly.BaseFlowStep>(),
        Transitions = (IReadOnlyCollection<Readonly.Transition>) flow.Transitions.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>().Select<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, Readonly.Transition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, Readonly.Transition>) (f => f.Result.AsReadonly())).ToArray<Readonly.Transition>()
      };
    }

    internal BoundedTo<TGraph, TPrimary>.Workflow To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.Workflow()
      {
        IsActive = this.IsActive,
        FlowID = this.FlowID,
        FlowSubID = this.FlowSubID,
        Description = this.Description,
        States = this.States.Select<Readonly.BaseFlowStep, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<Readonly.BaseFlowStep, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>) (f => f.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>(),
        Transitions = this.Transitions.Select<Readonly.Transition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<Readonly.Transition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>) (f => f.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>()
      };
    }

    public void CopyTo(AUWorkflow dest)
    {
      dest.Description = this.Description ?? (string.IsNullOrEmpty(this.FlowID) ? "Default workflow" : this.FlowID + " workflow");
      dest.WorkflowID = this.FlowID;
      dest.WorkflowSubID = this.FlowSubID;
      dest.WorkflowGUID = WorkflowHelper.GetWorkflowPseudoGuid(this.FlowID, this.FlowSubID);
      dest.IsActive = new bool?(true);
      dest.IsSystem = new bool?(true);
    }

    public void Publish(PXSystemWorkflowContainer dest, AUWorkflow parent)
    {
      int num1 = 0;
      if (this.States.Count > 0)
      {
        if (this.States.OfType<Readonly.FlowState>().Count<Readonly.FlowState>((Func<Readonly.FlowState, bool>) (state => state.IsInitial)) == 0)
          throw new ArgumentException($"Workflow {parent.WorkflowID} must have initial state");
        if (this.States.OfType<Readonly.FlowState>().Count<Readonly.FlowState>((Func<Readonly.FlowState, bool>) (state => state.IsInitial)) > 1)
          throw new ArgumentException($"Workflow {parent.WorkflowID} must have only one initial state");
      }
      foreach (Readonly.BaseFlowStep state in (IEnumerable<Readonly.BaseFlowStep>) this.States)
      {
        AUWorkflowState auWorkflowState = new AUWorkflowState()
        {
          ScreenID = parent.ScreenID,
          WorkflowGUID = parent.WorkflowGUID,
          StateLineNbr = new int?(num1++)
        };
        state.CopyTo(auWorkflowState);
        dest.Insert<AUWorkflowState>(auWorkflowState);
        state.Publish(dest, auWorkflowState);
      }
      foreach (IGrouping<string, Readonly.Transition> source1 in this.Transitions.GroupBy<Readonly.Transition, string>((Func<Readonly.Transition, string>) (it => it.SourceIdentifier)))
      {
        int num2 = 0;
        foreach (IGrouping<string, Readonly.Transition> source2 in source1.GroupBy<Readonly.Transition, string>((Func<Readonly.Transition, string>) (it => it.Action)))
        {
          if (source2.Count<Readonly.Transition>((Func<Readonly.Transition, bool>) (it =>
          {
            if (it.Condition == null)
              return true;
            bool? constant = it.Condition.Constant;
            bool flag = true;
            return constant.GetValueOrDefault() == flag & constant.HasValue;
          })) > 1)
            throw new ArgumentException($"There are more than one unconditional transition in workflow {parent.WorkflowID} in state {source1.Key} this is triggered by {source2.Key}.");
        }
        foreach (Readonly.Transition transition1 in (IEnumerable<Readonly.Transition>) source1)
        {
          Readonly.Transition transition = transition1;
          if (transition.ValidateNewAction)
          {
            HashSet<IBqlTable> uniqueItems = dest.UniqueItems;
            AUScreenNavigationActionState navigationActionState = new AUScreenNavigationActionState();
            navigationActionState.ScreenID = parent.ScreenID;
            navigationActionState.ActionName = transition.Action;
            if (!uniqueItems.Contains((IBqlTable) navigationActionState))
              throw new ArgumentException($"Transition {transition.Name} in workflow {parent.WorkflowID} is triggered by navigation action {transition.Action} that does not exists.");
          }
          if (transition.IsTriggeredOnEventHandler)
          {
            if (transition.Action != "@OnSequenceLeaving")
            {
              if (!dest.UniqueItems.Contains((IBqlTable) new AUWorkflowHandler()
              {
                ScreenID = parent.ScreenID,
                HandlerName = transition.Action
              }))
                throw new ArgumentException($"Transition {transition.Name} in workflow {parent.WorkflowID} is triggered by event handler {transition.Action} that is not exists.");
            }
          }
          else
          {
            List<string> source3 = new List<string>()
            {
              transition.SourceIdentifier
            };
            for (AUWorkflowState state = dest.GetItemsForScreen<AUWorkflowState>(parent.ScreenID).FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.WorkflowGUID == parent.WorkflowGUID && it.Identifier == transition.SourceIdentifier)); state?.ParentState != null; state = dest.GetItemsForScreen<AUWorkflowState>(parent.ScreenID).FirstOrDefault<AUWorkflowState>((Func<AUWorkflowState, bool>) (it => it.WorkflowGUID == parent.WorkflowGUID && it.Identifier == state.ParentState)))
              source3.Add(state.ParentState);
            if (!source3.Any<string>((Func<string, bool>) (s => dest.UniqueItems.Contains((IBqlTable) new AUWorkflowStateAction()
            {
              ScreenID = parent.ScreenID,
              WorkflowGUID = parent.WorkflowGUID,
              ActionName = transition.Action,
              StateName = s
            }))))
              throw new ArgumentException($"Transition {transition.Name} in workflow {parent.WorkflowID} is triggered by action {transition.Action} that does not added to {transition.SourceIdentifier} state.");
          }
          if (!dest.UniqueItems.Contains((IBqlTable) new AUWorkflowState()
          {
            ScreenID = parent.ScreenID,
            WorkflowGUID = parent.WorkflowGUID,
            Identifier = transition.SourceIdentifier
          }))
            throw new ArgumentException($"Transition {transition.Name} in workflow {parent.WorkflowID} triggered by action {transition.Action} starts from state {transition.SourceIdentifier} that does not added to workflow.");
          if (transition.TargetIdentifier != "@N" && transition.TargetIdentifier != "@P")
          {
            if (!dest.UniqueItems.Contains((IBqlTable) new AUWorkflowState()
            {
              ScreenID = parent.ScreenID,
              WorkflowGUID = parent.WorkflowGUID,
              Identifier = transition.TargetIdentifier
            }))
              throw new ArgumentException($"Transition {transition.Name} in workflow {parent.WorkflowID} triggered by action {transition.Action} targets to state {transition.TargetIdentifier} that does not added to workflow.");
          }
          AUWorkflowTransition workflowTransition = new AUWorkflowTransition()
          {
            WorkflowGUID = parent.WorkflowGUID,
            ScreenID = parent.ScreenID,
            TransitionLineNbr = new int?(num2++)
          };
          transition.CopyTo(workflowTransition);
          dest.Insert<AUWorkflowTransition>(workflowTransition);
          transition.Publish(dest, workflowTransition);
        }
      }
    }
  }

  public class WorkflowEventHandler
  {
    public string HandlerName { get; private set; }

    internal static Readonly.WorkflowEventHandler From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandler eventHandler)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.WorkflowEventHandler()
      {
        HandlerName = eventHandler.HandlerName
      };
    }

    internal BoundedTo<TGraph, TPrimary>.WorkflowEventHandler To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new BoundedTo<TGraph, TPrimary>.WorkflowEventHandler()
      {
        HandlerName = this.HandlerName
      };
    }

    public void CopyTo(AUWorkflowStateEventHandler dest)
    {
      dest.HandlerName = this.HandlerName;
      dest.IsActive = new bool?(true);
      dest.IsSystem = new bool?(true);
    }
  }

  public class WorkflowEventHandlerDefinition
  {
    public string Name { get; set; }

    public string DataMember { get; set; }

    public string DisplayName { get; set; }

    public bool CreateNewAction { get; set; }

    public string SourceType { get; set; }

    public string EventContainerName { get; set; }

    public string EventName { get; set; }

    public Readonly.Condition Condition { get; set; }

    public System.Type Select { get; set; }

    public bool AllowMultipleSelect { get; set; }

    public bool UseTargetAsPrimarySource { get; set; }

    public bool UseParameterAsPrimarySource { get; set; }

    public bool SelectIsCommand { get; set; }

    public System.Type UpcastType { get; set; }

    public IReadOnlyCollection<Readonly.Assignment> FieldAssignments { get; private set; }

    internal static Readonly.WorkflowEventHandlerDefinition From<TGraph, TPrimary>(
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition action)
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      return new Readonly.WorkflowEventHandlerDefinition()
      {
        Name = action.Name,
        CreateNewAction = action.CreateNewAction,
        DataMember = action.GetDataType().FullName,
        FieldAssignments = (IReadOnlyCollection<Readonly.Assignment>) action.FieldAssignments.GetSortedWorkflowElements<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>().Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, Readonly.Assignment>) (fa => fa.Result.AsReadonly())).ToArray<Readonly.Assignment>(),
        EventName = action.EventName,
        EventContainerName = action.EventContainerName,
        SourceType = action.SourceType.FullName,
        Condition = action.Condition?.AsReadonly(),
        Select = action.Select,
        AllowMultipleSelect = action.AllowMultipleSelect,
        UseParameterAsPrimarySource = action.UseParameterAsPrimarySource,
        UseTargetAsPrimarySource = action.UseTargetAsPrimarySource,
        DisplayName = action.DisplayName,
        UpcastType = action.UpcastType
      };
    }

    internal BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition To<TGraph, TPrimary>()
      where TGraph : PXGraph
      where TPrimary : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition handlerDefinition = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition();
      handlerDefinition.Name = this.Name;
      handlerDefinition.CreateNewAction = this.CreateNewAction;
      handlerDefinition.FieldAssignments = this.FieldAssignments.Select<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<Readonly.Assignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (fa => fa.To<TGraph, TPrimary>().ToConfigurator())).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      handlerDefinition.EventName = this.EventName;
      handlerDefinition.EventContainerName = this.EventContainerName;
      handlerDefinition.SourceType = PXBuildManager.GetType(this.SourceType, false);
      handlerDefinition.Condition = this.Condition?.To<TGraph, TPrimary>();
      handlerDefinition.Select = this.Select;
      handlerDefinition.AllowMultipleSelect = this.AllowMultipleSelect;
      handlerDefinition.UseParameterAsPrimarySource = this.UseParameterAsPrimarySource;
      handlerDefinition.UseTargetAsPrimarySource = this.UseTargetAsPrimarySource;
      handlerDefinition.DisplayName = this.DisplayName;
      handlerDefinition.UpcastType = this.UpcastType;
      return handlerDefinition;
    }

    public void CopyTo(AUWorkflowHandler row)
    {
      row.HandlerName = this.Name;
      row.EventName = this.EventName;
      row.EventContainerName = this.EventContainerName;
      row.IsActive = new bool?(true);
      row.Condition = this.Condition?.Name;
      row.SelectType = this.Select?.FullName;
      row.AllowMultipleSelect = new bool?(this.AllowMultipleSelect);
      row.UseParameterAsPrimarySource = new bool?(this.UseParameterAsPrimarySource);
      row.UseTargetAsPrimarySource = new bool?(this.UseTargetAsPrimarySource);
      row.DisplayName = this.DisplayName;
      row.UpcastType = this.UpcastType?.FullName;
      row.IsSystem = new bool?(true);
    }

    public void Publish(PXSystemWorkflowContainer dest, AUWorkflowHandler parent)
    {
      int num = 0;
      foreach (Readonly.Assignment fieldAssignment in (IEnumerable<Readonly.Assignment>) this.FieldAssignments)
      {
        AUWorkflowHandlerUpdateField row1 = new AUWorkflowHandlerUpdateField()
        {
          ScreenID = parent.ScreenID,
          HandlerName = parent.HandlerName,
          HandlerFieldLineNbr = new int?(num++)
        };
        AUWorkflowHandlerUpdateField row2 = row1;
        fieldAssignment.CopyTo(row2);
        dest.Insert<AUWorkflowHandlerUpdateField>(row1);
      }
    }
  }
}
