// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.DAC.Unbound.INCrossSellSuggestionProcessFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems.DAC.Unbound;

/// <exclude />
[PXHidden]
public class INCrossSellSuggestionProcessFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUnboundDefault("ApproveCrossSellSuggestion")]
  [INCrossSellSuggestionProcessFilter.action.List]
  [PXUIField(DisplayName = "Action", Required = true)]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Original Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? OriginalItemClassID { get; set; }

  [AnyInventory(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.kitItem, IBqlBool>.IsEqual<False>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INCrossSellSuggestionProcessFilter.originalItemClassID>, IsNull>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INCrossSellSuggestionProcessFilter.originalItemClassID, IBqlInt>.FromCurrent>>>>>.And<MatchUser>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Original Item")]
  public virtual int? OriginalInventoryID { get; set; }

  [AnyInventory(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.kitItem, Equal<False>, And<MatchUser>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Cross-Sell Item")]
  public virtual int? CrossSellInventoryID { get; set; }

  public abstract class action : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCrossSellSuggestionProcessFilter.action>
  {
    public const string ApproveCrossSellSuggestion = "ApproveCrossSellSuggestion";
    public const string DeleteCrossSellSuggestion = "DeleteCrossSellSuggestion";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("ApproveCrossSellSuggestion", "Approve"),
          PXStringListAttribute.Pair("DeleteCrossSellSuggestion", "Delete")
        })
      {
      }
    }

    public class approveCrossSellSuggestion : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INCrossSellSuggestionProcessFilter.action.approveCrossSellSuggestion>
    {
      public approveCrossSellSuggestion()
        : base("ApproveCrossSellSuggestion")
      {
      }
    }

    public class deleteCrossSellSuggestion : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INCrossSellSuggestionProcessFilter.action.deleteCrossSellSuggestion>
    {
      public deleteCrossSellSuggestion()
        : base("DeleteCrossSellSuggestion")
      {
      }
    }
  }

  public abstract class originalItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessFilter.originalItemClassID>
  {
  }

  public abstract class originalInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessFilter.originalInventoryID>
  {
  }

  public abstract class crossSellInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INCrossSellSuggestionProcessFilter.crossSellInventoryID>
  {
  }
}
