// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.InvalidRequestException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Api.Services;

public class InvalidRequestException : Exception
{
  public InvalidRequestException(string message)
    : base(message)
  {
  }

  public InvalidRequestException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
  }
}
