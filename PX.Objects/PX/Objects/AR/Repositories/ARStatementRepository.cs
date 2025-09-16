// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.ARStatementRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class ARStatementRepository(PXGraph graph) : RepositoryBase<ARStatement>(graph)
{
  /// <summary>
  /// Finds the last statement in the specified statement cycle.
  /// </summary>
  public ARStatement FindLastStatement(
    string statementCycleId,
    DateTime? priorToDate = null,
    bool includeOnDemand = false)
  {
    return this.SelectSingle<Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And2<Where<ARStatement.onDemand, Equal<False>, Or<ARStatement.onDemand, Equal<Required<ARStatement.onDemand>>>>, And<Where<ARStatement.statementDate, Less<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>>, OrderBy<Desc<ARStatement.statementDate>>>((object) statementCycleId, (object) includeOnDemand, (object) priorToDate, (object) priorToDate);
  }

  public ARStatement FindLastStatement(
    Customer customer,
    DateTime? priorToDate = null,
    bool includeOnDemand = false)
  {
    return this.SelectSingle<Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And<ARStatement.customerID, Equal<Required<ARStatement.customerID>>, And2<Where<ARStatement.onDemand, Equal<False>, Or<ARStatement.onDemand, Equal<Required<ARStatement.onDemand>>>>, And<Where<ARStatement.statementDate, Less<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>>>, OrderBy<Desc<ARStatement.statementDate>>>((object) customer.StatementCycleId, (object) customer.StatementCustomerID, (object) includeOnDemand, (object) priorToDate, (object) priorToDate);
  }

  public ARStatement FindFirstStatement(
    string statementCycleId,
    DateTime? afterDate = null,
    bool includeOnDemand = false)
  {
    return this.SelectSingle<Where<ARStatement.statementCycleId, Equal<Required<ARStatement.statementCycleId>>, And2<Where<ARStatement.onDemand, Equal<False>, Or<ARStatement.onDemand, Equal<Required<ARStatement.onDemand>>>>, And<Where<ARStatement.statementDate, Greater<Required<ARStatement.statementDate>>, Or<Required<ARStatement.statementDate>, IsNull>>>>>, OrderBy<Asc<ARStatement.statementDate>>>((object) statementCycleId, (object) includeOnDemand, (object) afterDate, (object) afterDate);
  }
}
