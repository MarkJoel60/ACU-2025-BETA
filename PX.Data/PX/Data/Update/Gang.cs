// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Gang
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public static class Gang
{
  public static Gang<T1> Create<T1>(T1 item1) => new Gang<T1>(item1);

  public static Gang<T1, T2> Create<T1, T2>(T1 item1, T2 item2) => new Gang<T1, T2>(item1, item2);

  public static Gang<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
  {
    return new Gang<T1, T2, T3>(item1, item2, item3);
  }

  public static Gang<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
    T1 item1,
    T2 item2,
    T3 item3,
    T4 item4)
  {
    return new Gang<T1, T2, T3, T4>(item1, item2, item3, item4);
  }

  public static Gang<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(
    T1 item1,
    T2 item2,
    T3 item3,
    T4 item4,
    T5 item5)
  {
    return new Gang<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
  }

  internal static int CombineHasCodes(int h1, int h2) => (h1 << 5) + h1 ^ h2;

  internal static int CombineHasCodes(int h1, int h2, int h3)
  {
    return Gang.CombineHasCodes(Gang.CombineHasCodes(h1, h2), h3);
  }

  internal static int CombineHasCodes(int h1, int h2, int h3, int h4)
  {
    return Gang.CombineHasCodes(Gang.CombineHasCodes(h1, h2), Gang.CombineHasCodes(h3, h4));
  }

  internal static int CombineHasCodes(int h1, int h2, int h3, int h4, int h5)
  {
    return Gang.CombineHasCodes(Gang.CombineHasCodes(h1, h2, h3), Gang.CombineHasCodes(h4, h5));
  }
}
