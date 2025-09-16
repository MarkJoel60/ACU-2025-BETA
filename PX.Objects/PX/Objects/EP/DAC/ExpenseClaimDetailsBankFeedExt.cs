// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DAC.ExpenseClaimDetailsBankFeedExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP.DAC;

public sealed class ExpenseClaimDetailsBankFeedExt : PXCacheExtension<
#nullable disable
EPExpenseClaimDetails>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.bankFeedIntegration>();

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Category")]
  public string Category { get; set; }

  [PXDBString]
  [EPBankTranStatus.List]
  [PXUIField(DisplayName = "Bank Transaction Status", Enabled = false)]
  public string BankTranStatus { get; set; }

  public abstract class category : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExpenseClaimDetailsBankFeedExt.category>
  {
  }

  public abstract class bankTranStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExpenseClaimDetailsBankFeedExt.bankTranStatus>
  {
  }
}
