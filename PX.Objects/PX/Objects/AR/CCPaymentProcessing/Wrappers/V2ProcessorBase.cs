// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Wrappers.V2ProcessorBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Repositories;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Wrappers;

public abstract class V2ProcessorBase : ISetCardProcessingReadersProvider
{
  protected ICardProcessingReadersProvider _provider;

  protected ICardProcessingReadersProvider GetProvider()
  {
    return this._provider != null ? this._provider : throw new PXInvalidOperationException("Could not get CardProcessingReadersProvider");
  }

  public void SetProvider(ICardProcessingReadersProvider provider) => this._provider = provider;
}
