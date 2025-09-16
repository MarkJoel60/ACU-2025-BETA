// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Gang`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public class Gang<T1, T2> : Gang<T1>
{
  public T2 Item2;

  public Gang(T1 item1, T2 item2)
    : base(item1)
  {
    this.Item2 = item2;
  }

  public override int GetHashCode()
  {
    return Gang.CombineHasCodes(this.GetHashCode((object) this.Item1), this.GetHashCode((object) this.Item2));
  }
}
