// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryPerUnitTaxDisableExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Extensions.PerUnitTax;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// A per-unit tax graph extension for <see cref="T:PX.Objects.AP.APInvoiceEntry" /> which will forbid edit of per-unit taxes in UI.
/// </summary>
public class APInvoiceEntryPerUnitTaxDisableExt : 
  PerUnitTaxDataEntryGraphExtension<APInvoiceEntry, APTaxTran>
{
  public static bool IsActive()
  {
    return PerUnitTaxDataEntryGraphExtension<APInvoiceEntry, APTaxTran>.IsActiveBase();
  }
}
