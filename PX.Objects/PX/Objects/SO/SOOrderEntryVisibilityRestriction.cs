// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntryVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderEntryVisibilityRestriction : PXGraphExtension<SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers,
    SOOrderEntryVisibilityRestriction.CopyPasteGetScriptDelegate baseMethod)
  {
    baseMethod(isImportSimple, script, containers);
    Utilities.SetFieldCommandToTheTop(script, containers, "CurrentDocument", "BranchID");
  }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public void _(PX.Data.Events.CacheAttached<SOLine.vendorID> e)
  {
  }

  public delegate void CopyPasteGetScriptDelegate(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers);
}
