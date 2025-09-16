// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DateSel;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _SiteID;
  protected string _CarrierPluginID;
  protected string _ShipVia;
  protected int? _CustomerID;
  protected DateTime? _ShipmentDate;

  [PXWorkflowMassProcessing(DisplayName = "Action")]
  public virtual string Action { get; set; }

  [PXDBString]
  [PXDefault("S")]
  [PXUIField(DisplayName = "Select By")]
  [SOOrderFilter.dateSel.List]
  public virtual string DateSel
  {
    get => this._DateSel;
    set => this._DateSel = value;
  }

  [PXDBDate]
  [PXUIField]
  [PXDefault]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Sched. Order Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? SchedOrderDate { get; set; }

  [PXDBDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [Site(DisplayName = "Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<CarrierPlugin.carrierPluginID>))]
  public virtual string CarrierPluginID
  {
    get => this._CarrierPluginID;
    set => this._CarrierPluginID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [PXUIField(DisplayName = "Customer")]
  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? ShipmentDate
  {
    get => this._ShipmentDate;
    set => this._ShipmentDate = value;
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderFilter.action>
  {
  }

  public abstract class dateSel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderFilter.dateSel>
  {
    public const string ShipDate = "S";
    public const string CancelBy = "C";
    public const string OrderDate = "O";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("S", "Ship Date"),
          PXStringListAttribute.Pair("C", "Cancel By"),
          PXStringListAttribute.Pair("O", "Order Date")
        })
      {
      }
    }

    public class shipDate : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOOrderFilter.dateSel.shipDate>
    {
      public shipDate()
        : base("S")
      {
      }
    }

    public class cancelBy : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOOrderFilter.dateSel.cancelBy>
    {
      public cancelBy()
        : base("C")
      {
      }
    }

    public class orderDate : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOOrderFilter.dateSel.orderDate>
    {
      public orderDate()
        : base("O")
      {
      }
    }
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrderFilter.startDate>
  {
  }

  public abstract class schedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderFilter.schedOrderDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrderFilter.endDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderFilter.siteID>
  {
  }

  public abstract class carrierPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderFilter.carrierPluginID>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderFilter.shipVia>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderFilter.customerID>
  {
  }

  public abstract class shipmentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderFilter.shipmentDate>
  {
  }
}
