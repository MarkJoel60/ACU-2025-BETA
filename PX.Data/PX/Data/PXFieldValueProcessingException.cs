// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldValueProcessingException
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
public class PXFieldValueProcessingException : PXFieldProcessingException, IExceptionWithDescriptor
{
  public PXFieldValueProcessingException(
    string fieldName,
    Exception inner,
    PXErrorLevel errorLevel,
    params object[] args)
    : base(fieldName, new PX.Data.DacDescriptorGeneration.DacDescriptor?(), (IBqlTable) null, inner, errorLevel, "An error occurred during processing of the field {0} value {1} {2}.", args)
  {
  }

  public PXFieldValueProcessingException(
    string fieldName,
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    Exception inner,
    PXErrorLevel errorLevel,
    string fieldNameInErrorMessage,
    object fieldValue,
    string errorText)
    : this(fieldName, dacDescriptor, (IBqlTable) null, inner, errorLevel, fieldNameInErrorMessage, fieldValue, errorText)
  {
  }

  public PXFieldValueProcessingException(
    string fieldName,
    PX.Data.DacDescriptorGeneration.DacDescriptor? dacDescriptor,
    IBqlTable row,
    Exception inner,
    PXErrorLevel errorLevel,
    string fieldNameInErrorMessage,
    object fieldValue,
    string errorText)
    : base(fieldName, dacDescriptor, row, inner, errorLevel, "An error occurred during processing of the field {0} value {1} {2}.", (object) fieldNameInErrorMessage, fieldValue, (object) errorText)
  {
  }

  public PXFieldValueProcessingException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXFieldValueProcessingException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXFieldValueProcessingException>(this, info);
    base.GetObjectData(info, context);
  }
}
