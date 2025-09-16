// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.AddressValidationHelperExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class AddressValidationHelperExtension
{
  public static bool RequiresValidation(
    this IEnumerable<IAddressValidationHelper> addressLookupExtensions)
  {
    return addressLookupExtensions.Any<IAddressValidationHelper>((Func<IAddressValidationHelper, bool>) (_ => _.CurrentAddressRequiresValidation));
  }

  public static void ValidateAddresses(
    this IEnumerable<IAddressValidationHelper> addressExtensions)
  {
    foreach (IAddressValidationHelper addressExtension in addressExtensions)
      addressExtension.ValidateAddress();
  }
}
