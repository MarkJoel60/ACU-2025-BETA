// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetailAcum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CT;

[ContractDetailAccum]
[PXBreakInheritance]
[Serializable]
public class ContractDetailAcum : ContractDetail
{
  [PXDBInt(IsKey = true)]
  public override int? ContractDetailID
  {
    get => this._ContractDetailID;
    set => this._ContractDetailID = value;
  }

  [PXDBInt]
  public override int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  public override int? RevID { get; set; }

  [PXDBInt]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public override 
  #nullable disable
  string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault("N")]
  [PXUIField(DisplayName = "Reset Usage")]
  [PXDBString(1, IsFixed = true)]
  [ResetUsageOption.List]
  public override string ResetUsage
  {
    get => this._ResetUsage;
    set => this._ResetUsage = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? Used
  {
    get => this._Used;
    set => this._Used = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? UsedTotal
  {
    get => this._UsedTotal;
    set => this._UsedTotal = value;
  }

  [PXDefault(typeof (Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<ContractDetail.inventoryID>>>>))]
  [INUnit(typeof (ContractDetail.inventoryID))]
  public override string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Billed Date", Enabled = false)]
  public override DateTime? LastBilledDate
  {
    get => this._LastBilledDate;
    set => this._LastBilledDate = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Billed Qty.", Enabled = false)]
  public override Decimal? LastBilledQty
  {
    get => this._LastBilledQty;
    set => this._LastBilledQty = value;
  }

  [PXDBTimestamp]
  public override byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public override Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public override string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public override DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public override Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public override string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public override DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public new abstract class contractDetailID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractDetailAcum.contractDetailID>
  {
  }

  public new abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailAcum.contractID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailAcum.lineNbr>
  {
  }

  public new abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailAcum.revID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetailAcum.inventoryID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetailAcum.description>
  {
  }

  public new abstract class resetUsage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetailAcum.resetUsage>
  {
  }

  public new abstract class included : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailAcum.included>
  {
  }

  public new abstract class used : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetailAcum.used>
  {
  }

  public new abstract class usedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailAcum.usedTotal>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractDetailAcum.uOM>
  {
  }

  public new abstract class lastBilledDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetailAcum.lastBilledDate>
  {
  }

  public new abstract class lastBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetailAcum.lastBilledQty>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractDetailAcum.Tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractDetailAcum.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetailAcum.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetailAcum.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractDetailAcum.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetailAcum.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetailAcum.lastModifiedDateTime>
  {
  }
}
