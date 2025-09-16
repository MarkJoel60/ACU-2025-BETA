// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSqlCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

#nullable disable
namespace PX.Data;

internal class PXSqlCache
{
  public static readonly bool IsEnabled = PXSqlCache.QueryCacheLevel == "db";
  public static readonly bool IsGraphLevelCacheEnabled = PXSqlCache.QueryCacheLevel == "graph";
  private static readonly ConcurrentDictionary<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData> Items = new ConcurrentDictionary<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>();

  private static string QueryCacheLevel
  {
    get
    {
      string str = WebConfigurationManager.AppSettings[nameof (QueryCacheLevel)];
      if (Str.IsNullOrEmpty(str))
        str = "graph";
      return str == "graph" || str == "db" || str == "none" ? str : throw new PXException("Invalid option {0} for web config QueryCacheLevel", new object[1]
      {
        (object) str
      });
    }
  }

  public static IEnumerable Groups
  {
    get
    {
      return (IEnumerable) ConcurrentDictionaryExtensions.ValuesExt<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>(PXSqlCache.Items).ToLookup<PXSqlCache.SqlCommandData, string>((System.Func<PXSqlCache.SqlCommandData, string>) (it => ((IEnumerable<string>) it.TablesAge).JoinToString<string>(",")));
    }
  }

  public static IDataReader CacheReader(IDbCommand cmd, Func<IDataReader> src)
  {
    PXSqlCache.SqlCommandKey key = new PXSqlCache.SqlCommandKey()
    {
      Command = cmd
    };
    PXSqlCache.SqlCommandData sqlCommandData1;
    if (PXSqlCache.Items.TryGetValue(key, out sqlCommandData1))
    {
      if (!sqlCommandData1.IsExpired)
        return (IDataReader) sqlCommandData1.Data.CreateDataReader();
      PXSqlCache.RemoveExpired();
    }
    PXSqlCache.SqlCommandData sqlCommandData2 = new PXSqlCache.SqlCommandData(PXDatabase.ExtractTablesFromSql(cmd.CommandText))
    {
      Key = key
    };
    if (sqlCommandData2.TablesAge.Length == 0)
      return src();
    DataSet dataSet = new DataSet()
    {
      EnforceConstraints = false,
      Tables = {
        sqlCommandData2.Data
      }
    };
    sqlCommandData2.Data.Load(src());
    PXSqlCache.Items.TryAdd(key, sqlCommandData2);
    return (IDataReader) sqlCommandData2.Data.CreateDataReader();
  }

  internal static async Task<DbDataReader> CacheReaderAsync(
    IDbCommand cmd,
    Func<Task<DbDataReader>> src)
  {
    PXSqlCache.SqlCommandKey key = new PXSqlCache.SqlCommandKey()
    {
      Command = cmd
    };
    PXSqlCache.SqlCommandData sqlCommandData;
    if (PXSqlCache.Items.TryGetValue(key, out sqlCommandData))
    {
      if (!sqlCommandData.IsExpired)
        return (DbDataReader) sqlCommandData.Data.CreateDataReader();
      PXSqlCache.RemoveExpired();
    }
    PXSqlCache.SqlCommandData t = new PXSqlCache.SqlCommandData(PXDatabase.ExtractTablesFromSql(cmd.CommandText))
    {
      Key = key
    };
    if (t.TablesAge.Length == 0)
      return await src();
    DataSet dataSet = new DataSet()
    {
      EnforceConstraints = false,
      Tables = {
        t.Data
      }
    };
    DataTable dataTable = t.Data;
    dataTable.Load((IDataReader) await src());
    dataTable = (DataTable) null;
    PXSqlCache.Items.TryAdd(key, t);
    return (DbDataReader) t.Data.CreateDataReader();
  }

  public static void RemoveExpired()
  {
    foreach (PXSqlCache.SqlCommandKey key in PXSqlCache.Items.Where<KeyValuePair<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>>((System.Func<KeyValuePair<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>, bool>) (_ => _.Value.IsExpired)).Select<KeyValuePair<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>, PXSqlCache.SqlCommandKey>((System.Func<KeyValuePair<PXSqlCache.SqlCommandKey, PXSqlCache.SqlCommandData>, PXSqlCache.SqlCommandKey>) (_ => _.Key)).ToArray<PXSqlCache.SqlCommandKey>())
      PXSqlCache.Items.TryRemove(key, out PXSqlCache.SqlCommandData _);
  }

  private class SqlCommandKey
  {
    public IDbCommand Command;
    private int? _hash;

    public override bool Equals(object obj)
    {
      return PXSqlCache.SqlCommandKey.CompareCommands(this.Command, ((PXSqlCache.SqlCommandKey) obj).Command);
    }

    private static bool CompareCommands(IDbCommand a, IDbCommand b)
    {
      if (a.CommandText != b.CommandText || a.Parameters.Count != b.Parameters.Count)
        return false;
      foreach (DbParameter parameter1 in (IEnumerable) a.Parameters)
      {
        DbParameter parameter2 = (DbParameter) b.Parameters[parameter1.ParameterName];
        if (parameter1.Value == null)
        {
          if (parameter2.Value != null)
            return false;
        }
        else if (!parameter1.Value.Equals(parameter2.Value))
          return false;
      }
      return true;
    }

    public override int GetHashCode()
    {
      if (this._hash.HasValue)
        return this._hash.Value;
      this._hash = new int?(this.Command.CommandText.GetHashCode());
      foreach (DbParameter parameter in (IEnumerable) this.Command.Parameters)
      {
        if (parameter.Value != null)
        {
          int? hash = this._hash;
          int hashCode = parameter.Value.GetHashCode();
          this._hash = hash.HasValue ? new int?(hash.GetValueOrDefault() ^ hashCode) : new int?();
        }
      }
      return this._hash.Value;
    }
  }

  private class SqlCommandData
  {
    public PXSqlCache.SqlCommandKey Key;
    private PXDatabase.PXTableAge Age;
    public readonly DataTable Data = new DataTable();
    internal readonly string[] TablesAge;

    public SqlCommandData(string[] tables)
    {
      this.Age = PXDatabase.GetAge();
      this.TablesAge = tables;
    }

    public bool IsExpired
    {
      get
      {
        return ((IEnumerable<string>) this.TablesAge).Any<string>((System.Func<string, bool>) (t => PXDatabase.IsTableModified(t, this.Age)));
      }
    }

    public override string ToString()
    {
      return ((IEnumerable<string>) this.TablesAge).JoinToString<string>(",") + this.Key.Command.Parameters.OfType<DbParameter>().Select<DbParameter, object>((System.Func<DbParameter, object>) (p => p.Value)).JoinToString<object>("(", ",", ")") + $" {this.Data.Rows.Count} rows";
    }
  }
}
