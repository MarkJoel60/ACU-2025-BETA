// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiTag
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
public class WikiTag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private Guid? _WikiID;
  protected int? _TagID;
  protected 
  #nullable disable
  string _Description;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (WikiDescriptor.pageID))]
  public Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBIdentity]
  public virtual int? TagID
  {
    get => this._TagID;
    set => this._TagID = value;
  }

  [PXDBString(InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiTag.wikiID>
  {
  }

  public abstract class tagID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiTag.tagID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTag.description>
  {
  }
}
