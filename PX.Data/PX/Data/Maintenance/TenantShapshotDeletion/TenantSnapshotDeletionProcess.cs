// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.TenantSnapshotDeletionProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.BulkInsert;
using PX.Common;
using PX.Data.Maintenance.TenantShapshotDeletion.DAC;
using PX.Data.SQLTree;
using PX.Data.Update;
using PX.Data.Update.Storage;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable disable
namespace PX.Data.Maintenance.TenantShapshotDeletion;

public class TenantSnapshotDeletionProcess : PXGraph<TenantSnapshotDeletionProcess>
{
  private static readonly Guid _deletionProcessUID = new Guid("705c3998-0f3a-4845-9d6b-0b7655a86908");
  public PXFilter<DeletionAction> Filter;
  public PXCancel<DeletionAction> Cancel;
  public PXAction<DeletionAction> NavigateToTenant;
  public PXFilteredProcessing<TenantSnapshotDeletion, DeletionAction, Where<PX.Data.True, Equal<PX.Data.True>>, OrderBy<Asc<TenantSnapshotDeletion.tenantId, Asc<TenantSnapshotDeletion.snapshotId, Asc<TenantSnapshotDeletion.deletionStatus>>>>> Records;
  private const int DeletionBatchSize = 1000;

  [InjectDependency]
  internal ILogger Logger { get; set; }

  public virtual IEnumerable records()
  {
    TenantSnapshotDeletion[] array = this.Records.Cache.Cached.Cast<TenantSnapshotDeletion>().ToArray<TenantSnapshotDeletion>();
    if (array.Length != 0)
      return (IEnumerable) array;
    IEnumerable<TenantSnapshotDeletion> snapshotDeletions;
    switch (this.Filter.Current.Name)
    {
      case "T":
        snapshotDeletions = this.SelectTenants();
        break;
      case "S":
        snapshotDeletions = this.SelectSnapshots((IEnumerable<UPSnapshot>) PXDatabase.Select<UPSnapshot>().ToArray<UPSnapshot>());
        break;
      case "O":
        snapshotDeletions = this.SelectOrphanedShapshots();
        break;
      default:
        throw new PXException("The selected action is not supported on the Delete Snapshots and Tenants (SM503000) form.");
    }
    foreach (object obj in snapshotDeletions)
      this.Records.Cache.SetStatus(obj, PXEntryStatus.Held);
    return this.Records.Cache.Cached;
  }

