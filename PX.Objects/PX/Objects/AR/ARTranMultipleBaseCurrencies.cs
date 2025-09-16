// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.AR;

public sealed class ARTranMultipleBaseCurrencies : PXCacheExtension<
#nullable disable
ARTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXString]
  [PXFormula(typeof (Selector<ARTran.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<InventoryItemCurySettings.inventoryID, Where<InventoryItemCurySettings.inventoryID, Equal<BqlField<ARTran.inventoryID, IBqlInt>.FromCurrent>, And<InventoryItemCurySettings.curyID, Equal<BqlField<ARTranMultipleBaseCurrencies.branchBaseCuryID, IBqlString>.FromCurrent>>>>), ValidateValue = false)]
  public int? CuryInventoryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranMultipleBaseCurrencies.branchBaseCuryID>
  {
  }
}
