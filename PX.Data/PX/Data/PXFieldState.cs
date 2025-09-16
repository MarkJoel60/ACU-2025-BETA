// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web.UI.Design;

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the input control for the DAC field.</summary>
[DebuggerDisplay("[{DisplayName}] [{ViewName}] {DataType} {Value}")]
public class PXFieldState : IDataSourceFieldSchema, ICloneable
{
  protected object _Value;
  protected string _Error;
  protected PXErrorLevel _ErrorLevel;
  protected bool _Enabled = true;
  protected bool _Visible = true;
  protected bool _IsReadOnly;
  protected string _FieldName;
  protected string _DescriptionName;
  protected internal bool _IsKey;
  protected bool _Nullable = true;
  protected int _Precision = -1;
  protected string _DisplayName;
  protected PXUIVisibility _Visibility = PXUIVisibility.Invisible;
  protected System.Type _DataType = typeof (object);
  protected object _DefaultValue;
  protected string _ViewName;
  protected string[] _FieldList;
  protected string[] _HeaderList;
  protected int _Length = -1;
  protected int _Required;
  protected string _ValueField;
  protected PXSelectorMode _SelectorMode;
  protected Dictionary<string, object> _ControlConfig;

  object ICloneable.Clone() => this.MemberwiseClone();

  /// <summary>The type of data stored in the field.</summary>
  public virtual System.Type DataType => this._DataType;

  /// <summary>The value that indicates (if set to <see langword="true" />) that the field is mapped to an identity column in a database table.</summary>
  public virtual bool Identity => false;

  /// <summary>The value that indicates (if set to <see langword="true" />) that the field is read-only.</summary>
  public virtual bool IsReadOnly
  {
    get => this._IsReadOnly || !this._Enabled;
    internal set => this._IsReadOnly = value;
  }

  /// <summary>A value that indicates the uniqueness constraint on the field.</summary>
  public virtual bool IsUnique => false;

  /// <summary>The storage size of the field.</summary>
  public virtual int Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  /// <summary>The name of the field.</summary>
  public virtual string Name
  {
    get => this._FieldName;
    internal set => this._FieldName = value;
  }

  /// <exclude />
  public virtual void SetAliased(string prefix)
  {
    this._FieldName = $"{prefix}__{this._FieldName}";
    this._IsKey = false;
  }

  /// <summary>A value that indicates (if set to <see langword="true" />) that the field can store the <see langword="null" /> value.</summary>
  public virtual bool Nullable
  {
    get => this._Nullable;
    internal set => this._Nullable = value;
  }

  /// <summary>The maximum number of digits used to represent a numeric value stored in the field.</summary>
  public virtual int Precision => this._Precision;

  /// <summary>A value that indicates (if set to <see langword="true" />) that the field is marked as a key field.</summary>
  public virtual bool PrimaryKey
  {
    get => this._IsKey;
    internal set => this._IsKey = value;
  }

  /// <summary>The number of digits to the right of the decimal point used to represent a numeric value stored in the field.</summary>
  public virtual int Scale => -1;

  /// <summary>A value that indicates (if set to <see langword="true" />) that the value of the field is required.</summary>
  public virtual bool? Required
  {
    get
    {
      if (this._Required < 0)
        return new bool?(false);
      return this._Required > 0 ? new bool?(true) : new bool?();
    }
    set
    {
      bool? nullable1 = value;
      bool flag1 = true;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      {
        this._Required = 1;
      }
      else
      {
        bool? nullable2 = value;
        bool flag2 = false;
        if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
          this._Required = -1;
        else
          this._Required = 0;
      }
    }
  }

  protected PXFieldState(object value)
  {
    if (!(value is PXFieldState pxFieldState))
    {
      this._Value = value;
    }
    else
    {
      this._Value = pxFieldState._Value;
      this._DataType = pxFieldState._DataType;
      this._IsKey = pxFieldState._IsKey;
      this._Nullable = pxFieldState._Nullable;
      this._Precision = pxFieldState._Precision;
      this._Length = pxFieldState._Length;
      this._DefaultValue = pxFieldState._DefaultValue;
      this._FieldName = pxFieldState._FieldName;
      this._DescriptionName = pxFieldState._DescriptionName;
      this.DisplayName = pxFieldState._DisplayName;
      this._Error = pxFieldState._Error;
      this._ErrorLevel = pxFieldState._ErrorLevel;
      this._Enabled = pxFieldState._Enabled;
      this._Visible = pxFieldState._Visible;
      this._IsReadOnly = pxFieldState._IsReadOnly;
      this._Visibility = pxFieldState._Visibility;
      this._ViewName = pxFieldState._ViewName;
      this._FieldList = pxFieldState._FieldList;
      this._HeaderList = pxFieldState._HeaderList;
      this._Required = pxFieldState._Required;
      this._ValueField = pxFieldState._ValueField;
      this._SelectorMode = pxFieldState._SelectorMode;
      this._ControlConfig = pxFieldState._ControlConfig;
    }
  }

