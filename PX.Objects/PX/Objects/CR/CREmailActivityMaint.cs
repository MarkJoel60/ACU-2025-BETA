// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CREmailActivityMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PM;
using PX.Objects.SM;
using PX.Reports;
using PX.Reports.Mail;
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.CR;

public class CREmailActivityMaint : CRBaseActivityMaint<
#nullable disable
CREmailActivityMaint, CRSMEmail>
{
  private readonly CREmailActivityMaint.EmbeddedImagesExtractor _extractor;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRSMEmail.body)})]
  public PXSelect<CRSMEmail> Message;
  public PXSelect<PMTimeActivity> TAct;
  public PXSelect<CRSMEmail, Where<CRSMEmail.noteID, Equal<Current<CRSMEmail.noteID>>>> CurrentMessage;
  [PXHidden]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  public PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Current<CRSMEmail.noteID>>>> CRAct;
  [PXHidden]
  public PXSelect<SMEmail, Where<SMEmail.refNoteID, Equal<Current<CRSMEmail.noteID>>>> SMMail;
  public PMTimeActivityList<CRSMEmail> TimeActivity;
  [PXHidden]
  public PXSetup<CRSetup> crSetup;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> BaseContract;
  public PXSelect<PX.SM.Notification> Notification;
  public SPWikiCategoryMaint.PXSelectWikiFoldersTree Folders;
  public CREmailActivityMaint.PXEMailActivityDelete<CRSMEmail> Delete;
  public PXAction<CRSMEmail> Send;
  public PXAction<CRSMEmail> Forward;
  public PXAction<CRSMEmail> ReplyAll;
  public PXAction<CRSMEmail> Reply;
  public PXAction<CRSMEmail> ReplyInline;
  public PXAction<CRSMEmail> process;
  public PXAction<CRSMEmail> CancelSending;
  public PXAction<CRSMEmail> DownloadEmlFile;
  public PXMenuAction<CRSMEmail> Action;
  public PXAction<CRSMEmail> Create;
  public PXAction<CRSMEmail> Archive;
  public PXAction<CRSMEmail> RestoreArchive;
  public PXAction<CRSMEmail> Restore;
  private static readonly EPSetup EmptyEpSetup = new EPSetup();
  protected Dictionary<string, System.Type> GraphTypes = new Dictionary<string, System.Type>()
  {
    {
      "Event",
      typeof (EPEventMaint)
    },
    {
      "Task",
      typeof (CRTaskMaint)
    },
    {
      "Lead",
      typeof (LeadMaint)
    },
    {
      "Contact",
      typeof (ContactMaint)
    },
    {
      "Case",
      typeof (CRCaseMaint)
    },
    {
      "Opportunity",
      typeof (OpportunityMaint)
    },
    {
      "Expense Receipt",
      typeof (ExpenseClaimDetailEntry)
    }
  };

  [InjectDependency]
  public IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  public IReportDataBinder ReportDataBinder { get; private set; }

  [InjectDependency]
  private IOriginalMailProvider OriginalMailProvider { get; set; }

  [InjectDependency]
  internal IMessageProccessor MessageProcessor { get; private set; }

  [InjectDependency]
  private IMailSendProvider MailSendProvider { get; set; }

  public CREmailActivityMaint()
  {
    this.AddDynamicCreateActions();
    this.AddEmailActions();
    PXGraph.FieldVerifyingEvents fieldVerifying = ((PXGraph) this).FieldVerifying;
    System.Type type = typeof (UploadFile);
    string name = typeof (UploadFile.name).Name;
    CREmailActivityMaint emailActivityMaint = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) emailActivityMaint, __vmethodptr(emailActivityMaint, UploadFileNameFieldVerifying));
    fieldVerifying.AddHandler(type, name, pxFieldVerifying);
    CRCaseActivityHelper.Attach((PXGraph) this);
    this._extractor = ((PXGraph) this).GetExtension<CREmailActivityMaint.EmbeddedImagesExtractor>();
    PXUIFieldAttribute.SetRequired<CRSMEmail.subject>(((PXGraph) this).Caches[typeof (CRSMEmail)], false);
    ((PXSelectBase) this.Message).View.Answer = (WebDialogResult) 1;
  }

  [PXUIField]
  [PXSendMailButton]
  protected virtual IEnumerable send(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CREmailActivityMaint.\u003C\u003Ec__DisplayClass38_0 cDisplayClass380 = new CREmailActivityMaint.\u003C\u003Ec__DisplayClass38_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass380.adapter = adapter;
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.Message).Current;
    if (current == null)
      return (IEnumerable) new CRSMEmail[0];
    CRSMEmail[] crsmEmailArray = new CRSMEmail[1]{ current };
    if (current.MPStatus != "DR" && current.MPStatus != "FL")
      return (IEnumerable) crsmEmailArray;
    this.ValidateEmailFields(current);
    ((PXGraph) this).Actions.PressSave();
    CRSMEmail copy = (CRSMEmail) ((PXSelectBase) this.Message).Cache.CreateCopy((object) current);
    this.TryCorrectMailDisplayNames(copy);
    copy.MPStatus = "PP";
    copy.RetryCount = new int?(0);
    CRSMEmail crsmEmail = (CRSMEmail) ((PXSelectBase) this.Message).Cache.Update((object) copy);
    ((PXAction) this.Save).Press();
    PreferencesEmail emailPreferences = MailAccountManager.GetEmailPreferences();
    if ((emailPreferences != null ? (emailPreferences.SendUserEmailsImmediately.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380.graph = this.CloneGraphState<CREmailActivityMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass380, __methodptr(\u003Csend\u003Eb__0)));
    }
    return (IEnumerable) new CRSMEmail[1]{ crsmEmail };
  }

  [PXUIField]
  [PXForwardMailButton]
  protected void forward()
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) this.CreateTargetMail(((PXSelectBase<CRSMEmail>) this.Message).Current), true, "Forward");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXReplyMailButton]
  protected void replyAll()
  {
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.Message).Current;
    string mailAccountAddress = this.GetMailAccountAddress(current);
    PXRedirectHelper.TryRedirect((PXGraph) this.CreateTargetMail(current, this.GetReplyAllAddress(current, mailAccountAddress), this.GetReplyAllCCAddress(current, mailAccountAddress), this.GetReplyAllBCCAddress(current, mailAccountAddress)), (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXReplyMailButton(ClosePopup = true)]
  protected IEnumerable reply(PXAdapter adapter)
  {
    foreach (CRSMEmail oldMessage in adapter.Get())
    {
      PXRedirectHelper.TryRedirect((PXGraph) this.CreateTargetMail(oldMessage, this.GetReplyAddress(oldMessage)), (PXRedirectHelper.WindowMode) 3);
      yield return (object) oldMessage;
    }
  }

  [PXUIField]
  [PXReplyMailButton(ClosePopup = true)]
  protected IEnumerable replyInline(PXAdapter adapter)
  {
    foreach (CRSMEmail oldMessage in adapter.Get())
    {
      PXRedirectHelper.TryRedirect((PXGraph) this.CreateTargetMail(oldMessage, this.GetReplyAddress(oldMessage)), (PXRedirectHelper.WindowMode) 0);
      yield return (object) oldMessage;
    }
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public virtual CREmailActivityMaint CreateTargetMail(
    CRSMEmail oldMessage,
    string replyTo = null,
    string cc = null,
    string bcc = null)
  {
    CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
    CRSMEmail crsmEmail = ((PXSelectBase<CRSMEmail>) instance.Message).Insert();
    if (MailAccountManager.GetEmailAccountIfAllowed((PXGraph) this, oldMessage.MailAccountID) != null)
      crsmEmail.MailAccountID = oldMessage.MailAccountID;
    crsmEmail.RefNoteID = oldMessage.RefNoteID;
    crsmEmail.BAccountID = oldMessage.BAccountID;
    crsmEmail.ContactID = oldMessage.ContactID;
    crsmEmail.ResponseToNoteID = oldMessage.EmailNoteID;
    crsmEmail.IsIncome = new bool?(false);
    crsmEmail.Subject = CREmailActivityMaint.GetSubjectPrefix(oldMessage.Subject, replyTo == null);
    crsmEmail.MailTo = replyTo;
    crsmEmail.MailCc = cc;
    crsmEmail.MailBcc = bcc;
    crsmEmail.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica{CREmailActivityMaint.GetMessageIDAppendix((PXGraph) this, crsmEmail)}>";
    crsmEmail.Body = this.CreateReplyBody(oldMessage.MailFrom, oldMessage.MailTo, oldMessage.MailCc, oldMessage.Subject, oldMessage.Body, (oldMessage.StartDate ?? oldMessage.LastModifiedDateTime).Value);
    if (OwnerAttribute.BelongsToWorkGroup((PXGraph) this, oldMessage.WorkgroupID, ((PXGraph) this).Accessinfo.ContactID))
      crsmEmail.WorkgroupID = oldMessage.WorkgroupID;
    try
    {
      ((PXSelectBase<CRSMEmail>) instance.Message).Update(crsmEmail);
    }
    catch (PXSetPropertyException ex)
    {
      throw PXException.ExtractInner(PXException.ExtractInner((Exception) ex));
    }
    crsmEmail.ParentNoteID = oldMessage.ParentNoteID;
    crsmEmail.NoteID = PXNoteAttribute.GetNoteID<CRSMEmail.noteID>(((PXSelectBase) instance.Message).Cache, (object) crsmEmail);
    ((PXSelectBase<CRSMEmail>) instance.Message).Update(crsmEmail);
    this.CopyAttachments((PXGraph) instance, oldMessage, crsmEmail, replyTo != null);
    ((PXSelectBase) instance.Message).Cache.IsDirty = false;
    return instance;
  }

  [PXUIField]
  [PXButton]
  protected IEnumerable Process(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CREmailActivityMaint.\u003C\u003Ec__DisplayClass50_0 cDisplayClass500 = new CREmailActivityMaint.\u003C\u003Ec__DisplayClass50_0()
    {
      message = ((PXSelectBase<SMEmail>) this.SMMail).SelectSingle(Array.Empty<object>())
    };
    // ISSUE: reference to a compiler-generated field
    cDisplayClass500.message.RetryCount = MailAccountManager.GetEmailPreferences().RepeatOnErrorSending;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass500, __methodptr(\u003CProcess\u003Eb__0)));
    return adapter.Get();
  }

  [Obsolete("This method is obsolete. Please use ProcessEmailMessage instance method instead")]
  public static void ProcessMessage(SMEmail message)
  {
    PXGraph.CreateInstance<CREmailActivityMaint>().ProcessEmailMessage(message);
  }

  public virtual void ProcessEmailMessage(SMEmail message)
  {
    if (MailAccountManager.IsMailProcessingOff)
      throw new PXException("Mail processing is turned off.");
    if (message == null || !(message.MPStatus == "PP") && !(message.MPStatus == "FL"))
      return;
    if (message.IsIncome.GetValueOrDefault())
      this.MessageProcessor.Process((object) message);
    else
      this.MailSendProvider.SendMessage((object) message);
    if (!string.IsNullOrEmpty(message.Exception))
      throw new PXException(message.Exception);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable cancelSending(PXAdapter adapter)
  {
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.Message).Current;
    if (current != null && current.MPStatus == "PP")
    {
      CRSMEmail copy = (CRSMEmail) ((PXSelectBase) this.Message).Cache.CreateCopy((object) current);
      copy.MPStatus = "DR";
      ((PXSelectBase) this.Message).Cache.Update((object) copy);
      ((PXGraph) this).Actions.PressSave();
    }
    return (IEnumerable) adapter.Get<CRSMEmail>();
  }

  [PXUIField(DisplayName = "Download .eml file")]
  [PXButton(Tooltip = "Export as .eml file")]
  public virtual void downloadEmlFile()
  {
    SMEmail smEmail = ((PXSelectBase<SMEmail>) this.SMMail).SelectSingle(Array.Empty<object>());
    if (smEmail == null || !smEmail.IsIncome.GetValueOrDefault())
      return;
    Email mail = this.OriginalMailProvider.GetMail((object) smEmail);
    if (mail == null)
      throw new PXException("The specified email does not exist on the remote server. (It has probably been deleted.)");
    throw PXExportHandlerEml.GenerateException(mail);
  }

  [PXUIField(DisplayName = "Create")]
  [PXButton(MenuAutoOpen = true)]
  public virtual IEnumerable create(PXAdapter adapter, string type)
  {
    if (string.IsNullOrEmpty(type))
      return adapter.Get();
    PXGraph pxGraph = !(type == "Expense Receipt") || EmployeeMaint.GetCurrentEmployeeID((PXGraph) this).HasValue ? PXGraph.CreateInstance(this.GraphTypes[type]) : throw new PXException("An expense receipt can be created only for an employee. You are trying to create an expense receipt for the {0} user account, which is not associated with any employee.", new object[1]
    {
      (object) ((PXGraph) this).Accessinfo.DisplayName
    });
    PXCache primaryCache = GraphHelper.GetPrimaryCache(pxGraph);
    object obj1 = primaryCache.Insert();
    CRLead crLead = obj1 as CRLead;
    Contact contact = obj1 as Contact;
    CRCase crCase = obj1 as CRCase;
    CROpportunity crOpportunity = obj1 as CROpportunity;
    CRActivity crActivity1 = obj1 as CRActivity;
    IActivityDetailsExt implementation = pxGraph.FindImplementation<IActivityDetailsExt>();
    if (implementation == null)
      return adapter.Get();
    System.Type activityType = implementation.GetActivityType();
    PXCache cach1 = pxGraph.Caches[activityType];
    List<CRSMEmail> list = GraphHelper.RowCast<CRSMEmail>(adapter.Get()).ToList<CRSMEmail>();
    List<CRSMEmail> crsmEmailList = adapter.MassProcess ? list.Where<CRSMEmail>((Func<CRSMEmail, bool>) (e => e.Selected.GetValueOrDefault())).ToList<CRSMEmail>() : list;
    CRSMEmail crsmEmail1 = (CRSMEmail) null;
    PXView pxView = new PXView((PXGraph) this, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[7]
      {
        typeof (Select<,>),
        activityType,
        typeof (Where<,>),
        activityType.GetNestedType(typeof (CRActivity.noteID).Name),
        typeof (Equal<>),
        typeof (Required<>),
        activityType.GetNestedType(typeof (CRActivity.noteID).Name)
      })
    }));
    HashSet<CREmailActivityMaint.EmailAddress> source = new HashSet<CREmailActivityMaint.EmailAddress>();
    foreach (CRSMEmail crsmEmail2 in crsmEmailList)
    {
      if (crsmEmail1 == null)
        crsmEmail1 = crsmEmail2;
      CRActivity crActivity2 = (CRActivity) pxView.SelectSingle(new object[1]
      {
        (object) crsmEmail2.NoteID
      });
      int? nullable1;
      if (crActivity1 != null)
      {
        nullable1 = crActivity1.ClassID;
        int num = 0;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          nullable1 = crActivity1.ClassID;
          if (nullable1.GetValueOrDefault() != 1)
          {
            crActivity2.ContactID = crActivity1.ContactID;
            crActivity2.BAccountID = crActivity1.BAccountID;
            crActivity2.RefNoteID = crActivity1.RefNoteID;
            goto label_20;
          }
        }
      }
      crActivity2.RefNoteID = PXNoteAttribute.GetNoteID(primaryCache, obj1, EntityHelper.GetNoteField(primaryCache.GetItemType()));
      CRActivity crActivity3 = crActivity2;
      int? nullable2;
      if (crLead != null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else if (contact == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = contact.ContactID;
      crActivity3.ContactID = nullable2;
label_20:
      object obj2 = (object) crActivity2;
      if (activityType != ((object) crActivity2).GetType())
      {
        obj2 = cach1.CreateInstance();
        cach1.RestoreCopy(obj2, (object) crActivity2);
      }
      object obj3 = cach1.Update(obj2);
      if (crActivity1 != null)
      {
        crActivity1.Subject = crsmEmail1.Subject;
        primaryCache.Update((object) crActivity1);
        cach1.SetValue<CRActivity.parentNoteID>(obj3, (object) crActivity1.NoteID);
      }
      CREmailActivityMaint.EmailAddress names;
      if ((contact != null || crOpportunity != null) && source.Count <= 1 && (names = CREmailActivityMaint.ParseNames(crsmEmail2.MailFrom)) != null)
        source.Add(names);
    }
    if (contact != null && source.Count == 1)
    {
      contact.LastName = string.IsNullOrEmpty(source.ToArray<CREmailActivityMaint.EmailAddress>()[0].LastName) ? source.ToArray<CREmailActivityMaint.EmailAddress>()[0].Email : source.ToArray<CREmailActivityMaint.EmailAddress>()[0].LastName;
      contact.FirstName = source.ToArray<CREmailActivityMaint.EmailAddress>()[0].FirstName;
      contact.EMail = source.ToArray<CREmailActivityMaint.EmailAddress>()[0].Email;
      primaryCache.Update((object) contact);
    }
    if (crCase != null && crsmEmail1 != null)
    {
      crCase.Subject = crsmEmail1.Subject;
      crCase.Description = crsmEmail1.Body;
      primaryCache.Update((object) crCase);
    }
    if (crOpportunity != null && crsmEmail1 != null)
    {
      crOpportunity.Subject = crsmEmail1.Subject;
      crOpportunity.Details = crsmEmail1.Body;
      primaryCache.Update((object) crOpportunity);
      PXCache cach2 = pxGraph.Caches[typeof (CRContact)];
      if (cach2 != null && source.Count == 1)
      {
        if (pxGraph.Views["Opportunity_Contact"].SelectSingle(Array.Empty<object>()) is CRContact crContact)
        {
          crOpportunity.AllowOverrideContactAddress = new bool?(true);
          crContact.LastName = source.ToArray<CREmailActivityMaint.EmailAddress>()[0].LastName;
          crContact.FirstName = source.ToArray<CREmailActivityMaint.EmailAddress>()[0].FirstName;
          crContact.Email = source.ToArray<CREmailActivityMaint.EmailAddress>()[0].Email;
        }
        cach2.Update((object) crContact);
        primaryCache.Update((object) crOpportunity);
      }
    }
    if (!((PXGraph) this).IsContractBasedAPI)
      PXRedirectHelper.TryRedirect(pxGraph, !adapter.ExternalCall ? (PXRedirectHelper.WindowMode) 4 : (PXRedirectHelper.WindowMode) 3);
    pxGraph.Actions.PressSave();
    return (IEnumerable) crsmEmailList;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable archive(PXAdapter adapter)
  {
    CREmailActivityMaint emailActivityMaint = this;
    PXCache<CRSMEmail> cache = GraphHelper.Caches<CRSMEmail>((PXGraph) emailActivityMaint);
    foreach (CRSMEmail crsmEmail in adapter.Get().Cast<CRSMEmail>())
    {
      if (!crsmEmail.IsArchived.GetValueOrDefault())
      {
        crsmEmail.IsArchived = new bool?(true);
        PXDefaultAttribute.SetPersistingCheck<CRSMEmail.mailTo>((PXCache) cache, (object) crsmEmail, (PXPersistingCheck) 2);
        ((PXSelectBase<CRSMEmail>) emailActivityMaint.Message).Update(crsmEmail);
      }
      ((PXGraph) emailActivityMaint).Actions.PressSave();
      yield return (object) crsmEmail;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable restoreArchive(PXAdapter adapter)
  {
    CREmailActivityMaint emailActivityMaint = this;
    PXCache<CRSMEmail> cache = GraphHelper.Caches<CRSMEmail>((PXGraph) emailActivityMaint);
    foreach (CRSMEmail crsmEmail in adapter.Get().Cast<CRSMEmail>())
    {
      if (crsmEmail.IsArchived.GetValueOrDefault())
      {
        crsmEmail.IsArchived = new bool?(false);
        PXDefaultAttribute.SetPersistingCheck<CRSMEmail.mailTo>((PXCache) cache, (object) crsmEmail, (PXPersistingCheck) 2);
        ((PXSelectBase<CRSMEmail>) emailActivityMaint.Message).Update(crsmEmail);
      }
      ((PXGraph) emailActivityMaint).Actions.PressSave();
      yield return (object) crsmEmail;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable restore(PXAdapter adapter)
  {
    CREmailActivityMaint emailActivityMaint = this;
    // ISSUE: reference to a compiler-generated method
    foreach (CRSMEmail crsmEmail1 in adapter.Get().Cast<CRSMEmail>().Select<CRSMEmail, CRSMEmail>(new Func<CRSMEmail, CRSMEmail>(emailActivityMaint.\u003Crestore\u003Eb__65_0)))
    {
      CRSMEmail crsmEmail2 = crsmEmail1;
      if (crsmEmail1.MPStatus == "DL")
      {
        crsmEmail1.MPStatus = crsmEmail1.IsIncome.GetValueOrDefault() ? "PD" : "DR";
        crsmEmail2 = ((PXSelectBase<CRSMEmail>) emailActivityMaint.Message).Update(crsmEmail1);
      }
      ((PXGraph) emailActivityMaint).Actions.PressSave();
      yield return (object) crsmEmail2;
    }
  }

  public virtual void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      this.CorrectFileNames();
      transactionScope.Complete();
    }
  }

  [PXMergeAttributes]
  [EPProject(typeof (PMTimeActivity.ownerID), true, FieldClass = "PROJECT")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTimeActivity.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRSMEmail.subject> e)
  {
  }

  [PXUIField(DisplayName = "Created On")]
  [PXMergeAttributes]
  protected void _(PX.Data.Events.CacheAttached<CRSMEmail.startDate> e)
  {
  }

  [PXUIField(DisplayName = "Parent Activity", Enabled = false)]
  [PXMergeAttributes]
  protected virtual void CRSMEmail_ParentNoteID_CacheAttached(PXCache cache)
  {
  }

  [PXDefault]
  [PXMergeAttributes]
  protected void CRSMEmail_MailAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Incoming")]
  [PXMergeAttributes]
  protected void CRSMEmail_IsIncome_CacheAttached(PXCache sender)
  {
  }

  [CREmailSelector]
  [PXMergeAttributes]
  protected void CRSMEmail_MailTo_CacheAttached(PXCache sender)
  {
  }

  [CREmailSelector]
  [PXMergeAttributes]
  protected void CRSMEmail_MailCc_CacheAttached(PXCache sender)
  {
  }

  [CREmailSelector]
  [PXMergeAttributes]
  protected void CRSMEmail_MailBcc_CacheAttached(PXCache sender)
  {
  }

  [PXDefault("DR")]
  [PXMergeAttributes]
  protected virtual void CRSMEmail_MPStatus_CacheAttached(PXCache cache)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void CRSMEmail_Subject_CacheAttached(PXCache sender)
  {
  }

  [PXParent(typeof (Select<CRSMEmail, Where<CRSMEmail.noteID, Equal<Current<PMTimeActivity.refNoteID>>>>), ParentCreate = true)]
  [PXMergeAttributes]
  protected void PMTimeActivity_RefNoteID_CacheAttached(PXCache sender)
  {
  }

  [PXSubordinateGroupSelector]
  [PXMergeAttributes]
  protected void PMTimeActivity_WorkgroupID_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<PMTimeActivity.trackTime, Equal<True>>, ActivityStatusListAttribute.open, Case<Where<PMTimeActivity.released, Equal<True>>, ActivityStatusListAttribute.released, Case<Where<PMTimeActivity.approverID, IsNotNull>, ActivityStatusListAttribute.pendingApproval>>>, ActivityStatusListAttribute.completed>))]
  [PXMergeAttributes]
  protected void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.approvalStatus> e)
  {
  }

  [PXMergeAttributes]
  [ProjectTask(typeof (PMTimeActivity.projectID), "TA", DisplayName = "Project Task", AllowNull = true, DefaultNotClosedTask = true)]
  public void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.projectTaskID> args)
  {
  }

  protected virtual void CRSMEmail_Body_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    string str = MailAccountManager.AppendSignature(e.NewValue as string, (PXGraph) this, (MailAccountManager.SignatureOptions) 1);
    if (string.IsNullOrEmpty(str))
      return;
    e.NewValue = (object) PXRichTextConverter.NormalizeHtml(str);
  }

  protected virtual void CRSMEmail_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CRSMEmail row = (CRSMEmail) e.Row;
    if (row == null)
    {
      ((PXAction) this.Forward).SetEnabled(true);
      ((PXAction) this.Reply).SetEnabled(true);
      ((PXAction) this.ReplyAll).SetEnabled(true);
      ((PXAction) this.Forward).SetVisible(true);
      ((PXAction) this.Reply).SetVisible(true);
      ((PXAction) this.ReplyAll).SetVisible(true);
    }
    else
    {
      PMTimeActivity pmTimeActivity = this.TimeActivity.SelectSingle();
      PXCache cache1 = ((PXSelectBase) this.TimeActivity).Cache;
      bool flag1 = cache.GetStatus((object) row) == 2;
      if ((!string.IsNullOrEmpty(pmTimeActivity?.TimeCardCD) ? 1 : (pmTimeActivity != null ? (pmTimeActivity.Billed.GetValueOrDefault() ? 1 : 0) : 0)) != 0)
        PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      bool valueOrDefault = this.EPSetupCurrent.RequireTimes.GetValueOrDefault();
      PXDBDateAndTimeAttribute.SetTimeVisible<CRSMEmail.startDate>(cache, (object) row, true);
      PXDBDateAndTimeAttribute.SetTimeEnabled<CRSMEmail.startDate>(cache, (object) row, true);
      PXDBDateAndTimeAttribute.SetTimeVisible<CRSMEmail.endDate>(cache, (object) row, valueOrDefault && pmTimeActivity != null && pmTimeActivity.TrackTime.GetValueOrDefault());
      string str = (string) ((PXSelectBase) this.Message).Cache.GetValueOriginal<CRSMEmail.uistatus>((object) row) ?? "OP";
      bool? nullable1 = new bool?(((bool?) ((PXSelectBase) this.TimeActivity).Cache.GetValueOriginal<PMTimeActivity.trackTime>((object) pmTimeActivity)).GetValueOrDefault());
      if (str == "CD" && !nullable1.GetValueOrDefault())
        str = "OP";
      if (row.IsLocked.GetValueOrDefault())
        str = "CD";
      PXUIFieldAttribute.SetEnabled(cache, (object) row, row.MPStatus == "DR" || row.MPStatus == "FL");
      if (str == "OP")
        PXUIFieldAttribute.SetEnabled<CRSMEmail.isExternal>(cache, (object) row, true);
      else
        PXUIFieldAttribute.SetEnabled<CRSMEmail.isExternal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.parentNoteID>(cache, (object) row, ((PXGraph) this).IsContractBasedAPI);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.mpstatus>(cache, (object) row, ((PXGraph) this).IsContractBasedAPI);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.startDate>(cache, (object) row, ((PXGraph) this).IsContractBasedAPI);
      PXUIFieldAttribute.SetVisible<CRSMEmail.uistatus>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.type>(cache, (object) row, ((((PXGraph) this).IsContractBasedAPI ? 1 : (((PXGraph) this).IsImport ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.isPrivate>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<CRSMEmail.ownerID>(cache, (object) row, str == "OP" && row.MPStatus == "DR" && !row.IsLocked.GetValueOrDefault() || row.IsIncome.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<CRSMEmail.workgroupID>(cache, (object) row, str == "OP" && row.MPStatus == "DR" && !row.IsLocked.GetValueOrDefault() || row.IsIncome.GetValueOrDefault());
      bool? nullable2 = row.IsIncome;
      bool isIncome = (nullable2.GetValueOrDefault() ? 1 : 0) != 0;
      int? nullable3 = row.ImapUID;
      bool hasValue = nullable3.HasValue;
      PXCacheEx.AdjustUI(cache, (object) row).For<CRSMEmail.mailFrom>((System.Action<PXUIFieldAttribute>) (ui => ui.Visible = isIncome)).For<CRSMEmail.mailAccountID>((System.Action<PXUIFieldAttribute>) (ui => ui.Visible = !isIncome));
      ((PXAction) this.Create).SetEnabled(isIncome);
      ((PXAction) this.Create).SetVisible(isIncome);
      ((PXAction) this.Send).SetVisible(!isIncome && (row.MPStatus == "FL" || row.MPStatus == "DR"));
      ((PXAction) this.Send).SetEnabled(!string.IsNullOrEmpty(row.MailFrom));
      ((PXAction) this.DownloadEmlFile).SetVisible(isIncome);
      ((PXAction) this.DownloadEmlFile).SetEnabled(isIncome & hasValue);
      ((PXAction) this.CancelSending).SetVisible(!isIncome && row.MPStatus == "PP");
      ((PXAction) this.CancelSending).SetEnabled(!isIncome && row.MPStatus == "PP");
      PXAction<CRSMEmail> archive1 = this.Archive;
      nullable2 = row.IsArchived;
      bool flag2 = false;
      int num1 = nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue ? 1 : 0;
      ((PXAction) archive1).SetVisible(num1 != 0);
      PXAction<CRSMEmail> archive2 = this.Archive;
      nullable2 = row.IsArchived;
      bool flag3 = false;
      int num2 = nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue ? 1 : 0;
      ((PXAction) archive2).SetEnabled(num2 != 0);
      PXAction<CRSMEmail> restoreArchive1 = this.RestoreArchive;
      nullable2 = row.IsArchived;
      int num3 = nullable2.GetValueOrDefault() ? 1 : 0;
      ((PXAction) restoreArchive1).SetVisible(num3 != 0);
      PXAction<CRSMEmail> restoreArchive2 = this.RestoreArchive;
      nullable2 = row.IsArchived;
      int num4 = nullable2.GetValueOrDefault() ? 1 : 0;
      ((PXAction) restoreArchive2).SetEnabled(num4 != 0);
      ((PXAction) this.Restore).SetVisible(row.MPStatus == "DL");
      ((PXAction) this.Restore).SetEnabled(row.MPStatus == "DL");
      ((PXAction) this.process).SetVisible("Process", (PXSelectorAttribute.Select<CRSMEmail.mailAccountID>(cache, (object) row) as EMailAccount)?.EmailAccountType != "E");
      ((PXAction) this.process).SetEnabled(row.MPStatus == "PP" || isIncome && row.MPStatus == "FL");
      ((PXAction) this.Forward).SetEnabled(!flag1 && row.MPStatus != "DR" && row.MPStatus != "PP");
      ((PXAction) this.Reply).SetEnabled(!flag1 && row.MPStatus != "DR" && row.MPStatus != "PP");
      ((PXAction) this.ReplyAll).SetEnabled(!flag1 && row.MPStatus != "DR" && row.MPStatus != "PP");
      PXCache pxCache = cache;
      int num5;
      if (!((PXGraph) this).UnattendedMode && pmTimeActivity != null)
      {
        nullable2 = pmTimeActivity.TrackTime;
        if (nullable2.GetValueOrDefault())
        {
          num5 = row.MPStatus != "DL" ? 1 : 0;
          goto label_15;
        }
      }
      num5 = 0;
label_15:
      PXUIFieldAttribute.SetRequired<CRSMEmail.ownerID>(pxCache, num5 != 0);
      nullable2 = row.IsIncome;
      if (nullable2.GetValueOrDefault())
        this.MarkAs(cache, (CRActivity) row, ((PXGraph) this).Accessinfo.ContactID, 1);
      nullable3 = row.ClassID;
      if (nullable3.GetValueOrDefault() == -2)
      {
        PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
        PXUIFieldAttribute.SetEnabled<CRSMEmail.isPrivate>(cache, (object) row, true);
      }
      PXUIFieldAttribute.SetEnabled<CRActivity.refNoteID>(cache, (object) row, cache.GetValue<CRActivity.refNoteIDType>((object) row) != null || ((PXGraph) this).IsContractBasedAPI);
      PXUIFieldAttribute.SetEnabled<CRActivity.responseActivityNoteID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CRActivity.responseDueDateTime>(cache, (object) row, false);
      cache.RaiseExceptionHandling("entityDescription", (object) row, (object) null, string.IsNullOrEmpty(row.Exception) ? (Exception) null : (Exception) new PXSetPropertyException(row.Exception, (PXErrorLevel) 5));
    }
  }

  protected virtual void _(
    PX.Data.Events.CommandPreparing<CRSMEmail.entityDescription> e)
  {
    e.Cancel = true;
  }

  protected virtual void PMTimeActivity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PMTimeActivity row = (PMTimeActivity) e.Row;
    CRSMEmail current = ((PXSelectBase<CRSMEmail>) this.CurrentMessage).Current;
    if (current == null)
      return;
    bool flag1 = ProjectAttribute.IsPMVisible("TA");
    PXUIFieldAttribute.SetVisible<PMTimeActivity.trackTime>(cache, (object) null, !current.IsIncome.GetValueOrDefault());
    PXCache pxCache1 = cache;
    bool? nullable1;
    int num1;
    if (row != null)
    {
      nullable1 = row.TrackTime;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = current.IsIncome;
        num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    PXUIFieldAttribute.SetVisible<PMTimeActivity.approvalStatus>(pxCache1, (object) null, num1 != 0);
    PXCache pxCache2 = cache;
    int num2;
    if (row == null)
    {
      num2 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.timeSpent>(pxCache2, (object) null, num2 != 0);
    PXCache pxCache3 = cache;
    int num3;
    if (row == null)
    {
      num3 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.earningTypeID>(pxCache3, (object) null, num3 != 0);
    PXCache pxCache4 = cache;
    int num4;
    if (row == null)
    {
      num4 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.isBillable>(pxCache4, (object) null, num4 != 0);
    PXCache pxCache5 = cache;
    int num5;
    if (row == null)
    {
      num5 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num5 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.released>(pxCache5, (object) null, num5 != 0);
    PXCache pxCache6 = cache;
    int num6;
    if (row != null)
    {
      nullable1 = row.IsBillable;
      if (nullable1.GetValueOrDefault())
      {
        if (row == null)
        {
          num6 = 0;
          goto label_23;
        }
        nullable1 = row.TrackTime;
        num6 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_23;
      }
    }
    num6 = 0;
label_23:
    PXUIFieldAttribute.SetVisible<PMTimeActivity.timeBillable>(pxCache6, (object) null, num6 != 0);
    PXCache pxCache7 = cache;
    int num7;
    if (row != null)
    {
      nullable1 = row.IsBillable;
      if (nullable1.GetValueOrDefault())
      {
        if (row == null)
        {
          num7 = 0;
          goto label_29;
        }
        nullable1 = row.TrackTime;
        num7 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_29;
      }
    }
    num7 = 0;
label_29:
    PXUIFieldAttribute.SetVisible<PMTimeActivity.overtimeBillable>(pxCache7, (object) null, num7 != 0);
    PXCache pxCache8 = cache;
    int num8;
    if (row == null)
    {
      num8 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num8 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.approverID>(pxCache8, (object) null, num8 != 0);
    PXUIFieldAttribute.SetVisible<PMTimeActivity.overtimeSpent>(cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTimeActivity.overtimeBillable>(cache, (object) null, false);
    PXCache pxCache9 = cache;
    int num9;
    if (row == null)
    {
      num9 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num10 = flag1 ? 1 : 0;
    int num11 = num9 & num10;
    PXUIFieldAttribute.SetVisible<PMTimeActivity.projectID>(pxCache9, (object) null, num11 != 0);
    PXCache pxCache10 = cache;
    int num12;
    if (row == null)
    {
      num12 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num12 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num13 = flag1 ? 1 : 0;
    int num14 = num12 & num13;
    PXUIFieldAttribute.SetVisible<PMTimeActivity.certifiedJob>(pxCache10, (object) null, num14 != 0);
    PXCache pxCache11 = cache;
    int num15;
    if (row == null)
    {
      num15 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num15 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num16 = flag1 ? 1 : 0;
    int num17 = num15 & num16;
    PXUIFieldAttribute.SetVisible<PMTimeActivity.projectTaskID>(pxCache11, (object) null, num17 != 0);
    PXCache pxCache12 = cache;
    int num18;
    if (row == null)
    {
      num18 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num18 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num19 = flag1 ? 1 : 0;
    int num20 = num18 & num19;
    PXUIFieldAttribute.SetVisible<PMTimeActivity.costCodeID>(pxCache12, (object) null, num20 != 0);
    PXCache pxCache13 = cache;
    int num21;
    if (row == null)
    {
      num21 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num21 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num22 = flag1 ? 1 : 0;
    int num23 = num21 & num22;
    PXUIFieldAttribute.SetVisible<PMTimeActivity.labourItemID>(pxCache13, (object) null, num23 != 0);
    PXCache pxCache14 = cache;
    int num24;
    if (row == null)
    {
      num24 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num24 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.unionID>(pxCache14, (object) null, num24 != 0);
    PXCache pxCache15 = cache;
    int num25;
    if (row == null)
    {
      num25 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num25 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.workCodeID>(pxCache15, (object) null, num25 != 0);
    PXCache pxCache16 = cache;
    int num26;
    if (row == null)
    {
      num26 = 0;
    }
    else
    {
      nullable1 = row.TrackTime;
      num26 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<PMTimeActivity.shiftID>(pxCache16, (object) null, num26 != 0);
    if (row != null)
    {
      PXCache pxCache17 = cache;
      int? nullable2 = row.ProjectID;
      int? nullable3;
      int num27;
      if (nullable2.HasValue)
      {
        nullable2 = row.ProjectID;
        nullable3 = ProjectDefaultAttribute.NonProject();
        num27 = !(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) ? 1 : 0;
      }
      else
        num27 = 0;
      PXUIFieldAttribute.SetRequired<PMTimeActivity.projectTaskID>(pxCache17, num27 != 0);
      PXCache pxCache18 = cache;
      PMTimeActivity pmTimeActivity = row;
      nullable1 = row.TrackTime;
      int num28;
      if (nullable1.GetValueOrDefault())
      {
        nullable3 = row.ProjectID;
        if (nullable3.HasValue)
        {
          nullable3 = row.ProjectID;
          nullable2 = ProjectDefaultAttribute.NonProject();
          if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
          {
            num28 = 1;
            goto label_65;
          }
        }
      }
      num28 = 2;
label_65:
      PXDefaultAttribute.SetPersistingCheck<PMTimeActivity.projectTaskID>(pxCache18, (object) pmTimeActivity, (PXPersistingCheck) num28);
    }
    string str1 = (string) ((PXSelectBase) this.Message).Cache.GetValueOriginal<CRSMEmail.uistatus>((object) current) ?? "OP";
    bool? nullable4;
    ref bool? local = ref nullable4;
    nullable1 = (bool?) ((PXSelectBase) this.TimeActivity).Cache.GetValueOriginal<PMTimeActivity.trackTime>((object) row);
    int num29 = nullable1.GetValueOrDefault() ? 1 : 0;
    local = new bool?(num29 != 0);
    string str2 = (string) ((PXSelectBase) this.TimeActivity).Cache.GetValueOriginal<PMTimeActivity.approvalStatus>((object) row) ?? "OP";
    if (str1 == "CD" && !nullable4.GetValueOrDefault())
      str1 = "OP";
    nullable1 = current.IsLocked;
    if (nullable1.GetValueOrDefault())
      str1 = "CD";
    if (str1 != "OP")
    {
      PXCache pxCache19 = cache;
      PMTimeActivity pmTimeActivity = row;
      nullable1 = current.IsLocked;
      int num30 = !nullable1.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.trackTime>(pxCache19, (object) pmTimeActivity, num30 != 0);
    }
    int num31;
    if (string.IsNullOrEmpty(row?.TimeCardCD))
    {
      if (row == null)
      {
        num31 = 0;
      }
      else
      {
        nullable1 = row.Billed;
        num31 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num31 = 1;
    bool flag2 = num31 != 0;
    CREmailActivityMaint.PXEMailActivityDelete<CRSMEmail> delete = this.Delete;
    int num32;
    if (!flag2)
    {
      if (row == null)
      {
        num32 = 1;
      }
      else
      {
        nullable1 = row.Released;
        num32 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num32 = 0;
    ((PXAction) delete).SetEnabled(num32 != 0);
    if (row != null)
    {
      nullable1 = row.Released;
      if (nullable1.GetValueOrDefault())
        str2 = "CD";
    }
    if (str2 == "OP")
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXCache pxCache20 = cache;
      PMTimeActivity pmTimeActivity1 = row;
      int num33;
      if (!flag2)
      {
        if (row == null)
        {
          num33 = 0;
        }
        else
        {
          nullable1 = row.IsBillable;
          num33 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num33 = 0;
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(pxCache20, (object) pmTimeActivity1, num33 != 0);
      PXCache pxCache21 = cache;
      PMTimeActivity pmTimeActivity2 = row;
      int num34;
      if (!flag2)
      {
        if (row == null)
        {
          num34 = 0;
        }
        else
        {
          nullable1 = row.IsBillable;
          num34 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num34 = 0;
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeBillable>(pxCache21, (object) pmTimeActivity2, num34 != 0);
    }
    else
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXCache pxCache22 = cache;
    PMTimeActivity pmTimeActivity3 = row;
    int num35;
    if (row != null)
    {
      nullable1 = row.TrackTime;
      if (nullable1.GetValueOrDefault())
      {
        num35 = !flag2 ? 1 : 0;
        goto label_102;
      }
    }
    num35 = 0;
label_102:
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.approvalStatus>(pxCache22, (object) pmTimeActivity3, num35 != 0);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.released>(cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMTimeActivity> e)
  {
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PMTimeActivity>>) e).Cache, e.Row, new DateTime?(), e.Row.Date);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.date> e)
  {
    if (e.Row == null)
      return;
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.date>>) e).Cache, e.Row, (DateTime?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.date>, PMTimeActivity, object>) e).OldValue, (DateTime?) e.NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRSMEmail, CRSMEmail.refNoteID> e)
  {
    CRSMEmail row = e.Row;
    if (row == null || !row.IsIncome.GetValueOrDefault())
      return;
    row.Exception = (string) null;
  }

  protected virtual void CRSMEmail_OwnerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CRSMEmail row = (CRSMEmail) e.Row;
    if (row == null)
      return;
    int? ownerId = row.OwnerID;
    if (!ownerId.HasValue)
      return;
    ownerId = row.OwnerID;
    int? oldValue = (int?) e.OldValue;
    if (ownerId.GetValueOrDefault() == oldValue.GetValueOrDefault() & ownerId.HasValue == oldValue.HasValue || !row.IsIncome.GetValueOrDefault())
      return;
    this.MarkAs(sender, (CRActivity) row, row.OwnerID, 0);
  }

  protected virtual void CRSMEmail_MailAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CRSMEmail row))
      return;
    row.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica{CREmailActivityMaint.GetMessageIDAppendix((PXGraph) this, row)}>";
    row.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) this, row, true);
    row.MailReply = CREmailActivityMaint.FillMailReply((PXGraph) this, row);
  }

  protected virtual void CRSMEmail_UIStatus_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<CRSMEmail, CRSMEmail.entityDescription> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CRSMEmail, CRSMEmail.entityDescription>>) e).ReturnValue = (object) $"<html><body>{this.GetEntityDescription((CRActivity) e.Row)}</body></html>";
  }

  protected virtual void CRSMEmail_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is CRSMEmail row))
      return;
    row.ClassID = new int?(4);
    int? nullable = row.OwnerID;
    if (!nullable.HasValue)
    {
      int? currentOwnerId = EmployeeMaint.GetCurrentOwnerID((PXGraph) this);
      if (OwnerAttribute.BelongsToWorkGroup((PXGraph) this, row.WorkgroupID, currentOwnerId))
        row.OwnerID = currentOwnerId;
    }
    nullable = row.MailAccountID;
    bool? isIncome;
    if (!nullable.HasValue)
    {
      isIncome = row.IsIncome;
      if (!isIncome.GetValueOrDefault())
      {
        nullable = row.ClassID;
        if (nullable.GetValueOrDefault() != -2)
        {
          row.MailAccountID = this.GetDefaultAccountId(row.OwnerID);
          cache.RaiseFieldUpdated<CRSMEmail.mailAccountID>((object) row, (object) null);
        }
      }
    }
    isIncome = row.IsIncome;
    if (!isIncome.GetValueOrDefault())
    {
      nullable = row.ClassID;
      if (nullable.GetValueOrDefault() != -2)
        row.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) this, row, true);
    }
    row.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) this, row, true);
    row.MailReply = CREmailActivityMaint.FillMailReply((PXGraph) this, row);
    row.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica{CREmailActivityMaint.GetMessageIDAppendix((PXGraph) this, row)}>";
  }

  protected virtual void CRSMEmail_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    CRSMEmail row = e.Row as CRSMEmail;
    CRSMEmail oldRow = e.OldRow as CRSMEmail;
    if (row == null || oldRow == null)
      return;
    row.ClassID = new int?(4);
    bool? isIncome = row.IsIncome;
    int? nullable1;
    if (!isIncome.GetValueOrDefault())
    {
      nullable1 = row.ClassID;
      if (nullable1.GetValueOrDefault() != -2)
      {
        nullable1 = oldRow.OwnerID;
        int? ownerId = row.OwnerID;
        if (!(nullable1.GetValueOrDefault() == ownerId.GetValueOrDefault() & nullable1.HasValue == ownerId.HasValue))
          row.MailFrom = CREmailActivityMaint.FillMailFrom((PXGraph) this, row, true);
      }
    }
    isIncome = row.IsIncome;
    if (!isIncome.GetValueOrDefault())
    {
      int? nullable2 = row.ClassID;
      if (nullable2.GetValueOrDefault() != -2)
      {
        nullable2 = oldRow.OwnerID;
        nullable1 = row.OwnerID;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          goto label_10;
      }
    }
    nullable1 = row.MailAccountID;
    if (nullable1.HasValue)
      goto label_11;
label_10:
    row.MailAccountID = this.GetDefaultAccountId(row.OwnerID);
label_11:
    if (row.MessageId != null)
      return;
    row.MessageId = $"<{Guid.NewGuid().ToString()}_acumatica{CREmailActivityMaint.GetMessageIDAppendix((PXGraph) this, row)}>";
  }

  protected virtual void CRSMEmail_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
  }

  protected static void UpdateEmailBeforeDeleting(CREmailActivityMaint graph, CRSMEmail record)
  {
    graph.TryCorrectMailDisplayNames(record);
    record.MPStatus = "DL";
    record.UIStatus = "CL";
    record = ((PXSelectBase<CRSMEmail>) graph.Message).Update(record);
    foreach (System.Type type in ((PXSelectBase) graph.Message).Cache.BqlFields.Where<System.Type>((Func<System.Type, bool>) (f => f != typeof (CRSMEmail.mpstatus) && f != typeof (CRSMEmail.noteID) && f != typeof (CRSMEmail.emailNoteID) && f != typeof (CRSMEmail.uistatus) && f != typeof (CRActivity.noteID) && f != typeof (CRActivity.uistatus) && f != typeof (SMEmail.noteID) && f != typeof (CRSMEmail.emailLastModifiedByID) && f != typeof (CRSMEmail.emailLastModifiedByScreenID) && f != typeof (CRSMEmail.emailLastModifiedDateTime) && f != typeof (CRActivity.lastModifiedByID) && f != typeof (CRActivity.lastModifiedByScreenID) && f != typeof (CRActivity.lastModifiedDateTime))))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXGraph) graph).CommandPreparing.AddHandler(typeof (CRSMEmail), type.Name, CREmailActivityMaint.\u003C\u003Ec.\u003C\u003E9__96_1 ?? (CREmailActivityMaint.\u003C\u003Ec.\u003C\u003E9__96_1 = new PXCommandPreparing((object) CREmailActivityMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateEmailBeforeDeleting\u003Eb__96_1))));
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CRSMEmail> e)
  {
    CRSMEmail copy = (CRSMEmail) ((PXSelectBase) this.Message).Cache.CreateCopy((object) e.Row);
    if (copy.MPStatus != "DL")
    {
      CREmailActivityMaint.UpdateEmailBeforeDeleting(this, copy);
      throw new PXSetPropertyException("Mail Status set to Deleted", (PXErrorLevel) 3);
    }
    if (e.Row == null)
      return;
    PMTimeActivity current = this.TimeActivity.Current;
    if (current == null)
      return;
    if (current.Billed.GetValueOrDefault() || !string.IsNullOrEmpty(current.TimeCardCD))
    {
      ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<CRSMEmail>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 0);
      throw new PXException("Email cannot be deleted.");
    }
    ((PXSelectBase) this.TimeActivity).Cache.Delete((object) current);
  }

  protected virtual void UploadFileNameFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRSMEmail> e)
  {
    if (e.Row == null)
      return;
    CRSMEmail row = e.Row;
    if (e.Operation == 3 || !string.IsNullOrEmpty(row.Subject))
      return;
    ((PXSelectBase) this.Message).Cache.SetValueExt<CRSMEmail.subject>((object) row, (object) PXMessages.LocalizeNoPrefix("(No subject)"));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.trackTime> e)
  {
    PMTimeActivity row = e.Row;
    if (row == null || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    bool flag1 = !ProjectAttribute.IsPMVisible("TA");
    bool? trackTime = row.TrackTime;
    bool flag2 = false;
    if (!(trackTime.GetValueOrDefault() == flag2 & trackTime.HasValue | flag1))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.trackTime>>) e).Cache.SetValueExt<PMTimeActivity.projectID>((object) row, (object) ProjectDefaultAttribute.NonProject());
  }

  protected virtual void CRSMEmail_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    CRSMEmail row = (CRSMEmail) e.Row;
    PMTimeActivity pmTimeActivity = this.TimeActivity.SelectSingle();
    PXDefaultAttribute.SetPersistingCheck<CRSMEmail.ownerID>(cache, (object) row, ((PXGraph) this).UnattendedMode || pmTimeActivity == null || !pmTimeActivity.TrackTime.GetValueOrDefault() || !(row.MPStatus != "DL") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
    if (e.Operation != 2 || e.TranStatus != 1)
      return;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SMEmail>(new PXDataField[2]
    {
      new PXDataField(typeof (SMEmail.id).Name),
      (PXDataField) new PXDataFieldValue(typeof (SMEmail.noteID).Name, (object) row.EmailNoteID)
    }))
    {
      if (pxDataRecord == null)
        return;
      cache.SetValue<CRSMEmail.id>(e.Row, (object) pxDataRecord.GetInt32(0));
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.ownerID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.ownerID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.costCodeID>>) e).Cache.SetDefaultExt<PMTimeActivity.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.unionID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.certifiedJob>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMTimeActivity, PMTimeActivity.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.costCodeID> e)
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMTimeActivity, PMTimeActivity.costCodeID>, PMTimeActivity, object>) e).NewValue = (object) CostCodeAttribute.DefaultCostCode;
  }

  public static string FillMailReply(PXGraph graph, CRSMEmail message)
  {
    string defaultDisplayName = (string) null;
    string str = (string) null;
    if (message.MailAccountID.HasValue)
    {
      EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelectReadonly<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<CRSMEmail.mailAccountID>>>>.Config>.Select(graph, new object[1]
      {
        (object) message.MailAccountID
      }));
      if (emailAccount != null)
      {
        defaultDisplayName = emailAccount.Description.With<string, string>((Func<string, string>) (_ => _.Trim()));
        str = emailAccount.ReplyAddress.With<string, string>((Func<string, string>) (_ => _.Trim()));
        if (string.IsNullOrEmpty(str))
          str = emailAccount.Address.With<string, string>((Func<string, string>) (_ => _.Trim()));
        if (emailAccount.SenderDisplayNameSource == "A")
          return !string.IsNullOrEmpty(emailAccount.AccountDisplayName) ? new MailAddress(str, emailAccount.AccountDisplayName).ToString() : str;
      }
      else
      {
        MailAddress mailAddress;
        EmailParser.TryParse(message.MailFrom, ref mailAddress);
        str = mailAddress?.Address;
      }
    }
    return CREmailActivityMaint.GenerateBackAddress(graph, message, defaultDisplayName, str, true);
  }

  public static string FillMailFrom(PXGraph graph, CRSMEmail message, bool allowUseCurrentUser = false)
  {
    string defaultDisplayName = (string) null;
    string str = (string) null;
    if (message.MailAccountID.HasValue)
    {
      EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelectReadonly<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<CRSMEmail.mailAccountID>>>>.Config>.Select(graph, new object[1]
      {
        (object) message.MailAccountID
      }));
      MailAddress mailAddress;
      EmailParser.TryParse(message.MailFrom, ref mailAddress);
      defaultDisplayName = emailAccount != null ? emailAccount.Description.With<string, string>((Func<string, string>) (_ => _.Trim())) : (string) null;
      str = emailAccount != null ? emailAccount.Address.With<string, string>((Func<string, string>) (_ => _.Trim())) : mailAddress?.Address;
      if (emailAccount != null && emailAccount.SenderDisplayNameSource == "A")
        return !string.IsNullOrEmpty(emailAccount.AccountDisplayName) ? new MailAddress(str, emailAccount.AccountDisplayName).ToString() : str;
    }
    return CREmailActivityMaint.GenerateBackAddress(graph, message, defaultDisplayName, str, allowUseCurrentUser);
  }

  private static string GenerateBackAddress(
    PXGraph graph,
    CRSMEmail message,
    string defaultDisplayName,
    string defaultAddress,
    bool allowUseCurrentUser)
  {
    string str1 = (string) null;
    if (message != null)
    {
      if (!message.OwnerID.HasValue || message.ClassID.GetValueOrDefault() == -2)
        return !string.IsNullOrEmpty(defaultDisplayName) ? new MailAddress(defaultAddress, defaultDisplayName).ToString() : defaultAddress;
      PXResultset<Contact> pxResultset = PXSelectBase<Contact, PXSelectReadonly2<Contact, LeftJoin<Users, On<Users.pKID, Equal<Contact.userID>>>, Where<Contact.contactID, Equal<Required<CRSMEmail.ownerID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) message.OwnerID
      });
      if (pxResultset == null || pxResultset.Count == 0)
        return defaultAddress;
      Contact contact = (Contact) ((PXResult) pxResultset[0])[typeof (Contact)];
      Users users = (Users) ((PXResult) pxResultset[0])[typeof (Users)];
      string displayName = (string) null;
      string address = defaultAddress;
      if (users != null && users.PKID.HasValue)
      {
        string str2 = users.FullName.With<string, string>((Func<string, string>) (_ => _.Trim()));
        if (!string.IsNullOrEmpty(str2))
          displayName = str2;
      }
      if (contact != null && contact.BAccountID.HasValue)
      {
        string str3 = contact.DisplayName.With<string, string>((Func<string, string>) (_ => _.Trim()));
        if (!string.IsNullOrEmpty(str3))
          displayName = str3;
      }
      str1 = string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(address) ? address : new MailAddress(address, displayName).ToString();
    }
    return string.IsNullOrEmpty(str1) & allowUseCurrentUser ? graph.Accessinfo.UserID.With<Guid, Users>((Func<Guid, Users>) (id => PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelect<Users>.Config>.Search<Users.pKID>(graph, (object) id, Array.Empty<object>())))).With<Users, string>((Func<Users, string>) (u => PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(u.Email, u.FullName))) : str1;
  }

  private static object[] GetKeys(object e, PXCache cache)
  {
    List<object> objectList = new List<object>();
    foreach (System.Type bqlKey in cache.BqlKeys)
      objectList.Add(cache.GetValue(e, bqlKey.Name));
    return objectList.ToArray();
  }

  public virtual void ValidateEmailFields(CRSMEmail row)
  {
    ((PXSelectBase) this.Message).Cache.RaiseExceptionHandling<CRSMEmail.mailAccountID>((object) row, (object) null, (Exception) null);
    if (!row.MailAccountID.HasValue)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException("'{0}' cannot be empty.");
      ((PXSelectBase) this.Message).Cache.RaiseExceptionHandling<CRSMEmail.mailAccountID>((object) row, (object) null, (Exception) propertyException);
      PXUIFieldAttribute.SetError<CRSMEmail.mailAccountID>(((PXSelectBase) this.Message).Cache, (object) row, ((Exception) propertyException).Message);
      throw propertyException;
    }
    ((PXSelectBase) this.Message).Cache.RaiseExceptionHandling<CRSMEmail.mailTo>((object) row, (object) null, (Exception) null);
    if (string.IsNullOrWhiteSpace(row.MailTo) && string.IsNullOrWhiteSpace(row.MailCc) && string.IsNullOrWhiteSpace(row.MailBcc))
      throw new PXSetPropertyException("At least one recipient must be specified for the Email activity.");
  }

  public virtual void AddDynamicCreateActions()
  {
    foreach (KeyValuePair<string, string> keyValuePair in (PXAccess.FeatureInstalled<FeaturesSet.customerModule>() ? (PXStringListAttribute) new CREmailActivityMaint.EntityList() : (PXStringListAttribute) new CREmailActivityMaint.EntityListSimple()).ValueLabelDic)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CREmailActivityMaint.\u003C\u003Ec__DisplayClass111_0 displayClass1110 = new CREmailActivityMaint.\u003C\u003Ec__DisplayClass111_0();
      // ISSUE: reference to a compiler-generated field
      displayClass1110.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      displayClass1110.source = keyValuePair;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      this.AddAction((PXGraph) this, displayClass1110.source.Value, displayClass1110.source.Value, true, new PXButtonDelegate((object) displayClass1110, __methodptr(\u003CAddDynamicCreateActions\u003Eb__0)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false,
        OnClosingPopup = (PXSpecialButtonType) 4,
        Category = "Create"
      });
    }
  }

  public virtual void AddEmailActions()
  {
    ((PXAction) this.Action).AddMenuAction((PXAction) this.Forward);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.process);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.DownloadEmlFile);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.Archive);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.RestoreArchive);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.Restore);
  }

  public virtual PXAction AddAction(
    PXGraph graph,
    string actionName,
    string displayName,
    bool visible,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] defaultAttributes)
  {
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = displayName,
      MapEnableRights = (PXCacheRights) 1
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute
    };
    if (defaultAttributes != null)
      subscriberAttributeList.AddRange(((IEnumerable<PXEventSubscriberAttribute>) defaultAttributes).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr => attr != null)));
    PXNamedAction<CRSMEmail> pxNamedAction = new PXNamedAction<CRSMEmail>(graph, actionName, handler, subscriberAttributeList.ToArray());
    graph.Actions[actionName] = (PXAction) pxNamedAction;
    return (PXAction) pxNamedAction;
  }

  private int? GetDefaultAccountId(int? owner)
  {
    if (owner.HasValue)
    {
      int? defaultMailAccountId = MailAccountManager.GetDefaultMailAccountID(new int?(owner.Value), true);
      if (defaultMailAccountId.HasValue && MailAccountManager.GetEmailAccountIfAllowed((PXGraph) this, defaultMailAccountId) != null)
        return defaultMailAccountId;
    }
    return MailAccountManager.GetEmailAccountIfAllowed((PXGraph) this, MailAccountManager.DefaultAnyMailAccountID) == null ? new int?() : MailAccountManager.DefaultAnyMailAccountID;
  }

  public virtual string GetReplyAddress(CRSMEmail oldMessage)
  {
    List<MailAddress> mailAddressList = new List<MailAddress>();
    if (oldMessage.MailReply != null)
    {
      foreach (MailAddress address in EmailParser.ParseAddresses(oldMessage.MailReply))
      {
        MailAddress item = address;
        string displayName = (string) null;
        if (string.IsNullOrWhiteSpace(item.DisplayName) && oldMessage.MailFrom != null)
        {
          foreach (MailAddress mailAddress in EmailParser.ParseAddresses(oldMessage.MailCc).Where<MailAddress>((Func<MailAddress, bool>) (_ => string.Equals(_.Address, item.Address, StringComparison.OrdinalIgnoreCase))))
            displayName = mailAddress.DisplayName;
        }
        else
          displayName = item.DisplayName;
        MailAddress mailAddress1 = new MailAddress(item.Address, displayName);
        mailAddressList.Add(mailAddress1);
      }
    }
    if (mailAddressList.Count == 0 && oldMessage.MailFrom != null)
    {
      foreach (MailAddress address in EmailParser.ParseAddresses(oldMessage.MailFrom))
        mailAddressList.Add(new MailAddress(address.Address, address.DisplayName));
    }
    return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) mailAddressList);
  }

  public string GetMailAccountAddress(CRSMEmail oldMessage)
  {
    return oldMessage.MailAccountID.With<int?, EMailAccount>((Func<int?, EMailAccount>) (_ => PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) _.Value
    })))).With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address));
  }

  internal static string GetMessageIDAppendix(PXGraph graph, CRSMEmail message)
  {
    if (!message.MailAccountID.HasValue)
      return "";
    string str = message.MailAccountID.With<int?, EMailAccount>((Func<int?, EMailAccount>) (_ => PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) _.Value
    })))).With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.OutcomingHostName));
    return str == null ? "" : "@" + str;
  }

  public virtual string GetReplyAllCCAddress(CRSMEmail oldMessage, string mailAccountAddress)
  {
    List<MailAddress> mailAddressList = new List<MailAddress>();
    if (oldMessage.MailCc != null)
    {
      foreach (MailAddress mailAddress in EmailParser.ParseAddresses(oldMessage.MailCc).Where<MailAddress>((Func<MailAddress, bool>) (_ => !string.Equals(_.Address, mailAccountAddress, StringComparison.OrdinalIgnoreCase))))
        mailAddressList.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName));
    }
    return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) mailAddressList);
  }

  public virtual string GetReplyAllBCCAddress(CRSMEmail oldMessage, string mailAccountAddress)
  {
    List<MailAddress> mailAddressList = new List<MailAddress>();
    if (oldMessage.MailBcc != null)
    {
      foreach (MailAddress mailAddress in EmailParser.ParseAddresses(oldMessage.MailBcc).Where<MailAddress>((Func<MailAddress, bool>) (_ => !string.Equals(_.Address, mailAccountAddress, StringComparison.OrdinalIgnoreCase))))
        mailAddressList.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName));
    }
    return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) mailAddressList);
  }

  public virtual string GetReplyAllAddress(CRSMEmail oldMessage, string mailAccountAddress)
  {
    List<MailAddress> mailAddressList = new List<MailAddress>();
    if (oldMessage.MailReply != null)
    {
      foreach (MailAddress address in EmailParser.ParseAddresses(oldMessage.MailReply))
      {
        MailAddress item = address;
        string displayName = (string) null;
        if (string.IsNullOrEmpty(item.DisplayName) && oldMessage.MailFrom != null)
        {
          foreach (MailAddress mailAddress in EmailParser.ParseAddresses(oldMessage.MailTo).Where<MailAddress>((Func<MailAddress, bool>) (_ => string.Equals(_.Address, item.Address, StringComparison.OrdinalIgnoreCase))))
            displayName = mailAddress.DisplayName;
        }
        else
          displayName = item.DisplayName;
        mailAddressList.Add(new MailAddress(item.Address, displayName));
      }
    }
    if (mailAddressList.Count == 0 && oldMessage.MailFrom != null)
    {
      foreach (MailAddress address in EmailParser.ParseAddresses(oldMessage.MailFrom))
        mailAddressList.Add(new MailAddress(address.Address, address.DisplayName));
    }
    if (oldMessage.MailTo != null)
    {
      foreach (MailAddress mailAddress in EmailParser.ParseAddresses(oldMessage.MailTo).Where<MailAddress>((Func<MailAddress, bool>) (_ => !string.Equals(_.Address, mailAccountAddress, StringComparison.OrdinalIgnoreCase))))
        mailAddressList.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName));
    }
    return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) mailAddressList);
  }

  private static string GetSubjectPrefix(string subject, bool forward)
  {
    if (subject != null)
    {
      bool flag;
      do
      {
        flag = false;
        if (subject.ToUpper().StartsWith("RE: ") || subject.ToUpper().StartsWith("FW: "))
        {
          subject = subject.Substring(4);
          flag = true;
        }
        if (subject.ToUpper().StartsWith(PXMessages.LocalizeNoPrefix("RE: ").ToUpper()))
        {
          subject = subject.Substring("RE: ".Length);
          flag = true;
        }
        if (subject.ToUpper().StartsWith(PXMessages.LocalizeNoPrefix("FW: ").ToUpper()))
        {
          subject = subject.Substring("FW: ".Length);
          flag = true;
        }
      }
      while (flag);
    }
    return (forward ? "FW: " : "RE: ") + subject;
  }

  protected virtual string CreateReplyBody(
    string mailFrom,
    string mailTo,
    string subject,
    string message,
    DateTime lastModifiedDateTime)
  {
    return this.CreateReplyBody(mailFrom, mailTo, (string) null, subject, message, lastModifiedDateTime);
  }

  private string CreateReplyBody(
    string mailFrom,
    string mailTo,
    string mailCc,
    string subject,
    string message,
    DateTime lastModifiedDateTime)
  {
    string str = $"<br/><br/><div class=\"wiki\" style=\"border-top:solid 1px black;padding:2px 0px;line-height:1.5em;\">\r\n<b>From:</b> {mailFrom}<br/>\r\n<b>Sent:</b> {lastModifiedDateTime.ToString()}<br/>\r\n<b>To:</b> {mailTo}{(string.IsNullOrEmpty(mailCc) ? "" : "<br/>\r\n<b>Cc:</b> " + mailCc)}<br/>\r\n<b>Subject:</b> {subject}<br/><br/>\r\n</div>";
    return PXRichTextConverter.NormalizeHtml(MailAccountManager.GetSignature((PXGraph) this, (MailAccountManager.SignatureOptions) 0) + str + message);
  }

  private void TryCorrectMailDisplayNames(CRSMEmail message)
  {
    if (!CREmailActivityMaint.TryCorrectMailDisplayNames((PXGraph) this, message))
      return;
    ((PXGraph) this).Caches[((object) message).GetType()].Update((object) message);
  }

  internal static bool TryCorrectMailDisplayNames(PXGraph graph, CRSMEmail message)
  {
    string str1 = CREmailActivityMaint.FillMailFrom(graph, message);
    string str2 = (string) null;
    MailAddress mailAddress1;
    if (str1 != null && EmailParser.TryParse(str1, ref mailAddress1))
      str2 = mailAddress1.DisplayName;
    if (str2 == null)
      str2 = ((PXResult) ((IQueryable<PXResult<EMailAccount>>) PXSelectBase<EMailAccount, PXSelect<EMailAccount>.Config>.Search<EMailAccount.emailAccountID>(graph, (object) message.MailAccountID, Array.Empty<object>())).FirstOrDefault<PXResult<EMailAccount>>())?.GetItem<EMailAccount>()?.Description;
    bool flag = false;
    string mailFrom = message.MailFrom;
    if (!string.IsNullOrEmpty(mailFrom) && EmailParser.TryParse(mailFrom, ref mailAddress1))
    {
      message.MailFrom = new MailAddress(mailAddress1.Address, str2).ToString();
      flag = true;
    }
    string mailReply = message.MailReply;
    MailAddress mailAddress2;
    if (!string.IsNullOrEmpty(mailReply) && EmailParser.TryParse(mailReply, ref mailAddress2) && !object.Equals((object) mailAddress2.DisplayName, (object) str2))
    {
      message.MailReply = new MailAddress(mailAddress2.Address, str2).ToString();
      flag = true;
    }
    return flag;
  }

  private void CorrectFileNames()
  {
    Guid? nullable1 = ((PXSelectBase<CRSMEmail>) this.Message).Current.With<CRSMEmail, Guid?>((Func<CRSMEmail, Guid?>) (m => m.NoteID));
    Guid? nullable2 = ((PXSelectBase<CRSMEmail>) this.Message).Current.With<CRSMEmail, Guid?>((Func<CRSMEmail, Guid?>) (act => act.NoteID));
    if (!nullable1.HasValue || !nullable2.HasValue)
      return;
    string oldValue = $"[{((PXSelectBase<CRSMEmail>) this.Message).Current.MessageId}]";
    string newValue = $"[{((PXSelectBase<CRSMEmail>) this.Message).Current.NoteID.ToString()}]";
    PXCache cach = ((PXGraph) this).Caches[typeof (UploadFile)];
    PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Clear((PXGraph) this);
    foreach (PXResult<UploadFile> pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) nullable1
    }))
    {
      UploadFile uploadFile = PXResult<UploadFile>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(uploadFile.Name) && uploadFile.Name.Contains(oldValue))
      {
        uploadFile.Name = uploadFile.Name.Replace(oldValue, newValue);
        cach.PersistUpdated((object) uploadFile);
      }
    }
  }

  private string GetEntityDescription(CRActivity row)
  {
    string entityDescription = string.Empty;
    EntityHelper helper = new EntityHelper((PXGraph) this);
    object obj = row.RefNoteID.With<Guid?, object>((Func<Guid?, object>) (_ => helper.GetEntityRow(new Guid?(_.Value), true)));
    if (obj != null)
      entityDescription = CacheUtility.GetDescription(helper, obj, obj.GetType());
    return entityDescription;
  }

  private void CopyAttachments(
    PXGraph targetGraph,
    CRSMEmail message,
    CRSMEmail newMessage,
    bool isReply)
  {
    if (message == null || newMessage == null)
      return;
    Guid[] guidArray = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Message).Cache, (object) message);
    if (guidArray == null || guidArray.Length == 0)
      return;
    if (isReply && !string.IsNullOrEmpty(message.Body))
    {
      List<UploadFile> list = GraphHelper.RowCast<UploadFile>((IEnumerable) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) message.NoteID
      })).ToList<UploadFile>();
      ICollection<Guid> source;
      if (!new ImageExtractor().FindImages(message.Body, list, ref source))
        return;
      guidArray = source.ToArray<Guid>();
    }
    PXNoteAttribute.SetFileNotes((PXCache) GraphHelper.Caches<CRSMEmail>(targetGraph), (object) newMessage, guidArray);
  }

  private EPSetup EPSetupCurrent
  {
    get
    {
      return PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) ?? CREmailActivityMaint.EmptyEpSetup;
    }
  }

  public static CREmailActivityMaint.EmailAddress ParseNames(string source)
  {
    return new List<string>()
    {
      "'(?<first>[\\w.@]+)\\s+(?<last>[\\w.@]+).*'\\s+<(?<email>[^<>]+)>",
      "'(?<last>[\\w.@]+),\\s+(?<first>[\\w.@]+).*'\\s+<(?<email>[^<>]+)>",
      "\"(?<first>[\\w.@]+)\\s+(?<last>[\\w.@]+).*\"\\s+<(?<email>[^<>]+)>",
      "\"(?<last>[\\w.@]+),\\s+(?<first>[\\w.@]+).*\"\\s+<(?<email>[^<>]+)>",
      "(?<first>[\\w.@]+)\\s+(?<last>[\\w.@]+).*\\s+<(?<email>[^<>]+)>",
      "(?<last>[\\w.@]+),\\s+(?<first>[\\w.@]+).*\\s+<(?<email>[^<>]+)>",
      "'(?<last>[\\w.@]+)'\\s+<(?<email>[^<>]+)>",
      "\"(?<last>[\\w.@]+)\"\\s+<(?<email>[^<>]+)>",
      "(?<last>[\\w.@]+)\\s+<(?<email>[^<>]+)>",
      "(?<email>[\\w@.-]+)"
    }.Select<string, Regex>((Func<string, Regex>) (pattern => new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline))).Select<Regex, GroupCollection>((Func<Regex, GroupCollection>) (re => re.Match(source).Groups)).Select(groups => new
    {
      groups = groups,
      email_grp = groups["email"]
    }).Where(t => t.email_grp.Success).Select(t => new CREmailActivityMaint.EmailAddress()
    {
      FirstName = t.groups["first"].Value,
      LastName = t.groups["last"].Value,
      Email = t.email_grp.Value
    }).FirstOrDefault<CREmailActivityMaint.EmailAddress>();
  }

  internal void InsertFile(FileDto file) => this._extractor.InsertFile(file);

  public class EmbeddedImagesExtractor : 
    EmbeddedImagesExtractorExtension<CREmailActivityMaint, CRSMEmail, CRSMEmail.body>.WithFieldForExceptionPersistence<CRSMEmail.exception>
  {
  }

  public class TemplateSourceType
  {
    public const string Notification = "NO";
    public const string Activity = "AC";
    public const string KnowledgeBase = "KB";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "NO", "AC", "KB" }, new string[3]
        {
          "Email Template",
          "Activity",
          "KB Article"
        })
      {
      }
    }

    public class ShortListAttribute : PXStringListAttribute
    {
      public ShortListAttribute()
        : base(new string[1]{ "KB" }, new string[1]
        {
          "KB Article"
        })
      {
      }
    }

    public class notification : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CREmailActivityMaint.TemplateSourceType.notification>
    {
      public notification()
        : base("NO")
      {
      }
    }

    public class activity : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CREmailActivityMaint.TemplateSourceType.activity>
    {
      public activity()
        : base("AC")
      {
      }
    }
  }

  [Obsolete("Use PX.Data.Reports.SendEmailParams instead.")]
  public class SendEmailParams(GroupMessage message) : PX.Data.Reports.SendEmailParams(message)
  {
  }

  public class PXEMailActivityDelete<TNode>(PXGraph graph, string name) : PXDelete<TNode>(graph, name)
    where TNode : class, IBqlTable, new()
  {
    [PXUIField]
    [PXDeleteButton(ConfirmationMessage = "The current {0} record will be deleted.", ClosePopup = true)]
    protected virtual IEnumerable Handler(PXAdapter adapter)
    {
      CREmailActivityMaint.PXEMailActivityDelete<TNode> mailActivityDelete = this;
      if (!adapter.View.Cache.AllowDelete)
        throw new PXException("The record cannot be deleted.");
      int startRow = adapter.StartRow;
      bool deleted = false;
      CREmailActivityMaint graph = (CREmailActivityMaint) adapter.View.Graph;
      foreach (CRSMEmail crsmEmail in adapter.Get())
      {
        CRSMEmail copy = (CRSMEmail) ((PXSelectBase) graph.Message).Cache.CreateCopy((object) crsmEmail);
        if (copy.MPStatus != "DL")
        {
          CREmailActivityMaint.UpdateEmailBeforeDeleting(graph, copy);
          ((PXGraph) graph).Actions.PressSave();
          yield return (object) copy;
        }
        else
        {
          ((PXSelectBase<CRSMEmail>) graph.Message).Delete(copy);
          deleted = true;
        }
      }
      if (deleted)
      {
        try
        {
          ((PXGraph) graph).Actions.PressSave();
        }
        catch
        {
          ((PXGraph) graph).Clear();
          throw;
        }
        ((PXGraph) graph).SelectTimeStamp();
        adapter.StartRow = startRow;
        if (adapter.View.Cache.AllowInsert)
        {
          ((PXAction<TNode>) mailActivityDelete).Insert(adapter);
          foreach (object obj in adapter.Get())
            yield return obj;
          adapter.View.Cache.IsDirty = false;
        }
        else
        {
          adapter.StartRow = 0;
          adapter.Searches = (object[]) null;
          foreach (object obj in adapter.Get())
            yield return obj;
        }
      }
    }
  }

  public class EmailAddress
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
  }

  public class EntityList : PXStringListAttribute
  {
    public EntityList()
      : base(new string[7]
      {
        "Event",
        "Task",
        "Lead",
        "Contact",
        "Case",
        "Opportunity",
        "Expense Receipt"
      }, new string[7]
      {
        "Event",
        "Task",
        "Lead",
        "Contact",
        "Case",
        "Opportunity",
        "Expense Receipt"
      })
    {
    }
  }

  public class EntityListSimple : PXStringListAttribute
  {
    public EntityListSimple()
      : base(new string[2]{ "Event", "Task" }, new string[2]
      {
        "Event",
        "Task"
      })
    {
    }
  }
}
