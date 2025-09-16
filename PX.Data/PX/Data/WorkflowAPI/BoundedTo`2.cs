// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.BoundedTo`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.ProjectDefinition.Workflow;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Logging.Sinks.SystemEventsDbSink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable enable
namespace PX.Data.WorkflowAPI;

/// <summary>
/// Provides classes and interfaces for workflow configuration.
/// </summary>
/// <typeparam name="TGraph">The type of the graph.</typeparam>
/// <typeparam name="TPrimary">The primary DAC.</typeparam>
public static class BoundedTo<TGraph, TPrimary>
  where TGraph : 
  #nullable disable
  PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public class ActionCategory
  {
    public string CategoryName { get; set; }

    public string DisplayName { get; set; }

    public Placement Placement { get; set; }

    public string After { get; set; }

    internal Readonly.ActionCategory AsReadonly()
    {
      return Readonly.ActionCategory.From<TGraph, TPrimary>(this);
    }

    public class ActionCategoryBuilder
    {
      private readonly WorkflowContext<TGraph, TPrimary> _context;

      public ActionCategoryBuilder(WorkflowContext<TGraph, TPrimary> context)
      {
        this._context = context;
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured CreateNew(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured> initializer = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory configuratorCategory = new BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory(actionName);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) configuratorCategory);
        }
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) configuratorCategory;
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Get(string categoryName)
      {
        BoundedTo<TGraph, TPrimary>.ActionCategory category = this._context.Configurator.Result.Categories.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionCategory>((Func<BoundedTo<TGraph, TPrimary>.ActionCategory, bool>) (it => it.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase)));
        return category == null ? (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) null : (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) new BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory(category);
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Get(FolderType folderType)
      {
        return this.Get(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(folderType));
      }
    }

    /// <summary>Provides configuration for the action category.</summary>
    public interface IAllowOptionalConfigCategory : 
      BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured
    {
      /// <summary>Sets a display name for the action category.</summary>
      /// <param name="displayName">A display name</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory DisplayName(
        string displayName);

      /// <summary>
      /// Places the action category after the specified action category.
      /// </summary>
      /// <param name="categoryName">A name of the action category after which the current category should be placed</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory PlaceAfter(
        string categoryName);

      /// <summary>
      /// Places the action category after the specified action category.
      /// </summary>
      /// <param name="folderType">A folder type of the category after which the current category should be placed</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory PlaceAfter(
        FolderType folderType);

      /// <summary>
      /// Places the action category after the specified action category.
      /// </summary>
      /// <param name="category">An action category after which the current category should be placed</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category);

      /// <summary>
      /// Places the action category in the specified location of the list of categories.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.First" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Last" />,
      /// the category is placed first or last, respectively, and the <paramref name="relativeCategoryName" /> parameter is ignored and can be <see langword="null" />.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.After" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Before" /> and the <paramref name="relativeCategoryName" /> parameter is specified,
      /// the category is placed after or before the category specified by <paramref name="relativeCategoryName" />, respectively.
      /// </summary>
      /// <param name="placement">A location of the category in the list of categories.</param>
      /// <param name="relativeCategoryName">A name of the action category relative to which the current category should be placed.</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory Place(
        Placement placement,
        string relativeCategoryName = null);

      /// <summary>
      /// Places the action category in the specified location of the list of categories.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.First" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Last" />,
      /// the category is placed first or last, respectively, and the <paramref name="relativeFolderType" /> parameter is ignored and can be <see langword="null" />.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.After" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Before" /> and the <paramref name="relativeFolderType" /> parameter is specified,
      /// the category is placed after or before the category specified by <paramref name="relativeFolderType" />, respectively.
      /// </summary>
      /// <param name="placement">A location of the category in the list of categories.</param>
      /// <param name="relativeFolderType">A folder type of the category relative to which the current category should be placed.</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory Place(
        Placement placement,
        FolderType relativeFolderType);

      /// <summary>
      /// Places the action category in the specified location of the list of categories.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.First" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Last" />,
      /// the category is placed first or last, respectively, and the <paramref name="relativeCategory" /> parameter is ignored and can be <see langword="null" />.
      /// If <paramref name="placement" /> equals <see cref="F:PX.Data.WorkflowAPI.Placement.After" /> or <see cref="F:PX.Data.WorkflowAPI.Placement.Before" /> and the <paramref name="relativeCategory" /> parameter is specified,
      /// the category is placed after or before the category specified by <paramref name="relativeCategory" />, respectively.
      /// </summary>
      /// <param name="placement">A location of the category in the list of categories.</param>
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory Place(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured relativeCategory);
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorCategory : 
      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory,
      BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.ActionCategory Result { get; }

      internal ConfiguratorCategory(string categoryName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.ActionCategory()
        {
          CategoryName = categoryName
        };
      }

      internal ConfiguratorCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory category)
      {
        this.Result = category;
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory WithDisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory PlaceAfter(
        string categoryName)
      {
        return this.Place(Placement.After, categoryName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory PlaceAfter(
        FolderType folderType)
      {
        return this.Place(Placement.After, BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(folderType));
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category)
      {
        return this.Place(Placement.After, ((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category).Result.CategoryName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory Place(
        Placement placement,
        string relativeCategoryName = null)
      {
        this.Result.Placement = placement;
        this.Result.After = relativeCategoryName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory Place(
        Placement placement,
        FolderType relativeFolderType)
      {
        return this.Place(placement, BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(relativeFolderType));
      }

      public BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory Place(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured relativeCategory)
      {
        return this.Place(placement, ((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) relativeCategory).Result.CategoryName);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.WithDisplayName(displayName);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.PlaceAfter(
        string categoryName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.PlaceAfter(categoryName);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.PlaceAfter(
        FolderType folderType)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.PlaceAfter(folderType);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.PlaceAfter(category);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.Place(
        Placement placement,
        string relativeCategoryName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.Place(placement, relativeCategoryName);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.Place(
        Placement placement,
        FolderType relativeFolderType)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.Place(placement, relativeFolderType);
      }

      BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory.Place(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured relativeCategory)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory) this.Place(placement, relativeCategory);
      }
    }

    /// <summary>Adds and updates categories.</summary>
    public interface IContainerFillerCategories
    {
      /// <summary>Adds a configured category object to the screen configuration.</summary>
      /// <param name="action">The configured category object.</param>
      void Add(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured action);

      void Update(
        FolderType folderType,
        Func<BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory> config);

      void Update(
        string categoryName,
        Func<BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory> config);
    }

    public class ContainerAdjusterCategories : 
      BoundedTo<TGraph, TPrimary>.ActionCategory.IContainerFillerCategories
    {
      internal List<BoundedTo<TGraph, TPrimary>.ActionCategory> Result { get; }

      internal ContainerAdjusterCategories(
        List<BoundedTo<TGraph, TPrimary>.ActionCategory> actions)
      {
        this.Result = actions;
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category)
      {
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionCategory>((Func<BoundedTo<TGraph, TPrimary>.ActionCategory, bool>) (it => string.Equals(it.CategoryName, ((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category).Result.CategoryName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Category Definition {((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category).Result.CategoryName} already exists.");
        this.Result.Add(((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category).Result);
      }

      public void Update(
        string categoryName,
        Func<BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory> config)
      {
        BoundedTo<TGraph, TPrimary>.ActionCategory category = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionCategory>((Func<BoundedTo<TGraph, TPrimary>.ActionCategory, bool>) (it => string.Equals(it.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase)));
        BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory configuratorCategory1 = !WebConfig.EnableWorkflowValidationOnStartup || category != null ? new BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory(category) : throw new InvalidOperationException($"Category Definition {categoryName} not found.");
        BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory configuratorCategory2 = config(configuratorCategory1);
      }

      public void Update(
        FolderType folderType,
        Func<BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory> config)
      {
        this.Update(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(folderType), config);
      }
    }
  }

  public class ActionDefinition : BoundedTo<TGraph, TPrimary>.ActionDefinitionBase
  {
    public string DisplayName { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition DisableCondition { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition HideCondition { get; set; }

    public string After { get; set; }

    public Placement PlacementInCategory { get; set; }

    public string AfterInMenu { get; set; }

    public string Form { get; set; }

    public string MassProcessingScreen { get; set; }

    public BoundedTo<TGraph, TPrimary>.NavigationDefinition Navigation { get; set; }

    internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> ParameterAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    public bool BatchMode { get; set; }

    public FolderType? ParentFolder { get; set; }

    public bool? DisablePersist { get; set; }

    public PXCacheRights? MapEnableRights { get; set; }

    public PXCacheRights? MapViewRights { get; set; }

    public string Category { get; set; }

    public bool? ExposeToMobile { get; set; } = new bool?(false);

    public bool? DisplayOnMainToolbar { get; internal set; }

    public bool? IsLockedOnToolbar { get; internal set; }

    public bool? IgnoresArchiveDisabling { get; internal set; }

    internal Readonly.ActionDefinition AsReadonly()
    {
      return Readonly.ActionDefinition.From<TGraph, TPrimary>(this);
    }

    internal static string GetCategoryPerFolderType(FolderType folderType)
    {
      switch (folderType)
      {
        case FolderType.ActionsFolder:
          return "Action";
        case FolderType.InquiriesFolder:
          return "Inquiry";
        case FolderType.ReportsFolder:
          return "Report";
        case FolderType.ToolbarFolder:
          return (string) null;
        default:
          throw new ArgumentOutOfRangeException(nameof (folderType), (object) folderType, (string) null);
      }
    }

    internal static string GetCategoryForPredefinedCategory(PredefinedCategory category)
    {
      switch (category)
      {
        case PredefinedCategory.Actions:
          return "Action";
        case PredefinedCategory.Inquiries:
          return "Inquiry";
        case PredefinedCategory.Reports:
          return "Report";
        default:
          throw new ArgumentOutOfRangeException(nameof (category), (object) category, (string) null);
      }
    }

    /// <summary>Creates and obtains action definitions.</summary>
    public class ActionDefinitionBuilder
    {
      private readonly WorkflowContext<TGraph, TPrimary> _context;

      public ActionDefinitionBuilder(WorkflowContext<TGraph, TPrimary> context)
      {
        this._context = context;
      }

      /// <summary>Creates a new navigation action or workflow action object.</summary>
      /// <param name="actionName">Internal name of an action which will be used to identify the action in the Graph.Actions collection.</param>
      /// <param name="initializer">Function that is used to configure the new action object.</param>
      /// <returns>Configured action object that can be used in a workflow and added to a graph as an action.</returns>
      /// <example><para>Suppose you need to define the new Assign action which updates the CROpportunity.Resolution field, is located in the Action folder, and is displayed on a mass processing form for updating opportunities (the Udpate Opportunities (CR503120) form). The code for such action looks as shown below.</para>
      ///   <code title="Example" lang="CS">
      /// var actionAssign = context.ActionDefinitions.CreateNew("Assign", action =&gt; action
      ///     .WithFieldAssignments(fields =&gt;
      ///     {
      ///         fields.Add&lt;CROpportunity.resolution&gt;(f =&gt; f.SetFromValue("Assigned"));
      ///     })
      ///     .DisplayName("Assign")
      ///     .InFolder(FolderType.ActionsFolder, "Lost")
      ///     .MassProcessingScreen&lt;UpdateOpportunityMassProcess&gt;());</code>
      /// </example>
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured CreateNew(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(actionName);
        configuratorAction.Result.CreateNewAction = true;
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured extendedConfigured = initializer((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) configuratorAction);
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured) configuratorAction;
      }

      /// <summary>Wraps an existing graph action with an action definition object addressing this object by the specified internal name.</summary>
      /// <param name="actionName">The internal name for an existing graph action.</param>
      /// <param name="initializer">Function that is used to configure behaviour of the existing action.</param>
      /// <returns>Configured action object that can be used in a workflow.</returns>
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured CreateExisting(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction existing = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(actionName);
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) existing);
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured) existing;
      }

      /// <summary>Wraps an existing graph action with an action definition object addressing this object by the PXAction graph member.</summary>
      /// <param name="actionSelector">Expression that is used to address the PXAction graph member.</param>
      /// <param name="initializer">Function that is used to configure behaviour of the existing action.</param>
      /// <returns>Configured action object that can be used in a workflow.</returns>
      /// <example><para>Suppose you want to wrap the Release action on the Cases (CR306000) form. The code looks as shown below. First you get the context of the screen. Then you call the CreateExisting method.</para>
      ///   <code title="Example" lang="CS">
      /// var context = config.GetScreenConfigurationContext&lt;CRCaseMaint, CRCase&gt;();
      /// 
      /// var gactionRelease = context.ActionDefinitions.CreateExisting(g =&gt; g.release, a =&gt; a
      ///                     .InFolder(FolderType.ActionsFolder)
      ///                     .MassProcessingScreen&lt;CRCaseReleaseProcess&gt;());</code>
      /// </example>
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured CreateExisting(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction existing = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(((MemberExpression) actionSelector.Body).Member.Name);
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) existing);
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured) existing;
      }

      /// <summary>Wraps an existing graph action with an action definition object addressing this object by the PXAction member of a graph extension.</summary>
      /// <typeparam name="TExtension">Type of the graph extension where the action is declared.</typeparam>
      /// <param name="actionSelector">Expression that is used to address the PXAction member of a graph extension.</param>
      /// <param name="initializer">Function that is used to configure behaviour of the existing action.</param>
      /// <returns>Configured action object that can be used in a workflow.</returns>
      /// <example><para>Suppose, on the Leads (CR301000) form, you need to wrap the ConvertToOpportunityAll action which is declared in the CreateOpportunity graph extension. The code looks as shown below. First, you get the context of the screen. Then you call the CreateExisting method.</para>
      ///   <code title="Example" lang="CS">
      /// var context = configuration.GetScreenConfigurationContext&lt;LeadMaint, CRLead&gt;();
      /// 
      /// var actionConvertToOpportunityAll = context.ActionDefinitions.CreateExisting&lt;CreateOpportunityExt&gt;(
      ///     e =&gt; e.ConvertToOpportunityAll, a =&gt; a
      ///         .WithFieldAssignments(fields =&gt;
      ///         {
      ///             fields.Add&lt;CRLead.resolution&gt;(f =&gt; f.SetFromValue(reasons[States.Converted].@default));
      ///             fields.Add&lt;CRLead.isActive&gt;(f =&gt; f.SetFromValue(false));
      ///         }).InFolder(FolderType.ActionsFolder));</code>
      /// </example>
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured CreateExisting<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> initializer)
        where TExtension : PXGraphExtension<TGraph>
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction existing = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(((MemberExpression) actionSelector.Body).Member.Name);
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) existing);
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured) existing;
      }

      /// <summary>Gets the configured action object by the internal name of the action from the screen configuration.</summary>
      /// <param name="actionName">Internal name of the action.</param>
      /// <returns>Configured action object.</returns>
      /// <example><para>Suppose you need to get an object for the action named Assign in the OpportunityMaint graph. The code looks as shown below. First you get the context of the screen. Then you get the action object.</para>
      ///   <code title="Example" lang="CS">
      /// var context = config.GetScreenConfigurationContext&lt;OpportunityMaint, CROpportunity&gt;();
      /// var actionAssign = context.ActionDefinitions.Get("Assign");</code>
      /// </example>
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured Get(string actionName)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition action = this._context.Configurator.Result.Actions.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => it.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase)));
        return action == null ? (BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured) null : (BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured) new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(action);
      }
    }

    /// <summary>
    /// Provides configuration options for an action, such as the display name and category.
    /// </summary>
    public interface IAllowOptionalConfigAction : 
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured
    {
      /// <summary>Sets the display name of an action.</summary>
      /// <param name="displayName">Display name of an action</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction DisplayName(
        string displayName);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        string previousActionName);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionNameSelector);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        string previousActionName);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionNameSelector);

      [Obsolete("Use WithCategory method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        string previousActionName);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        string relativeActionName = null);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        string previousActionName);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        string relativeActionName = null);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfterInCategory(
        string previousActionName);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfterInCategory(
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfterInCategory(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceInCategory(
        Placement placement,
        string relativeActionName = null);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceInCategory(
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeAActionSelector);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceInCategory(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAAction);

      /// <summary>Places the current action in the Graph.Actions collection after <paramref name="actionName" /> action.</summary>
      /// <param name="actionName">The internal name of the action in the Graph.Actions collection that should precede the current action.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfter(
        string actionName);

      /// <summary>Places the current action in the Graph.Actions collection after <paramref name="actionSelector" /> action.</summary>
      /// <param name="actionSelector">Expression to get the action in the Graph.Actions collection which should precede the current action.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfter(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector);

      /// <summary>
      /// Places the current action in the Graph.Actions collection after the <paramref name="action" /> action.</summary>
      /// <param name="action">Definition of an action that should precede the current action.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action);

      /// <summary>Sets a dialog box that will be shown to a user before the action is executed.</summary>
      /// <param name="form">Dialog box object.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithForm(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured form);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithForm<TForm>() where TForm : class, IWorkflowFormBqlTable, new();

      /// <summary>Specified the mass processing form where the current action will be available for processing multiple records.</summary>
      /// <param name="screenId">The ID of the mass processing form.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig MassProcessingScreen(
        string screenId);

      /// <summary>Specified the mass processing form where the current action will be available for processing multiple records.</summary>
      /// <param name="screenId"></param>
      /// <typeparam name="T">Graph name of the mass processing form.</typeparam>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig MassProcessingScreen<T>() where T : PXGraph;

      /// <summary>Specifies that the current action is a redirection action that creates a new record.</summary>
      /// <param name="createAction">Settings for a form which the action opens. The settings contain specifications on creating a new record.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsCreateRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate createAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsCreateRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate> createAction);

      /// <summary>Specified that the current action opens a form where a user can search for specific records.</summary>
      /// <param name="createAction">Allows to set screen settings for record creation.</param>
      /// <param name="searchAction">Predefined action which opens the form to search records.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsSearchRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch searchAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsSearchRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch> searchAction);

      /// <summary>Specified that the current action runs a report.</summary>
      /// <param name="createAction">Allows to set screen settings for record creation.</param>
      /// <param name="runReportAction">Predefined action which runs a report.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsRunReportScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport runReportAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsRunReportScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport> runReportAction);

      /// <summary>
      /// Specify that the current navigation action is a side panel action.
      /// </summary>
      /// <param name="sidePanelAction">Side panel configuration.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsSidePanelScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel sidePanelAction);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsSidePanelScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel> sidePanelAction);

      /// <summary>Specified that the current action must be hidden on the form when the provided condition is true.</summary>
      /// <param name="condition">Function which defines the condition.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>Specified that the current action must be hidden on the form when the provided condition is true.</summary>
      /// <param name="condition">Preconfigured condition.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Specifies that current action must always be hidden on the form. Use this method for auto-run actions.</summary>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsHiddenAlways();

      /// <summary>Specifies that current action must be disabled on the form when the provided condition is true.</summary>
      /// <param name="condition">Function which defines the condition.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>Specifies that current action must be disabled on the form when the provided condition is true.</summary>
      /// <param name="condition">Preconfigured condition.</param>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Specifies that the current action must always be disabled on the form.</summary>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsDisabledAlways();

      /// <summary>Specifies that the current action stays enabled even when the document is archived.</summary>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IgnoresArchiveDisabling(
        bool value);

      /// <summary>Sets an ordered list of primary DAC fields that will be updated before the action method is executed.</summary>
      /// <param name="containerFiller">Definition of fields to be updated.</param>
      /// <remarks>The method is applicable only to graph actions because only such actions have a method to execute.</remarks>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller);

      /// <summary>Sets an ordered list of parameters that will be used to execute the action method.</summary>
      /// <param name="containerFiller">Definition of parameters for the action.</param>
      /// <remarks>The method is applicable only to graph actions because only such actions have a method to execute.</remarks>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithParameterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerParameters> containerFiller);

      [Obsolete("Use WithPersistOptions method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction DoesNotPersist();

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithPersistOptions(
        ActionPersistOptions persistOptions);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithEnableRightsMapping(
        PXCacheRights mapping);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction MapEnableToSelect();

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction MapEnableToUpdate();

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithVisibleRightsMapping(
        PXCacheRights mapping);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction MapVisibleToSelect();

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction MapVisibleToUpdate();

      [Obsolete("This method will be removed in 2022r1. Use overloads instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithCategory(
        string category);

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction IsExposedToMobile(
        bool exposed);

      /// <summary>
      /// Specifies how current action is displayed on main toolbar
      /// </summary>
      /// <param name="displayOnToolbar"></param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction WithDisplayOnToolbar(
        DisplayOnToolBar displayOnToolbar);
    }

    /// <summary>
    /// Provides additional configuration for mass processing actions.
    /// </summary>
    public interface IMassProcessingOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured
    {
      /// <summary>
      /// Specifies that all affected records should be processed in a single execution of a mass processing action in a workflow.
      /// </summary>
      /// <remarks>
      /// A workflow action can be used for mass processing any number of records simultaneously. However the action is invoked for each record separately unless
      /// the <see cref="M:PX.Data.WorkflowAPI.BoundedTo`2.ActionDefinition.IMassProcessingOptionalConfig.InBatchMode" /> method is called. Please note that this method will only work as intended if the action being invoked utilizes all the records
      /// made available by its <see cref="T:PX.Data.PXAdapter" /> parameter in order to support batch mass processing.
      /// </remarks>
      /// <example>
      ///  <para>The code below shows how to call this method for a workflow action that is used for mass processing.</para>
      ///  <code title="Example" lang="CS">
      ///  actions.Add(g =&gt; g.Assign,
      ///  c =&gt; c.WithCategory(processingCategory, g =&gt; g.PutOnHold)
      /// .MassProcessingScreen&lt;RSSVAssignProcess&gt;()
      /// .InBatchMode());
      ///  </code>
      ///  </example>
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InBatchMode();
    }

    public interface IConfigured
    {
    }

    public interface IExtendedConfigured : BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured
    {
    }

    public class ConfiguratorAction : 
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured,
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.ActionDefinition Result { get; }

      internal ConfiguratorAction(string actionName)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition actionDefinition = new BoundedTo<TGraph, TPrimary>.ActionDefinition();
        actionDefinition.Name = actionName;
        this.Result = actionDefinition;
      }

      internal ConfiguratorAction(
        BoundedTo<TGraph, TPrimary>.ActionDefinition action)
      {
        this.Result = action;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithDisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder)
      {
        this.Result.ParentFolder = new FolderType?(parentFolder);
        this.Result.Category = BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(parentFolder);
        this.Result.DisplayOnMainToolbar = parentFolder != FolderType.ToolbarFolder ? new bool?() : new bool?(true);
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        string previousActionName)
      {
        this.Result.ParentFolder = new FolderType?(parentFolder);
        this.Result.Category = BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(parentFolder);
        if (parentFolder != FolderType.ToolbarFolder)
        {
          this.Result.PlacementInCategory = Placement.After;
          this.Result.AfterInMenu = previousActionName;
        }
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionNameSelector)
      {
        this.Result.ParentFolder = new FolderType?(parentFolder);
        this.Result.Category = BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(parentFolder);
        if (parentFolder != FolderType.ToolbarFolder)
        {
          this.Result.PlacementInCategory = Placement.After;
          this.Result.AfterInMenu = ((MemberExpression) previousActionNameSelector.Body).Member.Name;
        }
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        FolderType parentFolder,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        this.Result.ParentFolder = new FolderType?(parentFolder);
        this.Result.Category = BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryPerFolderType(parentFolder);
        if (parentFolder != FolderType.ToolbarFolder)
        {
          this.Result.PlacementInCategory = Placement.After;
          this.Result.AfterInMenu = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) previousAction).Result.Name;
        }
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory)
      {
        this.Result.ParentFolder = new FolderType?();
        this.Result.Category = ((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) parentCategory).Result.CategoryName;
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this;
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        string previousActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) parentCategory, relativeActionName: previousActionName);
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionNameSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) parentCategory, relativeActionName: ((MemberExpression) previousActionNameSelector.Body).Member.Name);
      }

      [Obsolete("Use WithCategory method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction InFolder(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured parentCategory,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) parentCategory, relativeActionName: ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) previousAction).Result.Name);
      }

      private BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategoryImpl(
        string category,
        Placement placement = Placement.After,
        string relativeActionName = null)
      {
        this.Result.ParentFolder = new FolderType?();
        this.Result.Category = category;
        this.Result.PlacementInCategory = placement;
        this.Result.AfterInMenu = relativeActionName;
        return this;
      }

      private BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategoryImpl(
        BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory category,
        Placement placement = Placement.After,
        string relativeActionName = null)
      {
        if (category != null)
          return this.WithCategoryImpl(category.Result.CategoryName, placement, relativeActionName);
        PXTrace.Logger.ForSystemEvents("System", "System_WorkflowActionCategoryIsNullEventId").ForCurrentCompanyContext().Error<string>("The {ActionName} action will be added to the Other category of the More menu because no category has been specified in the WithCategory method.", this.Result.Name);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category));
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        string previousActionName)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), relativeActionName: previousActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), relativeActionName: ((MemberExpression) previousActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), relativeActionName: ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) previousAction).Result.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        string relativeActionName = null)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), placement, relativeActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), placement, ((MemberExpression) relativeActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        PredefinedCategory category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return this.WithCategoryImpl(BoundedTo<TGraph, TPrimary>.ActionDefinition.GetCategoryForPredefinedCategory(category), placement, ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) relativeAction).Result.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        string previousActionName)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, relativeActionName: previousActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, relativeActionName: ((MemberExpression) previousActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, relativeActionName: ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) previousAction).Result.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        string relativeActionName = null)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, placement, relativeActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, placement, ((MemberExpression) relativeActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return this.WithCategoryImpl((BoundedTo<TGraph, TPrimary>.ActionCategory.ConfiguratorCategory) category, placement, ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) relativeAction).Result.Name);
      }

      private BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceInCategoryImpl(
        Placement placement,
        string relativeActionName)
      {
        this.Result.PlacementInCategory = placement;
        this.Result.AfterInMenu = relativeActionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfterInCategory(
        string previousActionName)
      {
        return this.PlaceInCategoryImpl(Placement.After, previousActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfterInCategory(
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return this.PlaceInCategoryImpl(Placement.After, ((MemberExpression) previousActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfterInCategory(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return this.PlaceInCategoryImpl(Placement.After, ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) previousAction).Result.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceInCategory(
        Placement placement,
        string relativeActionName = null)
      {
        return this.PlaceInCategoryImpl(placement, relativeActionName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceInCategory(
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return this.PlaceInCategoryImpl(placement, ((MemberExpression) relativeActionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceInCategory(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return this.PlaceInCategoryImpl(placement, ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) relativeAction).Result.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfter(
        string actionName)
      {
        this.Result.After = actionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfter(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        this.Result.After = ((MemberExpression) actionSelector.Body).Member.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        this.Result.After = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithForm(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured form)
      {
        this.Result.Form = ((BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm) form).Result.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithForm<TForm>() where TForm : class, IWorkflowFormBqlTable, new()
      {
        this.Result.Form = typeof (TForm).FullName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction MassProcessingScreen(
        string screenId)
      {
        this.Result.MassProcessingScreen = screenId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction MassProcessingScreen<T>() where T : PXGraph
      {
        this.Result.MassProcessingScreen = PXPageIndexingService.GetScreenIDFromGraphType(typeof (T));
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsCreateRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate createAction)
      {
        this.Result.Navigation = ((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition) createAction).Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsCreateRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate> createAction)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate configuredCreate = createAction((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen) navigationDefinition);
        navigationDefinition.Result.ActionType = "C";
        this.Result.Navigation = navigationDefinition.Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsSearchRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch searchAction)
      {
        this.Result.Navigation = ((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition) searchAction).Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsSearchRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch> searchAction)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch configuredSearch = searchAction((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen) navigationDefinition);
        navigationDefinition.Result.ActionType = "S";
        this.Result.Navigation = navigationDefinition.Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsRunReportScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport runReportAction)
      {
        this.Result.Navigation = ((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition) runReportAction).Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsRunReportScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport> runReportAction)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport configuredRunReport = runReportAction((BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen) navigationDefinition);
        navigationDefinition.Result.ActionType = "R";
        this.Result.Navigation = navigationDefinition.Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsSidePanelScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel sidePanelAction)
      {
        this.Result.Navigation = ((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition) sidePanelAction).Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsSidePanelScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel> sidePanelAction)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel configuredSidePanel = sidePanelAction((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen) navigationDefinition);
        navigationDefinition.Result.WindowMode = PXBaseRedirectException.WindowMode.Layer;
        navigationDefinition.Result.ActionType = "P";
        this.Result.Navigation = navigationDefinition.Result;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        throw new NotImplementedException("Inline conditions are not yet supported");
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.DisableCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsDisabledWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.DisableCondition != null)
          this.Result.DisableCondition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsDisabledWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.DisableCondition != null)
          this.Result.DisableCondition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.DisableCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsDisabledAlways()
      {
        this.Result.DisableCondition = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        throw new NotImplementedException("Inline conditions are not yet supported");
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.HideCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsHiddenWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.HideCondition != null)
          this.Result.HideCondition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsHiddenWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.HideCondition != null)
          this.Result.HideCondition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.HideCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsHiddenAlways()
      {
        this.Result.HideCondition = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.FieldAssignments);
        containerAdjuster(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithParameterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.ParameterContainerAdjusterAssignments> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ParameterContainerAdjusterAssignments adjusterAssignments = new BoundedTo<TGraph, TPrimary>.Assignment.ParameterContainerAdjusterAssignments(this.Result.ParameterAssignments);
        containerAdjuster(adjusterAssignments);
        return this;
      }

      [Obsolete("Use WithPersistOptions method instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction DoesNotPersist()
      {
        this.Result.DisablePersist = new bool?(true);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithPersistOptions(
        ActionPersistOptions persistOptions)
      {
        switch (persistOptions)
        {
          case ActionPersistOptions.Auto:
            this.Result.DisablePersist = new bool?();
            break;
          case ActionPersistOptions.NoPersist:
            this.Result.DisablePersist = new bool?(true);
            break;
          case ActionPersistOptions.PersistBeforeAction:
            this.Result.DisablePersist = new bool?(false);
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (persistOptions), (object) persistOptions, (string) null);
        }
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction InBatchMode()
      {
        this.Result.BatchMode = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithEnableRightsMapping(
        PXCacheRights mapping)
      {
        this.Result.MapEnableRights = new PXCacheRights?(mapping);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithVisibleRightsMapping(
        PXCacheRights mapping)
      {
        this.Result.MapViewRights = new PXCacheRights?(mapping);
        return this;
      }

      [Obsolete("This method will be removed in 2022r1. Use overloads instead")]
      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithCategory(
        string category)
      {
        this.Result.Category = category;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IsExposedToMobile(
        bool expose)
      {
        this.Result.ExposeToMobile = new bool?(expose);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction WithDisplayOnToolbar(
        DisplayOnToolBar displayOnToolbar)
      {
        switch (displayOnToolbar)
        {
          case DisplayOnToolBar.Hidden:
            this.Result.DisplayOnMainToolbar = new bool?(false);
            this.Result.IsLockedOnToolbar = new bool?(false);
            break;
          case DisplayOnToolBar.IfEnabled:
            this.Result.DisplayOnMainToolbar = new bool?(true);
            this.Result.IsLockedOnToolbar = new bool?(false);
            break;
          case DisplayOnToolBar.Always:
            this.Result.DisplayOnMainToolbar = new bool?(true);
            this.Result.IsLockedOnToolbar = new bool?(true);
            break;
        }
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction IgnoresArchiveDisabling(
        bool value)
      {
        this.Result.IgnoresArchiveDisabling = new bool?(value);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithDisplayName(displayName);
      }

      [Obsolete("Use WithPersistOptions method instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.DoesNotPersist()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.DoesNotPersist();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithPersistOptions(
        ActionPersistOptions persistOptions)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithPersistOptions(persistOptions);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig.InBatchMode()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.InBatchMode();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithEnableRightsMapping(
        PXCacheRights mapping)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithEnableRightsMapping(mapping);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MapEnableToSelect()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithEnableRightsMapping(PXCacheRights.Select);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MapEnableToUpdate()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithEnableRightsMapping(PXCacheRights.Update);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithVisibleRightsMapping(
        PXCacheRights mapping)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithVisibleRightsMapping(mapping);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MapVisibleToSelect()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithVisibleRightsMapping(PXCacheRights.Select);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MapVisibleToUpdate()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithVisibleRightsMapping(PXCacheRights.Update);
      }

      [Obsolete("This method will be removed in 2022r1. Use overloads instead")]
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        string category)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsExposedToMobile(
        bool exposed)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsExposedToMobile(exposed);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        string previousActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        Placement placement,
        string relativeActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        PredefinedCategory category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        string previousActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, previousAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        string relativeActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithCategory(
        BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured category,
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithCategory(category, placement, relativeAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfterInCategory(
        string previousActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfterInCategory(previousActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfterInCategory(
        Expression<Func<TGraph, PXAction<TPrimary>>> previousActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfterInCategory(previousActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfterInCategory(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured previousAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfterInCategory(previousAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceInCategory(
        Placement placement,
        string relativeActionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceInCategory(placement, relativeActionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceInCategory(
        Placement placement,
        Expression<Func<TGraph, PXAction<TPrimary>>> relativeActionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceInCategory(placement, relativeActionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceInCategory(
        Placement placement,
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured relativeAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceInCategory(placement, relativeAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfter(
        string actionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfter(actionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfter(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfter(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.PlaceAfter(action);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithForm(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured form)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithForm(form);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithForm<TForm>()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithForm<TForm>();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsDisabledWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsDisabledWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsDisabledAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsDisabledAlways();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsHiddenWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsHiddenWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsHiddenAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsHiddenAlways();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IgnoresArchiveDisabling(
        bool value)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IgnoresArchiveDisabling(value);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithFieldAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithParameterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerParameters> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithParameterAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.ParameterContainerAdjusterAssignments>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MassProcessingScreen(
        string screenId)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig) this.MassProcessingScreen(screenId);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.MassProcessingScreen<T>()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IMassProcessingOptionalConfig) this.MassProcessingScreen<T>();
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsCreateRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate createAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsCreateRecordScreen(createAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsCreateRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate> createAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsCreateRecordScreen(createAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsSearchRecordScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch searchAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsSearchRecordScreen(searchAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsSearchRecordScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch> searchAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsSearchRecordScreen(searchAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsRunReportScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport runReportAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsRunReportScreen(runReportAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsRunReportScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport> runReportAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsRunReportScreen(runReportAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsSidePanelScreen(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel sidePanelAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsSidePanelScreen(sidePanelAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.IsSidePanelScreen(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel> sidePanelAction)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.IsSidePanelScreen(sidePanelAction);
      }

      BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction.WithDisplayOnToolbar(
        DisplayOnToolBar displayOnToolbar)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) this.WithDisplayOnToolbar(displayOnToolbar);
      }
    }

    /// <summary>Adds an action to the screen configuration.</summary>
    public interface IContainerFillerActions
    {
      /// <summary>Adds settings for an existing graph action to the screen configuration.</summary>
      /// <param name="actionSelector">Expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">Function that configures behaviour of the existing action.</param>
      void Add(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null);

      /// <summary>Adds settings for an existing action defined in a graph extension to the screen configuration.</summary>
      /// <typeparam name="TExtension">Type of the graph extension where the action is declared.</typeparam>
      /// <param name="actionSelector">Expression that addresses the PXAction member of a graph extension.</param>
      /// <param name="config">Function that configures behaviour of the existing action.</param>
      void Add<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>Adds settings for an existing or new action to the screen configuration.</summary>
      /// <param name="actionName">The internal name of the action.</param>
      /// <param name="config">Function that configures the action behaviour.</param>
      void Add(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null);

      /// <summary>Adds settings for a new action to the screen configuration.</summary>
      /// <param name="actionName">The internal name of the action.</param>
      /// <param name="config">Function that configures the action behaviour.</param>
      void AddNew(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null);

      /// <summary>Adds a configured action object to the screen configuration.</summary>
      /// <param name="action">The configured action object.</param>
      void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action);
    }

    /// <summary>
    /// Adds, replaces, updates, or removes an action definition in a screen configuration.
    /// </summary>
    public class ContainerAdjusterActions : 
      BoundedTo<TGraph, TPrimary>.ActionDefinition.IContainerFillerActions
    {
      internal List<BoundedTo<TGraph, TPrimary>.ActionDefinition> Result { get; }

      internal ContainerAdjusterActions(
        List<BoundedTo<TGraph, TPrimary>.ActionDefinition> actions)
      {
        this.Result = actions;
      }

      /// <inheritdoc />
      public void Add(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <inheritdoc />
      public void Add<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <inheritdoc />
      public void Add(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(actionName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) configuratorAction);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action Definition {actionName} already exists.");
        this.Result.Add(configuratorAction.Result);
      }

      /// <inheritdoc />
      public void AddNew(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction1 = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(actionName);
        configuratorAction1.Result.CreateNewAction = true;
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction2 = configuratorAction1;
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) configuratorAction2);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action Definition {actionName} already exists.");
        this.Result.Add(configuratorAction2.Result);
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action Definition {((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name} already exists.");
        this.Result.Add(((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result);
      }

      /// <summary>
      /// Replace existing action definition configuration with new one.
      /// </summary>
      /// <param name="actionSelector">Expression, that is used to address PXAction Graph member.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Replace(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
      {
        this.Replace(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>
      /// Replace existing action definition configuration with new one.
      /// </summary>
      /// <typeparam name="TExtension">Graph extension type.</typeparam>
      /// <param name="actionSelector">Expression, that is used to address PXAction GraphExtension member.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Replace<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Replace(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>
      /// Replace existing action definition configuration with new one.
      /// </summary>
      /// <param name="actionName">System action name for existing action definition.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Replace(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition actionDefinition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionDefinition != null)
          this.Result.Remove(actionDefinition);
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(actionName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionDefinition.IAllowOptionalConfigAction) configuratorAction);
        }
        this.Result.Add(configuratorAction.Result);
      }

      /// <summary>Update existing action definition configuration.</summary>
      /// <param name="actionSelector">Expression, that is used to address PXAction Graph member.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Update(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction> config)
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Update existing action definition configuration.</summary>
      /// <typeparam name="TExtension">Graph extension type.</typeparam>
      /// <param name="actionSelector">Expression, that is used to address PXAction GraphExtension member.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Update<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction> config)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Update existing action definition configuration.</summary>
      /// <param name="actionName">System action name for existing action definition.</param>
      /// <param name="config">Function, that is used to configure new action behaviour.</param>
      public void Update(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction> config)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition action = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction1 = !WebConfig.EnableWorkflowValidationOnStartup || action != null ? new BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction(action) : throw new InvalidOperationException($"Action Definition {actionName} not found.");
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction configuratorAction2 = config(configuratorAction1);
      }

      /// <summary>Remove action definition from screen configuration.</summary>
      /// <param name="actionSelector">Expression, that is used to address PXAction Graph member.</param>
      public void Remove(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        this.Remove(((MemberExpression) actionSelector.Body).Member.Name);
      }

      /// <summary>Remove action definition from screen configuration.</summary>
      /// <typeparam name="TExtension">Graph extension type.</typeparam>
      /// <param name="actionSelector">Expression, that is used to address PXAction GraphExtension member.</param>
      public void Remove<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Remove(((MemberExpression) actionSelector.Body).Member.Name);
      }

      /// <summary>Remove action definition from screen configuration.</summary>
      /// <param name="actionName">System action name for existing action definition.</param>
      public void Remove(string actionName)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition actionDefinition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionDefinition>((Func<BoundedTo<TGraph, TPrimary>.ActionDefinition, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionDefinition == null)
          return;
        this.Result.Remove(actionDefinition);
      }
    }
  }

  public abstract class ActionDefinitionBase
  {
    public string Name { get; set; }

    public bool CreateNewAction { get; set; }

    internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> FieldAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    internal System.Type GetDataType() => typeof (TPrimary);
  }

  public class ActionSequence
  {
    internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> FieldAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    public string PrevActionName { get; set; }

    public string NextActionName { get; set; }

    public bool StopOnError { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition Condition { get; set; }

    internal Readonly.ActionSequence AsReadonly()
    {
      return Readonly.ActionSequence.From<TGraph, TPrimary>(this);
    }

    public class Configurator : 
      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction,
      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction,
      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.ActionSequence Result { get; set; }

      internal Configurator() => this.Result = new BoundedTo<TGraph, TPrimary>.ActionSequence();

      internal Configurator(
        BoundedTo<TGraph, TPrimary>.ActionSequence actionSequence)
      {
        this.Result = actionSequence;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator AfterAction(string actionName)
      {
        this.Result.PrevActionName = actionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator AfterAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return this.AfterAction(((MemberExpression) actionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator AfterAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension
      {
        return this.AfterAction(((MemberExpression) actionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator RunAction(string actionName)
      {
        this.Result.NextActionName = actionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator RunAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return this.RunAction(((MemberExpression) actionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator RunAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension
      {
        return this.RunAction(((MemberExpression) actionSelector.Body).Member.Name);
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator StopOnError(bool value)
      {
        this.Result.StopOnError = value;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator AppliesAlways()
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) null;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionSequence.IContainerFillerFormFields> fields)
      {
        BoundedTo<TGraph, TPrimary>.ActionSequence.ContainerFillerFormFields fillerFormFields = new BoundedTo<TGraph, TPrimary>.ActionSequence.ContainerFillerFormFields(this.Result);
        fields((BoundedTo<TGraph, TPrimary>.ActionSequence.IContainerFillerFormFields) fillerFormFields);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction.AfterAction(
        string actionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction) this.AfterAction(actionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction.AfterAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction) this.AfterAction(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction.AfterAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction) this.AfterAction<TExtension>(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction.RunAction(
        string actionName)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.RunAction(actionName);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction.RunAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.RunAction(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction.RunAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.RunAction<TExtension>(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig.StopOnError(
        bool value)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.StopOnError(value);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig.AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.AppliesWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig.AppliesAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.AppliesAlways();
      }

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionSequence.IContainerFillerFormFields> fields)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig) this.WithFieldAssignments(fields);
      }
    }

    public class ContainerFillerFormFields : 
      BoundedTo<TGraph, TPrimary>.ActionSequence.IContainerFillerFormFields
    {
      private readonly BoundedTo<TGraph, TPrimary>.ActionSequence _actionSequence;

      public ContainerFillerFormFields(
        BoundedTo<TGraph, TPrimary>.ActionSequence actionSequence)
      {
        this._actionSequence = actionSequence;
      }

      public void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedRightOperand, BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.ActionSequence.ConfiguratorFormFieldValue configuratorFormFieldValue = new BoundedTo<TGraph, TPrimary>.ActionSequence.ConfiguratorFormFieldValue(fieldName);
        BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured fieldConfigured = config((BoundedTo<TGraph, TPrimary>.ActionSequence.INeedRightOperand) configuratorFormFieldValue);
        this._actionSequence.FieldAssignments.Add(configuratorFormFieldValue.Result);
      }
    }

    public class ConfiguratorFormFieldValue : 
      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedRightOperand,
      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured
    {
      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment Result { get; }

      public ConfiguratorFormFieldValue(string formFieldName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(false, formFieldName);
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromValue(object value)
      {
        this.Result.SetFromValue(value);
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromExpression(
        string expression)
      {
        this.Result.SetFromExpression(expression);
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromField<TField>() where TField : IBqlField
      {
        this.Result.SetFromField<TField>();
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromField(
        string fieldName)
      {
        this.Result.SetFromField(fieldName);
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromToday()
      {
        this.Result.SetFromToday();
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromNow()
      {
        this.Result.SetFromNow();
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromCurrentUser()
      {
        this.Result.SetFromCurrentUser();
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromCurrentBranch()
      {
        this.Result.SetFromCurrentBranch();
        return (BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured) this;
      }
    }

    public interface INeedPreviousAction
    {
      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction AfterAction(string actionName);

      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction AfterAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector);

      BoundedTo<TGraph, TPrimary>.ActionSequence.INeedNextAction AfterAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension;
    }

    public interface INeedNextAction
    {
      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig RunAction(string actionName);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig RunAction(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig RunAction<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension;
    }

    public interface IConfigured
    {
    }

    public interface IAllowOptionalConfig : BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured
    {
      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig StopOnError(bool value);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig AppliesAlways();

      BoundedTo<TGraph, TPrimary>.ActionSequence.IAllowOptionalConfig WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionSequence.IContainerFillerFormFields> fields);
    }

    public interface IContainerFillerFormFields
    {
      void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedRightOperand, BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured> config);
    }

    public interface IFieldConfigured
    {
    }

    public interface INeedRightOperand
    {
      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromValue(object value);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromExpression(
        string expression);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromField<TField>() where TField : IBqlField;

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromField(string fieldName);

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromToday();

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromNow();

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromCurrentUser();

      BoundedTo<TGraph, TPrimary>.ActionSequence.IFieldConfigured SetFromCurrentBranch();
    }

    public class ContainerAdjusterActionSequences
    {
      private readonly List<BoundedTo<TGraph, TPrimary>.ActionSequence> _result;

      public ContainerAdjusterActionSequences(
        List<BoundedTo<TGraph, TPrimary>.ActionSequence> result)
      {
        this._result = result;
      }

      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction, BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured> config)
      {
        if (config == null)
          throw new ArgumentNullException(nameof (config));
        BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator configurator = new BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator();
        BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction) configurator);
        if (string.Equals(configurator.Result.PrevActionName, configurator.Result.NextActionName, StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException("Invalid configuration: next action cannot be the same as previous action", nameof (config));
        this._result.Add(configurator.Result);
      }

      public void Remove(
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction, BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured> selector)
      {
        if (selector == null)
          throw new ArgumentNullException(nameof (selector));
        BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator configurator = new BoundedTo<TGraph, TPrimary>.ActionSequence.Configurator();
        BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured configured = selector((BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction) configurator);
        BoundedTo<TGraph, TPrimary>.ActionSequence actionSequence = this._result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionSequence>((Func<BoundedTo<TGraph, TPrimary>.ActionSequence, bool>) (s => string.Equals(s.PrevActionName, configurator.Result.PrevActionName, StringComparison.OrdinalIgnoreCase) && string.Equals(s.NextActionName, configurator.Result.NextActionName, StringComparison.OrdinalIgnoreCase) && s.Condition == configurator.Result.Condition));
        if (actionSequence == null)
          return;
        this._result.Remove(actionSequence);
      }

      public void Replace(
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction, BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured> selector,
        Func<BoundedTo<TGraph, TPrimary>.ActionSequence.INeedPreviousAction, BoundedTo<TGraph, TPrimary>.ActionSequence.IConfigured> config)
      {
        this.Remove(selector);
        this.Add(config);
      }
    }
  }

  public class ActionState : IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.ActionState>
  {
    public string Name { get; set; }

    public bool DuplicatedInToolbar { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition AutoRun { get; set; } = (BoundedTo<TGraph, TPrimary>.Condition) false;

    public bool ValidateNewAction { get; set; }

    public ActionConnotation Connotation { get; set; }

    internal Readonly.ActionState AsReadonly() => Readonly.ActionState.From<TGraph, TPrimary>(this);

    public BoundedTo<TGraph, TPrimary>.ActionState CreateCopy()
    {
      return (BoundedTo<TGraph, TPrimary>.ActionState) this.MemberwiseClone();
    }

    /// <summary>Configures a state of an action.</summary>
    public class ActionStateBuilder
    {
      /// <summary>Creates and configures a state of a new action with the <paramref name="actionName" /> internal name.</summary>
      /// <param name="actionName">The internal name of an action.</param>
      /// <param name="initializer">The function, that is used to configure the new action state object.</param>
      /// <remarks>If you add an action state configuration to any state, the current action will be enabled in this state unless the action is not
      /// disabled by a condition on the action level. Thus, if the <paramref name="initializer" /> parameter value is null, the action is enabled in the current state.</remarks>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.ActionState.IConfigured Create(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> initializer = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction(actionName);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) configuratorAction);
        }
        return (BoundedTo<TGraph, TPrimary>.ActionState.IConfigured) configuratorAction;
      }
    }

    /// <summary>
    /// Configures connotation for an action and displaying of the action on the form toolbar.
    /// </summary>
    public interface IAllowOptionalConfig : BoundedTo<TGraph, TPrimary>.ActionState.IConfigured
    {
      /// <summary>Specifies that the current action in the current state must be displayed on the form toolbar.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig IsDuplicatedInToolbar();

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig WithConnotation(
        ActionConnotation connotation);

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig WithNoConnotation();
    }

    /// <summary>Configures automatic execution of actions.</summary>
    public interface IAllowExtendedOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionState.IConfigured
    {
      [Obsolete("Use IsAutoWhen or IsAutoAlways instead")]
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig IsAutoAction(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition = null);

      /// <summary>Specifies that the current action in the current state must be executed automatically if the <paramref name="condition" /> condition equals true.</summary>
      /// <param name="condition">A condition to be checked. If the condition is true, the system runs the current action automatically. </param>
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig IsAutoWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Specifies that the current action is always run automatically. </summary>
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig IsAutoAlways();
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorAction : 
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionState.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.ActionState Result { get; }

      internal ConfiguratorAction(string actionName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.ActionState()
        {
          Name = actionName
        };
      }

      internal ConfiguratorAction(
        BoundedTo<TGraph, TPrimary>.ActionState actionState)
      {
        this.Result = actionState;
      }

      public BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction IsDuplicatedInToolbar()
      {
        this.Result.DuplicatedInToolbar = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction WithConnotation(
        ActionConnotation connotation)
      {
        this.Result.Connotation = connotation;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig.IsDuplicatedInToolbar()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) this.IsDuplicatedInToolbar();
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig.WithConnotation(
        ActionConnotation connotation)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) this.WithConnotation(connotation);
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig.WithNoConnotation()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) this.WithConnotation(ActionConnotation.None);
      }
    }

    public class ExtendedConfiguratorAction : 
      BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction,
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ActionState.IConfigured
    {
      internal ExtendedConfiguratorAction(string actionName)
        : base(actionName)
      {
      }

      internal ExtendedConfiguratorAction(
        BoundedTo<TGraph, TPrimary>.ActionState actionState)
        : base(actionState)
      {
      }

      public BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction IsAutoWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.AutoRun = condition as BoundedTo<TGraph, TPrimary>.Condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction IsAutoAlways()
      {
        this.Result.AutoRun = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      [Obsolete("Use IsAutoWhen or IsAutoAlways instead")]
      public BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction IsAutoAction(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState result = this.Result;
        if (!(condition is BoundedTo<TGraph, TPrimary>.Condition condition1))
          condition1 = (BoundedTo<TGraph, TPrimary>.Condition) true;
        result.AutoRun = condition1;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig.IsAutoAction(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) this.IsAutoAction(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig.IsAutoWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) this.IsAutoWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig.IsAutoAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) this.IsAutoAlways();
      }
    }

    public interface IContainerFillerActions
    {
      void Add(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null);

      void Add(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null);

      void Add<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>;

      void Add<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>;

      void Add(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null);

      void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null);

      void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null);
    }

    public class ContainerAdjusterActions : 
      BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions
    {
      internal List<BoundedTo<TGraph, TPrimary>.ActionState> Result { get; }

      internal ContainerAdjusterActions(
        List<BoundedTo<TGraph, TPrimary>.ActionState> actionStates)
      {
        this.Result = actionStates;
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Add(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, (Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured>) config);
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Add(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      public void Add<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, (Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured>) config);
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      public void Add<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Add(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <param name="actionName">The internal name of the action.</param>
      public void Add(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction(actionName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) configuratorAction);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action State {actionName} already exists.");
        this.Result.Add(configuratorAction.Result);
      }

      /// <overloads>Adds a configuration of an action state to the current state.</overloads>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <param name="action">A configured action.</param>
      public void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configurator = new BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction(((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) configurator);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, configurator.Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action State {configurator.Result.Name} already exists.");
        configurator.Result.ValidateNewAction = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.CreateNewAction;
        this.Result.Add(configurator.Result);
      }

      public void Add(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IExtendedConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction configurator = new BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction(((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig) configurator);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, configurator.Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Action State {configurator.Result.Name} already exists.");
        configurator.Result.ValidateNewAction = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.CreateNewAction;
        this.Result.Add(configurator.Result);
      }

      /// <summary>Replaces an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Replace(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        this.Replace(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Replaces an existing configuration of an action state with the provided configuration.</summary>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Use this method if the action is declared in a graph extension.</remarks>
      public void Replace<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Replace(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Replaces an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="actionName">The internal name of the action.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Replace(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionState != null)
          this.Result.Remove(actionState);
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction(actionName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) configuratorAction);
        }
        this.Result.Add(configuratorAction.Result);
      }

      /// <summary>Replaces an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="action">A configured action.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Replace(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured> config = null)
      {
        string actionName = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name;
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionState != null)
          this.Result.Remove(actionState);
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction = new BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction(actionName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ActionState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig) configuratorAction);
        }
        this.Result.Add(configuratorAction.Result);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Do not recommend to use this method for actions which are not PXAutoAction type.</remarks>
      public void Update(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction> config)
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="actionSelector">An expression that addresses the PXAutoAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Use this method if the action is PXAutoAction type.</remarks>
      public void Update(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction> config)
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Use this method if the action is not PXAutoAction type and is declared in a graph extension.</remarks>
      public void Update<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction> config)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Use this method if the action is PXAutoAction type and is declared in a graph extension.</remarks>
      public void Update<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> actionSelector,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction> config)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Update(((MemberExpression) actionSelector.Body).Member.Name, config);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="actionName">The internal name of the action.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Do not recommend to use this method for actions which are not PXAutoAction type.</remarks>
      public void Update(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction> config)
      {
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.FindActionState(actionName);
        if (actionState == null)
          return;
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction1 = new BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction(actionState);
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction2 = config(configuratorAction1);
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="definedInWorkflowActionName">The internal name of the action defined in workflow or PXAutoAction.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      /// <remarks>Use this method if it is nesessary to update configuration of action state that was defined in workflow or the definedInWorkflowActionName is name of the PXAutoAction action.</remarks>
      public void Update(
        string actionName,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction> config)
      {
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.FindActionState(actionName);
        if (actionState == null)
          return;
        BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction configuratorAction1 = new BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction(actionState);
        BoundedTo<TGraph, TPrimary>.ActionState.ExtendedConfiguratorAction configuratorAction2 = config(configuratorAction1);
      }

      private BoundedTo<TGraph, TPrimary>.ActionState FindActionState(string actionName)
      {
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        return !WebConfig.EnableWorkflowValidationOnStartup || actionState != null ? actionState : throw new InvalidOperationException($"Action State {actionName} not found.");
      }

      /// <summary>Updates an existing configuration of an action state with the provided configuration.</summary>
      /// <param name="action">A configured action.</param>
      /// <param name="config">A function that configures behavior of the action in the current state.</param>
      public void Update(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action,
        Func<BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction, BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction> config)
      {
        string actionName = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name;
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (WebConfig.EnableWorkflowValidationOnStartup && actionState == null)
          throw new InvalidOperationException($"Action State {actionName} not found.");
        if (actionState == null)
          return;
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction1 = new BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction(actionState);
        BoundedTo<TGraph, TPrimary>.ActionState.ConfiguratorAction configuratorAction2 = config(configuratorAction1);
      }

      /// <summary>Removes a configuration of an action state from the current state. It means the method removes the action from the state configuration, but the action
      /// itself is not deleted.</summary>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      public void Remove(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        this.Remove(((MemberExpression) actionSelector.Body).Member.Name);
      }

      /// <summary>Removes a configuration of an action state from the current state. It means the method removes the action from the state configuration, but the action
      /// itself is not deleted.</summary>
      /// <typeparam name="TExtension">A type of the graph extension in which the action is declared.</typeparam>
      /// <param name="actionSelector">An expression that addresses the PXAction member of a graph.</param>
      /// <remarks>Use this method if the action is declared in a graph extension.</remarks>
      public void Remove<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Remove(((MemberExpression) actionSelector.Body).Member.Name);
      }

      /// <summary>Removes a configuration of an action state from the current state. It means the method removes the action from the state configuration, but the action
      /// itself is not deleted.</summary>
      /// <param name="actionName">The internal name of the action.</param>
      public void Remove(string actionName)
      {
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionState == null)
          return;
        this.Result.Remove(actionState);
      }

      /// <summary>Removes a configuration of an action state from the current state. It means the method removes the action from the state configuration, but the action
      /// itself is not deleted.</summary>
      /// <param name="action">A configured action.</param>
      public void Remove(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        string actionName = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name;
        BoundedTo<TGraph, TPrimary>.ActionState actionState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, bool>) (it => string.Equals(it.Name, actionName, StringComparison.OrdinalIgnoreCase)));
        if (actionState == null)
          return;
        this.Result.Remove(actionState);
      }
    }
  }

  public class ArchivingRule
  {
    public System.Type Primary { get; set; } = typeof (TPrimary);

    public System.Type Table { get; set; }

    public ArchivingReferStrategy ReferStrategy { get; set; }

    public System.Type FK { get; set; }

    public System.Type Select { get; set; }

    public bool IsParentToPrimary { get; set; }

    internal Readonly.ArchivingRule AsReadonly()
    {
      return Readonly.ArchivingRule.From<TGraph, TPrimary>(this);
    }

    public interface INeedTable
    {
      BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable> Archive<TTable>() where TTable : class, IBqlTable, new();
    }

    public interface INeedRelation<TTable> where TTable : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured UsingItsFK<TForeignKey>() where TForeignKey : IForeignKeyBetween<TTable, TPrimary>;

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured UsingPrimarysFK<TForeignKey>() where TForeignKey : IForeignKeyBetween<TPrimary, TTable>;

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured UsingItsParentAttribute();

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured UsingPrimarysParentAttribute();

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured Using<TSelect>(
        bool currentIsParentToPrimary = false)
        where TSelect : IBqlSelect<TTable>;
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorArchivingRule : BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable
    {
      internal BoundedTo<TGraph, TPrimary>.ArchivingRule Result { get; set; }

      protected ConfiguratorArchivingRule(BoundedTo<TGraph, TPrimary>.ArchivingRule result)
      {
        this.Result = result;
      }

      internal ConfiguratorArchivingRule()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.ArchivingRule();
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> Archive<TTable>() where TTable : class, IBqlTable, new()
      {
        this.Result.Table = typeof (TTable);
        return new BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable>(this.Result);
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable> BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable.Archive<TTable>()
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>) this.Archive<TTable>();
      }
    }

    public class ConfiguratorArchivingRule<TTable> : 
      BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule,
      BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>,
      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured
      where TTable : class, IBqlTable, new()
    {
      internal ConfiguratorArchivingRule(BoundedTo<TGraph, TPrimary>.ArchivingRule result)
        : base(result)
      {
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> UsingItsFK<TForeignKey>() where TForeignKey : IForeignKeyBetween<TTable, TPrimary>
      {
        this.Result.ReferStrategy = ArchivingReferStrategy.FK;
        this.Result.IsParentToPrimary = false;
        this.Result.FK = typeof (TForeignKey);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> UsingPrimarysFK<TForeignKey>() where TForeignKey : IForeignKeyBetween<TPrimary, TTable>
      {
        this.Result.ReferStrategy = ArchivingReferStrategy.FK;
        this.Result.IsParentToPrimary = true;
        this.Result.FK = typeof (TForeignKey);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> UsingItsParentAttribute()
      {
        this.Result.ReferStrategy = ArchivingReferStrategy.Parent;
        this.Result.IsParentToPrimary = false;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> UsingPrimarysParentAttribute()
      {
        this.Result.ReferStrategy = ArchivingReferStrategy.Parent;
        this.Result.IsParentToPrimary = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> Using<TSelect>(
        bool currentIsParentToPrimary = false)
        where TSelect : IBqlSelect<TTable>
      {
        this.Result.ReferStrategy = ArchivingReferStrategy.Select;
        this.Result.IsParentToPrimary = currentIsParentToPrimary;
        this.Result.FK = typeof (TSelect);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>.UsingItsFK<TForeignKey>()
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured) this.UsingItsFK<TForeignKey>();
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>.UsingPrimarysFK<TForeignKey>()
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured) this.UsingPrimarysFK<TForeignKey>();
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>.UsingItsParentAttribute()
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured) this.UsingItsParentAttribute();
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>.UsingPrimarysParentAttribute()
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured) this.UsingPrimarysParentAttribute();
      }

      BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>.Using<TSelect>(
        bool currentIsParentToPrimary)
      {
        return (BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured) this.Using<TSelect>(currentIsParentToPrimary);
      }
    }

    public interface IContainerFillerRules
    {
      void Add(
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured> config);

      void Add(
        BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured rule);
    }

    public class ContainerAdjusterRules : 
      BoundedTo<TGraph, TPrimary>.ArchivingRule.IContainerFillerRules
    {
      internal List<BoundedTo<TGraph, TPrimary>.ArchivingRule> Result { get; }

      internal ContainerAdjusterRules(
        List<BoundedTo<TGraph, TPrimary>.ArchivingRule> rules)
      {
        this.Result = rules;
      }

      /// <inheritdoc />
      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule configuratorArchivingRule = new BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule();
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable) configuratorArchivingRule);
        }
        this.Result.Add(configuratorArchivingRule.Result);
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured rule)
      {
        this.Result.Add(((BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule) rule).Result);
      }

      public void Replace<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>> ruleSelector,
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured> config = null)
        where TTable : class, IBqlTable, new()
      {
        this.Remove<TTable>(ruleSelector);
        this.Add(config);
      }

      public void Update<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>> ruleSelector,
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>, BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured> config)
        where TTable : class, IBqlTable, new()
      {
        BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule control = new BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule();
        BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable> needRelation = ruleSelector((BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable) control);
        BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable> configuratorArchivingRule = new BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule<TTable>(this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ArchivingRule>((Func<BoundedTo<TGraph, TPrimary>.ArchivingRule, bool>) (it => it.Table == control.Result.Table)));
        BoundedTo<TGraph, TPrimary>.ArchivingRule.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>) configuratorArchivingRule);
      }

      public void Remove<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable, BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable>> ruleSelector)
        where TTable : class, IBqlTable, new()
      {
        BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule control = new BoundedTo<TGraph, TPrimary>.ArchivingRule.ConfiguratorArchivingRule();
        BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedRelation<TTable> needRelation = ruleSelector((BoundedTo<TGraph, TPrimary>.ArchivingRule.INeedTable) control);
        BoundedTo<TGraph, TPrimary>.ArchivingRule archivingRule = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ArchivingRule>((Func<BoundedTo<TGraph, TPrimary>.ArchivingRule, bool>) (it => it.Table == control.Result.Table));
        if (archivingRule == null)
          return;
        this.Result.Remove(archivingRule);
      }
    }
  }

  public class Assignment : IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.Assignment>
  {
    public bool IsActive { get; set; } = true;

    public bool IsField { get; set; } = true;

    public string LeftOperandName { get; set; }

    public object RightOperandValue { get; set; }

    public bool IsFromScheme { get; set; } = true;

    internal Readonly.Assignment AsReadonly() => Readonly.Assignment.From<TGraph, TPrimary>(this);

    internal BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment ToConfigurator()
    {
      return new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(this);
    }

    public BoundedTo<TGraph, TPrimary>.Assignment CreateCopy()
    {
      return (BoundedTo<TGraph, TPrimary>.Assignment) this.MemberwiseClone();
    }

    /// <summary>
    /// Creates and configures new assignments for DAC fields.
    /// </summary>
    public class AssignmentBuilder
    {
      /// <summary>Creates and configure a new assignment for the DAC field.</summary>
      /// <param name="isField">Indicates if there is a DAC field or an action parameter in the left part of the assignment.</param>
      /// <param name="leftOperandName">A name of a DAC field name or action method parameter.</param>
      /// <param name="initializer">A function that configures an assignment.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Assignment.IConfigured Create(
        bool isField,
        string leftOperandName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(isField, leftOperandName);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment);
        return (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) configuratorAssignment;
      }

      /// <summary>Creates and configure a new assignment for the DAC field.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="initializer">A function that configures an assignment.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Assignment.IConfigured Create<TField>(
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> initializer)
        where TField : IBqlField
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(true, typeof (TField).Name);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment);
        return (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) configuratorAssignment;
      }
    }

    /// <summary>Specifies a value for the field assignment.</summary>
    public interface INeedRightOperand
    {
      /// <summary>Specifies a constant value for the field assignment.</summary>
      /// <param name="value">A value that is assigned to the field.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromValue(object value);

      /// <summary>Specifies a string expression for the field assignment. This expression is evaluated at runtime in the current graph context. The result value is
      /// applied to the current field or parameter.</summary>
      /// <param name="expression">A string expression.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromExpression(
        string expression);

      /// <summary>Specifies a dialog box field which value will be assigned to the DAC field.</summary>
      /// <param name="form">A configured dialog box.</param>
      /// <param name="fieldName">An internal name of the dialog box field.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromFormField(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured form,
        string fieldName);

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromFormField<TField>() where TField : IBqlField;

      /// <summary>Specifies a  dialog box field which value will be assigned to the DAC field.</summary>
      /// <typeparam name="TField">A dialog box field.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromField<TField>() where TField : IBqlField;

      /// <summary>Sets the current date to the DAC field at runtime. The method sets the sames value which the @Now string expression returns.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromToday();

      /// <summary>Sets the current date and time to the DAC field at runtime. The method sets the sames value which the @Now string expression returns.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromNow();

      /// <summary>Sets the current user name to the DAC field at runtime.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromCurrentUser();

      /// <summary>Sets the current branch ID to the DAC field at runtime.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromCurrentBranch();
    }

    public interface IAllowOptionalConfig : BoundedTo<TGraph, TPrimary>.Assignment.IConfigured
    {
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceFirst();

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceBefore(string fieldName);

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceAfter(string fieldName);

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment);

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment);

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceBefore<TField>() where TField : IBqlField;

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig PlaceAfter<TField>() where TField : IBqlField;
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorAssignment : 
      BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand,
      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.Assignment.IConfigured,
      IOrderableWorkflowElement
    {
      internal BoundedTo<TGraph, TPrimary>.Assignment Result { get; }

      public MoveObjectInCollection MoveBefore { get; set; }

      public string NearKey { get; set; }

      public string Key => this.Result.LeftOperandName;

      internal ConfiguratorAssignment(bool isField, string leftOperandName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.Assignment()
        {
          IsField = isField,
          LeftOperandName = leftOperandName
        };
      }

      internal ConfiguratorAssignment(BoundedTo<TGraph, TPrimary>.Assignment result)
      {
        this.Result = result;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromValue(object value)
      {
        this.Result.RightOperandValue = (object) value?.ToString();
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromExpression(
        string expression)
      {
        this.Result.RightOperandValue = (object) expression;
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromFormField(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured formConfig,
        string fieldName)
      {
        this.Result.RightOperandValue = (object) $"[{((BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm) formConfig).Result.Name}.{fieldName}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromFormField<TField>() where TField : IBqlField
      {
        this.Result.RightOperandValue = (object) $"[{BqlCommand.GetItemType<TField>().Name}.{typeof (TField).Name}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromField<TField>() where TField : IBqlField
      {
        this.Result.RightOperandValue = (object) $"[{typeof (TField).Name}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromField(
        string fieldName)
      {
        this.Result.RightOperandValue = (object) $"[{fieldName}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromToday()
      {
        this.Result.RightOperandValue = (object) "@Today";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromNow()
      {
        this.Result.RightOperandValue = (object) "=Now()";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromCurrentUser()
      {
        this.Result.RightOperandValue = (object) "@me";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig SetFromCurrentBranch()
      {
        this.Result.RightOperandValue = (object) "@branch";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment Place(
        string fieldName,
        MoveObjectInCollection place)
      {
        this.MoveBefore = place;
        this.NearKey = fieldName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceFirst()
      {
        return this.Place((string) null, MoveObjectInCollection.First);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceBefore(
        string fieldName)
      {
        return this.Place(fieldName, MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceAfter(
        string fieldName)
      {
        return this.Place(fieldName, MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment)
      {
        return this.Place(((BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment) fieldAssignment).Result.LeftOperandName, MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment)
      {
        return this.Place(((BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment) fieldAssignment).Result.LeftOperandName, MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceBefore<TField>() where TField : IBqlField
      {
        return this.Place(typeof (TField).Name, MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment PlaceAfter<TField>() where TField : IBqlField
      {
        return this.Place(typeof (TField).Name, MoveObjectInCollection.After);
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceFirst()
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceFirst();
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceBefore(
        string fieldName)
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceBefore(fieldName);
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceAfter(
        string fieldName)
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceAfter(fieldName);
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment)
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceBefore(fieldAssignment);
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured fieldAssignment)
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceAfter(fieldAssignment);
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceBefore<TField>()
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceBefore<TField>();
      }

      BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig.PlaceAfter<TField>()
      {
        return (BoundedTo<TGraph, TPrimary>.Assignment.IAllowOptionalConfig) this.PlaceAfter<TField>();
      }
    }

    /// <summary>Adds new field assignments.</summary>
    public interface IContainerFillerFields
    {
      /// <summary>Adds a new field assignment from a boolean value.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      void Add<TField>(bool? value) where TField : IBqlField;

      /// <summary>Adds a new field assignment from an integer value.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      void Add<TField>(int? value) where TField : IBqlField;

      /// <summary>Adds a new field assignment from a decimal value.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      void Add<TField>(Decimal? value) where TField : IBqlField;

      /// <summary>Adds a new field assignment from a string value.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      void Add<TField>(string value) where TField : IBqlField;

      /// <summary>Adds a new field assignment from a boolean value.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      void Add(string fieldName, bool? value);

      /// <summary>Adds a new field assignment from an integer value.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      void Add(string fieldName, int? value);

      /// <summary>Adds a new field assignment from a decimal value.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      void Add(string fieldName, Decimal? value);

      /// <summary>Adds a new field assignment from a string value.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      void Add(string fieldName, string value);

      /// <summary>Adds a new field assignment.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function that configures the current field assignment.</param>
      void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
        where TField : IBqlField;

      /// <summary>Adds a new field assignment.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function that configures the current field assignment.</param>
      void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config);
    }

    /// <summary>
    /// Adds, updates, replaces, and removes field assignments.
    /// </summary>
    public class FieldContainerAdjusterAssignment : 
      BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields
    {
      internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> Result { get; }

      internal FieldContainerAdjusterAssignment(
        List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> assignments)
      {
        this.Result = assignments;
      }

      /// <inheritdoc />
      public void Add<TField>(bool? value) where TField : IBqlField
      {
        this.Add<TField>((Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add<TField>(int? value) where TField : IBqlField
      {
        this.Add<TField>((Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add<TField>(Decimal? value) where TField : IBqlField
      {
        this.Add<TField>((Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add<TField>(string value) where TField : IBqlField
      {
        this.Add<TField>((Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string fieldName, bool? value)
      {
        this.Add(fieldName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string fieldName, int? value)
      {
        this.Add(fieldName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string fieldName, Decimal? value)
      {
        this.Add(fieldName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string fieldName, string value)
      {
        this.Add(fieldName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
        where TField : IBqlField
      {
        this.Add(typeof (TField).Name, config);
      }

      /// <inheritdoc />
      public void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(true, fieldName);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => string.Equals(it.Result.LeftOperandName, fieldName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Field Assignment {fieldName} already exists.");
        this.Result.Add(configuratorAssignment);
      }

      /// <summary>Replaces an existing assignment with the new one.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function that configures a current field.</param>
      public void Replace<TField>(
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
        where TField : IBqlField
      {
        this.Replace(typeof (TField).Name, config);
      }

      /// <summary>Replaces an existing assignment with the new one.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function that configures a current field.</param>
      public void Replace(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => it.Result.LeftOperandName == fieldName));
        if (configuratorAssignment1 != null)
          this.Result.Remove(configuratorAssignment1);
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment2 = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(true, fieldName);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment2);
        this.Result.Add(configuratorAssignment2);
      }

      /// <summary>Updates an existing assignment.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function that updates current field configuration.</param>
      public void Update<TField>(
        Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
        where TField : IBqlField
      {
        this.Update(typeof (TField).Name, config);
      }

      /// <summary>Updates an existing assignment.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function that updates current field configuration.</param>
      public void Update(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
      {
        int index = this.Result.FindIndex((Predicate<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (it => it.Result.LeftOperandName == fieldName));
        if (index >= 0)
        {
          BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = this.Result[index];
          BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = config(configuratorAssignment);
        }
        else if (WebConfig.EnableWorkflowValidationOnStartup)
          throw new InvalidOperationException($"Field {fieldName} not found.");
      }

      /// <summary>Removes an existing assignment.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      public void Remove<TField>() where TField : IBqlField => this.Remove(typeof (TField).Name);

      /// <summary>Removes an existing assignment.</summary>
      /// <param name="fieldName">A DAC field name.</param>
      public void Remove(string fieldName)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => it.Result.LeftOperandName == fieldName));
        if (configuratorAssignment == null)
          return;
        this.Result.Remove(configuratorAssignment);
      }
    }

    /// <summary>Adds method parameter assignments.</summary>
    public interface IContainerFillerParameters
    {
      /// <summary>
      /// Adds a new method parameter assignment from a boolean value.
      /// </summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      void Add(string parameterName, bool? value);

      /// <summary>
      /// Adds a new method parameter assignment from an integer value.
      /// </summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      void Add(string parameterName, int? value);

      /// <summary>
      /// Adds a new method parameter assignment from a decimal value.
      /// </summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      void Add(string parameterName, Decimal? value);

      /// <summary>
      /// Adds a new method parameter assignment from a string value.
      /// </summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      void Add(string parameterName, string value);

      /// <summary>Adds a new parameter assignment.</summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      /// <param name="config">A function that configures the current parameter assignment.</param>
      void Add(
        string parameterName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config);
    }

    /// <summary>
    /// Adds, replaces, and removes method parameter assignments.
    /// </summary>
    public class ParameterContainerAdjusterAssignments : 
      BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerParameters
    {
      internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> Result { get; }

      internal ParameterContainerAdjusterAssignments(
        List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> assignments)
      {
        this.Result = assignments;
      }

      /// <inheritdoc />
      public void Add(string parameterName, bool? value)
      {
        this.Add(parameterName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string parameterName, int? value)
      {
        this.Add(parameterName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string parameterName, Decimal? value)
      {
        this.Add(parameterName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(string parameterName, string value)
      {
        this.Add(parameterName, (Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.Assignment.IConfigured) c.SetFromValue((object) value)));
      }

      /// <inheritdoc />
      public void Add(
        string parameterName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(false, parameterName);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => string.Equals(it.Result.LeftOperandName, parameterName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Parameter Assignment {parameterName} already exists.");
        this.Result.Add(configuratorAssignment);
      }

      /// <summary>
      /// Replaces an existing parameter assignment with the new one.
      /// </summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      /// <param name="config">A function that configures the current parameter assignment.</param>
      public void Replace(
        string parameterName,
        Func<BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand, BoundedTo<TGraph, TPrimary>.Assignment.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => it.Result.LeftOperandName == parameterName));
        if (configuratorAssignment1 != null)
          this.Result.Remove(configuratorAssignment1);
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment2 = new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(false, parameterName);
        BoundedTo<TGraph, TPrimary>.Assignment.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Assignment.INeedRightOperand) configuratorAssignment2);
        this.Result.Add(configuratorAssignment2);
      }

      /// <summary>Removes an existing parameter assignment.</summary>
      /// <param name="parameterName">A name of the method parameter.</param>
      public void Remove(string parameterName)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment configuratorAssignment = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, bool>) (it => it.Result.LeftOperandName == parameterName));
        if (configuratorAssignment == null)
          return;
        this.Result.Remove(configuratorAssignment);
      }
    }
  }

  public abstract class BaseCompositeState : BoundedTo<TGraph, TPrimary>.BaseFlowStep
  {
    public List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase> States { get; protected internal set; } = new List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>();
  }

  public abstract class BaseFlowStep : ICloneable
  {
    public List<BoundedTo<TGraph, TPrimary>.FieldState> FieldStates { get; protected internal set; } = new List<BoundedTo<TGraph, TPrimary>.FieldState>();

    public List<BoundedTo<TGraph, TPrimary>.ActionState> Actions { get; protected internal set; } = new List<BoundedTo<TGraph, TPrimary>.ActionState>();

    public List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler> EventHandlers { get; protected internal set; } = new List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler>();

    public List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> OnEnterFieldAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    public List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> OnLeaveFieldAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    public string NextStateId { get; set; }

    public string Identifier { get; set; }

    public string Description { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition SkipCondition { get; set; }

    internal abstract BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase ToConfigurator();

    internal abstract Readonly.BaseFlowStep AsReadonly();

    public abstract object Clone();

    /// <summary>Configures the current state.</summary>
    public interface INeedAnyConfigState
    {
      /// <summary>Specifies behavior of actions in the current state.</summary>
      /// <param name="containerFiller">A function that configures state of actions.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to add the Assign and AutoAssign actions and remove the Open action to the current state of the workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// states.Update(States.New, state =&gt; state
      ///     .WithActions(actions =&gt;
      ///     {
      ///         actions.Add(actionAssign, a =&gt; a
      ///             .IsDuplicatedInToolbar());
      /// 
      ///         actions.Add(actionAutoAssign, a =&gt; a
      ///             .IsAutoAction(ownerNotNullCondition));
      ///         actions.Remove("Open");
      ///     }
      /// ));</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions> containerFiller);

      /// <summary>Specifies behavior of fields in the current state.</summary>
      /// <param name="containerFiller">A function that configures states of fields.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to update combo box values in the CROpportunity.resolution field in the New state of a workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// states.Update(States.New, state =&gt; state
      ///     .WithFieldStates(fields =&gt;
      ///     {
      ///         fields.UpdateField&lt;CROpportunity.resolution&gt;(field =&gt;
      ///             field.WithDefaultValue(_reasonUnassign).ComboBoxValues(_reasonUnassign, _reasonCanceled, _reasonOther, _reasonRejected));
      ///     })
      /// );</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerFiller);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithDescription(
        string description);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceFirst();

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceBefore<TStateBeforeId>() where TStateBeforeId : IConstant<string>, new();

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceBefore(string stateBeforeId);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceAfter<TStateAfterId>() where TStateAfterId : IConstant<string>, new();

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceAfter(string stateAfterId);

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter);

      /// <summary>
      /// Sets an ordered list of primary DAC fields that will be updated when entity enters the current state.
      /// </summary>
      /// <param name="containerFillerFields">A list of fields to be updated.</param>
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields);

      /// <summary>
      /// Sets an ordered list of primary DAC fields that will be updated when entity leaves the current state.
      /// </summary>
      /// <param name="containerFillerFields">A list of fields to be updated.</param>
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields);
    }

    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    internal interface IStateResult
    {
      BoundedTo<TGraph, TPrimary>.BaseFlowStep GetResult();
    }

    public abstract class ConfiguratorStateBase : 
      IOrderableWorkflowElement,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult,
      ICloneable
    {
      public MoveObjectInCollection MoveBefore { get; set; }

      public string NearKey { get; set; }

      public abstract string Key { get; }

      internal abstract string GetStateIdentifier();

      public abstract BoundedTo<TGraph, TPrimary>.BaseFlowStep GetResult();

      public abstract object Clone();
    }

    public class ConfiguratorState<TResult> : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult
      where TResult : BoundedTo<TGraph, TPrimary>.BaseFlowStep, new()
    {
      public override string Key => this.Result.Identifier;

      internal TResult Result { get; }

      internal ConfiguratorState(string stateId)
      {
        TResult result = new TResult();
        result.Identifier = stateId;
        this.Result = result;
      }

      internal ConfiguratorState(TResult state) => this.Result = state;

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions containerAdjusterActions = new BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions(this.Result.Actions);
        containerAdjuster(containerAdjusterActions);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields containerAdjusterFields = new BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields(this.Result.FieldStates);
        containerAdjuster(containerAdjusterFields);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.ContainerAdjusterHandlers adjusterHandlers = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.ContainerAdjusterHandlers(this.Result.EventHandlers);
        containerAdjuster((BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers) adjusterHandlers);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithDescription(
        string description)
      {
        this.Result.Description = description;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        this.Result.SkipCondition = skipCondition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceFirst()
      {
        this.MoveBefore = MoveObjectInCollection.First;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceBefore<TStateBeforeId>() where TStateBeforeId : IConstant<string>, new()
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = new TStateBeforeId().Value;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceBefore(
        string stateBeforeId)
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = stateBeforeId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore)
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = (stateBefore as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase).GetStateIdentifier();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceAfter<TStateAfterId>() where TStateAfterId : IConstant<string>, new()
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = new TStateAfterId().Value;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceAfter(
        string stateAfterId)
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = stateAfterId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter)
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = (stateAfter as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase).GetStateIdentifier();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnEnterFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult> WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnLeaveFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      internal override string GetStateIdentifier() => this.Result.Identifier;

      public override BoundedTo<TGraph, TPrimary>.BaseFlowStep GetResult()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep) this.Result;
      }

      public override object Clone()
      {
        return (object) new BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<TResult>(this.Result.Clone() as TResult);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult.GetResult()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep) this.Result;
      }
    }

    /// <summary>Adds configurations of worklfow states.</summary>
    public interface IContainerFillerStates
    {
      /// <summary>Adds a new configuration of a workflow state to the current workflow.</summary>
      /// <typeparam name="TStateId">A value of the state property field that corresponds to the current workflow state.</typeparam>
      /// <param name="config">A function which specifies behavior of the document in the current workflow state.</param>
      void Add<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new();

      /// <summary>Adds a new configuration of a workflow state to the current workflow.</summary>
      /// <param name="stateId">Value of the state property field that corresponds to current state.</param>
      /// <param name="config">A function which specifies behavior of the document in the current workflow state.</param>
      void Add(
        string stateId,
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null);

      /// <summary>Adds a new workflow state to the current workflow.</summary>
      /// <param name="state">A configured workflow state.</param>
      void Add(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured state);

      void AddSequence<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new();

      void AddSequence(
        string sequenceId,
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null);
    }

    /// <summary>
    /// Adds, replaces, updates, and removes configurations of workflow states.
    /// </summary>
    public class ContainerAdjusterStates : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates
    {
      internal List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase> Result { get; }

      internal ContainerAdjusterStates(
        List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase> states)
      {
        this.Result = states;
      }

      /// <inheritdoc />
      public void Add<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new()
      {
        this.Add(new TStateId().Value, config);
      }

      /// <inheritdoc />
      public void Add(
        string stateId,
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState configuratorState = new BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState(stateId);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig) configuratorState);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => string.Equals(it.GetStateIdentifier(), stateId, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Flow state {stateId} already exists.");
        if (WebConfig.EnableWorkflowValidationOnStartup && configuratorState.Result.IsInitial && this.Result.OfType<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>().Any<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>((Func<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState, bool>) (it => it.Result.IsInitial)))
          throw new ArgumentException("Initial flow state already exists.");
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) configuratorState);
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured state)
      {
        BoundedTo<TGraph, TPrimary>.FlowState flowState = ((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<BoundedTo<TGraph, TPrimary>.FlowState>) state).Result;
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => string.Equals(it.GetStateIdentifier(), flowState.Identifier, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Flow state {flowState.Identifier} already exists.");
        if (WebConfig.EnableWorkflowValidationOnStartup && flowState.IsInitial && this.Result.OfType<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>().Any<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>((Func<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState, bool>) (it => it.Result.IsInitial)))
          throw new ArgumentException("Initial flow state already exists.");
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) state);
      }

      public void AddSequence<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new()
      {
        BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence = new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(new TStateId().Value);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig) configuratorSequence);
        }
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) configuratorSequence);
      }

      public void AddSequence(
        string sequenceId,
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence = new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(sequenceId);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig) configuratorSequence);
        }
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) configuratorSequence);
      }

      /// <summary>Replaces an existing configuration of the workflow state with the provided one.</summary>
      /// <typeparam name="TStateId">A value of the state property field that corresponds to the current state.</typeparam>
      /// <param name="config">A function which specifies behavior of the document in the current workflow state.</param>
      public void Replace<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new()
      {
        this.Replace(new TStateId().Value, config);
      }

      /// <summary>Replaces an existing configuration of the workflow state with the provided one.</summary>
      /// <param name="stateId">A value of the state property field that corresponds to the current state.</param>
      /// <param name="config">A function which specifies behavior of the document in the current workflow state.</param>
      public void Replace(
        string stateId,
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase configuratorStateBase = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => it.GetStateIdentifier() == stateId));
        if (configuratorStateBase != null)
          this.Result.Remove(configuratorStateBase);
        BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState configuratorState = new BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState(stateId);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig) configuratorState);
        }
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) configuratorState);
      }

      public void ReplaceWithSequence(
        string sequenceId,
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase configuratorStateBase = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => it.GetStateIdentifier() == sequenceId));
        if (configuratorStateBase != null)
          this.Result.Remove(configuratorStateBase);
        BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence = new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(sequenceId);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig) configuratorSequence);
        }
        this.Result.Add((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) configuratorSequence);
      }

      /// <summary>Updates an existing configuration of the workflow state.</summary>
      /// <typeparam name="TStateId">A value of the state property field that corresponds to the current state.</typeparam>
      /// <param name="config">A function which overrides the configuration of the workflow state.</param>
      public void Update<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState, BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState> config)
        where TStateId : IConstant<string>, new()
      {
        this.Update(new TStateId().Value, config);
      }

      /// <summary>Updates an existing configuration of the workflow state.</summary>
      /// <param name="stateId">A value of the state property field that corresponds to the current state.</param>
      /// <param name="config">A function which overrides the configuration of the workflow state.</param>
      public void Update(
        string stateId,
        Func<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState, BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState> config)
      {
        BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState flowState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => it.GetStateIdentifier() == stateId)) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
        if (WebConfig.EnableWorkflowValidationOnStartup && flowState == null)
          throw new InvalidOperationException($"Flow state {stateId} not found.");
        if (flowState == null)
          return;
        BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState configuratorState = config(flowState);
        if (WebConfig.EnableWorkflowValidationOnStartup && flowState.Result.IsInitial && this.Result.OfType<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>().Any<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState>((Func<BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState, bool>) (it => it.Result.IsInitial && it != flowState)))
          throw new ArgumentException("Initial flow state already exists.");
      }

      public void UpdateSequence<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence, BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence> config)
        where TStateId : IConstant<string>, new()
      {
        this.UpdateSequence(new TStateId().Value, config);
      }

      public void UpdateSequence(
        string sequenceId,
        Func<BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence, BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence> config)
      {
        if (!(this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => it.GetStateIdentifier() == sequenceId)) is BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence1))
          return;
        BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence2 = config(configuratorSequence1);
      }

      /// <summary>Removes an existing state from the current workflow.</summary>
      /// <typeparam name="TStateId">A value of the state property field that corresponds to the current state.</typeparam>
      public void Remove<TStateId>() where TStateId : IConstant<string>, new()
      {
        this.Remove(new TStateId().Value);
      }

      /// <summary>Removes an existing state from the current workflow.</summary>
      /// <param name="stateId">A value of the state property field that corresponds to the current state.</param>
      public void Remove(string stateId)
      {
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase configuratorStateBase = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, bool>) (it => it.GetStateIdentifier() == stateId));
        if (configuratorStateBase == null)
          return;
        this.Result.Remove(configuratorStateBase);
      }
    }
  }

  public interface ISharedCondition
  {
    string Name { get; }
  }

  /// <summary>Defines a workflow condition.</summary>
  public class Condition : BoundedTo<TGraph, TPrimary>.ISharedCondition
  {
    internal bool IsSynthetic;
    private readonly BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions _container;

    public Func<IBqlTable, bool> Lambda { get; internal set; }

    public IBqlUnary BqlExpression { get; internal set; }

    public INamedCondition NamedCondition { get; internal set; }

    public bool? Constant { get; internal set; }

    public string Name { get; internal set; }

    internal virtual Readonly.Condition AsReadonly()
    {
      return Readonly.Condition.From<TGraph, TPrimary>(this);
    }

    internal Condition(
      BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions container)
    {
      this._container = container;
    }

    /// <summary>Sets an internal name of the current condition.</summary>
    /// <param name="name">The internal name of the condition.</param>
    /// <remarks>Every condition used on a screen must have an internal name.</remarks>
    /// <returns></returns>
    public BoundedTo<TGraph, TPrimary>.Condition WithSharedName(string name)
    {
      this.Name = name;
      this._container?.Add((BoundedTo<TGraph, TPrimary>.ISharedCondition) this);
      return this;
    }

    public static implicit operator BoundedTo<TGraph, TPrimary>.Condition(bool constant)
    {
      return new BoundedTo<TGraph, TPrimary>.Condition((BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions) null)
      {
        Constant = new bool?(constant),
        Name = constant.ToString().ToCapitalized()
      };
    }

    public static bool operator true(BoundedTo<TGraph, TPrimary>.Condition condition) => false;

    public static bool operator false(BoundedTo<TGraph, TPrimary>.Condition condition) => false;

    public static BoundedTo<TGraph, TPrimary>.Condition operator !(
      BoundedTo<TGraph, TPrimary>.Condition operand)
    {
      return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.Negate(operand);
    }

    public static BoundedTo<TGraph, TPrimary>.Condition operator &(
      BoundedTo<TGraph, TPrimary>.Condition left,
      BoundedTo<TGraph, TPrimary>.Condition right)
    {
      return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.Conjoin(left, right);
    }

    public static BoundedTo<TGraph, TPrimary>.Condition operator |(
      BoundedTo<TGraph, TPrimary>.Condition left,
      BoundedTo<TGraph, TPrimary>.Condition right)
    {
      return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.Disjoin(left, right);
    }

    /// <summary>Creates conditions.</summary>
    public class ConditionBuilder
    {
      private readonly BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions _container;

      public ConditionBuilder(
        BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions container)
      {
        this._container = container;
      }

      /// <summary>Creates a reusable condition based on the provided BQL statement.</summary>
      /// <typeparam name="TBqlCondition">A condition in the form of a BQL statement.</typeparam>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromBql<TBqlCondition>() where TBqlCondition : IBqlUnary, new()
      {
        return new BoundedTo<TGraph, TPrimary>.Condition(this._container)
        {
          BqlExpression = (IBqlUnary) new TBqlCondition()
        };
      }

      /// <summary>Creates a reusable condition based on the provided BQL statement.</summary>
      /// <typeparam name="TBqlCondition">A condition in the form of a BQL statement.</typeparam>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromBqlType(System.Type bqlCondition)
      {
        return new BoundedTo<TGraph, TPrimary>.Condition(this._container)
        {
          BqlExpression = (IBqlUnary) Activator.CreateInstance(bqlCondition)
        };
      }

      /// <summary>Creates a reusable condition based on a class that implements the <see cref="T:PX.Data.WorkflowAPI.INamedCondition" /> interface.</summary>
      /// <typeparam name="TCondition">A class that describes the condition.</typeparam>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromClass<TCondition>() where TCondition : INamedCondition, new()
      {
        return new BoundedTo<TGraph, TPrimary>.Condition(this._container)
        {
          NamedCondition = (INamedCondition) new TCondition()
        };
      }

      /// <summary>Creates a reusable condition by wrapping a boolean constant with a reusable condition object.</summary>
      /// <param name="constant">A boolean constant.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromConstant(bool constant)
      {
        return (BoundedTo<TGraph, TPrimary>.Condition) constant;
      }

      /// <summary>Creates a reusable condition which calls a boolean lambda function to evaluate a value against the current PXGraph object.</summary>
      /// <param name="expr">A boolean lambda function.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromLambda(Func<TPrimary, bool> expr)
      {
        return new BoundedTo<TGraph, TPrimary>.Condition(this._container)
        {
          Lambda = (Func<IBqlTable, bool>) (current => current != null && expr((TPrimary) current))
        };
      }

      /// <summary>Creates a reusable condition which calls a boolean lambda function to evaluate a value against the current PXGraph object.</summary>
      /// <param name="expr">A boolean lambda function.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Condition FromExpr(Func<TPrimary, bool> expr)
      {
        return this.FromLambda(expr);
      }

      /// <summary>Gets the configured condition object by the provided internal name.</summary>
      /// <param name="name">An internal name of a condition.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.ISharedCondition Get(string name)
      {
        BoundedTo<TGraph, TPrimary>.Condition.ContainerAdjusterConditions container = this._container;
        return container == null ? (BoundedTo<TGraph, TPrimary>.ISharedCondition) null : container.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ISharedCondition>((Func<BoundedTo<TGraph, TPrimary>.ISharedCondition, bool>) (it => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
      }

      public TPack GetPack<TPack>() where TPack : BoundedTo<TGraph, TPrimary>.Condition.Pack, new()
      {
        TPack pack = new TPack();
        pack.Builder = this;
        return pack;
      }
    }

    /// <summary>Adds conditions to the screen configuration.</summary>
    public interface IContainerFillerConditions
    {
      /// <summary>
      /// Adds a new configured shared condition to the screen configuration.
      /// </summary>
      /// <param name="name">An internal name of the new condition.</param>
      /// <param name="condition">A configured condition object.</param>
      /// <remarks>Only added condition objects (except condition in a form of boolean constants) are evaluated in runtime.</remarks>
      void Add(
        string name,
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>
      /// Adds a new configured shared condition to the screen configuration.
      /// </summary>
      /// <param name="condition">A function that configures a shared condition.</param>
      /// <remarks>Only added condition objects (except condition in a form of boolean constants) are evaluated in runtime.</remarks>
      void Add(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.ISharedCondition> condition);

      /// <summary>
      /// Adds a new configured shared condition to the screen configuration.
      /// </summary>
      /// <param name="condition">A configured condition object with an internal name.</param>
      /// <remarks>Only added condition objects (except condition in a form of boolean constants) are evaluated in runtime.</remarks>
      void Add(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);
    }

    /// <summary>
    /// Adds, replaces, and removes conditions in the screen configuration.
    /// </summary>
    public class ContainerAdjusterConditions : 
      BoundedTo<TGraph, TPrimary>.Condition.IContainerFillerConditions
    {
      private readonly WorkflowContext<TGraph, TPrimary> _context;

      internal List<BoundedTo<TGraph, TPrimary>.ISharedCondition> Result
      {
        get => this._context.Configurator.Result.SharedConditions;
      }

      internal ContainerAdjusterConditions(WorkflowContext<TGraph, TPrimary> context)
      {
        this._context = context;
      }

      public void Add(
        string name,
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        condition(new BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder(this)).WithSharedName(name);
      }

      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.ISharedCondition> condition)
      {
        this.Add(condition(new BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder(this)));
      }

      public void Add(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Any<BoundedTo<TGraph, TPrimary>.ISharedCondition>((Func<BoundedTo<TGraph, TPrimary>.ISharedCondition, bool>) (it => string.Equals(it.Name, condition.Name, StringComparison.Ordinal))))
        {
          if (condition is BoundedTo<TGraph, TPrimary>.Condition condition1 && condition1.IsSynthetic)
            return;
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw new ArgumentException($"Shared condition {condition.Name} already exists.");
        }
        this.Result.Add(condition);
      }

      /// <summary>Replaces an existing condition with the new one.</summary>
      /// <param name="name">An internal name of a condition to be replaced.</param>
      /// <param name="condition">A function which configures the shared condition.</param>
      public void Replace(
        string name,
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        BoundedTo<TGraph, TPrimary>.ISharedCondition sharedCondition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ISharedCondition>((Func<BoundedTo<TGraph, TPrimary>.ISharedCondition, bool>) (it => it.Name == name));
        if (sharedCondition != null)
          this.Result.Remove(sharedCondition);
        this.Result.Add((BoundedTo<TGraph, TPrimary>.ISharedCondition) condition(new BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder(this)).WithSharedName(name));
      }

      /// <summary>
      /// Removes an existing condition  from the screen configuration.
      /// </summary>
      /// <param name="name">An internal name of a condition.</param>
      public void Remove(string name)
      {
        BoundedTo<TGraph, TPrimary>.ISharedCondition sharedCondition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.ISharedCondition>((Func<BoundedTo<TGraph, TPrimary>.ISharedCondition, bool>) (it => it.Name == name));
        if (sharedCondition == null)
          return;
        this.Result.Remove(sharedCondition);
      }
    }

    public abstract class Pack
    {
      private readonly Dictionary<string, BoundedTo<TGraph, TPrimary>.Condition> _cache = new Dictionary<string, BoundedTo<TGraph, TPrimary>.Condition>();

      internal BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder Builder { get; set; }

      protected BoundedTo<TGraph, TPrimary>.Condition GetOrCreate(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> create,
        [CallerMemberName] string conditionName = null)
      {
        BoundedTo<TGraph, TPrimary>.Condition condition1;
        if (this._cache.TryGetValue(conditionName, out condition1))
          return condition1;
        BoundedTo<TGraph, TPrimary>.Condition condition2 = (BoundedTo<TGraph, TPrimary>.Condition) this.Builder.Get(conditionName);
        if (condition2 != null)
          return this._cache[conditionName] = condition2;
        BoundedTo<TGraph, TPrimary>.Condition condition3 = create(this.Builder).WithSharedName(conditionName);
        return this._cache[conditionName] = condition3;
      }
    }

    private class ConditionJoiner
    {
      public static BoundedTo<TGraph, TPrimary>.Condition Conjoin(
        BoundedTo<TGraph, TPrimary>.Condition left,
        BoundedTo<TGraph, TPrimary>.Condition right)
      {
        if (left != null)
        {
          bool? constant1 = left.Constant;
          bool flag1 = true;
          if (!(constant1.GetValueOrDefault() == flag1 & constant1.HasValue))
          {
            if (right != null)
            {
              bool? constant2 = right.Constant;
              bool flag2 = true;
              if (!(constant2.GetValueOrDefault() == flag2 & constant2.HasValue))
              {
                if (string.IsNullOrEmpty(left.Name))
                  throw new ArgumentException("Only named conditions can be conjuncted.");
                if (string.IsNullOrEmpty(right.Name))
                  throw new ArgumentException("Only named conditions can be conjuncted.");
                BoundedTo<TGraph, TPrimary>.Condition condition1 = new BoundedTo<TGraph, TPrimary>.Condition(left._container)
                {
                  IsSynthetic = true
                };
                bool? constant3 = left.Constant;
                if (constant3.HasValue)
                {
                  constant3 = right.Constant;
                  if (constant3.HasValue)
                  {
                    BoundedTo<TGraph, TPrimary>.Condition condition2 = condition1;
                    constant3 = left.Constant;
                    int num;
                    if (constant3.Value)
                    {
                      constant3 = right.Constant;
                      num = constant3.Value ? 1 : 0;
                    }
                    else
                      num = 0;
                    bool? nullable = new bool?(num != 0);
                    condition2.Constant = nullable;
                    goto label_24;
                  }
                }
                if (left.BqlExpression != null && right.BqlExpression != null)
                {
                  // ISSUE: method reference
                  // ISSUE: type reference
                  condition1.BqlExpression = GenericCall.Of<IBqlUnary>(Expression.Lambda<Func<IBqlUnary>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.BqlConjoin), __typeref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner)), Array.Empty<Expression>()))).ButWith(left.BqlExpression.GetType(), new System.Type[1]
                  {
                    right.BqlExpression.GetType()
                  });
                }
                else if (left.Lambda != null && right.Lambda != null)
                  condition1.Lambda = Func.Conjoin<IBqlTable>(left.Lambda, right.Lambda);
                else
                  condition1.NamedCondition = left.NamedCondition == null || right.NamedCondition == null ? (INamedCondition) BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Conjoin(BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Wrap(left), BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Wrap(right)) : (INamedCondition) BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Conjoin(left.NamedCondition, right.NamedCondition);
label_24:
                return condition1.WithSharedName($"( {left.Name} AND {right.Name} )");
              }
            }
            return left ?? throw new ArgumentNullException(nameof (left));
          }
        }
        return right ?? throw new ArgumentNullException(nameof (right));
      }

      public static BoundedTo<TGraph, TPrimary>.Condition Disjoin(
        BoundedTo<TGraph, TPrimary>.Condition left,
        BoundedTo<TGraph, TPrimary>.Condition right)
      {
        if (left != null)
        {
          bool? constant = left.Constant;
          bool flag = false;
          if (constant.GetValueOrDefault() == flag & constant.HasValue)
            return right ?? throw new ArgumentNullException(nameof (right));
        }
        if (right != null)
        {
          bool? constant = right.Constant;
          bool flag = false;
          if (constant.GetValueOrDefault() == flag & constant.HasValue)
            return left ?? throw new ArgumentNullException(nameof (left));
        }
        if (left == null)
          throw new ArgumentNullException(nameof (left));
        if (right == null)
          throw new ArgumentNullException(nameof (right));
        if (string.IsNullOrEmpty(left.Name))
          throw new ArgumentException("Only named conditions can be disjuncted.");
        if (string.IsNullOrEmpty(right.Name))
          throw new ArgumentException("Only named conditions can be disjuncted.");
        BoundedTo<TGraph, TPrimary>.Condition condition1 = new BoundedTo<TGraph, TPrimary>.Condition(left._container)
        {
          IsSynthetic = true
        };
        bool? constant1 = left.Constant;
        if (constant1.HasValue)
        {
          constant1 = right.Constant;
          if (constant1.HasValue)
          {
            BoundedTo<TGraph, TPrimary>.Condition condition2 = condition1;
            constant1 = left.Constant;
            int num;
            if (!constant1.Value)
            {
              constant1 = right.Constant;
              num = constant1.Value ? 1 : 0;
            }
            else
              num = 1;
            bool? nullable = new bool?(num != 0);
            condition2.Constant = nullable;
            goto label_28;
          }
        }
        if (left.BqlExpression != null && right.BqlExpression != null)
        {
          // ISSUE: method reference
          // ISSUE: type reference
          condition1.BqlExpression = GenericCall.Of<IBqlUnary>(Expression.Lambda<Func<IBqlUnary>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.BqlDisjoin), __typeref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner)), Array.Empty<Expression>()))).ButWith(left.BqlExpression.GetType(), new System.Type[1]
          {
            right.BqlExpression.GetType()
          });
        }
        else if (left.Lambda != null && right.Lambda != null)
          condition1.Lambda = Func.Disjoin<IBqlTable>(left.Lambda, right.Lambda);
        else
          condition1.NamedCondition = left.NamedCondition == null || right.NamedCondition == null ? (INamedCondition) BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Disjoin(BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Wrap(left), BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Wrap(right)) : (INamedCondition) BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Disjoin(left.NamedCondition, right.NamedCondition);
