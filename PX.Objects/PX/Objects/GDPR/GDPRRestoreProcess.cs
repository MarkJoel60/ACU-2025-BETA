// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRRestoreProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.MassProcess;
using PX.Data.Process;
using PX.Data.Search;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.GDPR;

public class GDPRRestoreProcess : GDPRRestoreProcessBase
{
  private readonly 
  #nullable disable
  Lazy<BqlFullTextRenderingMethod> FullTextCapability = new Lazy<BqlFullTextRenderingMethod>(new Func<BqlFullTextRenderingMethod>(PXDatabase.Provider.GetFullTextSearchCapability<SearchIndex.content>));
  public PXFilter<GDPRRestoreProcess.RestoreType> Filter;
  public PXFilteredProcessing<SMPersonalDataIndex, GDPRRestoreProcess.RestoreType> ObfuscatedItems;
  public PXCancel<GDPRRestoreProcess.RestoreType> Cancel;
  public PXAction<GDPRRestoreProcess.RestoreType> OpenContact;
  public PXAction<GDPRRestoreProcess.RestoreType> Restore;
  public PXAction<GDPRRestoreProcess.RestoreType> Erase;

  internal static bool IsExactMatch(string query) => query.StartsWith("\"") && query.EndsWith("\"");

  internal static string ConvertToContainsPatern(string query)
  {
    string containsPatern = (string) null;
    string str = string.Empty;
    string[] strArray = Regex.Split(query, "\\W+");
    if (strArray.Length != 0)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].Length > 0)
          stringList.Add(strArray[index]);
      }
      if (stringList.Count > 0)
      {
        for (int index = 0; index < stringList.Count - 1; ++index)
          str += $"\"{stringList[index]}*\" AND ";
        str += $"\"{stringList[stringList.Count - 1]}*\"";
      }
    }
    else
      str = query;
    if (!string.IsNullOrWhiteSpace(str))
      containsPatern = str;
    return containsPatern;
  }

  internal static string ConvertToContainsPaternMySql(string query)
  {
    IEnumerable<string> source = ((IEnumerable<string>) Regex.Split(query, "\\W+")).Where<string>((Func<string, bool>) (w => w != string.Empty));
    return source.Count<string>() <= 0 ? query : source.Select<string, string>((Func<string, string>) (w => $"+{w}*")).Aggregate<string>((Func<string, string, string>) ((f, w) => $"{f} {w}"));
  }

  public virtual IEnumerable obfuscatedItems()
  {
    string str1 = (string) null;
    foreach (PXFilterRow filter in PXView.Filters)
      str1 = $"{str1}{filter.Value?.ToString()} ";
    string str2 = str1?.Trim();
    if (string.IsNullOrWhiteSpace(str2))
      return (IEnumerable) ((PXSelectBase<SMPersonalDataIndex>) new PXSelectReadonly<SMPersonalDataIndex>((PXGraph) this)).Select(Array.Empty<object>());
    PXEntitySearch pxEntitySearch = new PXEntitySearch();
    return pxEntitySearch.IsFullText() ? (IEnumerable) ((PXSelectBase<SMPersonalDataIndex>) new PXSelectReadonly<SMPersonalDataIndex, Where<Contains<SMPersonalDataIndex.content, Required<SMPersonalDataIndex.content>, SMPersonalDataIndex.indexID>>, OrderBy<Desc<RankOf<SMPersonalDataIndex.content>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) pxEntitySearch.PrepareQuery(str2, false)
    }) : (IEnumerable) ((PXSelectBase<SearchIndex>) new PXSelectReadonly<SearchIndex, Where<SearchIndex.content, Like<Required<SearchIndex.content>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) $"%{str2}%"
    });
  }

  public GDPRRestoreProcess()
  {
    ((PXGraph) this).Actions["Schedule"].SetVisible(false);
    ((PXProcessingBase<SMPersonalDataIndex>) this.ObfuscatedItems).SetSelected<SMPersonalDataIndex.selected>();
    ((PXProcessing<SMPersonalDataIndex>) this.ObfuscatedItems).SetProcessVisible(false);
    ((PXProcessing<SMPersonalDataIndex>) this.ObfuscatedItems).SetProcessAllVisible(false);
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Contact", Visible = false)]
  public virtual IEnumerable openContact(PXAdapter adapter)
  {
    if (!(((PXGraph) this).Caches[typeof (SMPersonalDataIndex)].Current is SMPersonalDataIndex current))
      return adapter.Get();
    foreach (IBqlTable ibqlTable in GDPRRestoreProcess.RemapToPrimary((IEnumerable<SMPersonalDataIndex>) new List<SMPersonalDataIndex>()
    {
      current
    }))
    {
      if (ibqlTable is Contact)
        PXRedirectHelper.TryRedirect(new PXPrimaryGraphCollection((PXGraph) this)[ibqlTable], (object) ibqlTable, (PXRedirectHelper.WindowMode) 1);
      else if (ibqlTable is CRContact)
      {
        CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) new PXSelect<CROpportunity, Where<CROpportunity.opportunityContactID, Equal<Required<CRContact.contactID>>>>((PXGraph) this)).SelectSingle(new object[1]
        {
          (object) (ibqlTable as CRContact).ContactID
        });
        PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<OpportunityMaint>(), (object) crOpportunity, (PXRedirectHelper.WindowMode) 1);
      }
    }
    return adapter.Get();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Restore")]
  public virtual IEnumerable restore(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<SMPersonalDataIndex>) this.ObfuscatedItems).SetProcessDelegate(new PXProcessingBase<SMPersonalDataIndex>.ProcessListDelegate((object) new GDPRRestoreProcess.\u003C\u003Ec__DisplayClass13_0()
    {
      filter = ((PXSelectBase<GDPRRestoreProcess.RestoreType>) this.Filter).Current
    }, __methodptr(\u003Crestore\u003Eb__0)));
    return ((PXGraph) this).Actions["Process"].Press(adapter);
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Erase")]
  public virtual IEnumerable erase(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<SMPersonalDataIndex>) this.ObfuscatedItems).SetProcessDelegate(new PXProcessingBase<SMPersonalDataIndex>.ProcessListDelegate((object) new GDPRRestoreProcess.\u003C\u003Ec__DisplayClass15_0()
    {
      filter = ((PXSelectBase<GDPRRestoreProcess.RestoreType>) this.Filter).Current
    }, __methodptr(\u003Cerase\u003Eb__0)));
    return ((PXGraph) this).Actions["Process"].Press(adapter);
  }

  private static void RestoreImpl(
    IEnumerable<SMPersonalDataIndex> entities,
    GDPRRestoreProcess graph,
    GDPRRestoreProcess.RestoreType filter)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      IEnumerable<IBqlTable> primary = GDPRRestoreProcess.RemapToPrimary(entities);
      graph.ProcessImpl(primary, true, new Guid?());
      if (filter != null && filter.DeleteNonRestored.GetValueOrDefault())
        graph.CleanNonRestored(primary);
      transactionScope.Complete();
    }
  }

  private static void EraseImpl(
    IEnumerable<SMPersonalDataIndex> entities,
    GDPREraseOnRestoreProcess graph)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      graph.ProcessImpl(GDPRRestoreProcess.RemapToPrimary(entities), true, new Guid?());
      transactionScope.Complete();
    }
  }

  protected static IEnumerable<IBqlTable> RemapToPrimary(IEnumerable<SMPersonalDataIndex> entities)
  {
    using (new PXReadDeletedScope(false))
    {
      EntityHelper helper = new EntityHelper(new PXGraph());
      foreach (string str in entities.Select<SMPersonalDataIndex, string>((Func<SMPersonalDataIndex, string>) (_ => _.CombinedKey)))
      {
        helper.GetEntityRow(typeof (Contact), (object[]) str.Split(PXAuditHelper.SEPARATOR));
        if (helper.GetEntityRow(typeof (Contact), (object[]) str.Split(PXAuditHelper.SEPARATOR)) is IBqlTable entityRow)
          yield return entityRow;
        else
          yield return helper.GetEntityRow(typeof (CRContact), (object[]) str.Split(PXAuditHelper.SEPARATOR)) as IBqlTable;
      }
      helper = (EntityHelper) null;
    }
  }

  protected override void TopLevelProcessor(string combinedKey, Guid? topParentNoteID, string info)
  {
    this.HandleSearchIndex(combinedKey);
  }

  protected override void ChildLevelProcessor(
    PXGraph processingGraph,
    System.Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
    this.RestoreObfuscatedEntries(processingGraph, childTable, fields, childs);
  }

  private void HandleSearchIndex(string combinedKey)
  {
    PXDatabase.Delete<SMPersonalDataIndex>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalDataIndex.combinedKey>((object) combinedKey)
    });
  }

  private void CleanNonRestored(IEnumerable<IBqlTable> entities)
  {
    GDPRPersonalDataProcessBase instance = PXGraph.CreateInstance<GDPRPersonalDataProcessBase>();
    foreach (IBqlTable entity in entities)
    {
      GraphHelper.EnsureCachePersistence((PXGraph) instance, entity.GetType());
      PXDatabase.Delete<SMPersonalData>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalData.topParentNoteID>(((PXGraph) instance).Caches[entity.GetType()].GetValue((object) entity, "NoteID"))
      });
    }
  }

  [PXHidden]
  [Serializable]
  public class RestoreType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXUIField(DisplayName = "Delete data that cannot be restored")]
    [PXDefault(false)]
    public virtual bool? DeleteNonRestored { get; set; }

    public abstract class deleteNonRestored : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GDPRRestoreProcess.RestoreType.deleteNonRestored>
    {
    }
  }
}
