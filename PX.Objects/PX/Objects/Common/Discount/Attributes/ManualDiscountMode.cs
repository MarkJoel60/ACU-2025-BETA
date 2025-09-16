// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.Attributes.ManualDiscountMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.Common.Discount.Attributes;

/// <summary>
/// Sets ManualDisc flag based on the values of the depending fields. Manual Flag is set when user
/// overrides the discount values.
/// This attribute also updates the relative fields. Ex: Updates Discount Amount when Discount Pct is modified.
/// </summary>
public class ManualDiscountMode : 
  PXEventSubscriberAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowInsertedSubscriber
{
  private 
  #nullable disable
  Type curyDiscAmtT;
  private Type curyTranAmtT;
  private Type freezeDiscT;
  private Type discPctT;
  private DiscountFeatureType discountFeatureTypeE;

  public ManualDiscountMode(
    Type curyDiscAmt,
    Type curyTranAmt,
    Type discPct,
    Type freezeManualDisc,
    DiscountFeatureType discountType)
    : this(curyDiscAmt, curyTranAmt, discPct, freezeManualDisc)
  {
    this.discountFeatureTypeE = discountType;
  }

  public ManualDiscountMode(
    Type curyDiscAmt,
    Type curyTranAmt,
    Type discPct,
    Type freezeManualDisc)
    : this(curyDiscAmt, curyTranAmt, discPct)
  {
    if (curyDiscAmt == (Type) null)
      throw new ArgumentNullException();
    if (curyTranAmt == (Type) null)
      throw new ArgumentNullException();
    this.freezeDiscT = freezeManualDisc;
    this.curyTranAmtT = curyTranAmt;
  }

  public ManualDiscountMode(
    Type curyDiscAmt,
    Type curyTranAmt,
    Type discPct,
    DiscountFeatureType discountType)
    : this(curyDiscAmt, curyTranAmt, discPct)
  {
    this.discountFeatureTypeE = discountType;
  }

  public ManualDiscountMode(Type curyDiscAmt, Type curyTranAmt, Type discPct)
    : this(curyDiscAmt, discPct)
  {
    if (curyDiscAmt == (Type) null)
      throw new ArgumentNullException();
    this.curyTranAmtT = !(curyTranAmt == (Type) null) ? curyTranAmt : throw new ArgumentNullException();
  }

  public ManualDiscountMode(Type curyDiscAmt, Type discPct, DiscountFeatureType discountType)
    : this(curyDiscAmt, discPct)
  {
    this.discountFeatureTypeE = discountType;
  }

  public ManualDiscountMode(Type curyDiscAmt, Type discPct)
  {
    if (curyDiscAmt == (Type) null)
      throw new ArgumentNullException(nameof (curyDiscAmt));
    if (discPct == (Type) null)
      throw new ArgumentNullException(nameof (discPct));
    this.curyDiscAmtT = curyDiscAmt;
    this.discPctT = discPct;
  }

  public static AmountLineFields GetDiscountDocumentLine(PXCache sender, object line)
  {
    return AmountLineFields.GetMapFor<IBqlTable>((IBqlTable) line, sender, false);
  }

  public static DiscountLineFields GetDiscountedLine(PXCache sender, object line)
  {
    return DiscountLineFields.GetMapFor<IBqlTable>((IBqlTable) line, sender);
  }

  public static string GetLineDiscountTarget(PXCache sender, LineEntitiesFields efields)
  {
    string lineDiscountTarget = "E";
    if (efields != null)
    {
      int? nullable = efields.VendorID;
      if (nullable.HasValue)
      {
        nullable = efields.CustomerID;
        if (!nullable.HasValue)
        {
          PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
          {
            (object) efields.VendorID
          }));
          if (vendor != null)
          {
            lineDiscountTarget = vendor.LineDiscountTarget;
            goto label_7;
          }
          goto label_7;
        }
      }
    }
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    if (arSetup != null)
      lineDiscountTarget = arSetup.LineDiscountTarget;
