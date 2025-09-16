// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.CRDuplicateEntities;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.LeadMaint_Extensions;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR;

public class LeadMaint : PXGraph<
#nullable disable
LeadMaint, CRLead, CRLead.displayName>, ICaptionable
{
  [PXHidden]
  public PXSelect<BAccount> bAccountBasic;
  [PXHidden]
  public PXSelect<Contact> ContactDummy;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<Company> company;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<CRSetup> Setup;
  [PXViewName("Address")]
  public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  Address.addressID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRLead.defAddressID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  Address>.View AddressCurrent;
  [PXViewName("Lead")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRLead.status), typeof (CRLead.resolution), typeof (CRLead.duplicateStatus), typeof (CRLead.duplicateFound)})]
  public FbqlSelect<SelectFromBase<CRLead, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<
  #nullable enable
  BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  CRLead.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRLead.contactType, 
  #nullable disable
  Equal<ContactTypesAttribute.lead>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BAccount.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<BAccount, Current<AccessInfo.userName>>>>>>, CRLead>.View Lead;
  [PXHidden]
  public PXSelect<CRLead, Where<CRLead.contactID, Equal<Current<CRLead.contactID>>>> LeadCurrent;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<CRLead.noteID>>>> LeadActivityStatistics;
  [PXHidden]
  public PXSelect<CRLead, Where<CRLead.contactID, Equal<Current<CRLead.contactID>>>> LeadCurrent2;
  [PXViewName("Answers")]
  public CRAttributeList<CRLead> Answers;
  [PXViewName("Opportunities")]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CROpportunity, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRLead>.On<BqlOperand<
  #nullable enable
  CROpportunity.leadID, IBqlGuid>.IsEqual<
  #nullable disable
  CRLead.noteID>>>, FbqlJoins.Left<CROpportunityClass>.On<BqlOperand<
  #nullable enable
  CROpportunityClass.cROpportunityClassID, IBqlString>.IsEqual<
  #nullable disable
  CROpportunity.classID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRLead.contactID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CRLead.contactID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CRLead.contactType, IBqlString>.IsEqual<
  #nullable disable
  ContactTypesAttribute.lead>>>, CROpportunity>.View Opportunities;
  [PXViewName("Campaign Members")]
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<CRCampaignMembers, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRCampaign>.On<BqlOperand<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.IsEqual<
  #nullable disable
  CRCampaignMembers.campaignID>>>, FbqlJoins.Left<CRMarketingList>.On<BqlOperand<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.marketingListID>>>, FbqlJoins.Left<Contact>.On<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRCampaignMembers.contactID>>>>.Where<BqlOperand<
  #nullable enable
  CRCampaignMembers.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRLead.contactID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CRCampaignMembers>.View Members;
  [PXHidden]
  public PXSelect<CRMarketingListMember> Subscriptions_stub;
  [PXHidden]
  public PXSelectReadonly<CRLeadClass, Where<CRLeadClass.classID, Equal<Current<CRLead.classID>>>> LeadClass;
  public PXMenuAction<CRLead> Action;

  public LeadMaint()
  {
    PXUIFieldAttribute.SetDisplayName<BAccountCRM.acctCD>(((PXSelectBase) this.bAccountBasic).Cache, "Business Account");
    PXUIFieldAttribute.SetEnabled<Contact.assignDate>(((PXSelectBase) this.Lead).Cache, ((PXSelectBase) this.Lead).Cache.Current, false);
    if (((PXGraph) this).IsImport && !((PXGraph) this).IsExport)
    {
      ((PXSelectBase<CRLead>) this.Lead).WhereNew<Where<CRLead.contactType, Equal<ContactTypesAttribute.lead>>>();
      ((PXSelectBase<CRLead>) this.Lead).OrderByNew<OrderBy<Desc<CRLead.isActive, Desc<CRLead.duplicateStatus, Asc<CRLead.contactID>>>>>();
    }
    PXUIFieldAttribute.SetVisible<Contact.languageID>(((PXSelectBase) this.LeadCurrent).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }

  public string Caption()
  {
    CRLead current = ((PXSelectBase<CRLead>) this.Lead).Current;
    if (current == null)
      return "";
    string str = !string.IsNullOrEmpty(current.DisplayName) ? current.DisplayName : current.FullName;
    return !string.IsNullOrEmpty(current.LastName) && !string.IsNullOrEmpty(current.FullName) ? $"{str} - {current.FullName}" : str ?? "";
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRLead> e)
  {
    CRLead row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CRLead.contactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRLead>>) e).Cache, (object) row, true);
    this.ConfigureAddressSectionUI();
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRLead>>) e).Cache;
    CRLead crLead = row;
    int? nullable = row.RefContactID;
    int num;
    if (!nullable.HasValue)
    {
      nullable = row.BAccountID;
      num = nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    PXUIFieldAttribute.SetEnabled<CRLead.overrideRefContact>(cache, (object) crLead, num != 0);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRLead> e)
  {
    CRLead row = e.Row;
    if (row == null || !((PXGraph) this).IsImport || ((PXGraph) this).IsExport)
      return;
    bool flag = false;
    foreach (string field in (List<string>) ((PXGraph) this).Caches[typeof (CRLead)].Fields)
    {
      if (!object.Equals(((PXGraph) this).Caches[typeof (CRLead)].GetValueOriginal((object) row, field), ((PXGraph) this).Caches[typeof (CRLead)].GetValue((object) row, field)))
      {
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<Address> e)
  {
    Address row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<Address.isValidated>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<Address>>) e).Cache, (object) row, false);
    this.ConfigureAddressSectionUI();
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRLead.bAccountID> e)
  {
  }

  [PXUIField(DisplayName = "Lead")]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRLead.contactID> e)
  {
  }

  [PXDBDefault(typeof (CRLead.contactID))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCampaignMembers.contactID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Campaign")]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCampaignMembers.campaignID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Marketing List")]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRMarketingList.mailListCode> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Class Description")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityClass.description> e)
  {
  }

  private void ConfigureAddressSectionUI()
  {
    CRLead current1 = ((PXGraph) this).Caches[typeof (CRLead)].Current as CRLead;
    PXCache cach = ((PXGraph) this).Caches[typeof (Address)];
    Address current2 = cach.Current as Address;
    if (current1 == null || current2 == null)
      return;
    string str = "";
    bool flag1;
    if (current1.OverrideRefContact.GetValueOrDefault() || !current1.RefContactID.HasValue)
    {
      flag1 = true;
    }
    else
    {
      PXResultset<Contact> pxResultset = PXSelectBase<Contact, PXViewOf<Contact>.BasedOn<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.Where<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<BqlField<CRLead.refContactID, IBqlInt>.FromCurrent>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>());
      BAccount baccount = (BAccount) null;
      Contact contact = (Contact) null;
      foreach (PXResult<Contact, BAccount> pxResult in pxResultset)
      {
        contact = PXResult<Contact, BAccount>.op_Implicit(pxResult);
        baccount = PXResult<Contact, BAccount>.op_Implicit(pxResult);
      }
      bool flag2 = baccount != null && baccount.Type == "PR";
      int num;
      if (contact != null && baccount != null)
      {
        int? defAddressId1 = contact.DefAddressID;
        int? defAddressId2 = baccount.DefAddressID;
        num = defAddressId1.GetValueOrDefault() == defAddressId2.GetValueOrDefault() & defAddressId1.HasValue == defAddressId2.HasValue ? 1 : 0;
      }
      else
        num = 0;
      bool flag3 = num != 0;
      flag1 = baccount == null | flag2 || !flag3;
      str = flag2 & flag3 ? "If you make changes to the address, the changes will be also saved to the address of the business account specified for the related contact. If you want to save the changes only to the current record, select the Override check box." : "";
    }
    PXUIFieldAttribute.SetWarning<CRLead.overrideRefContact>(((PXGraph) this).Caches[typeof (CRLead)], (object) current1, str);
    PXUIFieldAttribute.SetEnabled<Address.addressLine1>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.addressLine2>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.city>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.state>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.postalCode>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.department>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.subDepartment>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.streetName>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.buildingNumber>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.buildingName>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.floor>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.unitNumber>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.postBox>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.room>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.townLocationName>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.districtName>(cach, (object) current2, flag1);
    PXUIFieldAttribute.SetEnabled<Address.countryID>(cach, (object) current2, flag1);
  }

  /// <exclude />
  public class DefaultLeadOwnerGraphExt : 
    CRDefaultDocumentOwner<LeadMaint, CRLead, CRLead.classID, CRLead.ownerID, CRLead.workgroupID>
  {
  }

  /// <exclude />
  public class LeadBAccountSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.DocumentMapping(typeof (CRLead))
      {
        RelatedID = typeof (CRLead.bAccountID),
        ChildID = typeof (CRLead.defAddressID),
        IsOverrideRelated = typeof (CRLead.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.RelatedMapping(typeof (BAccount))
      {
        RelatedID = typeof (BAccount.bAccountID),
        ChildID = typeof (BAccount.defAddressID)
      };
    }

    protected override CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.ChildMapping(typeof (Address))
      {
        ChildID = typeof (Address.addressID),
        RelatedID = typeof (Address.bAccountID)
      };
    }

    protected override void _(
      PX.Data.Events.RowUpdating<CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document> e)
    {
      if (!(e.NewRow?.Base is CRLead lead1) || !(e.Row?.Base is CRLead lead2))
        return;
      bool? overrideRefContact1 = lead1.OverrideRefContact;
      bool? overrideRefContact2 = lead2.OverrideRefContact;
      if (overrideRefContact1.GetValueOrDefault() == overrideRefContact2.GetValueOrDefault() & overrideRefContact1.HasValue == overrideRefContact2.HasValue)
        return;
      CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document newRow = e.NewRow;
      bool? overrideRefContact3 = lead1.OverrideRefContact;
      bool? nullable1 = new bool?(overrideRefContact3.HasValue && overrideRefContact3.GetValueOrDefault() || !this.IsSharedBAccountAddressAvailable(lead1));
      newRow.IsOverrideRelated = nullable1;
      CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document row = e.Row;
      overrideRefContact3 = lead2.OverrideRefContact;
      bool? nullable2 = new bool?(overrideRefContact3.HasValue && overrideRefContact3.GetValueOrDefault() || !this.IsSharedBAccountAddressAvailable(lead2));
      row.IsOverrideRelated = nullable2;
      base._(e);
    }

    public virtual void UpdateRelated(CRLead newRow, CRLead oldRow)
    {
      this.UpdateRelated(PXCacheEx.GetExtension<CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document>((IBqlTable) newRow), PXCacheEx.GetExtension<CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document>((IBqlTable) oldRow));
    }

    public virtual void UpdateRelatedOnBAccountIDChange(CRLead newRow, int? oldBAccountID)
    {
      int? nullable1 = oldBAccountID;
      int? baccountId = newRow.BAccountID;
      if (nullable1.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable1.HasValue == baccountId.HasValue)
        return;
      CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document extension = PXCacheEx.GetExtension<CRParentChild<LeadMaint, LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>.Document>((IBqlTable) newRow);
      bool? overrideAddress = newRow.OverrideAddress;
      bool? nullable2 = new bool?(overrideAddress.HasValue && overrideAddress.GetValueOrDefault() || !this.IsSharedBAccountAddressAvailable(newRow));
      extension.IsOverrideRelated = nullable2;
      CRLead copy = (CRLead) ((PXSelectBase) this.Base.Lead).Cache.CreateCopy((object) newRow);
      ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.bAccountID>((object) copy, (object) oldBAccountID);
      ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.defAddressID>((object) newRow, (object) null);
      this.UpdateRelated(newRow, copy);
    }

    public virtual bool IsSharedBAccountAddressAvailable(CRLead lead)
    {
      return BAccount.PK.Find((PXGraph) this.Base, lead.BAccountID)?.Type == "PR";
    }
  }

  /// <exclude />
  public class LeadAddressActions : CRAddressActions<LeadMaint, CRLead>
  {
    protected override CRParentChild<LeadMaint, CRAddressActions<LeadMaint, CRLead>>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<LeadMaint, CRAddressActions<LeadMaint, CRLead>>.ChildMapping(typeof (Address))
      {
        ChildID = typeof (Address.addressID),
        RelatedID = typeof (Address.bAccountID)
      };
    }

    protected override CRParentChild<LeadMaint, CRAddressActions<LeadMaint, CRLead>>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<LeadMaint, CRAddressActions<LeadMaint, CRLead>>.DocumentMapping(typeof (CRLead))
      {
        RelatedID = typeof (CRLead.bAccountID),
        ChildID = typeof (CRLead.defAddressID)
      };
    }
  }

  /// <exclude />
  public class CRDuplicateEntitiesForLeadGraphExt : PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>
  {
    public override System.Type AdditionalConditions
    {
      get
      {
        return typeof (BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<DuplicateDocument.refContactID>, IsNotNull>>>, And<BqlOperand<CRDuplicateGrams.entityID, IBqlInt>.IsNotEqual<BqlField<DuplicateDocument.refContactID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<Current<DuplicateDocument.refContactID>, IBqlInt>.IsNull>>>, And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<DuplicateDocument.bAccountID>, IsNotNull>>>, And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>, And<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsNotEqual<BqlField<DuplicateDocument.bAccountID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<DuplicateContact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.bAccountProperty>>>>>>.Or<BqlOperand<Current<DuplicateDocument.bAccountID>, IBqlInt>.IsNull>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.isActive, Equal<True>>>>, And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsNotEqual<ContactTypesAttribute.bAccountProperty>>>>.Or<BqlOperand<BAccountR.status, IBqlString>.IsNotEqual<CustomerStatus.inactive>>>);
      }
    }

    public override string WarningMessage => "This lead probably has duplicates";

    public static bool IsActive() => PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>.IsExtensionActive();

    public override void Initialize()
    {
      base.Initialize();
      this.DuplicateDocuments = new PXSelectExtension<DuplicateDocument>((PXSelectBase) this.Base.LeadCurrent);
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>.DocumentMapping(typeof (CRLead))
      {
        Key = typeof (CRLead.contactID)
      };
    }

    protected override PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>.DuplicateDocumentMapping GetDuplicateDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<LeadMaint, CRLead>.DuplicateDocumentMapping(typeof (CRLead))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<CRLead, CRLead.isActive> e)
    {
      CRLead row = e.Row;
      if (e.Row == null || !row.IsActive.GetValueOrDefault())
        return;
      bool? isActive = row.IsActive;
      bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CRLead, CRLead.isActive>, CRLead, object>) e).OldValue;
      if (isActive.GetValueOrDefault() == oldValue.GetValueOrDefault() & isActive.HasValue == oldValue.HasValue)
        return;
      row.DuplicateStatus = "NV";
    }

    protected override void _(PX.Data.Events.RowSelected<CRDuplicateRecord> e)
    {
      base._(e);
      CRDuplicateRecord row = e.Row;
      if (row == null || row.CanBeMerged.GetValueOrDefault())
        return;
      ((PXSelectBase) this.DuplicatesForMerging).Cache.RaiseExceptionHandling<CRDuplicateRecord.canBeMerged>((object) row, (object) row.CanBeMerged, (Exception) new PXSetPropertyException("The duplicate records cannot be merged because they are associated with different records.", (PXErrorLevel) 3));
    }

    public override CRLead GetTargetEntity(int targetID)
    {
      return PXResultset<CRLead>.op_Implicit(PXSelectBase<CRLead, PXSelect<CRLead, Where<CRLead.contactID, Equal<Required<CRLead.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetID
      }));
    }

    public override Contact GetTargetContact(CRLead targetEntity) => (Contact) targetEntity;

    public override Address GetTargetAddress(CRLead targetEntity)
    {
      return PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelect<Address, Where<Address.addressID, Equal<Required<Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) targetEntity.DefAddressID
      }));
    }

    protected override bool WhereMergingMet(CRDuplicateResult result)
    {
      DuplicateDocument current = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current;
      CRDuplicateRecord crDuplicateRecord = ((PXResult) result).GetItem<CRDuplicateRecord>();
      return crDuplicateRecord != null && crDuplicateRecord.DuplicateContactType == "LD";
    }

    protected override bool CanBeMerged(CRDuplicateResult result)
    {
      DuplicateDocument current = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current;
      CRDuplicateRecord crDuplicateRecord = ((PXResult) result).GetItem<CRDuplicateRecord>();
      if (crDuplicateRecord != null)
      {
        int? nullable1 = current.RefContactID;
        int? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = crDuplicateRecord.DuplicateRefContactID;
          if (nullable1.HasValue)
          {
            nullable1 = crDuplicateRecord.DuplicateRefContactID;
            nullable2 = current.RefContactID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
              goto label_8;
          }
        }
        nullable2 = current.BAccountID;
        if (nullable2.HasValue)
        {
          nullable2 = crDuplicateRecord.DuplicateBAccountID;
          if (nullable2.HasValue)
          {
            nullable2 = crDuplicateRecord.DuplicateBAccountID;
            nullable1 = current.BAccountID;
            return nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue;
          }
        }
        return true;
      }
