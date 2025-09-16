// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.DependencyInjection.Registration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Common.Services.DataProviders;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CN.Compliance.CL.Services.DataProviders;
using PX.Objects.CN.ProjectAccounting.PM.Services;

#nullable disable
namespace PX.Objects.CN.Common.DependencyInjection;

public class Registration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    base.Load(builder);
    RegistrationExtensions.RegisterType<CacheService>(builder).As<ICacheService>();
    RegistrationExtensions.RegisterType<ComplianceAttributeTypeDataProvider>(builder).As<IComplianceAttributeTypeDataProvider>();
    RegistrationExtensions.RegisterType<ProjectDataProvider>(builder).As<IProjectDataProvider>();
    RegistrationExtensions.RegisterType<ProjectTaskDataProvider>(builder).As<IProjectTaskDataProvider>();
    RegistrationExtensions.RegisterType<BusinessAccountDataProvider>(builder).As<IBusinessAccountDataProvider>();
    RegistrationExtensions.RegisterType<LienWaiverReportCreator>(builder).As<ILienWaiverReportCreator>();
    RegistrationExtensions.RegisterType<PrintEmailLienWaiverBaseService>(builder).As<IPrintEmailLienWaiverBaseService>();
    RegistrationExtensions.RegisterType<PrintLienWaiversService>(builder).As<IPrintLienWaiversService>();
    RegistrationExtensions.RegisterType<EmailLienWaiverService>(builder).As<IEmailLienWaiverService>();
    RegistrationExtensions.RegisterType<RecipientEmailDataProvider>(builder).As<IRecipientEmailDataProvider>();
    RegistrationExtensions.RegisterType<EmployeeDataProvider>(builder).As<IEmployeeDataProvider>();
  }
}
