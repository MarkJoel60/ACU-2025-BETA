// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Factories.ProcessingCardsPluginFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CA;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Factories;

public class ProcessingCardsPluginFactory
{
  private string processingCenterId;
  private ICCPaymentProcessingRepository paymentProcessingRepository;

  public ProcessingCardsPluginFactory(string processingCenterId)
  {
    this.processingCenterId = processingCenterId;
  }

  public ICCPaymentProcessingRepository GetPaymentProcessingRepository()
  {
    if (this.paymentProcessingRepository == null)
      this.paymentProcessingRepository = CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    return this.paymentProcessingRepository;
  }

  public CCProcessingCenter GetProcessingCenter()
  {
    this.GetPaymentProcessingRepository();
    return CCProcessingCenter.PK.Find(this.paymentProcessingRepository.Graph, this.processingCenterId);
  }

  public object GetPlugin() => this.GetProcessorPlugin(this.GetProcessingCenter());

  private object GetProcessorPlugin(CCProcessingCenter processingCenter)
  {
    return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
  }
}
