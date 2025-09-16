// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDiscount
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
namespace PX.Objects.AP;

/// <summary>
/// An accounts payable discount code that is used to define discount sequences
/// (which are stored in <see cref="T:PX.Objects.AP.VendorDiscountSequence" />).
/// The APDiscount records are edited on the Vendor Discount Codes (AP204000) form,
/// which corresponds to the <see cref="T:PX.Objects.AP.APDiscountMaint" /> graph.
/// </summary>
[PXCacheName("AP Discount")]
[PXPrimaryGraph(new System.Type[] {typeof (APDiscountSequenceMaint)}, new System.Type[] {typeof (Select<VendorDiscountSequence, Where<VendorDiscountSequence.discountID, Equal<Current<APDiscount.discountID>>, And<VendorDiscountSequence.vendorID, Equal<Current<APDiscount.bAccountID>>>>>)})]
[Serializable]
public class APDiscount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;
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
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Vendor.bAccountID))]
  [PXParent(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APDiscount.bAccountID>>>>))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Discount Code", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [DiscountType.List]
  [PXUIField(DisplayName = "Discount Type", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("VI")]
  [PXStringList(new string[] {"VE", "VI", "VP", "VL", "LI"}, new string[] {"Unconditional", "Item", "Item Price Class", "Location", "Item and Location"})]
  [PXUIField(DisplayName = "Applicable To", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ApplicableTo
  {
    get => this._ApplicableTo;
    set => this._ApplicableTo = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual", Visibility = PXUIVisibility.Visible)]
  public virtual bool? IsManual
  {
    get => this._IsManual;
    set => this._IsManual = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Discountable Amount", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ExcludeFromDiscountableAmt
  {
    get => this._ExcludeFromDiscountableAmt;
    set => this._ExcludeFromDiscountableAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Document Discounts", Visibility = PXUIVisibility.Visible)]
  public virtual bool? SkipDocumentDiscounts
  {
    get => this._SkipDocumentDiscounts;
    set => this._SkipDocumentDiscounts = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Auto-Numbering", Visibility = PXUIVisibility.Visible)]
  public virtual bool? IsAutoNumber
  {
    get => this._IsAutoNumber;
    set => this._IsAutoNumber = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Last Number", Visibility = PXUIVisibility.Visible)]
  public virtual string LastNumber
  {
    get => this._LastNumber;
    set => this._LastNumber = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? CreatedDateTime
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime
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

  public class PK : PrimaryKeyOf<APDiscount>.By<APDiscount.discountID, APDiscount.bAccountID>
  {
    public static APDiscount Find(
      PXGraph graph,
      int? discountID,
      int? bAccountID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APDiscount>.By<APDiscount.discountID, APDiscount.bAccountID>.FindBy(graph, (object) discountID, (object) bAccountID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APDiscount>.By<APDiscount.bAccountID>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APDiscount.bAccountID>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscount.discountID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscount.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscount.type>
  {
  }

  public abstract class applicableTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscount.applicableTo>
  {
  }

  public abstract class isManual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APDiscount.isManual>
  {
  }

  public abstract class excludeFromDiscountableAmt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDiscount.excludeFromDiscountableAmt>
  {
  }

  public abstract class skipDocumentDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDiscount.skipDocumentDiscounts>
  {
  }

  public abstract class isAutoNumber : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APDiscount.isAutoNumber>
  {
  }

  public abstract class lastNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APDiscount.lastNumber>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APDiscount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APDiscount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDiscount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APDiscount.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APDiscount.Tstamp>
  {
  }
}
