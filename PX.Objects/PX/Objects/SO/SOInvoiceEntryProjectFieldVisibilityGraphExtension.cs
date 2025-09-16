// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceEntryProjectFieldVisibilityGraphExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.SO;

public class SOInvoiceEntryProjectFieldVisibilityGraphExtension : PXGraphExtension<SOInvoiceEntry>
{
  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() || ProjectAttribute.IsPMVisible("SO") || ProjectAttribute.IsPMVisible("AR"));
    PXUIFieldAttribute.SetDisplayName<PX.Objects.AR.ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, "Project/Contract");
  }
}
