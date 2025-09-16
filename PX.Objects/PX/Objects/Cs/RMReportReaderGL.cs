// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportReaderGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Reports;
using PX.Objects.CA.Descriptor;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.CS;

public class RMReportReaderGL : PXGraphExtension<RMReportReader>
{
  public PXSetup<GLSetup> Setup;
  private int? _ytdNetIncomeAccountID;
  private List<BAccountR> _bAccounts;
  private Dictionary<int, PX.Objects.GL.Ledger> _ledgers;
  private Dictionary<string, PX.Objects.GL.Branch> _branches;
  private bool _initialized;
  private HashSet<Tuple<int, int>> _historyLoaded;
  private HashSet<Tuple<int, int, string>> _historyDrilldownLoaded;
  private HashSet<GLHistoryKeyTuple> _historySegments;
  private GLHistoryHierDict _glhistoryPeriodsNested;
  private RMReportPeriods<GLHistory> _reportPeriods;
  private RMReportRange<PX.Objects.GL.Account> _accountRangeCache;
  private RMReportRange<PX.Objects.GL.Sub> _subRangeCache;
  private RMReportRange<PX.Objects.GL.Branch> _branchRangeCache;
  private string _accountMask;
  private string _subMask;
  private static readonly IDictionary<string, RMReportReaderGL.Keys> _keysDictionary = (IDictionary<string, RMReportReaderGL.Keys>) Enum.GetValues(typeof (RMReportReaderGL.Keys)).Cast<RMReportReaderGL.Keys>().ToDictionary<RMReportReaderGL.Keys, string, RMReportReaderGL.Keys>((Func<RMReportReaderGL.Keys, string>) (e => e.ToString()), (Func<RMReportReaderGL.Keys, RMReportReaderGL.Keys>) (e => e));

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  [PXOverride]
  public void Clear(System.Action del)
  {
    del();
    this._initialized = false;
    this._historyLoaded = (HashSet<Tuple<int, int>>) null;
    this._historyDrilldownLoaded = (HashSet<Tuple<int, int, string>>) null;
    this._accountRangeCache = (RMReportRange<PX.Objects.GL.Account>) null;
    this._subRangeCache = (RMReportRange<PX.Objects.GL.Sub>) null;
    this._branchRangeCache = (RMReportRange<PX.Objects.GL.Branch>) null;
  }

