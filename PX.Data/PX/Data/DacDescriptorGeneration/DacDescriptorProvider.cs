// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacDescriptorProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.EP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.DacDescriptorGeneration;

internal class DacDescriptorProvider : IDacDescriptorProvider
{
  public DacDescriptor CreateDacDescriptor(
    PXGraph graph,
    IBqlTable dac,
    DacDescriptorCreationOptions? descriptorCreationOptions = null)
  {
    ExceptionExtensions.ThrowOnNull<IBqlTable>(dac, nameof (dac), (string) null);
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    if (descriptorCreationOptions == null)
      descriptorCreationOptions = DacDescriptorCreationOptions.Default;
    PXCache cach = graph.Caches[dac.GetType()];
    IBqlTable bqlTable = DacDescriptorProvider.CorrectDacType(cach, dac);
    bool isInsertedRecord = this.IsNewDacRecord(cach, dac, bqlTable);
    string nameForDescriptor = this.GetDacTypeNameForDescriptor(dac, descriptorCreationOptions);
    ILookup<bool, DacPropertyInDescriptorInfo> lookup = this.GetPropertiesForDescriptorWithDeclarationOrderFromDacAndDacExtensions(cach, descriptorCreationOptions).ToLookup<DacPropertyInDescriptorInfo, bool>((Func<DacPropertyInDescriptorInfo, bool>) (p => p.IsKeyField));
    IEnumerable<DacPropertyInDescriptorInfo> markedProperties1 = lookup[true];
    IEnumerable<DacPropertyInDescriptorInfo> markedProperties2 = lookup[false];
    IEnumerable<string> values = this.GetDacPropertiesRepresentations(cach, bqlTable, true, isInsertedRecord, markedProperties1, descriptorCreationOptions).Concat<string>(this.GetDacPropertiesRepresentations(cach, bqlTable, false, isInsertedRecord, markedProperties2, descriptorCreationOptions));
    string str = string.Join(descriptorCreationOptions.FieldsSeparator, values);
    return new DacDescriptor(str.Length > 0 ? nameForDescriptor + descriptorCreationOptions.DacTypeWithFieldValuesSeparator + str : nameForDescriptor);
  }

  private static IBqlTable CorrectDacType(PXCache dacCache, IBqlTable dac)
  {
    System.Type type = dac.GetType();
    return dacCache.GetItemType().IsSubclassOf(type) && dacCache.ToChildEntity(type, (object) dac) is IBqlTable childEntity ? childEntity : dac;
  }

  private bool IsNewDacRecord(PXCache dacCache, IBqlTable dac, IBqlTable correctedDac)
  {
    bool flag1;
    switch (dacCache.GetStatus((object) dac))
    {
      case PXEntryStatus.Inserted:
      case PXEntryStatus.InsertedDeleted:
        flag1 = true;
        break;
      default:
        flag1 = false;
        break;
    }
    if (flag1)
      return true;
    if (correctedDac == dac)
      return dacCache.Inserted.OfType<IBqlTable>().Any<IBqlTable>((Func<IBqlTable, bool>) (insertedRecord => dacCache.ObjectsEqual((object) insertedRecord, (object) dac)));
    bool flag2;
    switch (dacCache.GetStatus((object) correctedDac))
    {
      case PXEntryStatus.Inserted:
      case PXEntryStatus.InsertedDeleted:
        flag2 = true;
        break;
      default:
        flag2 = false;
        break;
    }
    return flag2 || dacCache.Inserted.OfType<IBqlTable>().Any<IBqlTable>((Func<IBqlTable, bool>) (insertedRecord => dacCache.ObjectsEqual((object) insertedRecord, (object) dac) || dacCache.ObjectsEqual((object) insertedRecord, (object) correctedDac)));
  }

  private string GetDacTypeNameForDescriptor(
    IBqlTable dac,
    DacDescriptorCreationOptions descriptorCreationOptions)
  {
    System.Type type = dac.GetType();
    switch (descriptorCreationOptions.DacTypeNameStyle)
    {
      case DacTypeNameInDacDescriptorStyle.ShortTypeName:
        return type.Name;
      case DacTypeNameInDacDescriptorStyle.UserFriendlyTypeName:
        return Str.NullIfWhitespace(EntityHelper.GetFriendlyEntityName(type, (object) dac)) ?? type.FullName;
      default:
        return type.FullName;
    }
  }

  private IEnumerable<DacPropertyInDescriptorInfo> GetPropertiesForDescriptorWithDeclarationOrderFromDacAndDacExtensions(
    PXCache dacCache,
    DacDescriptorCreationOptions descriptorCreationOptions)
  {
    return dacCache.Fields.Select<string, (string, bool, bool, bool)?>((Func<string, (string, bool, bool, bool)?>) (field => this.GetPropertyMarkedWithPXFieldDescriptionAttribute(dacCache, field, descriptorCreationOptions))).OfType<(string, bool, bool, bool)>().Select<(string, bool, bool, bool), DacPropertyInDescriptorInfo>((Func<(string, bool, bool, bool), int, DacPropertyInDescriptorInfo>) ((propertyInfo, order) => new DacPropertyInDescriptorInfo(propertyInfo.PropertyName, propertyInfo.UseNullOrEmptyValues, propertyInfo.IsKey, propertyInfo.HasAutoNumbering, order)));
  }

