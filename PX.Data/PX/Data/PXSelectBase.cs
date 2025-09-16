// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
[DebuggerDisplay("{View.DebuggerDisplay, nq}")]
public abstract class PXSelectBase
{
  public PXView View;
  protected PXGraph _Graph;
  private readonly List<Attribute> attributes = new List<Attribute>();

  public List<Attribute> Attributes => this.attributes;

  public virtual PXCache Cache => this.View.Cache;

  public virtual bool AllowSelect
  {
    get => this.View.AllowSelect;
    set => this.View.AllowSelect = value;
  }

  public virtual bool AllowInsert
  {
    get => this.View.AllowInsert;
    set => this.View.AllowInsert = value;
  }

  public virtual bool AllowUpdate
  {
    get => this.View.AllowUpdate;
    set => this.View.AllowUpdate = value;
  }

  public virtual bool AllowDelete
  {
    get => this.View.AllowDelete;
    set => this.View.AllowDelete = value;
  }

  protected PXSelectBase() => InjectMethods.InjectDependencies(this);

  protected static object[] ParseString(string argument)
  {
    string[] strArray = argument.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    object[] objArray = new object[strArray.Length];
    for (int index = 0; index < strArray.Length; ++index)
    {
      string s = strArray[index];
      if (s.Length != 0)
      {
        int result;
        objArray[index] = s[0] == '"' && s[s.Length - 1] == '"' || s[0] == '\'' && s[s.Length - 1] == '\'' ? (object) s.Substring(1, s.Length - 2) : (!char.IsDigit(s[0]) ? (object) s : (!int.TryParse(s, out result) ? (object) s : (object) result));
      }
    }
    return objArray;
  }

  public T GetAttribute<T>() where T : Attribute => this.attributes.OfType<T>().FirstOrDefault<T>();

  public override string ToString() => this.View.ToString();

  internal virtual void SetValueExt(object row, string fieldName, object value)
  {
    this.Cache.SetValueExt(row, fieldName, value);
  }

  internal virtual object GetValueExt(object row, string fieldName)
  {
    object valueInt = this.Cache.GetValueInt(row, fieldName);
    if (valueInt is PXFieldState)
      valueInt = ((PXFieldState) valueInt).Value;
    return valueInt;
  }

  public string Name => this.View.Name;
}
