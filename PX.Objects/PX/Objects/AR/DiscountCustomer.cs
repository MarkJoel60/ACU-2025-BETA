// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountCustomer
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
/// For <see cref="T:PX.Objects.AR.DiscountSequence">discount sequences</see> applicable to certain
/// <see cref="T:PX.Objects.AR.Customer">customers</see>, records of this type define specific
/// customers to which the corresponding sequence applies. The entities of
/// this type can be edited on the Customers tab of the Discounts (AR209500)
/// form, which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXCacheName("Discount for Customer")]
[Serializable]
public class DiscountCustomer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected int? _CustomerID;
  protected string _DiscountSequenceID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountID))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDefault]
  [Customer(IsKey = true)]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountCustomer.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountCustomer.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public class PK : 
    PrimaryKeyOf<DiscountCustomer>.By<DiscountCustomer.discountID, DiscountCustomer.discountSequenceID, DiscountCustomer.customerID>
  {
    public static DiscountCustomer Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      int? customerID,
      PKFindOptions options = 0)
    {
      return (DiscountCustomer) PrimaryKeyOf<DiscountCustomer>.By<DiscountCustomer.discountID, DiscountCustomer.discountSequenceID, DiscountCustomer.customerID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) customerID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<DiscountCustomer>.By<DiscountCustomer.customerID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<DiscountCustomer>.By<DiscountCustomer.discountID, DiscountCustomer.discountSequenceID>
    {
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountCustomer.discountID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountCustomer.customerID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomer.discountSequenceID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DiscountCustomer.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DiscountCustomer.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomer.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountCustomer.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountCustomer.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomer.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountCustomer.lastModifiedDateTime>
  {
  }
}
