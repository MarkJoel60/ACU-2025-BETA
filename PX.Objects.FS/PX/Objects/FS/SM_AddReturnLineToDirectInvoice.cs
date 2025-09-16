// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_AddReturnLineToDirectInvoice
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.SO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class SM_AddReturnLineToDirectInvoice : 
  PXGraphExtension<AddReturnLineToDirectInvoice, SOInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<ARTranForDirectInvoice>) this.Base1.arTranList).WhereAnd<Where<NotExists<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranForDirectInvoice.tranType, Equal<PX.Objects.AR.ARTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranForDirectInvoice.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>>>>>.And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSxARTran.appointmentRefNbr, IsNotNull>>>, Or<BqlOperand<FSxARTran.serviceOrderRefNbr, IBqlString>.IsNotNull>>>.Or<BqlOperand<FSxARTran.serviceContractRefNbr, IBqlString>.IsNotNull>>>>>>>>();
  }

  [PXOverride]
  public IEnumerable AddARTran(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseMethod)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>())
      return baseMethod(adapter);
    foreach (ARTranForDirectInvoice forDirectInvoice in ((PXSelectBase) this.Base1.arTranList).Cache.Updated)
    {
      bool? nullable = forDirectInvoice.Selected;
      if (nullable.GetValueOrDefault())
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base, forDirectInvoice.InventoryID);
        if (inventoryItem != null)
        {
          nullable = inventoryItem.StkItem;
          if (nullable.GetValueOrDefault())
            throw new PXException("A line with a stock item cannot be added because the Advanced SO Invoices feature is disabled on the Enable/Disable Features (CS100000) form.");
        }
      }
    }
    return baseMethod(adapter);
  }
}
