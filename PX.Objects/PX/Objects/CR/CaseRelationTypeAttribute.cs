// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CaseRelationTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class CaseRelationTypeAttribute : PXStringListAttribute
{
  public const string _BLOCKS_VALUE = "P";
  public const string _DEPENDS_ON_VALUE = "C";
  public const string _RELATED_VALUE = "R";
  public const string _DUBLICATE_OF_VALUE = "D";

  public CaseRelationTypeAttribute()
    : base(new string[4]{ "P", "C", "R", "D" }, new string[4]
    {
      "Blocks",
      "Depends On",
      "Related",
      "Duplicate Of"
    })
  {
  }
}
