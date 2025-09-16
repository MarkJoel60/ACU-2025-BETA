// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BranchBAccountDunningLetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.AR;

public sealed class BranchBAccountDunningLetter : PXCacheExtension<
#nullable disable
BranchMaint.BranchBAccount>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>();

  [PXInt]
  public int? BranchBranchID { get; set; }

  [PXInt]
  public int? DunningFeeBranchID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Dunning Letter Branch")]
  public bool? IsDunningCompanyBranchID { get; set; }

  public abstract class branchBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BranchBAccountDunningLetter.branchBranchID>
  {
  }

  public abstract class dunningFeeBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BranchBAccountDunningLetter.dunningFeeBranchID>
  {
  }

  public abstract class isDunningCompanyBranchID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BranchBAccountDunningLetter.isDunningCompanyBranchID>
  {
  }
}
