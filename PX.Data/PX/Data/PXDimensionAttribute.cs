// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDimensionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Common;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Data;

/// <summary>Sets up the input control for a DAC field that holds a
/// segmented value. The control formats the input as a segmented key
/// value and displays the list of allowed values for each key segment
/// when the user presses F3 on a keyboard.</summary>
/// <example>
/// <code>
/// [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
/// public virtual string SubID { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[Serializable]
public class PXDimensionAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldUpdatingSubscriber
{
  protected 
  #nullable disable
  string _Dimension;
  protected bool? _ValidComboRequired;
  public GroupHelper.ParamsPair[][] Restrictions;
  protected PXDimensionAttribute.Definition _Definition;
  protected Delegate _SegmentDelegate;
  protected string[] _SegmentParameters;
  private System.Type _parentSelect;
  protected System.Type _ParentValueField;
  protected PXView _ParentView;
  protected string _Wildcard;
  /// <exclude />
  public const int NoMaxLength = -1;
  private string LastNbr;
  private string NewNumber;
  private int NumberingSEQ;
  private string Numbering;
  protected static Dictionary<string, int> _DimensionMaxLength = new Dictionary<string, int>();
  protected static Dictionary<string, TableColumn> _DimensionColumns = new Dictionary<string, TableColumn>();
  protected static Dictionary<string, Dictionary<string, KeyValuePair<System.Type, string>>> _DimensionTables = new Dictionary<string, Dictionary<string, KeyValuePair<System.Type, string>>>();
  private static readonly System.Type[] _usedTables = new System.Type[3]
  {
    typeof (PXDimensionAttribute.Dimension),
    typeof (PXDimensionAttribute.Segment),
    typeof (PXDimensionAttribute.NumberingSequence)
  };
  protected string customNumbering;
  protected int? customNumberingSegment;
  protected string customNumberingSymbol;

  /// <exclude />
  public virtual void SetSegmentDelegate(Delegate handler)
  {
    this._SegmentDelegate = handler;
    if ((object) handler == null)
      return;
    ParameterInfo[] parameters = this._SegmentDelegate.Method.GetParameters();
    this._SegmentParameters = new string[parameters.Length];
    for (int index = 0; index < parameters.Length; ++index)
      this._SegmentParameters[index] = parameters[index].Name;
  }

