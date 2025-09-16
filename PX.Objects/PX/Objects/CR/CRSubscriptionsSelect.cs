// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSubscriptionsSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public sealed class CRSubscriptionsSelect
{
  public static IEnumerable Select(PXGraph graph, int? mailListID)
  {
    int startRow = PXView.StartRow;
    int totalRows = 0;
    IEnumerable enumerable = CRSubscriptionsSelect.Select(graph, mailListID, PXView.Searches, PXView.SortColumns, PXView.Descendings, ref startRow, PXView.MaximumRows, ref totalRows);
    PXView.StartRow = 0;
    return enumerable;
  }

  public static IEnumerable Select(
    PXGraph graph,
    int? mailListID,
    object[] searches,
    string[] sortColumns,
    bool[] descendings,
    ref int startRow,
    int maxRows,
    ref int totalRows)
  {
    if (!mailListID.HasValue || PXResultset<CRMarketingList>.op_Implicit(PXSelectBase<CRMarketingList, PXSelect<CRMarketingList>.Config>.Search<CRMarketingList.marketingListID>(graph, (object) mailListID, Array.Empty<object>())) == null)
      return (IEnumerable) new PXResultset<Contact, BAccount, BAccountParent, Address, PX.Objects.CS.State>();
    BqlCommand bqlCommand = (BqlCommand) new Select2<Contact, LeftJoin<CRMarketingListMember, On<CRMarketingListMember.contactID, Equal<Contact.contactID>, And<CRMarketingListMember.marketingListID, Equal<Current<CRMarketingList.marketingListID>>>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>, LeftJoin<BAccountParent, On<BAccountParent.bAccountID, Equal<Contact.parentBAccountID>>, LeftJoin<Address, On<Address.addressID, Equal<Contact.defAddressID>>, LeftJoin<PX.Objects.CS.State, On<PX.Objects.CS.State.countryID, Equal<Address.countryID>, And<PX.Objects.CS.State.stateID, Equal<Address.state>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<Contact.bAccountID>, And<Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>, LeftJoin<CRLead, On<BqlOperand<CRLead.contactID, IBqlInt>.IsEqual<Contact.contactID>>>>>>>>>, Where<PX.Objects.GL.Branch.branchID, IsNull>>();
    PXView pxView = new PXView(graph, true, bqlCommand);
    List<string> stringList = new List<string>()
    {
      "CRMarketingListMember__IsSubscribed",
      "MemberName",
      "ContactID"
    };
    List<bool> boolList = new List<bool>()
    {
      false,
      false,
      false
    };
    List<object> objectList = new List<object>()
    {
      (object) null,
      (object) null,
      (object) null
    };
    objectList.AddRange((IEnumerable<object>) searches);
    object[] array1 = objectList.ToArray();
    string[] array2 = stringList.ToArray();
    bool[] array3 = boolList.ToArray();
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int num = maxRows;
    ref int local2 = ref totalRows;
    return (IEnumerable) pxView.Select((object[]) null, (object[]) null, array1, array2, array3, pxFilterRowArray, ref local1, num, ref local2);
  }
}
