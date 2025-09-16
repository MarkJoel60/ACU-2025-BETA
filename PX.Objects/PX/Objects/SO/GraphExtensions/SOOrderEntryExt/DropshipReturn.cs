// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.DropshipReturn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class DropshipReturn : PXGraphExtension<SOOrderEntry>
{
  public PXAction<SOOrder> createVendorReturn;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable CreateVendorReturn(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DropshipReturn.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new DropshipReturn.\u003C\u003Ec__DisplayClass2_0();
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.currentDoc = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CCreateVendorReturn\u003Eb__0)));
    return (IEnumerable) list;
  }
}
