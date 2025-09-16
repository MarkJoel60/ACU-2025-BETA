// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.ServiceRegistration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using Autofac.Builder;
using PX.Data;
using System;

#nullable enable
namespace PX.Objects.CA.BankFeed;

internal class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<MXBankFeedManager>(builder).Named<BankFeedManager>("BankFeedManager_M").SingleInstance();
    RegistrationExtensions.RegisterType<PlaidBankFeedManager>(builder).Named<BankFeedManager>("BankFeedManager_P").SingleInstance();
    RegistrationExtensions.RegisterType<PlaidBankFeedManager>(builder).Named<BankFeedManager>("BankFeedManager_T").SingleInstance();
    RegistrationExtensions.AsSelf<BankFeedUserDataProvider, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<BankFeedUserDataProvider>(builder).SingleInstance());
    RegistrationExtensions.AsSelf<CsvBankFeedManager, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CsvBankFeedManager>(builder));
    RegistrationExtensions.AsSelf<BaiBankFeedManager, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<BaiBankFeedManager>(builder));
    RegistrationExtensions.Register<Func<string, BankFeedManager>>(builder, (Func<IComponentContext, Func<string, BankFeedManager>>) (c =>
    {
      IComponentContext context = ResolutionExtensions.Resolve<IComponentContext>(c);
      return (Func<string, BankFeedManager>) (feedType =>
      {
        return ResolutionExtensions.ResolveNamed<BankFeedManager>(context, "BankFeedManager_" + feedType) ?? throw new PXArgumentException(nameof (feedType));
      });
    })).SingleInstance();
    RegistrationExtensions.Register<Func<string, FileBankFeedManager>>(builder, (Func<IComponentContext, Func<string, FileBankFeedManager>>) (c =>
    {
      IComponentContext context = ResolutionExtensions.Resolve<IComponentContext>(c);
      return (Func<string, FileBankFeedManager>) (fileFormat =>
      {
        switch (fileFormat)
        {
          case "C":
            return (FileBankFeedManager) ResolutionExtensions.Resolve<CsvBankFeedManager>(context);
          case "B":
            return (FileBankFeedManager) ResolutionExtensions.Resolve<BaiBankFeedManager>(context);
          default:
            throw new PXArgumentException(nameof (fileFormat));
        }
      });
    })).SingleInstance();
  }
}
