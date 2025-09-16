// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ParentBAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.CR;

[PXAttributeFamily(typeof (PXEntityAttribute))]
public class ParentBAccountAttribute : BAccountAttribute
{
  public ParentBAccountAttribute(
    System.Type bAccountIDField,
    System.Type[] bAccountTypes = null,
    System.Type customSearchQuery = null,
    System.Type[] fieldList = null,
    string[] headerList = null)
  {
    System.Type[] bAccountTypes1 = bAccountTypes;
    if (bAccountTypes1 == null)
      bAccountTypes1 = new System.Type[4]
      {
        typeof (BAccountType.prospectType),
        typeof (BAccountType.customerType),
        typeof (BAccountType.combinedType),
        typeof (BAccountType.vendorType)
      };
    System.Type customSearchQuery1 = customSearchQuery;
    if ((object) customSearchQuery1 == null)
      customSearchQuery1 = ParentBAccountAttribute.CreateSelectWithRestriction(bAccountIDField);
    // ISSUE: explicit constructor call
    base.\u002Ector(bAccountTypes1, customSearchQuery1, fieldList, headerList);
    this.DisplayName = "Parent Account";
  }

  protected static System.Type CreateSelectWithRestriction(System.Type bAccountIDField)
  {
    return ((IBqlTemplate) BqlTemplate.OfCommand<Search2<BAccountR.bAccountID, LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccountR.bAccountID>, And<Contact.contactID, Equal<BAccountR.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BAccountR.bAccountID>, And<Address.addressID, Equal<BAccountR.defAddressID>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<BqlPlaceholder.A>, IsNull>>>>.Or<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsNotEqual<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.FromCurrent>>>>>.And<Match<Current<AccessInfo.userName>>>>>>.Replace<BqlPlaceholder.A>(bAccountIDField)).ToType();
  }
}
