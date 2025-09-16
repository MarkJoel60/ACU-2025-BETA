// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentTax
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment Tax")]
[Serializable]
public class FSAppointmentTax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentTax.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentTax.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FSAppointmentDet.lineNbr))]
  [PXUIField]
  [PXParent(typeof (Select<FSAppointmentDet, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointmentTax.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointmentTax.refNbr>>, And<FSAppointmentDet.lineNbr, Equal<Current<FSAppointmentTax.lineNbr>>>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (FSAppointmentTax.curyInfoID), typeof (FSAppointmentTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PXDBCurrency(typeof (FSAppointmentTax.curyInfoID), typeof (FSAppointmentTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<FSAppointmentTax>.By<FSAppointmentTax.srvOrdType, FSAppointmentTax.refNbr, FSAppointmentTax.lineNbr, FSAppointmentTax.taxID>
  {
    public static FSAppointmentTax Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentTax) PrimaryKeyOf<FSAppointmentTax>.By<FSAppointmentTax.srvOrdType, FSAppointmentTax.refNbr, FSAppointmentTax.lineNbr, FSAppointmentTax.taxID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentTax>.By<FSAppointmentTax.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentTax>.By<FSAppointmentTax.srvOrdType, FSAppointmentTax.refNbr>
    {
    }

    public class AppointmentLine : 
      PrimaryKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>.ForeignKeyOf<FSAppointmentTax>.By<FSAppointmentTax.srvOrdType, FSAppointmentTax.refNbr, FSAppointmentTax.lineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentTax>.By<FSAppointmentTax.curyInfoID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<FSAppointmentTax>.By<FSAppointmentTax.taxID>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTax.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTax.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAppointmentTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTax.curyExpenseAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentTax.Tstamp>
  {
  }
}
