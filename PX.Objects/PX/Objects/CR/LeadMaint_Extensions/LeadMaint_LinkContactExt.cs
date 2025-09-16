// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_LinkContactExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.SideBySideComparison;
using PX.Objects.CR.Extensions.SideBySideComparison.Link;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.LeadMaint_Extensions;

public class LeadMaint_LinkContactExt : 
  LinkEntitiesExt_EventBased<
  #nullable disable
  LeadMaint, CRLead, LinkFilter, CRLead, CRLead.refContactID>
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<ContactAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Contact.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CRLead.bAccountID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  Contact.contactType, IBqlString>.IsEqual<
  #nullable disable
  ContactTypesAttribute.person>>>.Order<By<BqlField<
  #nullable enable
  ContactAccount.isPrimary, IBqlBool>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  Contact.displayName, IBqlString>.Asc>>, 
  #nullable disable
  ContactAccount>.View Link_SelectEntityForLink;

  public override string LeftValueDescription => "Lead";

  public override string RightValueDescription => "Contact";

  public override EntitiesContext GetLeftEntitiesContext()
  {
    return new EntitiesContext((PXGraph) this.Base, new EntityEntry(typeof (Contact), ((PXSelectBase) this.Base.Lead).Cache, new IBqlTable[1]
    {
      (IBqlTable) ((PXSelectBase<CRLead>) this.Base.Lead).Current
    }), new EntityEntry[1]
    {
      new EntityEntry(typeof (Address), ((PXSelectBase) this.Base.AddressCurrent).Cache, new IBqlTable[1]
      {
        (IBqlTable) ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>())
      })
    });
  }

  public override EntitiesContext GetRightEntitiesContext()
  {
    ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
    int result;
    int? contactID = int.TryParse(((PXSelectBase<LinkFilter>) this.Filter).Current.LinkedEntityID, out result) ? new int?(result) : new int?();
    int? addressID = new int?();
    if (!contactID.HasValue)
    {
      BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, ((PXSelectBase<CRLead>) this.Base.Lead).Current.BAccountID);
      contactID = (int?) baccount?.DefContactID;
      addressID = (int?) baccount?.DefAddressID;
    }
    Contact contact = Contact.PK.Find((PXGraph) instance, contactID);
    if (contact == null)
      throw new PXException("The contact with the ID {0} cannot be found.", new object[1]
      {
        (object) contactID
      });
    if (!addressID.HasValue)
      addressID = contact.DefAddressID;
    Address address = Address.PK.Find((PXGraph) instance, addressID);
    if (address == null)
      throw new PXException("The contact's address with the ID {0} cannot be found.", new object[1]
      {
        (object) addressID
      });
    ((PXSelectBase<Contact>) instance.Contact).Current = contact;
    ((PXSelectBase) instance.Contact).Cache.RaiseRowSelected((object) contact);
    ((PXSelectBase<Address>) instance.AddressCurrent).Current = address;
    ((PXSelectBase) instance.AddressCurrent).Cache.RaiseRowSelected((object) address);
    return new EntitiesContext((PXGraph) instance, new EntityEntry(((PXSelectBase) instance.Contact).Cache, new IBqlTable[1]
    {
      (IBqlTable) contact
    }), new EntityEntry[1]
    {
      new EntityEntry(((PXSelectBase) instance.AddressCurrent).Cache, new IBqlTable[1]
      {
        (IBqlTable) address
      })
    });
  }

  public override void UpdateMainAfterProcess()
  {
    this.UpdatingEntityCurrent.OverrideRefContact = new bool?(!((PXSelectBase<LinkFilter>) this.Filter).Current.ProcessLink.GetValueOrDefault());
    base.UpdateMainAfterProcess();
  }

  public override void UpdateRightEntitiesContext(
    EntitiesContext context,
    IEnumerable<LinkComparisonRow> result)
  {
  }

  protected override object GetSelectedEntityID()
  {
    return (object) (int?) ((PXSelectBase<ContactAccount>) this.Link_SelectEntityForLink).Current?.ContactID;
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Sync with Lead")]
  protected virtual void _(Events.CacheAttached<LinkFilter.processLink> e)
  {
  }

  protected virtual void _(
    Events.FieldUpdated<CRLead, CRLead.overrideRefContact> e)
  {
    if (!(e.NewValue is bool newValue) || newValue || false.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<CRLead, CRLead.overrideRefContact>, CRLead, object>) e).OldValue))
      return;
    PreventRecursionCall.Execute((Action) (() =>
    {
      LinkFilter current = ((PXSelectBase<LinkFilter>) this.Filter).Current;
      int? refContactId = e.Row.RefContactID;
      ref int? local = ref refContactId;
      string str = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
      current.LinkedEntityID = str;
      List<LinkComparisonRow> list = this.GetPreparedComparisons().ToList<LinkComparisonRow>();
      list.ForEach((Action<LinkComparisonRow>) (item => item.Selection = ComparisonSelection.Right));
      this.ProcessComparisons((IReadOnlyCollection<LinkComparisonRow>) list);
    }), nameof (_), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\Graphs\\LeadMaint\\LeadMaint_LinkContactExt.cs", 99);
  }

  protected virtual void _(Events.FieldUpdated<CRLead, CRLead.refContactID> e)
  {
    if (((Events.FieldUpdatedBase<Events.FieldUpdated<CRLead, CRLead.refContactID>, CRLead, object>) e).OldValue == null || e.NewValue != null || !e.Row.BAccountID.HasValue)
      return;
    e.Row.OverrideRefContact = new bool?(true);
  }
}
