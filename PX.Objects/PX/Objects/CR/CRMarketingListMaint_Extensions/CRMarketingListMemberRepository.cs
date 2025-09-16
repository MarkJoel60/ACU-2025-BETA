// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.CRMarketingListMemberRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance.GI;
using PX.Objects.CR.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

internal class CRMarketingListMemberRepository : ICRMarketingListMemberRepository
{
  private readonly PXGraph _graph;
  private const string TABLE_ALIAS_POSTFIX = "MARKETINGLISTEXT";
  private const string LeadAlias = "CRLeadMARKETINGLISTEXT";
  private const string BAccountAlias = "BAccountMARKETINGLISTEXT";
  private const string AddressAlias = "AddressMARKETINGLISTEXT";
  private const string BAccountAddressAlias = "Address2MARKETINGLISTEXT";
  private const string MarketingMemberAlias = "CRMarketingListMemberMARKETINGLISTEXT";
  private static readonly IReadOnlyDictionary<System.Type, string> _aliasMapping = (IReadOnlyDictionary<System.Type, string>) new Dictionary<System.Type, string>()
  {
    [typeof (CRLead)] = "CRLeadMARKETINGLISTEXT",
    [typeof (BAccount)] = "BAccountMARKETINGLISTEXT",
    [typeof (Address)] = "AddressMARKETINGLISTEXT",
    [typeof (Address2)] = "Address2MARKETINGLISTEXT",
    [typeof (CRMarketingListMember)] = "CRMarketingListMemberMARKETINGLISTEXT"
  };

  public CRMarketingListMemberRepository(PXGraph graph)
  {
    this._graph = graph ?? PXGraph.CreateInstance<PXGraph>();
  }

