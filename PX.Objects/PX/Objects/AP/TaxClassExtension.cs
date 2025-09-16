// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.TaxClassExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AP;

public static class TaxClassExtension
{
  public static bool IsRegularInclusiveTax(this PX.Objects.TX.Tax tax)
  {
    return tax != null && tax.TaxCalcLevel == "0" && tax.TaxType != "W" && tax.TaxType != "Q" && !tax.ReverseTax.GetValueOrDefault();
  }
}