label_7:
    return lineDiscountTarget;
  }

  private void UpdateLineAmt<TField>(AmountLineFields aLine, Decimal? oldValue, Decimal? newValue) where TField : IBqlField
  {
    object newValue1 = (object) newValue;
    aLine.RaiseFieldUpdating<TField>(ref newValue1);
    aLine.RaiseFieldVerifying<TField>(ref newValue1);
    aLine.CuryLineAmount = (Decimal?) newValue1;
    aLine.RaiseFieldUpdated<TField>((object) oldValue);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    AmountLineFields discountDocumentLine1 = ManualDiscountMode.GetDiscountDocumentLine(sender, e.Row);
    if (discountDocumentLine1.FreezeManualDisc.GetValueOrDefault())
    {
      discountDocumentLine1.FreezeManualDisc = new bool?(false);
    }
    else
    {
      DiscountLineFields discountedLine1 = ManualDiscountMode.GetDiscountedLine(sender, e.Row);
      if (discountedLine1.LineType == "DS")
        return;
      AmountLineFields discountDocumentLine2 = ManualDiscountMode.GetDiscountDocumentLine(sender, e.OldRow);
      DiscountLineFields discountedLine2 = ManualDiscountMode.GetDiscountedLine(sender, e.OldRow);
      LineEntitiesFields mapFor1 = LineEntitiesFields.GetMapFor<object>(e.Row, sender);
      LineEntitiesFields mapFor2 = LineEntitiesFields.GetMapFor<object>(e.OldRow, sender);
      if (discountedLine1.DiscountID != discountedLine2.DiscountID && discountedLine1.DiscountID != null && discountedLine1.DiscountSequenceID != null)
      {
        Decimal? discPct1 = discountedLine1.DiscPct;
        Decimal? discPct2 = discountedLine2.DiscPct;
        if (!(discPct1.GetValueOrDefault() == discPct2.GetValueOrDefault() & discPct1.HasValue == discPct2.HasValue) && discountedLine1.DiscPct.HasValue)
        {
          Decimal? curyDiscAmt1 = discountedLine1.CuryDiscAmt;
          Decimal? curyDiscAmt2 = discountedLine2.CuryDiscAmt;
          if (!(curyDiscAmt1.GetValueOrDefault() == curyDiscAmt2.GetValueOrDefault() & curyDiscAmt1.HasValue == curyDiscAmt2.HasValue) && discountedLine1.CuryDiscAmt.HasValue)
          {
            Decimal? curyDiscAmt3 = discountedLine1.CuryDiscAmt;
            Decimal num = 0M;
            if (!(curyDiscAmt3.GetValueOrDefault() == num & curyDiscAmt3.HasValue) && discountedLine1.ManualDisc)
              return;
          }
        }
      }
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = true;
      bool flag5 = false;
      if (!discountedLine1.ManualDisc && discountedLine2.ManualDisc)
      {
        flag1 = false;
        flag5 = true;
      }
      if (discountedLine1.ManualDisc || sender.Graph.IsCopyPasteContext)
        flag1 = true;
      if (!discountedLine1.ManualDisc && discountedLine1.AutomaticDiscountsDisabled.GetValueOrDefault() && !flag5)
        flag2 = true;
      Decimal? quantity1 = discountDocumentLine1.Quantity;
      Decimal? quantity2 = discountDocumentLine2.Quantity;
      int num1;
      if (!(quantity1.GetValueOrDefault() == quantity2.GetValueOrDefault() & quantity1.HasValue == quantity2.HasValue))
      {
        Decimal? quantity3 = discountDocumentLine1.Quantity;
        Decimal num2 = 0M;
        num1 = quantity3.GetValueOrDefault() == num2 & quantity3.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag6 = num1 != 0;
      Decimal? nullable1 = discountedLine1.DiscPct;
      Decimal? nullable2 = discountedLine2.DiscPct;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        int? inventoryId1 = mapFor1.InventoryID;
        int? inventoryId2 = mapFor2.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue && !flag6)
        {
          flag1 = true;
          flag3 = true;
        }
      }
      nullable2 = discountedLine1.DiscPct;
      nullable1 = discountedLine2.DiscPct;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = discountedLine1.CuryDiscAmt;
        nullable2 = discountedLine2.CuryDiscAmt;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = discountDocumentLine1.Quantity;
          nullable1 = discountDocumentLine2.Quantity;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = discountDocumentLine1.CuryUnitPrice;
            nullable2 = discountDocumentLine2.CuryUnitPrice;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = discountDocumentLine1.CuryExtPrice;
              nullable1 = discountDocumentLine2.CuryExtPrice;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                goto label_27;
            }
          }
          flag3 = true;
        }
      }
