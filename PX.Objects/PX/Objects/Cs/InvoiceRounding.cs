// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.InvoiceRounding
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public static class InvoiceRounding
{
  public const string UseCurrencyPrecision = "Use Currency Precision";
  public const string Nearest = "Nearest";
  public const string Up = "Up";
  public const string Down = "Down";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "N", "R", "C", "F" }, new string[4]
      {
        "Use Currency Precision",
        "Nearest",
        "Up",
        "Down"
      })
    {
    }
  }
}
