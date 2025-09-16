// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLUtility
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class GLUtility
{
  public static bool IsAccountHistoryExist(PXGraph graph, int? accountID)
  {
    return ((PXSelectBase) new PXSelect<GLHistory, Where<GLHistory.accountID, Equal<Required<GLHistory.accountID>>>>(graph)).View.SelectSingle(new object[1]
    {
      (object) accountID
    }) != null;
  }

  internal static bool IsAccountGLTranExist(PXGraph graph, int? accountID)
  {
    return ((PXSelectBase) new PXSelect<GLTran, Where<GLTran.accountID, Equal<Required<GLTran.accountID>>>>(graph)).View.SelectSingle(new object[1]
    {
      (object) accountID
    }) != null;
  }

  public static bool IsLedgerHistoryExist(PXGraph graph, int? ledgerID)
  {
    return ((PXSelectBase) new PXSelect<GLHistory, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>>>(graph)).View.SelectSingle(new object[1]
    {
      (object) ledgerID
    }) != null;
  }

  public static bool RelatedGLHistoryExists(PXGraph graph, int? ledgerID, int? organizationID)
  {
    return PXResultset<GLHistory>.op_Implicit(PXSelectBase<GLHistory, PXSelectReadonly2<GLHistory, InnerJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>>, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<Branch.organizationID, Equal<Required<Branch.organizationID>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) ledgerID,
      (object) organizationID
    })) != null;
  }

  public static bool RelatedForOrganizationGLHistoryExists(PXGraph graph, int? organizationID)
  {
    return PXResultset<GLHistory>.op_Implicit(PXSelectBase<GLHistory, PXSelectReadonly2<GLHistory, InnerJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>>, Where<Branch.organizationID, Equal<Required<Branch.organizationID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) organizationID
    })) != null;
  }

  public static GLHistory GetRelatedToBranchGLHistory(PXGraph graph, int?[] branchIDs)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return (GLHistory) null;
    return PXResultset<GLHistory>.op_Implicit(PXSelectBase<GLHistory, PXSelectReadonly<GLHistory, Where<GLHistory.branchID, In<Required<GLHistory.branchID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) branchIDs
    }));
  }

  public static bool RelatedForBranchReleasedTransactionExists(PXGraph graph, int? branchID)
  {
    return PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelectReadonly<GLTran, Where<GLTran.branchID, Equal<Required<GLTran.branchID>>, And<GLTran.released, Equal<True>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) branchID
    })) != null;
  }
}