label_27:
      if (sender.Graph.IsContractBasedAPI)
      {
        nullable1 = discountedLine1.CuryDiscAmt;
        nullable2 = discountedLine2.CuryDiscAmt;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          nullable2 = discountedLine1.DiscPct;
          nullable1 = discountedLine2.DiscPct;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !discountedLine1.ManualDisc)
          {
            flag1 = true;
            flag3 = false;
          }
        }
      }
      nullable1 = discountedLine1.CuryDiscAmt;
      nullable2 = discountedLine2.CuryDiscAmt;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        nullable2 = discountDocumentLine1.Quantity;
        nullable1 = discountDocumentLine2.Quantity;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = discountDocumentLine1.CuryUnitPrice;
          nullable2 = discountDocumentLine2.CuryUnitPrice;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            flag1 = true;
            flag3 = false;
          }
        }
      }
      if (e.ExternalCall)
      {
        nullable2 = discountedLine1.CuryDiscAmt;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        nullable2 = discountedLine2.CuryDiscAmt;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        if (!(Math.Abs(valueOrDefault1 - valueOrDefault2) > 0.0000005M))
        {
          nullable2 = discountedLine1.DiscPct;
          Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
          nullable2 = discountedLine2.DiscPct;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          if (!(Math.Abs(valueOrDefault3 - valueOrDefault4) > 0.0000005M))
            goto label_40;
        }
        if (discountedLine1.DiscountID == discountedLine2.DiscountID)
          flag4 = false;
      }
