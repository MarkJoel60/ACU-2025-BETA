// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXText : PXRtfElement
{
  private string text;

  public PXText(PXDocument document, string text)
    : this(document, text, false)
  {
  }

  public PXText(PXDocument document, string text, bool preserveNewLines)
    : base(document)
  {
    this.text = text.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");
    if (!preserveNewLines)
      return;
    this.text = this.text.Replace(Environment.NewLine, Environment.NewLine + "\\line ");
  }

  public override void Render(StringBuilder result) => result.Append(this.text);
}
