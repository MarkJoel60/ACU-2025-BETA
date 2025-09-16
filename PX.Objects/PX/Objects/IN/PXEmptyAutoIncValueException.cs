// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXEmptyAutoIncValueException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN;

public class PXEmptyAutoIncValueException : PXException
{
  public PXEmptyAutoIncValueException(string Source)
    : base("Auto-Incremental value is not set in {0}.", new object[1]
    {
      (object) Source
    })
  {
  }

  public PXEmptyAutoIncValueException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXEmptyAutoIncValueException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXEmptyAutoIncValueException>(this, info);
    base.GetObjectData(info, context);
  }
}
