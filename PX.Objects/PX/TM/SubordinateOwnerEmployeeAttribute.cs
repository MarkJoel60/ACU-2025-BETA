// Decompiled with JetBrains decompiler
// Type: PX.TM.SubordinateOwnerEmployeeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows to show employees which are subordinated or coworkers for current logged in employee.
/// </summary>
/// <example>[SubordinateOwnerEmployeeAttribute]</example>
public class SubordinateOwnerEmployeeAttribute : OwnerEmployeeAttribute
{
  public SubordinateOwnerEmployeeAttribute()
    : base((Type) null, typeof (FbqlSelect<SelectFromBase<PX.Objects.EP.EPEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<OwnerAttribute.Owner>.On<BqlOperand<OwnerAttribute.Owner.contactID, IBqlInt>.IsEqual<PX.Objects.EP.EPEmployee.defContactID>>>, FbqlJoins.Left<EPCompanyTreeMember>.On<BqlOperand<EPCompanyTreeMember.contactID, IBqlInt>.IsEqual<OwnerAttribute.Owner.contactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<OwnerAttribute.Owner.contactID, Equal<BqlField<AccessInfo.contactID, IBqlInt>.FromCurrent>>>>>.Or<Where<EPCompanyTreeMember.workGroupID, IsWorkgroupOrSubgroupOfContact<BqlField<AccessInfo.contactID, IBqlInt>.FromCurrent>>>>.Aggregate<To<GroupBy<PX.Objects.EP.EPEmployee.bAccountID>>>, PX.Objects.EP.EPEmployee>.SearchFor<PX.Objects.EP.EPEmployee.defContactID>), false, true)
  {
  }
}
