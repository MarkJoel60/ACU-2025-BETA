// Decompiled with JetBrains decompiler
// Type: PX.Data.PXObjectDisposedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>Derived from PXException.</summary>
[Serializable]
public class PXObjectDisposedException : PXInvalidOperationException
{
  private string strObjectName;

  public PXObjectDisposedException()
    : base("The object is disposed.")
  {
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string objectName)
    : base("The object is disposed.")
  {
    this.strObjectName = objectName;
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string objectName, string message)
    : base(message)
  {
    this.strObjectName = objectName;
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string format, params object[] args)
    : base(format, args)
  {
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string objectName, string format, params object[] args)
    : base(format, args)
  {
    this.strObjectName = objectName;
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string message, Exception innerException)
    : base(message, innerException)
  {
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(string objectName, string message, Exception innerException)
    : base(message, innerException)
  {
    this.strObjectName = objectName;
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(
    Exception innerException,
    string objectName,
    string format,
    params object[] args)
    : base(innerException, format, args)
  {
    this.strObjectName = objectName;
    this.HResult = -2146232798;
  }

  public PXObjectDisposedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2146232798;
    ReflectionSerializer.RestoreObjectProps<PXObjectDisposedException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXObjectDisposedException>(this, info);
    base.GetObjectData(info, context);
  }

  public override string Message
  {
    get
    {
      return this.strObjectName == null || this.strObjectName == string.Empty ? base.Message : $"{base.Message}\nObject name: {this.strObjectName}";
    }
  }

  public override string MessageNoNumber
  {
    get
    {
      return this.strObjectName == null || this.strObjectName == string.Empty ? base.MessageNoNumber : $"{base.MessageNoNumber}\nObject name: {this.strObjectName}";
    }
  }

  public virtual string ObjectName => this.strObjectName;
}
