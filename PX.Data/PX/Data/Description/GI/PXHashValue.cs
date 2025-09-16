// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXHashValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Description.GI;

public class PXHashValue : IPXValue
{
  private readonly IReadOnlyCollection<PXFieldValue> _valuesTocalc;

  public PXHashValue(params PXFieldValue[] valuesTocalc)
  {
    this._valuesTocalc = (IReadOnlyCollection<PXFieldValue>) valuesTocalc;
  }

  public override bool Equals(IPXValue other)
  {
    return other is PXHashValue pxHashValue && pxHashValue._valuesTocalc.Equals((object) this._valuesTocalc);
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    return new PXDataValue[0];
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> onParameter,
    bool tryInline = false)
  {
    return (SQLExpression) new Md5Hash(this._valuesTocalc.Select<PXFieldValue, Column>((Func<PXFieldValue, Column>) (t => new Column(t.FieldName, t.TableName))).ToArray<Column>());
  }

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    return new object[0];
  }
}
