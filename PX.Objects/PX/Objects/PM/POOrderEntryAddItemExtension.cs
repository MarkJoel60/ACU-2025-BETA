// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntryAddItemExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using System;

#nullable enable
namespace PX.Objects.PM;

public class POOrderEntryAddItemExtension : 
  PXGraphExtension<POOrderSiteStatusLookupExt, POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual POLine InitNewLine(
    POSiteStatusSelected line,
    POLine newLine,
    Func<POSiteStatusSelected, POLine, POLine> baseMethod)
  {
    newLine = baseMethod(line, newLine);
    int? projectId = (int?) ((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current?.ProjectID;
    if (projectId.HasValue)
    {
      int? nullable1 = projectId;
      int? nullable2 = ProjectDefaultAttribute.NonProject();
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        newLine.ProjectID = projectId;
    }
    return newLine;
  }

  [PXOverride]
  public virtual POLine? CreateNewLine(
    POSiteStatusSelected? line,
    Func<POSiteStatusSelected?, POLine?> baseMethod)
  {
    POLine newLine = baseMethod(line);
    if (((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current == null)
      return newLine;
    ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Cache.SetDefaultExt<POOrder.taxZoneID>((object) ((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current);
    return newLine;
  }
}
