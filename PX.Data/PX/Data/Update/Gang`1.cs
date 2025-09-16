// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Gang`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

public class Gang<T1>
{
  public T1 Item1;

  public Gang(T1 item1) => this.Item1 = item1;

  protected int GetHashCode(object obj) => obj != null ? obj.GetHashCode() : 0;

  public override int GetHashCode() => this.GetHashCode((object) this.Item1);
}
