// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.ServiceRegistration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using PX.Data;
using PX.Objects.AP.PaymentProcessor.Managers;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<BillComManager>(builder).Named<PaymentProcessorManager>("PaymentProcessorManager_BC").SingleInstance();
    RegistrationExtensions.Register<Func<string, PaymentProcessorManager>>(builder, (Func<IComponentContext, Func<string, PaymentProcessorManager>>) (c =>
    {
      IComponentContext context = ResolutionExtensions.Resolve<IComponentContext>(c);
      return (Func<string, PaymentProcessorManager>) (processorType =>
      {
        return ResolutionExtensions.ResolveNamed<PaymentProcessorManager>(context, "PaymentProcessorManager_" + processorType) ?? throw new PXArgumentException(nameof (processorType));
      });
    })).SingleInstance();
  }
}