  public virtual void GLEnsureInitialized()
  {
    if (this._initialized)
      return;
    this._initialized = true;
    if (this.Setup != null && ((PXSelectBase<GLSetup>) this.Setup).Current != null)
      this._ytdNetIncomeAccountID = ((PXSelectBase<GLSetup>) this.Setup).Current.YtdNetIncAccountID;
    this._reportPeriods = new RMReportPeriods<GLHistory>((PXGraph) this.Base);
    this._bAccounts = new List<BAccountR>();
    this._branches = new Dictionary<string, PX.Objects.GL.Branch>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<BAccountR, PX.Objects.GL.Branch> pxResult in ((IEnumerable<PXResult<BAccountR>>) PXSelectBase<BAccountR, PXSelectJoinOrderBy<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>, OrderBy<Asc<BAccountR.acctCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<BAccountR>>())
    {
      BAccountR baccountR = PXResult<BAccountR, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      PX.Objects.GL.Branch branch = PXResult<BAccountR, PX.Objects.GL.Branch>.op_Implicit(pxResult);
      this._bAccounts.Add(baccountR);
      this._branches.Add(RMReportWildcard.NormalizeDsValue((object) branch.BranchCD), branch);
    }
    this._ledgers = GraphHelper.RowCast<PX.Objects.GL.Ledger>((IEnumerable) PXSelectBase<PX.Objects.GL.Ledger, PXSelectOrderBy<PX.Objects.GL.Ledger, OrderBy<Asc<PX.Objects.GL.Ledger.ledgerCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToDictionary<PX.Objects.GL.Ledger, int>((Func<PX.Objects.GL.Ledger, int>) (ledger => ledger.LedgerID.GetValueOrDefault()));
    ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.Account)].Clear();
    ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.Sub)].Clear();
    ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.Branch)].Clear();
    if (((PXSelectBase<RMReport>) this.Base.Report).Current.ApplyRestrictionGroups.GetValueOrDefault())
    {
      PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<PX.Objects.GL.Account.accountCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Account>>) PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<PX.Objects.GL.Account.accountCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Account>>();
      PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<PX.Objects.GL.Sub.subCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Sub>>) PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where<Match<Current<AccessInfo.userName>>>, OrderBy<Asc<PX.Objects.GL.Sub.subCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Sub>>();
      PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where2<MatchWithBranch<PX.Objects.GL.Branch.branchID>, And<Match<Current<AccessInfo.userName>>>>, OrderBy<Asc<PX.Objects.GL.Branch.branchCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Branch>>) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where2<MatchWithBranch<PX.Objects.GL.Branch.branchID>, And<Match<Current<AccessInfo.userName>>>>, OrderBy<Asc<PX.Objects.GL.Branch.branchCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Branch>>();
    }
    else
    {
      PXSelectBase<PX.Objects.GL.Account, PXSelectOrderBy<PX.Objects.GL.Account, OrderBy<Asc<PX.Objects.GL.Account.accountCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Account>>) PXSelectBase<PX.Objects.GL.Account, PXSelectOrderBy<PX.Objects.GL.Account, OrderBy<Asc<PX.Objects.GL.Account.accountCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Account>>();
      PXSelectBase<PX.Objects.GL.Sub, PXSelectOrderBy<PX.Objects.GL.Sub, OrderBy<Asc<PX.Objects.GL.Sub.subCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Sub>>) PXSelectBase<PX.Objects.GL.Sub, PXSelectOrderBy<PX.Objects.GL.Sub, OrderBy<Asc<PX.Objects.GL.Sub.subCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Sub>>();
      PXSelectBase<PX.Objects.GL.Branch, PXSelectOrderBy<PX.Objects.GL.Branch, OrderBy<Asc<PX.Objects.GL.Branch.branchCD>>>.Config>.Clear((PXGraph) this.Base);
      ((IEnumerable<PXResult<PX.Objects.GL.Branch>>) PXSelectBase<PX.Objects.GL.Branch, PXSelectOrderBy<PX.Objects.GL.Branch, OrderBy<Asc<PX.Objects.GL.Branch.branchCD>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToList<PXResult<PX.Objects.GL.Branch>>();
    }
    this._accountRangeCache = new RMReportRange<PX.Objects.GL.Account>((PXGraph) this.Base, "ACCOUNT", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._subRangeCache = new RMReportRange<PX.Objects.GL.Sub>((PXGraph) this.Base, "SUBACCOUNT", RMReportConstants.WildcardMode.Normal, RMReportConstants.BetweenMode.ByChar);
    this._branchRangeCache = new RMReportRange<PX.Objects.GL.Branch>((PXGraph) this.Base, "BRANCH", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._historySegments = new HashSet<GLHistoryKeyTuple>();
    this._glhistoryPeriodsNested = new GLHistoryHierDict();
    this._historyLoaded = new HashSet<Tuple<int, int>>();
    this._historyDrilldownLoaded = new HashSet<Tuple<int, int, string>>();
    this._accountMask = ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.Account)].GetStateExt<PX.Objects.GL.Account.accountCD>((object) null) is PXStringState stateExt1 ? stateExt1.InputMask : (string) null;
    this._subMask = ((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.Sub)].GetStateExt<PX.Objects.GL.Sub.subCD>((object) null) is PXStringState stateExt2 ? stateExt2.InputMask : (string) null;
  }

  public void NormalizeDataSource(RMDataSourceGL dsGL)
  {
    if (dsGL.StartBranch != null && dsGL.StartBranch.TrimEnd() == "")
      dsGL.StartBranch = (string) null;
    if (dsGL.EndBranch != null && dsGL.EndBranch.TrimEnd() == "")
      dsGL.EndBranch = (string) null;
    if (dsGL.AccountClassID != null && dsGL.AccountClassID.TrimEnd() == "")
      dsGL.AccountClassID = (string) null;
    if (dsGL.StartAccount != null && dsGL.StartAccount.TrimEnd() == "")
      dsGL.StartAccount = (string) null;
    if (dsGL.EndAccount != null && dsGL.EndAccount.TrimEnd() == "")
      dsGL.EndAccount = (string) null;
    if (dsGL.StartSub != null && dsGL.StartSub.TrimEnd() == "")
      dsGL.StartSub = (string) null;
    if (dsGL.EndSub != null && dsGL.EndSub.TrimEnd() == "")
      dsGL.EndSub = (string) null;
    if (dsGL.StartPeriod != null && dsGL.StartPeriod.TrimEnd() == "")
      dsGL.StartPeriod = (string) null;
    if (dsGL.EndPeriod != null && dsGL.EndPeriod.TrimEnd() == "")
      dsGL.EndPeriod = (string) null;
    short? nullable = dsGL.StartPeriodOffset;
    if (!nullable.HasValue)
      dsGL.StartPeriodOffset = new short?((short) 0);
    nullable = dsGL.StartPeriodYearOffset;
    if (!nullable.HasValue)
      dsGL.StartPeriodYearOffset = new short?((short) 0);
    nullable = dsGL.EndPeriodOffset;
    if (!nullable.HasValue)
      dsGL.EndPeriodOffset = new short?((short) 0);
    nullable = dsGL.EndPeriodYearOffset;
    if (nullable.HasValue)
      return;
    dsGL.EndPeriodYearOffset = new short?((short) 0);
  }

  private IEnumerable<GLHistory> NormalizeHistory(
    IEnumerable<PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>> history)
  {
    foreach (PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account> pxResult in history)
    {
      ArmGLHistoryByPeriod glHistoryByPeriod = PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult);
      GLHistory glHistory1 = PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult);
      if (glHistoryByPeriod.FinPeriodID == glHistoryByPeriod.LastActivityPeriod)
      {
        yield return glHistory1;
      }
      else
      {
        PX.Objects.GL.Account account = PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>.op_Implicit(pxResult);
        GLHistory glHistory2 = new GLHistory();
        glHistory2.LedgerID = glHistoryByPeriod.LedgerID;
        glHistory2.BranchID = glHistoryByPeriod.BranchID;
        glHistory2.AccountID = glHistoryByPeriod.AccountID;
        glHistory2.SubID = glHistoryByPeriod.SubID;
        glHistory2.FinPeriodID = glHistoryByPeriod.FinPeriodID;
        glHistory2.FinPtdCredit = new Decimal?(0M);
        glHistory2.TranPtdCredit = new Decimal?(0M);
        glHistory2.FinPtdDebit = new Decimal?(0M);
        glHistory2.TranPtdDebit = new Decimal?(0M);
        glHistory2.CuryFinPtdCredit = new Decimal?(0M);
        glHistory2.CuryFinPtdDebit = new Decimal?(0M);
        glHistory2.CuryTranPtdCredit = new Decimal?(0M);
        glHistory2.CuryTranPtdDebit = new Decimal?(0M);
        GLHistory glHistory3 = glHistory2;
        glHistory3.FinBegBalance = glHistory3.FinYtdBalance = (account.Type == "I" || account.Type == "E") && new ReportFunctions().ArePeriodsInSameYear(glHistoryByPeriod.LastActivityPeriod, glHistoryByPeriod.FinPeriodID) || account.Type == "A" || account.Type == "L" ? glHistory1.FinYtdBalance : new Decimal?(0M);
        glHistory3.TranBegBalance = glHistory3.TranYtdBalance = (account.Type == "I" || account.Type == "E") && new ReportFunctions().ArePeriodsInSameYear(glHistoryByPeriod.LastActivityPeriod, glHistoryByPeriod.FinPeriodID) || account.Type == "A" || account.Type == "L" ? glHistory1.TranYtdBalance : new Decimal?(0M);
        glHistory3.CuryFinBegBalance = glHistory3.CuryFinYtdBalance = (account.Type == "I" || account.Type == "E") && new ReportFunctions().ArePeriodsInSameYear(glHistoryByPeriod.LastActivityPeriod, glHistoryByPeriod.FinPeriodID) || account.Type == "A" || account.Type == "L" ? glHistory1.CuryFinYtdBalance : new Decimal?(0M);
        glHistory3.CuryTranBegBalance = glHistory3.CuryTranYtdBalance = (account.Type == "I" || account.Type == "E") && new ReportFunctions().ArePeriodsInSameYear(glHistoryByPeriod.LastActivityPeriod, glHistoryByPeriod.FinPeriodID) || account.Type == "A" || account.Type == "L" ? glHistory1.CuryTranYtdBalance : new Decimal?(0M);
        yield return glHistory3;
      }
    }
  }

  private void LoadHistory(int ledgerID, int year)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ProcessGLResultset((IEnumerable<GLHistory>) ((IQueryable<PXResult<GLHistory>>) PXSelectBase<GLHistory, PXSelectReadonly<GLHistory, Where<GLHistory.ledgerID, Equal<Required<GLHistory.ledgerID>>, And<GLHistory.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>, And<GLHistory.finPeriodID, Less<Required<GLHistory.finPeriodID>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) ledgerID,
      (object) year.ToString(),
      (object) (year + 1).ToString()
    })).Select<PXResult<GLHistory>, GLHistory>(Expression.Lambda<Func<PXResult<GLHistory>, GLHistory>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression1)));
    MasterFinPeriod masterFinPeriod = this._reportPeriods.FinPeriods.Where<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (p => string.Compare(p.FinPeriodID, year.ToString()) < 0)).LastOrDefault<MasterFinPeriod>();
    if (masterFinPeriod == null)
      return;
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ProcessGLResultset((IEnumerable<GLHistory>) ((IQueryable<PXResult<GLHistory>>) PXSelectBase<GLHistory, PXViewOf<GLHistory>.BasedOn<SelectFromBase<GLHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Account>.On<BqlOperand<GLHistory.accountID, IBqlInt>.IsEqual<PX.Objects.GL.Account.accountID>>>, FbqlJoins.Inner<ArmGLHistoryByPeriod>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLHistory.ledgerID, Equal<ArmGLHistoryByPeriod.ledgerID>>>>, And<BqlOperand<GLHistory.branchID, IBqlInt>.IsEqual<ArmGLHistoryByPeriod.branchID>>>, And<BqlOperand<GLHistory.accountID, IBqlInt>.IsEqual<ArmGLHistoryByPeriod.accountID>>>, And<BqlOperand<GLHistory.subID, IBqlInt>.IsEqual<ArmGLHistoryByPeriod.subID>>>>.And<BqlOperand<GLHistory.finPeriodID, IBqlString>.IsEqual<ArmGLHistoryByPeriod.lastActivityPeriod>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ArmGLHistoryByPeriod.ledgerID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ArmGLHistoryByPeriod.finPeriodID, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.GL.Account.type, IBqlString>.IsIn<AccountType.asset, AccountType.liability>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) ledgerID,
      (object) masterFinPeriod.FinPeriodID
    })).Select<PXResult<GLHistory>, GLHistory>(Expression.Lambda<Func<PXResult<GLHistory>, GLHistory>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression2)));
  }

  private void ProcessGLResultset(IEnumerable<GLHistory> resultset)
  {
    EnumerableExtensions.ForEach<GLHistory>(resultset, new Action<GLHistory>(this.ProcessGLHistoryItem));
  }

  private void ProcessGLHistoryItem(GLHistory historyItem)
  {
    (int, int, (int, int)) valueTuple1;
    ref (int, int, (int, int)) local = ref valueTuple1;
    int? nullable1 = historyItem.AccountID;
    int num1 = nullable1.Value;
    nullable1 = historyItem.SubID;
    int num2 = nullable1.Value;
    nullable1 = historyItem.BranchID;
    int num3 = nullable1.Value;
    nullable1 = historyItem.LedgerID;
    int num4 = nullable1.Value;
    (int, int) valueTuple2 = (num3, num4);
    local = (num1, num2, valueTuple2);
    Dictionary<string, GLHistory> dictionary;
    if (this._glhistoryPeriodsNested.TryGetValueNested(valueTuple1, ref dictionary))
      dictionary[historyItem.FinPeriodID] = historyItem;
    else
      this._glhistoryPeriodsNested.AddNested(valueTuple1, new Dictionary<string, GLHistory>()
      {
        {
          historyItem.FinPeriodID,
          historyItem
        }
      });
    HashSet<GLHistoryKeyTuple> historySegments1 = this._historySegments;
    int? nullable2 = historyItem.LedgerID;
    int ledgerID1 = nullable2.Value;
    nullable2 = historyItem.AccountID;
    int accountID1 = nullable2.Value;
    GLHistoryKeyTuple glHistoryKeyTuple1 = new GLHistoryKeyTuple(ledgerID1, 0, accountID1, 0);
    historySegments1.Add(glHistoryKeyTuple1);
    HashSet<GLHistoryKeyTuple> historySegments2 = this._historySegments;
    nullable2 = historyItem.LedgerID;
    int ledgerID2 = nullable2.Value;
    nullable2 = historyItem.AccountID;
    int accountID2 = nullable2.Value;
    nullable2 = historyItem.SubID;
    int subID = nullable2.Value;
    GLHistoryKeyTuple glHistoryKeyTuple2 = new GLHistoryKeyTuple(ledgerID2, 0, accountID2, subID);
    historySegments2.Add(glHistoryKeyTuple2);
  }

  private void LoadDrillDownHistory(int ledgerID, int year, string accountClassID)
  {
    PXSelectBase<ArmGLHistoryByPeriod> pxSelectBase = (PXSelectBase<ArmGLHistoryByPeriod>) new PXSelectReadonly2<ArmGLHistoryByPeriod, LeftJoin<GLHistory, On<ArmGLHistoryByPeriod.ledgerID, Equal<GLHistory.ledgerID>, And<ArmGLHistoryByPeriod.accountID, Equal<GLHistory.accountID>, And<ArmGLHistoryByPeriod.subID, Equal<GLHistory.subID>, And<ArmGLHistoryByPeriod.lastActivityPeriod, Equal<GLHistory.finPeriodID>, And<ArmGLHistoryByPeriod.branchID, Equal<GLHistory.branchID>>>>>>, LeftJoin<PX.Objects.GL.Account, On<ArmGLHistoryByPeriod.accountID, Equal<PX.Objects.GL.Account.accountID>>>>, Where<ArmGLHistoryByPeriod.ledgerID, Equal<Required<ArmGLHistoryByPeriod.ledgerID>>>>((PXGraph) this.Base);
    List<object> collection = new List<object>();
    collection.Add((object) ledgerID);
    if (!string.IsNullOrEmpty(accountClassID))
    {
      pxSelectBase.WhereAnd<Where<ArmGLHistoryByPeriod.accountClassID, Equal<Required<ArmGLHistoryByPeriod.accountClassID>>>>();
      collection.Add((object) accountClassID);
    }
    EnumerableExtensions.ForEach<GLHistory>(this.NormalizeHistory(new PXView((PXGraph) this.Base, true, ((PXSelectBase) pxSelectBase).View.BqlSelect.WhereAnd<Where<ArmGLHistoryByPeriod.lastActivityPeriod, Like<Required<GLHistory.finPeriodID>>, And<ArmGLHistoryByPeriod.finPeriodID, GreaterEqual<Required<GLHistory.finPeriodID>>, And<ArmGLHistoryByPeriod.finPeriodID, Less<Required<GLHistory.finPeriodID>>>>>>()).SelectMulti(new List<object>((IEnumerable<object>) collection)
    {
      (object) (year.ToString() + "%%"),
      (object) year.ToString(),
      (object) (year + 1).ToString()
    }.ToArray()).Cast<PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>>()), new Action<GLHistory>(this.ProcessGLHistoryItem));
    MasterFinPeriod masterFinPeriod = this._reportPeriods.FinPeriods.Where<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (p => string.Compare(p.FinPeriodID, year.ToString()) < 0)).LastOrDefault<MasterFinPeriod>();
    if (masterFinPeriod == null)
      return;
    EnumerableExtensions.ForEach<GLHistory>(this.NormalizeHistory(new PXView((PXGraph) this.Base, true, ((PXSelectBase) pxSelectBase).View.BqlSelect.WhereAnd<Where2<Where<PX.Objects.GL.Account.type, Equal<AccountType.asset>, Or<PX.Objects.GL.Account.type, Equal<AccountType.liability>>>, And<ArmGLHistoryByPeriod.finPeriodID, Equal<Required<ArmGLHistoryByPeriod.finPeriodID>>>>>()).SelectMulti(new List<object>((IEnumerable<object>) collection)
    {
      (object) masterFinPeriod.FinPeriodID
    }.ToArray()).Cast<PXResult<ArmGLHistoryByPeriod, GLHistory, PX.Objects.GL.Account>>()), new Action<GLHistory>(this.ProcessGLHistoryItem));
  }

  [PXOverride]
  public virtual object GetHistoryValue(
    ARmDataSet dataSet,
    bool drilldown,
    Func<ARmDataSet, bool, object> del)
  {
    if (!(((PXSelectBase<RMReport>) this.Base.Report).Current.Type == "GL"))
      return del(dataSet, drilldown);
    if (!(dataSet[(object) RMReportReaderGL.Keys.BookCode] is string str1))
      str1 = "";
    if (!string.IsNullOrEmpty(str1))
    {
      if (!(dataSet[(object) RMReportReaderGL.Keys.StartAccount] is string str2))
        str2 = "";
      if (string.IsNullOrEmpty(str2))
      {
        if (!(dataSet[(object) RMReportReaderGL.Keys.AccountClass] is string str3))
          str3 = "";
        if (string.IsNullOrEmpty(str3))
          goto label_73;
      }
      if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str4))
        str4 = "";
      if (!string.IsNullOrEmpty(str4))
      {
        short? nullable1 = (short?) dataSet[(object) RMReportReaderGL.Keys.AmountType];
        int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num1 = 0;
        if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
        {
          RMDataSource current = ((PXSelectBase<RMDataSource>) this.Base.DataSourceByID).Current;
          RMDataSourceGL dsGL = ((PXGraph) this.Base).Caches[typeof (RMDataSource)].GetExtension<RMDataSourceGL>((object) current);
          current.AmountType = (short?) dataSet[(object) RMReportReaderGL.Keys.AmountType];
          if (!string.IsNullOrEmpty(dataSet[(object) RMReportReaderGL.Keys.BookCode] as string))
            ((PXSelectBase<RMDataSource>) this.Base.DataSourceByID).SetValueExt<RMDataSourceGL.ledgerID>(current, (object) (dataSet[(object) RMReportReaderGL.Keys.BookCode] as string));
          RMDataSourceGL rmDataSourceGl1 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.AccountClass] is string str5))
            str5 = "";
          rmDataSourceGl1.AccountClassID = str5;
          RMDataSourceGL rmDataSourceGl2 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.StartAccount] is string str6))
            str6 = "";
          rmDataSourceGl2.StartAccount = str6;
          RMDataSourceGL rmDataSourceGl3 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.EndAccount] is string str7))
            str7 = "";
          rmDataSourceGl3.EndAccount = str7;
          RMDataSourceGL rmDataSourceGl4 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.StartSub] is string str8))
            str8 = "";
          rmDataSourceGl4.StartSub = str8;
          RMDataSourceGL rmDataSourceGl5 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.EndSub] is string str9))
            str9 = "";
          rmDataSourceGl5.EndSub = str9;
          if (!string.IsNullOrEmpty(dataSet[(object) RMReportReaderGL.Keys.Organization] as string))
            ((PXSelectBase<RMDataSource>) this.Base.DataSourceByID).SetValueExt<RMDataSourceGL.organizationID>(current, (object) (dataSet[(object) RMReportReaderGL.Keys.Organization] as string));
          dsGL.UseMasterCalendar = dataSet[(object) RMReportReaderGL.Keys.UseMasterCalendar] as bool?;
          RMDataSourceGL rmDataSourceGl6 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.StartBranch] is string str10))
            str10 = "";
          rmDataSourceGl6.StartBranch = str10;
          RMDataSourceGL rmDataSourceGl7 = dsGL;
          if (!(dataSet[(object) RMReportReaderGL.Keys.EndBranch] is string str11))
            str11 = "";
          rmDataSourceGl7.EndBranch = str11;
          if (!(dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str12))
            str12 = "";
          string str13 = str12;
          dsGL.EndPeriod = (str13.Length > 2 ? (str13.Substring(2) + "    ").Substring(0, 4) : "    ") + (str13.Length > 2 ? str13.Substring(0, 2) : str13);
          RMDataSourceGL rmDataSourceGl8 = dsGL;
          nullable2 = (int?) dataSet[(object) RMReportReaderGL.Keys.EndOffset];
          short? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new short?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new short?((short) nullable2.GetValueOrDefault());
          rmDataSourceGl8.EndPeriodOffset = nullable3;
          RMDataSourceGL rmDataSourceGl9 = dsGL;
          nullable2 = (int?) dataSet[(object) RMReportReaderGL.Keys.EndYearOffset];
          short? nullable4;
          if (!nullable2.HasValue)
          {
            nullable1 = new short?();
            nullable4 = nullable1;
          }
          else
            nullable4 = new short?((short) nullable2.GetValueOrDefault());
          rmDataSourceGl9.EndPeriodYearOffset = nullable4;
          if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str14))
            str14 = "";
          string str15 = str14;
          dsGL.StartPeriod = (str15.Length > 2 ? (str15.Substring(2) + "    ").Substring(0, 4) : "    ") + (str15.Length > 2 ? str15.Substring(0, 2) : str15);
          RMDataSourceGL rmDataSourceGl10 = dsGL;
          nullable2 = (int?) dataSet[(object) RMReportReaderGL.Keys.StartOffset];
          short? nullable5;
          if (!nullable2.HasValue)
          {
            nullable1 = new short?();
            nullable5 = nullable1;
          }
          else
            nullable5 = new short?((short) nullable2.GetValueOrDefault());
          rmDataSourceGl10.StartPeriodOffset = nullable5;
          RMDataSourceGL rmDataSourceGl11 = dsGL;
          nullable2 = (int?) dataSet[(object) RMReportReaderGL.Keys.StartYearOffset];
          short? nullable6;
          if (!nullable2.HasValue)
          {
            nullable1 = new short?();
            nullable6 = nullable1;
          }
          else
            nullable6 = new short?((short) nullable2.GetValueOrDefault());
          rmDataSourceGl11.StartPeriodYearOffset = nullable6;
          if (drilldown)
            ++this.Base.DrilldownNumber;
          List<object[]> splitret = (List<object[]>) null;
          if (current.Expand != "N")
            splitret = new List<object[]>();
          nullable2 = dsGL.LedgerID;
          if (nullable2.HasValue)
          {
            nullable1 = current.AmountType;
            if (nullable1.HasValue)
            {
              nullable1 = current.AmountType;
              nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              int num2 = 0;
              if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
              {
                this.GLEnsureInitialized();
                this.EnsureHistoryLoaded(dsGL, drilldown);
                this.NormalizeDataSource(dsGL);
                List<PX.Objects.GL.Account> itemsInRange1 = this.GetItemsInRange<PX.Objects.GL.Account>(dataSet);
                List<PX.Objects.GL.Sub> itemsInRange2 = this.GetItemsInRange<PX.Objects.GL.Sub>(dataSet);
                List<PX.Objects.GL.Branch> branchList = this.GetItemsInRange<PX.Objects.GL.Branch>(dataSet);
                nullable2 = dsGL.OrganizationID;
                if (nullable2.HasValue)
                  branchList = branchList.Where<PX.Objects.GL.Branch>((Func<PX.Objects.GL.Branch, bool>) (branch =>
                  {
                    int? organizationId1 = branch.OrganizationID;
                    int? organizationId2 = dsGL.OrganizationID;
                    return organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue;
                  })).ToList<PX.Objects.GL.Branch>();
                if (current.Expand == "A")
                {
                  foreach (PX.Objects.GL.Account account in itemsInRange1)
                  {
                    ARmDataSet armDataSet = new ARmDataSet(dataSet);
                    armDataSet[(object) RMReportReaderGL.Keys.StartSub] = armDataSet[(object) RMReportReaderGL.Keys.EndSub] = (object) account.AccountCD;
                    splitret.Add(new object[6]
                    {
                      (object) account.AccountCD,
                      (object) account.Description,
                      (object) 0M,
                      (object) armDataSet,
                      null,
                      (object) Mask.Format(this._accountMask, account.AccountCD)
                    });
                  }
                }
                else if (current.Expand == "S")
                {
                  foreach (PX.Objects.GL.Sub sub in itemsInRange2)
                  {
                    ARmDataSet armDataSet = new ARmDataSet(dataSet);
                    armDataSet[(object) RMReportReaderGL.Keys.StartSub] = armDataSet[(object) RMReportReaderGL.Keys.EndSub] = (object) sub.SubCD;
                    splitret.Add(new object[6]
                    {
                      (object) sub.SubCD,
                      (object) sub.Description,
                      (object) 0M,
                      (object) armDataSet,
                      null,
                      (object) Mask.Format(this._subMask, sub.SubCD)
                    });
                  }
                }
                if (drilldown && current.Expand == "A")
                  RMReportReaderGL.SortAccounts(itemsInRange1);
                return this.CalculateAndExpandValue(drilldown, current, dsGL, dataSet, itemsInRange1, itemsInRange2, branchList, splitret);
              }
            }
          }
          return (object) 0M;
        }
      }
    }
