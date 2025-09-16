// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelector_StaffMember_ServiceOrderProjectIDAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelector_StaffMember_ServiceOrderProjectIDAttribute : PXDimensionSelectorAttribute
{
  public FSSelector_StaffMember_ServiceOrderProjectIDAttribute()
    : base("BIZACCT", typeof (Search2<BAccountStaffMember.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<Current<FSServiceOrder.projectID>>>, LeftJoin<EPEmployeeContract, On<EPEmployeeContract.contractID, Equal<PMProject.contractID>, And<EPEmployeeContract.employeeID, Equal<BAccountStaffMember.bAccountID>>>, LeftJoin<EPEmployeePosition, On<EPEmployeePosition.employeeID, Equal<EPEmployee.bAccountID>, And<EPEmployeePosition.isActive, Equal<True>>>>>>>>, Where<PMProject.isActive, Equal<True>, And<PMProject.baseType, Equal<CTPRType.project>, And<Where2<Where<FSxVendor.sDEnabled, Equal<True>>, Or<Where<FSxEPEmployee.sDEnabled, Equal<True>, And<Where<PMProject.restrictToEmployeeList, Equal<False>, Or<EPEmployeeContract.employeeID, IsNotNull>>>>>>>>>, OrderBy<Asc<BAccountSelectorBase.acctCD>>>), typeof (BAccountSelectorBase.acctCD), new Type[5]
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
