// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldProcessingException
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
public class PXFieldProcessingException : PXSetPropertyException, IExceptionWithDescriptor
{
  public readonly string FieldName;

  protected PXFieldProcessingException(
    string fieldName,
    Exception inner,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : this(fieldName, new PX.Data.DacDescriptorGeneration.DacDescriptor?(), (IBqlTable) null, inner, errorLevel, format, args)
  {
  }

  public PXFieldProcessingException(
    string fieldName,
    Exception inner,
    PXErrorLevel errorLevel,
    params object[] args)
    : this(fieldName, new PX.Data.DacDescriptorGeneration.DacDescriptor?(), (IBqlTable) null, inner, errorLevel, "An error occurred during processing of the field {0}: {1}.", args)
  {
  }

  public PXFieldProcessingException(
    string fieldName,
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    Exception inner,
    PXErrorLevel errorLevel,
    string fieldNameInErrorMessage,
    string errorText)
    : this(fieldName, dacDescriptor, (IBqlTable) null, inner, errorLevel, "An error occurred during processing of the field {0}: {1}.", (object) fieldNameInErrorMessage, (object) errorText)
  {
    this.DacDescriptor = dacDescriptor;
  }

  public PXFieldProcessingException(
    string fieldName,
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    Exception inner,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : base(dacDescriptor, inner, row, errorLevel, format, args)
  {
    this.FieldName = fieldName;
    if (this._Message == null || this._Message.Length <= 2 || this._Message[this._Message.Length - 1] != '.' || this._Message[this._Message.Length - 2] != '.' || this._Message[this._Message.Length - 3] == '.')
      return;
    this._Message = this._Message.Substring(0, this._Message.Length - 1);
  }

  public PXFieldProcessingException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXFieldProcessingException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXFieldProcessingException>(this, info);
    base.GetObjectData(info, context);
  }
}
