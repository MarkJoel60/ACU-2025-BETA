// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CustomerPaymentMethodMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CA;

public sealed class CustomerPaymentMethodMultipleBaseCurrencies : 
  PXCacheExtension<
  #nullable disable
  PX.Objects.AR.CustomerPaymentMethod>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<PX.Objects.AR.CustomerPaymentMethod.bAccountID, BAccountR.baseCuryID>))]
  [PXUIField(DisplayName = "Currency")]
  public string BaseCuryID { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<CashAccount.baseCuryID, Equal<Current<CustomerPaymentMethodMultipleBaseCurrencies.baseCuryID>>>), null, new System.Type[] {})]
  public int? CashAccountID { get; set; }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerPaymentMethodMultipleBaseCurrencies.baseCuryID>
  {
  }
}
