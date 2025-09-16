// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitSpecHdr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
[Serializable]
public class INKitSpecHdr : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _KitInventoryID;
  protected 
  #nullable disable
  string _RevisionID;
  protected string _Descr;
  protected int? _KitSubItemID;
  protected bool? _IsActive;
  protected bool? _AllowCompAddition;
  protected bool? _IsNonStock;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [Inventory]
  [PXRestrictor(typeof (Where<InventoryItem.kitItem, Equal<boolTrue>>), "The inventory item is not a kit.", new Type[] {})]
  [PXDefault]
  [PXFieldDescription]
  [PXParent(typeof (INKitSpecHdr.FK.KitInventoryItem))]
  public virtual int? KitInventoryID
  {
    get => this._KitInventoryID;
    set => this._KitInventoryID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INKitSpecHdr.revisionID, Where<INKitSpecHdr.kitInventoryID, Equal<Optional<INKitSpecHdr.kitInventoryID>>>>))]
  [PXFieldDescription]
  public virtual string RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [SubItem(typeof (INKitSpecHdr.kitInventoryID))]
  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<INKitSpecHdr.kitInventoryID>))]
  public virtual int? KitSubItemID
  {
    get => this._KitSubItemID;
    set => this._KitSubItemID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Component Addition")]
  public virtual bool? AllowCompAddition
  {
    get => this._AllowCompAddition;
    set => this._AllowCompAddition = value;
  }

  [PXBool]
  [PXDBScalar(typeof (Search<InventoryItem.stkItem, Where<InventoryItem.inventoryID, Equal<INKitSpecHdr.kitInventoryID>>>))]
  [PXDefault(typeof (Search<InventoryItem.stkItem, Where<InventoryItem.inventoryID, Equal<Current<INKitSpecHdr.kitInventoryID>>>>))]
  public virtual bool? IsStock { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Non-Stock", Enabled = false)]
  [PXDBCalced(typeof (IIf<Where<INKitSpecHdr.isStock, Equal<True>>, False, True>), typeof (bool))]
  public virtual bool? IsNonStock
  {
    [PXDependsOnFields(new Type[] {typeof (INKitSpecHdr.isStock)})] get
    {
      return new bool?(!this.IsStock.GetValueOrDefault());
    }
  }

  [PXNote(DescriptionField = typeof (INKitSpecHdr.revisionID), Selector = typeof (Search<INKitSpecHdr.revisionID>), FieldList = new Type[] {typeof (INKitSpecHdr.kitInventoryID), typeof (INKitSpecHdr.revisionID), typeof (INKitSpecHdr.descr)})]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  public class PK : 
    PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>
  {
    public static INKitSpecHdr Find(
      PXGraph graph,
      int? kitInventoryID,
      string revisionID,
      PKFindOptions options = 0)
    {
      return (INKitSpecHdr) PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>.FindBy(graph, (object) kitInventoryID, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class KitInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID>
    {
    }

    public class KitSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitSubItemID>
    {
    }
  }

  public abstract class kitInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecHdr.kitInventoryID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecHdr.revisionID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitSpecHdr.descr>
  {
  }

  public abstract class kitSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitSpecHdr.kitSubItemID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitSpecHdr.isActive>
  {
  }

  public abstract class allowCompAddition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INKitSpecHdr.allowCompAddition>
  {
  }

  public abstract class isStock : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitSpecHdr.isNonStock>
  {
  }

  public abstract class isNonStock : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitSpecHdr.isNonStock>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitSpecHdr.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitSpecHdr.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecHdr.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecHdr.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitSpecHdr.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitSpecHdr.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitSpecHdr.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitSpecHdr.Tstamp>
  {
  }
}
