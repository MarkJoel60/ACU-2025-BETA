// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Defaults ProjectID to the Non-Project if the module supplied is either null or not not intergated with Project Management i.e. PMSetup.VisibleInXX = False.
/// When Search is supplied ProjectID is defaulted with the value returned by that search.
/// Selector also contains static Util methods.
/// </summary>
public class ProjectDefaultAttribute : PXDefaultAttribute
{
  protected readonly string module;

  public Type AccountType { get; set; }

  /// <summary>
  /// Forces user to explicitly set the Project irrespective of the AccountType settings.
  /// </summary>
  public bool ForceProjectExplicitly { get; set; }

  public ProjectDefaultAttribute()
    : this((string) null)
  {
  }

  public ProjectDefaultAttribute(string module) => this.module = module;

  public ProjectDefaultAttribute(string module, Type search)
    : this(module, search, (Type) null)
  {
  }

  public ProjectDefaultAttribute(string module, Type search, Type account)
    : base(search)
  {
    this.module = module;
    this.AccountType = account;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    if (e.NewValue == null)
    {
      if (!this.IsImporting(sender, e.Row) && !this.IsDefaultNonProject(sender, e.Row) && this.IsAccountGroupSpecified(sender, e.Row))
        return;
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.nonProject, Equal<True>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, (object[]) null));
      e.NewValue = (object) pmProject?.ContractCD;
    }
    else
    {
      if (!this.IsAccountGroupSpecified(sender, e.Row))
        return;
      e.NewValue = (object) null;
    }
  }

  protected virtual bool IsDefaultNonProject(PXCache sender, object row)
  {
    return this.module == null || !ProjectAttribute.IsPMVisible(this.module);
  }

  protected virtual bool IsImporting(PXCache sender, object row)
  {
    return sender.GetValuePending(row, PXImportAttribute.ImportFlag) != null;
  }

  /// <summary>
  /// When Account has no AccountGroup associated with it the only valid value for the Project is a Non-Project.
  /// </summary>
  protected bool IsAccountGroupSpecified(PXCache sender, object row)
  {
    if (this.ForceProjectExplicitly)
      return true;
    if (this.AccountType == (Type) null)
      return false;
    object obj = sender.GetValue(row, this.AccountType.Name);
    if (obj == null)
      return false;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      obj
    }));
    return account != null && account.AccountGroupID.HasValue;
  }

  /// <summary>
  /// Returns the Non-Project ID.
  /// Non-Project is stored in the table as a row with <see cref="T:PX.Objects.PM.PMProject.nonProject" />=1.
  /// </summary>
  public static int? NonProject()
  {
    return ServiceLocator.IsLocationProviderSet ? new int?(ServiceLocator.Current.GetInstance<IProjectSettingsManager>().NonProjectID) : new int?(0);
  }

  /// <summary>
  /// Returns true if the given ID is a Non-Project ID; oterwise false.
  /// </summary>
  public static bool IsNonProject(int? projectID)
  {
    int? nullable1 = projectID;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
  }

  public static bool IsProject(PXGraph graph, int? projectID)
  {
    return ProjectDefaultAttribute.IsProject(graph, projectID, out PMProject _);
  }

  /// <summary>
  /// If provided projectID is actuall project, but not not a non-project code, or contract or project/contract template.
  /// </summary>
  /// <param name="graph">current graph</param>
  /// <param name="projectID">project ID value</param>
  /// <param name="project">will be set to found project if it is found and it is project</param>
  /// <returns>true for project, false for non-project code, null or  project/contract template</returns>
  public static bool IsProject(PXGraph graph, int? projectID, out PMProject project)
  {
    project = (PMProject) null;
    if (!projectID.HasValue)
      return false;
    int? nullable1 = projectID;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return false;
    PMProject pmProject = PMProject.PK.Find(graph, projectID);
    if (pmProject == null || pmProject.BaseType != "P")
      return false;
    project = pmProject;
    return true;
  }

  /// <summary>
  /// If provided projectID is project or null or non-project code, but not a contact or project/contract template.
  /// Designed to answer question "can current value be replaced with other project?"
  /// </summary>
  /// <param name="graph">current graph</param>
  /// <param name="projectID">project ID value</param>
  /// <returns>true for non-project code or for null or for valid project</returns>
  public static bool IsProjectOrNonProject(PXGraph graph, int? projectID)
  {
    if (!projectID.HasValue)
      return true;
    int? nullable1 = projectID;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue || PMProject.PK.Find(graph, projectID).BaseType == "P";
  }

  public static bool IsBillableProject(PXGraph graph, int? projectID)
  {
    if (!projectID.HasValue)
      return false;
    PMProject pmProject = PMProject.PK.Find(graph, projectID);
    return pmProject != null && !(pmProject.BaseType != "P") && !pmProject.NonProject.GetValueOrDefault() && pmProject.CustomerID.HasValue;
  }
}