  public virtual void InsertMember(CRMarketingListMember member)
  {
    DateTime utcNow = DateTime.UtcNow;
    Guid userId = this._graph.Accessinfo.UserID;
    string normalizedScreenId = this._graph.Accessinfo.GetNormalizedScreenID();
    this._graph.ProviderInsert<CRMarketingListMember>(new PXDataFieldAssign[10]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.marketingListID>((object) member.MarketingListID),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.contactID>((object) member.ContactID),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.format>((object) member.Format),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.isSubscribed>((object) member.IsSubscribed),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.createdByID>((object) userId),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.lastModifiedByID>((object) userId),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.createdByScreenID>((object) normalizedScreenId),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.lastModifiedByScreenID>((object) normalizedScreenId),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.createdDateTime>((object) utcNow),
      (PXDataFieldAssign) new PXDataFieldAssign<CRMarketingListMember.lastModifiedDateTime>((object) utcNow)
    });
  }

  public virtual void UpdateMember(CRMarketingListMember member)
  {
    if (member?.Type == "D")
    {
      bool? isSubscribed = member.IsSubscribed;
      if (isSubscribed.HasValue && isSubscribed.GetValueOrDefault())
      {
        this.DeleteMember(member);
        return;
      }
      isSubscribed = member.IsSubscribed;
      if (isSubscribed.HasValue && !isSubscribed.GetValueOrDefault())
      {
        if (member.LastModifiedByScreenID == null)
        {
          try
          {
            this.InsertMember(member);
            return;
          }
          catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
          {
          }
        }
      }
    }
    else if (member?.LastModifiedByScreenID == null)
    {
      try
      {
        this.InsertMember(member);
        return;
      }
      catch (PXDatabaseException ex) when (ex.ErrorCode == 4)
      {
      }
    }
    this._graph.ProviderUpdate<CRMarketingListMember>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldRestrict<CRMarketingListMember.marketingListID>((object) member.MarketingListID),
      (PXDataFieldParam) new PXDataFieldRestrict<CRMarketingListMember.contactID>((object) member.ContactID),
      (PXDataFieldParam) new PXDataFieldAssign<CRMarketingListMember.isSubscribed>((object) member.IsSubscribed)
    });
  }

  public virtual void DeleteMember(CRMarketingListMember member)
  {
    this._graph.ProviderDelete<CRMarketingListMember>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<CRMarketingListMember.marketingListID>((object) member.MarketingListID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<CRMarketingListMember.contactID>((object) member.ContactID)
    });
  }

  public IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    int marketingListId,
    ICRMarketingListMemberRepository.Options options = null)
  {
    return this.GetMembers(this._graph.Select<CRMarketingList>().FirstOrDefault<CRMarketingList>((Expression<Func<CRMarketingList, bool>>) (m => m.MarketingListID == (int?) marketingListId)) ?? throw new PXArgumentException("The marketing list {0} is not found.", new object[1]
    {
      (object) marketingListId
    }), (ICRMarketingListMemberRepository.Options) null);
  }

  public IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    CRMarketingList list,
    ICRMarketingListMemberRepository.Options options = null)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (!list.MarketingListID.HasValue)
      throw new PXArgumentException("The marketing list ID cannot be empty.");
    return !(list.Type == "S") ? this.GetMembersFromDynamicList(list, options) : this.GetMembersFromStaticList(list, options);
  }

  public IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    IEnumerable<int> marketingListIds,
    ICRMarketingListMemberRepository.Options options = null)
  {
    PXGraph graph = this._graph;
    object[] objArray = new object[1];
    if (!(marketingListIds is int[] numArray))
      numArray = marketingListIds.ToArray<int>();
    objArray[0] = (object) numArray;
    return this.GetMembers(GraphHelper.RowCast<CRMarketingList>((IEnumerable) PXSelectBase<CRMarketingList, PXViewOf<CRMarketingList>.BasedOn<SelectFromBase<CRMarketingList, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRMarketingList.marketingListID, IBqlInt>.IsIn<P.AsInt>>>.ReadOnly.Config>.SelectMultiBound(graph, (object[]) null, objArray)), options);
  }

  public IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    IEnumerable<CRMarketingList> marketingLists,
    ICRMarketingListMemberRepository.Options options = null)
  {
    List<IGrouping<string, CRMarketingList>> list1 = marketingLists.GroupBy<CRMarketingList, string>((Func<CRMarketingList, string>) (l => l.Type)).ToList<IGrouping<string, CRMarketingList>>();
    IGrouping<string, CRMarketingList> source = list1.FirstOrDefault<IGrouping<string, CRMarketingList>>((Func<IGrouping<string, CRMarketingList>, bool>) (group => group.Key == "S"));
    return this.GetMembersFromStaticLists(source != null ? source.Select<CRMarketingList, int>((Func<CRMarketingList, int>) (list => list.MarketingListID.Value)) : (IEnumerable<int>) null, options).Concat<PXResult<CRMarketingListMember, Contact, Address>>(this.GetMembersFromDynamicLists((IEnumerable<CRMarketingList>) list1.FirstOrDefault<IGrouping<string, CRMarketingList>>((Func<IGrouping<string, CRMarketingList>, bool>) (group => group.Key == "D")), options));
  }

  public IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromGI(
    Guid designId,
    Guid? sharedFilterId,
    ICRMarketingListMemberRepository.Options options = null)
  {
    return this.GetMembersFromGI(designId, sharedFilterId, new int?(), options);
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromStaticList(
    CRMarketingList list,
    ICRMarketingListMemberRepository.Options options)
  {
    int? marketingListId = (int?) list?.MarketingListID;
    if (!marketingListId.HasValue)
      return Enumerable.Empty<PXResult<CRMarketingListMember, Contact, Address>>();
    marketingListId = list.MarketingListID;
    return this.GetMembersFromStaticLists(EnumerableExtensions.AsSingleEnumerable<int>(marketingListId.Value), options);
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromStaticLists(
    IEnumerable<int> listsIds,
    ICRMarketingListMemberRepository.Options options)
  {
    object[] array = listsIds != null ? listsIds.OfType<object>().ToArray<object>() : (object[]) null;
    if (array == null || !((IEnumerable<object>) array).Any<object>())
      return (IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>) Array.Empty<PXResult<CRMarketingListMember, Contact, Address>>();
    return this.GetSimpleMembers((PXSelectBase<CRMarketingListMember>) new FbqlSelect<SelectFromBase<CRMarketingListMember, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Contact>.On<BqlOperand<Contact.contactID, IBqlInt>.IsEqual<CRMarketingListMember.contactID>>>, FbqlJoins.Left<Address>.On<BqlOperand<Address.addressID, IBqlInt>.IsEqual<Contact.defAddressID>>>, FbqlJoins.Left<CRLead>.On<BqlOperand<CRLead.contactID, IBqlInt>.IsEqual<CRMarketingListMember.contactID>>>, FbqlJoins.Left<BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.defContactID, Equal<Contact.contactID>>>>>.And<BqlOperand<BAccount.bAccountID, IBqlInt>.IsEqual<Contact.bAccountID>>>>, FbqlJoins.Left<Address2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Address2.addressID, Equal<BAccount.defAddressID>>>>>.And<BqlOperand<Address2.bAccountID, IBqlInt>.IsEqual<BAccount.bAccountID>>>>>.Where<BqlOperand<CRMarketingListMember.marketingListID, IBqlInt>.IsIn<P.AsInt>>.Order<By<BqlField<CRMarketingListMember.isSubscribed, IBqlBool>.Desc, BqlField<Contact.displayName, IBqlString>.Asc>>, CRMarketingListMember>.View(this._graph), new object[1]
    {
      (object) array
    }, options);
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetSimpleMembers(
    PXSelectBase<CRMarketingListMember> view,
    object[] pars,
    ICRMarketingListMemberRepository.Options options)
  {
    bool? withViewContext = options?.WithViewContext;
    if (withViewContext.HasValue && withViewContext.GetValueOrDefault())
      return (IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>) Queryable.OfType<PXResult<CRMarketingListMember, Contact, Address>>((IQueryable) view.SelectWithViewContext(pars));
    if (options != null)
    {
      int chunkSize = options.ChunkSize;
      if (chunkSize > 0)
        return view.SelectChunked<CRMarketingListMember>(parameters: pars, filters: options.ExternalFilters, chunkSize: chunkSize).Cast<PXResult<CRMarketingListMember, Contact, Address>>();
    }
    return view.SelectExtended<CRMarketingListMember>(parameters: pars, filters: options?.ExternalFilters).AsEnumerable<PXResult<CRMarketingListMember>>().Cast<PXResult<CRMarketingListMember, Contact, Address>>();
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromDynamicList(
    CRMarketingList list,
    ICRMarketingListMemberRepository.Options options)
  {
    return !list.GIDesignID.HasValue ? Enumerable.Empty<PXResult<CRMarketingListMember, Contact, Address>>() : this.GetMembersFromGI(list.GIDesignID.Value, list.SharedGIFilter, list.MarketingListID, options);
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromDynamicLists(
    IEnumerable<CRMarketingList> lists,
    ICRMarketingListMemberRepository.Options options)
  {
    return lists == null ? (IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>) Array.Empty<PXResult<CRMarketingListMember, Contact, Address>>() : lists.Select<CRMarketingList, IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>>((Func<CRMarketingList, IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>>) (list => this.GetMembersFromDynamicList(list, options))).SelectMany<IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>, PXResult<CRMarketingListMember, Contact, Address>>((Func<IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>, IEnumerable<PXResult<CRMarketingListMember, Contact, Address>>>) (i => i));
  }

  private IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromGI(
    Guid designId,
    Guid? sharedFilterId,
    int? marketingListId,
    ICRMarketingListMemberRepository.Options options)
  {
    (GIDescription giDescription, string str) = this.BuildGIDescription(designId, sharedFilterId, marketingListId);
    PXGenericInqGrph graph = PXGenericInqGrph.CreateInstance(giDescription);
    ICRMarketingListMemberRepository.Options options1 = options;
    bool withViewContext = options1 != null && options1.WithViewContext;
    ICRMarketingListMemberRepository.Options options2 = options;
    int chunkSize = options2 != null ? options2.ChunkSize : 0;
    int maxRows = withViewContext ? PXView.MaximumRows : chunkSize;
    int startRow = withViewContext ? PXView.StartRow : 0;
    int totalRows = 0;
    int fetchedRows = 0;
    (object[] searches, string[] sortcolumns, bool[] descendings, PXFilterRow[] pxFilterRowArray) = this.GetGIViewParameters(options, str);
    if (sharedFilterId.HasValue)
    {
      PXFilterRow[] array = this.GetSharedFilterRows(giDescription, sharedFilterId).ToArray<PXFilterRow>();
      pxFilterRowArray = pxFilterRowArray == null ? array : (array != null ? ((IEnumerable<PXFilterRow>) array).Union<PXFilterRow>((IEnumerable<PXFilterRow>) pxFilterRowArray).ToArray<PXFilterRow>() : (PXFilterRow[]) null);
    }
label_4:
    List<object> objectList;
    try
    {
      if (!withViewContext)
        startRow = fetchedRows;
      objectList = ((PXSelectBase) graph.Results).View.Select((object[]) null, (object[]) null, searches, sortcolumns, descendings, pxFilterRowArray, ref startRow, maxRows, ref totalRows);
    }
    catch (PXException ex)
    {
      throw new PXException((Exception) ex, "The following error occurred in the {0} generic inquiry: {1}", new object[2]
      {
        (object) giDescription?.Design?.Name,
        (object) ex.MessageNoPrefix
      });
    }
    foreach (GenericResult genericResult in objectList)
    {
      Contact contact = (Contact) genericResult.Values[str];
      object obj1;
      genericResult.Values.TryGetValue("CRLeadMARKETINGLISTEXT", out obj1);
      CRLead crLead = obj1 as CRLead;
      object obj2;
      genericResult.Values.TryGetValue("AddressMARKETINGLISTEXT", out obj2);
      Address address = obj2 as Address;
      object obj3;
      genericResult.Values.TryGetValue("BAccountMARKETINGLISTEXT", out obj3);
      BAccount baccount = obj3 as BAccount;
      object obj4;
      genericResult.Values.TryGetValue("Address2MARKETINGLISTEXT", out obj4);
      Address2 address2 = obj4 as Address2;
      object obj5;
      if (genericResult.Values.TryGetValue("CRMarketingListMemberMARKETINGLISTEXT", out obj5) && obj5 is CRMarketingListMember marketingListMember && marketingListMember.ContactID.HasValue)
      {
        marketingListMember.IsVirtual = new bool?(false);
        marketingListMember.Type = "D";
      }
      else
        marketingListMember = new CRMarketingListMember()
        {
          MarketingListID = marketingListId,
          ContactID = (int?) contact?.ContactID,
          Format = "H",
          IsSubscribed = new bool?(true),
          IsVirtual = new bool?(true),
          Type = "D"
        };
      yield return (PXResult<CRMarketingListMember, Contact, Address>) new PXResult<CRMarketingListMember, Contact, Address, CRLead, BAccount, Address2>(marketingListMember, contact, address, crLead, baccount, address2);
    }
    fetchedRows += totalRows;
    if (withViewContext || totalRows < chunkSize || chunkSize <= 0)
    {
      if (withViewContext)
        PXView.StartRow = 0;
    }
    else
      goto label_4;
  }

  private (GIDescription description, string contactAlias) BuildGIDescription(
    Guid designId,
    Guid? sharedFilterId,
    int? marketingListId)
  {
    GIDescription gi = (GIDescription) (((IEnumerable<GIDescription>) PXGenericInqGrph.Def).FirstOrDefault<GIDescription>((Func<GIDescription, bool>) (x => x.DesignID == designId)) ?? throw new PXInvalidOperationException()).Clone();
    GITable contact = this.GetPrimaryContactTable(gi);
    if (marketingListId.HasValue)
    {
      GITable element1 = new GITable()
      {
        DesignID = new Guid?(gi.DesignID),
        Alias = "CRLeadMARKETINGLISTEXT",
        Name = typeof (CRLead).FullName
      };
      GITable element2 = new GITable()
      {
        DesignID = new Guid?(gi.DesignID),
        Alias = "AddressMARKETINGLISTEXT",
        Name = typeof (Address).FullName
      };
      GITable element3 = new GITable()
      {
        DesignID = new Guid?(gi.DesignID),
        Alias = "BAccountMARKETINGLISTEXT",
        Name = typeof (BAccount).FullName
      };
      GITable element4 = new GITable()
      {
        DesignID = new Guid?(gi.DesignID),
        Alias = "Address2MARKETINGLISTEXT",
        Name = typeof (Address2).FullName
      };
      GITable element5 = new GITable()
      {
        DesignID = new Guid?(gi.DesignID),
        Alias = "CRMarketingListMemberMARKETINGLISTEXT",
        Name = typeof (CRMarketingListMember).FullName
      };
      gi.Tables = (IEnumerable<GITable>) gi.Tables.Append<GITable>(element5).Append<GITable>(element1).Append<GITable>(element2).Append<GITable>(element3).Append<GITable>(element4).ToList<GITable>();
      int num1 = 0;
      if (gi.Relations.Any<GIRelation>() && gi.Ons.Any<GIOn>())
        num1 = Math.Max(gi.Relations.Max<GIRelation>((Func<GIRelation, int>) (r => r.LineNbr.GetValueOrDefault())), gi.Ons.Max<GIOn>((Func<GIOn, int>) (r => r.RelationNbr.GetValueOrDefault())));
      int num2 = num1 + 1;
      int num3 = num1 + 2;
      int num4 = num1 + 3;
      int num5 = num1 + 4;
      int num6 = num1 + 5;
      gi.Relations = (IEnumerable<GIRelation>) gi.Relations.Append<GIRelation>(new GIRelation()
      {
        DesignID = new Guid?(gi.DesignID),
        LineNbr = new int?(num2),
        ParentTable = contact.Alias,
        ChildTable = element5.Alias,
        IsActive = new bool?(true),
        JoinType = "L"
      }).Append<GIRelation>(new GIRelation()
      {
        DesignID = new Guid?(gi.DesignID),
        LineNbr = new int?(num3),
        ParentTable = contact.Alias,
        ChildTable = element1.Alias,
        IsActive = new bool?(true),
        JoinType = "L"
      }).Append<GIRelation>(new GIRelation()
      {
        DesignID = new Guid?(gi.DesignID),
        LineNbr = new int?(num4),
        ParentTable = contact.Alias,
        ChildTable = element2.Alias,
        IsActive = new bool?(true),
        JoinType = "L"
      }).Append<GIRelation>(new GIRelation()
      {
        DesignID = new Guid?(gi.DesignID),
        LineNbr = new int?(num5),
        ParentTable = contact.Alias,
        ChildTable = element3.Alias,
        IsActive = new bool?(true),
        JoinType = "L"
      }).Append<GIRelation>(new GIRelation()
      {
        DesignID = new Guid?(gi.DesignID),
        LineNbr = new int?(num6),
        ParentTable = element3.Alias,
        ChildTable = element4.Alias,
        IsActive = new bool?(true),
        JoinType = "L"
      }).ToList<GIRelation>();
      gi.Ons = (IEnumerable<GIOn>) gi.Ons.Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num2),
        ParentField = "contactID",
        Condition = "E",
        ChildField = "contactID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num2),
        ParentField = "=" + marketingListId.ToString(),
        Condition = "E",
        ChildField = "marketingListID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num3),
        ParentField = "contactID",
        Condition = "E",
        ChildField = "contactID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num4),
        ParentField = "defAddressID",
        Condition = "E",
        ChildField = "addressID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num5),
        ParentField = "bAccountID",
        Condition = "E",
        ChildField = "bAccountID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num5),
        ParentField = "contactID",
        Condition = "E",
        ChildField = "defContactID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num6),
        ParentField = "defAddressID",
        Condition = "E",
        ChildField = "addressID",
        Operation = "A"
      }).Append<GIOn>(new GIOn()
      {
        DesignID = new Guid?(gi.DesignID),
        RelationNbr = new int?(num6),
        ParentField = "bAccountID",
        Condition = "E",
        ChildField = "bAccountID",
        Operation = "A"
      }).ToList<GIOn>();
    }
    gi.GroupBys = (IEnumerable<GIGroupBy>) gi.GroupBys.Append<GIGroupBy>(new GIGroupBy()
    {
      DataFieldName = contact.Alias + ".contactID"
    }).ToList<GIGroupBy>();
    List<GIResult> list1 = ((IEnumerable<GIResult>) new GIResult[63 /*0x3F*/]
    {
      GetResultFor<Contact.contactType>(),
      GetResultFor<Contact.contactID>(),
      GetResultFor<Contact.displayName>(),
      GetResultFor<Contact.isActive>(),
      GetResultFor<Contact.classID>(),
      GetResultFor<Contact.salutation>(),
      GetResultFor<Contact.bAccountID>(),
      GetResultFor<Contact.fullName>(),
      GetResultFor<Contact.eMail>(),
      GetResultFor<Contact.lastModifiedDateTime>(),
      GetResultFor<Contact.createdDateTime>(),
      GetResultFor<Contact.source>(),
      GetResultFor<Contact.assignDate>(),
      GetResultFor<Contact.duplicateStatus>(),
      GetResultFor<Contact.phone1>(),
      GetResultFor<Contact.phone2>(),
      GetResultFor<Contact.phone3>(),
      GetResultFor<Contact.dateOfBirth>(),
      GetResultFor<Contact.fax>(),
      GetResultFor<Contact.webSite>(),
      GetResultFor<Contact.consentAgreement>(),
      GetResultFor<Contact.consentDate>(),
      GetResultFor<Contact.consentExpirationDate>(),
      GetResultFor<Contact.parentBAccountID>(),
      GetResultFor<Contact.gender>(),
      GetResultFor<Contact.method>(),
      GetResultFor<Contact.noCall>(),
      GetResultFor<Contact.noEMail>(),
      GetResultFor<Contact.noFax>(),
      GetResultFor<Contact.noMail>(),
      GetResultFor<Contact.noMarketing>(),
      GetResultFor<Contact.noMassMail>(),
      GetResultFor<Contact.campaignID>(),
      GetResultFor<Contact.phone1Type>(),
      GetResultFor<Contact.phone2Type>(),
      GetResultFor<Contact.phone3Type>(),
      GetResultFor<Contact.faxType>(),
      GetResultFor<Contact.maritalStatus>(),
      GetResultFor<Contact.spouse>(),
      GetResultFor<Contact.status>(),
      GetResultFor<Contact.resolution>(),
      GetResultFor<Contact.languageID>(),
      GetResultFor<Contact.ownerID>(),
      GetResultFor<BAccount.workgroupID>(),
      GetResultFor<BAccount.ownerID>(),
      GetResultFor<BAccount.classID>(),
      GetResultFor<BAccount.parentBAccountID>(),
      GetResultFor<Address.city>(),
      GetResultFor<Address.state>(),
      GetResultFor<Address.postalCode>(),
      GetResultFor<Address.countryID>(),
      GetResultFor<Address.addressLine1>(),
      GetResultFor<Address.addressLine2>(),
      GetResultFor<Address2.city>(),
      GetResultFor<Address2.state>(),
      GetResultFor<Address2.postalCode>(),
      GetResultFor<Address2.countryID>(),
      GetResultFor<Address2.addressLine1>(),
      GetResultFor<Address2.addressLine2>(),
      GetResultFor<CRMarketingListMember.contactID>(),
      GetResultFor<CRMarketingListMember.isSubscribed>(),
      GetResultFor<CRMarketingListMember.format>(),
      GetResultFor<CRMarketingListMember.createdDateTime>()
    }).ToList<GIResult>();
    List<GIResult> list2 = gi.Results.ToList<GIResult>();
    foreach (GIResult giResult in list1)
    {
      if (!list2.Contains(giResult))
        list2.Add(giResult);
    }
    gi.Results = (IEnumerable<GIResult>) list2.ToList<GIResult>();
    return (gi, contact.Alias);

    GIResult GetResultFor<TField>()
    {
      System.Type declaringType = typeof (TField).DeclaringType;
      return new GIResult()
      {
        ObjectName = declaringType == typeof (Contact) ? contact.Alias : CRMarketingListMemberRepository._aliasMapping[declaringType],
        Field = typeof (TField).Name
      };
    }
  }

  private (object[] searches, string[] sortcolumns, bool[] descendings, PXFilterRow[] filters) GetGIViewParameters(
    ICRMarketingListMemberRepository.Options options,
    string contactAlias)
  {
    bool? withViewContext = options?.WithViewContext;
    bool useViewContext = (!withViewContext.HasValue ? 0 : (withViewContext.GetValueOrDefault() ? 1 : 0)) != 0;
    string[] array = ((IEnumerable<string>) EnumerableExtensions.Prepend<string>(GetFromViewOrEmpty<string>(PXView.SortColumns), DacHelper.GetExplicitField<Contact.displayName>(contactAlias))).Select<string, string>(new Func<string, string>(ReplaceExplicitContactColumn)).Select<string, string>(new Func<string, string>(ReplaceMarketingListMemberContactIdColumn)).ToArray<string>();
    int[] indexesToRemove = EnumerableExtensions.SelectIndexesWhere<string>((IEnumerable<string>) array, new Func<string, bool>(IsMarketingListMemberColumn)).ToArray<int>();
    string[] strArray = RemoveMarketingListMemberFields<string>(array);
    object[] objArray = RemoveMarketingListMemberFields<object>(((IEnumerable<object>) EnumerableExtensions.Prepend<object>(GetFromViewOrEmpty<object>(PXView.Searches), (object) null)).ToArray<object>());
    bool[] flagArray = RemoveMarketingListMemberFields<bool>(((IEnumerable<bool>) EnumerableExtensions.Prepend<bool>(GetFromViewOrEmpty<bool>(PXView.Descendings), false)).ToArray<bool>());
    PXFilterRow[] pxFilterRowArray = !useViewContext || PXView.Filters == null ? options?.ExternalFilters : PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    if (pxFilterRowArray != null)
      EnumerableExtensions.ForEach<PXFilterRow>((IEnumerable<PXFilterRow>) pxFilterRowArray, (Action<PXFilterRow>) (f => f.DataField = ReplaceMarketingListMemberContactIdColumn(ReplaceExplicitContactColumn(f.DataField))));
    return (objArray, strArray, flagArray, pxFilterRowArray);

    static bool IsMarketingListMemberColumn(string column) => !column.Contains("_");

    T[] GetFromViewOrEmpty<T>(T[] viewItems) => useViewContext ? viewItems : Array.Empty<T>();

    string ReplaceExplicitContactColumn(string column)
    {
      return Regex.Replace(column, "^Contact__", contactAlias + "_", RegexOptions.IgnoreCase);
    }

    string ReplaceMarketingListMemberContactIdColumn(string column)
    {
      return !column.Equals("ContactID") ? column : DacHelper.GetExplicitField<Contact.contactID>(contactAlias);
    }

    T[] RemoveMarketingListMemberFields<T>(T[] items)
    {
      return Enumerable.ToArray<T>(((IEnumerable<T>) items).Where<T>((Func<T, int, bool>) ((_, i) => !((IEnumerable<int>) indexesToRemove).Contains<int>(i))));
    }
  }

  private GITable GetPrimaryContactTable(GIDescription gi)
  {
    (GITable primary1, GITable fallback1) = GetTables<Contact>();
    if (primary1 != null)
      return primary1;
    (GITable primary2, GITable fallback2) = GetTables<CRLead>();
    return primary2 ?? fallback1 ?? fallback2;

    (GITable primary, GITable fallback) GetTables<TDac>()
    {
      List<GITable> list = gi.Tables.Where<GITable>((Func<GITable, bool>) (table => table.Name == typeof (TDac).FullName)).ToList<GITable>();
      return (list.FirstOrDefault<GITable>((Func<GITable, bool>) (table => gi.Relations.All<GIRelation>((Func<GIRelation, bool>) (relation => (!(relation.ChildTable == table.Alias) || !(relation.JoinType == "L")) && (!(relation.ParentTable == table.Alias) || !(relation.JoinType == "R")))))), list.FirstOrDefault<GITable>());
    }
  }

  private IEnumerable<PXFilterRow> GetSharedFilterRows(GIDescription gi, Guid? sharedGIFilter)
  {
    if (sharedGIFilter.HasValue)
    {
      PXGraph graph = this._graph;
      object[] objArray = new object[1]
      {
        (object) sharedGIFilter
      };
      foreach (PXResult<FilterRow> pxResult in PXSelectBase<FilterRow, PXSelect<FilterRow, Where<FilterRow.filterID, Equal<Required<FilterRow.filterID>>, And<FilterRow.isUsed, Equal<True>>>>.Config>.Select(graph, objArray))
      {
        FilterRow filterRow = PXResult<FilterRow>.op_Implicit(pxResult);
        string[] a = filterRow.DataField.Split('_');
        bool flag = false;
        if (a.Length == 2)
        {
          string name = gi.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (_ => _.Alias == a[0]))?.Name;
          string str = a[1];
          if (!string.IsNullOrWhiteSpace(name))
          {
            System.Type type = PXBuildManager.GetType(name, false);
            if (filterRow.ValueSt != "@me" && filterRow.ValueSt != "@mygroups" && filterRow.ValueSt != "@myworktree" && this._graph.Caches[type].GetStateExt((object) null, str) is PXFieldState stateExt && stateExt.DataType == typeof (int))
            {
              if (!int.TryParse(filterRow.ValueSt, out int _))
                flag = true;
              if (!int.TryParse(filterRow.ValueSt2, out int _))
                flag = true;
            }
          }
        }
        PXFilterRow sharedFilterRow = new PXFilterRow();
        sharedFilterRow.DataField = flag ? filterRow.DataField + "_description" : filterRow.DataField;
        int? nullable = filterRow.OpenBrackets;
        sharedFilterRow.OpenBrackets = nullable.GetValueOrDefault();
        nullable = filterRow.CloseBrackets;
        sharedFilterRow.CloseBrackets = nullable.GetValueOrDefault();
        sharedFilterRow.OrigValue = (object) filterRow.ValueSt;
        sharedFilterRow.OrigValue2 = (object) filterRow.ValueSt2;
        nullable = filterRow.Operator;
        sharedFilterRow.OrOperator = nullable.GetValueOrDefault() == 1;
        sharedFilterRow.Value = (object) filterRow.ValueSt;
        sharedFilterRow.Value2 = (object) filterRow.ValueSt2;
        sharedFilterRow.Condition = (PXCondition) (int) filterRow.Condition.Value;
        yield return sharedFilterRow;
      }
    }
  }
}
