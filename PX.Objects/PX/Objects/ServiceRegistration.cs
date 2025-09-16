// Decompiled with JetBrains decompiler
// Type: PX.Objects.ServiceRegistration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Microsoft.Extensions.Options;
using PX.CloudServices;
using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Data.Process;
using PX.Data.RelativeDates;
using PX.Data.Reports.Services;
using PX.Data.Search;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AR.Repositories;
using PX.Objects.AU;
using PX.Objects.CA;
using PX.Objects.CA.Repositories;
using PX.Objects.CA.Utility;
using PX.Objects.CC.PaymentProcessing;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.Common.Services;
using PX.Objects.CR.CRMarketingListMaint_Extensions;
using PX.Objects.CR.Services;
using PX.Objects.CS;
using PX.Objects.EndpointAdapters;
using PX.Objects.EndpointAdapters.WorkflowAdapters.AP;
using PX.Objects.EndpointAdapters.WorkflowAdapters.AR;
using PX.Objects.EndpointAdapters.WorkflowAdapters.IN;
using PX.Objects.EndpointAdapters.WorkflowAdapters.PO;
using PX.Objects.EndpointAdapters.WorkflowAdapters.SO;
using PX.Objects.EP;
using PX.Objects.EP.ClockInClockOut;
using PX.Objects.EP.Imc;
using PX.Objects.FA;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.SM;
using PX.SM;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects;

