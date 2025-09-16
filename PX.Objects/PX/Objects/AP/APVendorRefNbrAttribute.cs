// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorRefNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.EP;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class APVendorRefNbrAttribute : BaseVendorRefNbrAttribute
{
  public APVendorRefNbrAttribute()
    : base(typeof (APInvoice.vendorID))
  {
  }

  protected override bool IsIgnored(PXCache sender, object row)
  {
    APInvoice apInvoice = (APInvoice) row;
    int num;
    if (apInvoice.OrigRefNbr != null)
      num = PXSelectBase<EPExpenseClaim, PXViewOf<EPExpenseClaim>.BasedOn<SelectFromBase<EPExpenseClaim, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<EPExpenseClaim.refNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(sender.Graph, (object) apInvoice.OrigRefNbr).AsEnumerable<PXResult<EPExpenseClaim>>().Any<PXResult<EPExpenseClaim>>() ? 1 : 0;
    else
      num = 0;
    return num != 0 || base.IsIgnored(sender, row);
  }

  protected override bool IsRequiredUniqueRefNbr(PXCache sender, object row)
  {
    return !((APInvoice) row).InstallmentNbr.HasValue && base.IsRequiredUniqueRefNbr(sender, row);
  }

  protected override BaseVendorRefNbrAttribute.EntityKey GetEntityKey(PXCache sender, object row)
  {
    return new BaseVendorRefNbrAttribute.EntityKey()
    {
      _DetailID = this.DETAIL_DUMMY,
      _MasterID = this.GetMasterNoteId(typeof (APInvoice), typeof (APInvoice.noteID), row)
    };
  }

  public override Guid? GetSiblingID(PXCache sender, object row)
  {
    APInvoice apInvoice = (APInvoice) row;
    if (apInvoice == null)
      return new Guid?();
    if (apInvoice.RefNoteID.HasValue)
      return apInvoice.RefNoteID;
    if (apInvoice == null || apInvoice.OrigDocType == null || apInvoice.OrigRefNbr == null)
      return apInvoice.NoteID;
    APInvoice row1 = (APInvoice) PXSelectBase<APInvoice, PXSelectReadonly<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.SelectSingleBound(this._Graph, (object[]) null, (object) apInvoice.OrigDocType, (object) apInvoice.OrigRefNbr);
    return row1 == null ? apInvoice.NoteID : this.GetSiblingID(sender, (object) row1);
  }
}
