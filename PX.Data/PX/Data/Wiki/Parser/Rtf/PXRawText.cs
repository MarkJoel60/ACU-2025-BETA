// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXRawText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXRawText : PXRtfElement
{
  private string rawText;

  public PXRawText(PXDocument document, string rawText)
    : base(document)
  {
    this.rawText = rawText;
  }

  public string RawText => this.rawText;

  public override void Render(StringBuilder result) => result.Append(this.RawText);
}
