// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DAC.EPExpenseClaimForCurrentUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.TM;

#nullable disable
namespace PX.Objects.EP.DAC;

[PXCacheName("Expense Claim")]
[PXProjection(typeof (Select2<EPExpenseClaim, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<EPExpenseClaim.employeeID>>>, Where<EPExpenseClaim.createdByID, Equal<CurrentValue<AccessInfo.userID>>, Or<PX.Objects.EP.EPEmployee.defContactID, Equal<CurrentValue<AccessInfo.contactID>>, Or<PX.Objects.EP.EPEmployee.defContactID, IsSubordinateOfContact<CurrentValue<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<CurrentValue<AccessInfo.contactID>>, Or<EPExpenseClaim.employeeID, WingmanUser<CurrentValue<AccessInfo.userID>, EPDelegationOf.expenses>>>>>>>))]
public class EPExpenseClaimForCurrentUser : EPExpenseClaim
{
}
