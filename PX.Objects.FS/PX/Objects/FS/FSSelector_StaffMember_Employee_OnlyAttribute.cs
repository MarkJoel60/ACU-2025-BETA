// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelector_StaffMember_Employee_OnlyAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelector_StaffMember_Employee_OnlyAttribute : PXDimensionSelectorAttribute
{
  public FSSelector_StaffMember_Employee_OnlyAttribute()
    : base("BIZACCT", typeof (Search2<BAccountStaffMember.bAccountID, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>, Where<FSxEPEmployee.sDEnabled, Equal<True>>, OrderBy<Asc<BAccountSelectorBase.acctCD>>>), typeof (BAccountSelectorBase.acctCD), new Type[5]
    {
      typeof (BAccountSelectorBase.acctCD),
      typeof (BAccountSelectorBase.acctName),
      typeof (BAccountSelectorBase.type),
      typeof (BAccountStaffMember.vStatus),
      typeof (EPEmployeePosition.positionID)
    })
  {
    this.DescriptionField = typeof (BAccountSelectorBase.acctName);
  }
}
