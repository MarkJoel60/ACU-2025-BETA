// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Descriptor.CAOptimizeLoadDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA.Descriptor;

public class CAOptimizeLoadDateAttribute : PXFormulaAttribute
{
  private int _transactionsLimit;

  public int TransactionsLimit
  {
    get => this._transactionsLimit;
    set
    {
      this._transactionsLimit = value;
      ((CAOptimizeLoadDateAttribute.OptimizeLoadDateEvaluator) this._Formula).TransactionsLimit = value;
    }
  }

  public CAOptimizeLoadDateAttribute()
    : base(typeof (CAOptimizeLoadDateAttribute.OptimizeLoadDateEvaluator))
  {
  }

  public class OptimizeLoadDateEvaluator : BqlFormulaEvaluator<CARecon.cashAccountID>
  {
    public int TransactionsLimit;

    public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
    {
      try
      {
        return (object) this.GetLoadDocumentsTill(cache, (CARecon) item);
      }
      catch (PXException ex)
      {
        throw new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 4);
      }
      catch
      {
        return (object) null;
      }
    }

    public virtual DateTime? GetLoadDocumentsTill(PXCache cache, CARecon row)
    {
      PXGraph graph = cache.Graph;
      if (row.Reconciled.GetValueOrDefault() || !row.ShowBatchPayments.HasValue || graph == null)
        return new DateTime?();
      IEnumerable<DateTime?> list = (IEnumerable<DateTime?>) (!row.ShowBatchPayments.GetValueOrDefault() ? this.GetTransactions(graph, row).Select<PXResult<CATran>, DateTime?>((Func<PXResult<CATran>, DateTime?>) (t => PXResult<CATran>.op_Implicit(t).TranDate)) : (IEnumerable<DateTime?>) this.GetNotBatchedTransactions(graph, row).Select<PXResult<CATran>, DateTime?>((Func<PXResult<CATran>, DateTime?>) (t => PXResult<CATran>.op_Implicit(t).TranDate)).Concat<DateTime?>(this.GetBatchedTransactions(graph, row).Select<PXResult<CABatch>, DateTime?>((Func<PXResult<CABatch>, DateTime?>) (t => PXResult<CABatch>.op_Implicit(t).TranDate))).OrderBy<DateTime?, DateTime?>((Func<DateTime?, DateTime?>) (_ => _))).Take<DateTime?>(this.TransactionsLimit + 1).ToList<DateTime?>();
      DateTime? nullable1 = list.LastOrDefault<DateTime?>();
      if (list.Count<DateTime?>() <= this.TransactionsLimit)
        return row.ReconDate;
      DateTime? nullable2 = list.FirstOrDefault<DateTime?>();
      DateTime? nullable3 = list.ElementAt<DateTime?>(list.Count<DateTime?>() - 2);
      DateTime? nullable4 = nullable1;
      DateTime? nullable5 = nullable3;
      DateTime? nullable6;
      if ((nullable4.HasValue == nullable5.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        nullable6 = nullable3;
      }
      else
      {
        nullable5 = nullable1;
        nullable4 = nullable2;
        if ((nullable5.HasValue == nullable4.HasValue ? (nullable5.HasValue ? (nullable5.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          DateTime? nullable7;
          if (!nullable1.HasValue)
          {
            nullable4 = new DateTime?();
            nullable7 = nullable4;
          }
          else
            nullable7 = new DateTime?(nullable1.GetValueOrDefault().AddDays(-1.0));
          nullable6 = nullable7;
        }
        else
          nullable6 = nullable2;
      }
      nullable4 = nullable6;
      nullable5 = row.ReconDate;
      return (nullable4.HasValue & nullable5.HasValue ? (nullable4.GetValueOrDefault() > nullable5.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? nullable6 : row.ReconDate;
    }

    private IEnumerable<PXResult<CATran>> GetTransactions(PXGraph graph, CARecon row)
    {
      return ((IEnumerable<PXResult<CATran>>) PXSelectBase<CATran, PXViewOf<CATran>.BasedOn<SelectFromBase<CATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATran.cashAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATran.reconNbr, Equal<P.AsString>>>>, Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<CATran.reconNbr, IBqlString>.IsNull>>>.Order<By<BqlField<CATran.tranDate, IBqlDateTime>.Asc>>>.ReadOnly.Config>.SelectWindowed(graph, 0, this.TransactionsLimit + 1, new object[3]
      {
        (object) row.CashAccountID,
        (object) row.ReconNbr,
        (object) row.Reconciled
      })).AsEnumerable<PXResult<CATran>>();
    }

    private IEnumerable<PXResult<CABatch>> GetBatchedTransactions(PXGraph graph, CARecon row)
    {
      return ((IEnumerable<PXResult<CABatch>>) PXSelectBase<CABatch, PXViewOf<CABatch>.BasedOn<SelectFromBase<CABatch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CABatchDetail>.On<BqlOperand<CABatch.batchNbr, IBqlString>.IsEqual<CABatchDetail.batchNbr>>>, FbqlJoins.Inner<CATran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABatchDetail.origModule, Equal<CATran.origModule>>>>, And<BqlOperand<CABatchDetail.origDocType, IBqlString>.IsEqual<CATran.origTranType>>>, And<BqlOperand<CABatchDetail.origRefNbr, IBqlString>.IsEqual<CATran.origRefNbr>>>>.And<BqlOperand<CATran.isPaymentChargeTran, IBqlBool>.IsEqual<False>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABatch.cashAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABatch.reconNbr, Equal<P.AsString>>>>, Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<CABatch.reconNbr, IBqlString>.IsNull>>>.Order<By<BqlField<CABatch.tranDate, IBqlDateTime>.Asc>>>.ReadOnly.Config>.SelectWindowed(graph, 0, this.TransactionsLimit + 1, new object[3]
      {
        (object) row.CashAccountID,
        (object) row.ReconNbr,
        (object) row.Reconciled
      })).AsEnumerable<PXResult<CABatch>>();
    }

    private IEnumerable<PXResult<CATran>> GetNotBatchedTransactions(PXGraph graph, CARecon row)
    {
      return ((IEnumerable<PXResult<CATran>>) PXSelectBase<CATran, PXViewOf<CATran>.BasedOn<SelectFromBase<CATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CABatchDetail>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABatchDetail.origModule, Equal<CATran.origModule>>>>, And<BqlOperand<CABatchDetail.origDocType, IBqlString>.IsEqual<CATran.origTranType>>>, And<BqlOperand<CABatchDetail.origRefNbr, IBqlString>.IsEqual<CATran.origRefNbr>>>>.And<BqlOperand<CATran.isPaymentChargeTran, IBqlBool>.IsEqual<False>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CABatchDetail.batchNbr, IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATran.cashAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATran.reconNbr, Equal<P.AsString>>>>, Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<CATran.reconNbr, IBqlString>.IsNull>>>>.Order<By<BqlField<CATran.tranDate, IBqlDateTime>.Asc>>>.ReadOnly.Config>.SelectWindowed(graph, 0, this.TransactionsLimit + 1, new object[3]
      {
        (object) row.CashAccountID,
        (object) row.ReconNbr,
        (object) row.Reconciled
      })).AsEnumerable<PXResult<CATran>>();
    }
  }
}
