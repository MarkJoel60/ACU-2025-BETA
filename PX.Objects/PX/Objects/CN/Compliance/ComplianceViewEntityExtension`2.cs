// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.ComplianceViewEntityExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.PM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Compliance;

public class ComplianceViewEntityExtension<Graph, PrimaryDac> : PXGraphExtension<Graph>
  where Graph : PXGraph
  where PrimaryDac : class, IBqlTable, new()
{
  public PXAction<PrimaryDac> complianceViewCustomer;
  public PXAction<PrimaryDac> complianceViewProject;
  public PXAction<PrimaryDac> complianceViewCostTask;
  public PXAction<PrimaryDac> complianceViewRevenueTask;
  public PXAction<PrimaryDac> complianceViewCostCode;
  public PXAction<PrimaryDac> complianceViewVendor;
  public PXAction<PrimaryDac> complianceViewSecondaryVendor;
  public PXAction<PrimaryDac> complianceViewJointVendor;

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewCustomer(PXAdapter adapter)
  {
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    ComplianceDocument current = (ComplianceDocument) ((PXCache) GraphHelper.Caches<ComplianceDocument>((PXGraph) this.Base)).Current;
    if (current == null)
      return adapter.Get();
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.AR.Customer.bAccountID>((object) current.CustomerID, Array.Empty<object>()));
    if (((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
    {
      (object) PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ComplianceDocument.customerID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AcctCD
    }));
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewProject(PXAdapter adapter)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<ComplianceDocument.projectID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (pmProject != null)
    {
      ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
      ((PXSelectBase<PMProject>) instance.Project).Current = pmProject;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewCostTask(PXAdapter adapter)
  {
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<ComplianceDocument.projectID>>, And<PMTask.taskID, Equal<Current<ComplianceDocument.costTaskID>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (pmTask != null)
    {
      ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
      ((PXSelectBase<PMTask>) instance.Task).Current = pmTask;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewRevenueTask(PXAdapter adapter)
  {
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<ComplianceDocument.projectID>>, And<PMTask.taskID, Equal<Current<ComplianceDocument.revenueTaskID>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (pmTask != null)
    {
      ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
      ((PXSelectBase<PMTask>) instance.Task).Current = pmTask;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewCostCode(PXAdapter adapter)
  {
    PMCostCode pmCostCode = PXResultset<PMCostCode>.op_Implicit(PXSelectBase<PMCostCode, PXSelect<PMCostCode, Where<PMCostCode.costCodeID, Equal<Current<ComplianceDocument.costCodeID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (pmCostCode != null)
    {
      CostCodeMaint instance = PXGraph.CreateInstance<CostCodeMaint>();
      ((PXSelectBase<PMCostCode>) instance.Items).Current = pmCostCode;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewVendor(PXAdapter adapter)
  {
    return this.ViewComplianceVendor<ComplianceDocument.vendorID>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewSecondaryVendor(PXAdapter adapter)
  {
    return this.ViewComplianceVendor<ComplianceDocument.secondaryVendorID>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewJointVendor(PXAdapter adapter)
  {
    return this.ViewComplianceVendor<ComplianceDocument.jointVendorInternalId>(adapter);
  }

  public virtual IEnumerable ViewComplianceVendor<TVendorID>(PXAdapter adapter) where TVendorID : IBqlField
  {
    VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
    string field = ((PXCache) GraphHelper.Caches<ComplianceDocument>((PXGraph) this.Base)).GetField(typeof (TVendorID));
    ComplianceDocument current = (ComplianceDocument) ((PXCache) GraphHelper.Caches<ComplianceDocument>((PXGraph) this.Base)).Current;
    if (current == null)
      return adapter.Get();
    ((PXSelectBase<VendorR>) instance.BAccount).Current = PXResultset<VendorR>.op_Implicit(((PXSelectBase<VendorR>) instance.BAccount).Search<VendorR.bAccountID>((object) ((object) current).GetPropertyValue<int>(field), Array.Empty<object>()));
    if (((PXSelectBase<VendorR>) instance.BAccount).Current != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "redirect");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
    {
      (object) PXResultset<VendorR>.op_Implicit(PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<TVendorID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AcctCD
    }));
  }
}
