// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GiValidationServicesBuilderExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Data.GenericInquiry.Services.ValidationServices;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;

#nullable disable
namespace PX.Data.GenericInquiry;

internal static class GiValidationServicesBuilderExtension
{
  public static void RegisterGIValidationServices(this ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<GenericInquiryValidationService>(builder).As<IGenericInquiryValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIDesignValidationService>(builder).As<IGIDesignValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIFilterValidationService>(builder).As<IGIFilterValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIGroupByValidationService>(builder).As<IGIGroupByValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GINavigationParameterValidationService>(builder).As<IGINavigationParameterValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GINavigationScreenValidationService>(builder).As<IGINavigationScreenValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIOnValidationService>(builder).As<IGIOnValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIRelationValidationService>(builder).As<IGIRelationValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIResultValidationService>(builder).As<IGIResultValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GISortValidationService>(builder).As<IGISortValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GITableValidationService>(builder).As<IGITableValidationService>().SingleInstance();
    RegistrationExtensions.RegisterType<GIWhereValidationService>(builder).As<IGIWhereValidationService>().SingleInstance();
  }
}
