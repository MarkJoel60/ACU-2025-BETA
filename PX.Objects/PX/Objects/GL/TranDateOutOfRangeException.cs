// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.TranDateOutOfRangeException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL;

public class TranDateOutOfRangeException : PXSetPropertyException
{
  public TranDateOutOfRangeException(DateTime? date, string organizationCD)
    : base("The financial period that corresponds to the {0} date does not exist in the {1} company.", new object[2]
    {
      (object) date?.ToShortDateString(),
      (object) organizationCD
    })
  {
  }

  public TranDateOutOfRangeException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<TranDateOutOfRangeException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<TranDateOutOfRangeException>(this, info);
    base.GetObjectData(info, context);
  }
}
