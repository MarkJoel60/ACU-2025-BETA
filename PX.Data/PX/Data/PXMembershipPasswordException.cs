// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMembershipPasswordException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when a password cannot be retrieved from the password store.
/// </summary>
[Serializable]
public class PXMembershipPasswordException : PXException
{
  public PXMembershipPasswordException()
  {
  }

  public PXMembershipPasswordException(string message)
    : base(message)
  {
  }

  public PXMembershipPasswordException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXMembershipPasswordException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  public PXMembershipPasswordException(
    Exception innerException,
    string format,
    params object[] args)
    : base(innerException, format, args)
  {
  }

  public PXMembershipPasswordException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
