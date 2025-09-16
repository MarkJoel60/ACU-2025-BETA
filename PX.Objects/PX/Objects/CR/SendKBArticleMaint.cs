// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SendKBArticleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Wiki.Parser;
using PX.Objects.EP;
using PX.SM;
using PX.SM.Email;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CR;

public class SendKBArticleMaint : PXGraph<
#nullable disable
SendKBArticleMaint>
{
  public PXFilter<SendKBArticleMaint.EmailMessage> Message;
  public PXFilter<SendKBArticleMaint.Article> InsertArticleFilter;
  public PXCancel<SendKBArticleMaint.EmailMessage> Cancel;
  public PXAction<SendKBArticleMaint.EmailMessage> Send;
  public PXAction<SendKBArticleMaint.EmailMessage> insertArticle;

  [PXButton]
  [PXUIField(DisplayName = "Send")]
  public virtual IEnumerable send(PXAdapter adapter)
  {
    SendKBArticleMaint.EmailMessage current = ((PXSelectBase<SendKBArticleMaint.EmailMessage>) this.Message).Current;
    SendKBArticleMaint.CheckValue((object) current.MailAccountID, "From");
    SendKBArticleMaint.CheckText(current.MailTo, "To");
    SendKBArticleMaint.CheckText(current.Subject, "Subject");
    WikiDescriptorExt wikiDescriptorExt = PXResultset<WikiDescriptorExt>.op_Implicit(PXSelectBase<WikiDescriptorExt, PXSelect<WikiDescriptorExt, Where<WikiDescriptorExt.pageID, Equal<Required<WikiDescriptorExt.pageID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.WikiID
    }));
    ISettings wikiSettings = this.WikiSettings;
    (wikiSettings == null ? new PXSettings() : new PXSettings(wikiSettings)).ExternalRootUrl = wikiDescriptorExt == null ? (string) null : ((WikiDescriptor) wikiDescriptorExt).PubVirtualPath;
    new NotificationGenerator((PXGraph) this)
    {
      MailAccountId = current.MailAccountID,
      Subject = current.Subject,
      To = current.MailTo,
      Cc = current.MailCc,
      Bcc = current.MailBcc,
      Body = current.WikiText
    }.Send();
    return adapter.Get();
  }

  [PXButton(ImageKey = "AddArticle", Tooltip = "Inserts article into the mail body")]
  [PXUIField(DisplayName = "Insert Article")]
  public virtual IEnumerable InsertArticle(PXAdapter adapter)
  {
    // ISSUE: method pointer
    if (((PXSelectBase<SendKBArticleMaint.Article>) this.InsertArticleFilter).AskExt("InsertArticleFilter", new PXView.InitializePanel((object) this, __methodptr(InitializeInsertArticleFilter)), true) == 1)
      ((PXSelectBase<SendKBArticleMaint.EmailMessage>) this.Message).Current.WikiText += ((PXSelectBase<SendKBArticleMaint.Article>) this.InsertArticleFilter).Current.ParsedContent;
    return adapter.Get();
  }

  protected virtual void Article_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SendKBArticleMaint.Article row))
      return;
    string str = (string) null;
    if (row.ArticleID.HasValue)
    {
      WikiReader instance = PXGraph.CreateInstance<WikiReader>();
      ((PXSelectBase<WikiPageFilter>) ((WikiPageReader<WikiPage, WikiPage.pageID, WikiPage.wikiID, WikiPage.name, WikiPageFilter, Where<WikiPage.pageID, Equal<WikiPage.pageID>>>) instance).Filter).Current.PageID = row.ArticleID;
      WikiPage wikiPage = ((WikiPageReader<WikiPage, WikiPage.pageID, WikiPage.wikiID, WikiPage.name, WikiPageFilter, Where<WikiPage.pageID, Equal<WikiPage.pageID>>>) instance).ReadPage();
      if (wikiPage != null)
        str = wikiPage.Content;
    }
    row.Content = str;
  }

  protected virtual void EmailMessage_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is SendKBArticleMaint.EmailMessage row))
      return;
    EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelectJoin<EMailAccount, LeftJoin<PreferencesEmail, On<PreferencesEmail.defaultEMailAccountID, Equal<EMailAccount.emailAccountID>>>, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Desc<PreferencesEmail.defaultEMailAccountID, Asc<EMailAccount.address>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (emailAccount == null)
      return;
    row.MailAccountID = emailAccount.EmailAccountID;
  }

  public ISettings WikiSettings { get; set; }

  private void InitializeInsertArticleFilter(PXGraph graph, string name)
  {
    ((PXSelectBase<SendKBArticleMaint.Article>) this.InsertArticleFilter).Current.ArticleID = new Guid?();
    ((PXSelectBase<SendKBArticleMaint.Article>) this.InsertArticleFilter).Current.Content = (string) null;
  }

  private static void CheckValue(object val, string fieldName)
  {
    if (val == null)
      throw new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("The data is incorrect in {0}.", new object[1]
      {
        (object) fieldName
      }));
  }

  private static void CheckText(string subject, string fieldName)
  {
    if (string.IsNullOrEmpty(subject))
      throw new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("The data is incorrect in {0}.", new object[1]
      {
        (object) fieldName
      }));
  }

  [Serializable]
  public class EmailMessage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [EmailAccountRaw]
    [PXDefault]
    public virtual int? MailAccountID { get; set; }

    [PXString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "To")]
    [PXDefault]
    public virtual string MailTo { get; set; }

    [PXString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "CC")]
    public virtual string MailCc { get; set; }

    [PXString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "BCC")]
    public virtual string MailBcc { get; set; }

    [PXString(255 /*0xFF*/)]
    [PXDefault]
    [PXUIField(DisplayName = "Subject")]
    public virtual string Subject { get; set; }

    [PXDBGuid(false)]
    public virtual Guid? MailUser { get; set; }

    [PXString]
    public virtual string WikiText { get; set; }

    [PXGuid]
    public virtual Guid? WikiID { get; set; }

    public abstract class mailAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.mailAccountID>
    {
    }

    public abstract class mailTo : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.mailTo>
    {
    }

    public abstract class mailCc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.mailCc>
    {
    }

    public abstract class mailBcc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.mailBcc>
    {
    }

    public abstract class subject : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.subject>
    {
    }

    public abstract class mailUser : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.mailUser>
    {
    }

    public abstract class wikiText : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.wikiText>
    {
    }

    public abstract class wikiID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SendKBArticleMaint.EmailMessage.wikiID>
    {
    }
  }

  public sealed class ArticleSelectorAttribute : PXCustomSelectorAttribute
  {
    public PXSelectBase<WikiPage> _select;
    public SendKBArticleMaint _graph;

    public ArticleSelectorAttribute()
      : base(typeof (WikiPage.pageID))
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this._graph = sender.Graph as SendKBArticleMaint;
      if (this._graph == null)
        return;
      this._select = (PXSelectBase<WikiPage>) new PXSelect<WikiPage, Where<WikiPage.name, NotLike<TemplateLeftLike>, And<WikiPage.name, NotLike<GenTemplateLeftLike>, And<WikiPage.name, NotLike<ContainerTemplateLeftLike>, And<WikiPage.wikiID, Equal<Required<WikiPage.wikiID>>>>>>>((PXGraph) this._graph);
    }

    public IEnumerable GetRecords()
    {
      if (this._select != null)
      {
        PXSelectBase<WikiPage> select = this._select;
        object[] objArray = new object[1]
        {
          (object) ((PXSelectBase<SendKBArticleMaint.EmailMessage>) this._graph.Message).Current.WikiID
        };
        foreach (PXResult<WikiPage> pxResult in select.Select(objArray))
          yield return (object) PXResult<WikiPage>.op_Implicit(pxResult);
      }
    }
  }

  [Serializable]
  public class Article : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXGuid]
    [PXUIField(DisplayName = "Article")]
    [SendKBArticleMaint.ArticleSelector]
    public virtual Guid? ArticleID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Wiki Text", IsReadOnly = true)]
    public virtual string Content { get; set; }

    [PXString]
    [PXUIField(Visible = false)]
    public virtual string ParsedContent { get; set; }

    public abstract class articleID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SendKBArticleMaint.Article.articleID>
    {
    }

    public abstract class content : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.Article.content>
    {
    }

    public abstract class parsedContent : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SendKBArticleMaint.Article.parsedContent>
    {
    }
  }
}
