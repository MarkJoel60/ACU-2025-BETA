// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.TagMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Wiki.Tags;

public class TagMaint : PXGraph<TagMaint>
{
  public PXFilter<SelectedNode> SelectedFolders;
  public PXSave<SelectedNode> Save;
  public PXCancel<SelectedNode> Cancel;
  public PXAction<SelectedNode> AddTag;
  public PXAction<SelectedNode> DeleteTag;
  public PXSelectOrderBy<Tag, OrderBy<Asc<Tag.tagCD>>> Folders;
  public PXSelectOrderBy<FolderTag, OrderBy<Asc<FolderTag.tagCD>>> ParentFolders;
  public PXSelect<Tag, Where<Tag.tagID, Equal<Current<Tag.tagID>>>> CurrentTag;
  public PXSelect<RoleInTag> AccessRights;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<Role> Members;

  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Enabled = true)]
  [PXButton]
  public virtual IEnumerable addTag(PXAdapter adapter)
  {
    Guid? tagId = (Guid?) this.Folders.Current?.TagID;
    this.Folders.Cache.ActiveRow = (IBqlTable) this.Caches<Tag>().Insert(new Tag()
    {
      TagID = new Guid?(Guid.NewGuid()),
      TagCD = PXMessages.LocalizeNoPrefix("<Enter Tag>"),
      ParentTagID = tagId
    });
    return adapter.Get();
  }

  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Enabled = true)]
  [PXButton]
  public virtual IEnumerable deleteTag(PXAdapter adapter)
  {
    if ((UploadFileTag) PXSelectBase<UploadFileTag, PXSelect<UploadFileTag, Where<UploadFileTag.tagID, Equal<Required<UploadFileTag.tagID>>>>.Config>.Select((PXGraph) this, (object) this.Folders.Current.TagID) != null)
      throw new PXException("The tag cannot be deleted because there is at least one file associated with this tag.");
    this.Caches<Tag>().Delete(this.Folders.Current);
    return adapter.Get();
  }

  protected virtual IEnumerable folders([PXGuid] Guid? tagID)
  {
    return (IEnumerable) this.GetFolders(tagID);
  }

  private IEnumerable<Tag> GetFolders(Guid? tagID)
  {
    if (!tagID.HasValue)
    {
      yield return CommonTags.AllTags;
      yield return CommonTags.Untagged;
    }
    else
    {
      foreach (Tag child in this.GetChildren(tagID.Value))
      {
        if (!string.IsNullOrEmpty(child.TagCD))
          yield return child;
      }
    }
  }

  private IEnumerable<Tag> GetChildren(Guid tagID)
  {
    return PXSelectBase<Tag, PXSelect<Tag, Where<Tag.parentTagID, Equal<Required<Tag.parentTagID>>>>.Config>.Select((PXGraph) this, (object) tagID).RowCast<Tag>();
  }

  protected virtual IEnumerable parentFolders([PXGuid] Guid? tagID)
  {
    return (IEnumerable) this.GetParentFolders(tagID);
  }

  private IEnumerable<FolderTag> GetParentFolders(Guid? tagID)
  {
    TagMaint graph = this;
    if (!tagID.HasValue)
    {
      FolderTag parentFolder = new FolderTag();
      parentFolder.TagID = new Guid?(CommonTags.AllTagsID);
      parentFolder.TagCD = "All Tags";
      parentFolder.NoteID = new Guid?(CommonTags.AllTagsID);
      yield return parentFolder;
    }
    else
    {
      Tag insertedTag = graph.Caches<Tag>().Rows.Inserted.FirstOrDefault<Tag>();
      foreach (Tag child in graph.GetChildren(tagID.Value))
      {
        if (!string.IsNullOrEmpty(child.TagCD))
        {
          Guid? tagId1 = child.TagID;
          Guid? tagId2 = graph.Folders.Current.TagID;
          if ((tagId1.HasValue == tagId2.HasValue ? (tagId1.HasValue ? (tagId1.GetValueOrDefault() == tagId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
          {
            tagId2 = child.TagID;
            tagId1 = (Guid?) insertedTag?.TagID;
            if ((tagId2.HasValue == tagId1.HasValue ? (tagId2.HasValue ? (tagId2.GetValueOrDefault() == tagId1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
            {
              FolderTag parentFolder = new FolderTag();
              parentFolder.TagID = child.TagID;
              parentFolder.TagCD = child.TagCD;
              parentFolder.NoteID = child.NoteID;
              yield return parentFolder;
            }
          }
        }
      }
    }
  }

  protected virtual IEnumerable members() => this.GetMembers();

  private IEnumerable GetMembers()
  {
    this.Members.Cache.Clear();
    List<Role> members = new List<Role>();
    Tag current = this.Folders.Current;
    if (current != null)
    {
      Guid? tagId = current.TagID;
      Guid allTagsId = CommonTags.AllTagsID;
      if ((tagId.HasValue ? (tagId.HasValue ? (tagId.GetValueOrDefault() != allTagsId ? 1 : 0) : 0) : 1) != 0)
      {
        foreach (PXResult<PX.SM.Roles, RoleInTag> pxResult in PXSelectBase<PX.SM.Roles, PXSelectJoin<PX.SM.Roles, LeftJoin<RoleInTag, On<PX.SM.Roles.rolename, Equal<RoleInTag.rolename>, And<RoleInTag.tagID, Equal<Required<RoleInTag.tagID>>>>>, Where<PX.SM.Roles.applicationName, Equal<Required<PX.SM.Roles.applicationName>>>>.Config>.Select((PXGraph) this, (object) current.TagID, (object) "/"))
        {
          PX.SM.Roles roles = pxResult.GetItem<PX.SM.Roles>();
          RoleInTag roleInTag = pxResult.GetItem<RoleInTag>();
          Role instance = this.Members.Cache.CreateInstance() as Role;
          instance.NodeID = current.NoteID;
          instance.RoleName = roles.Rolename;
          instance.RoleDescr = roles.Descr;
          instance.Guest = roles.Guest;
          instance.RoleRight = new int?((int) ((short?) roleInTag?.AccessRights).GetValueOrDefault());
          Role role = this.Members.Insert(instance);
          if (role != null && PXAccess.IsRoleEnabled(role.RoleName))
            members.Add(role);
        }
        this.Members.Cache.IsDirty = false;
      }
    }
    return (IEnumerable) members;
  }

  protected virtual void _(Events.RowSelected<Tag> e)
  {
    if (e.Row == null)
      return;
    Guid? tagId = this.Folders.Current.TagID;
    Guid? parentTagId = this.Folders.Current.ParentTagID;
    bool flag1 = CommonTags.IsTagCommon(tagId);
    PXUIFieldAttribute.SetVisible<Tag.tagCD>(this.Caches[typeof (Tag)], (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<Tag.parentTagID>(this.Caches[typeof (Tag)], (object) null, !flag1);
    this.Caches[typeof (Tag)].AllowInsert = !flag1;
    this.Caches[typeof (Tag)].AllowDelete = !flag1;
    this.Caches[typeof (Tag)].AllowUpdate = !flag1;
    this.Caches[typeof (RoleInTag)].AllowInsert = !flag1;
    this.Caches[typeof (RoleInTag)].AllowUpdate = !flag1;
    bool flag2 = NonGenericIEnumerableExtensions.Any_(this.Caches[typeof (Tag)].Inserted);
    PXAction<SelectedNode> addTag = this.AddTag;
    Guid? nullable = tagId;
    Guid untaggedId = CommonTags.UntaggedID;
    int num = (nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != untaggedId ? 1 : 0) : 0) : 1) == 0 ? 0 : (!flag2 ? 1 : 0);
    addTag.SetEnabled(num != 0);
    this.DeleteTag.SetEnabled(!flag1);
  }

  protected virtual void _(Events.RowDeleted<Tag> e)
  {
    Tag row = e.Row;
    if ((row != null ? (!row.TagID.HasValue ? 1 : 0) : 1) != 0)
      return;
    this.deleteRecurring(e.Row);
  }

  private void deleteRecurring(Tag tag)
  {
    if (tag == null)
      return;
    foreach (PXResult<Tag> pxResult in PXSelectBase<Tag, PXSelect<Tag, Where<Tag.parentTagID, Equal<Required<Tag.tagID>>>>.Config>.Select((PXGraph) this, (object) tag.TagID))
      this.deleteRecurring((Tag) pxResult);
    this.Caches<Tag>().Delete(tag);
  }

  protected virtual void _(Events.FieldVerifying<Tag, Tag.parentTagID> e)
  {
    if (e.NewValue == null)
      throw new PXSetPropertyException<Tag.parentTagID>("Parent tag cannot be empty.");
  }

  protected virtual void _(Events.FieldVerifying<Tag, Tag.tagCD> e)
  {
    if (!string.IsNullOrEmpty(e.NewValue as string))
      return;
    e.NewValue = (object) "<Enter Tag>";
  }

  protected virtual void _(Events.FieldUpdated<Tag, Tag.tagCD> e)
  {
    string tagCd = e.Row?.TagCD;
    if (string.IsNullOrEmpty(tagCd))
      return;
    string a = tagCd.Trim();
    if (string.Equals(a, tagCd, StringComparison.OrdinalIgnoreCase))
      return;
    e.Cache.SetValueExt<Tag.tagCD>((object) e.Row, (object) a);
  }

  protected virtual void _(Events.RowUpdated<Role> e)
  {
    Role row = e.Row;
    if (row == null)
      return;
    Guid? nullable1 = new Guid?();
    Guid? nullable2 = row.NodeID;
    Guid untaggedId = CommonTags.UntaggedID;
    if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == untaggedId ? 1 : 0) : 1) : 0) != 0)
    {
      nullable1 = new Guid?(CommonTags.UntaggedID);
    }
    else
    {
      Tag tag = (Tag) PXSelectBase<Tag, PXSelect<Tag, Where<Tag.noteID, Equal<Required<Tag.noteID>>>>.Config>.Select((PXGraph) this, (object) row.NodeID);
      Guid? nullable3;
      if (tag == null)
      {
        nullable2 = new Guid?();
        nullable3 = nullable2;
      }
      else
        nullable3 = tag.TagID;
      nullable1 = nullable3;
    }
    if (!nullable1.HasValue)
      return;
    RoleInTag roleInTag1 = RoleInTag.PK.Find((PXGraph) this, nullable1.Value, row.RoleName);
    bool flag = false;
    if (roleInTag1 == null)
    {
      flag = true;
      roleInTag1 = this.CreateRoleInTag(row, nullable1.Value);
    }
    short? accessRights = roleInTag1.AccessRights;
    int? nullable4 = accessRights.HasValue ? new int?((int) accessRights.GetValueOrDefault()) : new int?();
    int? roleRight1 = row.RoleRight;
    int? nullable5 = roleRight1.HasValue ? new int?((int) (short) roleRight1.GetValueOrDefault()) : new int?();
    if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
      return;
    RoleInTag roleInTag2 = roleInTag1;
    int? roleRight2 = row.RoleRight;
    short? nullable6 = roleRight2.HasValue ? new short?((short) roleRight2.GetValueOrDefault()) : new short?();
    roleInTag2.AccessRights = nullable6;
    if (flag)
      this.AccessRights.Cache.Insert((object) roleInTag1);
    else
      this.AccessRights.Cache.Update((object) roleInTag1);
  }

  private RoleInTag CreateRoleInTag(Role role, Guid tagID)
  {
    RoleInTag instance = (RoleInTag) this.AccessRights.Cache.CreateInstance();
    instance.TagID = new Guid?(tagID);
    instance.Rolename = role.RoleName;
    RoleInTag roleInTag1 = instance;
    Guid? nullable1 = role.CreatedByID;
    Guid? nullable2 = new Guid?(nullable1 ?? this.Accessinfo.UserID);
    roleInTag1.CreatedByID = nullable2;
    instance.CreatedByScreenID = role.CreatedByScreenID ?? this.Accessinfo.ScreenID;
    RoleInTag roleInTag2 = instance;
    System.DateTime? nullable3 = role.CreatedDateTime;
    System.DateTime? nullable4 = new System.DateTime?(nullable3 ?? System.DateTime.Now);
    roleInTag2.CreatedDateTime = nullable4;
    RoleInTag roleInTag3 = instance;
    nullable1 = role.LastModifiedByID;
    Guid? nullable5 = new Guid?(nullable1 ?? this.Accessinfo.UserID);
    roleInTag3.LastModifiedByID = nullable5;
    instance.LastModifiedByScreenID = role.LastModifiedByScreenID ?? this.Accessinfo.ScreenID;
    RoleInTag roleInTag4 = instance;
    nullable3 = role.LastModifiedDateTime;
    System.DateTime? nullable6 = new System.DateTime?(nullable3 ?? System.DateTime.Now);
    roleInTag4.LastModifiedDateTime = nullable6;
    return (RoleInTag) this.AccessRights.Cache.Locate((object) instance) ?? instance;
  }

  protected virtual void _(Events.RowPersisting<Role> e) => e.Cancel = true;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [SingleTagSelector(false, IncludeAllTags = true, IgnoreAccessRights = true)]
  protected virtual void _(Events.CacheAttached<Tag.parentTagID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXRoleRightAttribute))]
  [TagAccessLevelList]
  protected virtual void _(Events.CacheAttached<Role.roleRight> e)
  {
  }

  public override void Persist()
  {
    foreach (Tag tag in this.Caches<Tag>().Rows.Inserted)
    {
      this.CheckTagCD(tag);
      this.CheckParentTag(tag);
    }
    foreach (Tag tag in this.Caches<Tag>().Rows.Updated)
    {
      this.CheckTagCD(tag);
      this.CheckParentTag(tag);
    }
    this.Members.Cache.Clear();
    base.Persist();
    this.Members.View.RequestRefresh();
  }

  protected virtual void CheckTagCD(Tag tag)
  {
    if (!string.IsNullOrEmpty(this.Caches<Tag>().GetStateExt<Tag.tagCD>((object) tag) is PXFieldState stateExt ? stateExt.Error : (string) null))
      throw new PXException(stateExt.Error);
    if (string.Equals(tag.TagCD.Trim(), "<Enter Tag>", StringComparison.OrdinalIgnoreCase))
      throw new PXException("Specify another tag name. The <Enter Tag> name cannot be used.", new object[1]
      {
        (object) "<Enter Tag>"
      });
  }

  protected virtual void CheckParentTag(Tag tag)
  {
    if (!string.IsNullOrEmpty(this.Caches<Tag>().GetStateExt<Tag.parentTagID>((object) tag) is PXFieldState stateExt ? stateExt.Error : (string) null))
      throw new PXException(stateExt.Error);
  }
}
