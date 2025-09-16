// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiTemplateSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Web.Compilation;

#nullable enable
namespace PX.SM;

/// <summary>Allow show articles of certain template wiki.</summary>
/// <example>[PXWikiTemplateSelector(typeof(MyDAC.myWikiName)]</example>
[Serializable]
public class PXWikiTemplateSelectorAttribute(
#nullable disable
System.Type keyType) : PXCustomSelectorAttribute(keyType)
{
  protected System.Type _Wiki;
  protected System.Type _EntityType;
  protected System.Type _GraphType;

  public System.Type Wiki
  {
    get => this._Wiki;
    set => this._Wiki = value;
  }

  public System.Type EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  public System.Type GraphType
  {
    get => this._GraphType;
    set => this._GraphType = value;
  }

  public PXWikiTemplateSelectorAttribute()
    : this(typeof (WikiNotificationTemplate.pageID))
  {
  }

  public IEnumerable GetRecords()
  {
    PXWikiTemplateSelectorAttribute selectorAttribute = this;
    if (!(selectorAttribute.EntityType == (System.Type) null) && !(BqlCommand.GetItemType(selectorAttribute.EntityType) == (System.Type) null))
    {
      PXCache cach1 = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute.EntityType)];
      if (cach1.Current != null)
      {
        string typeName1 = (string) cach1.GetValue(cach1.Current, selectorAttribute.EntityType.Name);
        if (typeName1 != null)
        {
          string typeName2 = selectorAttribute.GraphType != (System.Type) null ? (string) cach1.GetValue(cach1.Current, selectorAttribute.GraphType.Name) : (string) null;
          System.Type type1 = System.Type.GetType(typeName1);
          if ((object) type1 == null)
            type1 = PXBuildManager.GetType(typeName1, true);
          System.Type type2 = type1;
          EntityHelper entityHelper = new EntityHelper(selectorAttribute._Graph);
          System.Type type3;
          if (typeName2 == null)
          {
            type3 = entityHelper.GetPrimaryGraphType(type2, (object) null, true);
          }
          else
          {
            type3 = System.Type.GetType(typeName2);
            if ((object) type3 == null)
              type3 = PXBuildManager.GetType(typeName2, true);
          }
          System.Type graphType = type3;
          System.Type c = graphType != (System.Type) null ? PXSubstManager.Substitute(type2, graphType) : type2;
          PXSelectBase<WikiNotificationTemplate> pxSelectBase = (PXSelectBase<WikiNotificationTemplate>) new PXSelectJoin<WikiNotificationTemplate, InnerJoin<WikiDescriptor, On<WikiNotificationTemplate.wikiID, Equal<WikiDescriptor.pageID>>>, Where<WikiNotificationTemplate.pageID, NotEqual<WikiNotificationTemplate.pageID>>>(selectorAttribute._Graph);
          ArrayList arrayList = new ArrayList();
          for (; typeof (IBqlTable).IsAssignableFrom(c); c = c.BaseType)
          {
            arrayList.Add((object) c.FullName);
            pxSelectBase.WhereOr<Where<WikiNotificationTemplate.entityType, Equal<Required<WikiNotificationTemplate.entityType>>>>();
          }
          if (selectorAttribute.Wiki != (System.Type) null && BqlCommand.GetItemType(selectorAttribute.Wiki) != (System.Type) null)
          {
            PXCache cach2 = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute.Wiki)];
            Guid? guid = GUID.CreateGuid(cach2.GetValueExt(cach2.Current, selectorAttribute.Wiki.Name).ToString());
            if (guid.HasValue)
            {
              pxSelectBase.WhereAnd<Where<WikiNotificationTemplate.wikiID, Equal<Required<WikiNotificationTemplate.wikiID>>>>();
              arrayList.Add((object) guid);
            }
          }
          foreach (PXResult<WikiNotificationTemplate, WikiDescriptor> pxResult in pxSelectBase.Select(arrayList.ToArray()))
          {
            WikiNotificationTemplate notificationTemplate = (WikiNotificationTemplate) pxResult;
            PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
            Guid? pageId = notificationTemplate.PageID;
            Guid key = pageId.Value;
            PXSiteMapNode siteMapNodeFromKey = wikiProvider1.FindSiteMapNodeFromKey(key);
            PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
            pageId = notificationTemplate.PageID;
            Guid pageID = pageId.Value;
            if (wikiProvider2.GetAccessRights(pageID) >= PXWikiRights.Select)
            {
              PXWikiTemplateSelectorAttribute.Template record = new PXWikiTemplateSelectorAttribute.Template();
              record.WikiName = ((WikiDescriptor) pxResult).Name;
              record.Title = siteMapNodeFromKey == null ? notificationTemplate.Name : siteMapNodeFromKey.Title;
              record.Name = notificationTemplate.Name;
              record.PageID = notificationTemplate.PageID;
              record.WikiID = notificationTemplate.WikiID;
              record.Key = $"{record.WikiName}\\{record.Name}";
              yield return (object) record;
            }
          }
        }
      }
    }
  }

  /// <exclude />
  [Serializable]
  public class Template : WikiNotificationTemplate
  {
    protected string _WikiName;
    protected string _Key;

    [PXString(InputMask = "")]
    [PXUIField(DisplayName = "Wiki", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string WikiName
    {
      get => this._WikiName;
      set => this._WikiName = value;
    }

    [PXString(InputMask = "")]
    public virtual string Key
    {
      get => this._Key;
      set => this._Key = value;
    }

    /// <exclude />
    public abstract class wikiName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXWikiTemplateSelectorAttribute.Template.wikiName>
    {
    }

    /// <exclude />
    public abstract class key : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXWikiTemplateSelectorAttribute.Template.key>
    {
    }
  }
}
