// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.TaxExpenseAllocationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO.GraphExtensions.APReleaseProcessExt;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class TaxExpenseAllocationExt : 
  PXGraphExtension<PX.Objects.PO.GraphExtensions.APReleaseProcessExt.TaxExpenseAllocationExt, UpdatePOOnRelease, APReleaseProcess.MultiCurrency, APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void CalculateTaxExpenseAllocation(
    APRegister apdoc,
    List<INRegister> inDocs,
    Action<APRegister, List<INRegister>> baseCalculateTaxExpenseAllocation)
  {
    using (new SkipDefaultingFromLocationScope())
      baseCalculateTaxExpenseAllocation(apdoc, inDocs);
  }
}
