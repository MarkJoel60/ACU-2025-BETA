// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NumberingMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CS;

public class NumberingMaint : PXGraph<NumberingMaint, PX.Objects.CS.Numbering>
{
  public PXSelectReadonly<PX.Objects.CS.Numbering> Numbering;
  public PXSelect<PX.Objects.CS.Numbering> Header;
  public PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Optional<PX.Objects.CS.Numbering.numberingID>>>> Sequence;
  private NumberingDetector detector;

  public NumberingMaint()
  {
    PXUIFieldAttribute.SetRequired<NumberingSequence.nbrStep>(((PXSelectBase) this.Sequence).Cache, true);
    this.detector = new NumberingDetector((PXGraph) this, ApplicationAreas.StandardFinance | ApplicationAreas.AdvancedFinance | ApplicationAreas.Distribution | ApplicationAreas.Project | ApplicationAreas.TimeAndExpenses);
  }

  private void CheckNumbers(PXCache cache, object row)
  {
    if (!(row is NumberingSequence numberingSequence))
      return;
    numberingSequence.StartNbr = numberingSequence.StartNbr.SafeTrimStart();
    numberingSequence.EndNbr = numberingSequence.EndNbr.SafeTrimStart();
    numberingSequence.LastNbr = numberingSequence.LastNbr.SafeTrimStart();
    numberingSequence.WarnNbr = numberingSequence.WarnNbr.SafeTrimStart();
    string nbr = (string) null;
    string str1 = numberingSequence.StartNbr == null ? (string) null : NumberingDetector.MakeMask(numberingSequence.StartNbr, ref nbr);
    string start = nbr;
    string str2 = numberingSequence.EndNbr == null ? (string) null : NumberingDetector.MakeMask(numberingSequence.EndNbr, ref nbr);
    string str3 = numberingSequence.LastNbr == null ? (string) null : NumberingDetector.MakeMask(numberingSequence.LastNbr, ref nbr);
    string last = nbr;
    string str4 = numberingSequence.WarnNbr == null ? (string) null : NumberingDetector.MakeMask(numberingSequence.WarnNbr, ref nbr);
    if (str1 != str2 || str3 != null && str1 != str3 || str4 != null && str1 != str4)
    {
      cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("Start, End Number, Last and Warning Numbers must have the identical length and numbering mask.", (PXErrorLevel) 5));
    }
    else
    {
      string startNbr = numberingSequence.StartNbr;
      if ((startNbr != null ? (startNbr.CompareTo(numberingSequence.EndNbr) >= 0 ? 1 : 0) : 0) != 0)
      {
        cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("The start number must be less than the end number.", (PXErrorLevel) 5));
      }
      else
      {
        string warnNbr1 = numberingSequence.WarnNbr;
        if ((warnNbr1 != null ? (warnNbr1.CompareTo(numberingSequence.EndNbr) >= 0 ? 1 : 0) : 0) != 0)
        {
          cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("Warning Number must be less than  the End Number.", (PXErrorLevel) 5));
        }
        else
        {
          string warnNbr2 = numberingSequence.WarnNbr;
          if ((warnNbr2 != null ? (warnNbr2.CompareTo(numberingSequence.StartNbr) <= 0 ? 1 : 0) : 0) != 0)
          {
            cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("Warning Number must be greater than the Start Number.", (PXErrorLevel) 5));
          }
          else
          {
            string lastNbr1 = numberingSequence.LastNbr;
            if ((lastNbr1 != null ? (lastNbr1.CompareTo(numberingSequence.EndNbr) >= 0 ? 1 : 0) : 0) != 0)
            {
              cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("Last Number must be less than the End Number.", (PXErrorLevel) 5));
            }
            else
            {
              string lastNbr2 = numberingSequence.LastNbr;
              if ((lastNbr2 != null ? (lastNbr2.CompareTo(numberingSequence.StartNbr) < 0 ? 1 : 0) : 0) == 0 || this.EqualLastAndStartMinusOne(start, last))
                return;
              cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) numberingSequence, (object) numberingSequence.NumberingID, (Exception) new PXSetPropertyException("Last Number must be greater than or equal to the Start Number-1.", (PXErrorLevel) 5));
            }
          }
        }
      }
    }
  }

  private bool EqualLastAndStartMinusOne(string start, string last)
  {
    char[] charArray = start.ToCharArray();
    for (int index = charArray.Length - 1; index >= 0; --index)
    {
      if (charArray[index] == '0')
      {
        charArray[index] = '9';
      }
      else
      {
        charArray[index] = Convert.ToChar((int) Convert.ToInt16(charArray[index]) - 1);
        break;
      }
    }
    return string.Equals(new string(charArray), last);
  }

  protected virtual void NumberingSequence_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    this.CheckNumbers(cache, e.Row);
  }

  protected virtual void Numbering_NewSymbol_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    e.NewValue = (object) e.NewValue.ToString().TrimStart();
    foreach (PXResult<NumberingSequence> pxResult in ((PXSelectBase<NumberingSequence>) this.Sequence).Select(new object[1]
    {
      (object) ((PX.Objects.CS.Numbering) e.Row).NumberingID
    }))
    {
      if (PXResult<NumberingSequence>.op_Implicit(pxResult).StartNbr.Length < ((string) e.NewValue).Length)
        throw new PXSetPropertyException("New Symbol length must not exceed Start or End Number length.");
    }
  }

  protected virtual void Numbering_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    PX.Objects.CS.Numbering row = (PX.Objects.CS.Numbering) e.Row;
    if (!row.UserNumbering.GetValueOrDefault() && string.IsNullOrEmpty(row.NewSymbol))
      cache.RaiseExceptionHandling<PX.Objects.CS.Numbering.newSymbol>(e.Row, (object) row.NewSymbol, (Exception) new PXException("Either New Number Symbol or Manual Numbering should be set"));
    if (row.NewSymbol == null)
      return;
    foreach (PXResult<NumberingSequence> pxResult in ((PXSelectBase<NumberingSequence>) this.Sequence).Select(new object[1]
    {
      (object) row.NumberingID
    }))
    {
      if (PXResult<NumberingSequence>.op_Implicit(pxResult).StartNbr.Length < row.NewSymbol.Length)
      {
        cache.RaiseExceptionHandling<PX.Objects.CS.Numbering.newSymbol>(e.Row, (object) row.NewSymbol, (Exception) new PXException("New Symbol length must not exceed Start or End Number length."));
        break;
      }
    }
  }

  protected virtual void Numbering_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    PX.Objects.CS.Numbering row = (PX.Objects.CS.Numbering) e.Row;
    string demensionID;
    string segmentID;
    if (this.detector.IsInUseSegments(row.NumberingID, out demensionID, out segmentID))
    {
      cache.RaiseExceptionHandling<PX.Objects.CS.Numbering.numberingID>((object) row, (object) row.NumberingID, (Exception) new PXSetPropertyException("This numbering sequence cannot be deleted. It is used by the '{0}' segmented key, in the '{1}' segment.", new object[2]
      {
        (object) demensionID,
        (object) segmentID
      }));
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      string cacheName;
      string fieldName;
      if (!this.detector.IsInUseSetups(row.NumberingID, out cacheName, out fieldName))
        return;
      cache.RaiseExceptionHandling<PX.Objects.CS.Numbering.numberingID>((object) row, (object) row.NumberingID, (Exception) new PXSetPropertyException("This numbering sequence cannot be deleted. It is used on the '{0}' form, in the '{1}' box.", new object[2]
      {
        (object) cacheName,
        (object) fieldName
      }));
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void Numbering_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CS.Numbering row = (PX.Objects.CS.Numbering) e.Row;
    PXCache pxCache = cache;
    bool? userNumbering = row.UserNumbering;
    bool flag = false;
    int num = userNumbering.GetValueOrDefault() == flag & userNumbering.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetRequired<PX.Objects.CS.Numbering.newSymbol>(pxCache, num != 0);
  }

  protected virtual void NumberingSequence_EndNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is NumberingSequence row) || string.IsNullOrEmpty(row.StartNbr) || !string.IsNullOrEmpty(row.EndNbr))
      return;
    char[] charArray = row.StartNbr.ToCharArray();
    for (int index = charArray.Length - 1; index >= 0 && char.IsDigit(charArray[index]); --index)
      charArray[index] = '9';
    row.EndNbr = new string(charArray);
  }

  protected virtual void NumberingSequence_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is NumberingSequence row) || row.StartDate.HasValue || PXSelectBase<NumberingSequence, PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Current<PX.Objects.CS.Numbering.numberingID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>()).Count >= 1)
      return;
    row.StartDate = new DateTime?(new DateTime(1900, 1, 1));
  }

  protected virtual void NumberingSequence_LastNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is NumberingSequence row) || !string.IsNullOrEmpty(row.LastNbr) || string.IsNullOrEmpty(row.StartNbr))
      return;
    char[] charArray = row.StartNbr.ToCharArray();
    char c = charArray[charArray.GetUpperBound(0)];
    if (char.IsDigit(c))
    {
      int num = int.Parse(new string(new char[1]{ c }));
      if (num > 0)
        charArray[charArray.GetUpperBound(0)] = (num - 1).ToString().ToCharArray()[0];
    }
    row.LastNbr = new string(charArray);
  }

  protected virtual void NumberingSequence_NbrStep_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && (int) e.NewValue == 0)
      throw new PXSetPropertyException("Zero increment is not allowed.");
  }

  protected virtual void NumberingSequence_WarnNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is NumberingSequence row) || string.IsNullOrEmpty(row.EndNbr) || !string.IsNullOrEmpty(row.WarnNbr) || row.EndNbr.Length < 3 || !char.IsDigit(row.EndNbr[row.EndNbr.Length - 1]) || !char.IsDigit(row.EndNbr[row.EndNbr.Length - 2]) || !char.IsDigit(row.EndNbr[row.EndNbr.Length - 3]))
      return;
    int num = int.Parse(row.EndNbr.Substring(row.EndNbr.Length - 3, 3));
    if (num <= 100)
      return;
    string str = $"{num - 100:000}";
    row.WarnNbr = row.EndNbr.Substring(0, row.EndNbr.Length - 3) + str;
  }

  protected virtual void NumberingSequence_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    NumberingSequence row = (NumberingSequence) e.Row;
    this.CheckNumbers(cache, (object) row);
    foreach (PXResult<Dimension, Segment> pxResult in PXSelectBase<Dimension, PXSelectJoin<Dimension, InnerJoin<Segment, On<Segment.dimensionID, Equal<Dimension.dimensionID>>>, Where<Dimension.numberingID, Equal<Optional<PX.Objects.CS.Numbering.numberingID>>, And<Segment.autoNumber, Equal<Optional<Segment.autoNumber>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((NumberingSequence) e.Row).NumberingID,
      (object) true
    }))
    {
      PXResult<Dimension, Segment>.op_Implicit(pxResult);
      Segment segment = PXResult<Dimension, Segment>.op_Implicit(pxResult);
      int length = row.StartNbr.Length;
      short? nullable1 = segment.Length;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable2.GetValueOrDefault();
      if (!(length == valueOrDefault & nullable2.HasValue))
      {
        PXCache pxCache = cache;
        NumberingSequence numberingSequence = row;
        string startNbr = row.StartNbr;
        object[] objArray = new object[2]
        {
          (object) segment.DimensionID,
          null
        };
        nullable1 = segment.SegmentID;
        objArray[1] = (object) nullable1.ToString();
        PXSetPropertyException propertyException = new PXSetPropertyException("Auto Numbering format violates the segment format. Segmented Key: '{0}' Segment: '{1}'.", objArray);
        pxCache.RaiseExceptionHandling<NumberingSequence.startNbr>((object) numberingSequence, (object) startNbr, (Exception) propertyException);
      }
      string str = Regex.Replace(Regex.Replace(row.StartNbr, "[0-9]", "9"), "[^0-9]", "?");
      if (segment.EditMask == "?" && str.Contains("9") || segment.EditMask == "9" && str.Contains("?"))
      {
        PXCache pxCache = cache;
        NumberingSequence numberingSequence = row;
        string startNbr = row.StartNbr;
        object[] objArray = new object[2]
        {
          (object) segment.DimensionID,
          null
        };
        nullable1 = segment.SegmentID;
        objArray[1] = (object) nullable1.ToString();
        PXSetPropertyException propertyException = new PXSetPropertyException("Auto Numbering format violates the segment format. Segmented Key: '{0}' Segment: '{1}'.", objArray);
        pxCache.RaiseExceptionHandling<NumberingSequence.startNbr>((object) numberingSequence, (object) startNbr, (Exception) propertyException);
      }
    }
  }

  protected virtual void _(Events.RowSelected<NumberingSequence> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<NumberingSequence>>) e).Cache.RaiseExceptionHandling<NumberingSequence.numberingID>((object) e.Row, (object) e.Row.NumberingID, (Exception) null);
    this.CheckNumbers(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<NumberingSequence>>) e).Cache, (object) e.Row);
  }

  public virtual void Persist()
  {
    HashSet<NumberingMaint.NumPair> numPairSet = new HashSet<NumberingMaint.NumPair>();
    foreach (PXResult<NumberingSequence> pxResult in ((PXSelectBase<NumberingSequence>) this.Sequence).Select(Array.Empty<object>()))
    {
      NumberingSequence numberingSequence = PXResult<NumberingSequence>.op_Implicit(pxResult);
      if (((PXSelectBase) this.Sequence).Cache.GetStatus((object) numberingSequence) == null && numberingSequence.StartDate.HasValue)
        numPairSet.Add(new NumberingMaint.NumPair(numberingSequence.NBranchID, numberingSequence.StartDate));
    }
    foreach (NumberingSequence numberingSequence in ((PXSelectBase) this.Sequence).Cache.Inserted)
    {
      if (numberingSequence.StartDate.HasValue && !numPairSet.Add(new NumberingMaint.NumPair(numberingSequence.NBranchID, numberingSequence.StartDate)))
        ((PXSelectBase) this.Sequence).Cache.RaiseExceptionHandling<NumberingSequence.startDate>((object) numberingSequence, (object) numberingSequence.StartDate, (Exception) new PXSetPropertyException("Start Date is not unique"));
    }
    foreach (NumberingSequence numberingSequence in ((PXSelectBase) this.Sequence).Cache.Updated)
    {
      if (numberingSequence.StartDate.HasValue && !numPairSet.Add(new NumberingMaint.NumPair(numberingSequence.NBranchID, numberingSequence.StartDate)))
        ((PXSelectBase) this.Sequence).Cache.RaiseExceptionHandling<NumberingSequence.startDate>((object) numberingSequence, (object) numberingSequence.StartDate, (Exception) new PXSetPropertyException("Start Date is not unique"));
    }
    ((PXGraph) this).Persist();
  }

  private struct NumPair(int? BranchID, DateTime? StartDate)
  {
    public int? BranchID = BranchID;
    public DateTime? StartDate = StartDate;
  }
}
