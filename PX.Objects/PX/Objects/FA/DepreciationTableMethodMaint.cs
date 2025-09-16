// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationTableMethodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class DepreciationTableMethodMaint : 
  PXGraph<DepreciationTableMethodMaint, FADepreciationMethod>
{
  public PXSelect<FADepreciationMethod, Where<FADepreciationMethod.isTableMethod, Equal<True>>> Method;
  public PXSelect<FADepreciationMethodLines, Where<FADepreciationMethodLines.methodID, Equal<Current<FADepreciationMethod.methodID>>>> details;
  public PXSetupOptional<PX.Objects.FA.FASetup> FASetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<FADepreciationMethod.methodCD, Where<FADepreciationMethod.isTableMethod, Equal<True>>>), DescriptionField = typeof (FADepreciationMethod.description))]
  [PXUIField]
  [PXDefault]
  [PXFieldDescription]
  public void FADepreciationMethod_MethodCD_CacheAttached(PXCache cache)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public void FADepreciationMethod_YearlyAccountancy_CacheAttached(PXCache cache)
  {
  }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField]
  [PXDefault]
  public void FADepreciationMethod_UsefulLife_CacheAttached(PXCache cache)
  {
  }

  public DepreciationTableMethodMaint()
  {
    PX.Objects.FA.FASetup current = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
    ((PXSelectBase) this.details).Cache.AllowInsert = false;
    ((PXSelectBase) this.details).Cache.AllowDelete = false;
  }

  protected virtual void FADepreciationMethod_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<FADepreciationMethod.parentMethodID>(sender, (object) row, row.RecordType == "A");
    PXDefaultAttribute.SetPersistingCheck<FADepreciationMethod.parentMethodID>(sender, (object) row, row.RecordType == "A" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<FADepreciationMethod.parentMethodID>(sender, row.RecordType == "A");
    PXUIFieldAttribute.SetEnabled<FADepreciationMethod.averagingConvPeriod>(sender, (object) row, row.AveragingConvention != "FY");
    FAAveragingConvention.SetAveragingConventionsList<FADepreciationMethod.averagingConvention>(sender, (object) row, new KeyValuePair<object, Dictionary<object, string[]>>((object) row.RecordType, FAAveragingConvention.RecordTypeDisabledValues));
    PXUIFieldAttribute.SetEnabled<FADepreciationMethod.usefulLife>(sender, (object) row, row.RecordType != "A");
    bool flag = !row.IsPredefined.GetValueOrDefault() || ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current.AllowEditPredefinedDeprMethod.GetValueOrDefault();
    sender.AllowUpdate = flag;
    sender.AllowDelete = flag;
    ((PXCache) GraphHelper.Caches<FADepreciationMethodLines>((PXGraph) this)).AllowUpdate = flag;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("FP")]
  [PXUIField]
  [FAAveragingConvention.List]
  [PXFormula(typeof (Switch<Case<Where<FADepreciationMethod.recordType, Equal<FARecordType.bothType>>, FAAveragingConvention.fullYear>, FADepreciationMethod.averagingConvention>))]
  protected virtual void FADepreciationMethod_AveragingConvention_CacheAttached(PXCache sender)
  {
  }

  protected virtual void AdjustMethodLines(FADepreciationMethod meth)
  {
    if (!meth.UsefulLife.HasValue || meth.AveragingConvention == null || !meth.AveragingConvPeriod.HasValue)
      return;
    int num1;
    switch (meth.AveragingConvention)
    {
      case "HY":
      case "FY":
        num1 = 12;
        break;
      case "HQ":
      case "FQ":
        num1 = 3;
        break;
      default:
        num1 = 1;
        break;
    }
    int num2 = ((int) meth.AveragingConvPeriod.Value - 1) * num1;
    int num3 = (int) Math.Ceiling(meth.UsefulLife.Value * 12M + (Decimal) num2);
    if (meth.AveragingConvention == "HY" || meth.AveragingConvention == "HQ" || meth.AveragingConvention == "HP" || meth.AveragingConvention == "MP" || meth.AveragingConvention == "M2" || meth.AveragingConvention == "NP")
      ++num3;
    int num4 = (int) Math.Ceiling((double) num3 / 12.0);
    List<FADepreciationMethodLines> list = GraphHelper.RowCast<FADepreciationMethodLines>((IEnumerable) PXSelectBase<FADepreciationMethodLines, PXSelect<FADepreciationMethodLines, Where<FADepreciationMethodLines.methodID, Equal<Current<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<FADepreciationMethodLines>();
    if (num4 == list.Count)
      return;
    Decimal num5 = 0M;
    Decimal num6 = 0M;
    int num7 = 0;
    FADepreciationMethodLines depreciationMethodLines1 = (FADepreciationMethodLines) null;
    list.Sort((Comparison<FADepreciationMethodLines>) ((line1, line2) =>
    {
      int? year1 = line1.Year;
      Decimal valueOrDefault1 = year1.HasValue ? (Decimal) year1.GetValueOrDefault() : 0M;
      ref Decimal local = ref valueOrDefault1;
      int? year2 = line2.Year;
      Decimal valueOrDefault2 = year2.HasValue ? (Decimal) year2.GetValueOrDefault() : 0M;
      return local.CompareTo(valueOrDefault2);
    }));
    foreach (FADepreciationMethodLines depreciationMethodLines2 in list)
    {
      num5 += depreciationMethodLines2.RatioPerYear.GetValueOrDefault();
      ++num7;
      if (num7 > num4)
      {
        ((PXSelectBase<FADepreciationMethodLines>) this.details).Delete(PXCache<FADepreciationMethodLines>.CreateCopy(depreciationMethodLines2));
      }
      else
      {
        depreciationMethodLines1 = PXCache<FADepreciationMethodLines>.CreateCopy(depreciationMethodLines2);
        num6 += depreciationMethodLines2.RatioPerYear.GetValueOrDefault();
      }
    }
    if (num7 > num4 && depreciationMethodLines1 != null)
    {
      FADepreciationMethodLines depreciationMethodLines3 = depreciationMethodLines1;
      Decimal? ratioPerYear = depreciationMethodLines3.RatioPerYear;
      Decimal num8 = num5 - num6;
      depreciationMethodLines3.RatioPerYear = ratioPerYear.HasValue ? new Decimal?(ratioPerYear.GetValueOrDefault() + num8) : new Decimal?();
      ((PXSelectBase) this.details).Cache.SetDefaultExt<FADepreciationMethodLines.displayRatioPerYear>((object) ((PXSelectBase<FADepreciationMethodLines>) this.details).Update(depreciationMethodLines1));
    }
    else
    {
      for (int index = num7 + 1; index <= num4; ++index)
        ((PXSelectBase<FADepreciationMethodLines>) this.details).Insert(new FADepreciationMethodLines()
        {
          Year = new int?(index),
          RatioPerYear = new Decimal?(0M)
        });
    }
  }

  [PXMergeAttributes]
  [PXDefault(0)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FADepreciationMethod.recoveryPeriod> e)
  {
  }

  protected virtual void FADepreciationMethod_AveragingConvention_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    if (((PXSelectBase<FADepreciationMethod>) this.Method).Current.AveragingConvention == "FY")
      sender.SetDefaultExt<FADepreciationMethod.recoveryPeriod>((object) ((PXSelectBase<FADepreciationMethod>) this.Method).Current);
    this.AdjustMethodLines(row);
  }

  protected virtual void FADepreciationMethod_UsefulLife_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    row.RecoveryPeriod = new int?((int) (row.UsefulLife.GetValueOrDefault() * 12M));
    this.AdjustMethodLines(row);
  }

  protected virtual void FADepreciationMethod_AveragingConvPeriod_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    this.AdjustMethodLines(row);
  }

  protected virtual void FADepreciationMethod_RecordType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    if (row.RecordType == "A")
    {
      PXDefaultAttribute.SetPersistingCheck<FADepreciationMethod.parentMethodID>(sender, (object) row, (PXPersistingCheck) 1);
      PXUIFieldAttribute.SetRequired<FADepreciationMethod.parentMethodID>(sender, true);
    }
    else
    {
      PXDefaultAttribute.SetPersistingCheck<FADepreciationMethod.parentMethodID>(sender, (object) row, (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetRequired<FADepreciationMethod.parentMethodID>(sender, false);
    }
    sender.SetDefaultExt<FADepreciationMethod.parentMethodID>((object) row);
  }

  protected virtual void FADepreciationMethod_AveragingConvPeriod_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      short newValue = (short) e.NewValue;
      if (newValue < (short) 1)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
        {
          (object) 1
        });
      switch (((PXSelectBase<FADepreciationMethod>) this.Method).Current.AveragingConvention)
      {
        case "HP":
          if (newValue <= (short) 12)
            break;
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", (PXErrorLevel) 4, new object[1]
          {
            (object) 12
          });
        case "HQ":
          if (newValue <= (short) 4)
            break;
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", (PXErrorLevel) 4, new object[1]
          {
            (object) 4
          });
        case "HY":
          if (newValue <= (short) 2)
            break;
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", (PXErrorLevel) 4, new object[1]
          {
            (object) 2
          });
      }
    }
  }

  protected virtual void FADepreciationMethod_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    FABookSettings faBookSettings = PXResultset<FABookSettings>.op_Implicit(PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.depreciationMethodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.MethodID
    }));
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.depreciationMethodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.MethodID
    }));
    if (faBookSettings != null || faBookBalance != null)
      throw new PXSetPropertyException("You cannot delete Depreciation Method because this Method used in Fixed Assets or Classes.");
  }

  protected virtual void FADepreciationMethod_DisplayTotalPercents_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 100M;
    if (newValue.GetValueOrDefault() > num & newValue.HasValue && !e.ExternalCall)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) 100
      });
  }

  protected virtual void FADepreciationMethod_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    Decimal? displayTotalPercents = row.DisplayTotalPercents;
    Decimal num = 100M;
    if (!(displayTotalPercents.GetValueOrDefault() > num & displayTotalPercents.HasValue))
      return;
    sender.RaiseExceptionHandling<FADepreciationMethod.displayTotalPercents>((object) row, (object) row.DisplayTotalPercents, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
    {
      (object) 100
    }));
  }

  protected virtual void FADepreciationMethod_ParentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    if (!row.ParentMethodID.HasValue)
    {
      sender.SetValueExt<FADepreciationMethod.usefulLife>((object) row, (object) null);
    }
    else
    {
      FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.recordType, Equal<FARecordType.classType>, And<FADepreciationMethod.methodID, Equal<Current<FADepreciationMethod.parentMethodID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      sender.SetValueExt<FADepreciationMethod.usefulLife>((object) row, (object) depreciationMethod.UsefulLife);
    }
  }

  protected virtual void FADepreciationMethodLines_DisplayRatioPerYear_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue1 = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue1.GetValueOrDefault() < num & newValue1.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    Decimal? newValue2 = (Decimal?) e.NewValue;
    num = 100M;
    if (newValue2.GetValueOrDefault() > num & newValue2.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) 100.ToString()
      });
  }

  public virtual void Persist()
  {
    foreach (FADepreciationMethod depreciationMethod in ((PXSelectBase) this.Method).Cache.Updated)
    {
      depreciationMethod.IsPredefined = new bool?(false);
      ((PXSelectBase<FADepreciationMethod>) this.Method).Update(depreciationMethod);
    }
    foreach (FADepreciationMethod depreciationMethod in new HashSet<int?>(((IEnumerable<FADepreciationMethodLines>) ((PXSelectBase) this.details).Cache.Inserted.ToArray<FADepreciationMethodLines>()).Concat<FADepreciationMethodLines>(((IEnumerable<FADepreciationMethodLines>) ((PXSelectBase) this.details).Cache.Updated.ToArray<FADepreciationMethodLines>()).Concat<FADepreciationMethodLines>((IEnumerable<FADepreciationMethodLines>) ((PXSelectBase) this.details).Cache.Deleted)).Select<FADepreciationMethodLines, int?>((Func<FADepreciationMethodLines, int?>) (line => line.MethodID))).Select<int?, FADepreciationMethod>((Func<int?, FADepreciationMethod>) (methodID => PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) methodID
    })))).Where<FADepreciationMethod>((Func<FADepreciationMethod, bool>) (method => method != null && method.IsPredefined.GetValueOrDefault())))
    {
      depreciationMethod.IsPredefined = new bool?(false);
      ((PXSelectBase<FADepreciationMethod>) this.Method).Update(depreciationMethod);
    }
    ((PXGraph) this).Persist();
  }
}
