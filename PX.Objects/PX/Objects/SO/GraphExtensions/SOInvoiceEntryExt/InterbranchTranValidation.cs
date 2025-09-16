// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.InterbranchTranValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class InterbranchTranValidation : 
  InterbranchSiteRestrictionExtension<SOInvoiceEntry, PX.Objects.AR.ARInvoice.branchID, ARTran, ARTran.siteID>
{
  protected override IEnumerable<ARTran> GetDetails()
  {
    return GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>()));
  }

  protected override void _(PX.Data.Events.RowPersisting<ARTran> e)
  {
    if (e.Row.LineType == "MI")
      return;
    base._(e);
  }
}
