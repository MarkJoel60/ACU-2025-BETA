// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProviderException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when a configuration provider error has occurred.
/// </summary>
[Serializable]
public class PXProviderException : PXException
{
  public PXProviderException()
  {
  }

  public PXProviderException(string message)
    : base(message)
  {
  }

  public PXProviderException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXProviderException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  public PXProviderException(Exception innerException, string format, params object[] args)
    : base(innerException, format, args)
  {
  }

  public PXProviderException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
