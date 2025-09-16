// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXFileList2TagElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

[PXSpecialTagParser.TagName(new string[] {"filelist2", "fileslist2"})]
public class PXFileList2TagElement : PXSpecialTagElement
{
  public override string TagValue => "filelist2";
}
