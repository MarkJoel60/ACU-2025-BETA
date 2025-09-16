// Decompiled with JetBrains decompiler
// Type: PX.Data.PXIntListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Model.Entities;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Sets a drop-down list as the input control for a DAC field.
/// In this control, a user selects from a fixed set of integer values, which are
/// represented in the drop-down list by string labels.</summary>
/// <remarks>
/// <para>The attribute configures a drop-down list that represents the
/// DAC field in the user interface. In the attribute constructor, you should provide the list of
/// possible integer values and the list of the corresponding labels.</para>
/// <para>You can reset the lists of values and labels at run time by
/// calling the <tt>SetList&lt;&gt;</tt> static method.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXIntList(
///     new int[] { 0, 1 },
///     new string[] { "Apply Credit Hold", "Release Credit Hold" })]
/// public virtual int? Action { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXBaseListAttribute))]
public class PXIntListAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXLocalizableList
{
  protected int[] _AllowedValues;
  protected string[] _AllowedLabels;
  protected string[] _NeutralAllowedLabels;
  protected bool _ExclusiveValues = true;
  protected string _Locale;

  /// <summary>Gets or sets the value that indicates whether the labels used
  /// by the attribute are localizable.</summary>
  public virtual bool IsLocalizable { get; set; }

  /// <summary>Gets or sets the value that indicates whether a user can
  /// input a value not available in the list of allowed values. By default, the property is set to
  /// <see langword="true" />, which means that the user can select only from the
  /// values in the drop-down list.</summary>
  public bool ExclusiveValues
  {
    get => this._ExclusiveValues;
    set => this._ExclusiveValues = value;
  }

  /// <summary>Gets the dictionary of allowed value-label pairs.</summary>
  public Dictionary<int, string> ValueLabelDic
  {
    get
    {
      Dictionary<int, string> valueLabelDic = new Dictionary<int, string>(this._AllowedValues.Length);
      for (int index = 0; index < this._AllowedValues.Length; ++index)
        valueLabelDic.Add(this._AllowedValues[index], this._AllowedLabels[index]);
      return valueLabelDic;
    }
  }

  /// <exclude />
  protected PXIntListAttribute.DBIntProperties DBProperties { get; private set; }

  /// <summary>Initializes a new instance with empty lists of possible values
  /// and labels.</summary>
  public PXIntListAttribute()
  {
    this.DBProperties = new PXIntListAttribute.DBIntProperties();
    this.IsLocalizable = true;
  }

  /// <summary>Initializes a new instance with the list of possible values
  /// obtained from the provided string. The string should contain either
  /// values separated by a comma, or value-label pairs where the value and
  /// label are separated by a semicolon and different pairs are separated
  /// by a comma. In the first case, labels are set to value strings. Values
  /// are converted from strings to integers.</summary>
  /// <param name="list">The string that contains the list of values separated
  /// by comma.</param>
  public PXIntListAttribute(string list)
    : this()
  {
    string[] strArray = list.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    this._AllowedValues = new int[strArray.Length];
    this._AllowedLabels = new string[strArray.Length];
    for (int index = 0; index < strArray.Length; ++index)
    {
      int length = strArray[index].IndexOf(';');
      if (length >= 0)
      {
        this._AllowedValues[index] = int.Parse(strArray[index].Substring(0, length));
        this._AllowedLabels[index] = length + 1 >= strArray[index].Length ? strArray[index].Substring(0, length) : strArray[index].Substring(length + 1);
      }
      else
      {
        this._AllowedValues[index] = int.Parse(strArray[index]);
        this._AllowedLabels[index] = strArray[index];
      }
    }
    this.CreateNeutralLabels();
  }

