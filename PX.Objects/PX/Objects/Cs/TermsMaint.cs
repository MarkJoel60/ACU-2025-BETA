// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class TermsMaint : PXGraph<TermsMaint, Terms>
{
  public PXSelect<Terms> TermsDef;
  public PXSelect<TermsInstallments, Where<TermsInstallments.termsID, Equal<Current<Terms.termsID>>>, OrderBy<Asc<TermsInstallments.instDays>>> Installments;
  private int? justInserted;
  private bool isMassDelete;

  protected virtual void Terms_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    Terms term = (Terms) e.Row;
    bool flag1 = term.InstallmentType == "M";
    bool flag2 = term.InstallmentMthd == "S";
    PXUIFieldAttribute.SetEnabled<Terms.installmentCntr>(cache, (object) term, flag1 && !flag2);
    PXUIFieldAttribute.SetEnabled<Terms.installmentFreq>(cache, (object) term, flag1 && !flag2);
    PXUIFieldAttribute.SetEnabled<Terms.installmentMthd>(cache, (object) term, flag1);
    bool flag3 = flag1 & flag2;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Installments).Cache, (string) null, flag3);
    ((PXSelectBase) this.Installments).Cache.AllowInsert = flag3;
    ((PXSelectBase) this.Installments).Cache.AllowUpdate = flag3;
    bool flag4 = term.DueType == "C";
    bool flag5 = term.DueType == "M";
    bool flag6 = term.DueType == "E";
    bool flag7 = term.DueType == "F";
    bool flag8 = term.DueType == "B";
    PXUIFieldAttribute.SetEnabled<Terms.dayDue00>(cache, (object) term, flag4 | flag7 || !(flag6 | flag5 | flag8));
    PXUIFieldAttribute.SetEnabled<Terms.dayDue01>(cache, (object) term, flag4 | flag7);
    PXUIFieldAttribute.SetEnabled<Terms.dayTo00>(cache, (object) term, flag4);
    bool flag9 = term.DiscType == "E";
    bool flag10 = term.DiscType == "M";
    bool flag11 = term.DueType == "N" || term.DueType == "P";
    bool flag12 = flag1 | flag6 | flag11;
    PXUIFieldAttribute.SetEnabled<Terms.discPercent>(cache, (object) term, !flag1 && !flag6);
    PXUIFieldAttribute.SetEnabled<Terms.discType>(cache, (object) term, !flag12);
    PXUIFieldAttribute.SetEnabled<Terms.dayDisc>(cache, (object) term, !flag1 && !(flag6 | flag9 | flag10));
    if (flag12)
      cache.RaiseExceptionHandling<Terms.discType>((object) term, (object) term.DiscType, (Exception) null);
    PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) term).For<Terms.prepaymentPct>((Action<PXUIFieldAttribute>) (a => a.Visible = EnumerableExtensions.IsIn<string>(term.VisibleTo, "CU", "AL"))).SameFor<Terms.prepaymentRequired>();
    PXUIFieldAttribute.SetEnabled<Terms.prepaymentPct>(cache, (object) term, term.PrepaymentRequired.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<Terms.prepaymentRequired>(cache, (object) term, term.InstallmentType == "S");
  }

  protected virtual void Terms_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    Terms row1 = (Terms) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    short? nullable1;
    if (row1.DueType == "D" || row1.DueType == "T")
    {
      nullable1 = row1.DayDue00;
      if (nullable1.Value > (short) 0)
      {
        nullable1 = row1.DayDue00;
        if (nullable1.Value <= (short) 31 /*0x1F*/)
          goto label_5;
      }
      PXCache pxCache = cache;
      object row2 = e.Row;
      nullable1 = row1.DayDue00;
      // ISSUE: variable of a boxed type
      __Boxed<short> local = (ValueType) nullable1.Value;
      PXSetPropertyException propertyException = new PXSetPropertyException("Value must be in the range of [1-31].");
      pxCache.RaiseExceptionHandling<Terms.dayDue00>(row2, (object) local, (Exception) propertyException);
      return;
    }