  private IEnumerable<TenantSnapshotDeletion> SelectTenants()
  {
    Dictionary<int?, TenantSnapshotDeletion> existingTenantDeletions = PXDatabase.Select<TenantSnapshotDeletion>().Where<TenantSnapshotDeletion>((Expression<System.Func<TenantSnapshotDeletion, bool>>) (t => t.TenantId > (int?) 0 && t.SnapshotId == new Guid?())).GroupBy<TenantSnapshotDeletion, int?>((Expression<System.Func<TenantSnapshotDeletion, int?>>) (t => t.TenantId)).ToDictionary<IGrouping<int?, TenantSnapshotDeletion>, int?, TenantSnapshotDeletion>((System.Func<IGrouping<int?, TenantSnapshotDeletion>, int?>) (g => g.Key), (System.Func<IGrouping<int?, TenantSnapshotDeletion>, TenantSnapshotDeletion>) (g => g.OrderByDescending<TenantSnapshotDeletion, System.DateTime?>((System.Func<TenantSnapshotDeletion, System.DateTime?>) (t => t.DeletionHeartbeat)).First<TenantSnapshotDeletion>()));
    foreach (UPCompany includeWithoutUser in PXCompanyHelper.SelectVisibleCompaniesIncludeWithoutUsers())
    {
      TenantSnapshotDeletion snapshotDeletion1;
      TenantSnapshotDeletion snapshotDeletion2;
      if (existingTenantDeletions.TryGetValue(includeWithoutUser.CompanyID, out snapshotDeletion1))
      {
        snapshotDeletion2 = snapshotDeletion1;
        existingTenantDeletions.Remove(includeWithoutUser.CompanyID);
      }
      else
        snapshotDeletion2 = new TenantSnapshotDeletion()
        {
          Id = new Guid?(Guid.NewGuid()),
          TenantId = includeWithoutUser.CompanyID,
          DeletionStatus = "N",
          NoteID = new Guid?(Guid.NewGuid())
        };
      snapshotDeletion2.Type = new int?(0);
      snapshotDeletion2.SizeMB = includeWithoutUser.SizeMB;
      Tenant extension = this.Records.Cache.GetExtension<Tenant>((object) snapshotDeletion2);
      extension.TenantName = includeWithoutUser.CompanyCD;
      extension.Status = includeWithoutUser.Status;
      yield return snapshotDeletion2;
    }
    Dictionary<int?, UPCompany> positiveCompanies = PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Positive, true).ToDictionary<UPCompany, int?>((System.Func<UPCompany, int?>) (c => c.CompanyID));
    foreach (KeyValuePair<int?, TenantSnapshotDeletion> keyValuePair in existingTenantDeletions)
    {
      TenantSnapshotDeletion snapshotDeletion = keyValuePair.Value;
      snapshotDeletion.Type = new int?(0);
      UPCompany upCompany;
      if (positiveCompanies.TryGetValue(keyValuePair.Key, out upCompany))
      {
        snapshotDeletion.SizeMB = upCompany.SizeMB;
        Tenant extension = this.Records.Cache.GetExtension<Tenant>((object) snapshotDeletion);
        extension.TenantName = upCompany.CompanyCD;
        extension.Status = upCompany.Status;
      }
      yield return snapshotDeletion;
    }
  }

  private IEnumerable<TenantSnapshotDeletion> SelectSnapshots(
    IEnumerable<UPSnapshot> visibleUPSnapshots)
  {
    Dictionary<Guid?, TenantSnapshotDeletion> existingSnapshotDeletions = PXDatabase.Select<TenantSnapshotDeletion>().Where<TenantSnapshotDeletion>((Expression<System.Func<TenantSnapshotDeletion, bool>>) (t => t.TenantId < (int?) 0 && t.SnapshotId != new Guid?())).GroupBy<TenantSnapshotDeletion, Guid?>((Expression<System.Func<TenantSnapshotDeletion, Guid?>>) (t => t.SnapshotId)).ToDictionary<IGrouping<Guid?, TenantSnapshotDeletion>, Guid?, TenantSnapshotDeletion>((System.Func<IGrouping<Guid?, TenantSnapshotDeletion>, Guid?>) (g => g.Key), (System.Func<IGrouping<Guid?, TenantSnapshotDeletion>, TenantSnapshotDeletion>) (g => g.OrderByDescending<TenantSnapshotDeletion, System.DateTime?>((System.Func<TenantSnapshotDeletion, System.DateTime?>) (t => t.DeletionHeartbeat)).First<TenantSnapshotDeletion>()));
    Dictionary<int?, UPCompany> companyById = PXCompanyHelper.SelectCompanies().ToDictionary<UPCompany, int?>((System.Func<UPCompany, int?>) (c => c.CompanyID));
    foreach (UPSnapshot visibleUpSnapshot in visibleUPSnapshots)
    {
      TenantSnapshotDeletion snapshotDeletion1;
      TenantSnapshotDeletion snapshotDeletion2;
      if (existingSnapshotDeletions.TryGetValue(visibleUpSnapshot.SnapshotID, out snapshotDeletion1))
      {
        snapshotDeletion2 = snapshotDeletion1;
        existingSnapshotDeletions.Remove(visibleUpSnapshot.SnapshotID);
      }
      else
        snapshotDeletion2 = new TenantSnapshotDeletion()
        {
          Id = new Guid?(Guid.NewGuid()),
          TenantId = visibleUpSnapshot.LinkedCompany,
          SnapshotId = visibleUpSnapshot.SnapshotID,
          DeletionStatus = "N",
          NoteID = new Guid?(Guid.NewGuid())
        };
      snapshotDeletion2.Type = new int?(1);
      snapshotDeletion2.SizeMB = visibleUpSnapshot.SizePrepared;
      PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot extension = this.Records.Cache.GetExtension<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot>((object) snapshotDeletion2);
      extension.SnapshotName = visibleUpSnapshot.Name;
      extension.Description = visibleUpSnapshot.Description;
      extension.SourceCompany = visibleUpSnapshot.SourceCompany;
      UPCompany upCompany;
      extension.Visibility = !visibleUpSnapshot.SourceCompany.HasValue || !companyById.TryGetValue(visibleUpSnapshot.SourceCompany, out upCompany) ? PXLocalizer.Localize("Across Tenants", typeof (Messages).FullName) : (extension.Visibility = upCompany.CompanyCD);
      extension.CreatedOn = visibleUpSnapshot.CreatedDateTime;
      extension.Version = visibleUpSnapshot.Version;
      extension.ExportMode = visibleUpSnapshot.ExportMode;
      yield return snapshotDeletion2;
    }
    foreach (TenantSnapshotDeletion snapshotDeletion in existingSnapshotDeletions.Values)
      yield return snapshotDeletion;
  }

  private static IEnumerable<UPSnapshot> SelectUPSnapshotRows(YaqlTableQuery upSnapshotQuery)
  {
    List<YaqlScalarAlilased> collection = new List<YaqlScalarAlilased>()
    {
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.snapshotID>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.sourceCompany>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.linkedCompany>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.name>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.description>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.createdDateTime>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.version>((string) null)),
      YaqlScalarAlilased.op_Implicit(Yaql.column<UPSnapshot.exportMode>((string) null))
    };
    upSnapshotQuery.Columns.AddRange((IEnumerable<YaqlScalarAlilased>) collection);
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    TableHeader table = dbServicesPoint.Schema.GetTable("UPSnapshot");
    List<TableColumn> tableColumnList = new List<TableColumn>();
    foreach (YaqlScalarAlilased yaqlScalarAlilased in collection)
    {
      YaqlColumn scalar = (YaqlColumn) yaqlScalarAlilased.Scalar;
      TableColumn columnByName = table.getColumnByName(scalar.Name);
      tableColumnList.Add(columnByName);
    }
    return dbServicesPoint.selectTable(upSnapshotQuery, tableColumnList, (SqlGenerationOptions) null).Select<object[], UPSnapshot>((System.Func<object[], UPSnapshot>) (s => new UPSnapshot()
    {
      SnapshotID = s[0] as Guid?,
      SourceCompany = s[1] as int?,
      LinkedCompany = s[2] as int?,
      Name = s[3] as string,
      Description = s[4] as string,
      CreatedDateTime = s[5] as System.DateTime?,
      Version = s[6] as string,
      ExportMode = s[7] as string
    }));
  }

  private static IEnumerable<UPSnapshot> SelectTenantsWithNormalVisibility(int tenantId)
  {
    YaqlTableQuery upSnapshotQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("UPSnapshot", "UPSnapshot"), (List<YaqlJoin>) null, (string) null);
    ((YaqlQueryBase) upSnapshotQuery).Condition = Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column<UPSnapshot.sourceCompany>("UPSnapshot"), tenantId), Yaql.isNotNull((YaqlScalar) Yaql.column<UPSnapshot.linkedCompany>("UPSnapshot")));
    ((YaqlQueryBase) upSnapshotQuery).Joins.Add(Yaql.join("Company", Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("CompanyID", "Company"), Yaql.column("SourceCompany", "UPSnapshot")), (YaqlJoinType) 0));
    return TenantSnapshotDeletionProcess.SelectUPSnapshotRows(upSnapshotQuery);
  }

  private static IEnumerable<UPSnapshot> SelectTenantsWithIncreasedVisibility(int tenantId)
  {
    YaqlTableQuery upSnapshotQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("UPSnapshot", "UPSnapshot"), (List<YaqlJoin>) null, (string) null);
    ((YaqlQueryBase) upSnapshotQuery).Condition = Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", "UPSnapshot"), tenantId), Yaql.and(Yaql.isNotNull((YaqlScalar) Yaql.column<UPSnapshot.linkedCompany>("UPSnapshot")), Yaql.isNull((YaqlScalar) Yaql.column<UPSnapshot.sourceCompany>("UPSnapshot"))));
    ((YaqlQueryBase) upSnapshotQuery).Joins.Add(Yaql.join("Company", Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("CompanyID", "Company"), Yaql.column("CompanyID", "UPSnapshot")), (YaqlJoinType) 0));
    return TenantSnapshotDeletionProcess.SelectUPSnapshotRows(upSnapshotQuery);
  }

  private IEnumerable<TenantSnapshotDeletion> SelectAllTenantSnapshots(int tenantId)
  {
    return this.SelectSnapshots(TenantSnapshotDeletionProcess.SelectTenantsWithNormalVisibility(tenantId).Concat<UPSnapshot>(TenantSnapshotDeletionProcess.SelectTenantsWithIncreasedVisibility(tenantId)));
  }

  private IEnumerable<UPSnapshot> SelectOrphanedShapshots(IEnumerable<int?> orphanedTenants)
  {
    YaqlTableQuery yaqlTableQuery1 = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("UPSnapshot", (string) null), (List<YaqlJoin>) null, (string) null);
    ((YaqlQueryBase) yaqlTableQuery1).Condition = Yaql.isIn<int?>((YaqlScalar) Yaql.column("LinkedCompany", (string) null), orphanedTenants);
    YaqlTableQuery yaqlTableQuery2 = yaqlTableQuery1;
    YaqlColumn[] source = new YaqlColumn[7]
    {
      Yaql.column("SnapshotID", "UPSnapshot"),
      Yaql.column("LinkedCompany", "UPSnapshot"),
      Yaql.column("Name", "UPSnapshot"),
      Yaql.column("Description", "UPSnapshot"),
      Yaql.column("Version", "UPSnapshot"),
      Yaql.column("ExportMode", "UPSnapshot"),
      Yaql.column("CreatedDateTime", "UPSnapshot")
    };
    foreach (YaqlColumn yaqlColumn in source)
      yaqlTableQuery2.Columns.Add(YaqlScalarAlilased.op_Implicit(yaqlColumn));
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    ITableAdapter snapshotTableAdapter = dbServicesPoint.GetTable("UPSnapshot", FileMode.Open);
    List<TableColumn> list = ((IEnumerable<YaqlColumn>) source).Select<YaqlColumn, TableColumn>((System.Func<YaqlColumn, TableColumn>) (c => snapshotTableAdapter.Header.getColumnByName(c.Name))).ToList<TableColumn>();
    foreach (object[] objArray in dbServicesPoint.selectTable(yaqlTableQuery2, list, (SqlGenerationOptions) null))
      yield return new UPSnapshot()
      {
        SnapshotID = objArray[0] as Guid?,
        LinkedCompany = objArray[1] as int?,
        Name = objArray[2] as string,
        Description = objArray[3] as string,
        Version = objArray[4] as string,
        ExportMode = objArray[5] as string,
        CreatedDateTime = !(objArray[6] is System.DateTime dateTime) ? new System.DateTime?() : new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(dateTime, LocaleInfo.GetTimeZone()))
      };
  }

  private IEnumerable<TenantSnapshotDeletion> SelectOrphanedShapshots()
  {
    Dictionary<int?, TenantSnapshotDeletion> existingSnapshotDeletions = PXDatabase.Select<TenantSnapshotDeletion>().LeftJoin((IEnumerable<UPSnapshot>) PXDatabase.Select<UPSnapshot>(), (Expression<System.Func<TenantSnapshotDeletion, Guid?>>) (d => d.SnapshotId), (Expression<System.Func<UPSnapshot, Guid?>>) (s => s.SnapshotID), (d, s) => new
    {
      d = d,
      s = s
    }).Where(result => result.d.TenantId < (int?) 0).ToArray().Where(result =>
    {
      if (!result.d.SnapshotId.HasValue)
        return true;
      UPSnapshot s = result.s;
      return s == null || !s.SnapshotID.HasValue;
    }).Select(result => result.d).GroupBy<TenantSnapshotDeletion, int?>((System.Func<TenantSnapshotDeletion, int?>) (d => d.TenantId)).ToDictionary<IGrouping<int?, TenantSnapshotDeletion>, int?, TenantSnapshotDeletion>((System.Func<IGrouping<int?, TenantSnapshotDeletion>, int?>) (g => g.Key), (System.Func<IGrouping<int?, TenantSnapshotDeletion>, TenantSnapshotDeletion>) (g => g.OrderByDescending<TenantSnapshotDeletion, System.DateTime?>((System.Func<TenantSnapshotDeletion, System.DateTime?>) (d => d.DeletionHeartbeat)).First<TenantSnapshotDeletion>()));
    YaqlVectorQuery orphanedCompaniesQuery = AcumaticaDbKeeperBase.getSelectOrphanedCompaniesQuery(true);
    HashSet<int?> orphanIdSet = PXDatabase.Provider.CreateDbServicesPoint().selectVector<int>(orphanedCompaniesQuery, false).Select<int, int?>((System.Func<int, int?>) (id => new int?(id))).ToHashSet<int?>();
    IEnumerable<UPCompany> upCompanies = PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.Negative, true).Where<UPCompany>((System.Func<UPCompany, bool>) (c => orphanIdSet.Contains(new int?(c.CompanyID.Value))));
    Dictionary<int?, UPSnapshot> orphanedSnapshots = this.SelectOrphanedShapshots((IEnumerable<int?>) orphanIdSet).ToDictionary<UPSnapshot, int?>((System.Func<UPSnapshot, int?>) (s => s.LinkedCompany));
    foreach (UPCompany upCompany in upCompanies)
    {
      TenantSnapshotDeletion snapshotDeletion1;
      TenantSnapshotDeletion snapshotDeletion2;
      if (existingSnapshotDeletions.TryGetValue(upCompany.CompanyID, out snapshotDeletion1))
      {
        snapshotDeletion2 = snapshotDeletion1;
        existingSnapshotDeletions.Remove(upCompany.CompanyID);
      }
      else
        snapshotDeletion2 = new TenantSnapshotDeletion()
        {
          Id = new Guid?(Guid.NewGuid()),
          TenantId = upCompany.CompanyID,
          DeletionStatus = "N",
          NoteID = new Guid?(Guid.NewGuid())
        };
      snapshotDeletion2.Type = new int?(2);
      PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot extension = this.Records.Cache.GetExtension<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot>((object) snapshotDeletion2);
      extension.Visibility = PXLocalizer.Localize("Orphaned", typeof (Messages).FullName);
      UPSnapshot upSnapshot;
      if (orphanedSnapshots.TryGetValue(upCompany.CompanyID, out upSnapshot))
      {
        snapshotDeletion2.SnapshotId = upSnapshot.SnapshotID;
        snapshotDeletion2.SizeMB = upSnapshot.SizePrepared;
        extension.SnapshotName = upSnapshot.Name;
        extension.Description = upSnapshot.Description;
        extension.CreatedOn = upSnapshot.CreatedDateTime;
        extension.Version = upSnapshot.Version;
        extension.ExportMode = upSnapshot.ExportMode;
      }
      else
      {
        snapshotDeletion2.SizeMB = upCompany.SizeMB;
        extension.SnapshotName = upCompany.CompanyCD;
        extension.Description = upCompany.Status;
      }
      yield return snapshotDeletion2;
    }
    foreach (TenantSnapshotDeletion snapshotDeletion in existingSnapshotDeletions.Values)
      yield return snapshotDeletion;
  }

  [PXButton]
  [PXUIField(Visible = false)]
  public virtual void navigateToTenant()
  {
    TenantSnapshotDeletion current = this.Records.Current;
    int? type = current.Type;
    int num = 0;
    int? field0 = !(type.GetValueOrDefault() == num & type.HasValue) ? this.Records.Cache.GetExtension<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot>((object) current).SourceCompany : current.TenantId;
    HashSet<int?> hashSet = PXCompanyHelper.SelectCompanies().Select<UPCompany, int?>((System.Func<UPCompany, int?>) (c => c.CompanyID)).ToHashSet<int?>();
    if (!field0.HasValue || !hashSet.Contains(field0) || current.DeletionStatus == "P" || current.DeletionStatus == "S")
      field0 = new int?(PXInstanceHelper.CurrentCompany);
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    PXResultset<UPCompany> pxResultset = instance.Companies.Search<UPCompany.companyID>((object) field0);
    if (pxResultset != null)
    {
      instance.Companies.Current = (UPCompany) pxResultset;
      throw new PXRedirectRequiredException((PXGraph) instance, true, (string) null);
    }
  }

  public virtual void _(Events.RowSelected<TenantSnapshotDeletion> e)
  {
    if (this.Filter.Current?.Name != "T" || e.Row == null)
      return;
    Tenant extension = this.Records.Cache.GetExtension<Tenant>((object) e.Row);
    if (!PXCompanyHelper.IsNotVisibleCompany(e.Row.TenantId))
      return;
    PXUIFieldAttribute.SetWarning<TenantSnapshotDeletion.tenantId>(this.Records.Cache, (object) e.Row, PXMessages.LocalizeFormatNoPrefix("The data of the {0} tenant was corrupted and cannot be restored. You need to delete this tenant.", (object) $"{extension.TenantName} ({e.Row.TenantId})"));
  }

  public virtual void _(Events.RowSelected<DeletionAction> e)
  {
    if (e.Row == null)
      return;
    bool isVisible = e.Row.Name == "T";
    PXUIFieldAttribute.SetVisible<TenantSnapshotDeletion.tenantId>(this.Records.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<Tenant.tenantName>(this.Records.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<Tenant.status>(this.Records.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.snapshotName>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.description>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.visibility>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.createdOn>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.version>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot.exportMode>(this.Records.Cache, (object) null, !isVisible);
    PXUIFieldAttribute.SetVisible<TenantSnapshotDeletion.snapshotId>(this.Records.Cache, (object) null, !isVisible);
    string actionName = e.Row.Name;
    this.Records.SetProcessDelegate((Action<List<TenantSnapshotDeletion>, CancellationToken>) ((deletionList, cancellationToken) => TenantSnapshotDeletionProcess.PerformDeletion(deletionList, cancellationToken, actionName)));
    int num = this.LongOperationManager.GetStatus() == PXLongRunStatus.InProcess ? 1 : 0;
    bool flag = num == 0 && this.LongOperationManager.GetStatus((object) TenantSnapshotDeletionProcess._deletionProcessUID) == PXLongRunStatus.InProcess;
    bool enabled = num == 0 && !flag;
    this.Records.SetProcessEnabled(enabled);
    this.Records.SetProcessAllEnabled(enabled);
    string error = flag ? "A snapshot or tenant is being deleted at the moment. A new deletion process can be started only after the current process is finished." : (string) null;
    PXUIFieldAttribute.SetWarning<DeletionAction.name>(e.Cache, (object) e.Row, error);
  }

  public virtual void _(Events.FieldUpdated<DeletionAction.name> e) => this.Records.Cache.Clear();

  internal static void PerformDeletion(
    List<TenantSnapshotDeletion> deletionList,
    CancellationToken token,
    string actionName)
  {
    switch (actionName)
    {
      case "T":
        TenantSnapshotDeletionProcess.TryExecuteSingleDeletionProcess((System.Action) (() => TenantSnapshotDeletionProcess.PerformTenantsDeletion((IList<TenantSnapshotDeletion>) deletionList, token)), deletionList.Count, "The tenant cannot be deleted because another snapshot or tenant is being deleted at the moment.");
        break;
      case "S":
        TenantSnapshotDeletionProcess.TryExecuteSingleDeletionProcess((System.Action) (() => TenantSnapshotDeletionProcess.PerformSnapshotsDeletion(deletionList, false, token)), deletionList.Count, "The snapshot cannot be deleted because another snapshot or tenant is being deleted at the moment.");
        break;
      case "O":
        TenantSnapshotDeletionProcess.TryExecuteSingleDeletionProcess((System.Action) (() => TenantSnapshotDeletionProcess.PerformSnapshotsDeletion(deletionList, true, token)), deletionList.Count, "The snapshot cannot be deleted because another snapshot or tenant is being deleted at the moment.");
        break;
      default:
        throw new PXException("The selected action is not supported on the Delete Snapshots and Tenants (SM503000) form.");
    }
  }

  private static void TryExecuteSingleDeletionProcess(
    System.Action deletionMethod,
    int itemsCount,
    string operationInProgressErrorMessage)
  {
    if (PXLongOperation.GetStatus((object) TenantSnapshotDeletionProcess._deletionProcessUID) == PXLongRunStatus.InProcess)
    {
      for (int index = 0; index < itemsCount; ++index)
        PXProcessing.SetError(index, operationInProgressErrorMessage);
      throw new PXException(operationInProgressErrorMessage);
    }
    object[] processingList;
    PXProcessingInfo processingInfo = PXProcessing.GetProcessingInfo(PXLongOperation.GetOperationKey(), out processingList);
    PXLongOperation.StartOperationWithForceAsync(TenantSnapshotDeletionProcess._deletionProcessUID, (PXToggleAsyncDelegate) (() =>
    {
      PXProcessing.SetProcessingInfo(processingInfo, processingList);
      deletionMethod();
    }));
    PXLongOperation.WaitCompletion((object) TenantSnapshotDeletionProcess._deletionProcessUID);
  }

  private static void PerformTenantsDeletion(
    IList<TenantSnapshotDeletion> tenantList,
    CancellationToken token)
  {
    List<TenantSnapshotDeletion> snapshotDeletionList = new List<TenantSnapshotDeletion>();
    for (int index = 0; index < tenantList.Count; ++index)
    {
      TenantSnapshotDeletion tenant = tenantList[index];
      int? tenantId = tenant.TenantId;
      int currentCompany = PXInstanceHelper.CurrentCompany;
      if (!(tenantId.GetValueOrDefault() == currentCompany & tenantId.HasValue))
      {
        snapshotDeletionList.Add(tenant);
      }
      else
      {
        string message = PXMessages.LocalizeNoPrefix("The tenant cannot be deleted because you are signed in to it.");
        PXProcessing.SetError(index, message);
      }
    }
    TenantSnapshotDeletionProcess instance = PXGraph.CreateInstance<TenantSnapshotDeletionProcess>();
    Guid userId = instance.Accessinfo.UserID;
    string screenId = instance.Accessinfo.ScreenID.Replace(".", "");
    foreach (TenantSnapshotDeletion deletion in snapshotDeletionList)
      TenantSnapshotDeletionProcess.MarkPlannedForDeletion(deletion, userId, screenId);
    PXDatabase.Provider.GetMaintenance().ReinitialiseCompanies();
    PXDatabase.ClearCompanyCache();
    foreach (TenantSnapshotDeletion tenantDeletion in snapshotDeletionList)
      TenantSnapshotDeletionProcess.DeleteTenant(instance, tenantDeletion, token);
  }

  private static void DeleteTenant(
    TenantSnapshotDeletionProcess graph,
    TenantSnapshotDeletion tenantDeletion,
    CancellationToken token)
  {
    string screenId = graph.Accessinfo.ScreenID.Replace(".", "");
    Tenant extension = graph.Records.Cache.GetExtension<Tenant>((object) tenantDeletion);
    graph.Logger.ForSystemEvents("System", "System_TenantDeletionStartEventId").ForContext("ContextScreenId", (object) screenId, false).Information<string>("Tenant deletion has been started {TargetTenant}", extension.TenantName);
    if (PXBlobStorage.Provider != null)
      PXBlobStorage.Provider.CleanUp(tenantDeletion.TenantId.Value);
    TenantSnapshotDeletion[] array = graph.SelectAllTenantSnapshots(tenantDeletion.TenantId.Value).ToArray<TenantSnapshotDeletion>();
    Guid userId = graph.Accessinfo.UserID;
    foreach (TenantSnapshotDeletion deletion in array)
      TenantSnapshotDeletionProcess.MarkPlannedForDeletion(deletion, userId, screenId);
    foreach (TenantSnapshotDeletion deletion in array)
      TenantSnapshotDeletionProcess.DeleteTenantData(graph, deletion, token);
    if (token.IsCancellationRequested)
      return;
    TenantSnapshotDeletionProcess.DeleteTenantData(graph, tenantDeletion, token);
    graph.Logger.ForSystemEvents("System", "System_TenantDeletionEndEventId").ForContext("ContextScreenId", (object) screenId, false).Information<string>("Tenant deletion has been completed {TargetTenant}", extension.TenantName);
  }

  internal static void PerformSnapshotsDeletion(
    List<TenantSnapshotDeletion> snapshotList,
    bool isOrphaned,
    CancellationToken token)
  {
    TenantSnapshotDeletionProcess instance = PXGraph.CreateInstance<TenantSnapshotDeletionProcess>();
    Guid userId = instance.Accessinfo.UserID;
    string screenId = instance.Accessinfo.ScreenID.Replace(".", "");
    foreach (TenantSnapshotDeletion snapshot in snapshotList)
      TenantSnapshotDeletionProcess.MarkPlannedForDeletion(snapshot, userId, screenId);
    string eventID1;
    string eventID2;
    string str1;
    string str2;
    if (isOrphaned)
    {
      eventID1 = "System_OrphanedSnapshotDeletionStartEventId";
      eventID2 = "System_OrphanedSnapshotDeletionEndEventId";
      str1 = "Orphaned snapshot deletion has been started";
      str2 = "Orphaned snapshot deletion has been completed";
    }
    else
    {
      eventID1 = "System_SnapshotDeletionStartEventId";
      eventID2 = "System_SnapshotDeletionEndEventId";
      str1 = "Snapshot deletion has been started";
      str2 = "Snapshot deletion has been completed";
    }
    ILogger ilogger1 = instance.Logger.ForSystemEvents("System", eventID1).ForContext("ContextScreenId", (object) screenId, false);
    ILogger ilogger2 = instance.Logger.ForSystemEvents("System", eventID2).ForContext("ContextScreenId", (object) screenId, false);
    foreach (TenantSnapshotDeletion snapshot in snapshotList)
    {
      PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot extension = instance.Records.Cache.GetExtension<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot>((object) snapshot);
      ilogger1.Information<int?, string>(str1 + " SourceTenant: {SourceTenant}, Snapshot: {Snapshot}", snapshot.TenantId, extension.SnapshotName);
      TenantSnapshotDeletionProcess.DeleteTenantData(instance, snapshot, token);
      ilogger2.Information<int?, string>(str2 + " SourceTenant: {SourceTenant}, Snapshot: {Snapshot}", snapshot.TenantId, extension.SnapshotName);
    }
  }

  private static void DeleteTenantData(
    TenantSnapshotDeletionProcess graph,
    TenantSnapshotDeletion deletion,
    CancellationToken token)
  {
    TenantSnapshotDeletionProcess.MarkDeletionStarted(deletion);
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    dbServicesPoint.SchemaReader.ClearCache();
    dbServicesPoint.SchemaReader.OmitTriggersAndFks = false;
    DeleteCompanyExecutionObserver observer = new DeleteCompanyExecutionObserver();
    DbmsMaintenance maintenance = PXDatabase.Provider.GetMaintenance(dbServicesPoint, (IExecutionObserver) observer);
    int? type = deletion.Type;
    int num1 = 1;
    ReadOnlyCollection<TableHeader> readOnlyCollection = (type.GetValueOrDefault() == num1 & type.HasValue ? maintenance.TablesForSnapshotDeletion : maintenance.TablesForCompanyDeletion).OrderBy<TableHeader, string>((System.Func<TableHeader, string>) (t => ((TableEntityBase) t).Name), (IComparer<string>) StringComparer.OrdinalIgnoreCase).ToList<TableHeader>().AsReadOnly();
    int num2 = deletion.TenantId.Value;
    System.Func<TableHeader, IEnumerable<CommandBase>> func = (System.Func<TableHeader, IEnumerable<CommandBase>>) (h => Enumerable.Empty<CommandBase>());
    DbmsMaintenance.ConstraintsOffFlags constraintsOffFlags1 = (DbmsMaintenance.ConstraintsOffFlags) 80 /*0x50*/;
    try
    {
      DbmsMaintenance.ConstraintsOffFlags constraintsOffFlags2 = (DbmsMaintenance.ConstraintsOffFlags) (constraintsOffFlags1 | 1);
      List<CommandBase> commandBaseList = maintenance.processTablesWithConstraintsOff((IEnumerable<TableHeader>) readOnlyCollection, func, constraintsOffFlags2);
      CmdDelete cmdDelete = new CmdDelete(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion"), (List<YaqlJoin>) null)
      {
        Condition = Yaql.and(Yaql.ne<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.id>((string) null), deletion.Id), Yaql.and(Yaql.eq<int?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.tenantId>((string) null), deletion.TenantId), Yaql.eq<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.snapshotId>((string) null), deletion.SnapshotId)))
      };
      commandBaseList.Add((CommandBase) cmdDelete);
      maintenance.execute(commandBaseList, (DbmsSession) null);
      bool flag = string.IsNullOrEmpty(deletion.DeletionProgress);
      foreach (TableHeader table in readOnlyCollection)
      {
        if (flag)
          TenantSnapshotDeletionProcess.DeleteTenantTableData(deletion, table, maintenance, observer, graph.Logger, token);
        else
          flag = deletion.DeletionProgress.Equals(((TableEntityBase) table).Name, StringComparison.OrdinalIgnoreCase);
      }
    }
    finally
    {
      DbmsMaintenance.ConstraintsOffFlags constraintsOffFlags3 = (DbmsMaintenance.ConstraintsOffFlags) (constraintsOffFlags1 | 2);
      List<CommandBase> commandBaseList = maintenance.processTablesWithConstraintsOff((IEnumerable<TableHeader>) readOnlyCollection, func, constraintsOffFlags3);
      maintenance.execute(commandBaseList, (DbmsSession) null);
    }
    if (token.IsCancellationRequested)
      return;
    Guid userId = graph.Accessinfo.UserID;
    string screenId = graph.Accessinfo.ScreenID.Replace(".", "");
    TenantSnapshotDeletionProcess.FinishTenantDeletion(deletion, userId, screenId);
  }

  private static void DeleteTenantTableData(
    TenantSnapshotDeletion deletion,
    TableHeader table,
    DbmsMaintenance maintenance,
    DeleteCompanyExecutionObserver observer,
    ILogger logger,
    CancellationToken token)
  {
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null);
    bool flag = false;
    observer.TableToTrack = ((TableEntityBase) table).Name;
    while (!flag && !token.IsCancellationRequested)
    {
      DeleteCompanyExecutionObserver executionObserver = observer;
      int? nullable1 = new int?();
      int? nullable2 = nullable1;
      executionObserver.LastDeleteTableRowsAffected = nullable2;
      DbmsMaintenance dbmsMaintenance1 = maintenance;
      nullable1 = deletion.TenantId;
      int num1 = nullable1.Value;
      TableHeader tableHeader1 = table;
      dbmsMaintenance1.DeleteCompanyByBatch(num1, 1000, tableHeader1, false);
      nullable1 = observer.LastDeleteTableRowsAffected;
      int num2;
      if (nullable1.HasValue)
      {
        nullable1 = observer.LastDeleteTableRowsAffected;
        int num3 = 0;
        if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
        {
          nullable1 = observer.LastDeleteTableRowsAffected;
          int num4 = 1000;
          num2 = nullable1.GetValueOrDefault() < num4 & nullable1.HasValue ? 1 : 0;
          goto label_5;
        }
      }
      num2 = 1;
label_5:
      flag = num2 != 0;
      if (flag)
      {
        nullable1 = deletion.Type;
        int num5 = 0;
        if (nullable1.GetValueOrDefault() == num5 & nullable1.HasValue && table.HasCompanyMask())
        {
          DbmsMaintenance dbmsMaintenance2 = maintenance;
          nullable1 = deletion.TenantId;
          int num6 = nullable1.Value;
          TableHeader tableHeader2 = table;
          dbmsMaintenance2.DeleteCompanyByBatch(num6, 0, tableHeader2, true);
        }
        CmdUpdate cmdUpdate = new CmdUpdate(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion"), (IEnumerable<YaqlJoin>) null)
        {
          Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.id>((string) null), deletion.Id),
          AssignValues = {
            {
              "DeletionProgress",
              Yaql.constant<string>(((TableEntityBase) table).Name, SqlDbType.Variant)
            }
          }
        };
        dbServicesPoint.executeSingleCommand((CommandBase) cmdUpdate, executionContext, false);
        nullable1 = deletion.Type;
        int num7 = 0;
        string str = nullable1.GetValueOrDefault() == num7 & nullable1.HasValue ? "Table tenant data has been deleted. Table name: {TableName}." : "Table snapshot data has been deleted. Table name: {TableName}.";
        logger.Information<string>(str, ((TableEntityBase) table).Name);
      }
      CmdUpdate cmdUpdate1 = new CmdUpdate(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion"), (IEnumerable<YaqlJoin>) null)
      {
        Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.id>((string) null), deletion.Id),
        AssignValues = {
          {
            "DeletionHeartbeat",
            Yaql.constant<System.DateTime>(System.DateTime.UtcNow, SqlDbType.Variant)
          }
        }
      };
      dbServicesPoint.executeSingleCommand((CommandBase) cmdUpdate1, executionContext, false);
    }
  }

  private static void FinishTenantDeletion(
    TenantSnapshotDeletion deletion,
    Guid userId,
    string screenId)
  {
    List<CommandBase> commandBaseList = new List<CommandBase>()
    {
      (CommandBase) new CmdDelete(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion"), (List<YaqlJoin>) null)
      {
        Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.id>((string) null), deletion.Id)
      },
      (CommandBase) new CmdDelete(YaqlSchemaTable.op_Implicit("Company"), (List<YaqlJoin>) null)
      {
        Condition = Yaql.eq<int?>((YaqlScalar) Yaql.column("CompanyID", (string) null), deletion.TenantId)
      }
    };
    if (deletion.SnapshotId.HasValue)
      commandBaseList.Add((CommandBase) new CmdUpdate(YaqlSchemaTable.op_Implicit("UPSnapshot"), (IEnumerable<YaqlJoin>) null)
      {
        Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<UPSnapshot.snapshotID>((string) null), deletion.SnapshotId),
        AssignValues = {
          {
            "DeletedDatabaseRecord",
            Yaql.@true
          },
          {
            "IsUnderDeletion",
            Yaql.@false
          },
          {
            "LastModifiedByID",
            Yaql.constant<Guid>(userId, SqlDbType.Variant)
          },
          {
            "LastModifiedDateTime",
            Yaql.utcnow()
          },
          {
            "LastModifiedByScreenID",
            Yaql.constant<string>(screenId, SqlDbType.Variant)
          }
        }
      });
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null);
    using (DbmsSession session = dbServicesPoint.createSession(false))
    {
      session.beginTransaction();
      try
      {
        dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, executionContext, false);
        session.commitTransaction();
      }
      catch
      {
        session.rollbackTransaction();
        throw;
      }
      Guid? snapshotId = deletion.SnapshotId;
      if (!snapshotId.HasValue)
        return;
      if (!PXStorageHelper.IsStorageSetup())
        return;
      try
      {
        IStorageProvider provider = PXStorageHelper.GetProvider();
        IStorageProvider storageProvider1 = provider;
        snapshotId = deletion.SnapshotId;
        Guid id1 = snapshotId.Value;
        if (!storageProvider1.Exists(id1))
          return;
        IStorageProvider storageProvider2 = provider;
        snapshotId = deletion.SnapshotId;
        Guid id2 = snapshotId.Value;
        storageProvider2.Delete(id2);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
      }
    }
  }

  private static void MarkPlannedForDeletion(
    TenantSnapshotDeletion deletion,
    Guid userId,
    string screenId)
  {
    if (deletion.DeletionStatus != "N")
      return;
    deletion.DeletionStatus = "P";
    List<CommandBase> commandBaseList = new List<CommandBase>()
    {
      (CommandBase) new CmdInsert(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion")).AddColumnWithValue<Guid?>(YaqlColumn.op_Implicit("Id"), deletion.Id).AddColumnWithValue<int?>(YaqlColumn.op_Implicit("TenantId"), deletion.TenantId).AddColumnWithValue<Guid?>(YaqlColumn.op_Implicit("SnapshotId"), deletion.SnapshotId).AddColumnWithValue<string>(YaqlColumn.op_Implicit("DeletionStatus"), deletion.DeletionStatus).AddColumnWithValue<System.DateTime>(YaqlColumn.op_Implicit("DeletionHeartbeat"), System.DateTime.UtcNow).AddColumnWithValue<Guid?>(YaqlColumn.op_Implicit("NoteID"), deletion.NoteID),
      (CommandBase) new CmdUpdate(YaqlSchemaTable.op_Implicit("Company"), (IEnumerable<YaqlJoin>) null)
      {
        Condition = Yaql.eq<int?>((YaqlScalar) Yaql.column("CompanyID", (string) null), deletion.TenantId),
        AssignValues = {
          {
            "IsUnderDeletion",
            Yaql.@true
          }
        }
      }
    };
    if (deletion.SnapshotId.HasValue)
      commandBaseList.Add((CommandBase) new CmdUpdate(YaqlSchemaTable.op_Implicit("UPSnapshot"), (IEnumerable<YaqlJoin>) null)
      {
        Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<UPSnapshot.snapshotID>((string) null), deletion.SnapshotId),
        AssignValues = {
          {
            "IsUnderDeletion",
            Yaql.@true
          },
          {
            "LastModifiedByID",
            Yaql.constant<Guid>(userId, SqlDbType.Variant)
          },
          {
            "LastModifiedDateTime",
            Yaql.utcnow()
          },
          {
            "LastModifiedByScreenID",
            Yaql.constant<string>(screenId, SqlDbType.Variant)
          }
        }
      });
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null);
    using (DbmsSession session = dbServicesPoint.createSession(false))
    {
      session.beginTransaction();
      try
      {
        dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, executionContext, false);
        session.commitTransaction();
      }
      catch
      {
        session.rollbackTransaction();
        throw;
      }
    }
  }

  private static void MarkDeletionStarted(TenantSnapshotDeletion deletion)
  {
    if (deletion.DeletionStatus != "P")
      return;
    deletion.DeletionStatus = "S";
    CmdUpdate cmdUpdate1 = new CmdUpdate(YaqlSchemaTable.op_Implicit("TenantSnapshotDeletion"), (IEnumerable<YaqlJoin>) null)
    {
      Condition = Yaql.eq<Guid?>((YaqlScalar) Yaql.column<TenantSnapshotDeletion.id>((string) null), deletion.Id),
      AssignValues = {
        {
          "DeletionStatus",
          Yaql.constant<string>(deletion.DeletionStatus, SqlDbType.Variant)
        },
        {
          "DeletionHeartbeat",
          Yaql.constant<System.DateTime>(System.DateTime.UtcNow, SqlDbType.Variant)
        }
      }
    };
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    ExecutionContext executionContext1 = new ExecutionContext((IExecutionObserver) null);
    CmdUpdate cmdUpdate2 = cmdUpdate1;
    ExecutionContext executionContext2 = executionContext1;
    dbServicesPoint.executeSingleCommand((CommandBase) cmdUpdate2, executionContext2, false);
  }
}
