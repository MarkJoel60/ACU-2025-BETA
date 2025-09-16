// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.ServiceRegistration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using Autofac.Builder;
using PX.Objects.AP.InvoiceRecognition.Feedback;
using PX.Objects.AP.InvoiceRecognition.VendorSearch;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<InvoiceRecognitionService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<InvoiceRecognitionService>(builder).As<IInvoiceRecognitionService>());
    RegistrationExtensions.RegisterType<ContactRepository>(builder).As<IContactRepository>().SingleInstance();
    RegistrationExtensions.RegisterType<VendorRepository>(builder).As<IVendorRepository>().SingleInstance();
    RegistrationExtensions.AsSelf<VendorSearchFeedbackBuilder, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<VendorSearchFeedbackBuilder>(builder));
    RegistrationExtensions.RegisterType<VendorSearcher>(builder).As<IVendorSearchService>();
    RegistrationExtensions.AsSelf<RecognizedRecordDetailsManager, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<RecognizedRecordDetailsManager>(builder));
  }
}
