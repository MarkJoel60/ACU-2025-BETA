// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PerUnitTaxDisableExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.PerUnitTax;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// A per-unit tax graph extension for <see cref="T:PX.Objects.PO.POOrderEntry" /> which will forbid edit of per-unit taxes in UI.
/// </summary>
public class PerUnitTaxDisableExt : PerUnitTaxDataEntryGraphExtension<POOrderEntry, POTaxTran>
{
  public static bool IsActive()
  {
    return PerUnitTaxDataEntryGraphExtension<POOrderEntry, POTaxTran>.IsActiveBase();
  }
}
