// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUndefinedCompanyException
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
public class PXUndefinedCompanyException : Exception
{
  public PXUndefinedCompanyException()
    : base("A proper company ID cannot be determined for the request.")
  {
  }

  public PXUndefinedCompanyException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXUndefinedCompanyException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXUndefinedCompanyException>(this, info);
    base.GetObjectData(info, context);
  }
}
