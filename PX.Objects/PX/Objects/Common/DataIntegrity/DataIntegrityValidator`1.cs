// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DataIntegrity.DataIntegrityValidator`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.DataIntegrity;

public class DataIntegrityValidator<TRegister> where TRegister : PX.Objects.CM.IRegister, IBalance
{
  private PXSelectBase<PX.Objects.GL.GLTran> _selectGLTran;
  private PXGraph _graph;
  private PXCache _docCache;
  private TRegister _doc;
  private string _module;
  private int? _referenceID;
  private bool? _released;
  private string _inconsistencyHandlingMode;
  private ICollection<DataIntegrityValidator<TRegister>.InconsistencyError> _errors;

  public DataIntegrityValidator(
    PXGraph graph,
    PXCache docCache,
    TRegister doc,
    string module,
    int? referenceID,
    bool? released,
    string inconsistencyHandlingMode)
  {
    this._graph = graph;
    this._docCache = docCache;
    this._doc = doc;
    this._module = module;
    this._referenceID = referenceID;
    this._released = released;
    this._inconsistencyHandlingMode = inconsistencyHandlingMode;
    this._errors = (ICollection<DataIntegrityValidator<TRegister>.InconsistencyError>) new List<DataIntegrityValidator<TRegister>.InconsistencyError>();
    this._selectGLTran = (PXSelectBase<PX.Objects.GL.GLTran>) new PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>, And<PX.Objects.GL.GLTran.tranType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<PX.Objects.GL.GLTran.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>, And<PX.Objects.GL.GLTran.referenceID, Equal<Required<PX.Objects.GL.GLTran.referenceID>>>>>>>(graph);
  }

  public void Commit()
  {
    if (this._inconsistencyHandlingMode == "N" || !this._errors.Any<DataIntegrityValidator<TRegister>.InconsistencyError>())
      return;
    foreach (DataIntegrityValidator<TRegister>.InconsistencyError error in (IEnumerable<DataIntegrityValidator<TRegister>.InconsistencyError>) this._errors)
    {
      foreach (DataIntegrityValidator<TRegister>.RecordContextInfo recordContextInfo in error.ContextData)
        PXTrace.WriteInformation(recordContextInfo.Cache.GetFullDescription(recordContextInfo.Record));
      PXTrace.WriteInformation($"{error.ErrorCode} {((Exception) error.IntegrityException).Message}");
    }
    if (this._inconsistencyHandlingMode == "P")
    {
      DataIntegrityException integrityException = this._errors.First<DataIntegrityValidator<TRegister>.InconsistencyError>().IntegrityException;
      throw new DataIntegrityException(integrityException.InconsistencyCode, "An error occurred during record processing: {0} Your changes cannot be saved. Please copy the error details from Help > Trace, and contact Support service.", new object[1]
      {
        (object) ((Exception) integrityException).Message
      });
    }
    if (!(this._inconsistencyHandlingMode == "L"))
      return;
    DateTime utcNow = DateTime.UtcNow;
    foreach (DataIntegrityValidator<TRegister>.InconsistencyError error in (IEnumerable<DataIntegrityValidator<TRegister>.InconsistencyError>) this._errors)
    {
      string str = error.ContextData.Any<DataIntegrityValidator<TRegister>.RecordContextInfo>() ? string.Join("\r\n", error.ContextData.Select<DataIntegrityValidator<TRegister>.RecordContextInfo, string>((Func<DataIntegrityValidator<TRegister>.RecordContextInfo, string>) (contextInfo => contextInfo.Cache.ToXml(contextInfo.Record)))) : string.Empty;
      PXTrace.WriteError($"Error message: {((Exception) error.IntegrityException).Message}; Date: {DateTime.Now}; Screen: {this._graph.Accessinfo.ScreenID}; Context: {str}; InconsistencyCode: {error.IntegrityException.InconsistencyCode}");
    }
  }

  private bool IsSkipCheck(bool checkLevelDisableFlag)
  {
    return checkLevelDisableFlag || this._inconsistencyHandlingMode == "N";
  }

