// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.TransferCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Specialized for the Transfer version of the CashTranID attribute<br />
/// Defines methods to create new CATran from (and for) CATransfer document<br />
/// Should be used on the CATransfer - derived types only.
/// <example>
/// [TransferCashTranID()]
/// </example>
/// </summary>
public class TransferCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is TransferCashTranIDAttribute)
      {
        ((TransferCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
        return ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
      }
    }
    return (CATran) null;
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    CATransfer caTransfer = (CATransfer) orig_Row;
    if (caTransfer.Released.GetValueOrDefault() && catran_Row.TranID.HasValue)
      return (CATran) null;
    if (catran_Row.TranID.HasValue)
    {
      long? tranId = catran_Row.TranID;
      long num = 0;
      if (!(tranId.GetValueOrDefault() < num & tranId.HasValue))
        goto label_5;
    }
    catran_Row.OrigModule = "CA";
    catran_Row.OrigRefNbr = caTransfer.TransferNbr;
label_5:
    if (object.Equals((object) this._FieldOrdinal, (object) sender.GetFieldOrdinal<CATransfer.tranIDOut>()))
    {
      catran_Row.CashAccountID = caTransfer.OutAccountID;
      catran_Row.OrigTranType = "CTO";
      catran_Row.ExtRefNbr = caTransfer.OutExtRefNbr;
      catran_Row.CuryID = caTransfer.OutCuryID;
      catran_Row.CuryInfoID = caTransfer.OutCuryInfoID;
      CATran caTran1 = catran_Row;
      Decimal? curyTranOut = caTransfer.CuryTranOut;
      Decimal? nullable1 = curyTranOut.HasValue ? new Decimal?(-curyTranOut.GetValueOrDefault()) : new Decimal?();
      caTran1.CuryTranAmt = nullable1;
      CATran caTran2 = catran_Row;
      Decimal? tranOut = caTransfer.TranOut;
      Decimal? nullable2 = tranOut.HasValue ? new Decimal?(-tranOut.GetValueOrDefault()) : new Decimal?();
      caTran2.TranAmt = nullable2;
      catran_Row.DrCr = "C";
      catran_Row.Cleared = caTransfer.ClearedOut;
      catran_Row.ClearDate = caTransfer.ClearDateOut;
      catran_Row.TranDate = caTransfer.OutDate;
      CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, caTransfer.OutTranPeriodID);
    }
    else
    {
      if (!object.Equals((object) this._FieldOrdinal, (object) sender.GetFieldOrdinal<CATransfer.tranIDIn>()))
        throw new PXException("The document cannot be processed because the document type is unknown.");
      catran_Row.CashAccountID = caTransfer.InAccountID;
      catran_Row.OrigTranType = "CTI";
      catran_Row.ExtRefNbr = caTransfer.InExtRefNbr;
      catran_Row.CuryID = caTransfer.InCuryID;
      catran_Row.CuryInfoID = caTransfer.InCuryInfoID;
      catran_Row.CuryTranAmt = caTransfer.CuryTranIn;
      catran_Row.TranAmt = caTransfer.TranIn;
      catran_Row.DrCr = "D";
      catran_Row.Cleared = caTransfer.ClearedIn;
      catran_Row.ClearDate = caTransfer.ClearDateIn;
      catran_Row.TranDate = caTransfer.InDate;
      CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, caTransfer.InTranPeriodID);
    }
    CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
    catran_Row.TranDesc = caTransfer.Descr;
    catran_Row.ReferenceID = new int?();
    catran_Row.Released = caTransfer.Released;
    catran_Row.Hold = caTransfer.Hold;
    return catran_Row;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisting(sender, e);
  }

  public override void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisted(sender, e);
  }
}
