// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNotEnoughRightsException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNotEnoughRightsException : PXSetPropertyException
{
  private PXCacheRights rightsMissing;

  public PXNotEnoughRightsException(PXCacheRights rightsMissing)
    : base("You don't have enough rights on '{0}'.", (object) rightsMissing.ToString())
  {
    this.rightsMissing = rightsMissing;
  }

  public PXNotEnoughRightsException(PXCacheRights rightsMissing, string message)
    : base(message)
  {
    this.rightsMissing = rightsMissing;
  }

  public PXCacheRights RightsMissing => this.rightsMissing;

  public PXNotEnoughRightsException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXNotEnoughRightsException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXNotEnoughRightsException>(this, info);
    base.GetObjectData(info, context);
  }
}
