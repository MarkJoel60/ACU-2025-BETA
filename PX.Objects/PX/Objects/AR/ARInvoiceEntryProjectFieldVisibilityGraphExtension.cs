// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryProjectFieldVisibilityGraphExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntryProjectFieldVisibilityGraphExtension : PXGraphExtension<ARInvoiceEntry>
{
  protected virtual void _(PX.Data.Events.RowSelected<ARInvoice> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.contractManagement>();
    bool flag2 = ProjectAttribute.IsPMVisible("AR");
    PXUIFieldAttribute.SetVisible<ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache, (object) e.Row, flag1 | flag2);
    if (flag1 & flag2)
      PXUIFieldAttribute.SetDisplayName<ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache, "Project/Contract");
    else if (flag2)
    {
      PXUIFieldAttribute.SetDisplayName<ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache, "Project");
    }
    else
    {
      if (!flag1)
        return;
      PXUIFieldAttribute.SetDisplayName<ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache, "Contract");
    }
  }
}
