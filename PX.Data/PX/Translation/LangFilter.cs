// Decompiled with JetBrains decompiler
// Type: PX.Translation.LangFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

/// <exclude />
[Serializable]
public class LangFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string LANGUAGE_PARAM = "Language";
  public const int KEY_VALUE = 0;
  protected string _Language;

  [PXDBInt(IsKey = true)]
  public virtual int? Key { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Language")]
  [PXStringList(MultiSelect = true)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Excluded")]
  public virtual bool? ShowExcluded { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Localized")]
  public virtual bool? ShowLocalized { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Only Unbound")]
  public virtual bool? ShowUnboundOnly { get; set; }

  [PXUIField(DisplayName = "Unbound Resources To Display")]
  [PXDefault("")]
  [PXStringList(MultiSelect = true)]
  [PXDBString]
  public virtual string ShowType { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Show Used in UI")]
  [PXSiteMapNodeSelector]
  public virtual string ScreenID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Created Since")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Modified After")]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  /// <exclude />
  public abstract class key : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LangFilter.key>
  {
  }

  /// <exclude />
  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LangFilter.language>
  {
  }

  /// <exclude />
  public abstract class showExcluded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LangFilter.showExcluded>
  {
  }

  /// <exclude />
  public abstract class showLocalized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LangFilter.showLocalized>
  {
  }

  /// <exclude />
  public abstract class showUnboundOnly : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LangFilter.showUnboundOnly>
  {
  }

  /// <exclude />
  public abstract class showType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LangFilter.showType>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LangFilter.screenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LangFilter.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LangFilter.lastModifiedDateTime>
  {
  }
}