public class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<FinancialPeriodManager>(builder).As<IFinancialPeriodManager>();
    RegistrationExtensions.WithParameter<FinPeriodScheduleAdjustmentRule, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<FinPeriodScheduleAdjustmentRule>(builder), (Parameter) TypedParameter.From<Func<PXGraph>>((Func<PXGraph>) (() => new PXGraph()))).As<IScheduleAdjustmentRule>().SingleInstance();
    RegistrationExtensions.RegisterType<TodayBusinessDate>(builder).As<ITodayUtc>();
    this.RegisterNotificationServices(builder);
    RegistrationExtensions.RegisterType<FinPeriodRepository>(builder).As<IFinPeriodRepository>();
    RegistrationExtensions.RegisterType<FinPeriodUtils>(builder).As<IFinPeriodUtils>();
    RegistrationExtensions.AsSelf<PaymentConnectorCallbackService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PaymentConnectorCallbackService>(builder));
    RegistrationExtensions.Register<Func<PXGraph, IPXCurrencyService>>(builder, (Func<IComponentContext, Func<PXGraph, IPXCurrencyService>>) (context => (Func<PXGraph, IPXCurrencyService>) (graph => (IPXCurrencyService) new DatabaseCurrencyService(graph))));
    RegistrationExtensions.RegisterType<FABookPeriodRepository>(builder).As<IFABookPeriodRepository>();
    RegistrationExtensions.RegisterType<FABookPeriodUtils>(builder).As<IFABookPeriodUtils>();
    RegistrationExtensions.RegisterType<BudgetService>(builder).As<IBudgetService>();
    RegistrationExtensions.RegisterType<UnitRateService>(builder).As<IUnitRateService>();
    RegistrationExtensions.RegisterType<ProjectSettingsManager>(builder).As<IProjectSettingsManager>();
    RegistrationExtensions.RegisterType<CostCodeManager>(builder).As<ICostCodeManager>();
    RegistrationExtensions.RegisterType<ProjectMultiCurrency>(builder).As<IProjectMultiCurrency>();
    this.RegisterCbApiAdapters(builder);
    RegistrationExtensions.AsSelf<BillAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<BillAdapter>(builder));
    RegistrationExtensions.AsSelf<CheckAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CheckAdapter>(builder));
    RegistrationExtensions.AsSelf<InvoiceAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<InvoiceAdapter>(builder));
    RegistrationExtensions.AsSelf<PaymentAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PaymentAdapter>(builder));
    RegistrationExtensions.AsSelf<InventoryReceiptAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<InventoryReceiptAdapter>(builder));
    RegistrationExtensions.AsSelf<InventoryAdjustmentAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<InventoryAdjustmentAdapter>(builder));
    RegistrationExtensions.AsSelf<TransferOrderAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<TransferOrderAdapter>(builder));
    RegistrationExtensions.AsSelf<KitAssemblyAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<KitAssemblyAdapter>(builder));
    RegistrationExtensions.AsSelf<PurchaseOrderAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PurchaseOrderAdapter>(builder));
    RegistrationExtensions.AsSelf<PurchaseReceiptAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PurchaseReceiptAdapter>(builder));
    RegistrationExtensions.AsSelf<SalesOrderAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<SalesOrderAdapter>(builder));
    RegistrationExtensions.AsSelf<ShipmentAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ShipmentAdapter>(builder));
    RegistrationExtensions.AsSelf<SalesInvoiceAdapter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<SalesInvoiceAdapter>(builder));
    RegistrationExtensions.RegisterType<NumberingSequenceUsage>(builder).As<INumberingSequenceUsage>();
    RegistrationExtensions.RegisterType<AdvancedAuthenticationRestrictor>(builder).As<IAdvancedAuthenticationRestrictor>().SingleInstance();
    RegistrationExtensions.RegisterType<PXEntitySearchEnriched>(builder).As<IEntitySearchService>();
    RegistrationExtensions.RegisterType<InventoryAccountService>(builder).As<IInventoryAccountService>();
    RegistrationExtensions.RegisterType<PXEntitySearchEnriched>(builder).As<IEntitySearchService>();
    RegistrationExtensions.RegisterType<CustomTimeRegionProvider>(builder).As<ITimeRegionProvider>().SingleInstance();
    RegistrationExtensions.AsSelf<DirectDepositTypeService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DirectDepositTypeService>(builder));
    RegistrationExtensions.RegisterType<CABankTransactionsRepository>(builder).As<ICABankTransactionsRepository>();
    RegistrationExtensions.RegisterType<MatchingService>(builder).As<IMatchingService>();
    RegistrationExtensions.PreserveExistingDefaults<CCDisplayMaskService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<CCDisplayMaskService>(builder).As<ICCDisplayMaskService>());
    this.RegisterEPModuleServices(builder);
    this.RegisterMailServices(builder);
    this.RegisterCrHelpers(builder);
    OptionsContainerBuilderExtensions.BindFromConfiguration<InvoiceRecognitionModelOptions>(builder, new string[1]
    {
      "invoicerecognitionmodel"
    });
    RegistrationExtensions.RegisterInstance<IValidateOptions<InvoiceRecognitionModelOptions>>(builder, (IValidateOptions<InvoiceRecognitionModelOptions>) new ValidateOptions<InvoiceRecognitionModelOptions>((string) null, (Func<InvoiceRecognitionModelOptions, bool>) (options => Models.KnownModels.ContainsKey(options.Name)), "Unknown invoice recognition model"));
    RegistrationExtensions.RegisterType<SendReportService>(builder).As<ISendReportService>();
    RegistrationExtensions.RegisterType<ClockInClockOutProvider>(builder).As<IClockInClockOutProvider>().SingleInstance();
  }

  private void RegisterNotificationServices(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<NotificationProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<NotificationProvider>(builder).As<INotificationSender>().SingleInstance());
    RegistrationExtensions.RegisterType<NotificationService>(builder).As<INotificationService>().SingleInstance();
  }

  private void RegisterEPModuleServices(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<EPEventVCalendarProcessor>(builder).As<IVCalendarProcessor>().SingleInstance();
    RegistrationExtensions.RegisterType<VCalendarFactory>(builder).As<IVCalendarFactory>().SingleInstance();
    RegistrationExtensions.RegisterType<ActivityService>(builder).As<IActivityService>().SingleInstance();
    RegistrationExtensions.RegisterType<ActivityCommandExecutor>(builder).As<IActivityCommandExecutor>().SingleInstance();
    RegistrationExtensions.AsSelf<ReportNotificationGenerator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ReportNotificationGenerator>(builder)).InstancePerDependency();
  }

  private void RegisterMailServices(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<CommonMailSendProvider>(builder).As<IMailSendProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<CommonMailReceiveProvider>(builder).As<IMailReceiveProvider>().As<IMessageProccessor>().As<IOriginalMailProvider>().SingleInstance();
    Assembly[] array = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray<Assembly>();
    RegistrationExtensions.AssignableTo<IEmailProcessor>(AutofacExtensions.RegisterAssemblyTypesAssignableToWithCaching(builder, (Func<Type, bool>) null, typeof (IEmailProcessor), array)).As<IEmailProcessor>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<OrderedEmailProcessorsProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<OrderedEmailProcessorsProvider>(builder).As<IEmailProcessorsProvider>().SingleInstance());
  }

  private void RegisterCbApiAdapters(ContainerBuilder builder)
  {
    RegistrationExtensions.AsSelf<DefaultEndpointImplCR20, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplCR20>(builder));
    RegistrationExtensions.AsSelf<DefaultEndpointImplCR22, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplCR22>(builder));
    RegistrationExtensions.AsSelf<DefaultEndpointImplCR23, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplCR23>(builder));
    RegistrationExtensions.AsSelf<DefaultEndpointImplCR24, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplCR24>(builder));
    RegistrationExtensions.AsSelf<DefaultEndpointImplCR25, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplCR25>(builder));
    RegistrationExtensions.AsSelf<DefaultEndpointImplPM, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DefaultEndpointImplPM>(builder));
    RegistrationExtensions.AsSelf<CbApiWorkflowApplicator.CaseApplicator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CbApiWorkflowApplicator.CaseApplicator>(builder).SingleInstance());
    RegistrationExtensions.AsSelf<CbApiWorkflowApplicator.OpportunityApplicator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CbApiWorkflowApplicator.OpportunityApplicator>(builder).SingleInstance());
    RegistrationExtensions.AsSelf<CbApiWorkflowApplicator.LeadApplicator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CbApiWorkflowApplicator.LeadApplicator>(builder).SingleInstance());
    RegistrationExtensions.AsSelf<CbApiWorkflowApplicator.ProjectTemplateApplicator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CbApiWorkflowApplicator.ProjectTemplateApplicator>(builder).SingleInstance());
    RegistrationExtensions.AsSelf<CbApiWorkflowApplicator.ProjectTaskApplicator, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<CbApiWorkflowApplicator.ProjectTaskApplicator>(builder).SingleInstance());
  }

  private void RegisterCrHelpers(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<CRMarketingListMemberRepository>(builder).As<ICRMarketingListMemberRepository>();
  }
}
