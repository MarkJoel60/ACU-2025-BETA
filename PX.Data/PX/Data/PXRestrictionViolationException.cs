// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRestrictionViolationException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXRestrictionViolationException : PXOverridableException
{
  public readonly int Index;

  public PXRestrictionViolationException(string message, object[] keys, int index)
    : base(message, keys)
  {
    this.Index = index;
  }

  public PXRestrictionViolationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXRestrictionViolationException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRestrictionViolationException>(this, info);
    base.GetObjectData(info, context);
  }
}
