// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MarketingMemberImport`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Objects.CR;

public abstract class MarketingMemberImport<TGraph, TMain, TDetail> : 
  PXGraphExtension<TGraph>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
  where TDetail : class, IBqlTable, new()
{
  [PXHidden]
  public PXSelect<PXImportAttribute.PXImportSettings> ImportSettings;
  [PXHidden]
  public PXSelect<CRMarketingMemberForImport, Where<True, Equal<False>>> ImportEntity;
  [PXHidden]
  public PXInnerProcessing<TMain, TDetail> ImportMembers;

  public virtual void Initialize()
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((PXSelectBase) this.ImportEntity).Cache, (object) null);
    attributeAdjuster.For<Contact.memberName>((Action<PXUIFieldAttribute>) (_ =>
    {
      _.Visible = true;
      _.Enabled = true;
    })).SameFor<Contact.fullName>();
    attributeAdjuster = PXCacheEx.AdjustUI(((PXSelectBase) this.ImportEntity).Cache, (object) null);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<CRMarketingMemberForImport.contactID>((Action<PXUIFieldAttribute>) (_ =>
    {
      _.Visible = false;
      _.Enabled = false;
    }));
    chained = chained.SameFor<CRMarketingMemberForImport.linkCampaignID>();
    chained = chained.SameFor<CRMarketingMemberForImport.linkMarketingListID>();
    chained = chained.SameFor<Contact.displayName>();
    chained = chained.SameFor<Contact.assignDate>();
    chained = chained.SameFor<Contact.attention>();
    chained = chained.SameFor<Contact.title>();
    chained = chained.SameFor<Contact.duplicateFound>();
    chained = chained.SameFor<Contact.duplicateStatus>();
    chained = chained.SameFor<Contact.resolution>();
    chained = chained.SameFor<Contact.searchSuggestion>();
    chained = chained.SameFor<Contact.selected>();
    chained = chained.SameFor<Contact.synchronize>();
    chained = chained.SameFor<Contact.anniversary>();
    chained = chained.SameFor<Contact.ownerID>();
    chained = chained.SameFor<Contact.status>();
    chained = chained.SameFor<Contact.defAddressID>();
    chained.SameFor<CRMarketingMemberForImport.addressType>();
  }

  protected virtual void _(
    Events.RowInserting<PXImportAttribute.PXImportSettings> e)
  {
    e.Row.Mode = "B";
  }

  protected virtual void _(
    Events.RowSelected<PXImportAttribute.PXImportSettings> e)
  {
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PXImportAttribute.PXImportSettings>>) e).Cache, (object) e.Row).For<PXImportAttribute.PXImportSettings.mode>((Action<PXUIFieldAttribute>) (_ => _.Enabled = false));
  }

  protected virtual void _(Events.RowSelected<TMain> e)
  {
    PXInnerProcessing<TMain, TDetail> importMembers = this.ImportMembers;
    MarketingMemberImport<TGraph, TMain, TDetail> marketingMemberImport = this;
    // ISSUE: virtual method pointer
    PXProcessingBase<TMain>.ParametersDelegate parametersDelegate = new PXProcessingBase<TMain>.ParametersDelegate((object) marketingMemberImport, __vmethodptr(marketingMemberImport, ParametersDelegate));
    ((PXProcessingBase<TMain>) importMembers).SetParametersDelegate(parametersDelegate);
  }

  public virtual bool ParametersDelegate(List<TMain> list)
  {
    list.Clear();
    List<CRMarketingMemberForImport> membersToCreate = GraphHelper.RowCast<CRMarketingMemberForImport>(this.Base.Caches[typeof (CRMarketingMemberForImport)].Inserted).ToList<CRMarketingMemberForImport>();
    this.GenerateDummyRowsForProcessingResultsGrid(list, membersToCreate);
    ((PXProcessingBase<TMain>) this.ImportMembers).SetProcessDelegate((Action<List<TMain>, CancellationToken>) ((_, cancellationToken) => MarketingMemberImport<TGraph, TMain, TDetail>.ProcessingHandler(membersToCreate, cancellationToken)));
    return true;
  }

  public abstract void GenerateDummyRowsForProcessingResultsGrid(
    List<TMain> list,
    List<CRMarketingMemberForImport> membersToCreate);

  public virtual Contact FindContactByContactID(int? contactID)
  {
    return PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>>, OrderBy<Asc<Contact.contactPriority>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) contactID
    }));
  }

  public virtual Contact FindContactByMemberName(string contactDisplayName)
  {
    return PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.memberName, Equal<Required<Contact.memberName>>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, Or<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>>, OrderBy<Asc<Contact.contactPriority>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) contactDisplayName
    }));
  }

  public static void ProcessingHandler(
    List<CRMarketingMemberForImport> newMembers,
    CancellationToken cancellationToken)
  {
    PXGraph.CreateInstance<TGraph>().GetProcessingExtension<MarketingMemberImport<TGraph, TMain, TDetail>>().CreateEntityFromImport(newMembers, cancellationToken);
  }

  public virtual void CreateEntityFromImport(
    List<CRMarketingMemberForImport> newMembers,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    ContactMaint graph1 = (ContactMaint) null;
    LeadMaint graph2 = (LeadMaint) null;
    BusinessAccountMaint graph3 = (BusinessAccountMaint) null;
    for (int index = 0; index < newMembers.Count; ++index)
    {
      CRMarketingMemberForImport newMember = newMembers[index];
      Contact contact = (Contact) newMember;
      try
      {
        if (newMember.ContactType == "LD")
        {
          if (num2++ % 100 == 0 || graph2 == null)
          {
            graph2 = PXGraph.CreateInstance<LeadMaint>();
            LeadMaint.CRDuplicateEntitiesForLeadGraphExt extension = ((PXGraph) graph2).GetExtension<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>();
            if (extension != null)
              extension.HardBlockOnly = true;
          }
          contact = (Contact) this.CreateLeadFromImport(graph2, newMember);
          ((PXAction<CRLead>) graph2.Save).PressImpl(false, true);
        }
        else if (newMember.ContactType == "PN")
        {
          if (num1++ % 100 == 0 || graph1 == null)
          {
            graph1 = PXGraph.CreateInstance<ContactMaint>();
            ContactMaint.CRDuplicateEntitiesForContactGraphExt extension = ((PXGraph) graph1).GetExtension<ContactMaint.CRDuplicateEntitiesForContactGraphExt>();
            if (extension != null)
              extension.HardBlockOnly = true;
          }
          contact = this.CreateContactFromImport(graph1, newMember);
          ((PXAction<Contact>) graph1.Save).PressImpl(false, true);
        }
        else if (newMember.ContactType == "AP")
        {
          if (newMember.ExistingContactID.HasValue)
          {
            if (num3++ % 100 == 0 || graph3 == null)
            {
              graph3 = PXGraph.CreateInstance<BusinessAccountMaint>();
              BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt extension = ((PXGraph) graph3).GetExtension<BusinessAccountMaint.CRDuplicateEntitiesForBAccountGraphExt>();
              if (extension != null)
                extension.HardBlockOnly = true;
            }
            contact = Contact.PK.Find((PXGraph) this.Base, newMember.ExistingContactID);
            this.InsertBAMarketingInfoIfNotExists(graph3, newMember.ExistingContactID, newMember);
            ((PXAction<BAccount>) graph3.Save).PressImpl(false, true);
          }
          else
            continue;
        }
        string str = (!string.IsNullOrWhiteSpace(contact?.DisplayName) ? contact?.DisplayName : contact?.FullName) ?? contact.MemberName;
        PXProcessing<TMain>.SetInfo(index, PXMessages.LocalizeFormatNoPrefixNLA("{0} has been added to the list.", new object[1]
        {
          (object) str
        }));
      }
      catch (PXException ex)
      {
        string str = !string.IsNullOrWhiteSpace(contact?.DisplayName) ? contact?.DisplayName : contact?.FullName;
        if (str == null && contact.MemberName != null)
          PXProcessing<TMain>.SetWarning(index, PXMessages.LocalizeFormatNoPrefixNLA("{0} has not been found in the system and the mapping file does not contain the required field values to create a new record.", new object[1]
          {
            (object) contact.MemberName
          }));
        else
          PXProcessing<TMain>.SetError(index, PXMessages.LocalizeFormatNoPrefixNLA("{0} has not been added to the list because of the following error: {1}", new object[2]
          {
            (object) str,
            (object) ((Exception) ex).Message
          }));
      }
      finally
      {
        ((PXGraph) graph1)?.Clear();
        ((PXGraph) graph2)?.Clear();
        ((PXGraph) graph3)?.Clear();
      }
    }
  }

  public virtual Contact CreateContactFromImport(
    ContactMaint graph,
    CRMarketingMemberForImport newMember)
  {
    Contact contactFromImport;
    if (!newMember.ExistingContactID.HasValue)
      contactFromImport = this.CreateNewContactFromImport(graph, newMember);
    else
      ((PXSelectBase<Contact>) graph.Contact).Current = contactFromImport = Contact.PK.Find((PXGraph) this.Base, newMember.ExistingContactID);
    this.InsertContactMarketingInfoIfNotExists(graph, contactFromImport, newMember);
    return contactFromImport;
  }

  public virtual Contact CreateNewContactFromImport(
    ContactMaint graph,
    CRMarketingMemberForImport newMember)
  {
    Contact contact = ((PXSelectBase<Contact>) graph.Contact).Insert((Contact) newMember);
    contact.OverrideAddress = new bool?(contact.BAccountID.HasValue);
    Contact contactFromImport = ((PXSelectBase<Contact>) graph.Contact).Update(contact);
    if (((PXSelectBase) graph.AddressCurrent).View.SelectSingle(Array.Empty<object>()) is Address address)
    {
      address.CountryID = newMember.CountryID;
      address.City = newMember.City;
      address.State = newMember.State;
      address.PostalCode = newMember.PostalCode;
      address.AddressLine1 = newMember.AddressLine1;
      address.AddressLine2 = newMember.AddressLine2;
      address.AddressLine3 = newMember.AddressLine3;
      address.Department = newMember.Department;
      address.SubDepartment = newMember.SubDepartment;
      address.StreetName = newMember.StreetName;
      address.BuildingNumber = newMember.BuildingNumber;
      address.BuildingName = newMember.BuildingName;
      address.Floor = newMember.Floor;
      address.UnitNumber = newMember.UnitNumber;
      address.PostBox = newMember.PostBox;
      address.Room = newMember.Room;
      address.TownLocationName = newMember.TownLocationName;
      address.DistrictName = newMember.DistrictName;
      address.Latitude = newMember.Latitude;
      address.Longitude = newMember.Longitude;
      ((PXSelectBase) graph.AddressCurrent).Cache.Update((object) address);
    }
    return contactFromImport;
  }

  public abstract void InsertContactMarketingInfoIfNotExists(
    ContactMaint graph,
    Contact contact,
    CRMarketingMemberForImport newMember);

  public virtual CRLead CreateLeadFromImport(LeadMaint graph, CRMarketingMemberForImport newMember)
  {
    CRLead newLeadFromImport;
    if (!newMember.ExistingContactID.HasValue)
      newLeadFromImport = this.CreateNewLeadFromImport(graph, newMember);
    else
      ((PXSelectBase<CRLead>) graph.Lead).Current = newLeadFromImport = CRLead.PK.Find((PXGraph) this.Base, newMember.ExistingContactID);
    this.InsertLeadMarketingInfoIfNotExtsis(graph, newLeadFromImport, newMember);
    return newLeadFromImport;
  }

  public virtual CRLead CreateNewLeadFromImport(
    LeadMaint graph,
    CRMarketingMemberForImport newMember)
  {
    CRLead crLead1 = ((PXSelectBase<CRLead>) graph.Lead).Extend<Contact>((Contact) newMember);
    ((PXSelectBase) graph.Lead).Cache.SetStatus((object) crLead1, (PXEntryStatus) 2);
    ((PXSelectBase) graph.Lead).Cache.SetDefaultExt<CRLead.resolution>((object) crLead1);
    crLead1.OverrideRefContact = new bool?(crLead1.BAccountID.HasValue);
    crLead1.Description = newMember.Description;
    CRLead crLead2 = ((PXSelectBase<CRLead>) graph.Lead).Update(crLead1);
    Address address = ((PXSelectBase<Address>) graph.AddressCurrent).Insert();
    crLead2.DefAddressID = address.AddressID;
    CRLead newLeadFromImport = ((PXSelectBase<CRLead>) graph.Lead).Update(crLead2);
    address.CountryID = newMember.CountryID;
    address.City = newMember.City;
    address.State = newMember.State;
    address.PostalCode = newMember.PostalCode;
    address.AddressLine1 = newMember.AddressLine1;
    address.AddressLine2 = newMember.AddressLine2;
    address.AddressLine3 = newMember.AddressLine3;
    address.Department = newMember.Department;
    address.SubDepartment = newMember.SubDepartment;
    address.StreetName = newMember.StreetName;
    address.BuildingNumber = newMember.BuildingNumber;
    address.BuildingName = newMember.BuildingName;
    address.Floor = newMember.Floor;
    address.UnitNumber = newMember.UnitNumber;
    address.PostBox = newMember.PostBox;
    address.Room = newMember.Room;
    address.TownLocationName = newMember.TownLocationName;
    address.DistrictName = newMember.DistrictName;
    address.Latitude = newMember.Latitude;
    address.Longitude = newMember.Longitude;
    ((PXSelectBase) graph.AddressCurrent).Cache.Update((object) address);
    return newLeadFromImport;
  }

  public abstract void InsertLeadMarketingInfoIfNotExtsis(
    LeadMaint graph,
    CRLead contact,
    CRMarketingMemberForImport newMember);

  public abstract void InsertBAMarketingInfoIfNotExists(
    BusinessAccountMaint graph,
    int? contactID,
    CRMarketingMemberForImport newMember);

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (!values.Contains((object) "MemberName") || values[(object) "MemberName"] == null)
      return true;
    int result;
    Contact contact = !int.TryParse(values[(object) "MemberName"].ToString(), out result) ? this.FindContactByMemberName(values[(object) "MemberName"].ToString()) : this.FindContactByContactID(new int?(result));
    if (contact == null)
      return true;
    values[(object) "ExistingContactID"] = (object) contact.ContactID;
    values[(object) "ContactType"] = (object) contact.ContactType;
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this.Base.Actions["ProcessAll"].Press();
  }
}
