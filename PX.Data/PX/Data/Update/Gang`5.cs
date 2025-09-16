// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Gang`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public class Gang<T1, T2, T3, T4, T5> : Gang<T1, T2, T3, T4>
{
  public T5 Item5;

  public Gang(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    : base(item1, item2, item3, item4)
  {
    this.Item5 = item5;
  }

  public override int GetHashCode()
  {
    return Gang.CombineHasCodes(this.GetHashCode((object) this.Item1), this.GetHashCode((object) this.Item2), this.GetHashCode((object) this.Item3), this.GetHashCode((object) this.Item4), this.GetHashCode((object) this.Item5));
  }
}