label_5:
    int? nullable2;
    if (row1.DueType == "C")
    {
      nullable1 = row1.DayDue00;
      if (nullable1.Value > (short) 0)
      {
        nullable1 = row1.DayDue00;
        if (nullable1.Value <= (short) 31 /*0x1F*/)
        {
          nullable1 = row1.DayFrom00;
          int num1 = (int) nullable1.Value;
          nullable1 = row1.DayTo00;
          int num2 = (int) nullable1.Value;
          if (num1 > num2)
          {
            PXCache pxCache = cache;
            object row3 = e.Row;
            nullable1 = row1.DayTo00;
            // ISSUE: variable of a boxed type
            __Boxed<short> local = (ValueType) nullable1.Value;
            PXSetPropertyException propertyException = new PXSetPropertyException("Day To must be later than or the same as Day From.");
            pxCache.RaiseExceptionHandling<Terms.dayTo00>(row3, (object) local, (Exception) propertyException);
            return;
          }
          nullable1 = row1.DayTo00;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num3 = 31 /*0x1F*/;
          if (nullable2.GetValueOrDefault() >= num3 & nullable2.HasValue)
          {
            PXCache pxCache = cache;
            object row4 = e.Row;
            nullable1 = row1.DayDue00;
            // ISSUE: variable of a boxed type
            __Boxed<short> local = (ValueType) nullable1.Value;
            PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row1, "The Day To value must be less than 31.");
            pxCache.RaiseExceptionHandling<Terms.dayDue00>(row4, (object) local, (Exception) propertyException);
            return;
          }
          nullable1 = row1.DayFrom01;
          int num4 = (int) nullable1.Value;
          nullable1 = row1.DayTo01;
          int num5 = (int) nullable1.Value;
          if (num4 > num5)
          {
            PXCache pxCache = cache;
            object row5 = e.Row;
            nullable1 = row1.DayTo01;
            // ISSUE: variable of a boxed type
            __Boxed<short> local = (ValueType) nullable1.Value;
            PXSetPropertyException propertyException = new PXSetPropertyException("Day To must be later than or the same as Day From.");
            pxCache.RaiseExceptionHandling<Terms.dayTo01>(row5, (object) local, (Exception) propertyException);
            return;
          }
          nullable1 = row1.DayFrom00;
          if (nullable1.Value < (short) 1)
          {
            PXCache pxCache = cache;
            object row6 = e.Row;
            nullable1 = row1.DayFrom00;
            // ISSUE: variable of a boxed type
            __Boxed<short> local = (ValueType) nullable1.Value;
            PXSetPropertyException propertyException = new PXSetPropertyException("Day From must be greater than or equal to 1.");
            pxCache.RaiseExceptionHandling<Terms.dayFrom00>(row6, (object) local, (Exception) propertyException);
            return;
          }
          nullable1 = row1.DayDue01;
          if (nullable1.HasValue)
          {
            nullable1 = row1.DayDue01;
            nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            int num6 = 0;
            if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue))
            {
              nullable1 = row1.DayFrom01;
              if (nullable1.Value < (short) 1)
              {
                PXCache pxCache = cache;
                object row7 = e.Row;
                nullable1 = row1.DayFrom01;
                // ISSUE: variable of a boxed type
                __Boxed<short> local = (ValueType) nullable1.Value;
                PXSetPropertyException propertyException = new PXSetPropertyException("Day From must be greater than or equal to 1.");
                pxCache.RaiseExceptionHandling<Terms.dayFrom01>(row7, (object) local, (Exception) propertyException);
                return;
              }
            }
          }
          nullable1 = row1.DayDue01;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int num7 = 0;
          if (!(nullable2.GetValueOrDefault() <= num7 & nullable2.HasValue))
          {
            nullable1 = row1.DayDue01;
            nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            int num8 = 31 /*0x1F*/;
            if (!(nullable2.GetValueOrDefault() > num8 & nullable2.HasValue))
              goto label_24;
          }
          PXCache pxCache1 = cache;
          object row8 = e.Row;
          nullable1 = row1.DayDue01;
          // ISSUE: variable of a boxed type
          __Boxed<short> local1 = (ValueType) nullable1.Value;
          PXSetPropertyException propertyException1 = new PXSetPropertyException("Value must be in the range of [1-31].");
          pxCache1.RaiseExceptionHandling<Terms.dayDue01>(row8, (object) local1, (Exception) propertyException1);
          return;
        }
      }
      PXCache pxCache2 = cache;
      object row9 = e.Row;
      nullable1 = row1.DayDue00;
      // ISSUE: variable of a boxed type
      __Boxed<short> local2 = (ValueType) nullable1.Value;
      PXSetPropertyException propertyException2 = new PXSetPropertyException("Value must be in the range of [1-31].");
      pxCache2.RaiseExceptionHandling<Terms.dayDue00>(row9, (object) local2, (Exception) propertyException2);
      return;
    }
