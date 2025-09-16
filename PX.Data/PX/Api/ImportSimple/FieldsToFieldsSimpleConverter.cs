// Decompiled with JetBrains decompiler
// Type: PX.Api.ImportSimple.FieldsToFieldsSimpleConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using PX.Data;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.ImportSimple;

public class FieldsToFieldsSimpleConverter
{
  private readonly Guid MappingID;
  private readonly IEnumerable<SYMappingField> MappingFields;
  private readonly Dictionary<string, KeyValuePair<string, string>> AttributeLikeGrids;
  private readonly PXSiteMap.ScreenInfo screenInfo;

  public FieldsToFieldsSimpleConverter(
    IEnumerable<SYMappingField> mappingFields,
    Guid mappingID,
    Dictionary<string, KeyValuePair<string, string>> attributeLikeGrids,
    string screenID)
  {
    this.MappingFields = mappingFields != null ? mappingFields : throw new PXException("{0} cannot be instantiated. The Ctr parameters are not valid.", new object[1]
    {
      (object) typeof (FieldsToFieldsSimpleConverter).Name
    });
    this.MappingID = mappingID;
    this.AttributeLikeGrids = attributeLikeGrids;
    this.screenInfo = ScreenUtils.ScreenInfo.TryGet(screenID);
  }

  public IEnumerable<SYMappingFieldSimple> ConvertToSimpleFields()
  {
    FieldsToFieldsSimpleConverter fieldsSimpleConverter1 = this;
    List<SYMappingField> mappingFieldsList = new List<SYMappingField>(fieldsSimpleConverter1.MappingFields);
    foreach (SYMappingField syMappingField1 in mappingFieldsList)
    {
      FieldsToFieldsSimpleConverter fieldsSimpleConverter = fieldsSimpleConverter1;
      SYMappingField field = syMappingField1;
      bool isExternalKey;
      bool isKeyField;
      if (!fieldsSimpleConverter1.IsSystemCommand(mappingFieldsList, field, out isExternalKey, out isKeyField))
      {
        SYMappingFieldSimple simpleField = fieldsSimpleConverter1.ConvertToSimpleField(field, isKeyField, isExternalKey);
        if (fieldsSimpleConverter1.AttributeLikeGrids.ContainsKey(field.ObjectName) && fieldsSimpleConverter1.AttributeLikeGrids[field.ObjectName].Value == field.FieldName)
        {
          SYMappingField syMappingField2 = mappingFieldsList.TakeWhile<SYMappingField>((Func<SYMappingField, bool>) (f => f != field)).Where<SYMappingField>((Func<SYMappingField, bool>) (f => f.ObjectName == field.ObjectName && f.FieldName == FieldNameAndValueGenerator.GenerateKeyFieldName(fieldsSimpleConverter.AttributeLikeGrids[field.ObjectName].Key))).LastOrDefault<SYMappingField>();
          if (syMappingField2 != null && syMappingField2.Value != null && syMappingField2.Value.StartsWith("='") && syMappingField2.Value.EndsWith("'"))
            simpleField.FieldName = syMappingField2.Value.Substring(2, syMappingField2.Value.Length - 3);
        }
        yield return simpleField;
      }
    }
  }

  private bool IsSystemCommand(
    List<SYMappingField> mappingFieldsList,
    SYMappingField field,
    out bool isExternalKey,
    out bool isKeyField)
  {
    isExternalKey = field.FieldName.OrdinalEquals(FieldNameAndValueGenerator.GenerateExtRefValue());
    int length = field.FieldName.IndexOf('!');
    string pureFieldName = length >= 0 ? field.FieldName.Substring(0, length) : field.FieldName;
    isKeyField = isExternalKey || mappingFieldsList.Any<SYMappingField>((Func<SYMappingField, bool>) (f => f.ObjectName == field.ObjectName && f.FieldName == FieldNameAndValueGenerator.GenerateKeyFieldName(pureFieldName) && f.Value == FieldNameAndValueGenerator.GenerateKeyValue(field.ObjectName, pureFieldName)));
    return !isKeyField && (field.FieldName.StartsWith("@@") || field.FieldName.StartsWith("<")) || (!field.FieldName.OrdinalEquals("##") ? 0 : (field.Value.OrdinalEquals(FieldNameAndValueGenerator.NewLineFieldValue) ? 1 : 0)) != 0;
  }

  private SYMappingFieldSimple ConvertToSimpleField(
    SYMappingField mappingField,
    bool isKey,
    bool isExternalKey)
  {
    SYMappingFieldSimple mappingFieldSimple = new SYMappingFieldSimple();
    mappingFieldSimple.MappingID = new Guid?(this.MappingID);
    mappingFieldSimple.LineNbr = mappingField.LineNbr;
    mappingFieldSimple.ObjectName = mappingField.ObjectName;
    mappingFieldSimple.FieldName = !isExternalKey ? mappingField.FieldName : (string) null;
    mappingFieldSimple.Value = mappingField.Value;
    mappingFieldSimple.IsActive = mappingField.IsActive;
    mappingFieldSimple.IsKey = new bool?(isKey);
    SYMappingFieldSimple ret = mappingFieldSimple;
    if (ret.FieldName != null && ret.FieldName.Contains<char>('!'))
    {
      PXViewDescription pxViewDescription = this.screenInfo.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => string.Equals(c.Value.ViewName, ret.ObjectName, StringComparison.OrdinalIgnoreCase))).Select<KeyValuePair<string, PXViewDescription>, PXViewDescription>((Func<KeyValuePair<string, PXViewDescription>, PXViewDescription>) (c => c.Value)).FirstOrDefault<PXViewDescription>();
      PX.Data.Description.FieldInfo fieldInfo = (PX.Data.Description.FieldInfo) null;
      if (pxViewDescription != null)
        fieldInfo = ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription.Fields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => string.Equals(f.FieldName, StringExtensions.FirstSegment(ret.FieldName, '!'), StringComparison.OrdinalIgnoreCase)));
      if (fieldInfo != null && !string.IsNullOrWhiteSpace(fieldInfo.TextField))
      {
        string b = $"{fieldInfo.FieldName}!{fieldInfo.TextField}";
        if (string.Equals(ret.FieldName, b, StringComparison.OrdinalIgnoreCase))
          ret.FieldName = fieldInfo.FieldName;
      }
    }
    return ret;
  }
}
