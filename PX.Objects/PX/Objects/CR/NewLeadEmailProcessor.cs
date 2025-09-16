// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.NewLeadEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class NewLeadEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault() || !account.CreateLead.GetValueOrDefault())
      return false;
    CRSMEmail message = package.Message;
    if (!string.IsNullOrEmpty(message.Exception) || !message.IsIncome.GetValueOrDefault() || message.RefNoteID.HasValue || message.ClassID.GetValueOrDefault() == -2)
      return false;
    object copy1 = package.Graph.Caches[typeof (CRSMEmail)].CreateCopy((object) message);
    try
    {
      LeadMaint instance = PXGraph.CreateInstance<LeadMaint>();
      LeadMaint.CRDuplicateEntitiesForLeadGraphExt extension = ((PXGraph) instance).GetExtension<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>();
      if (extension != null)
        extension.HardBlockOnly = true;
      PXCache cache = ((PXSelectBase) instance.Lead).Cache;
      CRLead crLead1 = (CRLead) cache.Insert();
      CRLead copy2 = PXCache<CRLead>.CreateCopy(PXResultset<CRLead>.op_Implicit(((PXSelectBase<CRLead>) instance.Lead).Search<CRLead.contactID>((object) crLead1.ContactID, Array.Empty<object>())));
      copy2.EMail = package.Address;
      copy2.LastName = package.Description;
      copy2.RefContactID = message.ContactID;
      copy2.OverrideRefContact = new bool?(true);
      CREmailActivityMaint.EmailAddress names = CREmailActivityMaint.ParseNames(message.MailFrom);
      copy2.FirstName = names.FirstName;
      copy2.LastName = string.IsNullOrEmpty(names.LastName) ? names.Email : names.LastName;
      if (account.CreateLeadClassID != null)
        copy2.ClassID = account.CreateLeadClassID;
      CRLead crLead2 = (CRLead) cache.Update((object) copy2);
      if (crLead2.ClassID != null)
      {
        if (PXResultset<CRLeadClass>.op_Implicit(PXSelectBase<CRLeadClass, PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Required<CRLeadClass.classID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[1]
        {
          (object) crLead2.ClassID
        }))?.DefaultOwner == "S")
        {
          crLead2.WorkgroupID = message.WorkgroupID;
          crLead2.OwnerID = message.OwnerID;
        }
      }
      message.RefNoteID = PXNoteAttribute.GetNoteID<CRLead.noteID>(cache, (object) crLead2);
      ((PXGraph) instance).Actions.PressSave();
    }
    catch (Exception ex)
    {
      package.Graph.Caches[typeof (CRSMEmail)].RestoreCopy((object) message, copy1);
      throw new PXException("Unable to create new lead.~ {0}", new object[1]
      {
        ex is PXOuterException ? (object) ("\r\n" + string.Join("\r\n", ((PXOuterException) ex).InnerMessages)) : (object) ex.Message
      });
    }
    return true;
  }
}
