// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Pair`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP;

public class Pair<FT, ST> : IComparable<Pair<FT, ST>>, IEquatable<Pair<FT, ST>>
  where FT : IComparable<FT>
  where ST : IComparable<ST>
{
  public FT first;
  public ST second;

  public Pair(FT aFirst, ST aSecond)
  {
    this.first = aFirst;
    this.second = aSecond;
  }

  public virtual int CompareTo(Pair<FT, ST> other)
  {
    int num = this.first.CompareTo(other.first);
    return num == 0 ? this.second.CompareTo(other.second) : num;
  }

  public override int GetHashCode() => 0;

  public virtual bool Equals(Pair<FT, ST> other) => this.CompareTo(other) == 0;

  public override bool Equals(object other)
  {
    return other is Pair<FT, ST> other1 ? this.CompareTo(other1) == 0 : this.Equals(other);
  }
}
