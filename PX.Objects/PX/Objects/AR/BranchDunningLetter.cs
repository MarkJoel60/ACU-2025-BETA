// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BranchDunningLetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.AR;

public sealed class BranchDunningLetter : PXCacheExtension<
#nullable disable
PX.Objects.GL.Branch>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>();

  /// <summary>
  /// Is the Branch is used for Dunning Letter as a source for DL when consolidating by Company
  /// </summary>
  [PXInt]
  public int? DunningCompanyBranchID { get; set; }

  /// <summary>
  /// Is the Branch is used for Dunning Letter as a source for DL when consolidating by Company
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Dunning Letter Branch", FieldClass = "DunningLetter")]
  public bool? IsDunningCompanyBranchID { get; set; }

  public abstract class dunningCompanyBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BranchDunningLetter.dunningCompanyBranchID>
  {
  }

  public abstract class isDunningCompanyBranchID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BranchDunningLetter.isDunningCompanyBranchID>
  {
  }
}
