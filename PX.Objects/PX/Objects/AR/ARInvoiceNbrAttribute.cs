// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Specialized for ARInvoice version of the InvoiceNbrAttribute.<br />
/// The main purpose of the attribute is poviding of the uniqueness of the RefNbr <br />
/// amoung  a set of  documents of the specifyed types (for example, each RefNbr of the ARInvoice <br />
/// the ARInvoices must be unique across all ARInvoices and AR Debit memos)<br />
/// This may be useful, if user has configured a manual numberin for ARInvoices  <br />
/// or needs  to create ARInvoice from another document (like SOOrder) allowing to type RefNbr <br />
/// for the to-be-created Invoice manually. To store the numbers, system using ARInvoiceNbr table, <br />
/// keyed uniquelly by DocType and RefNbr. A source document is linked to a number by NoteID.<br />
/// Attributes checks a number for uniqueness on FieldVerifying and RowPersisting events.<br />
/// </summary>
public class ARInvoiceNbrAttribute : InvoiceNbrAttribute
{
  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  protected override Guid? GetNoteID(PXCache sender, PXRowPersistedEventArgs e)
  {
    return (Guid?) sender.GetValue<ARInvoice.refNoteID>(e.Row) ?? base.GetNoteID(sender, e);
  }

  protected override bool DeleteOnUpdate(PXCache sender, PXRowPersistedEventArgs e)
  {
    Guid? nullable = (Guid?) sender.GetValue<ARInvoice.refNoteID>(e.Row);
    return ((e.Operation & 3) != 3 || !nullable.HasValue) && base.DeleteOnUpdate(sender, e);
  }

  public override void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    try
    {
      base.RowPersisted(sender, e);
    }
    catch (PXRowPersistedException ex)
    {
      if (e.Operation == 1 && e.TranStatus == null)
        return;
      throw;
    }
  }

  public ARInvoiceNbrAttribute()
    : base(typeof (ARInvoice.docType), typeof (ARInvoice.noteID))
  {
  }
}
