// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSnapshotBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.BulkInsert;
using PX.BulkInsert.Provider;
using PX.BulkInsert.Provider.RowTransforms;
using PX.Data.Services;
using PX.DbServices.Model;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

internal abstract class PXSnapshotBase
{
  private readonly SnapshotConfigReader _configReader = new SnapshotConfigReader();
  protected readonly ISnapshotService _snapshotService = ServiceLocator.Current.GetInstance<ISnapshotService>();
  protected const string CUSTOMIZED = "*";
  protected static readonly object LOCKER = new object();
  public const string FULL = "Full";
  protected UPCompany _Company;
  protected UPSnapshot _Snapshot;

  protected string TempFolder { get; }

  protected string SnapshotsFolder { get; }

  public static string GetSnapshotsFolderPath()
  {
    string appSetting = ConfigurationManager.AppSettings["SnapshotsFolder"];
    return !string.IsNullOrEmpty(appSetting) ? appSetting : throw new PXException("The directory for saving the snapshot is not specified.");
  }

  protected PXSnapshotBase()
  {
    this.SnapshotsFolder = PXSnapshotBase.GetSnapshotsFolderPath();
    this.TempFolder = Path.Combine(this.SnapshotsFolder, "temp", new Guid().ToString());
    if (Directory.Exists(this.TempFolder))
      return;
    Directory.CreateDirectory(this.TempFolder);
  }

  protected virtual XmlElement ReadExportScenario(string path)
  {
    return this._configReader.ReadConfigRootElement(path);
  }

  public abstract void Start();

  protected static int getNextFreeIdForCompany(PointDbmsBase point, int companyId)
  {
    int maxVal = -100000000 - companyId * 10000 - 1;
    int minVal = maxVal - 10000 + 2;
    YaqlColumn yaqlColumn = Yaql.column("LinkedCompany", (string) null);
    IEnumerable<int> first = point.getCompanies(true).Select<CompanyHeader, int>((Func<CompanyHeader, int>) (c => c.Id));
    YaqlVectorQuery yaqlVectorQuery = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("UPSnapshot", (string) null), (List<YaqlJoin>) null);
    ((YaqlQueryBase) yaqlVectorQuery).where(Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), companyId), Yaql.isNotNull((YaqlScalar) yaqlColumn)));
    yaqlVectorQuery.Column = YaqlScalarAlilased.op_Implicit(yaqlColumn);
    IEnumerable<int> second = (IEnumerable<int>) point.selectVector<int>(yaqlVectorQuery, false);
    int[] array = first.Union<int>(second).Where<int>((Func<int, bool>) (c => c <= maxVal && c > minVal)).ToArray<int>();
    Array.Sort<int>(array);
    return array.Length != 0 ? array[0] - 1 : maxVal;
  }

  protected IEnumerable<TransferTableTask> ParseExportScenarioXml(
    PointDbmsBase point,
    string exportMode,
    bool forceMasked,
    int company)
  {
    foreach (KeyValuePair<string, TableForSnapshot> keyValuePair in this.CollectExportScenariosInXml(point, exportMode, forceMasked, company))
    {
      TransferTableTask transferTableTask = new TransferTableTask()
      {
        Source = point.createTableAdapter(keyValuePair.Value.TableQuery, point.Schema.GetTable(keyValuePair.Key))
      };
      if (keyValuePair.Value.Transform != null)
        transferTableTask.Transforms.Add((IRowTransform) keyValuePair.Value.Transform);
      yield return transferTableTask;
    }
  }

  public static string[] GetExportModes(bool localized = false)
  {
    return SnapshotConfigReader.GetExportModes(localized);
  }

  protected Dictionary<string, TableForSnapshot> CollectExportScenariosInXml(
    PointDbmsBase point,
    string exportMode,
    bool forceMasked,
    int company,
    bool includeCompany = false)
  {
    return this._configReader.ReadTableRules(exportMode, point, forceMasked, company, includeCompany);
  }
}
