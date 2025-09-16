// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Exceptions.FinancialPeriodNotDefinedForDateException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL.Exceptions;

public class FinancialPeriodNotDefinedForDateException : PXFinPeriodException
{
  /// <summary>
  /// Gets the date for which the financial period was not found.
  /// </summary>
  public DateTime? Date { get; private set; }

  public FinancialPeriodNotDefinedForDateException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public FinancialPeriodNotDefinedForDateException(DateTime? date)
  {
    string message;
    if (date.HasValue)
      message = PXLocalizer.LocalizeFormat("The financial period for the {0} date is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[1]
      {
        (object) date?.ToShortDateString()
      });
    else
      message = "Financial Period cannot be found in the system.";
    // ISSUE: explicit constructor call
    base.\u002Ector(message);
    this.Date = date;
  }
}
