// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLogoutRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

internal class PXLogoutRedirectException : PXBaseRedirectException
{
  private const string DefaultTarget = "_top";

  public string Url { get; private set; }

  public string Target { get; set; }

  public PXLogoutRedirectException(
    PXForceLogOutException exception,
    string bathPath,
    string message,
    string target = null)
    : this(exception, bathPath, message, false, target)
  {
  }

  public PXLogoutRedirectException(
    PXForceLogOutException exception,
    string bathPath,
    string message,
    bool repaintControls,
    string target = null)
    : base(message, repaintControls)
  {
    string rawUrl = exception.RawUrl;
    this.Url = rawUrl != null ? rawUrl.ToRelativeUrl(bathPath) : (string) null;
    this.Target = target ?? "_top";
  }

  public PXLogoutRedirectException(
    PXForceLogOutException exception,
    string bathPath,
    string message,
    params object[] args)
    : base(message, args)
  {
    string rawUrl = exception.RawUrl;
    this.Url = rawUrl != null ? rawUrl.ToRelativeUrl(bathPath) : (string) null;
    this.Target = "_top";
  }

  public PXLogoutRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
