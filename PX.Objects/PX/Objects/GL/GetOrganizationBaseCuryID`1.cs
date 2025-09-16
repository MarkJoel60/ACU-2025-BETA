// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GetOrganizationBaseCuryID`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

public class GetOrganizationBaseCuryID<OrganizationBAccountID> : 
  BqlFormulaEvaluator<OrganizationBAccountID>,
  IBqlOperand
  where OrganizationBAccountID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    int? parameter = (int?) parameters[typeof (OrganizationBAccountID)];
    if (!parameter.HasValue)
      return (object) null;
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(parameter);
    if (branchByBaccountId != null)
      return (object) branchByBaccountId.BaseCuryID;
    PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(parameter);
    return organizationByBaccountId != null ? (object) ((PXAccess.Organization) organizationByBaccountId).BaseCuryID : (object) null;
  }
}
