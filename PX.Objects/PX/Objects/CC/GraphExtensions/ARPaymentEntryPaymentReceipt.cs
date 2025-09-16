// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARPaymentEntryPaymentReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.Repositories;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARPaymentEntryPaymentReceipt : PXGraphExtension<ARPaymentEntry>
{
  public PXAction<ARPayment> emailDocPaymentReceipt;
  public PXAction<ARPayment> printDocPaymentReceipt;

  public static bool IsActive() => !PXSiteMap.IsPortal;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailDocPaymentReceipt(PXAdapter adapter)
  {
    foreach (ARPayment doc in GraphHelper.RowCast<ARPayment>(adapter.Get()))
      this.SendEmail(doc);
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintDocPaymentReceipt(PXAdapter adapter)
  {
    foreach (ARPayment doc in GraphHelper.RowCast<ARPayment>(adapter.Get()))
      this.PrintPaymentReceipt(doc);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPayment> e)
  {
    ARPayment row = e.Row;
    if (row == null)
      return;
    bool flag1 = EnumerableExtensions.IsIn<string>(row.DocType, "PMT", "PPM");
    bool flag2 = row.DocType == "RPM";
    bool flag3 = row.DocType == "REF";
    bool flag4 = row.DocType == "VRF";
    bool valueOrDefault = row.Released.GetValueOrDefault();
    int num1 = row.IsCCPayment.GetValueOrDefault() ? 1 : 0;
    bool flag5 = num1 == 0 & valueOrDefault && flag1 | flag2 | flag3 | flag4;
    bool? nullable;
    int num2;
    if ((num1 & (valueOrDefault ? 1 : 0) & (flag1 ? 1 : 0)) != 0 && row.IsCCCaptured.GetValueOrDefault())
    {
      nullable = row.Voided;
      bool flag6 = false;
      num2 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag7 = num2 != 0;
    int num3;
    if ((num1 & (valueOrDefault ? 1 : 0) & (flag2 ? 1 : 0)) != 0)
    {
      nullable = row.IsCCCaptured;
      bool flag8 = false;
      if (!(nullable.GetValueOrDefault() == flag8 & nullable.HasValue))
      {
        nullable = row.IsCCRefunded;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 1;
    }
    else
      num3 = 0;
    bool flag9 = num3 != 0;
    int num4;
    if ((num1 & (valueOrDefault ? 1 : 0) & (flag3 ? 1 : 0)) != 0)
    {
      nullable = row.IsCCRefunded;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.Voided;
        bool flag10 = false;
        num4 = nullable.GetValueOrDefault() == flag10 & nullable.HasValue ? 1 : 0;
        goto label_13;
      }
    }
    num4 = 0;
label_13:
    bool flag11 = num4 != 0;
    bool flag12 = false;
    if (flag5 | flag7 | flag9 | flag11)
      flag12 = true;
    ((PXAction) this.emailDocPaymentReceipt).SetEnabled(flag12);
    ((PXAction) this.printDocPaymentReceipt).SetEnabled(flag12);
  }

  [PXSelector(typeof (Search<Customer.bAccountID>), SubstituteKey = typeof (Customer.acctCD), DirtyRead = true)]
  [PXMergeAttributes]
  protected void _(PX.Data.Events.CacheAttached<CRSMEmail.bAccountID> e)
  {
  }

  protected virtual CCProcTran GetCCProcTran()
  {
    return new CCProcTranRepository((PXGraph) this.Base).GetCCProcTranByTranID(((ExternalTransaction) ExternalTranHelper.GetActiveTransaction((PXSelectBase<ExternalTransaction>) this.Base.ExternalTran)).TransactionID).FirstOrDefault<CCProcTran>();
  }

  protected virtual void SendEmail(ARPayment doc)
  {
    Dictionary<string, string> parameters = new Dictionary<string, string>()
    {
      ["DocType"] = doc.DocType,
      ["RefNbr"] = doc.RefNbr
    };
    ((PXGraph) this.Base).GetExtension<ARPaymentEntryActivityDetailsExt>().SendNotification("Customer", "PAY RECEIPT", doc.BranchID, (IDictionary<string, string>) parameters, false, (IList<Guid?>) null);
  }

  protected virtual void PrintPaymentReceipt(ARPayment doc)
  {
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["RefNbr"] = doc.RefNbr,
      ["DocType"] = doc.DocType
    }, "AR643000", "Report", (CurrentLocalization) null);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    bool addCategory = false;
    WorkflowContext<ARPaymentEntry, ARPayment> configurationContext = config.GetScreenConfigurationContext<ARPaymentEntry, ARPayment>();
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured printingAndEmailingCategory = configurationContext.Categories.Get("PrintingAndEmailingID");
    if (printingAndEmailingCategory == null)
    {
      addCategory = true;
      printingAndEmailingCategory = configurationContext.Categories.CreateNew("PrintingAndEmailingID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing").PlaceAfter("CorrectionsID")));
    }
    configurationContext.UpdateScreenConfigurationFor((Func<BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow, BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Update<ARDocStatus.open>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.ConfiguratorState, BoundedTo<ARPaymentEntry, ARPayment>.FlowState.ConfiguratorState>) (flowstate => flowstate.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.printDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.emailDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))));
      fss.Update<ARDocStatus.closed>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.ConfiguratorState, BoundedTo<ARPaymentEntry, ARPayment>.FlowState.ConfiguratorState>) (flowstate => flowstate.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.ContainerAdjusterActions>) (actions =>
      {
        actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.printDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.emailDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))));
    })))).WithCategories((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      if (!addCategory)
        return;
      categories.Add(printingAndEmailingCategory);
    })).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.printDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.WithCategory(printingAndEmailingCategory).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.printed>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (e => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add<ARPaymentEntryPaymentReceipt>((Expression<Func<ARPaymentEntryPaymentReceipt, PXAction<ARPayment>>>) (g => g.emailDocPaymentReceipt), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.WithCategory(printingAndEmailingCategory).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.emailed>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (e => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    }))));
  }
}