  private (string PropertyName, bool UseNullOrEmptyValues, bool IsKey, bool HasAutoNumbering)? GetPropertyMarkedWithPXFieldDescriptionAttribute(
    PXCache dacCache,
    string propertyName,
    DacDescriptorCreationOptions descriptorCreationOptions)
  {
    bool flag1 = dacCache.Keys.Contains(propertyName);
    if (flag1 && descriptorCreationOptions.KeysInDescriptorStyle == DacKeysInDacDescriptorStyle.NeverInclude)
      return new (string, bool, bool, bool)?();
    PXFieldDescriptionAttribute descriptionAttribute = dacCache.GetAttributesReadonly(propertyName).OfType<PXFieldDescriptionAttribute>().FirstOrDefault<PXFieldDescriptionAttribute>();
    if (descriptionAttribute != null)
    {
      bool flag2 = dacCache.IsAutoNumber(propertyName);
      return new (string, bool, bool, bool)?((propertyName, descriptionAttribute.IncludeNullAndEmptyValuesInDacDescriptor, flag1, flag2));
    }
    if (!flag1 || descriptorCreationOptions.KeysInDescriptorStyle != DacKeysInDacDescriptorStyle.AlwaysInclude)
      return new (string, bool, bool, bool)?();
    bool flag3 = dacCache.IsAutoNumber(propertyName);
    return new (string, bool, bool, bool)?((propertyName, true, flag1, flag3));
  }

  private IEnumerable<string> GetDacPropertiesRepresentations(
    PXCache dacCache,
    IBqlTable dac,
    bool isKeyProperty,
    bool isInsertedRecord,
    IEnumerable<DacPropertyInDescriptorInfo> markedProperties,
    DacDescriptorCreationOptions options)
  {
    return markedProperties.OrderBy<DacPropertyInDescriptorInfo, int>((Func<DacPropertyInDescriptorInfo, int>) (property => property.DeclarationOrder)).Select<DacPropertyInDescriptorInfo, string>((Func<DacPropertyInDescriptorInfo, string>) (property => this.GetPropertyRepresentationInDescriptor(dacCache, dac, property, isKeyProperty, isInsertedRecord, options))).Where<string>((Func<string, bool>) (propertyRepresentation => propertyRepresentation != null));
  }

  private string? GetPropertyRepresentationInDescriptor(
    PXCache dacCache,
    IBqlTable dac,
    DacPropertyInDescriptorInfo propertyInfo,
    bool isKeyProperty,
    bool isInsertedRecord,
    DacDescriptorCreationOptions options)
  {
    string str = this.GetPropertyValue(dacCache, dac, propertyInfo, isInsertedRecord);
    bool flag = Str.IsNullOrWhiteSpace(str);
    if (flag)
    {
      if (!this.UseNullOrWhitespaceValues(options.NullOrEmptyValuesStyle, propertyInfo.NullOrEmptyValuesUsageFromAttribute))
        return (string) null;
      str = PXMessages.LocalizeNoPrefix("null");
    }
    return ((isKeyProperty || options.FieldNamesInDacDescriptorStyle != DacFieldNamesInDacDescriptorStyle.AllExceptKeys ? (options.FieldNamesInDacDescriptorStyle == DacFieldNamesInDacDescriptorStyle.All ? 1 : 0) : 1) | (flag ? 1 : 0)) == 0 ? str : propertyInfo.PropertyName + options.NameValueSeparatorInField + str;
  }

  private string? GetPropertyValue(
    PXCache dacCache,
    IBqlTable dac,
    DacPropertyInDescriptorInfo propertyInfo,
    bool isInsertedRecord)
  {
    if (propertyInfo.HasAutoNumbering & isInsertedRecord)
      return (string) null;
    if (!(dacCache.GetStateExt((object) dac, propertyInfo.PropertyName + "_Description") is PXFieldState stateExt))
      stateExt = dacCache.GetStateExt((object) dac, propertyInfo.PropertyName) as PXFieldState;
    return EntityHelper.GetFieldString(stateExt);
  }

  private bool UseNullOrWhitespaceValues(
    NullOrEmptyDacFieldValuesStyle emptyValuesStyle,
    bool valueFromAttributeOnDacProperty)
  {
    return emptyValuesStyle == NullOrEmptyDacFieldValuesStyle.UseFieldAttributes & valueFromAttributeOnDacProperty || emptyValuesStyle == NullOrEmptyDacFieldValuesStyle.AlwaysInclude;
  }
}
