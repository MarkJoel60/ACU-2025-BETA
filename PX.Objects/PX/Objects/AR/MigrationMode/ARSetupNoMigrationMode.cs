// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.MigrationMode.ARSetupNoMigrationMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.AR.MigrationMode;

/// <summary>
/// An <see cref="T:PX.Data.PXSetup`1" /> descendant. In addition to checking the
/// presence of the setup record, also checks that <see cref="P:PX.Objects.AR.ARSetup.MigrationMode">
/// migration mode</see> is not enabled in the module.
/// </summary>
public class ARSetupNoMigrationMode : PXSetup<ARSetup>
{
  private PXGraph Graph { get; set; }

  public ARSetupNoMigrationMode(PXGraph graph)
    : base(graph)
  {
    this.Graph = graph;
  }

  public virtual ARSetup Current
  {
    get
    {
      ARSetup current = ((PXSelectBase<ARSetup>) this).Current;
      ARSetupNoMigrationMode.EnsureMigrationModeDisabled(this.Graph, current);
      return current;
    }
    set => ((PXSelectBase<ARSetup>) this).Current = value;
  }

  public static void EnsureMigrationModeDisabled(PXGraph graph, ARSetup setup = null)
  {
    setup = setup ?? PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelectReadonly<ARSetup>.Config>.Select(graph, Array.Empty<object>()));
    if (setup != null)
      setup.MigrationMode = new bool?(CurrentConfiguration.ActualARSetup.isMigrationModeEnabled);
    if ((setup != null ? (!setup.MigrationMode.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      throw new PXSetupNotEnteredException("Migration mode is activated in the Accounts Receivable module.", typeof (ARSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Account Receivable Preferences")
      });
  }
}
