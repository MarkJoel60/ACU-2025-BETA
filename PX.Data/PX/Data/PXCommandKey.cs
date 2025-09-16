// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCommandKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXCommandKey
{
  /// <summary>
  /// PXView.GetResult returns empty result without executing real query because some parameters are invalid
  /// we do not need analyze query text and invalidate result based on depends tables
  /// </summary>
  internal bool BadParamsQueryNotExecuted;
  private object[] _Parameters;
  private object[] _Searches;
  private string[] _SortColumns;
  private bool[] _Descendings;
  private string[] _restrictedFields;
  public string CommandText;
  private int? _StartRow;
  private PXFilterRow[] _Filters;
  private bool _ReadBranchRestricted;
  private PXCommandKey.Archived _ReadArchived;
  public System.Type Select;

  internal int? _MaximumRows { get; private set; }

  public System.Type CacheType { get; set; }

  internal object[] GetParameters() => this._Parameters;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(((IEnumerable<object>) this._Parameters).JoinToString<object>("(", ",", ")"));
    stringBuilder.Append(((IEnumerable<object>) this._Searches).JoinToString<object>("[", ",", "]"));
    return stringBuilder.ToString();
  }

  public PXCommandKey(object[] parametes)
    : this(parametes, false)
  {
  }

  public PXCommandKey(object[] parametes, bool singleRow, bool? readBranchRestricted = null)
    : this(parametes, (object[]) null, (string[]) null, (bool[]) null, singleRow ? new int?(0) : new int?(), singleRow ? new int?(1) : new int?(), (PXFilterRow[]) null, ((int) readBranchRestricted ?? (PXDatabase.ReadBranchRestricted ? 1 : 0)) != 0)
  {
  }

  public PXCommandKey(
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    int? startRow,
    int? maximumRows,
    PXFilterRow[] filters,
    bool readBranchRestricted,
    string[] restrictedFields = null)
    : this(parameters, searches, sortcolumns, descendings, startRow, maximumRows, filters, readBranchRestricted, false, restrictedFields)
  {
  }

  public PXCommandKey(
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    int? startRow,
    int? maximumRows,
    PXFilterRow[] filters,
    bool readBranchRestricted,
    bool readArchived,
    string[] restrictedFields = null)
  {
    bool flag = false;
    if (parameters != null)
    {
      for (int index = 0; index < parameters.Length; ++index)
      {
        if (parameters[index] != null && parameters[index].GetType().IsArray)
        {
          flag = true;
          break;
        }
        if (parameters[index] is PXGraph)
          throw new PXException("A PXGraph instance has been passed as a query parameter to the Select method.");
        if (parameters[index] is PXGraphExtension)
          throw new PXException("A PXGraphExtension instance has been passed as a query parameter to the Select method.");
      }
    }
    if (flag)
    {
      List<object> objectList = new List<object>();
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        if (parameters[index1] != null && parameters[index1].GetType().IsArray)
        {
          objectList.Add((object) ((Array) parameters[index1]).Length);
          for (int index2 = 0; index2 < ((Array) parameters[index1]).Length; ++index2)
            objectList.Add(((Array) parameters[index1]).GetValue(index2));
        }
        else
          objectList.Add(parameters[index1]);
      }
      this._Parameters = objectList.ToArray();
    }
    else if (parameters != null)
      this._Parameters = (object[]) parameters.Clone();
    if (searches != null)
      this._Searches = (object[]) searches.Clone();
    if (sortcolumns != null)
      this._SortColumns = (string[]) sortcolumns.Clone();
    if (descendings != null)
      this._Descendings = (bool[]) descendings.Clone();
    this._StartRow = startRow;
    this._MaximumRows = maximumRows;
    if (filters != null)
      this._Filters = ((IEnumerable<PXFilterRow>) filters).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (f => f != null ? (PXFilterRow) f.Clone() : (PXFilterRow) null)).ToArray<PXFilterRow>();
    this._ReadBranchRestricted = readBranchRestricted;
    this._ReadArchived = PXDatabase.ReadThroughArchived ? PXCommandKey.Archived.ShowAll : (readArchived ? PXCommandKey.Archived.ShowArchived : PXCommandKey.Archived.ShowNonArchivedOnly);
    if (restrictedFields == null)
      return;
    this._restrictedFields = (string[]) restrictedFields.Clone();
  }

  public override bool Equals(object obj)
  {
    if (!(obj is PXCommandKey pxCommandKey))
      return false;
    if (this == pxCommandKey)
      return true;
    if (this._Parameters != null && pxCommandKey._Parameters != null && this._Parameters.Length == pxCommandKey._Parameters.Length)
    {
      for (int index = 0; index < this._Parameters.Length; ++index)
      {
        if ((this._Parameters[index] != null || pxCommandKey._Parameters[index] != null) && !object.Equals(this._Parameters[index], pxCommandKey._Parameters[index]))
          return false;
      }
    }
    else if (this._Parameters != null || pxCommandKey._Parameters != null)
      return false;
    if (this._Searches != null && pxCommandKey._Searches != null && this._Searches.Length == pxCommandKey._Searches.Length)
    {
      for (int index = 0; index < this._Searches.Length; ++index)
      {
        if ((this._Searches[index] != null || pxCommandKey._Searches[index] != null) && !object.Equals(this._Searches[index], pxCommandKey._Searches[index]))
          return false;
      }
    }
    else if (this._Searches != null || pxCommandKey._Searches != null)
      return false;
    if (this._SortColumns != null && pxCommandKey._SortColumns != null && this._SortColumns.Length == pxCommandKey._SortColumns.Length)
    {
      for (int index = 0; index < this._SortColumns.Length; ++index)
      {
        if ((this._SortColumns[index] != null || pxCommandKey._SortColumns[index] != null) && string.Compare(this._SortColumns[index], pxCommandKey._SortColumns[index], StringComparison.OrdinalIgnoreCase) != 0)
          return false;
      }
    }
    else if (this._SortColumns != null || pxCommandKey._SortColumns != null)
      return false;
    if (this._Descendings != null && pxCommandKey._Descendings != null && this._Descendings.Length == pxCommandKey._Descendings.Length)
    {
      for (int index = 0; index < this._Descendings.Length; ++index)
      {
        if (this._Descendings[index] != pxCommandKey._Descendings[index])
          return false;
      }
    }
    else if (this._Descendings != null || pxCommandKey._Descendings != null)
      return false;
    int? nullable = this._StartRow;
    int? startRow = pxCommandKey._StartRow;
    if (!(nullable.GetValueOrDefault() == startRow.GetValueOrDefault() & nullable.HasValue == startRow.HasValue))
      return false;
    int? maximumRows = this._MaximumRows;
    nullable = pxCommandKey._MaximumRows;
    if (!(maximumRows.GetValueOrDefault() == nullable.GetValueOrDefault() & maximumRows.HasValue == nullable.HasValue))
      return false;
    if (this._Filters != null && pxCommandKey._Filters != null && this._Filters.Length == pxCommandKey._Filters.Length)
    {
      for (int index = 0; index < this._Filters.Length; ++index)
      {
        if (this._Filters[index] != null && pxCommandKey._Filters[index] != null && (!object.Equals(this._Filters[index].Value, pxCommandKey._Filters[index].Value) || !object.Equals(this._Filters[index].Value2, pxCommandKey._Filters[index].Value2) || !object.Equals((object) this._Filters[index].DataField, (object) pxCommandKey._Filters[index].DataField) || !object.Equals((object) this._Filters[index].Condition, (object) pxCommandKey._Filters[index].Condition) || !object.Equals((object) this._Filters[index].OpenBrackets, (object) pxCommandKey._Filters[index].OpenBrackets) || !object.Equals((object) this._Filters[index].CloseBrackets, (object) pxCommandKey._Filters[index].CloseBrackets) || !object.Equals((object) this._Filters[index].OrOperator, (object) pxCommandKey._Filters[index].OrOperator)))
          return false;
      }
    }
    else if (this._Filters != null || pxCommandKey._Filters != null)
      return false;
    return this._ReadBranchRestricted == pxCommandKey._ReadBranchRestricted && this._ReadArchived == pxCommandKey._ReadArchived && !(this.CommandText != pxCommandKey.CommandText) && (this._restrictedFields == null && pxCommandKey._restrictedFields == null || this._restrictedFields != null && pxCommandKey._restrictedFields != null && ((IEnumerable<string>) this._restrictedFields).SequenceEqual<string>((IEnumerable<string>) pxCommandKey._restrictedFields, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)) && !(this.Select != pxCommandKey.Select) && !(this.CacheType != pxCommandKey.CacheType);
  }

  public override int GetHashCode()
  {
    int num1 = 13;
    if (this._Parameters != null)
    {
      for (int index = 0; index < this._Parameters.Length; ++index)
      {
        num1 *= 37;
        if (this._Parameters[index] != null)
          num1 += this._Parameters[index].GetHashCode();
      }
    }
    if (this._Searches != null)
    {
      for (int index = 0; index < this._Searches.Length; ++index)
      {
        num1 *= 37;
        if (this._Searches[index] != null)
          num1 += this._Searches[index].GetHashCode();
      }
    }
    if (this._SortColumns != null)
    {
      for (int index = 0; index < this._SortColumns.Length; ++index)
      {
        num1 *= 37;
        if (this._SortColumns[index] != null)
          num1 += this._SortColumns[index].ToLower().GetHashCode();
      }
    }
    int num2 = num1 * 37;
    if (this._StartRow.HasValue)
      num2 += this._StartRow.GetHashCode();
    int num3 = num2 * 37;
    int? maximumRows = this._MaximumRows;
    if (maximumRows.HasValue)
    {
      int num4 = num3;
      maximumRows = this._MaximumRows;
      int hashCode = maximumRows.GetHashCode();
      num3 = num4 + hashCode;
    }
    int num5 = num3 * 37;
    if (this._restrictedFields != null)
    {
      foreach (string restrictedField in this._restrictedFields)
        num5 += restrictedField.GetHashCode();
    }
    int num6 = num5 * 37;
    if (this.Select != (System.Type) null)
      num6 += this.Select.GetHashCode();
    int num7 = num6 * 37;
    if (this.CacheType != (System.Type) null)
      num7 += this.CacheType.GetHashCode();
    return num7 * 37 + this._ReadArchived.GetHashCode();
  }

  internal PXCommandKey ClonePrevKey()
  {
    PXCommandKey pxCommandKey = new PXCommandKey(this._Parameters);
    pxCommandKey.BadParamsQueryNotExecuted = this.BadParamsQueryNotExecuted;
    pxCommandKey._Parameters = this._Parameters;
    pxCommandKey._Searches = this._Searches;
    pxCommandKey._SortColumns = this._SortColumns;
    pxCommandKey._Descendings = this._Descendings;
    pxCommandKey._restrictedFields = this._restrictedFields;
    pxCommandKey.CommandText = this.CommandText;
    pxCommandKey._StartRow = this._StartRow;
    int? maximumRows = this._MaximumRows;
    pxCommandKey._MaximumRows = maximumRows.HasValue ? new int?(maximumRows.GetValueOrDefault() - 1) : new int?();
    pxCommandKey._Filters = this._Filters;
    pxCommandKey._ReadBranchRestricted = this._ReadBranchRestricted;
    pxCommandKey._ReadArchived = this._ReadArchived;
    pxCommandKey.Select = this.Select;
    pxCommandKey.CacheType = this.CacheType;
    return pxCommandKey;
  }

  private enum Archived
  {
    ShowAll = 1,
    ShowArchived = 2,
    ShowNonArchivedOnly = 4,
  }
}
