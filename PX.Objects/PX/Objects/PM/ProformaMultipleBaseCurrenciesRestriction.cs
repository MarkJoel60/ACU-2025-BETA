// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.PM;

public sealed class ProformaMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<
  #nullable disable
  PMProformaVisibilityRestriction, PMProforma>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Current2<PMProforma.branchID>, IsNotNull>, Current2<PMProforma.branchID>, Case<Where<PMProforma.locationID, IsNull, And<UnattendedMode, Equal<True>>>, Null, Case<Where<PX.Objects.CR.Location.bAccountID, Equal<Current<PMProforma.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<PMProforma.locationID>>>>, PX.Objects.CR.Location.cBranchID, Case<Where<PMProforma.customerID, IsNotNull, And<Not<Selector<PMProforma.customerID, PX.Objects.AR.Customer.cOrgBAccountID>, RestrictByBranch<Current2<PMProforma.branchID>>>>>, Null, Case<Where<PMProforma.customerID, IsNotNull, And<Selector<PMProforma.customerID, PX.Objects.AR.Customer.baseCuryID>, NotEqual<Current<AccessInfo.baseCuryID>>>>, Null>>>>>, Current<AccessInfo.branchID>>))]
  public int? BranchID { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<ProformaMultipleBaseCurrenciesRestriction.branchBaseCuryID>, IsNull, Or<PX.Objects.AR.Customer.baseCuryID, Equal<Current2<ProformaMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>), null, new System.Type[] {})]
  public int? CustomerID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<PMProforma.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public string BranchBaseCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<PMProforma.customerID, PX.Objects.AR.Customer.baseCuryID>))]
  public string CustomerBaseCuryID { get; set; }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProformaMultipleBaseCurrenciesRestriction.branchBaseCuryID>
  {
  }

  public abstract class customerBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProformaMultipleBaseCurrenciesRestriction.customerBaseCuryID>
  {
  }
}
