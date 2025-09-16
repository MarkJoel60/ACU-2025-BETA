// Decompiled with JetBrains decompiler
// Type: PX.CS.KeyValueHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Descriptor.KeyValue.DAC;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.CS;

[PXInternalUseOnly]
public static class KeyValueHelper
{
  public const string AttributePrefix = "Attribute";

  public static KeyValueHelper.Definition Def
  {
    get
    {
      return PXDatabase.GetSlot<KeyValueHelper.Definition>(typeof (KeyValueHelper).FullName + Thread.CurrentThread.CurrentUICulture.Name, typeof (CSAttribute), typeof (CSAttributeDetail), typeof (CSScreenAttribute));
    }
  }

  public static System.DateTime LastChangedDate => KeyValueHelper.Def.LastChangedDate;

  public static System.DateTime LastChangedDateUTC => KeyValueHelper.Def.LastChangedDateUTC;

  public static void CopyAttributes(
    System.Type tableType,
    PXCache cache,
    object sourceRow,
    object destinationRow)
  {
    foreach (KeyValueHelper.TableAttribute attribute in KeyValueHelper.Def.GetAttributes(tableType))
    {
      string fieldName = attribute.FieldName;
      PXFieldState valueExt = cache.GetValueExt(sourceRow, fieldName) as PXFieldState;
      cache.SetValueExt(destinationRow, fieldName, valueExt.Value);
    }
  }

  public static void CopyAttributes(PXCache cacheCopyFrom, PXCache cacheCopyTo, object row)
  {
    foreach (string existedInTwoEntity in KeyValueHelper.GetAttributesExistedInTwoEntities(cacheCopyTo, cacheCopyFrom))
    {
      object valueExt = cacheCopyFrom.GetValueExt(cacheCopyFrom.Current, existedInTwoEntity);
      cacheCopyTo.SetValueExt(row, existedInTwoEntity, valueExt);
    }
  }

  public static IEnumerable<CSAttributeDetail> GetAttributesValues(
    PXCache mainCache,
    PXCache additionalCache = null)
  {
    foreach (string existedInTwoEntity in KeyValueHelper.GetAttributesExistedInTwoEntities(mainCache, additionalCache))
      yield return new CSAttributeDetail()
      {
        AttributeID = existedInTwoEntity.StartsWith("Attribute") ? existedInTwoEntity.Substring("Attribute".Length) : existedInTwoEntity,
        Description = mainCache.GetValueExt(mainCache.Current, existedInTwoEntity).ToString()
      };
  }

  private static IEnumerable<string> GetAttributesExistedInTwoEntities(
    PXCache cache1,
    PXCache cache2)
  {
    return cache1.Fields.Where<string>((Func<string, bool>) (f =>
    {
      if (!cache1.IsKvExtAttribute(f))
        return false;
      return cache2 == null || cache2.IsKvExtAttribute(f);
    }));
  }

  public static string GetAttributeNameWithoutPrefix(string attributeName)
  {
    return attributeName.RemoveFromStart("Attribute");
  }

  public static PXFieldState GetAttributeState(PXCache cache, string attributeID)
  {
    return cache.GetValueExt(cache.Current, "Attribute" + attributeID) as PXFieldState;
  }

  public static Tuple<PXFieldState, short, short, string>[] GetAttributeFields(string screenID)
  {
    return ((IEnumerable<KeyValueHelper.AttributeFieldInfo>) KeyValueHelper.GetAttributeFieldsExtended(screenID)).Select<KeyValueHelper.AttributeFieldInfo, Tuple<PXFieldState, short, short, string>>((Func<KeyValueHelper.AttributeFieldInfo, Tuple<PXFieldState, short, short, string>>) (d => new Tuple<PXFieldState, short, short, string>(d.State, d.Column, d.Row, d.DefaultValue))).ToArray<Tuple<PXFieldState, short, short, string>>();
  }

  public static KeyValueHelper.AttributeFieldInfo[] GetAttributeFieldsExtended(string screenID)
  {
    List<KeyValueHelper.AttributeFieldInfo> attributeFieldInfoList = new List<KeyValueHelper.AttributeFieldInfo>();
    foreach (KeyValueHelper.ScreenAttribute attribute in KeyValueHelper.Def.GetAttributes(screenID))
    {
      if (string.IsNullOrEmpty(attribute.TypeValue))
      {
        PXFieldState pxFieldState = KeyValueHelper.MakeFieldState(attribute.Attribute, "Attribute" + attribute.AttributeID);
        if (pxFieldState != null)
        {
          pxFieldState.DisplayName = attribute.Attribute.Description;
          if (!PortalHelper.IsPortalContext() || !attribute.Attribute.IsInternal)
            attributeFieldInfoList.Add(new KeyValueHelper.AttributeFieldInfo()
            {
              State = pxFieldState,
              Column = attribute.Column,
              Row = attribute.Row,
              DefaultValue = attribute.DefaultValue,
              Hidden = attribute.Hidden,
              Required = attribute.Required
            });
        }
      }
    }
    return attributeFieldInfoList.ToArray();
  }

