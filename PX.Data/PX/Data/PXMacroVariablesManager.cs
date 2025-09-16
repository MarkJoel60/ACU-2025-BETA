// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMacroVariablesManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class PXMacroVariablesManager : IMacroVariablesManager
{
  private static readonly StringComparer DefaultComparer = (StringComparer) new PXMacroVariablesManager.IgnoreTrailingWhitespacesStringComparer(StringComparer.OrdinalIgnoreCase);
  private readonly IDictionary<string, IMacroVariable> _variables;

  public PXMacroVariablesManager(IEnumerable<IMacroVariable> variables)
  {
    this._variables = (IDictionary<string, IMacroVariable>) variables.ToDictionary<IMacroVariable, string, IMacroVariable>((Func<IMacroVariable, string>) (v => v.Name), (Func<IMacroVariable, IMacroVariable>) (v => v), (IEqualityComparer<string>) PXMacroVariablesManager.DefaultComparer);
  }

  public bool IsVariable(string definition)
  {
    return !string.IsNullOrEmpty(definition) && this._variables.ContainsKey(definition);
  }

  public object Resolve(string definition, System.Type dataType)
  {
    if (string.IsNullOrWhiteSpace(definition))
      throw new ArgumentException("Value cannot be null or whitespace.", nameof (definition));
    IMacroVariable macroVariable;
    return !this._variables.TryGetValue(definition, out macroVariable) ? (object) null : macroVariable.Resolve(dataType);
  }

  public object ResolveExt(string definition, PXCache cache, string fieldName, object row)
  {
    System.Type fieldType = cache.GetFieldType(fieldName);
    System.Type type = Nullable.GetUnderlyingType(fieldType);
    if ((object) type == null)
      type = fieldType;
    System.Type dataType = type;
    object returnValue = this.Resolve(definition, dataType);
    try
    {
      cache.RaiseFieldSelecting(fieldName, row, ref returnValue, true);
      returnValue = PXFieldState.UnwrapValue(returnValue);
      object newValue = returnValue;
      cache.RaiseFieldUpdating(fieldName, row, ref newValue);
    }
    catch (PXException ex)
    {
      object[] objArray = new object[1]
      {
        (object) definition
      };
      throw new PXException((Exception) ex, "Variable {0} is not applicable to this field.", objArray);
    }
    return returnValue;
  }

  private class IgnoreTrailingWhitespacesStringComparer : StringComparer
  {
    private readonly StringComparer _inner;

    public IgnoreTrailingWhitespacesStringComparer(StringComparer inner) => this._inner = inner;

    private string WithoutTrailingWhitespaces(string str) => str?.TrimEnd();

    public override int Compare(string x, string y)
    {
      return this._inner.Compare(this.WithoutTrailingWhitespaces(x), this.WithoutTrailingWhitespaces(y));
    }

    public override bool Equals(string x, string y)
    {
      return this._inner.Equals(this.WithoutTrailingWhitespaces(x), this.WithoutTrailingWhitespaces(y));
    }

    public override int GetHashCode(string obj)
    {
      return this._inner.GetHashCode(this.WithoutTrailingWhitespaces(obj));
    }
  }
}
