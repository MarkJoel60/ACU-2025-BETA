// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Exceptions.FinancialPeriodWithIdNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.GL.Exceptions;

public class FinancialPeriodWithIdNotFoundException : PXFinPeriodException
{
  /// <summary>
  /// Gets the financial period ID for which the financial period was not found.
  /// </summary>
  public string FinancialPeriodId { get; private set; }

  public FinancialPeriodWithIdNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <param name="financialPeriodId">
  /// The financial period ID in the internal representation.
  /// It will automatically be formatted for display in the error message.
  /// </param>
  public FinancialPeriodWithIdNotFoundException(string financialPeriodId)
  {
    string message;
    if (!string.IsNullOrEmpty(financialPeriodId))
      message = PXLocalizer.LocalizeFormat("The {0} financial period is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(financialPeriodId)
      });
    else
      message = "Financial Period cannot be found in the system.";
    // ISSUE: explicit constructor call
    base.\u002Ector(message);
    this.FinancialPeriodId = financialPeriodId;
  }
}
