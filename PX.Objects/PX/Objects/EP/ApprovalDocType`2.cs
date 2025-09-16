// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ApprovalDocType`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Formula that defines a UI-friendly type of the entity to be approved.
/// Uses the detailed record-level type (if it is not empty), otherwise,
/// uses the friendly name of the source item's cache.
/// </summary>
/// <typeparam name="EntityTypeName">
/// Field containing the type name of the source item, e.g.
/// <see cref="T:PX.Objects.EP.EPApprovalProcess.EPOwned.entityType" />. From
/// this type name, the friendly cache name will be deduced if
/// the record-level entity type field returns <c>null</c>.
/// </typeparam>
/// <typeparam name="SourceItemType">
/// Field containing the detailed, record-level source item type, e.g.
/// <see cref="T:PX.Objects.EP.EPApproval.sourceItemType" />. If this field does not
/// contain <c>null</c>, its value will be returned by this formula.
/// </typeparam>
public class ApprovalDocType<EntityTypeName, SourceItemType> : 
  BqlFormulaEvaluator<EntityTypeName, SourceItemType>
  where EntityTypeName : IBqlOperand
  where SourceItemType : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    string par1 = (string) pars[typeof (SourceItemType)];
    if (!string.IsNullOrEmpty(par1))
      return (object) PXMessages.LocalizeNoPrefix(par1);
    string par2 = (string) pars[typeof (EntityTypeName)];
    return !string.IsNullOrEmpty(par2) ? (object) PXMessages.LocalizeNoPrefix(EntityHelper.GetFriendlyEntityName(PXBuildManager.GetType(par2, false, true))) : (object) null;
  }
}
