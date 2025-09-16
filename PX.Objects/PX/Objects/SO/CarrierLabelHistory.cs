// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CarrierLabelHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXHidden]
public class CarrierLabelHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  public virtual 
  #nullable disable
  string ShipmentNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(255 /*0xFF*/)]
  public virtual string PluginTypeName { get; set; }

  [PXDBString(255 /*0xFF*/)]
  public virtual string ServiceMethod { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RateAmount { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierLabelHistory.recordID>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierLabelHistory.shipmentNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierLabelHistory.lineNbr>
  {
  }

  public abstract class pluginTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierLabelHistory.pluginTypeName>
  {
  }

  public abstract class serviceMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierLabelHistory.serviceMethod>
  {
    public const int Length = 255 /*0xFF*/;
  }

  public abstract class rateAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CarrierLabelHistory.rateAmount>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CarrierLabelHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierLabelHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierLabelHistory.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CarrierLabelHistory.Tstamp>
  {
  }
}
