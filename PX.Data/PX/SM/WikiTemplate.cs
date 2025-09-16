// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiTemplate
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
public class WikiTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _SourceType;
  protected Guid? _Wiki;
  protected Guid? _NotificationID;
  protected Guid? _ArticleID;

  [PXInt(IsKey = true)]
  public virtual int Key
  {
    get => 1;
    set
    {
    }
  }

  [PXString]
  public virtual string SourceType
  {
    get => this._SourceType;
    set => this._SourceType = value;
  }

  [PXGuid]
  [PXUIField(DisplayName = "Wiki")]
  [PXWikiSelector(SubstituteKey = typeof (WikiDescriptor.name))]
  public virtual Guid? Wiki
  {
    get => this._Wiki;
    set => this._Wiki = value;
  }

  [PXGuid]
  [PXUIField(DisplayName = "Page")]
  [PXWikiTemplateSelector(Wiki = typeof (WikiTemplate.wiki), EntityType = typeof (WikiTemplate.sourceType), SubstituteKey = typeof (WikiNotificationTemplate.name))]
  public virtual Guid? NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [PXGuid]
  [PXUIField(DisplayName = "Page")]
  [PXWikiArticleSelector(Wiki = typeof (WikiTemplate.wiki), SubstituteKey = typeof (WikiPageSimple.name))]
  public virtual Guid? ArticleID
  {
    get => this._ArticleID;
    set => this._ArticleID = value;
  }

  /// <exclude />
  public abstract class key : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiTemplate.key>
  {
  }

  /// <exclude />
  public abstract class sourceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTemplate.sourceType>
  {
  }

  /// <exclude />
  public abstract class wiki : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiTemplate.wiki>
  {
  }

  /// <exclude />
  public abstract class notificationID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiTemplate.notificationID>
  {
  }

  /// <exclude />
  public abstract class articleID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiTemplate.articleID>
  {
  }
}
