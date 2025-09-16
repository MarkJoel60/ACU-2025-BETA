// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.INSiteStatusSelectedExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

/// <summary>
/// DAC Extension of INSiteStatusSelected to add additional attributes
/// </summary>
public sealed class INSiteStatusSelectedExt : PXCacheExtension<
#nullable disable
INSiteStatusSelected>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  /// <summary>Project</summary>
  [PXInt]
  [PXUIField(DisplayName = "Project")]
  [PXFormula(typeof (BqlOperand<Selector<BqlField<INSiteStatusSelected.costCenterID, IBqlInt>.FromCurrent, INCostCenter.projectID>, IBqlInt>.When<BqlOperand<Current<INSiteStatusSelected.costCenterID>, IBqlInt>.IsNotEqual<CostCenter.freeStock>>.Else<BqlOperand<INLocation.projectID, IBqlInt>.FromSelectorOf<BqlField<INSiteStatusSelected.locationID, IBqlInt>.FromCurrent>>))]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD))]
  public int? ProjectID { get; set; }

  /// <summary>Project Task</summary>
  [PXInt]
  [PXUIField(DisplayName = "Project Task")]
  [PXFormula(typeof (BqlOperand<Selector<BqlField<INSiteStatusSelected.costCenterID, IBqlInt>.FromCurrent, INCostCenter.taskID>, IBqlInt>.When<BqlOperand<Current<INSiteStatusSelected.costCenterID>, IBqlInt>.IsNotEqual<CostCenter.freeStock>>.Else<BqlOperand<INLocation.taskID, IBqlInt>.FromSelectorOf<BqlField<INSiteStatusSelected.locationID, IBqlInt>.FromCurrent>>))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTask.projectID, Equal<BqlField<INSiteStatusSelectedExt.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTask.taskID, IBqlInt>.IsEqual<BqlField<INSiteStatusSelectedExt.taskID, IBqlInt>.FromCurrent>>>, PMTask>.SearchFor<PMTask.taskID>), SubstituteKey = typeof (PMTask.taskCD))]
  public int? TaskID { get; set; }

  /// <summary>Inventory Source</summary>
  [PXString]
  [PXUIField(DisplayName = "Inventory Source")]
  [PXFormula(typeof (BqlOperand<InventorySourceType.projectStock, IBqlString>.When<BqlOperand<Current<INSiteStatusSelectedExt.projectID>, IBqlInt>.IsNotNull>.Else<InventorySourceType.freeStock>))]
  [InventorySourceType.List(false)]
  public string InventorySource { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INCostCenter.CostLayerType" />
  [PXDBString(BqlField = typeof (INCostCenter.costLayerType))]
  [PX.Objects.IN.CostLayerType.List]
  public string CostLayerType { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelectedExt.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelectedExt.taskID>
  {
  }

  public abstract class inventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelectedExt.inventorySource>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelectedExt.costLayerType>
  {
  }
}
