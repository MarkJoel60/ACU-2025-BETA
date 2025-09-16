// Decompiled with JetBrains decompiler
// Type: PX.Data.ConstantInterval`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <exclude />
public abstract class ConstantInterval<TSelf> : BqlType<IBqlDateTime, System.DateTime>.Constant<
#nullable disable
TSelf> where TSelf : ConstantInterval<TSelf>, new()
{
  public ConstantInterval(
    int years,
    int month,
    int days,
    int hours,
    int minutes,
    int seconds,
    int milliseconds)
    : base(new System.DateTime(++years, ++month, ++days, hours, minutes, seconds, milliseconds))
  {
  }

  public ConstantInterval(int years, int month, int days)
    : this(years, month, days, 0, 0, 0, 0)
  {
  }

  public ConstantInterval(int hours, int minutes, int seconds, int milliseconds)
    : this(0, 0, 0, hours, minutes, seconds, milliseconds)
  {
  }
}
