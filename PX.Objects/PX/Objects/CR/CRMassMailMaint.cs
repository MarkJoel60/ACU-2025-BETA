// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMassMailMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class CRMassMailMaint : PXGraph<
#nullable disable
CRMassMailMaint, CRMassMail>
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSetup<CRSetup> Setup;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.Contact> BaseContacts;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXViewName("Summary")]
  public PXSelect<CRMassMail> MassMails;
  [PXViewName("History")]
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  [PXViewDetailsButton]
  [PXViewDetailsButton(typeof (CRMassMail), typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CRSMEmail.contactID>>>>), ActionName = "History_Contact_ViewDetails")]
  [PXViewDetailsButton(typeof (CRMassMail), typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<CRSMEmail.bAccountID>>>>), ActionName = "History_BAccount_ViewDetails")]
  public FbqlSelect<SelectFromBase<CRSMEmail, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRMassMailMessage>.On<BqlOperand<
  #nullable enable
  CRMassMailMessage.messageID, IBqlGuid>.IsEqual<
  #nullable disable
  CRSMEmail.imcUID>>>>.Where<BqlOperand<
  #nullable enable
  CRMassMailMessage.massMailID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRMassMail.massMailID, IBqlInt>.AsOptional>>.Order<
  #nullable disable
  By<Asc<CRSMEmail.createdDateTime>>>, CRSMEmail>.View History;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CRMassMailMessage, Where<CRMassMailMessage.massMailID, Equal<Current<CRMassMail.massMailID>>>> SendedMessages;
  [PXViewName("Entity Fields")]
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<CacheEntityItem, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  [PXViewName("Mail Lists")]
  [PXViewDetailsButton(typeof (CRMarketingList))]
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CRMarketingList, LeftJoin<CRMassMailMarketingList, On<CRMassMailMarketingList.mailListID, Equal<CRMarketingList.marketingListID>, And<CRMassMailMarketingList.massMailID, Equal<Current<CRMassMail.massMailID>>>>>, Where<CRMarketingList.status, Equal<CRMarketingList.status.active>>, OrderBy<Asc<CRMarketingList.name>>> MailLists;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly2<CRMarketingList, LeftJoin<CRMassMailMarketingList, On<CRMassMailMarketingList.mailListID, Equal<CRMarketingList.marketingListID>, And<CRMassMailMarketingList.massMailID, Equal<Current<CRMassMail.massMailID>>>>>, Where<CRMarketingList.status, Equal<CRMarketingList.status.active>>, OrderBy<Asc<CRMarketingList.name>>> MailListsExt;
  [PXViewName("Campaigns")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (CRMarketingList))]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CRCampaign, LeftJoin<CRMassMailCampaign, On<CRMassMailCampaign.campaignID, Equal<CRCampaign.campaignID>, And<CRMassMailCampaign.massMailID, Equal<Current<CRMassMail.massMailID>>>>>, Where<CRCampaign.isActive, Equal<boolTrue>>, OrderBy<Asc<CRCampaign.campaignName>>> Campaigns;
  [PXViewName("Leads and Contacts")]
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, 
  #nullable disable
  Equal<PX.Objects.CR.Contact.bAccountID>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CR.BAccount.type, IBqlString>.IsNotEqual<
  #nullable disable
  BAccountType.employeeType>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.BAccount.defContactID, 
  #nullable disable
  Equal<PX.Objects.CR.Contact.contactID>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CR.BAccount.type, IBqlString>.IsEqual<
  #nullable disable
  BAccountType.employeeType>>>>>, FbqlJoins.Left<Address>.On<BqlOperand<
  #nullable enable
  Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.Contact.defAddressID>>>, FbqlJoins.Left<CRMassMailMember>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRMassMailMember.contactID, 
  #nullable disable
  Equal<PX.Objects.CR.Contact.contactID>>>>>.And<BqlOperand<
  #nullable enable
  CRMassMailMember.massMailID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRMassMail.massMailID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  FbqlJoins.Left<CRLead>.On<BqlOperand<
  #nullable enable
  CRLead.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.Contact.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Contact.noMassMail, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.noMassMail, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Contact.noEMail, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.noEMail, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Contact.noMarketing, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.noMarketing, IBqlBool>.IsNotEqual<
  #nullable disable
  True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.CR.BAccount.type, IBqlString>.IsIn<
  #nullable disable
  BAccountType.customerType, BAccountType.prospectType, BAccountType.combinedType, BAccountType.vendorType, BAccountType.employeeType>>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.CR.Contact.displayName, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.Asc>>, 
  #nullable disable
  PX.Objects.CR.Contact>.View Leads;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CRMassMailMarketingList, Where<CRMassMailMarketingList.massMailID, Equal<Required<CRMassMail.massMailID>>>> selectedMailList;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CRMassMailCampaign, Where<CRMassMailCampaign.massMailID, Equal<Required<CRMassMail.massMailID>>>> selectedCampaigns;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CRMassMailMember, Where<CRMassMailMember.massMailID, Equal<Required<CRMassMail.massMailID>>>> selectedLeads;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<CRMarketingListMember, On<CRMarketingListMember.contactID, Equal<PX.Objects.CR.Contact.contactID>>>> DynamicSourceList;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.Contact, Where<True, Equal<False>>> Contact;
  public PXFilter<CRMassMailPrepare> MassMailPrepare;
  public PXAction<CRMassMail> PreviewMail;
  public PXAction<CRMassMail> Send;
  public PXAction<CRMassMail> MessageDetails;
  public PXAction<SMEmail> ViewEntity;
  public PXAction<SMEmail> ViewDocument;

  protected virtual IEnumerable entityItems(string parent)
  {
    CRMassMailMaint crMassMailMaint = this;
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (ContactMaint));
    if (siteMapNode != null)
    {
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) crMassMailMaint, parent, (string) null, siteMapNode.GraphType, true).OfType<CacheEntityItem>())
      {
        if (cacheEntityItem.SubKey == typeof (PX.Objects.CR.Contact).FullName || parent == typeof (PX.Objects.CR.Contact).Name)
          yield return (object) cacheEntityItem;
      }
    }
  }

  protected virtual IEnumerable mailLists()
  {
    foreach (PXResult<CRMarketingList> pxResult in ((PXSelectBase<CRMarketingList>) this.MailListsExt).Select(Array.Empty<object>()))
    {
      CRMarketingList crMarketingList1 = (CRMarketingList) ((PXResult) pxResult)[typeof (CRMarketingList)];
      CRMassMailMarketingList mailMarketingList = (CRMassMailMarketingList) ((PXResult) pxResult)[typeof (CRMassMailMarketingList)];
      CRMarketingList crMarketingList2 = (CRMarketingList) ((PXSelectBase) this.MailLists).Cache.Locate((object) crMarketingList1);
      if (!crMarketingList1.Selected.GetValueOrDefault() && mailMarketingList.MailListID.HasValue)
        crMarketingList1.Selected = new bool?(true);
      if (crMarketingList2 != null)
      {
        bool? selected = crMarketingList2.Selected;
        ((PXSelectBase) this.MailLists).Cache.RestoreCopy((object) crMarketingList2, (object) crMarketingList1);
        crMarketingList2.Selected = selected;
        crMarketingList1 = crMarketingList2;
      }
      yield return (object) new PXResult<CRMarketingList>(crMarketingList1);
    }
  }

  protected virtual IEnumerable campaigns()
  {
    foreach (PXResult pxResult in GraphHelper.QuickSelect(((PXSelectBase) this.Campaigns).View))
    {
      CRCampaign crCampaign = (CRCampaign) pxResult[typeof (CRCampaign)];
      CRMassMailCampaign massMailCampaign = (CRMassMailCampaign) pxResult[typeof (CRMassMailCampaign)];
      if (!crCampaign.Selected.GetValueOrDefault() && massMailCampaign.CampaignID != null && ((PXSelectBase) this.Campaigns).Cache.GetStatus((object) crCampaign) != 1)
        crCampaign.Selected = new bool?(true);
      yield return (object) new PXResult<CRCampaign>(crCampaign);
    }
  }

  protected virtual IEnumerable leads()
  {
    foreach (PXResult pxResult in GraphHelper.QuickSelect(((PXSelectBase) this.Leads).View))
    {
      PX.Objects.CR.Contact contact = pxResult.GetItem<PX.Objects.CR.Contact>();
      CRMassMailMember crMassMailMember = pxResult.GetItem<CRMassMailMember>();
      if (!contact.Selected.GetValueOrDefault() && crMassMailMember.ContactID.HasValue && ((PXSelectBase) this.Leads).Cache.GetStatus((object) contact) != 1)
        contact.Selected = new bool?(true);
      yield return (object) new PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, Address, CRLead>(contact, pxResult.GetItem<PX.Objects.CR.BAccount>(), pxResult.GetItem<Address>(), pxResult.GetItem<CRLead>());
    }
  }

  protected virtual IEnumerable dynamicSourceList([PXInt] int mailListID)
  {
    return CRSubscriptionsSelect.Select((PXGraph) this, new int?(mailListID));
  }

  public CRMassMailMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<CRSetup>) this.Setup).Current.MassMailNumberingID))
      throw new PXSetPropertyException("Numbering ID is not configured in the CR setup.", new object[1]
      {
        (object) "Customer Management Preferences"
      });
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Campaigns).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<CRCampaign.selected>(((PXSelectBase) this.Campaigns).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CRCampaign.sendFilter>(((PXSelectBase) this.Campaigns).Cache, (object) null, true);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.fullName>(((PXSelectBase) this.Leads).Cache, "Account Name");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.BAccount.classID>(((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)], "Company Class");
    PXDBAttributeAttribute.Activate(((PXSelectBase) this.BaseContacts).Cache);
  }

  [PXSelector(typeof (PX.Objects.CR.Contact.contactID), DescriptionField = typeof (PX.Objects.CR.Contact.memberName))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRSMEmail.contactID> e)
  {
  }

  [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRSMEmail.bAccountID> e)
  {
  }

  [PXFormula(typeof (EntityDescription<CRSMEmail.documentNoteID>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRSMEmail.documentSource> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Marketing List")]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRMarketingList.mailListCode> e)
  {
  }

  [PXUIField]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXFieldDescription]
  [PXNavigateSelector(typeof (Search<PX.Objects.CR.Contact.displayName>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.displayName> e)
  {
  }

  [Owner(typeof (PX.Objects.CR.Contact.workgroupID))]
  [PXChildUpdatable(AutoRefresh = true, TextField = "AcctName", ShowHint = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.ownerID> e)
  {
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Contact Class")]
  [PXNavigateSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.classID> e)
  {
  }

  [PXDefault("((Contact.Email))")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRMassMail.mailTo> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRCampaign, CRCampaign.selected> e)
  {
    CRCampaign row = e.Row;
    if (row == null || (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CRCampaign, CRCampaign.selected>, CRCampaign, object>) e).OldValue || !row.Selected.Value)
      return;
    foreach (PXResult<CRCampaign> pxResult in ((PXSelectBase<CRCampaign>) this.Campaigns).Select(Array.Empty<object>()))
    {
      CRCampaign crCampaign = PXResult<CRCampaign>.op_Implicit(pxResult);
      if (crCampaign.Selected.GetValueOrDefault() && crCampaign != row)
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CRCampaign, CRCampaign.selected>>) e).Cache.SetValue<CRCampaign.selected>((object) crCampaign, (object) false);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CRCampaign, CRCampaign.selected>>) e).Cache.SetStatus((object) crCampaign, (PXEntryStatus) 1);
      }
    }
    ((PXSelectBase) this.Campaigns).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRMassMail> e)
  {
    CRMassMail row = e.Row;
    if (row == null)
      return;
    this.CorrectUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRMassMail>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRMassMailPrepare> e)
  {
    CRMassMailPrepare row = e.Row;
    if (row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRMassMailPrepare>>) e).Cache;
    CRMassMailPrepare crMassMailPrepare = row;
    CRMassMail current = ((PXSelectBase<CRMassMail>) this.MassMails).Current;
    int num = current != null ? (current.Source.GetValueOrDefault() == 1 ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<CRMassMailPrepare.campaignUpdateListMembers>(cache, (object) crMassMailPrepare, num != 0);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRMarketingList> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Contact> e)
  {
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCampaign> e) => e.Cancel = true;

  public virtual void Persist()
  {
    this.saveMailLists();
    this.saveCampaigns();
    this.saveLeads();
    ((PXGraph) this).Persist();
    this.CorrectUI(((PXSelectBase) this.MassMails).Cache, ((PXSelectBase<CRMassMail>) this.MassMails).Current);
  }

  [PXUIField(DisplayName = "Preview Message")]
  [PXButton]
  public virtual IEnumerable previewMail(PXAdapter a)
  {
    CRMassMailMaint graph = this;
    Recipient recipient = graph.EnumerateRecipientsForSending(true).FirstOrDefault<Recipient>();
    ((PXGraph) graph).Caches[typeof (PX.Objects.CR.Contact)].Current = recipient != null ? (object) recipient.Contact : throw new PXException("At least one recipient must be specified for the mass email.");
    CRMassMail copy = PXCache<CRMassMail>.CreateCopy(((PXSelectBase<CRMassMail>) graph.MassMails).Current);
    copy.MailAccountID = ((PXSelectBase<CRMassMail>) graph.MassMails).Current.MailAccountID ?? MailAccountManager.DefaultMailAccountID;
    copy.MailTo = MailAccountManager.GetDefaultEmailAccount(false)?.Address;
    copy.MailCc = (string) null;
    copy.MailBcc = (string) null;
    IEnumerable<CRSMEmail> crsmEmails = new Recipient(recipient.Contact, recipient.Format).GetSender((PXGraph) graph, copy).MailMessages();
    CRSMEmail crsmEmail = crsmEmails.FirstOrDefault<CRSMEmail>();
    crsmEmail.MPStatus = "DR";
    crsmEmail.ContactID = new int?();
    graph.AddSendedMessages(copy, crsmEmails);
    ((PXGraph) graph).Actions.PressSave();
    CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
    ((PXSelectBase<CRSMEmail>) instance.Message).Current = ((PXSelectBase<CRSMEmail>) instance.Message).Insert(crsmEmail);
    PXNoteAttribute.SetFileNotes(((PXSelectBase) instance.Message).Cache, (object) ((PXSelectBase<CRSMEmail>) instance.Message).Current, PXNoteAttribute.GetFileNotes(((PXSelectBase) graph.MassMails).Cache, (object) copy));
    ((PXSelectBase) instance.Message).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    yield return (object) ((PXSelectBase<CRMassMail>) graph.MassMails).Current;
  }

  [PXUIField(DisplayName = "Send")]
  [PXSendMailButton]
  public virtual IEnumerable send(PXAdapter a)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    CRMassMailMaint crMassMailMaint = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    crMassMailMaint.CheckFields(((PXSelectBase) crMassMailMaint.MassMails).Cache, (object) ((PXSelectBase<CRMassMail>) crMassMailMaint.MassMails).Current, typeof (CRMassMail.mailAccountID), typeof (CRMassMail.mailSubject), typeof (CRMassMail.mailTo), typeof (CRMassMail.plannedDate));
    crMassMailMaint.SendMails();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) ((PXSelectBase<CRMassMail>) crMassMailMaint.MassMails).Current;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public void MassMailsPrepare()
  {
    if (((PXSelectBase<CRMassMail>) this.MassMails).Current.Source.GetValueOrDefault() != 1)
      return;
    CampaignMaint instance = PXGraph.CreateInstance<CampaignMaint>();
    foreach (CRCampaign cRCampaign in GraphHelper.RowCast<CRCampaign>((IEnumerable) ((PXSelectBase<CRCampaign>) this.Campaigns).Select(Array.Empty<object>())).Where<CRCampaign>((Func<CRCampaign, bool>) (campaign => campaign.Selected.GetValueOrDefault())))
    {
      try
      {
        ((PXSelectBase<CRCampaign>) instance.Campaign).Current = cRCampaign;
        List<CRMarketingList> list = GraphHelper.RowCast<CRMarketingList>((IEnumerable) ((IEnumerable<CRMarketingListWithLinkToCRCampaign>) ((PXSelectBase<CRMarketingListWithLinkToCRCampaign>) instance.CampaignMarketingLists).SelectMain(Array.Empty<object>())).Where<CRMarketingListWithLinkToCRCampaign>((Func<CRMarketingListWithLinkToCRCampaign, bool>) (marketingList => marketingList.SelectedForCampaign.GetValueOrDefault()))).ToList<CRMarketingList>();
        instance.CampaignUpdateListMembers(cRCampaign, (IEnumerable<CRMarketingList>) list);
      }
      catch (Exception ex)
      {
        throw new PXException("An error occurred during the preparation of mass emails.", ex);
      }
    }
  }

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable messageDetails(PXAdapter a)
  {
    PXRedirectHelper.TryOpenPopup(((PXSelectBase) this.History).Cache, (object) ((PXSelectBase<CRSMEmail>) this.History).Current, string.Empty);
    yield return (object) ((PXSelectBase<CRMassMail>) this.MassMails).Current;
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  protected virtual IEnumerable viewEntity(PXAdapter adapter)
  {
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.History).Current;
    if (current != null)
      new EntityHelper((PXGraph) this).NavigateToRow(current.RefNoteID, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXButton]
  protected virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.History).Current;
    if (current != null)
      new EntityHelper((PXGraph) this).NavigateToRow(current.DocumentNoteID, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  protected virtual void CorrectUI(PXCache cache, CRMassMail row)
  {
    if (row == null)
      return;
    bool flag1 = row.Status != "S";
    PXUIFieldAttribute.SetEnabled<CRMassMail.massMailID>(((PXSelectBase) this.MassMails).Cache, (object) row);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailAccountID>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailSubject>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailTo>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailCc>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailBcc>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.mailContent>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.source>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.sourceType>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.plannedDate>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.status>(((PXSelectBase) this.MassMails).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRMassMail.sentDateTime>(((PXSelectBase) this.MassMails).Cache, (object) row, false);
    ((PXSelectBase) this.MailLists).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.Leads).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.Campaigns).Cache.AllowUpdate = flag1;
    bool flag2 = cache.GetStatus((object) row) != 2;
    ((PXAction) this.Send).SetEnabled(flag1 & flag2);
    ((PXAction) this.PreviewMail).SetEnabled(flag1 & flag2);
  }

  protected virtual void SendMails()
  {
    if (((PXSelectBase<CRMassMail>) this.MassMails).Current == null || ((PXSelectBase<CRMassMail>) this.MassMails).Current.Status == "S")
      return;
    if (((PXSelectBase<CRMassMailPrepare>) this.MassMailPrepare).AskExt(true) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMassMailMaint.\u003C\u003Ec__DisplayClass50_0 cDisplayClass500 = new CRMassMailMaint.\u003C\u003Ec__DisplayClass50_0();
      ((PXAction) this.Save).Press();
      PXCache<CRMassMail>.CreateCopy(((PXSelectBase<CRMassMail>) this.MassMails).Current);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass500.graph = this.CloneGraphState<CRMassMailMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass500, __methodptr(\u003CSendMails\u003Eb__0)));
    }
    else
    {
      ((PXCache) GraphHelper.Caches<CRLead>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<PX.Objects.CR.Contact>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<PX.Objects.CR.BAccount>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<PX.Objects.EP.EPEmployee>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<CRMarketingListMember>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<CRMarketingList>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<CRCampaign>((PXGraph) this)).Clear();
    }
  }

  protected virtual void ProcessMassMailEmails(
    CRMassMail massMail,
    IEnumerable<Recipient> recipients)
  {
    GraphHelper.EnsureCachePersistence((PXGraph) this, typeof (Note));
    foreach (Recipient recipient in recipients)
    {
      ((PXGraph) this).Caches[recipient.Entity.GetType()].SetStatus(recipient.Entity, (PXEntryStatus) 0);
      IEnumerable<CRSMEmail> messages;
      try
      {
        ((PXSelectBase<PX.Objects.CR.Contact>) this.Contact).Current = recipient.Contact;
        messages = recipient.GetSender((PXGraph) this, massMail).Send();
      }
      catch (Exception ex)
      {
        PXTrace.WriteError((Exception) new PXException("An error occurred during the mass processing of emails and the email has not been sent.", ex));
        continue;
      }
      this.AddSendedMessages(massMail, messages);
    }
    ((PXSelectBase<CRMassMail>) this.MassMails).Current = massMail;
    ((PXSelectBase<CRMassMail>) this.MassMails).Current.Status = "S";
    ((PXSelectBase<CRMassMail>) this.MassMails).Current.SentDateTime = new DateTime?(DateTime.Now);
    ((PXSelectBase<CRMassMail>) this.MassMails).UpdateCurrent();
    ((PXGraph) this).Actions.PressSave();
  }

  protected virtual void AddSendedMessages(CRMassMail massMail, IEnumerable<CRSMEmail> messages)
  {
    foreach (CRSMEmail message in messages)
      ((PXSelectBase<CRMassMailMessage>) this.SendedMessages).Insert(new CRMassMailMessage()
      {
        MassMailID = massMail.MassMailID,
        MessageID = message.ImcUID
      });
  }

  protected virtual List<Recipient> GetRecipientsForSendingDistinctByEmail()
  {
    return this.EnumerateRecipientsForSending(false).GroupBy<Recipient, string>((Func<Recipient, string>) (i => i.Contact.EMail)).Select<IGrouping<string, Recipient>, Recipient>((Func<IGrouping<string, Recipient>, Recipient>) (i => i.OrderByDescending<Recipient, int?>((Func<Recipient, int?>) (c => c.Contact.ContactPriority)).First<Recipient>())).ToList<Recipient>();
  }

  protected virtual IEnumerable<Recipient> EnumerateRecipientsForSending(bool isPreviewMailMode)
  {
    CRMassMailMaint crMassMailMaint = this;
    EntityHelper helper = new EntityHelper((PXGraph) crMassMailMaint);
    int? source1 = ((PXSelectBase<CRMassMail>) crMassMailMaint.MassMails).Current.Source;
    if (source1.HasValue)
    {
      IEnumerable<Recipient> source2;
      switch (source1.GetValueOrDefault())
      {
        case 0:
          source2 = crMassMailMaint.EnumerateRecipientsForMailList(helper);
          break;
        case 1:
          source2 = crMassMailMaint.EnumerateRecipientsForCampaign(helper);
          break;
        case 2:
          source2 = crMassMailMaint.EnumerateRecipientsForLeads(helper, isPreviewMailMode);
          if (isPreviewMailMode && !source2.Any<Recipient>())
          {
            source2 = crMassMailMaint.EnumerateRecipientsForLeads(helper, isPreviewMailMode);
            break;
          }
          break;
        default:
          yield break;
      }
      foreach (Recipient recipient in source2)
      {
        if (isPreviewMailMode || !string.IsNullOrEmpty(recipient.Contact.EMail))
          yield return recipient;
      }
    }
  }

  protected virtual IEnumerable<Recipient> EnumerateRecipientsForMailList(EntityHelper helper)
  {
    this.ResetView(((PXSelectBase) this.MailListsExt).View);
    foreach (CRMarketingList list in GraphHelper.RowCast<CRMarketingList>((IEnumerable) ((PXSelectBase<CRMarketingList>) this.MailLists).Select(Array.Empty<object>())).Where<CRMarketingList>((Func<CRMarketingList, bool>) (l => l.Selected.GetValueOrDefault())))
    {
      CRMarketingListMaint instance = PXGraph.CreateInstance<CRMarketingListMaint>();
      ((PXSelectBase<CRMarketingList>) instance.MailLists).Current = list;
      foreach (PXResult<CRMarketingListMember> pxResult in ((PXSelectBase<CRMarketingListMember>) instance.ListMembers).Select(Array.Empty<object>()))
      {
        PX.Objects.CR.Contact contact = ((PXResult) pxResult).GetItem<PX.Objects.CR.Contact>();
        CRMarketingListMember marketingListMember = ((PXResult) pxResult).GetItem<CRMarketingListMember>();
        CRLead lead = ((PXResult) pxResult).GetItem<CRLead>();
        if ((marketingListMember != null ? (!marketingListMember.IsSubscribed.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        {
          bool? noMassMail = contact.NoMassMail;
          bool? noEmail = contact.NoEMail;
          bool? nullable = noMassMail.GetValueOrDefault() || !noEmail.GetValueOrDefault() && !noMassMail.HasValue ? noMassMail : noEmail;
          bool? noMarketing = contact.NoMarketing;
          if (!(nullable.GetValueOrDefault() || !noMarketing.GetValueOrDefault() && !nullable.HasValue ? nullable : noMarketing).GetValueOrDefault())
            yield return Recipient.SmartCreate(helper, contact, (object) list, lead, marketingListMember?.Format ?? "H");
        }
      }
    }
  }

  protected virtual IEnumerable<Recipient> EnumerateRecipientsForCampaign(EntityHelper helper)
  {
    CRMassMailMaint crMassMailMaint1 = this;
    crMassMailMaint1.ResetView(((PXSelectBase) crMassMailMaint1.Campaigns).View);
    foreach (CRCampaign campaign in GraphHelper.RowCast<CRCampaign>((IEnumerable) ((PXSelectBase<CRCampaign>) crMassMailMaint1.Campaigns).Select(Array.Empty<object>())).Where<CRCampaign>((Func<CRCampaign, bool>) (c => c.Selected.GetValueOrDefault())))
    {
      ((PXSelectBase) crMassMailMaint1.Campaigns).Cache.Current = (object) campaign;
      CRMassMailMaint crMassMailMaint2 = crMassMailMaint1;
      object[] objArray = new object[2]
      {
        (object) campaign.CampaignID,
        (object) campaign.SendFilter
      };
      foreach (PXResult<PX.Objects.CR.Contact, CRCampaignMembers, CRLead> pxResult in ((IEnumerable<PXResult<PX.Objects.CR.Contact>>) PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRCampaignMembers>.On<BqlOperand<CRCampaignMembers.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>, FbqlJoins.Left<CRLead>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>.And<BqlOperand<CRLead.contactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRCampaignMembers.campaignID, Equal<P.AsString>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GDPR.ContactExt.pseudonymizationStatus, Equal<PXPseudonymizationStatusListAttribute.notPseudonymized>>>>>.Or<BqlOperand<PX.Objects.GDPR.ContactExt.pseudonymizationStatus, IBqlInt>.IsNull>>>>, And<BqlOperand<True, IBqlBool>.IsNotIn<PX.Objects.CR.Contact.noMassMail, PX.Objects.CR.Contact.noEMail, PX.Objects.CR.Contact.noMarketing>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofString>, NotEqual<SendFilterSourcesAttribute.neverSent>>>>>.Or<BqlOperand<CRCampaignMembers.emailSendCount, IBqlInt>.IsEqual<Zero>>>>>>.ReadOnly.Config>.Select((PXGraph) crMassMailMaint2, objArray)).AsEnumerable<PXResult<PX.Objects.CR.Contact>>().Cast<PXResult<PX.Objects.CR.Contact, CRCampaignMembers, CRLead>>())
      {
        PX.Objects.CR.Contact contact;
        CRCampaignMembers crCampaignMembers;
        CRLead lead;
        pxResult.Deconstruct(ref contact, ref crCampaignMembers, ref lead);
        yield return Recipient.SmartCreate(helper, contact, (object) campaign, lead);
      }
    }
  }

  protected virtual IEnumerable<Recipient> EnumerateRecipientsForLeads(
    EntityHelper helper,
    bool isPreviewMailMode)
  {
    if (isPreviewMailMode)
    {
      PX.Objects.CR.Contact selected = (PX.Objects.CR.Contact) null;
      using (IEnumerator<PX.Objects.CR.Contact> enumerator = ((PXSelectBase) this.Leads).Cache.Cached.OfType<PX.Objects.CR.Contact>().Where<PX.Objects.CR.Contact>((Func<PX.Objects.CR.Contact, bool>) (p => p.Selected.GetValueOrDefault())).GetEnumerator())
      {
        if (enumerator.MoveNext())
          selected = enumerator.Current;
      }
      if (selected == null)
      {
        using (IEnumerator<PX.Objects.CR.Contact> enumerator = ((PXSelectBase) this.Leads).Cache.Cached.OfType<PX.Objects.CR.Contact>().GetEnumerator())
        {
          if (enumerator.MoveNext())
            selected = enumerator.Current;
        }
      }
      if (selected != null)
        yield return Recipient.SmartCreate(helper, selected, lead: ((PXSelectBase) this.Leads).Cache.Cached.OfType<CRLead>().Where<CRLead>((Func<CRLead, bool>) (p =>
        {
          int? contactId1 = p.ContactID;
          int? contactId2 = selected.ContactID;
          return contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue;
        })).FirstOrDefault<CRLead>());
    }
    else
    {
      foreach ((PX.Objects.CR.Contact, CRLead) valueTuple in ((IEnumerable<PXResult<PX.Objects.CR.Contact>>) ((PXSelectBase<PX.Objects.CR.Contact>) this.Leads).Select(Array.Empty<object>())).ToList<PXResult<PX.Objects.CR.Contact>>().Select<PXResult<PX.Objects.CR.Contact>, (PX.Objects.CR.Contact, CRLead)>((Func<PXResult<PX.Objects.CR.Contact>, (PX.Objects.CR.Contact, CRLead)>) (i => (((PXResult) i).GetItem<PX.Objects.CR.Contact>(), ((PXResult) i).GetItem<CRLead>()))).Where<(PX.Objects.CR.Contact, CRLead)>((Func<(PX.Objects.CR.Contact, CRLead), bool>) (i => i.contact.Selected.GetValueOrDefault())))
        yield return Recipient.SmartCreate(helper, valueTuple.Item1, lead: valueTuple.Item2);
    }
  }

  protected virtual void ResetView(PXView view)
  {
    view.Cache.Current = (object) null;
    view.Clear();
  }

  protected virtual void saveLeads()
  {
    if (((PXSelectBase<CRMassMail>) this.MassMails).Current == null)
      return;
    int? nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    if (!nullable.HasValue)
      return;
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    int num = nullable.Value;
    ((PXSelectBase) this.selectedLeads).View.Clear();
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.Source;
    if (nullable.GetValueOrDefault() == 2)
    {
      foreach (PX.Objects.CR.Contact contact in ((PXSelectBase) this.Leads).Cache.Updated)
      {
        if (contact != null)
        {
          nullable = contact.ContactID;
          if (nullable.HasValue)
          {
            CRMassMailMember crMassMailMember = PXResultset<CRMassMailMember>.op_Implicit(PXSelectBase<CRMassMailMember, PXSelect<CRMassMailMember>.Config>.Search<CRMassMailMember.massMailID, CRMassMailMember.contactID>((PXGraph) this, (object) num, (object) contact.ContactID, Array.Empty<object>()));
            bool? selected = contact.Selected;
            if (!selected.GetValueOrDefault() && crMassMailMember != null)
              ((PXSelectBase<CRMassMailMember>) this.selectedLeads).Delete(crMassMailMember);
            selected = contact.Selected;
            if (selected.GetValueOrDefault() && crMassMailMember == null)
              ((PXSelectBase<CRMassMailMember>) this.selectedLeads).Insert(new CRMassMailMember()
              {
                MassMailID = new int?(num),
                ContactID = contact.ContactID
              });
          }
        }
      }
    }
    else
    {
      foreach (PXResult<CRMassMailMember> pxResult in ((PXSelectBase<CRMassMailMember>) this.selectedLeads).Select(new object[1]
      {
        (object) num
      }))
        ((PXSelectBase<CRMassMailMember>) this.selectedLeads).Delete(PXResult<CRMassMailMember>.op_Implicit(pxResult));
    }
  }

  protected virtual void saveCampaigns()
  {
    if (((PXSelectBase<CRMassMail>) this.MassMails).Current == null)
      return;
    int? nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    if (!nullable.HasValue)
      return;
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    int num = nullable.Value;
    ((PXSelectBase) this.selectedCampaigns).View.Clear();
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.Source;
    if (nullable.GetValueOrDefault() == 1)
    {
      foreach (CRCampaign crCampaign in ((PXSelectBase) this.Campaigns).Cache.Updated)
      {
        if (crCampaign != null && crCampaign.CampaignID != null)
        {
          CRMassMailCampaign massMailCampaign = PXResultset<CRMassMailCampaign>.op_Implicit(PXSelectBase<CRMassMailCampaign, PXSelect<CRMassMailCampaign>.Config>.Search<CRMassMailCampaign.massMailID, CRMassMailCampaign.campaignID>((PXGraph) this, (object) num, (object) crCampaign.CampaignID, Array.Empty<object>()));
          bool? selected = crCampaign.Selected;
          if (!selected.GetValueOrDefault() && massMailCampaign != null)
            ((PXSelectBase<CRMassMailCampaign>) this.selectedCampaigns).Delete(massMailCampaign);
          selected = crCampaign.Selected;
          if (selected.GetValueOrDefault())
          {
            if (massMailCampaign == null)
              ((PXSelectBase<CRMassMailCampaign>) this.selectedCampaigns).Insert(new CRMassMailCampaign()
              {
                MassMailID = new int?(num),
                CampaignID = crCampaign.CampaignID
              });
            else
              ((PXSelectBase<CRMassMailCampaign>) this.selectedCampaigns).Update(massMailCampaign);
          }
        }
      }
    }
    else
    {
      foreach (PXResult<CRMassMailCampaign> pxResult in ((PXSelectBase<CRMassMailCampaign>) this.selectedCampaigns).Select(new object[1]
      {
        (object) num
      }))
        ((PXSelectBase<CRMassMailCampaign>) this.selectedCampaigns).Delete(PXResult<CRMassMailCampaign>.op_Implicit(pxResult));
    }
  }

  protected virtual void saveMailLists()
  {
    if (((PXSelectBase<CRMassMail>) this.MassMails).Current == null)
      return;
    int? nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    if (!nullable.HasValue)
      return;
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.MassMailID;
    int num1 = nullable.Value;
    ((PXSelectBase) this.selectedMailList).View.Clear();
    nullable = ((PXSelectBase<CRMassMail>) this.MassMails).Current.Source;
    int num2 = 0;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
    {
      foreach (CRMarketingList crMarketingList in ((PXSelectBase) this.MailLists).Cache.Updated)
      {
        if (crMarketingList != null)
        {
          nullable = crMarketingList.MarketingListID;
          if (nullable.HasValue)
          {
            CRMassMailMarketingList mailMarketingList = PXResultset<CRMassMailMarketingList>.op_Implicit(PXSelectBase<CRMassMailMarketingList, PXSelect<CRMassMailMarketingList>.Config>.Search<CRMassMailMarketingList.massMailID, CRMassMailMarketingList.mailListID>((PXGraph) this, (object) num1, (object) crMarketingList.MarketingListID, Array.Empty<object>()));
            bool? selected = crMarketingList.Selected;
            if (!selected.GetValueOrDefault() && mailMarketingList != null)
              ((PXSelectBase<CRMassMailMarketingList>) this.selectedMailList).Delete(mailMarketingList);
            selected = crMarketingList.Selected;
            if (selected.GetValueOrDefault() && mailMarketingList == null)
              ((PXSelectBase<CRMassMailMarketingList>) this.selectedMailList).Insert(new CRMassMailMarketingList()
              {
                MassMailID = new int?(num1),
                MailListID = crMarketingList.MarketingListID
              });
          }
        }
      }
    }
    else
    {
      foreach (PXResult<CRMassMailMarketingList> pxResult in ((PXSelectBase<CRMassMailMarketingList>) this.selectedMailList).Select(new object[1]
      {
        (object) num1
      }))
        ((PXSelectBase<CRMassMailMarketingList>) this.selectedMailList).Delete(PXResult<CRMassMailMarketingList>.op_Implicit(pxResult));
    }
  }

  protected virtual void CheckFields(PXCache cache, object row, params System.Type[] fields)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>(fields.Length);
    foreach (System.Type field1 in fields)
    {
      object obj = cache.GetValue(row, field1.Name);
      if (obj == null || obj is string && string.IsNullOrEmpty(obj as string))
      {
        string str = PXMessages.LocalizeFormatNoPrefix("'{0}' may not be empty.", new object[1]
        {
          (object) (!(cache.GetValueExt(row, field1.Name) is PXFieldState valueExt) || string.IsNullOrEmpty(valueExt.DisplayName) ? field1.Name : valueExt.DisplayName)
        });
        string field2 = cache.GetField(field1);
        dictionary.Add(field2, str);
        PXUIFieldAttribute.SetError(cache, row, field2, str);
      }
    }
    if (dictionary.Count > 0)
      throw new PXOuterException(dictionary, ((object) this).GetType(), row, "{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
      {
        null,
        (object) cache.GetItemType().Name
      });
  }
}
