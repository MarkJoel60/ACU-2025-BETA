// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Recipient
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

[PXInternalUseOnly]
public class Recipient
{
  public Recipient(
    Contact contact,
    string format,
    object entity = null,
    int? bAccountID = null,
    int? contactID = null,
    Guid? refNoteID = null,
    Guid? documentNoteID = null)
  {
    this.Contact = contact;
    this.Format = format;
    this.Entity = entity;
    this.BAccountID = bAccountID;
    this.ContactID = contactID;
    this.RefNoteID = refNoteID;
    this.DocumentNoteID = documentNoteID;
  }

  public static Recipient SmartCreate(
    EntityHelper helper,
    Contact contact,
    object entity = null,
    CRLead lead = null,
    string format = "H")
  {
    entity = entity ?? (object) contact;
    if (lead != null && lead.ContactID.HasValue)
      return new Recipient(contact, format, entity, contact.BAccountID, lead.RefContactID, helper.GetNoteIDAndEnsureNoteExists((object) contact), helper.GetNoteIDAndEnsureNoteExists(entity));
    Contact contact1 = contact;
    string format1 = format;
    object entity1 = entity;
    int? baccountId = contact.BAccountID;
    int? contactId = contact.ContactID;
    Guid? ensureNoteExists = helper.GetNoteIDAndEnsureNoteExists(entity);
    Guid? refNoteID = new Guid?();
    Guid? documentNoteID = ensureNoteExists;
    return new Recipient(contact1, format1, entity1, baccountId, contactId, refNoteID, documentNoteID);
  }

  public Contact Contact { get; }

  public object Entity { get; }

  public string Format { get; }

  public Guid? DocumentNoteID { get; }

  public Guid? RefNoteID { get; }

  public int? BAccountID { get; }

  public int? ContactID { get; }

  public TemplateNotificationGenerator GetSender(PXGraph graph, CRMassMail massMail)
  {
    TemplateNotificationGenerator sender = TemplateNotificationGenerator.Create(graph, (object) this.Contact);
    sender.MailAccountId = massMail.MailAccountID ?? MailAccountManager.DefaultMailAccountID;
    sender.To = massMail.MailTo;
    sender.Cc = massMail.MailCc;
    sender.Bcc = massMail.MailBcc;
    sender.Body = massMail.MailContent ?? string.Empty;
    sender.Subject = massMail.MailSubject;
    sender.AttachmentsID = massMail.NoteID;
    sender.BAccountID = this.BAccountID;
    sender.BodyFormat = this.Format;
    sender.RefNoteID = this.RefNoteID;
    sender.ContactID = this.ContactID;
    sender.DocumentNoteID = this.DocumentNoteID;
    return sender;
  }
}