  /// <summary>A value that is stored in the field.</summary>
  public virtual object Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  /// <summary>The error message assigned to the field.</summary>
  public virtual string Error
  {
    get => this._Error;
    set => this._Error = value;
  }

  /// <summary>A value that indicates (if set to <see langword="true" />) whether the field is marked with the warning sign.</summary>
  public virtual bool IsWarning
  {
    get => this._ErrorLevel == PXErrorLevel.Warning;
    set
    {
      if (value)
      {
        this._ErrorLevel = PXErrorLevel.Warning;
      }
      else
      {
        if (this._ErrorLevel != PXErrorLevel.Warning)
          return;
        this._ErrorLevel = PXErrorLevel.Error;
      }
    }
  }

  /// <summary>The error level assigned to the field.</summary>
  public virtual PXErrorLevel ErrorLevel
  {
    get => this._ErrorLevel;
    set => this._ErrorLevel = value;
  }

  /// <summary>The value that indicates (if set to <see langword="true" />) that the current field input control will respond to a user's interaction.</summary>
  public virtual bool Enabled
  {
    get => this._Enabled;
    set => this._Enabled = value;
  }

  /// <summary>The value that indicates (if set to <see langword="true" />) that the current field input control is displayed.</summary>
  public virtual bool Visible
  {
    get => this._Visible;
    set => this._Visible = value;
  }

  /// <summary>The display name for the field.</summary>
  /// <remarks>
  /// If <see cref="P:PX.Data.PXFieldState.DisplayName" /> has not been set explicitly then field name <see cref="P:PX.Data.PXFieldState.Name" /> is returned.
  /// </remarks>
  public virtual string DisplayName
  {
    get => this._DisplayName != null ? this._DisplayName : this._FieldName;
    set => this._DisplayName = value;
  }

  /// <summary>
  /// Indicates whether <see cref="P:PX.Data.PXFieldState.DisplayName" /> was explicitly specified for the field.
  /// </summary>
  /// <value>
  /// <see langword="true" /> if <see cref="P:PX.Data.PXFieldState.DisplayName" /> was explicitly specified for the field, <see langword="false" /> otherwise.
  /// </value>
  public virtual bool IsDisplayNameSpecified => this._DisplayName != null;

  /// <summary>The name of a DAC field displayed in the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> control of the field
  /// if the <tt>DisplayMode</tt> property is set to <tt>Text</tt>.
  /// If the <tt>DisplayMode</tt> property is set to <tt>Hint</tt>, the name is displayed in the <tt>ValueField</tt> - <tt>DescriptionName</tt> format.
  /// By default, <tt>DisplayMode</tt> is set to <tt>Hint</tt>.</summary>
  public virtual string DescriptionName
  {
    get => this._DescriptionName;
    set => this._DescriptionName = value;
  }

  /// <summary>The <see cref="T:PX.Data.PXUIVisibility" /> object for the field.</summary>
  public virtual PXUIVisibility Visibility
  {
    get => this._Visibility;
    set => this._Visibility = value;
  }

  /// <summary>The default value that is displayed in the field's cell for a new record that is not yet committed to the <see cref="T:PX.Data.PXGraph" /> instance.</summary>
  public virtual object DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  /// <summary>The name for the <see cref="T:PX.Data.PXView" /> object bound to the <see cref="!:PXSelector" /> field control.</summary>
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  /// <summary>The array of DAC fields for the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> field control.</summary>
  public virtual string[] FieldList
  {
    get => this._FieldList;
    set => this._FieldList = value;
  }

  /// <summary>The array of field display names for the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> field control.</summary>
  public virtual string[] HeaderList
  {
    get
    {
      return this._HeaderList != null && this._FieldList != null && this._HeaderList.Length == this._FieldList.Length ? this._HeaderList : this._FieldList;
    }
    set => this._HeaderList = value;
  }

  public virtual string ValueField
  {
    get => this._ValueField;
    set => this._ValueField = value;
  }

