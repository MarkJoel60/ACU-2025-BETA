// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXMaskArgumentException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.CS;

public class PXMaskArgumentException : PXArgumentException
{
  public int SourceIdx = -1;
  protected object[] _args = new object[0];

  public PXMaskArgumentException()
  {
  }

  public PXMaskArgumentException(int SourceIdx) => this.SourceIdx = SourceIdx;

  public PXMaskArgumentException(params object[] args)
    : base(args.Length < 3 ? "{0} {1} is missing." : "{0} '{2}' {1} is missing.", args)
  {
    this._args = args;
  }

  public static object[] ConcatArrays(object[] a, object[] b)
  {
    List<object> objectList = new List<object>((IEnumerable<object>) a);
    objectList.AddRange((IEnumerable<object>) b);
    return objectList.ToArray();
  }

  public PXMaskArgumentException(PXMaskArgumentException source, params object[] args)
    : this(PXMaskArgumentException.ConcatArrays(source._args, args))
  {
    this.SourceIdx = source.SourceIdx;
  }

  public PXMaskArgumentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXMaskArgumentException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXMaskArgumentException>(this, info);
    base.GetObjectData(info, context);
  }
}
