// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionPXResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionPXResult : ProjectionItem
{
  private readonly System.Type[] _result;
  private readonly ProjectionItem[] _itemsProjections;

  public bool HasCount { get; private set; }

  public ProjectionPXResult(System.Type[] result)
  {
    this.HasCount = !typeof (IBqlTable).IsAssignableFrom(result[result.Length - 1]);
    int length = this.HasCount ? result.Length - 1 : result.Length;
    this._itemsProjections = new ProjectionItem[length];
    this._result = new System.Type[length];
    for (int index = 0; index < length; ++index)
    {
      this._itemsProjections[index] = (ProjectionItem) new ProjectionReference(result[index]);
      this._result[index] = result[index];
    }
  }

  public override System.Type GetResultType() => ProjectionPXResult.GetResultType(this._result);

  public System.Type[] GetResultTypes() => this._result;

  internal static object CreateInstance(System.Type[] dacs, object[] data)
  {
    return Activator.CreateInstance(ProjectionPXResult.GetResultType(dacs).MakeGenericType(dacs), data);
  }

  private static System.Type GetResultType(System.Type[] dacs)
  {
    switch (dacs.Length)
    {
      case 1:
        return typeof (PXResult<>);
      case 2:
        return typeof (PXResult<,>);
      case 3:
        return typeof (PXResult<,,>);
      case 4:
        return typeof (PXResult<,,,>);
      case 5:
        return typeof (PXResult<,,,,>);
      case 6:
        return typeof (PXResult<,,,,,>);
      case 7:
        return typeof (PXResult<,,,,,,>);
      case 8:
        return typeof (PXResult<,,,,,,,>);
      case 9:
        return typeof (PXResult<,,,,,,,,>);
      case 10:
        return typeof (PXResult<,,,,,,,,,>);
      case 11:
        return typeof (PXResult<,,,,,,,,,,>);
      case 12:
        return typeof (PXResult<,,,,,,,,,,,>);
      case 13:
        return typeof (PXResult<,,,,,,,,,,,,>);
      case 14:
        return typeof (PXResult<,,,,,,,,,,,,,>);
      case 15:
        return typeof (PXResult<,,,,,,,,,,,,,,>);
      case 16 /*0x10*/:
        return typeof (PXResult<,,,,,,,,,,,,,,,>);
      case 17:
        return typeof (PXResult<,,,,,,,,,,,,,,,,>);
      case 18:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,>);
      case 19:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,>);
      case 20:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,>);
      case 21:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,>);
      case 22:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,>);
      case 23:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,>);
      case 24:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,>);
      case 25:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,,>);
      default:
        throw new PXException("There should be upto 25 params. ");
    }
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    object[] data1 = new object[this._itemsProjections.Length];
    for (int index = 0; index < this._itemsProjections.Length; ++index)
      data1[index] = this._itemsProjections[index].GetValue(data, ref position, context);
    object instance = ProjectionPXResult.CreateInstance(this._result, data1);
    if (this.HasCount && instance is PXResult pxResult)
    {
      int? int32 = data.GetInt32(position++);
      pxResult.RowCount = int32;
    }
    return instance;
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    PXResult pxResult = (PXResult) value;
    object[] data = new object[this._itemsProjections.Length];
    for (int i = 0; i < this._itemsProjections.Length; ++i)
      data[i] = this._itemsProjections[i].CloneValue(pxResult[i], context);
    PXResult instance = (PXResult) ProjectionPXResult.CreateInstance(this._result, data);
    instance.RowCount = pxResult.RowCount;
    return (object) instance;
  }

  internal override object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    PXResult pxResult = (PXResult) base.Transform(value, predicate, map);
    for (int i = 0; i < this._itemsProjections.Length; ++i)
      pxResult[i] = this._itemsProjections[i].Transform(pxResult[i], predicate, map);
    return (object) pxResult;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder("PXResult<");
    for (int index = 0; index < this._result.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(this._result[index].Name);
    }
    stringBuilder.Append(">");
    return stringBuilder.ToString();
  }
}