  /// <summary>The mode of the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> control.</summary>
  public virtual PXSelectorMode SelectorMode
  {
    get => this._SelectorMode;
    set => this._SelectorMode = value;
  }

  /// <summary>The definition of the control to be rendered.</summary>
  public virtual Dictionary<string, object> ControlConfig
  {
    get => this._ControlConfig;
    set => this._ControlConfig = value;
  }

  /// <summary>Returns the string representation of the data value held by the state object.</summary>
  /// <returns>The string representation of the value.</returns>
  public override string ToString() => this.Value == null ? string.Empty : this._Value.ToString();

  /// <summary>Compares the provided object with the data value.</summary>
  /// <param name="obj">The object to compare with the data value.</param>
  /// <returns>
  /// <see langword="true" /> if the provided object is equal to the data value and <see langword="false" /> otherwise.</returns>
  public override bool Equals(object obj) => this._Value != null && this._Value.Equals(obj);

  public override int GetHashCode() => this._Value == null ? 0 : this._Value.GetHashCode();

  public void SetFieldName(string name) => this._FieldName = name;

  public static PXFieldState CreateInstance(
    object value,
    System.Type dataType,
    bool? isKey = null,
    bool? nullable = null,
    int? required = null,
    int? precision = null,
    int? length = null,
    object defaultValue = null,
    string fieldName = null,
    string descriptionName = null,
    string displayName = null,
    string error = null,
    PXErrorLevel errorLevel = PXErrorLevel.Undefined,
    bool? enabled = null,
    bool? visible = null,
    bool? readOnly = null,
    PXUIVisibility visibility = PXUIVisibility.Undefined,
    string viewName = null,
    string[] fieldList = null,
    string[] headerList = null)
  {
    if (!(value is PXFieldState state))
      state = new PXFieldState(value);
    PXFieldState.FillValues(state, false, dataType, isKey, nullable, required, precision, length, defaultValue, fieldName, descriptionName, displayName, errorLevel, error, enabled, visible, readOnly, visibility, viewName, fieldList, headerList);
    return state;
  }

  public PXFieldState CreateInstance(
    System.Type dataType,
    bool? isKey,
    bool? nullable,
    int? required,
    int? precision,
    int? length,
    object defaultValue,
    string fieldName,
    string descriptionName,
    string displayName,
    string error,
    PXErrorLevel errorLevel,
    bool? enabled,
    bool? visible,
    bool? readOnly,
    PXUIVisibility visibility,
    string viewName,
    string[] fieldList,
    string[] headerList)
  {
    PXFieldState state = ((ICloneable) this).Clone() as PXFieldState;
    PXFieldState.FillValues(state, true, dataType, isKey, nullable, required, precision, length, defaultValue, fieldName, descriptionName, displayName, errorLevel, error, enabled, visible, readOnly, visibility, viewName, fieldList, headerList);
    return state;
  }

  private static void FillValues(
    PXFieldState state,
    bool @override,
    System.Type dataType,
    bool? isKey,
    bool? nullable,
    int? required,
    int? precision,
    int? length,
    object defaultValue,
    string fieldName,
    string descriptionName,
    string displayName,
    PXErrorLevel errorLevel,
    string error,
    bool? enabled,
    bool? visible,
    bool? readOnly,
    PXUIVisibility visibility,
    string viewName,
    string[] fieldList,
    string[] headerList)
  {
    if (dataType != (System.Type) null)
      state._DataType = dataType;
    if (isKey.HasValue)
      state._IsKey = isKey.Value;
    if (nullable.HasValue)
      state._Nullable = nullable.Value;
    if (required.HasValue)
      state._Required += required.Value;
    if (precision.HasValue)
      state._Precision = precision.Value;
    if (length.HasValue)
      state._Length = length.Value;
    if (defaultValue != null)
      state._DefaultValue = defaultValue;
    if (fieldName != null)
      state._FieldName = fieldName;
    if (descriptionName != null)
      state._DescriptionName = descriptionName;
    if (displayName != null)
      state.DisplayName = displayName;
    if (errorLevel >= state._ErrorLevel)
    {
      state._ErrorLevel = errorLevel;
      if (error != null)
        state._Error = error;
    }
    else if (state._Error == null && error != null)
    {
      state._Error = error;
      if (state._ErrorLevel == PXErrorLevel.Undefined)
        state._ErrorLevel = PXErrorLevel.Error;
    }
    if (enabled.HasValue && (@override || state._Enabled))
      state._Enabled = enabled.Value;
    if (visible.HasValue && (@override || state._Visible))
      state._Visible = visible.Value;
    if (readOnly.HasValue && (@override || !state._IsReadOnly))
      state._IsReadOnly = readOnly.Value;
    if (visibility != PXUIVisibility.Undefined)
      state._Visibility = visibility;
    if (viewName != null)
      state._ViewName = viewName;
    if (fieldList != null)
      state._FieldList = fieldList;
    if (headerList == null)
      return;
    state._HeaderList = headerList;
  }

