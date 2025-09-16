// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Exceptions.ReleaseException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common.Exceptions;

/// <summary>
/// Common class of errors that happen during entity release process.
/// </summary>
public class ReleaseException : PXException
{
  public FailedWith FailedWith { get; set; }

  public ReleaseException(string format, params object[] args)
    : base(format, args)
  {
  }

  public ReleaseException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public ReleaseException(FailedWith context, string format, params object[] args)
    : base(format, args)
  {
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<ReleaseException>(this, info);
    base.GetObjectData(info, context);
    info.AddValue("FailedWith", (object) this.FailedWith);
  }
}
