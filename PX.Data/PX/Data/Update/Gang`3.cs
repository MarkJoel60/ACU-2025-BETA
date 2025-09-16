// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Gang`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public class Gang<T1, T2, T3> : Gang<T1, T2>
{
  public T3 Item3;

  public Gang(T1 item1, T2 item2, T3 item3)
    : base(item1, item2)
  {
    this.Item3 = item3;
  }

  public override int GetHashCode()
  {
    return Gang.CombineHasCodes(this.GetHashCode((object) this.Item1), this.GetHashCode((object) this.Item2), this.GetHashCode((object) this.Item3));
  }
}
