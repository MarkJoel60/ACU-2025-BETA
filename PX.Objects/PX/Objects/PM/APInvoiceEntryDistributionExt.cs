// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APInvoiceEntryDistributionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Extends AP Invoice Entry with Project related functionality. Requires Project Accounting feature and Distribution feature.
/// </summary>
public class APInvoiceEntryDistributionExt : PXGraphExtension<APInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  public virtual string GetErrorMessage(string poOrderType, string poMesage, string scMessage)
  {
    if (string.IsNullOrWhiteSpace(poOrderType))
      throw new PXArgumentException(nameof (poOrderType));
    return poOrderType == "RS" ? scMessage : poMesage;
  }

  public virtual void POOrderRS_Selected_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(bool) e.NewValue)
      return;
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) e.Row;
    List<POLineS> poOrderLines = this.Base.GetPOOrderLines(row, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, true);
    HashSet<string> source1 = new HashSet<string>();
    HashSet<string> source2 = new HashSet<string>();
    foreach (POLineS poLineS in poOrderLines)
    {
      if (poLineS.TaskID.HasValue)
      {
        PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) poLineS.ProjectID
        }));
        if (pmProject.Status != "A")
        {
          source1.Add($"Project: {pmProject.ContractCD}");
        }
        else
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) poLineS.ProjectID,
            (object) poLineS.TaskID
          }));
          if (pmTask.Status != "A")
            source2.Add($"Project: {pmProject.ContractCD} Task: {pmTask.TaskCD}");
        }
      }
    }
    if (source1.Count > 0)
    {
      string errorMessage = this.GetErrorMessage(row.OrderType, "The purchase order cannot be added because it contains one or more lines related to the project that has the status different from Active.", "The subcontract cannot be added because it contains one or more lines related to the project that has the status different from Active.");
      PXTrace.WriteError(errorMessage + Environment.NewLine + string.Join(Environment.NewLine, (IEnumerable<string>) source1.OrderBy<string, string>((Func<string, string>) (p => p))));
      throw new PXSetPropertyException(errorMessage, (PXErrorLevel) 5);
    }
    if (source2.Count > 0)
    {
      string errorMessage = this.GetErrorMessage(row.OrderType, "The purchase order cannot be added because it contains one or more lines related to the project task that has the status different from Active.", "The subcontract cannot be added because it contains one or more lines related to the project task that has the status different from Active.");
      PXTrace.WriteError(errorMessage + Environment.NewLine + string.Join(Environment.NewLine, (IEnumerable<string>) source2.OrderBy<string, string>((Func<string, string>) (p => p))));
      throw new PXSetPropertyException(errorMessage, (PXErrorLevel) 5);
    }
  }

  public virtual void POLineRS_Selected_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    POLineRS row = (POLineRS) e.Row;
    if (!(bool) e.NewValue || !row.TaskID.HasValue)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.ProjectID
    }));
    if (pmProject.Status != "A")
      throw new PXSetPropertyException(this.GetErrorMessage(row.OrderType, "The purchase order line cannot be added because it is related to the {1} project that has the status different from Active.", "The subcontract line cannot be added because it is related to the {1} project that has the status different from Active."), (PXErrorLevel) 5, new object[2]
      {
        (object) string.Empty,
        (object) pmProject.ContractCD
      });
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.ProjectID,
      (object) row.TaskID
    }));
    if (pmTask.Status != "A")
      throw new PXSetPropertyException(this.GetErrorMessage(row.OrderType, "The purchase order line cannot be added because it is related to the {1} project task that has the status different from Active.", "The subcontract line cannot be added because it is related to the {1} project task that has the status different from Active."), (PXErrorLevel) 5, new object[2]
      {
        (object) string.Empty,
        (object) pmTask.TaskCD
      });
  }

  public virtual void POReceipt_Selected_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(bool) e.NewValue)
      return;
    List<POReceiptLineS> invoicePoReceiptLines = this.Base.GetInvoicePOReceiptLines((PX.Objects.PO.POReceipt) e.Row, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current);
    HashSet<string> source1 = new HashSet<string>();
    HashSet<string> source2 = new HashSet<string>();
    foreach (POReceiptLineS poReceiptLineS in invoicePoReceiptLines)
    {
      if (poReceiptLineS.TaskID.HasValue)
      {
        PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) poReceiptLineS.ProjectID
        }));
        bool? isActive = pmProject.IsActive;
        if (!isActive.GetValueOrDefault())
        {
          source1.Add($"Project: {pmProject.ContractCD}");
        }
        else
        {
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) poReceiptLineS.ProjectID,
            (object) poReceiptLineS.TaskID
          }));
          isActive = pmTask.IsActive;
          if (!isActive.GetValueOrDefault())
            source2.Add($"Project: {pmProject.ContractCD} Task: {pmTask.TaskCD}");
        }
      }
    }
    if (source1.Count > 0)
    {
      PXTrace.WriteError($"The purchase receipt cannot be added to the AP bill because at least one receipt line has an inactive project associated.{Environment.NewLine}{string.Join(Environment.NewLine, (IEnumerable<string>) source1.OrderBy<string, string>((Func<string, string>) (p => p)))}");
      throw new PXSetPropertyException("The purchase receipt cannot be added to the AP bill because at least one receipt line has an inactive project associated. See the Trace for details.", (PXErrorLevel) 5);
    }
    if (source2.Count > 0)
    {
      PXTrace.WriteError($"The purchase receipt cannot be added to the AP bill because at least one receipt line has an inactive project task associated.{Environment.NewLine}{string.Join(Environment.NewLine, (IEnumerable<string>) source2.OrderBy<string, string>((Func<string, string>) (p => p)))}");
      throw new PXSetPropertyException("The purchase receipt cannot be added to the AP bill because at least one receipt line has an inactive project task associated. See the Trace for details.", (PXErrorLevel) 5);
    }
  }

  public virtual void POReceiptLineAdd_Selected_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POReceiptLineS row = (POReceiptLineS) e.Row;
    string str = (string) null;
    if ((bool) e.NewValue && row.TaskID.HasValue)
    {
      if (!PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) row.ProjectID
      })).IsActive.GetValueOrDefault())
        str = "Project is not Active.";
      else if (!PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) row.ProjectID,
        (object) row.TaskID
      })).IsActive.GetValueOrDefault())
        str = "Project Task is not Active.";
    }
    if (str != null)
      throw new PXSetPropertyException(str, (PXErrorLevel) 5);
  }
}
