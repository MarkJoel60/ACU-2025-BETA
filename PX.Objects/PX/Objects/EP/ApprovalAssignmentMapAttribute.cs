// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ApprovalAssignmentMapAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public abstract class ApprovalAssignmentMapAttribute : PXEntityAttribute
{
  protected ApprovalAssignmentMapAttribute(
    Type entityType,
    bool assignment,
    Type customSearchField = null,
    Type customSearchQuery = null,
    Type[] fieldList = null,
    PXSelectorMode selectorMode = 16 /*0x10*/)
  {
    Type type = customSearchQuery;
    if ((object) type == null)
      type = this.CreateSelect(entityType, assignment, customSearchField);
    Type[] typeArray = fieldList;
    if (typeArray == null)
      typeArray = new Type[1]
      {
        typeof (EPAssignmentMap.name)
      };
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(type, typeArray)
    {
      DescriptionField = typeof (EPAssignmentMap.name),
      SelectorMode = selectorMode,
      Filterable = true,
      DirtyRead = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  protected virtual Type CreateSelect(Type entityType, bool assignment, Type customSearchField)
  {
    IBqlCommandTemplate ibqlCommandTemplate = BqlTemplate.OfCommand<FbqlSelect<SelectFromBase<EPAssignmentMap, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPAssignmentMap.entityType, Equal<BqlPlaceholder.Named<BqlPlaceholder.A>.AsOperand>>>>>.And<BqlOperand<EPAssignmentMap.mapType, IBqlInt>.IsIn<EPMapType.legacy, BqlPlaceholder.Named<BqlPlaceholder.B>.AsOperand>>>, EPAssignmentMap>.SearchFor<BqlPlaceholder.C>>.Replace<BqlPlaceholder.A>(entityType).Replace<BqlPlaceholder.B>(assignment ? typeof (EPMapType.assignment) : typeof (EPMapType.approval));
    Type type = customSearchField;
    if ((object) type == null)
      type = typeof (EPAssignmentMap.assignmentMapID);
    return ((IBqlTemplate) ibqlCommandTemplate.Replace<BqlPlaceholder.C>(type)).ToType();
  }
}
