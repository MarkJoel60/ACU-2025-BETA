// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickProcessParametersShipDateExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public sealed class SOQuickProcessParametersShipDateExt : PXCacheExtension<
#nullable disable
SOQuickProcessParameters>
{
  [PXShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Shipment Date")]
  [PXQuickProcess.Step.RelatedField(typeof (SOQuickProcessParameters.createShipment))]
  public short? ShipDateMode { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Custom Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXFormula(typeof (IIf<Where<SOQuickProcessParametersShipDateExt.shipDateMode, Equal<PX.Objects.SO.ShipDateMode.tomorrow>>, SOQuickProcessParametersShipDateExt.tomorrow, SOQuickProcessParametersShipDateExt.today>))]
  [PXUIEnabled(typeof (Where<SOQuickProcessParametersShipDateExt.shipDateMode, Equal<PX.Objects.SO.ShipDateMode.custom>>))]
  [PXQuickProcess.Step.RelatedParameter(typeof (SOQuickProcessParameters.createShipment), "shipDate")]
  public DateTime? ShipDate { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public DateTime? Today { get; set; }

  [PXDate]
  public DateTime? Tomorrow
  {
    get
    {
      return new DateTime?(this.Today.With<DateTime, DateTime>((Func<DateTime, DateTime>) (d => d.AddDays(1.0))));
    }
    set
    {
    }
  }

  public static void SetDate(PXCache cache, SOQuickProcessParameters row, DateTime date)
  {
    SOQuickProcessParametersShipDateExt extension = cache.GetExtension<SOQuickProcessParametersShipDateExt>((object) row);
    DateTime dateTime1 = date;
    DateTime? today = extension.Today;
    if ((today.HasValue ? (dateTime1 == today.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      cache.SetValueExt<SOQuickProcessParametersShipDateExt.shipDateMode>((object) row, (object) (short) 0);
    }
    else
    {
      DateTime dateTime2 = date;
      DateTime? tomorrow = extension.Tomorrow;
      if ((tomorrow.HasValue ? (dateTime2 == tomorrow.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        cache.SetValueExt<SOQuickProcessParametersShipDateExt.shipDateMode>((object) row, (object) (short) 1);
      }
      else
      {
        cache.SetValueExt<SOQuickProcessParametersShipDateExt.shipDateMode>((object) row, (object) (short) 2);
        cache.SetValueExt<SOQuickProcessParametersShipDateExt.shipDate>((object) row, (object) date);
      }
    }
  }

  public abstract class shipDateMode : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOQuickProcessParametersShipDateExt.shipDateMode>
  {
  }

  public abstract class shipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOQuickProcessParametersShipDateExt.shipDate>
  {
  }

  public abstract class today : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOQuickProcessParametersShipDateExt.today>
  {
  }

  public abstract class tomorrow : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOQuickProcessParametersShipDateExt.tomorrow>
  {
  }
}
