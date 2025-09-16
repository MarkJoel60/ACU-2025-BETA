// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectWithReportException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>The exception that opens two pages: the specified report in a new window, and the specified application page in the same window.</summary>
public class PXRedirectWithReportException : PXRedirectRequiredException
{
  protected PXReportRequiredException _Report;

  public PXReportRequiredException Report => this._Report;

  public PXRedirectWithReportException(PXGraph graph, PXReportRequiredException e, string message)
    : base(graph, message)
  {
    this._Report = e;
  }

  public PXRedirectWithReportException(
    PXGraph graph,
    PXReportRequiredException e,
    string format,
    params object[] args)
    : base(graph, format, args)
  {
    this._Report = e;
  }

  public PXRedirectWithReportException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXRedirectWithReportException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRedirectWithReportException>(this, info);
    base.GetObjectData(info, context);
  }
}
