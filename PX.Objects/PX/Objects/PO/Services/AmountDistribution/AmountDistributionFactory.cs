// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Services.AmountDistribution.AmountDistributionFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using Autofac.Builder;
using System;

#nullable disable
namespace PX.Objects.PO.Services.AmountDistribution;

public class AmountDistributionFactory
{
  public virtual IAmountDistributionService<Item> CreateService<Item>(
    DistributionMethod method,
    DistributionParameter<Item> distributeParameter)
    where Item : class, IAmountItem
  {
    switch (method)
    {
      case DistributionMethod.RemainderToBiggestLine:
        return (IAmountDistributionService<Item>) new RemainderToBiggestLineService<Item>(distributeParameter);
      case DistributionMethod.RemainderToLastLine:
        return (IAmountDistributionService<Item>) new RemainderToLastLineService<Item>(distributeParameter);
      case DistributionMethod.AccumulateRemainderToNonZeroLine:
        return (IAmountDistributionService<Item>) new RemainderToLastLineService<Item>(distributeParameter);
      default:
        throw new NotImplementedException();
    }
  }

  public class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      RegistrationExtensions.PreserveExistingDefaults<AmountDistributionFactory, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.AsSelf<AmountDistributionFactory, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<AmountDistributionFactory>(builder)));
    }
  }
}
