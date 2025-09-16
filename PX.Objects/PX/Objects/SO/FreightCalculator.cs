// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.FreightCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>Calculates Freight Cost and Freight Terms</summary>
public class FreightCalculator
{
  protected PXGraph graph;

  public FreightCalculator(PXGraph graph)
  {
    this.graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  /// <summary>
  /// First calculates and sets CuryFreightCost, then applies the Terms and updates CuryFreightAmt.
  /// </summary>
  public virtual void CalcFreight<T, CuryFreightCostField, CuryFreightAmtField>(
    PXCache sender,
    T data,
    int? linesCount)
    where T : class, IFreightBase, new()
    where CuryFreightCostField : IBqlField
    where CuryFreightAmtField : IBqlField
  {
    this.CalcFreightCost<T, CuryFreightCostField>(sender, data);
    this.ApplyFreightTerms<T, CuryFreightAmtField>(sender, data, linesCount);
  }

  /// <summary>Calculates and sets CuryFreightCost</summary>
  public virtual void CalcFreightCost<T, CuryFreightCostField>(PXCache sender, T data)
    where T : class, IFreightBase, new()
    where CuryFreightCostField : IBqlField
  {
    data.FreightCost = new Decimal?(this.CalculateFreightCost<T>(data));
    PXCurrencyAttribute.CuryConvCury<CuryFreightCostField>(sender, (object) data);
  }

  /// <summary>Applies the Terms and updates CuryFreightAmt.</summary>
  public virtual void ApplyFreightTerms<T, CuryFreightAmtField>(
    PXCache sender,
    T data,
    int? linesCount)
    where T : class, IFreightBase, new()
    where CuryFreightAmtField : IBqlField
  {
    ShipTermsDetail freightTerms = this.GetFreightTerms(data.ShipTermsID, data.LineTotal);
    if (freightTerms != null)
    {
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) data;
      Decimal? nullable1 = data.FreightCost;
      Decimal? nullable2 = freightTerms.FreightCostPercent;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num1 = (Decimal) 100;
      Decimal? nullable4;
      if (!nullable3.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable3.GetValueOrDefault() / num1);
      nullable2 = nullable4;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      nullable1 = data.LineTotal;
      Decimal? nullable5 = freightTerms.InvoiceAmountPercent;
      nullable2 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = (Decimal) 100;
      Decimal? nullable6;
      if (!nullable2.HasValue)
      {
        nullable5 = new Decimal?();
        nullable6 = nullable5;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() / num2);
      nullable5 = nullable6;
      Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
      Decimal num3 = valueOrDefault1 + valueOrDefault2;
      nullable5 = freightTerms.ShippingHandling;
      Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault3;
      int? nullable7 = linesCount;
      nullable5 = nullable7.HasValue ? new Decimal?((Decimal) nullable7.GetValueOrDefault()) : new Decimal?();
      nullable1 = freightTerms.LineHandling;
      Decimal valueOrDefault4 = (nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      Decimal? nullable8 = new Decimal?(num4 + valueOrDefault4);
      local.FreightAmt = nullable8;
    }
    else
      data.FreightAmt = data.FreightCost;
    PXCurrencyAttribute.CuryConvCury<CuryFreightAmtField>(sender, (object) data);
  }

  /// <summary>Applies the Terms and updates CuryFreightAmt.</summary>
  public virtual void ApplyFreightTerms<T, CuryFreightAmtField>(
    PXCache sender,
    T data,
    Lazy<int?> linesCount)
    where T : class, IFreightBase, new()
    where CuryFreightAmtField : IBqlField
  {
    ShipTermsDetail freightTerms = this.GetFreightTerms(data.ShipTermsID, data.LineTotal);
    if (freightTerms != null)
    {
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) data;
      Decimal? nullable1 = data.FreightCost;
      Decimal? nullable2 = freightTerms.FreightCostPercent;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num1 = (Decimal) 100;
      Decimal? nullable4;
      if (!nullable3.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable3.GetValueOrDefault() / num1);
      nullable2 = nullable4;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      nullable1 = data.LineTotal;
      Decimal? nullable5 = freightTerms.InvoiceAmountPercent;
      nullable2 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = (Decimal) 100;
      Decimal? nullable6;
      if (!nullable2.HasValue)
      {
        nullable5 = new Decimal?();
        nullable6 = nullable5;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() / num2);
      nullable5 = nullable6;
      Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
      Decimal num3 = valueOrDefault1 + valueOrDefault2;
      nullable5 = freightTerms.ShippingHandling;
      Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault3;
      int? nullable7 = linesCount.Value;
      nullable5 = nullable7.HasValue ? new Decimal?((Decimal) nullable7.GetValueOrDefault()) : new Decimal?();
      nullable1 = freightTerms.LineHandling;
      Decimal valueOrDefault4 = (nullable5.HasValue & nullable1.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      Decimal? nullable8 = new Decimal?(num4 + valueOrDefault4);
      local.FreightAmt = nullable8;
    }
    else
      data.FreightAmt = data.FreightCost;
    PXCurrencyAttribute.CuryConvCury<CuryFreightAmtField>(sender, (object) data);
  }

  /// <summary>
  /// Returns true if it is "flat rate shipping" ("Freight Cost %" is zero), otherwise, false.
  /// </summary>
  public virtual bool IsFlatRate<T>(PXCache sender, T data) where T : class, IFreightBase, new()
  {
    if (data.ShipTermsID == null)
      return false;
    ShipTermsDetail freightTerms = this.GetFreightTerms(data.ShipTermsID, data.LineTotal);
    if (freightTerms == null)
      return false;
    Decimal? freightCostPercent = freightTerms.FreightCostPercent;
    Decimal num = 0M;
    return freightCostPercent.GetValueOrDefault() == num & freightCostPercent.HasValue;
  }

  protected virtual Decimal CalculateFreightCost<T>(T data) where T : class, IFreightBase, new()
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find(this.graph, data.ShipVia);
    if (carrier == null)
      return 0M;
    if (!(carrier.CalcMethod != "M"))
      return data.FreightCost.GetValueOrDefault();
    Decimal num1;
    Decimal? nullable;
    if (data.OrderVolume.HasValue)
    {
      Decimal? orderVolume = data.OrderVolume;
      Decimal num2 = 0M;
      if (!(orderVolume.GetValueOrDefault() == num2 & orderVolume.HasValue))
      {
        if (data.PackageWeight.HasValue)
        {
          Decimal? packageWeight1 = data.PackageWeight;
          Decimal num3 = 0M;
          if (!(packageWeight1.GetValueOrDefault() == num3 & packageWeight1.HasValue))
          {
            string carrierId = carrier.CarrierID;
            string shipZoneId = data.ShipZoneID;
            Decimal? weight;
            if (data.PackageWeight.HasValue)
            {
              Decimal? packageWeight2 = data.PackageWeight;
              Decimal num4 = 0M;
              if (!(packageWeight2.GetValueOrDefault() == num4 & packageWeight2.HasValue))
              {
                weight = data.PackageWeight;
                goto label_24;
              }
            }
            weight = data.OrderWeight;
label_24:
            FreightRate rateBasedOnWeight = this.GetFreightRateBasedOnWeight(carrierId, shipZoneId, weight);
            FreightRate rateBasedOnVolume = this.GetFreightRateBasedOnVolume(carrier.CarrierID, data.ShipZoneID, data.OrderVolume);
            Decimal val1;
            Decimal val2;
            if (carrier.CalcMethod == "N")
            {
              val1 = rateBasedOnWeight.Rate.GetValueOrDefault();
              nullable = rateBasedOnVolume.Rate;
              val2 = nullable.GetValueOrDefault();
            }
            else
            {
              Decimal valueOrDefault1 = rateBasedOnWeight.Rate.GetValueOrDefault();
              nullable = data.PackageWeight;
              Decimal valueOrDefault2 = nullable.GetValueOrDefault();
              val1 = valueOrDefault1 * valueOrDefault2;
              nullable = rateBasedOnVolume.Rate;
              Decimal valueOrDefault3 = nullable.GetValueOrDefault();
              nullable = data.OrderVolume;
              Decimal valueOrDefault4 = nullable.GetValueOrDefault();
              val2 = valueOrDefault3 * valueOrDefault4;
            }
            num1 = Math.Max(val1, val2);
            goto label_28;
          }
        }
        FreightRate rateBasedOnVolume1 = this.GetFreightRateBasedOnVolume(carrier.CarrierID, data.ShipZoneID, data.OrderVolume);
        if (carrier.CalcMethod == "N")
        {
          num1 = rateBasedOnVolume1.Rate.GetValueOrDefault();
          goto label_28;
        }
        Decimal valueOrDefault5 = rateBasedOnVolume1.Rate.GetValueOrDefault();
        nullable = data.OrderVolume;
        Decimal valueOrDefault6 = nullable.GetValueOrDefault();
        num1 = valueOrDefault5 * valueOrDefault6;
        goto label_28;
      }
    }
    string carrierId1 = carrier.CarrierID;
    string shipZoneId1 = data.ShipZoneID;
    Decimal? weight1;
    if (data.PackageWeight.HasValue)
    {
      Decimal? packageWeight = data.PackageWeight;
      Decimal num5 = 0M;
      if (!(packageWeight.GetValueOrDefault() == num5 & packageWeight.HasValue))
      {
        weight1 = data.PackageWeight;
        goto label_9;
      }
    }
    weight1 = data.OrderWeight;
label_9:
    FreightRate rateBasedOnWeight1 = this.GetFreightRateBasedOnWeight(carrierId1, shipZoneId1, weight1);
    if (carrier.CalcMethod == "N")
    {
      num1 = rateBasedOnWeight1.Rate.GetValueOrDefault();
    }
    else
    {
      if (data.PackageWeight.HasValue)
      {
        Decimal? packageWeight = data.PackageWeight;
        Decimal num6 = 0M;
        if (!(packageWeight.GetValueOrDefault() == num6 & packageWeight.HasValue))
        {
          Decimal valueOrDefault7 = rateBasedOnWeight1.Rate.GetValueOrDefault();
          nullable = data.PackageWeight;
          Decimal valueOrDefault8 = nullable.GetValueOrDefault();
          num1 = valueOrDefault7 * valueOrDefault8;
          goto label_28;
        }
      }
      Decimal valueOrDefault9 = rateBasedOnWeight1.Rate.GetValueOrDefault();
      nullable = data.OrderWeight;
      Decimal valueOrDefault10 = nullable.GetValueOrDefault();
      num1 = valueOrDefault9 * valueOrDefault10;
    }
label_28:
    nullable = carrier.BaseRate;
    return nullable.GetValueOrDefault() + num1;
  }

