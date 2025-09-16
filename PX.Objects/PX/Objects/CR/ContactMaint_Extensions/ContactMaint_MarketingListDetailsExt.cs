// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint_Extensions.ContactMaint_MarketingListDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.ContactMaint_Extensions;

/// <exclude />
public class ContactMaint_MarketingListDetailsExt : 
  MarketingListDetailsExt<ContactMaint, Contact, Contact.contactID>
{
  [PXDBDefault(typeof (Contact.contactID))]
  [PXMergeAttributes]
  protected virtual void _(
    Events.CacheAttached<CRMarketingListMember.contactID> e)
  {
  }

  protected virtual void _(Events.RowSelected<CRMarketingListMember> e)
  {
    CRMarketingListMember row = e.Row;
    if (row == null)
      return;
    CRMarketingList crMarketingList = PXResultset<CRMarketingList>.op_Implicit(PXSelectBase<CRMarketingList, PXSelect<CRMarketingList, Where<CRMarketingList.marketingListID, Equal<Required<CRMarketingList.marketingListID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.MarketingListID
    }));
    if (crMarketingList == null)
      return;
    PXUIFieldAttribute.SetEnabled<CRMarketingList.marketingListID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRMarketingListMember>>) e).Cache, (object) row, crMarketingList.Type == "S");
  }
}
