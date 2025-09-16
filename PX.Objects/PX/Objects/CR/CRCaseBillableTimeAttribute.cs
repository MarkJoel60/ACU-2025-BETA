// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseBillableTimeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
internal sealed class CRCaseBillableTimeAttribute : PXIntAttribute
{
  private PXGraph _graph;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._graph = sender.Graph;
    // ISSUE: method pointer
    this._graph.RowSelected.AddHandler<CRCase>(new PXRowSelected((object) this, __methodptr(\u003CCacheAttached\u003Eb__1_0)));
  }

  private void CalculateBillableTime(CRCase caseRow)
  {
    if (caseRow == null)
      return;
    bool flag = false;
    CRCaseClass crCaseClass = (CRCaseClass) PXSelectorAttribute.Select<CRCase.caseClassID>(this._graph.Caches[typeof (CRCase)], (object) caseRow);
    if (crCaseClass != null)
      flag = crCaseClass.PerItemBilling.GetValueOrDefault() == 1;
    if ((flag ? 1 : (!caseRow.ManualBillableTimes.GetValueOrDefault() ? 1 : 0)) == 0)
      return;
    caseRow.TimeSpent = new int?(0);
    caseRow.OvertimeSpent = new int?(0);
    caseRow.TimeBillable = new int?(0);
    caseRow.OvertimeBillable = new int?(0);
    foreach (PXResult<CRPMTimeActivity> pxResult in PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.refNoteID, Equal<Required<CRPMTimeActivity.refNoteID>>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.task>, And<CRPMTimeActivity.classID, NotEqual<CRActivityClass.events>>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) caseRow.NoteID
    }))
    {
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
      CRCase crCase1 = caseRow;
      int? nullable = crCase1.TimeSpent;
      int valueOrDefault1 = crpmTimeActivity.TimeSpent.GetValueOrDefault();
      crCase1.TimeSpent = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault1) : new int?();
      CRCase crCase2 = caseRow;
      nullable = crCase2.OvertimeSpent;
      int valueOrDefault2 = crpmTimeActivity.OvertimeSpent.GetValueOrDefault();
      crCase2.OvertimeSpent = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault2) : new int?();
      if (crpmTimeActivity.IsBillable.GetValueOrDefault())
      {
        nullable = crpmTimeActivity.TimeBillable;
        int num1 = nullable.GetValueOrDefault();
        nullable = crpmTimeActivity.OvertimeBillable;
        int num2 = nullable.GetValueOrDefault();
        if (crCaseClass != null)
        {
          nullable = crCaseClass.RoundingInMinutes;
          int num3 = 1;
          if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
          {
            if (num1 > 0)
            {
              int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num1) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
              nullable = crCaseClass.RoundingInMinutes;
              int valueOrDefault3 = nullable.GetValueOrDefault();
              num1 = int32 * valueOrDefault3;
              nullable = crCaseClass.MinBillTimeInMinutes;
              int num4 = 0;
              if (nullable.GetValueOrDefault() > num4 & nullable.HasValue)
              {
                int val1 = num1;
                nullable = crCaseClass.MinBillTimeInMinutes;
                int val2 = nullable.Value;
                num1 = Math.Max(val1, val2);
              }
            }
            if (num2 > 0)
            {
              int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num2) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
              nullable = crCaseClass.RoundingInMinutes;
              int valueOrDefault4 = nullable.GetValueOrDefault();
              num2 = int32 * valueOrDefault4;
              nullable = crCaseClass.MinBillTimeInMinutes;
              int num5 = 0;
              if (nullable.GetValueOrDefault() > num5 & nullable.HasValue)
              {
                int val1 = num2;
                nullable = crCaseClass.MinBillTimeInMinutes;
                int val2 = nullable.Value;
                num2 = Math.Max(val1, val2);
              }
            }
          }
        }
        CRCase crCase3 = caseRow;
        nullable = crCase3.TimeBillable;
        int num6 = num1;
        crCase3.TimeBillable = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + num6) : new int?();
        CRCase crCase4 = caseRow;
        nullable = crCase4.OvertimeBillable;
        int num7 = num2;
        crCase4.OvertimeBillable = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + num7) : new int?();
      }
    }
  }
}
