// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PXFinPeriodException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL;

public class PXFinPeriodException : PXException
{
  public PXFinPeriodException()
    : base("Financial Period cannot be found in the system.")
  {
  }

  public PXFinPeriodException(string message)
    : base(message)
  {
  }

  public PXFinPeriodException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXFinPeriodException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXFinPeriodException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXFinPeriodException>(this, info);
    base.GetObjectData(info, context);
  }
}
