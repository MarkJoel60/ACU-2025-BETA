// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDiscount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Discount;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable discount code that is used to define
/// <see cref="T:PX.Objects.AR.DiscountSequence">discount sequences</see>. The primary function of a discount
/// code is to specify the type and applicability of the discount.
/// For example, a document discount can be applied to specific customers,
/// or a line discount can be applied to specific inventory items. The
/// entities of this type are edited on the Discount Codes (AR209000)
/// form, which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountMaint" /> graph.
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (ARDiscountSequenceMaint)}, new System.Type[] {typeof (Select<DiscountSequence, Where<DiscountSequence.discountID, Equal<Current<ARDiscount.discountID>>>>)})]
[PXCacheName("AR Discount")]
[Serializable]
public class ARDiscount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _Description;
  protected string _Type;
  protected string _ApplicableTo;
  protected bool? _IsManual;
  protected bool? _ExcludeFromDiscountableAmt;
  protected bool? _SkipDocumentDiscounts;
  protected bool? _IsAutoNumber;
  protected string _LastNumber;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [DiscountType.List]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("IN")]
  [PXStringList(new string[] {"CU", "CI", "CP", "CE", "PI", "PP", "CB", "PB", "WH", "WI", "WC", "WP", "WE", "BR", "IN", "IE", "UN"}, new string[] {"Customer", "Customer and Item", "Customer and Item Price Class", "Customer Price Class", "Customer Price Class and Item", "Customer Price Class and Item Price Class", "Customer and Branch", "Customer Price Class and Branch", "Warehouse", "Warehouse and Item", "Warehouse and Customer", "Warehouse and Item Price Class", "Warehouse and Customer Price Class", "Branch", "Item", "Item Price Class", "Unconditional"})]
  [PXUIField]
  public virtual string ApplicableTo
  {
    get => this._ApplicableTo;
    set => this._ApplicableTo = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Apply to Deferred Revenue")]
  public bool? IsAppliedToDR { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ExcludeFromDiscountableAmt
  {
    get => this._ExcludeFromDiscountableAmt;
    set => this._ExcludeFromDiscountableAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? SkipDocumentDiscounts
  {
    get => this._SkipDocumentDiscounts;
    set => this._SkipDocumentDiscounts = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsAutoNumber
  {
    get => this._IsAutoNumber;
    set => this._IsAutoNumber = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  public virtual string LastNumber
  {
    get => this._LastNumber;
    set => this._LastNumber = value;
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

  public class PK : PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>
  {
    public static ARDiscount Find(PXGraph graph, string discountID, PKFindOptions options = 0)
    {
      return (ARDiscount) PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.FindBy(graph, (object) discountID, options);
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDiscount.discountID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDiscount.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDiscount.type>
  {
  }

  public abstract class applicableTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDiscount.applicableTo>
  {
  }

  public abstract class isAppliedToDR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDiscount.isAppliedToDR>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDiscount.isManual>
  {
  }

  public abstract class excludeFromDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDiscount.excludeFromDiscountableAmt>
  {
  }

  public abstract class skipDocumentDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDiscount.skipDocumentDiscounts>
  {
  }

  public abstract class isAutoNumber : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDiscount.isAutoNumber>
  {
  }

  public abstract class lastNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDiscount.lastNumber>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARDiscount.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDiscount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDiscount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDiscount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDiscount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDiscount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDiscount.lastModifiedDateTime>
  {
  }
}
