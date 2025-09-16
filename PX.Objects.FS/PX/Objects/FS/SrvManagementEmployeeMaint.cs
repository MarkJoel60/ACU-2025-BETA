// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SrvManagementEmployeeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.FS;

public class SrvManagementEmployeeMaint : PXGraph<SrvManagementEmployeeMaint>
{
  public PXSelectJoin<BAccountStaffMember, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<BAccountStaffMember.bAccountID>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<BAccountSelectorBase.defContactID>>>>>, Where<FSxEPEmployee.sDEnabled, Equal<True>, Or<FSxVendor.sDEnabled, Equal<True>>>> SrvManagementStaffRecords;
  public PXAction<BAccountStaffMember> addEmployee;
  public PXAction<BAccountStaffMember> addVendor;

  public SrvManagementEmployeeMaint()
  {
    PXUIFieldAttribute.SetDisplayName<BAccountSelectorBase.acctCD>(((PXSelectBase) this.SrvManagementStaffRecords).Cache, "Staff Member ID");
    PXUIFieldAttribute.SetDisplayName<BAccountSelectorBase.acctName>(((PXSelectBase) this.SrvManagementStaffRecords).Cache, "Staff Member Name");
  }

  [PXButton]
  [PXUIField]
  public virtual void AddEmployee()
  {
    EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
    ((PXAction) instance.Insert).Press();
    ((PXSelectBase) instance.Employee).Cache.GetExtension<FSxEPEmployee>((object) ((PXSelectBase<EPEmployee>) instance.Employee).Current).SDEnabled = new bool?(true);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public virtual void AddVendor()
  {
    VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
    ((PXAction) instance.Insert).Press();
    ((PXSelectBase) instance.CurrentVendor).Cache.GetExtension<FSxVendor>((object) ((PXSelectBase<PX.Objects.AP.Vendor>) instance.CurrentVendor).Current).SDEnabled = new bool?(true);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }
}
