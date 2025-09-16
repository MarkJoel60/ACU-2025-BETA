// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.InterbranchTranValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class InterbranchTranValidation : 
  InterbranchSiteRestrictionExtension<POOrderEntry, POOrder.branchID, POLine, POLine.siteID>
{
  protected override void _(Events.FieldVerifying<POOrder.branchID> e)
  {
    if (((PXSelectBase<POOrder>) this.Base.CurrentDocument).Current?.OrderType == "SB")
      return;
    base._(e);
  }

  protected override void _(Events.RowPersisting<POLine> e)
  {
    if (((PXSelectBase<POOrder>) this.Base.CurrentDocument).Current?.OrderType == "SB")
      return;
    base._(e);
  }

  protected override IEnumerable<POLine> GetDetails()
  {
    return GraphHelper.RowCast<POLine>((IEnumerable) ((PXSelectBase<POLine>) this.Base.Transactions).Select(Array.Empty<object>()));
  }
}
