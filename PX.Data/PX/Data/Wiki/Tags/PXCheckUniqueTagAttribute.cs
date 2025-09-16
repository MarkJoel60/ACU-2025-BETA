// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.PXCheckUniqueTagAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data.Wiki.Tags;

public class PXCheckUniqueTagAttribute : PXCheckUnique
{
  protected override string PrepareMessage(PXCache cache, object currentRow, object duplicateRow)
  {
    return !(currentRow is Tag tag) ? base.PrepareMessage(cache, currentRow, duplicateRow) : $"The {tag.TagCD} tag already exists. Enter another tag name.";
  }

  public PXCheckUniqueTagAttribute()
    : base()
  {
  }
}
