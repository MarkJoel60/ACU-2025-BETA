// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicensingManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Services;
using PX.Data;
using PX.Data.Update;
using PX.Security;
using PX.SM;
using PX.SP;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using SerilogTimings.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

#nullable disable
namespace PX.Licensing;

internal sealed class LicensingManager : ILicensingManager, ILicensing, IStartable
{
  private static ILogger Logger = Serilog.Core.Logger.None;
  private readonly PXAccessProvider _accessProvider;
  private readonly ILicensingConfigurationProvider _licensingConfigurationProvider;
  private readonly Func<ILicenseService> _licenseServiceFactory;
  private readonly Func<ILicensing, IPXLicensePolicy> _licensePolicyFactory;
  private readonly ILegacySignOutManager _legacySignOutManager;
  private readonly IUserManagementService _userManagementService;
  private readonly ILegacyCompanyService _legacyCompanyService;
  private readonly IPortalService _portalService;
  private readonly IOptions<LicensingOptions> _licensingOptions;
  private readonly IOptions<LicenseObserverServiceOptions> _licenseObserverServiceOptions;
  private readonly ICodeSigningManager _codeSigningManager;
  private PXLicenseObserver _licenseObserver;
  private LicenseObserverService _licenseObserverService;
  private IPXLicensePolicy _licensePolicy;
  internal static readonly DummyLicensePolicy DummyLicensePolicy = new DummyLicensePolicy();

  internal static void InitializeLogging(ILogger logger)
  {
    LicensingManager.Logger = LicensingLog.ForClassContext(logger, typeof (LicensingManager));
  }

  public LicensingManager(
    PXAccessProvider accessProvider,
    ILicensingConfigurationProvider licensingConfigurationProvider,
    Func<ILicenseService> licenseServiceFactory,
    Func<ILicensing, IPXLicensePolicy> licensePolicyFactory,
    ILegacySignOutManager legacySignOutManager,
    IUserManagementService userManagementService,
    ILegacyCompanyService legacyCompanyService,
    IPortalService portalService,
    IOptions<LicensingOptions> licensingOptions,
    IOptions<LicenseObserverServiceOptions> licenseObserverServiceOptions,
    ICodeSigningManager codeSigningManager)
  {
    this._accessProvider = accessProvider;
    this._licensingConfigurationProvider = licensingConfigurationProvider;
    this._licenseServiceFactory = licenseServiceFactory;
    this._licensePolicyFactory = licensePolicyFactory;
    this._legacySignOutManager = legacySignOutManager;
    this._userManagementService = userManagementService;
    this._legacyCompanyService = legacyCompanyService;
    this._portalService = portalService;
    this._licensingOptions = licensingOptions;
    this._licenseObserverServiceOptions = licenseObserverServiceOptions;
    this._codeSigningManager = codeSigningManager;
  }

  internal static ILicensingManager Instance { get; private set; } = (ILicensingManager) new LicensingManager.ApplicationStart();

  void IStartable.Start()
  {
    using (LoggerOperationExtensions.TimeOperation(LicensingManager.Logger, "Starting LicensingManager", Array.Empty<object>()))
    {
      this._licenseObserver = new PXLicenseObserver(LicensingLog.ForClassContext(LicensingManager.Logger, typeof (PXLicenseObserver)), this._licenseServiceFactory, (ILicensingManager) this, this._legacySignOutManager, this._userManagementService, this._legacyCompanyService, this._portalService, this._licensingOptions);
      this._licenseObserverService = new LicenseObserverService(this._licenseObserverServiceOptions, this, LicensingLog.ForClassContext(LicensingManager.Logger, typeof (LicenseObserverService)));
      this._licensePolicy = this._licensePolicyFactory((ILicensing) this);
      LicensingManager.InvalidateLicense();
      PXLicense license;
      using (LoggerOperationExtensions.TimeOperation(LicensingManager.Logger, "Reading license", Array.Empty<object>()))
        license = this.License;
      CachedLicenseDefinition.Subscribe((System.Action) (() => HostingEnvironment.QueueBackgroundWorkItem((System.Action<CancellationToken>) (_ => this._licensingConfigurationProvider.SetLicense((PXLicenseDefinition) this.License)))));
      this._licensingConfigurationProvider.SetLicense((PXLicenseDefinition) license);
      LicensingManager.Instance = (ILicensingManager) this;
    }
  }

