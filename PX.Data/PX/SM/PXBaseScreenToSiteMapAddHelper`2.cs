// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBaseScreenToSiteMapAddHelper`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

#nullable disable
namespace PX.SM;

public class PXBaseScreenToSiteMapAddHelper<DacType, SiteMap> : 
  PXBaseGenScreenToSiteMapAddHelper<DacType>
  where DacType : IBqlTable
  where SiteMap : PX.SM.SiteMap, new()
{
  private const string DefaultScreenNumber = "000001";
  private readonly string _screenIDPrefix;
  private readonly string _urlPrefix;
  private readonly string _placeholderID;
  private System.Type _siteMapIDField;
  private System.Type _urlParamField;
  private Func<DacType, string> _generateNewUrlFunc;
  private PXView FindByScreenIDView;

  protected PXCache InsertSiteMapTreeCache { get; set; }

  protected System.Type SiteMapTitleField { get; }

  protected PXView FindByNodeIDView { get; set; }

  protected PXGraph Graph { get; }

  public System.Type SiteMapIDField => this._siteMapIDField;

  public PXBaseScreenToSiteMapAddHelper(
    string screenIDPrefix,
    string urlPrefix,
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
    if (!this.ValidateInputParameters(screenIDPrefix, urlPrefix, siteMapTitleField, defaultSiteMapTitleField, siteMapIDField, urlParamField, placeholderID, generateNewScreenIDFromKeys, siteMapTreeCache))
      return;
    this.Graph = siteMapTreeCache.Graph;
    this._screenIDPrefix = screenIDPrefix;
    this._urlPrefix = urlPrefix.EndsWith("?") ? urlPrefix : $"{urlPrefix}?";
    this._placeholderID = placeholderID;
    this.EntityCache = entityCache;
    this.InsertSiteMapTreeCache = (PXCache) new PXCache<SiteMap>(this.Graph);
    this.SiteMapTitleField = siteMapTitleField;
    this._siteMapIDField = siteMapIDField;
    this._urlParamField = urlParamField;
    this._generateNewUrlFunc = !generateNewScreenIDFromKeys ? new Func<DacType, string>(this.GenerateNewUrlFromUrlParamField) : new Func<DacType, string>(this.GenerateNewUrlFromKeys);
    System.Type type = typeof (SiteMap);
    System.Type nestedType1 = typeof (SiteMap).GetNestedType(typeof (PX.SM.SiteMap.nodeID).Name);
    System.Type nestedType2 = typeof (SiteMap).GetNestedType(typeof (PX.SM.SiteMap.screenID).Name);
    typeof (SiteMap).GetNestedType(typeof (PX.SM.SiteMap.parentID).Name);
    this.FindByNodeIDView = new PXView(this.Graph, false, BqlCommand.CreateInstance(BqlCommand.Compose(typeof (Select<,>), type, typeof (Where<,>), nestedType1, typeof (Equal<>), typeof (Required<>), nestedType1)));
    this.FindByScreenIDView = new PXView(this.Graph, false, BqlCommand.CreateInstance(BqlCommand.Compose(typeof (Select<,,>), type, typeof (Where<,>), nestedType2, typeof (Like<>), typeof (Required<>), nestedType2, typeof (OrderBy<>), typeof (Desc<>), nestedType2)));
  }

  public override bool Persist(DacType row)
  {
    bool isSiteMapAltered = false;
    if ((object) row != null)
    {
      string siteMapTitle = this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name) as string;
      if (!string.IsNullOrEmpty(siteMapTitle))
      {
        Guid? siteMapID = this.EntityCache.GetValue((object) row, this._siteMapIDField.Name) as Guid?;
        this.FindByNodeIDView.Cache.ClearQueryCache();
        SiteMap rowSiteMapNode = this.FindByNodeIDView.SelectSingle((object) siteMapID) as SiteMap;
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if ((object) rowSiteMapNode == null)
          {
            this.InsertNewNode(row, siteMapTitle, siteMapID);
            isSiteMapAltered = true;
          }
          else
            this.UpdateNode(siteMapTitle, rowSiteMapNode, ref isSiteMapAltered);
          transactionScope.Complete();
        }
      }
    }
    return isSiteMapAltered;
  }

  public override void InitializeSiteMapFields(DacType row)
  {
    if ((object) row == null)
      return;
    Guid? nullable = this.EntityCache.GetValue((object) row, this._siteMapIDField.Name) as Guid?;
    this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name)?.ToString();
    if (!nullable.HasValue)
      return;
    this.InsertSiteMapTreeCache.ClearQueryCache();
    PXSiteMap.Provider.ClearThreadSlot();
    PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nullable.Value);
    if (nodeFromKeyUnsecure == null)
      return;
    this.EntityCache.SetValue((object) row, this.SiteMapTitleField.Name, (object) nodeFromKeyUnsecure.Title);
  }

  public override bool DeleteObsoleteSiteMapNode(DacType row, PXDBOperation operation)
  {
    bool flag = false;
    if ((object) row != null)
    {
      Guid? nullable = this.EntityCache.GetValue((object) row, this._siteMapIDField.Name) as Guid?;
      if (nullable.HasValue && (operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      {
        if (this.FindByNodeIDView.SelectSingle((object) nullable) is SiteMap row1)
        {
          this.InsertSiteMapTreeCache.PersistDeleted((object) row1);
          flag = true;
        }
      }
    }
    return flag;
  }

  public override void UpdateSiteMapTitle(DacType row)
  {
    if ((object) row == null)
      return;
    string str = this.EntityCache.GetValue((object) row, this.SiteMapTitleField.Name) as string;
    Guid? nullable = this.EntityCache.GetValue((object) row, this._siteMapIDField.Name) as Guid?;
    if (string.IsNullOrEmpty(str) || nullable.HasValue)
      return;
    this.EntityCache.SetValue((object) row, this._siteMapIDField.Name, (object) Guid.NewGuid());
  }

  public override void UpdateSiteMapParent(DacType row)
  {
  }

  protected void InsertNewNode(DacType row, string siteMapTitle, Guid? siteMapID)
  {
    SiteMap instance = this.InsertSiteMapTreeCache.CreateInstance() as SiteMap;
    instance.NodeID = siteMapID;
    instance.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
    instance.Title = siteMapTitle;
    instance.ScreenID = this.GenerateNewScreenID();
    instance.Url = this._generateNewUrlFunc(row);
    this.InsertSiteMapTreeCache.PersistInserted((object) (SiteMap) this.InsertSiteMapTreeCache.Insert((object) instance));
  }

  protected void UpdateNode(string siteMapTitle, SiteMap rowSiteMapNode, ref bool isSiteMapAltered)
  {
    bool flag = false;
    if (rowSiteMapNode.Title != siteMapTitle)
    {
      rowSiteMapNode.Title = siteMapTitle;
      flag = true;
    }
    if (!flag)
      return;
    if (!rowSiteMapNode.ParentID.HasValue)
      rowSiteMapNode.ParentID = new Guid?(PXSiteMap.RootNode.NodeID);
    rowSiteMapNode = this.InsertSiteMapTreeCache.Update((object) rowSiteMapNode) as SiteMap;
    this.InsertSiteMapTreeCache.PersistUpdated((object) rowSiteMapNode);
    isSiteMapAltered = true;
  }

  private bool ValidateInputParameters(
    string screenIDPrefix,
    string urlPrefix,
    System.Type siteMapTitleField,
    System.Type defaultSiteMapTitleField,
    System.Type siteMapIDField,
    System.Type urlParamField,
    string placeholderID,
    bool generateNewScreenIDFromKeys,
    PXCache siteMapTreeCache)
  {
    if (string.IsNullOrEmpty(screenIDPrefix) || string.IsNullOrEmpty(urlPrefix) || siteMapTitleField == (System.Type) null || defaultSiteMapTitleField == (System.Type) null || siteMapIDField == (System.Type) null || siteMapTreeCache == null)
      throw new PXException("At least one of the mandatory parameters is null. Check the input parameters.");
    if ((urlParamField == (System.Type) null || string.IsNullOrEmpty(placeholderID)) && !generateNewScreenIDFromKeys)
      throw new PXException("You must either specify an URL parameter field and ID placeholder or generate a new screen ID from keys.");
    if (screenIDPrefix.Length != 2)
      throw new PXException("The length of a screen ID prefix must be two symbols.");
    if (!typeof (DacType).IsAssignableFrom(this.EntityCache.GetItemType()))
      throw new PXException("The input cache type doesn't match the DAC type.");
    System.Type[] typeArray = new System.Type[4]
    {
      siteMapTitleField,
      defaultSiteMapTitleField,
      siteMapIDField,
      urlParamField
    };
    foreach (System.Type c in typeArray)
    {
      if (c != (System.Type) null && (!c.IsNested || !typeof (IBqlField).IsAssignableFrom(c)))
        throw new PXException("The property {0} is not found or it is not a BQL field in a DAC.", new object[1]
        {
          (object) c.Name
        });
    }
    return true;
  }

  private string GenerateNewUrlFromKeys(DacType row)
  {
    StringBuilder stringBuilder = new StringBuilder(this._urlPrefix);
    foreach (string key in (IEnumerable<string>) this.EntityCache.Keys)
    {
      object obj = this.EntityCache.GetValue((object) row, key);
      stringBuilder.AppendFormat("{0}={1}&", (object) key, (object) HttpUtility.UrlEncode(obj.ToString()));
    }
    stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString();
  }

  public string GenerateNewUrlFromUrlParamField(DacType row)
  {
    return $"{this._urlPrefix}{this._placeholderID}={this.EntityCache.GetValue((object) row, this._urlParamField.Name)}";
  }

  private string GenerateNewScreenID()
  {
    return $"{this._screenIDPrefix}{(!(this.FindByScreenIDView.SelectSingle((object) $"{this._screenIDPrefix}%") is SiteMap siteMap) ? (object) "000001" : (object) this.IncrementScreenNumber(siteMap.ScreenID))}";
  }

  private string IncrementScreenNumber(string screenID)
  {
    char[] chArray = screenID.Length == 8 ? screenID.ToCharArray(2, 6) : throw new PXException("The length of the last screen ID is invalid. A new valid value cannot be generated.");
    for (int index = chArray.Length - 1; index >= 0; --index)
    {
      if (index == 0 && chArray[index] == '9')
        throw new PXException("The last screen ID is '99.99.99', which is a maximum value. A new screen cannot be added because incrementing the screen ID will cause an overflow.");
      if (!char.IsDigit(chArray[index]))
        throw new PXException("The last screen ID contains invalid symbols. A new valid value cannot be generated.");
      if (chArray[index] < '9')
      {
        ++chArray[index];
        break;
      }
      chArray[index] = '0';
    }
    return new string(chArray);
  }
}