label_8:
      return false;
    }

    public override void DoDuplicateAttach(DuplicateDocument duplicateDocument)
    {
      if (!(((PXSelectBase) this.DuplicatesForLinking).Cache.Current is CRDuplicateRecord current))
        return;
      Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Current<CRDuplicateRecord.duplicateContactID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) current
      }, Array.Empty<object>()));
      if (contact == null)
        return;
      if (contact.ContactType == "AP")
      {
        duplicateDocument.RefContactID = new int?();
        duplicateDocument.BAccountID = new int?();
        ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Update(duplicateDocument);
        duplicateDocument.BAccountID = contact.BAccountID;
      }
      else
      {
        duplicateDocument.RefContactID = contact.ContactType == "PN" ? contact.ContactID : throw new PXException("A lead can be linked only with a contact or with a business account.");
        duplicateDocument.BAccountID = contact.BAccountID;
      }
      ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Update(duplicateDocument);
    }

    public override void ValidateEntitiesBeforeMerge(List<CRLead> duplicateEntities)
    {
      int? nullable1 = new int?();
      foreach (CRLead duplicateEntity in duplicateEntities)
      {
        int? nullable2 = duplicateEntity.RefContactID;
        if (nullable2.HasValue)
        {
          if (!nullable1.HasValue)
          {
            nullable1 = duplicateEntity.RefContactID;
          }
          else
          {
            nullable2 = nullable1;
            int? refContactId = duplicateEntity.RefContactID;
            if (!(nullable2.GetValueOrDefault() == refContactId.GetValueOrDefault() & nullable2.HasValue == refContactId.HasValue))
              throw new PXException("The duplicates cannot be merged because they are linked with different contacts.");
          }
        }
      }
    }

    public override PXResult<Contact> GetGramContext(DuplicateDocument duplicateDocument)
    {
      return (PXResult<Contact>) new PXResult<Contact, Address>(duplicateDocument.Base as Contact, ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>()));
    }
  }

  /// <exclude />
  public class CreateAccountFromLeadGraphExt : CRCreateAccountAction<LeadMaint, CRLead>
  {
    protected override string TargetType => "PX.Objects.CR.CRLead";

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.AddressCurrent);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.LeadCurrent);
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping(typeof (CRLead))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping(typeof (Address));
    }

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<LeadMaint_ActivityDetailsExt>().Activities;
      }
    }

    protected override void _(PX.Data.Events.RowSelected<AccountsFilter> e)
    {
      this.NeedToUse = (bool?) ((PXSelectBase<CRLeadClass>) this.Base.LeadClass).SelectSingle(Array.Empty<object>())?.RequireBAccountCreation ?? true;
      base._(e);
      PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) e.Row).For<AccountsFilter.linkContactToAccount>((System.Action<PXUIFieldAttribute>) (_ => _.Visible = false));
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass> e)
    {
      BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
      if (baccount != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) baccount.ClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
      else
      {
        if (((PXSelectBase<CRLead>) this.Base.Lead).Current == null)
          return;
        CRLeadClass crLeadClass = ((PXSelectBase<CRLeadClass>) this.Base.LeadClass).SelectSingle(Array.Empty<object>());
        if (crLeadClass != null && crLeadClass.TargetBAccountClassID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) crLeadClass.TargetBAccountClassID;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultCustomerClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
    }
  }

  /// <exclude />
  public class CreateContactFromLeadGraphExt : CRCreateContactAction<LeadMaint, CRLead>
  {
    protected override string TargetType => "PX.Objects.CR.CRLead";

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.AddressCurrent);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.LeadCurrent);
      this.ContactMethod = new PXSelectExtension<DocumentContactMethod>((PXSelectBase) this.Base.LeadCurrent);
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping(typeof (CRLead))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected override CRCreateContactAction<LeadMaint, CRLead>.DocumentContactMethodMapping GetDocumentContactMethodMapping()
    {
      return new CRCreateContactAction<LeadMaint, CRLead>.DocumentContactMethodMapping(typeof (CRLead));
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping(typeof (Address));
    }

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<LeadMaint_ActivityDetailsExt>().Activities;
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass> e)
    {
      Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
      if (contact != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) contact.ClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
      }
      else
      {
        CRLead current = ((PXSelectBase<CRLead>) this.Base.Lead).Current;
        if (current == null)
          return;
        CRLeadClass crLeadClass = PXResultset<CRLeadClass>.op_Implicit(PXSelectBase<CRLeadClass, PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Required<CRLead.classID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) current.ClassID
        }));
        if (crLeadClass != null && crLeadClass.TargetContactClassID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) crLeadClass.TargetContactClassID;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultContactClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
      }
    }
  }

  /// <exclude />
  public class CreateOpportunityFromLeadGraphExt : CRCreateOpportunityAction<LeadMaint, CRLead>
  {
    protected override string TargetType => "PX.Objects.CR.CRLead";

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.AddressCurrent);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.LeadCurrent);
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentContactMapping(typeof (CRLead))
      {
        Email = typeof (Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<LeadMaint, CRLead>.DocumentAddressMapping(typeof (Address));
    }

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<LeadMaint_ActivityDetailsExt>().Activities;
      }
    }

    public virtual void _(
      PX.Data.Events.FieldDefaulting<OpportunityFilter, OpportunityFilter.opportunityClass> e)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<OpportunityFilter, OpportunityFilter.opportunityClass>, OpportunityFilter, object>) e).NewValue = (object) (((PXSelectBase<CRLeadClass>) this.Base.LeadClass).SelectSingle(Array.Empty<object>())?.TargetOpportunityClassID ?? ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultOpportunityClassID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<OpportunityFilter, OpportunityFilter.opportunityClass>>) e).Cancel = true;
    }

    protected override CROpportunity CreateMaster(
      OpportunityMaint graph,
      OpportunityConversionOptions options)
    {
      CROpportunity master1 = base.CreateMaster(graph, options);
      string opportunityStage = ((PXSelectBase<CRLeadClass>) this.Base.LeadClass).SelectSingle(Array.Empty<object>())?.TargetOpportunityStage;
      if (opportunityStage != null)
        master1.StageID = opportunityStage;
      CROpportunity master2 = ((PXSelectBase<CROpportunity>) graph.Opportunity).Update(master1);
      ((SelectedEntityEvent<CROpportunity>) PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>.Select((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity.Events>>>) (o => o.OpportunityCreatedFromLead))).FireOn((PXGraph) graph, master2);
      return master2;
    }
  }

  /// <exclude />
  public class CreateBothAccountAndContactFromLeadGraphExt : 
    CRCreateBothContactAndAccountAction<LeadMaint, CRLead, LeadMaint.CreateAccountFromLeadGraphExt, LeadMaint.CreateContactFromLeadGraphExt>
  {
  }

  /// <exclude />
  public class CreateOpportunityAllFromLeadGraphExt : 
    CRCreateOpportunityAllAction<LeadMaint, CRLead, LeadMaint.CreateOpportunityFromLeadGraphExt, LeadMaint.CreateAccountFromLeadGraphExt, LeadMaint.CreateContactFromLeadGraphExt>
  {
  }

  /// <exclude />
  public class UpdateRelatedContactInfoFromLeadGraphExt : 
    CRUpdateRelatedContactInfoGraphExt<LeadMaint>
  {
    public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<
    #nullable enable
    Contact.defAddressID, IBqlInt>.IsEqual<
    #nullable disable
    Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<
    #nullable disable
    Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<
    #nullable enable
    BAccount.bAccountID, IBqlInt>.IsEqual<
    #nullable disable
    Contact.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.refContactID, 
    #nullable disable
    Equal<P.AsInt>>>>, And<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<
    #nullable disable
    False>>>, Or<BqlOperand<
    #nullable enable
    BAccount.bAccountID, IBqlInt>.IsNotNull>>, 
    #nullable disable
    And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<
    #nullable enable
    BAccount.type, IBqlString>.IsIn<
    #nullable disable
    BAccountType.customerType, BAccountType.vendorType, BAccountType.combinedType>>>, And<BqlOperand<
    #nullable enable
    Contact.defAddressID, IBqlInt>.IsNotEqual<
    #nullable disable
    BAccount.defAddressID>>>>.Or<BqlOperand<
    #nullable enable
    BAccount.type, IBqlString>.IsEqual<
    #nullable disable
    BAccountType.prospectType>>>>, And<BqlOperand<
    #nullable enable
    Contact.contactID, IBqlInt>.IsEqual<
    #nullable disable
    P.AsInt>>>, Or<BqlOperand<
    #nullable enable
    Contact.bAccountID, IBqlInt>.IsNull>>>.And<
    #nullable disable
    BqlOperand<
    #nullable enable
    Contact.contactID, IBqlInt>.IsEqual<
    #nullable disable
    P.AsInt>>>, Address>.View RefContactRelatedAddresses;
    public FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<
    #nullable enable
    Contact.defAddressID, IBqlInt>.IsEqual<
    #nullable disable
    Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<
    #nullable disable
    Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<
    #nullable enable
    BAccount.defContactID, IBqlInt>.IsEqual<
    #nullable disable
    Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    Contact.bAccountID, 
    #nullable disable
    Equal<P.AsInt>>>>, And<BqlOperand<
    #nullable enable
    PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<
    #nullable disable
    False>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    BAccount.bAccountID, 
    #nullable disable
    Equal<P.AsInt>>>>>.And<BqlOperand<
    #nullable enable
    BAccount.type, IBqlString>.IsEqual<
    #nullable disable
    BAccountType.prospectType>>>>, Address>.View BAccountRelatedAddresses;

    protected virtual void _(PX.Data.Events.RowPersisting<CRLead> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<CRLead>(e, this.GetFields_ContactInfoExt(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRLead>>) e).Cache, (object) e.Row).Union<string>((IEnumerable<string>) new string[1]
      {
        "DefAddressID"
      }));
      this.SetUpdateRelatedInfo<CRLead, CRLead.refContactID>(e);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<Address> e)
    {
      if (e.Row == null)
        return;
      this.SetUpdateRelatedInfo<Address>(e, this.GetFields_ContactInfoExt(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<Address>>) e).Cache));
    }

    protected virtual void _(PX.Data.Events.RowPersisted<CRLead> e)
    {
      CRLead row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 1, (PXDBOperation) 2) || row.OverrideRefContact.GetValueOrDefault())
        return;
      if (row.RefContactID.HasValue)
      {
        this.UpdateContact<FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>>.Or<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>, Contact>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<CRLead>>) e).Cache, (object) row, new FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>>.Or<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>, Contact>.View((PXGraph) this.Base), (object) row.RefContactID, (object) row.RefContactID);
      }
      else
      {
        if (!row.BAccountID.HasValue)
          return;
        this.UpdateContact<FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.prospectType>>>>, Contact>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<CRLead>>) e).Cache, (object) row, new FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.prospectType>>>>, Contact>.View((PXGraph) this.Base), (object) row.BAccountID, (object) row.BAccountID);
      }
    }

    protected virtual void _(PX.Data.Events.RowPersisted<Address> e)
    {
      Address row = e.Row;
      if (row == null || !this.UpdateRelatedInfo.GetValueOrDefault() || e.TranStatus != null || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 1, (PXDBOperation) 2))
        return;
      CRLead crLead1 = ((PXSelectBase<CRLead>) this.Base.Lead).Current;
      if (crLead1 == null)
        crLead1 = PXResultset<CRLead>.op_Implicit(PXSelectBase<CRLead, PXSelect<CRLead, Where<BqlOperand<CRLead.defAddressID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) row.AddressID
        }));
      CRLead crLead2 = crLead1;
      if (crLead2 == null || crLead2.OverrideRefContact.GetValueOrDefault())
        return;
      if (crLead2.RefContactID.HasValue)
      {
        this.UpdateAddress<FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>, Or<BqlOperand<BAccount.bAccountID, IBqlInt>.IsNotNull>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<BAccount.type, IBqlString>.IsIn<BAccountType.customerType, BAccountType.vendorType, BAccountType.combinedType>>>, And<BqlOperand<Contact.defAddressID, IBqlInt>.IsNotEqual<BAccount.defAddressID>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.prospectType>>>>, And<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>, Or<BqlOperand<Contact.bAccountID, IBqlInt>.IsNull>>>.And<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<P.AsInt>>>, Address>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<Address>>) e).Cache, (object) row, this.RefContactRelatedAddresses, (object) crLead2.RefContactID, (object) crLead2.RefContactID, (object) crLead2.RefContactID);
      }
      else
      {
        if (!crLead2.BAccountID.HasValue)
          return;
        this.UpdateAddress<FbqlSelect<SelectFromBase<Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.defAddressID, IBqlInt>.IsEqual<Address.addressID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<BAccount.defContactID, IBqlInt>.IsEqual<Contact.contactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact.bAccountID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.Standalone.CRLead.overrideRefContact, IBqlBool>.IsEqual<False>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.prospectType>>>>, Address>.View>(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<Address>>) e).Cache, (object) row, this.BAccountRelatedAddresses, (object) crLead2.BAccountID, (object) crLead2.BAccountID);
      }
    }

    [PXOverride]
    public virtual void Persist(System.Action del)
    {
      del();
      bool? updateRelatedInfo = this.UpdateRelatedInfo;
      if (!updateRelatedInfo.HasValue || !updateRelatedInfo.GetValueOrDefault())
        return;
      PXSelectorAttribute.ClearGlobalCache<Contact>();
      ((PXCache) GraphHelper.Caches<Contact>((PXGraph) this.Base)).Clear();
    }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<LeadMaint>>.FilledWith<LeadMaint.DefaultLeadOwnerGraphExt, LeadMaint_LinkContactExt, LeadMaint.UpdateRelatedContactInfoFromLeadGraphExt, LeadMaint.CreateContactFromLeadGraphExt, LeadMaint.CreateAccountFromLeadGraphExt, LeadMaint.CreateOpportunityFromLeadGraphExt, LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, LeadMaint.CreateOpportunityAllFromLeadGraphExt, LeadMaint_LinkAccountExt>>
  {
  }

  /// <exclude />
  public class LeadMaintAddressLookupExtension : AddressLookupExtension<LeadMaint, CRLead, Address>
  {
    protected override string AddressView => "AddressCurrent";
  }
}