  public static Tuple<PXFieldState, bool, bool, string>[] GetAttributeFields(
    string screenID,
    string typeValue)
  {
    List<KeyValueHelper.ScreenAttribute> list = ((IEnumerable<KeyValueHelper.ScreenAttribute>) KeyValueHelper.Def.GetAttributes(screenID)).Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (attr => string.IsNullOrEmpty(attr.TypeValue))).ToList<KeyValueHelper.ScreenAttribute>();
    foreach (KeyValueHelper.ScreenAttribute screenAttribute in ((IEnumerable<KeyValueHelper.ScreenAttribute>) KeyValueHelper.Def.GetAttributes(screenID)).Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (attr => string.Equals(attr.TypeValue, typeValue?.Trim(), StringComparison.CurrentCultureIgnoreCase))))
    {
      KeyValueHelper.ScreenAttribute attribute = screenAttribute;
      int index = list.FindIndex((Predicate<KeyValueHelper.ScreenAttribute>) (attr => attr.AttributeID == attribute.AttributeID));
      if (index > -1)
        list[index] = attribute;
    }
    List<Tuple<PXFieldState, bool, bool, string>> tupleList = new List<Tuple<PXFieldState, bool, bool, string>>();
    foreach (KeyValueHelper.ScreenAttribute attr in list)
    {
      PXFieldState state = KeyValueHelper.GetState(attr);
      if (state != null)
      {
        state.DisplayName = attr.Attribute.Description;
        if (!PortalHelper.IsPortalContext() || !attr.Attribute.IsInternal)
          tupleList.Add(Tuple.Create<PXFieldState, bool, bool, string>(state, attr.Hidden, attr.Required, attr.DefaultValue));
      }
    }
    return tupleList.ToArray();
  }

  private static PXFieldState GetState(KeyValueHelper.ScreenAttribute attr)
  {
    PXFieldState state = KeyValueHelper.MakeFieldState(attr.Attribute, "Attribute" + attr.AttributeID, required: attr.Required ? new int?(1) : new int?());
    if (attr.Hidden && state != null)
      state.Visible = false;
    return state;
  }

  internal static PXFieldState GetState(
    CSAttribute attribute,
    CSScreenAttributeProperties property,
    CSAttributeDetail[] values)
  {
    return KeyValueHelper.MakeFieldState(attribute.With<CSAttribute, KeyValueHelper.Attribute>((Func<CSAttribute, KeyValueHelper.Attribute>) (a => new KeyValueHelper.Attribute(a, (IEnumerable<CSAttributeDetail>) values))), "Attribute" + attribute.AttributeID, required: new int?(-1), defaultValue: property.DefaultValue);
  }

  [PXInternalUseOnly]
  public static PXFieldState MakeFieldState(
    KeyValueHelper.Attribute attribute,
    string fieldName,
    object prevState = null,
    int? required = null,
    string defaultValue = null,
    string answerValue = null)
  {
    if (attribute != null && EnumerableExtensions.IsIn<int>(attribute.ControlType, 2, 6))
    {
      List<KeyValueHelper.AttributeValue> values = attribute.Values;
      if (values != null && values.Count >= 1)
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (KeyValueHelper.AttributeValue attributeValue in values)
        {
          if (!attributeValue.Disabled && !attributeValue.DeletedDatabaseRecord || !(attributeValue.ValueID != answerValue))
          {
            stringList1.Add(attributeValue.ValueID);
            stringList2.Add(attributeValue.Description);
          }
        }
        PXFieldState instance = PXStringState.CreateInstance(prevState, new int?(10), new bool?(true), fieldName, new bool?(false), required, attribute.EntryMask, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), defaultValue);
        if (attribute.ControlType == 6)
          ((PXStringState) instance).MultiSelect = true;
        return instance;
      }
    }
    int? controlType = attribute?.ControlType;
    PXFieldState pxFieldState;
    if (controlType.HasValue)
    {
      switch (controlType.GetValueOrDefault())
      {
        case 4:
          pxFieldState = PXFieldState.CreateInstance(prevState, typeof (bool), new bool?(false), new bool?(false), required, defaultValue: (object) defaultValue ?? (object) false, fieldName: fieldName, enabled: new bool?(true), visible: new bool?(true), visibility: PXUIVisibility.Visible);
          goto label_18;
        case 5:
          pxFieldState = PXDateState.CreateInstance(prevState, fieldName, new bool?(false), required, attribute.EntryMask, attribute.EntryMask, new System.DateTime?(), new System.DateTime?());
          goto label_18;
        case 7:
          pxFieldState = PXSelectorState.CreateInstance((PXFieldState) null, attribute.SchemaObject, attribute.SchemaField, fieldName);
          goto label_18;
        case 8:
          Decimal result;
          pxFieldState = PXDecimalState.CreateInstance(prevState, new int?(attribute.Precision), fieldName, new bool?(false), required, new Decimal?(), new Decimal?()).Apply<PXFieldState>((System.Action<PXFieldState>) (d => d.DefaultValue = (object) (Decimal.TryParse(defaultValue, out result) ? result : 0M)));
          goto label_18;
      }
    }
    pxFieldState = PXStringState.CreateInstance(prevState, new int?(attribute?.EntryMask?.Replace(">", "").Replace("<", "").Length ?? 60), new bool?(), fieldName, new bool?(false), required, attribute?.EntryMask, (string[]) null, (string[]) null, new bool?(true), defaultValue);
