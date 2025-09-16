// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractEntryVisibilityRestriction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class ServiceContractEntryVisibilityRestriction : PXGraphExtension<ServiceContractEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    ServiceContractEntryVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    string str1 = "ServiceContractRecords: 1";
    string str2 = "ServiceContractRecords";
    (string, string) firstField = ("BranchID", str1);
    Utilities.SetDependentFieldsAfterBranch(script, firstField, new List<(string, string)>()
    {
      ("CustomerID", str2),
      ("CustomerLocationID", str2),
      ("BillCustomerID", str1),
      ("BillLocationID", str1)
    });
  }

  public virtual void _(
    Events.FieldUpdated<FSServiceContract, FSServiceContract.billCustomerID> e)
  {
    if (e.Row == null)
      return;
    this.Base.SetBillTo(e.Row);
  }

  public virtual void _(
    Events.FieldUpdated<FSServiceContract, FSServiceContract.branchID> e)
  {
    if (e.Row == null)
      return;
    this.Base.SetBillTo(e.Row);
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
