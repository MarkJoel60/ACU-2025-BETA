// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectToLastUrlException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class PXRedirectToLastUrlException : PXRedirectRequiredException
{
  public PXRedirectToLastUrlException(
    PXGraph defaultGraph,
    PXBaseRedirectException.WindowMode windowMode)
    : base((string) PXContext.Session["LastUrl"], defaultGraph, windowMode, (string) null)
  {
  }

  public PXRedirectToLastUrlException(PXGraph defaultGraph)
    : base((string) PXContext.Session["LastUrl"], defaultGraph, (string) null)
  {
  }

  public PXRedirectToLastUrlException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
