// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPrintCheckDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("Print Check Detail")]
[Serializable]
public class APPrintCheckDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _adjdDocType;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [APPaymentType.List]
  [PXDBDefault(typeof (APPayment.docType))]
  public virtual string AdjgDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APPayment.refNbr))]
  [PXParent(typeof (Select<APPayment, Where<APPayment.docType, Equal<Current<APPrintCheckDetail.adjgDocType>>, And<APPayment.refNbr, Equal<Current<APPrintCheckDetail.adjgRefNbr>>>>>))]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  public string Source { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [APPaymentType.List]
  [PXDBDefault(typeof (APPayment.docType))]
  public virtual string AdjdDocType
  {
    get => this._adjdDocType;
    set
    {
      this._adjdDocType = value;
      while (true)
      {
        string adjdDocType = this._adjdDocType;
        if ((adjdDocType != null ? (adjdDocType.Length < 3 ? 1 : 0) : 0) != 0)
          this._adjdDocType += " ";
        else
          break;
      }
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public string StubNbr { get; set; }

  [PXDBInt]
  public int? CashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public string PaymentMethodID { get; set; }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "PO", CuryIDField = "AdjgCuryID")]
  public virtual long? AdjgCuryInfoID { get; set; }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(ModuleCode = "PO", CuryIDField = "AdjdCuryID")]
  public virtual long? AdjdCuryInfoID { get; set; }

  [PXDBCurrency(typeof (APPrintCheckDetail.adjgCuryInfoID), typeof (APPrintCheckDetail.outstandingBalance))]
  public Decimal? CuryOutstandingBalance { get; set; }

  [PXDBDecimal(4)]
  public Decimal? OutstandingBalance { get; set; }

  [PXDBDate]
  public System.DateTime? OutstandingBalanceDate { get; set; }

  [PXDBCurrency(typeof (APPrintCheckDetail.adjgCuryInfoID), typeof (APPrintCheckDetail.adjgAmt))]
  public virtual Decimal? CuryAdjgAmt { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? AdjgAmt { get; set; }

  [PXDBCurrency(typeof (APPrintCheckDetail.adjgCuryInfoID), typeof (APPrintCheckDetail.adjgDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgDiscAmt { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? AdjgDiscAmt { get; set; }

  [PXDBCurrency(typeof (APPrintCheckDetail.adjdCuryInfoID), typeof (APPrintCheckDetail.extraDocBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtraDocBal { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? ExtraDocBal { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPrintCheckDetail>.By<APPrintCheckDetail.adjgDocType, APPrintCheckDetail.adjgRefNbr, APPrintCheckDetail.source, APPrintCheckDetail.adjdDocType, APPrintCheckDetail.adjdRefNbr>
  {
    public static APPrintCheckDetail Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      string source,
      string adjdDocType,
      string adjdRefNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPrintCheckDetail>.By<APPrintCheckDetail.adjgDocType, APPrintCheckDetail.adjgRefNbr, APPrintCheckDetail.source, APPrintCheckDetail.adjdDocType, APPrintCheckDetail.adjdRefNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) source, (object) adjdDocType, (object) adjdRefNbr, options);
    }
  }

  public abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetail.adjgDocType>
  {
    public const int Length = 3;
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPrintCheckDetail.adjgRefNbr>
  {
    public const int Length = 15;
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPrintCheckDetail.source>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetail.adjdDocType>
  {
    public const int Length = 3;
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPrintCheckDetail.adjdRefNbr>
  {
    public const int Length = 15;
  }

  public abstract class stubNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPrintCheckDetail.stubNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPrintCheckDetail.cashAccountID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetail.paymentMethodID>
  {
  }

  public abstract class adjgCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    APPrintCheckDetail.adjgCuryInfoID>
  {
  }

  public abstract class adjdCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    APPrintCheckDetail.adjdCuryInfoID>
  {
  }

  public abstract class curyOutstandingBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.curyOutstandingBalance>
  {
  }

  public abstract class outstandingBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.outstandingBalance>
  {
  }

  public abstract class outstandingBalanceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPrintCheckDetail.outstandingBalanceDate>
  {
  }

  public abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.curyAdjgAmt>
  {
  }

  public abstract class adjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPrintCheckDetail.adjgAmt>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.curyAdjgDiscAmt>
  {
  }

  public abstract class adjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.adjgDiscAmt>
  {
  }

  public abstract class curyExtraDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.curyExtraDocBal>
  {
  }

  public abstract class extraDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPrintCheckDetail.extraDocBal>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPrintCheckDetail.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APPrintCheckDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetail.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APPrintCheckDetail.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APPrintCheckDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPrintCheckDetail.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APPrintCheckDetail.Tstamp>
  {
  }
}
