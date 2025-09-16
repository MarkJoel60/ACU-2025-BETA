// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryRetainage_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceEntryRetainage_Workflow : 
  PXGraphExtension<APInvoiceEntry_Workflow, APInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>();

  public sealed override void Configure(PXScreenConfiguration config)
  {
    APInvoiceEntryRetainage_Workflow.Configure(config.GetScreenConfigurationContext<APInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<APInvoiceEntry, APInvoice> context)
  {
    APInvoiceEntry_Workflow.Conditions apconditions = context.Conditions.GetPack<APInvoiceEntry_Workflow.Conditions>();
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured processingCategory = context.Categories.Get("Processing");
    var conditions = new
    {
      IsNotRetenageApplied = Bql<Not<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, In3<APDocType.invoice, APDocType.debitAdj>>>>, PX.Data.And<BqlOperand<APRegister.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<APRegister.retainageApply, IBqlBool>.IsEqual<True>>>>(),
      IsNotRetenageDoc = Bql<BqlOperand<APRegister.docType, IBqlString>.IsNotIn<APDocType.invoice, APDocType.debitAdj>>()
    }.AutoNameConditions();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<APInvoiceEntryRetainage>((Expression<Func<APInvoiceEntryRetainage, PXAction<APInvoice>>>) (g => g.releaseRetainage), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.InFolder(processingCategory, (Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice)).PlaceAfter((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice)).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsNotRetenageDoc || apconditions.IsMigrationMode)).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotRetenageApplied)))))));

    BoundedTo<APInvoiceEntry, APInvoice>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
