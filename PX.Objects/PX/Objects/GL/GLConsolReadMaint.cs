// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolReadMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Logging;
using PX.Objects.CS;
using PX.Objects.GL.Consolidation;
using PX.Objects.GL.ConsolidationImport;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class GLConsolReadMaint : PXGraph<GLConsolReadMaint>
{
  public PXCancel<GLConsolSetup> Cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<GLConsolSetup, Where<GLConsolSetup.isActive, Equal<boolTrue>>> ConsolSetupRecords;
  protected PXSelect<Segment, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>>> SubaccountSegmentsView;
  public List<GLConsolRead> listConsolRead = new List<GLConsolRead>();
  protected GLConsolSetup consolSetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXException exception;

  protected virtual IImportSubaccountMapper CreateImportSubaccountMapper(GLConsolSetup glConsolSetup)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? (IImportSubaccountMapper) new ImportSubaccountMapper((IReadOnlyCollection<Segment>) GraphHelper.RowCast<Segment>((IEnumerable) ((PXSelectBase<Segment>) this.SubaccountSegmentsView).Select(Array.Empty<object>())).ToArray<Segment>(), ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current, glConsolSetup, (Func<string, int?>) (subCD => Sub.UK.Find((PXGraph) this, subCD)?.SubID), (IAppLogger) new AppLogger()) : (IImportSubaccountMapper) new SubOffImportSubaccountMapper(new Func<int?>(SubAccountAttribute.TryGetDefaultSubID));
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public GLConsolReadMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    PXCache cache = ((PXSelectBase) this.ConsolSetupRecords).Cache;
    this.SubaccountSegmentsView = new PXSelect<Segment, Where<Segment.dimensionID, Equal<SubAccountAttribute.dimensionName>>>((PXGraph) this);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.description>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.lastConsDate>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.lastPostPeriod>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.ledgerId>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.login>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.password>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.pasteFlag>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.segmentValue>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.url>(cache, (object) null, false);
    cache.AllowInsert = false;
    cache.AllowDelete = false;
    // ISSUE: method pointer
    ((PXProcessingBase<GLConsolSetup>) this.ConsolSetupRecords).SetProcessDelegate<GLConsolReadMaint>(new PXProcessingBase<GLConsolSetup>.ProcessItemDelegate<GLConsolReadMaint>((object) null, __methodptr(ProcessConsolidationRead)));
    ((PXProcessing<GLConsolSetup>) this.ConsolSetupRecords).SetProcessAllVisible(false);
  }

  protected static void ProcessConsolidationRead(GLConsolReadMaint processor, GLConsolSetup item)
  {
    ((PXGraph) processor).Clear();
    processor.listConsolRead.Clear();
    int num = processor.ConsolidationRead(item);
    if (num > 0)
      throw new PXSetPropertyException("{0} records processed successfully.", (PXErrorLevel) 1, new object[1]
      {
        (object) num
      });
    throw new PXSetPropertyException("There are no records to process.", (PXErrorLevel) 1);
  }

  private GLConsolSetup DecryptRemoteUserPassword(GLConsolSetup item)
  {
    GLConsolSetup glConsolSetup = item;
    PXCache cach = ((PXGraph) this).Caches[typeof (GLConsolSetup)];
    PXDBCryptStringAttribute.SetDecrypted<GLConsolSetup.password>(cach, true);
    glConsolSetup.Password = cach.GetValueExt<GLConsolSetup.password>((object) glConsolSetup).ToString();
    PXDBCryptStringAttribute.SetDecrypted<GLConsolSetup.password>(cach, false);
    return glConsolSetup;
  }

  protected virtual int ConsolidationRead(GLConsolSetup item)
  {
    int num1 = 0;
    string periodId = (string) null;
    int? ledgerId = item.LedgerId;
    int? branchId = item.BranchID;
    IImportSubaccountMapper subaccountMapper = this.CreateImportSubaccountMapper(item);
    ((IEnumerable<PXResult<GLConsolHistory>>) PXSelectBase<GLConsolHistory, PXSelect<GLConsolHistory, Where<GLConsolHistory.setupID, Equal<Required<GLConsolHistory.setupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) item.SetupID
    })).ToList<PXResult<GLConsolHistory>>();
    Func<Decimal?, Decimal> delegateForLedger = this.GetRoundDelegateForLedger(ledgerId);
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    if (item.BypassAccountSubValidation.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXGraph) instance).FieldVerifying.AddHandler<GLTran.accountID>(GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9__13_0 ?? (GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9__13_0 = new PXFieldVerifying((object) GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CConsolidationRead\u003Eb__13_0))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXGraph) instance).FieldVerifying.AddHandler<GLTran.subID>(GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9__13_1 ?? (GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9__13_1 = new PXFieldVerifying((object) GLConsolReadMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CConsolidationRead\u003Eb__13_1))));
    }
    this.consolSetup = this.DecryptRemoteUserPassword(item);
    using (ConsolidationClient scope = new ConsolidationClient(this.consolSetup.Url, this.consolSetup.Login, this.consolSetup.Password, this.consolSetup.HttpClientTimeout))
    {
      Task<IEnumerable<ConsolidationItemAPI>> task;
      try
      {
        task = System.Threading.Tasks.Task.Run<IEnumerable<ConsolidationItemAPI>>((Func<Task<IEnumerable<ConsolidationItemAPI>>>) (() => scope.GetConsolidationData(item.SourceLedgerCD, item.SourceBranchCD)));
        task.Wait();
      }
      catch (Exception ex)
      {
        throw new PXException(ConsolidationClient.GetServerError(ex));
      }
      IEnumerable<GLConsolData> glConsolDatas = task.Result.Select<ConsolidationItemAPI, GLConsolData>((Func<ConsolidationItemAPI, GLConsolData>) (_ => new GLConsolData()
      {
        AccountCD = _.AccountCD,
        ConsolAmtCredit = _.ConsolAmtCredit,
        ConsolAmtDebit = _.ConsolAmtDebit,
        FinPeriodID = _.FinPeriodID,
        MappedValue = _.MappedValue,
        MappedValueLength = _.MappedValueLength
      }));
      int result1 = 0;
      if (!string.IsNullOrEmpty(item.StartPeriod))
        int.TryParse(item.StartPeriod, out result1);
      int result2 = 0;
      if (!string.IsNullOrEmpty(item.EndPeriod))
        int.TryParse(item.EndPeriod, out result2);
      foreach (GLConsolData data in glConsolDatas)
      {
        int result3;
        if (result1 <= 0 && result2 <= 0 || string.IsNullOrEmpty(data.FinPeriodID) || !int.TryParse(data.FinPeriodID, out result3) || (result1 <= 0 || result3 >= result1) && (result2 <= 0 || result3 <= result2))
        {
          if (periodId == null)
            periodId = data.FinPeriodID;
          else if (periodId != data.FinPeriodID)
          {
            if (this.listConsolRead.Count > 0)
            {
              num1 += this.AppendRemapped(periodId, ledgerId, branchId, item.SetupID, delegateForLedger);
              this.CreateBatch(instance, periodId, ledgerId, branchId, item);
            }
            periodId = data.FinPeriodID;
            this.listConsolRead.Clear();
          }
          GLConsolRead glConsolRead = new GLConsolRead();
          Account accountByCd = AccountMaint.GetAccountByCD((PXGraph) this, data.AccountCD);
          int? accountId = accountByCd.AccountID;
          int? ytdNetIncAccountId = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.YtdNetIncAccountID;
          if (accountId.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & accountId.HasValue == ytdNetIncAccountId.HasValue)
            throw new PXException("Importing the data of the YTD Net Income account is prohibited. Make sure that this account is not specified as a consolidation account on the Chart of Accounts (GL202500) form in the consolidation unit.");
          glConsolRead.AccountCD = accountByCd.AccountCD;
          glConsolRead.AccountID = accountByCd.AccountID;
          string mappedValue = this.GetMappedValue(data);
          Sub.Keys mappedSubaccountKeys = subaccountMapper.GetMappedSubaccountKeys(mappedValue);
          glConsolRead.MappedValue = mappedSubaccountKeys.SubCD;
          glConsolRead.SubID = mappedSubaccountKeys.SubID;
          glConsolRead.FinPeriodID = data.FinPeriodID;
          GLConsolHistory glConsolHistory = (GLConsolHistory) ((PXGraph) this).Caches[typeof (GLConsolHistory)].Locate((object) new GLConsolHistory()
          {
            SetupID = item.SetupID,
            FinPeriodID = glConsolRead.FinPeriodID,
            AccountID = glConsolRead.AccountID,
            SubID = glConsolRead.SubID,
            LedgerID = item.LedgerId,
            BranchID = item.BranchID
          });
          if (glConsolHistory != null)
          {
            glConsolRead.ConsolAmtCredit = new Decimal?(delegateForLedger(data.ConsolAmtCredit) - delegateForLedger(glConsolHistory.PtdCredit));
            glConsolRead.ConsolAmtDebit = new Decimal?(delegateForLedger(data.ConsolAmtDebit) - delegateForLedger(glConsolHistory.PtdDebit));
            glConsolHistory.PtdCredit = new Decimal?(0M);
            glConsolHistory.PtdDebit = new Decimal?(0M);
          }
          else
          {
            glConsolRead.ConsolAmtCredit = new Decimal?(delegateForLedger(data.ConsolAmtCredit));
            glConsolRead.ConsolAmtDebit = new Decimal?(delegateForLedger(data.ConsolAmtDebit));
          }
          Decimal? consolAmtCredit = glConsolRead.ConsolAmtCredit;
          Decimal num2 = 0M;
          if (consolAmtCredit.GetValueOrDefault() == num2 & consolAmtCredit.HasValue)
          {
            Decimal? consolAmtDebit = glConsolRead.ConsolAmtDebit;
            Decimal num3 = 0M;
            if (consolAmtDebit.GetValueOrDefault() == num3 & consolAmtDebit.HasValue)
              continue;
          }
          this.listConsolRead.Add(glConsolRead);
          ++num1;
        }
      }
    }
    if (this.listConsolRead.Count > 0)
    {
      num1 += this.AppendRemapped(periodId, ledgerId, branchId, item.SetupID, delegateForLedger);
      this.CreateBatch(instance, periodId, ledgerId, branchId, item);
    }
    if (this.exception != null)
    {
      PXException exception = this.exception;
      this.exception = (PXException) null;
      throw exception;
    }
    return num1;
  }

  public int AppendRemapped(
    string periodId,
    int? ledgerID,
    int? branchID,
    int? setupID,
    Func<Decimal?, Decimal> roundFunc)
  {
    int num = 0;
    foreach (GLConsolHistory glConsolHistory in ((PXGraph) this).Caches[typeof (GLConsolHistory)].Cached)
    {
      int? nullable1 = glConsolHistory.SetupID;
      int? nullable2 = setupID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && glConsolHistory.FinPeriodID == periodId)
      {
        nullable2 = glConsolHistory.LedgerID;
        nullable1 = ledgerID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = glConsolHistory.BranchID;
          nullable2 = branchID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && (roundFunc(glConsolHistory.PtdCredit) != 0M || roundFunc(glConsolHistory.PtdDebit) != 0M))
          {
            GLConsolRead glConsolRead = new GLConsolRead();
            glConsolRead.AccountID = glConsolHistory.AccountID;
            glConsolRead.SubID = glConsolHistory.SubID;
            glConsolRead.FinPeriodID = glConsolHistory.FinPeriodID;
            glConsolRead.ConsolAmtCredit = new Decimal?(-roundFunc(glConsolHistory.PtdCredit));
            glConsolRead.ConsolAmtDebit = new Decimal?(-roundFunc(glConsolHistory.PtdDebit));
            this.listConsolRead.Add(glConsolRead);
            ++num;
          }
        }
      }
    }
    return num;
  }

  public void CreateBatch(
    JournalEntry je,
    string periodId,
    int? ledgerID,
    int? branchID,
    GLConsolSetup item)
  {
    ((PXGraph) je).Clear();
    ((PXSelectBase<PX.Objects.GL.GLSetup>) je.glsetup).Current.RequireControlTotal = new bool?(false);
    Ledger ledger = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledgerID
    }));
    FinPeriod finPeriod = this.FinPeriodRepository.FindByID(PXAccess.GetParentOrganizationID(branchID), periodId);
    if (finPeriod == null)
      throw new FiscalPeriodInvalidException(periodId);
    AccessInfo accessinfo = ((PXGraph) je).Accessinfo;
    DateTime? endDate = finPeriod.EndDate;
    DateTime dateTime = endDate.Value;
    DateTime? nullable1 = new DateTime?(dateTime.AddDays(-1.0));
    accessinfo.BusinessDate = nullable1;
    PX.Objects.CM.CurrencyInfo info = new PX.Objects.CM.CurrencyInfo();
    info.CuryID = ledger.BaseCuryID;
    PX.Objects.CM.CurrencyInfo currencyInfo = info;
    endDate = finPeriod.EndDate;
    dateTime = endDate.Value;
    DateTime? nullable2 = new DateTime?(dateTime.AddDays(-1.0));
    currencyInfo.CuryEffDate = nullable2;
    info.CuryRate = new Decimal?(1M);
    info = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(info);
    Batch batch1 = new Batch()
    {
      BranchID = branchID,
      LedgerID = ledgerID,
      Module = "GL",
      Released = new bool?(false),
      CuryID = ledger.BaseCuryID,
      CuryInfoID = info.CuryInfoID,
      FinPeriodID = periodId
    };
    batch1.CuryID = ledger.BaseCuryID;
    batch1.BatchType = "C";
    batch1.Description = PXMessages.LocalizeFormatNoPrefix("Consolidation created from '{0}'.", new object[1]
    {
      (object) item.Description
    });
    Batch batch2 = ((PXSelectBase<Batch>) je.BatchModule).Insert(batch1);
    foreach (GLConsolRead glConsolRead in this.listConsolRead)
    {
      GLConsolRead read = glConsolRead;
      Action<Decimal?, Decimal?> action = (Action<Decimal?, Decimal?>) ((debitAmt, creditAmt) =>
      {
        GLTran glTran = ((PXSelectBase<GLTran>) je.GLTranModuleBatNbr).Insert(new GLTran()
        {
          AccountID = read.AccountID,
          SubID = read.SubID,
          CuryInfoID = info.CuryInfoID,
          CuryCreditAmt = creditAmt,
          CuryDebitAmt = debitAmt,
          CreditAmt = creditAmt,
          DebitAmt = debitAmt,
          TranType = "CON",
          TranClass = "C",
          TranDate = new DateTime?(finPeriod.EndDate.Value.AddDays(-1.0)),
          TranDesc = "Consolidation detail",
          FinPeriodID = periodId,
          RefNbr = "",
          ProjectID = ProjectDefaultAttribute.NonProject()
        });
        int? nullable3;
        if (glTran != null)
        {
          nullable3 = glTran.SubID;
          if (!nullable3.HasValue && read.MappedValue != null)
            ((PXSelectBase<GLTran>) je.GLTranModuleBatNbr).SetValueExt<GLTran.subID>(glTran, (object) read.MappedValue);
        }
        if (glTran != null)
        {
          nullable3 = glTran.AccountID;
          if (nullable3.HasValue)
          {
            nullable3 = glTran.SubID;
            if (nullable3.HasValue)
              return;
          }
        }
        throw new PXException("Either Account ID '{0}' or Sub. ID '{1}' specified is invalid.", new object[2]
        {
          (object) read.AccountCD,
          (object) read.MappedValue
        });
      });
      Decimal? nullable4 = read.ConsolAmtDebit;
      if (Math.Abs(nullable4.Value) > 0M)
        action(read.ConsolAmtDebit, new Decimal?(0M));
      nullable4 = read.ConsolAmtCredit;
      if (Math.Abs(nullable4.Value) > 0M)
        action(new Decimal?(0M), read.ConsolAmtCredit);
    }
    batch2.Hold = new bool?(false);
    ((PXSelectBase<Batch>) je.BatchModule).Update(batch2);
    item.LastPostPeriod = finPeriod.FinPeriodID;
    item.LastConsDate = new DateTime?(DateTime.Now);
    ((PXGraph) je).Caches[typeof (GLConsolSetup)].Update((object) item);
    if (!((PXGraph) je).Views.Caches.Contains(typeof (GLConsolSetup)))
      ((PXGraph) je).Views.Caches.Add(typeof (GLConsolSetup));
    ((PXGraph) je).Caches[typeof (GLConsolBatch)].Insert((object) new GLConsolBatch()
    {
      SetupID = item.SetupID
    });
    if (!((PXGraph) je).Views.Caches.Contains(typeof (GLConsolBatch)))
      ((PXGraph) je).Views.Caches.Add(typeof (GLConsolBatch));
    try
    {
      ((PXAction) je.Save).Press();
    }
    catch (PXException ex)
    {
      try
      {
        if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<Batch.curyCreditTotal>(((PXSelectBase) je.BatchModule).Cache, (object) ((PXSelectBase<Batch>) je.BatchModule).Current)) || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<Batch.curyDebitTotal>(((PXSelectBase) je.BatchModule).Cache, (object) ((PXSelectBase<Batch>) je.BatchModule).Current)))
        {
          ((PXSelectBase<Batch>) je.BatchModule).Current.Hold = new bool?(true);
          ((PXSelectBase<Batch>) je.BatchModule).Update(((PXSelectBase<Batch>) je.BatchModule).Current);
        }
        ((PXAction) je.Save).Press();
        if (this.exception == null)
          this.exception = new PXException("Consolidation GL Batch number {0} is out of balance, please review.", new object[1]
          {
            (object) ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr
          });
        else
          this.exception = new PXException(((Exception) this.exception).Message + "Consolidation GL Batch number {0} is out of balance, please review.", new object[1]
          {
            (object) ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr
          });
      }
      catch
      {
        throw ex;
      }
    }
  }

  protected virtual void GLConsolSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || PXLongOperation.Exists(((PXGraph) this).UID))
      return;
    this.CheckUnpostedBatchesNotExist((GLConsolSetup) e.Row);
  }

  private void CheckUnpostedBatchesNotExist(GLConsolSetup glConsolSetup)
  {
    PXSelectJoin<Batch, InnerJoin<GLConsolBatch, On<Batch.batchNbr, Equal<GLConsolBatch.batchNbr>>>, Where<GLConsolBatch.setupID, Equal<Required<GLConsolBatch.setupID>>, And<Batch.posted, Equal<False>, And<Batch.module, Equal<BatchModule.moduleGL>>>>> pxSelectJoin = new PXSelectJoin<Batch, InnerJoin<GLConsolBatch, On<Batch.batchNbr, Equal<GLConsolBatch.batchNbr>>>, Where<GLConsolBatch.setupID, Equal<Required<GLConsolBatch.setupID>>, And<Batch.posted, Equal<False>, And<Batch.module, Equal<BatchModule.moduleGL>>>>>((PXGraph) this);
    List<object> objectList = new List<object>()
    {
      (object) glConsolSetup.SetupID
    };
    if (glConsolSetup.StartPeriod != null)
    {
      ((PXSelectBase<Batch>) pxSelectJoin).WhereAnd<Where<Batch.finPeriodID, GreaterEqual<Required<Batch.finPeriodID>>>>();
      objectList.Add((object) glConsolSetup.StartPeriod);
    }
    if (glConsolSetup.EndPeriod != null)
    {
      ((PXSelectBase<Batch>) pxSelectJoin).WhereAnd<Where<Batch.finPeriodID, LessEqual<Required<Batch.finPeriodID>>>>();
      objectList.Add((object) glConsolSetup.EndPeriod);
    }
    if (PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) pxSelectJoin).Select(objectList.ToArray())) == null)
      return;
    ((PXSelectBase) this.ConsolSetupRecords).Cache.RaiseExceptionHandling<GLConsolSetup.selected>((object) glConsolSetup, (object) glConsolSetup.Selected, (Exception) new PXSetPropertyException("There are unposted batches in the consolidating branch/ledger generated by the previous consolidation. To proceed, release and post the batches.", (PXErrorLevel) 5));
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.selected>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) glConsolSetup, false);
  }

  public Func<Decimal?, Decimal> GetRoundDelegateForLedger(int? ledgerID)
  {
    return (Func<Decimal?, Decimal>) (value => Math.Round(value.Value, (int) (PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelectJoin<PX.Objects.CM.Currency, InnerJoin<Ledger, On<PX.Objects.CM.Currency.curyID, Equal<Ledger.baseCuryID>>>, Where<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledgerID
    })) ?? throw new PXException(PXMessages.LocalizeFormat("Currency for ledger with ID '{0}' cannot be found in the system", new object[1]
    {
      (object) ledgerID
    }))).DecimalPlaces.Value, MidpointRounding.AwayFromZero));
  }

  private string GetMappedValue(GLConsolData data)
  {
    return data.MappedValue.PadRight(data.MappedValueLength.Value, ' ');
  }
}