  private IEnumerable getOuterSegments(PXCache sender, short segment, string value, object row)
  {
    object[] objArray = new object[this._SegmentParameters.Length];
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (string.Equals(this._SegmentParameters[index], nameof (segment), StringComparison.OrdinalIgnoreCase))
        objArray[index] = (object) segment;
      else if (string.Equals(this._SegmentParameters[index], nameof (value), StringComparison.OrdinalIgnoreCase))
      {
        objArray[index] = (object) value;
      }
      else
      {
        objArray[index] = sender.GetValueExt(row, this._SegmentParameters[index]);
        if (objArray[index] is PXFieldState)
          objArray[index] = ((PXFieldState) objArray[index]).Value;
      }
    }
    return (IEnumerable) new PXView(sender.Graph, true, (BqlCommand) new Select<PXDimensionAttribute.SegmentValue>(), this._SegmentDelegate).SelectMultiBound(new object[1]
    {
      row
    }, objArray);
  }

  /// <summary>Gets or sets the value that indicates whether the user can
  /// specify only one of the predefined values as a segment or the user can
  /// input arbitrary values.</summary>
  public virtual bool ValidComboRequired
  {
    get
    {
      if (this._ValidComboRequired.HasValue)
        return this._ValidComboRequired.Value;
      if (this._Definition != null)
        return this._Definition.ValidCombos.Contains(this._Dimension);
      PXDimensionAttribute.Definition slot = PXContext.GetSlot<PXDimensionAttribute.Definition>();
      if (slot == null)
        PXContext.SetSlot<PXDimensionAttribute.Definition>(slot = PXDimensionAttribute.PXDatabaseGetSlot());
      return slot == null || slot.Dimensions.Count == 0 || slot.ValidCombos.Contains(this._Dimension);
    }
    set => this._ValidComboRequired = new bool?(value);
  }

  /// <summary>Gets or sets the one-character-long string that is
  /// treated as a wildcard (that is, a character that matches any symbols).
  /// Typically, the property is set when the field to which the attribute
  /// is attached is used for filtering. See also the <see cref="T:PX.Data.PXDimensionWildcardAttribute">PXDimensionWildcard</see>
  /// attribute.</summary>
  public virtual string Wildcard
  {
    get => this._Wildcard;
    set => this._Wildcard = value;
  }

  /// <exclude />
  public static int GetLength(string dimensionID)
  {
    PXDimensionAttribute.Definition slot = PXDimensionAttribute.PXDatabaseGetSlot();
    PXSegment[] pxSegmentArray;
    if (slot == null || !slot.Dimensions.TryGetValue(dimensionID, out pxSegmentArray))
      return 0;
    int length = 0;
    for (int index = 0; index < pxSegmentArray.Length; ++index)
      length += (int) pxSegmentArray[index].Length;
    return length;
  }

  /// <exclude />
  public static int GetMaxLength(string dimensionID)
  {
    int maxLength = -1;
    if (!string.IsNullOrEmpty(dimensionID) && !PXDimensionAttribute._DimensionMaxLength.TryGetValue(dimensionID, out maxLength))
      maxLength = -1;
    return maxLength;
  }

  public static Dictionary<string, KeyValuePair<System.Type, string>> GetTables(string dimensionID)
  {
    Dictionary<string, KeyValuePair<System.Type, string>> tables;
    PXDimensionAttribute._DimensionTables.TryGetValue(dimensionID, out tables);
    return tables;
  }

  /// <summary>
  /// A BQL select that defines the data set that is available
  /// for selectint parent records for lookup mode "By Segments: Child Segment Values".
  /// </summary>
  public System.Type ParentSelect
  {
    get => this._parentSelect;
    set
    {
      if (value != (System.Type) null && (this._Definition == null || !this._Definition.LookupModes.ContainsKey(this._Dimension) || this._Definition.LookupModes[this._Dimension] == DimensionLookupMode.BySegmentsAndChildSegmentValues) && !typeof (IBqlSelect).IsAssignableFrom(value))
        throw new PXArgumentException(nameof (ParentSelect), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
        {
          (object) value
        });
      this._parentSelect = value;
    }
  }

  /// <summary>
  /// Field that is used for "Child Segment Values" lookup mode to retrieve segment's value from a parent record.
  /// </summary>
  public System.Type ParentValueField
  {
    get => this._ParentValueField;
    set
    {
      this._ParentValueField = !(value != (System.Type) null) || typeof (IBqlField).IsAssignableFrom(value) && value.IsNested ? value : throw new PXArgumentException(nameof (ParentValueField), "The type {0} must inherit the PX.Data.IBqlField interface.");
    }
  }

  /// <exclude />
  public DimensionLookupMode? DimensionLookupModeState
  {
    get
    {
      return this._Definition == null || !this._Definition.LookupModes.ContainsKey(this._Dimension) ? new DimensionLookupMode?() : new DimensionLookupMode?(this._Definition.LookupModes[this._Dimension]);
    }
  }

  /// <summary>
  /// Gets or sets the value that indicates whether the new value can be automatically generated when create new record or save it.
  /// The default value is <see langword="true" />.
  /// Typically, the property is setting to <see langword="false" /> for non-persistent(virtual) records.
  /// </summary>
  public bool AutoNumbering { get; set; } = true;

  /// <summary>
  /// Creates an instance to work with the provided segmented key.
  /// </summary>
  /// <param name="dimension">The string identifier of the segmented key.</param>
  public PXDimensionAttribute(string dimension)
  {
    this._Dimension = dimension != null ? dimension : throw new PXArgumentException(nameof (dimension), "The argument cannot be null.");
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    string fieldName = this._FieldName;
    PXSegment[] segments = this._Definition == null || !this._Definition.Dimensions.ContainsKey(this._Dimension) ? new PXSegment[0] : this._Definition.Dimensions[this._Dimension];
    string segmentViewName;
    if (e.ReturnState is PXFieldState && !string.IsNullOrEmpty(((PXFieldState) e.ReturnState).ViewName))
      segmentViewName = (string) null;
    else if (this._Definition.LookupModes[this._Dimension] != DimensionLookupMode.BySegmentsAndChildSegmentValues)
      segmentViewName = $"_{this._Dimension}_Segments_";
    else
      segmentViewName = $"{sender.GetItemType().Name}_{this._FieldName}_{this._Dimension}_Segments_";
    DimensionLookupMode? lookupMode = new DimensionLookupMode?(this._Definition == null || !this._Definition.LookupModes.ContainsKey(this._Dimension) ? DimensionLookupMode.BySegmentedKeys : this._Definition.LookupModes[this._Dimension]);
    bool? validCombos = new bool?(this.ValidComboRequired);
    string wildcard = this._Wildcard;
    PXFieldState instance = PXSegmentedState.CreateInstance(returnState, fieldName, segments, segmentViewName, lookupMode, validCombos, wildcard);
    selectingEventArgs.ReturnState = (object) instance;
    ((PXStringState) e.ReturnState).IsUnicode = true;
    ((PXFieldState) e.ReturnState).DescriptionName = typeof (PXDimensionAttribute.SegmentValue.descr).Name;
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this._Definition.Dimensions.ContainsKey(this._Dimension))
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Dimension '{0}' does not exist in the system.", (object) this._Dimension));
    PXSegment[] dimension = this._Definition.Dimensions[this._Dimension];
    if (!(e.NewValue is string newValue) || newValue.StartsWith(int.MinValue.ToString().Substring(0, 5)))
      return;
    int num1 = 0;
    List<string> stringList1 = new List<string>();
    for (int index1 = 0; index1 < dimension.Length; ++index1)
    {
      if (((PXDimensionAttribute.SegDescr) dimension[index1]).AutoNumber)
      {
        if (sender.Locate(e.Row) == null && sender.GetValue(e.Row, this._FieldOrdinal) is string str1 && num1 < str1.Length && num1 < newValue.Length)
        {
          string str = num1 + (int) dimension[index1].Length > str1.Length ? str1.Substring(num1) : str1.Substring(num1, (int) dimension[index1].Length);
          e.NewValue = num1 + (int) dimension[index1].Length > newValue.Length ? (object) (newValue.Substring(0, num1) + str) : (object) (newValue.Substring(0, num1) + str + newValue.Substring(num1 + (int) dimension[index1].Length));
        }
      }
      else if (dimension[index1].Validate)
      {
        string str2 = num1 >= newValue.Length ? string.Empty : (num1 + (int) dimension[index1].Length > newValue.Length ? newValue.Substring(num1) : newValue.Substring(num1, (int) dimension[index1].Length));
        Dictionary<string, PXDimensionAttribute.ValueDescr> dictionary = this._Definition.Values[this._Dimension][(int) ((PXDimensionAttribute.SegDescr) dimension[index1]).SegmentID];
        short segmentId;
        if (!dictionary.ContainsKey(str2) && !dictionary.ContainsKey(str2 = str2.TrimEnd()))
        {
          if (!string.IsNullOrEmpty(this._Wildcard))
          {
            bool flag1 = false;
            for (int index2 = 0; index2 < this._Wildcard.Length; ++index2)
            {
              if (str2 == new string(this._Wildcard[index2], (int) dimension[index1].Length))
              {
                flag1 = true;
                break;
              }
            }
            if (!flag1)
            {
              bool flag2 = true;
              foreach (string key in dictionary.Keys)
              {
                flag2 = true;
                for (int index3 = 0; index3 < str2.Length; ++index3)
                {
                  bool flag3 = false;
                  for (int index4 = 0; index4 < this._Wildcard.Length; ++index4)
                  {
                    if ((int) str2[index3] == (int) this._Wildcard[index4])
                    {
                      flag3 = true;
                      break;
                    }
                  }
                  if (!flag3 && (index3 >= key.Length || (int) str2[index3] != (int) key[index3]))
                  {
                    flag2 = false;
                    break;
                  }
                }
                if (flag2)
                  break;
              }
              if (flag2)
                continue;
            }
            else
              continue;
          }
          List<string> stringList2 = stringList1;
          string descr;
          if (!string.IsNullOrEmpty(dimension[index1].Descr))
          {
            descr = dimension[index1].Descr;
          }
          else
          {
            segmentId = ((PXDimensionAttribute.SegDescr) dimension[index1]).SegmentID;
            descr = segmentId.ToString();
          }
          stringList2.Add(descr);
        }
        else
        {
          bool flag = true;
          if (this.Restrictions != null)
          {
            byte[] groupMask = dictionary[str2].GroupMask;
            if (groupMask != null)
            {
              for (int index5 = 0; index5 < this.Restrictions.Length; ++index5)
              {
                int num2 = 0;
                for (int index6 = 0; index6 < this.Restrictions[index5].Length; ++index6)
                {
                  int num3 = 0;
                  for (int index7 = index6 * 4; index7 < index6 * 4 + 4; ++index7)
                  {
                    num3 <<= 8;
                    if (index7 < groupMask.Length)
                      num3 |= (int) groupMask[index7];
                  }
                  if ((num3 & this.Restrictions[index5][index6].First) != 0)
                    flag = false;
                  if ((num3 & this.Restrictions[index5][index6].Second) == 0)
                    ++num2;
                }
                if (!flag)
                {
                  if (num2 < this.Restrictions[index5].Length)
                    flag = true;
                  else
                    break;
                }
              }
            }
          }
          if (flag && (object) this._SegmentDelegate != null)
            flag = this.FindValueBySegmentDelegate(sender, e.Row, dimension[index1].Descr, ((PXDimensionAttribute.SegDescr) dimension[index1]).SegmentID, newValue, str2);
          if (!flag)
          {
            List<string> stringList3 = stringList1;
            string descr;
            if (!string.IsNullOrEmpty(dimension[index1].Descr))
            {
              descr = dimension[index1].Descr;
            }
            else
            {
              segmentId = ((PXDimensionAttribute.SegDescr) dimension[index1]).SegmentID;
              descr = segmentId.ToString();
            }
            stringList3.Add(descr);
          }
        }
      }
      num1 += (int) dimension[index1].Length;
    }
    if (stringList1.Count <= 0)
      return;
    if (stringList1.Count == 1)
      throw new PXSetPropertyException("'{0}' of '{1}' does not exist in the system.", new object[2]
      {
        (object) stringList1[0],
        (object) $"[{this._FieldName}]"
      });
    stringList1.Add($"[{this._FieldName}]");
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num4;
    for (num4 = 0; num4 < stringList1.Count - 1; ++num4)
    {
      stringBuilder1.Append('{');
      stringBuilder1.Append(num4);
      stringBuilder1.Append('}');
      if (num4 < stringList1.Count - 2)
        stringBuilder1.Append(", ");
    }
    stringBuilder2.Append('{');
    stringBuilder2.Append(num4);
    stringBuilder2.Append('}');
    throw new PXSetPropertyException(string.Format(PXMessages.LocalizeFormat("{0} of {1} do not exist in the system.", (object) stringBuilder1.ToString(), (object) stringBuilder2.ToString()), (object[]) stringList1.ToArray()));
  }

  /// <exclude />
  protected virtual bool FindValueBySegmentDelegate(
    PXCache sender,
    object row,
    string segmentDescr,
    short segmentID,
    string val,
    string currentValue)
  {
    foreach (PXDimensionAttribute.SegmentValue outerSegment in this.getOuterSegments(sender, segmentID, val, row))
    {
      if (string.Equals(outerSegment.Value, currentValue, StringComparison.OrdinalIgnoreCase))
        return true;
    }
    return false;
  }

  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null || !this.AutoNumbering || !this._Definition.Autonumbers.TryGetValue(this._Dimension, out string _) && this.customNumbering == null && ((object) this._SegmentDelegate == null || e.Row == null))
      return;
    e.NewValue = (object) this.prepValue("", "");
    if ((object) this._SegmentDelegate == null || !(e.NewValue is string))
      return;
    int startIndex = 0;
    for (int index = 0; index < this._Definition.Dimensions[this._Dimension].Length; ++index)
    {
      PXSegment pxSegment = this._Definition.Dimensions[this._Dimension][index];
      if (pxSegment.Validate)
      {
        string str = (string) null;
        foreach (PXDimensionAttribute.SegmentValue outerSegment in this.getOuterSegments(sender, ((PXDimensionAttribute.SegDescr) pxSegment).SegmentID, (string) e.NewValue, e.Row))
        {
          if (str != null)
          {
            str = (string) null;
            break;
          }
          str = outerSegment.Value;
        }
        if (str != null)
        {
          if (str.Length < (int) pxSegment.Length)
            str += new string(' ', (int) pxSegment.Length - str.Length);
          else if (str.Length > (int) pxSegment.Length)
            str = str.Substring(0, (int) pxSegment.Length);
          ((string) e.NewValue).Remove(startIndex, (int) pxSegment.Length).Insert(startIndex, str);
        }
      }
    }
  }

  /// <summary>
  /// Validates if passed value correlates with current segment key rules
  /// </summary>
  /// <typeparam name="TField">Field with segmented key</typeparam>
  /// <param name="sender">Cache</param>
  /// <param name="value">Tested value</param>
  /// <returns></returns>
  public static bool MatchMask<TField>(PXCache sender, string value)
  {
    return !(sender.GetAttributesReadonly(typeof (TField).Name).FirstOrDefault<PXEventSubscriberAttribute>((System.Func<PXEventSubscriberAttribute, bool>) (x => x.GetType().Name == nameof (PXDimensionAttribute))) is PXDimensionAttribute dimensionAttribute) || dimensionAttribute.MatchMask(sender, value);
  }

  public virtual bool MatchMask(PXCache sender, string value)
  {
    if (value == null)
      return false;
    value = value.TrimEnd();
    if (value.Length == 0)
      return false;
    PXSegment[] dimension = this._Definition.Dimensions[this._Dimension];
    int startIndex = 0;
    for (int index = 0; index < dimension.Length && value.Length > startIndex; ++index)
    {
      string source = value.Substring(startIndex, value.Length < startIndex + (int) dimension[index].Length ? value.Length - startIndex : (int) dimension[index].Length);
      startIndex += (int) dimension[index].Length;
      bool flag = index == dimension.Length - 1;
      switch (dimension[index].EditMask)
      {
        case '9':
          if (source.Any<char>((System.Func<char, bool>) (x => !char.IsDigit(x))))
            return false;
          break;
        case '?':
          if (source.Any<char>((System.Func<char, bool>) (x => !char.IsLetter(x))))
            return false;
          break;
        case 'a':
          if (source.Any<char>((System.Func<char, bool>) (x => !char.IsLetterOrDigit(x))))
            return false;
          break;
      }
      if (flag && value.Length > startIndex)
        return false;
    }
    return true;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!this._Definition.Dimensions.ContainsKey(this._Dimension))
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Dimension '{0}' does not exist in the system.", (object) this._Dimension));
    PXSegment[] dimension = this._Definition.Dimensions[this._Dimension];
    if (!(e.NewValue is string newValue))
      return;
    int num = 0;
    bool flag = false;
    for (int index = 0; index < dimension.Length; ++index)
    {
      flag = flag || dimension[index].Align == (short) 0 && num < newValue.Length && (int) newValue[num] == (int) dimension[index].FillCharacter;
      num += (int) dimension[index].Length;
      if (index == dimension.Length - 1 && num > newValue.Length)
        e.NewValue = (object) (newValue + new string(dimension[index].FillCharacter, num - newValue.Length));
    }
    if (flag)
    {
      char[] charArray = ((string) e.NewValue).ToCharArray();
      int index1 = 0;
      for (int index2 = 0; index2 < dimension.Length; ++index2)
      {
        if (dimension[index2].Align == (short) 0 && (int) charArray[index1] == (int) dimension[index2].FillCharacter)
        {
          int index3 = index1 + 1;
          while (index3 < index1 + (int) dimension[index2].Length && (int) charArray[index3] == (int) dimension[index2].FillCharacter)
            ++index3;
          if (index3 < index1 + (int) dimension[index2].Length)
          {
            for (int index4 = 0; index4 < (int) dimension[index2].Length - index3 + index1; ++index4)
            {
              charArray[index1 + index4] = charArray[index3 + index4];
              charArray[index3 + index4] = dimension[index2].FillCharacter;
            }
          }
        }
        index1 += (int) dimension[index2].Length;
      }
      e.NewValue = (object) new string(charArray);
    }
    if (((string) e.NewValue).Length > num)
      e.NewValue = (object) ((string) e.NewValue).Substring(0, num);
    e.Cancel = true;
  }

  /// <exclude />
  public virtual void SelfRowSelecting(
    PXCache sender,
    PXRowSelectingEventArgs e,
    PXSegment[] segs,
    int length)
  {
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    if (!(obj is string))
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) PXDimensionAttribute.AdjustValueLength((string) obj, length));
  }

  public static string AdjustValueLength(string value, int length)
  {
    if (value == null)
      return (string) null;
    if (value.Length < length)
      return value + new string(' ', length - value.Length);
    return value.Length > length ? value.Substring(0, length) : value;
  }

  private string prepValue(string value, string symbol)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int startIndex = 0;
    for (int index = 0; index < this._Definition.Dimensions[this._Dimension].Length; ++index)
    {
      PXSegment pxSegment = this._Definition.Dimensions[this._Dimension][index];
      if (((PXDimensionAttribute.SegDescr) pxSegment).AutoNumber || this.customNumberingSegment.HasValue)
      {
        int segmentId = (int) ((PXDimensionAttribute.SegDescr) pxSegment).SegmentID;
        int? numberingSegment = this.customNumberingSegment;
        int valueOrDefault = numberingSegment.GetValueOrDefault();
        if (segmentId == valueOrDefault & numberingSegment.HasValue || !this.customNumberingSegment.HasValue)
        {
          if (symbol == "")
          {
            if (this.customNumberingSymbol != null)
            {
              symbol = this.customNumberingSymbol;
            }
            else
            {
              using (Dictionary<string, PXDimensionAttribute.ValueDescr>.KeyCollection.Enumerator enumerator = this._Definition.Values[this._Dimension][(int) ((PXDimensionAttribute.SegDescr) pxSegment).SegmentID].Keys.GetEnumerator())
              {
                if (enumerator.MoveNext())
                  symbol = enumerator.Current;
              }
            }
          }
          if (symbol.Length == (int) pxSegment.Length)
          {
            stringBuilder.Append(symbol);
            goto label_24;
          }
          if (symbol.Length > (int) pxSegment.Length)
          {
            stringBuilder.Append(symbol.Substring(0, (int) pxSegment.Length));
            goto label_24;
          }
          if (pxSegment.Align == (short) 0)
          {
            stringBuilder.Append(symbol);
            stringBuilder.Append(pxSegment.FillCharacter, (int) pxSegment.Length - symbol.Length);
            goto label_24;
          }
          stringBuilder.Append(pxSegment.FillCharacter, (int) pxSegment.Length - symbol.Length);
          stringBuilder.Append(symbol);
          goto label_24;
        }
      }
      if (startIndex + (int) pxSegment.Length == value.Length)
        stringBuilder.Append(value.Substring(startIndex));
      else if (startIndex + (int) pxSegment.Length < value.Length)
        stringBuilder.Append(value.Substring(startIndex, (int) pxSegment.Length));
      else if (startIndex < value.Length)
      {
        stringBuilder.Append(value.Substring(startIndex));
        stringBuilder.Append(pxSegment.FillCharacter, startIndex + (int) pxSegment.Length - value.Length);
      }
      else
        stringBuilder.Append(pxSegment.FillCharacter, (int) pxSegment.Length);
label_24:
      startIndex += (int) pxSegment.Length;
    }
    return stringBuilder.ToString();
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
      return;
    if (sender.GetValue(e.Row, this._FieldOrdinal) is string str1 && str1.Trim() == "" && sender.Keys.Contains(this._FieldName))
    {
      if (sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) $"[{this._FieldName}]"))))
        throw new PXRowPersistingException(this._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) this._FieldName
        });
    }
    else
    {
      if ((this.Numbering = this.customNumbering) == null && (!this.AutoNumbering || !this._Definition.Autonumbers.TryGetValue(this._Dimension, out this.Numbering)))
        return;
      this.NewNumber = this.GetNextNumber(sender, e.Row);
      if (!(sender.GetValue(e.Row, this._FieldOrdinal) is string str))
        str = "";
      sender.SetValue(e.Row, this._FieldOrdinal, (object) this.prepValue(str, this.NewNumber));
    }
  }

  protected static bool IS_SEPARATE_SCOPE => WebConfig.EnableAutoNumberingInSeparateConnection;

  protected string GetNextNumber(PXCache sender, object row)
  {
    if (!PXDimensionAttribute.IS_SEPARATE_SCOPE)
      return this.GetNextNumberInt(sender, row);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        string nextNumberInt = this.GetNextNumberInt(sender, row);
        transactionScope.Complete();
        return nextNumberInt;
      }
    }
  }

  protected string GetNextNumberInt(PXCache sender, object row)
  {
    string strB1;
    string strB2;
    int count;
    int? int32;
    string str1;
    System.DateTime? dateTime1;
    Guid? guid1;
    string str2;
    System.DateTime? dateTime2;
    Guid? guid2;
    string str3;
    System.DateTime? dateTime3;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PXDimensionAttribute.NumberingSequence>(new PXDataField("EndNbr"), new PXDataField(new Column("LastNbr").Coalesce((SQLExpression) new Column("StartNbr"))), new PXDataField("WarnNbr"), new PXDataField("NbrStep"), new PXDataField("NumberingSEQ"), new PXDataField("NBranchID"), new PXDataField("StartNbr"), new PXDataField("StartDate"), new PXDataField("CreatedByID"), new PXDataField("CreatedByScreenID"), new PXDataField("CreatedDateTime"), new PXDataField("LastModifiedByID"), new PXDataField("LastModifiedByScreenID"), new PXDataField("LastModifiedDateTime"), (PXDataField) new PXDataFieldValue("NumberingID", PXDbType.VarChar, new int?(10), (object) this.Numbering), (PXDataField) new PXDataFieldValue("StartDate", PXDbType.DateTime, new int?(4), (object) sender.Graph.Accessinfo.BusinessDate, PXComp.LE), (PXDataField) new PXDataFieldValue("NBranchID", PXDbType.Int, new int?(4), (object) sender.Graph.Accessinfo.BranchID, PXComp.EQorISNULL), (PXDataField) new PXDataFieldOrder("NBranchID", true), (PXDataField) new PXDataFieldOrder("StartDate", true)))
    {
      strB1 = pxDataRecord != null ? pxDataRecord.GetString(0) : throw new PXException("An identifier could not be assigned automatically because no applicable subsequence was found for the current branch and business date in the {0} numbering sequence.", new object[1]
      {
        (object) this.Numbering
      });
      this.LastNbr = pxDataRecord.GetString(1);
      strB2 = pxDataRecord.GetString(2);
      count = pxDataRecord.GetInt32(3).Value;
      this.NumberingSEQ = pxDataRecord.GetInt32(4).Value;
      int32 = pxDataRecord.GetInt32(5);
      str1 = pxDataRecord.GetString(6);
      dateTime1 = pxDataRecord.GetDateTime(7);
      guid1 = pxDataRecord.GetGuid(8);
      str2 = pxDataRecord.GetString(9);
      dateTime2 = pxDataRecord.GetDateTime(10);
      guid2 = pxDataRecord.GetGuid(11);
      str3 = pxDataRecord.GetString(12);
      dateTime3 = pxDataRecord.GetDateTime(13);
    }
    this.NewNumber = PXDimensionAttribute.nextNumber(this.LastNbr, count);
    if (this.NewNumber.CompareTo(strB2) >= 0)
      PXUIFieldAttribute.SetWarning(sender, row, this._FieldName, "The warning number has been reached.");
    if (this.NewNumber.CompareTo(strB1) >= 0)
      throw new PXException("The end of numbering has been reached.");
    try
    {
      PXDatabase.Update<PXDimensionAttribute.NumberingSequence>((PXDataFieldParam) new PXDataFieldAssign("LastNbr", (object) this.NewNumber), (PXDataFieldParam) new PXDataFieldRestrict("NumberingID", (object) this.Numbering), (PXDataFieldParam) new PXDataFieldRestrict("NumberingSEQ", (object) this.NumberingSEQ), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      PXDatabase.Insert<PXDimensionAttribute.NumberingSequence>(new PXDataFieldAssign("EndNbr", PXDbType.VarChar, new int?(15), (object) strB1), new PXDataFieldAssign("LastNbr", PXDbType.VarChar, new int?(15), (object) this.NewNumber), new PXDataFieldAssign("WarnNbr", PXDbType.VarChar, new int?(15), (object) strB2), new PXDataFieldAssign("NbrStep", PXDbType.Int, new int?(4), (object) count), new PXDataFieldAssign("StartNbr", PXDbType.VarChar, new int?(15), (object) str1), new PXDataFieldAssign("StartDate", PXDbType.DateTime, (object) dateTime1), new PXDataFieldAssign("CreatedByID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) guid1), new PXDataFieldAssign("CreatedByScreenID", PXDbType.Char, new int?(8), (object) str2), new PXDataFieldAssign("CreatedDateTime", PXDbType.DateTime, new int?(8), (object) dateTime2), new PXDataFieldAssign("LastModifiedByID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) guid2), new PXDataFieldAssign("LastModifiedByScreenID", PXDbType.Char, new int?(8), (object) str3), new PXDataFieldAssign("LastModifiedDateTime", PXDbType.DateTime, new int?(8), (object) dateTime3), new PXDataFieldAssign("NumberingID", PXDbType.VarChar, new int?(10), (object) this.Numbering), new PXDataFieldAssign("NBranchID", PXDbType.Int, new int?(4), (object) int32));
    }
    return this.NewNumber;
  }

  private static string nextNumber(string str, int count)
  {
    bool flag = true;
    int num = count;
    StringBuilder stringBuilder = new StringBuilder();
    for (int length = str.Length; length > 0; --length)
    {
      string input = str.Substring(length - 1, 1);
      if (Regex.IsMatch(input, "[^0-9]"))
        flag = false;
      if (flag && Regex.IsMatch(input, "[0-9]"))
      {
        int int16_1 = (int) Convert.ToInt16(input);
        string str1 = Convert.ToString(num);
        int int16_2 = (int) Convert.ToInt16(str1.Substring(str1.Length - 1, 1));
        stringBuilder.Append((int16_1 + int16_2) % 10);
        num = (num - int16_2 + (int16_1 + int16_2 - (int16_1 + int16_2) % 10)) / 10;
        if (num == 0)
          flag = false;
      }
      else
        stringBuilder.Append(input);
    }
    if (num != 0)
      throw new ArithmeticException("");
    char[] charArray = stringBuilder.ToString().ToCharArray();
    Array.Reverse((Array) charArray);
    return new string(charArray);
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Aborted)
      return;
    switch (sender.GetValue(e.Row, this._FieldOrdinal))
    {
      case string a:
      case null:
        if (a == null)
          a = "";
        try
        {
          sender.SetValue(e.Row, this._FieldOrdinal, (object) this.prepValue(a, ""));
        }
        catch (InvalidCastException ex)
        {
        }
        if (!(e.Exception is PXLockViolationException) || string.IsNullOrEmpty(a) || string.IsNullOrEmpty(this.NewNumber))
          break;
        if (!string.Equals(a, this.prepValue(a, this.NewNumber)))
          break;
        try
        {
          PXDatabase.Update<PXDimensionAttribute.NumberingSequence>((PXDataFieldParam) new PXDataFieldAssign("LastNbr", (object) this.NewNumber), (PXDataFieldParam) new PXDataFieldRestrict("NumberingID", (object) this.Numbering), (PXDataFieldParam) new PXDataFieldRestrict("NumberingSEQ", (object) this.NumberingSEQ), (PXDataFieldParam) new PXDataFieldRestrict("LastNbr", (object) this.LastNbr));
          ((PXLockViolationException) e.Exception).Retry = true;
          break;
        }
        catch
        {
          break;
        }
    }
  }

  public static void SuppressAutoNumbering<Field>(PXCache cache, bool suppress) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXDimensionAttribute dimensionAttribute in cache.GetAttributes<Field>().OfType<PXDimensionAttribute>())
      dimensionAttribute.AutoNumbering = !suppress;
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    lock (((ICollection) PXDimensionAttribute._DimensionMaxLength).SyncRoot)
    {
      string key = $"{bqlTable.Name}__{this.FieldName}";
      TableColumn tableColumn = (TableColumn) null;
      if (!PXDimensionAttribute._DimensionColumns.TryGetValue(key, out tableColumn))
      {
        try
        {
          TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(this._BqlTable.Name);
          if (tableStructure != null)
            tableColumn = PXDimensionAttribute._DimensionColumns[key] = tableStructure.Columns.FirstOrDefault<TableColumn>((System.Func<TableColumn, bool>) (f => ((TableEntityBase) f).Name.OrdinalEquals(this.FieldName)));
        }
        catch
        {
        }
      }
      if (tableColumn == null || tableColumn.Type != SqlDbType.NVarChar && tableColumn.Type != SqlDbType.VarChar)
        return;
      if (!PXDimensionAttribute._DimensionMaxLength.ContainsKey(this._Dimension))
        PXDimensionAttribute._DimensionMaxLength.Add(this._Dimension, tableColumn.Size);
      else if (PXDimensionAttribute._DimensionMaxLength[this._Dimension] > tableColumn.Size)
        PXDimensionAttribute._DimensionMaxLength[this._Dimension] = tableColumn.Size;
      KeyValuePair<System.Type, string> keyValuePair = new KeyValuePair<System.Type, string>(this._BqlTable, this._FieldName);
      Dictionary<string, KeyValuePair<System.Type, string>> dictionary;
      if (!PXDimensionAttribute._DimensionTables.TryGetValue(this._Dimension, out dictionary))
      {
        PXDimensionAttribute._DimensionTables.Add(this._Dimension, new Dictionary<string, KeyValuePair<System.Type, string>>()
        {
          {
            this._BqlTable.Name,
            keyValuePair
          }
        });
      }
      else
      {
        if (dictionary.ContainsKey(this._BqlTable.Name))
          return;
        dictionary.Add(this._BqlTable.Name, keyValuePair);
      }
    }
  }

  internal static System.Type[] UsedTables => PXDimensionAttribute._usedTables;

  public static string[] GetSegmentValues(string dimensionid, int segmentnumber)
  {
    PXDimensionAttribute.Definition slot = PXDimensionAttribute.PXDatabaseGetSlot();
    Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray;
    if (string.IsNullOrEmpty(dimensionid) || segmentnumber < 0 || !slot.Values.TryGetValue(dimensionid, out dictionaryArray) || dictionaryArray.Length <= segmentnumber)
      return new string[0];
    string[] array = new string[dictionaryArray[segmentnumber].Keys.Count];
    dictionaryArray[segmentnumber].Keys.CopyTo(array, 0);
    return array;
  }

  public static void Clear()
  {
    PXDatabase.ResetSlot<PXDimensionAttribute.Definition>("Definition", typeof (PXDimensionAttribute.Dimension), typeof (PXDimensionAttribute.Segment), typeof (PXDimensionAttribute.SegmentValue));
    PXContext.ClearSlot<PXDimensionAttribute.Definition>();
  }

  private static PXDimensionAttribute.Definition PXDatabaseGetSlot()
  {
    try
    {
      return PXDatabase.GetSlot<PXDimensionAttribute.Definition>("Definition", typeof (PXDimensionAttribute.Dimension), typeof (PXDimensionAttribute.Segment), typeof (PXDimensionAttribute.SegmentValue));
    }
    catch
    {
      if (!PXDatabase.Provider.SchemaCache.TableExists("Dimension"))
        return (PXDimensionAttribute.Definition) null;
      throw;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._Definition = PXDimensionAttribute.GetDefinition(this._Dimension);
    bool flag = false;
    if (this._Definition != null)
    {
      PXSegment[] segs = this._Definition.Dimensions[this._Dimension];
      int total = 0;
      for (int index = 0; index < segs.Length; ++index)
      {
        this.AddFieldForSegment(sender, index + 1, segs[index], total);
        total += (int) segs[index].Length;
      }
      sender.RowSelectingWhileReading += (PXRowSelecting) ((cache, e) => this.SelfRowSelecting(cache, e, segs, total));
      if (this._Definition.LookupModes[this._Dimension] == DimensionLookupMode.BySegmentsAndChildSegmentValues)
      {
        BqlCommand command = !(this.ParentSelect == (System.Type) null) && !(this.ParentValueField == (System.Type) null) ? BqlCommand.CreateInstance(this.ParentSelect).WhereAnd(BqlCommand.MakeGenericType(typeof (Where<,>), this.ParentValueField, typeof (Like<>), typeof (Required<>), this.ParentValueField)).AggregateNew(BqlCommand.MakeGenericType(typeof (Aggregate<>), typeof (GroupBy<,>), typeof (Substring<,,>), this.ParentValueField, typeof (Required<PXDimensionAttribute.ParentSelectHelper.intValue>), typeof (Required<PXDimensionAttribute.ParentSelectHelper.intValue>), typeof (Min<>), this.ParentValueField)) : throw new PXInvalidOperationException();
        this._ParentView = sender.Graph.TypedViews.GetView(command, false);
        flag = true;
        this.SetSegmentDelegate((Delegate) new PXSelectDelegate<short?, string>(this.SegmentSelectParents));
      }
    }
    string key1 = $"_{this._Dimension}_Segments_";
    if (!sender.Graph.Views.ContainsKey(key1))
      sender.Graph.Views[key1] = new PXView(sender.Graph, true, (BqlCommand) new Select<PXDimensionAttribute.SegmentValue>(), this.GetSegmentDelegate());
    if (!flag)
      return;
    string key2 = $"{sender.GetItemType().Name}_{this._FieldName}_{this._Dimension}_Segments_";
    if (sender.Graph.Views.ContainsKey(key2))
      return;
    sender.Graph.Views[key2] = new PXView(sender.Graph, true, (BqlCommand) new Select<PXDimensionAttribute.SegmentValue>(), this.GetSegmentDelegate());
  }

  protected static PXDimensionAttribute.Definition GetDefinition(string dimension)
  {
    PXDimensionAttribute.Definition definition = PXContext.GetSlot<PXDimensionAttribute.Definition>();
    if (definition == null)
      PXContext.SetSlot<PXDimensionAttribute.Definition>(definition = PXDimensionAttribute.PXDatabaseGetSlot());
    if (definition != null && !definition.Dimensions.ContainsKey(dimension))
    {
      if (definition.Dimensions.Count == 0)
      {
        definition = new PXDimensionAttribute.Definition();
        try
        {
          foreach (string dimension1 in ServiceLocator.Current.GetInstance<PXDimensionAttribute.IDimensionSource>().Dimensions())
          {
            definition.Dimensions[dimension1] = new PXSegment[1]
            {
              (PXSegment) new PXDimensionAttribute.SegDescr(dimension1, new short?((short) 1), 'C', ' ', new short?((short) 30), new bool?(false), new short?((short) 1), new short?((short) 0), '-', new bool?(false), "1st segment", (string) null)
            };
            definition.LookupModes[dimension1] = DimensionLookupMode.BySegmentedKeys;
          }
        }
        catch
        {
        }
      }
      if (!definition.Dimensions.ContainsKey(dimension))
        throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", (object) "Segmented Key", (object) dimension));
    }
    return definition;
  }

  private void AddFieldForSegment(PXCache cache, int segNumber, PXSegment seg, int startPos)
  {
    string segFieldName = $"{this._FieldName}_Segment{segNumber}";
    if (cache.Fields.Contains(segFieldName))
      return;
    cache.Fields.Add(segFieldName);
    cache.FieldSelectingEvents.Add(segFieldName, (PXFieldSelecting) ((sender, e) =>
    {
      string str1 = sender.GetValue(e.Row, this._FieldName) as string;
      string str2 = string.Empty;
      if (!string.IsNullOrEmpty(str1) && startPos < str1.Length)
      {
        int length = startPos + (int) seg.Length > str1.Length ? str1.Length - startPos : (int) seg.Length;
        str2 = PXDimensionAttribute.AdjustValueLength(str1.Substring(startPos, length), (int) seg.Length);
      }
      PXFieldState instance = PXStringState.CreateInstance((object) str2, new int?((int) seg.Length), new bool?(true), segFieldName, new bool?(false), new int?(0), PXSegmentedState.GetEditMaskForSegment(seg), (string[]) null, (string[]) null, new bool?(), (string) null);
      string displayName = PXUIFieldAttribute.GetDisplayName(sender, this._FieldName);
      instance.DisplayName = string.IsNullOrEmpty(displayName) ? seg.Descr : $"{displayName} ({seg.Descr})";
      instance.Visible = false;
      instance.Enabled = false;
      instance.Visibility = PXUIVisibility.Invisible;
      e.ReturnState = (object) instance;
    }));
    cache.FieldUpdatingEvents.Add(segFieldName, (PXFieldUpdating) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) (sender.GetValue(e.Row, this._FieldName) as string);
    }));
    cache.CommandPreparingEvents.Add(segFieldName, (PXCommandPreparing) ((sender, e) =>
    {
      if (!e.IsSelect() || (e.Operation & PXDBOperation.Option) != PXDBOperation.External)
        return;
      PXCommandPreparingEventArgs.FieldDescription description;
      sender.RaiseCommandPreparing(this.FieldName, (object) null, (object) null, PXDBOperation.External, this.BqlTable, out description);
      e.Expr = description?.Expr?.Substr((uint) (startPos + 1), (uint) seg.Length).NullIf((SQLExpression) new SQLConst((object) string.Empty));
      e.DataValue = e.Value;
    }));
  }

  protected internal Delegate GetSegmentDelegate()
  {
    return (object) this._SegmentDelegate != null ? this._SegmentDelegate : (Delegate) new PXSelectDelegate<short?, string>(this.SegmentSelect);
  }

  /// <exclude />
  public IEnumerable SegmentSelect([PXShort] short? segment, [PXString] string value)
  {
    if (this._Definition.Dimensions.ContainsKey(this._Dimension) && segment.HasValue)
    {
      short? nullable1 = segment;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num1 = 0;
      if (!(nullable2.GetValueOrDefault() < num1 & nullable2.HasValue))
      {
        short? nullable3 = segment;
        nullable2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        int length = this._Definition.Values[this._Dimension].Length;
        if (!(nullable2.GetValueOrDefault() >= length & nullable2.HasValue))
        {
          foreach (KeyValuePair<string, PXDimensionAttribute.ValueDescr> keyValuePair in this._Definition.Values[this._Dimension][(int) segment.Value])
          {
            if (this.Restrictions == null || keyValuePair.Value.GroupMask == null)
            {
              yield return (object) new PXDimensionAttribute.SegmentValue(keyValuePair.Key, keyValuePair.Value.Descr, keyValuePair.Value.IsConsolidatedValue);
            }
            else
            {
              byte[] groupMask = keyValuePair.Value.GroupMask;
              bool flag = true;
              for (int index1 = 0; index1 < this.Restrictions.Length; ++index1)
              {
                int num2 = 0;
                for (int index2 = 0; index2 < this.Restrictions[index1].Length; ++index2)
                {
                  int num3 = 0;
                  for (int index3 = index2 * 4; index3 < index2 * 4 + 4; ++index3)
                  {
                    num3 <<= 8;
                    if (index3 < groupMask.Length)
                      num3 |= (int) groupMask[index3];
                  }
                  if ((num3 & this.Restrictions[index1][index2].First) != 0)
                    flag = false;
                  if ((num3 & this.Restrictions[index1][index2].Second) == 0)
                    ++num2;
                }
                if (!flag)
                {
                  if (num2 < this.Restrictions[index1].Length)
                    flag = true;
                  else
                    break;
                }
              }
              if (flag)
                yield return (object) new PXDimensionAttribute.SegmentValue(keyValuePair.Key, keyValuePair.Value.Descr, keyValuePair.Value.IsConsolidatedValue);
            }
          }
        }
      }
    }
  }

  public virtual IEnumerable SegmentSelectParents([PXShort] short? segment, [PXString] string value)
  {
    if (this._Definition.Dimensions.ContainsKey(this._Dimension) && this._ParentView != null)
    {
      PXSegment[] dimension = this._Definition.Dimensions[this._Dimension];
      if (segment.HasValue)
      {
        short? nullable1 = segment;
        int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num = 0;
        if (!(nullable2.GetValueOrDefault() < num & nullable2.HasValue) && dimension.Length != 0)
        {
          short? nullable3 = segment;
          nullable2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
          int length = dimension.Length;
          if (!(nullable2.GetValueOrDefault() > length & nullable2.HasValue))
          {
            int segIndex = (int) segment.Value - 1;
            if (!string.IsNullOrEmpty(value) || segIndex <= 0)
            {
              PXCache cache = this._ParentView.Cache;
              string stringForParentSelect = this.GetSearchStringForParentSelect(cache.Graph.SqlDialect, dimension, segIndex, value);
              int pos = 0;
              int total = 0;
              for (int index = 0; index < dimension.Length; ++index)
              {
                if (index < segIndex)
                  pos += (int) dimension[index].Length;
                total += (int) dimension[index].Length;
              }
              int segLength = (int) dimension[segIndex].Length;
              object[] parameters = new object[this._ParentView.EnumParameters().Count];
              parameters[parameters.Length - 3] = (object) stringForParentSelect;
              parameters[parameters.Length - 2] = (object) (pos + 1);
              parameters[parameters.Length - 1] = (object) segLength;
              string fieldName = this.ParentValueField.Name;
              string descrFieldName = cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt ? stateExt.DescriptionName : (string) null;
              int startRow = PXView.StartRow;
              int totalRows = 0;
              int maximumRows = PXView.MaximumRows;
              if (PXView.Filters.Length > 0 || PXView.SortColumns.Length != 0)
              {
                startRow = 0;
                maximumRows = 0;
              }
              else
                PXView.StartRow = 0;
              foreach (object data in this._ParentView.Select((object[]) null, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, maximumRows, ref totalRows))
              {
                object obj = PXFieldState.UnwrapValue(cache.GetValueExt(data, fieldName));
                if (obj != null)
                {
                  string str1 = PXDimensionAttribute.AdjustValueLength(obj.ToString(), total);
                  string descr = string.Empty;
                  if (!string.IsNullOrEmpty(descrFieldName))
                    descr = PXSelectorAttribute.GetField(cache, (object) null, fieldName, (object) str1, descrFieldName) as string;
                  string str2 = str1.Substring(pos, segLength);
                  if (string.IsNullOrWhiteSpace(str2))
                    str2 = new string(' ', str2.Length);
                  yield return (object) new PXDimensionAttribute.SegmentValue(str2, descr, new bool?(false));
                }
              }
            }
          }
        }
      }
    }
  }

  protected string GetSearchStringForParentSelect(
    ISqlDialect sqlDialect,
    PXSegment[] segments,
    int segIndex,
    string value)
  {
    if (segIndex < 0 || segIndex >= segments.Length)
      throw new ArgumentOutOfRangeException(nameof (segIndex));
    if (string.IsNullOrEmpty(value))
      return sqlDialect.WildcardAnything;
    int startIndex = 0;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < segments.Length; ++index)
    {
      PXSegment segment = segments[index];
      int num = startIndex + (int) segment.Length;
      if (index >= segIndex)
      {
        stringBuilder.Append(sqlDialect.WildcardAnything);
        break;
      }
      if (num > value.Length)
      {
        if (value.Length < startIndex)
        {
          stringBuilder.Append(sqlDialect.WildcardAnything);
          break;
        }
        stringBuilder.Append(value.Substring(startIndex, value.Length - startIndex)).Append(' ', (int) segment.Length - (value.Length - startIndex));
      }
      else
        stringBuilder.Append(value.Substring(startIndex, (int) segment.Length));
      startIndex = num;
    }
    return stringBuilder.ToString();
  }

  [PXInternalUseOnly]
  public static void SetCustomNumbering<Field>(
    PXCache cache,
    string numberingSeq,
    int? segmentID,
    string newSymbol)
    where Field : IBqlField
  {
    PXDimensionAttribute.SetCustomNumbering(cache, typeof (Field).Name, numberingSeq, segmentID, newSymbol);
  }

  [PXInternalUseOnly]
  public static void SetCustomNumbering(
    PXCache cache,
    string name,
    string numberingSeq,
    int? segmentID,
    string newSymbol)
  {
    cache.SetAltered(name, true);
    foreach (PXDimensionAttribute dimensionAttribute in cache.GetAttributes(name).OfType<PXDimensionAttribute>())
    {
      dimensionAttribute.customNumbering = numberingSeq;
      dimensionAttribute.customNumberingSegment = segmentID;
      dimensionAttribute.customNumberingSymbol = newSymbol;
    }
  }

  public class SegDescr : PXSegment
  {
    public readonly string DimensionID;
    public readonly short SegmentID;
    public readonly bool AutoNumber;
    public readonly string ParentDimensionID;

    public SegDescr(
      string dimensionID,
      short? segmentID,
      char editMask,
      char fillCharacter,
      short? length,
      bool? validate,
      short? caseConvert,
      short? align,
      char separator,
      bool? readOnly,
      string descr,
      string parentDimensionID,
      char promptCharacter = '_')
      : base(editMask, fillCharacter, length.Value, validate.Value, caseConvert.Value, align.Value, separator, false, descr, promptCharacter)
    {
      this.DimensionID = dimensionID;
      this.SegmentID = segmentID.Value;
      this.AutoNumber = readOnly.Value;
      this.ParentDimensionID = parentDimensionID;
    }
  }

  public interface IDimensionSource
  {
    string[] Dimensions();
  }

  private sealed class ParentSelectHelper : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public int? IntValue { get; set; }

    public abstract class intValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXDimensionAttribute.ParentSelectHelper.intValue>
    {
    }
  }

  public sealed class ValueDescr
  {
    public readonly string Descr;
    public readonly bool? IsConsolidatedValue;
    public readonly byte[] GroupMask;

    public ValueDescr(string descr, bool? isConsolidatedValue, byte[] groupMask)
    {
      this.Descr = descr;
      this.IsConsolidatedValue = isConsolidatedValue;
      this.GroupMask = groupMask;
    }
  }

  [Serializable]
  public sealed class SegmentValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private string _Value;
    private string _Descr;
    private bool? _IsConsolidatedValue;

    [PXDBString(30, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Value", Visibility = PXUIVisibility.SelectorVisible)]
    public string Value
    {
      get => this._Value;
      set => this._Value = value;
    }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
    public string Descr
    {
      get => this._Descr;
      set => this._Descr = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Aggregation")]
    public bool? IsConsolidatedValue
    {
      get => this._IsConsolidatedValue;
      set => this._IsConsolidatedValue = value;
    }

    public SegmentValue(string value, string descr, bool? isConsolidatedValue)
    {
      this._Value = value;
      this._Descr = descr;
      this._IsConsolidatedValue = isConsolidatedValue;
    }

    public SegmentValue()
    {
    }

    public abstract class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDimensionAttribute.SegmentValue.value>
    {
    }

    public abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDimensionAttribute.SegmentValue.descr>
    {
    }

    public abstract class isConsolidatedValue : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXDimensionAttribute.SegmentValue.isConsolidatedValue>
    {
    }
  }

  public class Definition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<string, PXSegment[]> Dimensions = new Dictionary<string, PXSegment[]>();
    public Dictionary<string, Dictionary<string, PXDimensionAttribute.ValueDescr>[]> Values = new Dictionary<string, Dictionary<string, PXDimensionAttribute.ValueDescr>[]>();
    public HashSet<string> ValidCombos = new HashSet<string>();
    public Dictionary<string, DimensionLookupMode> LookupModes = new Dictionary<string, DimensionLookupMode>();
    public Dictionary<string, string> Autonumbers = new Dictionary<string, string>();

    public void Prefetch()
    {
      try
      {
        Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
        List<string> stringList = new List<string>();
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXDimensionAttribute.Dimension>(new PXDataField("DimensionID"), new PXDataField("LookupMode"), new PXDataField("Validate"), new PXDataField("NumberingID"), new PXDataField("ParentDimensionID")))
        {
          string key = pxDataRecord.GetString(0);
          string upperInvariant = pxDataRecord.GetString(1)?.ToUpperInvariant();
          DimensionLookupMode dimensionLookupMode;
          switch (upperInvariant)
          {
            case "SA":
              dimensionLookupMode = DimensionLookupMode.BySegmentsAndAllAvailableSegmentValues;
              break;
            case "SC":
              dimensionLookupMode = DimensionLookupMode.BySegmentsAndChildSegmentValues;
              break;
            case "K0":
              dimensionLookupMode = DimensionLookupMode.BySegmentedKeys;
              break;
            default:
              throw new NotSupportedException($"LookupMode {upperInvariant} is not supported.");
          }
          this.LookupModes[key] = dimensionLookupMode;
          bool? boolean = pxDataRecord.GetBoolean(2);
          bool flag = true;
          if (boolean.GetValueOrDefault() == flag & boolean.HasValue)
            this.ValidCombos.Add(key);
          string str1 = pxDataRecord.GetString(3);
          if (!string.IsNullOrEmpty(str1))
            this.Autonumbers.Add(key, str1);
          string str2 = pxDataRecord.GetString(4);
          if (!string.IsNullOrEmpty(str2))
          {
            dictionary1[key] = str2;
            if (!stringList.Contains(str2))
              stringList.Add(str2);
          }
        }
        foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
        {
          string key1 = keyValuePair.Key;
          string key2 = keyValuePair.Value;
          string str;
          if (!this.Autonumbers.ContainsKey(key1) && this.Autonumbers.TryGetValue(key2, out str))
            this.Autonumbers.Add(key1, str);
        }
        Dictionary<string, IList<short>> dictionary2 = new Dictionary<string, IList<short>>(dictionary1.Count);
        foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
          dictionary2.Add(keyValuePair.Key, (IList<short>) new List<short>());
        Dictionary<string, IDictionary<short, PXDimensionAttribute.SegDescr>> dictionary3 = new Dictionary<string, IDictionary<short, PXDimensionAttribute.SegDescr>>(stringList.Count);
        foreach (string key in stringList)
          dictionary3.Add(key, (IDictionary<short, PXDimensionAttribute.SegDescr>) new Dictionary<short, PXDimensionAttribute.SegDescr>());
        List<PXDimensionAttribute.SegDescr> segDescrList = new List<PXDimensionAttribute.SegDescr>();
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXDimensionAttribute.Segment>(new PXDataField("DimensionID"), new PXDataField("SegmentID"), new PXDataField("EditMask"), new PXDataField("FillCharacter"), new PXDataField("Length"), new PXDataField("Validate"), new PXDataField("CaseConvert"), new PXDataField("Align"), new PXDataField("Separator"), new PXDataField("AutoNumber"), new PXDataField("Descr"), new PXDataField("ParentDimensionID"), new PXDataField("PromptCharacter")))
        {
          PXDimensionAttribute.SegDescr segDescr = new PXDimensionAttribute.SegDescr(pxDataRecord.GetString(0), pxDataRecord.GetInt16(1), (pxDataRecord.GetString(2) + " ")[0], ' ', pxDataRecord.GetInt16(4), pxDataRecord.GetBoolean(5), pxDataRecord.GetInt16(6), pxDataRecord.GetInt16(7), (pxDataRecord.GetString(8) + " ")[0], pxDataRecord.GetBoolean(9), pxDataRecord.GetString(10), pxDataRecord.GetString(11), (pxDataRecord.GetString(12) + " ")[0]);
          segDescrList.Add(segDescr);
          if (dictionary1.ContainsKey(segDescr.DimensionID) && segDescr.ParentDimensionID != null)
            dictionary2[segDescr.DimensionID].Add(segDescr.SegmentID);
          if (stringList.Contains(segDescr.DimensionID))
            dictionary3[segDescr.DimensionID].Add(segDescr.SegmentID, segDescr);
        }
        foreach (KeyValuePair<string, string> keyValuePair1 in dictionary1)
        {
          string key3 = keyValuePair1.Key;
          IList<short> shortList = dictionary2[key3];
          string key4 = keyValuePair1.Value;
          foreach (KeyValuePair<short, PXDimensionAttribute.SegDescr> keyValuePair2 in (IEnumerable<KeyValuePair<short, PXDimensionAttribute.SegDescr>>) dictionary3[key4])
          {
            short key5 = keyValuePair2.Key;
            if (!shortList.Contains(key5))
            {
              PXDimensionAttribute.SegDescr segDescr = keyValuePair2.Value;
              segDescrList.Add(new PXDimensionAttribute.SegDescr(key3, new short?(key5), segDescr.EditMask, segDescr.FillCharacter, new short?(segDescr.Length), new bool?(segDescr.Validate), new short?(segDescr.CaseConvert), new short?(segDescr.Align), segDescr.Separator, new bool?(segDescr.AutoNumber), segDescr.Descr, segDescr.ParentDimensionID));
            }
          }
        }
        foreach (string str in new List<string>((IEnumerable<string>) this.Autonumbers.Keys))
        {
          bool flag = false;
          for (int index = 0; index < segDescrList.Count; ++index)
          {
            if (string.Equals(str, segDescrList[index].DimensionID, StringComparison.OrdinalIgnoreCase) && segDescrList[index].AutoNumber)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            this.Autonumbers.Remove(str);
        }
        segDescrList.Sort((Comparison<PXDimensionAttribute.SegDescr>) ((a, b) =>
        {
          int num = string.Compare(a.DimensionID, b.DimensionID, StringComparison.OrdinalIgnoreCase);
          return num != 0 ? num : a.SegmentID.CompareTo(b.SegmentID);
        }));
        int index1 = 0;
        for (int index2 = 0; index2 < segDescrList.Count; ++index2)
        {
          if (segDescrList[index1].DimensionID != segDescrList[index2].DimensionID)
          {
            PXSegment[] pxSegmentArray = new PXSegment[index2 - index1];
            short num = -1;
            for (int index3 = 0; index3 < index2 - index1; ++index3)
            {
              pxSegmentArray[index3] = (PXSegment) segDescrList[index1 + index3];
              if ((int) segDescrList[index1 + index3].SegmentID > (int) num)
                num = segDescrList[index1 + index3].SegmentID;
            }
            this.Dimensions[segDescrList[index1].DimensionID] = pxSegmentArray;
            short length = (short) ((int) num + 1);
            Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray = new Dictionary<string, PXDimensionAttribute.ValueDescr>[(int) length];
            for (int index4 = 0; index4 < (int) length; ++index4)
              dictionaryArray[index4] = new Dictionary<string, PXDimensionAttribute.ValueDescr>();
            this.Values[segDescrList[index1].DimensionID] = dictionaryArray;
            index1 = index2;
          }
        }
        if (index1 < segDescrList.Count)
        {
          PXSegment[] pxSegmentArray = new PXSegment[segDescrList.Count - index1];
          short num = -1;
          for (int index5 = 0; index5 < segDescrList.Count - index1; ++index5)
          {
            pxSegmentArray[index5] = (PXSegment) segDescrList[index1 + index5];
            if ((int) segDescrList[index1 + index5].SegmentID > (int) num)
              num = segDescrList[index1 + index5].SegmentID;
          }
          this.Dimensions[segDescrList[index1].DimensionID] = pxSegmentArray;
          short length = (short) ((int) num + 1);
          Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray = new Dictionary<string, PXDimensionAttribute.ValueDescr>[(int) length];
          for (int index6 = 0; index6 < (int) length; ++index6)
            dictionaryArray[index6] = new Dictionary<string, PXDimensionAttribute.ValueDescr>();
          this.Values[segDescrList[index1].DimensionID] = dictionaryArray;
        }
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXDimensionAttribute.SegmentValue>(new PXDataField("DimensionID"), new PXDataField("SegmentID"), new PXDataField("Value"), new PXDataField("Descr"), new PXDataField("GroupMask"), new PXDataField("IsConsolidatedValue"), (PXDataField) new PXDataFieldValue("Active", PXDbType.Bit, (object) 1)))
        {
          byte[] groupMask = pxDataRecord.GetBytes(4);
          string key = pxDataRecord.GetString(0);
          PXSegment[] pxSegmentArray;
          if (this.Dimensions.TryGetValue(key, out pxSegmentArray))
          {
            int index7 = (int) pxDataRecord.GetInt16(1).Value;
            Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray = this.Values[key];
            if (dictionaryArray != null && index7 < dictionaryArray.Length)
            {
              if (groupMask != null)
              {
                if (index7 > pxSegmentArray.Length || !pxSegmentArray[index7 - 1].Validate)
                {
                  groupMask = (byte[]) null;
                }
                else
                {
                  bool flag = false;
                  for (int index8 = 0; index8 < groupMask.Length; ++index8)
                  {
                    if (groupMask[index8] != (byte) 0)
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (!flag)
                    groupMask = (byte[]) null;
                }
              }
              dictionaryArray[index7][pxDataRecord.GetString(2)] = new PXDimensionAttribute.ValueDescr(pxDataRecord.GetString(3), pxDataRecord.GetBoolean(5), groupMask);
            }
          }
        }
        foreach (KeyValuePair<string, string> keyValuePair3 in dictionary1)
        {
          string key6 = keyValuePair3.Key;
          IList<short> shortList = dictionary2[key6];
          Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray1 = this.Values[key6];
          string key7 = keyValuePair3.Value;
          Dictionary<string, PXDimensionAttribute.ValueDescr>[] dictionaryArray2 = this.Values[key7];
          foreach (KeyValuePair<short, PXDimensionAttribute.SegDescr> keyValuePair4 in (IEnumerable<KeyValuePair<short, PXDimensionAttribute.SegDescr>>) dictionary3[key7])
          {
            short key8 = keyValuePair4.Key;
            if (!shortList.Contains(key8))
            {
              Dictionary<string, PXDimensionAttribute.ValueDescr> dictionary4 = dictionaryArray1[(int) key8];
              foreach (KeyValuePair<string, PXDimensionAttribute.ValueDescr> keyValuePair5 in dictionaryArray2[(int) key8])
              {
                if (!dictionary4.ContainsKey(keyValuePair5.Key))
                  dictionary4.Add(keyValuePair5.Key, keyValuePair5.Value);
              }
            }
          }
        }
      }
      catch
      {
        this.Dimensions.Clear();
        this.Values.Clear();
        this.ValidCombos.Clear();
        this.Autonumbers.Clear();
        throw;
      }
    }
  }

  [PXHidden]
  internal class Dimension : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  internal class Segment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  internal class NumberingSequence : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
