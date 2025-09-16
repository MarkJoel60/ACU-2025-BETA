// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineForReceivingProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PO;

public class POLineForReceivingProjection : PXProjectionAttribute
{
  public POLineForReceivingProjection()
    : base(typeof (Select<POLine>))
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    POSetup poSetup = PXResultset<POSetup>.op_Implicit(PXSetup<POSetup>.Select(sender.Graph, Array.Empty<object>()));
    if (poSetup.AddServicesFromNormalPOtoPR.GetValueOrDefault() && !poSetup.AddServicesFromDSPOtoPR.GetValueOrDefault())
      return typeof (Select<POLine, Where<POLine.lineType, NotEqual<POLineType.service>, Or<POLine.orderType, Equal<POOrderType.regularOrder>>>>);
    if (!poSetup.AddServicesFromNormalPOtoPR.GetValueOrDefault() && poSetup.AddServicesFromDSPOtoPR.GetValueOrDefault())
      return typeof (Select<POLine, Where<POLine.lineType, NotEqual<POLineType.service>, Or<POLine.orderType, Equal<POOrderType.dropShip>>>>);
    return poSetup.AddServicesFromNormalPOtoPR.GetValueOrDefault() && poSetup.AddServicesFromDSPOtoPR.GetValueOrDefault() ? typeof (Select<POLine, Where<POLine.lineType, NotEqual<POLineType.service>, Or<POLine.orderType, Equal<POOrderType.regularOrder>, Or<POLine.orderType, Equal<POOrderType.dropShip>>>>>) : typeof (Select<POLine, Where<POLine.lineType, NotEqual<POLineType.service>, Or<POLine.processNonStockAsServiceViaPR, Equal<True>>>>);
  }
}
