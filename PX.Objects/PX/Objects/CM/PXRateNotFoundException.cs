// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXRateNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CM;

public class PXRateNotFoundException : PXException
{
  public PXRateNotFoundException()
    : base("Currency Rate is not defined.")
  {
  }

  public PXRateNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXRateNotFoundException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRateNotFoundException>(this, info);
    base.GetObjectData(info, context);
  }
}
