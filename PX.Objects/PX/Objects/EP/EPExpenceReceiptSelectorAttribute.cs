// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenceReceiptSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.TM;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Allow show expence receipt records.</summary>
/// <example>[EPExpenceReceiptSelector]</example>
public class EPExpenceReceiptSelectorAttribute : PXSelectorAttribute
{
  public EPExpenceReceiptSelectorAttribute()
    : base(typeof (Search2<EPExpenseClaimDetails.claimDetailCD, LeftJoin<EPExpenseClaim, On<EPExpenseClaim.refNbr, Equal<EPExpenseClaimDetails.refNbr>>, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaimDetails.employeeID>>>>, Where<EPExpenseClaimDetails.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaimDetails.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>, Or<EPExpenseClaimDetails.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>>>>>>>, OrderBy<Desc<EPExpenseClaimDetails.claimDetailCD>>>), new Type[7]
    {
      typeof (EPExpenseClaimDetails.claimDetailCD),
      typeof (EPExpenseClaimDetails.expenseDate),
      typeof (EPExpenseClaimDetails.curyTranAmt),
      typeof (EPExpenseClaimDetails.curyID),
      typeof (EPExpenseClaimDetails.employeeID),
      typeof (EPExpenseClaimDetails.refNbr),
      typeof (EPExpenseClaimDetails.status)
    })
  {
  }
}
