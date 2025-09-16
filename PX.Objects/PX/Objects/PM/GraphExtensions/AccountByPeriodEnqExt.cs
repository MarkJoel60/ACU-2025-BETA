// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.AccountByPeriodEnqExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

/// <summary>AccountByPeriodEnq extension</summary>
public class AccountByPeriodEnqExt : PXGraphExtension<AccountByPeriodEnq>
{
  public PXAction<AccountByPeriodFilter> ViewPMTran;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewPMTran(PXAdapter adapter)
  {
    GLTranR current = ((PXSelectBase<GLTranR>) this.Base.GLTranEnq).Current;
    if (current != null && current.PMTranID.HasValue)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Insert().TranID = current.PMTranID;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return (IEnumerable) ((PXSelectBase<AccountByPeriodFilter>) this.Base.Filter).Select(Array.Empty<object>());
  }
}
