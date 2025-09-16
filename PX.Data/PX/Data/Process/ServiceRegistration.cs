// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Hosting;
using PX.Data.Maintenance.SM.SendRecurringNotifications;

#nullable disable
namespace PX.Data.Process;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    this.RegisterAdjustmentRules(builder);
    RegistrationExtensions.RegisterType<ScheduleProvider>(builder).As<IScheduleProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<ScheduleProcessor.Settings>(builder).As<ScheduleProcessor.ISettings>().SingleInstance();
    RegistrationExtensions.RegisterType<ScheduleProcessor>(builder).As<IScheduleProcessor>().SingleInstance();
    RegistrationExtensions.RegisterType<ScheduleProcessorService>(builder).As<IScheduleProcessorService>().As<IHostedService>().SingleInstance();
    RegistrationExtensions.RegisterType<RecurringNotificationsSender>(builder).Keyed<IScheduledJobHandler>((object) "SendEmailNotification").As<IScheduledJobHandler>();
    RegistrationExtensions.RegisterType<ScheduledJobHandlerProvider>(builder).As<IScheduledJobHandlerProvider>().SingleInstance();
    OptionsContainerBuilderExtensions.BindFromConfiguration<ScheduleProcessorOptions>(builder);
  }

  private void RegisterAdjustmentRules(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<DailyScheduleAdjustmentRule>(builder).As<IScheduleAdjustmentRule>().SingleInstance();
    RegistrationExtensions.RegisterType<WeeklyScheduleAdjustmentRule>(builder).As<IScheduleAdjustmentRule>().SingleInstance();
    RegistrationExtensions.RegisterType<MonthlyScheduleAdjustmentRule>(builder).As<IScheduleAdjustmentRule>().SingleInstance();
    RegistrationExtensions.RegisterType<ScheduleAdjustmentRuleProvider>(builder).As<IScheduleAdjustmentRuleProvider>().SingleInstance();
  }
}
