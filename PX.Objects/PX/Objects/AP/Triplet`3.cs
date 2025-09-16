// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Triplet`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP;

public class Triplet<T1, T2, T3> : IComparable<Triplet<T1, T2, T3>>, IEquatable<Triplet<T1, T2, T3>>
  where T1 : IComparable<T1>
  where T2 : IComparable<T2>
  where T3 : IComparable<T3>
{
  public T1 first;
  public T2 second;
  public T3 third;

  public Triplet(T1 aArg1, T2 aArg2, T3 aArg3)
  {
    this.first = aArg1;
    this.second = aArg2;
    this.third = aArg3;
  }

  public virtual int CompareTo(Triplet<T1, T2, T3> other)
  {
    int num = this.first.CompareTo(other.first);
    if (num == 0)
      num = this.second.CompareTo(other.second);
    return num == 0 ? this.third.CompareTo(other.third) : num;
  }

  public override int GetHashCode() => 0;

  public virtual bool Equals(Triplet<T1, T2, T3> other) => this.CompareTo(other) == 0;

  public override bool Equals(object other)
  {
    return other is Triplet<T1, T2, T3> other1 ? this.CompareTo(other1) == 0 : this.Equals(other);
  }
}
