// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectAccountingService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public static class ProjectAccountingService
{
  public static void NavigateToProjectScreen(int? projectID)
  {
    ProjectAccountingService.NavigateToProjectScreen(projectID, string.Empty);
  }

  public static void NavigateToProjectScreen(
    int? projectID,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (!projectID.HasValue)
      return;
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXGraph) instance).SelectTimeStamp();
    ((PXSelectBase<PMProject>) instance.Project).Current = PMProject.PK.Find((PXGraph) instance, projectID);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Project" : message, mode);
  }

  public static void NavigateToProjectOverviewScreen(int? projectID)
  {
    ProjectAccountingService.NavigateToProjectOverviewScreen(projectID, string.Empty);
  }

  public static void NavigateToProjectOverviewScreen(
    int? projectID,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (!projectID.HasValue)
      return;
    ProjectOverview instance = PXGraph.CreateInstance<ProjectOverview>();
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXGraph) instance).SelectTimeStamp();
    ((PXSelectBase<PMProject>) instance.Project).Current = PMProject.PK.Find((PXGraph) instance, projectID);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Project" : message, mode);
  }

  public static void NavigateToArInvoiceScreen(string doctype, string refNbr)
  {
    ProjectAccountingService.NavigateToArInvoiceScreen(doctype, refNbr, string.Empty);
  }

  public static void NavigateToArInvoiceScreen(
    string doctype,
    string refNbr,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (string.IsNullOrWhiteSpace(refNbr))
      return;
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) instance, doctype, refNbr);
    if (arInvoice?.OrigModule == "SO")
      instance = (ARInvoiceEntry) PXGraph.CreateInstance<SOInvoiceEntry>();
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXGraph) instance).SelectTimeStamp();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = arInvoice;
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "AR Invoice/Memo" : message, mode);
  }

  public static void NavigateToProformaScreen(string refNbr)
  {
    ProjectAccountingService.NavigateToProformaScreen(refNbr, string.Empty);
  }

  public static void NavigateToProformaScreen(
    string refNbr,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (string.IsNullOrWhiteSpace(refNbr))
      return;
    ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXGraph) instance).SelectTimeStamp();
    ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Required<PMProforma.refNbr>>, And<PMProforma.corrected, NotEqual<True>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) refNbr
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Pro Forma Invoice" : message, mode);
  }

  public static void NavigateToChangeOrderScreen(string refNbr)
  {
    ProjectAccountingService.NavigateToChangeOrderScreen(refNbr, string.Empty);
  }

  public static void NavigateToChangeOrderScreen(
    string refNbr,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (string.IsNullOrWhiteSpace(refNbr))
      return;
    ChangeOrderEntry instance = PXGraph.CreateInstance<ChangeOrderEntry>();
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXGraph) instance).SelectTimeStamp();
    ((PXSelectBase<PMChangeOrder>) instance.Document).Current = PMChangeOrder.PK.Find((PXGraph) instance, refNbr);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Change Order" : message, mode);
  }

  public static void NavigateToInventoryItemScreen(PX.Objects.IN.InventoryItem item)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(item, string.Empty);
  }

  public static void NavigateToInventoryItemScreen(
    PX.Objects.IN.InventoryItem item,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (item == null || item.ItemStatus == "XX")
      return;
    InventoryItemMaintBase inventoryItemGraph = ProjectAccountingService.CreateInventoryItemGraph(item);
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemGraph.Item).Current = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemGraph.Item).Search<PX.Objects.IN.InventoryItem.inventoryID>((object) item.InventoryID, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) inventoryItemGraph, string.IsNullOrWhiteSpace(message) ? "Inventory Item" : message, mode);
  }

  public static void NavigateToCustomerScreen(PX.Objects.CR.BAccount account)
  {
    ProjectAccountingService.NavigateToCustomerScreen(account, string.Empty);
  }

  public static void NavigateToCustomerScreen(
    PX.Objects.CR.BAccount account,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (account == null)
      return;
    if (account.Type == "CU" || account.Type == "VC")
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Search<PX.Objects.CR.BAccount.bAccountID>((object) account.BAccountID, Array.Empty<object>()));
      if (((PXSelectBase<PX.Objects.AR.Customer>) instance.BAccount).Current != null)
        ProjectAccountingService.NavigateToScreen((PXGraph) instance, message, mode);
      else
        throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
        {
          (object) PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) account.BAccountID
          })).AcctCD
        }));
    }
    else if (account.Type == "VE")
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      ((PXSelectBase<VendorR>) instance.BAccount).Current = PXResultset<VendorR>.op_Implicit(((PXSelectBase<VendorR>) instance.BAccount).Search<VendorR.bAccountID>((object) account.BAccountID, Array.Empty<object>()));
      if (((PXSelectBase<VendorR>) instance.BAccount).Current != null)
        ProjectAccountingService.NavigateToScreen((PXGraph) instance, message, mode);
      else
        throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
        {
          (object) PXResultset<VendorR>.op_Implicit(PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Required<VendorR.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) account.BAccountID
          })).AcctCD
        }));
    }
    else
    {
      if (!(account.Type == "EP") && !(account.Type == "EC"))
        return;
      EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
      ((PXSelectBase<EPEmployee>) instance.Employee).Current = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) account.BAccountID
      }));
      if (((PXSelectBase<EPEmployee>) instance.Employee).Current != null)
        ProjectAccountingService.NavigateToScreen((PXGraph) instance, message, mode);
      else
        throw new PXException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
        {
          (object) account.AcctCD
        }));
    }
  }

  public static void NavigateToPurchaseOrderScreen(
    PX.Objects.PO.POOrder commitment,
    string message,
    PXBaseRedirectException.WindowMode mode = 2)
  {
    if (commitment == null)
      return;
    if (commitment.OrderType == "RS")
    {
      SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) commitment.OrderNbr, new object[1]
      {
        (object) commitment.OrderType
      }));
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Subcontract" : message, mode);
    }
    else
    {
      POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) commitment.OrderNbr, new object[1]
      {
        (object) commitment.OrderType
      }));
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, string.IsNullOrWhiteSpace(message) ? "Purchase Order" : message, mode);
    }
  }

  public static void OpenInTheSameWindow(PXGraph graph)
  {
    ProjectAccountingService.NavigateToScreen(graph, string.Empty, (PXBaseRedirectException.WindowMode) 1);
  }

  public static void NavigateToScreen(PXGraph graph)
  {
    ProjectAccountingService.NavigateToScreen(graph, string.Empty);
  }

  public static void NavigateToScreen(
    PXGraph graph,
    string message,
    PXBaseRedirectException.WindowMode windowMode = 2)
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(graph, message);
    ((PXBaseRedirectException) requiredException).Mode = windowMode;
    throw requiredException;
  }

  public static void NavigateToReport(string reportId, Dictionary<string, string> parameters)
  {
    ProjectAccountingService.NavigateToReport(reportId, parameters, string.Empty);
  }

  public static void NavigateToReport(
    string reportId,
    Dictionary<string, string> parameters,
    string message,
    PXBaseRedirectException.WindowMode windowMode = 2)
  {
    throw new PXReportRequiredException(parameters, reportId, windowMode, message, (CurrentLocalization) null);
  }

  public static void NavigateToGenericIquiry(
    string inquiryID,
    params DataViewHelper.DataViewFilter[] filters)
  {
    ProjectAccountingService.NavigateToGenericIquiry(inquiryID, string.Empty, (PXBaseRedirectException.WindowMode) 2, filters);
  }

  public static void NavigateToGenericIquiry(
    string inquiryID,
    string message,
    params DataViewHelper.DataViewFilter[] filters)
  {
    ProjectAccountingService.NavigateToGenericIquiry(inquiryID, message, (PXBaseRedirectException.WindowMode) 2, filters);
  }

  public static void NavigateToGenericIquiry(
    string inquiryID,
    string message,
    PXBaseRedirectException.WindowMode windowMode,
    params DataViewHelper.DataViewFilter[] filters)
  {
    DataViewHelper.OpenGenericInquiry(inquiryID, message, windowMode, filters);
  }

  public static void NavigateToView(
    PXGraph graph,
    string viewName,
    string message,
    params DataViewHelper.DataViewFilter[] filters)
  {
    ProjectAccountingService.NavigateToView(graph, viewName, message, (PXBaseRedirectException.WindowMode) 2, filters);
  }

  public static void NavigateToView(
    PXGraph graph,
    string viewName,
    string message,
    PXBaseRedirectException.WindowMode windowMode,
    params DataViewHelper.DataViewFilter[] filters)
  {
    DataViewHelper.OpenDataView(graph, viewName, message, windowMode, filters);
  }

  private static InventoryItemMaintBase CreateInventoryItemGraph(PX.Objects.IN.InventoryItem item)
  {
    if (item.IsTemplate.GetValueOrDefault())
      return (InventoryItemMaintBase) PXGraph.CreateInstance<TemplateInventoryItemMaint>();
    return !item.StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) PXGraph.CreateInstance<NonStockItemMaint>() : (InventoryItemMaintBase) PXGraph.CreateInstance<InventoryItemMaint>();
  }
}
