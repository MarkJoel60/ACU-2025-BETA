// Decompiled with JetBrains decompiler
// Type: PX.TM.OwnedEscalatedFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.TM;

/// <exclude />
[PXHidden]
[Serializable]
public class OwnedEscalatedFilter : OwnedFilter
{
  protected bool? _MyEscalated;

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Me")]
  public override bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDefault(true)]
  [PXDBBool]
  [PXUIField(DisplayName = "Display Escalated", Visibility = PXUIVisibility.Visible)]
  public virtual bool? MyEscalated
  {
    get => this._MyEscalated;
    set => this._MyEscalated = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public override bool? FilterSet
  {
    get
    {
      bool? filterSet = base.FilterSet;
      bool flag1 = true;
      int num;
      if (!(filterSet.GetValueOrDefault() == flag1 & filterSet.HasValue))
      {
        bool? myEscalated = this.MyEscalated;
        bool flag2 = true;
        num = myEscalated.GetValueOrDefault() == flag2 & myEscalated.HasValue ? 1 : 0;
      }
      else
        num = 1;
      return new bool?(num != 0);
    }
  }

  /// <exclude />
  public new abstract class myOwner : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  OwnedEscalatedFilter.myOwner>
  {
  }

  /// <exclude />
  public abstract class myEscalated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OwnedEscalatedFilter.myEscalated>
  {
  }

  /// <exclude />
  public new abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OwnedEscalatedFilter.filterSet>
  {
  }

  /// <exclude />
  public new class ProjectionAttribute : OwnedFilter.ProjectionAttribute
  {
    public ProjectionAttribute(System.Type filter, System.Type groupID, System.Type ownerID, System.Type pendingDate)
      : base(filter, OwnedEscalatedFilter.ProjectionAttribute.Compose(BqlCommand.GetItemType(groupID), OwnedEscalatedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate)))
    {
    }

    public ProjectionAttribute(
      System.Type filter,
      System.Type select,
      System.Type join,
      System.Type aggregate,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate)
      : base(filter, OwnedEscalatedFilter.ProjectionAttribute.Compose(select, join, OwnedEscalatedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate), aggregate))
    {
    }

    public ProjectionAttribute(
      System.Type filter,
      System.Type select,
      System.Type join,
      System.Type aggregate,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate,
      System.Type paramWhere)
      : base(filter, OwnedEscalatedFilter.ProjectionAttribute.Compose(select, join, OwnedEscalatedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate, (System.Type) null, paramWhere), aggregate))
    {
    }

    public ProjectionAttribute(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate,
      System.Type where,
      System.Type paramWhere)
      : base(filter, OwnedEscalatedFilter.ProjectionAttribute.Compose(BqlCommand.GetItemType(groupID), OwnedEscalatedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate, where, paramWhere)))
    {
    }

    public ProjectionAttribute(System.Type filter, System.Type select)
      : base(filter, select)
    {
    }

    public static System.Type Compose(System.Type selectType, System.Type where)
    {
      return BqlCommand.Compose(typeof (Select<,>), selectType, where);
    }

    public static System.Type Compose(System.Type selectType, System.Type join, System.Type where, System.Type aggregate)
    {
      return !(aggregate != (System.Type) null) ? BqlCommand.Compose(typeof (Select2<,,>), selectType, join, where) : BqlCommand.Compose(typeof (Select5<,,,>), selectType, join, where, aggregate);
    }

    public static System.Type ComposeWhere(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate)
    {
      return BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.ownerID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.ownerID>(filter), typeof (Equal<>), ownerID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.workGroupID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.workGroupID>(filter), typeof (Equal<>), groupID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.myWorkGroup>(filter), typeof (Equal<True>), typeof (And<,>), groupID, typeof (IsWorkgroupOfContact<>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.currentOwnerID>(filter), typeof (PX.Data.Or<>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedEscalatedFilter.myEscalated>(filter), typeof (Equal<True>), typeof (PX.Data.And<>), typeof (Where<,,>), groupID, typeof (Escalated<,,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.currentOwnerID>(filter), groupID, ownerID, pendingDate, typeof (Or<,>), groupID, typeof (PX.Data.IsNull));
    }

    public static System.Type ComposeWhere(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate,
      System.Type where,
      System.Type paramWhere)
    {
      System.Type type = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.ownerID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.ownerID>(filter), typeof (Equal<>), ownerID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.workGroupID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.workGroupID>(filter), typeof (Equal<>), groupID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.myWorkGroup>(filter), typeof (Equal<True>), typeof (And<,>), groupID, typeof (IsWorkgroupOfContact<>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.currentOwnerID>(filter), typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedEscalatedFilter.myEscalated>(filter), typeof (Equal<True>), typeof (PX.Data.And<>), typeof (Where<,,>), groupID, typeof (Escalated<,,,>), OwnedFilter.ProjectionAttribute.CurrentValue<OwnedFilter.currentOwnerID>(filter), groupID, ownerID, pendingDate, typeof (Or<,>), groupID, typeof (PX.Data.IsNull), typeof (PX.Data.Or<>), paramWhere);
      if (where != (System.Type) null)
        type = BqlCommand.Compose(typeof (Where2<,>), where, typeof (PX.Data.And<>), type);
      return type;
    }
  }
}