  protected virtual ShipTermsDetail GetFreightTerms(string shipTermsID, Decimal? lineTotal)
  {
    return PXResultset<ShipTermsDetail>.op_Implicit(PXSelectBase<ShipTermsDetail, PXSelect<ShipTermsDetail, Where<ShipTermsDetail.shipTermsID, Equal<Required<SOOrder.shipTermsID>>, And<ShipTermsDetail.breakAmount, LessEqual<Required<SOOrder.lineTotal>>>>, OrderBy<Desc<ShipTermsDetail.breakAmount>>>.Config>.Select(this.graph, new object[2]
    {
      (object) shipTermsID,
      (object) lineTotal
    }));
  }

  protected virtual FreightRate GetFreightRateBasedOnWeight(
    string carrierID,
    string shipZoneID,
    Decimal? weight)
  {
    FreightRate freightRate = PXResultset<FreightRate>.op_Implicit(PXSelectBase<FreightRate, PXSelect<FreightRate, Where<FreightRate.carrierID, Equal<Required<FreightRate.carrierID>>, And<FreightRate.weight, LessEqual<Required<SOOrder.orderWeight>>, And<FreightRate.zoneID, Equal<Required<SOOrder.shipZoneID>>>>>, OrderBy<Desc<FreightRate.volume, Desc<FreightRate.weight, Asc<FreightRate.rate>>>>>.Config>.Select(this.graph, new object[3]
    {
      (object) carrierID,
      (object) weight,
      (object) shipZoneID
    }));
    if (freightRate == null)
      freightRate = PXResultset<FreightRate>.op_Implicit(PXSelectBase<FreightRate, PXSelect<FreightRate, Where<FreightRate.carrierID, Equal<Required<FreightRate.carrierID>>, And<FreightRate.weight, LessEqual<Required<FreightRate.weight>>>>, OrderBy<Desc<FreightRate.volume, Desc<FreightRate.weight, Desc<FreightRate.rate>>>>>.Config>.Select(this.graph, new object[1]
      {
        (object) weight
      }));
    return freightRate ?? new FreightRate();
  }

