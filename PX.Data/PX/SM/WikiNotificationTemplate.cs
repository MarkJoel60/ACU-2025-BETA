// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiNotificationTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM.Email;
using System;

#nullable enable
namespace PX.SM;

[PXTable(new System.Type[] {typeof (WikiPage.pageID)})]
[Serializable]
public class WikiNotificationTemplate : WikiPage
{
  protected 
  #nullable disable
  string _EntityType;
  protected string _MailTo;
  protected string _MailCc;
  protected string _MailBcc;
  protected string _Subject;
  protected string _Watchers;
  protected string _GraphType;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Folder")]
  [PXSelector(typeof (Search<WikiPageSimple.pageID, Where<WikiPageSimple.wikiID, Equal<Current<WikiPage.wikiID>>, Or<WikiPageSimple.pageID, Equal<Current<WikiPage.wikiID>>>>>), SubstituteKey = typeof (WikiPageSimple.name))]
  public override Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBInt]
  [PXDefault(12)]
  public override int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Entity")]
  public virtual string EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "To")]
  public virtual string MailTo
  {
    get => this._MailTo;
    set => this._MailTo = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "CC")]
  public virtual string MailCc
  {
    get => this._MailCc;
    set => this._MailCc = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "BCC")]
  public virtual string MailBcc
  {
    get => this._MailBcc;
    set => this._MailBcc = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Subject")]
  public virtual string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Watchers")]
  public virtual string Watchers
  {
    get => this._Watchers;
    set => this._Watchers = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Graph")]
  public virtual string GraphType
  {
    get => this._GraphType;
    set => this._GraphType = value;
  }

  [EmailAccountRaw(DisplayName = "Default Account")]
  public virtual int? DefaultEMailAccountID { get; set; }

  public new abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiNotificationTemplate.wikiID>
  {
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiNotificationTemplate.pageID>
  {
  }

  public new abstract class parentUID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiNotificationTemplate.parentUID>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiNotificationTemplate.name>
  {
  }

  public new abstract class articleType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiNotificationTemplate.articleType>
  {
  }

  public abstract class entityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiNotificationTemplate.entityType>
  {
  }

  public abstract class mailTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiNotificationTemplate.mailTo>
  {
  }

  public abstract class mailCc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiNotificationTemplate.mailCc>
  {
  }

  public abstract class mailBcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiNotificationTemplate.mailBcc>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiNotificationTemplate.subject>
  {
  }

  public abstract class watchers : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiNotificationTemplate.watchers>
  {
  }

  public abstract class graphType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiNotificationTemplate.graphType>
  {
  }

  public abstract class defaultEMailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiNotificationTemplate.defaultEMailAccountID>
  {
  }
}
