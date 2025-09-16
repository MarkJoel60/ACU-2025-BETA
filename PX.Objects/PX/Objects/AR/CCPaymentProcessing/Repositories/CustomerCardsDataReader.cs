// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CustomerCardsDataReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CustomerCardsDataReader
{
  private PXGraph _graph;
  private int? _customerID;
  private string _processingCenterId;
  private Func<PXGraph, int?, ICreditCardDataReader> _readerFactory;

  public CustomerCardsDataReader(
    PXGraph graph,
    int? customerId,
    string processingCenterId,
    Func<PXGraph, int?, ICreditCardDataReader> readerFactory)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (!customerId.HasValue)
      throw new ArgumentNullException(nameof (customerId));
    if (string.IsNullOrEmpty(processingCenterId))
      throw new ArgumentNullException(nameof (processingCenterId));
    if (readerFactory == null)
      throw new ArgumentNullException(nameof (readerFactory));
    this._graph = graph;
    this._customerID = customerId;
    this._processingCenterId = processingCenterId;
    this._readerFactory = readerFactory;
  }

  private IEnumerable<int?> GetPMIntances()
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    return (IEnumerable<int?>) ((IQueryable<PXResult<CustomerPaymentMethod>>) PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>, And<CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>>>>.Config>.Select(this._graph, new object[2]
    {
      (object) this._customerID,
      (object) this._processingCenterId
    })).Select<PXResult<CustomerPaymentMethod>, int?>(Expression.Lambda<Func<PXResult<CustomerPaymentMethod>, int?>>((Expression) Expression.Property((Expression) Expression.Call(detail, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CustomerPaymentMethod.get_PMInstanceID))), parameterExpression));
  }

  public IEnumerable<ICreditCardDataReader> GetCardReaders()
  {
    return this.GetPMIntances().Select<int?, ICreditCardDataReader>((Func<int?, ICreditCardDataReader>) (id => this._readerFactory(this._graph, id)));
  }
}