label_73:
    return (object) Decimal.MinValue;
  }

  private List<T> GetItemsInRange<T>(ARmDataSet dataSet)
  {
    return (List<T>) this.Base.GetItemsInRange(typeof (T), dataSet);
  }

  [PXOverride]
  public virtual IEnumerable GetItemsInRange(
    System.Type table,
    ARmDataSet dataSet,
    Func<System.Type, ARmDataSet, IEnumerable> del)
  {
    if (table == typeof (PX.Objects.GL.Account))
      return (IEnumerable) this._accountRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderGL.Keys.StartAccount] as string, (Func<string, string>) (range => range + dataSet[(object) RMReportReaderGL.Keys.AccountClass]?.ToString()), (Func<string, HashSet<PX.Objects.GL.Account>>) (range =>
      {
        string range1 = range;
        if (!(dataSet[(object) RMReportReaderGL.Keys.AccountClass] is string accountClassID2))
          accountClassID2 = "";
        return this.GetAccountsInRange(range1, accountClassID2);
      }));
    if (table == typeof (PX.Objects.GL.Sub))
      return (IEnumerable) this._subRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderGL.Keys.StartSub] as string, (Func<PX.Objects.GL.Sub, string>) (sub => sub.SubCD), (Action<PX.Objects.GL.Sub, string>) ((sub, code) => sub.SubCD = code));
    if (table == typeof (PX.Objects.GL.Branch))
      return (IEnumerable) this._branchRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderGL.Keys.StartBranch] is string data ? data.ToUpperInvariant() : (string) null, (Func<PX.Objects.GL.Branch, string>) (branch => branch.BranchCD?.ToUpperInvariant()), (Action<PX.Objects.GL.Branch, string>) ((branch, code) =>
      {
        PX.Objects.GL.Branch branch1;
        this._branches.TryGetValue(code, out branch1);
        branch.BranchCD = branch1?.BranchCD ?? code?.ToUpperInvariant();
      }));
    if (del != null)
      return del(table, dataSet);
    throw new NotSupportedException();
  }

  private HashSet<PX.Objects.GL.Account> GetAccountsInRange(string range, string accountClassID)
  {
    HashSet<PX.Objects.GL.Account> accountsInRange = new HashSet<PX.Objects.GL.Account>();
    string str = range;
    char[] chArray = new char[1]{ ',' };
    foreach (string range1 in str.Split(chArray))
    {
      string start;
      string end;
      RMReportRange.ParseRangeStartEndPair(range1, out start, out end);
      if (!string.IsNullOrEmpty(start))
      {
        if (string.IsNullOrEmpty(end) || end == start)
        {
          string acct = RMReportWildcard.EnsureWildcardForFixed(start, this._accountRangeCache.Wildcard);
          if (acct.Contains<char>('_'))
          {
            accountsInRange.UnionWith(this._accountRangeCache.Cache.Cached.Cast<PX.Objects.GL.Account>().Where<PX.Objects.GL.Account>((Func<PX.Objects.GL.Account, bool>) (a =>
            {
              if (!RMReportWildcard.IsLike(acct, a.AccountCD))
                return false;
              return string.IsNullOrEmpty(accountClassID) || accountClassID == a.AccountClassID;
            })));
          }
          else
          {
            this._accountRangeCache.Instance.AccountCD = acct;
            PX.Objects.GL.Account account = (PX.Objects.GL.Account) this._accountRangeCache.Cache.Locate((object) this._accountRangeCache.Instance);
            if (account != null && (string.IsNullOrEmpty(accountClassID) || accountClassID == account.AccountClassID))
              accountsInRange.Add(account);
          }
        }
        else
          accountsInRange.UnionWith(RMReportWildcard.GetBetweenForFixed<PX.Objects.GL.Account>(start, end, this._accountRangeCache.Wildcard, this._accountRangeCache.Cache.Cached, (Func<PX.Objects.GL.Account, string>) (a => a.AccountCD)).Where<PX.Objects.GL.Account>((Func<PX.Objects.GL.Account, bool>) (a => string.IsNullOrEmpty(accountClassID) || accountClassID == a.AccountClassID)));
      }
      else
        accountsInRange.UnionWith(this._accountRangeCache.Cache.Cached.Cast<PX.Objects.GL.Account>().Where<PX.Objects.GL.Account>((Func<PX.Objects.GL.Account, bool>) (a => string.IsNullOrEmpty(accountClassID) || accountClassID == a.AccountClassID)));
    }
    return accountsInRange;
  }

  private void EnsureHistoryLoaded(RMDataSourceGL dsGL, bool isDrillDown)
  {
    string str1 = string.Empty;
    string str2 = string.Empty;
    short? nullable1;
    int? nullable2;
    if (dsGL.StartPeriod != null)
    {
      string str3 = RMReportWildcard.EnsureWildcard(dsGL.StartPeriod, this._reportPeriods.PerWildcard);
      if (!str3.Contains<char>('_'))
      {
        nullable1 = dsGL.StartPeriodOffset;
        if (nullable1.HasValue)
        {
          nullable1 = dsGL.StartPeriodOffset;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num = 0;
          if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
            goto label_5;
        }
      }
      nullable1 = dsGL.StartPeriodYearOffset;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num1 = 0;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
        goto label_6;
label_5:
      str3 = this._reportPeriods.GetFinPeriod(str3, dsGL.StartPeriodYearOffset, dsGL.StartPeriodOffset);
label_6:
      str1 = str3.Replace('_', '0');
    }
    if (dsGL.EndPeriod != null)
    {
      string str4 = RMReportWildcard.EnsureWildcard(dsGL.EndPeriod, this._reportPeriods.PerWildcard);
      if (!str4.Contains<char>('_'))
      {
        nullable1 = dsGL.EndPeriodOffset;
        if (nullable1.HasValue)
        {
          nullable1 = dsGL.EndPeriodOffset;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num = 0;
          if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
            goto label_12;
        }
      }
      nullable1 = dsGL.EndPeriodYearOffset;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
        goto label_13;
label_12:
      str4 = this._reportPeriods.GetFinPeriod(str4, dsGL.EndPeriodYearOffset, dsGL.EndPeriodOffset);
label_13:
      str2 = str4.Replace('_', '9');
    }
    if (string.IsNullOrEmpty(str1))
      str1 = this._reportPeriods.FinPeriods.Select<MasterFinPeriod, string>((Func<MasterFinPeriod, string>) (p => p.FinPeriodID)).Min<string>();
    if (string.IsNullOrEmpty(str2))
      str2 = this._reportPeriods.FinPeriods.Select<MasterFinPeriod, string>((Func<MasterFinPeriod, string>) (p => p.FinPeriodID)).Max<string>();
    int num3 = int.Parse(str1.Substring(0, 4));
    int num4 = int.Parse(str2.Substring(0, 4));
    for (int year = num3; year <= num4; ++year)
    {
      nullable2 = dsGL.LedgerID;
      Tuple<int, int> tuple1 = new Tuple<int, int>(nullable2.Value, year);
      if (isDrillDown)
      {
        nullable2 = dsGL.LedgerID;
        Tuple<int, int, string> tuple2 = new Tuple<int, int, string>(nullable2.Value, year, dsGL.AccountClassID);
        if (!this._historyLoaded.Contains(tuple1) && !this._historyDrilldownLoaded.Contains(tuple2))
        {
          nullable2 = dsGL.LedgerID;
          this.LoadDrillDownHistory(nullable2.Value, year, dsGL.AccountClassID);
          this._historyDrilldownLoaded.Add(tuple2);
        }
      }
      else if (!this._historyLoaded.Contains(tuple1))
      {
        nullable2 = dsGL.LedgerID;
        this.LoadHistory(nullable2.Value, year);
        this._historyLoaded.Add(tuple1);
      }
    }
  }

  private static void SortAccounts(List<PX.Objects.GL.Account> accounts)
  {
    accounts.Sort((Comparison<PX.Objects.GL.Account>) ((a, b) =>
    {
      short? coaOrder1 = a.COAOrder;
      int? nullable1 = coaOrder1.HasValue ? new int?((int) coaOrder1.GetValueOrDefault()) : new int?();
      short? coaOrder2 = b.COAOrder;
      int? nullable2 = coaOrder2.HasValue ? new int?((int) coaOrder2.GetValueOrDefault()) : new int?();
      if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        return -1;
      short? coaOrder3 = a.COAOrder;
      nullable2 = coaOrder3.HasValue ? new int?((int) coaOrder3.GetValueOrDefault()) : new int?();
      short? coaOrder4 = b.COAOrder;
      int? nullable3 = coaOrder4.HasValue ? new int?((int) coaOrder4.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        return 1;
      return a.Type == b.Type ? string.Compare(a.AccountCD, b.AccountCD, StringComparison.OrdinalIgnoreCase) : string.Compare(a.Type, b.Type, StringComparison.OrdinalIgnoreCase);
    }));
  }

  private IReadOnlyCollection<GLHistory> GetPeriodsToCalculate(
    Dictionary<string, GLHistory> periodsForKey,
    PX.Objects.GL.Account account,
    RMDataSource ds,
    RMDataSourceGL dsGL,
    out bool takeLast)
  {
    takeLast = false;
    PX.Objects.GL.Ledger ledger;
    bool flag1 = this._ledgers.TryGetValue(dsGL.LedgerID.Value, out ledger) && ledger?.BalanceType == "B";
    int num1;
    if ((account.Type == "E" ? 1 : (account.Type == "I" ? 1 : 0)) == 0)
    {
      int? accountId = account.AccountID;
      int? netIncomeAccountId = this._ytdNetIncomeAccountID;
      num1 = accountId.GetValueOrDefault() == netIncomeAccountId.GetValueOrDefault() & accountId.HasValue == netIncomeAccountId.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    int num2 = flag1 ? 1 : 0;
    bool flag2 = (num1 | num2) != 0;
    short? amountType1 = ds.AmountType;
    int? nullable = amountType1.HasValue ? new int?((int) amountType1.GetValueOrDefault()) : new int?();
    int num3 = 4;
    if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
    {
      short? amountType2 = ds.AmountType;
      nullable = amountType2.HasValue ? new int?((int) amountType2.GetValueOrDefault()) : new int?();
      int num4 = 24;
      if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
      {
        amountType2 = ds.AmountType;
        nullable = amountType2.HasValue ? new int?((int) amountType2.GetValueOrDefault()) : new int?();
        int num5 = 5;
        if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
        {
          amountType2 = ds.AmountType;
          nullable = amountType2.HasValue ? new int?((int) amountType2.GetValueOrDefault()) : new int?();
          int num6 = 25;
          if (!(nullable.GetValueOrDefault() == num6 & nullable.HasValue))
            return this._reportPeriods.GetPeriodsForRegularAmountOptimized(dsGL, periodsForKey);
        }
        return this._reportPeriods.GetPeriodsForEndingBalanceAmountOptimized(dsGL, periodsForKey, flag2);
      }
    }
    return this._reportPeriods.GetPeriodsForBeginningBalanceAmountOptimized(dsGL, periodsForKey, flag2, out takeLast);
  }

  private object CalculateAndExpandValue(
    bool drilldown,
    RMDataSource ds,
    RMDataSourceGL dsGL,
    ARmDataSet dataSet,
    List<PX.Objects.GL.Account> accounts,
    List<PX.Objects.GL.Sub> subs,
    List<PX.Objects.GL.Branch> branchList,
    List<object[]> splitret)
  {
    RMReportReaderGL.SharedContextGL sharedContext = new RMReportReaderGL.SharedContextGL(this, drilldown, ds, dsGL, dataSet, accounts, subs, branchList, splitret);
    if (sharedContext.ParallelizeAccounts)
    {
      Parallel.For(0, sharedContext.Accounts.Count, sharedContext.ParallelOptions, new Action<int>(sharedContext.AccountIterationNoClosures));
    }
    else
    {
      for (int accountIndex = 0; accountIndex < sharedContext.Accounts.Count; ++accountIndex)
        RMReportReaderGL.AccountIteration(sharedContext, accountIndex);
    }
    if (drilldown)
    {
      IEnumerable<PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>> pxResults = sharedContext.DrilldownData.Values.Select<PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, (PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string)>((Func<PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, (PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string)>) (row => (row, ((PXResult) row).GetItem<PX.Objects.GL.Account>()?.AccountCD, ((PXResult) row).GetItem<PX.Objects.GL.Sub>()?.SubCD, ((PXResult) row).GetItem<ArmGLHistoryByPeriod>()?.FinPeriodID))).OrderBy<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>((Func<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>) (tuple => tuple.AccountCD)).ThenBy<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>((Func<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>) (tuple => tuple.SubCD)).ThenBy<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>((Func<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), string>) (tuple => tuple.FinPeriod)).Select<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>>((Func<(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>, string, string, string), PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>>) (tuple => tuple.Row));
      PXResultset<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup> andExpandValue = new PXResultset<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>();
      ((PXResultset<ArmGLHistoryByPeriod>) andExpandValue).AddRange((IEnumerable<PXResult<ArmGLHistoryByPeriod>>) pxResults);
      GLHistory glHistory = PXResultset<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>.op_Implicit(andExpandValue);
      if (glHistory == null)
        return (object) andExpandValue;
      glHistory.CuryYtdBalance = new Decimal?(sharedContext.TotalAmount);
      return (object) andExpandValue;
    }
    return sharedContext.DataSource.Expand != "N" ? (object) sharedContext.SplitReturn : (object) sharedContext.TotalAmount;
  }

  private static void AccountIteration(
    RMReportReaderGL.SharedContextGL sharedContext,
    int accountIndex)
  {
    PX.Objects.GL.Account account = sharedContext.Accounts[accountIndex];
    NestedDictionary<int, (int, int), Dictionary<string, GLHistory>> accountDict;
    if (!((Dictionary<int, NestedDictionary<int, (int, int), Dictionary<string, GLHistory>>>) sharedContext.This._glhistoryPeriodsNested).TryGetValue(account.AccountID.Value, out accountDict))
      return;
    int? nullable = sharedContext.DataSourceGL.LedgerID;
    int num = nullable.Value;
    HashSet<GLHistoryKeyTuple> historySegments = sharedContext.This._historySegments;
    int ledgerID = num;
    nullable = account.AccountID;
    int accountID = nullable.Value;
    GLHistoryKeyTuple glHistoryKeyTuple = new GLHistoryKeyTuple(ledgerID, 0, accountID, 0);
    if (!historySegments.Contains(glHistoryKeyTuple))
      return;
    RMReportReaderGL.SubIterationContextGL iterationContextGl = new RMReportReaderGL.SubIterationContextGL(sharedContext, account, accountIndex, accountDict);
    if (sharedContext.ParallelizeSubs)
      Parallel.ForEach<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>((IEnumerable<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>) iterationContextGl.AccountDict, sharedContext.ParallelOptions, new Action<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>(iterationContextGl.SubIterationNoClosures));
    else
      EnumerableExtensions.ForEach<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>((IEnumerable<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>) iterationContextGl.AccountDict, new Action<KeyValuePair<int, Dictionary<(int, int), Dictionary<string, GLHistory>>>>(iterationContextGl.SubIterationNoClosures));
  }

  private static void SubIteration(
    in RMReportReaderGL.SubIterationContextGL subIterationContext,
    KeyValuePair<int, Dictionary<(int BranchID, int LedgerID), Dictionary<string, GLHistory>>> subDict)
  {
    RMReportReaderGL.SharedContextGL sharedContext = subIterationContext.SharedContext;
    int ledgerID = sharedContext.DataSourceGL.LedgerID.Value;
    int accountID = subIterationContext.CurrentAccount.AccountID.Value;
    (PX.Objects.GL.Sub Subaccount, int Subindex) tuple;
    if (!sharedContext.Subs.TryGetValue(subDict.Key, out tuple) || !sharedContext.This._historySegments.Contains(new GLHistoryKeyTuple(ledgerID, 0, accountID, subDict.Key)))
      return;
    RMReportReaderGL.BranchIterationContextGL branchIterationContext = new RMReportReaderGL.BranchIterationContextGL(in subIterationContext, tuple.Subaccount, tuple.Subindex, subDict.Value);
    if (sharedContext.ParallelizeBranches)
    {
      Parallel.ForEach<PX.Objects.GL.Branch>((IEnumerable<PX.Objects.GL.Branch>) sharedContext.Branches, sharedContext.ParallelOptions, new Action<PX.Objects.GL.Branch>(branchIterationContext.BranchIterationNoClosures));
    }
    else
    {
      foreach (PX.Objects.GL.Branch branch in sharedContext.Branches)
        RMReportReaderGL.BranchIteration(in branchIterationContext, branch);
    }
  }

  private static void BranchIteration(
    in RMReportReaderGL.BranchIterationContextGL branchIterationContext,
    PX.Objects.GL.Branch currentBranch)
  {
    RMReportReaderGL.SharedContextGL sharedContext = branchIterationContext.SharedContext;
    int num1 = sharedContext.DataSourceGL.LedgerID.Value;
    IReadOnlyCollection<GLHistory> glHistories = (IReadOnlyCollection<GLHistory>) null;
    bool takeLast = false;
    Dictionary<string, GLHistory> periodsForKey;
    if (branchIterationContext.SubDict.TryGetValue((currentBranch.BranchID.Value, num1), out periodsForKey))
      glHistories = sharedContext.This.GetPeriodsToCalculate(periodsForKey, branchIterationContext.CurrentAccount, sharedContext.DataSource, sharedContext.DataSourceGL, out takeLast);
    if (glHistories == null)
      return;
    bool flag = !sharedContext.DataSourceGL.UseMasterCalendar.GetValueOrDefault();
    int num2 = branchIterationContext.CurrentAccount.AccountID.Value;
    int? nullable = branchIterationContext.CurrentSub.SubID;
    int num3 = nullable.Value;
    foreach (GLHistory hist in (IEnumerable<GLHistory>) glHistories)
    {
      hist.FinFlag = new bool?(flag);
      Decimal amount = RMReportReaderGL.GetAmountFromGLHistory(sharedContext.DataSource, branchIterationContext.CurrentAccount, hist, takeLast);
      sharedContext.AddToTotalAmount(in amount);
      if (sharedContext.Drilldown)
      {
        GLHistoryKeyTuple key;
        ref GLHistoryKeyTuple local = ref key;
        int ledgerID = num1;
        nullable = currentBranch.BranchID;
        int branchID = nullable.Value;
        int accountID = num2;
        int subID = num3;
        local = new GLHistoryKeyTuple(ledgerID, branchID, accountID, subID);
        PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup> pxResult;
        lock (sharedContext.DrilldownData)
        {
          if (!sharedContext.DrilldownData.TryGetValue(key, out pxResult))
          {
            pxResult = new PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>(sharedContext.This.GetArmGLHistoryByPeriodRecordForDrilldown(sharedContext.DataSource, hist), branchIterationContext.CurrentAccount, branchIterationContext.CurrentSub, new GLHistory(), sharedContext.GLSetup);
            PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>.op_Implicit(pxResult).FinFlag = new bool?(flag);
            sharedContext.DrilldownData.Add(key, pxResult);
          }
        }
        lock (pxResult)
        {
          hist.FinFlag = new bool?(flag);
          RMReportReaderGL.AggregateGLHistoryForDrillDown(PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>.op_Implicit(pxResult), hist);
        }
      }
      if (sharedContext.DataSource.Expand == "A")
      {
        lock (branchIterationContext.CurrentAccount)
        {
          int accountIndex = branchIterationContext.AccountIndex;
          sharedContext.SplitReturn[accountIndex][2] = (object) ((Decimal) sharedContext.SplitReturn[accountIndex][2] + amount);
          if (sharedContext.SplitReturn[accountIndex][4] == null)
          {
            ARmDataSet armDataSet = new ARmDataSet(sharedContext.DataSet);
            armDataSet[(object) RMReportReaderGL.Keys.StartAccount] = armDataSet[(object) RMReportReaderGL.Keys.EndAccount] = (object) branchIterationContext.CurrentAccount.AccountCD;
            sharedContext.SplitReturn[accountIndex][3] = (object) armDataSet;
            sharedContext.SplitReturn[accountIndex][4] = (object) sharedContext.This._bAccounts.FirstOrDefault<BAccountR>((Func<BAccountR, bool>) (ba =>
            {
              int? baccountId1 = ba.BAccountID;
              int? baccountId2 = currentBranch.BAccountID;
              return baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue;
            })).AcctName;
          }
        }
      }
      else if (sharedContext.DataSource.Expand == "S")
      {
        lock (branchIterationContext.CurrentSub)
        {
          int subIndex = branchIterationContext.SubIndex;
          sharedContext.SplitReturn[subIndex][2] = (object) ((Decimal) sharedContext.SplitReturn[subIndex][2] + amount);
          if (sharedContext.SplitReturn[subIndex][4] == null)
          {
            ARmDataSet armDataSet = new ARmDataSet(sharedContext.DataSet);
            armDataSet[(object) RMReportReaderGL.Keys.StartSub] = armDataSet[(object) RMReportReaderGL.Keys.EndSub] = (object) branchIterationContext.CurrentSub.SubCD;
            sharedContext.SplitReturn[subIndex][3] = (object) armDataSet;
            sharedContext.SplitReturn[subIndex][4] = (object) sharedContext.This._bAccounts.FirstOrDefault<BAccountR>((Func<BAccountR, bool>) (ba =>
            {
              int? baccountId3 = ba.BAccountID;
              int? baccountId4 = currentBranch.BAccountID;
              return baccountId3.GetValueOrDefault() == baccountId4.GetValueOrDefault() & baccountId3.HasValue == baccountId4.HasValue;
            })).AcctName;
          }
        }
      }
    }
  }

  private static void AggregateGLHistoryForDrillDown(GLHistory resulthist, GLHistory hist)
  {
    resulthist.LedgerID = hist.LedgerID;
    resulthist.BranchID = hist.BranchID;
    resulthist.AccountID = hist.AccountID;
    resulthist.SubID = hist.SubID;
    resulthist.FinPeriodID = hist.FinPeriodID;
    if (!resulthist.BegBalance.HasValue)
    {
      resulthist.BegBalance = hist.BegBalance;
      resulthist.PtdDebit = new Decimal?(0M);
      resulthist.PtdCredit = new Decimal?(0M);
    }
    GLHistory glHistory1 = resulthist;
    Decimal? nullable = glHistory1.PtdDebit;
    Decimal? ptdDebit = hist.PtdDebit;
    glHistory1.PtdDebit = nullable.HasValue & ptdDebit.HasValue ? new Decimal?(nullable.GetValueOrDefault() + ptdDebit.GetValueOrDefault()) : new Decimal?();
    GLHistory glHistory2 = resulthist;
    Decimal? ptdCredit = glHistory2.PtdCredit;
    nullable = hist.PtdCredit;
    glHistory2.PtdCredit = ptdCredit.HasValue & nullable.HasValue ? new Decimal?(ptdCredit.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    resulthist.YtdBalance = hist.YtdBalance;
  }

  private ArmGLHistoryByPeriod GetArmGLHistoryByPeriodRecordForDrilldown(
    RMDataSource ds,
    GLHistory hist)
  {
    return new ArmGLHistoryByPeriod()
    {
      LedgerID = hist.LedgerID,
      BranchID = hist.BranchID,
      AccountID = hist.AccountID,
      SubID = hist.SubID,
      FinPeriodID = hist.FinPeriodID,
      LastActivityPeriod = FinPeriodIDFormattingAttribute.FormatForStoringNoTrim($"{ds.AmountType.ToString()}:{this.Base.DrilldownNumber.ToString()}")
    };
  }

  private static Decimal GetAmountFromGLHistory(
    RMDataSource ds,
    PX.Objects.GL.Account account,
    GLHistory hist,
    bool takeLast)
  {
    switch (ds.AmountType.Value)
    {
      case 1:
        if (account.Type == "A" || account.Type == "E")
        {
          Decimal? ptdDebit = hist.PtdDebit;
          Decimal? ptdCredit = hist.PtdCredit;
          return (ptdDebit.HasValue & ptdCredit.HasValue ? new Decimal?(ptdDebit.GetValueOrDefault() - ptdCredit.GetValueOrDefault()) : new Decimal?()).Value;
        }
        Decimal? ptdCredit1 = hist.PtdCredit;
        Decimal? ptdDebit1 = hist.PtdDebit;
        return (ptdCredit1.HasValue & ptdDebit1.HasValue ? new Decimal?(ptdCredit1.GetValueOrDefault() - ptdDebit1.GetValueOrDefault()) : new Decimal?()).Value;
      case 2:
        return hist.PtdCredit.Value;
      case 3:
        return hist.PtdDebit.Value;
      case 4:
        return takeLast ? hist.YtdBalance.Value : hist.BegBalance.Value;
      case 5:
        return hist.YtdBalance.Value;
      case 21:
        if (account.Type == "A" || account.Type == "E")
        {
          Decimal? curyPtdDebit = hist.CuryPtdDebit;
          Decimal? curyPtdCredit = hist.CuryPtdCredit;
          return (curyPtdDebit.HasValue & curyPtdCredit.HasValue ? new Decimal?(curyPtdDebit.GetValueOrDefault() - curyPtdCredit.GetValueOrDefault()) : new Decimal?()).Value;
        }
        Decimal? curyPtdCredit1 = hist.CuryPtdCredit;
        Decimal? curyPtdDebit1 = hist.CuryPtdDebit;
        return (curyPtdCredit1.HasValue & curyPtdDebit1.HasValue ? new Decimal?(curyPtdCredit1.GetValueOrDefault() - curyPtdDebit1.GetValueOrDefault()) : new Decimal?()).Value;
      case 22:
        return hist.CuryPtdCredit.Value;
      case 23:
        return hist.CuryPtdDebit.Value;
      case 24:
        return takeLast ? hist.CuryYtdBalance.Value : hist.CuryBegBalance.Value;
      case 25:
        return hist.CuryYtdBalance.Value;
      default:
        return 0M;
    }
  }

  [PXOverride]
  public string GetUrl(Func<string> del)
  {
    if (!(((PXSelectBase<RMReport>) this.Base.Report).Current.Type == "GL"))
      return del();
    string str = "CS600000";
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(str);
    if (mapNodeByScreenId != null)
      return !Str.Contains(mapNodeByScreenId.Url, "ReportLauncher.aspx", StringComparison.OrdinalIgnoreCase) ? ReportMaint.GetRedirectUrlToReport(str, false, (Dictionary<string, string>) null) : PXUrl.TrimUrl(mapNodeByScreenId.Url);
    throw new PXException("You have insufficient rights to access the object ({0}).", new object[1]
    {
      (object) str
    });
  }

  [PXOverride]
  public bool IsParameter(ARmDataSet ds, string name, ValueBucket value)
  {
    RMReportReaderGL.Keys keys;
    if (!RMReportReaderGL._keysDictionary.TryGetValue(name, out keys))
      return false;
    if (keys == RMReportReaderGL.Keys.OrganizationName)
    {
      object org = ds[(object) RMReportReaderGL.Keys.Organization];
      if (org != null)
      {
        PXAccess.MasterCollection.Organization organization = this._currentUserInformationProvider.GetOrganizations(true, true).FirstOrDefault<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (o => string.Compare(((PXAccess.Organization) o).OrganizationCD, org.ToString(), true) == 0));
        value.Value = organization != null ? (object) ((PXAccess.Organization) organization).OrganizationName : (object) string.Empty;
      }
      return true;
    }
    value.Value = ds[(object) keys];
    bool flag1 = keys == RMReportReaderGL.Keys.StartAccount || keys == RMReportReaderGL.Keys.StartBranch || keys == RMReportReaderGL.Keys.StartSub;
    bool flag2 = keys == RMReportReaderGL.Keys.EndAccount || keys == RMReportReaderGL.Keys.EndBranch || keys == RMReportReaderGL.Keys.EndSub;
    if (flag2 && value.Value == null)
    {
      if (keys == RMReportReaderGL.Keys.EndAccount)
        value.Value = ds[(object) RMReportReaderGL.Keys.StartAccount];
      if (keys == RMReportReaderGL.Keys.EndBranch)
        value.Value = ds[(object) RMReportReaderGL.Keys.StartBranch];
      if (keys == RMReportReaderGL.Keys.EndSub)
        value.Value = ds[(object) RMReportReaderGL.Keys.StartSub];
    }
    if (value.Value is string range && flag1 | flag2)
    {
      string start;
      string end;
      RMReportRange.ParseRangeStartEndPair(range, out start, out end);
      value.Value = flag1 ? (object) start : (object) end;
    }
    return true;
  }

  [PXOverride]
  public ARmDataSet MergeDataSet(
    IEnumerable<ARmDataSet> list,
    string expand,
    MergingMode mode,
    Func<IEnumerable<ARmDataSet>, string, MergingMode, ARmDataSet> del)
  {
    ARmDataSet[] array = list.ToArray<ARmDataSet>();
    ARmDataSet target = del((IEnumerable<ARmDataSet>) array, expand, mode);
    foreach (ARmDataSet source in array)
    {
      if (source != null)
      {
        if (!(target[(object) RMReportReaderGL.Keys.BookCode] is string str1))
          str1 = "";
        if (string.IsNullOrEmpty(str1))
        {
          ARmDataSet armDataSet = target;
          // ISSUE: variable of a boxed type
          __Boxed<RMReportReaderGL.Keys> local = (Enum) RMReportReaderGL.Keys.BookCode;
          if (!(source[(object) RMReportReaderGL.Keys.BookCode] is string str2))
            str2 = "";
          armDataSet[(object) local] = (object) str2;
        }
        if (!(target[(object) RMReportReaderGL.Keys.StartOffset] as int?).HasValue)
          target[(object) RMReportReaderGL.Keys.StartOffset] = (object) (int?) source[(object) RMReportReaderGL.Keys.StartOffset];
        if (!(target[(object) RMReportReaderGL.Keys.StartYearOffset] as int?).HasValue)
          target[(object) RMReportReaderGL.Keys.StartYearOffset] = (object) (int?) source[(object) RMReportReaderGL.Keys.StartYearOffset];
        if (!(target[(object) RMReportReaderGL.Keys.EndOffset] as int?).HasValue)
          target[(object) RMReportReaderGL.Keys.EndOffset] = (object) (int?) source[(object) RMReportReaderGL.Keys.EndOffset];
        if (!(target[(object) RMReportReaderGL.Keys.EndYearOffset] as int?).HasValue)
          target[(object) RMReportReaderGL.Keys.EndYearOffset] = (object) (int?) source[(object) RMReportReaderGL.Keys.EndYearOffset];
        if ((target[(object) RMReportReaderGL.Keys.AmountType] as short?).GetValueOrDefault() == (short) 0)
          target[(object) RMReportReaderGL.Keys.AmountType] = source[(object) RMReportReaderGL.Keys.AmountType];
        ARmDataSet armDataSet1 = target;
        // ISSUE: variable of a boxed type
        __Boxed<RMReportReaderGL.Keys> local1 = (Enum) RMReportReaderGL.Keys.StartPeriod;
        RMReportReader rmReportReader1 = this.Base;
        if (!(target[(object) RMReportReaderGL.Keys.StartPeriod] is string str3))
          str3 = "";
        if (!(source[(object) RMReportReaderGL.Keys.StartPeriod] is string str4))
          str4 = "";
        string str5 = rmReportReader1.MergeMask(str3, str4);
        armDataSet1[(object) local1] = (object) str5;
        ARmDataSet armDataSet2 = target;
        // ISSUE: variable of a boxed type
        __Boxed<RMReportReaderGL.Keys> local2 = (Enum) RMReportReaderGL.Keys.EndPeriod;
        RMReportReader rmReportReader2 = this.Base;
        if (!(target[(object) RMReportReaderGL.Keys.EndPeriod] is string str6))
          str6 = "";
        if (!(source[(object) RMReportReaderGL.Keys.EndPeriod] is string str7))
          str7 = "";
        string str8 = rmReportReader2.MergeMask(str6, str7);
        armDataSet2[(object) local2] = (object) str8;
        if (string.IsNullOrEmpty(target[(object) RMReportReaderGL.Keys.Organization] as string))
        {
          ARmDataSet armDataSet3 = target;
          // ISSUE: variable of a boxed type
          __Boxed<RMReportReaderGL.Keys> local3 = (Enum) RMReportReaderGL.Keys.Organization;
          if (!(source[(object) RMReportReaderGL.Keys.Organization] is string str9))
            str9 = "";
          armDataSet3[(object) local3] = (object) str9;
        }
        if (source[(object) RMReportReaderGL.Keys.UseMasterCalendar] != null)
          target[(object) RMReportReaderGL.Keys.UseMasterCalendar] = source[(object) RMReportReaderGL.Keys.UseMasterCalendar];
        RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderGL.Keys.StartBranch, (object) RMReportReaderGL.Keys.EndBranch, mode);
        RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderGL.Keys.StartAccount, (object) RMReportReaderGL.Keys.EndAccount, mode);
        RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderGL.Keys.StartSub, (object) RMReportReaderGL.Keys.EndSub, mode);
        if (!(target[(object) RMReportReaderGL.Keys.AccountClass] is string str10))
          str10 = "";
        if (string.IsNullOrEmpty(str10))
        {
          ARmDataSet armDataSet4 = target;
          // ISSUE: variable of a boxed type
          __Boxed<RMReportReaderGL.Keys> local4 = (Enum) RMReportReaderGL.Keys.AccountClass;
          if (!(source[(object) RMReportReaderGL.Keys.AccountClass] is string str11))
            str11 = "";
          armDataSet4[(object) local4] = (object) str11;
        }
        if (string.IsNullOrEmpty(target.RowDescription))
          target.RowDescription = source.RowDescription;
      }
    }
    target.Expand = (array.Length == 4 ? array[1] : array[0]).Expand;
    if (target.Expand == "A")
    {
      if (!(target[(object) RMReportReaderGL.Keys.StartAccount] is string str12))
        str12 = "";
      if (string.IsNullOrEmpty(str12))
      {
        if (!(target[(object) RMReportReaderGL.Keys.AccountClass] is string str13))
          str13 = "";
        if (string.IsNullOrEmpty(str13))
          target.Expand = "N";
      }
    }
    return target;
  }

  [PXOverride]
  public virtual List<ARmUnit> ExpandUnit(
    RMDataSource ds,
    ARmUnit unit,
    Func<RMDataSource, ARmUnit, List<ARmUnit>> del)
  {
    if (unit.DataSet.Expand == "A")
    {
      this.GLEnsureInitialized();
      return RMReportUnitExpansion<PX.Objects.GL.Account>.ExpandUnit(this.Base, ds, unit, (object) RMReportReaderGL.Keys.StartAccount, (object) RMReportReaderGL.Keys.EndAccount, new Func<ARmDataSet, List<PX.Objects.GL.Account>>(this.GetItemsInRange<PX.Objects.GL.Account>), (Func<PX.Objects.GL.Account, string>) (account => account.AccountCD), (Func<PX.Objects.GL.Account, string>) (account => account.Description), (Action<PX.Objects.GL.Account, string>) ((account, wildcard) =>
      {
        account.AccountCD = wildcard;
        account.Description = wildcard;
      }));
    }
    if (!(unit.DataSet.Expand == "S"))
      return del(ds, unit);
    this.GLEnsureInitialized();
    return RMReportUnitExpansion<PX.Objects.GL.Sub>.ExpandUnit(this.Base, ds, unit, (object) RMReportReaderGL.Keys.StartSub, (object) RMReportReaderGL.Keys.EndSub, new Func<ARmDataSet, List<PX.Objects.GL.Sub>>(this.GetItemsInRange<PX.Objects.GL.Sub>), (Func<PX.Objects.GL.Sub, string>) (sub => sub.SubCD), (Func<PX.Objects.GL.Sub, string>) (sub => sub.Description), (Action<PX.Objects.GL.Sub, string>) ((sub, wildcard) =>
    {
      sub.SubCD = wildcard;
      sub.Description = wildcard;
    }));
  }

  [PXOverride]
  public void FillDataSource(
    RMDataSource ds,
    ARmDataSet dst,
    string rmType,
    Action<RMDataSource, ARmDataSet, string> del)
  {
    del(ds, dst, rmType);
    this.FillDataSourceInternal(ds, dst, rmType);
  }

  private void FillDataSourceInternal(RMDataSource ds, ARmDataSet dst, string rmType)
  {
    if (ds == null || !ds.DataSourceID.HasValue)
      return;
    RMDataSourceGL extension = ((PXGraph) this.Base).Caches[typeof (RMDataSource)].GetExtension<RMDataSourceGL>((object) ds);
    dst[(object) RMReportReaderGL.Keys.AmountType] = (object) ds.AmountType;
    dst[(object) RMReportReaderGL.Keys.StartBranch] = (object) extension.StartBranch;
    dst[(object) RMReportReaderGL.Keys.EndBranch] = (object) extension.EndBranch;
    if (rmType == "GL")
    {
      object valueExt1 = ((PXSelectBase) this.Base.DataSourceByID).Cache.GetValueExt((object) ds, "LedgerID");
      switch (valueExt1)
      {
        case null:
          object valueExt2 = ((PXSelectBase) this.Base.DataSourceByID).Cache.GetValueExt<RMDataSourceGL.organizationID>((object) ds);
          string empty = string.Empty;
          switch (valueExt2)
          {
            case null:
              dst[(object) RMReportReaderGL.Keys.UseMasterCalendar] = (object) extension.UseMasterCalendar;
              dst[(object) RMReportReaderGL.Keys.EndAccount] = (object) extension.EndAccount;
              dst[(object) RMReportReaderGL.Keys.EndSub] = (object) extension.EndSub;
              dst[(object) RMReportReaderGL.Keys.StartAccount] = (object) extension.StartAccount;
              dst[(object) RMReportReaderGL.Keys.StartSub] = (object) extension.StartSub;
              dst[(object) RMReportReaderGL.Keys.AccountClass] = (object) extension.AccountClassID;
              break;
            case PXFieldState _:
              string name = ((PXFieldState) valueExt2).Name;
              object obj1 = ((PXFieldState) valueExt2).Value;
              if (obj1 is string)
              {
                dst[(object) RMReportReaderGL.Keys.Organization] = (object) (string) obj1;
                goto case null;
              }
              if (obj1 != null)
                throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system.", new object[1]
                {
                  (object) name
                }));
              goto case null;
            case string _:
              dst[(object) RMReportReaderGL.Keys.Organization] = (object) (string) valueExt2;
              goto case null;
            default:
              throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system.", new object[1]
              {
                (object) empty
              }));
          }
          break;
        case PXFieldState _:
          object obj2 = ((PXFieldState) valueExt1).Value;
          if (obj2 is string)
          {
            dst[(object) RMReportReaderGL.Keys.BookCode] = (object) (string) obj2;
            goto case null;
          }
          if (obj2 != null)
            throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system.", new object[1]
            {
              (object) "LedgerID"
            }));
          goto case null;
        case string _:
          dst[(object) RMReportReaderGL.Keys.BookCode] = (object) (string) valueExt1;
          goto case null;
        default:
          throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system.", new object[1]
          {
            (object) "LedgerID"
          }));
      }
    }
    dst.Expand = ds.Expand;
    dst.RowDescription = ds.RowDescription;
    ARmDataSet armDataSet1 = dst;
    // ISSUE: variable of a boxed type
    __Boxed<RMReportReaderGL.Keys> local1 = (Enum) RMReportReaderGL.Keys.EndOffset;
    short? endPeriodOffset = extension.EndPeriodOffset;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local2 = (ValueType) (endPeriodOffset.HasValue ? new int?((int) endPeriodOffset.GetValueOrDefault()) : new int?());
    armDataSet1[(object) local1] = (object) local2;
    ARmDataSet armDataSet2 = dst;
    // ISSUE: variable of a boxed type
    __Boxed<RMReportReaderGL.Keys> local3 = (Enum) RMReportReaderGL.Keys.EndYearOffset;
    short? nullable = extension.EndPeriodYearOffset;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local4 = (ValueType) (nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?());
    armDataSet2[(object) local3] = (object) local4;
    string endPeriod = extension.EndPeriod;
    if (!string.IsNullOrEmpty(endPeriod))
      dst[(object) RMReportReaderGL.Keys.EndPeriod] = (object) ((endPeriod.Length > 4 ? (endPeriod.Substring(4) + "  ").Substring(0, 2) : "  ") + (endPeriod.Length > 4 ? endPeriod.Substring(0, 4) : endPeriod));
    ARmDataSet armDataSet3 = dst;
    // ISSUE: variable of a boxed type
    __Boxed<RMReportReaderGL.Keys> local5 = (Enum) RMReportReaderGL.Keys.StartOffset;
    nullable = extension.StartPeriodOffset;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local6 = (ValueType) (nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?());
    armDataSet3[(object) local5] = (object) local6;
    ARmDataSet armDataSet4 = dst;
    // ISSUE: variable of a boxed type
    __Boxed<RMReportReaderGL.Keys> local7 = (Enum) RMReportReaderGL.Keys.StartYearOffset;
    nullable = extension.StartPeriodYearOffset;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local8 = (ValueType) (nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?());
    armDataSet4[(object) local7] = (object) local8;
    string startPeriod = extension.StartPeriod;
    if (string.IsNullOrEmpty(startPeriod))
      return;
    dst[(object) RMReportReaderGL.Keys.StartPeriod] = (object) ((startPeriod.Length > 4 ? (startPeriod.Substring(4) + "  ").Substring(0, 2) : "  ") + (startPeriod.Length > 4 ? startPeriod.Substring(0, 4) : startPeriod));
  }

  [PXOverride]
  public virtual IEnumerable units(string parentCode, Func<string, IEnumerable> basemethod)
  {
    RMReportReaderGL rmReportReaderGl = this;
    foreach (PXResult<RMUnit> pxResult in basemethod(parentCode))
    {
      int? organizationId = PXCacheEx.GetExtension<RMDataSourceGL>((IBqlTable) ((PXResult) pxResult).GetItem<RMDataSource>()).OrganizationID;
      if (!organizationId.HasValue)
        yield return (object) pxResult;
      else if (((IQueryable<PXResult<PX.Objects.GL.DAC.Organization>>) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>, And<MatchWithOrganization<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select((PXGraph) rmReportReaderGl.Base, new object[1]
      {
        (object) organizationId
      })).Any<PXResult<PX.Objects.GL.DAC.Organization>>())
        yield return (object) pxResult;
    }
  }

  [PXOverride]
  public ARmReport GetReport(Func<ARmReport> del)
  {
    ARmReport ar = del();
    int? styleId = ((PXSelectBase<RMReport>) this.Base.Report).Current.StyleID;
    if (styleId.HasValue)
      this.Base.fillStyle(((PXSelectBase<RMStyle>) this.Base.StyleByID).SelectSingle(new object[1]
      {
        (object) styleId
      }), ar.Style);
    int? dataSourceId = ((PXSelectBase<RMReport>) this.Base.Report).Current.DataSourceID;
    if (dataSourceId.HasValue)
      this.FillDataSourceInternal(((PXSelectBase<RMDataSource>) this.Base.DataSourceByID).SelectSingle(new object[1]
      {
        (object) dataSourceId
      }), ar.DataSet, ar.Type);
    List<ARmReport.ARmReportParameter> armParams = ar.ARmParams;
    RMReportPM extension1 = ((PXSelectBase) this.Base.Report).Cache.GetExtension<RMReportPM>((object) ((PXSelectBase<RMReport>) this.Base.Report).Current);
    string str1 = string.Empty;
    string str2 = string.Empty;
    string str3 = string.Empty;
    bool? nullable;
    if (ar.Type == "GL")
    {
      RMReportGL extension2 = ((PXSelectBase) this.Base.Report).Cache.GetExtension<RMReportGL>((object) ((PXSelectBase<RMReport>) this.Base.Report).Current);
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt<RMDataSourceGL.organizationID>((object) null) is PXFieldState stateExt1 && !string.IsNullOrEmpty(stateExt1.ViewName))
      {
        str1 = stateExt1.ViewName;
        str3 = stateExt1.Name;
        if (stateExt1 is PXStringState)
          str2 = ((PXStringState) stateExt1).InputMask;
      }
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.Organization, str3, Messages.GetLocal("Company"), ar.DataSet[(object) RMReportReaderGL.Keys.Organization] as string, extension2.RequestOrganizationID.GetValueOrDefault(), 2, str1, str2, armParams);
      if (PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() && ((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt<RMDataSourceGL.useMasterCalendar>((object) null) is PXFieldState stateExt2)
      {
        str3 = stateExt2.Name;
        if (stateExt2.Value == null)
          stateExt2.Value = (object) true;
        RMReportReader rmReportReader = this.Base;
        // ISSUE: variable of a boxed type
        __Boxed<RMReportReaderGL.Keys> local1 = (Enum) RMReportReaderGL.Keys.UseMasterCalendar;
        string str4 = str3;
        string local2 = Messages.GetLocal("Use Master Calendar");
        nullable = ar.DataSet[(object) RMReportReaderGL.Keys.UseMasterCalendar] as bool?;
        int num1 = (int) nullable ?? ((bool) stateExt2.Value ? 1 : 0);
        nullable = extension2.RequestUseMasterCalendar;
        int num2 = nullable.GetValueOrDefault() ? 1 : 0;
        List<ARmReport.ARmReportParameter> armReportParameterList = armParams;
        rmReportReader.CreateParameter((object) local1, str4, local2, num1 != 0, num2 != 0, 2, (string) null, (string) null, armReportParameterList);
      }
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt<RMDataSourceGL.startBranch>((object) null) is PXFieldState stateExt3 && !string.IsNullOrEmpty(stateExt3.ViewName))
      {
        str1 = stateExt3.ViewName;
        str3 = stateExt3.Name;
        if (stateExt3 is PXStringState)
          str2 = ((PXStringState) stateExt3).InputMask;
      }
      RMReportReader rmReportReader1 = this.Base;
      // ISSUE: variable of a boxed type
      __Boxed<RMReportReaderGL.Keys> local3 = (Enum) RMReportReaderGL.Keys.StartBranch;
      string str5 = str3;
      string local4 = Messages.GetLocal("Start Branch :");
      string data1 = ar.DataSet[(object) RMReportReaderGL.Keys.StartBranch] as string;
      nullable = extension2.RequestStartBranch;
      int num3 = nullable.GetValueOrDefault() ? 1 : 0;
      string str6 = str1;
      string str7 = str2;
      List<ARmReport.ARmReportParameter> armReportParameterList1 = armParams;
      rmReportReader1.CreateParameter((object) local3, str5, local4, data1, num3 != 0, 2, str6, str7, armReportParameterList1);
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt<RMDataSourceGL.endBranch>((object) null) is PXFieldState stateExt4 && !string.IsNullOrEmpty(stateExt4.ViewName))
      {
        str1 = stateExt4.ViewName;
        str3 = stateExt4.Name;
        if (stateExt4 is PXStringState)
          str2 = ((PXStringState) stateExt4).InputMask;
      }
      RMReportReader rmReportReader2 = this.Base;
      // ISSUE: variable of a boxed type
      __Boxed<RMReportReaderGL.Keys> local5 = (Enum) RMReportReaderGL.Keys.EndBranch;
      string str8 = str3;
      string local6 = Messages.GetLocal("End Branch :");
      string data2 = ar.DataSet[(object) RMReportReaderGL.Keys.EndBranch] as string;
      nullable = extension2.RequestEndBranch;
      int num4 = nullable.GetValueOrDefault() ? 1 : 0;
      string str9 = str1;
      string str10 = str2;
      List<ARmReport.ARmReportParameter> armReportParameterList2 = armParams;
      rmReportReader2.CreateParameter((object) local5, str8, local6, data2, num4 != 0, 2, str9, str10, armReportParameterList2);
    }
    short? requestEndPeriod = extension1.RequestEndPeriod;
    bool bSinglePeriod = (requestEndPeriod.HasValue ? new int?((int) requestEndPeriod.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 2;
    requestEndPeriod = extension1.RequestEndPeriod;
    bool bRequestEndPeriod = requestEndPeriod.GetValueOrDefault() > (short) 0;
    nullable = extension1.RequestStartPeriod;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    if (valueOrDefault1)
    {
      if (!(ar.DataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str11))
        str11 = "";
      if (str11.TrimEnd() == "")
      {
        try
        {
          ar.DataSet[(object) RMReportReaderGL.Keys.StartPeriod] = (object) (string) ((SoapNavigator.DATA) this.Base.GetExprContext()).GetDefExt((object) "RowBatch.FinPeriodID");
        }
        catch
        {
        }
      }
    }
    if (bRequestEndPeriod)
    {
      if (!(ar.DataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str12))
        str12 = "";
      if (str12.TrimEnd() == "")
      {
        try
        {
          ar.DataSet[(object) RMReportReaderGL.Keys.EndPeriod] = (object) (string) ((SoapNavigator.DATA) this.Base.GetExprContext()).GetDefExt((object) "RowBatch.FinPeriodID");
        }
        catch
        {
        }
      }
    }
    string sViewNameStartEndPeriod = string.Empty;
    string sInputMaskStartEndPeriod = string.Empty;
    if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt<RMDataSourceGL.startPeriod>((object) null) is PXFieldState stateExt5 && !string.IsNullOrEmpty(stateExt5.ViewName))
    {
      sViewNameStartEndPeriod = stateExt5.ViewName;
      if (stateExt5 is PXStringState)
        sInputMaskStartEndPeriod = ((PXStringState) stateExt5).InputMask;
    }
    if (ar.Type == "GL")
    {
      RMReportGL extension3 = ((PXSelectBase) this.Base.Report).Cache.GetExtension<RMReportGL>((object) ((PXSelectBase<RMReport>) this.Base.Report).Current);
      string str13;
      string str14 = str13 = string.Empty;
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt((object) null, "LedgerID") is PXFieldState stateExt6 && !string.IsNullOrEmpty(stateExt6.ViewName))
      {
        str14 = stateExt6.ViewName;
        if (stateExt6 is PXStringState)
          str13 = ((PXStringState) stateExt6).InputMask;
      }
      RMReportReader rmReportReader3 = this.Base;
      // ISSUE: variable of a boxed type
      __Boxed<RMReportReaderGL.Keys> local7 = (Enum) RMReportReaderGL.Keys.BookCode;
      string local8 = Messages.GetLocal("Ledger :");
      string data3 = ar.DataSet[(object) RMReportReaderGL.Keys.BookCode] as string;
      nullable = extension3.RequestLedgerID;
      int num5 = nullable.GetValueOrDefault() ? 1 : 0;
      string str15 = str14;
      string str16 = str13;
      List<ARmReport.ARmReportParameter> armReportParameterList3 = armParams;
      rmReportReader3.CreateParameter((object) local7, "BookCode", local8, data3, num5 != 0, 2, str15, str16, armReportParameterList3);
      this.AddStartAndEndPeriodParameters(ar, armParams, bSinglePeriod, bRequestEndPeriod, valueOrDefault1, sViewNameStartEndPeriod, sInputMaskStartEndPeriod);
      nullable = extension3.RequestEndAccount;
      bool valueOrDefault2 = nullable.GetValueOrDefault();
      string str17;
      string str18 = str17 = string.Empty;
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt((object) null, "StartAccount") is PXFieldState stateExt7 && !string.IsNullOrEmpty(stateExt7.ViewName))
      {
        str18 = stateExt7.ViewName;
        if (stateExt7 is PXStringState)
          str17 = ((PXStringState) stateExt7).InputMask;
      }
      RMReportReader rmReportReader4 = this.Base;
      // ISSUE: variable of a boxed type
      __Boxed<RMReportReaderGL.Keys> local9 = (Enum) RMReportReaderGL.Keys.StartAccount;
      string local10 = Messages.GetLocal("Start Account :");
      string data4 = ar.DataSet[(object) RMReportReaderGL.Keys.StartAccount] as string;
      nullable = extension3.RequestStartAccount;
      int num6 = nullable.GetValueOrDefault() ? 1 : 0;
      string str19 = str18;
      string str20 = str17;
      List<ARmReport.ARmReportParameter> armReportParameterList4 = armParams;
      rmReportReader4.CreateParameter((object) local9, "StartAccount", local10, data4, num6 != 0, 2, str19, str20, armReportParameterList4);
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.EndAccount, "EndAccount", Messages.GetLocal("End Account :"), ar.DataSet[(object) RMReportReaderGL.Keys.EndAccount] as string, valueOrDefault2, 2, str18, str17, armParams);
      nullable = extension3.RequestEndSub;
      bool valueOrDefault3 = nullable.GetValueOrDefault();
      string str21;
      string str22 = str21 = string.Empty;
      if (((PXSelectBase) this.Base.Report).Cache.GetStateExt((object) null, "SubCD") is PXFieldState stateExt8 && !string.IsNullOrEmpty(stateExt8.ViewName))
      {
        str22 = stateExt8.ViewName;
        if (stateExt8 is PXStringState)
          str21 = ((PXStringState) stateExt8).InputMask;
      }
      RMReportReader rmReportReader5 = this.Base;
      // ISSUE: variable of a boxed type
      __Boxed<RMReportReaderGL.Keys> local11 = (Enum) RMReportReaderGL.Keys.StartSub;
      string local12 = Messages.GetLocal("Start Sub :");
      string data5 = ar.DataSet[(object) RMReportReaderGL.Keys.StartSub] as string;
      nullable = extension3.RequestStartSub;
      int num7 = nullable.GetValueOrDefault() ? 1 : 0;
      string str23 = str22;
      string str24 = str21;
      List<ARmReport.ARmReportParameter> armReportParameterList5 = armParams;
      rmReportReader5.CreateParameter((object) local11, "StartSub", local12, data5, num7 != 0, 2, str23, str24, armReportParameterList5);
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.EndSub, "EndSub", Messages.GetLocal("End Sub :"), ar.DataSet[(object) RMReportReaderGL.Keys.EndSub] as string, valueOrDefault3, 2, str22, str21, armParams);
      nullable = extension3.RequestAccountClassID;
      bool valueOrDefault4 = nullable.GetValueOrDefault();
      string str25;
      string str26 = str25 = string.Empty;
      if (((PXSelectBase) this.Base.DataSourceByID).Cache.GetStateExt((object) null, "AccountClassID") is PXFieldState stateExt9 && !string.IsNullOrEmpty(stateExt9.ViewName))
      {
        str26 = stateExt9.ViewName;
        if (stateExt9 is PXStringState)
          str25 = ((PXStringState) stateExt9).InputMask;
      }
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.AccountClass, "AccountClass", Messages.GetLocal("Account Class :"), ar.DataSet[(object) RMReportReaderGL.Keys.AccountClass] as string, valueOrDefault4, 2, str26, str25, armParams);
    }
    else
      this.AddStartAndEndPeriodParameters(ar, armParams, bSinglePeriod, bRequestEndPeriod, valueOrDefault1, sViewNameStartEndPeriod, sInputMaskStartEndPeriod);
    return ar;
  }

  private void AddStartAndEndPeriodParameters(
    ARmReport ar,
    List<ARmReport.ARmReportParameter> aRp,
    bool bSinglePeriod,
    bool bRequestEndPeriod,
    bool bRequestStartPeriod,
    string sViewNameStartEndPeriod,
    string sInputMaskStartEndPeriod)
  {
    string format = string.Join(",", "= Report.GetFieldSchema('RMDataSource.{0}", "organizationID", "useMasterCalendar", "useMasterCalendar')");
    if (!bSinglePeriod & bRequestEndPeriod)
    {
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.StartPeriod, "StartPeriod", Messages.GetLocal("Start Financial Period :"), ar.DataSet[(object) RMReportReaderGL.Keys.StartPeriod] as string, bRequestStartPeriod, 2, string.Format(format, (object) "StartPeriod"), sInputMaskStartEndPeriod, aRp);
      this.Base.CreateParameter((object) RMReportReaderGL.Keys.EndPeriod, "EndPeriod", Messages.GetLocal("End Financial Period :"), ar.DataSet[(object) RMReportReaderGL.Keys.EndPeriod] as string, bRequestEndPeriod, 2, string.Format(format, (object) "EndPeriod"), sInputMaskStartEndPeriod, aRp);
    }
    else
      this.Base.CreateParameter((object) new object[2]
      {
        (object) RMReportReaderGL.Keys.StartPeriod,
        (object) RMReportReaderGL.Keys.EndPeriod
      }, "StartPeriod", Messages.GetLocal("Financial Period :"), ar.DataSet[(object) RMReportReaderGL.Keys.StartPeriod] as string, (bRequestStartPeriod ? 1 : 0) != 0, 2, string.Format(format, (object) "StartPeriod"), sInputMaskStartEndPeriod, aRp);
  }

  public enum Keys
  {
    AmountType,
    StartBranch,
    EndBranch,
    BookCode,
    EndAccount,
    EndSub,
    StartAccount,
    StartSub,
    AccountClass,
    EndOffset,
    EndYearOffset,
    EndPeriod,
    StartOffset,
    StartYearOffset,
    StartPeriod,
    Organization,
    OrganizationName,
    UseMasterCalendar,
  }

  /// <summary>
  /// A local GL reports' context used during iteration over sub accounts.
  /// </summary>
  private readonly struct SubIterationContextGL(
    RMReportReaderGL.SharedContextGL sharedContext,
    PX.Objects.GL.Account currentAccount,
    int accountIndex,
    NestedDictionary<int, (int BranchID, int LedgerID), Dictionary<string, GLHistory>> accountDict)
  {
    public RMReportReaderGL.SharedContextGL SharedContext { get; } = sharedContext;

    public PX.Objects.GL.Account CurrentAccount { get; } = currentAccount;

    public int AccountIndex { get; } = accountIndex;

    public NestedDictionary<int, (int BranchID, int LedgerID), Dictionary<string, GLHistory>> AccountDict { get; } = accountDict;

    public void SubIterationNoClosures(
      KeyValuePair<int, Dictionary<(int BranchID, int LedgerID), Dictionary<string, GLHistory>>> subDict)
    {
      RMReportReaderGL.SubIteration(in this, subDict);
    }
  }

  /// <summary>
  /// A local GL reports' context used during iteration over branches.
  /// </summary>
  private readonly struct BranchIterationContextGL
  {
    public RMReportReaderGL.SharedContextGL SharedContext { get; }

    public PX.Objects.GL.Account CurrentAccount { get; }

    public int AccountIndex { get; }

    public PX.Objects.GL.Sub CurrentSub { get; }

    public int SubIndex { get; }

    public Dictionary<(int BranchID, int LedgerID), Dictionary<string, GLHistory>> SubDict { get; }

    public BranchIterationContextGL(
      in RMReportReaderGL.SubIterationContextGL subIterationContext,
      PX.Objects.GL.Sub currentSub,
      int subIndex,
      Dictionary<(int BranchID, int LedgerID), Dictionary<string, GLHistory>> subDict)
    {
      this.SharedContext = subIterationContext.SharedContext;
      this.CurrentAccount = subIterationContext.CurrentAccount;
      this.AccountIndex = subIterationContext.AccountIndex;
      this.CurrentSub = currentSub;
      this.SubIndex = subIndex;
      this.SubDict = subDict;
    }

    public void BranchIterationNoClosures(PX.Objects.GL.Branch currentBranch)
    {
      RMReportReaderGL.BranchIteration(in this, currentBranch);
    }
  }

  /// <summary>
  /// A shared GL reports' context used during parallel calculations of the cell value.
  /// </summary>
  private class SharedContextGL
  {
    private readonly object _locker = new object();
    private Decimal _totalAmount;

    public Decimal TotalAmount => this._totalAmount;

    public RMReportReaderGL This { get; }

    public RMDataSource DataSource { get; }

    public RMDataSourceGL DataSourceGL { get; }

    public ARmDataSet DataSet { get; }

    public bool Drilldown { get; }

    public Dictionary<GLHistoryKeyTuple, PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>> DrilldownData { get; }

    public GLSetup GLSetup { get; }

    public List<object[]> SplitReturn { get; }

    public List<PX.Objects.GL.Account> Accounts { get; }

    public Dictionary<int, (PX.Objects.GL.Sub Subaccount, int Subindex)> Subs { get; } = new Dictionary<int, (PX.Objects.GL.Sub, int)>();

    public List<PX.Objects.GL.Branch> Branches { get; }

    public bool ParallelizeAccounts { get; }

    public bool ParallelizeSubs { get; }

    public bool ParallelizeBranches { get; }

    public ParallelOptions ParallelOptions { get; }

    public SharedContextGL(
      RMReportReaderGL @this,
      bool drillDown,
      RMDataSource ds,
      RMDataSourceGL dsGL,
      ARmDataSet dataSet,
      List<PX.Objects.GL.Account> accounts,
      List<PX.Objects.GL.Sub> subs,
      List<PX.Objects.GL.Branch> branchList,
      List<object[]> splitret)
    {
      this.This = @this;
      this.Drilldown = drillDown;
      this.DataSource = ds;
      this.DataSourceGL = dsGL;
      this.DataSet = dataSet;
      this.Accounts = accounts;
      for (int index = 0; index < subs.Count; ++index)
      {
        PX.Objects.GL.Sub sub = subs[index];
        this.Subs.Add(sub.SubID.Value, (sub, index));
      }
      this.Branches = branchList;
      this.SplitReturn = splitret;
      (int, bool) parallelCalculation = this.This.Base.GetThreadsCountForCellValueParallelCalculation();
      int workerThreadsCount = parallelCalculation.Item1;
      int num = parallelCalculation.Item2 ? -1 : workerThreadsCount;
      this.ParallelOptions = new ParallelOptions()
      {
        MaxDegreeOfParallelism = num
      };
      if (this.Drilldown)
      {
        this.DrilldownData = new Dictionary<GLHistoryKeyTuple, PXResult<ArmGLHistoryByPeriod, PX.Objects.GL.Account, PX.Objects.GL.Sub, GLHistory, GLSetup>>();
        this.GLSetup = PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelect<GLSetup>.Config>.Select((PXGraph) this.This.Base, Array.Empty<object>()));
      }
      (this.ParallelizeAccounts, this.ParallelizeSubs, this.ParallelizeBranches) = this.DetermineParallelizationOptions(workerThreadsCount);
    }

    /// <summary>
    /// Determine parallelization options considering that we want no more than one nested parallel loop with good parallelization on worker threads.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">Thrown when the number of worker threads is less than 1.</exception>
    /// <param name="workerThreadsCount">Number of worker threads.</param>
    /// <returns>Parallelization options.</returns>
    private (bool ParallelizeAccounts, bool ParallelizeSubs, bool ParallelizeBranches) DetermineParallelizationOptions(
      int workerThreadsCount)
    {
      if (workerThreadsCount <= 0)
        throw new InvalidOperationException("The number of worker threads cannot be less than one");
      if (WebConfig.ParallelizeAllDimensionsInArmReports)
        return (true, true, true);
      if (workerThreadsCount == 1)
        return (false, false, false);
      if (this.Accounts.Count >= workerThreadsCount)
        return (true, false, false);
      if (this.Subs.Count >= workerThreadsCount)
        return (false, true, false);
      if (this.Branches.Count >= workerThreadsCount)
        return (false, false, true);
      bool flag1 = this.Accounts.Count > 1;
      bool flag2 = this.Subs.Count > 1 && !flag1;
      bool flag3 = this.Branches.Count > 1 && !flag1 && !flag2;
      return (flag1, flag2, flag3);
    }

    public void AccountIterationNoClosures(int accountIndex)
    {
      RMReportReaderGL.AccountIteration(this, accountIndex);
    }

    public void AddToTotalAmount(in Decimal amount)
    {
      if (amount == 0M)
        return;
      lock (this._locker)
        this._totalAmount += amount;
    }
  }
}