  /// <summary>Initializes a field state from another field state.</summary>
  /// <param name="state">A field state to initialize the field state from.</param>
  public static PXFieldState CreateInstance(IDataSourceFieldSchema state)
  {
    return new PXFieldState((object) state)
    {
      _DataType = state.DataType,
      _Nullable = state.Nullable,
      _Precision = state.Precision,
      _Length = state.Length,
      _FieldName = state.Name
    };
  }

  public static string GetStringValue(PXFieldState state, string fFormat, string dFormat)
  {
    if (state == null || state.Value == null)
      return string.Empty;
    if (state.DataType == typeof (string))
    {
      PXStringState pxStringState = state as PXStringState;
      string strA = (string) state.Value;
      if (pxStringState != null)
      {
        if (pxStringState.AllowedValues != null && pxStringState.AllowedLabels != null)
        {
          for (int index = 0; index < pxStringState.AllowedValues.Length && index < pxStringState.AllowedLabels.Length; ++index)
          {
            if (string.Compare(strA, pxStringState.AllowedValues[index], StringComparison.OrdinalIgnoreCase) == 0)
            {
              strA = pxStringState.AllowedLabels[index];
              break;
            }
          }
        }
        else if (!string.IsNullOrEmpty(pxStringState.InputMask))
          strA = Mask.Format(pxStringState.InputMask, strA);
      }
      return strA;
    }
    if (state.DataType == typeof (byte) || state.DataType == typeof (short) || state.DataType == typeof (int))
    {
      PXIntState pxIntState = state as PXIntState;
      string allowedLabel = state.Value.ToString();
      if (pxIntState != null && pxIntState.AllowedValues != null && pxIntState.AllowedLabels != null)
      {
        for (int index = 0; index < pxIntState.AllowedValues.Length && index < pxIntState.AllowedLabels.Length; ++index)
        {
          if ((int) state.Value == pxIntState.AllowedValues[index])
          {
            allowedLabel = pxIntState.AllowedLabels[index];
            break;
          }
        }
      }
      return allowedLabel;
    }
    if (state.DataType == typeof (Guid) || state.DataType == typeof (bool) || state.DataType == typeof (long))
      return state.Value.ToString();
    if (state.DataType == typeof (float))
      return ((float) state.Value).ToString(fFormat + state.Precision.ToString());
    if (state.DataType == typeof (double))
      return ((double) state.Value).ToString(fFormat + state.Precision.ToString());
    if (state.DataType == typeof (Decimal))
      return ((Decimal) state.Value).ToString(fFormat + state.Precision.ToString());
    return state.DataType == typeof (System.DateTime) ? ((System.DateTime) state.Value).ToString(dFormat) : state.Value.ToString();
  }

