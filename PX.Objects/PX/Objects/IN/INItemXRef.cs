// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemXRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Cross-Reference")]
[Serializable]
public class INItemXRef : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _AlternateType;
  protected int? _BAccountID;
  protected string _AlternateID;
  protected string _Descr;
  protected string _UOM;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [Inventory(IsKey = true, DirtyRead = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID), DefaultForInsert = false, DefaultForUpdate = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (INItemXRef.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(4, IsKey = true)]
  [INAlternateType.List]
  [PXDefault("GLBL")]
  [PXUIField(DisplayName = "Alternate Type")]
  public virtual string AlternateType
  {
    get => this._AlternateType;
    set => this._AlternateType = value;
  }

  [PXDefault]
  [PXRestrictor(typeof (Where<BAccountR.type, In3<BAccountType.customerType, BAccountType.vendorType, BAccountType.combinedType>, And<Where<Optional<INItemXRef.alternateType>, Equal<INAlternateType.vPN>, Or<Optional<INItemXRef.alternateType>, Equal<INAlternateType.cPN>>>>>), "The alternate type for '{0}' is '{1}', which does not match the selected alternate type.", new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.type)}, ShowWarning = true)]
  [PXRestrictor(typeof (Where<BAccountR.type, In3<BAccountType.vendorType, BAccountType.combinedType>, And<BAccountR.vStatus, NotEqual<VendorStatus.inactive>, And<Optional<INItemXRef.alternateType>, Equal<INAlternateType.vPN>, Or<Optional<INItemXRef.alternateType>, Equal<INAlternateType.cPN>>>>>), "The Vendor is inactive.", new System.Type[] {}, ShowWarning = true)]
  [PXRestrictor(typeof (Where<BAccountR.type, In3<BAccountType.customerType, BAccountType.combinedType>, And<BAccountR.status, NotEqual<CustomerStatus.inactive>, And<Optional<INItemXRef.alternateType>, Equal<INAlternateType.cPN>, Or<Optional<INItemXRef.alternateType>, Equal<INAlternateType.vPN>>>>>), "The Customer is inactive.", new System.Type[] {}, ShowWarning = true)]
  [INItemXRefBAccount(DisplayName = "Vendor/Customer", IsKey = true)]
  [PXParent(typeof (INItemXRef.FK.BAccount))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBString(50, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Alternate ID")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [INUnitXRef(typeof (INItemXRef.inventoryID), DirtyRead = true)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
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

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemXRef>.By<INItemXRef.inventoryID, INItemXRef.alternateType, INItemXRef.bAccountID, INItemXRef.alternateID, INItemXRef.subItemID>
  {
    public static INItemXRef Find(
      PXGraph graph,
      int? inventoryID,
      string alternateType,
      int? bAccountID,
      string alternateID,
      int? subItemID,
      PKFindOptions options = 0)
    {
      return (INItemXRef) PrimaryKeyOf<INItemXRef>.By<INItemXRef.inventoryID, INItemXRef.alternateType, INItemXRef.bAccountID, INItemXRef.alternateID, INItemXRef.subItemID>.FindBy(graph, (object) inventoryID, (object) alternateType, (object) bAccountID, (object) alternateID, (object) subItemID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemXRef>.By<INItemXRef.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemXRef>.By<INItemXRef.subItemID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<INItemXRef>.By<INItemXRef.bAccountID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemXRef.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemXRef.subItemID>
  {
  }

  public abstract class alternateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemXRef.alternateType>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemXRef.bAccountID>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemXRef.alternateID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemXRef.descr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemXRef.uOM>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemXRef.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemXRef.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemXRef.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemXRef.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemXRef.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemXRef.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemXRef.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemXRef.Tstamp>
  {
  }
}
