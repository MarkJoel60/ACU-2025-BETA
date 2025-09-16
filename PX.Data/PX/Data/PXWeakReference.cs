// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWeakReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXHidden]
[Serializable]
internal sealed class PXWeakReference : WeakReference, IBqlTable, IBqlTableSystemDataStorage
{
  private static readonly InvalidOperationException tableSystemDataException = new InvalidOperationException("PXWeakReference does not contain Target.");
  private readonly int _hashCode;

  ref PXBqlTableSystemData IBqlTableSystemDataStorage.GetBqlTableSystemData()
  {
    return this.Target is IBqlTable target ? ref target.GetBqlTableSystemData() : throw PXWeakReference.tableSystemDataException;
  }

  private PXWeakReference(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    if (this.IsAlive)
      this._hashCode = this.Target.GetHashCode();
    else
      this._hashCode = base.GetHashCode();
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
  }

  internal PXWeakReference(IBqlTable o)
    : base((object) o)
  {
    this._hashCode = o.GetHashCode();
  }

  public override bool Equals(object o)
  {
    object target;
    return o != null && o.GetHashCode() == this._hashCode && (o == this || (target = this.Target) != null && o == target);
  }

  public override int GetHashCode() => this._hashCode;
}
