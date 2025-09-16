// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.CommonTags
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable enable
namespace PX.Data.Wiki.Tags;

public static class CommonTags
{
  public const string AllTagsStringID = "11111111-1111-1111-1111-111111111111";
  public const string UntaggedStringID = "22222222-2222-2222-2222-222222222222";
  public const string AllFilesStringID = "33333333-3333-3333-3333-333333333333";
  public static readonly Guid AllTagsID = new Guid("11111111-1111-1111-1111-111111111111");
  public static readonly Guid UntaggedID = new Guid("22222222-2222-2222-2222-222222222222");
  public static readonly Guid AllFilesID = new Guid("33333333-3333-3333-3333-333333333333");

  public static Tag AllTags
  {
    get
    {
      return new Tag()
      {
        TagID = new Guid?(CommonTags.AllTagsID),
        TagCD = "All Tags",
        NoteID = new Guid?(CommonTags.AllTagsID)
      };
    }
  }

  public static Tag Untagged
  {
    get
    {
      return new Tag()
      {
        TagID = new Guid?(CommonTags.UntaggedID),
        TagCD = nameof (Untagged),
        NoteID = new Guid?(CommonTags.UntaggedID)
      };
    }
  }

  public static Tag AllFiles
  {
    get
    {
      return new Tag()
      {
        TagID = new Guid?(CommonTags.AllFilesID),
        TagCD = "All Files",
        NoteID = new Guid?(CommonTags.AllFilesID)
      };
    }
  }

  public static bool IsTagCommon(Guid? tagID)
  {
    Guid? nullable1 = tagID;
    Guid allTagsId = CommonTags.AllTagsID;
    if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == allTagsId ? 1 : 0) : 1) : 0) == 0)
    {
      Guid? nullable2 = tagID;
      Guid untaggedId = CommonTags.UntaggedID;
      if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == untaggedId ? 1 : 0) : 1) : 0) == 0)
      {
        Guid? nullable3 = tagID;
        Guid allFilesId = CommonTags.AllFilesID;
        if (!nullable3.HasValue)
          return false;
        return !nullable3.HasValue || nullable3.GetValueOrDefault() == allFilesId;
      }
    }
    return true;
  }
}
