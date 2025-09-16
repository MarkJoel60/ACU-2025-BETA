// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpgradeHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Win32;
using PX.Common;
using PX.Logging.Sinks.SystemEventsDbSink;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

#nullable disable
namespace PX.Data.Update;

[Serializable]
public sealed class PXUpgradeHelper
{
  public static void ValidateUpdate(PXUpgradeSpec spec)
  {
    if (spec == null)
      throw new ArgumentNullException(nameof (spec));
    if (spec.Launcher == null)
      throw new ArgumentNullException("Launcher");
    PXUpgradeHelper.DoValidateUpdate(PXUpgradeHelper.GetUpdateConfig(spec));
  }

  private static void DoValidateUpdate(PXUpgradeConfig config)
  {
    if (config.Parameters.ContainsKey("dotNet"))
    {
      Version result;
      if (!Version.TryParse(config.Parameters["dotNet"], out result))
        throw new PXException("The package is not valid.");
      Version installedDotNetVersion = PXUpgradeHelper.getInstalledDotNetVersion();
      if (installedDotNetVersion != (Version) null && result > installedDotNetVersion)
        throw new PXException($"Installation of the selected package requires .NET Framework version '{result}'");
    }
    Assembly assembly = AppDomain.CurrentDomain.Load(config.Launcher);
    System.Type type1 = assembly.GetType("PX.Config.Upgrade.PXUpgradeArgs", false, false);
    System.Type type2 = assembly.GetType("PX.Config.Upgrade.PXUpgrade", false, false);
    object obj = !(type1 == (System.Type) null) && !(type2 == (System.Type) null) ? PXUpgradeHelper.GetUpdateArgs(type1, config) : throw new PXException("The package is not valid.");
    try
    {
      type2.InvokeMember("ValidateUpdate", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, (object) null, new object[1]
      {
        obj
      });
    }
    catch (TargetInvocationException ex) when (ex.InnerException != null)
    {
      throw PXException.ExtractInner((Exception) ex);
    }
  }

  private static Version getInstalledDotNetVersion()
  {
    try
    {
      using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
      {
        Version result;
        if (registryKey.GetValue("Version") is string input && Version.TryParse(input, out result))
          return result;
        int num = (int) registryKey.GetValue("Release");
        if (num >= 528040)
          return new Version(4, 8);
        if (num >= 461808)
          return new Version(4, 7, 2);
        if (num >= 461308)
          return new Version(4, 7, 1);
        if (num >= 460798)
          return new Version(4, 7);
        if (num >= 394802)
          return new Version(4, 6, 2);
        if (num >= 394254)
          return new Version(4, 6, 1);
        if (num >= 393295)
          return new Version(4, 6);
        if (num >= 379893)
          return new Version(4, 5, 2);
        if (num >= 378675)
          return new Version(4, 5, 1);
        if (num >= 378389)
          return new Version(4, 5);
      }
    }
    catch
    {
    }
    return (Version) null;
  }

  public static void LaunchUpgrade(PXUpgradeSpec spec)
  {
    if (spec == null)
      throw new ArgumentNullException(nameof (spec));
    PXUpgradeConfig args = spec.Launcher != null ? PXUpgradeHelper.GetUpdateConfig(spec) : throw new ArgumentNullException("Launcher");
    System.Action<object> action = new System.Action<object>(PXUpgradeHelper.DoLaunchUpgrade);
    PXTrace.Logger.ForSystemEvents("System", "System_SiteUpgradeStartedEventId").ForContext("ContextScreenId", (object) "SM203510", false).Information("Site upgrade has started CurrentVersion:{CurrentVersion}, DestinationVersion:{DestinationVersion}, SiteUrl:{SiteUrl}, Parameters:{@Parameters}", new object[4]
    {
      (object) args.CurrentVersion,
      (object) args.DestinationVersion,
      (object) args.SiteUrl,
      (object) args.Parameters
    });
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.RaiseProcessTerminating();
    PXExternalDomainWrapper.DoWithinAnotherDomain(action, (object) args);
  }

  private static void DoLaunchUpgrade(object args)
  {
    PXUpgradeHelper.DoLaunchUpgrade((PXUpgradeConfig) args);
  }

