// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXBookmark
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXBookmark : PXRtfElement
{
  private string name;

  public PXBookmark(PXDocument document, string name)
    : base(document)
  {
    this.name = name;
  }

  public override void Render(StringBuilder result)
  {
    result.Append("{\\*\\bkmkstart ");
    result.Append(this.name);
    result.Append("}");
    result.Append("{\\*\\bkmkend ");
    result.Append(this.name);
    result.Append("}");
  }
}
