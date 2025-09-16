// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteMaintVisibilityRestriction : PXGraphExtension<PMQuoteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    PMQuoteMaintVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    string str1 = "QuoteCurrent: 1";
    string str2 = "Quote";
    (string, string) firstField = ("BranchID", str1);
    Utilities.SetDependentFieldsAfterBranch(script, firstField, new List<(string, string)>()
    {
      ("BAccountID", str2),
      ("LocationID", str1)
    });
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
