// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXCommonSpecialTagElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

public sealed class PXCommonSpecialTagElement : PXSpecialTagElement
{
  private readonly string _tagValue;

  public PXCommonSpecialTagElement(string tagValue) => this._tagValue = tagValue;

  public override string TagValue => this._tagValue;
}
