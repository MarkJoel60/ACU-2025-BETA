// Decompiled with JetBrains decompiler
// Type: PX.Data.PXInvalidOperationException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.DacDescriptorGeneration;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when a method call is invalid for the object's current state.
/// </summary>
[Serializable]
public class PXInvalidOperationException : PXException, IExceptionWithDescriptor
{
  public PXInvalidOperationException(PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor)
    : base(dacDescriptor, "The requested operation cannot be performed.")
  {
    this.SetHResult();
  }

  public PXInvalidOperationException()
    : base("The requested operation cannot be performed.")
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor, string message)
    : base(dacDescriptor, message)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(string message)
    : base(message)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    string format,
    params object[] args)
    : base(dacDescriptor, format, args)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(string format, params object[] args)
    : base(format, args)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    string message,
    Exception innerException)
    : base(dacDescriptor, message, innerException)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(string message, Exception innerException)
    : base(message, innerException)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    Exception innerException,
    string format,
    params object[] args)
    : base(dacDescriptor, innerException, format, args)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(Exception innerException, string format, params object[] args)
    : base(innerException, format, args)
  {
    this.SetHResult();
  }

  public PXInvalidOperationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.SetHResult();
  }

  private void SetHResult() => this.HResult = -2146233079;
}
