// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXEmailSyncHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Email;

public static class PXEmailSyncHelper
{
  public static Dictionary<string, System.Type> _exchangers = new Dictionary<string, System.Type>();

  static PXEmailSyncHelper()
  {
    PXEmailSyncHelper._exchangers.Add("E", typeof (MicrosoftExchangeSyncProvider));
  }

  public static bool IsExchange(int emailAccountID)
  {
    PXEmailSyncHelper.Definition slot = PXDatabase.GetSlot<PXEmailSyncHelper.Definition>("EmailExchangeAccounts", new System.Type[1]
    {
      typeof (EMailAccount)
    });
    return slot.Exchanges != null && slot.Exchanges.Contains(emailAccountID);
  }

  public static IEmailSyncProvider GetExchanger(int emailAccountID)
  {
    Tuple<EMailSyncServer, EMailSyncPolicy, PXSyncMailbox> config = PXEmailSyncHelper.GetConfig(emailAccountID);
    return PXEmailSyncHelper.GetExchanger(config.Item1, config.Item2);
  }

  public static IEmailSyncProvider GetExchanger(EMailSyncServer server, EMailSyncPolicy policy)
  {
    if (server == null || string.IsNullOrEmpty(server.ServerType) || !PXEmailSyncHelper._exchangers.ContainsKey(server.ServerType))
      throw new PXException("Exchange provider could not be found in the system.");
    return (IEmailSyncProvider) Activator.CreateInstance(PXEmailSyncHelper._exchangers[server.ServerType], (object) server, (object) policy);
  }

  public static void SendMessage(CRSMEmail message)
  {
    if (message == null || !message.MailAccountID.HasValue)
      throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
    Tuple<EMailSyncServer, EMailSyncPolicy, PXSyncMailbox> config = PXEmailSyncHelper.GetConfig(message.MailAccountID.Value);
    IEmailSyncProvider exchanger = PXEmailSyncHelper.GetExchanger(config.Item1, config.Item2);
    try
    {
      exchanger.SendMessage(config.Item3, (IEnumerable<CRSMEmail>) new CRSMEmail[1]
      {
        message
      });
      message.Exception = (string) null;
    }
    catch (Exception ex)
    {
      message.Exception = ex.Message;
    }
  }

