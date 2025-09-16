// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.EP.Attributes.ApprovalDocTypeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.Extensions;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Subcontracts.EP.Attributes;

public class ApprovalDocTypeExt : 
  ApprovalDocType<EPApprovalProcess.EPOwned.entityType, EPApproval.sourceItemType>
{
  public override object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    return (item as EPApprovalProcess.EPOwned).GetSubcontractEntity(cache.Graph) == null ? base.Evaluate(cache, item, pars) : (object) "Subcontract";
  }
}
