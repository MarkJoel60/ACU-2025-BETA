// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.CS.GraphExtensions.NumberingMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.CS.GraphExtensions;

public class NumberingMaintExt : PXGraphExtension<NumberingMaint>
{
  [InjectDependency]
  public INumberingSequenceUsage NumberingSequenceUsage { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected virtual void _(Events.RowDeleting<Numbering> args)
  {
    this.NumberingSequenceUsage.CheckForNumberingUsage<POSetup, PoSetupExt.subcontractNumberingID>(args.Row, (PXGraph) this.Base, "Subcontracts Preferences");
  }
}
