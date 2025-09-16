// Decompiled with JetBrains decompiler
// Type: PX.Data.PXForceLogOutException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Export.Authentication;
using System;
using System.Runtime.Serialization;
using System.Web;

#nullable enable
namespace PX.Data;

/// <exclude />
public class PXForceLogOutException : Exception
{
  private static 
  #nullable disable
  string _baseLoginUrl;

  public string Url { get; private set; }

  public string RawUrl { get; private set; }

  public string Title { get; private set; }

  public PXForceLogOutException(PXLogOutReason reason)
    : base(PXForceLogOutException.GetRedirectMessage(reason))
  {
    this.Title = PXForceLogOutException.GetTitle(reason);
    this.RawUrl = PXForceLogOutException.GetRawLoginUrl(reason);
    this.Url = PXForceLogOutException.GetSessionLoginUrl(reason);
  }

  public PXForceLogOutException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXForceLogOutException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXForceLogOutException>(this, info);
    base.GetObjectData(info, context);
  }

  private static string ReasonToString(PXLogOutReason reason)
  {
    if (reason == PXLogOutReason.UsersExceeded)
      return "users";
    return reason == PXLogOutReason.CompaniesExceeded ? "tenants" : reason.ToString();
  }

  private static string GetRawLoginUrl(PXLogOutReason reason)
  {
    string baseLoginUrl = PXForceLogOutException._baseLoginUrl;
    return reason == PXLogOutReason.UserDisabled || reason == PXLogOutReason.AccessDisabled || reason == PXLogOutReason.SnapshotRestored || reason == PXLogOutReason.ConcurrentLoginsExceeded ? $"{baseLoginUrl}&message={HttpUtility.UrlEncode(PXForceLogOutException.GetTitle(reason))}" : $"{baseLoginUrl}&licenseexceeded={HttpUtility.UrlEncode(PXForceLogOutException.ReasonToString(reason))}";
  }

  private static string GetSessionLoginUrl(PXLogOutReason reason)
  {
    string rawLoginUrl = PXForceLogOutException.GetRawLoginUrl(reason);
    return HttpContext.Current == null ? rawLoginUrl : PXSessionStateStore.GetSessionUrl(HttpContext.Current, rawLoginUrl);
  }

  private static string GetRedirectMessage(PXLogOutReason reason)
  {
    return $"Refresh|{PXForceLogOutException.GetSessionLoginUrl(reason)}|";
  }

  private static string GetTitle(PXLogOutReason reason)
  {
    switch (reason)
    {
      case PXLogOutReason.UsersExceeded:
      case PXLogOutReason.CompaniesExceeded:
        return PXMessages.LocalizeFormatNoPrefix("You have been logged out due to exceeding the number of {0} in the system.", (object) PXForceLogOutException.ReasonToString(reason));
      case PXLogOutReason.ConcurrentLoginsExceeded:
        return "You have been logged out because the number of concurrent logins specified for your user account on the Users (SM201010) form has been exceeded.";
      case PXLogOutReason.SnapshotRestored:
        return "You have been automatically signed out due to the successful restoration of a snapshot initiated by a user.";
      case PXLogOutReason.UserDisabled:
        return "You have been logged out because your account has been disabled. Please contact your system administrator.";
      case PXLogOutReason.AccessDisabled:
        return "You have been logged out because access to the system has been disabled. Please contact your system administrator.";
      default:
        return string.Empty;
    }
  }

  internal static PXForceLogOutException Find(Exception exception)
  {
    if (exception == null)
      return (PXForceLogOutException) null;
    return exception is PXForceLogOutException forceLogOutException ? forceLogOutException : PXForceLogOutException.Find(exception.InnerException);
  }

  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (ls =>
      {
        FormsAuthenticationOptions authenticationOptions = ResolutionExtensions.Resolve<IOptions<FormsAuthenticationOptions>>((IComponentContext) ls).Value;
        PXForceLogOutException._baseLoginUrl = $"{authenticationOptions.LoginUrl}?returnUrl={VirtualPathUtility.ToAbsolute(authenticationOptions.DefaultUrl)}";
      }));
    }
  }
}
