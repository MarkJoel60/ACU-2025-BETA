// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBaseWikiScreenToSiteMapAddHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

internal class PXBaseWikiScreenToSiteMapAddHelper<SiteMap> : 
  PXBaseScreenToSiteMapAddHelper<WikiDescriptor, SiteMap>
  where SiteMap : PX.SM.SiteMap, new()
{
  private System.Type _siteMapParentField;
  private PXView FindByPositionView;
  private System.Type _defaultSiteMapTitleField;

  public PXBaseWikiScreenToSiteMapAddHelper(
    string screenIDPrefix,
    string urlPrefix,
    System.Type siteMapParentField,
    System.Type siteMapTitleField,
    System.Type defaultSiteMapTitleField,
    System.Type siteMapIDField,
    System.Type urlParamField,
    string placeholderID,
    bool generateNewScreenIDFromKeys,
    PXCache entityCache,
    PXCache siteMapTreeCache)
    : base(screenIDPrefix, urlPrefix, siteMapTitleField, defaultSiteMapTitleField, siteMapIDField, urlParamField, placeholderID, generateNewScreenIDFromKeys, entityCache, siteMapTreeCache)
  {
    this._siteMapParentField = siteMapParentField;
    this._defaultSiteMapTitleField = defaultSiteMapTitleField;
    System.Type nestedType1 = typeof (SiteMap).GetNestedType(typeof (PX.SM.SiteMap.parentID).Name);
    System.Type nestedType2 = typeof (SiteMap).GetNestedType(typeof (PX.SM.SiteMap.position).Name);
    this.FindByPositionView = new PXView(this.Graph, false, BqlCommand.CreateInstance(BqlCommand.Compose(typeof (Select<,,>), typeof (SiteMap), typeof (Where<,>), nestedType1, typeof (Equal<>), typeof (Required<>), nestedType1, typeof (OrderBy<>), typeof (Desc<>), nestedType2)));
  }

  public override bool Persist(WikiDescriptor row)
  {
    base.Persist(row);
    bool isSiteMapAltered = false;
    if (row != null)
    {
      Guid? siteMapParent = this.EntityCache.GetValue((object) row, this._siteMapParentField.Name) as Guid?;
      if (siteMapParent.HasValue)
      {
        Guid? siteMapID = this.EntityCache.GetValue((object) row, this.SiteMapIDField.Name) as Guid?;
        this.FindByNodeIDView.Cache.ClearQueryCache();
        SiteMap rowSiteMapNode = this.FindByNodeIDView.SelectSingle((object) siteMapID) as SiteMap;
        string siteMapTitle = this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name) as string;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if ((object) rowSiteMapNode == null)
          {
            this.InsertNewNode(row, siteMapParent, siteMapTitle, siteMapID);
            isSiteMapAltered = true;
          }
          else
            this.UpdateNode(siteMapParent, siteMapTitle, rowSiteMapNode, ref isSiteMapAltered);
          transactionScope.Complete();
        }
      }
    }
    return isSiteMapAltered;
  }

  public override void UpdateSiteMapParent(WikiDescriptor row)
  {
    if (row == null)
      return;
    string str1 = this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name) as string;
    Guid? newValue = this.EntityCache.GetValue((object) row, this._siteMapParentField.Name) as Guid?;
    Guid? nullable1 = this.EntityCache.GetValue((object) row, this.SiteMapIDField.Name) as Guid?;
    Guid? nullable2 = newValue;
    Guid? nullable3 = nullable1;
    if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      this.EntityCache.SetValue((object) row, this._siteMapParentField.Name, (object) null);
      this.EntityCache.RaiseExceptionHandling(this._siteMapParentField.Name, (object) row, (object) newValue, (Exception) new PXSetPropertyException("A circular reference is not allowed."));
    }
    else
    {
      if (!newValue.HasValue || !string.IsNullOrEmpty(str1))
        return;
      string str2 = this.EntityCache.GetValue((object) row, this._defaultSiteMapTitleField.Name) as string;
      this.EntityCache.SetValue((object) row, this.SiteMapTitleField.Name, (object) str2);
      if (nullable1.HasValue)
        return;
      this.EntityCache.SetValue((object) row, this.SiteMapIDField.Name, (object) Guid.NewGuid());
    }
  }

  public override void InitializeSiteMapFields(WikiDescriptor row)
  {
    if (row == null)
      return;
    Guid? nullable = this.EntityCache.GetValue((object) row, this.SiteMapIDField.Name) as Guid?;
    this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name)?.ToString();
    if (!nullable.HasValue)
      return;
    this.InsertSiteMapTreeCache.ClearQueryCache();
    PXSiteMap.Provider.ClearThreadSlot();
    PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nullable.Value);
    if (nodeFromKeyUnsecure == null)
      return;
    this.EntityCache.SetValue((object) row, this.SiteMapTitleField.Name, (object) nodeFromKeyUnsecure.Title);
    this.EntityCache.SetValue((object) row, this._siteMapParentField.Name, (object) nodeFromKeyUnsecure.ParentID);
  }

  public override bool DeleteObsoleteSiteMapNode(WikiDescriptor row, PXDBOperation operation)
  {
    bool flag = false;
    if (row != null)
    {
      Guid? nullable1 = this.EntityCache.GetValue((object) row, this.SiteMapIDField.Name) as Guid?;
      Guid? nullable2 = this.EntityCache.GetValue((object) row, this._siteMapParentField.Name) as Guid?;
      if (nullable1.HasValue && ((operation & PXDBOperation.Delete) == PXDBOperation.Delete || !nullable2.HasValue && (operation & PXDBOperation.Update) == PXDBOperation.Update))
      {
        if (this.FindByNodeIDView.SelectSingle((object) nullable1) is SiteMap row1)
        {
          this.InsertSiteMapTreeCache.PersistDeleted((object) row1);
          flag = true;
        }
      }
    }
    return flag;
  }

  private void InsertNewNode(
    WikiDescriptor row,
    Guid? siteMapParent,
    string siteMapTitle,
    Guid? siteMapID)
  {
    this.InsertNewNode(row, siteMapTitle, siteMapID);
    SiteMap instance = this.InsertSiteMapTreeCache.CreateInstance() as SiteMap;
    instance.ParentID = siteMapParent;
    instance.Position = this.GeneratePosition(siteMapParent);
  }

  private void UpdateNode(
    Guid? siteMapParent,
    string siteMapTitle,
    SiteMap rowSiteMapNode,
    ref bool isSiteMapAltered)
  {
    this.UpdateNode(siteMapTitle, rowSiteMapNode, ref isSiteMapAltered);
    Guid? parentId = rowSiteMapNode.ParentID;
    Guid? nullable = siteMapParent;
    if ((parentId.HasValue == nullable.HasValue ? (parentId.HasValue ? (parentId.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    rowSiteMapNode.ParentID = siteMapParent;
    if (!rowSiteMapNode.ParentID.HasValue)
      rowSiteMapNode.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
    rowSiteMapNode = this.InsertSiteMapTreeCache.Update((object) rowSiteMapNode) as SiteMap;
    this.InsertSiteMapTreeCache.PersistUpdated((object) rowSiteMapNode);
    isSiteMapAltered = true;
  }

  public double? GeneratePosition(Guid? parentID)
  {
    if (!(this.FindByPositionView.SelectSingle((object) parentID) is SiteMap siteMap))
      return new double?(0.0);
    double? position = siteMap.Position;
    double num = 1.0;
    return !position.HasValue ? new double?() : new double?(position.GetValueOrDefault() + num);
  }
}
