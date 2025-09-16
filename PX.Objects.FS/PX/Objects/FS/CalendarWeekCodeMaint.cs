// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CalendarWeekCodeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

public class CalendarWeekCodeMaint : PXGraph<
#nullable disable
CalendarWeekCodeMaint>
{
  [PXImport(typeof (FSWeekCodeDate))]
  public PXSelectOrderBy<FSWeekCodeDate, OrderBy<Asc<FSWeekCodeDate.weekCodeDate>>> CalendarWeekCodeRecords;
  public PXFilter<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> CalendarWeekCodeGenerationOptions;
  public PXSave<FSWeekCodeDate> Save;
  public PXCancel<FSWeekCodeDate> Cancel;
  public PXAction<FSWeekCodeDate> generateWeekCode;

  /// <summary>
  /// Sets the values of the FSWeekCodeDate.BeginDateOfWeek and FSWeekCodeDate.EndDateOfWeek memory fields.
  /// </summary>
  /// <param name="fsWeekCodeDateRow">FSWeekCodeDate Row.</param>
  public virtual void SetBeginEndWeekDates(FSWeekCodeDate fsWeekCodeDateRow)
  {
    if (!fsWeekCodeDateRow.WeekCodeDate.HasValue)
      return;
    DateTime dateTime = fsWeekCodeDateRow.WeekCodeDate.Value;
    fsWeekCodeDateRow.BeginDateOfWeek = new DateTime?(SharedFunctions.StartOfWeek(fsWeekCodeDateRow.WeekCodeDate.Value, DayOfWeek.Monday));
    fsWeekCodeDateRow.EndDateOfWeek = new DateTime?(SharedFunctions.EndOfWeek(fsWeekCodeDateRow.WeekCodeDate.Value, DayOfWeek.Monday));
  }

  /// <summary>
  /// Split the fsWeekCodeDateRow.WeekCode field into the WeekCode parameters.
  /// </summary>
  /// <param name="fsWeekCodeDateRow">FSWeekCodeDate Row.</param>
  public virtual void SplitWeekCodeParameters(FSWeekCodeDate fsWeekCodeDateRow)
  {
    if (fsWeekCodeDateRow.WeekCode != null && fsWeekCodeDateRow.WeekCode.Length == 4)
    {
      fsWeekCodeDateRow.WeekCodeP1 = fsWeekCodeDateRow.WeekCode.Substring(0, 1);
      fsWeekCodeDateRow.WeekCodeP2 = fsWeekCodeDateRow.WeekCode.Substring(1, 1);
      fsWeekCodeDateRow.WeekCodeP3 = fsWeekCodeDateRow.WeekCode.Substring(2, 1);
      fsWeekCodeDateRow.WeekCodeP4 = fsWeekCodeDateRow.WeekCode.Substring(3, 1);
    }
    else
    {
      fsWeekCodeDateRow.WeekCodeP1 = (string) null;
      fsWeekCodeDateRow.WeekCodeP2 = (string) null;
      fsWeekCodeDateRow.WeekCodeP3 = (string) null;
      fsWeekCodeDateRow.WeekCodeP4 = (string) null;
    }
  }

  /// <summary>Calculates the Week Code of a specific date.</summary>
  /// <param name="baseDate">Date by which the Week Code will be calculated.</param>
  /// <param name="fsWeekCodeRow">FSWeekCodeDate Row.</param>
  public virtual void AutoCalcWeekCode(DateTime baseDate, FSWeekCodeDate fsWeekCodeRow)
  {
    int weeks = (int) (fsWeekCodeRow.WeekCodeDate.Value.Subtract(baseDate).TotalDays / 7.0) + 1;
    string str1 = this.CalcWeekCodeParameterP1(weeks);
    string str2 = this.CalcWeekCodeParameterP2(weeks);
    string str3 = this.CalcWeekCodeParameterP3(weeks);
    string str4 = this.CalcWeekCodeParameterP4(weeks);
    fsWeekCodeRow.DayOfWeek = new int?((int) fsWeekCodeRow.WeekCodeDate.Value.DayOfWeek);
    fsWeekCodeRow.WeekCode = str1 + str2 + str3 + str4;
  }

  /// <summary>Calculates the Fourth parameter of the Week Code.</summary>
  /// <param name="weeks">Number of the week in which the date belongs.</param>
  /// <returns>Fourth parameter of the Week Code.</returns>
  public virtual string CalcWeekCodeParameterP4(int weeks)
  {
    int num = weeks % 32 /*0x20*/;
    if (num > 0 && num <= 4)
      return "S";
    if (num > 4 && num <= 8)
      return "T";
    if (num > 8 && num <= 12)
      return "U";
    if (num > 12 && num <= 16 /*0x10*/)
      return "V";
    if (num > 16 /*0x10*/ && num <= 20)
      return "W";
    if (num > 20 && num <= 24)
      return "X";
    return num > 24 && num <= 28 ? "Y" : "Z";
  }

  /// <summary>Calculates the 3rd parameter of the Week Code.</summary>
  /// <param name="weeks">Number of the week in which the date belongs.</param>
  /// <returns>3rd Parameter of the Week Code.</returns>
  public virtual string CalcWeekCodeParameterP3(int weeks)
  {
    int num = weeks % 16 /*0x10*/;
    if (num > 0 && num <= 4)
      return "C";
    if (num > 4 && num <= 8)
      return "D";
    return num > 8 && num <= 12 ? "E" : "F";
  }

  /// <summary>Calculates the second parameter of the Week Code.</summary>
  /// <param name="weeks">Number of the week in which the date belongs.</param>
  /// <returns>Second parameter of the Week Code.</returns>
  public virtual string CalcWeekCodeParameterP2(int weeks)
  {
    int num = weeks % 8;
    return num > 0 && num <= 4 ? "A" : "B";
  }

  /// <summary>Calculates the 1st parameter of the Week Code.</summary>
  /// <param name="weeks">Number of the week in which the date belongs.</param>
  /// <returns>1st Parameter of the Week Code.</returns>
  public virtual string CalcWeekCodeParameterP1(int weeks)
  {
    int num = weeks % 4;
    return num == 0 ? "4" : num.ToString();
  }

  /// <summary>
  /// Validates the CalendarWeekCodeGeneration.endDate value.
  /// </summary>
  /// <param name="calendarWeekCodeGenrationRow">CalendarWeekCodeGeneration Row.</param>
  /// <param name="cache">Cache of the View.</param>
  /// <returns>true: valid value | false: invalid value.</returns>
  public virtual bool ValidateEndGenerationDate(
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration calendarWeekCodeGenrationRow,
    PXCache cache)
  {
    bool flag = true;
    if (!calendarWeekCodeGenrationRow.EndDate.HasValue)
    {
      cache.RaiseExceptionHandling<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>((object) calendarWeekCodeGenrationRow, (object) calendarWeekCodeGenrationRow.EndDate, (Exception) new PXException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>(cache)
      })));
      flag = false;
    }
    else
    {
      DateTime? endDate = calendarWeekCodeGenrationRow.EndDate;
      DateTime? startDate = calendarWeekCodeGenrationRow.StartDate;
      if ((endDate.HasValue & startDate.HasValue ? (endDate.GetValueOrDefault() <= startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        cache.RaiseExceptionHandling<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>((object) calendarWeekCodeGenrationRow, (object) calendarWeekCodeGenrationRow.EndDate, (Exception) new PXException("The dates are invalid. The end date cannot be earlier than the start date."));
        flag = false;
      }
    }
    return flag;
  }

  /// <summary>
  /// Validates the CalendarWeekCodeGeneration.startDate value.
  /// </summary>
  /// <param name="calendarWeekCodeGenrationRow">CalendarWeekCodeGeneration Row.</param>
  /// <param name="cache">Cache of the View.</param>
  /// <returns>true: valid value | false: invalid value.</returns>
  public virtual bool ValidateStartGenerationDate(
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration calendarWeekCodeGenrationRow,
    PXCache cache)
  {
    bool flag = true;
    if (!calendarWeekCodeGenrationRow.StartDate.HasValue)
    {
      cache.RaiseExceptionHandling<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>((object) calendarWeekCodeGenrationRow, (object) calendarWeekCodeGenrationRow.StartDate, (Exception) new PXException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>(cache)
      })));
      flag = false;
    }
    else
    {
      DateTime? startDate = calendarWeekCodeGenrationRow.StartDate;
      DateTime? defaultStartDate = calendarWeekCodeGenrationRow.DefaultStartDate;
      if ((startDate.HasValue & defaultStartDate.HasValue ? (startDate.GetValueOrDefault() < defaultStartDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        cache.RaiseExceptionHandling<CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>((object) calendarWeekCodeGenrationRow, (object) calendarWeekCodeGenrationRow.StartDate, (Exception) new PXException("The dates are invalid. The start date cannot be earlier than the default date."));
        flag = false;
      }
    }
    return flag;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable GenerateWeekCode(PXAdapter adapter)
  {
    if (((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).AskExt() == 1)
    {
      if (!this.ValidateEndGenerationDate(((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current, ((PXSelectBase) this.CalendarWeekCodeGenerationOptions).Cache) || !this.ValidateStartGenerationDate(((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current, ((PXSelectBase) this.CalendarWeekCodeGenerationOptions).Cache))
        throw new PXException("Inserting the generation options raised one or more errors. Please Review.");
      object[] objArray = new object[2];
      DateTime dateTime1 = ((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current.StartDate.Value;
      objArray[0] = (object) dateTime1.ToShortDateString();
      DateTime? nullable = ((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current.EndDate;
      dateTime1 = nullable.Value;
      objArray[1] = (object) dateTime1.ToShortDateString();
      if (1 == ((PXSelectBase<FSWeekCodeDate>) this.CalendarWeekCodeRecords).Ask("Confirm Calendar Week Code Generation", PXMessages.LocalizeFormatNoPrefix("Calendar Week Code will be automatically generated from {0} to {1}. Do you want to proceed?", objArray), (MessageButtons) 1))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CalendarWeekCodeMaint.\u003C\u003Ec__DisplayClass16_0 cDisplayClass160 = new CalendarWeekCodeMaint.\u003C\u003Ec__DisplayClass16_0();
        nullable = ((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current.DefaultStartDate;
        DateTime dateTime2 = nullable.Value;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass160.baseDate = dateTime2;
        nullable = ((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current.StartDate;
        DateTime dateTime3 = nullable.Value;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass160.iteratorDate = dateTime3;
        nullable = ((PXSelectBase<CalendarWeekCodeMaint.CalendarWeekCodeGeneration>) this.CalendarWeekCodeGenerationOptions).Current.EndDate;
        DateTime dateTime4 = nullable.Value;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass160.stopDate = dateTime4;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass160.graphCalendarWeekCodeMaint = PXGraph.CreateInstance<CalendarWeekCodeMaint>();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass160, __methodptr(\u003CGenerateWeekCode\u003Eb__0)));
      }
    }
    return adapter.Get();
  }

  protected virtual void _(
    Events.FieldUpdated<FSWeekCodeDate, FSWeekCodeDate.weekCodeDate> e)
  {
    if (e.Row == null)
      return;
    this.SetBeginEndWeekDates(e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<FSWeekCodeDate, FSWeekCodeDate.weekCode> e)
  {
    if (e.Row == null)
      return;
    this.SplitWeekCodeParameters(e.Row);
  }

  protected virtual void _(Events.RowSelecting<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowInserting<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSWeekCodeDate> e)
  {
  }

  protected virtual void _(
    Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.defaultStartDate> e)
  {
    if (e.Row == null)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.defaultStartDate>, CalendarWeekCodeMaint.CalendarWeekCodeGeneration, object>) e).NewValue = (object) new DateTime(2008, 12, 29);
  }

  protected virtual void _(
    Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate> e)
  {
    if (e.Row == null)
      return;
    FSWeekCodeDate fsWeekCodeDate = PXResultset<FSWeekCodeDate>.op_Implicit(PXSelectBase<FSWeekCodeDate, PXSelectOrderBy<FSWeekCodeDate, OrderBy<Desc<FSWeekCodeDate.weekCodeDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (fsWeekCodeDate != null)
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>, CalendarWeekCodeMaint.CalendarWeekCodeGeneration, object>) e).NewValue = (object) fsWeekCodeDate.WeekCodeDate.Value.AddDays(1.0);
    else
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>, CalendarWeekCodeMaint.CalendarWeekCodeGeneration, object>) e).NewValue = (object) new DateTime(2008, 12, 29);
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration row = e.Row;
    if (!row.StartDate.HasValue)
      return;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>>) e).Cache, ((Events.FieldDefaultingBase<Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>, CalendarWeekCodeMaint.CalendarWeekCodeGeneration, object>) e).NewValue);
    if (!handlingDateTime.HasValue)
      return;
    row.EndDate = new DateTime?(handlingDateTime.Value.AddYears(1));
  }

  protected virtual void _(
    Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate> e)
  {
    if (e.Row == null)
      return;
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration row = e.Row;
    DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate> fieldDefaulting = e;
    startDate = row.StartDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local = (ValueType) startDate.Value.AddYears(1);
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>, CalendarWeekCodeMaint.CalendarWeekCodeGeneration, object>) fieldDefaulting).NewValue = (object) local;
  }

  protected virtual void _(
    Events.FieldUpdated<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate> e)
  {
    if (e.Row == null)
      return;
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration row = e.Row;
    this.ValidateStartGenerationDate(row, ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>>) e).Cache);
    DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    CalendarWeekCodeMaint.CalendarWeekCodeGeneration weekCodeGeneration = row;
    startDate = row.StartDate;
    DateTime? nullable = new DateTime?(startDate.Value.AddYears(1));
    weekCodeGeneration.EndDate = nullable;
  }

  protected virtual void _(
    Events.FieldUpdated<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate> e)
  {
    if (e.Row == null)
      return;
    this.ValidateEndGenerationDate(e.Row, ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CalendarWeekCodeMaint.CalendarWeekCodeGeneration, CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>>) e).Cache);
  }

  protected virtual void _(
    Events.RowSelecting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowSelected<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowInserting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowInserted<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowUpdating<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowUpdated<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowDeleting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowDeleted<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowPersisting<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  protected virtual void _(
    Events.RowPersisted<CalendarWeekCodeMaint.CalendarWeekCodeGeneration> e)
  {
  }

  public class WeekCodeConstant
  {
    public const string A = "A";
    public const string B = "B";
    public const string C = "C";
    public const string D = "D";
    public const string E = "E";
    public const string F = "F";
    public const string S = "S";
    public const string T = "T";
    public const string U = "U";
    public const string V = "V";
    public const string W = "W";
    public const string X = "X";
    public const string Y = "Y";
    public const string Z = "Z";
  }

  [Serializable]
  public class CalendarWeekCodeGeneration : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    [PXUIField(DisplayName = "Default Start Date", Enabled = false)]
    public virtual DateTime? DefaultStartDate { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "From Date", Required = true)]
    public virtual DateTime? StartDate { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "To Date", Required = true)]
    public virtual DateTime? EndDate { get; set; }

    [PXString(4, IsUnicode = true, InputMask = ">CCCC")]
    [PXUIField(DisplayName = "Initial Week Code", Enabled = false)]
    [PXDefault("1ACS")]
    public virtual string InitialWeekCode { get; set; }

    public abstract class defaultStartDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CalendarWeekCodeMaint.CalendarWeekCodeGeneration.defaultStartDate>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CalendarWeekCodeMaint.CalendarWeekCodeGeneration.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CalendarWeekCodeMaint.CalendarWeekCodeGeneration.endDate>
    {
    }

    public abstract class initialWeekCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CalendarWeekCodeMaint.CalendarWeekCodeGeneration.initialWeekCode>
    {
    }
  }
}
