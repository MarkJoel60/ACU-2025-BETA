// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.EmployeeMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class EmployeeMaintExt : PXGraphExtension<EmployeeMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowSelected<EPEmployee> e)
  {
    this.ValidateEmployee(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPEmployee>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPEmployee> e)
  {
    this.ValidateEmployee(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<EPEmployee>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPEmployee> e)
  {
    if (e.Row == null)
      return;
    PXSelectReadonly<PMProjectContact, Where<PMProjectContact.contactID, Equal<Required<PMProjectContact.contactID>>>> pxSelectReadonly = new PXSelectReadonly<PMProjectContact, Where<PMProjectContact.contactID, Equal<Required<PMProjectContact.contactID>>>>((PXGraph) this.Base);
    ((PXSelectBase<PMProjectContact>) pxSelectReadonly).SelectWindowed(0, 10, new object[1]
    {
      (object) e.Row.DefContactID
    });
    List<string> items = new List<string>();
    foreach (PXResult<PMProjectContact> pxResult in ((PXSelectBase<PMProjectContact>) pxSelectReadonly).SelectWindowed(0, 10, new object[1]
    {
      (object) e.Row.DefContactID
    }))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, PXResult<PMProjectContact>.op_Implicit(pxResult).ProjectID);
      if (pmProject != null)
        items.Add(pmProject.ContractCD.Trim());
    }
    if (items.Count > 0)
      throw new PXSetPropertyException<EPEmployee.bAccountID>("The employee cannot be deleted because it is used in the following projects or project templates: {0}.", new object[1]
      {
        (object) items.JoinIntoStringForMessage<string>()
      });
  }

  protected virtual void ValidateEmployee(PXCache cache, EPEmployee employee)
  {
    if (employee == null)
      return;
    bool valueOrDefault = employee.AllowOverrideCury.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<EPEmployee.curyRateTypeID>(cache, (object) employee, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    EmployeeMaintExt.Configure(config.GetScreenConfigurationContext<EmployeeMaint, EPEmployee>());
  }

  protected static void Configure(WorkflowContext<EmployeeMaint, EPEmployee> context)
  {
    context.AddScreenConfigurationFor((Func<BoundedTo<EmployeeMaint, EPEmployee>.ScreenConfiguration.IStartConfigScreen, BoundedTo<EmployeeMaint, EPEmployee>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<EmployeeMaint, EPEmployee>.ScreenConfiguration.IConfigured) ((BoundedTo<EmployeeMaint, EPEmployee>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<EmployeeMaint, EPEmployee>.ActionDefinition.IContainerFillerActions>) (actions => actions.AddNew("ShowProjects", (Func<BoundedTo<EmployeeMaint, EPEmployee>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EmployeeMaint, EPEmployee>.ActionDefinition.IConfigured>) (a => (BoundedTo<EmployeeMaint, EPEmployee>.ActionDefinition.IConfigured) a.DisplayName("Projects").IsSidePanelScreen((Func<BoundedTo<EmployeeMaint, EPEmployee>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<EmployeeMaint, EPEmployee>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("PM3010SP").WithIcon("project").WithAssignments((Action<BoundedTo<EmployeeMaint, EPEmployee>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add("PMProjectContact_contactID", (Func<BoundedTo<EmployeeMaint, EPEmployee>.NavigationParameter.INeedRightOperand, BoundedTo<EmployeeMaint, EPEmployee>.NavigationParameter.IConfigured>) (e => e.SetFromField<EPEmployee.defContactID>()))))))))))));
  }
}
