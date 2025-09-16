// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXPicture
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Drawing;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXPicture : PXRtfElement
{
  private Image img;
  private int maxWidth;
  private int hmmPerInch = 2540;
  private int twipsPerInch = 1440;
  private float xDpi = 96f;
  private float yDpi = 96f;
  private int scalex;
  private int scaley;
  private double factor = 1.0;

  public PXPicture(PXDocument document, Image img)
    : base(document)
  {
    this.img = img;
    this.maxWidth = document.ClientWidth;
  }

  /// <summary>Gets target width of the image in twips</summary>
  public int Width
  {
    get
    {
      int width = (int) System.Math.Round((double) this.img.Width / (double) this.xDpi * (double) this.twipsPerInch);
      this.factor = 1.0;
      if (width > this.maxWidth)
      {
        this.factor = (double) width / (double) this.maxWidth;
        width = this.maxWidth;
      }
      return width;
    }
  }

  /// <summary>Gets target height of the image in twips</summary>
  public int Height
  {
    get
    {
      return (int) System.Math.Round((double) (int) System.Math.Round((double) this.img.Height / (double) this.yDpi * (double) this.twipsPerInch) / this.factor);
    }
  }

  /// <summary>Gets or sets horizontal scaling value.</summary>
  public int ScaleX
  {
    get => this.scalex;
    set => this.scalex = value;
  }

  /// <summary>Gets or sets vertical scaling value.</summary>
  public int ScaleY
  {
    get => this.scaley;
    set => this.scaley = value;
  }

  public override void Render(StringBuilder result)
  {
    int num1 = (int) System.Math.Round((double) this.img.Width / (double) this.xDpi * (double) this.hmmPerInch);
    int num2 = (int) System.Math.Round((double) this.img.Height / (double) this.yDpi * (double) this.hmmPerInch);
    result.Append("{\\pict\\wmetafile8\\picw");
    result.Append(num1);
    result.Append("\\pich");
    result.Append(num2);
    result.Append("\\picwgoal");
    result.Append(this.Width);
    result.Append("\\pichgoal");
    result.Append(this.Height);
    if (this.ScaleX > 0)
    {
      result.Append("\\picscalex");
      result.Append(this.ScaleX);
    }
    if (this.ScaleY > 0)
    {
      result.Append("\\picscaley");
      result.Append(this.ScaleY);
    }
    result.Append(Environment.NewLine);
    this.GetRtfImage(result);
    result.Append(Environment.NewLine);
    result.Append(Environment.NewLine);
    result.Append("}");
  }

  private void GetRtfImage(StringBuilder rtf)
  {
    this.img.Save((Stream) new StringStream(rtf), this.img.RawFormat);
  }
}
