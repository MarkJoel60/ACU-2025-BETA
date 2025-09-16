// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AdjustedNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.AP;

[Serializable]
public class AdjustedNotFoundException : PXException
{
  public AdjustedNotFoundException()
    : base("'{0}' cannot be found in the system.", (object) "AP document")
  {
  }

  public AdjustedNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<AdjustedNotFoundException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<AdjustedNotFoundException>(this, info);
    base.GetObjectData(info, context);
  }
}
