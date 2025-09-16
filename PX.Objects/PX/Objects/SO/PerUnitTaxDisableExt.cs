// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PerUnitTaxDisableExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.PerUnitTax;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// A per-unit tax graph extension for <see cref="T:PX.Objects.SO.SOOrderEntry" /> which will forbid edit of per-unit taxes in UI.
/// </summary>
public class PerUnitTaxDisableExt : PerUnitTaxDataEntryGraphExtension<SOOrderEntry, SOTaxTran>
{
  public static bool IsActive()
  {
    return PerUnitTaxDataEntryGraphExtension<SOOrderEntry, SOTaxTran>.IsActiveBase();
  }
}
