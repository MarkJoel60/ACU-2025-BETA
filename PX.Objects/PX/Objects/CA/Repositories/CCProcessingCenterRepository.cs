// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Repositories.CCProcessingCenterRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.BQLConstants;
using PX.Objects.CC;
using System;

#nullable disable
namespace PX.Objects.CA.Repositories;

public class CCProcessingCenterRepository
{
  protected readonly PXGraph _graph;

  public CCProcessingCenterRepository(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public CCProcessingCenter GetProcessingCenterByID(string processingCenterID)
  {
    return CCProcessingCenter.PK.Find(this._graph, processingCenterID);
  }

  public (CCProcessingCenter, CCProcessingCenterBranch) GetProcessingCenterBranchByBranchAndProcCenterIDs(
    int? branchId,
    string procCenterId)
  {
    PXResult<CCProcessingCenterBranch, CCProcessingCenter> pxResult = (PXResult<CCProcessingCenterBranch, CCProcessingCenter>) PXResultset<CCProcessingCenterBranch>.op_Implicit(PXSelectBase<CCProcessingCenterBranch, PXSelectJoin<CCProcessingCenterBranch, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterBranch.processingCenterID>>>, Where<CCProcessingCenterBranch.branchID, Equal<Required<CCProcessingCenterBranch.branchID>>, And<CCProcessingCenterBranch.processingCenterID, Equal<Required<CCProcessingCenterBranch.processingCenterID>>>>>.Config>.Select(this._graph, new object[2]
    {
      (object) branchId,
      (object) procCenterId
    }));
    return (PXResult<CCProcessingCenterBranch, CCProcessingCenter>.op_Implicit(pxResult), PXResult<CCProcessingCenterBranch, CCProcessingCenter>.op_Implicit(pxResult));
  }

  public CCProcessingCenter FindProcessingCenter(int? pMInstanceID, string aCuryId)
  {
    CCProcessingCenter processingCenter1 = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) pMInstanceID
    }));
    if (processingCenter1 != null)
      return processingCenter1;
    CCProcessingCenter processingCenter2;
    if (aCuryId != null)
      processingCenter2 = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>, And<CCProcessingCenter.isActive, Equal<BitOn>>>, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethod.isActive, Equal<BitOn>>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CCProcessingCenter.cashAccountID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>, And<PX.Objects.CA.CashAccount.curyID, Equal<Required<PX.Objects.CA.CashAccount.curyID>>>>, OrderBy<Desc<CCProcessingCenterPmntMethod.isDefault>>>.Config>.Select(this._graph, new object[2]
      {
        (object) pMInstanceID,
        (object) aCuryId
      }));
    else
      processingCenter2 = PXResultset<CCProcessingCenter>.op_Implicit(PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>, And<CCProcessingCenter.isActive, Equal<BitOn>>>, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethod.isActive, Equal<BitOn>>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CCProcessingCenter.cashAccountID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>, OrderBy<Desc<CCProcessingCenterPmntMethod.isDefault>>>.Config>.Select(this._graph, new object[1]
      {
        (object) pMInstanceID
      }));
    return processingCenter2;
  }
}
