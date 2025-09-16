// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiReadLanguage
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
public class WikiReadLanguage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _WikiID;
  protected 
  #nullable disable
  string _LocaleID;
  private bool? _Selected;
  protected string _Language;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiDescriptor.pageID))]
  public Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBString(6, IsKey = true, IsUnicode = true, InputMask = "")]
  public virtual string LocaleID
  {
    get => this._LocaleID;
    set => this._LocaleID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Allow Using")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Language", Enabled = false)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiReadLanguage.wikiID>
  {
  }

  public abstract class localeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiReadLanguage.localeID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiReadLanguage.selected>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiReadLanguage.language>
  {
  }
}
