// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEquipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EquipmentMaint))]
[PXCacheName("Equipment")]
[Serializable]
public class EPEquipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _EquipmentID;
  protected 
  #nullable disable
  string _EquipmentCD;
  protected string _Description;
  protected int? _FixedAssetID;
  protected string _CalendarID;
  protected int? _RunRateItemID;
  protected int? _SetupRateItemID;
  protected int? _SuspendRateItemID;
  protected Decimal? _RunRate;
  protected Decimal? _SetupRate;
  protected Decimal? _SuspendRate;
  protected int? _DefAccountGroupID;
  protected int? _DefaultAccountID;
  protected int? _DefaultSubID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  public virtual int? EquipmentID
  {
    get => this._EquipmentID;
    set => this._EquipmentID = value;
  }

  [PXUIField]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string EquipmentCD
  {
    get => this._EquipmentCD;
    set => this._EquipmentCD = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>Specifies (if set to <see langword="true"></see>) that the equipment is active.</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, NotEqual<FARecordType.classType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Fixed Asset")]
  public virtual int? FixedAssetID
  {
    get => this._FixedAssetID;
    set => this._FixedAssetID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Calendar")]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Run-Rate Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<EPEquipment.runRateItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? RunRateItemID
  {
    get => this._RunRateItemID;
    set => this._RunRateItemID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Setup-Rate Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<EPEquipment.runRateItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? SetupRateItemID
  {
    get => this._SetupRateItemID;
    set => this._SetupRateItemID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Suspend-Rate Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.nonStockItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXForeignReference(typeof (Field<EPEquipment.runRateItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? SuspendRateItemID
  {
    get => this._SuspendRateItemID;
    set => this._SuspendRateItemID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<InventoryItemCurySettings.stdCost, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<InventoryItemCurySettings.inventoryID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current<EPEquipment.runRateItemID>>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Run Rate")]
  public virtual Decimal? RunRate
  {
    get => this._RunRate;
    set => this._RunRate = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<InventoryItemCurySettings.stdCost, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<InventoryItemCurySettings.inventoryID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current<EPEquipment.setupRateItemID>>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Setup Rate")]
  public virtual Decimal? SetupRate
  {
    get => this._SetupRate;
    set => this._SetupRate = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<InventoryItemCurySettings.stdCost, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<InventoryItemCurySettings.inventoryID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current<EPEquipment.suspendRateItemID>>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>))]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Suspend Rate")]
  public virtual Decimal? SuspendRate
  {
    get => this._SuspendRate;
    set => this._SuspendRate = value;
  }

  [AccountGroup(DisplayName = "Default Account Group")]
  public virtual int? DefAccountGroupID
  {
    get => this._DefAccountGroupID;
    set => this._DefAccountGroupID = value;
  }

  [Account(DisplayName = "Default Account", AvoidControlAccounts = true)]
  public virtual int? DefaultAccountID
  {
    get => this._DefaultAccountID;
    set => this._DefaultAccountID = value;
  }

  [SubAccount]
  public virtual int? DefaultSubID
  {
    get => this._DefaultSubID;
    set => this._DefaultSubID = value;
  }

  [CRAttributesField(typeof (EPEquipment.classID))]
  public virtual string[] Attributes { get; set; }

  [PXString(20)]
  public string ClassID => "EQUIPMENT";

  [PXSearchable(64 /*0x40*/, "{0}", new System.Type[] {typeof (EPEquipment.equipmentCD)}, new System.Type[] {typeof (EPEquipment.description)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (EPEquipment.isActive)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (EPEquipment.description)})]
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class equipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.equipmentID>
  {
  }

  public abstract class equipmentCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipment.equipmentCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipment.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEquipment.isActive>
  {
  }

  public abstract class fixedAssetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.fixedAssetID>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipment.calendarID>
  {
  }

  public abstract class runRateItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.runRateItemID>
  {
  }

  public abstract class setupRateItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.setupRateItemID>
  {
  }

  public abstract class suspendRateItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipment.suspendRateItemID>
  {
  }

  public abstract class runRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPEquipment.runRate>
  {
  }

  public abstract class setupRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPEquipment.setupRate>
  {
  }

  public abstract class suspendRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPEquipment.suspendRate>
  {
  }

  public abstract class defAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEquipment.defAccountGroupID>
  {
  }

  public abstract class defaultAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.defaultAccountID>
  {
  }

  public abstract class defaultSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEquipment.defaultSubID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEquipment.classID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipment.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEquipment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEquipment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEquipment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEquipment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEquipment.lastModifiedDateTime>
  {
  }
}
