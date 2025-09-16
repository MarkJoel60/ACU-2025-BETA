// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPasswordRecoveryExpiredException
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
public class PXPasswordRecoveryExpiredException : PXException
{
  public PXPasswordRecoveryExpiredException()
  {
  }

  public PXPasswordRecoveryExpiredException(string message)
    : base(message)
  {
  }

  public PXPasswordRecoveryExpiredException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXPasswordRecoveryExpiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public PXPasswordRecoveryExpiredException(
    Exception innerException,
    string format,
    params object[] args)
    : base(innerException, format, args)
  {
  }

  public PXPasswordRecoveryExpiredException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
