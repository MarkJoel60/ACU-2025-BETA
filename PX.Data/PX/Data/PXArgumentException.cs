// Decompiled with JetBrains decompiler
// Type: PX.Data.PXArgumentException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when one of the arguments provided to a method is not valid.
/// </summary>
[Serializable]
public class PXArgumentException : PXException
{
  private string strParamName;

  public PXArgumentException()
    : base("An invalid argument has been specified.")
  {
    this.HResult = -2147024809;
  }

  public PXArgumentException(string paramName)
    : base("An invalid argument has been specified.")
  {
    this.HResult = -2147024809;
    this.strParamName = paramName;
  }

  public PXArgumentException(string paramName, string message)
    : base(message)
  {
    this.HResult = -2147024809;
    this.strParamName = paramName;
  }

  public PXArgumentException(string format, params object[] args)
    : base(format, args)
  {
    this.HResult = -2147024809;
  }

  public PXArgumentException(string paramName, string format, params object[] args)
    : base(format, args)
  {
    this.HResult = -2147024809;
    this.strParamName = paramName;
  }

  public PXArgumentException(string message, Exception innerException)
    : base(message, innerException)
  {
    this.HResult = -2147024809;
  }

  public PXArgumentException(string paramName, string message, Exception innerException)
    : base(message, innerException)
  {
    this.HResult = -2147024809;
    this.strParamName = paramName;
  }

  public PXArgumentException(Exception innerException, string format, params object[] args)
    : base(innerException, format, args)
  {
    this.HResult = -2147024809;
  }

  public PXArgumentException(
    Exception innerException,
    string paramName,
    string format,
    params object[] args)
    : base(innerException, format, args)
  {
    this.HResult = -2147024809;
    this.strParamName = paramName;
  }

  public PXArgumentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXArgumentException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXArgumentException>(this, info);
    base.GetObjectData(info, context);
  }

  public override string Message
  {
    get
    {
      return this.strParamName == null || this.strParamName == string.Empty ? base.Message : $"{base.Message}\nParameter name: {this.strParamName}";
    }
  }

  public override string MessageNoNumber
  {
    get
    {
      return this.strParamName == null || this.strParamName == string.Empty ? base.MessageNoNumber : $"{base.MessageNoNumber}\nParameter name: {this.strParamName}";
    }
  }

  public virtual string ParamName => this.strParamName;
}