  /// <summary>
  ///  Finding inconsistency between GL module and document.
  ///  Run this method at start point of the "Release" process.
  ///  Validating case:
  ///  Unreleased document shouldn't have GL Batch before/after the "Release" process.
  /// </summary>
  public DataIntegrityValidator<TRegister> CheckTransactionsExistenceForUnreleasedDocument(
    bool disableCheck = false)
  {
    if (this.IsSkipCheck(disableCheck) || this._released.GetValueOrDefault())
      return this;
    PXSelectBase<PX.Objects.GL.GLTran> selectGlTran = this._selectGLTran;
    object[] objArray = new object[4]
    {
      (object) this._module,
      (object) this._doc.DocType,
      (object) this._doc.RefNbr,
      (object) this._referenceID
    };
    PX.Objects.GL.GLTran record;
    if ((record = selectGlTran.SelectSingle(objArray)) != null)
      this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.unreleasedDocumentHasGlTransactions>(new DataIntegrityValidator<TRegister>.RecordContextInfo[2]
      {
        new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc),
        new DataIntegrityValidator<TRegister>.RecordContextInfo(((PXSelectBase) this._selectGLTran).Cache, (object) record)
      }));
    return this;
  }

  /// <summary>
  ///  Finding inconsistency between GL Batch and its transactions.
  ///  Run this method at end point of the "Release" process.
  ///  Validating case:
  ///  The document should always have GL Batch after the "Release" process.
  /// </summary>
  public DataIntegrityValidator<TRegister> CheckTransactionsExistenceForReleasedDocument(
    bool disableCheck = false)
  {
    if (this.IsSkipCheck(disableCheck) || !this._released.GetValueOrDefault())
      return this;
    PXSelectBase<PX.Objects.GL.GLTran> selectGlTran = this._selectGLTran;
    object[] objArray = new object[4]
    {
      (object) this._module,
      (object) this._doc.DocType,
      (object) this._doc.RefNbr,
      (object) this._referenceID
    };
    PX.Objects.GL.GLTran record;
    if ((record = selectGlTran.SelectSingle(objArray)) == null)
      this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.releasedDocumentHasNoGlTransactions>(new DataIntegrityValidator<TRegister>.RecordContextInfo[2]
      {
        new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc),
        new DataIntegrityValidator<TRegister>.RecordContextInfo(((PXSelectBase) this._selectGLTran).Cache, (object) record)
      }));
    return this;
  }

  /// <summary>
  ///  Finding inconsistency between GL Batch and its transactions.
  ///  Run this method at end point of the "Release" process.
  ///  Validating case:
  ///  Debit and credit sums for GL Batch and its transactions should always
  ///  be the same after the "Release" process.
  /// </summary>
  public DataIntegrityValidator<TRegister> CheckBatchAndTransactionsSumsForDocument(
    bool disableCheck = false)
  {
    if (this.IsSkipCheck(disableCheck))
      return this;
    PXSelectBase<Batch> pxSelectBase = (PXSelectBase<Batch>) new PXSelectJoin<Batch, InnerJoin<PX.Objects.GL.GLTran, On<PX.Objects.GL.GLTran.module, Equal<Batch.module>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Batch.batchNbr>>>>, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>(this._graph);
    foreach (string str in ((IEnumerable<PXResult<PX.Objects.GL.GLTran>>) this._selectGLTran.Select(new object[4]
    {
      (object) this._module,
      (object) this._doc.DocType,
      (object) this._doc.RefNbr,
      (object) this._referenceID
    })).AsEnumerable<PXResult<PX.Objects.GL.GLTran>>().Select<PXResult<PX.Objects.GL.GLTran>, PX.Objects.GL.GLTran>((Func<PXResult<PX.Objects.GL.GLTran>, PX.Objects.GL.GLTran>) (r => PXResult<PX.Objects.GL.GLTran>.op_Implicit(r))).Where<PX.Objects.GL.GLTran>((Func<PX.Objects.GL.GLTran, bool>) (tran => !tran.Posted.GetValueOrDefault())).GroupBy<PX.Objects.GL.GLTran, string>((Func<PX.Objects.GL.GLTran, string>) (tran => tran.BatchNbr)).Select<IGrouping<string, PX.Objects.GL.GLTran>, string>((Func<IGrouping<string, PX.Objects.GL.GLTran>, string>) (group => group.Key)))
    {
      List<PXResult<Batch>> list = ((IEnumerable<PXResult<Batch>>) pxSelectBase.Select(new object[2]
      {
        (object) this._module,
        (object) str
      })).ToList<PXResult<Batch>>();
      Batch record = PXResult<Batch>.op_Implicit(list.FirstOrDefault<PXResult<Batch>>());
      var data = list.Cast<PXResult<Batch, PX.Objects.GL.GLTran>>().Select(item => new
      {
        DebitTotal = PXResult<Batch, PX.Objects.GL.GLTran>.op_Implicit(item).DebitAmt,
        CreditTotal = PXResult<Batch, PX.Objects.GL.GLTran>.op_Implicit(item).CreditAmt,
        CuryDebitTotal = PXResult<Batch, PX.Objects.GL.GLTran>.op_Implicit(item).CuryDebitAmt,
        CuryCreditTotal = PXResult<Batch, PX.Objects.GL.GLTran>.op_Implicit(item).CuryCreditAmt
      }).Aggregate((prev, next) =>
      {
        Decimal? debitTotal = prev.DebitTotal;
        Decimal? nullable1 = next.DebitTotal;
        Decimal? nullable2 = debitTotal.HasValue & nullable1.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        nullable1 = prev.CreditTotal;
        Decimal? nullable3 = next.CreditTotal;
        Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        nullable3 = prev.CuryDebitTotal;
        nullable1 = next.CuryDebitTotal;
        Decimal? nullable5 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        nullable1 = prev.CuryCreditTotal;
        nullable3 = next.CuryCreditTotal;
        Decimal? nullable6 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        return new
        {
          DebitTotal = nullable2,
          CreditTotal = nullable4,
          CuryDebitTotal = nullable5,
          CuryCreditTotal = nullable6
        };
      });
      Decimal? nullable7 = record.DebitTotal;
      Decimal? nullable8 = data.DebitTotal;
      if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
      {
        nullable8 = record.CreditTotal;
        nullable7 = data.CreditTotal;
        if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
        {
          nullable7 = record.CuryDebitTotal;
          nullable8 = data.CuryDebitTotal;
          if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
          {
            nullable8 = record.CuryCreditTotal;
            nullable7 = data.CuryCreditTotal;
            if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
              continue;
          }
        }
      }
      this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.batchTotalNotEqualToTransactionTotal>(new DataIntegrityValidator<TRegister>.RecordContextInfo[2]
      {
        new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc),
        new DataIntegrityValidator<TRegister>.RecordContextInfo(((PXSelectBase) pxSelectBase).Cache, (object) record)
      }));
    }
    return this;
  }

  /// <summary>
  ///  Finding inconsistency between document and its applications.
  ///  Run this method at end point of the "Release" process.
  ///  Validating case:
  ///  Released flag for document and its applications should always
  ///  be the same after the "Release" process.
  /// </summary>
  public DataIntegrityValidator<TRegister> CheckApplicationsReleasedForDocument<TAdjust, TAdjgDocType, TAdjgRefNbr, TReleased>(
    bool disableCheck = false)
    where TAdjust : class, IBqlTable, new()
    where TAdjgDocType : IBqlField
    where TAdjgRefNbr : IBqlField
    where TReleased : IBqlField
  {
    if (this.IsSkipCheck(disableCheck))
      return this;
    PXSelectBase<TAdjust> pxSelectBase = (PXSelectBase<TAdjust>) new PXSelect<TAdjust, Where<TAdjgDocType, Equal<Required<TAdjgDocType>>, And<TAdjgRefNbr, Equal<Required<TAdjgRefNbr>>, And<TReleased, NotEqual<Required<TReleased>>>>>>(this._graph);
    TAdjust record = pxSelectBase.SelectSingle(new object[3]
    {
      (object) this._doc.DocType,
      (object) this._doc.RefNbr,
      (object) this._released
    });
    if ((object) record != null)
    {
      PXTrace.WriteInformation(this._docCache.GetFullDescription((object) this._doc));
      PXTrace.WriteInformation(((PXSelectBase) pxSelectBase).Cache.GetFullDescription((object) record));
      if (this._released.GetValueOrDefault())
        this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.releasedDocumentHasUnreleasedApplications>(new DataIntegrityValidator<TRegister>.RecordContextInfo[2]
        {
          new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc),
          new DataIntegrityValidator<TRegister>.RecordContextInfo(((PXSelectBase) pxSelectBase).Cache, (object) record)
        }));
      else
        this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.unreleasedDocumentHasReleasedApplications>(new DataIntegrityValidator<TRegister>.RecordContextInfo[2]
        {
          new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc),
          new DataIntegrityValidator<TRegister>.RecordContextInfo(((PXSelectBase) pxSelectBase).Cache, (object) record)
        }));
    }
    return this;
  }

  public DataIntegrityValidator<TRegister> CheckDocumentHasNonNegativeBalance(bool disableCheck = false)
  {
    if (!this.IsSkipCheck(disableCheck))
    {
      bool? nullable = ARDocType.HasNegativeAmount(this._doc.DocType);
      if (!nullable.GetValueOrDefault())
      {
        nullable = APDocType.HasNegativeAmount(this._doc.DocType);
        if (!nullable.GetValueOrDefault())
        {
          Decimal? docBal = this._doc.DocBal;
          Decimal num1 = 0M;
          if (!(docBal.GetValueOrDefault() < num1 & docBal.HasValue))
          {
            Decimal? curyDocBal = this._doc.CuryDocBal;
            Decimal num2 = 0M;
            if (!(curyDocBal.GetValueOrDefault() < num2 & curyDocBal.HasValue))
              goto label_7;
          }
          this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.documentNegativeBalance>(new DataIntegrityValidator<TRegister>.RecordContextInfo[1]
          {
            new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc)
          }));
label_7:
          return this;
        }
      }
    }
    return this;
  }

  public DataIntegrityValidator<TRegister> CheckDocumentTotalsConformToCurrencyPrecision(
    bool disableCheck = false)
  {
    if (this.IsSkipCheck(disableCheck))
      return this;
    PX.Objects.CM.Currency currency1 = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) PXResultset<Company>.op_Implicit(PXSetup<Company>.Select(this._graph, Array.Empty<object>())).BaseCuryID
    }));
    PX.Objects.CM.Currency currency2;
    if (!(this._doc.CuryID == currency1.CuryID))
      currency2 = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select(this._graph, new object[1]
      {
        (object) this._doc.CuryID
      }));
    else
      currency2 = currency1;
    short? decimalPlaces = currency1.DecimalPlaces;
    short valueOrDefault1 = decimalPlaces.GetValueOrDefault();
    decimalPlaces = currency2.DecimalPlaces;
    short valueOrDefault2 = decimalPlaces.GetValueOrDefault();
    Decimal valueOrDefault3 = this._doc.DocBal.GetValueOrDefault();
    Decimal? nullable = this._doc.CuryDocBal;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    nullable = this._doc.OrigDocAmt;
    Decimal valueOrDefault5 = nullable.GetValueOrDefault();
    nullable = this._doc.CuryOrigDocAmt;
    Decimal valueOrDefault6 = nullable.GetValueOrDefault();
    if (valueOrDefault3 != Math.Round(valueOrDefault3, (int) valueOrDefault1) || valueOrDefault5 != Math.Round(valueOrDefault5, (int) valueOrDefault1) || valueOrDefault4 != Math.Round(valueOrDefault4, (int) valueOrDefault2) || valueOrDefault6 != Math.Round(valueOrDefault6, (int) valueOrDefault2))
      this._errors.Add((DataIntegrityValidator<TRegister>.InconsistencyError) new DataIntegrityValidator<TRegister>.InconsistencyError<InconsistencyCode.documentTotalsWrongPrecision>(new DataIntegrityValidator<TRegister>.RecordContextInfo[1]
      {
        new DataIntegrityValidator<TRegister>.RecordContextInfo(this._docCache, (object) this._doc)
      }));
    return this;
  }

  private class RecordContextInfo(PXCache cache, object record) : Tuple<PXCache, object>(cache, record)
  {
    public PXCache Cache => this.Item1;

    public object Record => this.Item2;

    public string FullDescription => this.Cache.GetFullDescription(this.Record);
  }

  private class InconsistencyError(
    DataIntegrityException exception,
    IEnumerable<DataIntegrityValidator<TRegister>.RecordContextInfo> contextData) : 
    Tuple<DataIntegrityException, IEnumerable<DataIntegrityValidator<TRegister>.RecordContextInfo>>(exception, contextData)
  {
    public DataIntegrityException IntegrityException => this.Item1;

    public IEnumerable<DataIntegrityValidator<TRegister>.RecordContextInfo> ContextData
    {
      get => this.Item2;
    }

    public string ErrorCode => this.IntegrityException.InconsistencyCode;
  }

  private class InconsistencyError<TInconsistency>(
    params DataIntegrityValidator<TRegister>.RecordContextInfo[] contextData) : 
    DataIntegrityValidator<TRegister>.InconsistencyError(new DataIntegrityException(new TInconsistency().Value, GetLabel.For<TInconsistency>()), (IEnumerable<DataIntegrityValidator<TRegister>.RecordContextInfo>) contextData)
    where TInconsistency : IConstant<string>, IBqlOperand, new()
  {
  }
}
