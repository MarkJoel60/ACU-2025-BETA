// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticlesbyStatusReport
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
public class WikiArticlesbyStatusReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Language;
  protected string _Name;

  [PXString]
  [PXUIField(DisplayName = "Language")]
  [PXSelector(typeof (Locale.localeName), DescriptionField = typeof (Locale.description))]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXString(IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Wiki ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (WikiDescriptor.pageID), SubstituteKey = typeof (WikiDescriptor.name))]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public abstract class language : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiArticlesbyStatusReport.language>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiArticlesbyStatusReport.name>
  {
  }
}
