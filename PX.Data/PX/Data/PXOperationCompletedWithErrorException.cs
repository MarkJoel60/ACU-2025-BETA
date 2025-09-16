// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOperationCompletedWithErrorException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// An exception type that is used to indicate that the operation has completed with one or multiple errors.
/// </summary>
[Serializable]
public class PXOperationCompletedWithErrorException : PXOperationCompletedException
{
  public PXOperationCompletedWithErrorException(string message, Exception inner)
    : base(message, inner)
  {
  }

  public PXOperationCompletedWithErrorException(
    Exception inner,
    string format,
    params object[] args)
    : base(inner, format, args)
  {
  }

  public PXOperationCompletedWithErrorException()
    : this("At least one item has not been processed.")
  {
  }

  public PXOperationCompletedWithErrorException(string message)
    : base(message)
  {
  }

  public PXOperationCompletedWithErrorException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXOperationCompletedWithErrorException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
