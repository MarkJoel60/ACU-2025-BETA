// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowPersistedException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXRowPersistedException : PXOverridableException
{
  public readonly string Name;
  public readonly object Value;

  public PXRowPersistedException(string name, object value, string message)
    : base(message)
  {
    this.Name = name;
    this.Value = value;
  }

  public PXRowPersistedException(string name, object value, string format, params object[] args)
    : base(format, args)
  {
    this.Name = name;
    this.Value = value;
  }

  public PXRowPersistedException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXRowPersistedException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRowPersistedException>(this, info);
    base.GetObjectData(info, context);
  }
}
