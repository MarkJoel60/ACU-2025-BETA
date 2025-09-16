// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetPropertyException`1
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
public class PXSetPropertyException<Field> : PXSetPropertyException, IExceptionWithDescriptor where Field : IBqlField
{
  public PXSetPropertyException(PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor, string message)
    : base(dacDescriptor, message)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(string message)
    : base(message)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string message,
    PXErrorLevel errorLevel)
    : base(dacDescriptor, row, message, errorLevel)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(string message, PXErrorLevel errorLevel)
    : base(message, errorLevel)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string format,
    params object[] args)
    : base(dacDescriptor, row, format, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(string format, params object[] args)
    : base(format, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    string format,
    PXErrorLevel errorLevel,
    params object[] args)
    : base(dacDescriptor, row, format, errorLevel, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(string format, PXErrorLevel errorLevel, params object[] args)
    : base(format, errorLevel, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    Exception inner,
    IBqlTable row,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : base(dacDescriptor, inner, row, errorLevel, format, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(
    Exception inner,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : base(inner, errorLevel, format, args)
  {
    this._MapErrorTo = typeof (Field).Name;
  }

  public PXSetPropertyException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetPropertyException<Field>>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSetPropertyException<Field>>(this, info);
    base.GetObjectData(info, context);
  }
}