label_24:
    if (row1.InstallmentMthd == "S")
    {
      Decimal num9 = 0.0M;
      int num10 = 0;
      foreach (PXResult<TermsInstallments> pxResult in ((PXSelectBase<TermsInstallments>) this.Installments).Select(Array.Empty<object>()))
      {
        TermsInstallments termsInstallments = PXResult<TermsInstallments>.op_Implicit(pxResult);
        ++num10;
        Decimal? instPercent = termsInstallments.InstPercent;
        if (instPercent.HasValue)
        {
          Decimal num11 = num9;
          instPercent = termsInstallments.InstPercent;
          Decimal num12 = instPercent.Value;
          num9 = num11 + num12;
        }
      }
      nullable1 = row1.InstallmentCntr;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num13 = num10;
      if (!(nullable2.GetValueOrDefault() == num13 & nullable2.HasValue))
        throw new PXException("The number of records for the installment schedule must match the value in the Number of Installments field.");
      if (Math.Abs(num9 - 100.0M) > 0.01M)
        throw new PXException("The total percentage for Installments Schedule must be equal to 100%.");
    }
    if (row1.InstallmentType == "M")
    {
      nullable1 = row1.InstallmentCntr;
      if (nullable1.HasValue)
      {
        nullable1 = row1.InstallmentCntr;
        nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num = 0;
        if (!(nullable2.GetValueOrDefault() <= num & nullable2.HasValue))
          goto label_49;
      }
      PXCache pxCache = cache;
      object row10 = e.Row;
      nullable1 = row1.InstallmentCntr;
      // ISSUE: variable of a boxed type
      __Boxed<short> local = (ValueType) nullable1.Value;
      PXSetPropertyException propertyException = new PXSetPropertyException("Number of installments must be greater than 0.");
      pxCache.RaiseExceptionHandling<Terms.installmentCntr>(row10, (object) local, (Exception) propertyException);
    }
    else
    {
      if (row1.DiscType == "D" || row1.DiscType == "T")
      {
        nullable1 = row1.DayDisc;
        if (nullable1.Value > (short) 0)
        {
          nullable1 = row1.DayDisc;
          if (nullable1.Value <= (short) 31 /*0x1F*/)
            goto label_46;
        }
        PXCache pxCache = cache;
        object row11 = e.Row;
        nullable1 = row1.DayDisc;
        // ISSUE: variable of a boxed type
        __Boxed<short> local = (ValueType) nullable1.Value;
        PXSetPropertyException propertyException = new PXSetPropertyException("Value must be in the range of [1-31].");
        pxCache.RaiseExceptionHandling<Terms.dayDisc>(row11, (object) local, (Exception) propertyException);
        return;
      }
label_46:
      if ((row1.DiscType == "D" || row1.DiscType == "N" || row1.DiscType == "T") && row1.DueType == row1.DiscType)
      {
        nullable1 = row1.DayDisc;
        int num14 = (int) nullable1.Value;
        nullable1 = row1.DayDue00;
        int num15 = (int) nullable1.Value;
        if (num14 > num15)
        {
          PXCache pxCache3 = cache;
          object row12 = e.Row;
          nullable1 = row1.DayDisc;
          // ISSUE: variable of a boxed type
          __Boxed<short> local3 = (ValueType) nullable1.Value;
          PXSetPropertyException propertyException3 = new PXSetPropertyException("Value must less or equal then the Due Day.");
          pxCache3.RaiseExceptionHandling<Terms.dayDisc>(row12, (object) local3, (Exception) propertyException3);
          PXCache pxCache4 = cache;
          object row13 = e.Row;
          nullable1 = row1.DayDue00;
          // ISSUE: variable of a boxed type
          __Boxed<short> local4 = (ValueType) nullable1.Value;
          PXSetPropertyException propertyException4 = new PXSetPropertyException("Value must greater or equal then a discount day.");
          pxCache4.RaiseExceptionHandling<Terms.dayDue00>(row13, (object) local4, (Exception) propertyException4);
          return;
        }
      }
    }
