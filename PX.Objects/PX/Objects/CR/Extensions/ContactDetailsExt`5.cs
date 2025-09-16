// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ContactDetailsExt`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.CRCreateActions;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>Represents the Contacts grid</summary>
public abstract class ContactDetailsExt<TGraph, TCreateContactExt, TMaster, FContactField, FMasterField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TCreateContactExt : CRCreateContactActionBase<TGraph, TMaster>
  where TMaster : class, IBqlTable, new()
  where FContactField : class, IBqlField
  where FMasterField : class, IBqlField
{
  [PXViewName("Contacts")]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<Contact, LeftJoin<Address, On<Address.addressID, Equal<Contact.defAddressID>>>, Where<FContactField, Equal<Current<FMasterField>>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>> Contacts;
  public PXAction<TMaster> RefreshContact;
  public PXAction<TMaster> ViewContact;

  [PXUIField]
  [PXButton]
  public virtual void refreshContact()
  {
    this.Base.SelectTimeStamp();
    ((PXCache) GraphHelper.Caches<Contact>((PXGraph) this.Base)).ClearQueryCache();
  }

  [PXUIField]
  [PXButton(PopupCommand = "RefreshContact")]
  public virtual void viewContact()
  {
    if (((PXSelectBase<Contact>) this.Contacts).Current == null || this.Base.Caches[typeof (TMaster)].GetStatus(this.Base.Caches[typeof (TMaster)].Current) == 2)
      return;
    Contact current = ((PXSelectBase<Contact>) this.Contacts).Current;
    ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
    ((PXSelectBase<Contact>) instance.Contact).Current = current;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }
}
