// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXDataTypesHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer;
using PX.BulkInsert.Installer.DatabaseSetup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data.Update;

public static class PXDataTypesHelper
{
  private const string _COMPANY_SYSTEM = "System";
  private const string _COMPANY_DEMO = "Demo";
  private const string _COMPANY_TRIAL = "Trial";
  private const string _COMPANY_CUSTOM = "Custom";
  private static List<DataTypeInfo> _dataTypes;

  public static IEnumerable<DataTypeInfo> DataTypes
  {
    get
    {
      return LazyInitializer.EnsureInitialized<List<DataTypeInfo>>(ref PXDataTypesHelper._dataTypes, PXDataTypesHelper.\u003C\u003EO.\u003C0\u003E__Prefetch ?? (PXDataTypesHelper.\u003C\u003EO.\u003C0\u003E__Prefetch = new Func<List<DataTypeInfo>>(PXDataTypesHelper.Prefetch))).AsEnumerable<DataTypeInfo>();
    }
  }

  private static List<DataTypeInfo> Prefetch()
  {
    List<DataTypeInfo> source = new List<DataTypeInfo>();
    DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(PXInstanceHelper.UpdateDataFolder ?? string.Empty, "Data"));
    if (directoryInfo.Exists)
      source.AddRange(((IEnumerable<DirectoryInfo>) directoryInfo.GetDirectories()).Where<DirectoryInfo>((Func<DirectoryInfo, bool>) (sub => sub.EnumerateFiles("*.adb").Any<FileInfo>() || sub.EnumerateFiles("*.xml").Any<FileInfo>())).Select<DirectoryInfo, DataTypeInfo>((Func<DirectoryInfo, DataTypeInfo>) (sub => new DataTypeInfo(sub.Name, sub.FullName))));
    DataTypeInfo dataTypeInfo = source.FirstOrDefault<DataTypeInfo>((Func<DataTypeInfo, bool>) (i => string.Equals(i.Name, "System")));
    if (dataTypeInfo == null)
    {
      dataTypeInfo = new DataTypeInfo("System", string.Empty, (ExecuteConditions) 0);
      source.Add(dataTypeInfo);
    }
    dataTypeInfo.Hidden = true;
    dataTypeInfo.Execution = (ExecuteConditions) 3;
    if (!source.Any<DataTypeInfo>((Func<DataTypeInfo, bool>) (i => string.Equals(i.Name, "Demo"))))
      source.Add(new DataTypeInfo("Demo", string.Empty, (ExecuteConditions) 0));
    source.Add(new DataTypeInfo("Custom", string.Empty, (ExecuteConditions) 0));
    source.Add(new DataTypeInfo("Trial", string.Empty, (ExecuteConditions) 0));
    return source;
  }

  public static IEnumerable<DataTypeInfo> AvailableDataTypes
  {
    get
    {
      return PXDataTypesHelper.DataTypes.Where<DataTypeInfo>((Func<DataTypeInfo, bool>) (i => !i.Empty && !i.Hidden && i.Execution > 0));
    }
  }

  public static IEnumerable<DataTypeInfo> FilledDataTypes
  {
    get
    {
      return PXDataTypesHelper.DataTypes.Where<DataTypeInfo>((Func<DataTypeInfo, bool>) (i => !i.Empty));
    }
  }

  public static DataTypeInfo SystemCompany => PXDataTypesHelper.GetTypeByCode("System");

  public static DataTypeInfo DemoCompany => PXDataTypesHelper.GetTypeByCode("Demo");

  public static DataTypeInfo TrialCompany => PXDataTypesHelper.GetTypeByCode("Trial");

  public static DataTypeInfo UserCompany => PXDataTypesHelper.GetTypeByCode("Custom");

  public static DataTypeInfo GetTypeByCode(string name)
  {
    return PXDataTypesHelper.DataTypes.FirstOrDefault<DataTypeInfo>((Func<DataTypeInfo, bool>) (i => !string.IsNullOrEmpty(i.Name) && string.Equals(i.Name, name)));
  }
}
