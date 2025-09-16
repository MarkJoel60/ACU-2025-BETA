// Decompiled with JetBrains decompiler
// Type: PX.Data.PXResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public abstract class PXResult
{
  [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
  protected internal object[] Items;
  private System.Type[] tables;
  private int? rowcount;

  public abstract object this[System.Type t] { get; }

  public abstract object this[string s] { get; }

  public static PXResult<T0> New<T0>(PXResult<T0> t0) where T0 : class, IBqlTable, new()
  {
    return new PXResult<T0>((T0) t0);
  }

  public static PXResult<T0, T1> New<T0, T1>(PXResult<T0> t0, T1 t1)
    where T0 : class, IBqlTable, new()
    where T1 : class, IBqlTable, new()
  {
    return new PXResult<T0, T1>((T0) t0, t1);
  }

  public object this[int i]
  {
    get => i >= 0 && i < this.Items.Length ? this.Items[i] : (object) null;
    internal set
    {
      if (i < 0 || i >= this.Items.Length)
        return;
      this.Items[i] = value;
    }
  }

  public abstract System.Type GetItemType(int i);

  public abstract System.Type GetItemType(string s);

  public System.Type[] Tables
  {
    get
    {
      if (this.tables == null)
      {
        this.tables = new System.Type[this.TableCount];
        for (int i = 0; i < this.TableCount; ++i)
          this.tables[i] = this.GetItemType(i);
      }
      return this.tables;
    }
  }

  public int TableCount => this.Items.Length;

  public int? RowCount
  {
    get => this.rowcount;
    set => this.rowcount = value;
  }

  public static T Unwrap<T>(object row) where T : IBqlTable
  {
    return (T) (row is PXResult pxResult ? pxResult[typeof (T)] : row);
  }

  public T GetItem<T>() where T : IBqlTable => (T) this[typeof (T)];

  public static IBqlTable Unwrap(object row, System.Type rowType)
  {
    return row is PXResult pxResult ? (IBqlTable) pxResult[rowType] : (IBqlTable) row;
  }

  public static IBqlTable UnwrapMain(object row) => (IBqlTable) PXResult.UnwrapFirst(row);

  public static object UnwrapFirst(object row) => !(row is PXResult pxResult) ? row : pxResult[0];

  internal TResult Convert<TResult>() where TResult : PXResult
  {
    System.Type type = typeof (TResult);
    if (!type.IsGenericType)
      throw new ArgumentException("TResult in the method Convert must be generic");
    object[] objArray = new object[type.GenericTypeArguments.Length];
    for (int index = 0; index < type.GenericTypeArguments.Length; ++index)
      objArray[index] = this[type.GenericTypeArguments[index]] ?? throw new ArgumentException($"Not can convert from {this.GetType().FullName} to {type.FullName}");
    TResult result = (TResult) type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[0], (ParameterModifier[]) null).Invoke((object[]) null);
    result.Items = objArray;
    return result;
  }
}
