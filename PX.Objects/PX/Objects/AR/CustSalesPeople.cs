// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustSalesPeople
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A salesperson associated with a customer location. The entity implements the
/// many-to-many relationship between <see cref="T:PX.Objects.CR.Location">customer locations</see>
/// and <see cref="T:PX.Objects.AR.SalesPerson">salespeople</see>. For a salesperson, records
/// of this type allow to specify different default commission percentages for
/// different customer locations. The entities of this type can be edited on the
/// Salespersons (AR205000) form, which corresponds to the <see cref="T:PX.Objects.AR.SalesPersonMaint" />
/// graph. They can also be edited on the Salespersons tab of the Customer (AR303000) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.CustomerMaint" /> graph.
/// </summary>
/// <remarks>
/// When a user specifies a <see cref="T:PX.Objects.AR.SalesPerson">salesperson</see> in an invoice line, the
/// system looks for a <see cref="T:PX.Objects.AR.CustSalesPeople" /> record that corresponds
/// to the customer and location specified in the invoice, and defaults the value of
/// <see cref="P:PX.Objects.AR.ARTran.CommnPct" /> from the value of <see cref="P:PX.Objects.AR.CustSalesPeople.CommisionPct" />. If such
/// a record does not exist, the system takes the <see cref="P:PX.Objects.AR.SalesPerson.CommnPct">
/// default commission percentage of the salesperson</see>.
/// </remarks>
[PXCacheName("Customer Salespersons")]
[PXGroupMask(typeof (InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<CustSalesPeople.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class CustSalesPeople : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SalesPersonID;
  protected int? _BAccountID;
  protected int? _LocationID;
  protected bool? _IsDefault;
  protected Decimal? _CommisionPct;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The integer identifier of the salesperson. This field is a part
  /// of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.SalesPerson.SalesPersonID" /> field.
  /// </value>
  [PXDBDefault(typeof (SalesPerson.salesPersonID))]
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<CustSalesPeople.salesPersonID>>>>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  /// <summary>
  /// The integer identifier of the customer. This field is a part
  /// of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [Customer(DescriptionField = typeof (Customer.acctName), IsKey = true)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The integer identifier of the customer location. This
  /// field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>>), IsKey = true, DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(typeof (Search<Customer.defLocationID, Where<Customer.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>>>))]
  [PXParent(typeof (Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<CustSalesPeople.locationID>>>>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>
  /// Indicates (if set to <c>true</c>) that the salesperson
  /// is used by default for the customer location, which is defined
  /// by the <see cref="P:PX.Objects.AR.CustSalesPeople.BAccountID" /> and <see cref="P:PX.Objects.AR.CustSalesPeople.LocationID" />
  /// fields.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  /// <summary>
  /// The default sales commission percentage received
  /// by the <see cref="P:PX.Objects.AR.CustSalesPeople.SalesPersonID">salesperson</see> for the
  /// specified <see cref="P:PX.Objects.AR.CustSalesPeople.BAccountID">customer</see> and
  /// <see cref="P:PX.Objects.AR.CustSalesPeople.LocationID">location</see>.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(typeof (SalesPerson.commnPct))]
  [PXUIField(DisplayName = "Commission %")]
  public virtual Decimal? CommisionPct
  {
    get => this._CommisionPct;
    set => this._CommisionPct = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
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

  public class PK : 
    PrimaryKeyOf<CustSalesPeople>.By<CustSalesPeople.bAccountID, CustSalesPeople.salesPersonID, CustSalesPeople.locationID>
  {
    public static CustSalesPeople Find(
      PXGraph graph,
      int? bAccountID,
      int? salesPersonID,
      int? locationID,
      PKFindOptions options = 0)
    {
      return (CustSalesPeople) PrimaryKeyOf<CustSalesPeople>.By<CustSalesPeople.bAccountID, CustSalesPeople.salesPersonID, CustSalesPeople.locationID>.FindBy(graph, (object) bAccountID, (object) salesPersonID, (object) locationID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<CustSalesPeople>.By<CustSalesPeople.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<CustSalesPeople>.By<CustSalesPeople.bAccountID, CustSalesPeople.locationID>
    {
    }
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustSalesPeople.salesPersonID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustSalesPeople.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustSalesPeople.locationID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustSalesPeople.isDefault>
  {
  }

  public abstract class commisionPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CustSalesPeople.commisionPct>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CustSalesPeople.Tstamp>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustSalesPeople.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustSalesPeople.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustSalesPeople.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustSalesPeople.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustSalesPeople.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustSalesPeople.lastModifiedDateTime>
  {
  }
}