label_40:
      nullable2 = discountDocumentLine1.CuryLineAmount;
      nullable1 = discountDocumentLine2.CuryLineAmount;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        nullable1 = discountDocumentLine1.Quantity;
        nullable2 = discountDocumentLine2.Quantity;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = discountDocumentLine1.CuryUnitPrice;
          nullable1 = discountDocumentLine2.CuryUnitPrice;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = discountDocumentLine1.CuryExtPrice;
            nullable2 = discountDocumentLine2.CuryExtPrice;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = discountedLine1.DiscPct;
              nullable1 = discountedLine2.DiscPct;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              {
                nullable1 = discountedLine1.CuryDiscAmt;
                nullable2 = discountedLine2.CuryDiscAmt;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                {
                  nullable2 = discountedLine1.CuryDiscAmt;
                  Decimal num3 = 0M;
                  if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
                    flag1 = true;
                }
              }
            }
          }
        }
      }
      if (this.discountFeatureTypeE == DiscountFeatureType.CustomerDiscount && !PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || this.discountFeatureTypeE == DiscountFeatureType.VendorDiscount && !PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>())
        flag1 = true;
      Decimal? nullable3 = new Decimal?();
      nullable2 = discountDocumentLine1.CuryLineAmount;
      nullable1 = discountDocumentLine2.CuryLineAmount;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        if (flag3)
        {
          nullable1 = discountDocumentLine1.CuryExtPrice;
          Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
          Decimal num4;
          if (ManualDiscountMode.GetLineDiscountTarget(sender, mapFor1) == "S")
          {
            PXCache sender1 = sender;
            AmountLineFields row1 = discountDocumentLine1;
            nullable1 = discountDocumentLine1.Quantity;
            Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
            nullable1 = discountDocumentLine1.CuryUnitPrice;
            Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
            Decimal val1 = valueOrDefault6 * valueOrDefault7;
            Decimal num5 = MultiCurrencyCalculator.RoundCury(sender1, (object) row1, val1);
            PXCache sender2 = sender;
            AmountLineFields row2 = discountDocumentLine1;
            nullable1 = discountDocumentLine1.Quantity;
            Decimal valueOrDefault8 = nullable1.GetValueOrDefault();
            nullable1 = discountDocumentLine1.CuryUnitPrice;
            Decimal valueOrDefault9 = nullable1.GetValueOrDefault();
            nullable1 = discountedLine1.DiscPct;
            Decimal num6 = 1M - nullable1.GetValueOrDefault() * 0.01M;
            Decimal num7 = PXDBPriceCostAttribute.Round(valueOrDefault9 * num6);
            Decimal val2 = valueOrDefault8 * num7;
            Decimal num8 = MultiCurrencyCalculator.RoundCury(sender2, (object) row2, val2);
            num4 = num5 - num8;
          }
          else
          {
            Decimal num9 = valueOrDefault5;
            nullable1 = discountedLine1.DiscPct;
            Decimal valueOrDefault10 = nullable1.GetValueOrDefault();
            Decimal val = num9 * valueOrDefault10 * 0.01M;
            num4 = MultiCurrencyCalculator.RoundCury(sender, (object) discountedLine1, val);
          }
          nullable1 = discountDocumentLine1.CuryExtPrice;
          Decimal num10 = num4;
          Decimal? nullable4;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(nullable1.GetValueOrDefault() - num10);
          Decimal? nullable5 = nullable4;
          nullable3 = new Decimal?(MultiCurrencyCalculator.RoundCury(sender, (object) discountDocumentLine1, nullable5.GetValueOrDefault()));
        }
        else
        {
          nullable1 = discountedLine1.CuryDiscAmt;
          nullable2 = discountDocumentLine1.CuryExtPrice;
          Decimal? nullable6;
          if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            nullable6 = discountDocumentLine1.CuryExtPrice;
          }
          else
          {
            nullable2 = discountDocumentLine1.CuryExtPrice;
            nullable1 = discountedLine1.CuryDiscAmt;
            nullable6 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          }
          nullable3 = new Decimal?(MultiCurrencyCalculator.RoundCury(sender, (object) discountDocumentLine1, nullable6.GetValueOrDefault()));
        }
        nullable1 = discountDocumentLine1.CuryLineAmount;
        nullable2 = nullable3;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          nullable2 = discountedLine1.DiscPct;
          nullable1 = discountedLine2.DiscPct;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            flag1 = true;
        }
      }
      sender.SetValue(e.Row, this.FieldName, (object) flag1);
      if (flag1 | flag2 || sender.Graph.IsCopyPasteContext)
      {
        if (flag1 && !flag4 && !sender.Graph.IsImport)
        {
          discountedLine1.DiscountID = (string) null;
          discountedLine1.DiscountSequenceID = (string) null;
        }
        nullable1 = discountDocumentLine1.Quantity;
        Decimal num11 = 0M;
        if (nullable1.GetValueOrDefault() == num11 & nullable1.HasValue)
        {
          nullable1 = discountDocumentLine2.Quantity;
          Decimal num12 = 0M;
          if (!(nullable1.GetValueOrDefault() == num12 & nullable1.HasValue))
          {
            sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.curyDiscAmt)), (object) 0M);
            sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) 0M);
            return;
          }
        }
        nullable1 = discountDocumentLine1.CuryLineAmount;
        nullable2 = discountDocumentLine2.CuryLineAmount;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && !flag3)
        {
          Decimal? nullable7;
          ref Decimal? local1 = ref nullable7;
          nullable2 = discountDocumentLine1.CuryExtPrice;
          Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
          local1 = new Decimal?(valueOrDefault11);
          Decimal? newValue;
          ref Decimal? local2 = ref newValue;
          nullable2 = discountDocumentLine1.CuryLineAmount;
          Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
          local2 = new Decimal?(valueOrDefault12);
          nullable1 = nullable7;
          Decimal? nullable8 = discountDocumentLine1.CuryLineAmount;
          nullable2 = nullable1.HasValue & nullable8.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
          Decimal num13 = 0M;
          if (nullable2.GetValueOrDefault() >= num13 & nullable2.HasValue)
          {
            nullable2 = discountedLine1.CuryDiscAmt;
            Decimal num14 = Math.Abs(nullable7.GetValueOrDefault());
            if (nullable2.GetValueOrDefault() > num14 & nullable2.HasValue)
            {
              sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.curyDiscAmt)), (object) discountDocumentLine1.CuryExtPrice);
              PXUIFieldAttribute.SetWarning<DiscountLineFields.curyDiscAmt>(sender, e.Row, PXMessages.LocalizeFormatNoPrefix("The discount amount cannot be greater than the amount in the {0} column.", new object[1]
              {
                (object) discountDocumentLine1.ExtPriceDisplayName
              }));
            }
            else
            {
              PXCache pxCache = sender;
              object row = e.Row;
              string field = sender.GetField(typeof (ManualDiscountMode.curyDiscAmt));
              nullable2 = nullable7;
              nullable8 = discountDocumentLine1.CuryLineAmount;
              Decimal? nullable9;
              if (!(nullable2.HasValue & nullable8.HasValue))
              {
                nullable1 = new Decimal?();
                nullable9 = nullable1;
              }
              else
                nullable9 = new Decimal?(nullable2.GetValueOrDefault() - nullable8.GetValueOrDefault());
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local3 = (ValueType) nullable9;
              pxCache.SetValueExt(row, field, (object) local3);
            }
            nullable8 = nullable7;
            Decimal num15 = 0M;
            if (nullable8.GetValueOrDefault() == num15 & nullable8.HasValue || sender.Graph.IsCopyPasteContext)
              return;
            Decimal? nullable10 = this.CalcDiscountPercent(discountDocumentLine1, discountedLine1);
            sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) nullable10);
          }
          else
          {
            nullable8 = nullable7;
            Decimal num16 = 0M;
            if (nullable8.GetValueOrDefault() == num16 & nullable8.HasValue || sender.Graph.IsCopyPasteContext)
              return;
            nullable8 = discountedLine1.CuryDiscAmt;
            nullable2 = discountedLine2.CuryDiscAmt;
            if (!(nullable8.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable8.HasValue == nullable2.HasValue))
            {
              Decimal? nullable11 = this.CalcDiscountPercent(discountDocumentLine1, discountedLine1);
              sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) (nullable11.GetValueOrDefault() < -100M ? new Decimal?(-100M) : nullable11));
              if (!(nullable11.GetValueOrDefault() < -100M))
                return;
              PXCache pxCache = sender;
              object row = e.Row;
              string field = sender.GetField(typeof (ManualDiscountMode.curyDiscAmt));
              nullable2 = discountDocumentLine1.CuryExtPrice;
              Decimal? nullable12;
              if (!nullable2.HasValue)
              {
                nullable8 = new Decimal?();
                nullable12 = nullable8;
              }
              else
                nullable12 = new Decimal?(-nullable2.GetValueOrDefault());
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local4 = (ValueType) nullable12;
              pxCache.SetValueExt(row, field, (object) local4);
              PXUIFieldAttribute.SetWarning<DiscountLineFields.curyDiscAmt>(sender, e.Row, PXMessages.LocalizeFormatNoPrefix("The discount amount cannot be greater than the amount in the {0} column.", new object[1]
              {
                (object) discountDocumentLine1.ExtPriceDisplayName
              }));
            }
            else
            {
              sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) 0M);
              sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.curyDiscAmt)), (object) 0M);
              this.UpdateLineAmt<AmountLineFields.curyLineAmount>(discountDocumentLine1, discountDocumentLine2.CuryLineAmount, newValue);
            }
          }
        }
        else
        {
          nullable2 = discountedLine1.CuryDiscAmt;
          Decimal? curyDiscAmt = discountedLine2.CuryDiscAmt;
          if (!(nullable2.GetValueOrDefault() == curyDiscAmt.GetValueOrDefault() & nullable2.HasValue == curyDiscAmt.HasValue))
          {
            Decimal? curyExtPrice1 = discountDocumentLine1.CuryExtPrice;
            Decimal num17 = 0M;
            if (curyExtPrice1.GetValueOrDefault() == num17 & curyExtPrice1.HasValue || sender.Graph.IsCopyPasteContext)
              return;
            Decimal? curyExtPrice2 = discountDocumentLine1.CuryExtPrice;
            Decimal num18 = 0M;
            if (curyExtPrice2.GetValueOrDefault() == num18 & curyExtPrice2.HasValue || sender.Graph.IsCopyPasteContext)
              return;
            Decimal? nullable13 = this.CalcDiscountPercent(discountDocumentLine1, discountedLine1);
            sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) nullable13);
          }
          else
          {
            Decimal? discPct = discountedLine1.DiscPct;
            nullable2 = discountedLine2.DiscPct;
            if (discPct.GetValueOrDefault() == nullable2.GetValueOrDefault() & discPct.HasValue == nullable2.HasValue)
            {
              if (!nullable3.HasValue)
                return;
              nullable2 = discountDocumentLine1.CuryLineAmount;
              Decimal? nullable14 = nullable3;
              if (nullable2.GetValueOrDefault() == nullable14.GetValueOrDefault() & nullable2.HasValue == nullable14.HasValue)
                return;
            }
            Decimal num19 = this.CalcDiscountAmount(sender, ManualDiscountMode.GetLineDiscountTarget(sender, mapFor1), discountDocumentLine1, discountedLine1);
            sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.curyDiscAmt)), (object) num19);
          }
        }
      }
      else
      {
        if (!flag5 || discountedLine1.DiscountID != null && !discountedLine1.AutomaticDiscountsDisabled.GetValueOrDefault())
          return;
        sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.discPct)), (object) 0M);
        sender.SetValueExt(e.Row, sender.GetField(typeof (ManualDiscountMode.curyDiscAmt)), (object) 0M);
        if (!discountedLine1.AutomaticDiscountsDisabled.GetValueOrDefault())
          return;
        discountedLine1.DiscountID = (string) null;
        discountedLine1.DiscountSequenceID = (string) null;
      }
    }
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this.discountFeatureTypeE == DiscountFeatureType.CustomerDiscount && !PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || this.discountFeatureTypeE == DiscountFeatureType.VendorDiscount && !PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>())
      sender.SetValue(e.Row, this.FieldName, (object) true);
    if (sender.Graph.IsCopyPasteContext)
      return;
    AmountLineFields discountDocumentLine = ManualDiscountMode.GetDiscountDocumentLine(sender, e.Row);
    if (discountDocumentLine.FreezeManualDisc.GetValueOrDefault())
    {
      discountDocumentLine.FreezeManualDisc = new bool?(false);
    }
    else
    {
      DiscountLineFields discountedLine = ManualDiscountMode.GetDiscountedLine(sender, e.Row);
      if (discountedLine.LineType == "DS")
        return;
      Decimal? nullable1 = discountedLine.CuryDiscAmt;
      if (nullable1.HasValue)
      {
        nullable1 = discountedLine.CuryDiscAmt;
        Decimal num1 = 0M;
        if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
        {
          nullable1 = discountDocumentLine.CuryExtPrice;
          Decimal num2 = 0M;
          if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
          {
            Decimal? nullable2 = new Decimal?();
            nullable1 = discountedLine.DiscPct;
            if (nullable1.HasValue)
            {
              ref Decimal? local = ref nullable2;
              PXCache sender1 = sender;
              object row = e.Row;
              Decimal num3 = discountDocumentLine.CuryExtPrice.GetValueOrDefault() / 100M;
              nullable1 = discountedLine.DiscPct;
              Decimal val = (nullable1.HasValue ? new Decimal?(num3 * nullable1.GetValueOrDefault()) : new Decimal?()).Value;
              Decimal num4 = MultiCurrencyCalculator.RoundCury(sender1, row, val);
              local = new Decimal?(num4);
            }
            Decimal? nullable3;
            if (nullable2.HasValue)
            {
              nullable1 = nullable2;
              nullable3 = discountedLine.CuryDiscAmt;
              if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
              {
                DiscountLineFields discountLineFields = discountedLine;
                Decimal num5 = (Decimal) 100;
                Decimal? curyDiscAmt = discountedLine.CuryDiscAmt;
                nullable3 = curyDiscAmt.HasValue ? new Decimal?(num5 * curyDiscAmt.GetValueOrDefault()) : new Decimal?();
                nullable1 = discountDocumentLine.CuryExtPrice;
                Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
                discountLineFields.DiscPct = nullable4;
              }
            }
            if (sender.Graph.IsContractBasedAPI)
            {
              sender.SetValue(e.Row, discountedLine.GetField<DiscountLineFields.manualDisc>().Name, (object) true);
              PXCache pxCache = sender;
              object row = e.Row;
              string name = discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name;
              nullable1 = discountDocumentLine.CuryExtPrice;
              nullable3 = discountedLine.CuryDiscAmt;
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?());
              pxCache.SetValueExt(row, name, (object) local);
              return;
            }
            PXCache pxCache1 = sender;
            object row1 = e.Row;
            string name1 = discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name;
            nullable3 = discountDocumentLine.CuryExtPrice;
            nullable1 = discountedLine.CuryDiscAmt;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local1 = (ValueType) (nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?());
            pxCache1.SetValue(row1, name1, (object) local1);
            return;
          }
        }
      }
      nullable1 = discountedLine.DiscPct;
      if (nullable1.HasValue)
      {
        nullable1 = discountedLine.DiscPct;
        Decimal num = 0M;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          nullable1 = discountDocumentLine.CuryExtPrice;
          Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
          nullable1 = discountedLine.DiscPct;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          Decimal val = valueOrDefault1 * valueOrDefault2 * 0.01M;
          sender.SetValueExt(e.Row, discountedLine.GetField<DiscountLineFields.curyDiscAmt>().Name, (object) MultiCurrencyCalculator.RoundCury(sender, (object) discountedLine, val));
          if (sender.Graph.IsContractBasedAPI)
          {
            sender.SetValue(e.Row, discountedLine.GetField<DiscountLineFields.manualDisc>().Name, (object) true);
            PXCache pxCache = sender;
            object row = e.Row;
            string name = discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name;
            nullable1 = discountDocumentLine.CuryExtPrice;
            Decimal? curyDiscAmt = discountedLine.CuryDiscAmt;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & curyDiscAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyDiscAmt.GetValueOrDefault()) : new Decimal?());
            pxCache.SetValueExt(row, name, (object) local);
            return;
          }
          PXCache pxCache2 = sender;
          object row2 = e.Row;
          string name2 = discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name;
          Decimal? curyExtPrice = discountDocumentLine.CuryExtPrice;
          nullable1 = discountedLine.CuryDiscAmt;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local2 = (ValueType) (curyExtPrice.HasValue & nullable1.HasValue ? new Decimal?(curyExtPrice.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?());
          pxCache2.SetValue(row2, name2, (object) local2);
          return;
        }
      }
      nullable1 = discountDocumentLine.CuryExtPrice;
      if (!nullable1.HasValue)
        return;
      nullable1 = discountDocumentLine.CuryExtPrice;
      Decimal num6 = 0M;
      if (nullable1.GetValueOrDefault() == num6 & nullable1.HasValue)
        return;
      nullable1 = discountDocumentLine.CuryLineAmount;
      if (nullable1.HasValue)
      {
        nullable1 = discountDocumentLine.CuryLineAmount;
        Decimal num7 = 0M;
        if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
          return;
      }
      if (sender.Graph.IsContractBasedAPI)
        sender.SetValueExt(e.Row, discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name, (object) discountDocumentLine.CuryExtPrice);
      else
        sender.SetValue(e.Row, discountDocumentLine.GetField<AmountLineFields.curyLineAmount>().Name, (object) discountDocumentLine.CuryExtPrice);
    }
  }

  protected virtual Decimal CalcDiscountAmount(
    PXCache sender,
    string lineDiscountTarget,
    AmountLineFields lineAmountsFields,
    DiscountLineFields lineDiscountFields)
  {
    Decimal? nullable;
    if (lineDiscountTarget == "S")
    {
      nullable = lineAmountsFields.CuryUnitPrice;
      Decimal num1 = 0M;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        nullable = lineAmountsFields.Quantity;
        Decimal num2 = 0M;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          PXCache sender1 = sender;
          AmountLineFields row1 = lineAmountsFields;
          nullable = lineAmountsFields.Quantity;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = lineAmountsFields.CuryUnitPrice;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          Decimal val1 = valueOrDefault1 * valueOrDefault2;
          Decimal num3 = MultiCurrencyCalculator.RoundCury(sender1, (object) row1, val1);
          PXCache sender2 = sender;
          AmountLineFields row2 = lineAmountsFields;
          nullable = lineAmountsFields.Quantity;
          Decimal valueOrDefault3 = nullable.GetValueOrDefault();
          nullable = lineAmountsFields.CuryUnitPrice;
          Decimal valueOrDefault4 = nullable.GetValueOrDefault();
          nullable = lineAmountsFields.CuryUnitPrice;
          Decimal valueOrDefault5 = nullable.GetValueOrDefault();
          nullable = lineDiscountFields.DiscPct;
          Decimal valueOrDefault6 = nullable.GetValueOrDefault();
          Decimal num4 = PXDBPriceCostAttribute.Round(valueOrDefault5 * valueOrDefault6 * 0.01M);
          Decimal num5 = valueOrDefault4 - num4;
          Decimal val2 = valueOrDefault3 * num5;
          Decimal num6 = MultiCurrencyCalculator.RoundCury(sender2, (object) row2, val2);
          return num3 - num6;
        }
      }
    }
    nullable = lineAmountsFields.CuryExtPrice;
    Decimal valueOrDefault7 = nullable.GetValueOrDefault();
    nullable = lineDiscountFields.DiscPct;
    Decimal valueOrDefault8 = nullable.GetValueOrDefault();
    return valueOrDefault7 * valueOrDefault8 * 0.01M;
  }

  protected virtual Decimal? CalcDiscountPercent(
    AmountLineFields lineAmountsFields,
    DiscountLineFields lineDiscountFields)
  {
    Decimal num = lineDiscountFields.CuryDiscAmt.GetValueOrDefault() * 100M;
    Decimal? curyExtPrice = lineAmountsFields.CuryExtPrice;
    return !curyExtPrice.HasValue ? new Decimal?() : new Decimal?(num / curyExtPrice.GetValueOrDefault());
  }

  private abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ManualDiscountMode.discPct>
  {
  }

  private abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ManualDiscountMode.curyDiscAmt>
  {
  }

  private abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ManualDiscountMode.curyLineAmt>
  {
  }
}
