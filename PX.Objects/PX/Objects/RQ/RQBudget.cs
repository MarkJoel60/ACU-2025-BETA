// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXProjection(typeof (Select2<RQRequestLineBudget, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequestLineBudget.orderNbr>>>>))]
[PXCacheName("Request Budget Line")]
[Serializable]
public class RQBudget : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;
  protected long? _CuryInfoID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected Decimal? _DocRequestAmt;
  protected Decimal? _CuryDocRequestAmt;
  protected Decimal? _RequestAmt;
  protected Decimal? _CuryRequestAmt;
  protected Decimal? _AprovedAmt;
  protected Decimal? _CuryAprovedAmt;
  protected Decimal? _UnaprovedAmt;
  protected Decimal? _CuryUnaprovedAmt;
  protected string _OrderNbr;
  protected DateTime? _OrderDate;
  protected string _FinPeriodID;
  protected Decimal? _BudgetAmt;
  protected Decimal? _UsageAmt;

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (RQRequest.curyID))]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong(BqlField = typeof (RQRequest.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [Account]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [SubAccount(typeof (RQBudget.expenseAcctID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal]
  [PXUIField]
  public virtual Decimal? DocRequestAmt
  {
    get => this._DocRequestAmt;
    set => this._DocRequestAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal]
  public virtual Decimal? CuryDocRequestAmt
  {
    get => this._CuryDocRequestAmt;
    set => this._CuryDocRequestAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(BqlField = typeof (RQRequestLineBudget.estExtCost))]
  [PXUIField]
  public virtual Decimal? RequestAmt
  {
    get => this._RequestAmt;
    set => this._RequestAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(BqlField = typeof (RQRequestLineBudget.curyEstExtCost))]
  public virtual Decimal? CuryRequestAmt
  {
    get => this._CuryRequestAmt;
    set => this._CuryRequestAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolTrue>>, RQRequestLineBudget.estExtCost>, decimal0>), typeof (Decimal))]
  [PXUIField]
  public virtual Decimal? AprovedAmt
  {
    get => this._AprovedAmt;
    set => this._AprovedAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolTrue>>, RQRequestLineBudget.curyEstExtCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryAprovedAmt
  {
    get => this._CuryAprovedAmt;
    set => this._CuryAprovedAmt = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolFalse>>, RQRequestLineBudget.estExtCost>, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnaprovedAmt
  {
    get => this._UnaprovedAmt;
    set => this._UnaprovedAmt = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<RQRequest.approved, Equal<boolFalse>>, RQRequestLineBudget.curyEstExtCost>, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnaprovedAmt
  {
    get => this._CuryUnaprovedAmt;
    set => this._CuryUnaprovedAmt = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (RQRequest.orderNbr))]
  [PXDefault]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBDate(BqlField = typeof (RQRequest.orderDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBString(BqlField = typeof (RQRequest.finPeriodID))]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXBaseCury]
  [PXUIField]
  public virtual Decimal? BudgetAmt
  {
    get => this._BudgetAmt;
    set => this._BudgetAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXBaseCury]
  [PXUIField]
  public virtual Decimal? UsageAmt
  {
    get => this._UsageAmt;
    set => this._UsageAmt = value;
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBudget.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQBudget.curyInfoID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBudget.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBudget.expenseSubID>
  {
  }

  public abstract class docRequestAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.docRequestAmt>
  {
  }

  public abstract class curyDocRequestAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBudget.curyDocRequestAmt>
  {
  }

  public abstract class requestAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.requestAmt>
  {
  }

  public abstract class curyRequestAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.curyRequestAmt>
  {
  }

  public abstract class aprovedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.aprovedAmt>
  {
  }

  public abstract class curyAprovedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.curyAprovedAmt>
  {
  }

  public abstract class unaprovedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.unaprovedAmt>
  {
  }

  public abstract class curyUnaprovedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBudget.curyUnaprovedAmt>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBudget.orderNbr>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RQBudget.orderDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBudget.finPeriodID>
  {
  }

  public abstract class budgetAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.budgetAmt>
  {
  }

  public abstract class usageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBudget.usageAmt>
  {
  }
}
