// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint_Extensions.BusinessAccountMaint_MarketingListDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.BusinessAccountMaint_Extensions;

/// <exclude />
public class BusinessAccountMaint_MarketingListDetailsExt : 
  MarketingListDetailsExt<BusinessAccountMaint, BAccount, BAccount.defContactID>
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (BAccount.defContactID), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Name")]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRMarketingListMember.contactID> e)
  {
  }
}