label_49:
    if (EnumerableExtensions.IsNotIn<string>(row1.VisibleTo, "CU", "AL"))
      cache.SetValueExt<Terms.prepaymentRequired>(e.Row, (object) false);
    if (!(row1.PrepaymentPct.GetValueOrDefault() == 0M) || !row1.PrepaymentRequired.GetValueOrDefault())
      return;
    cache.RaiseExceptionHandling<Terms.prepaymentPct>((object) row1, (object) null, (Exception) new PXSetPropertyException("The prepayment percent must be defined if a prepayment is required."));
  }

  protected virtual void Terms_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    this.isMassDelete = true;
  }

  protected virtual void Terms_InstallmentType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if (row.InstallmentType == "M")
    {
      cache.SetDefaultExt<Terms.dayDisc>((object) row);
      cache.SetDefaultExt<Terms.discPercent>((object) row);
      cache.SetDefaultExt<Terms.prepaymentRequired>((object) row);
    }
    else
    {
      if (!(e.OldValue is string) || !(row.InstallmentType != (string) e.OldValue))
        return;
      row.InstallmentCntr = new short?((short) 0);
      row.InstallmentFreq = "W";
      row.InstallmentMthd = "E";
      this.DeleteInstallments();
    }
  }

  protected virtual void Terms_InstallmentMthd_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if (!(row.InstallmentMthd != (string) e.OldValue))
      return;
    if ((string) e.OldValue == "S")
      this.DeleteInstallments();
    if (!(row.InstallmentMthd == "S"))
      return;
    row.InstallmentCntr = new short?((short) 0);
    cache.SetDefaultExt<Terms.installmentFreq>((object) row);
  }

  protected virtual void Terms_DiscType_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if (!((string) e.OldValue != row.DiscType) || !(row.DiscType == "E") && !(row.DiscType == "M"))
      return;
    row.DayDisc = new short?((short) 0);
  }

  protected virtual void Terms_DayDisc_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if (!(row.DueType == "M") || !(row.DiscType == "N"))
      return;
    short? newValue = (short?) e.NewValue;
    int? nullable = newValue.HasValue ? new int?((int) newValue.GetValueOrDefault()) : new int?();
    int num = 28;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    cache.RaiseExceptionHandling<Terms.dayDisc>((object) row, e.NewValue, (Exception) new PXSetPropertyException("If calculated discount date is greater than due date, it will be reset to due date.", (PXErrorLevel) 2));
  }

  protected virtual void Terms_DiscType_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if ("M" == row.DueType && "P" == (string) e.NewValue)
      throw new PXSetPropertyException("This option cannot be used with the selected Due Date Type.");
    if (("N" == row.DueType || "P" == row.DueType) && "N" != (string) e.NewValue && "P" != (string) e.NewValue)
      throw new PXSetPropertyException("Only Fixed Number of Days option can be used with the selected Due Date Type.");
    if (("D" == row.DueType || "B" == row.DueType) && "E" != (string) e.NewValue && "D" != (string) e.NewValue && "T" != (string) e.NewValue)
      throw new PXSetPropertyException("Only End of Month, Day of the Month and Day of Next Month options can be used with the selected Due Date Type.");
    if ("T" == row.DueType && "T" != (string) e.NewValue)
      throw new PXSetPropertyException("Only Day of the Month option can be used with the selected Due Date Type.");
    if ("F" == row.DueType && "N" != (string) e.NewValue && "D" != (string) e.NewValue && "T" != (string) e.NewValue && "E" != (string) e.NewValue)
      throw new PXSetPropertyException("Only Fixed Number of Days, Day of Next Month, End of Month and Day of the Month options can be used with the selected Due Date Type.");
  }

  protected virtual void Terms_DayTo00_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.CustomDatesUpdated((Terms) e.Row);
  }

  protected virtual void Terms_DayFrom01_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.CustomDatesUpdated((Terms) e.Row);
  }

  protected virtual void Terms_DayDue01_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.CustomDatesUpdated((Terms) e.Row);
  }

  protected virtual void CustomDatesUpdated(Terms row)
  {
    if (row == null || row.DueType != "C")
      return;
    Terms terms1 = row;
    short? dayTo00 = row.DayTo00;
    int? nullable1 = dayTo00.HasValue ? new int?((int) dayTo00.GetValueOrDefault() + 1) : new int?();
    int num1 = 31 /*0x1F*/;
    short? nullable2;
    int? nullable3;
    if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue))
    {
      nullable2 = row.DayTo00;
      if (!nullable2.HasValue)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new int?((int) nullable2.GetValueOrDefault() + 1);
    }
    else
      nullable3 = new int?(0);
    nullable1 = nullable3;
    short? nullable4 = new short?((short) nullable1.Value);
    terms1.DayFrom01 = nullable4;
    nullable2 = row.DayTo00;
    if (nullable2.HasValue)
    {
      nullable2 = row.DayTo00;
      nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        return;
    }
    nullable2 = row.DayFrom01;
    if (!nullable2.HasValue)
      return;
    nullable2 = row.DayFrom01;
    nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    int num3 = 0;
    if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
      return;
    Terms terms2 = row;
    nullable2 = row.DayFrom01;
    int? nullable5;
    if (!nullable2.HasValue)
    {
      nullable1 = new int?();
      nullable5 = nullable1;
    }
    else
      nullable5 = new int?((int) nullable2.GetValueOrDefault() - 1);
    nullable1 = nullable5;
    short? nullable6 = new short?((short) nullable1.Value);
    terms2.DayTo00 = nullable6;
  }

  protected virtual void Terms_DueType_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    Terms row = (Terms) e.Row;
    if (!(row.DueType != (string) e.OldValue))
      return;
    if ((string) e.OldValue == "C")
    {
      row.DayDue01 = new short?((short) 0);
      row.DayFrom00 = new short?((short) 0);
      row.DayFrom01 = new short?((short) 0);
      row.DayTo00 = new short?((short) 0);
      row.DayTo01 = new short?((short) 0);
    }
    row.DayDue00 = new short?((short) 0);
    if (row.DueType == "E")
    {
      row.DayDisc = new short?((short) 0);
      row.DiscType = "E";
      row.DiscPercent = new Decimal?(0.0M);
    }
    if (row.DueType == "M")
    {
      row.DayDisc = new short?((short) 1);
      row.DiscType = "D";
      row.DiscPercent = new Decimal?(0.0M);
    }
    if (row.DueType == "N" || row.DueType == "P" || row.DueType == "D" || row.DueType == "T")
    {
      row.DayDisc = row.DayDue00;
      row.DiscType = row.DueType;
      row.DiscPercent = new Decimal?(0.0M);
    }
    if (row.DueType == "B")
      row.DiscType = "D";
    if (!(row.DueType == "C"))
      return;
    row.DayDue01 = new short?((short) 0);
    row.DayFrom00 = new short?((short) 1);
    row.DayTo00 = new short?((short) 0);
    Terms terms = row;
    short? dayTo00 = row.DayTo00;
    int? nullable1 = dayTo00.HasValue ? new int?((int) dayTo00.GetValueOrDefault() + 1) : new int?();
    int num = 31 /*0x1F*/;
    int? nullable2;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
    {
      dayTo00 = row.DayTo00;
      if (!dayTo00.HasValue)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new int?((int) dayTo00.GetValueOrDefault() + 1);
    }
    else
      nullable2 = new int?(0);
    nullable1 = nullable2;
    short? nullable3 = new short?((short) nullable1.Value);
    terms.DayFrom01 = nullable3;
    row.DayTo01 = new short?((short) 31 /*0x1F*/);
  }

  protected virtual void _(
    Events.FieldUpdated<Terms, Terms.prepaymentRequired> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.Row.PrepaymentRequired.GetValueOrDefault())
      return;
    Decimal? prepaymentPct = eventArgs.Row.PrepaymentPct;
    Decimal num = 0M;
    if (prepaymentPct.GetValueOrDefault() == num & prepaymentPct.HasValue)
      return;
    eventArgs.Row.PrepaymentPct = new Decimal?(0M);
  }

  protected virtual void _(
    Events.FieldVerifying<Terms, Terms.prepaymentPct> eventArgs)
  {
    if (eventArgs.Row != null && ((Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<Terms, Terms.prepaymentPct>, Terms, object>) eventArgs).NewValue).GetValueOrDefault() == 0M && eventArgs.Row.PrepaymentRequired.GetValueOrDefault())
      throw new PXSetPropertyException("The prepayment percent must be defined if a prepayment is required.");
  }

  protected virtual void TermsInstallments_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TermsInstallments row = (TermsInstallments) e.Row;
    short num1 = 0;
    short? installmentNbr1;
    if (row.InstallmentNbr.HasValue)
    {
      installmentNbr1 = row.InstallmentNbr;
      if (installmentNbr1.Value != (short) 0)
      {
        this.justInserted = new int?();
        return;
      }
    }
    foreach (PXResult<TermsInstallments> pxResult in ((PXSelectBase<TermsInstallments>) this.Installments).Select(Array.Empty<object>()))
    {
      TermsInstallments termsInstallments = PXResult<TermsInstallments>.op_Implicit(pxResult);
      if (termsInstallments != row)
      {
        installmentNbr1 = termsInstallments.InstallmentNbr;
        if (installmentNbr1.HasValue)
        {
          installmentNbr1 = termsInstallments.InstallmentNbr;
          if ((int) installmentNbr1.Value > (int) num1)
          {
            installmentNbr1 = termsInstallments.InstallmentNbr;
            num1 = installmentNbr1.Value;
          }
        }
      }
    }
    short num2 = (short) ((int) num1 + 1);
    row.InstallmentNbr = new short?(num2);
    short? installmentNbr2 = row.InstallmentNbr;
    this.justInserted = installmentNbr2.HasValue ? new int?((int) installmentNbr2.GetValueOrDefault()) : new int?();
  }

  protected virtual void TermsInstallments_InstPercent_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    TermsInstallments row = (TermsInstallments) e.Row;
    Decimal? nullable1 = new Decimal?((Decimal) 100);
    foreach (PXResult<TermsInstallments> pxResult in ((PXSelectBase<TermsInstallments>) this.Installments).Select(Array.Empty<object>()))
    {
      TermsInstallments termsInstallments = PXResult<TermsInstallments>.op_Implicit(pxResult);
      if (termsInstallments != row)
      {
        Decimal? nullable2 = nullable1;
        Decimal? instPercent = termsInstallments.InstPercent;
        nullable1 = nullable2.HasValue & instPercent.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - instPercent.GetValueOrDefault()) : new Decimal?();
      }
    }
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    Decimal? nullable3 = nullable1;
    Decimal num = 0M;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable3.GetValueOrDefault() < num & nullable3.HasValue ? new Decimal?(0M) : nullable1);
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual void TermsInstallments_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    Terms current = ((PXSelectBase<Terms>) this.TermsDef).Current;
    Terms terms = current;
    short? installmentCntr = terms.InstallmentCntr;
    terms.InstallmentCntr = installmentCntr.HasValue ? new short?((short) ((int) installmentCntr.GetValueOrDefault() + 1)) : new short?();
    ((PXSelectBase) this.TermsDef).Cache.Update((object) current);
  }

  protected virtual void TermsInstallments_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (((PXSelectBase) this.TermsDef).Cache.GetStatus((object) ((PXSelectBase<Terms>) this.TermsDef).Current) != null)
      return;
    ((PXSelectBase) this.TermsDef).Cache.SetStatus((object) ((PXSelectBase<Terms>) this.TermsDef).Current, (PXEntryStatus) 1);
  }

  protected virtual void TermsInstallments_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    TermsInstallments row = (TermsInstallments) e.Row;
    if (this.isMassDelete)
      return;
    Terms current = ((PXSelectBase<Terms>) this.TermsDef).Current;
    short? nullable1;
    if (this.justInserted.HasValue)
    {
      int num = this.justInserted.Value;
      nullable1 = row.InstallmentNbr;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable2.GetValueOrDefault();
      if (num == valueOrDefault & nullable2.HasValue)
        goto label_4;
    }
    this.distributePercentOf(row);
    ((PXSelectBase) this.Installments).View.RequestRefresh();
