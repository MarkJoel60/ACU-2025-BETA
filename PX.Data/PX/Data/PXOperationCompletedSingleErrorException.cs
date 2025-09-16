// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOperationCompletedSingleErrorException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>An exception type that is used to indicate that the operation has completed with one error.</summary>
[Serializable]
public class PXOperationCompletedSingleErrorException : PXOperationCompletedException
{
  public PXOperationCompletedSingleErrorException(string message, Exception inner)
    : base(message, inner)
  {
  }

  public PXOperationCompletedSingleErrorException(Exception inner)
    : base(inner is PXOuterException ? ((PXOuterException) inner).GetFullMessage("\r\n") : inner.Message, inner)
  {
  }

  public PXOperationCompletedSingleErrorException(
    Exception inner,
    string format,
    params object[] args)
    : base(inner, format, args)
  {
  }

  public PXOperationCompletedSingleErrorException(string message)
    : base(message)
  {
  }

  public PXOperationCompletedSingleErrorException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXOperationCompletedSingleErrorException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
