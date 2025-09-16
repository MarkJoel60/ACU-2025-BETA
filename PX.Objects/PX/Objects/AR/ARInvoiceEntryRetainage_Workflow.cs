// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryRetainage_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntryRetainage_Workflow : 
  PXGraphExtension<ARInvoiceEntry_Workflow, ARInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.retainage>();

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARInvoiceEntryRetainage_Workflow.Configure(config.GetScreenConfigurationContext<ARInvoiceEntry, ARInvoice>());
  }

  protected static void Configure(WorkflowContext<ARInvoiceEntry, ARInvoice> context)
  {
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured processingCategory = context.Categories.Get("ProcessingID");
    var conditions = new
    {
      IsNotRetenageApplied = Bql<Not<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, In3<ARDocType.invoice, ARDocType.creditMemo>>>>, And<BqlOperand<ARRegister.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARRegister.retainageApply, IBqlBool>.IsEqual<True>>>>(),
      IsNotRetenageInvoice = Bql<BqlOperand<ARRegister.docType, IBqlString>.IsNotIn<ARDocType.invoice, ARDocType.creditMemo>>(),
      IsRetenageMigrationMode = (ARSetupDefinition.GetSlot().MigrationMode.GetValueOrDefault() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<True>>() : Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>())
    }.AutoNameConditions();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<ARInvoiceEntryRetainage>((Expression<Func<ARInvoiceEntryRetainage, PXAction<ARInvoice>>>) (g => g.releaseRetainage), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
    {
      BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.InFolder(processingCategory, (Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice)).PlaceAfter((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice));
      BoundedTo<ARInvoiceEntry, ARInvoice>.Condition notRetenageInvoice = conditions.IsNotRetenageInvoice;
      BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(notRetenageInvoice) ? notRetenageInvoice : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(notRetenageInvoice, conditions.IsRetenageMigrationMode);
      return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsNotRetenageApplied);
    }))))));

    BoundedTo<ARInvoiceEntry, ARInvoice>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
