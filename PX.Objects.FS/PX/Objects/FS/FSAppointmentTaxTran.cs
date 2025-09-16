// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentTaxTran
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment Tax Detail")]
[Serializable]
public class FSAppointmentTaxTran : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _JurisType;
  protected string _JurisName;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected byte[] _tstamp;

  [PXDBString(4, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentTaxTran.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentTaxTran.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(9, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Type")]
  public virtual string JurisType
  {
    get => this._JurisType;
    set => this._JurisType = value;
  }

  [PXDBString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Jurisdiction Name")]
  public virtual string JurisName
  {
    get => this._JurisName;
    set => this._JurisName = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (FSAppointmentTaxTran.curyInfoID), typeof (FSAppointmentTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereExempt<FSAppointmentTaxTran.taxID>>, FSAppointmentTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<FSAppointment.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<WhereTaxable<FSAppointmentTaxTran.taxID>>, FSAppointmentTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<FSAppointment.curyVatTaxableTotal>))]
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

  [PXDBCurrency(typeof (FSAppointmentTaxTran.curyInfoID), typeof (FSAppointmentTaxTran.taxAmt))]
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

  /// <summary>
  /// The reference to the tax zone (<see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" />).
  /// </summary>
  [PXString(10, IsUnicode = true)]
  public virtual string TaxZoneID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// A Boolean value that specifies that the tax transaction is tax inclusive or not
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Inclusive")]
  public virtual bool? IsTaxInclusive { get; set; }

  public class PK : 
    PrimaryKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.srvOrdType, FSAppointmentTaxTran.refNbr, FSAppointmentTaxTran.recordID, FSAppointmentTaxTran.taxID>
  {
    public static FSAppointmentTaxTran Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? recordID,
      string taxID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentTaxTran) PrimaryKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.srvOrdType, FSAppointmentTaxTran.refNbr, FSAppointmentTaxTran.recordID, FSAppointmentTaxTran.taxID>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) recordID, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.srvOrdType, FSAppointmentTaxTran.refNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.curyInfoID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<FSAppointmentTaxTran>.By<FSAppointmentTaxTran.taxID>
    {
    }
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentTaxTran.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTaxTran.refNbr>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentTaxTran.recordID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTaxTran.taxID>
  {
  }

  public abstract class jurisType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTaxTran.jurisType>
  {
  }

  public abstract class jurisName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTaxTran.jurisName>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTaxTran.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAppointmentTaxTran.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTaxTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTaxTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTaxTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentTaxTran.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentTaxTran.curyExpenseAmt>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentTaxTran.taxZoneID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentTaxTran.Tstamp>
  {
  }

  public abstract class isTaxInclusive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentTaxTran.isTaxInclusive>
  {
  }
}
