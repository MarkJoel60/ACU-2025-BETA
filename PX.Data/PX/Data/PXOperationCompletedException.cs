// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOperationCompletedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// An exception type that is used to indicate that the operation has completed successfully.
/// </summary>
/// <remarks>The long-running operations handle this exception and can display a message on the form.</remarks>
[Serializable]
public class PXOperationCompletedException : PXOverridableException
{
  public PXOperationCompletedException(string message, Exception inner)
    : base(message, inner)
  {
  }

  public PXOperationCompletedException(Exception inner, string format, params object[] args)
    : base(inner, format, args)
  {
  }

  public PXOperationCompletedException(string message)
    : base(message)
  {
  }

  public PXOperationCompletedException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXOperationCompletedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
