// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.TagMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Data.Wiki.Tags;

[PXLocalizable]
public class TagMessages
{
  public const string NewKey = "<Enter Tag>";
  public const string AllTagsCD = "All Tags";
  public const string UntaggedCD = "Untagged";
  public const string AllFilesCD = "All Files";
  public const string DuplicateTagAdded = "The {0} tag already exists. Enter another tag name.";
  public const string InvalidTagName = "Specify another tag name. The <Enter Tag> name cannot be used.";
  public const string ParentTagNotEmpty = "Parent tag cannot be empty.";
  public const string FilesExistsForTag = "The tag cannot be deleted because there is at least one file associated with this tag.";
  public const string RevokedAccessLevel = "Revoked";
  public const string ViewAccessLevel = "View Only";
  public const string CreateVersionAccessLevel = "Create Version";
  public const string UploadAccessLevel = "Upload";
  public const string DeleteAccessLevel = "Delete";
}
