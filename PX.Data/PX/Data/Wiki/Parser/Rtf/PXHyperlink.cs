// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXHyperlink
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXHyperlink : PXRtfElement
{
  private string hyperlink = "";
  private Color color = Color.Blue;

  public PXHyperlink(PXDocument document, string hyperlink)
    : base(document)
  {
    this.hyperlink = hyperlink;
  }

  public Color CaptionColor
  {
    get => this.color;
    set => this.color = value;
  }

  public string Caption { get; set; }

  public override void Render(StringBuilder result)
  {
    result.Append("{\\field{\\*\\fldinst{HYPERLINK ");
    result.Append("\"");
    result.Append(this.hyperlink);
    result.Append("\"");
    result.Append("}}{\\fldrslt{\\cf");
    result.Append(this.document.GetColorCode(this.CaptionColor));
    result.Append("\\ul ");
    result.Append(this.Caption);
    result.Append("\\ul0\\cf0}}}");
  }
}
