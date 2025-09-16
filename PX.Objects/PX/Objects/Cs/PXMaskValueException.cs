// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXMaskValueException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CS;

public class PXMaskValueException : PXArgumentException
{
  public int SourceIdx = -1;

  public PXMaskValueException()
  {
  }

  public PXMaskValueException(int SourceIdx) => this.SourceIdx = SourceIdx;

  public PXMaskValueException(params object[] args)
    : base("Invalid mask specified in segment {0} for {1}.", args)
  {
  }

  public PXMaskValueException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXMaskValueException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXMaskValueException>(this, info);
    base.GetObjectData(info, context);
  }
}
