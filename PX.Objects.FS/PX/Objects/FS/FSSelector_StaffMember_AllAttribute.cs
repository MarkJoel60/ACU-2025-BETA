// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelector_StaffMember_AllAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelector_StaffMember_AllAttribute : PXDimensionSelectorAttribute
{
  public FSSelector_StaffMember_AllAttribute(Type parmSubstituteKey = null)
  {
    Type type1 = typeof (Search2<BAccountStaffMember.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>, Where2<Where<FSxVendor.sDEnabled, Equal<True>, And<Where<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.oneTime>>>>>, Or<Where<FSxEPEmployee.sDEnabled, Equal<True>>>>, OrderBy<Asc<BAccountSelectorBase.acctCD>>>);
    Type type2 = parmSubstituteKey;
    if ((object) type2 == null)
      type2 = typeof (BAccountSelectorBase.acctCD);
    Type[] typeArray = new Type[5]
    {
      typeof (BAccountSelectorBase.acctCD),
      typeof (BAccountSelectorBase.acctName),
      typeof (BAccountSelectorBase.type),
      typeof (BAccountStaffMember.vStatus),
      typeof (EPEmployeePosition.positionID)
    };
    // ISSUE: explicit constructor call
    base.\u002Ector("BIZACCT", type1, type2, typeArray);
    this.DescriptionField = typeof (BAccountSelectorBase.acctName);
  }
}
