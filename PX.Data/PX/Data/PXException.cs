// Decompiled with JetBrains decompiler
// Type: PX.Data.PXException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DacDescriptorGeneration;
using System;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// An Acumatica ERP-specific exception, which is derived from <see cref="T:System.Exception" />.
/// </summary>
[Serializable]
public class PXException : Exception, IExceptionWithDescriptor
{
  private static readonly System.Action<Exception> _preserveStackTrace;
  protected uint _ExceptionNumber;
  protected string _Message;
  protected string _MessagePrefix;

  static PXException()
  {
    try
    {
      MethodInfo method = typeof (Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);
      if (method != (MethodInfo) null)
        PXException._preserveStackTrace = Delegate.CreateDelegate(typeof (System.Action<Exception>), method) as System.Action<Exception>;
      if (PXException._preserveStackTrace != null)
        return;
      InvalidOperationException preserveStackTraceNotFoundException = new InvalidOperationException("Failed to obtain the Exception.InternalPreserveStackTrace method");
      PXTrace.WriteError((Exception) preserveStackTraceNotFoundException);
      PXException._preserveStackTrace = (System.Action<Exception>) (exception =>
      {
        throw preserveStackTraceNotFoundException;
      });
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      PXException._preserveStackTrace = (System.Action<Exception>) (exception =>
      {
        throw ex;
      });
    }
  }

  public PXException(PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor)
    : this()
  {
    this.DacDescriptor = descriptor;
  }

  public PXException() => this._Message = string.Empty;

  public static Exception ExtractInner(Exception exception)
  {
    if (exception.InnerException != null)
    {
      PXException._preserveStackTrace(exception.InnerException);
      return exception.InnerException;
    }
    PXException._preserveStackTrace(exception);
    return exception;
  }

  public static Exception PreserveStack(Exception exception)
  {
    PXException._preserveStackTrace(exception);
    return exception;
  }

  public PXException(PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor, string message)
    : this(message)
  {
    this.DacDescriptor = descriptor;
  }

  public PXException(string message)
  {
    this._Message = PXMessages.Localize(message, out this._MessagePrefix);
  }

  public PXException(PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor, string format, params object[] args)
    : this(format, args)
  {
    this.DacDescriptor = descriptor;
  }

  public PXException(string format, params object[] args)
  {
    this._Message = PXMessages.LocalizeFormat(format, out this._MessagePrefix, args);
  }

  public PXException(PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor, string message, Exception innerException)
    : this(message, innerException)
  {
    this.DacDescriptor = descriptor;
  }

  public PXException(string message, Exception innerException)
    : base("", innerException)
  {
    this._Message = PXMessages.Localize(message, out this._MessagePrefix);
  }

  public PXException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor,
    Exception innerException,
    string format,
    params object[] args)
    : this(innerException, format, args)
  {
    this.DacDescriptor = descriptor;
  }

  public PXException(Exception innerException, string format, params object[] args)
    : base("", innerException)
  {
    this._Message = PXMessages.LocalizeFormat(format, out this._MessagePrefix, args);
  }

  public PXException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._Message = info.GetString(nameof (_Message));
    this._MessagePrefix = info.GetString(nameof (_MessagePrefix));
    string str = info.GetString(nameof (DacDescriptor));
    this.DacDescriptor = Str.IsNullOrWhiteSpace(str) ? new PX.Data.DacDescriptorGeneration.DacDescriptor?() : new PX.Data.DacDescriptorGeneration.DacDescriptor?(new PX.Data.DacDescriptorGeneration.DacDescriptor(str));
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("_Message", (object) this._Message);
    info.AddValue("_MessagePrefix", (object) this._MessagePrefix);
    SerializationInfo serializationInfo = info;
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor = this.DacDescriptor;
    ref PX.Data.DacDescriptorGeneration.DacDescriptor? local = ref dacDescriptor;
    string str = local.HasValue ? local.GetValueOrDefault().Value : (string) null;
    System.Type type = typeof (string);
    serializationInfo.AddValue("DacDescriptor", (object) str, type);
    base.GetObjectData(info, context);
  }

  /// <summary>
  /// Gets a message that describes the current exception with message number and prefix (if defined)
  /// </summary>
  public override string Message => this.GetLocalizedMessage(this._Message);

  protected string GetLocalizedMessage(string message)
  {
    return this._MessagePrefix == null ? message : $"{this._MessagePrefix}: {message}";
  }

  /// <summary>
  /// Gets a message that describes the current exception with prefix (if defined), but without message number
  /// </summary>
  public virtual string MessageNoNumber
  {
    get => this._MessagePrefix != null ? $"{this._MessagePrefix}: {this._Message}" : this._Message;
  }

  /// <summary>
  /// Gets a message that describes the current exception without message prefix and number
  /// </summary>
  public virtual string MessageNoPrefix => this._Message;

  /// <summary>Gets exception message number</summary>
  public virtual uint ExceptionNumber => this._ExceptionNumber;

  /// <summary>Gets exception message prefix</summary>
  public virtual string MessagePrefix => this._MessagePrefix;

  internal virtual bool HasBeenLogged { get; set; }

  protected string LocalizedMessage => this._Message;

  /// <summary>
  /// The optional DAC descriptor of the DAC record related to the exception. Can be null.
  /// </summary>
  /// <value>The DAC descriptor.</value>
  public PX.Data.DacDescriptorGeneration.DacDescriptor? DacDescriptor { get; protected set; }
}
