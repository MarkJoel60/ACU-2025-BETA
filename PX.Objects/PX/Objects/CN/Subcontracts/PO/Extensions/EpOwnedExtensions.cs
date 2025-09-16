// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.Extensions.EpOwnedExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.Extensions;

public static class EpOwnedExtensions
{
  public static POOrder GetSubcontractEntity(this EPApprovalProcess.EPOwned epOwned, PXGraph graph)
  {
    if (epOwned == null || epOwned.EntityType != typeof (POOrder).FullName)
      return (POOrder) null;
    using (new PXReadBranchRestrictedScope())
    {
      POOrder poOrder = ((PXSelectBase<POOrder>) new PXSelect<POOrder, Where<POOrder.noteID, Equal<Required<POOrder.noteID>>>>(graph)).SelectSingle(new object[1]
      {
        (object) epOwned.RefNoteID
      });
      return poOrder.OrderType == "RS" ? poOrder : (POOrder) null;
    }
  }
}
