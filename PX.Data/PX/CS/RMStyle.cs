// Decompiled with JetBrains decompiler
// Type: PX.CS.RMStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.CS;

[PXCacheName("Style")]
public class RMStyle : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _StyleID;
  protected 
  #nullable disable
  string _Color;
  protected string _BackColor;
  protected short? _TextAlign;
  protected string _FontName;
  protected double? _FontSize;
  protected short? _FontSizeType;
  protected short? _FontStyle;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual int? StyleID
  {
    get => this._StyleID;
    set => this._StyleID = value;
  }

  [RMColor]
  [PXUIField(DisplayName = "Color")]
  public virtual string Color
  {
    get => this._Color;
    set => this._Color = value;
  }

  [RMColor]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Backgr. Color")]
  public virtual string BackColor
  {
    get => this._BackColor;
    set => this._BackColor = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Color")]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.color)})]
  public string ColorRGBA
  {
    get
    {
      return !string.IsNullOrEmpty(this.Color) ? $"#{this.Color.Substring(2)}{this.Color.Substring(0, 2)}" : this.Color;
    }
    set
    {
      this.Color = string.IsNullOrEmpty(value) ? (string) null : this.Color.Substring(7) + this.Color.Substring(1, 6);
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Color")]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.color)})]
  public string ColorRGB
  {
    get => !string.IsNullOrEmpty(this.Color) ? "#" + this.Color.Substring(2) : this.Color;
    set => this.Color = string.IsNullOrEmpty(value) ? (string) null : "FF" + value.Substring(1, 6);
  }

  [PXString]
  [PXUIField(DisplayName = "Backgr. Color")]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.backColor)})]
  public string BackColorRGBA
  {
    get
    {
      return !string.IsNullOrEmpty(this.BackColor) ? "#" + this.BackColor.Substring(2) : this.BackColor;
    }
    set
    {
      this.BackColor = string.IsNullOrEmpty(value) ? (string) null : "FF" + this.BackColor.Substring(1, 6);
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Backgr. Color")]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.backColor)})]
  public string BackColorRGB
  {
    get
    {
      return !string.IsNullOrEmpty(this.BackColor) ? "#" + this.BackColor.Substring(2) : this.BackColor;
    }
    set
    {
      this.BackColor = string.IsNullOrEmpty(value) ? (string) null : "FF" + value.Substring(1, 6);
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.fontStyle)})]
  [PXUIField(DisplayName = "Bold")]
  public bool? Bold
  {
    get => new bool?(((uint) this.FontStyle.GetValueOrDefault() & 1U) > 0U);
    set
    {
      bool? nullable = value;
      bool flag = true;
      this.FontStyle = new short?(nullable.GetValueOrDefault() == flag & nullable.HasValue ? (short) ((int) this.FontStyle.GetValueOrDefault() | 1) : (short) ((int) this.FontStyle.GetValueOrDefault() & -2));
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.fontStyle)})]
  [PXUIField(DisplayName = "Italic")]
  public bool? Italic
  {
    get => new bool?(((uint) this.FontStyle.GetValueOrDefault() & 2U) > 0U);
    set
    {
      bool? nullable = value;
      bool flag = true;
      this.FontStyle = new short?(nullable.GetValueOrDefault() == flag & nullable.HasValue ? (short) ((int) this.FontStyle.GetValueOrDefault() | 2) : (short) ((int) this.FontStyle.GetValueOrDefault() & -3));
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.fontStyle)})]
  [PXUIField(DisplayName = "Underline")]
  public bool? Underline
  {
    get => new bool?(((uint) this.FontStyle.GetValueOrDefault() & 4U) > 0U);
    set
    {
      bool? nullable = value;
      bool flag = true;
      this.FontStyle = new short?(nullable.GetValueOrDefault() == flag & nullable.HasValue ? (short) ((int) this.FontStyle.GetValueOrDefault() | 4) : (short) ((int) this.FontStyle.GetValueOrDefault() & -5));
    }
  }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (RMStyle.fontStyle)})]
  [PXUIField(DisplayName = "Strikeout")]
  public bool? Strikeout
  {
    get => new bool?(((uint) this.FontStyle.GetValueOrDefault() & 8U) > 0U);
    set
    {
      bool? nullable = value;
      bool flag = true;
      this.FontStyle = new short?(nullable.GetValueOrDefault() == flag & nullable.HasValue ? (short) ((int) this.FontStyle.GetValueOrDefault() | 8) : (short) ((int) this.FontStyle.GetValueOrDefault() & -9));
    }
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList("0;Not Set,1;Left,2;Center,3;Right")]
  [PXUIField(DisplayName = "Text Align")]
  public virtual short? TextAlign
  {
    get => this._TextAlign;
    set => this._TextAlign = value;
  }

  [RMFontName]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [RMFontNamesList]
  [PXUIField(DisplayName = "Font")]
  public virtual string FontName
  {
    get => this._FontName;
    set => this._FontName = value;
  }

  [PXDBDouble]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Font Size")]
  public virtual double? FontSize
  {
    get => this._FontSize;
    set => this._FontSize = value;
  }

  [PXDBShort]
  [PXDefault(1)]
  [PXIntList("1;Pixel,2;Point,3;Pica,4;Inch,5;Mm,6;Cm")]
  [PXUIField(DisplayName = "Size Type")]
  public virtual short? FontSizeType
  {
    get => this._FontSizeType;
    set => this._FontSizeType = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Font Style")]
  public virtual short? FontStyle
  {
    get => this._FontStyle;
    set => this._FontStyle = value;
  }

  public class PK : PrimaryKeyOf<RMStyle>.By<RMStyle.styleID>
  {
    public static RMStyle Find(PXGraph graph, int? styleID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMStyle>.By<RMStyle.styleID>.FindBy(graph, (object) styleID, options);
    }
  }

  public abstract class styleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMStyle.styleID>
  {
  }

  public abstract class color : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.color>
  {
  }

  public abstract class backColor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.backColor>
  {
  }

  public abstract class colorRGBA : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.colorRGBA>
  {
  }

  public abstract class colorRGB : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.colorRGB>
  {
  }

  public abstract class backColorRGBA : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.backColorRGBA>
  {
  }

  public abstract class backColorRGB : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.backColorRGB>
  {
  }

  public abstract class bold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMStyle.bold>
  {
  }

  public abstract class italic : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMStyle.italic>
  {
  }

  public abstract class underline : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMStyle.underline>
  {
  }

  public abstract class strikeout : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RMStyle.strikeout>
  {
  }

  public abstract class textAlign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMStyle.textAlign>
  {
  }

  public abstract class fontName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMStyle.fontName>
  {
  }

  public abstract class fontSize : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  RMStyle.fontSize>
  {
  }

  public abstract class fontSizeType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMStyle.fontSizeType>
  {
  }

  public abstract class fontStyle : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMStyle.fontStyle>
  {
  }
}
