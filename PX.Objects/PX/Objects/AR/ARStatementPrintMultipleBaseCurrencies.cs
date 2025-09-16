// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementPrintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AR;

public sealed class ARStatementPrintMultipleBaseCurrencies : PXGraphExtension<ARStatementPrint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected void _(
    Events.FieldVerifying<ARStatementPrint.PrintParameters.statementCycleId> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() || !((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.PrepareStatements.Equals("A"))
      return;
    ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<ARStatementPrint.PrintParameters.statementCycleId>>) e).Cache.RaiseExceptionHandling<ARStatementPrint.PrintParameters.statementCycleId>(e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<ARStatementPrint.PrintParameters.statementCycleId>, object, object>) e).NewValue, (Exception) new PXSetPropertyException("The statements cannot be printed because it is not allowed to consolidate statements for all companies if the Multiple Base Currency feature is enabled. Select a different option in the Prepare Statements box on the Accounts Receivable Preferences (AR101000) form."));
  }
}
