// Decompiled with JetBrains decompiler
// Type: PX.Api.SYSubstitutionMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

public class SYSubstitutionMaint : 
  PXGraph<SYSubstitutionMaint>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXSelect<SYSubstitution> Substitution;
  [PXImport(typeof (SYSubstitution))]
  public PXSelect<SYSubstitutionValues, Where<SYSubstitutionValues.substitutionID, Equal<Current<SYSubstitution.substitutionID>>>> SubstitutionValues;
  public PXSave<SYSubstitution> Save;
  public PXCancel<SYSubstitution> Cancel;
  public PXInsert<SYSubstitution> Insert;
  public PXDeleteNoClose<SYSubstitution> Delete;
  public PXCopyPasteAction<SYSubstitution> CopyPaste;
  public PXFirst<SYSubstitution> First;
  public PXPrevious<SYSubstitution> Previous;
  public PXNext<SYSubstitution> Next;
  public PXLast<SYSubstitution> Last;
  public Dictionary<SYSubstitutionValues, SYSubstitutionValues> Details;

  protected virtual void SYSubstitution_FieldName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SYSubstitution row) || row.TableName == null)
      return;
    List<string> internalNames;
    List<string> displayNames;
    SYSubstitutionMaint.GetTableFields(row.TableName, (PXGraph) this, out internalNames, out displayNames);
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), "fieldName", new bool?(), new int?(), (string) null, internalNames.ToArray(), displayNames.ToArray(), new bool?(true), (string) null);
  }

  protected virtual void SYSubstitution_TableName_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is SYSubstitution row) || row.TableName.OrdinalEquals((string) e.NewValue))
      return;
    row.FieldName = (string) null;
  }

  protected virtual void SYSubstitutionValues_SubstitutedValue_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    SYSubstitution current = this.Substitution.Current;
    if (current == null || current.TableName == null || current.FieldName == null || !(this.Caches[PXBuildManager.GetType(current.TableName, true)].GetStateExt((object) null, current.FieldName) is PXFieldState stateExt))
      return;
    stateExt.Value = e.ReturnValue;
    e.ReturnState = (object) stateExt;
  }

  protected virtual void SYSubstitutionValues_OriginalValue_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SYSubstitutionValues row) || !(e.NewValue is string newValue1))
      return;
    if ((SYSubstitutionValues) PXSelectBase<SYSubstitutionValues, PXSelect<SYSubstitutionValues, Where<SYSubstitutionValues.substitutionID, Equal<Current<SYSubstitution.substitutionID>>, And<SYSubstitutionValues.originalValue, Equal<Required<SYSubstitutionValues.originalValue>>>>>.Config>.Select((PXGraph) this, (object) newValue1) == null)
      return;
    PXCache pxCache = cache;
    string newValue2 = newValue1;
    PXSetPropertyException propertyException = new PXSetPropertyException("An element with the {0} value already exists.", new object[1]
    {
      (object) newValue1
    });
    pxCache.RaiseExceptionHandling<SYSubstitutionValues.originalValue>((object) row, (object) newValue2, (Exception) propertyException);
  }

  protected virtual void SYSubstitutionValues_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    if (e.Operation != PXDBOperation.Insert && e.Operation != PXDBOperation.Update || !(e.Row is SYSubstitutionValues row) || Str.IsNullOrEmpty(row.OriginalValue))
      return;
    if ((SYSubstitutionValues) PXSelectBase<SYSubstitutionValues, PXSelect<SYSubstitutionValues, Where<SYSubstitutionValues.substitutionID, Equal<Current<SYSubstitution.substitutionID>>, And<SYSubstitutionValues.originalValue, Equal<Required<SYSubstitutionValues.originalValue>>, And<SYSubstitutionValues.valueID, NotEqual<Required<SYSubstitutionValues.valueID>>>>>>.Config>.Select((PXGraph) this, (object) row.OriginalValue, (object) row.ValueID) == null)
      return;
    cache.RaiseExceptionHandling<SYSubstitutionValues.originalValue>((object) row, (object) row.OriginalValue, (Exception) new PXSetPropertyException("An element with the {0} value already exists.", new object[1]
    {
      (object) row.OriginalValue
    }));
  }

  internal static void GetTableFields(
    string tableName,
    PXGraph graph,
    out List<string> internalNames,
    out List<string> displayNames)
  {
    internalNames = new List<string>();
    displayNames = new List<string>();
    if (Str.IsNullOrEmpty(tableName))
      return;
    System.Type type = PXBuildManager.GetType(tableName, false);
    if (type == (System.Type) null)
      return;
    HashSet<string> stringSet = new HashSet<string>();
    List<(string, string)> valueTupleList = new List<(string, string)>();
    PXCache cach = graph.Caches[type];
    foreach (string field1 in (List<string>) cach.Fields)
    {
      System.Type bqlField = cach.GetBqlField(field1);
      if ((!(bqlField != (System.Type) null) || BqlCommand.GetItemType(bqlField).IsAssignableFrom(type)) && (!(bqlField == (System.Type) null) || field1.EndsWith("_Attributes") || field1.EndsWith("Signed") || cach.IsKvExtAttribute(field1)) && !cach.GetAttributes(field1).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute)) && cach.GetStateExt((object) null, field1) is PXFieldState stateExt && stateExt.Enabled && stateExt.Visible && stateExt.Visibility != PXUIVisibility.Invisible && !Str.IsNullOrEmpty(stateExt.DisplayName) && !stringSet.Contains(field1))
      {
        stringSet.Add(field1);
        string field2 = bqlField != (System.Type) null ? bqlField.Name : field1;
        if (!PXGenericInqGrph.IsVirtualField(cach, field2))
          valueTupleList.Add((field2, field1));
      }
    }
    valueTupleList.Sort((Comparison<(string, string)>) ((f1, f2) => string.Compare(f1.DisplayName, f2.DisplayName)));
    foreach ((string, string) valueTuple in valueTupleList)
    {
      internalNames.Add(valueTuple.Item1);
      displayNames.Add(valueTuple.Item2);
    }
  }

  internal SYSubstitution SearchSubstitution(string substitutionID)
  {
    return (SYSubstitution) this.Substitution.Search<SYSubstitution.substitutionID>((object) substitutionID);
  }

  internal SYSubstitutionValues SearchSubstitutionValue(string originalValue)
  {
    return (SYSubstitutionValues) this.SubstitutionValues.Search<SYSubstitutionValues.originalValue>((object) originalValue);
  }

  protected virtual System.Type[] GetAlternativeKeyFields()
  {
    return new System.Type[1]
    {
      typeof (SYSubstitutionValues.originalValue)
    };
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName.OrdinalEquals("SubstitutionValues"))
    {
      System.Type[] alternativeKeyFields = this.GetAlternativeKeyFields();
      PXCache cache = this.SubstitutionValues.Cache;
      if (this.Details == null)
      {
        List<SYSubstitutionValues> list = this.SubstitutionValues.Select().RowCast<SYSubstitutionValues>().ToList<SYSubstitutionValues>();
        this.Details = new Dictionary<SYSubstitutionValues, SYSubstitutionValues>((IEqualityComparer<SYSubstitutionValues>) new SYSubstitutionMaint.KeyValuesComparer<SYSubstitutionValues>(cache, (IEnumerable<System.Type>) alternativeKeyFields));
        foreach (SYSubstitutionValues key in list)
        {
          if (!this.Details.ContainsKey(key))
            this.Details.Add(key, key);
        }
      }
      SYSubstitutionValues instance = (SYSubstitutionValues) cache.CreateInstance();
      foreach (System.Type bqlField in alternativeKeyFields)
      {
        string field = cache.GetField(bqlField);
        object newValue = values[(object) field];
        if (newValue == null)
          cache.RaiseFieldDefaulting(field, (object) instance, out newValue);
        if (newValue != null && !cache.RaiseFieldUpdating(field, (object) instance, ref newValue))
          newValue = (object) null;
        cache.SetValue((object) instance, bqlField.Name, newValue);
      }
      SYSubstitutionValues substitutionValues;
      if (this.Details.TryGetValue(instance, out substitutionValues))
        keys[(object) "ValueID"] = (object) substitutionValues.ValueID;
    }
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this.Details = (Dictionary<SYSubstitutionValues, SYSubstitutionValues>) null;
  }

  private class KeyValuesComparer<TEntity> : IEqualityComparer<TEntity> where TEntity : class, IBqlTable
  {
    private readonly int[] _keyOrdinals;
    private readonly PXCache _cache;

    public KeyValuesComparer(PXCache cache, IEnumerable<System.Type> keyFields)
    {
      this._cache = cache;
      this._keyOrdinals = keyFields.Select<System.Type, int>((Func<System.Type, int>) (f => this._cache.GetFieldOrdinal(f.Name))).ToArray<int>();
    }

    public bool Equals(TEntity x, TEntity y)
    {
      if ((object) x == (object) y)
        return true;
      foreach (int keyOrdinal in this._keyOrdinals)
      {
        if (!object.Equals(this._cache.GetValue((object) x, keyOrdinal), this._cache.GetValue((object) y, keyOrdinal)))
          return false;
      }
      return true;
    }

    public int GetHashCode(TEntity entity)
    {
      if ((object) entity == null)
        return 0;
      int hashCode = 13;
      foreach (int keyOrdinal in this._keyOrdinals)
        hashCode = hashCode * 37 + (this._cache.GetValue((object) entity, keyOrdinal) ?? (object) 0).GetHashCode();
      return hashCode;
    }
  }
}
