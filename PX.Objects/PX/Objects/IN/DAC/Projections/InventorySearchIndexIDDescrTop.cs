// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Projections.InventorySearchIndexIDDescrTop
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC.Unbound;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Projections;

/// <exclude />
[PXProjection(typeof (SelectFromBase<InventorySearchIndex, TypeArrayOf<IFbqlJoin>.Empty>.Where<Contains<InventorySearchIndex.contentIDDesc, CurrentValue<InventoryFullTextSearchFilter.containsSearchCondition>, InventorySearchIndex.indexID, CurrentValue<InventoryFullTextSearchFilter.top>>>))]
[PXHidden]
public class InventorySearchIndexIDDescrTop : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (InventorySearchIndex.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBGuid(false, BqlField = typeof (InventorySearchIndex.indexID))]
  public virtual Guid? IndexID { get; set; }

  [PXDBText(IsUnicode = true, BqlField = typeof (InventorySearchIndex.contentIDDesc))]
  public virtual 
  #nullable disable
  string ContentIDDesc { get; set; }

  [DBRank(BqlField = typeof (InventorySearchIndex.rank))]
  public virtual int? Rank { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySearchIndexIDDescrTop.inventoryID>
  {
  }

  public abstract class indexID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventorySearchIndexIDDescrTop.indexID>
  {
  }

  public abstract class contentIDDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySearchIndexIDDescrTop.contentIDDesc>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventorySearchIndexIDDescrTop.rank>
  {
  }
}
