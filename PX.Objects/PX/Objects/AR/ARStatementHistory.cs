// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARStatementHistory : PXGraph<
#nullable disable
ARStatementHistory>
{
  public PXFilter<ARStatementHistory.ARStatementHistoryParameters> Filter;
  public PXCancel<ARStatementHistory.ARStatementHistoryParameters> Cancel;
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<ARStatementHistory.HistoryResult, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<Asc<ARStatementHistory.HistoryResult.statementCycleId, Desc<ARStatementHistory.HistoryResult.statementDate>>>>, ARStatementHistory.HistoryResult>.View History;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<ARStatementHistory.ARStatementHistoryParameters> viewDetails;
  public PXAction<ARStatementHistory.ARStatementHistoryParameters> printReport;

  public ARStatementHistory()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.History).Cache.AllowDelete = false;
    ((PXSelectBase) this.History).Cache.AllowInsert = false;
    ((PXSelectBase) this.History).Cache.AllowUpdate = false;
  }

  protected virtual IEnumerable history()
  {
    ARStatementHistory statementHistory = this;
    ((PXSelectBase) statementHistory.History).Cache.Clear();
    ARStatementHistory.ARStatementHistoryParameters current = ((PXSelectBase<ARStatementHistory.ARStatementHistoryParameters>) statementHistory.Filter).Current;
    if (current != null)
    {
      int? nullable1 = new int?();
      string str = (string) null;
      ARStatementHistory.HistoryResult historyResult1 = (ARStatementHistory.HistoryResult) null;
      List<object> objectList = new List<object>();
      PXSelectBase<ARStatement> pxSelectBase = (PXSelectBase<ARStatement>) new PXSelectJoin<ARStatement, LeftJoin<ARStatementCycle, On<ARStatementCycle.statementCycleId, Equal<ARStatement.statementCycleId>>, LeftJoin<ARStatementHistory.Customer, On<ARStatementHistory.Customer.bAccountID, Equal<ARStatement.statementCustomerID>>>>, Where<ARStatement.statementCustomerID, Equal<ARStatement.customerID>, And<ARStatement.statementDate, GreaterEqual<Current<ARStatementHistory.ARStatementHistoryParameters.startDate>>, And<ARStatement.statementDate, LessEqual<Current<ARStatementHistory.ARStatementHistoryParameters.endDate>>, And2<Where<ARStatement.statementCycleId, Equal<Current<ARStatementHistory.ARStatementHistoryParameters.statementCycleId>>, Or<Current<ARStatementHistory.ARStatementHistoryParameters.statementCycleId>, IsNull>>, And<Where<ARStatement.onDemand, Equal<False>, Or<Current<ARStatementHistory.ARStatementHistoryParameters.includeOnDemandStatements>, Equal<True>>>>>>>>, OrderBy<Asc<ARStatement.statementCycleId, Asc<ARStatement.statementDate, Asc<ARStatement.customerID, Asc<ARStatement.curyID>>>>>>((PXGraph) statementHistory);
      PXResultset<ARStatement> pxResultset;
      using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[12]
      {
        typeof (ARStatement.branchID),
        typeof (ARStatement.statementCycleId),
        typeof (ARStatement.statementDate),
        typeof (ARStatement.customerID),
        typeof (ARStatement.statementCustomerID),
        typeof (ARStatement.curyID),
        typeof (ARStatement.dontPrint),
        typeof (ARStatement.dontEmail),
        typeof (ARStatement.printed),
        typeof (ARStatement.emailed),
        typeof (ARStatementCycle.descr),
        typeof (ARStatementHistory.Customer.printCuryStatements)
      }))
        pxResultset = pxSelectBase.Select(Array.Empty<object>());
      foreach (PXResult<ARStatement, ARStatementCycle, ARStatementHistory.Customer> pxResult in pxResultset)
      {
        objectList.Add((object) pxResult);
        ARStatement arStatement = PXResult<ARStatement, ARStatementCycle, ARStatementHistory.Customer>.op_Implicit(pxResult);
        ARStatementCycle arStatementCycle = PXResult<ARStatement, ARStatementCycle, ARStatementHistory.Customer>.op_Implicit(pxResult);
        ARStatementHistory.Customer customer = PXResult<ARStatement, ARStatementCycle, ARStatementHistory.Customer>.op_Implicit(pxResult);
        ARStatementHistory.HistoryResult historyResult2 = new ARStatementHistory.HistoryResult()
        {
          StatementCycleId = arStatement.StatementCycleId,
          StatementDate = arStatement.StatementDate,
          Descr = arStatementCycle.Descr,
          DontPrintCount = new int?(0),
          DontEmailCount = new int?(0),
          PrintedCount = new int?(0),
          EmailedCount = new int?(0),
          NoActionCount = new int?(0)
        };
        ARStatementHistory.HistoryResult historyResult3 = (ARStatementHistory.HistoryResult) ((PXSelectBase) statementHistory.History).Cache.Locate((object) historyResult2) ?? ((PXSelectBase<ARStatementHistory.HistoryResult>) statementHistory.History).Insert(historyResult2);
        if (historyResult3 != null)
        {
          if (historyResult1 != historyResult3)
          {
            nullable1 = new int?();
            str = (string) null;
          }
          int? nullable2 = nullable1;
          int? nullable3 = arStatement.CustomerID;
          if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) || customer.PrintCuryStatements.GetValueOrDefault() && str != arStatement.CuryID)
          {
            ARStatementHistory.HistoryResult historyResult4 = historyResult3;
            nullable3 = historyResult4.NumberOfDocuments;
            int? nullable4;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable4 = nullable2;
            }
            else
              nullable4 = new int?(nullable3.GetValueOrDefault() + 1);
            historyResult4.NumberOfDocuments = nullable4;
            ARStatementHistory.HistoryResult historyResult5 = historyResult3;
            nullable3 = historyResult5.DontPrintCount;
            int num1 = arStatement.DontPrint.GetValueOrDefault() ? 1 : 0;
            int? nullable5;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable5 = nullable2;
            }
            else
              nullable5 = new int?(nullable3.GetValueOrDefault() + num1);
            historyResult5.DontPrintCount = nullable5;
            ARStatementHistory.HistoryResult historyResult6 = historyResult3;
            nullable3 = historyResult6.DontEmailCount;
            int num2 = arStatement.DontEmail.GetValueOrDefault() ? 1 : 0;
            int? nullable6;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable6 = nullable2;
            }
            else
              nullable6 = new int?(nullable3.GetValueOrDefault() + num2);
            historyResult6.DontEmailCount = nullable6;
            ARStatementHistory.HistoryResult historyResult7 = historyResult3;
            nullable3 = historyResult7.NoActionCount;
            int num3 = !arStatement.DontEmail.GetValueOrDefault() || !arStatement.DontPrint.GetValueOrDefault() ? 0 : 1;
            int? nullable7;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable7 = nullable2;
            }
            else
              nullable7 = new int?(nullable3.GetValueOrDefault() + num3);
            historyResult7.NoActionCount = nullable7;
            ARStatementHistory.HistoryResult historyResult8 = historyResult3;
            nullable3 = historyResult8.PrintedCount;
            int num4 = arStatement.Printed.GetValueOrDefault() ? 1 : 0;
            int? nullable8;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable8 = nullable2;
            }
            else
              nullable8 = new int?(nullable3.GetValueOrDefault() + num4);
            historyResult8.PrintedCount = nullable8;
            ARStatementHistory.HistoryResult historyResult9 = historyResult3;
            nullable3 = historyResult9.EmailedCount;
            int num5 = arStatement.Emailed.GetValueOrDefault() ? 1 : 0;
            int? nullable9;
            if (!nullable3.HasValue)
            {
              nullable2 = new int?();
              nullable9 = nullable2;
            }
            else
              nullable9 = new int?(nullable3.GetValueOrDefault() + num5);
            historyResult9.EmailedCount = nullable9;
          }
          historyResult1 = historyResult3;
          nullable1 = arStatement.CustomerID;
          str = arStatement.CuryID;
        }
      }
      pxSelectBase.StoreCached(new PXCommandKey(new object[4]
      {
        (object) current.StartDate,
        (object) current.EndDate,
        (object) current.StatementCycleId,
        (object) current.StatementCycleId
      }), objectList);
      ((PXSelectBase) statementHistory.History).Cache.IsDirty = false;
      foreach (ARStatementHistory.HistoryResult historyResult10 in ((PXSelectBase) statementHistory.History).Cache.Inserted)
        yield return (object) historyResult10;
    }
  }

  protected virtual void ARStatementHistoryParameters_StartDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) new DateTime(((PXGraph) this).Accessinfo.BusinessDate.Value.Year, 1, 1);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = true)]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementHistory.HistoryResult>) this.History).Current != null && ((PXSelectBase<ARStatementHistory.ARStatementHistoryParameters>) this.Filter).Current != null)
    {
      ARStatementHistory.HistoryResult current1 = ((PXSelectBase<ARStatementHistory.HistoryResult>) this.History).Current;
      ARStatementDetails instance = PXGraph.CreateInstance<ARStatementDetails>();
      ARStatementDetails.ARStatementDetailsParameters current2 = ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) instance.Filter).Current;
      current2.StatementCycleId = current1.StatementCycleId;
      current2.StatementDate = current1.StatementDate;
      ((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) instance.Filter).Update(current2);
      PXResultset<ARStatementDetails.ARStatementDetailsParameters>.op_Implicit(((PXSelectBase<ARStatementDetails.ARStatementDetailsParameters>) instance.Filter).Select(Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Statement Details");
    }
    return (IEnumerable) ((PXSelectBase<ARStatementHistory.ARStatementHistoryParameters>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable PrintReport(PXAdapter adapter)
  {
    if (((PXSelectBase<ARStatementHistory.HistoryResult>) this.History).Current != null)
    {
      ARStatementPrint instance = PXGraph.CreateInstance<ARStatementPrint>();
      ((PXSelectBase<ARStatementPrint.PrintParameters>) instance.Filter).Current.StatementCycleId = ((PXSelectBase<ARStatementHistory.HistoryResult>) this.History).Current.StatementCycleId;
      ((PXSelectBase<ARStatementPrint.PrintParameters>) instance.Filter).Current.StatementDate = ((PXSelectBase<ARStatementHistory.HistoryResult>) this.History).Current.StatementDate;
      throw new PXRedirectRequiredException((PXGraph) instance, "Report Process");
    }
    return adapter.Get();
  }

  protected static void Copy(
    Dictionary<string, string> aDest,
    ARStatementHistory.HistoryResult aSrc)
  {
    aDest["StatementCycleId"] = aSrc.StatementCycleId;
    aDest["StatementDate"] = aSrc.StatementDate.Value.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  [Serializable]
  public class ARStatementHistoryParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (ARStatementCycle.statementCycleId))]
    public virtual string StatementCycleId { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? StartDate { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? EndDate { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? IncludeOnDemandStatements { get; set; }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementHistory.ARStatementHistoryParameters.statementCycleId>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementHistory.ARStatementHistoryParameters.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementHistory.ARStatementHistoryParameters.endDate>
    {
    }

    public abstract class includeOnDemandStatements : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementHistory.ARStatementHistoryParameters.includeOnDemandStatements>
    {
    }
  }

  [Serializable]
  public class HistoryResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _StatementCycleId;
    protected DateTime? _StatementDate;
    protected string _Descr;
    protected int? _NumberOfDocuments;
    protected int? _DontPrintCount;
    protected int? _PrintedCount;
    protected int? _DontEmailCount;
    protected int? _EmailedCount;
    protected int? _NoActionCount;

    [PXDBString(10, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [PXDBDate(IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Statement Date")]
    public virtual DateTime? StatementDate
    {
      get => this._StatementDate;
      set => this._StatementDate = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string Descr
    {
      get => this._Descr;
      set => this._Descr = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Number Of Documents")]
    public virtual int? NumberOfDocuments
    {
      get => this._NumberOfDocuments;
      set => this._NumberOfDocuments = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Don't Print", Enabled = false)]
    public virtual int? DontPrintCount
    {
      get => this._DontPrintCount;
      set => this._DontPrintCount = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Printed", Enabled = false)]
    public virtual int? PrintedCount
    {
      get => this._PrintedCount;
      set => this._PrintedCount = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Don't Email", Enabled = false)]
    public virtual int? DontEmailCount
    {
      get => this._DontEmailCount;
      set => this._DontEmailCount = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Emailed", Enabled = false)]
    public virtual int? EmailedCount
    {
      get => this._EmailedCount;
      set => this._EmailedCount = value;
    }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "No Action", Enabled = false)]
    public virtual int? NoActionCount
    {
      get => this._NoActionCount;
      set => this._NoActionCount = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "To Print", Enabled = false)]
    public virtual int? ToPrintCount
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementHistory.HistoryResult.numberOfDocuments), typeof (ARStatementHistory.HistoryResult.dontPrintCount)})] get
      {
        int? numberOfDocuments = this.NumberOfDocuments;
        int? dontPrintCount = this.DontPrintCount;
        return !(numberOfDocuments.HasValue & dontPrintCount.HasValue) ? new int?() : new int?(numberOfDocuments.GetValueOrDefault() - dontPrintCount.GetValueOrDefault());
      }
      set => this._PrintedCount = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "To Email", Enabled = false)]
    public virtual int? ToEmailCount
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementHistory.HistoryResult.numberOfDocuments), typeof (ARStatementHistory.HistoryResult.dontEmailCount)})] get
      {
        int? numberOfDocuments = this._NumberOfDocuments;
        int? dontEmailCount = this._DontEmailCount;
        return !(numberOfDocuments.HasValue & dontEmailCount.HasValue) ? new int?() : new int?(numberOfDocuments.GetValueOrDefault() - dontEmailCount.GetValueOrDefault());
      }
      set
      {
      }
    }

    [PXDecimal(2)]
    [PXUIField(DisplayName = "Print Completion %", Enabled = false)]
    public virtual Decimal? PrintCompletion
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementHistory.HistoryResult.toPrintCount), typeof (ARStatementHistory.HistoryResult.printedCount)})] get
      {
        Decimal num1;
        if (this.ToPrintCount.HasValue)
        {
          int? nullable = this.ToPrintCount;
          int num2 = 0;
          if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
          {
            nullable = this.PrintedCount;
            Decimal num3 = (Decimal) nullable.Value;
            nullable = this.ToPrintCount;
            Decimal num4 = (Decimal) nullable.Value;
            num1 = num3 / num4 * 100.0M;
            goto label_4;
          }
        }
        num1 = 100.0M;
label_4:
        return new Decimal?(num1);
      }
      set
      {
      }
    }

    [PXDecimal(2)]
    [PXUIField(DisplayName = "Email Completion %", Enabled = false)]
    public virtual Decimal? EmailCompletion
    {
      [PXDependsOnFields(new Type[] {typeof (ARStatementHistory.HistoryResult.toEmailCount), typeof (ARStatementHistory.HistoryResult.emailedCount)})] get
      {
        Decimal num1;
        if (this.ToEmailCount.HasValue)
        {
          int? nullable = this.ToEmailCount;
          int num2 = 0;
          if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
          {
            nullable = this.EmailedCount;
            Decimal num3 = (Decimal) nullable.Value;
            nullable = this.ToEmailCount;
            Decimal num4 = (Decimal) nullable.Value;
            num1 = num3 / num4 * 100.0M;
            goto label_4;
          }
        }
        num1 = 100.0M;
label_4:
        return new Decimal?(num1);
      }
      set
      {
      }
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.statementCycleId>
    {
    }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.statementDate>
    {
    }

    public abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.descr>
    {
    }

    public abstract class numberOfDocuments : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.numberOfDocuments>
    {
    }

    public abstract class dontPrintCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.dontPrintCount>
    {
    }

    public abstract class printedCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.printedCount>
    {
    }

    public abstract class dontEmailCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.dontEmailCount>
    {
    }

    public abstract class emailedCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.emailedCount>
    {
    }

    public abstract class noActionCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.noActionCount>
    {
    }

    public abstract class toPrintCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.toPrintCount>
    {
    }

    public abstract class toEmailCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.toEmailCount>
    {
    }

    public abstract class printCompletion : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.printCompletion>
    {
    }

    public abstract class emailCompletion : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARStatementHistory.HistoryResult.emailCompletion>
    {
    }
  }

  public class Customer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBBool]
    public virtual bool? PrintCuryStatements { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARStatementHistory.Customer.bAccountID>
    {
    }

    public abstract class printCuryStatements : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARStatementHistory.Customer.printCuryStatements>
    {
    }
  }
}