label_18:
    return pxFieldState;
  }

  [PXInternalUseOnly]
  public static object GetDefaultValueFor(int? controlType)
  {
    object defaultValueFor;
    if (controlType.HasValue)
    {
      switch (controlType.GetValueOrDefault())
      {
        case 4:
          defaultValueFor = (object) false;
          goto label_5;
        case 8:
          defaultValueFor = (object) 0;
          goto label_5;
      }
    }
    defaultValueFor = (object) null;
label_5:
    return defaultValueFor;
  }

  public class Attribute
  {
    public readonly string Description;
    public readonly int ControlType;
    public readonly string EntryMask;
    public readonly string RegExp;
    public readonly bool IsInternal;
    public readonly string SchemaObject;
    public readonly string SchemaField;
    public readonly int Precision;
    public readonly List<KeyValueHelper.AttributeValue> Values;

    public Attribute(
      string description,
      int controlType,
      string entryMask,
      string regExp,
      bool isInternal,
      string schemaObject,
      string schemaField,
      int precision,
      IEnumerable<KeyValueHelper.AttributeValue> values)
    {
      this.Description = description;
      this.ControlType = controlType;
      this.EntryMask = entryMask;
      this.RegExp = regExp;
      this.IsInternal = isInternal;
      this.SchemaObject = schemaObject;
      this.SchemaField = schemaField;
      this.Precision = precision;
      this.Values = ((IEnumerable<KeyValueHelper.AttributeValue>) ((object) values ?? (object) Array.Empty<KeyValueHelper.AttributeValue>())).ToList<KeyValueHelper.AttributeValue>();
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    public Attribute(PXDataRecord record)
    {
      string description = record.GetString(1);
      int? int32 = record.GetInt32(2);
      int controlType = int32 ?? 1;
      string entryMask = record.GetString(3);
      string regExp = record.GetString(4);
      bool? boolean = record.GetBoolean(5);
      bool flag = true;
      int num = boolean.GetValueOrDefault() == flag & boolean.HasValue ? 1 : 0;
      string schemaObject = record.GetString(6);
      string schemaField = record.GetString(7);
      int32 = record.GetInt32(8);
      int valueOrDefault = int32.GetValueOrDefault();
      // ISSUE: explicit constructor call
      this.\u002Ector(description, controlType, entryMask, regExp, num != 0, schemaObject, schemaField, valueOrDefault, (IEnumerable<KeyValueHelper.AttributeValue>) null);
    }

    public Attribute(CSAttribute attribute, IEnumerable<CSAttributeDetail> values)
    {
      string description = attribute.Description;
      int? nullable = attribute.ControlType;
      int controlType = nullable ?? 1;
      string entryMask = attribute.EntryMask;
      string regExp = attribute.RegExp;
      bool? isInternal = attribute.IsInternal;
      bool flag = true;
      int num = isInternal.GetValueOrDefault() == flag & isInternal.HasValue ? 1 : 0;
      string objectName = attribute.ObjectName;
      string fieldName = attribute.FieldName;
      nullable = attribute.Precision;
      int valueOrDefault = nullable.GetValueOrDefault();
      IEnumerable<KeyValueHelper.AttributeValue> values1 = values != null ? values.Select<CSAttributeDetail, KeyValueHelper.AttributeValue>((Func<CSAttributeDetail, KeyValueHelper.AttributeValue>) (v => new KeyValueHelper.AttributeValue(v))) : (IEnumerable<KeyValueHelper.AttributeValue>) null;
      // ISSUE: explicit constructor call
      this.\u002Ector(description, controlType, entryMask, regExp, num != 0, objectName, fieldName, valueOrDefault, values1);
    }
  }

  public class AttributeValue
  {
    public readonly string ValueID;
    public readonly string Description;
    public readonly bool Disabled;
    public readonly bool DeletedDatabaseRecord;

    public AttributeValue(string valueID, string description, bool disabled, bool deleted)
    {
      this.ValueID = valueID;
      this.Description = description;
      this.Disabled = disabled;
      this.DeletedDatabaseRecord = deleted;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    public AttributeValue(PXDataRecord record)
    {
      string valueID = record.GetString(1);
      string description = record.GetString(2);
      bool? boolean = record.GetBoolean(3);
      bool flag1 = true;
      int num1 = boolean.GetValueOrDefault() == flag1 & boolean.HasValue ? 1 : 0;
      boolean = record.GetBoolean(4);
      bool flag2 = true;
      int num2 = boolean.GetValueOrDefault() == flag2 & boolean.HasValue ? 1 : 0;
      // ISSUE: explicit constructor call
      this.\u002Ector(valueID, description, num1 != 0, num2 != 0);
    }

    public AttributeValue(CSAttributeDetail attributeDetail)
    {
      string valueId = attributeDetail.ValueID;
      string description = attributeDetail.Description;
      bool? nullable = attributeDetail.Disabled;
      bool flag1 = true;
      int num1 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
      nullable = attributeDetail.DeletedDatabaseRecord;
      bool flag2 = true;
      int num2 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
      // ISSUE: explicit constructor call
      this.\u002Ector(valueId, description, num1 != 0, num2 != 0);
    }
  }

  public class ScreenAttribute
  {
    public readonly KeyValueHelper.Attribute Attribute;
    public readonly string AttributeID;
    public readonly string TypeValue;
    public readonly bool Hidden;
    public readonly bool Required;
    public readonly short Column;
    public readonly short Row;
    public readonly string DefaultValue;

    public ScreenAttribute(KeyValueHelper.Attribute attribute, PXDataRecord record)
    {
      this.Attribute = attribute;
      this.AttributeID = record.GetString(1);
      this.TypeValue = record.GetString(2);
      this.Hidden = record.GetBoolean(3).Value;
      if (attribute.IsInternal && PXSiteMap.IsPortal)
        this.Hidden = true;
      this.Required = record.GetBoolean(4).Value;
      this.Column = record.GetInt16(5).GetValueOrDefault();
      this.Row = record.GetInt16(6).GetValueOrDefault();
      this.DefaultValue = record.GetString(7);
    }

    private ScreenAttribute(
      KeyValueHelper.Attribute attribute,
      string attributeID,
      string typeValue,
      bool hidden,
      bool required,
      short column,
      short row,
      string defaultValue)
    {
      this.Attribute = attribute;
      this.AttributeID = attributeID;
      this.TypeValue = typeValue;
      this.Hidden = hidden;
      this.Required = required;
      this.Column = column;
      this.Row = row;
      this.DefaultValue = defaultValue;
    }

    internal KeyValueHelper.ScreenAttribute Clone(bool forceHidden)
    {
      return new KeyValueHelper.ScreenAttribute(this.Attribute, this.AttributeID, this.TypeValue, forceHidden || this.Hidden, this.Required, this.Column, this.Row, this.DefaultValue);
    }
  }

  public class TableAttribute
  {
    public readonly KeyValueHelper.Attribute Attribute;
    public readonly string AttributeID;
    public readonly string FieldName;
    public PXFieldUpdating FieldUpdating;
    public PXFieldSelecting FieldSelecting;
    public PXFieldVerifying FieldVerifying;
    public PXExceptionHandling ExceptionHandling;
    public PXCommandPreparing CommandPreparing;
    public System.Action<PXCache> CacheAttached;
    internal StorageBehavior Storage;
    public PXUIFieldAttribute UIField;
    public Dictionary<string, string> DefaulValues;
    public Dictionary<string, Dictionary<string, object>> ScreensRequires;
    public Dictionary<string, Dictionary<string, bool>> ScreensHidden;
    protected object _LastRow;

    public TableAttribute(
      KeyValueHelper.Attribute attribute,
      string attributeID,
      System.Type table,
      Dictionary<string, string> defaulValues = null,
      Dictionary<string, Dictionary<string, object>> screensRequires = null,
      Dictionary<string, Dictionary<string, bool>> screensHidden = null,
      PXGraph graph = null)
    {
      this.Attribute = attribute;
      this.AttributeID = attributeID;
      this.FieldName = nameof (Attribute) + attributeID;
      this.DefaulValues = defaulValues;
      this.ScreensRequires = screensRequires ?? new Dictionary<string, Dictionary<string, object>>();
      this.ScreensHidden = screensHidden ?? new Dictionary<string, Dictionary<string, bool>>();
      if (attribute.ControlType == 5)
      {
        PXDBDateAttribute pxdbDateAttribute = new PXDBDateAttribute();
        pxdbDateAttribute.FieldName = this.FieldName;
        this.FieldUpdating += new PXFieldUpdating(pxdbDateAttribute.FieldUpdating);
        this.FieldSelecting += new PXFieldSelecting(pxdbDateAttribute.FieldSelecting);
        this.Storage = StorageBehavior.KeyValueDate;
      }
      else if (attribute.ControlType == 8)
      {
        PXDBDecimalAttribute decimalAttribute = new PXDBDecimalAttribute(attribute.Precision);
        decimalAttribute.FieldName = this.FieldName;
        this.FieldUpdating += new PXFieldUpdating(decimalAttribute.FieldUpdating);
        this.FieldSelecting += new PXFieldSelecting(decimalAttribute.FieldSelecting);
        this.Storage = StorageBehavior.KeyValueNumeric;
      }
      else if (attribute.ControlType == 4)
      {
        PXDBBoolAttribute attr = new PXDBBoolAttribute();
        attr.FieldName = this.FieldName;
        this.FieldUpdating += new PXFieldUpdating(attr.FieldUpdating);
        this.FieldSelecting += (PXFieldSelecting) ((s, e) =>
        {
          if (e.ReturnValue is int returnValue4)
            e.ReturnValue = (object) (returnValue4 == 1);
          else if (e.ReturnValue is Decimal returnValue3)
            e.ReturnValue = (object) (returnValue3 == 1M);
          attr.FieldSelecting(s, e);
        });
        this.Storage = StorageBehavior.KeyValueNumeric;
      }
      else if (attribute.ControlType == 2)
      {
        PXStringListAttribute stringListAttribute = new PXStringListAttribute(attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => !_.DeletedDatabaseRecord && !_.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => !_.DeletedDatabaseRecord && !_.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.DeletedDatabaseRecord)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.DeletedDatabaseRecord)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>());
        stringListAttribute.FieldName = this.FieldName;
        this.FieldSelecting += new PXFieldSelecting(stringListAttribute.FieldSelecting);
        this.Storage = StorageBehavior.KeyValueString;
      }
      else if (attribute.ControlType == 6)
      {
        PXStringListAttribute stringListAttribute = new PXStringListAttribute(attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => !_.DeletedDatabaseRecord && !_.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => !_.DeletedDatabaseRecord && !_.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.Disabled)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.DeletedDatabaseRecord)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.ValueID)).ToArray<string>(), attribute.Values.Where<KeyValueHelper.AttributeValue>((Func<KeyValueHelper.AttributeValue, bool>) (_ => _.DeletedDatabaseRecord)).Select<KeyValueHelper.AttributeValue, string>((Func<KeyValueHelper.AttributeValue, string>) (_ => _.Description)).ToArray<string>());
        stringListAttribute.MultiSelect = true;
        stringListAttribute.FieldName = this.FieldName;
        this.FieldSelecting += new PXFieldSelecting(stringListAttribute.FieldSelecting);
        this.Storage = StorageBehavior.KeyValueString;
      }
      else if (EnumerableExtensions.IsIn<int>(attribute.ControlType, 1, 7))
      {
        PXDBStringAttribute pxdbStringAttribute1;
        if (!string.IsNullOrEmpty(attribute.EntryMask))
          pxdbStringAttribute1 = new PXDBStringAttribute(attribute.EntryMask.Replace(">", "").Replace("<", "").Length)
          {
            InputMask = attribute.EntryMask
          };
        else
          pxdbStringAttribute1 = new PXDBStringAttribute();
        PXDBStringAttribute pxdbStringAttribute2 = pxdbStringAttribute1;
        pxdbStringAttribute2.FieldName = this.FieldName;
        pxdbStringAttribute2.IsUnicode = true;
        this.FieldUpdating += new PXFieldUpdating(pxdbStringAttribute2.FieldUpdating);
        this.FieldSelecting += new PXFieldSelecting(pxdbStringAttribute2.FieldSelecting);
        string regex = attribute.RegExp;
        if (!string.IsNullOrEmpty(regex))
          this.FieldVerifying += (PXFieldVerifying) ((s, e) =>
          {
            if (e.NewValue is string newValue2 && !new Regex(regex).IsMatch(newValue2))
              throw new PXSetPropertyException("Provided value does not pass validation rules defined for this field.");
          });
        if (attribute.ControlType == 7 && graph != null)
        {
          System.Type type = PXBuildManager.GetType(attribute.SchemaObject, true);
          PXCache readonlyCache = graph._GetReadonlyCache(type);
          List<PXEventSubscriberAttribute> attributesReadonly = readonlyCache.GetAttributesReadonly(attribute.SchemaField);
          PXSelectorAttribute selectorAttribute = attributesReadonly != null ? attributesReadonly.OfType<PXSelectorAttribute>().FirstOrDefault<PXSelectorAttribute>() : (PXSelectorAttribute) null;
          if (selectorAttribute == null)
          {
            PXSelectorAttribute selectorAttribute1 = new PXSelectorAttribute(readonlyCache.GetBqlField(attribute.SchemaField));
            selectorAttribute1.FieldName = this.FieldName;
            selectorAttribute = selectorAttribute1;
            selectorAttribute.CacheAttached(readonlyCache);
          }
          else
          {
            selectorAttribute = (PXSelectorAttribute) selectorAttribute.Clone(PXAttributeLevel.Type);
            selectorAttribute.FieldName = this.FieldName;
          }
          this.FieldVerifying += (PXFieldVerifying) ((sender, args) =>
          {
            PXFieldVerifyingEventArgs e = new PXFieldVerifyingEventArgs((object) null, args.NewValue, args.ExternalCall);
            selectorAttribute.FieldVerifying(sender, e);
            args.NewValue = e.NewValue;
            args.Cancel = e.Cancel;
          });
          if (selectorAttribute.SubstituteKey != (System.Type) null)
          {
            this.FieldUpdating += (PXFieldUpdating) ((sender, args) =>
            {
              PXFieldUpdatingEventArgs e = new PXFieldUpdatingEventArgs((object) null, args.NewValue);
              selectorAttribute.SubstituteKeyFieldUpdating(sender, e);
              args.NewValue = e.NewValue;
              args.Cancel = e.Cancel;
            });
            this.FieldSelecting += (PXFieldSelecting) ((sender, args) =>
            {
              PXFieldSelectingEventArgs e = new PXFieldSelectingEventArgs((object) null, args.ReturnValue, args.IsAltered, args.ExternalCall)
              {
                ReturnState = args.ReturnState
              };
              selectorAttribute.SubstituteKeyFieldSelecting(sender, e);
              args.ReturnValue = e.ReturnValue;
              args.ReturnState = e.ReturnState;
              args.Cancel = e.Cancel;
              args.IsAltered = e.IsAltered;
            });
          }
        }
        this.Storage = StorageBehavior.KeyValueString;
      }
      this.UIField = new PXUIFieldAttribute();
      this.UIField.FieldName = this.FieldName;
      this.UIField.SetBqlTable(table);
      this.UIField = (PXUIFieldAttribute) this.UIField.Clone(PXAttributeLevel.Item);
      this.UIField.DisplayName = attribute.Description;
      this.FieldSelecting += (PXFieldSelecting) ((s, e) =>
      {
        if (this._LastRow != null && this._LastRow != e.Row && !s.ObjectsEqual(this._LastRow, e.Row))
          return;
        this.UIField.FieldSelecting(s, e);
      });
      this.ExceptionHandling += (PXExceptionHandling) ((s, e) =>
      {
        if (e.Row != null)
          this._LastRow = e.Row;
        this.UIField.ExceptionHandling(s, e);
      });
      this.CommandPreparing += (PXCommandPreparing) ((s, e) =>
      {
        if (this._LastRow != null && this._LastRow != e.Row && !s.ObjectsEqual(this._LastRow, e.Row))
          return;
        this.UIField.CommandPreparing(s, e);
      });
      this.CacheAttached += new System.Action<PXCache>(((PXEventSubscriberAttribute) this.UIField).CacheAttached);
    }
  }

  public class AttributeFieldInfo
  {
    public PXFieldState State { get; set; }

    public short Column { get; set; }

    public short Row { get; set; }

    public string DefaultValue { get; set; }

    public bool Hidden { get; set; }

    public bool Required { get; set; }
  }

  public class Definition : IPrefetchable, IPXCompanyDependent
  {
    protected Dictionary<string, KeyValueHelper.Attribute> _Attributes;
    protected Dictionary<string, List<KeyValueHelper.ScreenAttribute>> _Screens;
    protected Dictionary<System.Type, List<string>> _Tables;

    public System.DateTime LastChangedDate { get; private set; }

    public System.DateTime LastChangedDateUTC { get; private set; }

    public KeyValueHelper.ScreenAttribute[] GetAttributes(string screenID)
    {
      List<KeyValueHelper.ScreenAttribute> source;
      if (string.IsNullOrEmpty(screenID) || !this._Screens.TryGetValue(screenID, out source))
        return new KeyValueHelper.ScreenAttribute[0];
      bool isPortal = PortalHelper.IsPortalContext(PortalContexts.Modern);
      return isPortal ? source.Select<KeyValueHelper.ScreenAttribute, KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, KeyValueHelper.ScreenAttribute>) (_ => _.Clone(_.Attribute.IsInternal & isPortal))).ToArray<KeyValueHelper.ScreenAttribute>() : source.ToArray();
    }

    public KeyValueHelper.TableAttribute[] GetAttributes(System.Type table, PXGraph graph = null)
    {
      table = PXCache.GetBqlTable(table);
      List<string> stringList;
      if (!(table != (System.Type) null) || !this._Tables.TryGetValue(table, out stringList))
        return new KeyValueHelper.TableAttribute[0];
      Dictionary<string, KeyValueHelper.TableAttribute> dictionary1 = new Dictionary<string, KeyValueHelper.TableAttribute>();
      foreach (string key in stringList)
      {
        List<KeyValueHelper.ScreenAttribute> source;
        if (this._Screens.TryGetValue(key, out source))
        {
          foreach (KeyValueHelper.ScreenAttribute screenAttribute in source)
          {
            KeyValueHelper.ScreenAttribute attr = screenAttribute;
            if (string.IsNullOrEmpty(attr.TypeValue))
            {
              List<\u003C\u003Ef__AnonymousType23<string, bool, bool>> list = source.Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (n => n.AttributeID == attr.AttributeID)).Select(n => new
              {
                Key = n.TypeValue,
                Required = n.Required,
                Hidden = n.Hidden
              }).ToList();
              Dictionary<string, object> dictionary2 = list.ToDictionary(x => x.Key, x => (object) x.Required);
              Dictionary<string, bool> dictionary3 = list.ToDictionary(x => x.Key, x => x.Hidden);
              if (!dictionary1.ContainsKey(attr.AttributeID))
              {
                Dictionary<string, KeyValueHelper.TableAttribute> dictionary4 = dictionary1;
                string attributeId1 = attr.AttributeID;
                KeyValueHelper.Attribute attribute = attr.Attribute;
                string attributeId2 = attr.AttributeID;
                System.Type table1 = table;
                Dictionary<string, string> dictionary5 = source.Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (n => !string.IsNullOrEmpty(n.DefaultValue) && n.AttributeID == attr.AttributeID)).Select(n => new
                {
                  Key = n.TypeValue,
                  Value = n.DefaultValue
                }).ToDictionary(x => x.Key, x => x.Value);
                Dictionary<string, Dictionary<string, object>> screensRequires = new Dictionary<string, Dictionary<string, object>>();
                screensRequires.Add(key, dictionary2);
                Dictionary<string, Dictionary<string, bool>> screensHidden = new Dictionary<string, Dictionary<string, bool>>();
                screensHidden.Add(key, dictionary3);
                PXGraph graph1 = graph;
                KeyValueHelper.TableAttribute tableAttribute = new KeyValueHelper.TableAttribute(attribute, attributeId2, table1, dictionary5, screensRequires, screensHidden, graph1);
                dictionary4[attributeId1] = tableAttribute;
              }
              else
              {
                dictionary1[attr.AttributeID].ScreensRequires[key] = dictionary2;
                dictionary1[attr.AttributeID].ScreensHidden[key] = dictionary3;
              }
            }
          }
        }
      }
      return dictionary1.Values.ToArray<KeyValueHelper.TableAttribute>();
    }

    public List<string> GetScreensWithAttributesForTable(System.Type table)
    {
      List<string> stringList;
      return !this._Tables.TryGetValue(table, out stringList) ? new List<string>() : stringList;
    }

    void IPrefetchable.Prefetch()
    {
      this.LastChangedDate = System.DateTime.Now;
      this.LastChangedDateUTC = System.DateTime.UtcNow;
      using (new PXConnectionScope())
      {
        this._Attributes = new Dictionary<string, KeyValueHelper.Attribute>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this._Screens = new Dictionary<string, List<KeyValueHelper.ScreenAttribute>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this._Tables = new Dictionary<System.Type, List<string>>();
        foreach (PXDataRecord record in PXDatabase.SelectMulti<CSAttribute>((PXDataField) new PXDataField<CSAttribute.attributeID>(), PXDBLocalizableStringAttribute.GetValueSelect(typeof (CSAttribute).Name, typeof (CSAttribute.description).Name, false), (PXDataField) new PXDataField<CSAttribute.controlType>(), (PXDataField) new PXDataField<CSAttribute.entryMask>(), (PXDataField) new PXDataField<CSAttribute.regExp>(), (PXDataField) new PXDataField<CSAttribute.isInternal>(), (PXDataField) new PXDataField<CSAttribute.objectName>(), (PXDataField) new PXDataField<CSAttribute.fieldName>(), (PXDataField) new PXDataField<CSAttribute.precision>()))
        {
          if (!record.GetString(0).Any<char>((Func<char, bool>) (_ => _ > '\u007F')))
            this._Attributes[record.GetString(0)] = new KeyValueHelper.Attribute(record);
        }
        using (new PXReadDeletedScope())
        {
          foreach (PXDataRecord record in PXDatabase.SelectMulti<CSAttributeDetail>((PXDataField) new PXDataField<CSAttributeDetail.attributeID>(), (PXDataField) new PXDataField<CSAttributeDetail.valueID>(), PXDBLocalizableStringAttribute.GetValueSelect(typeof (CSAttributeDetail).Name, typeof (CSAttributeDetail.description).Name, false), (PXDataField) new PXDataField<CSAttributeDetail.disabled>(), (PXDataField) new PXDataField<CSAttributeDetail.deletedDatabaseRecord>(), (PXDataField) new PXDataFieldOrder<CSAttributeDetail.attributeID>(), (PXDataField) new PXDataFieldOrder<CSAttributeDetail.sortOrder>()))
          {
            KeyValueHelper.Attribute attribute;
            if (this._Attributes.TryGetValue(record.GetString(0), out attribute))
              attribute.Values.Add(new KeyValueHelper.AttributeValue(record));
          }
        }
        foreach (PXDataRecord record in PXDatabase.SelectMulti<CSScreenAttribute>((PXDataField) new PXDataField<CSScreenAttribute.screenID>(), (PXDataField) new PXDataField<CSScreenAttribute.attributeID>(), (PXDataField) new PXDataField<CSScreenAttribute.typeValue>(), (PXDataField) new PXDataField<CSScreenAttribute.hidden>(), (PXDataField) new PXDataField<CSScreenAttribute.required>(), (PXDataField) new PXDataField<CSScreenAttribute.column>(), (PXDataField) new PXDataField<CSScreenAttribute.row>(), (PXDataField) new PXDataField<CSScreenAttribute.defaultValue>()))
        {
          KeyValueHelper.Attribute attribute;
          if (this._Attributes.TryGetValue(record.GetString(1), out attribute))
          {
            string key;
            List<KeyValueHelper.ScreenAttribute> screenAttributeList;
            if (!this._Screens.TryGetValue(key = record.GetString(0), out screenAttributeList))
              this._Screens[key] = screenAttributeList = new List<KeyValueHelper.ScreenAttribute>();
            screenAttributeList.Add(new KeyValueHelper.ScreenAttribute(attribute, record));
          }
        }
      }
      foreach (KeyValuePair<string, List<KeyValueHelper.ScreenAttribute>> screen in this._Screens)
      {
        string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screen.Key);
        if (!string.IsNullOrWhiteSpace(graphTypeByScreenId))
        {
          PXCacheInfo primaryCache = GraphHelper.GetPrimaryCache(graphTypeByScreenId);
          if (primaryCache?.CacheType != (System.Type) null)
          {
            System.Type bqlTable = PXCache.GetBqlTable(primaryCache.CacheType);
            if (bqlTable != (System.Type) null)
            {
              List<string> stringList;
              if (!this._Tables.TryGetValue(bqlTable, out stringList))
                this._Tables[bqlTable] = stringList = new List<string>();
              stringList.Add(screen.Key);
            }
          }
        }
      }
    }
  }
}
