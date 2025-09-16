// Decompiled with JetBrains decompiler
// Type: PX.Data.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PX.Api.Mobile.PushNotifications;
using PX.Async;
using PX.Caching;
using PX.Common;
using PX.Common.Mail;
using PX.Common.Service;
using PX.Data.Access.LoginAs;
using PX.Data.Api;
using PX.Data.Automation;
using PX.Data.Config;
using PX.Data.DacDescriptorGeneration;
using PX.Data.DeletedRecordsTracking;
using PX.Data.DependencyInjection;
using PX.Data.Descriptor;
using PX.Data.EP;
using PX.Data.Hosting.RegularOperations;
using PX.Data.Localization;
using PX.Data.MultiFactorAuth;
using PX.Data.Reports;
using PX.Data.Services;
using PX.Data.Services.Implementations;
using PX.Data.Services.Interfaces;
using PX.Data.UserRecords;
using PX.Data.Wiki.Parser;
using PX.Export.Authentication;
using PX.Licensing;
using PX.Reports;
using PX.Security;
using PX.Security.Authorization;
using PX.SM;
using PX.Translation;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;

#nullable enable
namespace PX.Data;

public class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<PropertyDependencyInjector, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PropertyDependencyInjector>(builder).As<IDependencyInjector>().InstancePerLifetimeScope());
    RegistrationExtensions.RegisterType<DataScreenFactory>(builder).As<IDataScreenFactory>().SingleInstance();
    RegistrationExtensions.Register<IListProvider>(builder, (Func<IComponentContext, IListProvider>) (c => (IListProvider) new PXListProvider()
    {
      Successor = (IListProvider) new GIListProvider(ResolutionExtensions.Resolve<IPXPageIndexingService>(c))
      {
        Successor = (IListProvider) new PXListProvider()
      }
    })).As<IListProvider>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<MapperConfiguration, SimpleActivatorData, SingleRegistrationStyle>(RegistrationExtensions.Register<MapperConfiguration>(builder, (Func<IComponentContext, MapperConfiguration>) (c =>
    {
      IComponentContext ctx = ResolutionExtensions.Resolve<IComponentContext>(c);
      return new MapperConfiguration((System.Action<IMapperConfigurationExpression>) (cfg =>
      {
        cfg.ConstructServicesUsing(new Func<System.Type, object>(((ResolutionExtensions) ctx).Resolve));
        foreach (Profile profile in ResolutionExtensions.Resolve<IEnumerable<Profile>>(ctx))
          cfg.AddProfile(profile);
      }));
    })).As<IConfigurationProvider>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<Mapper, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<Mapper>(builder).As<IMapper>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<SessionContextService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<SessionContextService>(builder).As<ISessionContextService>());
    RegistrationExtensions.PreserveExistingDefaults<FeatureService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<FeatureService>(builder).As<IFeatureService>());
    RegistrationExtensions.PreserveExistingDefaults<SnapshotServiceStub, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<SnapshotServiceStub>(builder).As<ISnapshotService>());
    RegistrationExtensions.PreserveExistingDefaults<StorageServiceStub, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<StorageServiceStub>(builder).As<IStorageService>());
    RegistrationExtensions.PreserveExistingDefaults<UserService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<UserService>(builder).As<IUserService>());
    RegistrationExtensions.PreserveExistingDefaults<PX.Data.Services.VersionService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PX.Data.Services.VersionService>(builder).As<IVersionService>());
    RegistrationExtensions.PreserveExistingDefaults<PXVersionInfo.VersionService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXVersionInfo.VersionService>(builder).As<IVersionService>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<CompanyService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<CompanyService>(builder).As<ICompanyService>());
    RegistrationExtensions.RegisterType<LoginAsCookieProvider>(builder).As<ILoginAsCookieProvider>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<LoginAsUser, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<LoginAsUser>(builder).As<ILoginAsUser>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<DummyPushNotificationSender, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<DummyPushNotificationSender>(builder).As<IPushNotificationSender>());
    RegistrationExtensions.RegisterType<GraphFactory>(builder).As<IGraphFactory>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<PXGraphFactory, SimpleActivatorData, SingleRegistrationStyle>(RegistrationExtensions.AsSelf<PXGraphFactory, SimpleActivatorData>(RegistrationExtensions.Register<PXGraphFactory>(builder, (Func<IComponentContext, PXGraphFactory>) (c =>
    {
      IComponentContext ctx = ResolutionExtensions.Resolve<IComponentContext>(c);
      return (PXGraphFactory) (screenId =>
      {
        System.Type graphType = GIScreenHelper.GetGraphType(screenId);
        if (graphType == (System.Type) null)
          throw new PXException("A graph cannot be created.");
        object obj;
        PXGraphFactory pxGraphFactory = !ResolutionExtensions.TryResolveKeyed(ctx, (object) graphType, typeof (PXGraphFactory), ref obj) ? (PXGraphFactory) (_ => PXGraph.CreateInstance(graphType)) : (PXGraphFactory) obj;
        using (new PXScreenIDScope(screenId))
          return pxGraphFactory(screenId);
      });
    }))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    RegistrationExtensions.Register<PXGraphFactory>(builder, (Func<IComponentContext, PXGraphFactory>) (c => ServiceRegistration.\u003C\u003EO.\u003C0\u003E__CreateInstance ?? (ServiceRegistration.\u003C\u003EO.\u003C0\u003E__CreateInstance = new PXGraphFactory(PXGenericInqGrph.CreateInstance)))).Keyed<PXGraphFactory>((object) typeof (PXGenericInqGrph));
    RegistrationExtensions.RegisterType<EmailFactorSender>(builder).As<ITwoFactorSender>().SingleInstance();
    RegistrationExtensions.RegisterType<AccessCodeFactorSender>(builder).As<ITwoFactorSender>().SingleInstance();
    RegistrationExtensions.RegisterType<EmailMultifactorRegistrationSender>(builder).As<IMultifactorRegistrationSender>().SingleInstance();
    RegistrationExtensions.RegisterType<FilesDialogExtender>(builder).As<IFilesDialogExtender>();
    OptionsContainerBuilderExtensions.Configure<AuthenticationManagerOptions, IConfiguration>(builder, new Action<AuthenticationManagerOptions, IConfiguration>(this.ConfigureAuthentication));
    RegistrationExtensions.RegisterType<BAccountRestrictionHelper>(builder).As<IBAccountRestrictionHelper>();
    RegistrationExtensions.RegisterType<ContactProvider>(builder).As<IContactProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<ContextPXIdentityAccessor>(builder).As<IPXIdentityAccessor>().SingleInstance();
    builder.RegisterAsDefaultAllowingSingleOverride<WebAppType, DefaultWebAppType>().SingleInstance();
    RegistrationExtensions.Register<AppInstanceInfo>(builder, (Func<IComponentContext, AppInstanceInfo>) (c => new AppInstanceInfo(PXVersionInfo.Version, ResolutionExtensions.Resolve<ILicensing>(c).PrettyInstallationId, ResolutionExtensions.Resolve<WebAppType>(c), WebConfig.IsClusterEnabled))).As<IAppInstanceInfo>().InstancePerLifetimeScope();
    if (PXHostingEnvironment.IsHosted)
    {
      ApplicationStartActivation.ActivateOnApplicationStart<ReportFileWatcher, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ReportFileWatcher>(builder).SingleInstance(), (System.Action<ReportFileWatcher>) null);
      ApplicationStartActivation.ActivateOnApplicationStart<PXPageFileWatcher, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PXPageFileWatcher>(builder).SingleInstance(), (System.Action<PXPageFileWatcher>) null);
    }
    RegistrationExtensions.RegisterGeneric(builder, typeof (CacheControlAggregator<>)).As(new System.Type[1]
    {
      typeof (ICacheControl<>)
    });
    this.RegisterUserRecordsServices(builder);
    this.RegisterDacDescriptorService(builder);
    this.RegisterReportRelatedServices(builder);
    this.RegisterTimeZoneServices(builder);
    this.InitializeLocalizer(builder);
    this.RegisterMacroVariables(builder);
    this.RegisterPageIndexerService(builder);
    this.RegisterLicensingServices(builder);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ApplicationStartActivation.RunOnApplicationStart<ILogger>(builder, ServiceRegistration.\u003C\u003EO.\u003C1\u003E__CollectMessages ?? (ServiceRegistration.\u003C\u003EO.\u003C1\u003E__CollectMessages = new System.Action<ILogger>(PXMessages.CollectMessages)), (string) null);
    ApplicationStartActivation.RunOnApplicationStart<IConfiguration>(builder, (System.Action<IConfiguration>) (configuration =>
    {
      string path = Path.Combine(configuration.GetSection("SnapshotsFolder").Value ?? "", "temp");
      if (!Directory.Exists(path))
        return;
      Directory.Delete(path, true);
    }), (string) null);
    RegistrationExtensions.PreserveExistingDefaults<ScreenToGraphWorkflowMappingService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<ScreenToGraphWorkflowMappingService>(builder).As<IScreenToGraphWorkflowMappingService>());
    RegistrationExtensions.PreserveExistingDefaults<FavoriteActionsService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<FavoriteActionsService>(builder).As<IFavoriteActionsService>().InstancePerLifetimeScope());
    ServiceRegistration.RegisterMembershipServices(builder);
    RegistrationExtensions.AsSelf<PXLogin, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PXLogin>(builder).WithInternalConstructor<PXLogin, ConcreteReflectionActivatorData, SingleRegistrationStyle>()).As<IPXLogin>().OnActivated((System.Action<IActivatedEventArgs<PXLogin>>) (args => ResolutionExtensions.Resolve<ILicensingManager>(args.Context).InitializePXLogin(args.Instance))).SingleInstance();
    RegistrationExtensions.RegisterType<LocalizationFeaturesService>(builder).As<ILocalizationFeaturesService>().SingleInstance();
    OptionsContainerBuilderExtensions.BindFromConfiguration<StringCollectingOptions>(builder, new string[1]
    {
      "translation"
    });
    OptionsContainerBuilderExtensions.Configure<AuditJournalOptions, PXBaseMembershipProvider>(builder, (Action<AuditJournalOptions, PXBaseMembershipProvider>) ((options, membershipProvider) => options.Configure(membershipProvider)));
    RegistrationExtensions.RegisterType<RoleManagementService>(builder).As<IRoleManagementService>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<MailReceiver.DefaultMailReceiverBuilder, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<MailReceiver.DefaultMailReceiverBuilder>(builder).As<MailReceiver.IMailReceiverBuilder>().SingleInstance());
    MailAccountManager.RegisterFactories(builder);
    RegistrationExtensions.RegisterType<PXEmailPluginService>(builder).As<IPXEmailPluginService>();
    RegistrationExtensions.RegisterInstance<Func<ICertificateRepository>>(builder, (Func<ICertificateRepository>) (() => (ICertificateRepository) new CertificateRepository())).As<Func<ICertificateRepository>>().SingleInstance();
    ServiceRegistration.RegisterPredefinedRolesProvider<ReportOptions, ReportPredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<CustomizationOptions, CustomizationPredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<DashboardOptions, DashboardPredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<BusinessDateOptions, BusinessDatePredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<AuditOptions, AuditPredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<FinancialOptions, FinancialPredefinedRolesProvider>(builder);
    ServiceRegistration.RegisterPredefinedRolesProvider<AcumaticaSupportOptions, AcumaticaSupportPredefinedRolesProvider>(builder);
    RegistrationExtensions.RegisterType<NonConfigurablePredefinedRolesProvider>(builder).As<IPredefinedRolesProvider>().SingleInstance();
    RegistrationExtensions.Register<PXAccessProvider>(builder, (Func<IComponentContext, PXAccessProvider>) (c => ResolutionExtensions.Resolve<PXAccessProvider>(c))).As<ISystemRolesProvider, IPredefinedRolesProvider>().SingleInstance();
    builder.RegisterLongOperations();
    RegistrationExtensions.AsSelf<TSRipper, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<TSRipper>(builder)).SingleInstance();
    RegistrationExtensions.AsSelf<ScreenSidePanelRipper, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ScreenSidePanelRipper>(builder)).SingleInstance();
    OptionsContainerBuilderExtensions.Configure<FormsAuthenticationOptions>(builder, (Action<FormsAuthenticationOptions, IConfiguration>) ((options, configuration) => options.Configure(configuration.GetSection("formsAuth"), HttpRuntime.AppDomainAppVirtualPath, PXUrl.GetDefaultMainPage())));
    builder.ConfigureFromDac<FormsAuthenticationOptions, PreferencesGlobal>((DacBasedConfigureOptions<FormsAuthenticationOptions, PreferencesGlobal>.ConfigureFromDacDelegate) ((options, dac) =>
    {
      if (dac == null)
        return;
      bool? preconfiguredTimeouts = dac.UsePreconfiguredTimeouts;
      bool flag = false;
      if (!(preconfiguredTimeouts.GetValueOrDefault() == flag & preconfiguredTimeouts.HasValue))
        return;
      int? logoutTimeout = dac.LogoutTimeout;
      int num = 0;
      if (!(logoutTimeout.GetValueOrDefault() > num & logoutTimeout.HasValue))
        return;
      options.Timeout = dac.LogoutTimeout.Value;
    }));
    RegistrationExtensions.AsSelf<ModernUiAggregatedScreenWhitelist, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ModernUiAggregatedScreenWhitelist>(builder));
    OptionsContainerBuilderExtensions.BindFromConfiguration<ModernUiSwitchOptions>(builder, new string[2]
    {
      "ui",
      "switchUi"
    });
    RegistrationExtensions.RegisterType<LocalizableFieldService>(builder).As<ILocalizableFieldService>().SingleInstance();
    OptionsContainerBuilderExtensions.BindFromConfiguration<LocalizableFieldOptions>(builder, new string[2]
    {
      "localization",
      "localizableFields"
    });
    RegistrationExtensions.PreserveExistingDefaults<PXUrlResolver, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXUrlResolver>(builder).As<IUrlResolver>().SingleInstance());
    RegistrationExtensions.RegisterType<DeletedRecordsTrackingService>(builder).As<IDeletedRecordsTrackingService>().SingleInstance();
    PX.Hosting.ContainerBuilderExtensions.AsBackgroundHostedService<DeletedRecordsHistoryClearingService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<DeletedRecordsHistoryClearingService>(builder), true).SingleInstance();
    OptionsContainerBuilderExtensions.BindFromConfiguration<OptimizationOptions>(builder);
  }

  private static void RegisterPredefinedRolesProvider<TOptions, TProvider>(ContainerBuilder builder)
    where TOptions : ConfiguredValueBase, new()
    where TProvider : IPredefinedRolesProvider
  {
    OptionsContainerBuilderExtensions.Configure<TOptions>(builder, (Action<TOptions, IConfiguration>) ((options, configuration) => options.Configure(configuration)));
    RegistrationExtensions.RegisterType<TProvider>(builder).As<IPredefinedRolesProvider>().SingleInstance();
  }

  internal static void RegisterMembershipServices(ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<LicensingOptions, PXBaseMembershipProvider>(builder, (Action<LicensingOptions, PXBaseMembershipProvider>) ((options, membershipProvider) => options.Configure(membershipProvider)));
    RegistrationExtensions.RegisterType<UserManagementService>(builder).As<IUserValidationService>().As<IUserManagementService>().SingleInstance();
    RegistrationExtensions.RegisterType<UserOrganizationService>(builder).As<IUserOrganizationService>().As<PX.Data.Internal.IUserOrganizationService>().As<IUserBranchSlotControl>().SingleInstance();
    RegistrationExtensions.RegisterType<LegacyCompanyService>(builder).As<ILegacyCompanyService>().SingleInstance();
    CurrentUserInformationProvider.Register(builder);
  }

  private void RegisterUserRecordsServices(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<RecordCachedContentBuilder, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<RecordCachedContentBuilder>(builder).As<IRecordCachedContentBuilder>().InstancePerDependency());
    RegistrationExtensions.PreserveExistingDefaults<UserRecordsDBUpdater, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<UserRecordsDBUpdater>(builder).As<IUserRecordsDBUpdater>());
  }

  private void RegisterDacDescriptorService(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<DacDescriptorProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<DacDescriptorProvider>(builder).As<IDacDescriptorProvider>().SingleInstance());
    OptionsContainerBuilderExtensions.BindFromConfiguration<DacDescriptorTraceWindowOptions>(builder);
  }

  private void RegisterLicensingServices(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<LicenseWarningService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<LicenseWarningService>(builder).As<ILicenseWarningService>());
    RegistrationExtensions.PreserveExistingDefaults<PXReportLicenseProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXReportLicenseProvider>(builder).As<ReportLicenseProvider>().SingleInstance());
    RegistrationExtensions.RegisterType<CodeSigningManager>(builder).As<ICodeSigningManager>().SingleInstance();
  }

  private void RegisterReportRelatedServices(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<PXCertificateProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXCertificateProvider>(builder).As<CertificateProvider>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<PXSettingProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXSettingProvider>(builder).As<SettingsProvider>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<PXReportLocalizationProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXReportLocalizationProvider>(builder).As<LocalizationProvider>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<PXDbImagesProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXDbImagesProvider>(builder).As<DbImagesProvider>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<ReportCachingService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<ReportCachingService>(builder).As<IReportCachingService>().As<IFullTrustReportCachingService>().SingleInstance());
    RegistrationExtensions.PreserveExistingDefaults<ReportLoaderService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithParameter<ReportLoaderService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<ReportLoaderService>(builder).As<IReportLoaderService>().As<IFullTrustReportLoaderService>(), (Parameter) TypedParameter.From<ISettings>(new PXWikiSettings((Page) null).Absolute.Settings)).SingleInstance());
    RegistrationExtensions.RegisterType<ReportRunner>(builder).SingleInstance().As<IReportRunner>();
    OptionsContainerBuilderExtensions.Configure<ReportStoreOptions>(builder, (Action<ReportStoreOptions, IConfiguration>) ((options, configuration) =>
    {
      options.Timeout = PXSessionStateStore.GetWebConfigSessionTimeout() ?? PXSessionStateStore.DefaultSessionTimeout;
      string s = ((IConfiguration) PX.Hosting.SessionState.Extensions.GetSessionStateStoreProviderSettings(configuration).Configuration)["reportsTimeout"];
      int result;
      if (s == null || !int.TryParse(s, out result) || result <= 0)
        return;
      options.Timeout = TimeSpan.FromMinutes((double) result);
    }));
  }

  private void InitializeLocalizer(ContainerBuilder builder)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container => Localizer.Localize = ServiceRegistration.\u003C\u003EO.\u003C2\u003E__Localize ?? (ServiceRegistration.\u003C\u003EO.\u003C2\u003E__Localize = new Func<string, string, string>(PXLocalizer.Localize))));
  }

  private void RegisterPageIndexerService(ContainerBuilder builder)
  {
    RegistrationExtensions.AsSelf<PXPageIndexingService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PXPageIndexingService>(builder)).SingleInstance();
    RegistrationExtensions.RegisterType<SiteMapUITypeProvider>(builder).As<ISiteMapUITypeProvider>().SingleInstance();
  }

  private void RegisterTimeZoneServices(ContainerBuilder builder)
  {
    RegistrationExtensions.PreserveExistingDefaults<SystemTimeRegionProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<SystemTimeRegionProvider>(builder).As<ITimeRegionProvider>().SingleInstance());
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container => PXTimeZoneInfo.RegisterTimeRegionProvider(ResolutionExtensions.Resolve<ITimeRegionProvider>((IComponentContext) container))));
  }

  private void RegisterMacroVariables(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<MeVariable>(builder).As<IMacroVariable>().SingleInstance();
    RegistrationExtensions.RegisterType<MyUsernameVariable>(builder).As<IMacroVariable>().SingleInstance();
    RegistrationExtensions.RegisterType<CurrentBranchVariable>(builder).As<IMacroVariable>().SingleInstance();
    RegistrationExtensions.RegisterType<CurrentOrganizationVariable>(builder).As<IMacroVariable>().SingleInstance();
    RegistrationExtensions.RegisterType<PXMacroVariablesManager>(builder).As<IMacroVariablesManager>().SingleInstance();
  }

  private void ConfigureAuthentication(
    AuthenticationManagerOptions options,
    IConfiguration configuration)
  {
    IAuthenticationManagerLocationOptionsBuilder builder = options.AddLocation("/");
    CookieAuthenticationExtensions.WithBrowser(builder);
    LegacyBasicMode legacyBasicMode = ConfigurationBinder.GetValue<LegacyBasicMode>((IConfiguration) configuration.GetSection("multiAuth"), "legacyBasicAtRoot");
    switch (legacyBasicMode)
    {
      case LegacyBasicMode.None:
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Api")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/App_Themes")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/App_Themes/GetCSS.aspx")).WithAnonymous();
        options.AddLocation("/calendarSync.ics").WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Content")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Export")).WithBasicNoCompany();
        options.AddLocation("/favicon.ico").WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/fonts")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Frames/Error.aspx")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Frames/LoginRemind.aspx")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Frames/Maintenance.aspx")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Frames/MobileAuth.aspx")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Frames/PasswordRemind.aspx")).WithAnonymous();
        options.AddLocation("/Handlers").WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Icons")).WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Images")).WithAnonymous();
        options.AddLocation("/robots.txt").WithAnonymous();
        CookieAuthenticationExtensions.WithCookie(options.AddLocation("/Scripts")).WithAnonymous();
        break;
      case LegacyBasicMode.Active:
        builder.WithBasic();
        goto case LegacyBasicMode.None;
      default:
        throw new InvalidOperationException($"Unknown {"LegacyBasicMode"} value {legacyBasicMode}");
    }
  }
}
