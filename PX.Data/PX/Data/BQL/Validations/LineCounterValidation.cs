// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Validations.LineCounterValidation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.BQL.Validations;

internal abstract class LineCounterValidation
{
  protected PXCache _ChildCache;
  protected PXCache _ParentCache;
  protected System.Type _ParentType;
  protected string _ParentFieldName;
  protected string _ChildFieldName;
  protected string _TypeCode;

  public static LineCounterValidation CreateValidation(
    System.Type parentparentFieldTypeField,
    bool UseStrongLineCounterCalculation)
  {
    LineCounterValidation validation;
    switch (System.Type.GetTypeCode(parentparentFieldTypeField))
    {
      case TypeCode.Int16:
        validation = (LineCounterValidation) LineCounterValidation<short>.CreateValidation(UseStrongLineCounterCalculation);
        break;
      case TypeCode.Int32:
        validation = (LineCounterValidation) LineCounterValidation<int>.CreateValidation(UseStrongLineCounterCalculation);
        break;
      case TypeCode.Int64:
        validation = (LineCounterValidation) LineCounterValidation<long>.CreateValidation(UseStrongLineCounterCalculation);
        break;
      default:
        validation = (LineCounterValidation) null;
        break;
    }
    return validation;
  }

  public virtual void Initialize(
    PXCache cache,
    System.Type parentType,
    string fieldName,
    string parentFieldName)
  {
    this._ChildCache = cache;
    this._ParentType = parentType;
    this._ParentCache = cache.Graph.Caches[parentType];
    this._ChildFieldName = fieldName;
    this._ParentFieldName = parentFieldName;
    cache.Graph.OnBeforeCommit += new System.Action<PXGraph>(this.Validate);
  }

  protected abstract void Validate(PXGraph graph);
}
