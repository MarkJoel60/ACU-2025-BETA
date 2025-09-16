// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiCss
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiCss : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _CssID;
  protected 
  #nullable disable
  string _Name;
  protected string _Description;
  protected string _Style;

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField]
  public Guid? CssID
  {
    get => this._CssID;
    set => this._CssID = value;
  }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Style Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (WikiCss.name))]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Style Description", Visibility = PXUIVisibility.SelectorVisible)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Style Definition")]
  public string Style
  {
    get => this._Style;
    set => this._Style = value;
  }

  public abstract class cssID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiCss.cssID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiCss.name>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiCss.description>
  {
  }

  public abstract class style : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiCss.style>
  {
  }
}
