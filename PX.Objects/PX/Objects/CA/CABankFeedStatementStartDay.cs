// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedStatementStartDay
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedStatementStartDay : PXIntListAttribute
{
  private static int[] days = Enumerable.Range(1, 31 /*0x1F*/).ToArray<int>();
  private static string[] dayLabels = Enumerable.Range(1, 31 /*0x1F*/).Select<int, string>((Func<int, string>) (i => i.ToString())).ToArray<string>();
  private static int[] daysOfWeek = Enumerable.Range(1, 7).ToArray<int>();
  private static string[] dayOfWeekLabels = new string[7]
  {
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday"
  };
  private Type statementPeriodField;

  public CABankFeedStatementStartDay(Type periodField) => this.statementPeriodField = periodField;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string str = sender.GetValue(e.Row, this.statementPeriodField.Name) as string;
    int[] numArray = new int[0];
    string[] strArray = new string[0];
    switch (str)
    {
      case "M":
        numArray = CABankFeedStatementStartDay.days;
        strArray = CABankFeedStatementStartDay.dayLabels;
        break;
      case "W":
        numArray = CABankFeedStatementStartDay.daysOfWeek;
        strArray = CABankFeedStatementStartDay.dayOfWeekLabels;
        break;
      case "D":
        numArray = new int[1]{ 1 };
        strArray = new string[1]{ "1" };
        break;
    }
    this._AllowedValues = numArray;
    this._AllowedLabels = strArray;
    base.FieldSelecting(sender, e);
  }
}
