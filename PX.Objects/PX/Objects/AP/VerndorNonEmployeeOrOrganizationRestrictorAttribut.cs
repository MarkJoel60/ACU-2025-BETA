// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VerndorNonEmployeeOrOrganizationRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.AP;

public class VerndorNonEmployeeOrOrganizationRestrictorAttribute : PXRestrictorAttribute
{
  public VerndorNonEmployeeOrOrganizationRestrictorAttribute()
    : base(typeof (Where<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType, BAccountType.vendorType, BAccountType.combinedType>>), "Only a vendor or company business account can be specified (not an employee business account).")
  {
  }

  public VerndorNonEmployeeOrOrganizationRestrictorAttribute(System.Type receiptType)
    : base(BqlTemplate.OfCondition<Where<Current<BqlPlaceholder.A>, NotEqual<POReceiptType.transferreceipt>, And<Vendor.vStatus, PX.Data.IsNotNull, And<BAccountR.type, In3<BAccountType.vendorType, BAccountType.combinedType>, Or<Current<BqlPlaceholder.A>, Equal<POReceiptType.transferreceipt>, PX.Data.And<Where<PX.Objects.CR.BAccount.isBranch, Equal<True>, Or<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType, BAccountType.combinedType>>>>>>>>>.Replace<BqlPlaceholder.A>(receiptType).ToType(), "The value you have typed is not valid for the selected type of a purchase receipt. View the list of correct values by clicking the magnifier icon.")
  {
  }
}
