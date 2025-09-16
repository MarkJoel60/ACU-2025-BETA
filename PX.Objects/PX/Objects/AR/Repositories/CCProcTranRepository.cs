// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.CCProcTranRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class CCProcTranRepository
{
  protected readonly PXGraph _graph;

  public CCProcTranRepository(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public CCProcTran InsertCCProcTran(CCProcTran transaction)
  {
    return (CCProcTran) this._graph.Caches[typeof (CCProcTran)].Insert((object) transaction);
  }

  public CCProcTran UpdateCCProcTran(CCProcTran transaction)
  {
    return (CCProcTran) this._graph.Caches[typeof (CCProcTran)].Update((object) transaction);
  }

  public CCProcTran FindVerifyingCCProcTran(int? pMInstanceID)
  {
    return PXResultset<CCProcTran>.op_Implicit(PXSelectBase<CCProcTran, PXSelect<CCProcTran, Where<CCProcTran.pMInstanceID, Equal<Required<CCProcTran.pMInstanceID>>, And<CCProcTran.procStatus, Equal<CCProcStatus.finalized>, And<CCProcTran.tranStatus, Equal<CCTranStatusCode.approved>, And<CCProcTran.cVVVerificationStatus, Equal<CVVVerificationStatusCode.match>>>>>, OrderBy<Desc<CCProcTran.tranNbr>>>.Config>.Select(this._graph, new object[1]
    {
      (object) pMInstanceID
    }));
  }

  public IEnumerable<CCProcTran> GetCCProcTranByTranID(int? transactionId)
  {
    return GraphHelper.RowCast<CCProcTran>((IEnumerable) PXSelectBase<CCProcTran, PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>, OrderBy<Asc<CCProcTran.tranNbr>>>.Config>.Select(this._graph, new object[1]
    {
      (object) transactionId
    }));
  }
}
