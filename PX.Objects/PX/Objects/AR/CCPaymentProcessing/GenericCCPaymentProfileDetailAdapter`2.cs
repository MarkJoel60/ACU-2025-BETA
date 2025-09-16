// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.GenericCCPaymentProfileDetailAdapter`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

internal class GenericCCPaymentProfileDetailAdapter<T, P> : ICCPaymentProfileDetailAdapter
  where T : class, IBqlTable, ICCPaymentProfileDetail, new()
  where P : class, IBqlTable, ICCPaymentMethodDetail, new()
{
  private PXSelectBase<T> profileDetailView;
  private PXSelectBase<P> pmDetailView;

  public GenericCCPaymentProfileDetailAdapter(
    PXSelectBase<T> profileDetail,
    PXSelectBase<P> pmDetail)
  {
    this.profileDetailView = profileDetail;
    this.pmDetailView = pmDetail;
  }

  public ICCPaymentProfileDetail Current
  {
    get => (ICCPaymentProfileDetail) this.profileDetailView.Current;
  }

  public PXCache Cache => ((PXSelectBase) this.profileDetailView).Cache;

  public IEnumerable<Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail>> Select(
    params object[] arguments)
  {
    foreach (PXResult<T> pxResult in this.profileDetailView.Select(arguments))
    {
      T input = PXResult<T>.op_Implicit(pxResult);
      P pmDetail = this.GetPMDetail(input);
      yield return Tuple.Create<ICCPaymentProfileDetail, ICCPaymentMethodDetail>((ICCPaymentProfileDetail) input, (ICCPaymentMethodDetail) pmDetail);
    }
  }

  private P GetPMDetail(T input)
  {
    return PXResult<P>.op_Implicit(((IEnumerable<PXResult<P>>) this.pmDetailView.Select(Array.Empty<object>())).ToList<PXResult<P>>().Where<PXResult<P>>((Func<PXResult<P>, bool>) (i =>
    {
      P p = PXResult<P>.op_Implicit(i);
      return p.PaymentMethodID == input.PaymentMethodID && p.DetailID == input.DetailID;
    })).First<PXResult<P>>());
  }
}
