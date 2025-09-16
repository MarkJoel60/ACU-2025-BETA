// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LSSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public abstract class LSSelect
{
  public static DateTime? ExpireDateByLot(
  #nullable disable
  PXGraph graph, ILSMaster item, ILSMaster master)
  {
    int? nullable1;
    if (master != null && master.ExpireDate.HasValue)
    {
      short? invtMult = master.InvtMult;
      nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
        return master.ExpireDate;
    }
    PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial> pxResult = (PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>) PXResultset<INSite>.op_Implicit(PXSelectBase<INSite, PXViewOf<INSite>.BasedOn<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<InventoryItem>>, FbqlJoins.Left<INLotSerClass>.On<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<InventoryItem.lotSerClassID>>>, FbqlJoins.Left<INItemRep>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemRep.replenishmentClassID, Equal<INSite.replenishmentClassID>>>>, And<BqlOperand<INItemRep.inventoryID, IBqlInt>.IsEqual<InventoryItem.inventoryID>>>>.And<BqlOperand<INItemRep.curyID, IBqlString>.IsEqual<INSite.baseCuryID>>>>, FbqlJoins.Left<PX.Objects.IN.S.INItemSite>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.S.INItemSite.inventoryID, Equal<InventoryItem.inventoryID>>>>>.And<BqlOperand<PX.Objects.IN.S.INItemSite.siteID, IBqlInt>.IsEqual<INSite.siteID>>>>, FbqlJoins.Left<INItemLotSerial>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemLotSerial.inventoryID, Equal<InventoryItem.inventoryID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemLotSerial.lotSerialNbr, Equal<P.AsString>>>>>.And<BqlOperand<INItemLotSerial.expireDate, IBqlDateTime>.IsNotNull>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) item.LotSerialNbr,
      (object) item.InventoryID,
      (object) item.SiteID
    }));
    if (pxResult == null)
      return (DateTime?) master?.ExpireDate ?? item.ExpireDate;
    DateTime? nullable2 = new DateTime?();
    if (PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>.op_Implicit(pxResult).LotSerClassID == null || PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>.op_Implicit(pxResult).LotSerTrackExpiration.GetValueOrDefault())
    {
      nullable1 = PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>.op_Implicit(pxResult).MaxShelfLife;
      int? nullable3 = nullable1 ?? PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>.op_Implicit(pxResult).MaxShelfLife;
      nullable1 = nullable3;
      int num = 0;
      nullable2 = !(nullable1.GetValueOrDefault() > num & nullable1.HasValue) || !item.TranDate.HasValue ? new DateTime?() : new DateTime?(item.TranDate.Value.AddDays((double) nullable3.Value));
    }
    INItemLotSerial inItemLotSerial = PXResult<INSite, InventoryItem, INLotSerClass, INItemRep, PX.Objects.IN.S.INItemSite, INItemLotSerial>.op_Implicit(pxResult);
    nullable1 = inItemLotSerial.InventoryID;
    return (!nullable1.HasValue ? new DateTime?() : inItemLotSerial.ExpireDate) ?? (DateTime?) master?.ExpireDate ?? item.ExpireDate ?? nullable2;
  }

  /// <summary>Settings for Generation of Lot/Serial Number</summary>
  [PXHidden]
  [Serializable]
  public class LotSerOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _StartNumVal;
    protected Decimal? _UnassignedQty;
    protected Decimal? _Qty;
    protected bool? _AllowGenerate;
    protected bool? _IsSerial;
    protected string _ExtensionName;

    /// <summary>Start Lot/Serial Number</summary>
    [PXDBString(30)]
    [PXUIField(DisplayName = "Start Lot/Serial Number", FieldClass = "LotSerial")]
    public virtual string StartNumVal
    {
      get => this._StartNumVal;
      set => this._StartNumVal = value;
    }

    /// <summary>Unassigned Qty</summary>
    [PXDBDecimal]
    [PXUIField(DisplayName = "Unassigned Qty.", Enabled = false)]
    public virtual Decimal? UnassignedQty
    {
      get => this._UnassignedQty;
      set => this._UnassignedQty = value;
    }

    /// <summary>Quantity to Generate</summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBDecimal]
    [PXUIField(DisplayName = "Quantity to Generate", FieldClass = "LotSerial")]
    public virtual Decimal? Qty
    {
      get => this._Qty;
      set => this._Qty = value;
    }

    /// <summary>Allow to generate</summary>
    [PXDBBool]
    public virtual bool? AllowGenerate
    {
      get => this._AllowGenerate;
      set => this._AllowGenerate = value;
    }

    /// <summary>Is this a Serial</summary>
    [PXDBBool]
    public virtual bool? IsSerial
    {
      get => this._IsSerial;
      set => this._IsSerial = value;
    }

    /// <summary>Name of the Extension</summary>
    [PXString(255 /*0xFF*/)]
    public virtual string ExtensionName
    {
      get => this._ExtensionName;
      set => this._ExtensionName = value;
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.StartNumVal" />
    public abstract class startNumVal : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LSSelect.LotSerOptions.startNumVal>
    {
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.UnassignedQty" />
    public abstract class unassignedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LSSelect.LotSerOptions.unassignedQty>
    {
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.Qty" />
    public abstract class qty : BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LSSelect.LotSerOptions.qty>
    {
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.AllowGenerate" />
    public abstract class allowGenerate : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      LSSelect.LotSerOptions.allowGenerate>
    {
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.IsSerial" />
    public abstract class isSerial : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LSSelect.LotSerOptions.isSerial>
    {
    }

    /// <inheritdoc cref="P:PX.Objects.IN.LSSelect.LotSerOptions.ExtensionName" />
    public abstract class extensionName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LSSelect.LotSerOptions.extensionName>
    {
    }
  }

  public class Counters
  {
    public int RecordCount;
    public Decimal BaseQty;
    public Dictionary<DateTime?, int> ExpireDates = new Dictionary<DateTime?, int>();
    public int ExpireDatesNull;
    public DateTime? ExpireDate;
    public Dictionary<int?, int> SubItems = new Dictionary<int?, int>();
    public int SubItemsNull;
    public int? SubItem;
    public Dictionary<int?, int> Locations = new Dictionary<int?, int>();
    public int LocationsNull;
    public int? Location;
    public Dictionary<string, int> LotSerNumbers = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    public int LotSerNumbersNull;
    public string LotSerNumber;
    public int UnassignedNumber;
    public Dictionary<KeyValuePair<int?, int?>, int> ProjectTasks = new Dictionary<KeyValuePair<int?, int?>, int>();
    public int ProjectTasksNull;
    public int? ProjectID;
    public int? TaskID;
  }
}
