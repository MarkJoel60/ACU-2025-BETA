// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetPropertyException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DacDescriptorGeneration;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXSetPropertyException : PXOverridableException, IExceptionWithDescriptor
{
  protected PXErrorLevel _ErrorLevel = PXErrorLevel.Error;
  protected object _ErrorValue;

  public object ErrorValue
  {
    get => this._ErrorValue;
    set => this._ErrorValue = value;
  }

  public PXErrorLevel ErrorLevel => this._ErrorLevel;

  public override string Message => this.MessageNoNumber;

  internal IBqlTable Row { get; set; }

  public override void SetMessage(string message)
  {
    if (this.MessagePrefix != null)
    {
      string str = this.MessagePrefix + ":";
      if (message.IndexOf(str) != -1)
        message = message.Substring(message.IndexOf(str) + str.Length);
    }
    this._Message = PXMessages.LocalizeNoPrefix(message);
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor, string message)
    : this(message)
  {
    this.DacDescriptor = dacDescriptor;
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(string message)
    : base(message)
  {
  }

  public PXSetPropertyException(PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor, IBqlTable row, string message)
    : this(row, message)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXSetPropertyException(IBqlTable row, string message)
    : this(message)
  {
    this.Row = row;
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(string message, PXErrorLevel errorLevel)
    : this(message)
  {
    this._ErrorLevel = errorLevel;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string message,
    PXErrorLevel errorLevel)
    : this(row, message, errorLevel)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXSetPropertyException(IBqlTable row, string message, PXErrorLevel errorLevel)
    : this(message, errorLevel)
  {
    this.Row = row;
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string format,
    params object[] args)
    : this(row, format, args)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXSetPropertyException(IBqlTable row, string format, params object[] args)
    : this(format, args)
  {
    this.Row = row;
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(string format, PXErrorLevel errorLevel, params object[] args)
    : base(format, args)
  {
    this._ErrorLevel = errorLevel;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string format,
    PXErrorLevel errorLevel,
    params object[] args)
    : this(row, format, errorLevel, args)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXSetPropertyException(
    IBqlTable row,
    string format,
    PXErrorLevel errorLevel,
    params object[] args)
    : this(format, errorLevel, args)
  {
    this.Row = row;
  }

  [Obsolete("Please use constructor with additional IBqlTable row argument")]
  public PXSetPropertyException(
    Exception inner,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : base(inner, format, args)
  {
    this._ErrorLevel = errorLevel;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    Exception inner,
    IBqlTable row,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : this(inner, row, errorLevel, format, args)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXSetPropertyException(
    Exception inner,
    IBqlTable row,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : this(inner, errorLevel, format, args)
  {
    this.Row = row;
  }

  public PXSetPropertyException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetPropertyException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSetPropertyException>(this, info);
    base.GetObjectData(info, context);
  }
}
