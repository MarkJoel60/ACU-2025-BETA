// Decompiled with JetBrains decompiler
// Type: PX.Data.TypedWeakReference`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class TypedWeakReference<T> : WeakReference
{
  public T Target
  {
    get => (T) base.Target;
    set => this.Target = (object) value;
  }

  public TypedWeakReference(T target)
    : base((object) target)
  {
  }

  public TypedWeakReference(T target, bool trackResurrection)
    : base((object) target, trackResurrection)
  {
  }

  protected TypedWeakReference(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
