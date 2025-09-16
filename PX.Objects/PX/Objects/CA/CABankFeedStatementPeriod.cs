// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedStatementPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedStatementPeriod
{
  public const string Month = "M";
  public const string Week = "W";
  public const string Day = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new (string, string)[3]
      {
        ("M", "Month"),
        ("W", "Week"),
        ("D", "Day")
      })
    {
    }
  }
}
