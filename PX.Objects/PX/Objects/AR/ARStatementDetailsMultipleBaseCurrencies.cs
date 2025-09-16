// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementDetailsMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public sealed class ARStatementDetailsMultipleBaseCurrencies : PXGraphExtension<ARStatementDetails>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public IEnumerable PrintReport(
    PXAdapter adapter,
    ARStatementDetailsMultipleBaseCurrencies.PrintReportDelegate baseMethod)
  {
    if (((PXSelectBase<ARStatementDetails.DetailsResult>) this.Base.Details).Current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXSelectBase<ARStatementDetails.DetailsResult>) this.Base.Details).Current.CustomerId
      }));
      if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.PrepareStatements.Equals("A") && customer.BaseCuryID == null)
        throw new PXException("The statements cannot be printed because it is not allowed to consolidate statements for all companies if the Multiple Base Currency feature is enabled. Select a different option in the Prepare Statements box on the Accounts Receivable Preferences (AR101000) form.");
    }
    return baseMethod(adapter);
  }

  public delegate IEnumerable PrintReportDelegate(PXAdapter adapter);
}
