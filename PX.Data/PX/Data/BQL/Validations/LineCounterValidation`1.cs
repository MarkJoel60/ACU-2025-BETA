// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Validations.LineCounterValidation`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Validations;

internal class LineCounterValidation<TDataType> : LineCounterValidation where TDataType : IComparable<TDataType>
{
  public static LineCounterValidation<TDataType> CreateValidation(
    bool UseStrongLineCounterCalculation)
  {
    return UseStrongLineCounterCalculation ? (LineCounterValidation<TDataType>) new LineCounterStrongValidation<TDataType>() : new LineCounterValidation<TDataType>();
  }

  protected override void Validate(PXGraph graph)
  {
    if (!this._ChildCache.IsInsertedUpdatedDeleted || this._ParentCache.Current == null)
      return;
    TDataType lineCounterValue = (TDataType) this._ParentCache.GetValue(this._ParentCache.Current, this._ParentFieldName);
    TDataType maxLineNumber = this.GetMaxLineNumber();
    if (this.CheckConsistency(lineCounterValue, maxLineNumber))
      return;
    string name = PXLocalizer.LocalizeFormat("Line Counter Validation: {0}", (object) this._ParentCache.GetItemType().FullName);
    string diagnosticDetails = PXLocalizer.LocalizeFormat("Line counter value: {0}, maximum line number value: {1}.", (object) lineCounterValue, (object) maxLineNumber);
    graph.SetDataConsistencyIssue(name, diagnosticDetails, false);
  }

  protected virtual IEnumerable<TDataType> GetLineNumbers()
  {
    return this._ChildCache.Cached.Cast<object>().Where<object>((Func<object, bool>) (row => EnumerableExtensions.IsIn<PXEntryStatus>(this._ChildCache.GetStatus(row), PXEntryStatus.Inserted, PXEntryStatus.Updated) && PXParentAttribute.IsParent(this._ChildCache, row, this._ParentType))).Select<object, TDataType>((Func<object, TDataType>) (row => (TDataType) this._ChildCache.GetValue(row, this._ChildFieldName)));
  }

  protected virtual bool CheckConsistency(TDataType lineCounterValue, TDataType maxLineNumberValue)
  {
    return maxLineNumberValue.CompareTo(lineCounterValue) <= 0;
  }

  private TDataType GetMaxLineNumber()
  {
    IEnumerable<TDataType> lineNumbers = this.GetLineNumbers();
    return lineNumbers.Any<TDataType>() ? lineNumbers.Max<TDataType>() : default (TDataType);
  }
}
