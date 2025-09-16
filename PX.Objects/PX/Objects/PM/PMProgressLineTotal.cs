// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProgressLineTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Used in Reports to calculate Previously invoiced amount.
/// </summary>
[PXCacheName("Pro Forma Line")]
[PXProjection(typeof (Select4<PMProformaLine, Where<PMProformaLine.type, Equal<PMProformaLineType.progressive>, And<PMProformaLine.corrected, NotEqual<True>>>, Aggregate<GroupBy<PMProformaLine.projectID, GroupBy<PMProformaLine.refNbr, GroupBy<PMProformaLine.taskID, GroupBy<PMProformaLine.accountGroupID, GroupBy<PMProformaLine.inventoryID, GroupBy<PMProformaLine.costCodeID, Sum<PMProformaLine.curyLineTotal, Sum<PMProformaLine.lineTotal, Sum<PMProformaLine.curyMaterialStoredAmount, Sum<PMProformaLine.materialStoredAmount, Sum<PMProformaLine.curyRetainage, Sum<PMProformaLine.retainage>>>>>>>>>>>>>>), Persistent = false)]
[Serializable]
public class PMProgressLineTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected int? _AccountGroupID;

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PMProformaLine.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PMProformaLine.projectID))]
  [PXForeignReference(typeof (Field<PMProgressLineTotal.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMProformaLine.taskID))]
  [PXForeignReference(typeof (CompositeKey<Field<PMProgressLineTotal.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMProgressLineTotal.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(BqlField = typeof (PMProformaLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (PMProformaLine.costCodeID))]
  [PXForeignReference(typeof (Field<PMProgressLineTotal.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMProformaLine.accountGroupID))]
  [PXForeignReference(typeof (Field<PMProgressLineTotal.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.curyMaterialStoredAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Stored Material")]
  public virtual Decimal? CuryMaterialStoredAmount { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.materialStoredAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Stored Material in Base Currency")]
  public virtual Decimal? MaterialStoredAmount { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.curyLineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total")]
  public virtual Decimal? CuryLineTotal { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total in Base Currency")]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.curyRetainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage")]
  public virtual Decimal? CuryRetainage { get; set; }

  [PXDBBaseCury(BqlField = typeof (PMProformaLine.retainage))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retainage in Base Currency")]
  public virtual Decimal? Retainage { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProgressLineTotal.refNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressLineTotal.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressLineTotal.taskID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressLineTotal.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProgressLineTotal.costCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProgressLineTotal.accountGroupID>
  {
  }

  public abstract class curyMaterialStoredAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.curyMaterialStoredAmount>
  {
  }

  public abstract class materialStoredAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.materialStoredAmount>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.curyLineTotal>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.lineTotal>
  {
  }

  public abstract class curyRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.curyRetainage>
  {
  }

  public abstract class retainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProgressLineTotal.retainage>
  {
  }
}
