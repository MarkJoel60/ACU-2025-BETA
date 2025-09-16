// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectWithUrlException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXRedirectWithUrlException : PXRedirectRequiredException
{
  protected string _ExtraUrl;

  public string ExtraUrl => this._ExtraUrl;

  public PXRedirectWithUrlException(PXGraph graph, string url, string message)
    : base(graph, message)
  {
    this._ExtraUrl = url;
  }

  public PXRedirectWithUrlException(
    PXGraph graph,
    string url,
    string format,
    params object[] args)
    : base(graph, format, args)
  {
    this._ExtraUrl = url;
  }
}
