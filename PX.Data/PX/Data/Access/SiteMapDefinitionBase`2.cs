// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.SiteMapDefinitionBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace PX.Data.Access;

internal abstract class SiteMapDefinitionBase<TEntityTable, TEntityIdField> : 
  IPrefetchable,
  IPXCompanyDependent
  where TEntityTable : class, IBqlTable
  where TEntityIdField : IBqlField
{
  private readonly PXDataField[] _additionalFields;
  private readonly YaqlCondition _additionalCondition;

  protected SiteMapDefinitionBase(params PXDataField[] additionalFields)
  {
    this._additionalFields = additionalFields;
  }

  protected SiteMapDefinitionBase(YaqlCondition additionalCondition)
  {
    this._additionalCondition = additionalCondition;
    this._additionalFields = Array.Empty<PXDataField>();
  }

  void IPrefetchable.Prefetch()
  {
    this.SiteMapNodes = (IDictionary<Guid, IList<Guid>>) new Dictionary<Guid, IList<Guid>>();
    if (PXSiteMap.IsPortal)
      this.Prefetch<PX.SM.PortalMap>();
    else
      this.Prefetch<PX.SM.SiteMap>();
  }

  public IDictionary<Guid, IList<Guid>> SiteMapNodes { get; private set; }

  public IDictionary<Guid, IList<Guid>> LegacyDashboardIdByScreenId { get; private set; }

  private void Prefetch<TSiteMapTable>() where TSiteMapTable : PX.SM.SiteMap
  {
    YaqlCondition yaqlCondition = this.GetBaseCondition();
    if (this._additionalCondition != null)
      yaqlCondition = Yaql.and(this._additionalCondition, Yaql.parethesis(yaqlCondition));
    List<PXDataField> pxDataFieldList = new List<PXDataField>()
    {
      (PXDataField) new PXDataField<TEntityIdField>(),
      new PXDataField("nodeID")
    };
    pxDataFieldList.AddRange((IEnumerable<PXDataField>) this._additionalFields);
    foreach (PXDataRecord record in PXDatabase.SelectMulti<TSiteMapTable>(Yaql.join<TEntityTable>(yaqlCondition, (YaqlJoinType) 0), pxDataFieldList.ToArray()))
    {
      Guid entityId = this.GetEntityId(record);
      Guid guid = record.GetGuid(1).Value;
      IList<Guid> guidList;
      if (!this.SiteMapNodes.TryGetValue(entityId, out guidList))
        this.SiteMapNodes[entityId] = guidList = (IList<Guid>) new List<Guid>();
      guidList.Add(guid);
    }
  }

  protected virtual Guid GetEntityId(PXDataRecord record) => record.GetGuid(0).Value;

  protected abstract YaqlCondition GetBaseCondition();

  protected static YaqlCondition GetCondition(string url, string paramName, System.Type paramType)
  {
    return Yaql.like<YaqlScalar>((YaqlScalar) Yaql.column("Url", (string) null), Yaql.concat(Yaql.constant<string>(url, SqlDbType.Variant), new YaqlScalar[3]
    {
      Yaql.constant<string>($"?{paramName}=", SqlDbType.Variant),
      Yaql.convert((YaqlScalar) Yaql.column(paramType.Name, (string) null), SqlDbType.VarChar, -1),
      Yaql.likeWildcard
    }));
  }
}
