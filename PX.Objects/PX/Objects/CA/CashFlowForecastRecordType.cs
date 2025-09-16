// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashFlowForecastRecordType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CashFlowForecastRecordType
{
  public enum RecordType
  {
    CashOutUnapplied = -2, // 0xFFFFFFFE
    CashOut = -1, // 0xFFFFFFFF
    Balance = 0,
    CashIn = 1,
    CashInUnapplied = 2,
  }

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[5]{ -1, -2, 0, 1, 2 }, new string[5]
      {
        "Cash Paid Out",
        "Non-Applied Cash Paid",
        "Cash On Hand",
        "Cash Receipts",
        "Non-Applied Receipts"
      })
    {
    }
  }
}
