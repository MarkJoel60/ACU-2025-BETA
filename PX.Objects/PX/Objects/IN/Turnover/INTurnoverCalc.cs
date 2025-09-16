// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.INTurnoverCalc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.Turnover;

[PXCacheName]
public class INTurnoverCalc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(null, null, true, false, false, IsKey = true)]
  public virtual int? BranchID { get; set; }

  [PXUIField(DisplayName = "From Period")]
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string FromPeriodID { get; set; }

  [PXUIField(DisplayName = "To Period")]
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual string ToPeriodID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsFullCalc { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsInventoryListCalc { get; set; }

  [Site]
  public virtual int? SiteID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionLite<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>, PX.Objects.IN.InventoryItem>.SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (INSetup.includeProductionInTurnover))]
  public virtual bool? IncludedProduction { get; set; }

  [PXDBBool]
  [PXDefault(typeof (INSetup.includeAssemblyInTurnover))]
  public virtual bool? IncludedAssembly { get; set; }

  [PXDBBool]
  [PXDefault(typeof (INSetup.includeIssueInTurnover))]
  public virtual bool? IncludedIssue { get; set; }

  [PXDBBool]
  [PXDefault(typeof (INSetup.includeTransferInTurnover))]
  public virtual bool? IncludedTransfer { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Calculation Date and Time", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  public class PK : 
    PrimaryKeyOf<INTurnoverCalc>.By<INTurnoverCalc.branchID, INTurnoverCalc.fromPeriodID, INTurnoverCalc.toPeriodID>
  {
    public static INTurnoverCalc Find(
      PXGraph graph,
      int? branchID,
      string fromPeriodID,
      string toPeriodID,
      PKFindOptions options = 0)
    {
      return (INTurnoverCalc) PrimaryKeyOf<INTurnoverCalc>.By<INTurnoverCalc.branchID, INTurnoverCalc.fromPeriodID, INTurnoverCalc.toPeriodID>.FindBy(graph, (object) branchID, (object) fromPeriodID, (object) toPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INTurnoverCalc>.By<INTurnoverCalc.branchID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTurnoverCalc>.By<INTurnoverCalc.siteID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INTurnoverCalc>.By<INTurnoverCalc.itemClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INTurnoverCalc>.By<INTurnoverCalc.inventoryID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalc.branchID>
  {
  }

  public abstract class fromPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTurnoverCalc.fromPeriodID>
  {
  }

  public abstract class toPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTurnoverCalc.toPeriodID>
  {
  }

  public abstract class isFullCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTurnoverCalc.isFullCalc>
  {
  }

  public abstract class isInventoryListCalc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverCalc.isInventoryListCalc>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalc.siteID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalc.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTurnoverCalc.inventoryID>
  {
  }

  public abstract class includedProduction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverCalc.includedProduction>
  {
  }

  public abstract class includedAssembly : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverCalc.includedAssembly>
  {
  }

  public abstract class includedIssue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTurnoverCalc.includedIssue>
  {
  }

  public abstract class includedTransfer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTurnoverCalc.includedTransfer>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTurnoverCalc.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTurnoverCalc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTurnoverCalc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTurnoverCalc.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTurnoverCalc.Tstamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTurnoverCalc.selected>
  {
  }
}
