// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectToUrlException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Opens a webpage with the specified external URL in a new window. This exception is also used for opening an inquiry page that is loaded into the same window
/// by default.
/// </summary>
/// <example>
/// <code title="Example" description="" lang="CS">
/// throw new PXRedirectToUrlException(externalLink, PXBaseRedirectException.WindowMode.New, string.Empty);</code>
/// </example>
public class PXRedirectToUrlException : PXBaseRedirectException
{
  protected bool _SuppressFrameset;
  protected string _Url;
  private string _targetFrame;

  public string Url => this._Url;

  public bool SuppressFrameset
  {
    get => this._SuppressFrameset;
    set => this._SuppressFrameset = value;
  }

  public string TargetFrame
  {
    get => this._targetFrame;
    set
    {
      if (this.SuppressFrameset)
        throw new InvalidOperationException("The TargetFrame property cannot be set when the SuppressFrameset value is True.");
      this._targetFrame = value;
    }
  }

  public PXRedirectToUrlException(string url, string message)
    : base(message)
  {
    this._Url = url;
  }

  public PXRedirectToUrlException(string url, string format, params object[] args)
    : base(format, args)
  {
    this._Url = url;
  }

  public PXRedirectToUrlException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string message)
    : this(url, newWindow, message, false)
  {
  }

  public PXRedirectToUrlException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string message,
    bool repaintControls)
    : base(message, repaintControls)
  {
    this._Url = url;
    this.Mode = newWindow;
  }

  public PXRedirectToUrlException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string format,
    params object[] args)
    : base(format, args)
  {
    this._Url = url;
    this.Mode = newWindow;
  }

  public PXRedirectToUrlException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    bool suppressFrameset,
    string message)
    : this(url, newWindow, message)
  {
    this._SuppressFrameset = suppressFrameset;
  }

  public PXRedirectToUrlException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    bool suppressFrameset,
    string format,
    params object[] args)
    : this(url, newWindow, format, args)
  {
    this._SuppressFrameset = suppressFrameset;
  }

  public PXRedirectToUrlException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXRedirectToUrlException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRedirectToUrlException>(this, info);
    base.GetObjectData(info, context);
  }
}
