// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Specialized for SOInvoice version of the InvoiceNbrAttribute.<br />
/// The main purpose of the attribute is poviding of the uniqueness of the RefNbr <br />
/// amoung  a set of  documents of the specifyed types (for example, each RefNbr of the ARInvoice <br />
/// the ARInvoices must be unique across all ARInvoices and AR Debit memos)<br />
/// This may be useful, if user has configured a manual numberin for SOInvoices  <br />
/// or needs  to create SOInvoice from another document (like SOOrder) allowing to type RefNbr <br />
/// for the to-be-created Invoice manually. To store the numbers, system using ARInvoiceNbr table, <br />
/// keyed uniquelly by DocType and RefNbr. A source document is linked to a number by NoteID.<br />
/// Attributes checks a number for uniqueness on FieldVerifying and RowPersisting events.<br />
/// </summary>
public class SOInvoiceNbrAttribute : InvoiceNbrAttribute
{
  public SOInvoiceNbrAttribute()
    : base(typeof (SOOrder.aRDocType), typeof (SOOrder.noteID))
  {
  }

  protected override bool DeleteOnUpdate(PXCache sender, PXRowPersistedEventArgs e)
  {
    return base.DeleteOnUpdate(sender, e) || ((bool?) sender.GetValue<SOOrder.cancelled>(e.Row)).GetValueOrDefault();
  }
}
