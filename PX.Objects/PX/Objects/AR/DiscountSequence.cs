// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountSequence
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable discount sequence based on a
/// <see cref="T:PX.Objects.AR.ARDiscount">discount code</see>. A discount sequence specifies
/// exactly how the discount is calculated, based on the amount or quantity of
/// the line item, or on the amount of the document. In contrast to discount
/// codes, which simply define discount types, discount sequences based on a
/// given code specify exactly to which specific entities the discount
/// will apply. For example, if a discount code defines a line-level discount
/// applicable to specific inventory items, discount sequences based on it
/// will each define inventory items to which they apply. The entities of
/// this type can be edited on the Discounts (AR209500) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (ARDiscountSequenceMaint))]
[PXCacheName("Discount Sequence")]
[Serializable]
public class DiscountSequence : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _DiscountSequenceID;
  protected int? _LineCntr;
  protected string _Description;
  protected string _DiscountedFor;
  protected string _BreakBy;
  protected bool? _IsPromotion;
  protected bool? _IsActive;
  protected bool? _Prorate;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected DateTime? _UpdateDate;
  protected int? _FreeItemID;
  protected int? _PendingFreeItemID;
  protected int? _LastFreeItemID;
  protected bool? _ShowFreeItem;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXSelector(typeof (ARDiscount.discountID))]
  [PXUIField]
  [PXParent(typeof (Select<ARDiscount, Where<ARDiscount.discountID, Equal<Current<DiscountSequence.discountID>>>>))]
  [PXReferentialIntegrityCheck]
  [PXFieldDescription]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDBDefault(typeof (ARDiscount.lastNumber), DefaultForUpdate = false)]
  [PXUIField]
  [PXSelector(typeof (Search<DiscountSequence.discountSequenceID, Where<DiscountSequence.discountID, Equal<Current<DiscountSequence.discountID>>>>), new Type[] {typeof (DiscountSequence.discountID), typeof (DiscountSequence.discountSequenceID), typeof (DiscountSequence.isActive), typeof (DiscountSequence.description)})]
  [PXFieldDescription]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [DiscountOption.List]
  [PXUIField]
  public virtual string DiscountedFor
  {
    get => this._DiscountedFor;
    set => this._DiscountedFor = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [BreakdownType.List]
  [PXUIField]
  public virtual string BreakBy
  {
    get => this._BreakBy;
    set => this._BreakBy = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsPromotion
  {
    get => this._IsPromotion;
    set => this._IsPromotion = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Prorate
  {
    get => this._Prorate;
    set => this._Prorate = value;
  }

  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? UpdateDate
  {
    get => this._UpdateDate;
    set => this._UpdateDate = value;
  }

  [Inventory]
  [PXForeignReference(typeof (Field<DiscountSequence.freeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? FreeItemID
  {
    get => this._FreeItemID;
    set => this._FreeItemID = value;
  }

  [Inventory]
  [PXForeignReference(typeof (Field<DiscountSequence.pendingFreeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? PendingFreeItemID
  {
    get => this._PendingFreeItemID;
    set => this._PendingFreeItemID = value;
  }

  [Inventory]
  [PXForeignReference(typeof (Field<DiscountSequence.lastFreeItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LastFreeItemID
  {
    get => this._LastFreeItemID;
    set => this._LastFreeItemID = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? ShowFreeItem
  {
    get => new bool?(this.DiscountedFor == "F");
    set
    {
    }
  }

  [PXNote(DescriptionField = typeof (DiscountSequence.discountSequenceID), Selector = typeof (Search<DiscountSequence.discountSequenceID>))]
  public virtual Guid? NoteID { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>
  {
    public static DiscountSequence Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      PKFindOptions options = 0)
    {
      return (DiscountSequence) PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.FindBy(graph, (object) discountID, (object) discountSequenceID, options);
    }
  }

  public static class FK
  {
    public class FreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DiscountSequence>.By<DiscountSequence.freeItemID>
    {
    }

    public class PendingFreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DiscountSequence>.By<DiscountSequence.pendingFreeItemID>
    {
    }

    public class LastFreeItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DiscountSequence>.By<DiscountSequence.lastFreeItemID>
    {
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountSequence.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequence.discountSequenceID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountSequence.lineCntr>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountSequence.description>
  {
  }

  public abstract class discountedFor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequence.discountedFor>
  {
  }

  public abstract class breakBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountSequence.breakBy>
  {
  }

  public abstract class isPromotion : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequence.isPromotion>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequence.isActive>
  {
  }

  public abstract class prorate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequence.prorate>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DiscountSequence.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DiscountSequence.endDate>
  {
  }

  public abstract class updateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequence.updateDate>
  {
  }

  public abstract class freeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountSequence.freeItemID>
  {
  }

  public abstract class pendingFreeItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DiscountSequence.pendingFreeItemID>
  {
  }

  public abstract class lastFreeItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountSequence.lastFreeItemID>
  {
  }

  public abstract class showFreeItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DiscountSequence.showFreeItem>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DiscountSequence.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DiscountSequence.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DiscountSequence.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequence.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequence.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountSequence.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountSequence.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountSequence.lastModifiedDateTime>
  {
  }
}
