// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageMeta
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
public class WikiPageMeta : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Name;
  protected string _Content;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiPage.pageID))]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Name")]
  [PXDefault]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Content")]
  public virtual string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageMeta.pageID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageMeta.name>
  {
  }

  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageMeta.content>
  {
  }
}
