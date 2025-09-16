// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOParamFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _ShipDate;
  protected int? _SiteID;

  [PXDate]
  [PXUIField(DisplayName = "Shipment Date", Required = true)]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Warehouse ID", Required = true, FieldClass = "INSITE")]
  [OrderSiteSelector]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  public abstract class shipDate : BqlType<IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOParamFilter.shipDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOParamFilter.siteID>
  {
  }
}
