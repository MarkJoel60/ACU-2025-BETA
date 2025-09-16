// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP.Standalone;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Graph extension to restrict vendors selection based on base currency when MBC is on.
/// </summary>
public sealed class APQuickCheckMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  APQuickCheckVisibilityRestriction, APQuickCheck>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  /// <summary>Vendor ID based on selected branch base currency.</summary>
  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (Where<Current2<APQuickCheckMultipleBaseCurrenciesRestriction.branchBaseCuryID>, PX.Data.IsNull, Or<Vendor.baseCuryID, PX.Data.IsNull, Or<Vendor.baseCuryID, Equal<Current2<APQuickCheckMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  public int? VendorID { get; set; }

  /// <summary>Branch base currency ID.</summary>
  [PXString]
  [PXFormula(typeof (Selector<APQuickCheck.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  /// <summary>Vendor base currency ID.</summary>
  [PXString]
  [PXFormula(typeof (Selector<APQuickCheck.vendorID, Vendor.baseCuryID>))]
  public string VendorBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheckMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class vendorBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheckMultipleBaseCurrenciesRestriction.vendorBaseCuryID>
  {
  }
}
