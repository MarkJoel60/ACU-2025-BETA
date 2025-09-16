// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.MarketingListDetailsExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CR.CRMarketingListMaint_Extensions;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public abstract class MarketingListDetailsExt<TGraph, TMaster, TContactField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
  where TContactField : class, IBqlField
{
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelectReadonly<CRMarketingList, Where<CRMarketingList.marketingListID, Equal<Required<CRMarketingListMember.marketingListID>>>> CRMarketingListView;
  [PXViewName("Subscriptions")]
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<CRMarketingListMember, InnerJoin<CRMarketingList, On<True, Equal<False>>, InnerJoin<Contact, On<True, Equal<False>>>>, Where<CRMarketingListMember.contactID, Equal<Current<TContactField>>>, OrderBy<Desc<CRMarketingListMember.isSubscribed, Asc<CRMarketingList.mailListCode>>>> Subscriptions;
  private ILogger _logger;
  public PXAction<TMaster> SubscribeAll;
  public PXAction<TMaster> UnsubscribeAll;
  public PXAction<TMaster> ViewMarketingList;
  public PXAction<TMaster> RefreshMarketingListMembers;

  [InjectDependency]
  public ICRMarketingListMemberRepository MemberRepository { get; set; }

  protected virtual IEnumerable subscriptions()
  {
    PXResultset<CRMarketingListMember, CRMarketingList, Contact> subscriptions = new PXResultset<CRMarketingListMember, CRMarketingList, Contact>();
    PXCache cache = this.Base.Views[this.Base.PrimaryView]?.Cache;
    if (cache != null && cache.Current != null)
    {
      bool isDirty = this.Base.Caches[typeof (CRMarketingListMember)].IsDirty;
      EnumerableExtensions.ForEach<PXResult<CRMarketingListMember>>((IEnumerable<PXResult<CRMarketingListMember>>) this.GetFromStatic(), (Action<PXResult<CRMarketingListMember>>) (_ => ((PXResultset<CRMarketingListMember>) subscriptions).Add(_)));
      if (cache.GetStatus(cache.Current) != 2)
        EnumerableExtensions.ForEach<PXResult<CRMarketingListMember>>((IEnumerable<PXResult<CRMarketingListMember>>) this.GetFromDynamic(), (Action<PXResult<CRMarketingListMember>>) (_ => ((PXResultset<CRMarketingListMember>) subscriptions).Add(_)));
      this.Base.Caches[typeof (CRMarketingListMember)].IsDirty = isDirty;
    }
    return (IEnumerable) subscriptions;
  }

  protected virtual PXResultset<CRMarketingListMember, CRMarketingList> GetFromStatic()
  {
    PXResultset<CRMarketingListMember, CRMarketingList, Contact> fromStatic = new PXResultset<CRMarketingListMember, CRMarketingList, Contact>();
    foreach (PXResult<CRMarketingList> pxResult in PXSelectBase<CRMarketingList, PXSelectJoin<CRMarketingList, LeftJoin<CRMarketingListMember, On<CRMarketingListMember.marketingListID, Equal<CRMarketingList.marketingListID>, And<CRMarketingListMember.contactID, Equal<Current<TContactField>>>>, LeftJoin<Contact, On<Contact.contactID, Equal<CRMarketingListMember.contactID>>>>, Where<CRMarketingList.type, Equal<CRMarketingList.type.@static>, And<CRMarketingList.status, Equal<CRMarketingList.status.active>>>, OrderBy<Desc<CRMarketingListMember.isSubscribed>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      CRMarketingList list = ((PXResult) pxResult).GetItem<CRMarketingList>();
      CRMarketingListMember member = ((PXResult) pxResult).GetItem<CRMarketingListMember>();
      Contact contact = ((PXResult) pxResult).GetItem<Contact>();
      CRMarketingListMember cachedMember = this.GetCachedMember(member, list);
      ((PXResultset<CRMarketingListMember>) fromStatic).Add((PXResult<CRMarketingListMember>) new PXResult<CRMarketingListMember, CRMarketingList, Contact>(cachedMember, list, contact));
    }
    return (PXResultset<CRMarketingListMember, CRMarketingList>) fromStatic;
  }

  protected virtual PXResultset<CRMarketingListMember, CRMarketingList> GetFromDynamic()
  {
    PXResultset<CRMarketingListMember, CRMarketingList, Contact> fromDynamic = new PXResultset<CRMarketingListMember, CRMarketingList, Contact>();
    List<CRMarketingList> lists = new List<CRMarketingList>();
    EnumerableExtensions.ForEach<PXResult<CRMarketingList>>((IEnumerable<PXResult<CRMarketingList>>) PXSelectBase<CRMarketingList, PXSelect<CRMarketingList, Where<CRMarketingList.type, Equal<CRMarketingList.type.dynamic>, And<CRMarketingList.status, Equal<CRMarketingList.status.active>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()), (Action<PXResult<CRMarketingList>>) (_ => lists.Add(PXResult<CRMarketingList>.op_Implicit(_))));
    foreach (CRMarketingList crMarketingList in lists)
    {
      PXResult<CRMarketingListMember, Contact, Address> pxResult = this.MemberRepository.GetMembers(crMarketingList, new ICRMarketingListMemberRepository.Options()
      {
        ExternalFilters = this.PrepareFilter().SingleToArray<PXFilterRow>()
      }).FirstOrDefault<PXResult<CRMarketingListMember, Contact, Address>>();
      if (pxResult != null)
      {
        CRMarketingListMember member = ((PXResult) pxResult).GetItem<CRMarketingListMember>();
        Contact contact = ((PXResult) pxResult).GetItem<Contact>();
        CRMarketingListMember cachedMember = this.GetCachedMember(member, crMarketingList);
        ((PXResultset<CRMarketingListMember>) fromDynamic).Add((PXResult<CRMarketingListMember>) new PXResult<CRMarketingListMember, CRMarketingList, Contact>(cachedMember, crMarketingList, contact));
      }
    }
    return (PXResultset<CRMarketingListMember, CRMarketingList>) fromDynamic;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXUIFieldAttribute.SetVisible<CRMarketingListMember.format>(((PXSelectBase) this.Subscriptions).Cache, (object) null, false);
    ((PXCache) GraphHelper.Caches<CRMarketingList>((PXGraph) this.Base)).Fields.Add("IsDynamic");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(typeof (CRMarketingList), "IsDynamic", MarketingListDetailsExt<TGraph, TMaster, TContactField>.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (MarketingListDetailsExt<TGraph, TMaster, TContactField>.\u003C\u003Ec.\u003C\u003E9__9_0 = new PXFieldSelecting((object) MarketingListDetailsExt<TGraph, TMaster, TContactField>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitialize\u003Eb__9_0))));
  }

  public virtual ICRMarketingListMemberRepository.Options PersistMembersSelectOptions { get; } = new ICRMarketingListMemberRepository.Options()
  {
    ChunkSize = 1000000000
  };

  [InjectDependency]
  public ILogger Logger
  {
    get => this._logger;
    set => this._logger = value?.ForContext<CRMarketingListMaint>();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable subscribeAll(PXAdapter adapter)
  {
    this.Base.Actions.PressSave();
    this.ChangeMemberSubscription(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable unsubscribeAll(PXAdapter adapter)
  {
    this.Base.Actions.PressSave();
    this.ChangeMemberSubscription(false);
    return adapter.Get();
  }

  [PXUIField(Visible = false)]
  [PXButton(DisplayOnMainToolbar = false, PopupCommand = "RefreshMarketingListMembers")]
  public virtual IEnumerable viewMarketingList(PXAdapter adapter)
  {
    CRMarketingListMember current = ((PXSelectBase<CRMarketingListMember>) this.Subscriptions).Current;
    if (current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(this.Base.Caches[typeof (CRMarketingList)], (object) CRMarketingList.PK.Find((PXGraph) this.Base, current.MarketingListID), "", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField(Visible = false)]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable refreshMarketingListMembers(PXAdapter adapter)
  {
    this.Base.SelectTimeStamp();
    ((PXSelectBase) this.Subscriptions).Cache.ClearQueryCache();
    ((PXSelectBase) this.Subscriptions).Cache.Clear();
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Member Since", Enabled = false)]
  protected virtual void _(
    Events.CacheAttached<CRCampaignMembers.createdDateTime> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<CRMarketingList.marketingListID, Where<CRMarketingList.type, Equal<CRMarketingList.type.@static>, And<CRMarketingList.status, Equal<CRMarketingList.status.active>>>>), DescriptionField = typeof (CRMarketingList.mailListCode))]
  protected virtual void _(
    Events.CacheAttached<CRMarketingListMember.marketingListID> e)
  {
  }

  protected virtual void _(Events.RowDeleting<CRMarketingListMember> e)
  {
    if (e.Row == null)
      return;
    if (!(((PXSelectBase<CRMarketingList>) this.CRMarketingListView).SelectSingle(new object[1]
    {
      (object) e.Row.MarketingListID
    })?.Type == "D"))
      return;
    e.Cancel = true;
  }

  protected virtual void _(Events.RowPersisting<CRMarketingListMember> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 1 && !this.Base.IsContractBasedAPI)
      return;
    switch (e?.Row?.Type)
    {
      case "D":
        bool? isSubscribed1 = e.Row.IsSubscribed;
        if (isSubscribed1.HasValue && !isSubscribed1.GetValueOrDefault())
        {
          this.InsertMarketingListMember(e.Row);
        }
        else
        {
          isSubscribed1 = e.Row.IsSubscribed;
          if (isSubscribed1.HasValue && isSubscribed1.GetValueOrDefault())
            this.MemberRepository.DeleteMember(e.Row);
        }
        e.Cancel = true;
        break;
      case "S":
        bool? isSubscribed2 = e.Row.IsSubscribed;
        if (isSubscribed2.HasValue && isSubscribed2.GetValueOrDefault())
        {
          ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRMarketingListMember>>) e).Cache.SetDefaultExt<CRMarketingListMember.contactID>((object) e.Row);
          this.InsertMarketingListMember(e.Row);
          ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRMarketingListMember>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 0);
          e.Cancel = true;
        }
        else
        {
          isSubscribed2 = e.Row.IsSubscribed;
          if (isSubscribed2.HasValue && !isSubscribed2.GetValueOrDefault())
            this.MemberRepository.DeleteMember(e.Row);
        }
        e.Cancel = true;
        break;
    }
  }

  public virtual void ChangeMemberSubscription(bool subscribe)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) new MarketingListDetailsExt<TGraph, TMaster, TContactField>.\u003C\u003Ec__DisplayClass29_0()
    {
      subscribe = subscribe,
      filters = ((PXSelectBase) this.Subscriptions).View.GetExternalFilters(),
      graph = this.Base.CloneGraphState<TGraph>()
    }, __methodptr(\u003CChangeMemberSubscription\u003Eb__0)));
  }

  public virtual void ChangeMemberSubscription(bool subscribe, PXFilterRow[] filters)
  {
    int num1 = 0;
    int num2 = 0;
    foreach (PXResult pxResult in ((PXSelectBase) this.Subscriptions).View.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters, ref num1, 0, ref num2))
    {
      pxResult.GetItem<CRMarketingList>();
      CRMarketingListMember member = pxResult.GetItem<CRMarketingListMember>();
      member.IsSubscribed = new bool?(subscribe);
      this.MemberRepository.UpdateMember(member);
    }
  }

  public virtual PXFilterRow PrepareFilter()
  {
    return new PXFilterRow()
    {
      OrOperator = false,
      OpenBrackets = 1,
      DataField = "Contact__ContactID",
      Condition = (PXCondition) 0,
      Value = ((PXCache) GraphHelper.Caches<TMaster>((PXGraph) this.Base)).GetValue<TContactField>(((PXCache) GraphHelper.Caches<TMaster>((PXGraph) this.Base)).Current),
      CloseBrackets = 1
    };
  }

  protected virtual CRMarketingListMember GetCachedMember(
    CRMarketingListMember member,
    CRMarketingList list)
  {
    int? nullable1;
    int num;
    if (member == null)
    {
      num = 1;
    }
    else
    {
      nullable1 = member.ContactID;
      num = !nullable1.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      member = GraphHelper.InitNewRow<CRMarketingListMember>(this.Base.Caches[typeof (CRMarketingListMember)], member);
      CRMarketingListMember marketingListMember = member;
      int? nullable2;
      if (list == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = list.MarketingListID;
      marketingListMember.MarketingListID = nullable2;
      member.IsSubscribed = new bool?(false);
    }
    if (!(this.Base.Caches[typeof (CRMarketingListMember)].Locate((object) member) is CRMarketingListMember cachedMember))
    {
      GraphHelper.Hold(this.Base.Caches[typeof (CRMarketingListMember)], (object) member);
      cachedMember = member;
    }
    return cachedMember;
  }

  private void InsertMarketingListMember(CRMarketingListMember row)
  {
    try
    {
      this.MemberRepository.InsertMember(row);
    }
    catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
    {
      this.Logger.Verbose<int?, int?>((Exception) ex, "Marketing member {ContactID} for list {MarketingListID} already exists", row.ContactID, row.MarketingListID);
    }
  }
}
