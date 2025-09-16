// Decompiled with JetBrains decompiler
// Type: PX.SM.MobileSiteMapEntry
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
public class MobileSiteMapEntry : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Title;
  protected string _Url;
  protected string _ScreenID;
  protected int? _Indent;
  private string _graphType;

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXString(512 /*0x0200*/)]
  public virtual string Url
  {
    get => this._Url;
    set => this._Url = value;
  }

  [PXString(8)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXInt]
  public virtual int? Indent
  {
    get => this._Indent;
    set => this._Indent = value;
  }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Graph Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Graphtype
  {
    [PXDependsOnFields(new System.Type[] {typeof (MobileSiteMapEntry.url)})] get
    {
      return this._graphType ?? (this._graphType = PXPageIndexingService.GetGraphType(this.Url));
    }
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapEntry.title>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapEntry.url>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapEntry.screenID>
  {
  }

  public abstract class indent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MobileSiteMapEntry.indent>
  {
  }

  public abstract class graphtype : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMapEntry.graphtype>
  {
  }
}
