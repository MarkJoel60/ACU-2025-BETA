// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MigrationMode.APSetupNoMigrationMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;

#nullable disable
namespace PX.Objects.AP.MigrationMode;

/// <summary>
/// An <see cref="T:PX.Data.PXSetup`1" /> descendant. In addition to checking the
/// presence of the setup record, also checks that <see cref="P:PX.Objects.AP.APSetup.MigrationMode">
/// migration mode</see> is not enabled in the module.
/// </summary>
public class APSetupNoMigrationMode : PXSetup<APSetup>
{
  private PXGraph Graph { get; set; }

  public APSetupNoMigrationMode(PXGraph graph)
    : base(graph)
  {
    this.Graph = graph;
  }

  public bool DisableMigrationCheck { get; set; }

  public override APSetup Current
  {
    get
    {
      APSetup current = base.Current;
      if (!this.DisableMigrationCheck)
        APSetupNoMigrationMode.EnsureMigrationModeDisabled(this.Graph, current);
      return current;
    }
    set => base.Current = value;
  }

  public static void EnsureMigrationModeDisabled(PXGraph graph, APSetup setup = null)
  {
    setup = setup ?? (APSetup) PXSelectBase<APSetup, PXSelectReadonly<APSetup>.Config>.Select(graph);
    if (setup != null)
      setup.MigrationMode = new bool?(CurrentConfiguration.ActualAPSetup.isMigrationModeEnabled);
    if ((setup != null ? (!setup.MigrationMode.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      throw new PXSetupNotEnteredException("Migration mode is activated in the Accounts Payable module.", typeof (APSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Accounts Payable Preferences")
      });
  }
}
