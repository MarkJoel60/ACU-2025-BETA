// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATransferMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

public sealed class CATransferMultipleBaseCurrenciesRestriction : PXCacheExtension<
#nullable disable
CATransfer>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<CATransfer.inAccountID>, IsNull, Or<CashAccount.baseCuryID, Equal<Current<CATransferMultipleBaseCurrenciesRestriction.inAccountCuryID>>>>), null, new Type[] {})]
  public int? OutAccountID { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current2<CATransfer.outAccountID>, IsNull, Or<CashAccount.baseCuryID, Equal<Current<CATransferMultipleBaseCurrenciesRestriction.outAccountCuryID>>>>), null, new Type[] {})]
  public int? InAccountID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<CATransfer.inAccountID, CashAccount.baseCuryID>))]
  public string InAccountCuryID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<CATransfer.outAccountID, CashAccount.baseCuryID>))]
  public string OutAccountCuryID { get; set; }

  public abstract class inAccountCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransferMultipleBaseCurrenciesRestriction.inAccountCuryID>
  {
  }

  public abstract class outAccountCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATransferMultipleBaseCurrenciesRestriction.outAccountCuryID>
  {
  }
}
