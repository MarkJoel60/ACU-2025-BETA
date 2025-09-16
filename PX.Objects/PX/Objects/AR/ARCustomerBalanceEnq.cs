// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCustomerBalanceEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA.Light;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARCustomerBalanceEnq : PXGraph<
#nullable disable
ARCustomerBalanceEnq>
{
  public PXFilter<ARCustomerBalanceEnq.ARHistoryFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<ARCustomerBalanceEnq.ARHistoryResult> History;
  [PXVirtualDAC]
  public PXFilter<ARCustomerBalanceEnq.ARHistorySummary> Summary;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXCancel<ARCustomerBalanceEnq.ARHistoryFilter> Cancel;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> viewDetails;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> previousPeriod;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> nextPeriod;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> aRBalanceByCustomerReport;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> customerHistoryReport;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> aRAgedPastDueReport;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> aRAgedOutstandingReport;
  public PXAction<ARCustomerBalanceEnq.ARHistoryFilter> reports;
  protected readonly Dictionary<System.Type, System.Type> mapFin = new Dictionary<System.Type, System.Type>()
  {
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.acctCD),
      typeof (PX.Objects.CA.Light.Customer.acctCD)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.acctName),
      typeof (PX.Objects.CA.Light.Customer.acctName)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.customerID),
      typeof (PX.Objects.CA.Light.Customer.bAccountID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.noteID),
      typeof (PX.Objects.CA.Light.Customer.noteID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID),
      typeof (ARHistoryByPeriod.curyID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finPeriodID),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPeriodID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyBegBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinBegBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyEndBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curySales),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdSales)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyPayments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdPayments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDiscount),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDiscounts)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyCrAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdCrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDrAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDeposits),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDepositsBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyFinCharges),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdFinCharges)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyRetainageWithheld),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdRetainageWithheld)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyRetainageReleased),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdRetainageReleased)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyBegRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinBegRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyEndRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.begBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finBegBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.endBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finYtdBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.sales),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdSales)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.payments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdPayments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.discount),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDiscounts)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.crAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdCrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.drAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.deposits),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.rGOL),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRGOL)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.depositsBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finYtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finCharges),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdFinCharges)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.cOGS),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdCOGS)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finPtdRevaluated),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRevalued)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.retainageWithheld),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRetainageWithheld)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.retainageReleased),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRetainageReleased)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.begRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finBegRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.endRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finYtdRetainedBalance)
    }
  };
  protected readonly Dictionary<System.Type, System.Type> mapTran = new Dictionary<System.Type, System.Type>()
  {
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.acctCD),
      typeof (PX.Objects.CA.Light.Customer.acctCD)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.acctName),
      typeof (PX.Objects.CA.Light.Customer.acctName)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.customerID),
      typeof (PX.Objects.CA.Light.Customer.bAccountID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.noteID),
      typeof (PX.Objects.CA.Light.Customer.noteID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID),
      typeof (ARHistoryByPeriod.curyID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finPeriodID),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPeriodID)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyBegBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranBegBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyEndBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curySales),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdSales)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyPayments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdPayments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDiscount),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDiscounts)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyCrAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdCrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDrAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDeposits),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyDepositsBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyFinCharges),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdFinCharges)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyRetainageWithheld),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdRetainageWithheld)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyRetainageReleased),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdRetainageReleased)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyBegRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranBegRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.curyEndRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.begBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranBegBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.endBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.sales),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdSales)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.payments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdPayments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.discount),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDiscounts)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.rGOL),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRGOL)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.crAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdCrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.drAdjustments),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDrAdjustments)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.deposits),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.depositsBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdDeposits)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finCharges),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdFinCharges)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.cOGS),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdCOGS)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.finPtdRevaluated),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRevalued)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.retainageWithheld),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRetainageWithheld)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.retainageReleased),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRetainageReleased)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.begRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranBegRetainedBalance)
    },
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult.endRetainedBalance),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdRetainedBalance)
    }
  };

  public ARCustomerBalanceEnq()
  {
    PX.Objects.AR.ARSetup current1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PX.Objects.GL.Company current2 = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current;
    ((PXSelectBase) this.History).Cache.AllowDelete = false;
    ((PXSelectBase) this.History).Cache.AllowInsert = false;
    ((PXSelectBase) this.History).Cache.AllowUpdate = false;
    ((PXAction) this.reports).MenuAutoOpen = true;
    ((PXAction) this.reports).AddMenuAction((PXAction) this.aRBalanceByCustomerReport);
    ((PXAction) this.reports).AddMenuAction((PXAction) this.customerHistoryReport);
    ((PXAction) this.reports).AddMenuAction((PXAction) this.aRAgedPastDueReport);
    ((PXAction) this.reports).AddMenuAction((PXAction) this.aRAgedOutstandingReport);
  }

  public virtual bool IsDirty => false;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.Period, true);
    current.Period = prevPeriod?.FinPeriodID;
    ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current.ClearSummary();
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.Period, true);
    current.Period = nextPeriod?.FinPeriodID;
    ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current.ClearSummary();
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current != null && ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current != null)
    {
      ARCustomerBalanceEnq.ARHistoryResult current1 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current;
      ARCustomerBalanceEnq.ARHistoryFilter current2 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
      ARDocumentEnq instance = PXGraph.CreateInstance<ARDocumentEnq>();
      ARDocumentEnq.ARDocumentFilter current3 = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Current;
      ARCustomerBalanceEnq.Copy(current3, current2);
      current3.CustomerID = current1.CustomerID;
      current3.BalanceSummary = new Decimal?();
      ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Update(current3);
      PXResultset<ARDocumentEnq.ARDocumentFilter>.op_Implicit(((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Select(Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Customer Details");
    }
    return (IEnumerable) ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Reports(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  public virtual IEnumerable ARBalanceByCustomerReport(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current1 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    ARCustomerBalanceEnq.ARHistoryResult current2 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current;
    if (current1 != null && current2 != null)
    {
      PX.Objects.CA.Light.Customer customer = PXResultset<PX.Objects.CA.Light.Customer>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.Customer, PXSelect<PX.Objects.CA.Light.Customer, Where<PX.Objects.CA.Light.Customer.bAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryResult.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(current1.Period))
        dictionary["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.Period);
      dictionary["CustomerID"] = customer.AcctCD;
      dictionary["UseMasterCalendar"] = current1.UseMasterCalendar.GetValueOrDefault() ? true.ToString() : false.ToString();
      if (current1.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current1.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR632500", (PXBaseRedirectException.WindowMode) 3, "AR Balance by Customer", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable CustomerHistoryReport(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current1 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    ARCustomerBalanceEnq.ARHistoryResult current2 = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current;
    if (current1 != null && current2 != null)
    {
      PX.Objects.CA.Light.Customer customer = PXResultset<PX.Objects.CA.Light.Customer>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.Customer, PXSelect<PX.Objects.CA.Light.Customer, Where<PX.Objects.CA.Light.Customer.bAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryResult.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(current1.Period))
      {
        dictionary["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.Period);
        dictionary["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.Period);
      }
      dictionary["CustomerID"] = customer.AcctCD;
      if (current1.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current1.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR652000", (PXBaseRedirectException.WindowMode) 3, "Customer History", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ARAgedPastDueReport(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    if (((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current != null)
    {
      PX.Objects.CA.Light.Customer customer = PXResultset<PX.Objects.CA.Light.Customer>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.Customer, PXSelect<PX.Objects.CA.Light.Customer, Where<PX.Objects.CA.Light.Customer.bAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryResult.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR631000", (PXBaseRedirectException.WindowMode) 3, "AR Aging", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ARAgedOutstandingReport(PXAdapter adapter)
  {
    ARCustomerBalanceEnq.ARHistoryFilter current = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current;
    if (((PXSelectBase<ARCustomerBalanceEnq.ARHistoryResult>) this.History).Current != null)
    {
      PX.Objects.CA.Light.Customer customer = PXResultset<PX.Objects.CA.Light.Customer>.op_Implicit(PXSelectBase<PX.Objects.CA.Light.Customer, PXSelect<PX.Objects.CA.Light.Customer, Where<PX.Objects.CA.Light.Customer.bAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryResult.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR631500", (PXBaseRedirectException.WindowMode) 3, "AR Coming Due", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  protected virtual IEnumerable history()
  {
    return this.RetrieveHistoryForPeriod(((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current);
  }

  protected virtual IEnumerable summary()
  {
    bool? calculated = ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current.Calculated;
    bool flag = false;
    if (calculated.GetValueOrDefault() == flag & calculated.HasValue)
    {
      ARCustomerBalanceEnq.ARHistorySummary instance = ((PXSelectBase) this.Summary).Cache.CreateInstance() as ARCustomerBalanceEnq.ARHistorySummary;
      instance.ClearSummary();
      ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Insert(instance);
      this.RetrieveHistoryForPeriod(((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current, instance);
      ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Update(instance);
      ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current.Calculated = new bool?(true);
    }
    yield return (object) ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current;
  }

  protected virtual IEnumerable RetrieveHistoryForPeriod(
    ARCustomerBalanceEnq.ARHistoryFilter header,
    ARCustomerBalanceEnq.ARHistorySummary summary = null)
  {
    bool flag1 = !string.IsNullOrEmpty(header.CuryID);
    bool valueOrDefault = header.SplitByCurrency.GetValueOrDefault();
    bool flag2 = !header.UseMasterCalendar.GetValueOrDefault();
    bool flag3 = header.IncludeChildAccounts.GetValueOrDefault() && summary == null;
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && !header.OrgBAccountID.HasValue)
      return (IEnumerable) new object[0];
    System.Type type1 = typeof (PX.Objects.CA.Light.Customer.bAccountID);
    List<System.Type> typeList1 = new List<System.Type>()
    {
      typeof (Select5<,,,,>),
      typeof (PX.Objects.CA.Light.Customer)
    };
    if (flag3)
    {
      typeList1.AddRange((IEnumerable<System.Type>) new System.Type[3]
      {
        typeof (LeftJoin<,,>),
        typeof (ARCustomerBalanceEnq.ChildBAccount),
        typeof (On<ARCustomerBalanceEnq.ChildBAccount.consolidatingBAccountID, Equal<PX.Objects.CA.Light.Customer.bAccountID>>)
      });
      type1 = typeof (ARCustomerBalanceEnq.ChildBAccount.bAccountID);
    }
    System.Type type2 = (System.Type) null;
    if (header.ShowWithBalanceOnly.GetValueOrDefault() && summary == null)
      type2 = !flag2 ? typeof (Having<BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdBalance>, IBqlDecimal>.IsNotEqual<Zero>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRevalued>, IBqlDecimal>.IsNotEqual<Zero>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdDeposits>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdRetainedBalance>, IBqlDecimal>.IsNotEqual<Zero>>>) : typeof (Having<BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.finYtdBalance>, IBqlDecimal>.IsNotEqual<Zero>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRevalued>, IBqlDecimal>.IsNotEqual<Zero>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdDeposits>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Sum<ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdRetainedBalance>, IBqlDecimal>.IsNotEqual<Zero>>>);
    typeList1.AddRange((IEnumerable<System.Type>) new System.Type[9]
    {
      typeof (LeftJoin<,,>),
      typeof (ARHistoryByPeriod),
      typeof (On<,>),
      typeof (ARHistoryByPeriod.customerID),
      typeof (Equal<>),
      type1,
      typeof (LeftJoin<ARCustomerBalanceEnq.CuryARHistoryTran, On<ARHistoryByPeriod.accountID, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.accountID>, And<ARHistoryByPeriod.branchID, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.branchID>, And<ARHistoryByPeriod.customerID, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.customerID>, And<ARHistoryByPeriod.subID, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.subID>, And<ARHistoryByPeriod.curyID, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.curyID>, And<ARHistoryByPeriod.lastActivityPeriod, Equal<ARCustomerBalanceEnq.CuryARHistoryTran.finPeriodID>>>>>>>>),
      typeof (Where<ARHistoryByPeriod.finPeriodID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.period>>, And<Where2<Match<PX.Objects.CA.Light.Customer, Current<AccessInfo.userName>>, And<PX.Objects.CA.Light.BAccount.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>>),
      type2 == (System.Type) null ? typeof (PX.Data.Aggregate<>) : typeof (PX.Data.Aggregate<,>)
    });
    typeList1.AddRange(BqlHelper.GetDecimalFieldsAggregate<ARCustomerBalanceEnq.CuryARHistoryTran>((PXGraph) this));
    System.Type type3;
    System.Type type4;
    if (summary != null)
    {
      type3 = typeof (GroupBy<ARHistoryByPeriod.curyID>);
      type4 = typeof (OrderBy<Asc<ARHistoryByPeriod.curyID>>);
    }
    else if (!valueOrDefault)
    {
      type3 = typeof (GroupBy<PX.Objects.CA.Light.Customer.acctCD>);
      type4 = typeof (OrderBy<Asc<PX.Objects.CA.Light.Customer.acctCD>>);
    }
    else
    {
      type3 = typeof (GroupBy<PX.Objects.CA.Light.Customer.acctCD, GroupBy<ARHistoryByPeriod.curyID>>);
      type4 = typeof (OrderBy<Asc<PX.Objects.CA.Light.Customer.acctCD, Asc<ARHistoryByPeriod.curyID>>>);
    }
    typeList1.Add(type3);
    if (type2 != (System.Type) null)
      typeList1.Add(type2);
    typeList1.Add(type4);
    System.Type type5 = BqlCommand.Compose(typeList1.ToArray());
    if (!SubCDUtils.IsSubCDEmpty(header.SubCD))
      type5 = BqlCommand.AppendJoin(type5, BqlCommand.Compose(new System.Type[1]
      {
        typeof (LeftJoin<PX.Objects.GL.Sub, On<ARHistoryByPeriod.subID, Equal<PX.Objects.GL.Sub.subID>>>)
      }));
    if (PXAccess.FeatureInstalled<FeaturesSet.rowLevelSecurity>())
      type5 = BqlCommand.AppendJoin(type5, BqlCommand.Compose(new System.Type[1]
      {
        typeof (InnerJoin<CustomerRLS, On<PX.Objects.CA.Light.Customer.bAccountID, Equal<CustomerRLS.bAccountID>, And<Match<CustomerRLS, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>)
      }));
    PXView view = new PXView((PXGraph) this, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      type5
    }));
    if (flag3)
      view.WhereAnd<Where<PX.Objects.CA.Light.Customer.consolidatingBAccountID, Equal<PX.Objects.CA.Light.Customer.consolidatingBAccountID>>>();
    if (flag1)
      view.WhereAnd<Where<ARHistoryByPeriod.curyID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.curyID>>>>();
    if (header.OrgBAccountID.HasValue)
      view.WhereAnd<Where<ARHistoryByPeriod.branchID, Inside<Current<ARCustomerBalanceEnq.ARHistoryFilter.orgBAccountID>>>>();
    if (header.ARAcctID.HasValue)
      view.WhereAnd<Where<ARHistoryByPeriod.accountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.aRAcctID>>>>();
    if (header.ARSubID.HasValue)
      view.WhereAnd<Where<ARHistoryByPeriod.subID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.aRSubID>>>>();
    this.AppendCommonWhereFilters(view, header);
    int startRow = PXView.StartRow;
    int num = 0;
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, flag2 ? this.mapFin : this.mapTran, new System.Type[1]
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult)
    });
    pxResultMapper.ExtFilters.Add(typeof (ARCustomerBalanceEnq.CuryARHistoryTran));
    List<System.Type> typeList2 = new List<System.Type>()
    {
      typeof (ARCustomerBalanceEnq.ARHistoryResult),
      typeof (ARHistoryByPeriod),
      typeof (PX.Objects.CA.Light.Customer),
      typeof (ARCustomerBalanceEnq.CuryARHistoryTran),
      typeof (CustomerRLS.bAccountID),
      typeof (CustomerRLS.groupMask)
    };
    using (new PXFieldScope(view, (IEnumerable<System.Type>) typeList2, true))
    {
      List<object> objectList = summary == null ? view.Select((object[]) null, (object[]) null, PXView.Searches, pxResultMapper.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper.Filters), ref startRow, PXView.MaximumRows, ref num) : view.SelectMulti(Array.Empty<object>());
      PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult(summary == null);
      foreach (PXResult<PX.Objects.CA.Light.Customer> source in objectList)
      {
        ARCustomerBalanceEnq.ARHistoryResult result = pxResultMapper.CreateResult((PXResult) source) as ARCustomerBalanceEnq.ARHistoryResult;
        result.RecalculateBalance();
        if (!flag1 && !valueOrDefault)
          result.CopyValueToCuryValue(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID);
        ((List<object>) delegateResult).Add((object) result);
        if (summary != null)
          this.Aggregate(summary, result);
      }
      PXView.StartRow = 0;
      return (IEnumerable) delegateResult;
    }
  }

  protected virtual void AppendCommonWhereFilters(
    PXView view,
    ARCustomerBalanceEnq.ARHistoryFilter filter)
  {
    if (!SubCDUtils.IsSubCDEmpty(filter.SubCD))
      view.WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<ARCustomerBalanceEnq.ARHistoryFilter.subCDWildcard>>>>();
    if (filter.CustomerClassID != null)
      view.WhereAnd<Where<PX.Objects.CA.Light.Customer.customerClassID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.customerClassID>>>>();
    if (!filter.CustomerID.HasValue)
      return;
    view.WhereAnd<Where<PX.Objects.CA.Light.Customer.bAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.customerID>>, Or<PX.Objects.CA.Light.Customer.consolidatingBAccountID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.customerID>>, And<Current<ARCustomerBalanceEnq.ARHistoryFilter.includeChildAccounts>, Equal<True>>>>>();
  }

  public virtual void ARHistoryFilter_CuryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<ARCustomerBalanceEnq.ARHistoryFilter.splitByCurrency>(e.Row);
  }

  public virtual void ARHistoryFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void ARHistoryFilter_ARAcctID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    int? nullable = new int?();
    row.ARAcctID = nullable;
  }

  public virtual void ARHistoryFilter_ARSubID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    int? nullable = new int?();
    row.ARSubID = nullable;
  }

  public virtual void ARHistoryFilter_CuryID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    row.CuryID = (string) null;
  }

  public virtual void ARHistoryFilter_Period_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    row.Period = (string) null;
  }

  public virtual void ARHistoryFilter_CustomerClassID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    row.CustomerClassID = (string) null;
  }

  protected virtual void ARHistoryFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) this.Summary).Current.ClearSummary();
  }

  protected virtual void ARHistoryFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARCustomerBalanceEnq.ARHistoryFilter row))
      return;
    PX.Objects.GL.Company current = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryFilter.showWithBalanceOnly>(sender, (object) row, true);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryFilter.curyID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryFilter.includeChildAccounts>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    bool flag2 = !string.IsNullOrEmpty(row.CuryID);
    bool flag3 = !string.IsNullOrEmpty(row.CuryID) && current.BaseCuryID == row.CuryID;
    bool valueOrDefault = row.SplitByCurrency.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryFilter.splitByCurrency>(sender, (object) row, flag1 && !flag2);
    PXUIFieldAttribute.SetEnabled<ARCustomerBalanceEnq.ARHistoryFilter.splitByCurrency>(sender, (object) row, flag1 && !flag2);
    PXCache cache = ((PXSelectBase) this.History).Cache;
    bool flag4 = !flag1 | flag3 || !flag2 && !valueOrDefault;
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyID>(cache, (object) null, flag1 && flag2 | valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyPayments>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curySales>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyDiscount>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyCrAdjustments>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyDrAdjustments>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyDeposits>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyDepositsBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyBegBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyEndBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyBegRetainedBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyEndRetainedBalance>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyRetainageWithheld>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyRetainageReleased>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.rGOL>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyFinCharges>(cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.balance>(cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.curyBalance>(cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.finPeriodID>(cache, (object) null);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.begBalance>(cache, (object) null);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.endBalance>(cache, (object) null);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.begRetainedBalance>(((PXSelectBase) this.History).Cache, (object) null);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistoryResult.endRetainedBalance>(((PXSelectBase) this.History).Cache, (object) null);
    bool flag5 = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetRequired<ARCustomerBalanceEnq.ARHistoryFilter.orgBAccountID>(sender, flag5);
    bool flag6 = flag5 ? row.Period != null && row.OrgBAccountID.HasValue : row.Period != null;
    ((PXAction) this.aRBalanceByCustomerReport).SetEnabled(flag6);
    ((PXAction) this.customerHistoryReport).SetEnabled(flag6);
    ((PXAction) this.aRAgedPastDueReport).SetEnabled(flag6);
    ((PXAction) this.aRAgedOutstandingReport).SetEnabled(flag6);
  }

  protected virtual void ARHistorySummary_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARCustomerBalanceEnq.ARHistorySummary row = (ARCustomerBalanceEnq.ARHistorySummary) e.Row;
    if (row == null)
      return;
    bool flag = !string.IsNullOrEmpty(((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current.CuryID) && ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID != ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) this.Filter).Current.CuryID;
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistorySummary.curyBalanceSummary>(sender, (object) row, flag);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistorySummary.curyDepositsSummary>(sender, (object) row, flag);
    PXUIFieldAttribute.SetVisible<ARCustomerBalanceEnq.ARHistorySummary.curyBalanceRetainedSummary>(sender, (object) row, flag);
  }

  public virtual string GetLastActivityPeriod(int? aCustomerID)
  {
    return ((CuryARHistory) ((PXSelectBase) new PXSelect<CuryARHistory, Where<CuryARHistory.customerID, Equal<Required<CuryARHistory.customerID>>>, OrderBy<Desc<CuryARHistory.finPeriodID>>>((PXGraph) this)).View.SelectSingle(new object[1]
    {
      (object) aCustomerID
    }))?.FinPeriodID;
  }

  public virtual string GetLastActivityPeriod(int? aCustomerID, bool IncludeChildAccounts)
  {
    if (!IncludeChildAccounts)
      return this.GetLastActivityPeriod(aCustomerID);
    return PXResultset<CuryARHistory>.op_Implicit(PXSelectBase<CuryARHistory, PXSelectJoin<CuryARHistory, InnerJoin<ARCustomerBalanceEnq.ChildBAccount, On<ARCustomerBalanceEnq.ChildBAccount.bAccountID, Equal<CuryARHistory.customerID>>>, Where<ARCustomerBalanceEnq.ChildBAccount.consolidatingBAccountID, Equal<Required<CuryARHistory.customerID>>, Or<ARCustomerBalanceEnq.ChildBAccount.bAccountID, Equal<Required<CuryARHistory.customerID>>>>, OrderBy<Desc<CuryARHistory.finPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) aCustomerID,
      (object) aCustomerID
    }))?.FinPeriodID;
  }

  protected virtual void Aggregate(
    ARCustomerBalanceEnq.ARHistorySummary aDest,
    ARCustomerBalanceEnq.ARHistoryResult aSrc)
  {
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary1 = aDest;
    Decimal? nullable = arHistorySummary1.CuryBalanceSummary;
    Decimal valueOrDefault1 = aSrc.CuryEndBalance.GetValueOrDefault();
    arHistorySummary1.CuryBalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary2 = aDest;
    nullable = arHistorySummary2.BalanceSummary;
    Decimal valueOrDefault2 = aSrc.EndBalance.GetValueOrDefault();
    arHistorySummary2.BalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary3 = aDest;
    nullable = arHistorySummary3.RevaluedSummary;
    Decimal valueOrDefault3 = aSrc.FinPtdRevaluated.GetValueOrDefault();
    arHistorySummary3.RevaluedSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary4 = aDest;
    nullable = arHistorySummary4.CuryDepositsSummary;
    Decimal valueOrDefault4 = aSrc.CuryDepositsBalance.GetValueOrDefault();
    arHistorySummary4.CuryDepositsSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary5 = aDest;
    nullable = arHistorySummary5.DepositsSummary;
    Decimal valueOrDefault5 = aSrc.DepositsBalance.GetValueOrDefault();
    arHistorySummary5.DepositsSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary6 = aDest;
    nullable = arHistorySummary6.BalanceRetainedSummary;
    Decimal valueOrDefault6 = aSrc.EndRetainedBalance.GetValueOrDefault();
    arHistorySummary6.BalanceRetainedSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault6) : new Decimal?();
    ARCustomerBalanceEnq.ARHistorySummary arHistorySummary7 = aDest;
    nullable = arHistorySummary7.CuryBalanceRetainedSummary;
    Decimal valueOrDefault7 = aSrc.CuryEndRetainedBalance.GetValueOrDefault();
    arHistorySummary7.CuryBalanceRetainedSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault7) : new Decimal?();
  }

  public static void Copy(
    ARDocumentEnq.ARDocumentFilter filter,
    ARCustomerBalanceEnq.ARHistoryFilter histFilter)
  {
    filter.OrganizationID = histFilter.OrganizationID;
    filter.BranchID = histFilter.BranchID;
    filter.OrgBAccountID = histFilter.OrgBAccountID;
    filter.Period = histFilter.Period;
    filter.SubCD = histFilter.SubCD;
    filter.ARAcctID = histFilter.ARAcctID;
    filter.ARSubID = histFilter.ARSubID;
    filter.CuryID = histFilter.CuryID;
    filter.UseMasterCalendar = histFilter.UseMasterCalendar;
    filter.IncludeChildAccounts = histFilter.IncludeChildAccounts;
  }

  public static void Copy(
    ARCustomerBalanceEnq.ARHistoryFilter histFilter,
    ARDocumentEnq.ARDocumentFilter filter)
  {
    histFilter.OrganizationID = filter.OrganizationID;
    histFilter.BranchID = filter.BranchID;
    histFilter.OrgBAccountID = filter.OrgBAccountID;
    histFilter.CustomerID = filter.CustomerID;
    histFilter.Period = filter.Period;
    histFilter.SubCD = filter.SubCD;
    histFilter.ARAcctID = filter.ARAcctID;
    histFilter.ARSubID = filter.ARSubID;
    histFilter.CuryID = filter.CuryID;
    histFilter.UseMasterCalendar = filter.UseMasterCalendar;
    histFilter.IncludeChildAccounts = filter.IncludeChildAccounts;
  }

  [Serializable]
  public class ARHistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Decimal? _CuryBalanceSummary;
    protected Decimal? _BalanceSummary;
    protected Decimal? _CuryRevaluedSummary;
    protected Decimal? _RevaluedSummary;
    protected Decimal? _DepositsSummary;

    [Organization(false, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), false, null, null)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<ARHistory, On<PX.Objects.GL.Account.accountID, Equal<ARHistory.accountID>>>, Where<Match<Current<AccessInfo.userName>>>, PX.Data.Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "AR Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? ARAcctID { get; set; }

    [SubAccount(DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Visible = false)]
    public virtual int? ARSubID { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField]
    [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
    public virtual string SubCD { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency ID")]
    public virtual string CuryID { get; set; }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Customers with Balance Only")]
    public virtual bool? ShowWithBalanceOnly { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Use Master Calendar")]
    [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleCalendarsSupport>))]
    public bool? UseMasterCalendar { get; set; }

    [PXDefault(typeof (Coalesce<Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Current<ARCustomerBalanceEnq.ARHistoryFilter.organizationID>>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.endDate>>>>, Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Zero>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.endDate>>>>>))]
    [AnyPeriodFilterable(null, null, typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.useMasterCalendar), null, false, null, null)]
    [PXUIField]
    public virtual string Period { get; set; }

    [PXDBString(30, IsUnicode = true)]
    public virtual string SubCDWildcard => SubCDUtils.CreateSubCDWildcard(this.SubCD, "SUBACCOUNT");

    [PXDBInt]
    public virtual int? CustomerID { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Split by Currency")]
    public virtual bool? SplitByCurrency { get; set; }

    [PXCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance", Enabled = false)]
    public virtual Decimal? CuryBalanceSummary
    {
      get => this._CuryBalanceSummary;
      set => this._CuryBalanceSummary = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance", Enabled = false)]
    public virtual Decimal? BalanceSummary
    {
      get => this._BalanceSummary;
      set => this._BalanceSummary = value;
    }

    [PXCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
    public virtual Decimal? CuryRevaluedSummary
    {
      get => this._CuryRevaluedSummary;
      set => this._CuryRevaluedSummary = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
    public virtual Decimal? RevaluedSummary
    {
      get => this._RevaluedSummary;
      set => this._RevaluedSummary = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Prepayments", Enabled = false)]
    public virtual Decimal? DepositsSummary { get; set; }

    [PXDBBool]
    [PXDefault(typeof (Search<FeaturesSet.parentChildAccount>))]
    [PXUIField(DisplayName = "Consolidate by Parent")]
    public virtual bool? IncludeChildAccounts { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class aRAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.aRAcctID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.aRSubID>
    {
    }

    public abstract class subCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.subCD>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.curyID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.customerClassID>
    {
    }

    public abstract class showWithBalanceOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.showWithBalanceOnly>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.useMasterCalendar>
    {
    }

    public abstract class period : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.period>
    {
    }

    public abstract class subCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.subCDWildcard>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.customerID>
    {
    }

    public abstract class splitByCurrency : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.splitByCurrency>
    {
    }

    public abstract class curyBalanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.curyBalanceSummary>
    {
    }

    public abstract class balanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.balanceSummary>
    {
    }

    public abstract class curyRevaluedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.curyRevaluedSummary>
    {
    }

    public abstract class revaluedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.revaluedSummary>
    {
    }

    public abstract class depositsSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.depositsSummary>
    {
    }

    public abstract class includeChildAccounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryFilter.includeChildAccounts>
    {
    }
  }

  public class ARHistorySummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [CurySymbol(null, null, typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID), null, null, null, "Total Balance", true, false)]
    [PXCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryBalanceSummary { get; set; }

    [CurySymbol(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance", Enabled = false)]
    public virtual Decimal? BalanceSummary { get; set; }

    public virtual bool? IncludeChildAccounts { get; set; }

    [PXCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
    public virtual Decimal? CuryRevaluedSummary { get; set; }

    [CurySymbol(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Unrealized Gain/Loss", Enabled = false)]
    public virtual Decimal? RevaluedSummary { get; set; }

    [CurySymbol(null, null, typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID), null, null, null, "Total Prepayments", true, false)]
    [PXCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Prepayments (Currency)", Enabled = false)]
    public virtual Decimal? CuryDepositsSummary { get; set; }

    [CurySymbol(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Prepayments", Enabled = false)]
    public virtual Decimal? DepositsSummary { get; set; }

    [CurySymbol(null, null, typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID), null, null, null, "Total Retained Balance", true, false)]
    [PXBaseCury(typeof (ARCustomerBalanceEnq.ARHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Retained Balance (Currency)", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? CuryBalanceRetainedSummary { get; set; }

    [CurySymbol(typeof (ARCustomerBalanceEnq.ARHistoryFilter.organizationID), typeof (ARCustomerBalanceEnq.ARHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Retained Balance", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? BalanceRetainedSummary { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the <see cref="M:PX.Objects.AR.ARCustomerBalanceEnq.history" /> delegate calculated the summary.
    /// </summary>
    [PXBool]
    [PXDefault(false)]
    public virtual bool? Calculated { get; set; }

    public virtual void ClearSummary()
    {
      this.BalanceSummary = new Decimal?(0M);
      this.RevaluedSummary = new Decimal?(0M);
      this.DepositsSummary = new Decimal?(0M);
      this.CuryBalanceSummary = new Decimal?(0M);
      this.CuryRevaluedSummary = new Decimal?(0M);
      this.CuryDepositsSummary = new Decimal?(0M);
      this.BalanceRetainedSummary = new Decimal?(0M);
      this.CuryBalanceRetainedSummary = new Decimal?(0M);
      this.Calculated = new bool?(false);
    }

    public abstract class curyBalanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.curyBalanceSummary>
    {
    }

    public abstract class balanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.balanceSummary>
    {
    }

    public abstract class includeChildAccounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.includeChildAccounts>
    {
    }

    public abstract class curyRevaluedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.curyRevaluedSummary>
    {
    }

    public abstract class revaluedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.revaluedSummary>
    {
    }

    public abstract class curyDepositsSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.curyDepositsSummary>
    {
    }

    public abstract class depositsSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.depositsSummary>
    {
    }

    public abstract class curyBalanceRetainedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.curyBalanceRetainedSummary>
    {
    }

    public abstract class balanceRetainedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.balanceRetainedSummary>
    {
    }

    public abstract class calculated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistorySummary.calculated>
    {
    }
  }

  [Serializable]
  public class ARHistoryResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    [PXDefault]
    public virtual int? CustomerID { get; set; }

    [PXDimensionSelector("BIZACCT", typeof (PX.Objects.CA.Light.Customer.acctCD), typeof (ARCustomerBalanceEnq.ARHistoryResult.acctCD), new System.Type[] {typeof (PX.Objects.CA.Light.Customer.acctCD), typeof (PX.Objects.CA.Light.Customer.acctName)})]
    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField]
    public virtual string AcctCD { get; set; }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string AcctName { get; set; }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    [PXDBString(5, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public virtual string CuryID { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Beginning Balance", Visible = false)]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Beginning Balance", Visible = false)]
    public virtual Decimal? BegBalance { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField]
    public virtual Decimal? CuryEndBalance { get; set; }

    [PXBaseCury]
    [PXUIField]
    public virtual Decimal? EndBalance { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Balance", Visible = false)]
    public virtual Decimal? CuryBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Balance", Visible = false)]
    public virtual Decimal? Balance { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Sales")]
    public virtual Decimal? CurySales { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Sales")]
    public virtual Decimal? Sales { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Payments")]
    public virtual Decimal? CuryPayments { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Payments")]
    public virtual Decimal? Payments { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Cash Discount Taken")]
    public virtual Decimal? CuryDiscount { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Cash Discount Taken")]
    public virtual Decimal? Discount { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Realized Gain/Loss")]
    public virtual Decimal? RGOL { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Credit Memos")]
    public virtual Decimal? CuryCrAdjustments { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Credit Memos")]
    public virtual Decimal? CrAdjustments { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Debit Memos")]
    public virtual Decimal? CuryDrAdjustments { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Debit Memos")]
    public virtual Decimal? DrAdjustments { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "PTD COGS")]
    public virtual Decimal? COGS { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unrealized Gain/Loss")]
    public virtual Decimal? FinPtdRevaluated { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency PTD Overdue Charges", Visible = true)]
    public virtual Decimal? CuryFinCharges { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "PTD Overdue Charges", Visible = true)]
    public virtual Decimal? FinCharges { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Prepayments")]
    public virtual Decimal? CuryDeposits { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Prepayments")]
    public virtual Decimal? Deposits { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Prepayments Balance")]
    public virtual Decimal? CuryDepositsBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Prepayments Balance")]
    public virtual Decimal? DepositsBalance { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Retainage Withheld", FieldClass = "Retainage")]
    public virtual Decimal? CuryRetainageWithheld { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Retainage Withheld", FieldClass = "Retainage")]
    public virtual Decimal? RetainageWithheld { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Retainage Released", FieldClass = "Retainage")]
    public virtual Decimal? CuryRetainageReleased { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Retainage Released", FieldClass = "Retainage")]
    public virtual Decimal? RetainageReleased { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Beginning Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? CuryBegRetainedBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Beginning Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? BegRetainedBalance { get; set; }

    [PXDBCury(typeof (ARCustomerBalanceEnq.ARHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Ending Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? CuryEndRetainedBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Ending Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? EndRetainedBalance { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Converted to Base Currency", Visible = false, Enabled = false)]
    public virtual bool? Converted { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public virtual void RecalculateEndBalance()
    {
      this.RecalculateBalance();
      Decimal? nullable1 = this.BegBalance;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.Balance;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      this.EndBalance = new Decimal?(valueOrDefault1 + valueOrDefault2);
      Decimal? nullable2 = this.CuryBegBalance;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryBalance;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      this.CuryEndBalance = new Decimal?(valueOrDefault3 + valueOrDefault4);
    }

    public virtual void RecalculateBalance()
    {
      Decimal? nullable1 = this.Sales;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.Payments;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 - valueOrDefault2;
      nullable1 = this.Discount;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal num2 = num1 - valueOrDefault3;
      nullable1 = this.RGOL;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault4;
      nullable1 = this.CrAdjustments;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      Decimal num4 = num3 - valueOrDefault5;
      nullable1 = this.FinCharges;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal num5 = num4 + valueOrDefault6;
      nullable1 = this.DrAdjustments;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      this.Balance = new Decimal?(num5 + valueOrDefault7);
      Decimal? nullable2 = this.CurySales;
      Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPayments;
      Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
      Decimal num6 = valueOrDefault8 - valueOrDefault9;
      nullable2 = this.CuryDiscount;
      Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
      Decimal num7 = num6 - valueOrDefault10;
      nullable2 = this.CuryCrAdjustments;
      Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
      Decimal num8 = num7 - valueOrDefault11;
      nullable2 = this.CuryFinCharges;
      Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
      Decimal num9 = num8 + valueOrDefault12;
      nullable2 = this.CuryDrAdjustments;
      Decimal valueOrDefault13 = nullable2.GetValueOrDefault();
      this.CuryBalance = new Decimal?(num9 + valueOrDefault13);
    }

    public virtual void CopyValueToCuryValue(string aBaseCuryID)
    {
      this.CuryBegBalance = new Decimal?(this.BegBalance.GetValueOrDefault());
      this.CurySales = new Decimal?(this.Sales.GetValueOrDefault());
      this.CuryPayments = new Decimal?(this.Payments.GetValueOrDefault());
      this.CuryDiscount = new Decimal?(this.Discount.GetValueOrDefault());
      this.CuryFinCharges = new Decimal?(this.FinCharges.GetValueOrDefault());
      this.CuryCrAdjustments = new Decimal?(this.CrAdjustments.GetValueOrDefault());
      this.CuryDrAdjustments = new Decimal?(this.DrAdjustments.GetValueOrDefault());
      this.CuryDeposits = new Decimal?(this.Deposits.GetValueOrDefault());
      this.CuryDepositsBalance = new Decimal?(this.DepositsBalance.GetValueOrDefault());
      this.CuryEndBalance = new Decimal?(this.EndBalance.GetValueOrDefault());
      this.CuryRetainageWithheld = new Decimal?(this.RetainageWithheld.GetValueOrDefault());
      this.CuryRetainageReleased = new Decimal?(this.RetainageReleased.GetValueOrDefault());
      this.CuryBegRetainedBalance = new Decimal?(this.BegRetainedBalance.GetValueOrDefault());
      this.CuryEndRetainedBalance = new Decimal?(this.EndRetainedBalance.GetValueOrDefault());
      this.CuryID = aBaseCuryID;
      this.Converted = new bool?(true);
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.customerID>
    {
    }

    public abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.acctCD>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.acctName>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.finPeriodID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.begBalance>
    {
    }

    public abstract class curyEndBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyEndBalance>
    {
    }

    public abstract class endBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.endBalance>
    {
    }

    public abstract class curyBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyBalance>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.balance>
    {
    }

    public abstract class curySales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curySales>
    {
    }

    public abstract class sales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.sales>
    {
    }

    public abstract class curyPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyPayments>
    {
    }

    public abstract class payments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.payments>
    {
    }

    public abstract class curyDiscount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyDiscount>
    {
    }

    public abstract class discount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.discount>
    {
    }

    public abstract class rGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.rGOL>
    {
    }

    public abstract class curyCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyCrAdjustments>
    {
    }

    public abstract class crAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.crAdjustments>
    {
    }

    public abstract class curyDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyDrAdjustments>
    {
    }

    public abstract class drAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.drAdjustments>
    {
    }

    public abstract class cOGS : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.cOGS>
    {
    }

    public abstract class finPtdRevaluated : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.finPtdRevaluated>
    {
    }

    public abstract class curyFinCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyFinCharges>
    {
    }

    public abstract class finCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.finCharges>
    {
    }

    public abstract class curyDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyDeposits>
    {
    }

    public abstract class deposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.deposits>
    {
    }

    public abstract class curyDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyDepositsBalance>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.depositsBalance>
    {
    }

    public abstract class curyRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyRetainageWithheld>
    {
    }

    public abstract class retainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.retainageWithheld>
    {
    }

    public abstract class curyRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyRetainageReleased>
    {
    }

    public abstract class retainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.retainageReleased>
    {
    }

    public abstract class curyBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyBegRetainedBalance>
    {
    }

    public abstract class begRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.begRetainedBalance>
    {
    }

    public abstract class curyEndRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.curyEndRetainedBalance>
    {
    }

    public abstract class endRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.endRetainedBalance>
    {
    }

    public abstract class converted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.converted>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARHistoryResult.noteID>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select<PX.Objects.CR.BAccount>))]
  [Serializable]
  public class ChildBAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.CR.BAccount))]
    public virtual int? BAccountID { get; set; }

    [PXDBInt(BqlTable = typeof (PX.Objects.CR.BAccount))]
    public virtual int? ConsolidatingBAccountID { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ChildBAccount.bAccountID>
    {
    }

    public abstract class consolidatingBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ChildBAccount.consolidatingBAccountID>
    {
    }
  }

  [PXProjection(typeof (Select4<CuryARHistory, PX.Data.Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, Max<CuryARHistory.finPeriodID>>>>>>>>))]
  [PXCacheName("AR Latest History")]
  [Serializable]
  public class ARLatestHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BranchID;
    protected int? _CustomerID;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _CuryID;
    protected string _LastActivityPeriod;

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.branchID))]
    [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.customerID))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.accountID))]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.subID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CuryARHistory.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryARHistory.finPeriodID))]
    public virtual string LastActivityPeriod
    {
      get => this._LastActivityPeriod;
      set => this._LastActivityPeriod = value;
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.customerID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.subID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.curyID>
    {
    }

    public abstract class lastActivityPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.ARLatestHistory.lastActivityPeriod>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select<CuryARHistory>))]
  [Serializable]
  public class CuryARHistoryTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
    public virtual int? BranchID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
    public virtual int? AccountID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
    public virtual int? SubID { get; set; }

    [PXDBString(BqlTable = typeof (CuryARHistory))]
    public virtual string CuryID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
    public virtual int? CustomerID { get; set; }

    [PXDBString(BqlTable = typeof (CuryARHistory))]
    public virtual string FinPeriodID { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finBegBalance>, CuryARHistory.finYtdBalance>), typeof (Decimal))]
    public virtual Decimal? FinBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdSales>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdSales { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdDiscounts>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDiscounts { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdCOGS>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdCOGS { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRGOL>, Zero>>), typeof (Decimal))]
    public virtual Decimal? FinPtdRGOL { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdFinCharges>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdFinCharges { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? FinYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<CuryARHistory.finYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? FinYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranBegBalance>, CuryARHistory.tranYtdBalance>), typeof (Decimal))]
    public virtual Decimal? TranBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdSales>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdSales { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdDiscounts>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDiscounts { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdRGOL>, Zero>>), typeof (Decimal))]
    public virtual Decimal? TranPtdRGOL { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdCOGS>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdCOGS { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdFinCharges>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdFinCharges { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? TranYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<CuryARHistory.tranYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? TranYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinBegBalance>, CuryARHistory.curyFinYtdBalance>), typeof (Decimal))]
    public virtual Decimal? CuryFinBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdSales>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdSales { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdDiscounts>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDiscounts { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdFinCharges>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdFinCharges { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryFinYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<CuryARHistory.curyFinYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? CuryFinYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranBegBalance>, CuryARHistory.curyTranYtdBalance>), typeof (Decimal))]
    public virtual Decimal? CuryTranBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdSales>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdSales { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdDiscounts>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDiscounts { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdFinCharges>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdFinCharges { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryTranYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Minus<CuryARHistory.curyTranYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? CuryTranYtdDeposits { get; set; }

    [PXDBBool(BqlTable = typeof (CuryARHistory))]
    public virtual bool? DetDeleted { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRevalued>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRevalued { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryFinYtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? FinYtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.curyFinYtdRetainageWithheld, Add<CuryARHistory.curyFinYtdRetainageReleased, Sub<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? CuryFinBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.finYtdRetainageWithheld, Add<CuryARHistory.finYtdRetainageReleased, Sub<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? FinBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.curyTranYtdRetainageWithheld, Add<CuryARHistory.curyTranYtdRetainageReleased, Sub<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? CuryTranBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.tranYtdRetainageWithheld, Add<CuryARHistory.tranYtdRetainageReleased, Sub<Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? TranBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.curyFinYtdRetainageWithheld, CuryARHistory.curyFinYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? CuryFinYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.finYtdRetainageWithheld, CuryARHistory.finYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? FinYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.curyTranYtdRetainageWithheld, CuryARHistory.curyTranYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? CuryTranYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryARHistory.tranYtdRetainageWithheld, CuryARHistory.tranYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? TranYtdRetainedBalance { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryTranYtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? TranYtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyFinPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.finPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.curyTranPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryARHistory.finPeriodID, Equal<CurrentValue<ARCustomerBalanceEnq.ARHistoryFilter.period>>>, CuryARHistory.tranPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryFinYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? FinYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? CuryTranYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
    public virtual Decimal? TranYtdRetainageReleased { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.branchID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.subID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.customerID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPeriodID>
    {
    }

    public abstract class finBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finBegBalance>
    {
    }

    public abstract class finPtdSales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdSales>
    {
    }

    public abstract class finPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdPayments>
    {
    }

    public abstract class finPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDrAdjustments>
    {
    }

    public abstract class finPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdCrAdjustments>
    {
    }

    public abstract class finPtdDiscounts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDiscounts>
    {
    }

    public abstract class finPtdCOGS : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdCOGS>
    {
    }

    public abstract class finPtdRGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRGOL>
    {
    }

    public abstract class finPtdFinCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdFinCharges>
    {
    }

    public abstract class finYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finYtdBalance>
    {
    }

    public abstract class finPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdDeposits>
    {
    }

    public abstract class finYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finYtdDeposits>
    {
    }

    public abstract class tranBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranBegBalance>
    {
    }

    public abstract class tranPtdSales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdSales>
    {
    }

    public abstract class tranPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdPayments>
    {
    }

    public abstract class tranPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDrAdjustments>
    {
    }

    public abstract class tranPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdCrAdjustments>
    {
    }

    public abstract class tranPtdDiscounts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDiscounts>
    {
    }

    public abstract class tranPtdRGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRGOL>
    {
    }

    public abstract class tranPtdCOGS : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdCOGS>
    {
    }

    public abstract class tranPtdFinCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdFinCharges>
    {
    }

    public abstract class tranYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdBalance>
    {
    }

    public abstract class tranPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdDeposits>
    {
    }

    public abstract class tranYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdDeposits>
    {
    }

    public abstract class curyFinBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinBegBalance>
    {
    }

    public abstract class curyFinPtdSales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdSales>
    {
    }

    public abstract class curyFinPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdPayments>
    {
    }

    public abstract class curyFinPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDrAdjustments>
    {
    }

    public abstract class curyFinPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdCrAdjustments>
    {
    }

    public abstract class curyFinPtdDiscounts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDiscounts>
    {
    }

    public abstract class curyFinPtdFinCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdFinCharges>
    {
    }

    public abstract class curyFinYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdBalance>
    {
    }

    public abstract class curyFinPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdDeposits>
    {
    }

    public abstract class curyFinYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdDeposits>
    {
    }

    public abstract class curyTranBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranBegBalance>
    {
    }

    public abstract class curyTranPtdSales : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdSales>
    {
    }

    public abstract class curyTranPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdPayments>
    {
    }

    public abstract class curyTranPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDrAdjustments>
    {
    }

    public abstract class curyTranPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdCrAdjustments>
    {
    }

    public abstract class curyTranPtdDiscounts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDiscounts>
    {
    }

    public abstract class curyTranPtdFinCharges : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdFinCharges>
    {
    }

    public abstract class curyTranYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdBalance>
    {
    }

    public abstract class curyTranPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdDeposits>
    {
    }

    public abstract class curyTranYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdDeposits>
    {
    }

    public abstract class detDeleted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.detDeleted>
    {
    }

    public abstract class finPtdRevalued : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRevalued>
    {
    }

    public abstract class curyFinPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdRetainageWithheld>
    {
    }

    public abstract class finPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRetainageWithheld>
    {
    }

    public abstract class curyTranPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdRetainageWithheld>
    {
    }

    public abstract class tranPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRetainageWithheld>
    {
    }

    public abstract class curyFinYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdRetainageWithheld>
    {
    }

    public abstract class finYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finYtdRetainageWithheld>
    {
    }

    public abstract class curyFinBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinBegRetainedBalance>
    {
    }

    public abstract class finBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finBegRetainedBalance>
    {
    }

    public abstract class curyTranBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranBegRetainedBalance>
    {
    }

    public abstract class tranBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranBegRetainedBalance>
    {
    }

    public abstract class curyFinYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdRetainedBalance>
    {
    }

    public abstract class finYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finYtdRetainedBalance>
    {
    }

    public abstract class curyTranYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdRetainedBalance>
    {
    }

    public abstract class tranYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdRetainedBalance>
    {
    }

    public abstract class curyTranYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdRetainageWithheld>
    {
    }

    public abstract class tranYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdRetainageWithheld>
    {
    }

    public abstract class curyFinPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinPtdRetainageReleased>
    {
    }

    public abstract class finPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finPtdRetainageReleased>
    {
    }

    public abstract class curyTranPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranPtdRetainageReleased>
    {
    }

    public abstract class tranPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranPtdRetainageReleased>
    {
    }

    public abstract class curyFinYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyFinYtdRetainageReleased>
    {
    }

    public abstract class finYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.finYtdRetainageReleased>
    {
    }

    public abstract class curyTranYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.curyTranYtdRetainageReleased>
    {
    }

    public abstract class tranYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARCustomerBalanceEnq.CuryARHistoryTran.tranYtdRetainageReleased>
    {
    }
  }

  private sealed class decimalZero : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Constant<
    #nullable disable
    ARCustomerBalanceEnq.decimalZero>
  {
    public decimalZero()
      : base(0M)
    {
    }
  }
}
