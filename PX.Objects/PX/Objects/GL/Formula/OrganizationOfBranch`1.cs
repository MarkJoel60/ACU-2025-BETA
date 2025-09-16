// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Formula.OrganizationOfBranch`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Formula;

public class OrganizationOfBranch<BranchID> : BqlFormulaEvaluator<BranchID>, IBqlOperand where BranchID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
  {
    return (object) PXAccess.GetParentOrganizationID((int?) parameters[typeof (BranchID)]);
  }
}
