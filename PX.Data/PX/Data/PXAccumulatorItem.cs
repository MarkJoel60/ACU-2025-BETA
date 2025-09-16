// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccumulatorItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public sealed class PXAccumulatorItem
{
  private string _Field;
  private List<KeyValuePair<PXComp, object>> _PastComparison;
  private List<KeyValuePair<PXComp, object>> _CurrentComparison;
  private List<KeyValuePair<PXComp, object>> _FutureComparison;
  private string _InitializeFrom;
  private bool _HasInitializeValue;
  private object _InitializeWith;
  private bool? _OrderPast;
  private object _CurrentUpdate;
  private PXDataFieldAssign.AssignBehavior _CurrentUpdateBehavior;
  private object _FutureUpdate;
  private PXDataFieldAssign.AssignBehavior _FutureUpdateBehavior;

  public PXAccumulatorItem(string field) => this._Field = field;

  public void RestrictPast(PXComp comparison, object value)
  {
    if (this._PastComparison == null)
      this._PastComparison = new List<KeyValuePair<PXComp, object>>();
    this._PastComparison.Add(new KeyValuePair<PXComp, object>(comparison, value));
  }

  public void OrderPast(bool ascending) => this._OrderPast = new bool?(ascending);

  public void RestrictCurrent(PXComp comparison, object value)
  {
    if (this._CurrentComparison == null)
      this._CurrentComparison = new List<KeyValuePair<PXComp, object>>();
    this._CurrentComparison.Add(new KeyValuePair<PXComp, object>(comparison, value));
  }

  public void RestrictFuture(PXComp comparison, object value)
  {
    if (this._FutureComparison == null)
      this._FutureComparison = new List<KeyValuePair<PXComp, object>>();
    this._FutureComparison.Add(new KeyValuePair<PXComp, object>(comparison, value));
  }

  public void InitializeFrom(string field) => this._InitializeFrom = field;

  public void InitializeWith(object value)
  {
    this._InitializeWith = value;
    this._HasInitializeValue = true;
  }

  public void UpdateCurrent(object value)
  {
    this.UpdateCurrent(value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  public void UpdateCurrent(object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this._CurrentUpdate = value;
    this._CurrentUpdateBehavior = value != null || behavior != PXDataFieldAssign.AssignBehavior.Replace ? behavior : PXDataFieldAssign.AssignBehavior.Nullout;
  }

  public void UpdateFuture(object value)
  {
    this._FutureUpdate = value;
    this._FutureUpdateBehavior = PXDataFieldAssign.AssignBehavior.Summarize;
  }

  public void UpdateFuture(object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this._FutureUpdate = value;
    this._FutureUpdateBehavior = value != null || behavior != PXDataFieldAssign.AssignBehavior.Replace ? behavior : PXDataFieldAssign.AssignBehavior.Nullout;
  }

  public string Field => this._Field;

  public KeyValuePair<PXComp, object>[] PastComparison
  {
    get
    {
      return this._PastComparison == null || this._PastComparison.Count == 0 ? new KeyValuePair<PXComp, object>[0] : this._PastComparison.ToArray();
    }
  }

  public KeyValuePair<PXComp, object>[] CurrentComparison
  {
    get
    {
      return this._CurrentComparison == null || this._CurrentComparison.Count == 0 ? new KeyValuePair<PXComp, object>[0] : this._CurrentComparison.ToArray();
    }
  }

  public KeyValuePair<PXComp, object>[] FutureComparison
  {
    get
    {
      return this._FutureComparison == null || this._FutureComparison.Count == 0 ? new KeyValuePair<PXComp, object>[0] : this._FutureComparison.ToArray();
    }
  }

  public KeyValuePair<string, object>? Initializer
  {
    get
    {
      return this._InitializeFrom == null && !this._HasInitializeValue ? new KeyValuePair<string, object>?() : new KeyValuePair<string, object>?(new KeyValuePair<string, object>(this._InitializeFrom, this._InitializeWith));
    }
  }

  public bool? OrderBy => this._OrderPast;

  public object CurrentUpdate => this._CurrentUpdate;

  public object FutureUpdate => this._FutureUpdate;

  public PXDataFieldAssign.AssignBehavior CurrentUpdateBehavior => this._CurrentUpdateBehavior;

  public PXDataFieldAssign.AssignBehavior FutureUpdateBehavior => this._FutureUpdateBehavior;
}