  Task ILicensingManager.StartAsync(CancellationToken cancellationToken)
  {
    this._licenseObserverService.Start();
    return Task.CompletedTask;
  }

  private static byte[] GetInstallationId(
    PXAccessProvider accessProvider,
    ILicenseService licenseService,
    ICodeSigningManager codeSigningManager)
  {
    bool flag;
    try
    {
      flag = licenseService != null && licenseService.GetInstallationIdFromProvider();
    }
    catch (Exception ex)
    {
      LicensingManager.Logger.ForMethodContext(nameof (GetInstallationId)).Error<string, string>(ex, "Error while calling {Method} from {Service}", "GetInstallationIdFromProvider", licenseService?.GetType().FullName);
      throw;
    }
    byte[] numArray = (byte[]) null;
    if (accessProvider.IsMultiDbMode | flag && codeSigningManager != null && codeSigningManager.VerifyAssemblyCodeSign(accessProvider))
      numArray = accessProvider.InstallationID;
    return numArray ?? PXCriptoHelper.CalculateSHA(PXLicenseHelper.InstallationIdBase);
  }

  private static string GetPrettyStringInstallationId(
    PXAccessProvider accessProvider,
    ILicenseService licenseService,
    ICodeSigningManager codeSigningManager)
  {
    return PXCriptoHelper.ConvertBytes(LicensingManager.GetInstallationId(accessProvider, licenseService, codeSigningManager), 4);
  }

  public string InstallationId
  {
    get
    {
      return PXCriptoHelper.ConvertBytes(LicensingManager.GetInstallationId(this._accessProvider, this._licenseServiceFactory(), this._codeSigningManager), 0);
    }
  }

  string ILicensing.PrettyInstallationId
  {
    get
    {
      return LicensingManager.GetPrettyStringInstallationId(this._accessProvider, this._licenseServiceFactory(), this._codeSigningManager);
    }
  }

  internal void InitialiseLicense(PXLicense license)
  {
    CachedLicenseDefinition slot;
    try
    {
      slot = CachedLicenseDefinition.GetSlot((ILicensingManager) this);
    }
    catch (Exception ex)
    {
      LicensingManager.Logger.ForMethodContext(nameof (InitialiseLicense)).Error(ex, "Error while getting cached license definition slot");
      return;
    }
    if (slot != null)
    {
      slot.SetCachedLicense(license);
    }
    else
    {
      if (!LicensingManager.Logger.IsEnabled((LogEventLevel) 0))
        return;
      LicensingManager.Logger.ForMethodContext(nameof (InitialiseLicense)).Verbose("Cached license definition is null, skipping license initialization");
    }
  }

  public PXLicense License
  {
    get
    {
      CachedLicenseDefinition slot;
      try
      {
        slot = CachedLicenseDefinition.GetSlot((ILicensingManager) this);
      }
      catch (Exception ex)
      {
        return LicensingManager.LogAndReturnBlankLicense("error while getting cached license definition slot", this._codeSigningManager, (LogEventLevel) 4, ex, nameof (License));
      }
      if (slot == null)
        return LicensingManager.LogAndReturnBlankLicense("cached license definition slot is empty", this._codeSigningManager, memberName: nameof (License));
      PXLicense cachedLicense = slot.GetCachedLicense();
      return cachedLicense == null ? LicensingManager.LogAndReturnBlankLicense("cached license definition is null", this._codeSigningManager, memberName: nameof (License)) : cachedLicense.Clone();
    }
  }

