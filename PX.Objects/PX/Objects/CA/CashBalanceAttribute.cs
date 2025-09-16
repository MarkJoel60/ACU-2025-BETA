// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashBalanceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// This attribute allows to display current CashAccount balance from CADailySummary<br />
/// Read-only. Should be placed on Decimal? field<br />
/// <example>
/// [CashBalance(typeof(PayBillsFilter.payAccountID))]
/// </example>
/// </summary>
public class CashBalanceAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _CashAccount;

  /// <summary>Ctor</summary>
  /// <param name="cashAccountType">Must be IBqlField. Refers to the cashAccountID field in the row</param>
  public CashBalanceAttribute(Type cashAccountType) => this._CashAccount = cashAccountType.Name;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    CASetup caSetup = PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    Decimal? nullable1 = new Decimal?(0M);
    object obj = sender.GetValue(e.Row, this._CashAccount);
    CADailySummary caDailySummary = PXResultset<CADailySummary>.op_Implicit(PXSelectBase<CADailySummary, PXSelectGroupBy<CADailySummary, Where<CADailySummary.cashAccountID, Equal<Required<CADailySummary.cashAccountID>>>, Aggregate<Sum<CADailySummary.amtReleasedClearedCr, Sum<CADailySummary.amtReleasedClearedDr, Sum<CADailySummary.amtReleasedUnclearedCr, Sum<CADailySummary.amtReleasedUnclearedDr, Sum<CADailySummary.amtUnreleasedClearedCr, Sum<CADailySummary.amtUnreleasedClearedDr, Sum<CADailySummary.amtUnreleasedUnclearedCr, Sum<CADailySummary.amtUnreleasedUnclearedDr>>>>>>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      obj
    }));
    if (caDailySummary != null && caDailySummary.CashAccountID.HasValue)
    {
      Decimal? releasedClearedDr = caDailySummary.AmtReleasedClearedDr;
      Decimal? nullable2 = caDailySummary.AmtReleasedClearedCr;
      nullable1 = releasedClearedDr.HasValue & nullable2.HasValue ? new Decimal?(releasedClearedDr.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      if (caSetup.CalcBalDebitClearedUnreleased.Value)
      {
        nullable2 = nullable1;
        Decimal? unreleasedClearedDr = caDailySummary.AmtUnreleasedClearedDr;
        nullable1 = nullable2.HasValue & unreleasedClearedDr.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + unreleasedClearedDr.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable3;
      if (caSetup.CalcBalCreditClearedUnreleased.Value)
      {
        nullable3 = nullable1;
        nullable2 = caDailySummary.AmtUnreleasedClearedCr;
        nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      if (caSetup.CalcBalDebitUnclearedReleased.Value)
      {
        nullable2 = nullable1;
        nullable3 = caDailySummary.AmtReleasedUnclearedDr;
        nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      if (caSetup.CalcBalCreditUnclearedReleased.Value)
      {
        nullable3 = nullable1;
        nullable2 = caDailySummary.AmtReleasedUnclearedCr;
        nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      if (caSetup.CalcBalDebitUnclearedUnreleased.Value)
      {
        nullable2 = nullable1;
        nullable3 = caDailySummary.AmtUnreleasedUnclearedDr;
        nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      if (caSetup.CalcBalCreditUnclearedUnreleased.Value)
      {
        nullable3 = nullable1;
        nullable2 = caDailySummary.AmtUnreleasedUnclearedCr;
        nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
    }
    e.ReturnValue = (object) nullable1;
    ((CancelEventArgs) e).Cancel = true;
  }
}