label_28:
        return condition1.WithSharedName($"( {left.Name} OR {right.Name} )");
      }

      public static BoundedTo<TGraph, TPrimary>.Condition Negate(
        BoundedTo<TGraph, TPrimary>.Condition operand)
      {
        if (operand == null)
          throw new ArgumentNullException(nameof (operand));
        BoundedTo<TGraph, TPrimary>.Condition condition = !string.IsNullOrEmpty(operand.Name) ? new BoundedTo<TGraph, TPrimary>.Condition(operand._container)
        {
          IsSynthetic = true
        } : throw new ArgumentException("Only named conditions can be negated.");
        if (operand.Constant.HasValue)
          condition.Constant = new bool?(!operand.Constant.Value);
        else if (operand.BqlExpression != null)
        {
          // ISSUE: method reference
          // ISSUE: type reference
          condition.BqlExpression = GenericCall.Of<IBqlUnary>(Expression.Lambda<Func<IBqlUnary>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.BqlNegate), __typeref (BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner)), Array.Empty<Expression>()))).ButWith(operand.BqlExpression.GetType(), Array.Empty<System.Type>());
        }
        else if (operand.Lambda != null)
          condition.Lambda = Func.Negate<IBqlTable>(operand.Lambda);
        else
          condition.NamedCondition = operand.NamedCondition != null ? (INamedCondition) BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.Negate(operand.NamedCondition) : throw new InvalidOperationException();
        return condition.WithSharedName($"NOT( {operand.Name} )");
      }

      private static IBqlUnary BqlConjoin<TLeft, TRight>()
        where TLeft : class, IBqlUnary, new()
        where TRight : class, IBqlUnary, new()
      {
        return (IBqlUnary) new Where2<TLeft, PX.Data.And<TRight>>();
      }

      private static IBqlUnary BqlDisjoin<TLeft, TRight>()
        where TLeft : class, IBqlUnary, new()
        where TRight : class, IBqlUnary, new()
      {
        return (IBqlUnary) new Where2<TLeft, PX.Data.Or<TRight>>();
      }

      private static IBqlUnary BqlNegate<TOperand>() where TOperand : class, IBqlUnary, new()
      {
        return (IBqlUnary) new Not<TOperand>();
      }

      private class NamedConditionWrapper : INamedCondition
      {
        private readonly INamedCondition _left;
        private readonly INamedCondition _right;
        private readonly bool? _isConjunction;

        public static INamedCondition Wrap(BoundedTo<TGraph, TPrimary>.Condition c)
        {
          if (c.Constant.HasValue)
            return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.FromConstant(c.Constant.Value);
          if (c.BqlExpression != null)
            return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.FromBql(c.BqlExpression);
          if (c.Lambda != null)
            return BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.FromLambda(c.Lambda);
          return c.NamedCondition != null ? c.NamedCondition : throw new InvalidOperationException();
        }

        private static INamedCondition FromConstant(bool constant)
        {
          return (INamedCondition) new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.ConstantWrapper(constant);
        }

        private static INamedCondition FromLambda(Func<IBqlTable, bool> lambda)
        {
          return (INamedCondition) new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.LambdaWrapper(lambda);
        }

        private static INamedCondition FromBql(IBqlUnary bql)
        {
          return (INamedCondition) new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper.BqlWrapper(bql);
        }

        private NamedConditionWrapper(
          bool? isConjunction,
          INamedCondition left,
          INamedCondition right)
        {
          this._isConjunction = isConjunction;
          this._left = left;
          this._right = right;
        }

        bool INamedCondition.Eval(PXCache cache, object current)
        {
          if (!this._isConjunction.HasValue)
            return !this._left.Eval(cache, current);
          return !this._isConjunction.Value ? this._left.Eval(cache, current) || this._right.Eval(cache, current) : this._left.Eval(cache, current) && this._right.Eval(cache, current);
        }

        public static BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper Conjoin(
          INamedCondition left,
          INamedCondition right)
        {
          return new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper(new bool?(true), left, right);
        }

        public static BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper Disjoin(
          INamedCondition left,
          INamedCondition right)
        {
          return new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper(new bool?(false), left, right);
        }

        public static BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper Negate(
          INamedCondition operand)
        {
          return new BoundedTo<TGraph, TPrimary>.Condition.ConditionJoiner.NamedConditionWrapper(new bool?(), operand, (INamedCondition) null);
        }

        private class ConstantWrapper : INamedCondition
        {
          private readonly bool _constant;

          public ConstantWrapper(bool constant) => this._constant = constant;

          bool INamedCondition.Eval(PXCache cache, object current) => this._constant;
        }

        private class LambdaWrapper : INamedCondition
        {
          private readonly Func<IBqlTable, bool> _lambda;

          public LambdaWrapper(Func<IBqlTable, bool> lambda) => this._lambda = lambda;

          bool INamedCondition.Eval(PXCache cache, object current)
          {
            return this._lambda((IBqlTable) current);
          }
        }

        private class BqlWrapper : INamedCondition
        {
          private readonly IBqlUnary _bql;

          public BqlWrapper(IBqlUnary bql) => this._bql = bql;

          bool INamedCondition.Eval(PXCache cache, object current)
          {
            bool? result = new bool?();
            object obj = (object) null;
            BqlFormula.Verify(cache, current, (IBqlCreator) this._bql, ref result, ref obj);
            bool? nullable = result;
            bool flag = true;
            return nullable.GetValueOrDefault() == flag & nullable.HasValue;
          }
        }
      }
    }
  }

  /// <summary>Defines a dynamic field state for a workflow.</summary>
  public class DynamicFieldState
  {
    public System.Type Table { get; set; }

    public string FieldName { get; set; }

    public Dictionary<string, ComboBoxItemsModification> ComboBoxValuesModifications { get; set; } = new Dictionary<string, ComboBoxItemsModification>();

    public BoundedTo<TGraph, TPrimary>.Condition Disabled { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition Hidden { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition IsRequired { get; set; }

    public string DisplayName { get; set; }

    public bool SkipExistenceCheck { get; set; }

    public bool IsFromSchema { get; set; }

    public string DefaultValue { get; set; }

    internal Readonly.DynamicFieldState AsReadonly()
    {
      return Readonly.DynamicFieldState.From<TGraph, TPrimary>(this);
    }

    /// <summary>Configures the current DAC field.</summary>
    public interface INeedAnyConfigField
    {
      /// <summary>
      /// Specifies that the current DAC field should be hidden when <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">A function which configures the condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>
      /// Specifies that the current DAC field should be hidden when <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">A configured condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>
      /// Specifies that the current DAC field should always be hidden. Use this method for auto-run actions.
      /// </summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsHiddenAlways();

      /// <summary>
      /// Specifies that the current DAC field should be disabled when the <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">A function that configures the condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>
      /// Specifies that the current DAC field should be disabled when the <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">A configured condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>
      /// Specifies that the current DAC field should always be disabled.
      /// </summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsDisabledAlways();

      /// <summary>
      /// Specifies that the current DAC field should be required when the <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">Function that configures the condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsRequiredWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition);

      /// <summary>
      /// Specifies that the current DAC field should be required when the <paramref name="condition" /> equals <see langword="true" />.
      /// </summary>
      /// <param name="condition">Configured condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>
      /// Specifies that the current DAC field should always be required.
      /// </summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig IsRequiredAlways();

      /// <summary>
      /// For string DAC fields marked with the <see cref="T:PX.Data.PXStringListAttribute" />, adds an additional combo box value.
      /// </summary>
      /// <param name="key">An additional combo box value.</param>
      /// <param name="description">The display name of the combo box value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig SetComboValue(
        string key,
        string description);

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig SetComboValues(
        params (string key, string description)[] values);

      /// <summary>Sets the display name of a field.</summary>
      /// <param name="displayName">Display name of a field.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig DisplayName(
        string displayName);

      /// <summary>Sets the default value of a field.</summary>
      /// <param name="defaultValue">Default value of a field.</param>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig WithDefaultValue(
        object defaultValue);

      /// <summary>Sets the default value of a field as a string expression for the subsequent evaluation.</summary>
      /// <param name="defaultValue">Expression for evaluation the default value of a field.</param>
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig WithDefaultExpression(
        string expression);
    }

    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField,
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorField : 
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField,
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.DynamicFieldState Result { get; }

      internal ConfiguratorField(System.Type table, string fieldName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.DynamicFieldState()
        {
          Table = table,
          FieldName = fieldName
        };
      }

      internal ConfiguratorField(
        BoundedTo<TGraph, TPrimary>.DynamicFieldState fieldState)
      {
        this.Result = fieldState;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        throw new NotImplementedException("Inline conditions are not yet supported");
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Disabled = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsDisabledWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Disabled != null)
          this.Result.Disabled &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsDisabledWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Disabled != null)
          this.Result.Disabled |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Disabled = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsDisabledAlways()
      {
        this.Result.Disabled = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        throw new NotImplementedException("Inline conditions are not yet supported");
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Hidden = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsHiddenWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Hidden != null)
          this.Result.Hidden &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsHiddenWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Hidden != null)
          this.Result.Hidden |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Hidden = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsHiddenAlways()
      {
        this.Result.Hidden = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsRequiredWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        throw new NotImplementedException("Inline conditions are not yet supported");
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.IsRequired = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsRequiredWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.IsRequired != null)
          this.Result.IsRequired &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsRequiredWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.IsRequired != null)
          this.Result.IsRequired |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.IsRequired = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField IsRequiredAlways()
      {
        this.Result.IsRequired = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField SetComboValue(
        string key,
        string description)
      {
        this.Result.ComboBoxValuesModifications.Remove(key);
        this.Result.ComboBoxValuesModifications.Add(key, new ComboBoxItemsModification()
        {
          Description = description,
          ID = key,
          Action = ComboBoxItemsModificationAction.Set
        });
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField SetComboValues(
        params (string key, string description)[] values)
      {
        foreach ((string key, string description) tuple in values)
        {
          this.Result.ComboBoxValuesModifications.Remove(tuple.key);
          this.Result.ComboBoxValuesModifications.Add(tuple.key, new ComboBoxItemsModification()
          {
            Description = tuple.description,
            ID = tuple.key,
            Action = ComboBoxItemsModificationAction.Set
          });
        }
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField DisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField WithDefaultValue(
        object defaultValue)
      {
        this.Result.DefaultValue = defaultValue.ToString();
        this.Result.IsFromSchema = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField WithDefaultExpression(
        string expression)
      {
        this.Result.DefaultValue = expression;
        this.Result.IsFromSchema = false;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsDisabledWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsDisabledWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsDisabledWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsDisabledWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsDisabledAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsDisabledAlways();
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsHiddenWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsHiddenWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsHiddenWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsHiddenAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsHiddenAlways();
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsRequiredWhen(
        Func<BoundedTo<TGraph, TPrimary>.Condition.ConditionBuilder, BoundedTo<TGraph, TPrimary>.Condition> condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsRequiredWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsRequiredWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.IsRequiredAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.IsRequiredAlways();
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.SetComboValue(
        string key,
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.SetComboValue(key, description);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.SetComboValues(
        params (string key, string description)[] values)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.SetComboValues(values);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.DisplayName(displayName);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.WithDefaultValue(
        object defaultValue)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.WithDefaultValue(defaultValue);
      }

      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField.WithDefaultExpression(
        string expression)
      {
        return (BoundedTo<TGraph, TPrimary>.DynamicFieldState.IAllowOptionalConfig) this.WithDefaultExpression(expression);
      }
    }

    /// <summary>Adds configurations for existing DAC fields.</summary>
    public interface IContainerFillerFields
    {
      /// <summary>Adds a new configuration for an existing DAC field.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function which configures behavior of the DAC field.</param>
      void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null)
        where TField : IBqlField;

      /// <summary>Adds a new configuration for an existing DAC field.</summary>
      /// <typeparam name="TField">&gt;A DAC type.</typeparam>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function which configures behavior of the DAC field.</param>
      void Add<TTable>(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null,
        bool skipExistenceCheck = false)
        where TTable : IBqlTable;

      /// <summary>Adds a new configuration for an existing DAC field.</summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function which configures behavior of the DAC field.</param>
      void Add(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null,
        bool skipExistenceCheck = false);
    }

    /// <summary>
    /// Adds, replaces, updates, and removes configurations for existing DAC fields.
    /// </summary>
    public class ContainerAdjusterFields : 
      BoundedTo<TGraph, TPrimary>.DynamicFieldState.IContainerFillerFields
    {
      internal List<BoundedTo<TGraph, TPrimary>.DynamicFieldState> Result { get; }

      internal ContainerAdjusterFields(
        List<BoundedTo<TGraph, TPrimary>.DynamicFieldState> fieldStates)
      {
        this.Result = fieldStates;
      }

      /// <inheritdoc />
      public void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null)
        where TField : IBqlField
      {
        this.Add(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config, false);
      }

      /// <inheritdoc />
      public void Add<TTable>(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null,
        bool skipExistenceCheck = false)
        where TTable : IBqlTable
      {
        this.Add(typeof (TTable), fieldName, config, skipExistenceCheck);
      }

      /// <inheritdoc />
      public void Add(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null,
        bool skipExistenceCheck = false)
      {
        BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField(table, fieldName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.DynamicFieldState>((Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState, bool>) (it => string.Equals(it.FieldName, fieldName, StringComparison.OrdinalIgnoreCase) && it.Table == table)))
          throw new ArgumentException($"Field configuration {table.Name}.{fieldName} already exists.");
        configuratorField.Result.SkipExistenceCheck = skipExistenceCheck;
        this.Result.Add(configuratorField.Result);
      }

      /// <summary>
      /// Replaces an existing configuration of a field with the provided one.
      /// </summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function which configures behavior of an existing field.</param>
      public void Replace<TField>(
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null)
        where TField : IBqlField
      {
        this.Replace(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config);
      }

      /// <summary>
      /// Replaces an existing configuration of a field with the provided one.
      /// </summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function which configures behavior of an existing field.</param>
      public void Replace(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.DynamicFieldState dynamicFieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.DynamicFieldState>((Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (dynamicFieldState != null)
          this.Result.Remove(dynamicFieldState);
        BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField(table, fieldName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.DynamicFieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.DynamicFieldState.INeedAnyConfigField) configuratorField);
        }
        this.Result.Add(configuratorField.Result);
      }

      /// <summary>Updates an existing configuration of a field.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function which configures behavior of an existing field.</param>
      public void Update<TField>(
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField> config)
        where TField : IBqlField
      {
        this.Update(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config);
      }

      /// <summary>Updates an existing configuration of a field.</summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">A DAC field name.</param>
      /// <param name="config">A function which configures behavior of an existing field.</param>
      public void Update(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField, BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField> config)
      {
        BoundedTo<TGraph, TPrimary>.DynamicFieldState fieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.DynamicFieldState>((Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (WebConfig.EnableWorkflowValidationOnStartup && fieldState == null)
          throw new InvalidOperationException($"Field configuration {table.Name}.{fieldName} not found.");
        if (fieldState == null)
          return;
        BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField configuratorField1 = new BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField(fieldState);
        BoundedTo<TGraph, TPrimary>.DynamicFieldState.ConfiguratorField configuratorField2 = config(configuratorField1);
      }

      /// <summary>Removes an existing configuration of a field.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      public void Remove<TField>() where TField : IBqlField
      {
        this.Remove(BqlCommand.GetItemType<TField>(), typeof (TField).Name);
      }

      /// <summary>Removes an existing configuration of a field.</summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">A DAC field name.</param>
      public void Remove(System.Type table, string fieldName)
      {
        BoundedTo<TGraph, TPrimary>.DynamicFieldState dynamicFieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.DynamicFieldState>((Func<BoundedTo<TGraph, TPrimary>.DynamicFieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (dynamicFieldState == null)
          return;
        this.Result.Remove(dynamicFieldState);
      }
    }
  }

  /// <summary>Defines a workflow field state.</summary>
  public class FieldState : IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.FieldState>
  {
    public System.Type Table { get; set; }

    public string FieldName { get; set; }

    public bool IsDisabled { get; set; }

    public bool IsHidden { get; set; }

    public bool IsRequired { get; set; }

    public bool AllFields { get; set; }

    public string DefaultValue { get; set; }

    public string ComboBoxValues { get; set; }

    internal Readonly.FieldState AsReadonly() => Readonly.FieldState.From<TGraph, TPrimary>(this);

    public BoundedTo<TGraph, TPrimary>.FieldState CreateCopy()
    {
      return (BoundedTo<TGraph, TPrimary>.FieldState) this.MemberwiseClone();
    }

    /// <summary>Creates reusable configurations of field states.</summary>
    public class FieldStateBuilder
    {
      /// <summary>Creates a new reusable configuration of a field state.</summary>
      /// <typeparam name="TField">A DAC field which state is configured.</typeparam>
      /// <param name="initializer">A function which configures behavior of an existing field in the current state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.FieldState.IConfigured Create<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> initializer = null)
        where TField : IBqlField
      {
        return this.Create(BqlCommand.GetItemType<TField>(), typeof (TField).Name, initializer);
      }

      /// <summary>Creates a new reusable configuration of a field state.</summary>
      /// <typeparam name="TTable">A DAC type.</typeparam>
      /// <param name="fieldName">Name of a DAC field.</param>
      /// <param name="initializer">A function which configures behavior of an existing field in the current state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.FieldState.IConfigured Create<TTable>(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> initializer = null)
        where TTable : IBqlTable
      {
        return this.Create(typeof (TTable), fieldName, initializer);
      }

      /// <summary>Creates a reusable configuration of a provided DAC.</summary>
      /// <typeparam name="TTable">A DAC type.</typeparam>
      /// <param name="initializer">A function which configures behavior of a DAC in the current state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.FieldState.IConfigured CreateTable<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> initializer)
        where TTable : IBqlTable
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField table = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(typeof (TTable), (string) null);
        BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) table);
        return (BoundedTo<TGraph, TPrimary>.FieldState.IConfigured) table;
      }

      /// <summary>Creates a reusable configuration for all fields of the provided DAC.</summary>
      /// <typeparam name="TTable">A DAC type.</typeparam>
      /// <param name="initializer">A functions which configures behavior of all fields of the provided DAC in the current state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.FieldState.IConfigured CreateAllFields<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> initializer)
        where TTable : IBqlTable
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField allFields = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(typeof (TTable), (string) null);
        allFields.Result.AllFields = true;
        BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) allFields);
        return (BoundedTo<TGraph, TPrimary>.FieldState.IConfigured) allFields;
      }

      /// <summary>Creates a new reusable configuration of a field state.</summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">Name of a DAC field.</param>
      /// <param name="initializer">A function which configures behavior of an existing field in the current state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.FieldState.IConfigured Create(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> initializer = null)
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(table, fieldName);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) configuratorField);
        }
        return (BoundedTo<TGraph, TPrimary>.FieldState.IConfigured) configuratorField;
      }
    }

    /// <summary>Configures a DAC field for the current state.</summary>
    public interface INeedAnyConfigField
    {
      /// <summary>Specifies that the current field or all fields of the DAC should be hidden in the current state.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig IsHidden();

      /// <summary>Specifies that the current field or all fields of the DAC should be disabled in the current state.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig IsDisabled();

      /// <summary>Specifies that the current field or all fields of the DAC should be required in the current state.</summary>
      /// <returns></returns>
      /// <example><para>Suppose you need to make the CROpportunity.ownerID field required in the Open state of a workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// states.Update(States.Open, state =&gt; state
      ///     .WithFieldStates(fields =&gt;
      ///     {
      ///           fields.AddField&lt;CROpportunity.ownerID&gt;(field =&gt;
      ///         field.IsRequired());
      ///     }
      /// ));</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig IsRequired();

      /// <summary>Specifies a default value for the current field.</summary>
      /// <param name="defaultValue">A default value of the field.</param>
      /// <remarks>This setting is applied only in the initial state of the workflow.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig DefaultValue(object defaultValue);

      /// <summary>Specified a list of available combo box values for the current field in current workflow state.</summary>
      /// <param name="v">A list of available combo box values.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to update the list of available combo box values for a field. The example is shown in the following code. _reason*** variables are string constants.</para>
      ///   <code title="Example" lang="CS">
      /// field.WithDefaultValue(_reasonUnassign).ComboBoxValues(_reasonUnassign, _reasonCanceled, _reasonOther, _reasonRejected));</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig ComboBoxValues(params string[] v);
    }

    /// <summary>
    /// Provides additional configuration of a DAC field for the current state.
    /// </summary>
    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField,
      BoundedTo<TGraph, TPrimary>.FieldState.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorField : 
      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField,
      BoundedTo<TGraph, TPrimary>.FieldState.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.FieldState Result { get; }

      internal ConfiguratorField(System.Type table, string fieldName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.FieldState()
        {
          Table = table,
          FieldName = fieldName
        };
      }

      internal ConfiguratorField(BoundedTo<TGraph, TPrimary>.FieldState fieldState)
      {
        this.Result = fieldState;
      }

      public BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField IsDisabled()
      {
        this.Result.IsDisabled = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField IsHidden()
      {
        this.Result.IsHidden = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField IsRequired()
      {
        this.Result.IsRequired = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField WithDefaultValue(
        object defaultValue)
      {
        this.Result.DefaultValue = defaultValue?.ToString();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField ComboBoxValues(
        params string[] v)
      {
        this.Result.ComboBoxValues = string.Join(";", v);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField.IsDisabled()
      {
        return (BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig) this.IsDisabled();
      }

      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField.IsHidden()
      {
        return (BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig) this.IsHidden();
      }

      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField.IsRequired()
      {
        return (BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig) this.IsRequired();
      }

      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField.DefaultValue(
        object defaultValue)
      {
        return (BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig) this.WithDefaultValue(defaultValue);
      }

      BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField.ComboBoxValues(
        params string[] v)
      {
        return (BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig) this.ComboBoxValues(v);
      }
    }

    /// <summary>Adds configurations of DAC fields.</summary>
    public interface IContainerFillerFields
    {
      /// <summary>Adds a new configuration of a DAC field. The configuration is applied to the field in the current workflow state.</summary>
      /// <typeparam name="TField">A DAC field.</typeparam>
      /// <param name="config">A function which configures field behavior in the current workflow state.</param>
      /// <remarks>If the current state has configuration for Table/All Fields of a DAC and a particular configuration for one or more field of the same DAC, such
      /// configuration overrides field behavior specified in Table/All Fields configurations for the same field.</remarks>
      void AddField<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TField : IBqlField;

      void AddField(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null);

      void AddFields<TFields>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TFields : ITypeArrayOf<IBqlField>, TypeArray.IsNotEmpty;

      /// <summary>
      /// Adds new DAC configuration, that will be applied in current state.
      /// </summary>
      /// <remarks>If the current state has configuration for Table/All Fields of a DAC and a particular configuration for one or more field of the same DAC, such
      /// configuration overrides field behavior specified in Table/All Fields configurations for the same field.</remarks>
      /// <typeparam name="TTable">DAC type.</typeparam>
      /// <param name="config">Functions that configures DAC behavior in the current state.</param>
      void AddTable<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTable : IBqlTable;

      /// <summary>
      /// Adds new DAC configuration, that will be applied in current state.
      /// </summary>
      /// <remarks>If the current state has configuration for Table/All Fields of a DAC and a particular configuration for one or more field of the same DAC, such
      /// configuration overrides field behavior specified in Table/All Fields configurations for the same field.</remarks>
      /// <param name="table">DAC type.</param>
      /// <param name="config">Functions that configures DAC behavior in the current state.</param>
      void AddTable(
        System.Type table,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null);

      void AddTables<TTables>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTables : ITypeArrayOf<IBqlTable>, TypeArray.IsNotEmpty;

      /// <summary>Adds a configuration for all fields in the provided DAC The configuration is applied to the fields in the current workflow state.</summary>
      /// <typeparam name="TTable">DAC type.</typeparam>
      /// <param name="config">A function which configures behavior of all fields of the provided DAC in the current workflow state.</param>
      void AddAllFields<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTable : IBqlTable;

      /// <summary>Adds a configuration for all fields in the provided DAC The configuration is applied to the fields in the current workflow state.</summary>
      /// <param name="table">DAC type.</param>
      /// <param name="config">A function which configures behavior of all fields of the provided DAC in the current workflow state.</param>
      void AddAllFields(
        System.Type table,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null);

      void AddAllFieldsFromTables<TTables>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTables : ITypeArrayOf<IBqlTable>, TypeArray.IsNotEmpty;
    }

    /// <summary>
    /// Adds, replaces, updates, and removes configurations of DAC fields.
    /// </summary>
    public class ContainerAdjusterFields : 
      BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields
    {
      internal List<BoundedTo<TGraph, TPrimary>.FieldState> Result { get; }

      internal ContainerAdjusterFields(
        List<BoundedTo<TGraph, TPrimary>.FieldState> fieldStates)
      {
        this.Result = fieldStates;
      }

      public void AddField<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TField : IBqlField
      {
        this.AddField(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config);
      }

      public void AddField(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(table, fieldName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => string.Equals(it.FieldName, fieldName, StringComparison.OrdinalIgnoreCase) && it.Table == table)))
          throw new ArgumentException($"Field state configuration {table.Name}.{fieldName} already exists.");
        this.Result.Add(configuratorField.Result);
      }

      public void AddFields<TFields>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TFields : ITypeArrayOf<IBqlField>, TypeArray.IsNotEmpty
      {
        foreach (System.Type field in TypeArrayOf<IBqlField>.CheckAndExtract(typeof (TFields), (string) null))
          this.AddField(BqlCommand.GetItemType(field), field.Name, config);
      }

      public void AddTable<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTable : IBqlTable
      {
        this.AddTable(typeof (TTable), config);
      }

      public void AddTable(
        System.Type table,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(table, (string) null);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => !it.AllFields && it.FieldName == null && it.Table == table)))
          throw new ArgumentException($"Field state configuration {table}.<TABLE> already exists.");
        this.Result.Add(configuratorField.Result);
      }

      public void AddTables<TTables>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTables : ITypeArrayOf<IBqlTable>, TypeArray.IsNotEmpty
      {
        foreach (System.Type table in TypeArrayOf<IBqlTable>.CheckAndExtract(typeof (TTables), (string) null))
          this.AddTable(table, config);
      }

      public void AddAllFields<TTable>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTable : IBqlTable
      {
        this.AddAllFields(typeof (TTable), config);
      }

      public void AddAllFields(
        System.Type table,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(table, (string) null);
        configuratorField.Result.AllFields = true;
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => it.AllFields && it.FieldName == null && it.Table == table)))
          throw new ArgumentException($"Field state configuration {table}.<TABLE> already exists.");
        this.Result.Add(configuratorField.Result);
      }

      public void AddAllFieldsFromTables<TTables>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config = null)
        where TTables : ITypeArrayOf<IBqlTable>, TypeArray.IsNotEmpty
      {
        foreach (System.Type table in TypeArrayOf<IBqlTable>.CheckAndExtract(typeof (TTables), (string) null))
          this.AddAllFields(table, config);
      }

      /// <summary>
      /// Replaces field configuration in current workflow state.
      /// </summary>
      /// <typeparam name="TField">DAC field.</typeparam>
      /// <param name="config">Functions that configures field behavior in the current state.</param>
      public void ReplaceField<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config)
        where TField : IBqlField
      {
        this.ReplaceField(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config);
      }

      /// <summary>
      /// Replaces field configuration in current workflow state.
      /// </summary>
      /// <param name="table">DAC type.</param>
      /// <param name="fieldName">Field name.</param>
      /// <param name="config">Functions that configures field behavior in the current state.</param>
      public void ReplaceField(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField, BoundedTo<TGraph, TPrimary>.FieldState.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.FieldState fieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (fieldState != null)
          this.Result.Remove(fieldState);
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(table, fieldName);
        BoundedTo<TGraph, TPrimary>.FieldState.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) configuratorField);
        this.Result.Add(configuratorField.Result);
      }

      /// <summary>
      /// Updates field configuration in current workflow state.
      /// </summary>
      /// <typeparam name="TField">DAC field.</typeparam>
      /// <param name="config">Functions that overrides field behavior in the current state.</param>
      public void UpdateField<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField, BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField> config)
        where TField : IBqlField
      {
        this.UpdateField(BqlCommand.GetItemType<TField>(), typeof (TField).Name, config);
      }

      /// <summary>
      /// Updates field configuration in current workflow state.
      /// </summary>
      /// <param name="table">DAC type.</param>
      /// <param name="fieldName">Field name.</param>
      /// <param name="config">Functions that overrides field behavior in the current state.</param>
      public void UpdateField(
        System.Type table,
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField, BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField> config)
      {
        BoundedTo<TGraph, TPrimary>.FieldState fieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (WebConfig.EnableWorkflowValidationOnStartup && fieldState == null)
          throw new InvalidOperationException($"Field state configuration {table.Name}.{fieldName} not found.");
        if (fieldState == null)
          return;
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField1 = new BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField(fieldState);
        BoundedTo<TGraph, TPrimary>.FieldState.ConfiguratorField configuratorField2 = config(configuratorField1);
      }

      /// <summary>
      /// Removes field configuration in current workflow state.
      /// </summary>
      /// <typeparam name="TField">DAC field.</typeparam>
      public void RemoveField<TField>() where TField : IBqlField
      {
        this.RemoveField(BqlCommand.GetItemType<TField>(), typeof (TField).Name);
      }

      /// <summary>
      /// Removes field configuration in current workflow state.
      /// </summary>
      /// <param name="table">DAC type.</param>
      /// <param name="fieldName">Field name.</param>
      public void RemoveField(System.Type table, string fieldName)
      {
        BoundedTo<TGraph, TPrimary>.FieldState fieldState = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, bool>) (it => it.FieldName == fieldName && it.Table == table));
        if (fieldState == null)
          return;
        this.Result.Remove(fieldState);
      }
    }
  }

  /// <summary>Defines a workflow state.</summary>
  public class FlowState : 
    BoundedTo<TGraph, TPrimary>.BaseFlowStep,
    IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.FlowState>
  {
    public bool IsInitial { get; set; }

    /// <summary>If true, an entity in the state cannot be saved.</summary>
    internal bool IsNonPersistent { get; set; }

    internal override BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase ToConfigurator()
    {
      return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) new BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState(this);
    }

    internal override Readonly.BaseFlowStep AsReadonly()
    {
      return (Readonly.BaseFlowStep) Readonly.FlowState.From<TGraph, TPrimary>(this);
    }

    public virtual BoundedTo<TGraph, TPrimary>.FlowState CreateCopy()
    {
      BoundedTo<TGraph, TPrimary>.FlowState copy = (BoundedTo<TGraph, TPrimary>.FlowState) this.MemberwiseClone();
      copy.Actions = this.Actions.Select<BoundedTo<TGraph, TPrimary>.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>((Func<BoundedTo<TGraph, TPrimary>.ActionState, BoundedTo<TGraph, TPrimary>.ActionState>) (it => it.CreateCopy())).ToList<BoundedTo<TGraph, TPrimary>.ActionState>();
      copy.FieldStates = this.FieldStates.Select<BoundedTo<TGraph, TPrimary>.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>((Func<BoundedTo<TGraph, TPrimary>.FieldState, BoundedTo<TGraph, TPrimary>.FieldState>) (it => it.CreateCopy())).ToList<BoundedTo<TGraph, TPrimary>.FieldState>();
      return copy;
    }

    public override object Clone() => (object) this.CreateCopy();

    /// <summary>Creates reusable workflow states.</summary>
    public class FlowStateBuilder
    {
      /// <summary>Creates a new reusable configured workflow state.</summary>
      /// <param name="flowState">A value of the state property field that corresponds to the current workflow state.</param>
      /// <param name="initializer">A function which specifies behavior of the document in the current workflow state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured Create(
        string flowState,
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> initializer = null)
      {
        BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState configuratorState = new BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState(flowState);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig) configuratorState);
        }
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured) configuratorState;
      }

      /// <summary>Creates a new reusable configured workflow state.</summary>
      /// <typeparam name="TStateId">A value of the state property field that corresponds to the current state.</typeparam>
      /// <param name="config">A function which specifies behavior of the document in the current workflow state.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured Create<TStateId>(
        Func<BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> config = null)
        where TStateId : IConstant<string>, new()
      {
        return this.Create(new TStateId().Value, config);
      }
    }

    /// <summary>Configures a workflow state.</summary>
    public interface INeedAnyFlowStateConfig : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState
    {
      /// <summary>Specifies that the current state must be initial in the current workflow.</summary>
      /// <remarks>Only one state in each workflow can be initial.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig IsInitial();

      /// <summary>Specifies that the current state must be initial in the current workflow and sets a state initializing action.</summary>
      /// <remarks>Only one state in each workflow can be initial.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig IsInitial(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> initializerSelector);

      /// <summary>Specifies that the current state must be initial in the current workflow and sets a state initializing action.</summary>
      /// <remarks>Only one state in each workflow can be initial.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig IsInitial<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> initializerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>Specifies that the current state must be non persistent in the current workflow.</summary>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig IsNonPersistent();

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions> containerFiller);

      /// <summary>Specifies behavior of fields in the current state.</summary>
      /// <param name="containerFiller">A function that configures states of fields.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to update combo box values in the CROpportunity.resolution field in the New state of a workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// states.Update(States.New, state =&gt; state
      ///     .WithFieldStates(fields =&gt;
      ///     {
      ///         fields.UpdateField&lt;CROpportunity.resolution&gt;(field =&gt;
      ///             field.WithDefaultValue(_reasonUnassign).ComboBoxValues(_reasonUnassign, _reasonCanceled, _reasonOther, _reasonRejected));
      ///     })
      /// );</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerFiller);

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithDescription(
        string description);

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition);

      /// <summary>
      /// Sets an ordered list of primary DAC fields that will be updated when entity enters the current state.
      /// </summary>
      /// <param name="containerFillerFields">A list of fields to be updated.</param>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields);

      /// <summary>
      /// Sets an ordered list of primary DAC fields that will be updated when entity leaves the current state.
      /// </summary>
      /// <param name="containerFillerFields">A list of fields to be updated.</param>
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields);
    }

    /// <summary>
    /// Provides additional configuration for a wokflow state.
    /// </summary>
    public interface IAllowOptionalFlowStateConfig : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured,
      BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig
    {
    }

    /// <summary>Defines a workflow state.</summary>
    public class ConfiguratorState : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<BoundedTo<TGraph, TPrimary>.FlowState>,
      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured,
      BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig
    {
      internal ConfiguratorState(string stateId)
        : base(stateId)
      {
      }

      internal ConfiguratorState(BoundedTo<TGraph, TPrimary>.FlowState state)
        : base(state)
      {
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState IsInitial()
      {
        this.Result.IsInitial = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState IsInitial(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> initializerSelector)
      {
        return this.IsInitial().MarkAsNonPersistent().WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions>) (acts => acts.Add(initializerSelector, (Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured>) (a => (BoundedTo<TGraph, TPrimary>.ActionState.IConfigured) a.IsAutoAction()))));
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState IsInitial<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> initializerSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        return this.IsInitial().MarkAsNonPersistent().WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions>) (acts => acts.Add<TExtension>(initializerSelector, (Func<BoundedTo<TGraph, TPrimary>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TGraph, TPrimary>.ActionState.IConfigured>) (a => (BoundedTo<TGraph, TPrimary>.ActionState.IConfigured) a.IsAutoAction()))));
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions> containerAdjuster)
      {
        return base.WithActions(containerAdjuster) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields> containerAdjuster)
      {
        return base.WithFieldStates(containerAdjuster) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerAdjuster)
      {
        return base.WithEventHandlers(containerAdjuster) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithDescription(
        string description)
      {
        return base.WithDescription(description) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        return base.IsSkippedWhen(skipCondition) as BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceFirst()
      {
        this.MoveBefore = MoveObjectInCollection.First;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceBefore<TStateBeforeId>() where TStateBeforeId : IConstant<string>, new()
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = new TStateBeforeId().Value;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceBefore(
        string stateBeforeId)
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = stateBeforeId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore)
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = (stateBefore as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase).GetStateIdentifier();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceAfter<TStateAfterId>() where TStateAfterId : IConstant<string>, new()
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = new TStateAfterId().Value;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceAfter(string stateAfterId)
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = stateAfterId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter)
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = (stateAfter as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase).GetStateIdentifier();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnEnterFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnLeaveFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      public override object Clone()
      {
        return (object) new BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState(this.Result.CreateCopy());
      }

      /// <summary>
      /// If you mark the state as non-persistent, you will not be able to save an entity in this state.
      /// </summary>
      public BoundedTo<TGraph, TPrimary>.FlowState.ConfiguratorState MarkAsNonPersistent()
      {
        this.Result.IsNonPersistent = true;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.IsInitial()
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.IsInitial();
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.IsInitial(
        Expression<Func<TGraph, PXAutoAction<TPrimary>>> initializerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.IsInitial(initializerSelector);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.IsInitial<TExtension>(
        Expression<Func<TExtension, PXAutoAction<TPrimary>>> initializerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.IsInitial<TExtension>(initializerSelector);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.IsNonPersistent()
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.MarkAsNonPersistent();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithFieldStates((System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithFieldStates((System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithEventHandlers(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithEventHandlers(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithDescription(
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithDescription(description);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.IsSkippedWhen(skipCondition);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithDescription(
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithDescription(description);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.IsSkippedWhen(skipCondition);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceFirst()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceFirst();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore<TStateBeforeId>()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceBefore<TStateBeforeId>();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore(
        string stateBeforeId)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateBeforeId);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceBefore(stateBefore);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter<TStateAfterId>()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter<TStateAfterId>();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter(
        string stateAfterId)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateAfterId);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateAfter);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithOnEnterAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithOnEnterAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithOnLeaveAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig BoundedTo<TGraph, TPrimary>.FlowState.INeedAnyFlowStateConfig.WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.FlowState.IAllowOptionalFlowStateConfig) this.WithOnLeaveAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }
    }
  }

  /// <summary>Defines a workflow dialog box.</summary>
  public class Form
  {
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public int ColumnsCount { get; set; }

    public string DacType { get; set; }

    public bool MapAllFields { get; set; }

    internal List<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField> Fields { get; set; } = new List<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>();

    internal Readonly.Form AsReadonly() => Readonly.Form.From<TGraph, TPrimary>(this);

    /// <summary>
    /// Creates reusable configurations of workflow dialog boxes.
    /// </summary>
    public class FormBuilder
    {
      private readonly WorkflowContext<TGraph, TPrimary> _context;

      public FormBuilder(WorkflowContext<TGraph, TPrimary> context) => this._context = context;

      /// <summary>Creates a new reusable configuration of a dialog box.</summary>
      /// <param name="formName">An internal name of the dialog box.</param>
      /// <param name="initializer">A function that descibes the dialog box appearance and field configuration.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Form.IConfigured Create(
        string formName,
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(formName);
        BoundedTo<TGraph, TPrimary>.Form.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm) configuratorForm);
        return (BoundedTo<TGraph, TPrimary>.Form.IConfigured) configuratorForm;
      }

      public BoundedTo<TGraph, TPrimary>.Form.IConfigured Create<TForm>(
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> initializer = null)
        where TForm : class, IWorkflowFormBqlTable, new()
      {
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(typeof (TForm).FullName);
        configuratorForm.WithFormClass<TForm>(true);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.Form.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm) configuratorForm);
        }
        return (BoundedTo<TGraph, TPrimary>.Form.IConfigured) configuratorForm;
      }

      /// <summary>Returns a configured reusable dialog box that has been previously configured.</summary>
      /// <param name="formName">An internal name of the dialog box.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Form.IConfigured Get(string formName)
      {
        BoundedTo<TGraph, TPrimary>.Form form = this._context.Configurator.Result.Forms.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => it.Name.Equals(formName, StringComparison.OrdinalIgnoreCase)));
        return form == null ? (BoundedTo<TGraph, TPrimary>.Form.IConfigured) null : (BoundedTo<TGraph, TPrimary>.Form.IConfigured) new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(form);
      }

      public BoundedTo<TGraph, TPrimary>.Form.IConfigured Get<TForm>() where TForm : class, IWorkflowFormBqlTable, new()
      {
        return this.Get(typeof (TForm).Name);
      }
    }

    /// <summary>Configures a workflow dialog box.</summary>
    public interface INeedAnyConfigForm
    {
      /// <summary>Specifies a number of columns to be used for displaying fields in the dialog box.</summary>
      /// <param name="cnt">A number of columns.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig ColumnsCount(int cnt);

      /// <summary>Specifies the title of the dialog box.</summary>
      /// <param name="prompt">A dialog box title.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig Prompt(string prompt);

      /// <summary>Specifies a list of form fields which are visible in the dialog box.</summary>
      /// <param name="containerFiller">A function that specifies configuration of form fields.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig WithFields(
        System.Action<BoundedTo<TGraph, TPrimary>.FormField.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig WithFormClass<T>(bool mapAllFields = true) where T : class, IWorkflowFormBqlTable, new();
    }

    /// <summary>
    /// Provides additional configuration of a workflow dialog box.
    /// </summary>
    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm,
      BoundedTo<TGraph, TPrimary>.Form.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorForm : 
      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm,
      BoundedTo<TGraph, TPrimary>.Form.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.Form Result { get; }

      internal ConfiguratorForm(string name)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.Form()
        {
          Name = name
        };
      }

      internal ConfiguratorForm(BoundedTo<TGraph, TPrimary>.Form form) => this.Result = form;

      public BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm WithColumnsCount(int cnt)
      {
        this.Result.ColumnsCount = cnt;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm WithPrompt(string prompt)
      {
        this.Result.DisplayName = prompt;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm WithFields(
        System.Action<BoundedTo<TGraph, TPrimary>.FormField.ContainerAdjusterFields> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.FormField.ContainerAdjusterFields containerAdjusterFields = new BoundedTo<TGraph, TPrimary>.FormField.ContainerAdjusterFields(this.Result.Fields);
        containerAdjuster(containerAdjusterFields);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm WithFormClass<T>(bool mapAllFields = true) where T : IBqlTable
      {
        this.Result.DacType = typeof (T).FullName;
        this.Result.MapAllFields = mapAllFields;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm.ColumnsCount(
        int cnt)
      {
        return (BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig) this.WithColumnsCount(cnt);
      }

      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm.Prompt(
        string prompt)
      {
        return (BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig) this.WithPrompt(prompt);
      }

      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm.WithFields(
        System.Action<BoundedTo<TGraph, TPrimary>.FormField.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig) this.WithFields((System.Action<BoundedTo<TGraph, TPrimary>.FormField.ContainerAdjusterFields>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm.WithFormClass<T>(
        bool mapAllFields)
      {
        return (BoundedTo<TGraph, TPrimary>.Form.IAllowOptionalConfig) this.WithFormClass<T>(mapAllFields);
      }
    }

    /// <summary>Adds workflow dialog boxes to screen configurations.</summary>
    public interface IContainerForms
    {
      /// <summary>Adds a new dialog box to the screen configuration.</summary>
      /// <param name="formName">An internal name of the dialog box.</param>
      /// <param name="config">A function that describes the dialog box appearance and field configuration.</param>
      void Add(
        string formName,
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> config = null);

      /// <summary>Adds a new configured dialog box to the screen configuration.</summary>
      /// <param name="form">A configured dialog box.</param>
      void Add(BoundedTo<TGraph, TPrimary>.Form.IConfigured form);

      void Add<TForm>(
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> config = null)
        where TForm : class, IWorkflowFormBqlTable, new();
    }

    /// <summary>
    /// Adds, replaces, updates, and removes workflow dialog boxes in screen configurations.
    /// </summary>
    public class ContainerAdjusterForms : BoundedTo<TGraph, TPrimary>.Form.IContainerForms
    {
      internal List<BoundedTo<TGraph, TPrimary>.Form> Result { get; }

      internal ContainerAdjusterForms(List<BoundedTo<TGraph, TPrimary>.Form> forms)
      {
        this.Result = forms;
      }

      public void Add(
        string formName,
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(formName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.Form.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm) configuratorForm);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => string.Equals(it.Name, formName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Form {formName} already exists.");
        this.Result.Add(configuratorForm.Result);
      }

      public void Add(BoundedTo<TGraph, TPrimary>.Form.IConfigured form)
      {
        BoundedTo<TGraph, TPrimary>.Form configuredForm = ((BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm) form).Result;
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => string.Equals(it.Name, configuredForm.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Form {configuredForm.Name} already exists.");
        this.Result.Add(configuredForm);
      }

      public void Add<TForm>(
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> config = null)
        where TForm : class, IWorkflowFormBqlTable, new()
      {
        string formName = typeof (TForm).FullName;
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(formName);
        configuratorForm.WithFormClass<TForm>(true);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.Form.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm) configuratorForm);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => string.Equals(it.Name, formName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Form {formName} already exists.");
        this.Result.Add(configuratorForm.Result);
      }

      /// <summary>
      /// Replaces an existing dialog box configuration with the new one.
      /// </summary>
      /// <param name="formName">An internal name of a dialog box.</param>
      /// <param name="config">A function that replaces the dialog box appearance and field configuration.</param>
      public void Replace(
        string formName,
        Func<BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm, BoundedTo<TGraph, TPrimary>.Form.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.Form form = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => it.Name == formName));
        if (form != null)
          this.Result.Remove(form);
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(formName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.Form.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Form.INeedAnyConfigForm) configuratorForm);
        }
        this.Result.Add(configuratorForm.Result);
      }

      /// <summary>Updates a configuration of an existing dialog box.</summary>
      /// <param name="formName">An internal name of a dialog box.</param>
      /// <param name="config">A function that updates the dialog box appearance and field configuration.</param>
      public void Update(
        string formName,
        Func<BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm, BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm> config)
      {
        BoundedTo<TGraph, TPrimary>.Form form = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => it.Name == formName));
        if (WebConfig.EnableWorkflowValidationOnStartup && form == null)
          throw new InvalidOperationException($"Form {formName} not found.");
        if (form == null)
          return;
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm1 = new BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm(form);
        BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm configuratorForm2 = config(configuratorForm1);
      }

      public void Update<TForm>(
        Func<BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm, BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm> config)
        where TForm : class, IWorkflowFormBqlTable, new()
      {
        this.Update(typeof (TForm).FullName, config);
      }

      /// <summary>Removes an existing dialog box configuration.</summary>
      /// <param name="formName">An internal name of a dialog box.</param>
      public void Remove(string formName)
      {
        BoundedTo<TGraph, TPrimary>.Form form = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Form>((Func<BoundedTo<TGraph, TPrimary>.Form, bool>) (it => it.Name == formName));
        if (form == null)
          return;
        this.Result.Remove(form);
      }

      public void Remove<TForm>() where TForm : class, IWorkflowFormBqlTable, new()
      {
        this.Remove(typeof (TForm).FullName);
      }
    }
  }

  /// <summary>Defines a field of a workflow dialog box.</summary>
  public class FormField
  {
    public bool IsActive { get; set; } = true;

    public string SchemaField { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string DefaultValue { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition RequiredCondition { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition HideCondition { get; set; }

    public int ColumnSpan { get; set; } = 1;

    public string ControlSize { get; set; }

    public bool SetNewComboBoxValues { get; set; }

    public string ComboBoxValues { get; set; }

    public ComboBoxValuesSource ComboBoxValuesSource { get; set; }

    public DefaultValueSource DefaultValueSource { get; set; }

    public System.Type ComboboxAndDefaultSourceField { get; set; }

    public bool IsFromScheme { get; set; } = true;

    internal Readonly.FormField AsReadonly() => Readonly.FormField.From<TGraph, TPrimary>(this);

    internal BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField ToConfigurator()
    {
      return new BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField(this);
    }

    /// <summary>Defines a new field for a workflow dialog box.</summary>
    public interface INeedTypeConfigField
    {
      /// <summary>
      /// Defines a new field for a dialog box. The field state is copied from the provided DAC field.
      /// </summary>
      /// <typeparam name="TField">A DAC field that is used to generate the field state.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig WithSchemaOf<TField>() where TField : IBqlField;

      /// <summary>
      /// Defines a new field for a dialog box. The field state is copied from the provided DAC field.
      /// </summary>
      /// <typeparam name="TTable">A DAC type.</typeparam>
      /// <param name="fieldName">A DAC field that is used to generate the field state.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig WithSchemaOf<TTable>(
        string fieldName)
        where TTable : IBqlTable;

      /// <summary>
      /// Defines a new field for a dialog box. The field state is copied from the provided DAC field.
      /// </summary>
      /// <param name="table">A DAC type.</param>
      /// <param name="fieldName">A DAC field that is used to generate the field state.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig WithSchemaOf(
        System.Type table,
        string fieldName);

      /// <summary>Defines a new combo box field.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues WithComboBoxField();

      /// <summary>Defines a new check box field.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig WithCheckBoxField();

      /// <summary>Defines a new rich text editor field.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig WithRichTextEditorField();
    }

    /// <summary>
    /// Defines a list of combo box values for a field in a workflow dialog box.
    /// </summary>
    public interface INeedComboBoxValues
    {
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ComboBoxValues(
        params (string value, string description)[] values);
    }

    /// <summary>
    /// Provides additional configuration for a field in a workflow dialog box.
    /// </summary>
    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.FormField.IConfigured,
      BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues
    {
      /// <summary>Specified that the current field must be required on the form when the provided condition is true.</summary>
      /// <param name="condition">Preconfigured condition.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Specifies that current field must always be required on the form.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig IsRequired();

      /// <summary>
      /// Specifies the default value for the current field of the dialog box.
      /// </summary>
      /// <param name="defaultValue">A constant default value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig DefaultValue(object defaultValue);

      /// <summary>
      /// Specifies the default value for the current field of the dialog box. The value is taken from the corresponding DAC field.
      /// </summary>
      /// <remarks>This setting could not be applied for new combo box and check box fields.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig DefaultValueFromSchemaField();

      /// <summary>
      /// Specifies a string expression that will be used to set the default value for the current field of the dialog box.
      /// </summary>
      /// <param name="defaultValue">A string expression for the default value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig DefaultExpression(
        string defaultValue);

      /// <summary>Specified that the current field must be hidden on the form when the provided condition is true.</summary>
      /// <param name="condition">Preconfigured condition.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Specifies that current field must always be hidden on the form.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig IsHiddenAlways();

      /// <summary>Restricts a list of possible combo box values.</summary>
      /// <param name="values">A list of available combo box values.</param>
      /// <remarks>This setting is available only for dialog box fields which states are copied from DAC fields, and DAC fields have the attached <see cref="T:PX.Data.PXStringListAttribute" /> </remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig OnlyComboBoxValues(
        params string[] values);

      /// <summary>
      /// Adds a new pair of a combox box value and its display name to the combo box field of the dialog box.
      /// </summary>
      /// <param name="key">A combo box value.</param>
      /// <param name="description">A display name of the combox box value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ComboBoxValue(
        string key,
        string description);

      /// <summary>Specifies a source of combo box values.</summary>
      /// <param name="valuesSource">A source of combo box values.</param>
      /// <remarks>This setting is available only for dialog box fields which states are copied from DAC fields, and DAC fields have the attached <see cref="T:PX.Data.PXStringListAttribute" /> </remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ComboBoxValuesSource(
        ComboBoxValuesSource valuesSource);

      /// <summary>Specifies A label text for a dialog box field.</summary>
      /// <param name="prompt">A label text.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig Prompt(string prompt);

      /// <summary>
      /// Specifies a column span for the current field of the dialox box.
      /// </summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ColumnSpan(int columnSpan);

      /// <summary>
      /// Specifies the size of the control for the current field of the dialog box.
      /// </summary>
      /// <param name="controlSize">A size of the control.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ControlSize(string controlSize);

      /// <summary>
      /// Specifies a size of the control for the current field of the dialog box.
      /// </summary>
      /// <param name="controlSize">A size of the control.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ControlSize(
        FormFieldControlSize controlSize);

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig PlaceBefore(string formFieldName);

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig PlaceAfter(string formFieldName);

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig DefaultValuesSource(
        DefaultValueSource defaultValuesSource);

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig ComboboxAndDefaultSourceField<TField>() where TField : class, IBqlField;
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorField : 
      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.FormField.IConfigured,
      BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues,
      BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField,
      IOrderableWorkflowElement
    {
      internal BoundedTo<TGraph, TPrimary>.FormField Result { get; }

      public MoveObjectInCollection MoveBefore { get; set; }

      public string NearKey { get; set; }

      public string Key => this.Result.Name;

      internal ConfiguratorField(string fieldName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.FormField()
        {
          Name = fieldName
        };
      }

      internal ConfiguratorField(BoundedTo<TGraph, TPrimary>.FormField formField)
      {
        this.Result = formField;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithSchemaOf<TField>() where TField : IBqlField
      {
        return this.WithSchemaOf(BqlCommand.GetItemType<TField>(), typeof (TField).Name);
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithSchemaOf<TTable>(
        string fieldName)
        where TTable : IBqlTable
      {
        return this.WithSchemaOf(typeof (TTable), fieldName);
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithSchemaOf(
        System.Type table,
        string fieldName)
      {
        this.Result.SchemaField = $"{table.FullName}.{fieldName}";
        this.Result.DisplayName = fieldName;
        this.Result.ComboBoxValuesSource = ComboBoxValuesSource.SourceState;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithComboBoxField()
      {
        this.Result.SchemaField = $"[{(Enum) SchemaFieldEditors.ComboBox}]";
        this.Result.DisplayName = SchemaFieldEditors.ComboBox.ToString();
        this.Result.ComboBoxValuesSource = ComboBoxValuesSource.ExplicitValues;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithCheckBoxField()
      {
        this.Result.SchemaField = $"[{(Enum) SchemaFieldEditors.CheckBox}]";
        this.Result.DisplayName = SchemaFieldEditors.CheckBox.ToString();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithDefaultValue(
        object defaultValue)
      {
        this.Result.DefaultValue = defaultValue?.ToString();
        this.Result.IsFromScheme = true;
        this.Result.DefaultValueSource = DefaultValueSource.ExplicitValue;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithDefaultExpression(
        string defaultValue)
      {
        this.Result.DefaultValue = defaultValue;
        this.Result.IsFromScheme = false;
        this.Result.DefaultValueSource = DefaultValueSource.ExplicitValue;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField DefaultValueFromSchemaField()
      {
        this.Result.DefaultValue = $"[{((IEnumerable<string>) this.Result.SchemaField.Split('.')).Last<string>()}]";
        this.Result.IsFromScheme = false;
        this.Result.DefaultValueSource = DefaultValueSource.ExplicitValue;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithComboBoxValues(
        params (string value, string description)[] values)
      {
        this.Result.SetNewComboBoxValues = true;
        if (WebConfig.EnableWorkflowValidationOnStartup && ((IEnumerable<(string, string)>) values).Select<(string, string), string>((Func<(string, string), string>) (it => it.value)).Distinct<string>().Count<string>() != values.Length)
          throw new ArgumentException("Combo box values contains duplicate values");
        this.Result.ComboBoxValues = string.Join(",", ((IEnumerable<(string, string)>) values).Select<(string, string), string>((Func<(string, string), string>) (it => $"{it.value};{it.description}")));
        this.Result.ComboBoxValuesSource = ComboBoxValuesSource.ExplicitValues;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithComboBoxValuesSource(
        ComboBoxValuesSource valuesSource)
      {
        this.Result.ComboBoxValuesSource = valuesSource;
        if (valuesSource != ComboBoxValuesSource.ExplicitValues)
          this.Result.ComboBoxValues = (string) null;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithOnlyComboBoxValues(
        params string[] values)
      {
        this.Result.ComboBoxValues = string.Join(";", values);
        this.Result.ComboBoxValuesSource = ComboBoxValuesSource.ExplicitValues;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithComboBoxValue(
        string key,
        string description)
      {
        this.Result.SetNewComboBoxValues = true;
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.ComboBoxValues != null)
        {
          if (((IEnumerable<string>) this.Result.ComboBoxValues.Split(',')).Any<string>((Func<string, bool>) (it => it.StartsWith(key + ";"))))
            throw new ArgumentException("Combo box values contains duplicate values");
        }
        this.Result.ComboBoxValues = string.Join(",", this.Result.ComboBoxValues, $"{key};{description}");
        this.Result.ComboBoxValuesSource = ComboBoxValuesSource.ExplicitValues;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithRichTextEditorField()
      {
        this.Result.SchemaField = $"[{(Enum) SchemaFieldEditors.RichTextEdit}]";
        this.Result.DisplayName = SchemaFieldEditors.RichTextEdit.ToString();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithPrompt(string prompt)
      {
        this.Result.DisplayName = prompt;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.HideCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField IsHiddenAlways()
      {
        this.Result.HideCondition = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.RequiredCondition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField IsRequired()
      {
        this.Result.RequiredCondition = (BoundedTo<TGraph, TPrimary>.Condition) true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithColumnSpan(int columnSpan)
      {
        this.Result.ColumnSpan = columnSpan;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithControlSize(
        string controlSize)
      {
        this.Result.ControlSize = controlSize;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField WithControlSize(
        FormFieldControlSize controlSize)
      {
        this.Result.ControlSize = controlSize.ToString();
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField PlaceFirst()
      {
        return this.Place((string) null, MoveObjectInCollection.First);
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField PlaceBefore(string formField)
      {
        return this.Place(formField, MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField PlaceAfter(string formField)
      {
        return this.Place(formField, MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField Place(
        string formField,
        MoveObjectInCollection place)
      {
        this.MoveBefore = place;
        this.NearKey = formField;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField DefaultValuesSource(
        DefaultValueSource defaultValuesSource)
      {
        this.Result.DefaultValueSource = defaultValuesSource;
        if (defaultValuesSource != DefaultValueSource.ExplicitValue)
          this.Result.DefaultValue = (string) null;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField ComboboxAndDefaultSourceField<TField>() where TField : class, IBqlField
      {
        this.Result.ComboboxAndDefaultSourceField = typeof (TField);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithSchemaOf<TField>()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithSchemaOf<TField>();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithSchemaOf<TTable>(
        string fieldName)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithSchemaOf<TTable>(fieldName);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithSchemaOf(
        System.Type table,
        string fieldName)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithSchemaOf(table, fieldName);
      }

      BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithComboBoxField()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues) this.WithComboBoxField();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithCheckBoxField()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithCheckBoxField();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField.WithRichTextEditorField()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithRichTextEditorField();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.INeedComboBoxValues.ComboBoxValues(
        params (string value, string description)[] values)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithComboBoxValues(values);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ComboBoxValuesSource(
        ComboBoxValuesSource valuesSource)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithComboBoxValuesSource(valuesSource);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.Prompt(
        string prompt)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithPrompt(prompt);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ColumnSpan(
        int columnSpan)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithColumnSpan(columnSpan);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ControlSize(
        string controlSize)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithControlSize(controlSize);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ControlSize(
        FormFieldControlSize controlSize)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithControlSize(controlSize);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.IsRequiredWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.IsRequiredWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.IsRequired()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.IsRequired();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.DefaultValue(
        object defaultValue)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithDefaultValue(defaultValue);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.DefaultExpression(
        string defaultValue)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithDefaultExpression(defaultValue);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.OnlyComboBoxValues(
        params string[] values)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithOnlyComboBoxValues(values);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ComboBoxValue(
        string key,
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.WithComboBoxValue(key, description);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.DefaultValueFromSchemaField()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.DefaultValueFromSchemaField();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.PlaceBefore(
        string formField)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.PlaceBefore(formField);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.PlaceAfter(
        string formField)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.PlaceAfter(formField);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.IsHiddenWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.IsHiddenWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.IsHiddenAlways()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.IsHiddenAlways();
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.DefaultValuesSource(
        DefaultValueSource defaultValuesSource)
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.DefaultValuesSource(defaultValuesSource);
      }

      BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig.ComboboxAndDefaultSourceField<TField>()
      {
        return (BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) this.ComboboxAndDefaultSourceField<TField>();
      }
    }

    /// <summary>
    /// Adds configurations of fields to the current workflow dialog box.
    /// </summary>
    public interface IContainerFillerFields
    {
      /// <summary>
      /// Adds a configuration of a new dialog box field to the current dialog box.
      /// </summary>
      /// <param name="fieldName">An internal name of a dialog box field.</param>
      /// <param name="config">A function that configures the behavior of the dialog box field.</param>
      void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField, BoundedTo<TGraph, TPrimary>.FormField.IConfigured> config = null);

      void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.FormField.IConfigured> config = null)
        where TField : IBqlField;
    }

    /// <summary>
    /// Adds, replaces, updates, or removes configurations of fields in the current workflow dialog box.
    /// </summary>
    public class ContainerAdjusterFields : 
      BoundedTo<TGraph, TPrimary>.FormField.IContainerFillerFields
    {
      internal List<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField> Result { get; }

      internal ContainerAdjusterFields(
        List<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField> formFields)
      {
        this.Result = formFields;
      }

      public void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField, BoundedTo<TGraph, TPrimary>.FormField.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField(fieldName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FormField.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, bool>) (it => string.Equals(it.Result.Name, fieldName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Form field {fieldName} already exists.");
        this.Result.Add(configuratorField);
      }

      public void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig, BoundedTo<TGraph, TPrimary>.FormField.IConfigured> config = null)
        where TField : IBqlField
      {
        string fieldName = typeof (TField).Name;
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField = new BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField(fieldName);
        configuratorField.WithSchemaOf<TField>();
        configuratorField.Result.DisplayName = (string) null;
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FormField.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FormField.IAllowOptionalConfig) configuratorField);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, bool>) (it => string.Equals(it.Result.Name, fieldName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Form field {fieldName} already exists.");
        this.Result.Add(configuratorField);
      }

      /// <summary>
      /// Replaces an existing configuration of a dialog box field with the new one.
      /// </summary>
      /// <param name="fieldName">An internal name of a dialog box field.</param>
      /// <param name="config">A function that configures the behavior of the dialog box field.</param>
      public void Replace(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField, BoundedTo<TGraph, TPrimary>.FormField.IConfigured> config = null)
      {
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, bool>) (it => it.Result.Name == fieldName));
        if (configuratorField1 != null)
          this.Result.Remove(configuratorField1);
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField2 = new BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField(fieldName);
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.FormField.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.FormField.INeedTypeConfigField) configuratorField2);
        }
        this.Result.Add(configuratorField2);
      }

      /// <summary>
      /// Updates an existing configuration of a dialog box field.
      /// </summary>
      /// <param name="fieldName">An internal name of a dialog box field.</param>
      /// <param name="config">A function that overrides the behavior of the dialog box field.</param>
      public void Update(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField> config)
      {
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, bool>) (it => it.Result.Name == fieldName));
        if (WebConfig.EnableWorkflowValidationOnStartup && configuratorField1 == null)
          throw new InvalidOperationException($"Form field {fieldName} not found.");
        if (configuratorField1 == null)
          return;
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField2 = config(configuratorField1);
      }

      /// <summary>
      /// Removes an existing configuration of a dialog box field.
      /// </summary>
      /// <param name="fieldName">An internal name of a dialog box field.</param>
      public void Remove(string fieldName)
      {
        BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField configuratorField = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField>((Func<BoundedTo<TGraph, TPrimary>.FormField.ConfiguratorField, bool>) (it => it.Result.Name == fieldName));
        if (configuratorField == null)
          return;
        this.Result.Remove(configuratorField);
      }
    }
  }

  /// <summary>Defines a navigation action.</summary>
  public class NavigationDefinition
  {
    public string NavigationScreen { get; protected internal set; }

    public PXBaseRedirectException.WindowMode WindowMode { get; protected internal set; }

    public string IconName { get; protected internal set; }

    public List<BoundedTo<TGraph, TPrimary>.NavigationParameter> Assignments { get; protected internal set; } = new List<BoundedTo<TGraph, TPrimary>.NavigationParameter>();

    public string ActionType { get; protected internal set; }

    internal Readonly.NavigationDefinition AsReadonly()
    {
      return Readonly.NavigationDefinition.From<TGraph, TPrimary>(this);
    }

    /// <summary>Creates settings for navigation actions.</summary>
    public class NavigationDefinitionBuilder
    {
      /// <summary>Creates a set of settings for a navigation action that creates a new record.</summary>
      /// <param name="initializer">Function that configures settings for the navigation action which creates a new record.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate CreateRecord(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate> initializer)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition record = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate configuredCreate = initializer((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen) record);
        record.Result.ActionType = "C";
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate) record;
      }

      /// <summary>Creates a set of settings for a navigation action that opens a list of records.</summary>
      /// <param name="initializer">Function that configures settings for a navigation action which opens a list of records</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch SearchRecord(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch> initializer)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch configuredSearch = initializer((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen) navigationDefinition);
        navigationDefinition.Result.ActionType = "S";
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch) navigationDefinition;
      }

      /// <summary>Creates a set of settings for a navigation action that opens a report.</summary>
      /// <param name="initializer">Function that configures settings for a navigation action which opens a report.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport RunReport(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport> initializer)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport configuredRunReport = initializer((BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen) navigationDefinition);
        navigationDefinition.Result.ActionType = "R";
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport) navigationDefinition;
      }

      /// <summary>
      /// Creates a set of settings for a navigation action that opens a side panel.
      /// </summary>
      /// <param name="initializer">Function that configures settings for a navigation action which opens a side panel.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel SidePanel(
        Func<BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel> initializer)
      {
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition navigationDefinition = new BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition();
        BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel configuredSidePanel = initializer((BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen) navigationDefinition);
        navigationDefinition.Result.WindowMode = PXBaseRedirectException.WindowMode.Layer;
        navigationDefinition.Result.ActionType = "P";
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel) navigationDefinition;
      }
    }

    /// <summary>
    /// Configures navigation to an Acumatica ERP form to create a record.
    /// </summary>
    public interface ICreateNeedScreen
    {
      /// <summary>Specifies an ID of the form which will be used to create a record.</summary>
      /// <param name="screenId">Form ID.</param>
      /// <remarks>This method is applicable only to actions which open a form with a new record.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults NavigateToScreen(
        string screenId);

      /// <summary>Specifies primary graph of a form to be opened.</summary>
      /// <typeparam name="T">Type of the primary graph of the form.</typeparam>
      /// <remarks>This method is applicable only to actions which open a form with a new record.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults NavigateToScreen<T>() where T : PXGraph;
    }

    /// <summary>
    /// Configures navigation to an Acumatica ERP form to display a list of records.
    /// </summary>
    public interface ISearchNeedScreen
    {
      /// <summary>Specifies an ID of the form with a list of record to be opened.</summary>
      /// <param name="screenId">Form ID.</param>
      /// <remarks>This method is applicable only to actions which open a list of records.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters NavigateToScreen(
        string screenId);

      /// <summary>Specifies primary graph of a form with the list of records to be opened.</summary>
      /// <typeparam name="T">Type of the primary graph of the form.</typeparam>
      /// <remarks>This method is applicable only to actions which open a list of records.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters NavigateToScreen<T>() where T : PXGraph;
    }

    /// <summary>Configures navigation to a report form.</summary>
    public interface IRunReportNeedScreen
    {
      /// <summary>Specifies a report to be opened when a user select the action.</summary>
      /// <param name="screenId">Report ID.</param>
      /// <remarks>This method is applicable only to actions with open a report.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters ReportID(
        string screenId);
    }

    /// <summary>
    /// Specifies additional settings for navigation to an Acumatica ERP form when a record is created on this form.
    /// </summary>
    public interface ICreateOptionalDefaults
    {
      /// <summary>Specifies a window mode in which a form should be opened when a user select the action.</summary>
      /// <param name="windowMode">Window mode.</param>
      /// <remarks>This method is applicable only to actions which open a specific record on a form.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode);

      /// <summary>Specifies a list of navigation parameters.</summary>
      /// <param name="containerFiller">Function that specifies a list of navigation parameters.</param>
      /// <remarks>This method is applicable only to actions which open a specific record on a form.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller);
    }

    /// <summary>
    /// Specifies additional settings for navigation to an Acumatica ERP form when a list of records is displayed on this form.
    /// </summary>
    public interface ISearchOptionalParameters
    {
      /// <summary>Specifies a window mode in which a form should be opened when a user select the action.</summary>
      /// <param name="windowMode">Window mode.</param>
      /// <remarks>This method is applicable only to actions which open a list of records.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode);

      /// <summary>Specifies a list of navigation parameters for an action.</summary>
      /// <param name="containerFiller">Function that specifies a list of navigation parameters.</param>
      /// <remarks>This method is applicable only to actions which open a list of records.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller);

      /// <summary>Specifies that selected foreign key object must be used to calculate navigation parameter values.</summary>
      /// <typeparam name="TFK">Foreign key type.</typeparam>
      /// <remarks>This method is applicable only to actions which open a list of records.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch WithFK<TFK>() where TFK : IForeignKeyFrom<TPrimary>;
    }

    /// <summary>
    /// Specifies additional settings for navigation to a report form.
    /// </summary>
    public interface IRunReportOptionalParameters
    {
      /// <summary>Specifies a window mode in which a report should be opened when a user select the action.</summary>
      /// <param name="windowMode">Window mode.</param>
      /// <remarks>This method is applicable only to actions with open a report.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode);

      /// <summary>Specifies a list of report parameters.</summary>
      /// <param name="containerFiller">Function that specifies list of report parameters.</param>
      /// <remarks>This method is applicable only to actions with open a report.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller);
    }

    /// <summary>Specifies settings for displaying of a side panel</summary>
    public interface ISidePanelNeedScreen
    {
      /// <summary>Specifies ID of a form which will be displayed on a side panel.</summary>
      /// <param name="screenId">Form ID.</param>
      /// <remarks>This method is applicable only to actions which open a side panel.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults NavigateToScreen(
        string screenId);

      /// <summary>Specifies primary graph of a form which will be displayed on a side panel.</summary>
      /// <typeparam name="T">Type of the primary graph of the form.</typeparam>
      /// <remarks>This method is applicable only to actions which open a side panel.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults NavigateToScreen<T>() where T : PXGraph;
    }

    /// <summary>
    /// Specifies additional settings for displaying of a side panel.
    /// </summary>
    public interface ISidePanelOptionalDefaults
    {
      /// <summary>Specifies an icon which will be displayed for the action.</summary>
      /// <param name="iconName">An icon name from the font-awesome.css file.</param>
      /// <remarks>This method is applicable only for side panels.</remarks>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults WithIcon(
        string iconName);

      /// <summary>Specifies a list of navigation parameters.</summary>
      /// <param name="containerFiller">Function that specifies list of navigation parameters.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller);
    }

    public interface IConfiguredCreate
    {
    }

    public interface IConfiguredSearch
    {
    }

    public interface IConfiguredRunReport
    {
    }

    public interface IConfiguredSidePanel
    {
    }

    public class ConfiguratorNavigationDefinition : 
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters,
      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults
    {
      internal BoundedTo<TGraph, TPrimary>.NavigationDefinition Result { get; }

      internal ConfiguratorNavigationDefinition()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.NavigationDefinition();
      }

      internal ConfiguratorNavigationDefinition(string screenId)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.NavigationDefinition()
        {
          NavigationScreen = screenId
        };
      }

      internal ConfiguratorNavigationDefinition(
        BoundedTo<TGraph, TPrimary>.NavigationDefinition navigationDefinition)
      {
        this.Result = navigationDefinition;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster containerAdjuster1 = new BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster(this.Result.Assignments);
        containerAdjuster(containerAdjuster1);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode)
      {
        this.Result.WindowMode = windowMode;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition NavigateToScreen(
        string screenId)
      {
        this.Result.NavigationScreen = screenId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.ConfiguratorNavigationDefinition NavigateToScreen<T>() where T : PXGraph
      {
        this.Result.NavigationScreen = PXPageIndexingService.GetScreenIDFromGraphType(typeof (T));
        return this;
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters.WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters) this.WithWindowMode(windowMode);
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults WithIcon(
        string iconName)
      {
        this.Result.IconName = iconName;
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults) this;
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters.WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredRunReport) this.WithAssignments((System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster>) containerFiller);
      }

      public BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch WithFK<TFK>() where TFK : IForeignKeyFrom<TPrimary>
      {
        BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster containerAdjuster = new BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster(this.Result.Assignments);
        IForeignKeyFrom<TPrimary> fkdefinition = (IForeignKeyFrom<TPrimary>) Activator.CreateInstance<TFK>();
        if (fkdefinition.FieldsMapping.Count == 1)
        {
          KeysCollection keys = PXGraph.CreateInstance(typeof (PXGraph)).Caches[fkdefinition.ParentTable].Keys;
          if (keys.Count == 1)
          {
            containerAdjuster.Add(keys[0], (Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured>) (operand => operand.SetFromField(fkdefinition.FieldsMapping.First<KeyValuePair<System.Type, System.Type>>().Key.Name)));
            return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch) this;
          }
        }
        foreach (KeyValuePair<System.Type, System.Type> keyValuePair in fkdefinition.FieldsMapping)
        {
          KeyValuePair<System.Type, System.Type> type = keyValuePair;
          containerAdjuster.Add(type.Value.Name, (Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured>) (operand => operand.SetFromField(type.Key.Name)));
        }
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch) this;
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters.WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSearch) this.WithAssignments((System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen.NavigateToScreen(
        string screenId)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults) this.NavigateToScreen(screenId);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportNeedScreen.ReportID(
        string screenId)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IRunReportOptionalParameters) this.NavigateToScreen(screenId);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen.NavigateToScreen<T>()
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters) this.NavigateToScreen<T>();
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchNeedScreen.NavigateToScreen(
        string screenId)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters) this.NavigateToScreen(screenId);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateNeedScreen.NavigateToScreen<T>()
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults) this.NavigateToScreen<T>();
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen.NavigateToScreen<T>()
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults) this.NavigateToScreen<T>();
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelNeedScreen.NavigateToScreen(
        string screenId)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults) this.NavigateToScreen(screenId);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults.WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults) this.WithWindowMode(windowMode);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters.WithWindowMode(
        PXBaseRedirectException.WindowMode windowMode)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISearchOptionalParameters) this.WithWindowMode(windowMode);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate BoundedTo<TGraph, TPrimary>.NavigationDefinition.ICreateOptionalDefaults.WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredCreate) this.WithAssignments((System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel BoundedTo<TGraph, TPrimary>.NavigationDefinition.ISidePanelOptionalDefaults.WithAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.NavigationDefinition.IConfiguredSidePanel) this.WithAssignments((System.Action<BoundedTo<TGraph, TPrimary>.NavigationParameter.NavigationActionParametersContainerAdjuster>) containerFiller);
      }
    }
  }

  /// <summary>Defines a navigation parameter.</summary>
  public class NavigationParameter
  {
    public string FieldName { get; set; }

    public object Value { get; set; }

    public bool IsFromScheme { get; set; } = true;

    internal Readonly.NavigationParameter AsReadonly()
    {
      return Readonly.NavigationParameter.From<TGraph, TPrimary>(this);
    }

    /// <summary>
    /// Specifies a value for the current navigation or report parameter.
    /// </summary>
    public interface INeedRightOperand
    {
      /// <summary>Specifies a constant value for the current navigation or report parameter.</summary>
      /// <param name="value">The constant value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromValue(object value);

      /// <summary>Specifies a string expression for the current navigation or report parameter. This expression is evaluated at runtime in the current graph context.
      /// </summary>
      /// <param name="expression">A string expression.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromExpression(
        string expression);

      /// <summary>Specifies a dialog box field which value will be assigned to the current navigation or report parameter.</summary>
      /// <param name="form">A configured dialog box.</param>
      /// <param name="fieldName">An internal name of the dialog box field.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromFormField(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured form,
        string fieldName);

      /// <summary>Specifies that value for the current navigation or report parameter must be taken from the provided DAC field.</summary>
      /// <typeparam name="TField">A DAC field which value should be passed as a navigation or report parameter value.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromField<TField>() where TField : IBqlField;

      /// <summary>Specifies that value for the current navigation or report parameter must be taken from the provided DAC field.</summary>
      /// <param name="fieldName">A name of a DAC field which value should be passed as a navigation or report parameter value.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromField(string fieldName);

      /// <summary>Sets the current date to the current navigation or report parameter at runtime. The method sets the sames value which the @Now string expression returns.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromToday();

      /// <summary>Sets the current date and time to the current navigation or report parameter at runtime. The method sets the sames value which the @Now string expression returns.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromNow();

      /// <summary>Sets the current user name to the current navigation or report parameter at runtime.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromCurrentUser();

      /// <summary>Sets the current branch ID to the current navigation or report parameter at runtime.</summary>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromCurrentBranch();
    }

    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    internal class ConfiguratorNavigationParameter : 
      BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand,
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.NavigationParameter Result { get; }

      internal ConfiguratorNavigationParameter(string fieldName)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.NavigationParameter()
        {
          FieldName = fieldName
        };
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromValue(object value)
      {
        this.Result.Value = (object) value?.ToString();
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromExpression(
        string expression)
      {
        this.Result.Value = (object) expression;
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromFormField(
        BoundedTo<TGraph, TPrimary>.Form.IConfigured formConfig,
        string fieldName)
      {
        this.Result.Value = (object) $"[{((BoundedTo<TGraph, TPrimary>.Form.ConfiguratorForm) formConfig).Result.Name}.{fieldName}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromField<TField>() where TField : IBqlField
      {
        this.Result.Value = (object) $"[{typeof (TField).Name}]";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromField(
        string fieldName)
      {
        this.Result.Value = (object) fieldName;
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromToday()
      {
        this.Result.Value = (object) "@Today";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromNow()
      {
        this.Result.Value = (object) "=Now()";
        this.Result.IsFromScheme = false;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromCurrentUser()
      {
        this.Result.Value = (object) "@me";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }

      public BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured SetFromCurrentBranch()
      {
        this.Result.Value = (object) "@branch";
        this.Result.IsFromScheme = true;
        return (BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured) this;
      }
    }

    /// <summary>Adds navigation parameters.</summary>
    public interface IContainerFillerNavigationActionParameters
    {
      /// <summary>Adds a configuration of a new navigation parameter for the current navigation action.</summary>
      /// <typeparam name="TField">A navigation parameter.</typeparam>
      /// <param name="config">A function that configures the navigation parameter.</param>
      void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config)
        where TField : IBqlField;

      /// <summary>Adds a configuration of a new navigation parameter for the current navigation action.</summary>
      /// <param name="fieldName">A navigation parameter.</param>
      /// <param name="config">A function that configures the navigation parameter.</param>
      void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config);
    }

    /// <summary>Adds, replaces, or removes navigation parameters.</summary>
    public class NavigationActionParametersContainerAdjuster : 
      BoundedTo<TGraph, TPrimary>.NavigationParameter.IContainerFillerNavigationActionParameters
    {
      internal List<BoundedTo<TGraph, TPrimary>.NavigationParameter> Result { get; }

      internal NavigationActionParametersContainerAdjuster(
        List<BoundedTo<TGraph, TPrimary>.NavigationParameter> assignments)
      {
        this.Result = assignments;
      }

      /// <inheritdoc />
      public void Add<TField>(
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config)
        where TField : IBqlField
      {
        this.Add(typeof (TField).Name, config);
      }

      /// <inheritdoc />
      public void Add(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.NavigationParameter.ConfiguratorNavigationParameter navigationParameter = new BoundedTo<TGraph, TPrimary>.NavigationParameter.ConfiguratorNavigationParameter(fieldName);
        BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand) navigationParameter);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.NavigationParameter>((Func<BoundedTo<TGraph, TPrimary>.NavigationParameter, bool>) (it => string.Equals(it.FieldName, fieldName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Navigation parameter {fieldName} already exists.");
        this.Result.Add(navigationParameter.Result);
      }

      /// <summary>Replaces an existing configuration of a navigation parameter with the new configuration.</summary>
      /// <typeparam name="TField">A navigation parameter.</typeparam>
      /// <param name="config">A function that configures a navigation parameter.</param>
      public void Replace<TField>(
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config)
        where TField : IBqlField
      {
        this.Replace(typeof (TField).Name, config);
      }

      /// <summary>Replaces an existing configuration of a navigation parameter with the new configuration.</summary>
      /// <param name="fieldName">A navigation parameter name.</param>
      /// <param name="config">A function that configures a navigation parameter.</param>
      public void Replace(
        string fieldName,
        Func<BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand, BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.NavigationParameter navigationParameter1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.NavigationParameter>((Func<BoundedTo<TGraph, TPrimary>.NavigationParameter, bool>) (it => it.FieldName == fieldName));
        if (navigationParameter1 != null)
          this.Result.Remove(navigationParameter1);
        BoundedTo<TGraph, TPrimary>.NavigationParameter.ConfiguratorNavigationParameter navigationParameter2 = new BoundedTo<TGraph, TPrimary>.NavigationParameter.ConfiguratorNavigationParameter(fieldName);
        BoundedTo<TGraph, TPrimary>.NavigationParameter.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.NavigationParameter.INeedRightOperand) navigationParameter2);
        this.Result.Add(navigationParameter2.Result);
      }

      /// <summary>Removes an existing configuration of a navigation parameter.</summary>
      /// <typeparam name="TField">A navigation parameter.</typeparam>
      public void Remove<TField>() where TField : IBqlField => this.Remove(typeof (TField).Name);

      /// <summary>Removes an existing configuration of a navigation parameter.</summary>
      /// <param name="fieldName">A name of the navigation parameter.</param>
      public void Remove(string fieldName)
      {
        BoundedTo<TGraph, TPrimary>.NavigationParameter navigationParameter = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.NavigationParameter>((Func<BoundedTo<TGraph, TPrimary>.NavigationParameter, bool>) (it => it.FieldName == fieldName));
        if (navigationParameter == null)
          return;
        this.Result.Remove(navigationParameter);
      }
    }
  }

  /// <summary>Defines the screen configuration for a workflow.</summary>
  public class ScreenConfiguration
  {
    public string StateIdentifier { get; set; }

    public string FlowIdentifier { get; set; }

    public bool AllowUserToChange { get; set; }

    public string FlowSubIdentifier { get; set; }

    public bool AllowUserToChangeSubType { get; set; }

    public bool AllowWorkflowCustomization { get; set; } = true;

    public List<BoundedTo<TGraph, TPrimary>.DynamicFieldState> GlobalFieldStates { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.DynamicFieldState>();

    public List<BoundedTo<TGraph, TPrimary>.ActionCategory> Categories { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.ActionCategory>()
    {
      new BoundedTo<TGraph, TPrimary>.ActionCategory()
      {
        CategoryName = "Action",
        DisplayName = nameof (Actions)
      },
      new BoundedTo<TGraph, TPrimary>.ActionCategory()
      {
        CategoryName = "Inquiry",
        DisplayName = "Inquiries"
      },
      new BoundedTo<TGraph, TPrimary>.ActionCategory()
      {
        CategoryName = "Report",
        DisplayName = "Reports"
      }
    };

    public List<BoundedTo<TGraph, TPrimary>.ActionDefinition> Actions { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.ActionDefinition>();

    public List<BoundedTo<TGraph, TPrimary>.ActionSequence> ActionSequences { get; set; } = new List<BoundedTo<TGraph, TPrimary>.ActionSequence>();

    public List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition> Handlers { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>();

    public List<BoundedTo<TGraph, TPrimary>.ArchivingRule> ArchivingRules { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.ArchivingRule>();

    public List<BoundedTo<TGraph, TPrimary>.Form> Forms { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.Form>();

    public List<BoundedTo<TGraph, TPrimary>.Workflow> Flows { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.Workflow>();

    public List<BoundedTo<TGraph, TPrimary>.ISharedCondition> SharedConditions { get; internal set; } = new List<BoundedTo<TGraph, TPrimary>.ISharedCondition>();

    internal Readonly.ScreenConfiguration AsReadonly()
    {
      return Readonly.ScreenConfiguration.From<TGraph, TPrimary>(this);
    }

    /// <summary>Specifies a field of the primary DAC as the state ID.</summary>
    public interface INeedStateIDScreen
    {
      /// <summary>Specifies a primary DAC field as a property that controls the state of the current workflow.</summary>
      /// <typeparam name="TField">A primary DAC field.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow StateIdentifierIs<TField>() where TField : IBqlField;

      /// <summary>Specifies a primary DAC field as a property that controls the state of current workflow.</summary>
      /// <param name="fieldName">Name of a primary DAC field.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow StateIdentifierIs(
        string fieldName);
    }

    /// <summary>
    /// Specifies the default workflow or the field that identifies the workflow type.
    /// </summary>
    public interface INeedFlowTypeIDOrDefaultFlow : 
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured
    {
      /// <summary>Adds a new workflow which will be used for the document in case there are no other workflows or in case the flow identifier does not match any defined value.</summary>
      /// <param name="config">A function that configures all settings of the default workflow.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig AddDefaultFlow(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config);

      /// <summary>Specifies a primary DAC field as a property which is used to identify different workflows for the document.</summary>
      /// <typeparam name="TField">A primary DAC field.</typeparam>
      /// <param name="allowUserToChange"><code>true</code> to allow a user manually change a workflow of the current document in any state; otherwise, <code>false</code>.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList FlowTypeIdentifierIs<TField>(
        bool allowUserToChange = false)
        where TField : IBqlField;

      /// <summary>Specifies a primary DAC field as a property which is used to identify different workflows for the document.</summary>
      /// <param name="fieldName">Name of a primary DAC field.</param>
      /// <param name="allowUserToChange"><code>true</code> to allow a user manually change a workflow of the current document in any state; otherwise, <code>false</code>.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList FlowTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange = false);
    }

    /// <summary>
    /// Defines the list of workflows by the workflow type field.
    /// </summary>
    public interface INeedFlowList : BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured
    {
      /// <summary>Defines a list of available workflows identified by the flow type field.</summary>
      /// <param name="containerFiller">Functions that specifies list of possible workflows.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerFlows> containerFiller);

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList FlowSubTypeIdentifierIs<TField>(
        bool allowUserToChange = false)
        where TField : IBqlField;

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList FlowSubTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange = false);
    }

    /// <summary>
    /// Defines the list of subflows by the workflow type field.
    /// </summary>
    public interface INeedSubFlowList : BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured
    {
      /// <summary>Defines a list of available workflows identified by the flow type field.</summary>
      /// <param name="containerFiller">Functions that specifies list of possible workflows.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerFlows> containerFiller);
    }

    /// <summary>
    /// Defines additional configuration for the screen configuration.
    /// </summary>
    public interface IAllowOptionalConfig : 
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured
    {
      /// <summary>Defines a list of action configurations.</summary>
      /// <param name="containerFiller">A function that specifies a list of action configurations.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionDefinition.IContainerFillerActions> containerFiller);

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithCategories(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionCategory.IContainerFillerCategories> containerFiller);

      /// <summary>Defines a list of field configurations.</summary>
      /// <param name="containerFiller">A function that specifies a list of field configurations.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.DynamicFieldState.IContainerFillerFields> containerFiller);

      /// <summary>Defines a list of dialog box configurations.</summary>
      /// <param name="containerFiller">A function that specifies list of dialog box configurations.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithForms(
        System.Action<BoundedTo<TGraph, TPrimary>.Form.IContainerForms> containerFiller);

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IContainerFillerHandlers> containerFiller);

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig WithArchivingRules(
        System.Action<BoundedTo<TGraph, TPrimary>.ArchivingRule.IContainerFillerRules> containerFiller);

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig ForbidFurtherChanges();
    }

    public interface IStartConfigScreen : 
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedStateIDScreen,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured
    {
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorScreen : 
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IConfigured,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IStartConfigScreen,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedStateIDScreen,
      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig
    {
      internal BoundedTo<TGraph, TPrimary>.ScreenConfiguration Result { get; }

      internal ConfiguratorScreen()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.ScreenConfiguration();
      }

      internal ConfiguratorScreen(
        BoundedTo<TGraph, TPrimary>.ScreenConfiguration configuration)
      {
        this.Result = configuration;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen StateIdentifierIs<TField>()
      {
        return this.StateIdentifierIs(typeof (TField).Name);
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen StateIdentifierIs(
        string fieldName)
      {
        if (!string.IsNullOrEmpty(fieldName) && string.Equals(fieldName, this.Result.FlowIdentifier, StringComparison.OrdinalIgnoreCase))
        {
          PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<string>("The Workflow-Identifying Field and the State Identifier cannot be the same. StateIdentifier: {FieldName}", fieldName);
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw new PXException("The Workflow-Identifying Field and the State Identifier cannot be the same.");
        }
        this.Result.StateIdentifier = fieldName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen AddDefaultFlow(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result.Flows, (string) null, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Flows.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null)))
          throw new ArgumentException("Default workflow already exists.");
        this.Result.Flows.Add(configuratorFlow.Result);
        return this;
      }

      /// <summary>Replaces a configuration of a default workflow with the provided one.</summary>
      /// <param name="config">A function that configures the default workflow settings.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen ReplaceDefaultFlow(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result.Flows, (string) null, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        BoundedTo<TGraph, TPrimary>.Workflow workflow = this.Result.Flows.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null));
        if (workflow != null)
          this.Result.Flows.Remove(workflow);
        this.Result.Flows.Add(configuratorFlow.Result);
        return this;
      }

      /// <summary>Updates a configuration of the default workflow.</summary>
      /// <param name="config">A function that configures the default workflow settings.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen UpdateDefaultFlow(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flow = this.Result.Flows.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null));
        if (WebConfig.EnableWorkflowValidationOnStartup && flow == null)
          throw new InvalidOperationException("Default workflow not found.");
        if (flow != null)
        {
          BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow1 = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result.Flows, flow);
          BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow2 = config(configuratorFlow1);
        }
        return this;
      }

      /// <summary>Completely removes a configuration of the default workflow.</summary>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen RemoveDefaultFlow()
      {
        BoundedTo<TGraph, TPrimary>.Workflow workflow = this.Result.Flows.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null));
        if (workflow != null)
          this.Result.Flows.Remove(workflow);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen FlowTypeIdentifierIs<TField>(
        bool allowUserToChange = false)
        where TField : IBqlField
      {
        return this.FlowTypeIdentifierIs(typeof (TField).Name, allowUserToChange);
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen FlowTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange = false)
      {
        if (!string.IsNullOrEmpty(fieldName) && (string.Equals(fieldName, this.Result.StateIdentifier, StringComparison.OrdinalIgnoreCase) || string.Equals(fieldName, this.Result.FlowSubIdentifier, StringComparison.OrdinalIgnoreCase)))
        {
          PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<string>("The Workflow-Identifying Field and the State Identifier cannot be the same. FlowTypeIdentifier: {FieldName}", fieldName);
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw new PXException("The Workflow-Identifying Field and the State Identifier cannot be the same.");
        }
        this.Result.FlowIdentifier = fieldName;
        this.Result.AllowUserToChange = allowUserToChange;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen FlowSubTypeIdentifierIs<TField>(
        bool allowUserToChange = false)
        where TField : IBqlField
      {
        return this.FlowSubTypeIdentifierIs(typeof (TField).Name, allowUserToChange);
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen FlowSubTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange = false)
      {
        if (!string.IsNullOrEmpty(fieldName) && (string.Equals(fieldName, this.Result.StateIdentifier, StringComparison.OrdinalIgnoreCase) || string.Equals(fieldName, this.Result.FlowIdentifier, StringComparison.OrdinalIgnoreCase)))
        {
          PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<string>("The Workflow-Identifying Field and the State Identifier cannot be the same. FlowTypeIdentifier: {FieldName}", fieldName);
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw new PXException("The Workflow-Identifying Field and the State Identifier cannot be the same.");
        }
        this.Result.FlowSubIdentifier = fieldName;
        this.Result.AllowUserToChangeSubType = allowUserToChange;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterFlows> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterFlows containerAdjusterFlows = new BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterFlows(this.Result.Flows);
        containerAdjuster(containerAdjusterFlows);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionDefinition.ContainerAdjusterActions> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.ActionDefinition.ContainerAdjusterActions containerAdjusterActions = new BoundedTo<TGraph, TPrimary>.ActionDefinition.ContainerAdjusterActions(this.Result.Actions);
        containerAdjuster(containerAdjusterActions);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithCategories(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionCategory.ContainerAdjusterCategories> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.ActionCategory.ContainerAdjusterCategories adjusterCategories = new BoundedTo<TGraph, TPrimary>.ActionCategory.ContainerAdjusterCategories(this.Result.Categories);
        containerAdjuster(adjusterCategories);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.DynamicFieldState.ContainerAdjusterFields> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.DynamicFieldState.ContainerAdjusterFields containerAdjusterFields = new BoundedTo<TGraph, TPrimary>.DynamicFieldState.ContainerAdjusterFields(this.Result.GlobalFieldStates);
        containerAdjuster(containerAdjusterFields);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithForms(
        System.Action<BoundedTo<TGraph, TPrimary>.Form.ContainerAdjusterForms> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Form.ContainerAdjusterForms containerAdjusterForms = new BoundedTo<TGraph, TPrimary>.Form.ContainerAdjusterForms(this.Result.Forms);
        containerAdjuster(containerAdjusterForms);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ContainerAdjusterHandlers> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ContainerAdjusterHandlers adjusterHandlers = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ContainerAdjusterHandlers(this.Result.Handlers);
        containerAdjuster(adjusterHandlers);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithActionSequences(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionSequence.ContainerAdjusterActionSequences> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.ActionSequence.ContainerAdjusterActionSequences adjusterActionSequences = new BoundedTo<TGraph, TPrimary>.ActionSequence.ContainerAdjusterActionSequences(this.Result.ActionSequences);
        containerAdjuster(adjusterActionSequences);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen WithArchivingRules(
        System.Action<BoundedTo<TGraph, TPrimary>.ArchivingRule.ContainerAdjusterRules> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.ArchivingRule.ContainerAdjusterRules containerAdjusterRules = new BoundedTo<TGraph, TPrimary>.ArchivingRule.ContainerAdjusterRules(this.Result.ArchivingRules);
        containerAdjuster(containerAdjusterRules);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.ScreenConfiguration.ConfiguratorScreen ForbidFurtherChanges()
      {
        this.Result.AllowWorkflowCustomization = false;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedStateIDScreen.StateIdentifierIs<TField>()
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow) this.StateIdentifierIs<TField>();
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedStateIDScreen.StateIdentifierIs(
        string fieldName)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow) this.StateIdentifierIs(fieldName);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow.AddDefaultFlow(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.AddDefaultFlow(config);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow.FlowTypeIdentifierIs<TField>(
        bool allowUserToChange)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList) this.FlowTypeIdentifierIs<TField>(allowUserToChange);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowTypeIDOrDefaultFlow.FlowTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList) this.FlowTypeIdentifierIs(fieldName, allowUserToChange);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList.FlowSubTypeIdentifierIs<TField>(
        bool allowUserToChange = false)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList) this.FlowSubTypeIdentifierIs<TField>(allowUserToChange);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList.FlowSubTypeIdentifierIs(
        string fieldName,
        bool allowUserToChange = false)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList) this.FlowTypeIdentifierIs(fieldName, allowUserToChange);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedFlowList.WithFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerFlows> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithFlows((System.Action<BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterFlows>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.INeedSubFlowList.WithFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerFlows> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithFlows((System.Action<BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterFlows>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionDefinition.IContainerFillerActions> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionDefinition.ContainerAdjusterActions>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithCategories(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionCategory.IContainerFillerCategories> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithCategories((System.Action<BoundedTo<TGraph, TPrimary>.ActionCategory.ContainerAdjusterCategories>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IContainerFillerHandlers> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithHandlers((System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ContainerAdjusterHandlers>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.ForbidFurtherChanges()
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.ForbidFurtherChanges();
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.DynamicFieldState.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithFieldStates((System.Action<BoundedTo<TGraph, TPrimary>.DynamicFieldState.ContainerAdjusterFields>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithForms(
        System.Action<BoundedTo<TGraph, TPrimary>.Form.IContainerForms> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithForms((System.Action<BoundedTo<TGraph, TPrimary>.Form.ContainerAdjusterForms>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig.WithArchivingRules(
        System.Action<BoundedTo<TGraph, TPrimary>.ArchivingRule.IContainerFillerRules> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.ScreenConfiguration.IAllowOptionalConfig) this.WithArchivingRules((System.Action<BoundedTo<TGraph, TPrimary>.ArchivingRule.ContainerAdjusterRules>) containerFiller);
      }
    }
  }

  public class Sequence : 
    BoundedTo<TGraph, TPrimary>.BaseCompositeState,
    IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.Sequence>
  {
    internal override BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase ToConfigurator()
    {
      return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(this);
    }

    internal override Readonly.BaseFlowStep AsReadonly()
    {
      return (Readonly.BaseFlowStep) Readonly.Sequence.From<TGraph, TPrimary>(this);
    }

    public BoundedTo<TGraph, TPrimary>.Sequence CreateCopy()
    {
      BoundedTo<TGraph, TPrimary>.Sequence copy = (BoundedTo<TGraph, TPrimary>.Sequence) this.MemberwiseClone();
      copy.States = this.States.Select<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>) (it => it.Clone() as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase)).ToList<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>();
      return copy;
    }

    public override object Clone() => (object) this.CreateCopy();

    public class SequenceBuilder
    {
      public BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured Create(
        string sequenceId,
        Func<BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured> initializer = null)
      {
        BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence configuratorSequence = new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(sequenceId);
        if (initializer != null)
        {
          BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig) configuratorSequence);
        }
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured) configuratorSequence;
      }
    }

    public interface INeedStatesThenConditionsConfig
    {
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig WithStates(
        System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates> containerFiller);
    }

    public class ConfiguratorSequence : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<BoundedTo<TGraph, TPrimary>.Sequence>,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured,
      BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig
    {
      internal ConfiguratorSequence(string sequenceId)
        : base(sequenceId)
      {
      }

      internal ConfiguratorSequence(BoundedTo<TGraph, TPrimary>.Sequence sequence)
        : base(sequence)
      {
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithStates(
        System.Action<BoundedTo<TGraph, TPrimary>.Sequence.ContainerAdjusterSequenceStates> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Sequence.ContainerAdjusterSequenceStates adjusterSequenceStates = new BoundedTo<TGraph, TPrimary>.Sequence.ContainerAdjusterSequenceStates(this.Result.States);
        containerAdjuster(adjusterSequenceStates);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions> containerAdjuster)
      {
        return base.WithActions(containerAdjuster) as BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields> containerAdjuster)
      {
        return base.WithFieldStates(containerAdjuster) as BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerAdjuster)
      {
        return base.WithEventHandlers(containerAdjuster) as BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithDescription(
        string description)
      {
        return base.WithDescription(description) as BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        return base.IsSkippedWhen(skipCondition) as BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceFirst()
      {
        this.MoveBefore = MoveObjectInCollection.First;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceBefore<TStateBeforeId>() where TStateBeforeId : IConstant<string>, new()
      {
        return this.PlaceBefore(new TStateBeforeId().Value);
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceBefore(
        string stateBeforeId)
      {
        this.MoveBefore = MoveObjectInCollection.Before;
        this.NearKey = stateBeforeId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore)
      {
        return this.PlaceBefore(((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) stateBefore).GetStateIdentifier());
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceAfter<TStateAfterId>() where TStateAfterId : IConstant<string>, new()
      {
        return this.PlaceAfter(new TStateAfterId().Value);
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceAfter(
        string stateAfterId)
      {
        this.MoveBefore = MoveObjectInCollection.After;
        this.NearKey = stateAfterId;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter)
      {
        return this.PlaceAfter(((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase) stateAfter).GetStateIdentifier());
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnEnterFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.OnLeaveFieldAssignments);
        containerFiller(adjusterAssignment);
        return this;
      }

      public override object Clone()
      {
        return (object) new BoundedTo<TGraph, TPrimary>.Sequence.ConfiguratorSequence(this.Result.CreateCopy());
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Sequence.INeedStatesThenConditionsConfig.WithStates(
        System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithStates((System.Action<BoundedTo<TGraph, TPrimary>.Sequence.ContainerAdjusterSequenceStates>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithDescription(
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithDescription(description);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithActions(
        System.Action<BoundedTo<TGraph, TPrimary>.ActionState.IContainerFillerActions> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithActions((System.Action<BoundedTo<TGraph, TPrimary>.ActionState.ContainerAdjusterActions>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithFieldStates(
        System.Action<BoundedTo<TGraph, TPrimary>.FieldState.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithFieldStates((System.Action<BoundedTo<TGraph, TPrimary>.FieldState.ContainerAdjusterFields>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithEventHandlers(
        System.Action<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithEventHandlers(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.IsSkippedWhen(
        BoundedTo<TGraph, TPrimary>.Condition skipCondition)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.IsSkippedWhen(skipCondition);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceFirst()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceFirst();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore<TStateBeforeId>()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceBefore<TStateBeforeId>();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore(
        string stateBeforeId)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateBeforeId);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceBefore(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateBefore)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceBefore(stateBefore);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter<TStateAfterId>()
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter<TStateAfterId>();
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter(
        string stateAfterId)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateAfterId);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured stateAfter)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.PlaceAfter(stateAfter);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithOnEnterAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithOnEnterAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.BaseFlowStep.INeedAnyConfigState.WithOnLeaveAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.BaseFlowStep.IAllowOptionalConfig) this.WithOnLeaveAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
      }
    }

    public class ContainerAdjusterSequenceStates : 
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.ContainerAdjusterStates,
      BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates
    {
      internal ContainerAdjusterSequenceStates(
        List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase> states)
        : base(states)
      {
      }
    }
  }

  /// <summary>Defines a transition.</summary>
  public class Transition : IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.Transition>
  {
    public bool IsActive { get; set; } = true;

    public string Name { get; set; }

    public string SourceIdentifier { get; set; }

    public string TargetIdentifier { get; set; }

    public string Action { get; set; }

    public bool IsTriggeredOnEventHandler { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition Condition { get; set; }

    public bool DisablePersist { get; set; }

    public bool ValidateNewAction { get; set; }

    internal List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment> FieldAssignments { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();

    internal Readonly.Transition AsReadonly() => Readonly.Transition.From<TGraph, TPrimary>(this);

    internal BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition ToConfigurator()
    {
      return new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(this);
    }

    public BoundedTo<TGraph, TPrimary>.Transition CreateCopy()
    {
      BoundedTo<TGraph, TPrimary>.Transition copy = (BoundedTo<TGraph, TPrimary>.Transition) this.MemberwiseClone();
      copy.FieldAssignments = this.FieldAssignments.Select<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>((Func<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment, BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>) (it => new BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment(it.Result.CreateCopy()))).ToList<BoundedTo<TGraph, TPrimary>.Assignment.ConfiguratorAssignment>();
      return copy;
    }

    /// <summary>Specifies a source state for the current transition.</summary>
    public interface INeedSource
    {
      /// <summary>Specifies a source state for the current transition.</summary>
      /// <typeparam name="TSourceState">A value of the workflow state property.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget From<TSourceState>() where TSourceState : IConstant<string>, new();

      /// <summary>Specifies a source state for the current transition.</summary>
      /// <param name="sourceState">A value for the workflow state property.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget From(string sourceState);

      /// <summary>Specifies a source state for the current transition.</summary>
      /// <param name="from">A configured workflow state.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget From(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured from);
    }

    /// <summary>Specifies a target state for the current transition.</summary>
    public interface INeedTarget
    {
      /// <summary>Specifies a target state for the current transition.</summary>
      /// <typeparam name="TTargetState">A value for the workflow state property.</typeparam>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction To<TTargetState>() where TTargetState : IConstant<string>, new();

      /// <summary>Specifies a target state for the current transition.</summary>
      /// <param name="targetState">A value for the workflow state property.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction To(string targetState);

      /// <summary>Specifies a target state for the current transition.</summary>
      /// <param name="to">A configured workflow state.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction To(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured to);

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction ToNext();

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction ToParentNext();
    }

    /// <summary>
    /// Specifies an action that triggers the current transition.
    /// </summary>
    public interface INeedTriggerAction
    {
      /// <summary>Specifies an action that triggers the current transition.</summary>
      /// <param name="actionSelector">An expression that is used to address the PXAction graph member.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector);

      /// <summary>Specifies an action that triggers the current transition.</summary>
      /// <typeparam name="TExtension">A type of the graph extension where the action is declared.</typeparam>
      /// <param name="actionSelector">An expression that is used to address the PXAction member of a graph extension.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension<TGraph>;

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>Specifies an action that triggers the current transition.</summary>
      /// <param name="actionName">An internal name of an action which will be used to identify the action in the Graph.Actions collection.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(string actionName);

      /// <summary>Specifies an action that triggers the current transition.</summary>
      /// <param name="action">A configured action object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase action);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOnSequenceLeaving();
    }

    /// <summary>
    /// Specifies a condition that is evaluated after an action is executed.
    /// </summary>
    public interface IAllowOptionalConfig : BoundedTo<TGraph, TPrimary>.Transition.IConfigured
    {
      /// <summary>Specifies a condition that will be evaluated after an action is executed to check whether the transition should be performed.</summary>
      /// <param name="condition">A configured condition object.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig When(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>
      /// Sets an ordered list of primary DAC fields that will be updated after the action method is executed.
      /// </summary>
      /// <param name="containerFillerFields">A list of fields to be updated.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to add a transition during which the CROpportunity.ownerID field value should be changed. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// transitions.Add(transition =&gt; transition
      ///         .From(States.Assigned)
      ///         .To(States.New)
      ///         .IsTriggeredOn(actionReject)
      ///         .WithFieldAssignments(fields =&gt;
      ///         {
      ///           fields.Add&lt;CROpportunity.ownerID&gt;(f =&gt; f.SetFromExpression(null));
      ///         })
      ///         );</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig DoesNotPersist();

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceFirst();

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceBefore(string transitionName);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceAfter(string transitionName);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceBefore(
        Func<BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector, BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector> transitionSelector);

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig PlaceAfter(
        Func<BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector, BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector> transitionSelector);
    }

    public interface IConfigured
    {
    }

    public interface ITransitionSelector
    {
      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector To<TTargetState>() where TTargetState : IConstant<string>, new();

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector To(string targetState);

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector To(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured to);

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector ToNext();

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector ToParentNext();

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector);

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension<TGraph>;

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector);

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn(string actionName);

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action);
    }

    public class ConfiguratorTransition : 
      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget,
      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction,
      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.Transition.IConfigured,
      BoundedTo<TGraph, TPrimary>.Transition.INeedSource,
      IOrderableWorkflowElement,
      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector
    {
      internal BoundedTo<TGraph, TPrimary>.Transition Result { get; }

      public MoveObjectInCollection MoveBefore { get; set; }

      public string NearKey { get; set; }

      public string Key => this.Result.Name;

      internal ConfiguratorTransition(string name)
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.Transition()
        {
          Name = name
        };
      }

      internal ConfiguratorTransition()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.Transition();
      }

      internal ConfiguratorTransition(BoundedTo<TGraph, TPrimary>.Transition transition)
      {
        this.Result = transition;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition From<TSourceState>() where TSourceState : IConstant<string>, new()
      {
        return this.From(new TSourceState().Value);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition From(string sourceState)
      {
        this.Result.SourceIdentifier = sourceState;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition From(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured from)
      {
        this.Result.SourceIdentifier = ((BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult) from).GetResult().Identifier;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition To<TTargetState>() where TTargetState : IConstant<string>, new()
      {
        return this.To(new TTargetState().Value);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition To(string targetState)
      {
        this.Result.TargetIdentifier = targetState;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition To(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured to)
      {
        this.Result.TargetIdentifier = ((BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult) to).GetResult().Identifier;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition ToNext()
      {
        this.Result.TargetIdentifier = "@N";
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition ToParentNext()
      {
        this.Result.TargetIdentifier = "@P";
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return this.IsTriggeredOn(((MemberExpression) actionSelector.Body).Member.Name, false);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        return this.IsTriggeredOn(((MemberExpression) actionSelector.Body).Member.Name, false);
      }

      private BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        return this.IsTriggeredOn(((MemberExpression) handlerSelector.Body).Member.Name, true);
      }

      private BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        return this.IsTriggeredOn(((MemberExpression) handlerSelector.Body).Member.Name, true);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn(
        string actionName,
        bool triggeredByEventHandler)
      {
        this.Result.IsTriggeredOnEventHandler = triggeredByEventHandler;
        this.Result.Action = actionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        this.Result.IsTriggeredOnEventHandler = false;
        this.Result.Action = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.Name;
        this.Result.ValidateNewAction = ((BoundedTo<TGraph, TPrimary>.ActionDefinition.ConfiguratorAction) action).Result.CreateNewAction;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase action)
      {
        this.Result.IsTriggeredOnEventHandler = true;
        this.Result.Action = ((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler) action).Result.Name;
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition IsTriggeredOnSequenceLeaving()
      {
        this.Result.IsTriggeredOnEventHandler = true;
        this.Result.Action = "@OnSequenceLeaving";
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition When(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition WhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition WhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerFillerFields)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.FieldAssignments);
        containerFillerFields(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition DoesNotPersist()
      {
        this.Result.DisablePersist = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceBefore(
        string transitionName)
      {
        return this.Place(transitionName, MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceAfter(
        string transitionName)
      {
        return this.Place(transitionName, MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition)
      {
        return this.Place($"{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.SourceIdentifier}To{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.TargetIdentifier}On{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.Action}", MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition)
      {
        return this.Place($"{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.SourceIdentifier}To{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.TargetIdentifier}On{((BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition) transition).Result.Action}", MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceBefore(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition);
        return this.Place($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", MoveObjectInCollection.Before);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceAfter(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition);
        return this.Place($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", MoveObjectInCollection.After);
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition Place(
        string transitionName,
        MoveObjectInCollection place)
      {
        this.MoveBefore = place;
        this.NearKey = transitionName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition PlaceFirst()
      {
        this.MoveBefore = MoveObjectInCollection.First;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget BoundedTo<TGraph, TPrimary>.Transition.INeedSource.From<TSourceState>()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) this.From<TSourceState>();
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget BoundedTo<TGraph, TPrimary>.Transition.INeedSource.From(
        string sourceState)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) this.From(sourceState);
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTarget BoundedTo<TGraph, TPrimary>.Transition.INeedSource.From(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured sourceState)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) this.From(sourceState);
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction BoundedTo<TGraph, TPrimary>.Transition.INeedTarget.To<TTargetState>()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction) this.To<TTargetState>();
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction BoundedTo<TGraph, TPrimary>.Transition.INeedTarget.To(
        string targetState)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction) this.To(targetState);
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction BoundedTo<TGraph, TPrimary>.Transition.INeedTarget.To(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured targetState)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction) this.To(targetState);
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction BoundedTo<TGraph, TPrimary>.Transition.INeedTarget.ToNext()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction) this.To("@N");
      }

      BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction BoundedTo<TGraph, TPrimary>.Transition.INeedTarget.ToParentNext()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction) this.To("@P");
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn<TExtension>(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn(handlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn<TExtension>(handlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn(
        string actionName)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn(actionName, false);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOn(action);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.INeedTriggerAction.IsTriggeredOnSequenceLeaving()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.IsTriggeredOnSequenceLeaving();
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.When(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.When(condition);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFillerFields)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.WithFieldAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFillerFields);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.DoesNotPersist()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.DoesNotPersist();
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceFirst()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.PlaceFirst();
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceBefore(
        string transitionName)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.Place(transitionName, MoveObjectInCollection.Before);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceAfter(
        string transitionName)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.Place(transitionName, MoveObjectInCollection.After);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceBefore(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.PlaceBefore(transition);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceAfter(
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured transition)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.PlaceAfter(transition);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceBefore(
        Func<BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector, BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition.InheritFrom(this.Result, new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition());
        BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector transitionSelector1 = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) configuratorTransition);
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.Place($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", MoveObjectInCollection.Before);
      }

      BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig.PlaceAfter(
        Func<BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector, BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition.InheritFrom(this.Result, new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition());
        BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector transitionSelector1 = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) configuratorTransition);
        return (BoundedTo<TGraph, TPrimary>.Transition.IAllowOptionalConfig) this.Place($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", MoveObjectInCollection.After);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.To<TTargetState>()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.To<TTargetState>();
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.To(
        string targetState)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.To(targetState);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.To(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured to)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.To(to);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.ToNext()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.ToNext();
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.ToParentNext()
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.ToParentNext();
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn(
        Expression<Func<TGraph, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXAction<TPrimary>>> actionSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn<TExtension>(actionSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn(handlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn<TExtension>(handlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn(
        string actionName)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn(actionName, false);
      }

      BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector.IsTriggeredOn(
        BoundedTo<TGraph, TPrimary>.ActionDefinition.IConfigured action)
      {
        return (BoundedTo<TGraph, TPrimary>.Transition.ITransitionSelector) this.IsTriggeredOn(action);
      }

      private static BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition InheritFrom(
        BoundedTo<TGraph, TPrimary>.Transition transition,
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition config)
      {
        return config.From(transition.SourceIdentifier).To(transition.SourceIdentifier).IsTriggeredOn(transition.Action, transition.IsTriggeredOnEventHandler);
      }
    }

    /// <summary>Adds new transitions to the current workflow.</summary>
    public interface IContainerFillerTransitions
    {
      /// <summary>Adds a new transition to the current workflow.</summary>
      /// <param name="transitionName">An internal name of the transition.</param>
      /// <param name="configTransition">A function that configures behavior of the new transition.</param>
      void Add(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition);

      /// <summary>Adds a new transition to the current workflow.</summary>
      /// <param name="configTransition">A function that configures behavior of the new transition.</param>
      /// <remarks>The internal name of the transition will be generated automatically.</remarks>
      /// <example><para>Suppose you need to add a transition with the New source state and the Assigned target state. The transition is triggered with the Assign action. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// transitions.Add(transition =&gt; transition
      ///     .From(States.New)
      ///     .To(States.Assigned)
      ///     .IsTriggeredOn(actionAutoAssign));</code>
      /// </example>
      void Add(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition);

      /// <summary>Adds new transitions to the current workflow. All added transitions will have the same source state.</summary>
      /// <param name="fromState">A source state for new transitions.</param>
      /// <param name="transitions">A function that configures behavior of the new transitions.</param>
      void AddGroupFrom(
        string fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions);

      /// <summary>Adds new transitions to the current workflow. All added transitions will have the same source state.</summary>
      /// <param name="fromState">A source state for new transitions.</param>
      /// <param name="transitions">A function that configures behavior of the new transitions.</param>
      void AddGroupFrom(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions);

      /// <summary>Adds new transitions to the current workflow. All added transitions will have the same source state.</summary>
      /// <typeparam name="TFromState">A source state for new transitions.</typeparam>
      /// <param name="transitions">A function that configures behavior of the new transitions.</param>
      void AddGroupFrom<TFromState>(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
        where TFromState : IConstant<string>, new();
    }

    /// <summary>
    /// Adds new transitions from the current state of the workflow.
    /// </summary>
    public interface ISourceContainerFillerTransitions
    {
      /// <summary>Adds a new transition to the current workflow. The source state of the transition will be the current state of the workflow.</summary>
      /// <param name="transitionName">An internal name of the transition.</param>
      /// <param name="configTransition">A function that configures behavior of the transition.</param>
      void Add(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition);

      /// <summary>Adds a new transition to the current workflow. The source state of the transition will be the current state of the workflow.</summary>
      /// <param name="configTransition">A function that configures behavior of the transition.</param>
      /// <remarks>The internal name of the transition will be generated automatically.</remarks>
      void Add(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition);
    }

    /// <summary>
    /// Adds, replaces, removes, or updates transitions from the current state of the workflow.
    /// </summary>
    public class SourceContainerFillerTransitions : 
      BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions
    {
      private readonly BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions _container;
      private readonly string _fromState;

      public SourceContainerFillerTransitions(
        BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions container,
        string fromState)
      {
        this._container = container;
        this._fromState = fromState;
      }

      public void Add(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(transitionName);
        configuratorTransition.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) configuratorTransition);
        if (WebConfig.EnableWorkflowValidationOnStartup && this._container.Result.Any<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => string.Equals(it.Result.Name, transitionName, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Transition {transitionName} already exists.");
        this._container.Result.Add(configuratorTransition);
      }

      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition control = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        control.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) control);
        control.Result.Name = $"{control.Result.SourceIdentifier}To{control.Result.TargetIdentifier}On{control.Result.Action}";
        if (WebConfig.EnableWorkflowValidationOnStartup && this._container.Result.Any<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => string.Equals(it.Result.Name, control.Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Transition {control.Result.Name} already exists.");
        this._container.Result.Add(control);
      }

      internal void Replace(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition1 = this._container.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (configuratorTransition1 != null)
          this._container.Result.Remove(configuratorTransition1);
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition2 = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(transitionName);
        configuratorTransition2.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) configuratorTransition2);
        this._container.Result.Add(configuratorTransition2);
      }

      public void Replace(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        configuratorTransition.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) configuratorTransition);
        this.Replace($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", configTransition);
      }

      internal void Update(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition1 = this._container.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (WebConfig.EnableWorkflowValidationOnStartup && configuratorTransition1 == null)
          throw new InvalidOperationException($"Transition {transitionName} not found.");
        if (configuratorTransition1 == null)
          return;
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition2 = configTransition(configuratorTransition1);
      }

      public void Update(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector,
        Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        configuratorTransition.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) configuratorTransition);
        this.Update($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", configTransition);
      }

      internal void Remove(string transitionName)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = this._container.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (configuratorTransition == null)
          return;
        this._container.Result.Remove(configuratorTransition);
      }

      public void Remove(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedTarget, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        configuratorTransition.Result.SourceIdentifier = this._fromState;
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedTarget) configuratorTransition);
        this.Remove($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}");
      }
    }

    /// <summary>
    /// Adds, replaces, removes, or updates transitions in the current workflow.
    /// </summary>
    public class ContainerAdjusterTransitions : 
      BoundedTo<TGraph, TPrimary>.Transition.IContainerFillerTransitions
    {
      internal List<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> Result { get; }

      internal ContainerAdjusterTransitions(
        List<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> transitions)
      {
        this.Result = transitions;
      }

      public void Add(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition control = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(transitionName);
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) control);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => string.Equals(it.Result.Name, control.Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Transition {control.Result.Name} already exists.");
        this.Result.Add(control);
      }

      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition control = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) control);
        control.Result.Name = $"{control.Result.SourceIdentifier}To{control.Result.TargetIdentifier}On{control.Result.Action}";
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => string.Equals(it.Result.Name, control.Result.Name, StringComparison.OrdinalIgnoreCase))))
          throw new ArgumentException($"Transition {control.Result.Name} already exists.");
        this.Result.Add(control);
      }

      public void AddGroupFrom(
        string fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
      {
        BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions fillerTransitions = new BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions(this, fromState);
        transitions((BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions) fillerTransitions);
      }

      public void AddGroupFrom(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
      {
        BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions fillerTransitions = new BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions(this, ((BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorState<BoundedTo<TGraph, TPrimary>.FlowState>) fromState).Result.Identifier);
        transitions((BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions) fillerTransitions);
      }

      public void AddGroupFrom<TFromState>(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
        where TFromState : IConstant<string>, new()
      {
        BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions fillerTransitions = new BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions(this, new TFromState().Value);
        transitions((BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions) fillerTransitions);
      }

      /// <summary>Replaces an existing transition with the provided one.</summary>
      /// <param name="transitionName">An internal name of a transition.</param>
      /// <param name="configTransition">A function that configures behaviour of the transition.</param>
      public void Replace(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (configuratorTransition1 != null)
          this.Result.Remove(configuratorTransition1);
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition2 = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(transitionName);
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = configTransition((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition2);
        this.Result.Add(configuratorTransition2);
      }

      /// <summary>Replaces an existing transition with the new one.</summary>
      /// <param name="transitionSelector">A function that allows to get a transition by the source state, the target state and the action.</param>
      /// <param name="configTransition">A function that configures behaviour of the transition.</param>
      public void Replace(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector,
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition);
        this.Replace($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", configTransition);
      }

      public void ReplaceGroupFrom(
        string fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
      {
        this.Result.RemoveAll((Predicate<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>) (transition => transition.Result.SourceIdentifier.Equals(fromState, StringComparison.OrdinalIgnoreCase)));
        BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions fillerTransitions = new BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions(this, fromState);
        transitions((BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions) fillerTransitions);
      }

      public void ReplaceGroupFrom(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
      {
        this.ReplaceGroupFrom(((BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult) fromState).GetResult().Identifier, transitions);
      }

      public void ReplaceGroupFrom<TFromState>(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ISourceContainerFillerTransitions> transitions)
        where TFromState : IConstant<string>, new()
      {
        this.ReplaceGroupFrom(new TFromState().Value, transitions);
      }

      /// <summary>Updates an existing transition with the new one.</summary>
      /// <param name="transitionName">An internal name of a transition.</param>
      /// <param name="configTransition">A function that configures behaviour of the transition.</param>
      public void Update(
        string transitionName,
        Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (WebConfig.EnableWorkflowValidationOnStartup && configuratorTransition1 == null)
          throw new InvalidOperationException($"Transition {transitionName} not found.");
        if (configuratorTransition1 == null)
          return;
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition2 = configTransition(configuratorTransition1);
      }

      /// <summary>Updates an existing transition with the new one.</summary>
      /// <param name="transitionSelector">A function that allows to get a transition by the source state, the target state and the action.</param>
      /// <param name="configTransition">A function that configures behaviour of the transition.</param>
      public void Update(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector,
        Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> configTransition)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition);
        this.Update($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}", configTransition);
      }

      public void UpdateGroupFrom(
        string fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions> transitions)
      {
        BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions fillerTransitions = new BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions(this, fromState);
        transitions(fillerTransitions);
      }

      public void UpdateGroupFrom(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured fromState,
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions> transitions)
      {
        this.UpdateGroupFrom(((BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult) fromState).GetResult().Identifier, transitions);
      }

      public void UpdateGroupFrom<TFromState>(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.SourceContainerFillerTransitions> transitions)
        where TFromState : IConstant<string>, new()
      {
        this.UpdateGroupFrom(new TFromState().Value, transitions);
      }

      /// <summary>Removes an existing transition.</summary>
      /// <param name="transitionName">An internal name of a transition.</param>
      public void Remove(string transitionName)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, bool>) (it => it.Result.Name == transitionName));
        if (configuratorTransition == null)
          return;
        this.Result.Remove(configuratorTransition);
      }

      /// <summary>Removes an existing transition.</summary>
      /// <param name="transitionSelector">A function that allows to get a transition by the source state, the target state and the action.</param>
      public void Remove(
        Func<BoundedTo<TGraph, TPrimary>.Transition.INeedSource, BoundedTo<TGraph, TPrimary>.Transition.IConfigured> transitionSelector)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition configuratorTransition = new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition();
        BoundedTo<TGraph, TPrimary>.Transition.IConfigured configured = transitionSelector((BoundedTo<TGraph, TPrimary>.Transition.INeedSource) configuratorTransition);
        this.Remove($"{configuratorTransition.Result.SourceIdentifier}To{configuratorTransition.Result.TargetIdentifier}On{configuratorTransition.Result.Action}");
      }

      public void RemoveGroupFrom(string fromState)
      {
        this.Result.RemoveAll((Predicate<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>) (transition => transition.Result.SourceIdentifier.Equals(fromState, StringComparison.OrdinalIgnoreCase)));
      }

      public void RemoveGroupFrom(
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.IConfigured fromState)
      {
        this.RemoveGroupFrom(((BoundedTo<TGraph, TPrimary>.BaseFlowStep.IStateResult) fromState).GetResult().Identifier);
      }

      public void RemoveGroupFrom<TFromState>() where TFromState : IConstant<string>, new()
      {
        this.RemoveGroupFrom(new TFromState().Value);
      }
    }
  }

  /// <summary>Defines a workflow.</summary>
  public class Workflow : IWorkflowDeepCopy<BoundedTo<TGraph, TPrimary>.Workflow>
  {
    public const string DefaultFlow = null;

    public bool IsActive { get; set; } = true;

    public string FlowID { get; set; }

    public string FlowSubID { get; set; }

    public string Description { get; set; }

    public List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase> States { get; set; } = new List<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>();

    public List<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition> Transitions { get; set; } = new List<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>();

    internal Readonly.Workflow AsReadonly() => Readonly.Workflow.From<TGraph, TPrimary>(this);

    public BoundedTo<TGraph, TPrimary>.Workflow CreateCopy()
    {
      BoundedTo<TGraph, TPrimary>.Workflow copy = (BoundedTo<TGraph, TPrimary>.Workflow) this.MemberwiseClone();
      copy.States = this.States.Select<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>((Func<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase, BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>) (it => it.Clone() as BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase)).ToList<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ConfiguratorStateBase>();
      copy.Transitions = this.Transitions.Select<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>((Func<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition, BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>) (it => new BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition(it.Result.CreateCopy()))).ToList<BoundedTo<TGraph, TPrimary>.Transition.ConfiguratorTransition>();
      return copy;
    }

    /// <summary>Creates reusable workflows.</summary>
    public class WorkflowBuilder
    {
      public WorkflowBuilder(List<BoundedTo<TGraph, TPrimary>.Workflow> flows)
      {
        this.Flows = flows;
      }

      internal List<BoundedTo<TGraph, TPrimary>.Workflow> Flows { get; }

      /// <summary>Creates a reusable workflow.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the new workflow.</param>
      /// <param name="initializer">A function that specifies behavior of the document in the new workflow.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Workflow.IConfigured Create(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Flows, flowID, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        return (BoundedTo<TGraph, TPrimary>.Workflow.IConfigured) configuratorFlow;
      }

      /// <summary>Creates a reusable workflow.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the new workflow.</typeparam>
      /// <param name="initializer">A function that specifies behavior of the document in the new workflow.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Workflow.IConfigured Create<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> initializer)
        where ID : IConstant<string>, new()
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Flows, new ID().Value, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        return (BoundedTo<TGraph, TPrimary>.Workflow.IConfigured) configuratorFlow;
      }

      /// <summary>Creates a reusable default workflow.</summary>
      /// <param name="initializer">A function that specifies behavior of the document in the new workflow.</param>
      /// <returns></returns>
      public BoundedTo<TGraph, TPrimary>.Workflow.IConfigured CreateDefault(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> initializer)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Flows, (string) null, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = initializer((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        return (BoundedTo<TGraph, TPrimary>.Workflow.IConfigured) configuratorFlow;
      }
    }

    /// <summary>Specifies a list of workflow states.</summary>
    public interface INeedStatesFlow
    {
      /// <summary>Specifies a list of workflow states.</summary>
      /// <param name="containerFiller">A function that configures states of the current workflow.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to update state of a workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// .UpdateDefaultFlow(config1 =&gt; config1.WithFlowStates
      ///     (states =&gt;
      ///     {
      ///       states.Update(States.New, state =&gt; state
      ///        .WithFieldStates(fields =&gt;
      ///        {
      ///          fields.UpdateField&lt;CROpportunity.resolution&gt;(field =&gt;
      ///           field.WithDefaultValue(_reasonUnassign).ComboBoxValues(_reasonUnassign, _reasonCanceled, _reasonOther, _reasonRejected));
      ///        })
      ///       .WithActions(actions =&gt;
      ///       {
      ///         actions.Add(actionAssign, a =&gt; a
      ///             .IsDuplicatedInToolbar());
      /// 
      ///         actions.Add(actionAutoAssign, a =&gt; a
      ///         .IsAutoAction(ownerNotNullCondition));
      ///         actions.Remove("Open");
      ///       }));
      ///       states.Add(assignedState);
      ///       states.Update(States.Open, state =&gt; state
      ///         .WithFieldStates(fields =&gt;
      ///         {
      ///           fields.AddField&lt;CROpportunity.ownerID&gt;(field =&gt;
      ///             field.IsRequired());
      ///         }));
      ///     }))</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig WithFlowStates(
        System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates> containerFiller);
    }

    /// <summary>Defines additional settings for the workflow.</summary>
    public interface IAllowOptionalConfig : BoundedTo<TGraph, TPrimary>.Workflow.IConfigured
    {
      /// <summary>Specifies an ordered list of workflow transitions.</summary>
      /// <param name="containerFiller">A function that configures transitions of the current workflow.</param>
      /// <returns></returns>
      /// <example><para>Suppose you need to remove the NewToOpenOnOpen transition and add a list of new transitions to the current workflow. The example is shown in the following code.</para>
      ///   <code title="Example" lang="CS">
      /// .UpdateDefaultFlow(config1 =&gt; config1.WithTransitions(transitions =&gt;
      ///     {
      ///       transitions.Remove("NewToOpenOnOpen");
      /// 
      ///       transitions.Add(transition =&gt; transition
      ///         .From(States.New)
      ///         .To(States.Assigned)
      ///         .IsTriggeredOn(actionAssign));
      ///       transitions.Add(transition =&gt; transition
      ///         .From(States.New)
      ///         .To(States.Assigned)
      ///         .IsTriggeredOn(actionAutoAssign));
      ///       transitions.Add(transition =&gt; transition
      ///         .From(States.Assigned)
      ///         .To(States.New)
      ///         .IsTriggeredOn(actionReject)
      ///         .WithFieldAssignments(fields =&gt;
      ///         {
      ///           fields.Add&lt;CROpportunity.ownerID&gt;(f =&gt; f.SetFromExpression(null));
      ///         })
      ///         );
      ///       transitions.Add(transition =&gt; transition
      ///         .From(States.Assigned)
      ///         .To(States.Open)
      ///         .IsTriggeredOn(actionAccept));
      ///     })</code>
      /// </example>
      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig WithTransitions(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.IContainerFillerTransitions> containerFiller);

      /// <summary>Specifies the display name for the current workflow.</summary>
      /// <param name="description">A display name for workflow.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig AlteredDescription(
        string description);

      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig WithSubFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerSubFlows> containerFiller);
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorFlow : 
      BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow,
      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig,
      BoundedTo<TGraph, TPrimary>.Workflow.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.Workflow Result { get; }

      internal List<BoundedTo<TGraph, TPrimary>.Workflow> Flows { get; }

      internal ConfiguratorFlow(
        List<BoundedTo<TGraph, TPrimary>.Workflow> flows,
        string flowID,
        string flowSubID)
      {
        this.Flows = flows;
        this.Result = new BoundedTo<TGraph, TPrimary>.Workflow()
        {
          FlowID = flowID,
          FlowSubID = flowSubID
        };
      }

      internal ConfiguratorFlow(
        List<BoundedTo<TGraph, TPrimary>.Workflow> flows,
        BoundedTo<TGraph, TPrimary>.Workflow flow)
      {
        this.Result = flow;
        this.Flows = flows;
      }

      public BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow WithFlowStates(
        System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ContainerAdjusterStates> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.BaseFlowStep.ContainerAdjusterStates containerAdjusterStates = new BoundedTo<TGraph, TPrimary>.BaseFlowStep.ContainerAdjusterStates(this.Result.States);
        containerAdjuster(containerAdjusterStates);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow WithTransitions(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions adjusterTransitions = new BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions(this.Result.Transitions);
        containerAdjuster(adjusterTransitions);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow WithAlteredDescription(
        string description)
      {
        this.Result.Description = description;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow WithSubFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterSubFlows> containerFiller)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterSubFlows adjusterSubFlows = this.Flows != null ? new BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterSubFlows(this.Flows, this.Result) : throw new InvalidOperationException();
        containerFiller(adjusterSubFlows);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow.WithFlowStates(
        System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.IContainerFillerStates> flowStateContainerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig) this.WithFlowStates((System.Action<BoundedTo<TGraph, TPrimary>.BaseFlowStep.ContainerAdjusterStates>) flowStateContainerFiller);
      }

      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig.WithTransitions(
        System.Action<BoundedTo<TGraph, TPrimary>.Transition.IContainerFillerTransitions> transitionContainerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig) this.WithTransitions((System.Action<BoundedTo<TGraph, TPrimary>.Transition.ContainerAdjusterTransitions>) transitionContainerFiller);
      }

      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig.AlteredDescription(
        string description)
      {
        return (BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig) this.WithAlteredDescription(description);
      }

      BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig.WithSubFlows(
        System.Action<BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerSubFlows> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.Workflow.IAllowOptionalConfig) this.WithSubFlows((System.Action<BoundedTo<TGraph, TPrimary>.Workflow.ContainerAdjusterSubFlows>) containerFiller);
      }
    }

    /// <summary>Adds new workflows to the screen configuration.</summary>
    public interface IContainerFillerFlows
    {
      /// <summary>Adds a new workflow with the specified ID to the screen configuration.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      void Add<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
        where ID : IConstant<string>, new();

      /// <summary>Adds a new workflow with the specified ID to the screen configuration.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the current workflow.</param>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      void Add(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config);

      /// <summary>Adds a new workflow to the screen configuration.</summary>
      /// <param name="flow">A configured workflow.</param>
      void Add(
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow);

      /// <summary>Adds a new default workflow to the screen configuration.</summary>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      void AddDefault(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config);

      /// <summary>Adds new workflow based on a predefined workflow (inherited) with the specified ID.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the current workflow.</param>
      /// <param name="flow">A parent workflow (a workflow on which the new workflow will be based on).</param>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      /// <remarks>This method creates a copy of the parent workflow and then modifies it.</remarks>
      void AddInheritedFrom(
        string flowID,
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null);

      /// <summary>Adds new workflow based on a predefined workflow (inherited) with the specified ID.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the current workflow.</param>
      /// <param name="copyFromFlowId">An identifier of the parent workflow (a workflow on which the new workflow will be based on).</param>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      /// <remarks>This method creates copy of original workflow and then modify it.</remarks>
      void AddInheritedFrom(
        string flowID,
        string copyFromFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null);

      /// <summary>Adds a new workflow based on a predefined workflow (inherited) from a default workflow with the specified ID.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the current workflow.</param>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      void AddInheritedFromDefault(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null);

      /// <summary>Adds new workflow based on a predefined workflow (inherited) with the specified ID.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="flow">A parent workflow (a workflow on which the new workflow will be based on).</param>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      void AddInheritedFrom<ID>(
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new();

      /// <summary>Adds new workflow based on a predefined workflow (inherited) with the specified ID.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="copyFromFlowId">An identifier of the parent workflow (a workflow on which the new workflow will be based on).</param>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      void AddInheritedFrom<ID>(
        string copyFromFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new();

      /// <summary>
      /// Adds new inherited from default workflow with the specified id.
      /// </summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      void AddInheritedFromDefault<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new();

      /// <summary>Adds new workflow based on a predefined workflow (inherited) with the specified ID.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <typeparam name="CopyFromID">An identifier of the parent workflow (a workflow on which the new workflow will be based on).</typeparam>
      /// <param name="config">A function that overrides behavior of the document defined in the parent workflow.</param>
      void AddInheritedFrom<ID, CopyFromID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new()
        where CopyFromID : IConstant<string>, new();
    }

    /// <summary>Adds new subflows to the screen configuration.</summary>
    public interface IContainerFillerSubFlows
    {
      /// <summary>Adds a new workflow with the specified ID to the screen configuration.</summary>
      /// <typeparam name="SubID">A value of the property field of a sub-flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      void Add<SubID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
        where SubID : IConstant<string>, new();

      /// <summary>Adds a new workflow with the specified ID to the screen configuration.</summary>
      /// <param name="subFlowID">A value of the property field of a sub-flow type that corresponds to the current workflow.</param>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      void Add(
        string subFlowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config);
    }

    public class ContainerAdjusterFlows : BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerFlows
    {
      internal List<BoundedTo<TGraph, TPrimary>.Workflow> Result { get; }

      internal ContainerAdjusterFlows(List<BoundedTo<TGraph, TPrimary>.Workflow> flows)
      {
        this.Result = flows;
      }

      public void Add<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
        where ID : IConstant<string>, new()
      {
        this.Add(new ID().Value, config);
      }

      public void Add(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flowID, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => string.Equals(it.FlowID, flowID, StringComparison.OrdinalIgnoreCase) && it.FlowSubID == null)))
          throw new ArgumentException($"Workflow {flowID} already exists.");
        this.Result.Add(configuratorFlow.Result);
      }

      public void Add(
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flowResult = ((BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow) flow).Result;
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => string.Equals(it.FlowID, flowResult.FlowID, StringComparison.OrdinalIgnoreCase) && it.FlowSubID == flowResult.FlowSubID)))
          throw new ArgumentException($"Workflow {flowResult.FlowID}-{flowResult.FlowSubID} already exists.");
        this.Result.Add(flowResult);
      }

      public void AddDefault(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, (string) null, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null && it.FlowSubID == null)))
          throw new ArgumentException("Default workflow already exists.");
        this.Result.Add(configuratorFlow.Result);
      }

      public void AddInheritedFrom(
        string flowID,
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configurator = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, ((BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow) flow).Result.CreateCopy());
        configurator.Result.FlowID = flowID;
        configurator.Result.FlowSubID = (string) null;
        configurator.Result.Description = flowID;
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = config(configurator);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => string.Equals(it.FlowID, configurator.Result.FlowID, StringComparison.OrdinalIgnoreCase) && it.FlowSubID == ((BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow) flow).Result.FlowSubID)))
          throw new ArgumentException($"Workflow {configurator.Result.FlowID} already exists.");
        this.Result.Add(configurator.Result);
      }

      public void AddInheritedFrom(
        string flowID,
        string copyFromFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flow1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == copyFromFlowId && it.FlowSubID == null));
        if (flow1 == null)
          return;
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow flow2 = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flow1);
        this.AddInheritedFrom(flowID, (BoundedTo<TGraph, TPrimary>.Workflow.IConfigured) flow2, config);
      }

      public void AddInheritedFromDefault(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flow1 = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == null && it.FlowSubID == null));
        if (flow1 == null)
          return;
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow flow2 = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flow1);
        this.AddInheritedFrom(flowID, (BoundedTo<TGraph, TPrimary>.Workflow.IConfigured) flow2, config);
      }

      public void AddInheritedFrom<ID>(
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured flow,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new()
      {
        this.AddInheritedFrom(new ID().Value, flow, config);
      }

      public void AddInheritedFrom<ID>(
        string copyFromFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new()
      {
        this.AddInheritedFrom(new ID().Value, copyFromFlowId, config);
      }

      public void AddInheritedFromDefault<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new()
      {
        this.AddInheritedFromDefault(new ID().Value, config);
      }

      public void AddInheritedFrom<ID, CopyFromID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config = null)
        where ID : IConstant<string>, new()
        where CopyFromID : IConstant<string>, new()
      {
        this.AddInheritedFrom(new ID().Value, new CopyFromID().Value, config);
      }

      /// <summary>Replaces an existing workflow with the provided one.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Replace<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
        where ID : IConstant<string>, new()
      {
        this.Replace(new ID().Value, config);
      }

      /// <summary>Replaces an existing workflow with the new one.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the removed workflow.</param>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Replace(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow, BoundedTo<TGraph, TPrimary>.Workflow.IConfigured> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flowID, (string) null);
        BoundedTo<TGraph, TPrimary>.Workflow.IConfigured configured = config((BoundedTo<TGraph, TPrimary>.Workflow.INeedStatesFlow) configuratorFlow);
        BoundedTo<TGraph, TPrimary>.Workflow workflow = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == flowID && it.FlowSubID == null));
        if (workflow != null)
          this.Result.Remove(workflow);
        this.Result.Add(configuratorFlow.Result);
      }

      /// <summary>Updates an existing workflow.</summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Update<ID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
        where ID : IConstant<string>, new()
      {
        this.Update(new ID().Value, config);
      }

      /// <summary>Updates an existing workflow.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the removed workflow.</param>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Update(
        string flowID,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flow = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == flowID && it.FlowSubID == null));
        if (WebConfig.EnableWorkflowValidationOnStartup && flow == null)
          throw new InvalidOperationException($"Workflow {flowID} not found.");
        if (flow == null)
          return;
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow1 = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flow);
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow2 = config(configuratorFlow1);
      }

      /// <summary>
      /// Removes an existing workflow from the screen configuration.
      /// </summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the removed workflow.</typeparam>
      public void Remove<ID>() where ID : IConstant<string>, new() => this.Remove(new ID().Value);

      /// <summary>
      /// Removes an existing workflow from the screen configuration.
      /// </summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the removed workflow.</param>
      public void Remove(string flowID)
      {
        BoundedTo<TGraph, TPrimary>.Workflow workflow = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == flowID && it.FlowSubID == null));
        if (workflow == null)
          return;
        this.Result.Remove(workflow);
      }
    }

    public class ContainerAdjusterSubFlows : 
      BoundedTo<TGraph, TPrimary>.Workflow.IContainerFillerSubFlows
    {
      internal List<BoundedTo<TGraph, TPrimary>.Workflow> Result { get; }

      internal BoundedTo<TGraph, TPrimary>.Workflow FlowId { get; }

      internal ContainerAdjusterSubFlows(
        List<BoundedTo<TGraph, TPrimary>.Workflow> flows,
        BoundedTo<TGraph, TPrimary>.Workflow flowId)
      {
        this.FlowId = flowId;
        this.Result = flows;
      }

      public void Add<SubID>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
        where SubID : IConstant<string>, new()
      {
        this.Add(new SubID().Value, config);
      }

      public void Add(
        string subFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
      {
        string flowId = this.FlowId.FlowID;
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configurator = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, this.FlowId.CreateCopy());
        configurator.Result.FlowID = flowId;
        configurator.Result.FlowSubID = subFlowId;
        configurator.Result.Description = $"{this.FlowId.FlowID}-{subFlowId}";
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow = config(configurator);
        }
        if (WebConfig.EnableWorkflowValidationOnStartup && this.Result.Any<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == configurator.Result.FlowID && it.FlowSubID == configurator.Result.FlowSubID)))
          throw new ArgumentException($"Workflow {configurator.Result.FlowID}-{configurator.Result.FlowSubID} already exists.");
        this.Result.Add(configurator.Result);
      }

      /// <summary>Updates an existing workflow.</summary>
      /// <typeparam name="SubId">A value of the property field of a flow type that corresponds to the current workflow.</typeparam>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Update<SubId>(
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
        where SubId : IConstant<string>, new()
      {
        this.Update(new SubId().Value, config);
      }

      /// <summary>Updates an existing workflow.</summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the removed workflow.</param>
      /// <param name="config">A function that specifies behavior of the document in the current workflow.</param>
      public void Update(
        string subFlowId,
        Func<BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow, BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow> config)
      {
        BoundedTo<TGraph, TPrimary>.Workflow flow = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == this.FlowId.FlowID && it.FlowSubID == subFlowId));
        if (WebConfig.EnableWorkflowValidationOnStartup && flow == null)
          throw new InvalidOperationException($"Workflow {this.FlowId.FlowID}-{subFlowId} not found.");
        if (flow == null)
          return;
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow1 = new BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow(this.Result, flow);
        BoundedTo<TGraph, TPrimary>.Workflow.ConfiguratorFlow configuratorFlow2 = config(configuratorFlow1);
      }

      /// <summary>
      /// Removes an existing workflow from the screen configuration.
      /// </summary>
      /// <typeparam name="ID">A value of the property field of a flow type that corresponds to the removed workflow.</typeparam>
      public void Remove<SubId>() where SubId : IConstant<string>, new()
      {
        this.Remove(new SubId().Value);
      }

      /// <summary>
      /// Removes an existing workflow from the screen configuration.
      /// </summary>
      /// <param name="flowID">A value of the property field of a flow type that corresponds to the removed workflow.</param>
      public void Remove(string subFlowId)
      {
        BoundedTo<TGraph, TPrimary>.Workflow workflow = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.Workflow>((Func<BoundedTo<TGraph, TPrimary>.Workflow, bool>) (it => it.FlowID == this.FlowId.FlowID && it.FlowSubID == subFlowId));
        if (workflow == null)
          return;
        this.Result.Remove(workflow);
      }
    }

    public class defaultFlow : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      BoundedTo<TGraph, TPrimary>.Workflow.defaultFlow>
    {
      public defaultFlow()
        : base((string) null)
      {
      }
    }
  }

  /// <summary>Defines a workflow event handler.</summary>
  public class WorkflowEventHandler
  {
    public string HandlerName { get; set; }

    internal Readonly.WorkflowEventHandler AsReadonly()
    {
      return Readonly.WorkflowEventHandler.From<TGraph, TPrimary>(this);
    }

    public interface IConfigured
    {
    }

    public class ConfiguratorWorkflowEventHandler : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IConfigured
    {
      internal BoundedTo<TGraph, TPrimary>.WorkflowEventHandler Result { get; set; }

      protected ConfiguratorWorkflowEventHandler(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandler result)
      {
        this.Result = result;
      }

      internal ConfiguratorWorkflowEventHandler()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandler();
      }
    }

    /// <summary>
    /// Subscribes an event handler defined in a graph to a workflow state.
    /// </summary>
    public interface IContainerFillerHandlers
    {
      /// <summary>
      /// Subscribes an event handler defined in a graph to a workflow state. You specify an event handler by selecting it from a graph.
      /// </summary>
      /// <param name="handlerSelector">An expression that selects an event handler from the graph.</param>
      void Add(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary>>> handlerSelector);

      /// <summary>
      /// Subscribes an event handler defined in a graph extension to a workflow state. You specify an event handler by selecting it from a graph extension.
      /// </summary>
      /// <typeparam name="TExtension">A type of the graph extension.</typeparam>
      /// <param name="handlerSelector">An expression that selects an event handler from the graph extension.</param>
      void Add<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>
      /// Subscribes an event handler defined in a graph to a workflow state. You specify an event handler by selecting it from a graph.
      /// </summary>
      /// <typeparam name="TEventTarget">A type of the event.</typeparam>
      /// <param name="handlerSelector">An expression that selects an event handler from the graph.</param>
      void Add<TEventTarget>(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget>>> handlerSelector)
        where TEventTarget : class, IBqlTable, new();

      /// <summary>
      /// Subscribes an event handler defined in a graph to a workflow state. You specify an event handler by selecting from a graph.
      /// </summary>
      /// <typeparam name="TEventTarget">A type of the event.</typeparam>
      /// <typeparam name="TParams">A type of the event handler parameter.</typeparam>
      /// <param name="handlerSelector">An expression that selects an event handlers from the graph.</param>
      void Add<TEventTarget, TParams>(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget, TParams>>> handlerSelector)
        where TEventTarget : class, IBqlTable, new()
        where TParams : class, IBqlTable, new();

      /// <summary>Subscribes an event handler to a workflow state.</summary>
      /// <param name="handler">An event handler that should be added to the state.</param>
      void Add(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handler);
    }

    /// <summary>
    /// Subscribes event handlers defined in a graph to a workflow state.
    /// </summary>
    public class ContainerAdjusterHandlers : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.IContainerFillerHandlers
    {
      internal List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler> Result { get; }

      internal ContainerAdjusterHandlers(
        List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandler> actionStates)
      {
        this.Result = actionStates;
      }

      /// <inheritdoc />
      public void Add(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary>>> handlerSelector)
      {
        this.InternalAdd(handlerSelector.Body);
      }

      /// <inheritdoc />
      public void Add<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.InternalAdd(handlerSelector.Body);
      }

      /// <inheritdoc />
      public void Add<TEventTarget>(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget>>> handlerSelector)
        where TEventTarget : class, IBqlTable, new()
      {
        this.InternalAdd(handlerSelector.Body);
      }

      /// <inheritdoc />
      public void Add<TEventTarget, TParams>(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget, TParams>>> handlerSelector)
        where TEventTarget : class, IBqlTable, new()
        where TParams : class, IBqlTable, new()
      {
        this.InternalAdd(handlerSelector.Body);
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handler)
      {
        if (!(handler is BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler workflowEventHandler) || workflowEventHandler.Result == null || workflowEventHandler.Result.Name == null)
          throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
        this.Result.Add(new BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.ConfiguratorWorkflowEventHandler()
        {
          Result = {
            HandlerName = workflowEventHandler.Result.Name
          }
        }.Result);
      }

      private void InternalAdd(Expression handlerSelectorBody)
      {
        if (!(handlerSelectorBody is MemberExpression memberExpression) || memberExpression == null || memberExpression.Member == (MemberInfo) null || memberExpression.Member.Name == null)
          throw new PXArgumentException(nameof (handlerSelectorBody), "The argument cannot be null.");
        this.Result.Add(new BoundedTo<TGraph, TPrimary>.WorkflowEventHandler.ConfiguratorWorkflowEventHandler()
        {
          Result = {
            HandlerName = memberExpression.Member.Name
          }
        }.Result);
      }
    }
  }

  /// <summary>Specifies a workflow event handler definition.</summary>
  public class WorkflowEventHandlerDefinition : BoundedTo<TGraph, TPrimary>.ActionDefinitionBase
  {
    public string DisplayName { get; set; }

    public System.Type SourceType { get; set; }

    public string EventContainerName { get; set; }

    public string EventName { get; set; }

    public BoundedTo<TGraph, TPrimary>.Condition Condition { get; set; }

    public System.Type Select { get; set; }

    public bool AllowMultipleSelect { get; set; }

    public bool UseTargetAsPrimarySource { get; set; }

    public bool UseParameterAsPrimarySource { get; set; }

    public System.Type UpcastType { get; set; }

    internal Readonly.WorkflowEventHandlerDefinition AsReadonly()
    {
      return Readonly.WorkflowEventHandlerDefinition.From<TGraph, TPrimary>(this);
    }

    public interface INeedEventTarget
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget> WithTargetOf<TEventTarget>() where TEventTarget : class, IBqlTable, new();
    }

    public interface INeedEventContainer<TEventTarget> where TEventTarget : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfActionCompletion<TEventGraph>(
        Expression<Func<TEventGraph, PXAction<TEventTarget>>> eventSelector)
        where TEventGraph : PXGraph;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfActionCompletion<TEventGraph, TExtension>(
        Expression<Func<TExtension, PXAction<TEventTarget>>> eventSelector)
        where TEventGraph : PXGraph
        where TExtension : PXGraphExtension<TEventGraph>;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget>>> eventSelector)
        where TEventContainer : PXEntityEventBase<TEventTarget>.Container<TEventContainer>;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget, TEventParams> WithParametersOf<TEventParams>() where TEventParams : class, IBqlTable, new();

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfFieldUpdated<TField>() where TField : IBqlField;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfFieldsUpdated<TFields>() where TFields : ITypeArrayOf<IBqlField>;
    }

    /// <summary>Subscribes an event handler to an event.</summary>
    /// <typeparam name="TEventTarget">The type of the event.</typeparam>
    public interface INeedSubscriber<TEventTarget> where TEventTarget : class, IBqlTable, new()
    {
      /// <summary>
      /// Declares an event handler defined in a graph to an event. You specify the event handler by selecting it from the graph.
      /// </summary>
      /// <param name="eventHandlerSelector">An expression that selects an event handler from the graph.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector);

      /// <summary>
      /// Declares an event handler defined in a graph extension to an event. You specify the event handler by selecting it from the graph extension.
      /// </summary>
      /// <typeparam name="TExtension">A type of the graph extension.</typeparam>
      /// <param name="eventHandlerSelector">An expression that selects an event handler from the graph extension.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>Declares an event handler to an event.</summary>
      /// <param name="eventHandlerDefinition">An event handler that should be subsribed.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget> eventHandlerDefinition);

      /// <summary>
      /// Declares an event handler to an event. This method allows you to specify the event handler by its name.
      /// </summary>
      /// <param name="eventHandlerName">A name of an event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> IsNew(
        string eventHandlerName);
    }

    /// <summary>Specifies a subscriber for the event.</summary>
    /// <typeparam name="TEventTarget">The type of the event.</typeparam>
    /// <typeparam name="TEventParams">The event parameters.</typeparam>
    public interface INeedSubscriber<TEventTarget, TEventParams> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>
      where TEventTarget : class, IBqlTable, new()
      where TEventParams : class, IBqlTable, new()
    {
      /// <summary>
      /// Declares an event handler defined in a graph to an event. You specify the event handler by selecting it from the graph.
      /// </summary>
      /// <param name="eventHandlerSelector">An expression that selects an event handler from the graph.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventHandlerSelector);

      /// <summary>
      /// Declares an event handler defined in a graph extension to an event. You specify the event handler by selecting it from the graph extension.
      /// </summary>
      /// <typeparam name="TExtension">A type of the graph extension.</typeparam>
      /// <param name="eventHandlerSelector">An expression that selects an event handler from the graph extension.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventHandlerSelector)
        where TExtension : PXGraphExtension<TGraph>;

      /// <summary>
      /// Declares an event handler to an event. This method allows to define the event handler in the parameter.
      /// </summary>
      /// <param name="eventHandlerDefinition">A definition of an event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TEventParams> eventHandlerDefinition);

      /// <summary>
      /// Declares an event handler to an event. This method allows you to specify the event handler by its name.
      /// </summary>
      /// <param name="eventHandlerName">A name of the event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> IsNew(
        string eventHandlerName);
    }

    public interface INeedEventContainer<TEventTarget, TEventParams>
      where TEventTarget : class, IBqlTable, new()
      where TEventParams : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams> OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget, TEventParams>>> eventSelector)
        where TEventContainer : PXEntityEventBase<TEventTarget>.Container<TEventContainer>;
    }

    public interface INeedEventPrimaryEntityGetter<TEventTarget> where TEventTarget : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords = false)
        where TSelect : IBqlSelect<TPrimary>;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords = false);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> UsesTargetAsPrimaryEntity();
    }

    public interface INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>
      where TEventTarget : class, IBqlTable, new()
      where TEventParams : class, IBqlTable, new()
    {
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords = false)
        where TSelect : IBqlSelect<TPrimary>;

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords = false);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> UsesTargetAsPrimaryEntity();

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> UsesParameterAsPrimaryEntity();
    }

    /// <summary>
    /// Defines additional settings for the workflow event handler.
    /// </summary>
    public interface IAllowOptionalConfigAction : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase
    {
      /// <summary>
      /// Allows you to set an ordered list of primary DAC fields that will be updated before the action is executed.
      /// </summary>
      /// <param name="containerFiller">Field updates definition for current action.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Sets the display name of an event handler.</summary>
      /// <param name="displayName">Display name of an event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction DisplayName(
        string displayName);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new();
    }

    /// <summary>
    /// Defines additional settings for the workflow event handler.
    /// </summary>
    public interface IAllowOptionalConfigAction<TEventTarget> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget>
      where TEventTarget : class, IBqlTable, new()
    {
      /// <summary>
      /// Allows you to set an ordered list of primary DAC fields that will be updated before the action is executed.
      /// </summary>
      /// <param name="containerFiller">Field updates definition for current action.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Sets the display name of an event handler.</summary>
      /// <param name="displayName">Display name of an event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> DisplayName(
        string displayName);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new();
    }

    /// <summary>
    /// Defines additional settings for the workflow event handler.
    /// </summary>
    public interface IAllowOptionalConfigAction<TEventTarget, TParams> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TParams>
      where TEventTarget : class, IBqlTable, new()
      where TParams : class, IBqlTable, new()
    {
      /// <summary>
      /// Allows to set an ordered list of primary DAC fields, that will be updated before action executed.
      /// </summary>
      /// <param name="containerFiller">Field updates definition for current action.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TParams> WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TParams> AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition);

      /// <summary>Sets the display name of an event handler.</summary>
      /// <param name="displayName">Display name of an event handler.</param>
      /// <returns></returns>
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TParams> DisplayName(
        string displayName);

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TParams> WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new();
    }

    public interface IHandlerConfiguredBase
    {
    }

    public interface IConfigured<TEventTarget> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase
      where TEventTarget : class, IBqlTable, new()
    {
    }

    public interface IConfigured<TEventTarget, TParams> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase
      where TEventTarget : class, IBqlTable, new()
      where TParams : class, IBqlTable, new()
    {
    }

    public class ConfiguratorWorkflowEventHandler : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget
    {
      internal BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition Result { get; set; }

      protected ConfiguratorWorkflowEventHandler(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition result)
      {
        this.Result = result;
      }

      internal ConfiguratorWorkflowEventHandler()
      {
        this.Result = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition();
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler AppliesWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler AppliesWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> WithTargetOf<TEventTarget>() where TEventTarget : class, IBqlTable, new()
      {
        this.Result.SourceType = typeof (TEventTarget);
        return new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget>(this.Result);
      }

      protected BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler FieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment> containerAdjuster)
      {
        BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment adjusterAssignment = new BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment(this.Result.FieldAssignments);
        containerAdjuster(adjusterAssignment);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        this.FieldAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler DisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new()
      {
        this.Result.UpcastType = typeof (TEntity);
        return this;
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction) this.WithFieldAssignments(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction.AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction) this.AppliesWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction) this.DisplayName(displayName);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction.WithUpcastTo<TEntity>()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction) this.WithUpcastTo<TEntity>();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget.WithTargetOf<TEventTarget>()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>) this.WithTargetOf<TEventTarget>();
      }
    }

    public class ConfiguratorWorkflowEventHandler<TEventTarget> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget>
      where TEventTarget : class, IBqlTable, new()
    {
      public ConfiguratorWorkflowEventHandler(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition result)
        : base(result)
      {
      }

      public ConfiguratorWorkflowEventHandler()
      {
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        this.FieldAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> AppliesWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> AppliesWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> DisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new()
      {
        this.Result.UpcastType = typeof (TEntity);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords = false)
        where TSelect : IBqlSelect<TPrimary>
      {
        this.Result.Select = BqlCommandDecorator.Unwrap(typeof (TSelect));
        this.Result.AllowMultipleSelect = allowSelectMultipleRecords;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords = false)
      {
        PXGraph instance = PXGraph.CreateInstance<PXGraph>();
        this.Result.Select = (Activator.CreateInstance(viewSelector.Body.Type, (object) instance) as PXSelectBase<TPrimary>).View.BqlSelect.GetSelectType();
        this.Result.AllowMultipleSelect = allowSelectMultipleRecords;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> UsesTargetAsPrimaryEntity()
      {
        if (!typeof (TPrimary).IsAssignableFrom(typeof (TEventTarget)))
          throw new ArgumentException($"Event target of type {typeof (TEventTarget).FullName} could not be used in event handler {this.Result.Name} as a primary source for {typeof (TPrimary).FullName}.");
        this.Result.UseTargetAsPrimarySource = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> OfActionCompletion<TEventGraph>(
        Expression<Func<TEventGraph, PXAction<TEventTarget>>> eventSelector)
        where TEventGraph : PXGraph
      {
        this.Result.EventContainerName = typeof (TEventGraph).FullName;
        this.Result.EventName = ((MemberExpression) eventSelector.Body).Member.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> OfActionCompletion<TEventGraph, TExtension>(
        Expression<Func<TExtension, PXAction<TEventTarget>>> eventSelector)
        where TEventGraph : PXGraph
        where TExtension : PXGraphExtension<TEventGraph>
      {
        this.Result.EventContainerName = typeof (TEventGraph).FullName;
        this.Result.EventName = ((MemberExpression) eventSelector.Body).Member.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget>>> eventSelector)
        where TEventContainer : PXEntityEventBase<TEventTarget>.Container<TEventContainer>
      {
        this.Result.EventContainerName = ((MemberExpression) eventSelector.Body).Member.DeclaringType.FullName;
        this.Result.EventName = ((MemberExpression) eventSelector.Body).Member.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> WithParametersOf<TEventParams>() where TEventParams : class, IBqlTable, new()
      {
        return new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams>(this.Result);
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfFieldUpdated<TField>() where TField : IBqlField
      {
        this.Result.EventContainerName = $"@{typeof (TEventTarget).FullName}PropertyChanged";
        this.Result.EventName = typeof (TField).Name;
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>) this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> OfFieldsUpdated<TFields>() where TFields : ITypeArrayOf<IBqlField>
      {
        this.Result.EventContainerName = $"@{typeof (TEventTarget).FullName}PropertyChanged";
        this.Result.EventName = string.Join("|", ((IEnumerable<System.Type>) TypeArray.CheckAndExtract<IBqlField>(typeof (TFields), (string) null)).Select<System.Type, string>((Func<System.Type, string>) (it => it.Name)).ToArray<string>());
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>) this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector)
      {
        this.Result.Name = this.GetEventHandlerName(eventHandlerSelector.Body);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Result.Name = this.GetEventHandlerName(eventHandlerSelector.Body);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget> eventHandlerDefinition)
      {
        if (!(eventHandlerDefinition is BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> workflowEventHandler) || workflowEventHandler.Result == null || workflowEventHandler.Result.Name == null)
          throw new PXArgumentException(nameof (eventHandlerDefinition), "The argument cannot be null.");
        this.Result.Name = workflowEventHandler.Result.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> IsNew(
        string eventHandlerName)
      {
        this.Result.Name = eventHandlerName;
        this.Result.CreateNewAction = true;
        return this;
      }

      private string GetEventHandlerName(Expression eventHandlerSelectorBody)
      {
        if (!(eventHandlerSelectorBody is MemberExpression memberExpression) || memberExpression == null || memberExpression.Member == (MemberInfo) null || memberExpression.Member.Name == null)
          throw new PXArgumentException(nameof (eventHandlerSelectorBody), "The argument cannot be null.");
        return memberExpression.Member.Name;
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>.UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.UsesPrimaryEntityGetter<TSelect>(allowSelectMultipleRecords);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>.UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.UsesPrimaryEntityGetter(viewSelector, allowSelectMultipleRecords);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>.UsesTargetAsPrimaryEntity()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.UsesTargetAsPrimaryEntity();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.OfActionCompletion<TEventGraph>(
        Expression<Func<TEventGraph, PXAction<TEventTarget>>> eventSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>) this.OfActionCompletion<TEventGraph>(eventSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.OfActionCompletion<TEventGraph, TExtension>(
        Expression<Func<TExtension, PXAction<TEventTarget>>> eventSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>) this.OfActionCompletion<TEventGraph, TExtension>(eventSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget>>> eventSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>) this.OfEntityEvent<TEventContainer>(eventSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.OfFieldUpdated<TField>()
      {
        return this.OfFieldUpdated<TField>();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.OfFieldsUpdated<TFields>()
      {
        return this.OfFieldsUpdated<TFields>();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget>.WithParametersOf<TEventParams>()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget, TEventParams>) this.WithParametersOf<TEventParams>();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>.Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>) this.Is(eventHandlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>.Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget>>> eventHandlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>) this.Is<TExtension>(eventHandlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>.Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget> eventHandlerDefinition)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>) this.Is(eventHandlerDefinition);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>.IsNew(
        string eventHandlerName)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>) this.IsNew(eventHandlerName);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.WithFieldAssignments(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>.AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.AppliesWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.DisplayName(displayName);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>.WithUpcastTo<TEntity>()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget>) this.WithUpcastTo<TEntity>();
      }
    }

    public class ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget, TEventParams>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TEventParams>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>,
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction
      where TEventTarget : class, IBqlTable, new()
      where TEventParams : class, IBqlTable, new()
    {
      public ConfiguratorWorkflowEventHandler(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition result)
        : base(result)
      {
      }

      public ConfiguratorWorkflowEventHandler()
      {
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        this.FieldAssignments((System.Action<BoundedTo<TGraph, TPrimary>.Assignment.FieldContainerAdjusterAssignment>) containerFiller);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> AppliesWhenAlso(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition &= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> AppliesWhenElse(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        if (this.Result.Condition != null)
          this.Result.Condition |= (BoundedTo<TGraph, TPrimary>.Condition) condition;
        else
          this.Result.Condition = (BoundedTo<TGraph, TPrimary>.Condition) condition;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> DisplayName(
        string displayName)
      {
        this.Result.DisplayName = displayName;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> WithUpcastTo<TEntity>() where TEntity : class, IBqlTable, new()
      {
        this.Result.UpcastType = typeof (TEntity);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords = false)
        where TSelect : IBqlSelect<TPrimary>
      {
        this.Result.Select = BqlCommandDecorator.Unwrap(typeof (TSelect));
        this.Result.AllowMultipleSelect = allowSelectMultipleRecords;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords = false)
      {
        PXGraph instance = PXGraph.CreateInstance<PXGraph>();
        this.Result.Select = (Activator.CreateInstance(viewSelector.Body.Type, (object) instance) as PXSelectBase<TPrimary>).View.BqlSelect.GetSelectType();
        this.Result.AllowMultipleSelect = allowSelectMultipleRecords;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> UsesTargetAsPrimaryEntity()
      {
        if (!typeof (TPrimary).IsAssignableFrom(typeof (TEventTarget)))
          throw new ArgumentException($"Event target of type {typeof (TEventTarget).FullName} could not be used in event handler {this.Result.Name} as a primary source for {typeof (TPrimary).FullName}.");
        this.Result.UseTargetAsPrimarySource = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Result.Name = this.GetEventHandlerName(eventSelector.Body);
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TEventParams> eventHandlerDefinition)
      {
        if (!(eventHandlerDefinition is BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> workflowEventHandler) || workflowEventHandler.Result == null || workflowEventHandler.Result.Name == null)
          throw new PXArgumentException(nameof (eventHandlerDefinition), "The argument cannot be null.");
        this.Result.Name = workflowEventHandler.Result.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventHandlerSelector)
      {
        this.Result.Name = this.GetEventHandlerName(eventHandlerSelector.Body);
        return this;
      }

      private string GetEventHandlerName(Expression eventHandlerSelectorBody)
      {
        if (!(eventHandlerSelectorBody is MemberExpression memberExpression) || memberExpression == null || memberExpression.Member == (MemberInfo) null || memberExpression.Member.Name == null)
          throw new PXArgumentException(nameof (eventHandlerSelectorBody), "The argument cannot be null.");
        return memberExpression.Member.Name;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> IsNew(
        string eventHandlerName)
      {
        this.Result.Name = eventHandlerName;
        this.Result.CreateNewAction = true;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget, TEventParams>>> eventSelector)
        where TEventContainer : PXEntityEventBase<TEventTarget>.Container<TEventContainer>
      {
        this.Result.EventContainerName = ((MemberExpression) eventSelector.Body).Member.DeclaringType.FullName;
        this.Result.EventName = ((MemberExpression) eventSelector.Body).Member.Name;
        return this;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TEventParams> UsesParameterAsPrimaryEntity()
      {
        if (!typeof (TPrimary).IsAssignableFrom(typeof (TEventParams)))
          throw new ArgumentException($"Parameter of type {typeof (TEventParams).FullName} could not be used in event handler {this.Result.Name} as a primary source for {typeof (TPrimary).FullName}.");
        this.Result.UseParameterAsPrimarySource = true;
        return this;
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>.Is(
        Expression<Func<TGraph, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventHandlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>) this.Is(eventHandlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>.Is<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandler<TPrimary, TEventTarget, TEventParams>>> eventHandlerSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>) this.Is<TExtension>(eventHandlerSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>.Is(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TEventParams> eventHandlerDefinition)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>) this.Is(eventHandlerDefinition);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>.IsNew(
        string eventHandlerName)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>) this.IsNew(eventHandlerName);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>.AppliesWhen(
        BoundedTo<TGraph, TPrimary>.ISharedCondition condition)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.AppliesWhen(condition);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>.DisplayName(
        string displayName)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.DisplayName(displayName);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>.WithUpcastTo<TEntity>()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.WithUpcastTo<TEntity>();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>.WithFieldAssignments(
        System.Action<BoundedTo<TGraph, TPrimary>.Assignment.IContainerFillerFields> containerFiller)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.WithFieldAssignments(containerFiller);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>.UsesPrimaryEntityGetter<TSelect>(
        bool allowSelectMultipleRecords)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.UsesPrimaryEntityGetter<TSelect>(allowSelectMultipleRecords);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>.UsesPrimaryEntityGetter(
        Expression<Func<TGraph, PXSelectBase<TPrimary>>> viewSelector,
        bool allowSelectMultipleRecords)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.UsesPrimaryEntityGetter(viewSelector, allowSelectMultipleRecords);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>.UsesTargetAsPrimaryEntity()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.UsesTargetAsPrimaryEntity();
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventContainer<TEventTarget, TEventParams>.OfEntityEvent<TEventContainer>(
        Expression<Func<TEventContainer, PXEntityEvent<TEventTarget, TEventParams>>> eventSelector)
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedSubscriber<TEventTarget, TEventParams>) this.OfEntityEvent<TEventContainer>(eventSelector);
      }

      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams> BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<TEventTarget, TEventParams>.UsesParameterAsPrimaryEntity()
      {
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<TEventTarget, TEventParams>) this.UsesParameterAsPrimaryEntity();
      }
    }

    public class EventHandlerBuilder
    {
      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget> Create<TEventTarget>(
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget>> initializer)
        where TEventTarget : class, IBqlTable, new()
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget> workflowEventHandler = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget>();
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget> configured = initializer((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget) workflowEventHandler);
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget>) workflowEventHandler;
      }

      public BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TParams> Create<TEventTarget, TParams>(
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TParams>> initializer)
        where TEventTarget : class, IBqlTable, new()
        where TParams : class, IBqlTable, new()
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TParams> workflowEventHandler = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler<TEventTarget, TParams>();
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TParams> configured = initializer((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget) workflowEventHandler);
        return (BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IConfigured<TEventTarget, TParams>) workflowEventHandler;
      }
    }

    public interface IContainerFillerHandlers
    {
      void Add(
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config);

      void Add(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase action);
    }

    public class ContainerAdjusterHandlers : 
      BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IContainerFillerHandlers
    {
      internal List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition> Result { get; }

      internal ContainerAdjusterHandlers(
        List<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition> actions)
      {
        this.Result = actions;
      }

      /// <inheritdoc />
      public void Add(
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config = null)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler workflowEventHandler = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler();
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handlerConfiguredBase = config((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget) workflowEventHandler);
        }
        this.Result.Add(workflowEventHandler.Result);
      }

      /// <inheritdoc />
      public void Add(
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handler)
      {
        this.Result.Add(((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler) handler).Result);
      }

      public void Replace(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config = null)
      {
        this.Replace(((MemberExpression) handlerSelector.Body).Member.Name, config);
      }

      public void Replace<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config = null)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Replace(((MemberExpression) handlerSelector.Body).Member.Name, config);
      }

      public void Replace(
        string handlerName,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config = null)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition handlerDefinition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition, bool>) (it => string.Equals(it.Name, handlerName, StringComparison.OrdinalIgnoreCase)));
        if (handlerDefinition != null)
          this.Result.Remove(handlerDefinition);
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler workflowEventHandler1 = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler();
        workflowEventHandler1.Result.Name = handlerName;
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler workflowEventHandler2 = workflowEventHandler1;
        if (config != null)
        {
          BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handlerConfiguredBase = config((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.INeedEventTarget) workflowEventHandler2);
        }
        this.Result.Add(workflowEventHandler2.Result);
      }

      public void Update(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config)
      {
        this.Update(((MemberExpression) handlerSelector.Body).Member.Name, config);
      }

      public void Update<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Update(((MemberExpression) handlerSelector.Body).Member.Name, config);
      }

      public void Update(
        string handlerName,
        Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase> config)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition handlerDefinition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition, bool>) (it => string.Equals(it.Name, handlerName, StringComparison.OrdinalIgnoreCase)));
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler workflowEventHandler = new BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.ConfiguratorWorkflowEventHandler()
        {
          Result = handlerDefinition
        };
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase handlerConfiguredBase = config((BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction) workflowEventHandler);
      }

      public void Remove(
        Expression<Func<TGraph, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
      {
        this.Remove(((MemberExpression) handlerSelector.Body).Member.Name);
      }

      public void Remove<TExtension>(
        Expression<Func<TExtension, PXWorkflowEventHandlerBase<TPrimary>>> handlerSelector)
        where TExtension : PXGraphExtension<TGraph>
      {
        this.Remove(((MemberExpression) handlerSelector.Body).Member.Name);
      }

      public void Remove(string handlerName)
      {
        BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition handlerDefinition = this.Result.FirstOrDefault<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition>((Func<BoundedTo<TGraph, TPrimary>.WorkflowEventHandlerDefinition, bool>) (it => string.Equals(it.Name, handlerName, StringComparison.OrdinalIgnoreCase)));
        if (handlerDefinition == null)
          return;
        this.Result.Remove(handlerDefinition);
      }
    }
  }
}
