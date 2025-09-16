// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Attributes.ManualDiscountModeVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Discount.Mappers;
using System;

#nullable disable
namespace PX.Objects.Common.Discount.Attributes;

public class ManualDiscountModeVATRecognitionOnPrepayments(
  Type curyDiscAmt,
  Type curyTranAmt,
  Type discPct,
  Type freezeManualDisc,
  DiscountFeatureType discountType) : ManualDiscountMode(curyDiscAmt, curyTranAmt, discPct, freezeManualDisc, discountType)
{
  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran) || sender.GetValue<ARTran.tranType>(e.Row) as string != "PPI")
    {
      base.RowUpdated(sender, e);
    }
    else
    {
      AmountLineFields discountDocumentLine1 = ManualDiscountMode.GetDiscountDocumentLine(sender, e.Row);
      if (discountDocumentLine1.FreezeManualDisc.GetValueOrDefault())
      {
        discountDocumentLine1.FreezeManualDisc = new bool?(false);
      }
      else
      {
        DiscountLineFields discountedLine1 = ManualDiscountMode.GetDiscountedLine(sender, e.Row);
        DiscountLineFields discountedLine2 = ManualDiscountMode.GetDiscountedLine(sender, e.OldRow);
        if (discountedLine1.LineType == "DS")
          return;
        Decimal? nullable1;
        Decimal? nullable2;
        if (discountedLine1.DiscountID != discountedLine2.DiscountID && discountedLine1.DiscountID != null && discountedLine1.DiscountSequenceID != null)
        {
          nullable1 = discountedLine1.DiscPct;
          Decimal? discPct = discountedLine2.DiscPct;
          if (!(nullable1.GetValueOrDefault() == discPct.GetValueOrDefault() & nullable1.HasValue == discPct.HasValue))
          {
            nullable2 = discountedLine1.DiscPct;
            if (nullable2.HasValue)
            {
              nullable2 = discountedLine1.CuryDiscAmt;
              nullable1 = discountedLine2.CuryDiscAmt;
              if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              {
                nullable1 = discountedLine1.CuryDiscAmt;
                if (nullable1.HasValue)
                {
                  nullable1 = discountedLine1.CuryDiscAmt;
                  Decimal num = 0M;
                  if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue) && discountedLine1.ManualDisc)
                    return;
                }
              }
            }
          }
        }
        if (!discountedLine1.ManualDisc && discountedLine2.ManualDisc)
        {
          sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.discPct)), (object) 0M);
          sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.curyDiscAmt)), (object) 0M);
        }
        else
        {
          nullable1 = discountDocumentLine1.CuryExtPrice;
          if (nullable1.GetValueOrDefault() == 0M)
          {
            sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.curyDiscAmt)), (object) 0M);
          }
          else
          {
            LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<object>(e.Row, sender);
            AmountLineFields discountDocumentLine2 = ManualDiscountMode.GetDiscountDocumentLine(sender, e.OldRow);
            bool flag = false;
            nullable1 = discountedLine1.CuryDiscAmt;
            nullable2 = discountedLine2.CuryDiscAmt;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            {
              nullable2 = discountedLine1.CuryDiscAmt;
              Decimal num1 = Math.Abs(nullable2.GetValueOrDefault());
              nullable2 = discountDocumentLine1.CuryExtPrice;
              Decimal num2 = Math.Abs(nullable2.Value);
              if (num1 > num2)
              {
                sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.curyDiscAmt)), (object) discountDocumentLine1.CuryExtPrice);
                PXUIFieldAttribute.SetWarning<DiscountLineFields.curyDiscAmt>(sender, e.Row, PXMessages.LocalizeFormatNoPrefix("The discount amount cannot be greater than the amount in the {0} column.", new object[1]
                {
                  (object) discountDocumentLine1.ExtPriceDisplayName
                }));
              }
              Decimal? nullable3 = this.CalcDiscountPercent(discountDocumentLine1, discountedLine1);
              sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.discPct)), (object) nullable3);
              flag = true;
            }
            else
            {
              nullable2 = discountedLine1.DiscPct;
              nullable1 = discountedLine2.DiscPct;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              {
                nullable1 = discountDocumentLine2.CuryExtPrice;
                nullable2 = discountDocumentLine1.CuryExtPrice;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                  goto label_24;
              }
              Decimal num = this.CalcDiscountAmount(sender, ManualDiscountMode.GetLineDiscountTarget(sender, mapFor), discountDocumentLine1, discountedLine1);
              sender.SetValueExt(e.Row, sender.GetField(typeof (DiscountLineFields.curyDiscAmt)), (object) num);
              flag = true;
            }
label_24:
            if (!flag && !sender.Graph.IsCopyPasteContext)
              return;
            sender.SetValue(e.Row, this.FieldName, (object) true);
          }
        }
      }
    }
  }
}