  /// <summary>Initializes a new instance with the specified lists of
  /// possible values and corresponding labels. The two lists must be of the same
  /// length.</summary>
  /// <param name="allowedValues">The list of values assigned to the field
  /// when a user selects the corresponding labels..</param>
  /// <param name="allowedLabels">The list of labels displayed in the user
  /// interface when a user expands the control.</param>
  public PXIntListAttribute(int[] allowedValues, string[] allowedLabels)
    : this()
  {
    if (allowedValues.Length != allowedLabels.Length)
      throw new PXArgumentException(nameof (allowedValues), "The length of the values array is not equal to the length of labels array.");
    this._AllowedValues = allowedValues;
    this._AllowedLabels = allowedLabels;
    this.CreateNeutralLabels();
  }

  /// <summary>Initializes a new instance, extracting the list of possible
  /// values and labels from the provided enumeration. Uses the enumeration
  /// values as possible values and enumeration values names as the
  /// corresponding labels.</summary>
  /// <param name="enumType">The <tt>enum</tt> type that defines the lists
  /// of possible values and labels.</param>
  public PXIntListAttribute(System.Type enumType, bool byteValues = false)
    : this()
  {
    Array values = Enum.GetValues(enumType);
    this._AllowedValues = byteValues ? Array.ConvertAll<byte, int>((byte[]) values, (Converter<byte, int>) (b => (int) b)) : (int[]) values;
    this._AllowedLabels = Enum.GetNames(enumType);
    this.CreateNeutralLabels();
  }

  public PXIntListAttribute(System.Type enumType, string[] allowedLabels)
    : this()
  {
    this._AllowedValues = (int[]) Enum.GetValues(enumType);
    if (this._AllowedValues.Length != allowedLabels.Length)
      throw new PXArgumentException(nameof (allowedLabels), "The length of the labels array is less than the values count.");
    this._AllowedLabels = allowedLabels;
    this.CreateNeutralLabels();
  }

  /// <summary>Initializes a new instance with the specified collection of tuples
  /// of possible values and corresponding labels.</summary>
  /// <param name="valuesToLabels">The list of pairs where
  /// first item is a value assigned to the field when a user selects the corresponding labels, and
  /// second item is a label displayed in the user interface when a user expands the control.</param>
  protected PXIntListAttribute(params Tuple<int, string>[] valuesToLabels)
    : this(((IEnumerable<Tuple<int, string>>) valuesToLabels).Select<Tuple<int, string>, int>((Func<Tuple<int, string>, int>) (t => t.Item1)).ToArray<int>(), ((IEnumerable<Tuple<int, string>>) valuesToLabels).Select<Tuple<int, string>, string>((Func<Tuple<int, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  public PXIntListAttribute(params (int Value, string Label)[] valuesToLabels)
    : this(((IEnumerable<(int, string)>) valuesToLabels).Select<(int, string), int>((Func<(int, string), int>) (pair => pair.Value)).ToArray<int>(), ((IEnumerable<(int, string)>) valuesToLabels).Select<(int, string), string>((Func<(int, string), string>) (pair => pair.Label)).ToArray<string>())
  {
  }

  /// <summary>Assigns the provided lists of possible values and labels
  /// to the attribute instance that marks the specified field in a
  /// particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXIntList</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="allowedValues">The new list of values.</param>
  /// <param name="allowedLabels">The new list of labels.</param>
  public static void SetList<Field>(
    PXCache cache,
    object data,
    int[] allowedValues,
    string[] allowedLabels)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXIntListAttribute)
      {
        PXIntListAttribute intListAttribute = (PXIntListAttribute) attribute;
        intListAttribute._AllowedValues = (int[]) allowedValues?.Clone();
        intListAttribute._AllowedLabels = (string[]) allowedLabels?.Clone();
        intListAttribute._NeutralAllowedLabels = (string[]) null;
        intListAttribute.CreateNeutralLabels();
        intListAttribute.TryLocalize(cache);
      }
    }
  }

