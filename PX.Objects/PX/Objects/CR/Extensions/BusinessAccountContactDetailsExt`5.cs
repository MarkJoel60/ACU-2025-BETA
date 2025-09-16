// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.BusinessAccountContactDetailsExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.CRCreateActions;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>Represents the Contacts grid</summary>
public abstract class BusinessAccountContactDetailsExt<TGraph, TCreateContactExt, TMaster, FBAccountIDField, FBAccountAcctNameField> : 
  ContactDetailsExt<TGraph, TCreateContactExt, TMaster, Contact.bAccountID, FBAccountIDField>
  where TGraph : PXGraph, new()
  where TCreateContactExt : CRCreateContactActionBase<TGraph, TMaster>
  where TMaster : BAccount, IBqlTable, new()
  where FBAccountIDField : class, IBqlField
  where FBAccountAcctNameField : class, IBqlField
{
  public virtual void _(
    Events.FieldUpdated<TMaster, FBAccountAcctNameField> e)
  {
    foreach (PXResult<Contact> pxResult in ((PXSelectBase<Contact>) this.Contacts).Select(Array.Empty<object>()))
    {
      Contact contact = PXResult<Contact>.op_Implicit(pxResult);
      contact.FullName = (string) e.NewValue;
      this.Base.Caches[typeof (Contact)].Update((object) contact);
    }
  }

  [PXOverride]
  public virtual void Persist(Action del)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.InactivateActiveContacts();
      del();
      transactionScope.Complete();
    }
  }

  public virtual void InactivateActiveContacts()
  {
    PXCache cach = this.Base.Caches[typeof (TMaster)];
    if (!(cach.Current is TMaster current) || !(current.Status == "I") || "I".Equals(cach.GetValueOriginal<BAccount.status>((object) current)))
      return;
    this.InactivateActiveContacts((PXGraph) this.Base);
  }

  public virtual void InactivateActiveContacts(PXGraph baseGraph)
  {
    ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
    foreach (PXResult<Contact> pxResult in ((PXSelectBase<Contact>) baseGraph.FindImplementation<BusinessAccountContactDetailsExt<TGraph, TCreateContactExt, TMaster, FBAccountIDField, FBAccountAcctNameField>>().Contacts).Select(Array.Empty<object>()))
    {
      Contact contact = PXResult<Contact>.op_Implicit(pxResult);
      if (!(contact.Status == "I"))
      {
        contact.Status = "I";
        ((PXSelectBase) instance.ContactCurrent).Cache.Update((object) contact);
      }
    }
    if (!((PXGraph) instance).IsDirty)
      return;
    ((PXAction) instance.Save).Press();
    baseGraph.SelectTimeStamp();
  }
}
