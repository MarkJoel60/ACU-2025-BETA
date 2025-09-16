// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity.PXScreenRelatedInfoLink
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;

[PXInternalUseOnly]
public class PXScreenRelatedInfoLink
{
  public string Text { get; set; }

  public string Link { get; set; }

  public bool? HasVideo { get; set; }
}
