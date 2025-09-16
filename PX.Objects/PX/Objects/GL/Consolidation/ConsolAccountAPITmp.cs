// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolAccountAPITmp
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.GL.Consolidation;

internal class ConsolAccountAPITmp
{
  public virtual ApiProperty<string> AccountCD { get; set; }

  public virtual ApiProperty<string> Description { get; set; }

  public ConsolAccountAPITmp()
  {
  }

  public ConsolAccountAPITmp(string accountCD, string description)
  {
    this.AccountCD = new ApiProperty<string>(accountCD);
    this.Description = new ApiProperty<string>(description);
  }
}
