// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderEntryVisibilityRestriction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderEntryVisibilityRestriction : PXGraphExtension<ServiceOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXRemoveBaseAttribute(typeof (RestrictCustomerByUserBranches))]
  public void _(PX.Data.Events.CacheAttached<FSServiceOrder.customerID> e)
  {
  }

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    ServiceOrderEntryVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    string str1 = "CurrentServiceOrder: 1";
    string str2 = "ServiceOrderRecords";
    (string, string) firstField = ("BranchID", str1);
    PX.Objects.Common.Utilities.SetDependentFieldsAfterBranch(script, firstField, new List<(string, string)>()
    {
      ("CustomerID", str2),
      ("LocationID", str2),
      ("BranchLocationID", str2),
      ("BillCustomerID", str1),
      ("BillLocationID", str1)
    });
  }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public void _(PX.Data.Events.CacheAttached<FSSODet.poVendorID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (FSSelector_StaffMember_ServiceOrderProjectIDAttribute))]
  [PXMergeAttributes]
  [FSSelector_StaffMember_ServiceOrderProjectIDVisibilityRestriction]
  public void _(PX.Data.Events.CacheAttached<FSSOEmployee.employeeID> e)
  {
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
