// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXShape
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXShape : PXRtfElement
{
  private int width;
  private int height;
  private ShapeWrapType wrap = ShapeWrapType.Around;
  private ShapeWrapSideType sideWrap;
  private bool isBelowText;
  private int zindex;
  private Dictionary<string, object> properties = new Dictionary<string, object>();
  private PXRtfElement innerRTF;

  /// <summary>Initializes a new instance of PXShape class.</summary>
  /// <param name="width">Shape width in twips.</param>
  /// <param name="height">Shape height in twips.</param>
  public PXShape(PXDocument document, int width, int height)
    : base(document)
  {
    this.width = width;
    this.height = height;
  }

  /// <summary>Gets width of shape. The value is in twips.</summary>
  public int Width => this.width;

  /// <summary>Gets height of shape. The value is in twips.</summary>
  public int Height => this.height;

  /// <summary>Gets or sets the type of wrap for the shape.</summary>
  public ShapeWrapType Wrap
  {
    get => this.wrap;
    set => this.wrap = value;
  }

  /// <summary>
  /// Gets or sets wrap on side (for types Around and AroundTightly for Wrap).
  /// </summary>
  public ShapeWrapSideType SideWrap
  {
    get => this.sideWrap;
    set => this.sideWrap = value;
  }

  /// <summary>
  /// Gets or sets relative z-ordering (false - text is below shape, true - shape is below text).
  /// </summary>
  public bool IsBelowText
  {
    get => this.isBelowText;
    set => this.isBelowText = value;
  }

  /// <summary>Gets or sets the z-order of the shape.</summary>
  public int ZIndex
  {
    get => this.zindex;
    set => this.zindex = value;
  }

  public PXRtfElement InnerRTF
  {
    get => this.innerRTF;
    set => this.innerRTF = value;
  }

  /// <summary>Gets or sets type of shape.</summary>
  public ShapeType Type
  {
    get
    {
      return this.properties.ContainsKey("shapeType") ? (ShapeType) this.properties["shapeType"] : ShapeType.Freeform;
    }
    set => this.properties["shapeType"] = (object) (int) value;
  }

  /// <summary>Gets or sets spae horizontal alignment.</summary>
  public HorizontalAlgn PosH
  {
    get
    {
      return this.properties.ContainsKey("posh") ? (HorizontalAlgn) this.properties["posh"] : HorizontalAlgn.Left;
    }
    set => this.properties["posh"] = (object) (int) value;
  }

  /// <summary>Gets or sets shape vertical alignment.</summary>
  public VerticalAlign PosV
  {
    get
    {
      return this.properties.ContainsKey("posv") ? (VerticalAlign) this.properties["posv"] : VerticalAlign.Inside;
    }
    set => this.properties["posv"] = (object) (int) value;
  }

  public Color BackgroundColor
  {
    get
    {
      return this.properties.ContainsKey("fillBackColor") ? this.CreateColorFromLong((long) this.properties["fillBackColor"]) : Color.White;
    }
    set
    {
      this.properties["fillBackColor"] = (object) (long) ((int) value.R + (int) value.G * 256 /*0x0100*/ + (int) value.B * 65536 /*0x010000*/);
    }
  }

  public Color ForegroundColor
  {
    get
    {
      return this.properties.ContainsKey("fillColor") ? this.CreateColorFromLong((long) this.properties["fillColor"]) : Color.White;
    }
    set
    {
      this.properties["fillColor"] = (object) ((long) ((int) value.R + (int) value.G * 256 /*0x0100*/ + (int) value.B * 65536 /*0x010000*/)).ToString();
    }
  }

  /// <summary>Gets shape properties dictionary.</summary>
  public Dictionary<string, object> Properties => this.properties;

  public override void Render(StringBuilder result)
  {
    result.Append("{\\shp{\\*\\shpinst");
    result.Append("\\shpleft0\\shpright");
    result.Append(this.Width);
    result.Append("\\shptop0\\shpbottom");
    result.Append(this.Height);
    result.Append("\\shpwr");
    result.Append((int) this.Wrap);
    result.Append("\\shpwrk");
    result.Append((int) this.SideWrap);
    result.Append("\\shpfblwtxt");
    result.Append(Convert.ToInt32(this.IsBelowText));
    result.Append("\\shpz");
    result.AppendLine(this.ZIndex.ToString());
    foreach (string key in this.properties.Keys)
    {
      result.Append("{\\sp{\\sn ");
      result.Append(key);
      result.Append("}{\\sv ");
      if (this.properties[key] is PXRtfElement)
        ((PXRtfElement) this.properties[key]).Render(result);
      else
        result.Append(this.properties[key].ToString());
      result.AppendLine("}}");
    }
    result.AppendLine("{\\sp{\\sn fAllowOverlap}{\\sv 0}}");
    if (this.InnerRTF != null)
    {
      result.AppendLine("{\\shptxt");
      this.InnerRTF.Render(result);
      result.AppendLine("}");
    }
    result.Append("}}");
  }

  private Color CreateColorFromLong(long value)
  {
    int red = (int) (byte) ((ulong) value % 256UL /*0x0100*/);
    byte num1 = (byte) ((ulong) (value / 256L /*0x0100*/) % 256UL /*0x0100*/);
    byte num2 = (byte) ((ulong) (value / 65536L /*0x010000*/) % 256UL /*0x0100*/);
    int green = (int) num1;
    int blue = (int) num2;
    return Color.FromArgb(red, green, blue);
  }
}
