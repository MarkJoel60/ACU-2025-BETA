// Decompiled with JetBrains decompiler
// Type: PX.SM.GetLinkFilterType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class GetLinkFilterType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ExternalLink;
  protected string _WikiLink;

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "External Link")]
  public virtual string ExternalLink
  {
    get => this._ExternalLink;
    set => this._ExternalLink = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Wiki Link")]
  public virtual string WikiLink
  {
    get => this._WikiLink;
    set => this._WikiLink = value;
  }

  /// <exclude />
  public abstract class externalLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetLinkFilterType.externalLink>
  {
  }

  /// <exclude />
  public abstract class wikiLink : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetLinkFilterType.wikiLink>
  {
  }
}
