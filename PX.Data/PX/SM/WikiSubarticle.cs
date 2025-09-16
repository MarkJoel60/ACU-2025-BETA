// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSubarticle
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
public class WikiSubarticle : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected Guid? _WikiID;
  protected double? _Number;
  protected 
  #nullable disable
  string _Name;
  protected string _Title;
  protected bool? _Folder;

  public WikiSubarticle()
  {
  }

  public WikiSubarticle(WikiPage pg)
  {
    this.PageID = pg.PageID;
    this.WikiID = pg.WikiID;
    this.Number = pg.Number;
    this.Name = pg.Name;
    this.Title = pg.Title;
    this.Folder = pg.Folder;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBDouble]
  [PXDefault(0.0)]
  [PXUIField(DisplayName = "Number")]
  public virtual double? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Article ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Article Name", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Null)]
  [PXUIField(DisplayName = "Folder", Enabled = false)]
  public virtual bool? Folder
  {
    get => this._Folder;
    set => this._Folder = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiSubarticle.pageID>
  {
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiSubarticle.wikiID>
  {
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  WikiSubarticle.number>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSubarticle.name>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSubarticle.title>
  {
  }

  public abstract class folder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiSubarticle.folder>
  {
  }
}
