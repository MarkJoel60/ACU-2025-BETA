// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.StatementCycleProcessBO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.CustomerStatements;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Aging;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
public class StatementCycleProcessBO : PXGraph<
#nullable disable
StatementCycleProcessBO>
{
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSelect<ARStatementCycle, Where<ARStatementCycle.statementCycleId, Equal<Required<ARStatementCycle.statementCycleId>>>> CyclesList;
  public PXSelect<ARRegister> Register;

  public StatementCycleProcessBO()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public static void ProcessCycles(StatementCycleProcessBO graph, ARStatementCycle aCycle)
  {
    if (!aCycle.NextStmtDate.HasValue)
      return;
    ((PXGraph) graph).Clear();
    ARStatementCycle cycle = PXResultset<ARStatementCycle>.op_Implicit(((PXSelectBase<ARStatementCycle>) graph.CyclesList).Select(new object[1]
    {
      (object) aCycle.StatementCycleId
    }));
    DateTime statementDate = aCycle.NextStmtDate ?? ((PXGraph) graph).Accessinfo.BusinessDate.Value;
    PXProcessing<ARStatementCycle>.SetCurrentItem((object) aCycle);
    graph.GenerateStatement(cycle, statementDate);
  }

  public static void RegenerateLastStatement(StatementCycleProcessBO graph, ARStatementCycle aCycle)
  {
    ((PXGraph) graph).Clear();
    ARStatementCycle cycle = PXResultset<ARStatementCycle>.op_Implicit(((PXSelectBase<ARStatementCycle>) graph.CyclesList).Select(new object[1]
    {
      (object) aCycle.StatementCycleId
    }));
    if (!cycle.LastStmtDate.HasValue)
      return;
    DateTime statementDate = cycle.LastStmtDate.Value;
    graph.RegenerateLastStatement(cycle, statementDate);
  }

  public static void DeleteLastStatement(StatementCycleProcessBO graph, ARStatementCycle aCycle)
  {
    ((PXGraph) graph).Clear();
    ARStatementCycle cycle = PXResultset<ARStatementCycle>.op_Implicit(((PXSelectBase<ARStatementCycle>) graph.CyclesList).Select(new object[1]
    {
      (object) aCycle.StatementCycleId
    }));
    if (!cycle.LastStmtDate.HasValue)
      return;
    DateTime deleteStatementDate = cycle.LastStmtDate.Value;
    graph.DeleteLastStatementExec(cycle, deleteStatementDate);
  }

  private static IEnumerable<Customer> GetStatementCustomers(
    PXGraph graph,
    IEnumerable<Customer> customers)
  {
    List<Customer> statementCustomers = new List<Customer>();
    foreach (int? nullable in customers.Select<Customer, int?>((Func<Customer, int?>) (customer => customer.StatementCustomerID)).Distinct<int?>())
    {
      IEnumerable<Customer> collection = GraphHelper.RowCast<Customer>((IEnumerable) PXSelectBase<Customer, PXSelect<Customer, Where<Customer.statementCustomerID, Equal<Required<Customer.statementCustomerID>>>>.Config>.Select(graph, new object[1]
      {
        (object) nullable
      }));
      statementCustomers.AddRange(collection);
    }
    return (IEnumerable<Customer>) statementCustomers;
  }

  private static Dictionary<int, List<Customer>> PreloadPrependFamilyCustomersFromPreviousStatement(
    PXGraph graph,
    IEnumerable<Customer> customers,
    DateTime previousStatementDate)
  {
    int[] array1 = customers.Select<Customer, int>((Func<Customer, int>) (customer => customer.BAccountID.Value)).ToArray<int>();
    (Customer, ARStatement, ARStatementDetail)[] array2 = ((IEnumerable<PXResult<Customer>>) PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARStatement>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<ARStatement.customerID>>>>, And<BqlOperand<ARStatement.statementDate, IBqlDateTime>.IsEqual<P.AsDateTime.UTC>>>>.And<BqlOperand<ARStatement.statementCustomerID, IBqlInt>.IsIn<P.AsInt>>>>, FbqlJoins.Left<ARStatementDetail>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatementDetail.statementDate, Equal<P.AsDateTime.UTC>>>>, And<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<ARStatementDetail.customerID>>>, And<BqlOperand<ARStatement.branchID, IBqlInt>.IsEqual<ARStatementDetail.branchID>>>>.And<BqlOperand<ARStatementDetail.customerID, IBqlInt>.IsIn<P.AsInt>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.statementDate, IsNotNull>>>>.Or<BqlOperand<ARStatementDetail.statementDate, IBqlDateTime>.IsNotNull>>>.Config>.Select(graph, new object[4]
    {
      (object) previousStatementDate,
      (object) array1,
      (object) previousStatementDate,
      (object) array1
    })).AsEnumerable<PXResult<Customer>>().Select<PXResult<Customer>, (Customer, ARStatement, ARStatementDetail)>((Func<PXResult<Customer>, (Customer, ARStatement, ARStatementDetail)>) (_ => (((PXResult) _).GetItem<Customer>(), ((PXResult) _).GetItem<ARStatement>(), ((PXResult) _).GetItem<ARStatementDetail>()))).ToArray<(Customer, ARStatement, ARStatementDetail)>();
    int[] array3 = ((IEnumerable<(Customer, ARStatement, ARStatementDetail)>) array2).Where<(Customer, ARStatement, ARStatementDetail)>((Func<(Customer, ARStatement, ARStatementDetail), bool>) (_ => _.Statement != null && _.Statement.StatementCustomerID.HasValue)).Select<(Customer, ARStatement, ARStatementDetail), int>((Func<(Customer, ARStatement, ARStatementDetail), int>) (_ => _.Statement.StatementCustomerID.Value)).Union<int>(((IEnumerable<(Customer, ARStatement, ARStatementDetail)>) array2).Where<(Customer, ARStatement, ARStatementDetail)>((Func<(Customer, ARStatement, ARStatementDetail), bool>) (_ => _.StatementDetail != null && _.StatementDetail.CustomerID.HasValue)).Select<(Customer, ARStatement, ARStatementDetail), int>((Func<(Customer, ARStatement, ARStatementDetail), int>) (_ => _.StatementDetail.CustomerID.Value))).Distinct<int>().ToArray<int>();
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    Dictionary<int, Customer> dictionary1 = ((IQueryable<PXResult<Customer>>) PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsIn<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) array3
    })).Select<PXResult<Customer>, Customer>(Expression.Lambda<Func<PXResult<Customer>, Customer>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).AsEnumerable<Customer>().Distinct<Customer>().ToDictionary<Customer, int, Customer>((Func<Customer, int>) (k => k.BAccountID.Value), (Func<Customer, Customer>) (v => v));
    Dictionary<int, List<Customer>> source = new Dictionary<int, List<Customer>>();
    foreach ((Customer, ARStatement, ARStatementDetail) tuple in array2)
    {
      Dictionary<int, List<Customer>> dictionary2 = source;
      int? nullable = tuple.Item1.BAccountID;
      int key1 = nullable.Value;
      if (!dictionary2.ContainsKey(key1))
      {
        Dictionary<int, List<Customer>> dictionary3 = source;
        nullable = tuple.Item1.BAccountID;
        int key2 = nullable.Value;
        List<Customer> customerList = new List<Customer>();
        dictionary3.Add(key2, customerList);
        Dictionary<int, List<Customer>> dictionary4 = source;
        nullable = tuple.Item1.BAccountID;
        int key3 = nullable.Value;
        dictionary4[key3].Add(tuple.Item1);
      }
      if (tuple.Item2 != null)
      {
        nullable = tuple.Item2.StatementCustomerID;
        if (nullable.HasValue)
        {
          Dictionary<int, List<Customer>> dictionary5 = source;
          nullable = tuple.Item2.StatementCustomerID;
          int key4 = nullable.Value;
          if (!dictionary5.ContainsKey(key4))
          {
            Dictionary<int, List<Customer>> dictionary6 = source;
            nullable = tuple.Item2.StatementCustomerID;
            int key5 = nullable.Value;
            List<Customer> customerList = new List<Customer>();
            dictionary6.Add(key5, customerList);
          }
          Dictionary<int, List<Customer>> dictionary7 = source;
          nullable = tuple.Item2.StatementCustomerID;
          int key6 = nullable.Value;
          dictionary7[key6].Add(tuple.Item1);
        }
      }
      if (tuple.Item3 != null)
      {
        nullable = tuple.Item3.CustomerID;
        if (nullable.HasValue)
        {
          Dictionary<int, List<Customer>> dictionary8 = source;
          nullable = tuple.Item1.BAccountID;
          int key7 = nullable.Value;
          List<Customer> customerList = dictionary8[key7];
          Dictionary<int, Customer> dictionary9 = dictionary1;
          nullable = tuple.Item3.CustomerID;
          int key8 = nullable.Value;
          Customer customer = dictionary9[key8];
          customerList.Add(customer);
        }
      }
    }
    return source.ToDictionary<KeyValuePair<int, List<Customer>>, int, List<Customer>>((Func<KeyValuePair<int, List<Customer>>, int>) (k => k.Key), (Func<KeyValuePair<int, List<Customer>>, List<Customer>>) (v => v.Value.Distinct<Customer>((IEqualityComparer<Customer>) new StatementCycleProcessBO.CustomerIDComparer()).ToList<Customer>()));
  }

  /// <summary>
  /// After parent-child relationship is broken and a user wants to regenerate last statement
  /// for a customer that was in the family earlier, this method ensures that customers that
  /// "were family" at the moment of previous statement generation will get into the family
  /// for regeneration.
  /// </summary>
  /// <param name="customerFamily">
  /// The customer family collected on the basis of parent-child links between customers.
  /// </param>
  private static ICollection<Customer> PrependFamilyCustomersFromPreviousStatement(
    PXGraph graph,
    IEnumerable<Customer> customerFamily,
    DateTime previousStatementDate,
    Dictionary<int, List<Customer>> preloaded)
  {
    customerFamily.Select<Customer, int>((Func<Customer, int>) (customer => customer.BAccountID.Value)).ToArray<int>();
    List<Customer> r = new List<Customer>();
    foreach (Customer customer in customerFamily)
    {
      Dictionary<int, List<Customer>> dictionary1 = preloaded;
      int? baccountId = customer.BAccountID;
      int key1 = baccountId.Value;
      if (dictionary1.ContainsKey(key1))
      {
        Dictionary<int, List<Customer>> dictionary2 = preloaded;
        baccountId = customer.BAccountID;
        int key2 = baccountId.Value;
        dictionary2[key2].ForEach((Action<Customer>) (_ => r.Add(_)));
      }
    }
    return (ICollection<Customer>) r.Concat<Customer>(customerFamily).Distinct<Customer>((IEqualityComparer<Customer>) new StatementCycleProcessBO.CustomerIDComparer()).ToArray<Customer>();
  }

  public static void RegenerateStatements(
    StatementCycleProcessBO graph,
    ARStatementCycle cycle,
    IEnumerable<Customer> customers)
  {
    StatementCycleProcessBO.RegenerateStatementsExplicitStmtDate(graph, cycle, customers, new DateTime?());
  }

  public static void RegenerateStatementsExplicitStmtDate(
    StatementCycleProcessBO graph,
    ARStatementCycle cycle,
    IEnumerable<Customer> customers,
    DateTime? explicitStmtDate)
  {
    ((PXGraph) graph).Clear();
    if (!cycle.LastStmtDate.HasValue && !explicitStmtDate.HasValue)
      return;
    DateTime stmtDate = !explicitStmtDate.HasValue ? cycle.LastStmtDate.Value : explicitStmtDate.GetValueOrDefault();
    StatementCreateBO instance = PXGraph.CreateInstance<StatementCreateBO>();
    HashSet<Customer> hashSet = StatementCycleProcessBO.GetStatementCustomers((PXGraph) graph, customers).ToHashSet<Customer>();
    Dictionary<int, List<Customer>> preloadedCustomerFamilies = StatementCycleProcessBO.PreloadPrependFamilyCustomersFromPreviousStatement((PXGraph) graph, (IEnumerable<Customer>) hashSet, stmtDate);
    ICollection<Customer>[] array = hashSet.GroupBy<Customer, int?>((Func<Customer, int?>) (customer => customer.StatementCustomerID)).Select<IGrouping<int?, Customer>, ICollection<Customer>>((Func<IGrouping<int?, Customer>, ICollection<Customer>>) (family => StatementCycleProcessBO.PrependFamilyCustomersFromPreviousStatement((PXGraph) graph, (IEnumerable<Customer>) family, stmtDate, preloadedCustomerFamilies))).OrderByDescending<ICollection<Customer>, int>((Func<ICollection<Customer>, int>) (family => family.Count)).ToArray<ICollection<Customer>>();
    HashSet<int> processedCustomers = new HashSet<int>();
    foreach (IEnumerable<Customer> customers1 in array)
    {
      if (!customers1.Any<Customer>((Func<Customer, bool>) (customer => processedCustomers.Contains(customer.BAccountID.Value))))
      {
        graph.GenerateStatementForCustomerFamily(instance, cycle, customers1, stmtDate, true, false);
        processedCustomers.UnionWith(customers1.Select<Customer, int>((Func<Customer, int>) (customer => customer.BAccountID.Value)));
      }
    }
  }

  public static void GenerateOnDemandStatement(
    StatementCycleProcessBO graph,
    ARStatementCycle statementCycle,
    Customer customer,
    DateTime statementDate)
  {
    ((PXGraph) graph).Clear();
    StatementCreateBO instance = PXGraph.CreateInstance<StatementCreateBO>();
    IEnumerable<Customer> statementCustomers = StatementCycleProcessBO.GetStatementCustomers((PXGraph) graph, EnumerableExtensions.AsSingleEnumerable<Customer>(customer));
    graph.GenerateStatementForCustomerFamily(instance, statementCycle, statementCustomers, statementDate, true, true);
  }

  protected virtual void GenerateStatement(ARStatementCycle cycle, DateTime statementDate)
  {
    bool flag = ((IQueryable<PXResult<PX.Objects.GL.Branch>>) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.active, Equal<False>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<PX.Objects.GL.Branch>>();
    Dictionary<int, HashSet<string>> dictionary = new Dictionary<int, HashSet<string>>();
    if (flag)
    {
      PXSelectBase<PX.Objects.GL.Branch> pxSelectBase = (PXSelectBase<PX.Objects.GL.Branch>) new PXSelectJoinGroupBy<PX.Objects.GL.Branch, InnerJoin<ARStatement, On<ARStatement.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.active, Equal<False>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>>>>, Aggregate<GroupBy<ARStatement.customerID, GroupBy<PX.Objects.GL.Branch.branchID, GroupBy<PX.Objects.GL.Branch.branchCD>>>>>((PXGraph) this);
      using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[2]
      {
        typeof (ARStatement.customerID),
        typeof (PX.Objects.GL.Branch.branchCD)
      }))
      {
        foreach (PXResult<PX.Objects.GL.Branch, ARStatement> pxResult in pxSelectBase.Select(new object[2]
        {
          (object) cycle.LastStmtDate,
          (object) cycle.StatementCycleId
        }))
        {
          int key = PXResult<PX.Objects.GL.Branch, ARStatement>.op_Implicit(pxResult).CustomerID.Value;
          string branchCd = PXResult<PX.Objects.GL.Branch, ARStatement>.op_Implicit(pxResult).BranchCD;
          HashSet<string> stringSet;
          if (!dictionary.TryGetValue(key, out stringSet))
          {
            stringSet = new HashSet<string>();
            dictionary.Add(key, stringSet);
          }
          if (!stringSet.Contains(branchCd))
            stringSet.Add(branchCd);
        }
      }
    }
    StatementCreateBO instance = PXGraph.CreateInstance<StatementCreateBO>();
    IEnumerable<ICollection<Customer>> customers1 = this.CollectCustomerFamiliesForCycleProcessing(cycle, statementDate);
    ICollection<string> strings1 = (ICollection<string>) new List<string>();
    ICollection<string> strings2 = (ICollection<string>) new List<string>();
    HashSet<string> values1 = new HashSet<string>();
    foreach (IEnumerable<Customer> customers2 in (IEnumerable<IEnumerable<Customer>>) customers1)
    {
      bool clearExisting = ((PXSelectBase<ARStatement>) new PXSelect<ARStatement, Where<ARStatement.customerID, In<Required<ARStatement.customerID>>, And<ARStatement.onDemand, Equal<True>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>>>>>((PXGraph) this)).Any<ARStatement>((object) customers2.Select<Customer, int?>((Func<Customer, int?>) (customer => customer.BAccountID)).ToArray<int?>(), (object) statementDate);
      if (clearExisting)
        strings1.Add(customers2.First<Customer>().AcctCD.Trim());
      IEnumerable<Customer> customers3 = customers2.Where<Customer>((Func<Customer, bool>) (customer =>
      {
        DateTime? statementLastDate = customer.StatementLastDate;
        DateTime dateTime = statementDate;
        return statementLastDate.HasValue && statementLastDate.GetValueOrDefault() >= dateTime;
      }));
      strings2.AddRange<string>(customers3.Select<Customer, string>((Func<Customer, string>) (customer => customer.AcctCD)));
      IEnumerable<Customer> customers4 = customers2.Except<Customer>(customers3);
      if (customers4.Count<Customer>() > 0)
      {
        if (flag)
        {
          foreach (Customer customer in customers4)
          {
            HashSet<string> values2;
            if (dictionary.TryGetValue(customer.BAccountID.Value, out values2) && values2.Count > 0)
            {
              PXTrace.WriteWarning("The documents of the following customers for the {1} inactive branches have been excluded from the prepared statements: {0}.", new object[2]
              {
                (object) customer.AcctCD,
                (object) string.Join(", ", (IEnumerable<string>) values2)
              });
              EnumerableExtensions.AddRange<string>((ISet<string>) values1, (IEnumerable<string>) values2);
            }
          }
        }
        this.GenerateStatementForCustomerFamily(instance, cycle, customers4, statementDate, clearExisting, false);
      }
    }
    if (strings1.Any<string>())
      PXTrace.WriteWarning("On-demand statements have been overwritten for the following customers: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) strings1)
      });
    if (strings2.Any<string>())
    {
      PXTrace.WriteWarning("The following customers have been excluded from the processing because statements that cover the selected date already exist: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) strings2)
      });
      PXProcessing<ARStatementCycle>.SetWarning("Some customers have been excluded from statement generation. Check Trace for details.");
    }
    if (values1.Count > 0)
      PXProcessing<ARStatementCycle>.SetWarning($"Documents of the {string.Join(",", (IEnumerable<string>) values1)} inactive branch or branches have been excluded from the prepared statements.");
    this.UpdateStatementCycleLastStatementDate(cycle, statementDate);
  }

  protected virtual void RegenerateLastStatement(ARStatementCycle cycle, DateTime statementDate)
  {
    StatementCreateBO statementGraph = PXGraph.CreateInstance<StatementCreateBO>();
    IEnumerable<ICollection<Customer>> source = this.CollectCustomerFamiliesForCycleProcessing(cycle, statementDate).Where<ICollection<Customer>>((Func<ICollection<Customer>, bool>) (family => !family.Any<Customer>((Func<Customer, bool>) (customer =>
    {
      DateTime? statementLastDate = customer.StatementLastDate;
      DateTime dateTime = statementDate;
      return statementLastDate.HasValue && statementLastDate.GetValueOrDefault() > dateTime;
    }))));
    HashSet<Customer> allCustomers = new HashSet<Customer>();
    EnumerableExtensions.ForEach<IEnumerable<Customer>>((IEnumerable<IEnumerable<Customer>>) source, (Action<IEnumerable<Customer>>) (family => EnumerableExtensions.ForEach<Customer>(family, (Action<Customer>) (customer => allCustomers.Add(customer)))));
    Dictionary<int, List<Customer>> preloadedCustomerFamilies = StatementCycleProcessBO.PreloadPrependFamilyCustomersFromPreviousStatement((PXGraph) statementGraph, (IEnumerable<Customer>) allCustomers, statementDate);
    IOrderedEnumerable<ICollection<Customer>> orderedEnumerable = ((IEnumerable<IEnumerable<Customer>>) source).Select<IEnumerable<Customer>, ICollection<Customer>>((Func<IEnumerable<Customer>, ICollection<Customer>>) (family => StatementCycleProcessBO.PrependFamilyCustomersFromPreviousStatement((PXGraph) statementGraph, family, statementDate, preloadedCustomerFamilies))).OrderByDescending<ICollection<Customer>, int>((Func<ICollection<Customer>, int>) (family => family.Count));
    HashSet<int> processedCustomers = new HashSet<int>();
    foreach (IEnumerable<Customer> customers in (IEnumerable<IEnumerable<Customer>>) orderedEnumerable)
    {
      if (!customers.Any<Customer>((Func<Customer, bool>) (customer => processedCustomers.Contains(customer.BAccountID.Value))))
      {
        this.GenerateStatementForCustomerFamily(statementGraph, cycle, customers, statementDate, true, false);
        processedCustomers.UnionWith(customers.Select<Customer, int>((Func<Customer, int>) (customer => customer.BAccountID.Value)));
      }
    }
    this.UpdateStatementCycleLastStatementDate(cycle, statementDate);
  }

  protected virtual void DeleteLastStatementExec(
    ARStatementCycle cycle,
    DateTime deleteStatementDate)
  {
    StatementCreateBO pGraph = PXGraph.CreateInstance<StatementCreateBO>();
    IEnumerable<ICollection<Customer>> source1 = this.CollectCustomerFamiliesForCycleDeleting(cycle, deleteStatementDate);
    HashSet<Customer> allCustomersForLoad = new HashSet<Customer>();
    EnumerableExtensions.ForEach<ICollection<Customer>>(source1, (Action<ICollection<Customer>>) (r0 => EnumerableExtensions.ForEach<Customer>((IEnumerable<Customer>) r0, (Action<Customer>) (r1 => allCustomersForLoad.Add(r1)))));
    Dictionary<int, List<Customer>> preloadedCustomerFamilies = StatementCycleProcessBO.PreloadPrependFamilyCustomersFromPreviousStatement((PXGraph) pGraph, (IEnumerable<Customer>) allCustomersForLoad, deleteStatementDate);
    IOrderedEnumerable<ICollection<Customer>> orderedEnumerable = source1.Select<ICollection<Customer>, ICollection<Customer>>((Func<ICollection<Customer>, ICollection<Customer>>) (family => StatementCycleProcessBO.PrependFamilyCustomersFromPreviousStatement((PXGraph) pGraph, (IEnumerable<Customer>) family, deleteStatementDate, preloadedCustomerFamilies))).OrderByDescending<ICollection<Customer>, int>((Func<ICollection<Customer>, int>) (family => family.Count));
    HashSet<int> intSet = new HashSet<int>();
    DateTime? previousStatementDate = this.GetPreviousStatementDate(cycle);
    List<int> source2 = new List<int>();
    foreach (IEnumerable<Customer> customers in (IEnumerable<IEnumerable<Customer>>) orderedEnumerable)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (Customer customer in customers)
        {
          if (!customer.StatementLastDate.HasValue || !(customer.StatementLastDate.Value > deleteStatementDate))
          {
            foreach (PXResult<ARStatement> pxResult in ((PXSelectBase<ARStatement>) pGraph.CustomerStatementForDelete).Select(new object[2]
            {
              (object) customer.BAccountID,
              (object) deleteStatementDate
            }))
            {
              ARStatement arStatement = PXResult<ARStatement>.op_Implicit(pxResult);
              List<int> intList1 = source2;
              int? nullable = arStatement.CustomerID;
              int num1 = nullable.Value;
              intList1.Add(num1);
              PXUpdate<Set<ARBalances.statementRequired, True>, ARBalances, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>, And<ARBalances.branchID, Equal<Required<ARBalances.branchID>>>>>.Update((PXGraph) pGraph, new object[2]
              {
                (object) arStatement.CustomerID,
                (object) arStatement.BranchID
              });
              if (customer.ConsolidateStatements.GetValueOrDefault())
              {
                nullable = customer.ParentBAccountID;
                if (nullable.HasValue)
                {
                  List<int> intList2 = source2;
                  nullable = customer.ParentBAccountID;
                  int num2 = nullable.Value;
                  intList2.Add(num2);
                  PXUpdate<Set<ARBalances.statementRequired, True>, ARBalances, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>, And<ARBalances.branchID, Equal<Required<ARBalances.branchID>>>>>.Update((PXGraph) pGraph, new object[2]
                  {
                    (object) customer.ParentBAccountID,
                    (object) arStatement.BranchID
                  });
                }
              }
              ((PXSelectBase<ARStatement>) pGraph.CustomerStatementForDelete).Delete(arStatement);
            }
            ((PXGraph) pGraph).Actions.PressSave();
            PXUpdate<Set<ARRegister.statementDate, Null>, ARRegister, Where<ARRegister.statementDate, Equal<Required<ARRegister.statementDate>>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>>>>.Update((PXGraph) pGraph, new object[2]
            {
              (object) deleteStatementDate,
              (object) customer.BAccountID
            });
            PXUpdate<Set<ARAdjust.statementDate, Null>, ARAdjust, Where<ARAdjust.statementDate, Equal<Required<ARAdjust.statementDate>>, And<ARAdjust.customerID, Equal<Required<ARAdjust.customerID>>>>>.Update((PXGraph) pGraph, new object[2]
            {
              (object) deleteStatementDate,
              (object) customer.BAccountID
            });
            if (customer.StatementType == "B")
              PXUpdate<Set<ARTranPost.statementDate, Null>, ARTranPost, Where<ARTranPost.statementDate, Equal<Required<ARTranPost.statementDate>>, And<ARTranPost.docDate, Greater<Required<ARTranPost.docDate>>, And<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>>>>>.Update((PXGraph) pGraph, new object[3]
              {
                (object) deleteStatementDate,
                (object) (previousStatementDate ?? DateTime.MinValue),
                (object) customer.BAccountID
              });
            if (customer.StatementType == "O")
            {
              StatementCreateBO statementCreateBo = pGraph;
              object[] objArray = new object[2]
              {
                (object) "O",
                (object) customer.BAccountID
              };
              foreach (ARStatementDetail arStatementDetail in GraphHelper.RowCast<ARStatementDetail>((IEnumerable) PXSelectBase<ARStatementDetail, PXViewOf<ARStatementDetail>.BasedOn<SelectFromBase<ARStatementDetail, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Customer>.On<BqlOperand<ARStatementDetail.customerID, IBqlInt>.IsEqual<Customer.bAccountID>>>, FbqlJoins.Left<ARStatement>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.branchID, Equal<ARStatementDetail.branchID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.customerID, Equal<ARStatementDetail.customerID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.curyID, Equal<ARStatementDetail.curyID>>>>>.And<BqlOperand<ARStatement.statementDate, IBqlDateTime>.IsEqual<ARStatementDetail.statementDate>>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.statementType, Equal<P.AsString>>>>, And<BqlOperand<ARStatement.onDemand, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<GroupBy<ARStatementDetail.refNbr>, GroupBy<ARStatementDetail.docType>, Max<ARStatement.statementDate>>>>.Config>.Select((PXGraph) statementCreateBo, objArray)).ToArray<ARStatementDetail>())
                PXUpdate<Set<ARTranPost.statementDate, Required<ARTranPost.statementDate>>, ARTranPost, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.type, Equal<Required<ARTranPost.type>>, And<ARTranPost.refNbr, Equal<Required<ARTranPost.refNbr>>, And<ARTranPost.docType, Equal<Required<ARTranPost.docType>>>>>>>.Update((PXGraph) pGraph, new object[5]
                {
                  (object) arStatementDetail.StatementDate,
                  (object) customer.BAccountID,
                  (object) "S",
                  (object) arStatementDetail.RefNbr,
                  (object) arStatementDetail.DocType
                });
              PXUpdate<Set<ARTranPost.statementDate, Null>, ARTranPost, Where<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>, And<ARTranPost.statementDate, Equal<Required<ARTranPost.statementDate>>>>>.Update((PXGraph) pGraph, new object[2]
              {
                (object) customer.BAccountID,
                (object) deleteStatementDate
              });
            }
          }
        }
        transactionScope.Complete();
      }
    }
    List<int> list = source2.Distinct<int>().ToList<int>();
    Dictionary<int?, DateTime?> dictionary = GraphHelper.RowCast<ARStatement>((IEnumerable) PXSelectBase<ARStatement, PXViewOf<ARStatement>.BasedOn<SelectFromBase<ARStatement, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.customerID, In<P.AsInt>>>>>.And<BqlOperand<ARStatement.onDemand, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<ARStatement.customerID>, Max<ARStatement.statementDate>>>>.Config>.Select((PXGraph) pGraph, new object[1]
    {
      (object) list
    })).ToDictionary<ARStatement, int?, DateTime?>((Func<ARStatement, int?>) (_ => _.CustomerID), (Func<ARStatement, DateTime?>) (_ => _.StatementDate));
    foreach (int num in list)
    {
      int? key = new int?(num);
      DateTime? nullable = new DateTime?();
      dictionary.TryGetValue(key, out nullable);
      PXUpdate<Set<Customer.statementLastDate, Required<Customer.statementLastDate>>, Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Update((PXGraph) pGraph, new object[2]
      {
        (object) nullable,
        (object) key
      });
    }
    cycle.LastStmtDate = previousStatementDate;
    ((PXSelectBase<ARStatementCycle>) this.CyclesList).Update(cycle);
    ((PXGraph) this).Actions.PressSave();
  }

  protected virtual IEnumerable<ICollection<Customer>> CollectCustomerFamiliesForCycleProcessing(
    ARStatementCycle cycle,
    DateTime statementDate)
  {
    ICollection<ICollection<Customer>> customers = (ICollection<ICollection<Customer>>) new List<ICollection<Customer>>();
    PXSelectJoin<Customer, InnerJoin<CustomerMaster, On<CustomerMaster.bAccountID, Equal<Customer.statementCustomerID>>, LeftJoin<StatementCycleProcessBO.CustomerWithActiveBalance, On<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, Equal<Customer.bAccountID>>>>, Where<CustomerMaster.statementCycleId, Equal<Required<Customer.statementCycleId>>, And<Where2<Where<Required<ARStatementCycle.printEmptyStatements>, Equal<True>, Or<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, IsNotNull>>, Or<Exists<Select<ARStatement, Where<ARStatement.statementCycleId, Equal<CustomerMaster.statementCycleId>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.customerID, Equal<Customer.bAccountID>, And<ARStatement.onDemand, Equal<True>>>>>>>>>>>, OrderBy<Asc<Customer.statementCustomerID>>> pxSelectJoin = new PXSelectJoin<Customer, InnerJoin<CustomerMaster, On<CustomerMaster.bAccountID, Equal<Customer.statementCustomerID>>, LeftJoin<StatementCycleProcessBO.CustomerWithActiveBalance, On<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, Equal<Customer.bAccountID>>>>, Where<CustomerMaster.statementCycleId, Equal<Required<Customer.statementCycleId>>, And<Where2<Where<Required<ARStatementCycle.printEmptyStatements>, Equal<True>, Or<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, IsNotNull>>, Or<Exists<Select<ARStatement, Where<ARStatement.statementCycleId, Equal<CustomerMaster.statementCycleId>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.customerID, Equal<Customer.bAccountID>, And<ARStatement.onDemand, Equal<True>>>>>>>>>>>, OrderBy<Asc<Customer.statementCustomerID>>>((PXGraph) this);
    List<Customer> source = new List<Customer>();
    object[] objArray = new object[3]
    {
      (object) cycle.StatementCycleId,
      (object) cycle.PrintEmptyStatements,
      (object) statementDate
    };
    foreach (PXResult<Customer> pxResult in ((PXSelectBase<Customer>) pxSelectJoin).Select(objArray))
    {
      Customer customer = PXResult<Customer>.op_Implicit(pxResult);
      if (source.Any<Customer>())
      {
        int? statementCustomerId1 = source.First<Customer>().StatementCustomerID;
        int? statementCustomerId2 = customer.StatementCustomerID;
        if (!(statementCustomerId1.GetValueOrDefault() == statementCustomerId2.GetValueOrDefault() & statementCustomerId1.HasValue == statementCustomerId2.HasValue))
        {
          customers.Add((ICollection<Customer>) source);
          source = new List<Customer>() { customer };
          continue;
        }
      }
      source.Add(customer);
    }
    if (source.Any<Customer>())
      customers.Add((ICollection<Customer>) source);
    return (IEnumerable<ICollection<Customer>>) customers;
  }

  protected virtual IEnumerable<ICollection<Customer>> CollectCustomerFamiliesForCycleDeleting(
    ARStatementCycle cycle,
    DateTime statementDate)
  {
    ICollection<ICollection<Customer>> customers = (ICollection<ICollection<Customer>>) new List<ICollection<Customer>>();
    Customer[] array = GraphHelper.RowCast<Customer>((IEnumerable) PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CustomerMaster>.On<BqlOperand<Customer.parentBAccountID, IBqlInt>.IsEqual<CustomerMaster.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.statementCycleId, Equal<P.AsString>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.consolidateStatements, Equal<False>>>>>.Or<BqlOperand<Customer.parentBAccountID, IBqlInt>.IsNull>>>, Or<BqlOperand<CustomerMaster.statementCycleId, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<Customer.consolidateStatements, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cycle.StatementCycleId,
      (object) cycle.StatementCycleId
    })).ToArray<Customer>();
    List<Customer> source = new List<Customer>();
    foreach (Customer customer in array)
    {
      if (source.Any<Customer>())
      {
        int? statementCustomerId1 = source.First<Customer>().StatementCustomerID;
        int? statementCustomerId2 = customer.StatementCustomerID;
        if (!(statementCustomerId1.GetValueOrDefault() == statementCustomerId2.GetValueOrDefault() & statementCustomerId1.HasValue == statementCustomerId2.HasValue))
        {
          customers.Add((ICollection<Customer>) source);
          source = new List<Customer>() { customer };
          continue;
        }
      }
      source.Add(customer);
    }
    if (source.Any<Customer>())
      customers.Add((ICollection<Customer>) source);
    return (IEnumerable<ICollection<Customer>>) customers;
  }

  /// <summary>
  /// Checks if all customers belonging to the specified statement cyclehave their
  /// statement date updated (i.e. they have been properly processed by the
  /// <see cref="!:GenerateCustomerStatement(StatementCreateBO, ARStatementCycle, Customer, DateTime, &#xD;&#xA;            IDictionary&lt;ARStatementKey, ARStatement&gt;, IDictionary&lt;ARStatementKey, ARStatement&gt;, bool)" />
  /// function). If so, updates the <see cref="P:PX.Objects.AR.ARStatementCycle.LastStmtDate">last statement date</see>
  /// of the statement cycle.
  /// </summary>
  protected virtual void UpdateStatementCycleLastStatementDate(
    ARStatementCycle cycle,
    DateTime statementDate,
    bool isOnDemand = false)
  {
    if (isOnDemand)
      return;
    if (!((IEnumerable<PXResult<Customer>>) PXSelectBase<Customer, PXSelectReadonly2<Customer, InnerJoin<CustomerMaster, On<CustomerMaster.bAccountID, Equal<Customer.statementCustomerID>>, LeftJoin<StatementCycleProcessBO.CustomerWithActiveBalance, On<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, Equal<Customer.bAccountID>>>>, Where<CustomerMaster.statementCycleId, Equal<Required<Customer.statementCycleId>>, And2<Where<Customer.statementLastDate, IsNull, Or<Customer.statementLastDate, Less<Required<Customer.statementLastDate>>>>, And<Where<Required<ARStatementCycle.printEmptyStatements>, Equal<True>, Or<StatementCycleProcessBO.CustomerWithActiveBalance.customerID, IsNotNull>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) cycle.StatementCycleId,
      (object) statementDate,
      (object) cycle.PrintEmptyStatements
    })).IsEmpty<PXResult<Customer>>())
      return;
    cycle.LastStmtDate = new DateTime?(statementDate);
    ((PXSelectBase<ARStatementCycle>) this.CyclesList).Update(cycle);
    ((PXGraph) this).Actions.PressSave();
  }

  /// <summary>
  /// Deletes the existing statements for the family on the specified date,
  /// depending on the settings for the current statement generation.
  /// </summary>
  /// <returns>
  /// Trace information about the deleted statements, which is required
  /// to pass print / email counts to regenerated statements.
  /// </returns>
  protected virtual IDictionary<ARStatementKey, ARStatement> DeleteExistingStatements(
    IEnumerable<Customer> customerFamily,
    DateTime statementDate,
    bool clearExisting,
    bool isOnDemand)
  {
    IEnumerable<ARStatement> arStatements = Enumerable.Empty<ARStatement>();
    HashSet<int> intSet1 = new HashSet<int>();
    foreach (Customer customer1 in customerFamily)
    {
      if (!clearExisting)
      {
        DateTime? statementLastDate = customer1.StatementLastDate;
        DateTime dateTime = statementDate;
        if ((statementLastDate.HasValue ? (statementLastDate.GetValueOrDefault() == dateTime ? 1 : 0) : 0) == 0)
          continue;
      }
      if (isOnDemand)
        this.EnsureNoRegularStatementExists(customer1.BAccountID, statementDate);
      arStatements = arStatements.Concat<ARStatement>(this.DeleteCustomerStatement(customer1, statementDate, isOnDemand));
      int? baccountId = customer1.BAccountID;
      int? statementCustomerId = customer1.StatementCustomerID;
      if (!(baccountId.GetValueOrDefault() == statementCustomerId.GetValueOrDefault() & baccountId.HasValue == statementCustomerId.HasValue))
      {
        statementCustomerId = customer1.StatementCustomerID;
        if (statementCustomerId.HasValue)
        {
          HashSet<int> intSet2 = intSet1;
          statementCustomerId = customer1.StatementCustomerID;
          int num1 = statementCustomerId.Value;
          if (!intSet2.Contains(num1))
          {
            IEnumerable<ARStatement> first = arStatements;
            Customer customer2 = new Customer();
            customer2.BAccountID = customer1.StatementCustomerID;
            IEnumerable<ARStatement> second = this.DeleteCustomerStatement(customer2, statementDate, isOnDemand);
            arStatements = first.Concat<ARStatement>(second);
            HashSet<int> intSet3 = intSet1;
            statementCustomerId = customer1.StatementCustomerID;
            int num2 = statementCustomerId.Value;
            intSet3.Add(num2);
          }
        }
      }
    }
    return (IDictionary<ARStatementKey, ARStatement>) arStatements.ToDictionary<ARStatement, ARStatementKey>((Func<ARStatement, ARStatementKey>) (statement => new ARStatementKey(statement)));
  }

  protected virtual void ForceBeginningBalanceToPreviousStatementEndBalance(
    IDictionary<ARStatementKey, ARStatement> familyStatements,
    IDictionary<ARStatementKey, ICollection<ARStatementDetail>> familyStatementDetails)
  {
    foreach (KeyValuePair<ARStatementKey, ARStatement> familyStatement in (IEnumerable<KeyValuePair<ARStatementKey, ARStatement>>) familyStatements)
    {
      ARStatementKey key = familyStatement.Key;
      ARStatement aDest = familyStatement.Value;
      ARStatement previousStatement = this.GetPreviousStatement(aDest.BranchID, aDest.CustomerID, aDest.CuryID);
      if (previousStatement != null)
      {
        aDest.BegBalance = previousStatement.EndBalance;
        aDest.CuryBegBalance = previousStatement.CuryEndBalance;
      }
      StatementCycleProcessBO.ApplyFIFORule(aDest, ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.AgeCredits.GetValueOrDefault());
      ICollection<ARStatementDetail> arStatementDetails;
      if (!familyStatementDetails.TryGetValue(key, out arStatementDetails) || arStatementDetails == null)
      {
        arStatementDetails = (ICollection<ARStatementDetail>) new List<ARStatementDetail>();
        familyStatementDetails[key] = arStatementDetails;
      }
      arStatementDetails.Add(new ARStatementDetail()
      {
        BranchID = new int?(key.BranchID),
        CustomerID = new int?(key.CustomerID),
        CuryID = key.CurrencyID,
        StatementDate = new DateTime?(key.StatementDate),
        DocType = string.Empty,
        RefNbr = string.Empty,
        RefNoteID = new Guid?(Guid.NewGuid()),
        TranPostID = new int?(0),
        DocBalance = new Decimal?(0M),
        CuryDocBalance = new Decimal?(0M),
        StatementType = aDest.StatementType,
        BegBalance = aDest.BegBalance,
        CuryBegBalance = aDest.CuryBegBalance,
        AgeBalance00 = aDest.AgeBalance00,
        AgeBalance01 = aDest.AgeBalance01,
        AgeBalance02 = aDest.AgeBalance02,
        AgeBalance03 = aDest.AgeBalance03,
        AgeBalance04 = aDest.AgeBalance04,
        CuryAgeBalance00 = aDest.CuryAgeBalance00,
        CuryAgeBalance01 = aDest.CuryAgeBalance01,
        CuryAgeBalance02 = aDest.CuryAgeBalance02,
        CuryAgeBalance03 = aDest.CuryAgeBalance03,
        CuryAgeBalance04 = aDest.CuryAgeBalance04
      });
    }
  }

  /// <summary>
  /// If <see cref="P:PX.Objects.AR.ARStatementCycle.PrintEmptyStatements" /> is set to
  /// <c>false</c>, marks all empty open item statements and empty
  /// zero-balance BBF statements as "do not print" and "do not email".
  /// </summary>
  protected virtual void MarkEmptyStatementsForPrintingAndEmailing(
    ARStatementCycle statementCycle,
    IEnumerable<ARStatement> statements,
    IDictionary<ARStatementKey, ICollection<ARStatementDetail>> statementDetails)
  {
    if (statementCycle.PrintEmptyStatements.GetValueOrDefault())
      return;
    foreach (ARStatement statement in statements)
    {
      ARStatementKey key = new ARStatementKey(statement);
      if (ARStatementProcess.IsEmptyStatement(statement, (IEnumerable<ARStatementDetail>) statementDetails.GetValueOrEmpty<ARStatementKey, ARStatementDetail>(key)))
      {
        statement.DontEmail = new bool?(true);
        statement.DontPrint = new bool?(true);
      }
    }
  }

  /// <param name="isOnDemand">
  /// If set to <c>true</c>, indicates that the statements to be persisted
  /// are on-demand statements, so that <see cref="T:PX.Objects.AR.Customer.statementLastDate" />,
  /// <see cref="T:PX.Objects.AR.ARRegister.statementDate" />, and <see cref="T:PX.Objects.AR.ARAdjust.statementDate" />
  /// will not be updated.
  /// </param>
  protected static bool PersistStatement(
    StatementCreateBO statementPersistGraph,
    IEnumerable<ARStatement> statements,
    IEnumerable<ARStatementDetail> statementDetails)
  {
    ((PXGraph) statementPersistGraph).Clear();
    foreach (ARStatement statement in statements)
      ((PXSelectBase<ARStatement>) statementPersistGraph.Statement).Insert(statement);
    foreach (ARStatementDetail statementDetail in statementDetails)
      ((PXSelectBase<ARStatementDetail>) statementPersistGraph.StatementDetail).Insert(statementDetail);
    ((PXGraph) statementPersistGraph).Actions.PressSave();
    return true;
  }

  protected static void UpdateCustomersLastStatementDate(
    PXGraph statementPersistGraph,
    DateTime statementDate,
    int customerID)
  {
    PXUpdate<Set<PX.Objects.AR.Override.Customer.statementLastDate, Required<PX.Objects.AR.Override.Customer.statementLastDate>>, PX.Objects.AR.Override.Customer, Where<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<PX.Objects.AR.Override.Customer.bAccountID>>, And<Where<PX.Objects.AR.Override.Customer.statementLastDate, IsNull, Or<PX.Objects.AR.Override.Customer.statementLastDate, Less<Required<PX.Objects.AR.Override.Customer.statementLastDate>>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) customerID,
      (object) statementDate
    });
  }

  protected static void UpdateARBalanceStatementNotRequired(
    PXGraph statementPersistGraph,
    ARStatement statement,
    DateTime statementDate)
  {
    PXUpdate<Set<ARBalances.statementRequired, False>, ARBalances, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>, And<ARBalances.branchID, Equal<Required<ARBalances.branchID>>, And<ARBalances.lastDocDate, LessEqual<Required<ARBalances.lastDocDate>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statement.CustomerID.Value,
      (object) statement.BranchID.Value,
      (object) statementDate
    });
  }

  protected static void UpdateARBalanceStatementNotRequired(
    PXGraph statementPersistGraph,
    int customerID,
    DateTime statementDate)
  {
    PXUpdate<Set<ARBalances.statementRequired, False>, ARBalances, Where<ARBalances.customerID, Equal<Required<ARBalances.customerID>>, And<ARBalances.lastDocDate, LessEqual<Required<ARBalances.lastDocDate>>>>>.Update(statementPersistGraph, new object[2]
    {
      (object) customerID,
      (object) statementDate
    });
  }

  /// <summary>
  /// Used for open item statement processing. Updates all documents of the customers that do not
  /// yet have an <see cref="P:PX.Objects.AR.ARRegister.StatementDate" />, regardless of whether the document
  /// has got into statement. This is done so that if the customer switches to BBF statement
  /// type, the documents that didn't get into BBF statements don't suddenly appear in the
  /// new statements.
  /// </summary>
  protected static void UpdateDocumentsLastStatementDate(
    PXGraph statementPersistGraph,
    DateTime? statementDate,
    int? customerID)
  {
    PXUpdate<Set<ARRegister.statementDate, Required<ARRegister.statementDate>>, ARRegister, Where<ARRegister.statementDate, IsNull, And<ARRegister.docDate, LessEqual<Required<ARRegister.docDate>>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) statementDate,
      (object) customerID
    });
  }

  /// <summary>
  /// Used for balance brought forward statements. Updates <see cref="P:PX.Objects.AR.ARRegister.StatementDate" />
  /// for documents corresponding to <see cref="T:PX.Objects.AR.ARStatementDetail" /> records of a given statement,
  /// so that these documents are not included into future BBF statements.
  /// </summary>
  protected static void UpdateDocumentsLastStatementDate(
    PXGraph statementPersistGraph,
    ARStatement statement)
  {
    PXUpdateJoin<Set<ARRegister.statementDate, Required<ARRegister.statementDate>>, ARRegister, InnerJoin<ARStatementDetail, On<ARRegister.docType, Equal<ARStatementDetail.docType>, And<ARRegister.refNbr, Equal<ARStatementDetail.refNbr>>>>, Where<ARRegister.statementDate, IsNull, And<ARStatementDetail.branchID, Equal<Required<ARStatementDetail.branchID>>, And<ARStatementDetail.curyID, Equal<Required<ARStatementDetail.curyID>>, And<ARStatementDetail.customerID, Equal<Required<ARStatementDetail.customerID>>, And<ARStatementDetail.statementDate, Equal<Required<ARStatementDetail.statementDate>>>>>>>>.Update(statementPersistGraph, new object[5]
    {
      (object) statement.StatementDate,
      (object) statement.BranchID,
      (object) statement.CuryID,
      (object) statement.CustomerID,
      (object) statement.StatementDate
    });
  }

  /// <summary>
  /// Used for statement re-generation, when the old statement objects are deleted.
  /// In order for the documents to re-appear in the new statement, their statement
  /// date needs to be reset to <c>null</c>.
  /// </summary>
  /// <remarks>
  /// It is insufficient to just update documents for which matching statement details
  /// exist, because in case of switching from Open Item to BBF, some documents could have
  /// been closed at the moment of Open Item processing, and no <see cref="T:PX.Objects.AR.ARStatementDetail" />
  /// was created for them.
  /// </remarks>
  protected static void ResetDocumentsLastStatementDate(
    PXGraph statementPersistGraph,
    DateTime? statementDate,
    int? customerID)
  {
    PXUpdate<Set<ARRegister.statementDate, Null>, ARRegister, Where<ARRegister.statementDate, Equal<Required<ARRegister.statementDate>>, And<ARRegister.docDate, LessEqual<Required<ARRegister.docDate>>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) statementDate,
      (object) customerID
    });
    PXUpdate<Set<ARTranPost.statementDate, Null>, ARTranPost, Where<ARTranPost.statementDate, Equal<Required<ARTranPost.statementDate>>, And<ARTranPost.docDate, LessEqual<Required<ARTranPost.docDate>>, And<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) statementDate,
      (object) customerID
    });
  }

  /// <summary>
  /// Used for open item statements. Updates <see cref="P:PX.Objects.AR.ARAdjust.StatementDate" /> for all relevant
  /// applications of the customer so that they don't suddenly show up in customer statement when
  /// the user switches the customer to BBF.
  /// </summary>
  protected static void UpdateApplicationsLastStatementDate(
    PXGraph statementPersistGraph,
    DateTime? statementDate,
    int? customerID)
  {
    PXUpdate<Set<ARAdjust.statementDate, Required<ARAdjust.statementDate>>, ARAdjust, Where<ARAdjust.statementDate, IsNull, And<ARAdjust.adjgDocDate, LessEqual<Required<ARRegister.docDate>>, And<ARAdjust.customerID, Equal<Required<ARAdjust.customerID>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) statementDate,
      (object) customerID
    });
    PXUpdate<Set<ARTranPost.statementDate, Required<ARTranPost.statementDate>>, ARTranPost, Where<ARTranPost.statementDate, IsNull, And<ARTranPost.docDate, LessEqual<Required<ARTranPost.docDate>>, And<ARTranPost.customerID, Equal<Required<ARTranPost.customerID>>>>>>.Update(statementPersistGraph, new object[3]
    {
      (object) statementDate,
      (object) statementDate,
      (object) customerID
    });
  }

  /// <summary>
  /// Used for balance brought forward statements. Updates <see cref="P:PX.Objects.AR.ARAdjust.StatementDate" />
  /// for applications corresponding to <see cref="T:PX.Objects.AR.ARStatementDetail" /> records of a given statement,
  /// so that these applications are not included into future statements.
  /// </summary>
  protected static void UpdateApplicationsLastStatementDate(
    PXGraph statementPersistGraph,
    ARStatement statement)
  {
    PXUpdateJoin<Set<ARAdjust.statementDate, Required<ARAdjust.statementDate>>, ARAdjust, InnerJoin<ARStatementDetail, On<ARAdjust.noteID, Equal<ARStatementDetail.refNoteID>>>, Where<ARAdjust.statementDate, IsNull, And<ARStatementDetail.branchID, Equal<Required<ARStatementDetail.branchID>>, And<ARStatementDetail.curyID, Equal<Required<ARStatementDetail.curyID>>, And<ARStatementDetail.customerID, Equal<Required<ARStatementDetail.customerID>>, And<ARStatementDetail.statementDate, Equal<Required<ARStatementDetail.statementDate>>>>>>>>.Update(statementPersistGraph, new object[5]
    {
      (object) statement.StatementDate,
      (object) statement.BranchID,
      (object) statement.CuryID,
      (object) statement.CustomerID,
      (object) statement.StatementDate
    });
  }

  protected static void Recalculate(ARStatement aDest)
  {
    if (!(aDest.StatementType == "B"))
      return;
    ARStatement arStatement1 = aDest;
    Decimal? curyAgeBalance00 = aDest.CuryAgeBalance00;
    Decimal? curyAgeBalance01 = aDest.CuryAgeBalance01;
    Decimal? nullable1 = curyAgeBalance00.HasValue & curyAgeBalance01.HasValue ? new Decimal?(curyAgeBalance00.GetValueOrDefault() + curyAgeBalance01.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAgeBalance02 = aDest.CuryAgeBalance02;
    Decimal? nullable2 = nullable1.HasValue & curyAgeBalance02.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyAgeBalance02.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAgeBalance03 = aDest.CuryAgeBalance03;
    Decimal? nullable3 = nullable2.HasValue & curyAgeBalance03.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyAgeBalance03.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAgeBalance04 = aDest.CuryAgeBalance04;
    Decimal? nullable4 = nullable3.HasValue & curyAgeBalance04.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + curyAgeBalance04.GetValueOrDefault()) : new Decimal?();
    arStatement1.CuryEndBalance = nullable4;
    ARStatement arStatement2 = aDest;
    Decimal? ageBalance00 = aDest.AgeBalance00;
    Decimal? ageBalance01 = aDest.AgeBalance01;
    Decimal? nullable5 = ageBalance00.HasValue & ageBalance01.HasValue ? new Decimal?(ageBalance00.GetValueOrDefault() + ageBalance01.GetValueOrDefault()) : new Decimal?();
    nullable1 = aDest.AgeBalance02;
    Decimal? nullable6 = nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    nullable2 = aDest.AgeBalance03;
    Decimal? nullable7;
    if (!(nullable6.HasValue & nullable2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable7 = nullable1;
    }
    else
      nullable7 = new Decimal?(nullable6.GetValueOrDefault() + nullable2.GetValueOrDefault());
    Decimal? nullable8 = nullable7;
    nullable3 = aDest.AgeBalance04;
    Decimal? nullable9;
    if (!(nullable8.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable9 = nullable2;
    }
    else
      nullable9 = new Decimal?(nullable8.GetValueOrDefault() + nullable3.GetValueOrDefault());
    arStatement2.EndBalance = nullable9;
  }

  protected static void ApplyFIFORule(ARStatement aDest, bool aAgeCredits)
  {
    if (aAgeCredits)
      return;
    Decimal? ageBalance04_1 = aDest.AgeBalance04;
    Decimal num1 = 0M;
    Decimal? nullable;
    if (ageBalance04_1.GetValueOrDefault() < num1 & ageBalance04_1.HasValue)
    {
      ARStatement arStatement1 = aDest;
      nullable = arStatement1.AgeBalance03;
      Decimal? ageBalance04_2 = aDest.AgeBalance04;
      arStatement1.AgeBalance03 = nullable.HasValue & ageBalance04_2.HasValue ? new Decimal?(nullable.GetValueOrDefault() + ageBalance04_2.GetValueOrDefault()) : new Decimal?();
      aDest.AgeBalance04 = new Decimal?(0M);
      ARStatement arStatement2 = aDest;
      Decimal? curyAgeBalance03 = arStatement2.CuryAgeBalance03;
      nullable = aDest.CuryAgeBalance04;
      arStatement2.CuryAgeBalance03 = curyAgeBalance03.HasValue & nullable.HasValue ? new Decimal?(curyAgeBalance03.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      aDest.CuryAgeBalance04 = new Decimal?(0M);
    }
    nullable = aDest.AgeBalance03;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
    {
      ARStatement arStatement3 = aDest;
      nullable = arStatement3.AgeBalance02;
      Decimal? ageBalance03 = aDest.AgeBalance03;
      arStatement3.AgeBalance02 = nullable.HasValue & ageBalance03.HasValue ? new Decimal?(nullable.GetValueOrDefault() + ageBalance03.GetValueOrDefault()) : new Decimal?();
      aDest.AgeBalance03 = new Decimal?(0M);
      ARStatement arStatement4 = aDest;
      Decimal? curyAgeBalance02 = arStatement4.CuryAgeBalance02;
      nullable = aDest.CuryAgeBalance03;
      arStatement4.CuryAgeBalance02 = curyAgeBalance02.HasValue & nullable.HasValue ? new Decimal?(curyAgeBalance02.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      aDest.CuryAgeBalance03 = new Decimal?(0M);
    }
    nullable = aDest.AgeBalance02;
    Decimal num3 = 0M;
    if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
    {
      ARStatement arStatement5 = aDest;
      nullable = arStatement5.AgeBalance01;
      Decimal? ageBalance02 = aDest.AgeBalance02;
      arStatement5.AgeBalance01 = nullable.HasValue & ageBalance02.HasValue ? new Decimal?(nullable.GetValueOrDefault() + ageBalance02.GetValueOrDefault()) : new Decimal?();
      aDest.AgeBalance02 = new Decimal?(0M);
      ARStatement arStatement6 = aDest;
      Decimal? curyAgeBalance01 = arStatement6.CuryAgeBalance01;
      nullable = aDest.CuryAgeBalance02;
      arStatement6.CuryAgeBalance01 = curyAgeBalance01.HasValue & nullable.HasValue ? new Decimal?(curyAgeBalance01.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      aDest.CuryAgeBalance02 = new Decimal?(0M);
    }
    nullable = aDest.AgeBalance01;
    Decimal num4 = 0M;
    if (!(nullable.GetValueOrDefault() < num4 & nullable.HasValue))
      return;
    ARStatement arStatement7 = aDest;
    nullable = arStatement7.AgeBalance00;
    Decimal? ageBalance01 = aDest.AgeBalance01;
    arStatement7.AgeBalance00 = nullable.HasValue & ageBalance01.HasValue ? new Decimal?(nullable.GetValueOrDefault() + ageBalance01.GetValueOrDefault()) : new Decimal?();
    aDest.AgeBalance01 = new Decimal?(0M);
    ARStatement arStatement8 = aDest;
    Decimal? curyAgeBalance00 = arStatement8.CuryAgeBalance00;
    nullable = aDest.CuryAgeBalance01;
    arStatement8.CuryAgeBalance00 = curyAgeBalance00.HasValue & nullable.HasValue ? new Decimal?(curyAgeBalance00.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
    aDest.CuryAgeBalance01 = new Decimal?(0M);
  }

  protected virtual void GenerateStatementForCustomerFamily(
    StatementCreateBO persistGraph,
    ARStatementCycle statementCycle,
    IEnumerable<Customer> customerFamily,
    DateTime statementDate,
    bool clearExisting,
    bool isOnDemand)
  {
    IDictionary<ARStatementKey, ARStatement> familyStatements = (IDictionary<ARStatementKey, ARStatement>) new Dictionary<ARStatementKey, ARStatement>();
    IDictionary<ARStatementKey, ICollection<ARStatementDetail>> dictionary = (IDictionary<ARStatementKey, ICollection<ARStatementDetail>>) new Dictionary<ARStatementKey, ICollection<ARStatementDetail>>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      IDictionary<ARStatementKey, ARStatement> deletedFamilyStatementsTrace = this.DeleteExistingStatements(customerFamily, statementDate, clearExisting, isOnDemand);
      foreach (Customer customer in customerFamily)
        this.GenerateCustomerStatement(statementCycle, customer, statementDate, familyStatements, dictionary, deletedFamilyStatementsTrace, isOnDemand);
      this.ForceBeginningBalanceToPreviousStatementEndBalance(familyStatements, dictionary);
      this.MarkEmptyStatementsForPrintingAndEmailing(statementCycle, (IEnumerable<ARStatement>) familyStatements.Values, dictionary);
      StatementCycleProcessBO.PersistStatement(persistGraph, (IEnumerable<ARStatement>) familyStatements.Values, dictionary.Values.SelectMany<ICollection<ARStatementDetail>, ARStatementDetail>((Func<ICollection<ARStatementDetail>, IEnumerable<ARStatementDetail>>) (sequence => (IEnumerable<ARStatementDetail>) sequence)));
      if (!isOnDemand)
      {
        foreach (Customer customer in customerFamily)
        {
          StatementCycleProcessBO.UpdateCustomersLastStatementDate((PXGraph) persistGraph, statementDate, customer.BAccountID.Value);
          if (this.GetStatementType(customer) == "O")
          {
            StatementCycleProcessBO.UpdateDocumentsLastStatementDate((PXGraph) persistGraph, new DateTime?(statementDate), customer.BAccountID);
            StatementCycleProcessBO.UpdateApplicationsLastStatementDate((PXGraph) persistGraph, new DateTime?(statementDate), customer.BAccountID);
          }
        }
        foreach (ARStatement arStatement in (IEnumerable<ARStatement>) familyStatements.Values)
        {
          ARStatement statement = arStatement;
          StatementCycleProcessBO.UpdateDocumentsLastStatementDate((PXGraph) persistGraph, statement);
          StatementCycleProcessBO.UpdateApplicationsLastStatementDate((PXGraph) persistGraph, statement);
          ARStatementKey arStatementKey = new ARStatementKey(statement);
          IEnumerable<ARStatementDetail> statementDetails = dictionary.Where<KeyValuePair<ARStatementKey, ICollection<ARStatementDetail>>>((Func<KeyValuePair<ARStatementKey, ICollection<ARStatementDetail>>, bool>) (detail =>
          {
            int branchId1 = detail.Key.BranchID;
            int? branchId2 = statement.BranchID;
            int valueOrDefault1 = branchId2.GetValueOrDefault();
            if (branchId1 == valueOrDefault1 & branchId2.HasValue)
            {
              int customerId1 = detail.Key.CustomerID;
              int? customerId2 = statement.CustomerID;
              int valueOrDefault2 = customerId2.GetValueOrDefault();
              if (customerId1 == valueOrDefault2 & customerId2.HasValue)
              {
                DateTime statementDate1 = detail.Key.StatementDate;
                DateTime? statementDate2 = statement.StatementDate;
                return statementDate2.HasValue && statementDate1 == statementDate2.GetValueOrDefault();
              }
            }
            return false;
          })).SelectMany<KeyValuePair<ARStatementKey, ICollection<ARStatementDetail>>, ARStatementDetail>((Func<KeyValuePair<ARStatementKey, ICollection<ARStatementDetail>>, IEnumerable<ARStatementDetail>>) (kvp => (IEnumerable<ARStatementDetail>) kvp.Value));
          if (ARStatementProcess.IsEmptyStatement(statement, statementDetails))
            StatementCycleProcessBO.UpdateARBalanceStatementNotRequired((PXGraph) persistGraph, statement, statementDate);
        }
        if (familyStatements.Values.Count<ARStatement>() == 0)
        {
          foreach (Customer customer in customerFamily)
            StatementCycleProcessBO.UpdateARBalanceStatementNotRequired((PXGraph) persistGraph, customer.BAccountID.Value, statementDate);
        }
      }
      transactionScope.Complete();
    }
  }

  private DateTime? GetPreviousStatementDate(ARStatementCycle cycle)
  {
    if (!cycle.LastStmtDate.HasValue)
      return new DateTime?();
    return PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXViewOf<ARStatement>.BasedOn<SelectFromBase<ARStatement, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.statementCycleId, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARStatement.statementDate, Less<P.AsDateTime.UTC>>>>>.And<BqlOperand<ARStatement.onDemand, IBqlBool>.IsEqual<False>>>>.Order<PX.Data.BQL.Fluent.By<BqlField<ARStatement.statementDate, IBqlDateTime>.Desc>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cycle.StatementCycleId,
      (object) cycle.LastStmtDate
    }))?.StatementDate;
  }

  private string GetStatementType(Customer customer)
  {
    if (!customer.StatementChild.GetValueOrDefault())
      return customer.StatementType;
    return GraphHelper.RowCast<Customer>((IEnumerable) PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) customer.StatementCustomerID
    })).FirstOrDefault<Customer>()?.StatementType;
  }

  /// <summary>
  /// Returns the last statement generated for the specified customer,
  /// branch, and currency. Excludes on-demand statements.
  /// In case no last statement is present, returns <c>null</c>.
  /// </summary>
  private ARStatement GetPreviousStatement(int? branchID, int? customerID, string currencyID)
  {
    return PXResultset<ARStatement>.op_Implicit(PXSelectBase<ARStatement, PXSelectJoin<ARStatement, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARStatement.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>>, Where<ARStatement.branchID, Equal<Required<ARStatement.branchID>>, And<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.curyID, Equal<Required<ARStatement.curyID>>, And<ARStatement.onDemand, Equal<False>>>>>, OrderBy<Desc<ARStatement.statementDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) branchID,
      (object) customerID,
      (object) currencyID
    }));
  }

  protected virtual void GenerateCustomerStatement(
    ARStatementCycle statementCycle,
    Customer customer,
    DateTime statementDate,
    IDictionary<ARStatementKey, ARStatement> familyStatements,
    IDictionary<ARStatementKey, ICollection<ARStatementDetail>> familyStatementDetails,
    IDictionary<ARStatementKey, ARStatement> deletedFamilyStatementsTrace,
    bool isOnDemand)
  {
    ICollection<ARRegister> openDocuments = (ICollection<ARRegister>) new List<ARRegister>();
    foreach (StatementCycleProcessBO.ARTranPostStatement document in this.GetDataForStatement(customer, statementDate))
    {
      if (document.ARRegisterHasBalance())
        openDocuments.Add(document.ARRegister);
      ARStatement statementForDocument = this.GetOrAddStatementForDocument(familyStatements, deletedFamilyStatementsTrace, document, customer, statementCycle, statementDate, isOnDemand);
      if (document.ShouldBeConvertedToStatementDetail())
      {
        document.AdjustStatementEndBalance(statementForDocument);
        familyStatementDetails.GetOrAdd<ARStatementKey, ICollection<ARStatementDetail>>(new ARStatementKey(statementForDocument), (Func<ICollection<ARStatementDetail>>) (() => (ICollection<ARStatementDetail>) new List<ARStatementDetail>())).Add(this.CombineStatementDetailCustomizable(statementForDocument, document));
        PXUpdate<Set<ARTranPost.statementDate, Required<ARTranPost.statementDate>>, ARTranPost, Where<ARTranPost.iD, Equal<Required<ARTranPost.iD>>>>.Update((PXGraph) this, new object[2]
        {
          (object) statementForDocument.StatementDate,
          (object) document.ARTranPost.ID
        });
      }
    }
    this.AccumulateAgeBalancesIntoStatements(statementCycle, statementDate, (IEnumerable<ARRegister>) openDocuments, familyStatements, ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.AgeCredits.GetValueOrDefault());
    PXSelectJoin<ARStatement, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARStatement.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>, InnerJoin<ARBalances, On<ARBalances.branchID, Equal<ARStatement.branchID>, And<ARBalances.customerID, Equal<ARStatement.customerID>>>>>, Where<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.onDemand, Equal<False>, And<ARBalances.statementRequired, Equal<True>>>>, OrderBy<Asc<ARStatement.curyID, Desc<ARStatement.statementDate>>>> pxSelectJoin = new PXSelectJoin<ARStatement, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARStatement.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>, InnerJoin<ARBalances, On<ARBalances.branchID, Equal<ARStatement.branchID>, And<ARBalances.customerID, Equal<ARStatement.customerID>>>>>, Where<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.onDemand, Equal<False>, And<ARBalances.statementRequired, Equal<True>>>>, OrderBy<Asc<ARStatement.curyID, Desc<ARStatement.statementDate>>>>((PXGraph) this);
    IDictionary<ARStatementKey, DateTime> dictionary1 = (IDictionary<ARStatementKey, DateTime>) new Dictionary<ARStatementKey, DateTime>();
    object[] objArray = new object[1]
    {
      (object) customer.BAccountID
    };
    foreach (PXResult<ARStatement> pxResult in ((PXSelectBase<ARStatement>) pxSelectJoin).Select(objArray))
    {
      ARStatement arStatement = PXResult<ARStatement>.op_Implicit(pxResult);
      int? nullable = arStatement.BranchID;
      int branchID = nullable.Value;
      string curyId = arStatement.CuryID;
      nullable = customer.BAccountID;
      int customerID = nullable.Value;
      DateTime statementDate1 = statementDate;
      ARStatementKey arStatementKey = new ARStatementKey(branchID, curyId, customerID, statementDate1);
      DateTime? statementDate2;
      if (dictionary1.ContainsKey(arStatementKey))
      {
        DateTime dateTime = dictionary1[arStatementKey];
        statementDate2 = arStatement.StatementDate;
        if ((statementDate2.HasValue ? (dateTime > statementDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          continue;
      }
      ARStatement orAddStatement = this.GetOrAddStatement(familyStatements, arStatementKey, statementCycle, customer, isOnDemand);
      orAddStatement.BegBalance = arStatement.EndBalance;
      orAddStatement.CuryBegBalance = arStatement.CuryEndBalance;
      StatementCycleProcessBO.Recalculate(orAddStatement);
      IDictionary<ARStatementKey, DateTime> dictionary2 = dictionary1;
      ARStatementKey key = arStatementKey;
      statementDate2 = arStatement.StatementDate;
      DateTime dateTime1 = statementDate2.Value;
      dictionary2[key] = dateTime1;
    }
    if (!isOnDemand || familyStatements.Values.Count != 0)
      return;
    int branchID1 = ((PXGraph) this).Accessinfo.BranchID.Value;
    string currencyID = customer.CuryID;
    if (currencyID == null)
      currencyID = GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXGraph) this).Accessinfo.BranchID.Value
      })).FirstOrDefault<PX.Objects.GL.Branch>().BaseCuryID;
    int customerID1 = customer.BAccountID.Value;
    DateTime statementDate3 = statementDate;
    ARStatementKey statementKey = new ARStatementKey(branchID1, currencyID, customerID1, statementDate3);
    this.GetOrAddStatement(familyStatements, statementKey, statementCycle, customer, isOnDemand);
  }

  private ARStatement GetOrAddStatementForDocument(
    IDictionary<ARStatementKey, ARStatement> familyStatements,
    IDictionary<ARStatementKey, ARStatement> deletedStatementsTrace,
    StatementCycleProcessBO.ARTranPostStatement document,
    Customer customer,
    ARStatementCycle statementCycle,
    DateTime statementDate,
    bool isOnDemand)
  {
    if (document.ARTranPost.Type == "R")
      return (ARStatement) null;
    ARStatementKey arStatementKey1 = document.GetARStatementKey(customer, statementDate);
    ARStatement orAddStatement = this.GetOrAddStatement(familyStatements, arStatementKey1, statementCycle, customer, isOnDemand);
    this.SetPreviousStatementInfo(orAddStatement, deletedStatementsTrace);
    int? baccountId = customer.BAccountID;
    int? statementCustomerId = customer.StatementCustomerID;
    if (!(baccountId.GetValueOrDefault() == statementCustomerId.GetValueOrDefault() & baccountId.HasValue == statementCustomerId.HasValue))
    {
      ARStatementKey arStatementKey2 = arStatementKey1;
      statementCustomerId = customer.StatementCustomerID;
      int customerID = statementCustomerId.Value;
      ARStatementKey statementKey = arStatementKey2.CopyForAnotherCustomer(customerID);
      this.SetPreviousStatementInfo(this.GetOrAddStatement(familyStatements, statementKey, statementCycle, customer, isOnDemand), deletedStatementsTrace);
    }
    return orAddStatement;
  }

  private static StatementCycleProcessBO.ARTranPostStatement[] ParseDBStatementsData(
    string statementType,
    PXResultset<ARTranPostGL> response)
  {
    switch (statementType)
    {
      case "O":
        return (StatementCycleProcessBO.ARTranPostStatement[]) ((IQueryable<PXResult<ARTranPostGL>>) response).Select<PXResult<ARTranPostGL>, StatementCycleProcessBO.ARTranPostOpenItem>((Expression<Func<PXResult<ARTranPostGL>, StatementCycleProcessBO.ARTranPostOpenItem>>) (_ => new StatementCycleProcessBO.ARTranPostOpenItem(_))).ToArray<StatementCycleProcessBO.ARTranPostOpenItem>();
      case "B":
        return (StatementCycleProcessBO.ARTranPostStatement[]) ((IQueryable<PXResult<ARTranPostGL>>) response).Select<PXResult<ARTranPostGL>, StatementCycleProcessBO.ARTranPostBBF>((Expression<Func<PXResult<ARTranPostGL>, StatementCycleProcessBO.ARTranPostBBF>>) (_ => new StatementCycleProcessBO.ARTranPostBBF(_))).ToArray<StatementCycleProcessBO.ARTranPostBBF>();
      default:
        throw new PXInvalidOperationException("Unknown customer statement type.");
    }
  }

  private IEnumerable<StatementCycleProcessBO.ARTranPostStatement> GetDataForStatement(
    Customer customer,
    DateTime statementDate)
  {
    PXSelectBase<ARTranPostGL> pxSelectBase = (PXSelectBase<ARTranPostGL>) new PXSelectJoin<ARTranPostGL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARTranPostGL.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>, LeftJoin<ARRegister, On<ARTranPostGL.docType, Equal<ARRegister.docType>, And<ARTranPostGL.refNbr, Equal<ARRegister.refNbr>>>, LeftJoin<ARRegister2, On<ARTranPostGL.sourceDocType, Equal<ARRegister2.docType>, And<ARTranPostGL.sourceRefNbr, Equal<ARRegister2.refNbr>>>>>>, Where<ARRegister.customerID, Equal<Required<ARTranPostGL.customerID>>, And<ARTranPostGL.docDate, LessEqual<Required<ARTranPostGL.docDate>>>>>((PXGraph) this);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
      pxSelectBase.WhereAnd<Where<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.docType, NotEqual<ARDocType.prepaymentInvoice>>>>>.Or<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.pendingPayment, Equal<True>>>>, And<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsEqual<ARRegister.aRAccountID>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.pendingPayment, Equal<False>>>>>.And<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsNotEqual<ARRegister.aRAccountID>>>>>>>>();
    if (customer.StatementType == "O")
      pxSelectBase.WhereAnd<Where<ARRegister.closedTranPeriodID, IsNull, Or<ARRegister.closedDate, Greater<Required<ARRegister.closedDate>>>>>();
    else
      pxSelectBase.WhereAnd<Where<ARRegister.closedTranPeriodID, IsNull, Or<ARRegister.closedDate, GreaterEqual<Required<ARRegister.closedDate>>, Or<ARRegister.statementDate, IsNull, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.type, NotEqual<ARTranPost.type.origin>>>>>.And<BqlOperand<ARTranPostGL.statementDate, IBqlDateTime>.IsNull>>>>>>();
    object[] objArray = new object[3]
    {
      (object) customer.BAccountID,
      (object) statementDate,
      (object) (customer.StatementType == "O" ? new DateTime?(statementDate) : this.FinPeriodRepository.GetFinPeriodByDate(new DateTime?(statementDate), new int?(0)).StartDate)
    };
    StatementCycleProcessBO.ARTranPostStatement[] dbStatementsData = StatementCycleProcessBO.ParseDBStatementsData(customer.StatementType, pxSelectBase.Select(objArray));
    foreach (StatementCycleProcessBO.ARTranPostStatement[] source in ((IEnumerable<StatementCycleProcessBO.ARTranPostStatement>) dbStatementsData).GroupBy<StatementCycleProcessBO.ARTranPostStatement, DocumentKey>((Func<StatementCycleProcessBO.ARTranPostStatement, DocumentKey>) (_ => new DocumentKey((IDocumentKey) _))).ToDictionary<IGrouping<DocumentKey, StatementCycleProcessBO.ARTranPostStatement>, DocumentKey, StatementCycleProcessBO.ARTranPostStatement[]>((Func<IGrouping<DocumentKey, StatementCycleProcessBO.ARTranPostStatement>, DocumentKey>) (_ => _.Key), (Func<IGrouping<DocumentKey, StatementCycleProcessBO.ARTranPostStatement>, StatementCycleProcessBO.ARTranPostStatement[]>) (_ => _.ToArray<StatementCycleProcessBO.ARTranPostStatement>())).Values)
    {
      Decimal num1 = ((IEnumerable<StatementCycleProcessBO.ARTranPostStatement>) source).Sum<StatementCycleProcessBO.ARTranPostStatement>((Func<StatementCycleProcessBO.ARTranPostStatement, Decimal>) (_ => _.ARTranPost.CuryBalanceAmt.Value));
      Decimal num2 = ((IEnumerable<StatementCycleProcessBO.ARTranPostStatement>) source).Sum<StatementCycleProcessBO.ARTranPostStatement>((Func<StatementCycleProcessBO.ARTranPostStatement, Decimal>) (_ => _.ARTranPost.BalanceAmt.Value));
      foreach (StatementCycleProcessBO.ARTranPostStatement tranPostStatement in ((IEnumerable<StatementCycleProcessBO.ARTranPostStatement>) source).Where<StatementCycleProcessBO.ARTranPostStatement>((Func<StatementCycleProcessBO.ARTranPostStatement, bool>) (_ => _.ARRegister.RefNbr != null)))
      {
        ARRegister arRegister1 = tranPostStatement.ARRegister;
        Decimal? signBalance = tranPostStatement.ARRegister.SignBalance;
        Decimal num3 = num1;
        Decimal? nullable1 = signBalance.HasValue ? new Decimal?(signBalance.GetValueOrDefault() * num3) : new Decimal?();
        arRegister1.CuryDocBal = nullable1;
        ARRegister arRegister2 = tranPostStatement.ARRegister;
        signBalance = tranPostStatement.ARRegister.SignBalance;
        Decimal num4 = num2;
        Decimal? nullable2 = signBalance.HasValue ? new Decimal?(signBalance.GetValueOrDefault() * num4) : new Decimal?();
        arRegister2.DocBal = nullable2;
      }
    }
    return (IEnumerable<StatementCycleProcessBO.ARTranPostStatement>) dbStatementsData;
  }

  private void SetPreviousStatementInfo(
    ARStatement statement,
    IDictionary<ARStatementKey, ARStatement> statementsTrace)
  {
    if (statement.Processed.GetValueOrDefault())
      return;
    ARStatement previousStatement = this.GetPreviousStatement(statement.BranchID, statement.CustomerID, statement.CuryID);
    if (previousStatement != null)
      statement.PrevStatementDate = previousStatement.StatementDate;
    ARStatement arStatement;
    if (statementsTrace.TryGetValue(new ARStatementKey(statement), out arStatement))
    {
      statement.PrevPrintedCnt = arStatement.PrevPrintedCnt;
      statement.PrevEmailedCnt = arStatement.PrevEmailedCnt;
    }
    statement.Processed = new bool?(true);
  }

  protected virtual ARStatement GetOrAddStatement(
    IDictionary<ARStatementKey, ARStatement> statementsDictionary,
    ARStatementKey statementKey,
    ARStatementCycle statementCycle,
    Customer customer,
    bool isOnDemand)
  {
    return statementsDictionary.GetOrAdd<ARStatementKey, ARStatement>(statementKey, (Func<ARStatement>) (() =>
    {
      ARStatement statement = this.CombineStatementCustomizable(statementKey, statementCycle, customer, isOnDemand);
      using (new PXLocaleScope(statement.LocaleName))
        this.FillBucketDescriptions(statement, statementCycle);
      return statement;
    }));
  }

  /// <param name="familyCustomer">
  /// Any customer from the family. The method operates under
  /// assumption that all customers in the family share their
  /// statement parameters, such as statement type, printing
  /// flags etc.
  /// </param>
  public static ARStatement CombineStatement(
    ARStatementKey statementKey,
    ARStatementCycle statementCycle,
    Customer familyCustomer,
    bool isOnDemand)
  {
    ARStatement statement = new ARStatement();
    StatementCycleProcessBO.SetStatementAgeDaysToZero(statement);
    StatementCycleProcessBO.SetStatementAgeBalancesToZero(statement);
    statement.BranchID = new int?(statementKey.BranchID);
    statement.CuryID = statementKey.CurrencyID;
    statement.CustomerID = new int?(statementKey.CustomerID);
    statement.StatementDate = new DateTime?(statementKey.StatementDate);
    statement.StatementCycleId = statementCycle.StatementCycleId;
    statement.StatementCustomerID = familyCustomer.StatementCustomerID;
    statement.StatementType = familyCustomer.StatementType;
    statement.DontPrint = new bool?(!familyCustomer.PrintStatements.GetValueOrDefault());
    statement.DontEmail = new bool?(!familyCustomer.SendStatementByEmail.GetValueOrDefault());
    statement.OnDemand = new bool?(isOnDemand);
    statement.AgeDays00 = new short?((short) 0);
    statement.AgeDays01 = statementCycle.AgeDays00;
    statement.AgeDays02 = statementCycle.AgeDays01;
    statement.AgeDays03 = statementCycle.AgeDays02;
    statement.LocaleName = familyCustomer.LocaleName ?? CultureInfo.CurrentCulture.Name;
    return statement;
  }

  protected static void SetStatementAgeDaysToZero(ARStatement statement)
  {
    ARStatement arStatement1 = statement;
    ARStatement arStatement2 = statement;
    ARStatement arStatement3 = statement;
    ARStatement arStatement4 = statement;
    short? nullable1 = new short?((short) 0);
    short? nullable2 = nullable1;
    arStatement4.AgeDays03 = nullable2;
    short? nullable3;
    short? nullable4 = nullable3 = nullable1;
    arStatement3.AgeDays02 = nullable3;
    short? nullable5;
    short? nullable6 = nullable5 = nullable4;
    arStatement2.AgeDays01 = nullable5;
    short? nullable7 = nullable6;
    arStatement1.AgeDays00 = nullable7;
  }

  protected static void SetStatementAgeBalancesToZero(ARStatement statement)
  {
    ARStatement arStatement1 = statement;
    ARStatement arStatement2 = statement;
    ARStatement arStatement3 = statement;
    ARStatement arStatement4 = statement;
    ARStatement arStatement5 = statement;
    ARStatement arStatement6 = statement;
    ARStatement arStatement7 = statement;
    ARStatement arStatement8 = statement;
    ARStatement arStatement9 = statement;
    ARStatement arStatement10 = statement;
    ARStatement arStatement11 = statement;
    ARStatement arStatement12 = statement;
    ARStatement arStatement13 = statement;
    ARStatement arStatement14 = statement;
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = nullable1;
    arStatement14.CuryEndBalance = nullable2;
    Decimal? nullable3;
    Decimal? nullable4 = nullable3 = nullable1;
    arStatement13.CuryBegBalance = nullable3;
    Decimal? nullable5;
    Decimal? nullable6 = nullable5 = nullable4;
    arStatement12.EndBalance = nullable5;
    Decimal? nullable7;
    Decimal? nullable8 = nullable7 = nullable6;
    arStatement11.BegBalance = nullable7;
    Decimal? nullable9;
    Decimal? nullable10 = nullable9 = nullable8;
    arStatement10.CuryAgeBalance04 = nullable9;
    Decimal? nullable11;
    Decimal? nullable12 = nullable11 = nullable10;
    arStatement9.CuryAgeBalance03 = nullable11;
    Decimal? nullable13;
    Decimal? nullable14 = nullable13 = nullable12;
    arStatement8.CuryAgeBalance02 = nullable13;
    Decimal? nullable15;
    Decimal? nullable16 = nullable15 = nullable14;
    arStatement7.CuryAgeBalance01 = nullable15;
    Decimal? nullable17;
    Decimal? nullable18 = nullable17 = nullable16;
    arStatement6.CuryAgeBalance00 = nullable17;
    Decimal? nullable19;
    Decimal? nullable20 = nullable19 = nullable18;
    arStatement5.AgeBalance04 = nullable19;
    Decimal? nullable21;
    Decimal? nullable22 = nullable21 = nullable20;
    arStatement4.AgeBalance03 = nullable21;
    Decimal? nullable23;
    Decimal? nullable24 = nullable23 = nullable22;
    arStatement3.AgeBalance02 = nullable23;
    Decimal? nullable25;
    Decimal? nullable26 = nullable25 = nullable24;
    arStatement2.AgeBalance01 = nullable25;
    Decimal? nullable27 = nullable26;
    arStatement1.AgeBalance00 = nullable27;
  }

  /// <summary>
  /// Creates a new <see cref="T:PX.Objects.AR.ARStatementDetail" /> record using the information
  /// from the given customer statement and document records.
  /// </summary>
  /// <param name="statement">The statement to which the created detail belongs.</param>
  /// <param name="document">The document to which the created detail corresponds.</param>
  protected static ARStatementDetail CombineStatementDetail(
    ARStatement statement,
    StatementCycleProcessBO.ARTranPostStatement document)
  {
    return new ARStatementDetail()
    {
      DocType = document.ARTranPost.DocType,
      RefNbr = document.ARTranPost.RefNbr,
      BranchID = document.ARTranPost.BranchID,
      DocBalance = document.ARRegister.DocBal ?? document.ARTranPost.BalanceAmt,
      CuryDocBalance = document.ARRegister.CuryDocBal ?? document.ARTranPost.CuryBalanceAmt,
      IsOpen = (bool?) document.ARRegister?.OpenDoc,
      CustomerID = statement.CustomerID,
      CuryID = statement.CuryID,
      StatementDate = statement.StatementDate,
      RefNoteID = document.ARTranPost.RefNoteID,
      TranPostID = document.ARTranPost.ID
    };
  }

  protected virtual ARStatement CombineStatementCustomizable(
    ARStatementKey statementKey,
    ARStatementCycle statementCycle,
    Customer familyCustomer,
    bool isOnDemand)
  {
    return StatementCycleProcessBO.CombineStatement(statementKey, statementCycle, familyCustomer, isOnDemand);
  }

  protected virtual ARStatementDetail CombineStatementDetailCustomizable(
    ARStatement statement,
    StatementCycleProcessBO.ARTranPostStatement document)
  {
    return StatementCycleProcessBO.CombineStatementDetail(statement, document);
  }

  protected virtual void FillBucketDescriptions(
    ARStatement statement,
    ARStatementCycle statementCycle)
  {
    DateTime currentDate = statement.StatementDate.Value;
    if (statementCycle.UseFinPeriodForAging.GetValueOrDefault())
    {
      IList<string> array = (IList<string>) AgingEngine.GetPeriodAgingBucketDescriptions(this.FinPeriodRepository, currentDate, AgingDirection.Backwards, 5).ToArray<string>();
      statement.AgeBucketCurrentDescription = array[0];
      statement.AgeBucket01Description = array[1];
      statement.AgeBucket02Description = array[2];
      statement.AgeBucket03Description = array[3];
      statement.AgeBucket04Description = array[4];
    }
    else
    {
      int[] bucketBoundaries = new int[4];
      short? nullable = statement.AgeDays00;
      bucketBoundaries[0] = (int) nullable.GetValueOrDefault();
      nullable = statement.AgeDays01;
      bucketBoundaries[1] = (int) nullable.GetValueOrDefault();
      nullable = statement.AgeDays02;
      bucketBoundaries[2] = (int) nullable.GetValueOrDefault();
      nullable = statement.AgeDays03;
      bucketBoundaries[3] = (int) nullable.GetValueOrDefault();
      IList<string> array = (IList<string>) AgingEngine.GetDayAgingBucketDescriptions(AgingDirection.Backwards, (IEnumerable<int>) bucketBoundaries, false).ToArray<string>();
      PXCache cach = ((PXGraph) this).Caches[typeof (ARStatementCycle)];
      statement.AgeBucketCurrentDescription = this.GetBucketDescription<ARStatementCycle.ageMsgCurrent>(cach, (object) statementCycle) ?? array[0];
      statement.AgeBucket01Description = this.GetBucketDescription<ARStatementCycle.ageMsg00>(cach, (object) statementCycle) ?? array[1];
      statement.AgeBucket02Description = this.GetBucketDescription<ARStatementCycle.ageMsg01>(cach, (object) statementCycle) ?? array[2];
      statement.AgeBucket03Description = this.GetBucketDescription<ARStatementCycle.ageMsg02>(cach, (object) statementCycle) ?? array[3];
      statement.AgeBucket04Description = this.GetBucketDescription<ARStatementCycle.ageMsg03>(cach, (object) statementCycle) ?? array[4];
    }
  }

  private string GetBucketDescription<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return (valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt) as string;
  }

  /// <summary>
  /// Fills the age balances of relevant statements from the provided statement
  /// dictionary based on the information of invoices and payments open on the
  /// statement date.
  /// </summary>
  protected virtual void AccumulateAgeBalancesIntoStatements(
    ARStatementCycle statementCycle,
    DateTime statementDate,
    IEnumerable<ARRegister> openDocuments,
    IDictionary<ARStatementKey, ARStatement> statements,
    bool ageCredits)
  {
    foreach (ARRegister openDocument in openDocuments)
    {
      int? nullable = openDocument.BranchID;
      int branchID = nullable.Value;
      string curyId = openDocument.CuryID;
      nullable = openDocument.CustomerID;
      int customerID = nullable.Value;
      DateTime statementDate1 = statementDate;
      ARStatementKey key = new ARStatementKey(branchID, curyId, customerID, statementDate1);
      if (statements.ContainsKey(key) && openDocument.DocType != "CSL" && openDocument.DocType != "RCS")
        StatementCycleProcessBO.AccumulateAgeBalances((PXGraph) this, statementCycle, statements[key], openDocument, ageCredits);
    }
  }

  protected static void AccumulateAgeBalances(
    PXGraph graph,
    ARStatementCycle statementCycle,
    ARStatement statement,
    ARRegister document,
    bool ageCredits)
  {
    bool? nullable1;
    if (document.Payable.GetValueOrDefault() && document?.DocType != "SMC")
    {
      if (document.IsPrepaymentInvoiceDocument())
      {
        nullable1 = document.PendingPayment;
        bool flag = false;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
          goto label_5;
      }
      else
        goto label_5;
    }
    int num1;
    if (!(document.DocType == "REF"))
    {
      num1 = document.DocType == "VRF" ? 1 : 0;
      goto label_6;
    }
label_5:
    num1 = 1;
label_6:
    int num2 = ageCredits ? 1 : 0;
    if ((num1 | num2) != 0)
    {
      DateTime dateTime1 = statement.StatementDate.Value;
      ARInvoice arInvoice = ARInvoice.PK.Find(graph, document?.DocType, document?.RefNbr);
      if (statementCycle.AgeBasedOn == "U")
      {
        nullable1 = document.Payable;
        if (nullable1.GetValueOrDefault())
          goto label_11;
      }
      DateTime dateTime2;
      if (((!(statementCycle.AgeBasedOn == "U") ? 0 : (document?.DocType == "CRM" ? 1 : 0)) & (ageCredits ? 1 : 0)) == 0 || arInvoice == null || arInvoice.TermsID == null)
      {
        dateTime2 = document.DocDate.Value;
        goto label_12;
      }
label_11:
      dateTime2 = document.DueDate.Value;
label_12:
      DateTime dateToAge = dateTime2;
      nullable1 = statementCycle.UseFinPeriodForAging;
      int num3;
      if (!nullable1.GetValueOrDefault())
        num3 = AgingEngine.AgeByDays(dateTime1, dateToAge, AgingDirection.Backwards, (int) statement.AgeDays00.GetValueOrDefault(), (int) statement.AgeDays01.GetValueOrDefault(), (int) statement.AgeDays02.GetValueOrDefault(), (int) statement.AgeDays03.GetValueOrDefault());
      else
        num3 = AgingEngine.AgeByPeriods(dateTime1, dateToAge, graph.GetService<IFinPeriodRepository>(), AgingDirection.Backwards, 5);
      int num4 = num3;
      nullable1 = document.Paying;
      int num5 = !nullable1.GetValueOrDefault() || !(document.DocType != "REF") || !(document.DocType != "VRF") ? 1 : -1;
      Decimal num6 = (Decimal) num5;
      Decimal? nullable2 = document.DocBal;
      Decimal num7 = nullable2.Value;
      Decimal num8 = num6 * num7;
      Decimal num9 = (Decimal) num5;
      nullable2 = document.CuryDocBal;
      Decimal num10 = nullable2.Value;
      Decimal num11 = num9 * num10;
      switch (num4)
      {
        case 0:
          ARStatement arStatement1 = statement;
          nullable2 = statement.AgeBalance00;
          Decimal? nullable3 = new Decimal?(nullable2.GetValueOrDefault() + num8);
          arStatement1.AgeBalance00 = nullable3;
          ARStatement arStatement2 = statement;
          nullable2 = statement.CuryAgeBalance00;
          Decimal? nullable4 = new Decimal?(nullable2.GetValueOrDefault() + num11);
          arStatement2.CuryAgeBalance00 = nullable4;
          break;
        case 1:
          ARStatement arStatement3 = statement;
          nullable2 = statement.AgeBalance01;
          Decimal? nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num8);
          arStatement3.AgeBalance01 = nullable5;
          ARStatement arStatement4 = statement;
          nullable2 = statement.CuryAgeBalance01;
          Decimal? nullable6 = new Decimal?(nullable2.GetValueOrDefault() + num11);
          arStatement4.CuryAgeBalance01 = nullable6;
          break;
        case 2:
          ARStatement arStatement5 = statement;
          nullable2 = statement.AgeBalance02;
          Decimal? nullable7 = new Decimal?(nullable2.GetValueOrDefault() + num8);
          arStatement5.AgeBalance02 = nullable7;
          ARStatement arStatement6 = statement;
          nullable2 = statement.CuryAgeBalance02;
          Decimal? nullable8 = new Decimal?(nullable2.GetValueOrDefault() + num11);
          arStatement6.CuryAgeBalance02 = nullable8;
          break;
        case 3:
          ARStatement arStatement7 = statement;
          nullable2 = statement.AgeBalance03;
          Decimal? nullable9 = new Decimal?(nullable2.GetValueOrDefault() + num8);
          arStatement7.AgeBalance03 = nullable9;
          ARStatement arStatement8 = statement;
          nullable2 = statement.CuryAgeBalance03;
          Decimal? nullable10 = new Decimal?(nullable2.GetValueOrDefault() + num11);
          arStatement8.CuryAgeBalance03 = nullable10;
          break;
        case 4:
          ARStatement arStatement9 = statement;
          nullable2 = statement.AgeBalance04;
          Decimal? nullable11 = new Decimal?(nullable2.GetValueOrDefault() + num8);
          arStatement9.AgeBalance04 = nullable11;
          ARStatement arStatement10 = statement;
          nullable2 = statement.CuryAgeBalance04;
          Decimal? nullable12 = new Decimal?(nullable2.GetValueOrDefault() + num11);
          arStatement10.CuryAgeBalance04 = nullable12;
          break;
        default:
          throw new PXException("The system could not age one of the documents included in the statement because an unexpected aging period number was produced by the aging engine. Please contact support service.");
      }
    }
    else
    {
      ARStatement arStatement11 = statement;
      Decimal? nullable13 = statement.AgeBalance04;
      Decimal? nullable14 = document.DocBal;
      Decimal? nullable15 = nullable13.HasValue & nullable14.HasValue ? new Decimal?(nullable13.GetValueOrDefault() - nullable14.GetValueOrDefault()) : new Decimal?();
      arStatement11.AgeBalance04 = nullable15;
      ARStatement arStatement12 = statement;
      nullable14 = statement.CuryAgeBalance04;
      nullable13 = document.CuryDocBal;
      Decimal? nullable16 = nullable14.HasValue & nullable13.HasValue ? new Decimal?(nullable14.GetValueOrDefault() - nullable13.GetValueOrDefault()) : new Decimal?();
      arStatement12.CuryAgeBalance04 = nullable16;
    }
  }

  /// <param name="isOnDemand">
  /// If set to <c>true</c>, indicates that the existing statement should be deleted
  /// so that a new on-demand statement will be generated on that date.
  /// </param>
  protected virtual IEnumerable<ARStatement> DeleteCustomerStatement(
    Customer customer,
    DateTime statementDate,
    bool isOnDemand)
  {
    StatementCreateBO instance = PXGraph.CreateInstance<StatementCreateBO>();
    List<ARStatement> arStatementList = new List<ARStatement>();
    int num = 0;
    foreach (PXResult<ARStatement> pxResult in ((PXSelectBase<ARStatement>) instance.CustomerStatement).Select(new object[2]
    {
      (object) customer.BAccountID,
      (object) statementDate
    }))
    {
      ARStatement statement = PXResult<ARStatement>.op_Implicit(pxResult);
      arStatementList.Add(StatementCycleProcessBO.StatementTrace(statement));
      ((PXSelectBase<ARStatement>) instance.CustomerStatement).Delete(statement);
      ++num;
    }
    if (num == 0 && !isOnDemand)
      PXUpdate<Set<PX.Objects.AR.Override.Customer.statementLastDate, Required<PX.Objects.AR.Override.Customer.statementLastDate>>, PX.Objects.AR.Override.Customer, Where<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<PX.Objects.AR.Override.Customer.bAccountID>>>>.Update((PXGraph) this, new object[2]
      {
        (object) instance.FindLastCstmStatementDate(customer.BAccountID, new DateTime?(statementDate)),
        (object) customer.BAccountID
      });
    StatementCycleProcessBO.ResetDocumentsLastStatementDate((PXGraph) instance, new DateTime?(statementDate), customer.BAccountID);
    ((PXGraph) instance).Actions.PressSave();
    return (IEnumerable<ARStatement>) arStatementList;
  }

  private void EnsureNoRegularStatementExists(int? customerID, DateTime statementDate)
  {
    if (((PXSelectBase<ARStatement>) new PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And<ARStatement.statementDate, Equal<Required<ARStatement.statementDate>>, And<ARStatement.onDemand, NotEqual<True>>>>>((PXGraph) this)).Any<ARStatement>((object) customerID, (object) statementDate))
      throw new PXException("The statement that covers the selected date has already been generated for the customer.");
  }

  protected static ARStatement StatementTrace(ARStatement statement)
  {
    ARStatement arStatement1 = new ARStatement()
    {
      BranchID = statement.BranchID,
      CuryID = statement.CuryID,
      CustomerID = statement.CustomerID,
      StatementDate = statement.StatementDate,
      PrevPrintedCnt = statement.PrevPrintedCnt,
      PrevEmailedCnt = statement.PrevEmailedCnt
    };
    short? nullable;
    if (statement.Printed.GetValueOrDefault())
    {
      ARStatement arStatement2 = arStatement1;
      nullable = arStatement2.PrevPrintedCnt;
      arStatement2.PrevPrintedCnt = nullable.HasValue ? new short?((short) ((int) nullable.GetValueOrDefault() + 1)) : new short?();
    }
    if (statement.Emailed.GetValueOrDefault())
    {
      ARStatement arStatement3 = arStatement1;
      nullable = arStatement3.PrevEmailedCnt;
      arStatement3.PrevEmailedCnt = nullable.HasValue ? new short?((short) ((int) nullable.GetValueOrDefault() + 1)) : new short?();
    }
    return arStatement1;
  }

  [PXProjection(typeof (Select5<ARBalances, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARBalances.customerID>>>, Where<ARBalances.lastDocDate, IsNull, Or<Where<ARBalances.statementRequired, Equal<True>, Or<ARBalances.lastDocDate, Greater<Customer.statementLastDate>>>>>, Aggregate<GroupBy<ARBalances.customerID>>>), Persistent = false)]
  public class CustomerWithActiveBalance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlField = typeof (ARBalances.customerID))]
    public virtual int? CustomerID { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      StatementCycleProcessBO.CustomerWithActiveBalance.customerID>
    {
    }
  }

  private class CustomerIDComparer : IEqualityComparer<Customer>
  {
    public bool Equals(Customer x, Customer y)
    {
      int? baccountId1 = x.BAccountID;
      int? baccountId2 = y.BAccountID;
      return baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue;
    }

    public int GetHashCode(Customer obj) => obj.BAccountID.GetHashCode();
  }

  protected abstract class ARTranPostStatement : IDocumentKey
  {
    public ARTranPostGL ARTranPost { get; }

    public ARRegister ARRegister { get; }

    public ARRegister2 SourceARRegister { get; }

    public string DocType
    {
      get => this.ARTranPost.DocType;
      set => this.ARTranPost.DocType = value;
    }

    public string RefNbr
    {
      get => this.ARTranPost.RefNbr;
      set => this.ARTranPost.RefNbr = value;
    }

    public ARTranPostStatement(PXResult<ARTranPostGL> pXResult)
    {
      this.ARTranPost = PXResult<ARTranPostGL>.op_Implicit(pXResult);
      this.ARRegister = ((PXResult) pXResult).GetItem<ARRegister>();
      this.SourceARRegister = ((PXResult) pXResult).GetItem<ARRegister2>();
    }

    public ARStatementKey GetARStatementKey(Customer customer, DateTime statementDate)
    {
      int? nullable = this.ARTranPost.BranchID;
      int branchID = nullable.Value;
      string curyId = this.ARTranPost.CuryID;
      nullable = customer.BAccountID;
      int customerID = nullable.Value;
      DateTime statementDate1 = statementDate;
      return new ARStatementKey(branchID, curyId, customerID, statementDate1);
    }

    public bool ARRegisterHasBalance()
    {
      if (this.ARRegister.IsPrepaymentInvoiceDocument())
      {
        if ("S" == this.ARTranPost.Type && this.ARRegister.PendingPayment.GetValueOrDefault())
        {
          int? accountId = this.ARTranPost.AccountID;
          int? arAccountId = this.ARRegister.ARAccountID;
          if (accountId.GetValueOrDefault() == arAccountId.GetValueOrDefault() & accountId.HasValue == arAccountId.HasValue && this.ARRegister.CuryDocBal.IsNonZero())
            return this.ARRegister.DocBal.IsNonZero();
        }
        return false;
      }
      return "S" == this.ARTranPost.Type && this.ARRegister.HasBalance();
    }

    public virtual void AdjustStatementEndBalance(ARStatement statement)
    {
      if (this.ARTranPost.Type != "S")
        return;
      if (this.ARRegister.IsPrepaymentInvoiceDocument())
      {
        bool? pendingPayment = this.ARRegister.PendingPayment;
        bool flag = false;
        if (pendingPayment.GetValueOrDefault() == flag & pendingPayment.HasValue)
          return;
      }
      ARStatement arStatement1 = statement;
      Decimal? endBalance = arStatement1.EndBalance;
      Decimal? signBalance = this.ARRegister.SignBalance;
      Decimal? nullable1 = this.ARRegister.DocBal;
      Decimal? nullable2 = signBalance.HasValue & nullable1.HasValue ? new Decimal?(signBalance.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!(endBalance.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(endBalance.GetValueOrDefault() + nullable2.GetValueOrDefault());
      arStatement1.EndBalance = nullable3;
      ARStatement arStatement2 = statement;
      Decimal? curyEndBalance = arStatement2.CuryEndBalance;
      nullable1 = this.ARRegister.SignBalance;
      Decimal? curyDocBal = this.ARRegister.CuryDocBal;
      Decimal? nullable4 = nullable1.HasValue & curyDocBal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * curyDocBal.GetValueOrDefault()) : new Decimal?();
      arStatement2.CuryEndBalance = curyEndBalance.HasValue & nullable4.HasValue ? new Decimal?(curyEndBalance.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    }

    public abstract bool ShouldBeConvertedToStatementDetail();
  }

  protected class ARTranPostOpenItem(PXResult<ARTranPostGL> pXResult) : 
    StatementCycleProcessBO.ARTranPostStatement(pXResult)
  {
    public override bool ShouldBeConvertedToStatementDetail() => this.ARRegisterHasBalance();
  }

  protected class ARTranPostBBF(PXResult<ARTranPostGL> pXResult) : 
    StatementCycleProcessBO.ARTranPostStatement(pXResult)
  {
    private bool WithinSameCuryBranchCustomer()
    {
      if (this.ARTranPost.CuryID == this.SourceARRegister.CuryID)
      {
        int? branchId = this.ARTranPost.BranchID;
        int? nullable = this.SourceARRegister.BranchID;
        if (branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue)
        {
          nullable = this.ARTranPost.CustomerID;
          int? customerId = this.SourceARRegister.CustomerID;
          return nullable.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable.HasValue == customerId.HasValue;
        }
      }
      return false;
    }

    private bool ShouldDocumentBePresent() => !this.ARRegister.StatementDate.HasValue;

    private bool ShouldSourceDocumentBePresent() => !this.SourceARRegister.StatementDate.HasValue;

    private bool ShouldApplicationBePresent() => !this.ARTranPost.StatementDate.HasValue;

    private bool IsSelfVoidingApplication
    {
      get
      {
        return this.SourceARRegister.Voided.GetValueOrDefault() && ARDocType.IsSelfVoiding(this.ARTranPost.SourceDocType);
      }
    }

    public override bool ShouldBeConvertedToStatementDetail()
    {
      switch (this.ARTranPost.Type)
      {
        case "S":
          return this.ShouldDocumentBePresent();
        case "D":
          if (!this.ShouldApplicationBePresent())
            return false;
          if (!this.WithinSameCuryBranchCustomer())
            return true;
          if (this.IsSelfVoidingApplication && !this.ARTranPost.VoidAdjNbr.HasValue || this.ShouldDocumentBePresent())
            return false;
          return this.ShouldSourceDocumentBePresent() || this.IsSelfVoidingApplication;
        case "G":
          return (!this.IsSelfVoidingApplication || this.ARTranPost.VoidAdjNbr.HasValue) && this.ShouldApplicationBePresent();
        default:
          return false;
      }
    }
  }
}
