// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// A service table that is used to maintain the numbers of already printed check forms
/// to avoid double count of the used numbers.
/// </summary>
[PXCacheName("Cash Account Check")]
[Serializable]
public class CashAccountCheck : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Cash Account ID", Visible = false)]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (PaymentMethod.paymentMethodID))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBIdentity]
  public virtual int? CashAccountCheckID { get; set; }

  [PXDBString(40, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Check Number")]
  [PXDefault]
  [PXParent(typeof (Select<APAdjust, Where<APAdjust.stubNbr, Equal<Current<CashAccountCheck.checkNbr>>, And<APAdjust.paymentMethodID, Equal<Current<CashAccountCheck.paymentMethodID>>, And<APAdjust.cashAccountID, Equal<Current<CashAccountCheck.cashAccountID>>, And<APAdjust.voided, NotEqual<True>>>>>>))]
  public virtual string CheckNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [APDocType.List]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AP.APPayment.refNbr, Where<PX.Objects.AP.APPayment.refNbr, Equal<Current<CashAccountCheck.refNbr>>, And<PX.Objects.AP.APPayment.docType, Equal<Current<CashAccountCheck.docType>>>>>), ValidateValue = false)]
  public virtual string RefNbr { get; set; }

  [PXDBString(6, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [FinPeriodIDFormatting]
  public virtual string FinPeriodID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXDefault]
  [Vendor]
  public virtual int? VendorID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<CashAccountCheck>.By<CashAccountCheck.cashAccountID, CashAccountCheck.paymentMethodID, CashAccountCheck.checkNbr>
  {
    public static CashAccountCheck Find(
      PXGraph graph,
      int? cashAccountID,
      string paymentMethodID,
      string checkNbr,
      PKFindOptions options = 0)
    {
      return (CashAccountCheck) PrimaryKeyOf<CashAccountCheck>.By<CashAccountCheck.cashAccountID, CashAccountCheck.paymentMethodID, CashAccountCheck.checkNbr>.FindBy(graph, (object) cashAccountID, (object) paymentMethodID, (object) checkNbr, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountCheck>.By<CashAccountCheck.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CashAccountCheck>.By<CashAccountCheck.paymentMethodID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<CashAccountCheck>.By<CashAccountCheck.vendorID>
    {
    }

    public class APPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<CashAccountCheck>.By<CashAccountCheck.docType, CashAccountCheck.refNbr>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccountCheck.cashAccountID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountCheck.paymentMethodID>
  {
  }

  public abstract class cashAccountCheckID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccountCheck.cashAccountCheckID>
  {
  }

  public abstract class checkNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccountCheck.checkNbr>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccountCheck.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccountCheck.refNbr>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccountCheck.finPeriodID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CashAccountCheck.docDate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccountCheck.vendorID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CashAccountCheck.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CashAccountCheck.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountCheck.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccountCheck.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CashAccountCheck.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountCheck.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccountCheck.lastModifiedDateTime>
  {
  }
}
