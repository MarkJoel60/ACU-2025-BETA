// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.GraphExtensions.APSetupMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AP.GraphExtensions;

public class APSetupMaintExt : PXGraphExtension<APSetupMaint>
{
  protected virtual void _(
    Events.FieldVerifying<APSetup, APSetupExt.reclassifyInvoices> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((Events.FieldVerifyingBase<Events.FieldVerifying<APSetup, APSetupExt.reclassifyInvoices>, APSetup, object>) e).NewValue;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue && PXResultset<APInvoice>.op_Implicit(PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.status, Equal<APDocStatus.underReclassification>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, Array.Empty<object>())) != null)
      throw new PXSetPropertyException<APSetupExt.reclassifyInvoices>("To be able to clear the Allow Bill Reclassification check box, release the bills that are assigned the Under Reclassification status first.");
  }
}
