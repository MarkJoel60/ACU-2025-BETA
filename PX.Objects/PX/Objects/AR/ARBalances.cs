// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARBalances
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Overrides.ARDocumentRelease;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A balance record of an accounts receivable customer.
/// Customer balances are accumulated into records across the
/// following dimensions: branch, customer, and customer location.
/// The balance records are created and updated by the <see cref="T:PX.Objects.AR.ARDocumentRelease" />
/// graph during the document release process.
/// </summary>
[ARBalAccum]
[PXCacheName("AR Balance")]
[Serializable]
public class ARBalances : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _CustomerLocationID;
  protected 
  #nullable disable
  string _CuryID;
  protected Decimal? _CurrentBal;
  protected Decimal? _UnreleasedBal;
  protected Decimal? _TotalPrepayments;
  protected Decimal? _TotalQuotations;
  protected Decimal? _TotalOpenOrders;
  protected Decimal? _TotalShipped;
  protected DateTime? _LastInvoiceDate;
  protected DateTime? _OldInvoiceDate;
  protected int? _NumberInvoicePaid;
  protected int? _PaidInvoiceDays;
  protected int? _AverageDaysToPay;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected bool? _DatesUpdated;

  [PXDBInt(IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARBalances.customerID>>>>))]
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurrentBal
  {
    get => this._CurrentBal;
    set => this._CurrentBal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnreleasedBal
  {
    get => this._UnreleasedBal;
    set => this._UnreleasedBal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalPrepayments
  {
    get => this._TotalPrepayments;
    set => this._TotalPrepayments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalQuotations
  {
    get => this._TotalQuotations;
    set => this._TotalQuotations = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalOpenOrders
  {
    get => this._TotalOpenOrders;
    set => this._TotalOpenOrders = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalShipped
  {
    get => this._TotalShipped;
    set => this._TotalShipped = value;
  }

  [PXDBDate]
  public virtual DateTime? LastInvoiceDate
  {
    get => this._LastInvoiceDate;
    set => this._LastInvoiceDate = value;
  }

  [PXDBDate]
  public virtual DateTime? OldInvoiceDate
  {
    get => this._OldInvoiceDate;
    set => this._OldInvoiceDate = value;
  }

  [PXDBInt]
  public virtual int? NumberInvoicePaid
  {
    get => this._NumberInvoicePaid;
    set => this._NumberInvoicePaid = value;
  }

  [PXDBInt]
  public virtual int? PaidInvoiceDays
  {
    get => this._PaidInvoiceDays;
    set => this._PaidInvoiceDays = value;
  }

  [PXDBInt]
  public virtual int? AverageDaysToPay
  {
    get => this._AverageDaysToPay;
    set => this._AverageDaysToPay = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? DatesUpdated
  {
    get => this._DatesUpdated;
    set => this._DatesUpdated = value;
  }

  [PXDBDate]
  public virtual DateTime? LastDocDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? StatementRequired { get; set; }

  public class PK : 
    PrimaryKeyOf<ARBalances>.By<ARBalances.branchID, ARBalances.customerID, ARBalances.customerLocationID>
  {
    public static ARBalances Find(
      PXGraph graph,
      int? branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARBalances) PrimaryKeyOf<ARBalances>.By<ARBalances.branchID, ARBalances.customerID, ARBalances.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARBalances>.By<ARBalances.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARBalances>.By<ARBalances.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARBalances>.By<ARBalances.customerID, ARBalances.customerLocationID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalances.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalances.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARBalances.customerLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARBalances.curyID>
  {
  }

  public abstract class currentBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARBalances.currentBal>
  {
  }

  public abstract class unreleasedBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARBalances.unreleasedBal>
  {
  }

  public abstract class totalPrepayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalances.totalPrepayments>
  {
  }

  public abstract class totalQuotations : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalances.totalQuotations>
  {
  }

  public abstract class totalOpenOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalances.totalOpenOrders>
  {
  }

  public abstract class totalShipped : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARBalances.totalShipped>
  {
  }

  public abstract class lastInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARBalances.lastInvoiceDate>
  {
  }

  public abstract class oldInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARBalances.oldInvoiceDate>
  {
  }

  public abstract class numberInvoicePaid : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalances.numberInvoicePaid>
  {
  }

  public abstract class paidInvoiceDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalances.paidInvoiceDays>
  {
  }

  public abstract class averageDaysToPay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalances.averageDaysToPay>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARBalances.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARBalances.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARBalances.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARBalances.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARBalances.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARBalances.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARBalances.Tstamp>
  {
  }

  public abstract class datesUpdated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARBalances.datesUpdated>
  {
  }

  public abstract class lastDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARBalances.lastDocDate>
  {
  }

  public abstract class statementRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARBalances.statementRequired>
  {
  }
}
