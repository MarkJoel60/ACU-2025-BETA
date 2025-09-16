// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageStatusFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPageStatusFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _WikiID;
  protected int? _StatusID;
  protected bool? _MyOwner;
  protected Guid? _OwnerID;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;
  protected int? _TagID;
  protected Guid? _UserID;
  protected Guid? _FolderID;
  protected System.DateTime? _CreatedFrom;
  protected System.DateTime? _CreatedTill;
  protected bool? _MyEscalated;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Wiki Name")]
  [PXSelector(typeof (Search<WikiPage.pageID, Where<WikiPage.wikiID, Equal<emptyGuid>>>), DescriptionField = typeof (WikiPage.name))]
  public virtual Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Status")]
  [WikiPageStatus.List]
  public int? StatusID
  {
    get => this._StatusID;
    set => this._StatusID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? CurrentOwnerID => new Guid?(PXAccess.GetUserID());

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Assigned To")]
  public virtual Guid? OwnerID
  {
    get
    {
      bool? owner = this._MyOwner;
      bool flag = true;
      return !(owner.GetValueOrDefault() == flag & owner.HasValue) ? this._OwnerID : this.CurrentOwnerID;
    }
    set => this._OwnerID = value;
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

  [PXDBInt]
  [PXSelector(typeof (Search<WikiTag.tagID, Where<WikiTag.wikiID, Equal<Current<WikiPageStatusFilter.wikiID>>>>), SubstituteKey = typeof (WikiTag.description))]
  [PXUIField(DisplayName = "Tag")]
  public virtual int? TagID
  {
    get => this._TagID;
    set => this._TagID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Author")]
  [PXSelector(typeof (Users.pKID), DescriptionField = typeof (Users.username))]
  public virtual Guid? UserID
  {
    get => this._UserID;
    set => this._UserID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Folder")]
  [PXFormula(typeof (Default<WikiPageStatusFilter.wikiID>))]
  public virtual Guid? FolderID
  {
    get => this._FolderID;
    set => this._FolderID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Created From")]
  public virtual System.DateTime? CreatedFrom
  {
    get => this._CreatedFrom;
    set => this._CreatedFrom = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "To")]
  public virtual System.DateTime? CreatedTill
  {
    get => this._CreatedTill;
    set => this._CreatedTill = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "Display Escalated", Visibility = PXUIVisibility.Visible)]
  public virtual bool? MyEscalated
  {
    get => this._MyEscalated;
    set => this._MyEscalated = value;
  }

  public abstract class wikiID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageStatusFilter.wikiID>
  {
  }

  public abstract class statusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageStatusFilter.statusID>
  {
  }

  /// <exclude />
  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiPageStatusFilter.currentOwnerID>
  {
  }

  /// <exclude />
  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageStatusFilter.myOwner>
  {
  }

  /// <exclude />
  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageStatusFilter.ownerID>
  {
  }

  /// <exclude />
  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageStatusFilter.workGroupID>
  {
  }

  /// <exclude />
  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageStatusFilter.myWorkGroup>
  {
  }

  /// <exclude />
  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageStatusFilter.filterSet>
  {
  }

  public abstract class tagID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageStatusFilter.tagID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageStatusFilter.userID>
  {
  }

  public abstract class folderID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageStatusFilter.folderID>
  {
  }

  public abstract class createdFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPageStatusFilter.createdFrom>
  {
  }

  public abstract class createdTill : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPageStatusFilter.createdTill>
  {
  }

  public abstract class myEscalated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageStatusFilter.myEscalated>
  {
  }

  public class ProjectionAttribute : OwnedEscalatedFilter.ProjectionAttribute
  {
    public ProjectionAttribute()
      : this(typeof (WikiPageStatusFilter), typeof (WikiPage.approvalGroupID), typeof (WikiPage.approvalUserID), typeof (WikiPage.lastModifiedDateTime), typeof (Where<WikiPage.articleType, GreaterEqual<WikiArticleType.article>>), typeof (Where<CurrentValue<WikiPageStatusFilter.filterSet>, Equal<False>>))
    {
    }

    public ProjectionAttribute(System.Type filter, System.Type groupID, System.Type ownerID, System.Type pendingDate)
      : base(filter, WikiPageStatusFilter.ProjectionAttribute.Compose(BqlCommand.GetItemType(groupID), WikiPageStatusFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate)))
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
      : base(filter, WikiPageStatusFilter.ProjectionAttribute.Compose(select, join, WikiPageStatusFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate), aggregate))
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
      : base(filter, WikiPageStatusFilter.ProjectionAttribute.Compose(select, join, WikiPageStatusFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate, (System.Type) null, paramWhere), aggregate))
    {
    }

    public ProjectionAttribute(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate,
      System.Type where,
      System.Type paramWhere)
      : base(filter, WikiPageStatusFilter.ProjectionAttribute.Compose(BqlCommand.GetItemType(groupID), WikiPageStatusFilter.ProjectionAttribute.ComposeWhere(filter, groupID, ownerID, pendingDate, where, paramWhere)))
    {
    }

    public ProjectionAttribute(System.Type filter, System.Type select)
      : base(filter, select)
    {
    }

    public new static System.Type Compose(System.Type selectType, System.Type where)
    {
      return BqlCommand.Compose(typeof (Select<,>), selectType, where);
    }

    public new static System.Type Compose(System.Type selectType, System.Type join, System.Type where, System.Type aggregate)
    {
      return !(aggregate != (System.Type) null) ? BqlCommand.Compose(typeof (Select2<,,>), selectType, join, where) : BqlCommand.Compose(typeof (Select5<,,,>), selectType, join, where, aggregate);
    }

    public new static System.Type ComposeWhere(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate)
    {
      return BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.ownerID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.ownerID>(filter), typeof (Equal<>), ownerID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.workGroupID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.workGroupID>(filter), typeof (Equal<>), groupID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.myWorkGroup>(filter), typeof (Equal<PX.Data.True>), typeof (And<,>), groupID, typeof (InMember<>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.currentOwnerID>(filter), typeof (PX.Data.Or<>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.myEscalated>(filter), typeof (Equal<PX.Data.True>), typeof (PX.Data.And<>), typeof (Where<,,>), groupID, typeof (EscalatedObsolete<,,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.currentOwnerID>(filter), groupID, ownerID, pendingDate, typeof (Or<,>), groupID, typeof (PX.Data.IsNull));
    }

    public new static System.Type ComposeWhere(
      System.Type filter,
      System.Type groupID,
      System.Type ownerID,
      System.Type pendingDate,
      System.Type where,
      System.Type paramWhere)
    {
      System.Type type = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.ownerID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.ownerID>(filter), typeof (Equal<>), ownerID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.workGroupID>(filter), typeof (PX.Data.IsNotNull), typeof (And<,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.workGroupID>(filter), typeof (Equal<>), groupID, typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.myWorkGroup>(filter), typeof (Equal<PX.Data.True>), typeof (And<,>), groupID, typeof (InMember<>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.currentOwnerID>(filter), typeof (Or2<,>), typeof (Where<,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.myEscalated>(filter), typeof (Equal<PX.Data.True>), typeof (PX.Data.And<>), typeof (Where<,,>), groupID, typeof (EscalatedObsolete<,,,>), OwnedFilter.ProjectionAttribute.CurrentValue<WikiPageStatusFilter.currentOwnerID>(filter), groupID, ownerID, pendingDate, typeof (Or<,>), groupID, typeof (PX.Data.IsNull), typeof (PX.Data.Or<>), paramWhere);
      if (where != (System.Type) null)
        type = BqlCommand.Compose(typeof (Where2<,>), where, typeof (PX.Data.And<>), type);
      return type;
    }

    public override void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      sender.Graph.RowSelected.AddHandler<WikiPageStatusFilter>(WikiPageStatusFilter.ProjectionAttribute.\u003C\u003EO.\u003C0\u003E__OnRowSelected ?? (WikiPageStatusFilter.ProjectionAttribute.\u003C\u003EO.\u003C0\u003E__OnRowSelected = new PXRowSelected(WikiPageStatusFilter.ProjectionAttribute.OnRowSelected)));
    }

    protected static void OnRowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      if (!(e.Row is WikiPageStatusFilter row))
        return;
      bool? filterSet = row.FilterSet;
      bool flag1 = true;
      if (filterSet.GetValueOrDefault() == flag1 & filterSet.HasValue)
        row.StatusID = new int?(1);
      PXCache cache1 = cache;
      WikiPageStatusFilter data = row;
      filterSet = row.FilterSet;
      bool flag2 = true;
      int num = !(filterSet.GetValueOrDefault() == flag2 & filterSet.HasValue) ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<WikiPageStatusFilter.statusID>(cache1, (object) data, num != 0);
    }
  }
}
