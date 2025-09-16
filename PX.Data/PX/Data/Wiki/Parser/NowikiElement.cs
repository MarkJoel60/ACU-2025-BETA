// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.NowikiElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class NowikiElement : PXElement
{
  private string value;

  public string Value
  {
    get => this.value;
    set => this.value = value;
  }

  public NowikiElement(string value) => this.value = value;
}
