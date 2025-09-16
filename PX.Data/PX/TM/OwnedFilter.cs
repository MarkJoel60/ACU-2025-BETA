// Decompiled with JetBrains decompiler
// Type: PX.TM.OwnedFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.TM;

/// <exclude />
[PXHidden]
[Serializable]
public class OwnedFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IOwnedFilter
{
  public const int DASHBOARD_TYPE = 6;
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;

  [PXDBInt]
  public virtual int? CurrentOwnerID => PXAccess.GetContactID();

  [PXDBInt]
  [PXUIField(DisplayName = "Assigned To")]
  public virtual int? OwnerID
  {
    get
    {
      bool? owner = this._MyOwner;
      bool flag = true;
      return !(owner.GetValueOrDefault() == flag & owner.HasValue) ? this._OwnerID : this.CurrentOwnerID;
    }
    set => this._OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get
    {
      bool? workGroup = this._MyWorkGroup;
      bool flag = true;
      return !(workGroup.GetValueOrDefault() == flag & workGroup.HasValue) ? this._WorkGroupID : new int?();
    }
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "My", Visibility = PXUIVisibility.Visible)]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? FilterSet
  {
    get
    {
      int num;
      if (!this.OwnerID.HasValue && !this.WorkGroupID.HasValue)
      {
        bool? myWorkGroup = this.MyWorkGroup;
        bool flag = true;
        num = myWorkGroup.GetValueOrDefault() == flag & myWorkGroup.HasValue ? 1 : 0;
      }
      else
        num = 1;
      return new bool?(num != 0);
    }
  }

  /// <exclude />
  public abstract class currentOwnerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  OwnedFilter.currentOwnerID>
  {
  }

  /// <exclude />
  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OwnedFilter.ownerID>
  {
  }

  /// <exclude />
  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OwnedFilter.myOwner>
  {
  }

  /// <exclude />
  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OwnedFilter.workGroupID>
  {
  }

  /// <exclude />
  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OwnedFilter.myWorkGroup>
  {
  }

  /// <exclude />
  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OwnedFilter.filterSet>
  {
  }

  /// <exclude />
  public class ProjectionAttribute : PXProjectionAttribute
  {
    private readonly System.Type filterType;

    public ProjectionAttribute(System.Type filterType, System.Type select, System.Type groupID, System.Type ownerID)
      : this(filterType, OwnedFilter.ProjectionAttribute.AppendOwnedFilterCondition(select, filterType, groupID, ownerID))
    {
    }

    public ProjectionAttribute(System.Type filterType, System.Type groupID, System.Type ownerID)
      : this(filterType, OwnedFilter.ProjectionAttribute.Compose(filterType, groupID, ownerID))
    {
    }

    protected ProjectionAttribute(System.Type filterType, System.Type select)
      : base(select)
    {
      this.filterType = filterType;
    }

    private static System.Type AppendOwnedFilterCondition(
      System.Type select,
      System.Type filter,
      System.Type groupID,
      System.Type ownerID)
    {
      return ((BqlCommand) Activator.CreateInstance(select)).WhereAnd(OwnedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID)).GetType();
    }

    public static System.Type Compose(System.Type filter, System.Type groupID, System.Type ownerID)
    {
      return BqlCommand.Compose(typeof (Select<,>), BqlCommand.GetItemType(groupID), OwnedFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID));
    }

    public static System.Type ComposeWhere(System.Type filter, System.Type groupID, System.Type ownerID)
    {
      (System.Type OwnerID, System.Type MyOwner, System.Type WorkGroupID, System.Type MyWorkGroup, System.Type CurrentOwnerID) ownedFilterFields = OwnedFilter.ProjectionAttribute.ExtractOwnedFilterFields(filter);
      return BqlTemplate.OfCondition<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<CurrentValue<OwnedFilter.ProjectionAttribute.PhFilter.OwnerID>, PX.Data.IsNull>>>>.Or<BqlOperand<CurrentValue<OwnedFilter.ProjectionAttribute.PhFilter.OwnerID>, BqlPlaceholder.IBqlAny>.IsEqual<BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhTarget.OwnerID>.AsField>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<CurrentValue<OwnedFilter.ProjectionAttribute.PhFilter.WorkGroupID>, PX.Data.IsNull>>>>.Or<BqlOperand<CurrentValue<OwnedFilter.ProjectionAttribute.PhFilter.WorkGroupID>, BqlPlaceholder.IBqlAny>.IsEqual<BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID>.AsField>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<CurrentValue<OwnedFilter.ProjectionAttribute.PhFilter.MyWorkGroup>, Equal<False>>>>>.Or<BqlOperand<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID, BqlPlaceholder.IBqlAny>.Is<IsWorkgroupOfContact<BqlField<OwnedFilter.ProjectionAttribute.PhFilter.CurrentOwnerID, BqlPlaceholder.IBqlAny>.FromCurrent.Value>>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID, PX.Data.IsNull>>>>.Or<BqlOperand<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID, BqlPlaceholder.IBqlAny>.Is<IsWorkgroupOrSubgroupOfContact<BqlField<OwnedFilter.ProjectionAttribute.PhFilter.CurrentOwnerID, BqlPlaceholder.IBqlAny>.FromCurrent.Value>>>>>>.Replace<OwnedFilter.ProjectionAttribute.PhFilter.OwnerID>(ownedFilterFields.OwnerID).Replace<OwnedFilter.ProjectionAttribute.PhFilter.WorkGroupID>(ownedFilterFields.WorkGroupID).Replace<OwnedFilter.ProjectionAttribute.PhFilter.MyWorkGroup>(ownedFilterFields.MyWorkGroup).Replace<OwnedFilter.ProjectionAttribute.PhFilter.CurrentOwnerID>(ownedFilterFields.CurrentOwnerID).Replace<OwnedFilter.ProjectionAttribute.PhTarget.OwnerID>(ownerID).Replace<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID>(groupID).ToType();
    }

    private static (System.Type OwnerID, System.Type MyOwner, System.Type WorkGroupID, System.Type MyWorkGroup, System.Type CurrentOwnerID) ExtractOwnedFilterFields(
      System.Type filter)
    {
      System.Type type1 = (System.Type) null;
      System.Type type2 = (System.Type) null;
      System.Type type3 = (System.Type) null;
      System.Type type4 = (System.Type) null;
      System.Type type5 = (System.Type) null;
      foreach (System.Type type6 in filter.GetInheritanceChain())
      {
        if ((object) type1 == null)
          type1 = type6.GetNestedType("ownerID");
        if ((object) type2 == null)
          type2 = type6.GetNestedType("myOwner");
        if ((object) type3 == null)
          type3 = type6.GetNestedType("workGroupID");
        if ((object) type4 == null)
          type4 = type6.GetNestedType("myWorkGroup");
        if ((object) type5 == null)
          type5 = type6.GetNestedType("currentOwnerID");
      }
      return (type1 ?? throw new NotSupportedException(MakeError<OwnedFilter.ownerID>()), type2 ?? throw new NotSupportedException(MakeError<OwnedFilter.myOwner>()), type3 ?? throw new NotSupportedException(MakeError<OwnedFilter.workGroupID>()), type4 ?? throw new NotSupportedException(MakeError<OwnedFilter.myWorkGroup>()), type5 ?? throw new NotSupportedException(MakeError<OwnedFilter.currentOwnerID>()));

      string MakeError<TField>()
      {
        return $"The filter class {filter.FullName} must contain a bql-field named {typeof (TField).Name} in order to be used with {typeof (OwnedFilter.ProjectionAttribute).FullName}.";
      }
    }

    protected static System.Type CurrentValue<Field>(System.Type filter) where Field : IBqlField
    {
      for (System.Type type = filter; type != (System.Type) null; type = type.BaseType)
      {
        System.Type nestedType = type.GetNestedType(typeof (Field).Name);
        if (nestedType != (System.Type) null)
          return BqlCommand.Compose(typeof (CurrentValue<>), nestedType);
      }
      return (System.Type) null;
    }

    public override void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      sender.Graph.RowSelected.AddAbstractHandler(this.filterType, (System.Action<AbstractEvents.IRowSelected<object>>) (e =>
      {
        PXCache cache1 = e.Cache;
        object row1 = e.Row;
        bool? nullable;
        int num1;
        if (e.Row != null)
        {
          nullable = (bool?) e.Cache.GetValue<OwnedFilter.myOwner>(e.Row);
          bool flag = false;
          num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
        PXUIFieldAttribute.SetEnabled<OwnedFilter.ownerID>(cache1, row1, num1 != 0);
        PXCache cache2 = e.Cache;
        object row2 = e.Row;
        int num2;
        if (e.Row != null)
        {
          nullable = (bool?) e.Cache.GetValue<OwnedFilter.myWorkGroup>(e.Row);
          bool flag = false;
          num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        else
          num2 = 1;
        PXUIFieldAttribute.SetEnabled<OwnedFilter.workGroupID>(cache2, row2, num2 != 0);
      }));
      sender.Graph.RowUpdated.AddAbstractHandler(this.filterType, (System.Action<AbstractEvents.IRowUpdated<object>>) (e =>
      {
        if (e.Cache.ObjectsEqual<OwnedFilter.myOwner>(e.Row, e.OldRow))
          return;
        bool? nullable = (bool?) e.Cache.GetValue<OwnedFilter.myOwner>(e.Row);
        bool flag = true;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          return;
        e.Cache.SetValue<OwnedFilter.ownerID>(e.Row, (object) null);
      }));
    }

    private static class PhTarget
    {
      [PXHidden]
      public class OwnerID : BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhTarget.OwnerID>
      {
      }

      [PXHidden]
      public class WorkGroupID : 
        BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhTarget.WorkGroupID>
      {
      }
    }

    private static class PhFilter
    {
      [PXHidden]
      public class OwnerID : BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhFilter.OwnerID>
      {
      }

      [PXHidden]
      public class WorkGroupID : 
        BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhFilter.WorkGroupID>
      {
      }

      [PXHidden]
      public class MyWorkGroup : 
        BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhFilter.MyWorkGroup>
      {
      }

      [PXHidden]
      public class CurrentOwnerID : 
        BqlPlaceholder.Named<OwnedFilter.ProjectionAttribute.PhFilter.CurrentOwnerID>
      {
      }
    }
  }
}