  private static Tuple<EMailSyncServer, EMailSyncPolicy, PXSyncMailbox> GetConfig(int emailAccountID)
  {
    PXGraph pxGraph = new PXGraph();
    using (IEnumerator<PXResult<EMailSyncAccount>> enumerator = PXSelectBase<EMailSyncAccount, PXSelectJoin<EMailSyncAccount, InnerJoin<EMailSyncServer, On<EMailSyncServer.accountID, Equal<EMailSyncAccount.serverID>>, InnerJoin<EMailAccount, On<EMailAccount.emailAccountID, Equal<EMailSyncAccount.emailAccountID>>, LeftJoin<EPEmployee, On<EMailSyncAccount.employeeID, Equal<EPEmployee.bAccountID>>, LeftJoin<Contact, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>>>>, Where<EMailSyncAccount.emailAccountID, Equal<Required<EMailSyncAccount.emailAccountID>>>, OrderBy<Asc<EMailSyncAccount.serverID, Asc<EMailSyncAccount.employeeID>>>>.Config>.SelectSingleBound(pxGraph, (object[]) null, new object[1]
    {
      (object) emailAccountID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact> current = (PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact>) enumerator.Current;
        EMailSyncServer emailSyncServer = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact>.op_Implicit(current);
        EMailSyncAccount emailSyncAccount = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact>.op_Implicit(current);
        Contact contact = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact>.op_Implicit(current);
        EMailAccount emailAccount = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee, Contact>.op_Implicit(current);
        if (emailSyncServer == null || emailSyncAccount == null || string.IsNullOrEmpty(emailSyncAccount.Address))
          throw new PXException("An appropriate email account could not be found in the system.");
        bool? nullable = emailSyncServer.IsActive;
        if (!nullable.GetValueOrDefault())
          throw new PXException("Specified email account is not enabled for sending/receiving emails.");
        string mailbox = contact?.EMail ?? emailSyncAccount.Address;
        int employee = emailSyncAccount.EmployeeID.Value;
        int? emailAccountID1 = new int?(emailAccountID);
        PXSyncMailboxPreset exportPreset = new PXSyncMailboxPreset(new DateTime?(), (string) null);
        PXSyncMailboxPreset importPreset = new PXSyncMailboxPreset(new DateTime?(), (string) null);
        nullable = emailAccount.IncomingProcessing;
        int num = nullable.GetValueOrDefault() ? 1 : 0;
        PXSyncMailbox pxSyncMailbox = new PXSyncMailbox(mailbox, employee, emailAccountID1, exportPreset, importPreset, num != 0);
        string str = emailSyncAccount.PolicyName ?? emailSyncServer.DefaultPolicyName;
        EMailSyncPolicy emailSyncPolicy = PXResultset<EMailSyncPolicy>.op_Implicit(PXSelectBase<EMailSyncPolicy, PXSelect<EMailSyncPolicy, Where<EMailSyncPolicy.policyName, Equal<Required<EMailSyncPolicy.policyName>>>>.Config>.SelectSingleBound(pxGraph, (object[]) null, new object[1]
        {
          (object) str
        }));
        if (emailSyncPolicy == null)
          throw new PXException("Synchronization policy could not be found for account '{0}'.", new object[1]
          {
            (object) emailSyncAccount.Address
          });
        if (string.IsNullOrEmpty(emailSyncServer.ServerType) || !PXEmailSyncHelper._exchangers.ContainsKey(emailSyncServer.ServerType))
          throw new PXException("Exchange provider could not be found in the system.");
        return Tuple.Create<EMailSyncServer, EMailSyncPolicy, PXSyncMailbox>(emailSyncServer, emailSyncPolicy, pxSyncMailbox);
      }
    }
    throw new PXException("An appropriate email account could not be found in the system.");
  }

  internal static bool ValidateAllAddressesInEmail(CRSMEmail email, out string errorMessage)
  {
    List<(string, string, Exception)> res = new List<(string, string, Exception)>();
    Validate("MailFrom", email.MailFrom);
    Validate("MailReply", email.MailReply);
    Validate("MailTo", email.MailTo);
    Validate("MailCc", email.MailCc);
    Validate("MailBcc", email.MailBcc);
    if (res.Count > 0)
    {
      PXTrace.Logger.Warning<Guid?, IEnumerable<\u003C\u003Ef__AnonymousType38<string, string>>>((Exception) new AggregateException(res.Select<(string, string, Exception), Exception>((Func<(string, string, Exception), Exception>) (r => r.exception))), "Email validation failed for CRSMEmail: {NoteID}. Following fields couldn't be validated: {@FieldsAndValues}", email.NoteID, res.Select(r => new
      {
        FieldName = r.field,
        Value = r.value
      }));
      errorMessage = PXLocalizer.LocalizeFormat("Email validation failed. The following fields contain invalid email addresses: {0}", new object[1]
      {
        (object) string.Join(", ", res.Select<(string, string, Exception), string>((Func<(string, string, Exception), string>) (r => r.field)))
      });
      return false;
    }
    errorMessage = (string) null;
    return true;

    void Validate(string field, string value)
    {
      try
      {
        EmailParser.ParseAddresses(value);
      }
      catch (Exception ex)
      {
        res.Add((field, value, ex));
      }
    }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public List<int> Exchanges;

    public void Prefetch()
    {
      this.Exchanges = new List<int>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<EMailAccount>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<EMailAccount.emailAccountID>(),
        (PXDataField) new PXDataFieldValue<EMailAccount.emailAccountType>((object) "E")
      }))
      {
        int? int32 = pxDataRecord.GetInt32(0);
        if (int32.HasValue)
          this.Exchanges.Add(int32.Value);
      }
    }
  }
}
