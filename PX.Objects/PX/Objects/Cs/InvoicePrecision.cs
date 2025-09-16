// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.InvoicePrecision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public static class InvoicePrecision
{
  public const string m005 = "0.05";
  public const string m01 = "0.1";
  public const string m05 = "0.5";
  public const string m1 = "1.0";
  public const string m10 = "10";
  public const string m100 = "100";

  public class ListAttribute : PXDecimalListAttribute
  {
    public ListAttribute()
      : base(new string[6]
      {
        "0.05",
        "0.1",
        "0.5",
        "1.0",
        "10",
        "100"
      }, new string[6]
      {
        "0.05",
        "0.1",
        "0.5",
        "1.0",
        "10",
        "100"
      })
    {
    }
  }
}
