// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenceClaimSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.TM;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Allow show expence claim records.</summary>
/// <example>[EPExpenceClaimSelector]</example>
public class EPExpenceClaimSelectorAttribute : PXSelectorAttribute
{
  public EPExpenceClaimSelectorAttribute()
    : base(typeof (Search2<EPExpenseClaim.refNbr, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaim.employeeID>>>, Where<EPExpenseClaim.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>>>>>>, OrderBy<Desc<EPExpenseClaim.refNbr>>>), new Type[8]
    {
      typeof (EPExpenseClaim.docDate),
      typeof (EPExpenseClaim.refNbr),
      typeof (EPExpenseClaim.status),
      typeof (EPExpenseClaim.docDesc),
      typeof (EPExpenseClaim.curyDocBal),
      typeof (EPExpenseClaim.curyID),
      typeof (EPEmployee.acctName),
      typeof (EPExpenseClaim.departmentID)
    })
  {
  }
}
