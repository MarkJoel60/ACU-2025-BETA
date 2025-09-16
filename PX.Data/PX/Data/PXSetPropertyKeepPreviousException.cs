// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetPropertyKeepPreviousException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSetPropertyKeepPreviousException : PXSetPropertyException
{
  public PXSetPropertyKeepPreviousException(string message)
    : base(message)
  {
  }

  public PXSetPropertyKeepPreviousException(string message, PXErrorLevel errorLevel)
    : base(message, errorLevel)
  {
  }

  public PXSetPropertyKeepPreviousException(
    string format,
    PXErrorLevel errorLevel,
    params object[] args)
    : base(format, errorLevel, args)
  {
  }

  public PXSetPropertyKeepPreviousException(
    Exception inner,
    PXErrorLevel errorLevel,
    string format,
    params object[] args)
    : base(inner, errorLevel, format, args)
  {
  }

  public PXSetPropertyKeepPreviousException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetPropertyKeepPreviousException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSetPropertyKeepPreviousException>(this, info);
    base.GetObjectData(info, context);
  }
}
