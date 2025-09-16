// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolBranchMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.DAC;

#nullable disable
namespace PX.Objects.GL.Consolidation;

public class ConsolBranchMaint : PXGraph<ConsolBranchMaint>
{
  public PXSelectJoin<Branch, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>, And<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesBalancing>>>, InnerJoin<PX.Objects.GL.Ledger, On<PX.Objects.GL.Ledger.ledgerID, Equal<Branch.ledgerID>>>>, Where<PX.Objects.GL.Ledger.consolAllowed, Equal<True>, Or<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.actual>>>> BranchRecords;
}
