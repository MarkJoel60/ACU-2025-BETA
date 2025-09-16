// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt.InterbranchTranValidation
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
namespace PX.Objects.IN.GraphExtensions.INAdjustmentEntryExt;

public class InterbranchTranValidation : 
  InterbranchSiteRestrictionExtension<INAdjustmentEntry, INRegister.branchID, INTran, INTran.siteID>
{
  protected override IEnumerable<INTran> GetDetails()
  {
    return GraphHelper.RowCast<INTran>((IEnumerable) ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>()));
  }
}
