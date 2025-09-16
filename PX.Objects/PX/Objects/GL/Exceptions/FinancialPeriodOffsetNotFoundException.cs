// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Exceptions.FinancialPeriodOffsetNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL.Exceptions;

public class FinancialPeriodOffsetNotFoundException : PXFinPeriodException
{
  /// <summary>
  /// Gets the financial period ID for which the offset period was not found.
  /// </summary>
  public string FinancialPeriodId { get; private set; }

  /// <summary>
  /// The positive or negative number of periods offset from the <see cref="P:PX.Objects.GL.Exceptions.FinancialPeriodOffsetNotFoundException.FinancialPeriodId" />,
  /// for which the financial period was not found.
  /// </summary>
  public int Offset { get; private set; }

  public FinancialPeriodOffsetNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <param name="financialPeriodId">
  /// The financial period ID in the internal representation.
  /// It will automatically be formatted for display in the error message.
  /// </param>
  public FinancialPeriodOffsetNotFoundException(string financialPeriodId, int offset)
    : base(FinancialPeriodOffsetNotFoundException.GetMessage(financialPeriodId, offset))
  {
    this.FinancialPeriodId = financialPeriodId;
    this.Offset = offset;
  }

  private static string GetMessage(string financialPeriodId, int offset)
  {
    if (string.IsNullOrEmpty(financialPeriodId))
      return "Financial Period cannot be found in the system.";
    switch (offset)
    {
      case -1:
        return PXLocalizer.LocalizeFormat("The financial period preceding {0} is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(financialPeriodId)
        });
      case 0:
        return PXLocalizer.LocalizeFormat("The {0} financial period is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(financialPeriodId)
        });
      case 1:
        return PXLocalizer.LocalizeFormat("The financial period after {0} is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(financialPeriodId)
        });
      default:
        return PXLocalizer.LocalizeFormat("The financial period that is {0} periods {1} {2} is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[3]
        {
          (object) Math.Abs(offset),
          offset > 0 ? (object) "after" : (object) "before",
          (object) PeriodIDAttribute.FormatForError(financialPeriodId)
        });
    }
  }
}
