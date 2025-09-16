// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.WorkTimeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CS.Services.WorkTimeCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.CS;

[PXInt]
[PXDBInt]
[PXDefault("Disabled")]
public class WorkTimeAttribute : 
  PXEntityAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXDependsOnFields
{
  public Type? AvailabilityField { get; set; }

  public IBqlSearch SearchCalendarId { get; }

  public BqlCommand SearchCalendarIdBqlCommand { get; }

  protected virtual string DisplayFormat => "{0:000}{1:00}{2:00}";

  protected virtual string DisabledText => PXMessages.LocalizeNoPrefix("Disabled");

  protected virtual string InputMask => PXMessages.LocalizeNoPrefix("###d ##h ##m");

  protected virtual int InputMaskLength
  {
    get
    {
      return this.InputMask.Count<char>((Func<char, bool>) (c => EnumerableExtensions.IsIn<char>(c, '#', '0')));
    }
  }

  protected virtual Regex ParsePattern { get; } = new Regex("(?<days>[\\d| ]{1,3})(?<hours>[\\d| ]{0,2})(?<minutes>[\\d| ]{0,2})", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

  public WorkTimeAttribute(Type calendarIdSearch)
  {
    this.SearchCalendarId = this.GetCalendarIdSearchCommand(calendarIdSearch);
    this.SearchCalendarIdBqlCommand = (BqlCommand) this.SearchCalendarId;
  }

  protected virtual IBqlSearch GetCalendarIdSearchCommand(Type calendarIdSearch)
  {
    if ((object) calendarIdSearch == null)
      throw new ArgumentNullException(nameof (calendarIdSearch));
    if (typeof (IBqlSearch).IsAssignableFrom(calendarIdSearch))
      return (IBqlSearch) BqlCommand.CreateInstance(new Type[1]
      {
        calendarIdSearch
      });
    return calendarIdSearch.IsNested && typeof (IBqlField).IsAssignableFrom(calendarIdSearch) ? (IBqlSearch) BqlCommand.CreateInstance(new Type[2]
    {
      typeof (Search<>),
      calendarIdSearch
    }) : throw new PXArgumentException(nameof (calendarIdSearch), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
    {
      (object) calendarIdSearch
    });
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    WorkTimeInfo? workTimeInfo;
    if (this.IsAvailable(sender, e.Row))
    {
      if (e.ReturnValue is int returnValue)
      {
        IWorkTimeCalculator workTimeCalculator = this.TryGetWorkTimeCalculator(sender.Graph, e.Row);
        if (workTimeCalculator != null && workTimeCalculator.IsValid)
        {
          TimeSpan timeSpan = TimeSpan.FromMinutes((double) returnValue);
          workTimeInfo = new WorkTimeInfo?(WorkTimeInfo.FromWorkTimeSpan(workTimeCalculator.ToWorkTimeSpan(timeSpan)));
          goto label_6;
        }
      }
      workTimeInfo = new WorkTimeInfo?(WorkTimeInfo.Empty);
    }
    else
      workTimeInfo = new WorkTimeInfo?();
label_6:
    e.ReturnState = (object) this.GetFieldState(workTimeInfo);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PXFieldUpdatingEventArgs updatingEventArgs = e;
    int num1;
    switch (e.NewValue)
    {
      case int num2:
        num1 = num2;
        break;
      case string str:
        WorkTimeInfo? workTimeInfo = this.TryParseWorkTimeInfo(str);
        if (workTimeInfo.HasValue)
        {
          WorkTimeInfo valueOrDefault = workTimeInfo.GetValueOrDefault();
          IWorkTimeCalculator workTimeCalculator = this.TryGetWorkTimeCalculator(sender.Graph, e.Row);
          if (workTimeCalculator != null && workTimeCalculator.IsValid)
          {
            num1 = (int) workTimeCalculator.ToWorkTimeSpan(valueOrDefault).TotalMinutes;
            break;
          }
          goto default;
        }
        goto default;
      default:
        num1 = 0;
        break;
    }
    // ISSUE: variable of a boxed type
    __Boxed<int> local = (ValueType) num1;
    updatingEventArgs.NewValue = (object) local;
  }

  protected virtual WorkTimeInfo? TryParseWorkTimeInfo(string value)
  {
    Match match = this.ParsePattern.Match(value);
    if (match == null || !match.Success)
      return new WorkTimeInfo?();
    string s1 = match.Groups["days"].Value.Replace(' ', '0');
    string s2 = match.Groups["hours"].Value.Replace(' ', '0');
    string s3 = match.Groups["minutes"].Value.Replace(' ', '0');
    return new WorkTimeInfo?(new WorkTimeInfo(string.IsNullOrEmpty(s1) ? 0 : int.Parse(s1), string.IsNullOrEmpty(s2) ? 0 : int.Parse(s2), string.IsNullOrEmpty(s3) ? 0 : int.Parse(s3)));
  }

  protected virtual IWorkTimeCalculator? TryGetWorkTimeCalculator(PXGraph graph, object? row)
  {
    string calendarId = this.TryGetCalendarId(graph, row);
    return calendarId != null ? WorkTimeCalculatorProvider.GetWorkTimeCalculator(calendarId) : (IWorkTimeCalculator) null;
  }

  protected virtual string? TryGetCalendarId(PXGraph graph, object? row)
  {
    if (row == null)
      return (string) null;
    object obj = graph.TypedViews.GetView(this.SearchCalendarIdBqlCommand, false).SelectSingleBound(new object[1]
    {
      row
    }, Array.Empty<object>());
    if (obj == null)
      return (string) null;
    Type itemType = BqlCommand.GetItemType(this.SearchCalendarId.GetField());
    if (itemType == (Type) null)
      return (string) null;
    IBqlTable ibqlTable = PXResult.Unwrap(obj, itemType);
    if (ibqlTable == null)
      return (string) null;
    PXCache cach = graph.Caches[itemType];
    string field = cach.GetField(this.SearchCalendarId.GetField());
    return cach.GetValue((object) ibqlTable, field) as string;
  }

  protected virtual bool IsAvailable(PXCache sender, object? row)
  {
    if (sender.Graph is PXGenericInqGrph || (object) this.AvailabilityField == null)
      return true;
    return row != null && sender.GetValue(row, this.AvailabilityField) is bool flag && flag;
  }

  protected virtual PXStringState GetFieldState(WorkTimeInfo? workTimeInfo)
  {
    PXStringState instance = (PXStringState) PXStringState.CreateInstance((object) null, new int?(this.InputMaskLength), new bool?(), this.FieldName, new bool?(this.IsKey), new int?(), this.InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    if (workTimeInfo.HasValue)
    {
      WorkTimeInfo valueOrDefault = workTimeInfo.GetValueOrDefault();
      ((PXFieldState) instance).Value = (object) string.Format(this.DisplayFormat, (object) valueOrDefault.Workdays, (object) valueOrDefault.Hours, (object) valueOrDefault.Minutes);
    }
    else
    {
      ((PXFieldState) instance).Enabled = false;
      instance.InputMask = string.Empty;
      ((PXFieldState) instance).Length = this.DisabledText.Length;
      ((PXFieldState) instance).Value = (object) this.DisabledText;
    }
    return instance;
  }

  public ISet<Type> GetDependencies(PXCache cache)
  {
    HashSet<Type> dependencies = new HashSet<Type>();
    if ((object) this.AvailabilityField != null)
      dependencies.Add(this.AvailabilityField);
    return (ISet<Type>) dependencies;
  }
}