  public static PXFieldState[] GetFields(PXGraph graph, System.Type[] tables, bool designMode)
  {
    List<PXFieldState> pxFieldStateList = new List<PXFieldState>();
    Dictionary<string, PXFieldState.FieldInfo> dictionary = new Dictionary<string, PXFieldState.FieldInfo>();
    for (int index1 = 0; index1 < tables.Length; ++index1)
    {
      string name = tables[index1].Name;
      if (Attribute.IsDefined((MemberInfo) tables[index1], typeof (PXCacheNameAttribute)))
        name = graph.Caches[tables[index1]].GetName();
      PXCache cach = graph.Caches[tables[index1]];
      for (int index2 = 0; index2 < cach.Fields.Count; ++index2)
      {
        string str = cach.Fields[index2];
        PXFieldState pxFieldState = cach.GetStateExt((object) null, str) as PXFieldState;
        if (index1 > 1)
          str = $"{tables[index1].Name}__{str}";
        string foreignTableName = (string) null;
        if (pxFieldState == null)
        {
          PropertyInfo property = tables[index1].GetProperty(str);
          pxFieldState = PXFieldState.CreateInstance((object) null, property != (PropertyInfo) null ? property.PropertyType : typeof (object), fieldName: str, displayName: str, enabled: new bool?(false), visible: new bool?(false), visibility: PXUIVisibility.Invisible);
        }
        else
        {
          if (designMode)
            PXFieldState.CorrectViewName(graph, (object) pxFieldState);
          if (index1 > 0)
          {
            pxFieldState.SetAliased(tables[index1].Name);
            foreignTableName = name;
            PXFieldState.FieldInfo fieldInfo;
            if (!string.IsNullOrEmpty(pxFieldState.DisplayName) && dictionary.TryGetValue(pxFieldState.DisplayName, out fieldInfo))
            {
              pxFieldState.DisplayName = $"{name}-{pxFieldState.DisplayName}";
              if (!string.IsNullOrEmpty(fieldInfo.ForeignTableName))
              {
                dictionary.Remove(fieldInfo.State.DisplayName);
                fieldInfo.State.DisplayName = $"{fieldInfo.ForeignTableName}-{fieldInfo.State.DisplayName}";
                if (!dictionary.ContainsKey(fieldInfo.State.DisplayName))
                  dictionary.Add(fieldInfo.State.DisplayName, fieldInfo);
              }
            }
          }
        }
        pxFieldStateList.Add(pxFieldState);
        if (!string.IsNullOrEmpty(pxFieldState.DisplayName) && !dictionary.ContainsKey(pxFieldState.DisplayName))
          dictionary.Add(pxFieldState.DisplayName, new PXFieldState.FieldInfo(pxFieldState, foreignTableName));
      }
    }
    List<string> stringList = new List<string>();
    foreach (KeyValuePair<string, PXFieldState.FieldInfo> keyValuePair in dictionary)
    {
      string key1 = keyValuePair.Key;
      string str1 = "-";
      int length = key1.IndexOf("-$");
      if (length < 0)
      {
        length = key1.IndexOf('$');
        str1 = string.Empty;
      }
      if (length > -1 && length < key1.Length - 1)
      {
        int num = key1.IndexOf("$-", length + 1);
        if (num > -1)
        {
          string str2 = key1.Substring(0, length);
          string str3 = key1.Substring(length + str1.Length + 1, num - length - str1.Length - 1);
          string str4 = key1.Substring(num + 2);
          string key2 = str2 + str1 + str4;
          if (dictionary.ContainsKey(key2) || stringList.Contains(key2))
            key2 = $"{str2}{str1}{str3}-{str4}";
          keyValuePair.Value.State.DisplayName = key2;
          stringList.Add(key2);
        }
      }
    }
    return pxFieldStateList.ToArray();
  }

  public static void CorrectViewName(PXGraph graph, object val)
  {
    PXView pxView;
    if (!(val is PXFieldState pxFieldState) || string.IsNullOrEmpty(pxFieldState.ViewName) || pxFieldState.ViewName[0] == '_' || !graph.Views.TryGetValue(pxFieldState.ViewName, out pxView))
      return;
    IBqlParameter[] parameters = pxView.BqlSelect.GetParameters();
    StringBuilder stringBuilder = new StringBuilder();
    foreach (System.Type table in pxView.BqlSelect.GetTables())
    {
      stringBuilder.Append('_');
      stringBuilder.Append(table.Name);
    }
    if (parameters != null)
    {
      foreach (IBqlParameter bqlParameter in parameters)
      {
        if (bqlParameter.HasDefault)
        {
          System.Type referencedType = bqlParameter.GetReferencedType();
          stringBuilder.Append('_');
          stringBuilder.Append(BqlCommand.GetItemType(referencedType).Name);
          stringBuilder.Append('.');
          stringBuilder.Append(referencedType.Name);
        }
      }
    }
    stringBuilder.Append('_');
    if (!graph.Views.ContainsKey(stringBuilder.ToString()))
    {
      graph.Views[stringBuilder.ToString()] = graph.Views[pxFieldState.ViewName];
      graph._viewNames = (List<string>) null;
    }
    pxFieldState.ViewName = stringBuilder.ToString();
  }

  public static object UnwrapValue(object stateOrValue)
  {
    return !(stateOrValue is PXFieldState pxFieldState) ? stateOrValue : pxFieldState.Value;
  }

  public static implicit operator string(PXFieldState val) => val.Value as string;

  /// <exclude />
  private class FieldInfo
  {
    public readonly PXFieldState State;
    public readonly string ForeignTableName;

    public FieldInfo(PXFieldState state, string foreignTableName)
    {
      this.State = state;
      this.ForeignTableName = foreignTableName;
    }
  }
}
