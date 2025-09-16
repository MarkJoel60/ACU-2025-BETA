// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM.TemporaryHelpers;
using System;

#nullable disable
namespace PX.Objects.CS;

public class TermsAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  protected Type _DocDate;
  protected Type _DueDate;
  protected Type _DiscDate;
  protected Type _CuryDocBal;
  protected Type _CuryDiscBal;
  protected Type _CuryTaxTotal;
  protected Type _CuryTaxDiscountTotal;
  protected Type _BranchID;
  private Decimal? _CuryDiscBalBefore1stBalanceUpdate;

  public TermsAttribute(
    Type DocDate,
    Type DueDate,
    Type DiscDate,
    Type CuryDocBal,
    Type CuryDiscBal)
  {
    this._DocDate = DocDate;
    this._DueDate = DueDate;
    this._DiscDate = DiscDate;
    this._CuryDiscBal = CuryDiscBal;
    this._CuryDocBal = CuryDocBal;
  }

  public TermsAttribute(
    Type DocDate,
    Type DueDate,
    Type DiscDate,
    Type CuryDocBal,
    Type CuryDiscBal,
    Type CuryTaxTotal)
  {
    this._DocDate = DocDate;
    this._DueDate = DueDate;
    this._DiscDate = DiscDate;
    this._CuryDiscBal = CuryDiscBal;
    this._CuryDocBal = CuryDocBal;
    this._CuryTaxTotal = CuryTaxTotal;
  }

  public TermsAttribute(
    Type DocDate,
    Type DueDate,
    Type DiscDate,
    Type CuryDocBal,
    Type CuryDiscBal,
    Type CuryTaxTotal,
    Type BranchID)
  {
    this._DocDate = DocDate;
    this._DueDate = DueDate;
    this._DiscDate = DiscDate;
    this._CuryDiscBal = CuryDiscBal;
    this._CuryDocBal = CuryDocBal;
    this._CuryTaxTotal = CuryTaxTotal;
    this._BranchID = BranchID;
  }

  public TermsAttribute(
    Type DocDate,
    Type DueDate,
    Type DiscDate,
    Type CuryDocBal,
    Type CuryDiscBal,
    Type CuryTaxTotal,
    Type CuryTaxDiscountTotal,
    Type BranchID)
  {
    this._DocDate = DocDate;
    this._DueDate = DueDate;
    this._DiscDate = DiscDate;
    this._CuryDiscBal = CuryDiscBal;
    this._CuryDocBal = CuryDocBal;
    this._CuryTaxTotal = CuryTaxTotal;
    this._CuryTaxDiscountTotal = CuryTaxDiscountTotal;
    this._BranchID = BranchID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this._DocDate != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._DocDate);
      string name = this._DocDate.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) termsAttribute, __vmethodptr(termsAttribute, CalcTerms));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    if (this._CuryDocBal != (Type) null)
    {
      if (sender.Graph.IsContractBasedAPI)
      {
        // ISSUE: method pointer
        sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(this._CuryDiscBal), this._CuryDiscBal.Name, new PXFieldUpdated((object) this, __methodptr(SetCuryDiscBalBefore1stBalanceUpdate)));
      }
      this.SubscribeCalcDisc(sender);
    }
    if (!(this._CuryDiscBal != (Type) null))
      return;
    if (sender.Graph.IsContractBasedAPI)
    {
      // ISSUE: method pointer
      sender.Graph.RowPersisting.AddHandler(BqlCommand.GetItemType(this._CuryDiscBal), new PXRowPersisting((object) this, __methodptr(\u003CCacheAttached\u003Eb__13_0)));
    }
    else
    {
      PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
      Type itemType = BqlCommand.GetItemType(this._CuryDiscBal);
      string name = this._CuryDiscBal.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) termsAttribute, __vmethodptr(termsAttribute, VerifyDiscount));
      fieldVerifying.AddHandler(itemType, name, pxFieldVerifying);
    }
  }

  private void SetCuryDiscBalBefore1stBalanceUpdate(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (sender.GetStatus(e.Row) == 2)
    {
      Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryDiscBal.Name);
      Decimal? nullable2 = nullable1;
      Decimal num = 0M;
      Decimal? nullable3;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
      {
        nullable3 = nullable1;
      }
      else
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      this._CuryDiscBalBefore1stBalanceUpdate = nullable3;
    }
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.RemoveHandler(BqlCommand.GetItemType(this._CuryDocBal), this._CuryDocBal.Name, new PXFieldUpdated((object) this, __methodptr(SetCuryDiscBalBefore1stBalanceUpdate)));
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CalcTerms(sender, e);
    this.CalcDisc(sender, e);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._CuryDiscBal == (Type) null)
      return;
    Terms terms = TermsAttribute.SelectTerms(sender.Graph, (string) sender.GetValue(e.Row, this._FieldName));
    if (terms == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, e.Row, this._CuryDiscBal.Name, terms.InstallmentType == "S");
  }

  protected virtual void VerifyDiscount(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Graph.IsCopyPasteContext)
      return;
    if (sender.Graph.Accessinfo.CuryViewState && e.ExternalCall)
      e.NewValue = sender.GetValue(e.Row, this._CuryDiscBal.Name);
    if (!(this._CuryDocBal != (Type) null) || !(this._CuryDiscBal != (Type) null) || e.NewValue == null || sender.GetValue(e.Row, this._FieldName) == null)
      return;
    this.CalcDisc(sender, new PXFieldUpdatedEventArgs(e.Row, (object) null, true));
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryDocBal.Name);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryDiscBal.Name);
    Decimal newValue1 = (Decimal) e.NewValue;
    Decimal? nullable3 = nullable1;
    Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
    if (newValue1 > valueOrDefault1 & nullable3.HasValue)
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", new object[1]
      {
        (object) nullable1.ToString()
      });
    Decimal newValue2 = (Decimal) e.NewValue;
    nullable3 = nullable2;
    Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
    if (!(newValue2 > valueOrDefault2 & nullable3.HasValue))
      return;
    sender.RaiseExceptionHandling(this._CuryDiscBal.Name, e.Row, e.NewValue, (Exception) new PXSetPropertyException("Discount entered exceeds the discount specified for this terms.", (PXErrorLevel) 2));
  }

  protected virtual void CalcDisc(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(this._CuryDocBal != (Type) null) || !(this._CuryDiscBal != (Type) null) || sender.GetValue(e.Row, this._FieldName) == null || sender.GetValue(e.Row, this._CuryDocBal.Name) == null || sender.Graph.IsCopyPasteContext)
      return;
    string TermsID = (string) sender.GetValue(e.Row, this._FieldName);
    Decimal num1 = (Decimal) sender.GetValue(e.Row, this._CuryDocBal.Name);
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    Decimal num4 = 0M;
    string str = (string) null;
    if (this._BranchID != (Type) null)
    {
      int? branchID = sender.GetValue(e.Row, this._BranchID.Name) as int?;
      str = OrganizationMaint.GetCashDiscountBase(sender.Graph, branchID);
    }
    if (str == "TA")
    {
      if (this._CuryTaxTotal != (Type) null && sender.GetValue(e.Row, this._CuryTaxTotal.Name) != null)
        num3 = (Decimal) sender.GetValue(e.Row, this._CuryTaxTotal.Name);
      if (this._CuryTaxDiscountTotal != (Type) null && sender.GetValue(e.Row, this._CuryTaxDiscountTotal.Name) != null)
        num4 = (Decimal) sender.GetValue(e.Row, this._CuryTaxDiscountTotal.Name);
      num1 -= num3 + num4;
    }
    Terms terms = TermsAttribute.SelectTerms(sender.Graph, TermsID);
    if (terms != null && terms.InstallmentType == "S" && num1 > 0M)
    {
      Decimal? discPercent = terms.DiscPercent;
      Decimal num5 = 0M;
      if (!(discPercent.GetValueOrDefault() == num5 & discPercent.HasValue))
      {
        if ((e.Row is ARInvoice row1 ? row1.DocType : (string) null) != "PPI")
        {
          PXCache sender1 = sender;
          object row = e.Row;
          Decimal num6 = num1;
          discPercent = terms.DiscPercent;
          Decimal num7 = discPercent.Value;
          Decimal val = num6 * num7 / 100M;
          num2 = MultiCurrencyCalculator.RoundCury(sender1, row, val);
        }
        else
          sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) TermsID, (Exception) new PXSetPropertyException("A prepayment invoice cannot have terms with a cash discount.", (PXErrorLevel) 2));
      }
    }
    sender.SetValue(e.Row, this._CuryDiscBal.Name, (object) num2);
  }

  protected virtual void CalcTerms(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._DueDate == (Type) null || this._DiscDate == (Type) null)
      return;
    if (sender.GetValue(e.Row, this._FieldName) != null)
    {
      string TermsID = (string) sender.GetValue(e.Row, this._FieldName);
      DateTime? docDate = (DateTime?) sender.GetValue(e.Row, this._DocDate.Name);
      DateTime? dueDate;
      DateTime? discDate;
      TermsAttribute.CalcTermsDates(TermsAttribute.SelectTerms(sender.Graph, TermsID), docDate, out dueDate, out discDate);
      sender.SetValueExt(e.Row, this._DueDate.Name, (object) dueDate);
      sender.SetValueExt(e.Row, this._DiscDate.Name, (object) discDate);
    }
    else
    {
      sender.SetValueExt(e.Row, this._DueDate.Name, (object) null);
      sender.SetValueExt(e.Row, this._DiscDate.Name, (object) null);
    }
  }

  public static void CalcTermsDates(
    Terms terms,
    DateTime? docDate,
    out DateTime? dueDate,
    out DateTime? discDate)
  {
    dueDate = new DateTime?();
    discDate = new DateTime?();
    if (!docDate.HasValue || terms == null)
      return;
    DateTime dateTime1 = docDate.Value;
    string dueType = terms.DueType;
    short? nullable1;
    int? nullable2;
    int? nullable3;
    DateTime dateTime2;
    if (dueType != null && dueType.Length == 1)
    {
      switch (dueType[0])
      {
        case 'B':
          dueDate = new DateTime?(dateTime1.AddMonths(1).SetDateByEndOfDecade());
          break;
        case 'C':
          int months1 = 0;
          int day1 = dateTime1.Day;
          short? dayFrom00 = terms.DayFrom00;
          nullable2 = dayFrom00.HasValue ? new int?((int) dayFrom00.GetValueOrDefault()) : new int?();
          int valueOrDefault1 = nullable2.GetValueOrDefault();
          if (day1 >= valueOrDefault1 & nullable2.HasValue)
          {
            int day2 = dateTime1.Day;
            short? dayTo00 = terms.DayTo00;
            nullable2 = dayTo00.HasValue ? new int?((int) dayTo00.GetValueOrDefault()) : new int?();
            int valueOrDefault2 = nullable2.GetValueOrDefault();
            if (day2 <= valueOrDefault2 & nullable2.HasValue)
            {
              nullable1 = terms.DayDue00;
              nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              nullable1 = terms.DayTo00;
              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              if (nullable2.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
                months1 = 1;
              ref DateTime? local = ref dueDate;
              int year = dateTime1.Year;
              int month = dateTime1.Month;
              nullable1 = terms.DayDue00;
              int day3 = (int) nullable1.Value;
              DateTime? nullable4 = new DateTime?(new PXDateTime(year, month, day3).AddMonths(months1));
              local = nullable4;
            }
          }
          int day4 = dateTime1.Day;
          nullable1 = terms.DayFrom01;
          int? nullable5;
          if (!nullable1.HasValue)
          {
            nullable2 = new int?();
            nullable5 = nullable2;
          }
          else
            nullable5 = new int?((int) nullable1.GetValueOrDefault());
          nullable3 = nullable5;
          int valueOrDefault3 = nullable3.GetValueOrDefault();
          if (day4 >= valueOrDefault3 & nullable3.HasValue)
          {
            int day5 = dateTime1.Day;
            nullable1 = terms.DayTo01;
            int? nullable6;
            if (!nullable1.HasValue)
            {
              nullable2 = new int?();
              nullable6 = nullable2;
            }
            else
              nullable6 = new int?((int) nullable1.GetValueOrDefault());
            nullable3 = nullable6;
            int valueOrDefault4 = nullable3.GetValueOrDefault();
            if (day5 <= valueOrDefault4 & nullable3.HasValue)
            {
              nullable1 = terms.DayDue01;
              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              nullable1 = terms.DayTo01;
              nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              if (nullable3.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue)
                months1 = 1;
              ref DateTime? local = ref dueDate;
              int year = dateTime1.Year;
              int month = dateTime1.Month;
              nullable1 = terms.DayDue01;
              int day6 = (int) nullable1.Value;
              DateTime? nullable7 = new DateTime?(new PXDateTime(year, month, day6).AddMonths(months1));
              local = nullable7;
              break;
            }
            break;
          }
          break;
        case 'D':
          ref DateTime? local1 = ref dueDate;
          int year1 = dateTime1.Year;
          int month1 = dateTime1.Month;
          nullable1 = terms.DayDue00;
          int day7 = (int) nullable1.Value;
          DateTime? nullable8 = new DateTime?(new PXDateTime(year1, month1, day7).AddMonths(1));
          local1 = nullable8;
          break;
        case 'E':
          ref DateTime? local2 = ref dueDate;
          dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
          dateTime2 = dateTime2.AddMonths(1);
          DateTime? nullable9 = new DateTime?(dateTime2.AddDays(-1.0));
          local2 = nullable9;
          break;
        case 'F':
          dueDate = new DateTime?(dateTime1.AddDays((double) terms.DayDue00.Value).GetEndOfMonth());
          short? dayDue01 = terms.DayDue01;
          nullable2 = dayDue01.HasValue ? new int?((int) dayDue01.GetValueOrDefault()) : new int?();
          int num1 = 0;
          if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
          {
            ref DateTime? local3 = ref dueDate;
            DateTime date = dueDate.Value;
            nullable1 = terms.DayDue01;
            int dayNumber = (int) nullable1.Value;
            DateTime? nullable10 = new DateTime?(date.SetDayInMonthAfter(dayNumber));
            local3 = nullable10;
            break;
          }
          break;
        case 'M':
          ref DateTime? local4 = ref dueDate;
          dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
          dateTime2 = dateTime2.AddMonths(2);
          DateTime? nullable11 = new DateTime?(dateTime2.AddDays(-1.0));
          local4 = nullable11;
          break;
        case 'N':
          dueDate = new DateTime?(dateTime1.AddDays((double) terms.DayDue00.Value));
          break;
        case 'P':
          DateTime dateTime3 = dateTime1.AddMonths(1);
          DateTime dateTime4 = new DateTime(dateTime3.Year, dateTime3.Month, 1);
          ref DateTime? local5 = ref dueDate;
          ref DateTime local6 = ref dateTime4;
          nullable1 = terms.DayDue00;
          double num2 = (double) nullable1.Value;
          DateTime? nullable12 = new DateTime?(local6.AddDays(num2));
          local5 = nullable12;
          break;
        case 'T':
          int months2 = dateTime1.Day > (int) terms.DayDue00.Value ? 1 : 0;
          dueDate = new DateTime?(new PXDateTime(dateTime1.Year, dateTime1.Month, (int) terms.DayDue00.Value).AddMonths(months2));
          break;
      }
    }
    if (terms.InstallmentType == "M")
    {
      discDate = dueDate;
    }
    else
    {
      string discType = terms.DiscType;
      if (discType != null && discType.Length == 1)
      {
        switch (discType[0])
        {
          case 'D':
            ref DateTime? local7 = ref discDate;
            int year2 = dateTime1.Year;
            int month2 = dateTime1.Month;
            nullable1 = terms.DayDisc;
            int day8 = (int) nullable1.Value;
            DateTime? nullable13 = new DateTime?(new PXDateTime(year2, month2, day8).AddMonths(1));
            local7 = nullable13;
            break;
          case 'E':
            ref DateTime? local8 = ref discDate;
            dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
            dateTime2 = dateTime2.AddMonths(1);
            DateTime? nullable14 = new DateTime?(dateTime2.AddDays(-1.0));
            local8 = nullable14;
            break;
          case 'F':
            ref DateTime? local9 = ref dueDate;
            ref DateTime local10 = ref dateTime1;
            nullable1 = terms.DayDue00;
            double num3 = (double) nullable1.Value;
            DateTime? nullable15 = new DateTime?(local10.AddDays(num3).GetEndOfMonth());
            local9 = nullable15;
            nullable1 = terms.DayDue01;
            int? nullable16;
            if (!nullable1.HasValue)
            {
              nullable3 = new int?();
              nullable16 = nullable3;
            }
            else
              nullable16 = new int?((int) nullable1.GetValueOrDefault());
            nullable2 = nullable16;
            int num4 = 0;
            if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
            {
              ref DateTime? local11 = ref dueDate;
              DateTime date = dueDate.Value;
              nullable1 = terms.DayDue01;
              int dayNumber = (int) nullable1.Value;
              DateTime? nullable17 = new DateTime?(date.SetDayInMonthAfter(dayNumber));
              local11 = nullable17;
              break;
            }
            break;
          case 'M':
            ref DateTime? local12 = ref discDate;
            dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
            dateTime2 = dateTime2.AddMonths(2);
            DateTime? nullable18 = new DateTime?(dateTime2.AddDays(-1.0));
            local12 = nullable18;
            break;
          case 'N':
            ref DateTime? local13 = ref discDate;
            ref DateTime local14 = ref dateTime1;
            nullable1 = terms.DayDisc;
            double num5 = (double) nullable1.Value;
            DateTime? nullable19 = new DateTime?(local14.AddDays(num5));
            local13 = nullable19;
            break;
          case 'P':
            DateTime dateTime5 = dateTime1.AddMonths(1);
            DateTime dateTime6 = new DateTime(dateTime5.Year, dateTime5.Month, 1);
            ref DateTime? local15 = ref discDate;
            ref DateTime local16 = ref dateTime6;
            nullable1 = terms.DayDisc;
            double num6 = (double) nullable1.Value;
            DateTime? nullable20 = new DateTime?(local16.AddDays(num6));
            local15 = nullable20;
            break;
          case 'T':
            int months3;
            if (terms.DueType == "D")
            {
              int day9 = dateTime1.Day;
              nullable1 = terms.DayDue00;
              int num7 = (int) nullable1.Value;
              if (day9 <= num7)
              {
                int day10 = dateTime1.Day;
                nullable1 = terms.DayDisc;
                int num8 = (int) nullable1.Value;
                months3 = day10 >= num8 ? 1 : 0;
                goto label_44;
              }
            }
            if (terms.DueType == "M")
            {
              int day11 = dateTime1.Day;
              nullable1 = terms.DayDisc;
              int num9 = (int) nullable1.Value;
              months3 = day11 >= num9 ? 1 : 0;
            }
            else if (terms.DueType == "B" || terms.DueType == "D")
            {
              int day12 = dateTime1.Day;
              nullable1 = terms.DayDisc;
              int num10 = (int) nullable1.Value;
              months3 = day12 > num10 ? 1 : 0;
            }
            else
            {
              int day13 = dateTime1.Day;
              nullable1 = terms.DayDue00;
              int num11 = (int) nullable1.Value;
              months3 = day13 > num11 ? 1 : 0;
            }
label_44:
            ref DateTime? local17 = ref discDate;
            int year3 = dateTime1.Year;
            int month3 = dateTime1.Month;
            nullable1 = terms.DayDisc;
            int day14 = (int) nullable1.Value;
            DateTime? nullable21 = new DateTime?(new PXDateTime(year3, month3, day14).AddMonths(months3));
            local17 = nullable21;
            break;
        }
      }
    }
    DateTime? nullable22 = discDate;
    DateTime? nullable23 = dueDate;
    if ((nullable22.HasValue & nullable23.HasValue ? (nullable22.GetValueOrDefault() > nullable23.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    discDate = dueDate;
  }

  public static Terms SelectTerms(PXGraph graph, string TermsID)
  {
    if (TermsID == null)
      return (Terms) null;
    return PXResultset<Terms>.op_Implicit(PXSelectBase<Terms, PXSelect<Terms, Where<Terms.termsID, Equal<Required<Terms.termsID>>>>.Config>.Select(graph, new object[1]
    {
      (object) TermsID
    }));
  }

  public static PXResultset<TermsInstallments> SelectInstallments(
    PXGraph graph,
    Terms terms,
    DateTime firstdate)
  {
    PXResultset<TermsInstallments> pxResultset = new PXResultset<TermsInstallments>();
    switch (terms.InstallmentMthd)
    {
      case "A":
      case "E":
        int months = 0;
        while (true)
        {
          int num = months;
          short? installmentCntr = terms.InstallmentCntr;
          int? nullable1 = installmentCntr.HasValue ? new int?((int) installmentCntr.GetValueOrDefault()) : new int?();
          int valueOrDefault = nullable1.GetValueOrDefault();
          if (num < valueOrDefault & nullable1.HasValue)
          {
            TermsInstallments termsInstallments1 = new TermsInstallments();
            termsInstallments1.InstallmentNbr = new short?((short) (months + 1));
            TermsInstallments termsInstallments2 = termsInstallments1;
            installmentCntr = terms.InstallmentCntr;
            Decimal? nullable2 = new Decimal?(100M / (Decimal) installmentCntr.Value);
            termsInstallments2.InstPercent = nullable2;
            switch (terms.InstallmentFreq)
            {
              case "W":
                termsInstallments1.InstDays = new short?((short) (7 * months));
                break;
              case "B":
                if (months % 2 == 0)
                {
                  TimeSpan timeSpan = firstdate.AddMonths(months / 2) - firstdate;
                  termsInstallments1.InstDays = new short?((short) timeSpan.Days);
                  break;
                }
                TimeSpan timeSpan1 = firstdate.AddMonths((months - 1) / 2) - firstdate;
                termsInstallments1.InstDays = new short?((short) (timeSpan1.Days + 14));
                break;
              case "M":
                TimeSpan timeSpan2 = firstdate.AddMonths(months) - firstdate;
                termsInstallments1.InstDays = new short?((short) timeSpan2.Days);
                break;
            }
            pxResultset.Add(new PXResult<TermsInstallments>(termsInstallments1));
            ++months;
          }
          else
            break;
        }
        return pxResultset;
      case "S":
        short num1 = 0;
        foreach (PXResult<TermsInstallments> pxResult in PXSelectBase<TermsInstallments, PXSelectReadonly<TermsInstallments, Where<TermsInstallments.termsID, Equal<Required<TermsInstallments.termsID>>>, OrderBy<Asc<TermsInstallments.instDays>>>.Config>.Select(graph, new object[1]
        {
          (object) terms.TermsID
        }))
        {
          TermsInstallments termsInstallments = PXResult<TermsInstallments>.op_Implicit(pxResult);
          termsInstallments.InstallmentNbr = new short?(++num1);
          pxResultset.Add(new PXResult<TermsInstallments>(termsInstallments));
        }
        return pxResultset;
      default:
        return (PXResultset<TermsInstallments>) null;
    }
  }

  protected virtual void UnsubscribeCalcDisc(PXCache sender)
  {
    if (this._CuryDocBal != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._CuryDocBal);
      string name = this._CuryDocBal.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) termsAttribute, __vmethodptr(termsAttribute, CalcDisc));
      fieldUpdated.RemoveHandler(itemType, name, pxFieldUpdated);
    }
    if (this._BranchID != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._BranchID);
      string name = this._BranchID.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) termsAttribute, __vmethodptr(termsAttribute, CalcDisc));
      fieldUpdated.RemoveHandler(itemType, name, pxFieldUpdated);
    }
    if (!(this._CuryTaxTotal != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type itemType1 = BqlCommand.GetItemType(this._CuryTaxTotal);
    string name1 = this._CuryTaxTotal.Name;
    TermsAttribute termsAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) termsAttribute1, __vmethodptr(termsAttribute1, CalcDisc));
    fieldUpdated1.RemoveHandler(itemType1, name1, pxFieldUpdated1);
  }

  protected virtual void SubscribeCalcDisc(PXCache sender)
  {
    if (this._CuryDocBal != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._CuryDocBal);
      string name = this._CuryDocBal.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) termsAttribute, __vmethodptr(termsAttribute, CalcDisc));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    if (this._BranchID != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._BranchID);
      string name = this._BranchID.Name;
      TermsAttribute termsAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) termsAttribute, __vmethodptr(termsAttribute, CalcDisc));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    if (!(this._CuryTaxTotal != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type itemType1 = BqlCommand.GetItemType(this._CuryTaxTotal);
    string name1 = this._CuryTaxTotal.Name;
    TermsAttribute termsAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) termsAttribute1, __vmethodptr(termsAttribute1, CalcDisc));
    fieldUpdated1.AddHandler(itemType1, name1, pxFieldUpdated1);
  }

  public class UnsubscribeCalcDiscScope : IDisposable
  {
    private readonly PXCache _Cache;
    private readonly TermsAttribute _Attribute;

    public UnsubscribeCalcDiscScope(PXCache sender)
    {
      this._Cache = sender;
      foreach (PXEventSubscriberAttribute subscriberAttribute in this._Cache.GetAttributesReadonly((string) null))
      {
        if (subscriberAttribute is TermsAttribute)
        {
          this._Attribute = (TermsAttribute) subscriberAttribute;
          this._Attribute.UnsubscribeCalcDisc(this._Cache);
          break;
        }
      }
    }

    public void Dispose() => this._Attribute?.SubscribeCalcDisc(this._Cache);
  }
}
