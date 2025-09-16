// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.GenericCCPaymentProfileAdapter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

internal class GenericCCPaymentProfileAdapter<T> : ICCPaymentProfileAdapter where T : class, IBqlTable, ICCPaymentProfile, new()
{
  private PXSelectBase<T> dataView;

  public GenericCCPaymentProfileAdapter(PXSelectBase<T> dataView) => this.dataView = dataView;

  public ICCPaymentProfile Current => (ICCPaymentProfile) this.dataView.Current;

  public PXCache Cache => ((PXSelectBase) this.dataView).Cache;
}
