// Decompiled with JetBrains decompiler
// Type: PX.SM.Email.EmailAccountRawAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable disable
namespace PX.SM.Email;

[PXDBInt]
[PXInt]
[PXUIField(DisplayName = "Email Account")]
[PXAttributeFamily(typeof (PXEntityAttribute))]
public class EmailAccountRawAttribute : PXEntityAttribute
{
  public EmailAccountsToShowOptions EmailAccountsToShow { get; set; }

  public PXSelectorMode SelectorMode { get; set; } = PXSelectorMode.DisplayModeText;

  public System.Type ForeignReference { get; set; }

  public ReferenceBehavior ReferenceBehavior { get; set; } = ReferenceBehavior.SetNull;

  public bool AddRowLevelSecurity { get; set; } = true;

  public bool OnlyActive { get; set; } = true;

  public EmailAccountRawAttribute()
    : this(EmailAccountsToShowOptions.All, (System.Type) null)
  {
  }

  public EmailAccountRawAttribute(
    EmailAccountsToShowOptions emailAccountsToShow = EmailAccountsToShowOptions.All,
    System.Type additionalWhere = null)
  {
    this.EmailAccountsToShow = emailAccountsToShow;
    this._Attributes.Add((PXEventSubscriberAttribute) this.GetSelector(additionalWhere));
    this._SelAttrIndex = this._Attributes.Count - 1;
    this.AddRestrictors();
  }

  public EmailAccountRawAttribute(System.Type additionalWhere = null)
    : this(EmailAccountsToShowOptions.All, additionalWhere)
  {
  }

  protected virtual PXSelectorAttribute GetSelector(System.Type additionalWhere)
  {
    return new PXSelectorAttribute(this.CreateSelect(additionalWhere), new System.Type[4]
    {
      typeof (EMailAccount.address),
      typeof (EMailAccount.description),
      typeof (EMailAccount.emailAccountType),
      typeof (EMailAccount.replyAddress)
    })
    {
      DescriptionField = typeof (EMailAccount.displayEmailAddress),
      SelectorMode = this.SelectorMode,
      CacheGlobal = true,
      DirtyRead = true
    };
  }

  protected virtual System.Type CreateSelect(System.Type additionalWhere)
  {
    System.Type actual1;
    switch (this.EmailAccountsToShow)
    {
      case EmailAccountsToShowOptions.OnlyMine:
        actual1 = typeof (Where<EMailAccount.userID, Equal<Current<AccessInfo.userID>>>);
        break;
      case EmailAccountsToShowOptions.OnlySystem:
        actual1 = typeof (Where<EMailAccount.userID, PX.Data.IsNull>);
        break;
      case EmailAccountsToShowOptions.MineAndSystem:
        actual1 = typeof (Where<EMailAccount.userID, Equal<Current<AccessInfo.userID>>, Or<EMailAccount.userID, PX.Data.IsNull>>);
        break;
      default:
        actual1 = typeof (Where<PX.Data.True, Equal<PX.Data.True>>);
        break;
    }
    System.Type actual2 = additionalWhere;
    if ((object) actual2 == null)
      actual2 = typeof (Where<PX.Data.True, Equal<PX.Data.True>>);
    return BqlTemplate.OfCommand<Search<EMailAccount.emailAccountID, Where2<BqlPlaceholder.A, PX.Data.And<BqlPlaceholder.B>>>>.Replace<BqlPlaceholder.A>(actual2).Replace<BqlPlaceholder.B>(actual1).ToType();
  }

  protected virtual void AddRestrictors()
  {
    if (this.EmailAccountsToShow != EmailAccountsToShowOptions.All)
      this._Attributes.Add((PXEventSubscriberAttribute) this.GetEmailAccountRestriction(this.EmailAccountsToShow));
    if (this.AddRowLevelSecurity)
      this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<PX.Data.Match<Current<AccessInfo.userName>>>), "You do not have permission to access the account {0}.", new System.Type[1]
      {
        typeof (EMailAccount.description)
      })
      {
        ShowWarning = true
      });
    if (!this.OnlyActive)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<EMailAccount.isActive, Equal<PX.Data.True>>), "The email account is inactive.", Array.Empty<System.Type>())
    {
      ShowWarning = true
    });
  }

  protected virtual PXRestrictorAttribute GetEmailAccountRestriction(
    EmailAccountsToShowOptions emailAccountsToShow)
  {
    PXRestrictorAttribute accountRestriction;
    switch (emailAccountsToShow)
    {
      case EmailAccountsToShowOptions.OnlyMine:
        accountRestriction = new PXRestrictorAttribute(typeof (Where<EMailAccount.userID, Equal<Current<AccessInfo.userID>>>), "", Array.Empty<System.Type>())
        {
          ShowWarning = true
        };
        break;
      case EmailAccountsToShowOptions.OnlySystem:
        accountRestriction = new PXRestrictorAttribute(typeof (Where<EMailAccount.userID, PX.Data.IsNull>), "", Array.Empty<System.Type>())
        {
          ShowWarning = true
        };
        break;
      case EmailAccountsToShowOptions.MineAndSystem:
        accountRestriction = new PXRestrictorAttribute(typeof (Where<EMailAccount.userID, Equal<Current<AccessInfo.userID>>, Or<EMailAccount.userID, PX.Data.IsNull>>), "", Array.Empty<System.Type>())
        {
          ShowWarning = true
        };
        break;
      default:
        accountRestriction = new PXRestrictorAttribute(typeof (Where<PX.Data.True, Equal<PX.Data.True>>), string.Empty, Array.Empty<System.Type>());
        break;
    }
    return accountRestriction;
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    this.AddForeignReference();
  }

  protected virtual void AddForeignReference()
  {
    if (this.ForeignReference == (System.Type) null)
      return;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXForeignReferenceAttribute(this.ForeignReference, this.ReferenceBehavior));
  }

  public override void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    cache.Graph.FieldVerifying.AddHandler(cache.GetItemType(), this.FieldName, new PXFieldVerifying(this._EmailAccountID_FieldVerifying));
  }

  protected void _EmailAccountID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if ((EMailAccount) PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<PX.Data.Required<EMailAccount.emailAccountID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, e.NewValue) != null)
      e.Cancel = true;
    else
      this.SelectorAttribute?.FieldVerifying(sender, e);
  }
}
