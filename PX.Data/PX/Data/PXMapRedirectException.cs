// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMapRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class PXMapRedirectException : PXBaseUnhandledRedirectException
{
  public string Url { get; set; }

  public bool SuppressFrameset { get; set; }

  public PXMapRedirectException(string url, string message)
    : base(message)
  {
    this.Url = url;
  }

  public PXMapRedirectException(string url, string format, params object[] args)
    : base(format, args)
  {
    this.Url = url;
  }

  public PXMapRedirectException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string message)
    : this(url, newWindow, message, false)
  {
  }

  public PXMapRedirectException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string message,
    bool repaintControls)
    : base(message, repaintControls)
  {
    this.Url = url;
    this.Mode = newWindow;
  }

  public PXMapRedirectException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    string format,
    params object[] args)
    : base(format, args)
  {
    this.Url = url;
    this.Mode = newWindow;
  }

  public PXMapRedirectException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    bool suppressFrameset,
    string message)
    : this(url, newWindow, message)
  {
    this.SuppressFrameset = suppressFrameset;
  }

  public PXMapRedirectException(
    string url,
    PXBaseRedirectException.WindowMode newWindow,
    bool suppressFrameset,
    string format,
    params object[] args)
    : this(url, newWindow, format, args)
  {
    this.SuppressFrameset = suppressFrameset;
  }

  public PXMapRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
