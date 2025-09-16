// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAccessRightsMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.SM;

/// <exclude />
public class WikiAccessRightsMaintenance : PXGraph<WikiAccessRightsMaintenance>
{
  public PXSave<RolesFilter> Save;
  public PXCancel<RolesFilter> Cancel;
  public PXAction<RolesFilter> First;
  public PXAction<RolesFilter> Prev;
  public PXAction<RolesFilter> Next;
  public PXAction<RolesFilter> Last;
  public PXFilter<RolesFilter> RolesRecords;
  public PXSelectWikiFoldersTree Folders;
  public PXSelectOrderBy<WikiPage, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Children;
  public PXSelect<WikiAccessRights, Where<WikiAccessRights.pageID, Equal<Required<WikiAccessRights.pageID>>>> PageAccessRights;

  public WikiAccessRightsMaintenance() => Wiki.BlockIfOnlineHelpIsOn();

  protected IEnumerable children([PXGuid] Guid? parent)
  {
    WikiPage current1 = this.Children.Current;
    List<WikiPage> wikiPageList = new List<WikiPage>();
    this.Children.Cache.Clear();
    if (this.RolesRecords.Current == null || string.IsNullOrEmpty(this.RolesRecords.Current.Rolename))
      return (IEnumerable) wikiPageList;
    if (!parent.HasValue)
      parent = new Guid?(Guid.Empty);
    HttpContext current2 = HttpContext.Current;
    HttpContext.Current = (HttpContext) null;
    PXCache cach = this.Caches[typeof (WikiPage)];
    foreach (PXResult<WikiPage> pxResult1 in PXSelectBase<WikiPage, PXSelectReadonly<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) parent))
    {
      WikiPage wikiPage = (WikiPage) pxResult1;
      PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.WikiProvider.FindSiteMapNodeFromKeyUnsecure(wikiPage.PageID.Value);
      WikiPage instance = (WikiPage) cach.CreateInstance();
      instance.Name = wikiPage.Name;
      WikiDescriptor wikiDescriptor = (WikiDescriptor) PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor, Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) wikiPage.PageID);
      bool flag = PXSiteMap.WikiProvider.HasAccessRights(wikiPage.ParentUID.Value);
      PXResultset<WikiAccessRights> pxResultset = this.PageAccessRights.Select((object) wikiPage.PageID.Value);
      instance.Title = wikiDescriptor != null ? wikiDescriptor.WikiTitle : nodeFromKeyUnsecure.Title;
      instance.PageID = wikiPage.PageID;
      instance.ArticleType = wikiPage.ArticleType;
      instance.ParentUID = wikiPage.ParentUID;
      instance.Number = wikiPage.Number;
      instance.AccessRights = new short?((short) -1);
      instance.ParentAccessRights = new short?((short) -1);
      if (flag && PXSiteMap.WikiProvider.HasAccessRights(instance.ParentUID.Value, this.RolesRecords.Current.Rolename))
        instance.ParentAccessRights = new short?((short) PXSiteMap.WikiProvider.GetAccessRights(instance.ParentUID.Value, this.RolesRecords.Current.Rolename));
      foreach (PXResult<WikiAccessRights> pxResult2 in pxResultset)
      {
        WikiAccessRights wikiAccessRights = (WikiAccessRights) pxResult2;
        if (wikiAccessRights.RoleName == this.RolesRecords.Current.Rolename)
          instance.AccessRights = wikiAccessRights.AccessRights;
      }
      this.Children.Insert(instance);
      wikiPageList.Add(instance);
    }
    HttpContext.Current = current2;
    this.Children.Cache.IsDirty = false;
    if (current1 != null)
      this.Children.Current = current1;
    return (IEnumerable) wikiPageList;
  }

  [PXUIField(DisplayName = "First", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXFirstButton]
  public IEnumerable first(PXAdapter adapter)
  {
    Roles roles = (Roles) PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>>, OrderBy<Asc<Roles.rolename>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.RolesRecords.Current.ApplicationName);
    if (roles != null)
    {
      this.RolesRecords.Current.Rolename = roles.Rolename;
      this.RolesRecords.Current.Descr = roles.Descr;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public IEnumerable prev(PXAdapter adapter)
  {
    Roles roles = (Roles) PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>, And<Roles.rolename, Less<Required<Roles.rolename>>>>, OrderBy<Desc<Roles.rolename>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.RolesRecords.Current.ApplicationName, (object) this.RolesRecords.Current.Rolename);
    if (roles != null)
    {
      this.RolesRecords.Current.Rolename = roles.Rolename;
      this.RolesRecords.Current.Descr = roles.Descr;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public IEnumerable next(PXAdapter adapter)
  {
    Roles roles = (Roles) PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>, And<Roles.rolename, Greater<Required<Roles.rolename>>>>, OrderBy<Asc<Roles.rolename>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.RolesRecords.Current.ApplicationName, (object) this.RolesRecords.Current.Rolename);
    if (roles != null)
    {
      this.RolesRecords.Current.Rolename = roles.Rolename;
      this.RolesRecords.Current.Descr = roles.Descr;
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Last", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLastButton]
  public IEnumerable last(PXAdapter adapter)
  {
    Roles roles = (Roles) PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>>, OrderBy<Desc<Roles.rolename>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.RolesRecords.Current.ApplicationName);
    if (roles != null)
    {
      this.RolesRecords.Current.Rolename = roles.Rolename;
      this.RolesRecords.Current.Descr = roles.Descr;
    }
    return adapter.Get();
  }

  protected void RolesFilter_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    RolesFilter newRow = (RolesFilter) e.NewRow;
    if (newRow == null)
      return;
    Roles roles = (Roles) PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>, And<Roles.rolename, Equal<Required<Roles.rolename>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) newRow.ApplicationName, (object) newRow.Rolename);
    if (roles == null)
      return;
    newRow.Descr = roles.Descr;
  }

  protected void WikiPage_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    WikiPage newRow = (WikiPage) e.NewRow;
    if (!(this.Caches[typeof (WikiAccessRights)].CreateInstance() is WikiAccessRights instance))
      return;
    instance.AccessRights = newRow.AccessRights;
    instance.ApplicationName = this.RolesRecords.Current.ApplicationName;
    instance.PageID = newRow.PageID;
    instance.RoleName = this.RolesRecords.Current.Rolename;
    short? accessRights = newRow.AccessRights;
    int? nullable = accessRights.HasValue ? new int?((int) accessRights.GetValueOrDefault()) : new int?();
    int num = -1;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      this.Caches[typeof (WikiAccessRights)].Delete((object) instance);
    else
      this.Caches[typeof (WikiAccessRights)].Update((object) instance);
  }

  protected void WikiPage_AccessRights_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    WikiPage row = (WikiPage) e.Row;
    short newValue = (short) e.NewValue;
    short? accessRights = row.AccessRights;
    int? nullable = accessRights.HasValue ? new int?((int) accessRights.GetValueOrDefault()) : new int?();
    int num = -1;
    if ((nullable.GetValueOrDefault() == num & nullable.HasValue || newValue != (short) -1) && newValue != (short) -1)
      return;
    this.CheckChildrenNodes(row.PageID, this.RolesRecords.Current.Isinherited.Value, this.RolesRecords.Current.ApplicationName, this.RolesRecords.Current.Rolename);
  }

  protected void WikiPage_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void WikiPage_Name_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    string returnValue = e.ReturnValue as string;
    if (string.IsNullOrEmpty(returnValue) || returnValue[0] != '\u0001')
      return;
    e.ReturnValue = (object) returnValue.Substring(1);
  }

  public override void Persist()
  {
    int num = this.Caches[typeof (WikiAccessRights)].IsDirty ? 1 : 0;
    base.Persist();
    if (num == 0)
      return;
    PXSiteMap.Provider.Clear();
    PXSiteMap.WikiProvider.Clear();
    PXAccess.Clear();
  }

  public void CheckChildrenNodes(
    Guid? parent,
    bool Isinherited,
    string ApplicationName,
    string RoleName)
  {
    if (!this.ChildrenHaveCustomRights(parent, ApplicationName, RoleName) || !Isinherited)
      return;
    this.ResetChildrenRights(parent, ApplicationName, RoleName);
  }

  public bool ChildrenHaveCustomRights(Guid? parent, string ApplicationName, string RoleName)
  {
    if (!parent.HasValue)
      return false;
    List<Guid?> nullableList = new List<Guid?>();
    foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) parent))
    {
      WikiPage wikiPage = (WikiPage) pxResult;
      nullableList.Add(wikiPage.PageID);
      if ((WikiAccessRights) PXSelectBase<WikiAccessRights, PXSelect<WikiAccessRights, Where<WikiAccessRights.applicationName, Equal<Required<WikiAccessRights.applicationName>>, And<WikiAccessRights.roleName, Equal<Required<WikiAccessRights.roleName>>, And<WikiAccessRights.pageID, Equal<Required<WikiAccessRights.pageID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) ApplicationName, (object) RoleName, (object) wikiPage.PageID) != null)
        return true;
    }
    using (List<Guid?>.Enumerator enumerator = nullableList.GetEnumerator())
    {
      if (enumerator.MoveNext())
        return this.ChildrenHaveCustomRights(enumerator.Current, ApplicationName, RoleName);
    }
    return false;
  }

  public void ResetChildrenRights(Guid? parent, string ApplicationName, string RoleName)
  {
    if (!parent.HasValue)
      return;
    foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) parent))
    {
      WikiPage wikiPage = (WikiPage) pxResult;
      this.ResetChildrenRights(wikiPage.PageID, ApplicationName, RoleName);
      WikiAccessRights wikiAccessRights = (WikiAccessRights) PXSelectBase<WikiAccessRights, PXSelect<WikiAccessRights, Where<WikiAccessRights.applicationName, Equal<Required<WikiAccessRights.applicationName>>, And<WikiAccessRights.roleName, Equal<Required<WikiAccessRights.roleName>>, And<WikiAccessRights.pageID, Equal<Required<WikiAccessRights.pageID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) ApplicationName, (object) RoleName, (object) wikiPage.PageID);
      if (wikiAccessRights != null)
        this.Caches[typeof (WikiAccessRights)].Delete((object) wikiAccessRights);
    }
  }
}
