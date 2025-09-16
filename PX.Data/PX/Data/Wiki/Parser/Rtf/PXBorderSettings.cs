// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXBorderSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXBorderSettings
{
  public BorderType type;
  public Color? color;
  public int width = 10;
  public int space;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0}{1}", (object) "\\brdrw", (object) this.width);
    if ((this.type & BorderType.Single) == BorderType.Single)
      stringBuilder.Append("\\brdrs");
    if ((this.type & BorderType.Striped) == BorderType.Striped)
      stringBuilder.Append("\\brdrdashdotstr");
    if ((this.type & BorderType.Triple) == BorderType.Triple)
      stringBuilder.Append("\\brdrtriple");
    if ((this.type & BorderType.Wavy) == BorderType.Wavy)
      stringBuilder.Append("\\brdrwavy");
    if ((this.type & BorderType.Dashed) == BorderType.Dashed)
      stringBuilder.Append("\\brdrdash");
    if ((this.type & BorderType.DashSmall) == BorderType.DashSmall)
      stringBuilder.Append("\\brdrdashsm");
    if ((this.type & BorderType.DotDash) == BorderType.DotDash)
      stringBuilder.Append("\\brdrdashd");
    if ((this.type & BorderType.DotDotDash) == BorderType.DotDotDash)
      stringBuilder.Append("\\brdrdashdd");
    if ((this.type & BorderType.Dotted) == BorderType.Dotted)
      stringBuilder.Append("\\brdrdot");
    if ((this.type & BorderType.Double) == BorderType.Double)
      stringBuilder.Append("\\brdrdb");
    if ((this.type & BorderType.DoubleThick) == BorderType.DoubleThick)
      stringBuilder.Append("\\brdrth");
    if ((this.type & BorderType.DoubleWavy) == BorderType.DoubleWavy)
      stringBuilder.Append("\\brdrwavydb");
    if ((this.type & BorderType.Emboss) == BorderType.Emboss)
      stringBuilder.Append("\\brdremboss");
    if ((this.type & BorderType.Engrave) == BorderType.Engrave)
      stringBuilder.Append("\\brdrengrave");
    if ((this.type & BorderType.Hairline) == BorderType.Hairline)
      stringBuilder.Append("\\brdrhair");
    if ((this.type & BorderType.Shadowed) == BorderType.Shadowed)
      stringBuilder.Append("\\brdrsh");
    if (this.space > 0)
      stringBuilder.AppendFormat("{0}{1}", (object) "\\brsp", (object) this.space);
    return stringBuilder.ToString();
  }
}
