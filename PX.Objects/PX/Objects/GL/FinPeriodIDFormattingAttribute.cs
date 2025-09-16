// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriodIDFormattingAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL.FinPeriods;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Implements Formatting for FinPeriod field.
/// FinPeriod is stored and dispalyed as a string but in different formats.
/// This Attribute handles the conversion of one format into another.
/// </summary>
public class FinPeriodIDFormattingAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber
{
  protected const string CS_DISPLAY_MASK = "##-####";

  public string DisplayMask { get; set; } = "##-####";

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    e.NewValue = (object) FinPeriodIDFormattingAttribute.FormatForStoring(e.NewValue as string);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue is string returnValue && (e.Row == null || object.Equals(e.ReturnValue, sender.GetValue(e.Row, this._FieldOrdinal))))
      e.ReturnValue = (object) FinPeriodIDFormattingAttribute.FormatForDisplay(returnValue);
    if (this._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(6), new bool?(), this._FieldName, new bool?(), new int?(), this.DisplayMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
  }

  public static string FormatForError(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForError(period, "##-####");
  }

  public static string FormatForError(string period, string displayMask)
  {
    return Mask.Format(displayMask, FinPeriodIDFormattingAttribute.FormatForDisplay(period));
  }

  protected static string FixedLength(string period)
  {
    if (period == null)
      return (string) null;
    return period.Length >= 6 ? period.Substring(0, 6) : period.PadRight(6);
  }

  public static string FormatForDisplay(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForDisplayInt(FinPeriodIDFormattingAttribute.FixedLength(period));
  }

  protected static string FormatForDisplayInt(string period)
  {
    return !string.IsNullOrEmpty(period) ? FinPeriodUtils.PeriodInYear(period) + FinPeriodUtils.FiscalYear(period) : (string) null;
  }

  public static string FormatForStoring(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForStoringInt(FinPeriodIDFormattingAttribute.FixedLength(period));
  }

  public static string FormatForStoringNoTrim(string period)
  {
    period = FinPeriodIDFormattingAttribute.FixedLength(period);
    return !string.IsNullOrEmpty(period) ? period.Substring(2, 4) + period.Substring(0, 2) : (string) null;
  }

  protected static string FormatForStoringInt(string period)
  {
    string str = period?.Trim();
    if (!string.IsNullOrEmpty(str) && str.Length < 6)
      return str;
    return !string.IsNullOrEmpty(period) ? period.Substring(2, 4) + period.Substring(0, 2) : (string) null;
  }

  protected static string FormatPeriod(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForDisplay(period);
  }

  protected static string UnFormatPeriod(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForStoring(period);
  }
}
