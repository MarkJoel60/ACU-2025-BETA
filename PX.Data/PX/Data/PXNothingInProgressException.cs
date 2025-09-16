// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNothingInProgressException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
[Serializable]
public class PXNothingInProgressException : PXOverridableException
{
  public PXNothingInProgressException(string message, Exception inner)
    : base(message, inner)
  {
  }

  public PXNothingInProgressException(Exception inner, string format, params object[] args)
    : base(inner, format, args)
  {
  }

  public PXNothingInProgressException(string message)
    : base(message)
  {
  }

  public PXNothingInProgressException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXNothingInProgressException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
