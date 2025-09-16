// Decompiled with JetBrains decompiler
// Type: PX.Data.RuleWeakeningScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class RuleWeakeningScope : IDisposable
{
  internal static readonly RuleWeakeningScope Dummy = new RuleWeakeningScope();
  private readonly PXGraph _graph;
  private readonly Lookup<System.Type, PXSetPropertyException> _errors;
  private readonly IReadOnlyDictionary<System.Type, RuleWeakenLevel> _fieldsWeakenLevels;

  private IEnumerable<ISuppressiblePreventer> Rules
  {
    get => this._graph.Extensions.OfType<ISuppressiblePreventer>();
  }

  internal RuleWeakeningScope()
  {
  }

  public RuleWeakeningScope(PXGraph graph, System.Type field, RuleWeakenLevel ruleWeakenLevel)
    : this(graph, (IReadOnlyDictionary<System.Type, RuleWeakenLevel>) new Dictionary<System.Type, RuleWeakenLevel>()
    {
      [field] = ruleWeakenLevel
    })
  {
  }

  public RuleWeakeningScope(
    PXGraph graph,
    IReadOnlyDictionary<System.Type, RuleWeakenLevel> fieldsWeakenLevels)
  {
    this._graph = graph;
    this._errors = new Lookup<System.Type, PXSetPropertyException>((IEqualityComparer<System.Type>) null, (IEqualityComparer<PXSetPropertyException>) null);
    this._fieldsWeakenLevels = (IReadOnlyDictionary<System.Type, RuleWeakenLevel>) fieldsWeakenLevels.ToDictionary<KeyValuePair<System.Type, RuleWeakenLevel>, System.Type, RuleWeakenLevel>((Func<KeyValuePair<System.Type, RuleWeakenLevel>, System.Type>) (t => t.Key), (Func<KeyValuePair<System.Type, RuleWeakenLevel>, RuleWeakenLevel>) (t => t.Value));
    foreach (ISuppressiblePreventer rule in this.Rules)
    {
      foreach (KeyValuePair<System.Type, RuleWeakenLevel> fieldsWeakenLevel in (IEnumerable<KeyValuePair<System.Type, RuleWeakenLevel>>) this._fieldsWeakenLevels)
      {
        if (rule.Fields.Contains<System.Type>(fieldsWeakenLevel.Key))
          rule.PushWeakeningScope(fieldsWeakenLevel.Key, this);
      }
    }
  }

  void IDisposable.Dispose()
  {
    if (this._graph == null)
      return;
    foreach (ISuppressiblePreventer rule in this.Rules)
    {
      foreach (KeyValuePair<System.Type, RuleWeakenLevel> fieldsWeakenLevel in (IEnumerable<KeyValuePair<System.Type, RuleWeakenLevel>>) this._fieldsWeakenLevels)
      {
        if (rule.Fields.Contains<System.Type>(fieldsWeakenLevel.Key))
          rule.PopWeakeningScope(fieldsWeakenLevel.Key);
      }
    }
  }

  public RuleWeakenLevel GetRuleWeakenLevelForField(System.Type field)
  {
    RuleWeakenLevel weakenLevelForField = RuleWeakenLevel.None;
    this._fieldsWeakenLevels?.TryGetValue(field, out weakenLevelForField);
    return weakenLevelForField;
  }

  public ILookup<System.Type, PXSetPropertyException> SuppressedErrors
  {
    get => (ILookup<System.Type, PXSetPropertyException>) this._errors;
  }

  internal void RegisterSuppressedError(System.Type field, PXSetPropertyException error)
  {
    this._errors?.Add(field, error);
  }
}
