// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.ICRMarketingListMemberRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

[PXInternalUseOnly]
public interface ICRMarketingListMemberRepository
{
  IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    int marketingListId,
    ICRMarketingListMemberRepository.Options options = null);

  IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    CRMarketingList marketingList,
    ICRMarketingListMemberRepository.Options options = null);

  IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    IEnumerable<int> marketingListIds,
    ICRMarketingListMemberRepository.Options options = null);

  IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembers(
    IEnumerable<CRMarketingList> marketingLists,
    ICRMarketingListMemberRepository.Options options = null);

  IEnumerable<PXResult<CRMarketingListMember, Contact, Address>> GetMembersFromGI(
    Guid designId,
    Guid? sharedFilterId,
    ICRMarketingListMemberRepository.Options options = null);

  void InsertMember(CRMarketingListMember member);

  void UpdateMember(CRMarketingListMember member);

  void DeleteMember(CRMarketingListMember member);

  class Options
  {
    public bool WithViewContext { get; set; }

    [Obsolete("Currently not working")]
    public int ChunkSize { get; set; }

    public PXFilterRow[] ExternalFilters { get; set; }
  }
}
