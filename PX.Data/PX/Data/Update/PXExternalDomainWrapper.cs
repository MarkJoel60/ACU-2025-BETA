// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXExternalDomainWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;

#nullable disable
namespace PX.Data.Update;

[Serializable]
[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
internal class PXExternalDomainWrapper : MarshalByRefObject
{
  private const string DOMAINANME = "UpdateDomain";

  public static void DoWithinAnotherDomain(System.Action<object> action, object args)
  {
    PXExternalDomainWrapper.DoWithinAnotherDomain((PXExternalDomainWrapper.DomainSetupArgs) null, action, args);
  }

  public static void DoWithinAnotherDomain(
    PXExternalDomainWrapper.DomainSetupArgs setup,
    System.Action<object> action,
    object args)
  {
    AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
    PermissionSet grantSet = new PermissionSet(PermissionState.Unrestricted);
    AppDomainSetup info = new AppDomainSetup();
    info.ApplicationName = "UpdateDomain";
    info.ApplicationBase = setupInformation.ApplicationBase;
    info.PrivateBinPath = setupInformation.PrivateBinPath;
    info.PrivateBinPathProbe = setupInformation.PrivateBinPathProbe;
    info.DisallowBindingRedirects = false;
    info.DisallowCodeDownload = true;
    info.ShadowCopyFiles = setupInformation.ShadowCopyFiles;
    info.ShadowCopyDirectories = setupInformation.ShadowCopyDirectories;
    info.ConfigurationFile = setupInformation.ConfigurationFile;
    info.ApplicationTrust = setupInformation.ApplicationTrust;
    info.CachePath = PXInstanceHelper.Impersonation ? PXInstanceHelper.GetTempFolder("UpdateDomain") : setupInformation.CachePath;
    AppDomain domain = (AppDomain) null;
    try
    {
      domain = AppDomain.CreateDomain("UpdateDomain", (Evidence) null, info, grantSet);
      ((PXExternalDomainWrapper.DomainProxy) domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof (PXExternalDomainWrapper.DomainProxy).FullName)).Execute(new PXExternalDomainWrapper.ProcessArgs(PXInstanceHelper.ScopeUser, WindowsIdentity.GetCurrent(), action, args));
    }
    catch
    {
      if (domain != null)
        AppDomain.Unload(domain);
      throw;
    }
  }

  [Serializable]
  public class DomainSetupArgs
  {
  }

  [Serializable]
  public class ProcessArgs
  {
    public readonly string Username;
    public readonly WindowsIdentity Identity;
    public readonly System.Action<object> Handler;
    public readonly object Args;

    public ProcessArgs(
      string username,
      WindowsIdentity identity,
      System.Action<object> handler,
      object args)
    {
      this.Username = username;
      this.Identity = identity;
      this.Handler = handler;
      this.Args = args;
    }
  }

  [Serializable]
  public class DomainProxy : MarshalByRefObject
  {
    public void Execute(PXExternalDomainWrapper.ProcessArgs args)
    {
      new Thread(new ParameterizedThreadStart(PXExternalDomainWrapper.DomainProxy.ThreadStart)).Start((object) args);
    }

    private static void ThreadStart(object o)
    {
      try
      {
        PXExternalDomainWrapper.ProcessArgs processArgs = (PXExternalDomainWrapper.ProcessArgs) o;
        using (new PXImpersonationContext(processArgs.Username, processArgs.Identity))
          processArgs.Handler(processArgs.Args);
      }
      catch
      {
      }
      finally
      {
        AppDomain.Unload(AppDomain.CurrentDomain);
      }
    }
  }
}
