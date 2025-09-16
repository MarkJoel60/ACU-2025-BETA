// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.NewCaseEmailProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR.CRCaseMaint_Extensions;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class NewCaseEmailProcessor : BasicEmailProcessor
{
  protected override bool Process(BasicEmailProcessor.Package package)
  {
    EMailAccount account = package.Account;
    if (!account.IncomingProcessing.GetValueOrDefault() || !account.CreateCase.GetValueOrDefault())
      return false;
    CRSMEmail message = package.Message;
    if (!string.IsNullOrEmpty(message.Exception) || !message.IsIncome.GetValueOrDefault() || message.RefNoteID.HasValue || message.ClassID.GetValueOrDefault() == -2)
      return false;
    CRCaseMaint instance = PXGraph.CreateInstance<CRCaseMaint>();
    this.SetCRSetup((PXGraph) instance);
    this.SetAccessInfo((PXGraph) instance);
    object copy1 = package.Graph.Caches[typeof (CRSMEmail)].CreateCopy((object) message);
    try
    {
      PXCache cach1 = ((PXGraph) instance).Caches[typeof (CRCase)];
      CRCase crCase1 = (CRCase) cach1.Insert();
      CRCase copy2 = PXCache<CRCase>.CreateCopy(PXResultset<CRCase>.op_Implicit(((PXSelectBase<CRCase>) instance.Case).Search<CRCase.caseCD>((object) crCase1.CaseCD, Array.Empty<object>())));
      copy2.Subject = message.Subject;
      if (copy2.Subject == null || copy2.Subject.Trim().Length == 0)
        copy2.Subject = NewCaseEmailProcessor.GetFromString(package.Address, package.Description);
      copy2.Description = message.Body;
      if (account.CreateCaseClassID != null)
        copy2.CaseClassID = account.CreateCaseClassID;
      CRCaseClass crCaseClass = PXResultset<CRCaseClass>.op_Implicit(PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[1]
      {
        (object) copy2.CaseClassID
      }));
      if (crCaseClass == null)
        return false;
      CRCase crCase2 = (CRCase) cach1.Update((object) copy2);
      BAccount baccount = (BAccount) null;
      CRCaseMaint graph1 = instance;
      string address1 = package.Address;
      bool? nullable = crCaseClass.AllowEmployeeAsContact;
      int num1 = nullable ?? true ? 1 : 0;
      Contact contact = this.FindContact((PXGraph) graph1, address1, num1 != 0);
      if (contact != null)
      {
        crCase2.ContactID = contact.ContactID;
        message.ContactID = contact.ContactID;
        baccount = this.FindAccount((PXGraph) instance, contact);
      }
      else
      {
        nullable = crCaseClass.RequireContact;
        if (nullable.GetValueOrDefault())
          return false;
      }
      if (baccount == null && contact == null)
      {
        CRCaseMaint graph2 = instance;
        string address2 = package.Address;
        nullable = crCaseClass.AllowEmployeeAsContact;
        int num2 = nullable ?? true ? 1 : 0;
        baccount = this.FindAccount((PXGraph) graph2, address2, num2 != 0);
      }
      if (baccount != null)
      {
        PXCache cach2 = ((PXGraph) instance).Caches[typeof (BAccount)];
        GraphHelper.EnsureCachePersistence((PXGraph) instance, cach2.GetItemType());
        message.BAccountID = baccount.BAccountID;
        crCase2.CustomerID = baccount.BAccountID;
      }
      else
      {
        nullable = crCaseClass.RequireCustomer;
        if (!nullable.GetValueOrDefault())
        {
          nullable = crCaseClass.RequireVendor;
          if (!nullable.GetValueOrDefault())
            goto label_22;
        }
        return false;
      }
label_22:
      message.RefNoteID = PXNoteAttribute.GetNoteID<CRCase.noteID>(((PXGraph) instance).Caches[typeof (CRCase)], (object) crCase2);
      cach1.Update((object) crCase2);
      ((PXGraph) instance).GetExtension<CRCaseMaint_InitialActivityExt>().SkipInitialActivityCreation = true;
      CRCaseMaint_CRCaseCommitmentsExt extension = ((PXGraph) instance).GetExtension<CRCaseMaint_CRCaseCommitmentsExt>();
      if (extension != null)
        extension.SuspendCalculation = true;
      ((PXAction<CRCase>) instance.Save).PressImpl(false, false);
    }
    catch (Exception ex)
    {
      package.Graph.Caches[typeof (CRSMEmail)].RestoreCopy((object) message, copy1);
      throw new PXException("Unable to create new case.~ {0}", new object[1]
      {
        ex is PXOuterException ? (object) ("\r\n" + string.Join("\r\n", ((PXOuterException) ex).InnerMessages)) : (object) ex.Message
      });
    }
    return true;
  }

  private static string GetFromString(string address, string description)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("From {0} {1}", new object[2]
    {
      (object) description,
      (object) address
    });
  }

  private void SetAccessInfo(PXGraph graph)
  {
    graph.Caches[typeof (AccessInfo)].Current = (object) graph.Accessinfo;
  }

  private void SetCRSetup(PXGraph graph)
  {
    graph.Caches[typeof (CRSetup)].Current = (object) PXResultset<CRSetup>.op_Implicit(PXSelectBase<CRSetup, PXSelect<CRSetup>.Config>.SelectWindowed(graph, 0, 1, Array.Empty<object>()));
  }

  private BAccount FindAccount(PXGraph graph, Contact contact)
  {
    if (contact == null || !contact.BAccountID.HasValue)
      return (BAccount) null;
    BAccount account = (BAccount) null;
    if (contact.ContactType == "PN")
    {
      PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>>.Config>.Clear(graph);
      account = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<BAccount.status, NotEqual<CustomerStatus.inactive>, And<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) contact.BAccountID
      }));
    }
    else if (contact.ContactType == "EP")
    {
      PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<BAccount.isBranch, Equal<True>>>>.Config>.Clear(graph);
      account = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<BAccount.status, NotEqual<CustomerStatus.inactive>, And<BAccount.isBranch, Equal<True>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) contact.BAccountID
      }));
    }
    return account;
  }

  private BAccount FindAccount(PXGraph graph, string address, bool searchForEmployee)
  {
    if (string.IsNullOrEmpty(address))
      return (BAccount) null;
    PXSelectBase<BAccount, PXSelectJoin<BAccount, InnerJoin<Contact, On<Contact.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactID, Equal<BAccount.defContactID>>>>, Where<Contact.eMail, Equal<Required<Contact.eMail>>, And<Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>, Or<Required<CRCaseClass.allowEmployeeAsContact>, Equal<True>, And<BAccount.isBranch, Equal<True>>>>>>>.Config>.Clear(graph);
    return PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelectJoin<BAccount, InnerJoin<Contact, On<Contact.bAccountID, Equal<BAccount.bAccountID>, And<Contact.contactID, Equal<BAccount.defContactID>>>>, Where<Contact.eMail, Equal<Required<Contact.eMail>>, And<Contact.isActive, Equal<True>, And<BAccount.status, NotEqual<CustomerStatus.inactive>, And<Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>, Or<Required<CRCaseClass.allowEmployeeAsContact>, Equal<True>, And<BAccount.isBranch, Equal<True>>>>>>>>, OrderBy<Desc<Contact.lastModifiedDateTime>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) address,
      (object) searchForEmployee
    }));
  }

  private Contact FindContact(PXGraph graph, string address, bool searchForEmployee)
  {
    PXSelectBase<Contact, PXSelect<Contact, Where<Contact.eMail, Equal<Required<Contact.eMail>>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Required<CRCaseClass.allowEmployeeAsContact>, Equal<True>, And<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>>>.Config>.Clear(graph);
    return PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.eMail, Equal<Required<Contact.eMail>>, And<Contact.isActive, Equal<True>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Required<CRCaseClass.allowEmployeeAsContact>, Equal<True>, And<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>>>, OrderBy<Desc<Contact.lastModifiedDateTime>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) address,
      (object) searchForEmployee
    }));
  }
}
