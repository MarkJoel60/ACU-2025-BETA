// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProviderEmptyConnectionStringException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXProviderException. The exception that is thrown when a configuration provider error has occurred because of empty connection string.
/// </summary>
[Serializable]
public class PXProviderEmptyConnectionStringException : PXException
{
  public PXProviderEmptyConnectionStringException()
    : base("The connection string cannot be empty.")
  {
  }

  public PXProviderEmptyConnectionStringException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
