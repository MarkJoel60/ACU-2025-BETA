// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CACorpCardsMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.EP.DAC;

#nullable disable
namespace PX.Objects.CA;

public class CACorpCardsMaintMultipleBaseCurrencies : PXGraphExtension<CACorpCardsMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXDimensionSelector("EMPLOYEE", typeof (Search2<EPEmployee.bAccountID, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<Current<CACorpCard.cashAccountID>>>>, Where<BAccount.baseCuryID, Equal<CashAccount.baseCuryID>>>), typeof (EPEmployee.acctCD), new System.Type[] {typeof (EPEmployee.bAccountID), typeof (EPEmployee.acctCD), typeof (EPEmployee.acctName), typeof (EPEmployee.departmentID)}, DescriptionField = typeof (EPEmployee.acctName))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeCorpCardLink.employeeID> e)
  {
  }
}
