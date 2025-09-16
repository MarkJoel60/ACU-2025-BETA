// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public class Intercompany : PXGraphExtension<INReleaseProcess>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXOverride]
  public virtual int? GetCogsAcctID(
    InventoryAccountServiceParams @params,
    Func<InventoryAccountServiceParams, int?> baseFunc)
  {
    INTran inTran = @params.INTran;
    PX.Objects.IN.InventoryItem inventoryItem = @params.Item;
    if (inTran != null && inTran.BAccountID.HasValue && inTran != null && inTran.SOOrderType != null)
    {
      PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, inTran.BAccountID);
      if (customer != null && customer.IsBranch.GetValueOrDefault())
      {
        PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this.Base, inTran.SOOrderType);
        if (soOrderType != null)
        {
          switch (soOrderType.IntercompanyCOGSAcctDefault)
          {
            case "I":
              return inventoryItem?.COGSAcctID;
            case "L":
              if (!customer.COGSAcctID.HasValue)
                throw new PXException("The inventory issue cannot be created because the COGS account is not specified for the customer. Specify the COGS account on the GL Accounts tab of the Customers (AR303000) form.");
              return customer.COGSAcctID;
          }
        }
        return new int?();
      }
    }
    return baseFunc(@params);
  }
}