label_4:
    Terms terms = current;
    nullable1 = terms.InstallmentCntr;
    short? nullable3 = nullable1;
    terms.InstallmentCntr = nullable3.HasValue ? new short?((short) ((int) nullable3.GetValueOrDefault() - 1)) : new short?();
    ((PXSelectBase) this.TermsDef).Cache.Update((object) current);
  }

  private bool DeleteInstallments()
  {
    this.isMassDelete = true;
    try
    {
      foreach (PXResult<TermsInstallments> pxResult in ((PXSelectBase<TermsInstallments>) this.Installments).Select(Array.Empty<object>()))
        ((PXSelectBase) this.Installments).Cache.Delete((object) PXResult<TermsInstallments>.op_Implicit(pxResult));
    }
    finally
    {
      this.isMassDelete = false;
    }
    ((PXSelectBase<Terms>) this.TermsDef).Current.InstallmentCntr = new short?((short) 0);
    return true;
  }

  private void distributePercentOf(TermsInstallments row)
  {
    if (row == null || !row.InstPercent.HasValue || !row.InstPercent.HasValue)
      return;
    TermsInstallments termsInstallments1 = (TermsInstallments) null;
    foreach (PXResult<TermsInstallments> pxResult in ((PXSelectBase<TermsInstallments>) this.Installments).Select(Array.Empty<object>()))
    {
      TermsInstallments termsInstallments2 = PXResult<TermsInstallments>.op_Implicit(pxResult);
      if (termsInstallments2 != row)
      {
        if (termsInstallments1 != null)
        {
          if (termsInstallments2.InstallmentNbr.HasValue)
          {
            int num = (int) termsInstallments2.InstallmentNbr.Value;
            short? installmentNbr = termsInstallments1.InstallmentNbr;
            int? nullable = installmentNbr.HasValue ? new int?((int) installmentNbr.GetValueOrDefault()) : new int?();
            int valueOrDefault = nullable.GetValueOrDefault();
            if (!(num > valueOrDefault & nullable.HasValue))
              continue;
          }
          else
            continue;
        }
        termsInstallments1 = termsInstallments2;
      }
    }
    if (termsInstallments1 == null)
      return;
    TermsInstallments termsInstallments3 = termsInstallments1;
    Decimal? instPercent1 = termsInstallments3.InstPercent;
    Decimal? instPercent2 = row.InstPercent;
    termsInstallments3.InstPercent = instPercent1.HasValue & instPercent2.HasValue ? new Decimal?(instPercent1.GetValueOrDefault() + instPercent2.GetValueOrDefault()) : new Decimal?();
    row.InstPercent = new Decimal?(0M);
    ((PXSelectBase) this.Installments).Cache.Update((object) termsInstallments1);
  }
}
