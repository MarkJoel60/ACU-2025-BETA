// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorPriceUpdateType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APVendorPriceUpdateType
{
  public const string None = "N";
  public const string Purchase = "P";
  public const string ReleaseAPBill = "B";

  public class List : PXStringListAttribute
  {
    public List()
      : base(PXStringListAttribute.Pair("N", "None"), PXStringListAttribute.Pair("P", "On PO Entry"), PXStringListAttribute.Pair("B", "On AP Bill Release"))
    {
    }
  }
}