  protected virtual FreightRate GetFreightRateBasedOnVolume(
    string carrierID,
    string shipZoneID,
    Decimal? volume)
  {
    FreightRate freightRate = PXResultset<FreightRate>.op_Implicit(PXSelectBase<FreightRate, PXSelect<FreightRate, Where<FreightRate.carrierID, Equal<Required<FreightRate.carrierID>>, And<FreightRate.volume, LessEqual<Required<FreightRate.volume>>, And<FreightRate.zoneID, Equal<Required<FreightRate.zoneID>>>>>, OrderBy<Desc<FreightRate.volume, Desc<FreightRate.weight, Asc<FreightRate.rate>>>>>.Config>.Select(this.graph, new object[3]
    {
      (object) carrierID,
      (object) volume,
      (object) shipZoneID
    }));
    if (freightRate == null)
      freightRate = PXResultset<FreightRate>.op_Implicit(PXSelectBase<FreightRate, PXSelect<FreightRate, Where<FreightRate.carrierID, Equal<Required<FreightRate.carrierID>>, And<FreightRate.weight, LessEqual<Required<FreightRate.weight>>>>, OrderBy<Desc<FreightRate.volume, Desc<FreightRate.weight, Desc<FreightRate.rate>>>>>.Config>.Select(this.graph, new object[1]
      {
        (object) volume
      }));
    return freightRate ?? new FreightRate();
  }
}