  private static void DoLaunchUpgrade(PXUpgradeConfig config)
  {
    Assembly assembly = AppDomain.CurrentDomain.Load(config.Launcher);
    if (System.DateTime.Now.Ticks < 0L)
      assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes("E:\\TFS\\Main\\AcumaticaInstall\\ConfigCommon\\bin\\Debug\\PX.Config.Common.dll"));
    System.Type type1 = assembly.GetType("PX.Config.Upgrade.PXUpgradeArgs", false, false);
    System.Type type2 = assembly.GetType("PX.Config.Upgrade.PXUpgrade", false, false);
    object obj = !(type1 == (System.Type) null) && !(type2 == (System.Type) null) ? PXUpgradeHelper.GetUpdateArgs(type1, config) : throw new PXException("The package is not valid.");
    try
    {
      if (System.DateTime.Now.Ticks < 0L)
        throw new Exception("test error");
      type2.InvokeMember("StartUpgrade", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, (object) null, new object[1]
      {
        obj
      });
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      PXTrace.Logger.ForSystemEvents("System", "System_SiteUpgradeFailedEventId").ForContext("ContextScreenId", (object) "SM203510", false).Error(exception, "Site upgrade failed with exception CurrentVersion:{CurrentVersion}, DestinationVersion:{DestinationVersion}, SiteUrl:{SiteUrl}, Parameters:{@Parameters}", new object[4]
      {
        (object) config.CurrentVersion,
        (object) config.DestinationVersion,
        (object) config.SiteUrl,
        (object) config.Parameters
      });
      if (exception is TargetInvocationException)
        exception = exception.InnerException;
      if (config.MessageCallback == null)
        return;
      config.MessageCallback(EventLogEntryType.Error, (string) null, exception);
    }
  }

  private static PXUpgradeConfig GetUpdateConfig(PXUpgradeSpec spec)
  {
    Action<EventLogEntryType, string, Exception> action = new Action<EventLogEntryType, string, Exception>(new UpgradeLogger(spec.CurrentVersion.ToString(true)).Write);
    PXUpgradeConfig updateConfig = new PXUpgradeConfig();
    updateConfig.Launcher = spec.Launcher;
    updateConfig.ZipPath = spec.ZipPath;
    updateConfig.ZipFiles = spec.ZipFiles;
    updateConfig.Parameters = spec.Parameters;
    updateConfig.CurrentVersion = spec.CurrentVersion;
    updateConfig.IsErp = PXInstanceHelper.CurrentInstanceType == PXInstanceType.Erp;
    updateConfig.IISVersion = PXInstanceHelper.IISVersion;
    updateConfig.ASPVersion = Environment.Version;
    updateConfig.RootFolder = PXInstanceHelper.RootFolder;
    updateConfig.SiteUrl = PXUrl.SiteUrlWithPath();
    updateConfig.MessageCallback = action;
    return updateConfig;
  }

  private static object GetUpdateArgs(System.Type argsType, PXUpgradeConfig config)
  {
    object instance = Activator.CreateInstance(argsType);
    PXUpgradeHelper.SetProperty(argsType, instance, "RootFolder", (object) config.RootFolder);
    PXUpgradeHelper.SetProperty(argsType, instance, "ZipPath", (object) config.ZipPath);
    PXUpgradeHelper.SetProperty(argsType, instance, "ZipFiles", (object) config.ZipFiles);
    PXUpgradeHelper.SetProperty(argsType, instance, "Parameters", (object) config.Parameters);
    PXUpgradeHelper.SetProperty(argsType, instance, "CurrentVersion", (object) config.CurrentVersion);
    PXUpgradeHelper.SetProperty(argsType, instance, "IsErp", (object) config.IsErp);
    PXUpgradeHelper.SetProperty(argsType, instance, "IISVersion", (object) config.IISVersion);
    PXUpgradeHelper.SetProperty(argsType, instance, "ASPVersion", (object) config.ASPVersion);
    PXUpgradeHelper.SetProperty(argsType, instance, "MessageCallback", (object) config.MessageCallback);
    PXUpgradeHelper.SetProperty(argsType, instance, "SiteUrl", (object) config.SiteUrl);
    return instance;
  }

  private static void SetProperty(System.Type argsType, object args, string property, object value)
  {
    if (value == null)
      return;
    PropertyInfo property1 = argsType.GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
    if (property1 == (PropertyInfo) null)
      throw new PXException("Unable to initialize the upgrade. Please try to upgrade your instance manually.");
    if (!property1.PropertyType.IsAssignableFrom(value.GetType()))
      throw new PXException("Unable to initialize the upgrade. Please try to upgrade your instance manually.");
    property1.SetValue(args, value, (object[]) null);
  }

  public static bool SiteUpgradeShouldBeLogged()
  {
    string path = Path.Combine(PXInstanceHelper.AppDataFolder, "UpgradeNotLogged.txt");
    bool flag = File.Exists(path);
    try
    {
      File.Delete(path);
    }
    catch
    {
    }
    return flag;
  }

  public static bool IsUpgradeSuccessful() => PXFileStatusWriter.GetUpdateStatus();
}