  private static void InvalidateLicense()
  {
    using (LoggerOperationExtensions.OperationAt(LicensingManager.Logger, (LogEventLevel) 1, new LogEventLevel?()).Time("Resetting cached license definition slot", Array.Empty<object>()))
      CachedLicenseDefinition.ResetSlot();
  }

  void ILicensingManager.InvalidateLicense() => LicensingManager.InvalidateLicense();

  void ILicensingManager.TrackRequest() => this._licenseObserver.OnHandler();

  void ILicensingManager.TrackAuthentication() => this._licenseObserver.OnAuthenticate();

  IEnumerable<RowActiveUserInfo> ILicensingManager.GetCurrentUsers()
  {
    return this._licenseObserver.GetCurrentUsers();
  }

  void ILicensingManager.RemoveSession(ILicensingSession session)
  {
    this._licenseObserver.OnExpiration(session.SessionId);
  }

  void ILicensingManager.RequestLogOut(string company)
  {
    this._licenseObserver.RequestLogOut(company);
  }

  IPXLicensePolicy ILicensingManager.Policy => this._licensePolicy;

  IApiLicensePolicy ILicensing.ApiPolicy => (IApiLicensePolicy) this._licensePolicy;

  bool ILicensingManager.ValidateLicense(LicenseBucket license, string externalInstallationId = null)
  {
    if (license == null)
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "license is null");
      return false;
    }
    string str = externalInstallationId ?? this.InstallationId;
    if (string.IsNullOrEmpty(license.Restriction))
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "no restriction");
      return false;
    }
    if (string.IsNullOrEmpty(license.Signature))
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "no signature");
      return false;
    }
    string installationId = LicensingManager.ExtractInstallationID(license.Restriction);
    if (!str.Equals(installationId, StringComparison.Ordinal))
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").ForContext("InstallationID", (object) str, false).ForContext("InstallationIDFromLicense", (object) installationId, false).Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "installation ids do not match");
      return false;
    }
    byte[] signatureHash = Convert.FromBase64String(license.Signature);
    if (!PXCriptoHelper.ValidateHash(PXCriptoHelper.CalculateSHA(license.Restriction), signatureHash))
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "signature/hash mismatch");
      return false;
    }
    ILicenseService ilicenseService;
    try
    {
      ilicenseService = this._licenseServiceFactory();
    }
    catch (Exception ex)
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Error<System.Type>(ex, "Error while getting {Service}", typeof (ILicenseService));
      throw;
    }
    try
    {
      if (!ilicenseService.ValidateRestriction(license.Restriction))
      {
        LicensingManager.Logger.ForMethodContext("ValidateLicense").Warning<bool, string>("License validation returning {LicenseValidationResult} because {LicenseValidationSkipReason}", false, "license service said the license is invalid");
        return false;
      }
    }
    catch (Exception ex)
    {
      LicensingManager.Logger.ForMethodContext("ValidateLicense").Error<string, string>(ex, "Error while calling {Method} from {Service}", "ValidateRestriction", ilicenseService.GetType().FullName);
      throw;
    }
    return true;
  }

  private static string ExtractInstallationID(string restriction)
  {
    XDocument xdocument;
    try
    {
      xdocument = XDocument.Parse(restriction);
    }
    catch (Exception ex)
    {
      LicensingManager.Logger.ForMethodContext(nameof (ExtractInstallationID)).Error(ex, "Error while loading license definition xml");
      throw;
    }
    return xdocument.Root.Elements((XName) "general").FirstOrDefault<XElement>()?.Attribute((XName) "installationID")?.Value;
  }

  private static PXLicense LogAndReturnBlankLicense(
    string reason,
    LogEventLevel level = 3,
    Exception exception = null,
    [CallerMemberName] string memberName = null)
  {
    return LicensingManager.LogAndReturnBlankLicense(reason, (ICodeSigningManager) null, level, exception, memberName);
  }

  private static PXLicense LogAndReturnBlankLicense(
    string reason,
    ICodeSigningManager codeSigningManager,
    LogEventLevel level = 3,
    Exception exception = null,
    [CallerMemberName] string memberName = null)
  {
    LicensingManager.Logger.ForContext("MemberContext", (object) memberName, false).Write<string>(level, exception, "Returning blank license: {BlankLicenseReason}", reason);
    return new PXLicense(codeSigningManager);
  }

  public PXLicense GetLicense(LicenseBucket bucket)
  {
    try
    {
      if (bucket == null)
        return LicensingManager.LogAndReturnBlankLicense("bucket is null", this._codeSigningManager, memberName: nameof (GetLicense));
      if (string.IsNullOrEmpty(bucket.Restriction))
        return LicensingManager.LogAndReturnBlankLicense("no restriction", this._codeSigningManager, memberName: nameof (GetLicense));
      return string.IsNullOrEmpty(bucket.Signature) ? LicensingManager.LogAndReturnBlankLicense("no signature", this._codeSigningManager, memberName: nameof (GetLicense)) : new PXLicense((ILicensingManager) this, this._codeSigningManager, bucket);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      return LicensingManager.LogAndReturnBlankLicense(ex.Message, this._codeSigningManager, (LogEventLevel) 4, ex, nameof (GetLicense));
    }
  }

  PXLicense ILicensingManager.GetLicense()
  {
    ILogger logger = LicensingManager.Logger.ForMethodContext("GetLicense");
    string installationId = this.InstallationId;
    PXLoginScope pxLoginScope = (PXLoginScope) null;
    if (!PXContext.PXIdentity.Authenticated)
    {
      string userName = "admin" + (PXDatabase.Companies.Length != 0 ? "@" + PXDatabase.Companies[0] : string.Empty);
      pxLoginScope = new PXLoginScope(userName, PXAccess.GetAdministratorRoles());
      logger.Debug<string>("Request not authenticated, impersonating as {UserName}", userName);
    }
    using (pxLoginScope)
    {
      LicenseBucket bucket = this.ReadLicense();
      PXLicense license1 = this.GetLicense(bucket);
      LicenseBucket licenseBucket = (LicenseBucket) null;
      if (bucket == null || !license1.Configured)
      {
        logger.Debug<string, string>("License not found ({LicenseFallbackReason}), attempting to {LicenseFallback}", bucket == null ? "bucket is null" : "license is not configured", "read license file");
        try
        {
          string appDataFolder = PXInstanceHelper.AppDataFolder;
          if (!string.IsNullOrEmpty(appDataFolder))
          {
            string path = Path.Combine(appDataFolder, "license.als");
            if (File.Exists(path))
            {
              byte[] data;
              try
              {
                data = File.ReadAllBytes(path);
              }
              catch (Exception ex)
              {
                logger.Error<string>(ex, "Error while reading license file {LicenseFile}", path);
                throw;
              }
              try
              {
                using (LogContext.PushProperty("LicenseFile", (object) path, false))
                  licenseBucket = this.ParseLicenseFile(new PX.SM.FileInfo("license.als", (string) null, data));
              }
              catch (Exception ex)
              {
                logger.Error<string>(ex, "Error while parsing license file {LicenseFile}", path);
                throw;
              }
              logger.ForContext(licenseBucket).Information("Successfully loaded license file");
              try
              {
                File.Delete(path);
              }
              catch (Exception ex)
              {
                logger.Error<string>(ex, "Error while deleting license file {LicenseFile}", path);
                throw;
              }
              if (!licenseBucket.SkipValidateOnLoadFromFile)
              {
                if (((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).All<Assembly>((Func<Assembly, bool>) (a => a.GetName().Name != "PX.Licensing")))
                {
                  PXLicenseHelper.ForceRequestLicense = true;
                  try
                  {
                    File.WriteAllText(Path.Combine(PXInstanceHelper.AppDataFolder, "license.temp.info"), System.DateTime.UtcNow.ToString());
                  }
                  catch (Exception ex)
                  {
                    logger.Error(ex, "Can't create license.temp.info file.");
                  }
                }
              }
            }
            else
              logger.Debug<string>("Can't find license file {LicenseFile}", path);
          }
          else
            logger.Warning("Can't determine AppData folder path");
        }
        catch (Exception ex)
        {
          logger.Error(ex, "License file auto-import failed");
        }
      }
      try
      {
        string appDataFolder = PXInstanceHelper.AppDataFolder;
        if (!string.IsNullOrEmpty(appDataFolder))
        {
          if (File.Exists(Path.Combine(appDataFolder, "license.temp.info")))
            PXLicenseHelper.ForceRequestLicense = true;
        }
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Can't check existing of license.temp.info file.");
      }
      if (licenseBucket == null && (bucket == null || !license1.Configured))
      {
        logger.Debug<string, string>("License not found ({LicenseFallbackReason}), attempting to {LicenseFallback}", bucket == null ? "bucket is null" : "license is not configured", "request license from server");
        if (PXLicenseHelper.GetLicenseKey(installationId, true) != null)
        {
          if (PXLicenseHelper.AutoRenewal)
          {
            if (!((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Any<Assembly>((Func<Assembly, bool>) (a => a.GetName().Name == "PX.Licensing")))
            {
              try
              {
                licenseBucket = this.RequestLicense(PXLicenseReason.Repair, true);
              }
              catch (Exception ex)
              {
                logger.Error(ex, "Error while getting license from server");
              }
              logger.ForContext(licenseBucket).Information("Successfully got license from server");
            }
            else
              logger.Debug<string>("Won't get license from server: {LicenseServerRequestSkipReason}", "PX.Licensing loaded into AppDomain");
          }
          else
            logger.Debug<string>("Won't get license from server: {LicenseServerRequestSkipReason}", "auto-renewal disabled");
        }
        else
          logger.Debug<string>("Won't get license from server: {LicenseServerRequestSkipReason}", "no license key");
      }
      if (licenseBucket != null)
      {
        PXLicense license2 = this.GetLicense(licenseBucket);
        if (license2.Validated)
        {
          logger.ForContext("License", (object) license2, true).Debug("Got license from license fallback, updating");
          this.WriteLicense(licenseBucket);
          license1 = license2;
        }
        else
          logger.ForContext("License", (object) license2, true).Error<PXLicenseState>("Got invalid license from license fallback: {LicenseState}", license2.State);
      }
      else if (bucket == null || !license1.Configured)
        logger.Debug("License fallback didn't get anything");
      return license1;
    }
  }

  internal static ILicensingManager GetVerifiedManager(IServiceProvider services)
  {
    if (LicensingManager.Instance is LicensingManager instance)
    {
      instance.Verify(services);
      return LicensingManager.Instance;
    }
    LicensingManager.ThrowForFailedServiceVerification("0");
    throw new InvalidOperationException();
  }

  private void Verify(IServiceProvider services)
  {
    this.VerifyInterfaceImplementation(services, typeof (ILicensingManager));
    this.VerifyInterfaceImplementation(services, typeof (ILicensing));
    this.VerifyLicensePolicy();
  }

  private void VerifyInterfaceImplementation(IServiceProvider services, System.Type serviceType)
  {
    object requiredService = ServiceProviderServiceExtensions.GetRequiredService(services, serviceType);
    if (requiredService.GetType() != typeof (LicensingManager))
      LicensingManager.ThrowForFailedServiceVerification("a");
    object[] array = ServiceProviderServiceExtensions.GetServices(services, serviceType).ToArray<object>();
    if (array.Length != 1)
      LicensingManager.ThrowForFailedServiceVerification("b");
    if (array[0] != requiredService)
      LicensingManager.ThrowForFailedServiceVerification("c");
    if (this == requiredService)
      return;
    LicensingManager.ThrowForFailedServiceVerification("d");
  }

  private void VerifyLicensePolicy()
  {
    Assembly assembly = this._licensePolicy.GetType().Assembly;
    if (!this._codeSigningManager.VerifyAssemblyCodeSign(assembly))
      LicensingManager.ThrowForFailedServiceVerification("e", assembly.FullName);
    if (assembly.GetName().Name.Equals("PX.LicensePolicy", StringComparison.Ordinal))
      return;
    LicensingManager.ThrowForFailedServiceVerification("f", assembly.FullName);
  }

  private static void ThrowForFailedServiceVerification(string marker)
  {
    throw new InvalidOperationException($"The licensing service configuration is invalid (case {marker})");
  }

  private static void ThrowForFailedServiceVerification(string marker, string assemblyName)
  {
    throw new InvalidOperationException($"The licensing service configuration is invalid (case {marker}) for assembly {assemblyName}");
  }

  void ILicensingManager.InitializePXLogin(PXLogin toInitialize)
  {
    toInitialize.InitializeLicensing((ILicensingManager) this, this._licenseServiceFactory, this._licensePolicy);
  }

  private class ApplicationStart : ILicensingManager, ILicensing
  {
    private static Exception Error()
    {
      throw new InvalidOperationException("Licensing services not yet available");
    }

    Task ILicensingManager.StartAsync(CancellationToken cancellationToken)
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    string ILicensingManager.InstallationId => throw LicensingManager.ApplicationStart.Error();

    string ILicensing.PrettyInstallationId
    {
      get
      {
        return LicensingManager.GetPrettyStringInstallationId(PXAccess.Provider, (ILicenseService) null, (ICodeSigningManager) null);
      }
    }

    void ILicensingManager.TrackRequest()
    {
      HttpContext current = HttpContext.Current;
      if ((current != null ? current.TryGetLicensingSession() : (ILicensingSession) null) != null)
        throw LicensingManager.ApplicationStart.Error();
    }

    void ILicensingManager.TrackAuthentication() => throw LicensingManager.ApplicationStart.Error();

    IEnumerable<RowActiveUserInfo> ILicensingManager.GetCurrentUsers()
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    void ILicensingManager.RemoveSession(ILicensingSession _)
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    void ILicensingManager.RequestLogOut(string _)
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    void ILicensingManager.InvalidateLicense() => throw LicensingManager.ApplicationStart.Error();

    bool ILicensingManager.ValidateLicense(LicenseBucket license, string installId)
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    PXLicense ILicensingManager.GetLicense(LicenseBucket bucket)
    {
      return LicensingManager.LogAndReturnBlankLicense("Licensing services not yet available", (LogEventLevel) 4, memberName: "GetLicense");
    }

    PXLicense ILicensingManager.GetLicense()
    {
      return LicensingManager.LogAndReturnBlankLicense("Licensing services not yet available", (LogEventLevel) 4, memberName: "GetLicense");
    }

    PXLicense ILicensing.License
    {
      get
      {
        return LicensingManager.LogAndReturnBlankLicense("Licensing services not yet available", (LogEventLevel) 4, memberName: "License");
      }
    }

    void ILicensingManager.InitializePXLogin(PXLogin toInitialize)
    {
      throw LicensingManager.ApplicationStart.Error();
    }

    IPXLicensePolicy ILicensingManager.Policy
    {
      get => (IPXLicensePolicy) LicensingManager.DummyLicensePolicy;
    }

    IApiLicensePolicy ILicensing.ApiPolicy
    {
      get => (IApiLicensePolicy) LicensingManager.DummyLicensePolicy;
    }
  }
}
