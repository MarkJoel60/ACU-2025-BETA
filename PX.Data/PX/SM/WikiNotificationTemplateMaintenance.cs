// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiNotificationTemplateMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden(ServiceVisible = true)]
public class WikiNotificationTemplateMaintenance : 
  WikiPageMaint<WikiNotificationTemplate, WikiNotificationTemplate, Where<WikiNotificationTemplate.articleType, Equal<WikiArticleType.notification>>>
{
  public PXSelect<CacheEntityItem, Where<CacheEntityItem.path, Equal<CacheEntityItem.path>>, OrderBy<Asc<CacheEntityItem.number>>> EntityItems;
  public PXSelectOrderBy<EntityItemSource, OrderBy<Asc<EntityItemSource.number>>> GraphTree;
  public PXSelectOrderBy<EntityItemSource, OrderBy<Asc<EntityItemSource.number>>> CacheTree;

  protected IEnumerable entityItems(string parent)
  {
    WikiNotificationTemplateMaintenance graph = this;
    if (graph.Pages.Current != null)
    {
      foreach (object obj in EMailSourceHelper.TemplateEntity((PXGraph) graph, parent, graph.Pages.Current.EntityType, graph.Pages.Current.GraphType))
        yield return obj;
    }
  }

  protected virtual IEnumerable graphTree([PXString] string key)
  {
    string entityType = this.Pages.Current.EntityType;
    return (IEnumerable) EMailSourceHelper.TemplateScreens((PXGraph) this, key, entityType);
  }

  protected virtual IEnumerable cacheTree([PXString] string key)
  {
    return (IEnumerable) EMailSourceHelper.TemplateScreens((PXGraph) this, key, (string) null);
  }

  public WikiNotificationTemplateMaintenance()
  {
    PXUIFieldAttribute.SetDisplayName<WikiPage.name>(this.Pages.Cache, "Notification ID");
  }

  protected void EntityType_Verifying(
    Events.FieldVerifying<WikiNotificationTemplate, WikiNotificationTemplate.entityType> e)
  {
    if (e.Row == null)
      return;
    string newValue = (string) e.NewValue;
    System.Type type1 = (System.Type) null;
    if (newValue != null)
    {
      System.Type type2 = PXBuildManager.GetType(newValue, false);
      type1 = typeof (IBqlTable).IsAssignableFrom(type2) ? new EntityHelper((PXGraph) this).GetPrimaryGraphType(type2, (object) null, true) : throw new PXSetPropertyException("{0} is not valid entity type", new object[1]
      {
        (object) newValue
      });
    }
    e.Row.GraphType = type1 != (System.Type) null ? type1.FullName : (string) null;
    e.NewValue = (object) newValue;
  }

  protected void GraphType_Verifying(
    Events.FieldVerifying<WikiNotificationTemplate, WikiNotificationTemplate.graphType> e)
  {
    if (e.Row == null)
      return;
    string typeName = WikiNotificationTemplateMaintenance.LastValue((string) e.NewValue);
    if (typeName != null)
    {
      if (!typeof (PXGraph).IsAssignableFrom(PXBuildManager.GetType(typeName, false)))
        throw new PXSetPropertyException("{0} is not a graph subclass", new object[1]
        {
          (object) typeName
        });
    }
    e.NewValue = (object) typeName;
  }

  private static string LastValue(string value)
  {
    if (value == null)
      return value;
    int num = value.IndexOf('|');
    return num <= 0 ? value : value.Substring(num + 1);
  }

  public override void OnWikiCreated(WikiDescriptor rec)
  {
    PXDatabase.Insert<WikiNotificationTemplate>(new PXDataFieldAssign("PageID", (object) rec.DeletedID), new PXDataFieldAssign("EntityType", (object) typeof (WikiNotificationTemplate).FullName), new PXDataFieldAssign("GraphType", (object) typeof (WikiNotificationTemplateMaintenance).FullName));
  }
}
