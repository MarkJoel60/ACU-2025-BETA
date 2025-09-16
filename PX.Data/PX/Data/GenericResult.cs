// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXCacheName("Generic Inquiry Result")]
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
[DebuggerTypeProxy(typeof (GenericResult.GenericResultDebugView))]
[Serializable]
public class GenericResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private static readonly 
  #nullable disable
  HashSet<string> _auxiliaryFields = new HashSet<string>((IEnumerable<string>) new string[1]
  {
    "selected"
  }, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? Selected { get; set; }

  [PXInt]
  public int? Row { get; set; }

  public static IReadOnlyCollection<string> AuxiliaryFields
  {
    get
    {
      return (IReadOnlyCollection<string>) GenericResult._auxiliaryFields.ToList<string>().AsReadOnly();
    }
  }

  public static bool IsAuxiliaryField(string fieldName)
  {
    return GenericResult._auxiliaryFields.Contains(fieldName);
  }

  /// <summary>Actual values for each table. Key is an alias.</summary>
  /// <remarks>If null, something went wrong.</remarks>
  public IDictionary<string, object> Values { get; set; }

  private string DebuggerDisplay
  {
    get
    {
      return this.Values == null ? this.ToString() : $"Records ({this.Values.Count}): {(this.Values.Count > 0 ? (object) string.Join(", ", (IEnumerable<string>) this.Values.Keys) : (object) "<no records>")}";
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GenericResult.selected>
  {
  }

  /// <exclude />
  public abstract class row : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GenericResult.row>
  {
  }

  /// <exclude />
  private class GenericResultDebugView
  {
    private readonly GenericResult _instance;

    public GenericResultDebugView(GenericResult instance) => this._instance = instance;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public object[] Records
    {
      get
      {
        return this._instance.Values == null ? new object[0] : this._instance.Values.Values.ToArray<object>();
      }
    }
  }
}
