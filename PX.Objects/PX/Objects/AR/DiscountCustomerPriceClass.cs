// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountCustomerPriceClass
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
/// <see cref="T:PX.Objects.AR.ARPriceClass">customer price classes</see>, records of this type define
/// specific customer price classes to which the corresponding sequence applies. The
/// entities of this type can be edited on the Customer Price Classes tab of the Discounts
/// (AR209500) form, which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXCacheName("Discount for Customer and Price Class")]
[Serializable]
public class DiscountCustomerPriceClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _CustomerPriceClassID;
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

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (ARPriceClass.priceClassID))]
  [PXUIField]
  public virtual string CustomerPriceClassID
  {
    get => this._CustomerPriceClassID;
    set => this._CustomerPriceClassID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountCustomerPriceClass.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountCustomerPriceClass.discountID>>>>>))]
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
    PrimaryKeyOf<DiscountCustomerPriceClass>.By<DiscountCustomerPriceClass.discountID, DiscountCustomerPriceClass.discountSequenceID, DiscountCustomerPriceClass.customerPriceClassID>
  {
    public static DiscountCustomerPriceClass Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      string customerPriceClassID,
      PKFindOptions options = 0)
    {
      return (DiscountCustomerPriceClass) PrimaryKeyOf<DiscountCustomerPriceClass>.By<DiscountCustomerPriceClass.discountID, DiscountCustomerPriceClass.discountSequenceID, DiscountCustomerPriceClass.customerPriceClassID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) customerPriceClassID, options);
    }
  }

  public static class FK
  {
    public class DiscountSequence : 
      PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<DiscountCustomerPriceClass>.By<DiscountCustomerPriceClass.discountID, DiscountCustomerPriceClass.discountSequenceID>
    {
    }

    public class CustomerPriceClass : 
      PrimaryKeyOf<ARPriceClass>.By<ARPriceClass.priceClassID>.ForeignKeyOf<DiscountCustomerPriceClass>.By<DiscountCustomerPriceClass.customerPriceClassID>
    {
    }
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomerPriceClass.discountID>
  {
  }

  public abstract class customerPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomerPriceClass.customerPriceClassID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomerPriceClass.discountSequenceID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    DiscountCustomerPriceClass.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountCustomerPriceClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomerPriceClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountCustomerPriceClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountCustomerPriceClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountCustomerPriceClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountCustomerPriceClass.lastModifiedDateTime>
  {
  }
}
