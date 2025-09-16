// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRPersonalDataProcessBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Process;
using PX.Objects.AR;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPRPersonalDataProcessBase : PXGraph<GDPRPersonalDataProcessBase>
{
  public System.Type GetPseudonymizationStatus;
  public int SetPseudonymizationStatus;
  [PXHidden]
  public PXSelect<CustomerPaymentMethodDetail> customerPaymentMethodDetail;

  protected virtual void TopLevelProcessor(string combinedKey, Guid? topParentNoteID, string info)
  {
  }

  protected virtual void ChildLevelProcessor(
    PXGraph graph,
    System.Type table,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
  }

  public GDPRPersonalDataProcessBase()
  {
    this.GetPseudonymizationStatus = typeof (PXPseudonymizationStatusListAttribute.notPseudonymized);
    this.SetPseudonymizationStatus = 0;
  }

  public void ProcessImpl(
    IEnumerable<IBqlTable> entities,
    bool topLayer,
    Guid? topParentNoteID,
    bool eraseAll = false)
  {
    GDPRPersonalDataProcessBase instance = PXGraph.CreateInstance<GDPRPersonalDataProcessBase>();
    foreach (IBqlTable entity1 in entities)
    {
      IBqlTable entity = entity1;
      Dictionary<System.Type, List<BqlCommand>> entitiesMapping = PXPersonalDataTableAttribute.GetEntitiesMapping(entity.GetType());
      GDPRPersonalDataProcessBase.AddCachesIntoGraph((PXGraph) instance, (IEnumerable<System.Type>) entitiesMapping.Keys);
      PXCache entityCache = ((PXGraph) instance).Caches[entity.GetType()];
      entityCache.Current = (object) entity;
      if (topLayer)
      {
        topParentNoteID = entityCache.GetValue((object) entity, "NoteID") as Guid?;
        string searchInfo = GDPRPersonalDataProcessBase.GenerateSearchInfo((PXGraph) instance, entity);
        if (searchInfo != null)
        {
          string combinedKey = string.Join<string>(PXAuditHelper.SEPARATOR.ToString(), ((IEnumerable<string>) entityCache.Keys).Select<string, string>((Func<string, string>) (_ => entityCache.GetValue((object) entity, _).ToString())));
          this.TopLevelProcessor(combinedKey, topParentNoteID, searchInfo);
          this.LogOperation(entity.GetType(), combinedKey);
        }
        else
          continue;
      }
      foreach (KeyValuePair<System.Type, List<BqlCommand>> keyValuePair in entitiesMapping)
      {
        System.Type key = keyValuePair.Key;
        foreach (BqlCommand bqlCommand1 in keyValuePair.Value)
        {
          BqlCommand bqlCommand2 = bqlCommand1;
          System.Type bqlField = ((PXGraph) instance).Caches[key].GetBqlField("PseudonymizationStatus");
          if (bqlField != (System.Type) null)
          {
            if (eraseAll)
              bqlCommand2 = bqlCommand2.WhereAnd(BqlCommand.Compose(new System.Type[4]
              {
                typeof (Where<,>),
                bqlField,
                typeof (NotEqual<>),
                typeof (PXPseudonymizationStatusListAttribute.erased)
              }));
            else if (this.GetPseudonymizationStatus == typeof (PXPseudonymizationStatusListAttribute.notPseudonymized))
              bqlCommand2 = bqlCommand2.WhereAnd(BqlCommand.Compose(new System.Type[7]
              {
                typeof (Where<,,>),
                bqlField,
                typeof (Equal<>),
                this.GetPseudonymizationStatus,
                typeof (Or<,>),
                bqlField,
                typeof (IsNull)
              }));
            else
              bqlCommand2 = bqlCommand2.WhereAnd(BqlCommand.Compose(new System.Type[4]
              {
                typeof (Where<,>),
                bqlField,
                typeof (Equal<>),
                this.GetPseudonymizationStatus
              }));
          }
          List<object> source = new PXView((PXGraph) instance, false, bqlCommand2).SelectMulti(Array.Empty<object>());
          if (source.Count != 0)
          {
            IEnumerable<object> objects = source.Select<object, object>((Func<object, object>) (_ => !(_ is PXResult) ? _ : (_ as PXResult)[0]));
            List<PXPersonalDataFieldAttribute> dataFieldAttributeList = (List<PXPersonalDataFieldAttribute>) null;
            bool personalDataFields = PXPersonalDataFieldAttribute.GetPersonalDataFields(key, ref dataFieldAttributeList);
            dataFieldAttributeList = dataFieldAttributeList.Where<PXPersonalDataFieldAttribute>((Func<PXPersonalDataFieldAttribute, bool>) (_ => !(_ is PXPersonalDataTableAnchorAttribute))).ToList<PXPersonalDataFieldAttribute>();
            if (dataFieldAttributeList.Count > 0)
            {
              if (((PXGraph) instance).Caches[key].Fields.Contains("NoteID") && ((PXGraph) instance).Caches[key].Fields.Contains("PseudonymizationStatus"))
              {
                if (!personalDataFields)
                  this.ChildLevelProcessor((PXGraph) instance, key, (IEnumerable<PXPersonalDataFieldAttribute>) dataFieldAttributeList, objects, topParentNoteID);
                else
                  this.ChildLevelProcessor((PXGraph) instance, key, dataFieldAttributeList.Where<PXPersonalDataFieldAttribute>((Func<PXPersonalDataFieldAttribute, bool>) (_ => _ is PXPersonalDataFieldAttribute.Value)), objects, topParentNoteID);
              }
              else
                continue;
            }
            this.ProcessImpl(objects.Cast<IBqlTable>(), false, topParentNoteID);
          }
        }
      }
    }
  }

  protected static void AddCachesIntoGraph(PXGraph graph, IEnumerable<System.Type> caches)
  {
    foreach (System.Type cach in caches)
    {
      GraphHelper.EnsureCachePersistence(graph, cach);
      graph.Caches[cach].AllowInsert = graph.Caches[cach].AllowUpdate = graph.Caches[cach].AllowDelete = false;
    }
  }

  protected static string GenerateSearchInfo(PXGraph processingGraph, IBqlTable entity)
  {
    PXSearchableAttribute searchableAttribute = processingGraph.Caches[entity.GetType()].GetAttributes("NoteID").OfType<PXSearchableAttribute>().FirstOrDefault<PXSearchableAttribute>();
    if (searchableAttribute == null && entity.GetType() == typeof (CRContact))
      searchableAttribute = new PXSearchableAttribute(1024 /*0x0400*/, "Opportunity Contact {0}", new System.Type[1]
      {
        typeof (CRContact.displayName)
      }, new System.Type[5]
      {
        typeof (CRContact.email),
        typeof (CRContact.phone1),
        typeof (CRContact.phone2),
        typeof (CRContact.phone3),
        typeof (CRContact.webSite)
      })
      {
        Line1Format = "{0}{1}{2}",
        Line1Fields = new System.Type[3]
        {
          typeof (CRContact.salutation),
          typeof (CRContact.phone1),
          typeof (CRContact.email)
        }
      };
    return searchableAttribute?.BuildContent(processingGraph.Caches[entity.GetType()], (object) entity, (PXResult) null);
  }

  protected void LogOperation(System.Type tableName, string combinedKey)
  {
    PXDatabase.Insert<SMPersonalDataLog>(new PXDataFieldAssign[5]
    {
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataLog.tableName>((object) tableName.FullName),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataLog.combinedKey>((object) combinedKey),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataLog.pseudonymizationStatus>((object) this.SetPseudonymizationStatus),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataLog.createdByID>((object) PXAccess.GetTrueUserID()),
      (PXDataFieldAssign) new PXDataFieldAssign<SMPersonalDataLog.createdDateTime>((object) PXTimeZoneInfo.UtcNow)
    });
  }

  [PXDefault]
  [PXUIField(DisplayName = "Value")]
  [GDPRPersonalDataProcessBase.PXDBBypassString]
  [PXPersonalDataFieldAttribute.Value]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CustomerPaymentMethodDetail.value> e)
  {
  }

  private class PXDBBypassStringAttribute : PXDBStringAttribute
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      if (sender.BypassAuditFields.Contains(((PXEventSubscriberAttribute) this).FieldName))
        return;
      sender.BypassAuditFields.Add(((PXEventSubscriberAttribute) this).FieldName);
    }
  }
}
