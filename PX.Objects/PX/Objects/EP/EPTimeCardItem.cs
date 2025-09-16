// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCardItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Time Card Item")]
[Serializable]
public class EPTimeCardItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Description;
  protected string _UOM;
  protected Decimal? _Mon;
  protected Decimal? _Tue;
  protected Decimal? _Wed;
  protected Decimal? _Thu;
  protected Decimal? _Fri;
  protected Decimal? _Sat;
  protected Decimal? _Sun;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXForeignReference(typeof (EPTimeCardItem.FK.InventoryItem))]
  [PXDBDefault(typeof (EPTimeCard.timeCardCD))]
  [PXDBString(10, IsKey = true)]
  [PXUIField(Visible = false)]
  [PXParent(typeof (Select<EPTimeCard, Where<EPTimeCard.timeCardCD, Equal<Current<EPTimeCardItem.timeCardCD>>>>))]
  public virtual string TimeCardCD { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (EPTimeCard))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [ProjectDefault("TA", ForceProjectExplicitly = true)]
  [EPTimeCardProject]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPTimeCardItem.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPTimeCardItem.projectID), "TA", DisplayName = "Project Task")]
  [PXForeignReference(typeof (CompositeKey<Field<EPTimeCardItem.projectID>.IsRelatedTo<PMTask.projectID>, Field<EPTimeCardItem.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  [CostCode(null, typeof (EPTimeCardItem.taskID), "E", InventoryField = typeof (EPTimeCardItem.inventoryID), ProjectField = typeof (EPTimeCardItem.projectID), UseNewDefaulting = true)]
  public virtual int? CostCodeID { get; set; }

  [PXDefault]
  [NonStockItem]
  [PXForeignReference(typeof (Field<EPTimeCardItem.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.baseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<EPTimeCardItem.inventoryID>>>>))]
  [INUnit(typeof (EPTimeCardItem.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Mon")]
  public virtual Decimal? Mon
  {
    get => this._Mon;
    set => this._Mon = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Tue")]
  public virtual Decimal? Tue
  {
    get => this._Tue;
    set => this._Tue = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Wed")]
  public virtual Decimal? Wed
  {
    get => this._Wed;
    set => this._Wed = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Thu")]
  public virtual Decimal? Thu
  {
    get => this._Thu;
    set => this._Thu = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Fri")]
  public virtual Decimal? Fri
  {
    get => this._Fri;
    set => this._Fri = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Sat")]
  public virtual Decimal? Sat
  {
    get => this._Sat;
    set => this._Sat = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Sun")]
  public virtual Decimal? Sun
  {
    get => this._Sun;
    set => this._Sun = value;
  }

  [PXQuantity]
  [PXUIField(DisplayName = "Total Qty.", Enabled = false)]
  public virtual Decimal? TotalQty
  {
    get
    {
      Decimal? nullable = this.Mon;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.Tue;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.Wed;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num2 = num1 + valueOrDefault3;
      nullable = this.Thu;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault4;
      nullable = this.Fri;
      Decimal valueOrDefault5 = nullable.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault5;
      nullable = this.Sat;
      Decimal valueOrDefault6 = nullable.GetValueOrDefault();
      Decimal num5 = num4 + valueOrDefault6;
      nullable = this.Sun;
      Decimal valueOrDefault7 = nullable.GetValueOrDefault();
      return new Decimal?(num5 + valueOrDefault7);
    }
  }

  [PXDBInt]
  public virtual int? OrigLineNbr { get; set; }

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

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<EPTimeCardItem>.By<EPTimeCardItem.timeCardCD, EPTimeCardItem.lineNbr>
  {
    public static EPTimeCardItem Find(
      PXGraph graph,
      string timeCardCD,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (EPTimeCardItem) PrimaryKeyOf<EPTimeCardItem>.By<EPTimeCardItem.timeCardCD, EPTimeCardItem.lineNbr>.FindBy(graph, (object) timeCardCD, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Time Card</summary>
    public class Timecard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<EPTimeCardItem>.By<EPTimeCardItem.timeCardCD>
    {
    }

    /// <summary>Project</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<EPTimeCardItem>.By<EPTimeCardItem.projectID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<EPTimeCardItem>.By<EPTimeCardItem.projectID, EPTimeCardItem.taskID>
    {
    }

    /// <summary>Cost Code</summary>
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<EPTimeCardItem>.By<EPTimeCardItem.costCodeID>
    {
    }

    /// <summary>Item</summary>
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<EPTimeCardItem>.By<EPTimeCardItem.inventoryID>
    {
    }
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardItem.timeCardCD>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.lineNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.inventoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardItem.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardItem.uOM>
  {
  }

  public abstract class mon : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.mon>
  {
  }

  public abstract class tue : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.tue>
  {
  }

  public abstract class wed : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.wed>
  {
  }

  public abstract class thu : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.thu>
  {
  }

  public abstract class fri : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.fri>
  {
  }

  public abstract class sat : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.sat>
  {
  }

  public abstract class sun : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.sun>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPTimeCardItem.totalQty>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardItem.origLineNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCardItem.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPTimeCardItem.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCardItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCardItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPTimeCardItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCardItem.lastModifiedDateTime>
  {
  }
}
