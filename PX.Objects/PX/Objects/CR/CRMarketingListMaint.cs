// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance.GI;
using PX.Objects.Common;
using PX.Objects.CR.CRMarketingListMaint_Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class CRMarketingListMaint : PXGraph<
#nullable disable
CRMarketingListMaint, CRMarketingList>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<Address> Addresses;
  [PXViewName("Marketing List Info")]
  public PXSelect<CRMarketingList> MailLists;
  [PXHidden]
  public PXSelect<CRMarketingList, Where<CRMarketingList.marketingListID, Equal<Current<CRMarketingList.marketingListID>>>> MailListsCurrent;
  [PXViewName("Mailing List Subscribers")]
  [PXImportSubstitute(typeof (CRMarketingList), typeof (CRMarketingMemberForImport))]
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  [PXDependToCache(new System.Type[] {typeof (CRMarketingList)})]
  public FbqlSelect<SelectFromBase<CRMarketingListMember, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRMarketingListMember.contactID>>>>.Order<By<BqlField<
  #nullable enable
  CRMarketingListMember.createdDateTime, IBqlDateTime>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  CRMarketingListMember.isSubscribed, IBqlBool>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  Contact.displayName, IBqlString>.Asc>>, 
  #nullable disable
  CRMarketingListMember>.View ListMembers;
  [PXCopyPasteHiddenView]
  [PXViewName("Campaign Members")]
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<CRCampaignToCRMarketingListLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRCampaign>.On<BqlOperand<
  #nullable enable
  CRCampaign.campaignID, IBqlString>.IsEqual<
  #nullable disable
  CRCampaignToCRMarketingListLink.campaignID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRCampaignToCRMarketingListLink.marketingListID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CRCampaignToCRMarketingListLink.selectedForCampaign, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, CRCampaignToCRMarketingListLink>.View MarketingCampaigns;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXFilter<CopyMembersFilter> CopyMembersFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<AddMembersToNewListFilter> AddMembersToNewListFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRMarketingListAlias, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CRMarketingListAlias.type, 
  #nullable disable
  Equal<CRMarketingList.type.@static>>>>>.And<BqlOperand<
  #nullable enable
  CRMarketingListAlias.marketingListID, IBqlInt>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  CRMarketingListAlias>.View AddMembersToExistingListsFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<AddMembersFromGIFilter> AddMembersFromGIFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRMarketingListAlias, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRMarketingListAlias.marketingListID, IBqlInt>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRMarketingList.marketingListID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CRMarketingListAlias>.View AddMembersFromMarketingListsFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRCampaign, TypeArrayOf<IFbqlJoin>.Empty>, CRCampaign>.View AddMembersFromCampaignsFilterView;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> AddMembersToNewListFilterUdfView;
  private ILogger _logger;
  public PXAction<CRMarketingList> RedirectToGIResult;
  public PXAction<AddMembersFromGIFilter> RedirectToGIInquiry;
  public PXAction<CRMarketingList> AddMembers;
  public PXAction<CRMarketingList> ManageSubscription;
  public PXAction<CRMarketingList> ConvertToDynamicList;
  public PXAction<CRMarketingList> ConvertToStaticList;
  public PXAction<CRMarketingList> CopyMembers;
  public PXAction<CRMarketingList> ClearMembers;
  public PXAction<CRMarketingList> DeleteSelectedMembers;
  public PXAction<CRMarketingList> SubscribeAll;
  public PXAction<CRMarketingList> UnsubscribeAll;
  public PXAction<CRMarketingList> AddMembersFromGI;
  public PXAction<CRMarketingList> AddMembersFromMarketingLists;
  public PXAction<CRMarketingList> AddMembersFromCampaigns;

  protected virtual IEnumerable listMembers()
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current != null)
    {
      ICRMarketingListMemberRepository memberRepository = this.MemberRepository;
      CRMarketingList current = ((PXSelectBase<CRMarketingList>) this.MailLists).Current;
      foreach (PXResult<CRMarketingListMember, Contact, Address> member in memberRepository.GetMembers(current, new ICRMarketingListMemberRepository.Options()
      {
        WithViewContext = true
      }))
      {
        CRMarketingListMember marketingListMember = ((PXResult) member).GetItem<CRMarketingListMember>();
        if (((PXSelectBase) this.ListMembers).Cache.Locate((object) marketingListMember) == null)
          GraphHelper.Hold(((PXSelectBase) this.ListMembers).Cache, (object) marketingListMember);
        yield return (object) member;
      }
    }
  }

  protected virtual IEnumerable<PopupUDFAttributes> addMembersToNewListFilterUdfView()
  {
    return UDFHelper.GetRequiredUDFFields(((PXSelectBase) this.MailLists).Cache, (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current, ((object) this).GetType());
  }

  public CRMarketingListMaint()
  {
    UDFHelper.AddRequiredUDFFieldsEvents((PXGraph) this);
    PXCache<Address2> pxCache = GraphHelper.Caches<Address2>((PXGraph) this);
    PXUIFieldAttribute.SetDisplayName<Address2.addressLine1>((PXCache) pxCache, "Business Account Address Line 1");
    PXUIFieldAttribute.SetDisplayName<Address2.addressLine2>((PXCache) pxCache, "Business Account Address Line 2");
    PXUIFieldAttribute.SetDisplayName<Address2.addressLine3>((PXCache) pxCache, "Business Account Address Line 3");
    PXUIFieldAttribute.SetDisplayName<Address2.city>((PXCache) pxCache, "Business Account City");
    PXUIFieldAttribute.SetDisplayName<Address2.state>((PXCache) pxCache, "Business Account State");
    PXUIFieldAttribute.SetDisplayName<Address2.postalCode>((PXCache) pxCache, "Business Account Postal Code");
    PXUIFieldAttribute.SetDisplayName<Address2.countryID>((PXCache) pxCache, "Business Account Country");
    PXUIFieldAttribute.SetDisplayName<Address.department>((PXCache) pxCache, "Business Account Department");
    PXUIFieldAttribute.SetDisplayName<Address.subDepartment>((PXCache) pxCache, "Business Account Subdepartment");
    PXUIFieldAttribute.SetDisplayName<Address.streetName>((PXCache) pxCache, "Business Account Street Name");
    PXUIFieldAttribute.SetDisplayName<Address.buildingNumber>((PXCache) pxCache, "Business Account Building Number");
    PXUIFieldAttribute.SetDisplayName<Address.buildingName>((PXCache) pxCache, "Business Account Building Name");
    PXUIFieldAttribute.SetDisplayName<Address.floor>((PXCache) pxCache, "Business Account Floor");
    PXUIFieldAttribute.SetDisplayName<Address.unitNumber>((PXCache) pxCache, "Business Account Unit Number");
    PXUIFieldAttribute.SetDisplayName<Address.postBox>((PXCache) pxCache, "Business Account Post Box");
    PXUIFieldAttribute.SetDisplayName<Address.room>((PXCache) pxCache, "Business Account Room");
    PXUIFieldAttribute.SetDisplayName<Address.townLocationName>((PXCache) pxCache, "Business Account Town Location Name");
    PXUIFieldAttribute.SetDisplayName<Address.districtName>((PXCache) pxCache, "Business Account District Name");
    ((PXAction) this.ManageSubscription).AddMenuAction((PXAction) this.SubscribeAll);
    ((PXAction) this.ManageSubscription).AddMenuAction((PXAction) this.UnsubscribeAll, nameof (SubscribeAll), true);
    ((PXAction) this.AddMembers).AddMenuAction((PXAction) this.AddMembersFromGI);
    ((PXAction) this.AddMembers).AddMenuAction((PXAction) this.AddMembersFromMarketingLists, nameof (AddMembersFromGI), true);
    ((PXAction) this.AddMembers).AddMenuAction((PXAction) this.AddMembersFromCampaigns, nameof (AddMembersFromMarketingLists), true);
  }

  public virtual ICRMarketingListMemberRepository.Options PersistMembersSelectOptions { get; } = new ICRMarketingListMemberRepository.Options()
  {
    ChunkSize = 1000000000
  };

  [InjectDependency]
  public ICRMarketingListMemberRepository MemberRepository { get; private set; }

  [InjectDependency]
  public ILogger Logger
  {
    get => this._logger;
    set => this._logger = value?.ForContext<CRMarketingListMaint>();
  }

  [PXUIField(DisplayName = "Open GI Result")]
  [PXButton(ImageKey = "WebN", ImageSet = "control", DisplayOnMainToolbar = false)]
  public virtual IEnumerable redirectToGIResult(PXAdapter adapter)
  {
    this.RedirectToGI(((PXSelectBase<CRMarketingList>) this.MailLists).Current.GIDesignID);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Open GI Result")]
  [PXButton(ImageKey = "WebN", ImageSet = "control", DisplayOnMainToolbar = false)]
  public virtual IEnumerable redirectToGIInquiry(PXAdapter adapter)
  {
    Guid? giDesignId = (Guid?) ((PXSelectBase<AddMembersFromGIFilter>) this.AddMembersFromGIFilterView).Current?.GIDesignID;
    if (giDesignId.HasValue)
      this.RedirectToGI(giDesignId);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(MenuAutoOpen = true)]
  protected virtual IEnumerable addMembers(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(MenuAutoOpen = true)]
  protected virtual IEnumerable manageSubscription(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, Category = "List Management")]
  protected virtual IEnumerable convertToDynamicList(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The marketing list {0} is already dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (!((PXSelectBase<CRMarketingListMember>) this.ListMembers).Any<CRMarketingListMember>())
    {
      ((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type = "D";
      ((PXSelectBase<CRMarketingList>) this.MailLists).UpdateCurrent();
    }
    else if (WebDialogResultExtension.IsPositive(((PXSelectBase) this.MailLists).View.Ask((object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current, "Confirmation", "All members will be deleted from this list after conversion to a dynamic list.", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      [(WebDialogResult) 6] = "Delete",
      [(WebDialogResult) 7] = "Cancel"
    }, (MessageIcon) 0)))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass36_0 cDisplayClass360 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass36_0();
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass360.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass360, __methodptr(\u003CconvertToDynamicList\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, Category = "List Management")]
  protected virtual IEnumerable convertToStaticList(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "S")
      throw new PXInvalidOperationException("The marketing list {0} is already static.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (!((PXSelectBase<CRMarketingListMember>) this.ListMembers).Any<CRMarketingListMember>())
    {
      ((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type = "S";
      ((PXSelectBase<CRMarketingList>) this.MailLists).UpdateCurrent();
    }
    else if (EnumerableExtensions.IsIn<WebDialogResult>(((PXSelectBase) this.MailLists).View.Ask((object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current, "Confirmation", "Do you want to keep the members in this list after conversion to a static list?", (MessageButtons) 3, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
    {
      [(WebDialogResult) 6] = "Keep",
      [(WebDialogResult) 7] = "Delete",
      [(WebDialogResult) 2] = "Cancel"
    }, (MessageIcon) 0), (WebDialogResult) 6, (WebDialogResult) 7))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass38_0 cDisplayClass380 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass38_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380.keepMembers = ((PXSelectBase) this.MailLists).View.Answer == 6;
      ((PXGraph) this).Actions.PressSave();
      ((PXGraph) this).Actions.PressCancel();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass380, __methodptr(\u003CconvertToStaticList\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Copy all members of the selected marketing list")]
  protected virtual IEnumerable copyMembers(PXAdapter adapter)
  {
    if (this.CopyMembersFilterView.AskExtFullyValid((DialogAnswerType) 1, true))
    {
      int? addMembersOption = ((PXSelectBase<CopyMembersFilter>) this.CopyMembersFilterView).Current.AddMembersOption;
      if (addMembersOption.HasValue)
      {
        switch (addMembersOption.GetValueOrDefault())
        {
          case 0:
            this.CreateNewListAndCopyMembers();
            break;
          case 1:
            this.CopyMembersToExistingLists();
            break;
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Remove members currently shown on all pages from the list")]
  protected virtual IEnumerable clearMembers(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The members cannot be removed from the marketing list {0} because it is dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (((PXSelectBase) this.MailListsCurrent).View.Ask((string) null, "All currently displayed members will be removed from the list. Do you want to proceed?", (MessageButtons) 1) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass42_0 cDisplayClass420 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass42_0();
      if (((PXGraph) this).IsDirty)
        ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass420.filters = ((PXSelectBase) this.ListMembers).View.GetExternalFilters();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass420.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass420, __methodptr(\u003CclearMembers\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "RecordDel", ImageSet = "main")]
  protected virtual IEnumerable deleteSelectedMembers(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The members cannot be removed from the marketing list {0} because it is dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    List<CRMarketingListMember> list = ((PXSelectBase) this.ListMembers).Cache.Updated.OfType<CRMarketingListMember>().Union<CRMarketingListMember>(((PXSelectBase) this.ListMembers).Cache.Inserted.OfType<CRMarketingListMember>()).Where<CRMarketingListMember>((Func<CRMarketingListMember, bool>) (member => member.Selected ?? false)).ToList<CRMarketingListMember>();
    if (!list.Any<CRMarketingListMember>())
    {
      CRMarketingListMember current = ((PXSelectBase<CRMarketingListMember>) this.ListMembers).Current;
      if (current != null)
        list.Add(current);
    }
    list.ForEach((Action<CRMarketingListMember>) (member => ((PXSelectBase) this.ListMembers).Cache.Delete((object) member)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable subscribeAll(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRMarketingListMaint.\u003C\u003Ec__DisplayClass46_0 cDisplayClass460 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass46_0();
    ((PXGraph) this).Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass460.filters = ((PXSelectBase) this.ListMembers).View.GetExternalFilters();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass460.graph = this.CloneGraphState<CRMarketingListMaint>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass460, __methodptr(\u003CsubscribeAll\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable unsubscribeAll(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRMarketingListMaint.\u003C\u003Ec__DisplayClass48_0 cDisplayClass480 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass48_0();
    ((PXGraph) this).Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass480.filters = ((PXSelectBase) this.ListMembers).View.GetExternalFilters();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass480.graph = this.CloneGraphState<CRMarketingListMaint>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass480, __methodptr(\u003CunsubscribeAll\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable addMembersFromGI(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The members cannot be added to the marketing list {0} because it is dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (this.AddMembersFromGIFilterView.AskExtFullyValid((DialogAnswerType) 1, true))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass50_0 cDisplayClass500 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass50_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass500.filter = ((PXSelectBase<AddMembersFromGIFilter>) this.AddMembersFromGIFilterView).Current;
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass500.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass500, __methodptr(\u003CaddMembersFromGI\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable addMembersFromMarketingLists(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The members cannot be added to the marketing list {0} because it is dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<CRMarketingListAlias>) this.AddMembersFromMarketingListsFilterView).AskExt(this.AskExtResetCache((PXSelectBase) this.AddMembersFromMarketingListsFilterView), true)))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass52_0 cDisplayClass520 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass52_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass520.selectedItems = ((IEnumerable<CRMarketingListAlias>) ((PXSelectBase<CRMarketingListAlias>) this.AddMembersFromMarketingListsFilterView).SelectMain(Array.Empty<object>())).Where<CRMarketingListAlias>((Func<CRMarketingListAlias, bool>) (i => i.Selected ?? false)).DefaultIfEmpty<CRMarketingListAlias>(((PXSelectBase<CRMarketingListAlias>) this.AddMembersFromMarketingListsFilterView).Current).Where<CRMarketingListAlias>((Func<CRMarketingListAlias, bool>) (i => i != null)).ToList<CRMarketingListAlias>();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass520.selectedItems.Count == 0)
        return adapter.Get();
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass520.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass520, __methodptr(\u003CaddMembersFromMarketingLists\u003Eb__2)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable addMembersFromCampaigns(PXAdapter adapter)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "D")
      throw new PXInvalidOperationException("The members cannot be added to the marketing list {0} because it is dynamic.", new object[1]
      {
        (object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID
      });
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<CRCampaign>) this.AddMembersFromCampaignsFilterView).AskExt(this.AskExtResetCache((PXSelectBase) this.AddMembersFromCampaignsFilterView), true)))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRMarketingListMaint.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass54_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.selectedItems = ((IEnumerable<CRCampaign>) ((PXSelectBase<CRCampaign>) this.AddMembersFromCampaignsFilterView).SelectMain(Array.Empty<object>())).Where<CRCampaign>((Func<CRCampaign, bool>) (i => i.Selected ?? false)).Select<CRCampaign, string>((Func<CRCampaign, string>) (i => i.CampaignID)).DefaultIfEmpty<string>(((PXSelectBase<CRCampaign>) this.AddMembersFromCampaignsFilterView).Current?.CampaignID).OfType<object>().ToArray<object>();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass540.selectedItems.Length == 0)
        return adapter.Get();
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.listId = ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass540, __methodptr(\u003CaddMembersFromCampaigns\u003Eb__2)));
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute]
  protected virtual void _(Events.CacheAttached<Contact.fullName> e)
  {
  }

  [CRMBAccount(null, null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<Contact.bAccountID> e)
  {
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Consented to the Processing of Personal Data", FieldClass = "GDPR")]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<Contact.consentAgreement> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Type")]
  protected virtual void _(Events.CacheAttached<PX.Objects.CR.BAccount.type> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(Events.CacheAttached<CRMarketingList.name> e)
  {
  }

  [PXSelector(typeof (PX.Objects.CS.State.stateID), DescriptionField = typeof (PX.Objects.CS.State.name))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<Address.state> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Member Since", Enabled = false)]
  protected virtual void _(
    Events.CacheAttached<CRMarketingListMember.createdDateTime> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (CRCampaign.campaignID), DescriptionField = typeof (CRCampaign.campaignName))]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Campaign")]
  protected virtual void _(
    Events.CacheAttached<CRCampaignToCRMarketingListLink.campaignID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Current<CRMarketingList.marketingListID>))]
  protected virtual void _(
    Events.CacheAttached<CRCampaignToCRMarketingListLink.marketingListID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Workgroup")]
  protected virtual void _(Events.CacheAttached<PX.Objects.CR.BAccount.workgroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Business Account Owner")]
  protected virtual void _(Events.CacheAttached<PX.Objects.CR.BAccount.ownerID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Business Account Parent Account")]
  protected virtual void _(Events.CacheAttached<PX.Objects.CR.BAccount.parentBAccountID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Source Campaign")]
  protected virtual void _(Events.CacheAttached<PX.Objects.CR.BAccount.campaignSourceID> e)
  {
  }

  protected virtual void _(Events.RowSelected<CRMarketingList> e)
  {
    ((PXAction) this.CopyPaste).SetVisible(false);
    if (e.Row == null)
      return;
    bool isDynamic = e.Row.Type == "D";
    bool flag1 = !isDynamic;
    bool flag2 = !string.IsNullOrEmpty(e.Row.MailListCode);
    ((PXAction) this.AddMembersFromGI).SetEnabled(flag1);
    ((PXAction) this.AddMembersFromMarketingLists).SetEnabled(flag1);
    ((PXAction) this.AddMembersFromCampaigns).SetEnabled(flag1);
    ((PXAction) this.ClearMembers).SetEnabled(flag1);
    ((PXAction) this.DeleteSelectedMembers).SetEnabled(flag1);
    ((PXAction) this.ConvertToDynamicList).SetDisplayOnMainToolbar(flag1);
    ((PXAction) this.ConvertToDynamicList).SetEnabled(flag1 & flag2);
    ((PXAction) this.ConvertToStaticList).SetDisplayOnMainToolbar(isDynamic);
    ((PXAction) this.ConvertToStaticList).SetEnabled(isDynamic & flag2);
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingList>>) e).Cache, (object) e.Row).For<CRMarketingList.gIDesignID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = ui.Visible = isDynamic)).SameFor<CRMarketingList.sharedGIFilter>();
    ((PXSelectBase) this.ListMembers).View.AllowInsert = flag1;
    ((PXSelectBase) this.ListMembers).View.AllowDelete = flag1;
    ((PXSelectBase) this.ListMembers).View.AllowUpdate = true;
    PXImportAttribute.SetEnabled((PXGraph) this, "ListMembers", flag1 && ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingList>>) e).Cache.GetOriginal((object) e.Row) != null);
  }

  protected virtual void _(Events.RowSelected<CRMarketingListAlias> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListAlias>>) e).Cache.AllowUpdate = true;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListAlias>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListAlias>>) e).Cache.AllowDelete = false;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUIReadonly(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListAlias>>) e).Cache, (object) e.Row);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    attributeAdjuster.For<CRMarketingListAlias.selected>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
  }

  protected virtual void _(Events.RowSelected<CRCampaign> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCampaign>>) e).Cache.AllowUpdate = true;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCampaign>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCampaign>>) e).Cache.AllowDelete = false;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUIReadonly(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRCampaign>>) e).Cache, (object) e.Row);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    attributeAdjuster.For<CRMarketingListAlias.selected>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
  }

  protected virtual void _(Events.RowSelected<CRMarketingListMember> e)
  {
    bool isStatic = ((PXSelectBase<CRMarketingList>) this.MailLists).Current?.Type == "S";
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUIReadonly(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListMember>>) e).Cache, (object) e.Row);
    attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<CRMarketingListMember.isSubscribed>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = true));
    chained = chained.For<CRMarketingListMember.selected>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = ui.Visible = isStatic));
    chained.For<CRMarketingListMember.contactID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = isStatic));
  }

  protected virtual void _(Events.RowSelected<Contact> e)
  {
    PXCacheEx.AdjustUIReadonly(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Contact>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
  }

  protected virtual void _(
    Events.FieldUpdated<CRMarketingList, CRMarketingList.type> e)
  {
    if (!(e.NewValue is string newValue) || !(newValue == "D"))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRMarketingList, CRMarketingList.type>>) e).Cache.SetValue<CRMarketingList.gIDesignID>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRMarketingList, CRMarketingList.type>>) e).Cache.Current, (object) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRMarketingList, CRMarketingList.type>>) e).Cache.SetValue<CRMarketingList.sharedGIFilter>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CRMarketingList, CRMarketingList.type>>) e).Cache.Current, (object) null);
  }

  protected virtual void _(Events.RowPersisting<CRMarketingListMember> e)
  {
    if (((PXSelectBase<CRMarketingList>) this.MailLists).Current.Type == "S" || PXDBOperationExt.Command(e.Operation) != 1)
      return;
    int? marketingListId = e.Row.MarketingListID;
    int num = 0;
    if (marketingListId.GetValueOrDefault() < num & marketingListId.HasValue)
      e.Row.MarketingListID = ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID;
    bool? isSubscribed = e.Row.IsSubscribed;
    if (isSubscribed.HasValue)
    {
      if (!isSubscribed.GetValueOrDefault())
      {
        try
        {
          this.MemberRepository.InsertMember(e.Row);
          goto label_10;
        }
        catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
        {
          this.Logger.Verbose<int?, int?>((Exception) ex, "Marketing member {ContactID} for list {MarketingListID} already exists", e.Row.ContactID, e.Row.MarketingListID);
          this.MemberRepository.UpdateMember(e.Row);
          goto label_10;
        }
      }
    }
    this.MemberRepository.DeleteMember(e.Row);
label_10:
    e.Cancel = true;
  }

  protected virtual void _(
    Events.FieldUpdated<AddMembersToNewListFilter, AddMembersToNewListFilter.mailListCode> e)
  {
    if (!(e.NewValue is string newValue) || string.IsNullOrWhiteSpace(newValue) || CRMarketingList.UK.Find((PXGraph) this, newValue) == null)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<AddMembersToNewListFilter, AddMembersToNewListFilter.mailListCode>>) e).Cache.RaiseExceptionHandling<AddMembersToNewListFilter.mailListCode>((object) e.Row, e.NewValue, (Exception) new PXSetPropertyException<AddMembersToNewListFilter.mailListCode>("The marketing list with the {0} ID already exists.", new object[1]
    {
      e.NewValue
    }));
  }

  public virtual void RedirectToGI(Guid? giID)
  {
    GIDesign giDesign = GIDesign.PK.Find((PXGraph) this, giID, (PKFindOptions) 0);
    if (giDesign != null)
      throw new PXRedirectToGIRequiredException(giDesign, (PXBaseRedirectException.WindowMode) 2, false);
  }

  public virtual void CreateNewListAndCopyMembers()
  {
    if (!this.AddMembersToNewListFilterView.AskExtFullyValid((DialogAnswerType) 1, true))
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRMarketingListMaint.\u003C\u003Ec__DisplayClass77_0 cDisplayClass770 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass77_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass770.filter = ((PXSelectBase<AddMembersToNewListFilter>) this.AddMembersToNewListFilterView).Current;
    // ISSUE: reference to a compiler-generated field
    if (CRMarketingList.UK.Find((PXGraph) this, cDisplayClass770.filter.MailListCode) != null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.AddMembersToNewListFilterView).Cache.RaiseExceptionHandling<AddMembersToNewListFilter.mailListCode>((object) cDisplayClass770.filter, (object) cDisplayClass770.filter.MailListCode, (Exception) new PXSetPropertyException<AddMembersToNewListFilter.mailListCode>("The marketing list with the {0} ID already exists.", new object[1]
      {
        (object) cDisplayClass770.filter.MailListCode
      }));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.redirect = ((PXSelectBase) this.AddMembersToNewListFilterView).View.Answer == 6;
      ((PXGraph) this).Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.graph = this.CloneGraphState<CRMarketingListMaint>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass770, __methodptr(\u003CCreateNewListAndCopyMembers\u003Eb__0)));
    }
  }

  public virtual void CopyMembersToExistingLists()
  {
    if (!WebDialogResultExtension.IsPositive(((PXSelectBase<CRMarketingListAlias>) this.AddMembersToExistingListsFilterView).AskExt(this.AskExtResetCache((PXSelectBase) this.AddMembersToExistingListsFilterView), true)))
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRMarketingListMaint.\u003C\u003Ec__DisplayClass78_0 cDisplayClass780 = new CRMarketingListMaint.\u003C\u003Ec__DisplayClass78_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass780.selectedItems = ((IEnumerable<CRMarketingListAlias>) ((PXSelectBase<CRMarketingListAlias>) this.AddMembersToExistingListsFilterView).SelectMain(Array.Empty<object>())).Where<CRMarketingListAlias>((Func<CRMarketingListAlias, bool>) (i => i.Selected ?? false)).DefaultIfEmpty<CRMarketingListAlias>(((PXSelectBase<CRMarketingListAlias>) this.AddMembersToExistingListsFilterView).Current).Where<CRMarketingListAlias>((Func<CRMarketingListAlias, bool>) (i => i != null && i.MarketingListID.HasValue)).Select<CRMarketingListAlias, int>((Func<CRMarketingListAlias, int>) (i => i.MarketingListID.Value)).ToList<int>();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass780.selectedItems.Count == 0)
      return;
    ((PXGraph) this).Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass780.graph = this.CloneGraphState<CRMarketingListMaint>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass780, __methodptr(\u003CCopyMembersToExistingLists\u003Eb__3)));
  }

  public virtual void CopyPersistMembersToLists(
    IEnumerable<int> marketingListIds,
    IEnumerable<PXResult<CRMarketingListMember>> members)
  {
    if (!(marketingListIds is int[] numArray1))
      numArray1 = marketingListIds.ToArray<int>();
    int[] numArray2 = numArray1;
    HashSet<int?> nullableSet = new HashSet<int?>();
    foreach (PXResult<CRMarketingListMember> member1 in members)
    {
      CRMarketingListMember member2 = PXResult<CRMarketingListMember>.op_Implicit(member1);
      if (nullableSet.Add(member2.ContactID))
      {
        int? marketingListId = member2.MarketingListID;
        foreach (int num in numArray2)
        {
          member2.MarketingListID = new int?(num);
          member2.IsSubscribed = new bool?(true);
          try
          {
            this.MemberRepository.InsertMember(member2);
          }
          catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
          {
            this.Logger.Verbose<int?, int?>((Exception) ex, "Marketing member {ContactID} for list {MarketingListID} already exists", member2.ContactID, member2.MarketingListID);
          }
        }
        member2.MarketingListID = marketingListId;
      }
    }
  }

  public virtual void CopyPersistMembersToList(
    CRMarketingList marketingList,
    IEnumerable<PXResult<CRMarketingListMember>> members)
  {
    this.CopyPersistMembersToLists((IEnumerable<int>) new int[1]
    {
      marketingList.MarketingListID.Value
    }, members);
  }

  public virtual void CopyPersistMembersToLists(IEnumerable<int> marketingListIds)
  {
    this.CopyPersistMembersToLists(marketingListIds, (IEnumerable<PXResult<CRMarketingListMember>>) this.MemberRepository.GetMembers(((PXSelectBase<CRMarketingList>) this.MailLists).Current, this.PersistMembersSelectOptions));
  }

  public virtual void CopyPersistMembersToList(CRMarketingList marketingList)
  {
    this.CopyPersistMembersToLists((IEnumerable<int>) new int[1]
    {
      marketingList.MarketingListID.Value
    });
  }

  public virtual void DeletePersistMembers()
  {
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
      ((PXGraph) this).ProviderDelete<CRMarketingListMember>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<CRMarketingListMember.marketingListID>((object) ((PXSelectBase<CRMarketingList>) this.MailLists).Current.MarketingListID)
      });
  }

  public virtual void DeletePersistFilteredMembers(PXFilterRow[] listMembersFilters)
  {
    ICRMarketingListMemberRepository memberRepository = this.MemberRepository;
    CRMarketingList current = ((PXSelectBase<CRMarketingList>) this.MailLists).Current;
    foreach (PXResult<CRMarketingListMember, Contact, Address> member1 in memberRepository.GetMembers(current, new ICRMarketingListMemberRepository.Options()
    {
      ChunkSize = this.PersistMembersSelectOptions.ChunkSize,
      ExternalFilters = listMembersFilters
    }))
    {
      CRMarketingListMember member2 = PXResult<CRMarketingListMember, Contact, Address>.op_Implicit(member1);
      if (!member2.IsVirtual.GetValueOrDefault())
        this.MemberRepository.DeleteMember(member2);
    }
  }

  public virtual void ChangeFilteredMembersSubscription(
    bool subscribe,
    PXFilterRow[] listMembersFilters)
  {
    ICRMarketingListMemberRepository memberRepository = this.MemberRepository;
    CRMarketingList current = ((PXSelectBase<CRMarketingList>) this.MailLists).Current;
    foreach (PXResult<CRMarketingListMember, Contact, Address> member1 in memberRepository.GetMembers(current, new ICRMarketingListMemberRepository.Options()
    {
      ChunkSize = this.PersistMembersSelectOptions.ChunkSize,
      ExternalFilters = listMembersFilters
    }))
    {
      CRMarketingListMember member2 = PXResult<CRMarketingListMember, Contact, Address>.op_Implicit(member1);
      member2.IsSubscribed = new bool?(subscribe);
      this.MemberRepository.UpdateMember(member2);
    }
  }

  public virtual PXView.InitializePanel AskExtResetCache(PXSelectBase select)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return new PXView.InitializePanel((object) new CRMarketingListMaint.\u003C\u003Ec__DisplayClass86_0()
    {
      select = select
    }, __methodptr(\u003CAskExtResetCache\u003Eb__0));
  }
}
