// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolRecordsParametersAPI
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.GL.Consolidation;

internal class ConsolRecordsParametersAPI
{
  public virtual string LedgerCD { get; set; }

  public virtual string BranchCD { get; set; }

  public ConsolRecordsParametersAPI()
  {
  }

  public ConsolRecordsParametersAPI(string ledgerCD, string branchCD)
  {
    this.LedgerCD = ledgerCD;
    this.BranchCD = branchCD;
  }
}
