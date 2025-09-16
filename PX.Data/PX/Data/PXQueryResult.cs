// Decompiled with JetBrains decompiler
// Type: PX.Data.PXQueryResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public sealed class PXQueryResult : IDeserializationCallback
{
  internal bool BadParamsSkipMergeCache;
  public List<object> Items;
  private string[] TablesAge;
  internal PXDatabase.PXTableAge Age;
  [NonSerialized]
  public bool HasPlacedNotChanged;
  public readonly bool RequestOnly;

  public PXQueryResult(string[] dbTableNames, bool requestOnly = false)
  {
    this.Age = PXDatabase.GetAge();
    this.TablesAge = dbTableNames;
    this.RequestOnly = requestOnly;
  }

  public bool IsExpired(PXDatabaseProvider provider)
  {
    return ((IEnumerable<string>) this.TablesAge).Any<string>((Func<string, bool>) (t => provider.IsTableModified(t, this.Age)));
  }

  void IDeserializationCallback.OnDeserialization(object sender) => this.Age.ResetLocalAge();
}
