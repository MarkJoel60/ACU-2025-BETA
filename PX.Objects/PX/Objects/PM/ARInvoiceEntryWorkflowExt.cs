// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ARInvoiceEntryWorkflowExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class ARInvoiceEntryWorkflowExt : PXGraphExtension<ARInvoiceEntry_Workflow, ARInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    ARInvoiceEntryWorkflowExt.Configure(config.GetScreenConfigurationContext<ARInvoiceEntry, ARInvoice>());
  }

  protected static void Configure(WorkflowContext<ARInvoiceEntry, ARInvoice> context)
  {
    var conditions = new
    {
      CreateScheduleHidden = Bql<BqlOperand<ARInvoice.proformaExists, IBqlBool>.IsEqual<True>>()
    }.AutoNameConditions();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Update((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (c => c.IsHiddenWhenElse((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.CreateScheduleHidden)))))));

    BoundedTo<ARInvoiceEntry, ARInvoice>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
