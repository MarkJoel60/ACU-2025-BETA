// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRefreshException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class PXRefreshException : PXBaseRedirectException
{
  public PXRefreshException()
    : base("Refresh")
  {
  }

  public PXRefreshException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXRefreshException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRefreshException>(this, info);
    base.GetObjectData(info, context);
  }
}
