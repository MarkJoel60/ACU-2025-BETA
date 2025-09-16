// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXParameterValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXParameterValue : IPXValue
{
  private readonly Func<object, object> _paramValueHandler;

  public PXParameter Parameter { get; }

  public PXGraph Graph { get; }

  public PXParameterValue(PXParameter parameter, PXGraph graph)
  {
    if (parameter == null)
      throw new ArgumentNullException(nameof (parameter));
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    this.Parameter = parameter;
    this.Graph = graph;
  }

  /// <param name="paramValueHandler">For handling special parameters like segmented keys.</param>
  public PXParameterValue(
    PXParameter parameter,
    PXGraph graph,
    Func<object, object> paramValueHandler)
    : this(parameter, graph)
  {
    this._paramValueHandler = paramValueHandler;
  }

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    object obj = this.Parameter.Value;
    if (obj is string str && RelativeDatesManager.IsRelativeDatesString(str))
      obj = (object) RelativeDatesManager.EvaluateAsDateTime(str);
    if (!string.IsNullOrEmpty(this.Parameter.DataField))
    {
      PXCache cach = this.Graph.Caches[this.Parameter.Table];
      if (this._paramValueHandler != null)
        obj = this._paramValueHandler(obj);
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(this.Parameter.DataField, (object) null, obj, PXDBOperation.External, cach.GetItemType(), out description);
      obj = description.DataValue ?? obj;
    }
    return new object[1]{ obj };
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    object obj1 = this.Parameter.Value;
    if (obj1 is string str && RelativeDatesManager.IsRelativeDatesString(str))
      obj1 = (object) RelativeDatesManager.EvaluateAsDateTime(str);
    if (!string.IsNullOrEmpty(this.Parameter.DataField))
    {
      PXCache cach = this.Graph.Caches[this.Parameter.Table];
      if (this._paramValueHandler != null)
        obj1 = this._paramValueHandler(obj1);
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(this.Parameter.DataField, (object) null, obj1, PXDBOperation.External, cach.GetItemType(), out description);
      object obj2 = description.DataValue ?? obj1;
      return new PXDataValue[1]
      {
        new PXDataValue(description.DataType, obj2)
      };
    }
    return new PXDataValue[1]{ new PXDataValue(obj1) };
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> paramHandler,
    bool tryInline = false)
  {
    return paramHandler(this.Parameter.Name);
  }

  public override bool Equals(IPXValue other)
  {
    PXParameterValue pxParameterValue = other as PXParameterValue;
    if (other == null || pxParameterValue == null)
      return false;
    return this == other || this.Parameter.Equals((object) pxParameterValue.Parameter);
  }

  internal string GetParameterName() => this.Parameter.Name;
}
