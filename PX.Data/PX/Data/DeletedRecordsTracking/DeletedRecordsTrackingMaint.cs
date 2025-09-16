// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DeletedRecordsTrackingMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Data.DeletedRecordsTracking.DAC;
using System.Collections;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.DeletedRecordsTracking;

public class DeletedRecordsTrackingMaint : PXGraph<DeletedRecordsTrackingMaint>
{
  public PXSelect<ODataPreferences> Preferences;
  [PXImport(typeof (SMDeletedRecordsTrackingTables))]
  public PXSelectOrderBy<SMDeletedRecordsTrackingTables, OrderBy<Asc<SMDeletedRecordsTrackingTables.createdDateTime>>> Tables;
  public PXSave<ODataPreferences> Save;
  public PXCancel<ODataPreferences> Cancel;

  [InjectDependency]
  internal IDeletedRecordsTrackingService DeletedRecordsTrackingService { get; set; }

  protected IEnumerable preferences()
  {
    DeletedRecordsTrackingMaint graph = this;
    if (graph.Preferences.Current != null)
    {
      yield return (object) graph.Preferences.Current;
    }
    else
    {
      if (new PXView((PXGraph) graph, true, graph.Preferences.View.BqlSelect).SelectSingle() is ODataPreferences row)
        graph.Preferences.Cache.Hold((object) row);
      else
        row = graph.Preferences.Cache.NonDirtyInsert() as ODataPreferences;
      yield return (object) row;
    }
  }

  protected virtual void _(
    Events.FieldSelecting<SMDeletedRecordsTrackingTables.description> e)
  {
    if (!(e.Row is SMDeletedRecordsTrackingTables row) || string.IsNullOrEmpty(row?.TableName))
      return;
    Events.FieldSelecting<SMDeletedRecordsTrackingTables.description> fieldSelecting = e;
    System.Type type = PXBuildManager.GetType(row.TableName, false);
    string xmlDocsSummary = (object) type != null ? XmlDocsExtensions.GetXmlDocsSummary(type, true) : (string) null;
    fieldSelecting.ReturnValue = (object) xmlDocsSummary;
  }

  protected virtual void _(
    Events.FieldDefaulting<SMDeletedRecordsTrackingTables.createdDateTime> e)
  {
    if (!(e.Row is SMDeletedRecordsTrackingTables))
      return;
    e.NewValue = (object) System.DateTime.Now;
  }
}
