// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DefaultOrganizationID
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public class DefaultOrganizationID : BqlFormulaEvaluator
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    return (object) (int?) PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectJoin<PX.Objects.GL.DAC.Organization, InnerJoin<Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>>, Where<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>, And<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>.Config>.Select(cache.Graph, Array.Empty<object>()))?.OrganizationID;
  }
}
