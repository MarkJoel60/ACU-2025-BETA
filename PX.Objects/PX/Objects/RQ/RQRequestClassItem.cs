// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestClassItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXPrimaryGraph(typeof (RQRequestClassMaint))]
[Serializable]
public class RQRequestClassItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineID;
  protected 
  #nullable disable
  string _ReqClassID;
  protected int? _InventoryID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (RQRequestClass.reqClassID))]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
  [PXParent(typeof (RQRequestClassItem.FK.RequestClass))]
  public virtual string ReqClassID
  {
    get => this._ReqClassID;
    set => this._ReqClassID = value;
  }

  [PXDefault]
  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search<RQInventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (RQInventoryItem.inventoryCD), DescriptionField = typeof (RQInventoryItem.descr))]
  [PXForeignReference(typeof (RQRequestClassItem.FK.InventoryItem))]
  [PXUIField(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
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
    PrimaryKeyOf<RQRequestClassItem>.By<RQRequestClassItem.reqClassID, RQRequestClassItem.lineID>
  {
    public static RQRequestClassItem Find(
      PXGraph graph,
      string reqClassID,
      int? lineID,
      PKFindOptions options = 0)
    {
      return (RQRequestClassItem) PrimaryKeyOf<RQRequestClassItem>.By<RQRequestClassItem.reqClassID, RQRequestClassItem.lineID>.FindBy(graph, (object) reqClassID, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class RequestClass : 
      PrimaryKeyOf<RQRequestClass>.By<RQRequestClass.reqClassID>.ForeignKeyOf<RQRequestClassItem>.By<RQRequestClassItem.reqClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RQRequestClassItem>.By<RQRequestClassItem.inventoryID>
    {
    }
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestClassItem.lineID>
  {
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestClassItem.reqClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestClassItem.inventoryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequestClassItem.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequestClassItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClassItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestClassItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequestClassItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestClassItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestClassItem.lastModifiedDateTime>
  {
  }
}
