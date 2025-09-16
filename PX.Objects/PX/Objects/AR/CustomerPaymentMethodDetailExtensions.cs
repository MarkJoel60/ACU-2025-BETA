// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerPaymentMethodDetailExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public static class CustomerPaymentMethodDetailExtensions
{
  public static CustomerPaymentMethodDetail CreateCopy(
    this CustomerPaymentMethodDetail cpmd,
    PXCache cache)
  {
    CustomerPaymentMethodDetail copy = cache.CreateCopy((object) cpmd) as CustomerPaymentMethodDetail;
    cache.CreateCopy((object) cpmd).GetType();
    return copy;
  }
}