  public static void SetList<Field>(
    PXCache cache,
    object data,
    params (int Value, string Label)[] valuesToLabels)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXIntListAttribute intListAttribute in cache.GetAttributes<Field>(data).OfType<PXIntListAttribute>())
    {
      intListAttribute._AllowedValues = ((IEnumerable<(int, string)>) valuesToLabels).Select<(int, string), int>((Func<(int, string), int>) (pair => pair.Value)).ToArray<int>();
      intListAttribute._AllowedLabels = ((IEnumerable<(int, string)>) valuesToLabels).Select<(int, string), string>((Func<(int, string), string>) (pair => pair.Label)).ToArray<string>();
      intListAttribute._NeutralAllowedLabels = (string[]) null;
      intListAttribute.CreateNeutralLabels();
      intListAttribute.TryLocalize(cache);
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (this._AllowedLabels != null)
    {
      string[] allowedLabels = this._AllowedLabels;
      this._AllowedLabels = new string[allowedLabels.Length];
      Array.Copy((Array) allowedLabels, 0, (Array) this._AllowedLabels, 0, allowedLabels.Length);
    }
    this.AppendNeutral();
    this._Locale = Thread.CurrentThread.CurrentCulture.Name;
    this.TryLocalize(sender);
  }

  private void AppendNeutral()
  {
    if (this._NeutralAllowedLabels == null || this._AllowedLabels == null || this._NeutralAllowedLabels.Length == this._AllowedLabels.Length)
      return;
    this._NeutralAllowedLabels = new string[this._AllowedLabels.Length];
    this._AllowedLabels.CopyTo((Array) this._NeutralAllowedLabels, 0);
  }

  private void TryLocalize(PXCache sender)
  {
    if (!this.IsLocalizable)
      return;
    if (ResourceCollectingManager.IsStringCollecting)
      PXPageRipper.RipList(this.FieldName, sender, this._NeutralAllowedLabels, CollectResourceSettings.Resource);
    else
      PXLocalizerRepository.ListLocalizer.Localize(this.FieldName, sender, this._NeutralAllowedLabels, this._AllowedLabels);
  }

  private void CreateNeutralLabels()
  {
    if (this._AllowedLabels == null || this._AllowedLabels.Length == 0 || this._NeutralAllowedLabels != null)
      return;
    this._NeutralAllowedLabels = new string[this._AllowedLabels.Length];
    this._AllowedLabels.CopyTo((Array) this._NeutralAllowedLabels, 0);
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    string name = Thread.CurrentThread.CurrentCulture.Name;
    if (this._Locale != null && !string.Equals(this._Locale, name))
    {
      this._Locale = name;
      this.TryLocalize(sender);
    }
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(), new int?(), new int?(), new int?(), this._AllowedValues, this._AllowedLabels, new bool?(this._ExclusiveValues), (System.Type) null, new int?(), this._NeutralAllowedLabels);
    if (!this.DBProperties.IsSet)
      this.DBProperties.Fill(this._BqlTable, this._FieldName);
    if (!this.DBProperties.IsSet)
      return;
    bool? nullable = this.DBProperties.Nullable;
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    ((PXIntState) e.ReturnState).EmptyPossible = true;
  }

  /// <summary>Pairs value to its label</summary>
  protected static Tuple<int, string> Pair(int value, string label)
  {
    return Tuple.Create<int, string>(value, label);
  }

  protected class DBIntProperties
  {
    public bool? _nullable;
    private ReaderWriterLock _sync = new ReaderWriterLock();

    public bool? Nullable
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._nullable;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }

    public void Fill(System.Type table, string field)
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        if (this._nullable.HasValue)
          return;
        ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
        if (this._nullable.HasValue)
          return;
        if (table == (System.Type) null)
        {
          this._nullable = new bool?(true);
        }
        else
        {
          try
          {
            TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(table.Name);
            if (tableStructure != null)
            {
              TableColumn tableColumn = tableStructure.Columns.FirstOrDefault<TableColumn>((Func<TableColumn, bool>) (c => string.Equals(((TableEntityBase) c).Name, field, StringComparison.OrdinalIgnoreCase)));
              if (tableColumn != null)
                this._nullable = new bool?(tableColumn.IsNullable);
              else
                this._nullable = new bool?(true);
            }
            else
              this._nullable = new bool?(true);
          }
          catch
          {
          }
        }
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }

    public bool IsSet
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._nullable.HasValue;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }
  }
}
