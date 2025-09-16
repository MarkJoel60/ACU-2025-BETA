// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorDiscountSequence
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// A specific discount sequence based on a discount code.
/// The discount sequence specifies how the discount is calculated
/// based on the amount or quantity of the line item, or on the amount of the document.
/// Discount sequences can be edited on the Vendor Discounts (AP205000) form,
/// which corresponds to the <see cref="T:PX.Objects.AP.APDiscountSequenceMaint" /> graph.
/// </summary>
[PXProjection(typeof (Select2<DiscountSequence, InnerJoin<APDiscount, On<DiscountSequence.discountID, Equal<APDiscount.discountID>>>>), new System.Type[] {typeof (DiscountSequence)})]
[PXPrimaryGraph(typeof (APDiscountSequenceMaint))]
[Serializable]
public class VendorDiscountSequence : DiscountSequence
{
  protected int? _VendorID;

  [Vendor(IsKey = true, BqlField = typeof (APDiscount.bAccountID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.bAccountID, Equal<Current<VendorDiscountSequence.vendorID>>>>))]
  [PXUIField(DisplayName = "Discount Code", Visibility = PXUIVisibility.SelectorVisible)]
  [PXParent(typeof (Select<APDiscount, Where<APDiscount.discountID, Equal<Current<VendorDiscountSequence.discountID>>>>))]
  [PXReferentialIntegrityCheck]
  [PXFieldDescription]
  public override 
  #nullable disable
  string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDBDefault(typeof (APDiscount.lastNumber), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Sequence", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
  [PXSelector(typeof (Search<VendorDiscountSequence.discountSequenceID, Where<VendorDiscountSequence.discountID, Equal<Current<VendorDiscountSequence.discountID>>>>))]
  [PXFieldDescription]
  public override string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBTimestamp]
  public override byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public new class PK : 
    PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>
  {
    public static VendorDiscountSequence Find(
      PXGraph graph,
      int? vendorID,
      string discountID,
      string discountSequenceID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.FindBy(graph, (object) vendorID, (object) discountID, (object) discountSequenceID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>
  {
    public static VendorDiscountSequence Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.FindBy(graph, (object) discountID, (object) discountSequenceID, options);
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorDiscountSequence.vendorID>
  {
  }

  public new abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.discountID>
  {
  }

  public new abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.discountSequenceID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorDiscountSequence.lineCntr>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.description>
  {
  }

  public new abstract class discountedFor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.discountedFor>
  {
  }

  public new abstract class breakBy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.breakBy>
  {
  }

  public new abstract class isPromotion : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorDiscountSequence.isPromotion>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorDiscountSequence.isActive>
  {
  }

  public new abstract class prorate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorDiscountSequence.prorate>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorDiscountSequence.startDate>
  {
  }

  public new abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorDiscountSequence.endDate>
  {
  }

  public new abstract class updateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorDiscountSequence.updateDate>
  {
  }

  public new abstract class freeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorDiscountSequence.freeItemID>
  {
  }

  public new abstract class pendingFreeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorDiscountSequence.pendingFreeItemID>
  {
  }

  public new abstract class lastFreeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorDiscountSequence.lastFreeItemID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VendorDiscountSequence.noteID>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    VendorDiscountSequence.Tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    VendorDiscountSequence.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorDiscountSequence.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    VendorDiscountSequence.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorDiscountSequence.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorDiscountSequence.lastModifiedDateTime>
  {
  }
}
