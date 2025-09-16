// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateLeadAction`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateLeadAction<TGraph, TMain> : CRCreateActionBaseInit<TGraph, TMain>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public PXAction<TMain> CreateLead;

  public virtual TMain GetCurrentMain(params object[] pars)
  {
    return (TMain) ((PXCache) GraphHelper.Caches<TMain>((PXGraph) this.Base)).Current;
  }

  [PXUIField]
  [PXButton]
  public virtual void createLead()
  {
    if (this.Base.IsDirty)
      this.Base.Actions.PressSave();
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    DocumentContact source1 = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    DocumentAddress source2 = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(Array.Empty<object>());
    if (current == null || source1 == null || source2 == null)
      return;
    LeadMaint instance = PXGraph.CreateInstance<LeadMaint>();
    CRLead target1 = ((PXSelectBase<CRLead>) instance.Lead).Insert();
    CRLead crLead = target1;
    int? contactId = current.ContactID;
    int num = 0;
    int? nullable = contactId.GetValueOrDefault() < num & contactId.HasValue ? new int?() : current.ContactID;
    crLead.RefContactID = nullable;
    target1.BAccountID = current.BAccountID;
    target1.OverrideSalesTerritory = current.OverrideSalesTerritory;
    bool? overrideSalesTerritory = target1.OverrideSalesTerritory;
    if (overrideSalesTerritory.HasValue && overrideSalesTerritory.GetValueOrDefault())
      target1.SalesTerritoryID = current.SalesTerritoryID;
    this.MapContact(source1, (IPersonalContact) target1);
    this.MapConsentable(source1, (IConsentable) target1);
    if (PXResultset<CRLeadClass>.op_Implicit(PXSelectBase<CRLeadClass, PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Current<CRLead.classID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) target1
    }, Array.Empty<object>()))?.DefaultOwner == "S")
    {
      target1.WorkgroupID = current.WorkgroupID;
      target1.OwnerID = current.OwnerID;
    }
    Contact contact = Contact.PK.Find((PXGraph) this.Base, target1.RefContactID);
    if (contact != null)
    {
      CRContactClass parent = (CRContactClass) PrimaryKeyOf<CRContactClass>.By<CRContactClass.classID>.ForeignKeyOf<Contact>.By<Contact.classID>.FindParent((PXGraph) this.Base, (Contact.classID) contact, (PKFindOptions) 0);
      if (parent != null)
        target1.ClassID = parent.TargetLeadClassID;
    }
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<TMain>((PXGraph) this.Base), (object) this.GetCurrentMain((object) target1.RefContactID), ((PXSelectBase) instance.Lead).Cache, (object) ((PXSelectBase<CRLead>) instance.Lead).Current, target1.ClassID);
    ((PXSelectBase<CRLead>) instance.Lead).Update(target1);
    Address target2 = ((PXSelectBase<Address>) instance.AddressCurrent).SelectSingle(Array.Empty<object>()) ?? throw new InvalidOperationException("Cannot get Address for Lead.");
    this.MapAddress(source2, (IAddressBase) target2);
    ((PXSelectBase) instance.AddressCurrent).Cache.Update((object) target2);
    if (!this.Base.IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    ((PXAction) instance.Save).Press();
  }
}
