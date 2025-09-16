// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.CS;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Objects.CR;

public class CRAttribute
{
  private static CRAttribute.Definition Definitions
  {
    get
    {
      string key = "CSAttributes" + (PXDBLocalizableStringAttribute.IsEnabled ? Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName : string.Empty);
      CRAttribute.Definition definitions = PXContext.GetSlot<CRAttribute.Definition>(key);
      if (definitions == null)
        definitions = PXContext.SetSlot<CRAttribute.Definition>(PXDatabase.GetSlot<CRAttribute.Definition>(key, new System.Type[3]
        {
          typeof (CSAttribute),
          typeof (CSAttributeDetail),
          typeof (CSAttributeGroup)
        }));
      return definitions;
    }
  }

  public static CRAttribute.AttributeList Attributes => CRAttribute.Definitions.Attributes;

  public static CRAttribute.AttributeList AttributesByDescr
  {
    get => CRAttribute.Definitions.AttributesByDescr;
  }

  public static CRAttribute.AttributeList EntityAttributes(string type)
  {
    CRAttribute.AttributeList attributeList;
    return !CRAttribute.Definitions.EntityAttributes.TryGetValue(type, out attributeList) ? new CRAttribute.AttributeList() : attributeList;
  }

  private static CRAttribute.ClassAttributeList EntityAttributes(string type, string classID)
  {
    Dictionary<string, CRAttribute.ClassAttributeList> dictionary;
    CRAttribute.ClassAttributeList classAttributeList;
    return type != null && classID != null && CRAttribute.Definitions.ClassAttributes.TryGetValue(type, out dictionary) && dictionary.TryGetValue(classID, out classAttributeList) ? classAttributeList : new CRAttribute.ClassAttributeList();
  }

  public static CRAttribute.ClassAttributeList EntityAttributes(System.Type entityType, string classID)
  {
    return CRAttribute.EntityAttributes(entityType.FullName, classID);
  }

  public class Attribute
  {
    public readonly string ID;
    public readonly string Description;
    public readonly int? ControlType;
    public readonly string EntryMask;
    public readonly string RegExp;
    public readonly string List;
    public readonly bool IsInternal;
    public readonly int Precision;
    public readonly List<CRAttribute.AttributeValue> Values;

    public Attribute(PXDataRecord record)
    {
      this.ID = record.GetString(0);
      this.Description = record.GetString(1);
      this.ControlType = record.GetInt32(2);
      this.EntryMask = record.GetString(3);
      this.RegExp = record.GetString(4);
      this.List = record.GetString(5);
      this.IsInternal = record.GetBoolean(6).GetValueOrDefault();
      this.Precision = record.GetInt32(7).GetValueOrDefault();
      this.Values = new List<CRAttribute.AttributeValue>();
    }

    protected Attribute(CRAttribute.Attribute clone)
    {
      this.ID = clone.ID;
      this.Description = clone.Description;
      this.ControlType = clone.ControlType;
      this.EntryMask = clone.EntryMask;
      this.RegExp = clone.RegExp;
      this.List = clone.List;
      this.IsInternal = clone.IsInternal;
      this.Precision = clone.Precision;
      this.Values = clone.Values;
    }

    public void AddValue(CRAttribute.AttributeValue value) => this.Values.Add(value);

    public KeyValueHelper.Attribute ToPlatformAttribute()
    {
      string description = this.Description;
      int num1 = this.ControlType ?? 1;
      string entryMask = this.EntryMask;
      string regExp = this.RegExp;
      int num2 = this.IsInternal ? 1 : 0;
      int precision = this.Precision;
      List<CRAttribute.AttributeValue> values = this.Values;
      IEnumerable<KeyValueHelper.AttributeValue> attributeValues = values != null ? values.Select<CRAttribute.AttributeValue, KeyValueHelper.AttributeValue>((Func<CRAttribute.AttributeValue, KeyValueHelper.AttributeValue>) (v => v.ToPlatformAttributeValue())) : (IEnumerable<KeyValueHelper.AttributeValue>) null;
      return new KeyValueHelper.Attribute(description, num1, entryMask, regExp, num2 != 0, (string) null, (string) null, precision, attributeValues);
    }
  }

  public class AttributeValue
  {
    public readonly string ValueID;
    public readonly string Description;
    public readonly bool Disabled;

    public AttributeValue(PXDataRecord record)
    {
      this.ValueID = record.GetString(1);
      this.Description = record.GetString(2);
      this.Disabled = record.GetBoolean(3).GetValueOrDefault();
    }

    public KeyValueHelper.AttributeValue ToPlatformAttributeValue()
    {
      return new KeyValueHelper.AttributeValue(this.ValueID, this.Description, this.Disabled, false);
    }
  }

  [DebuggerDisplay("ID={ID} Description={Description} Required={Required} IsActive={IsActive}")]
  public class AttributeExt : CRAttribute.Attribute
  {
    public readonly string DefaultValue;
    public readonly bool Required;
    public readonly bool IsActive;
    public readonly string AttributeCategory;
    public bool NotInClass;

    public AttributeExt(
      CRAttribute.Attribute attr,
      string defaultValue,
      bool required,
      bool isActive)
      : this(attr, defaultValue, required, isActive, (string) null)
    {
    }

    public AttributeExt(
      CRAttribute.Attribute attr,
      string defaultValue,
      bool required,
      bool isActive,
      string attributeCategory)
      : base(attr)
    {
      this.DefaultValue = defaultValue;
      this.Required = required;
      this.IsActive = isActive;
      this.AttributeCategory = attributeCategory;
    }
  }

  public class AttributeList : DList<string, CRAttribute.Attribute>
  {
    private readonly bool useDescriptionAsKey;

    public AttributeList(bool useDescriptionAsKey = false)
      : base((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase, 4, 8, true)
    {
      this.useDescriptionAsKey = useDescriptionAsKey;
    }

    protected virtual string GetKeyForItem(CRAttribute.Attribute item)
    {
      return !this.useDescriptionAsKey || Str.IsNullOrEmpty(item.Description) ? item.ID : item.Description;
    }

    public virtual CRAttribute.Attribute this[string key]
    {
      get
      {
        CRAttribute.Attribute attribute;
        return !((KList<string, CRAttribute.Attribute>) this).TryGetValue(key, ref attribute) ? (CRAttribute.Attribute) null : attribute;
      }
    }
  }

  public class ClassAttributeList : DList<string, CRAttribute.AttributeExt>
  {
    public ClassAttributeList()
      : base((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase, 4, 8, true)
    {
    }

    protected virtual string GetKeyForItem(CRAttribute.AttributeExt item) => item.ID;

    public virtual CRAttribute.AttributeExt this[string key]
    {
      get
      {
        CRAttribute.AttributeExt attributeExt;
        return !((KList<string, CRAttribute.AttributeExt>) this).TryGetValue(key, ref attributeExt) ? (CRAttribute.AttributeExt) null : attributeExt;
      }
    }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public readonly CRAttribute.AttributeList Attributes;
    public readonly CRAttribute.AttributeList AttributesByDescr;
    public readonly Dictionary<string, CRAttribute.AttributeList> EntityAttributes;
    public readonly Dictionary<string, Dictionary<string, CRAttribute.ClassAttributeList>> ClassAttributes;

    public Definition()
    {
      this.Attributes = new CRAttribute.AttributeList();
      this.AttributesByDescr = new CRAttribute.AttributeList(true);
      this.ClassAttributes = new Dictionary<string, Dictionary<string, CRAttribute.ClassAttributeList>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      this.EntityAttributes = new Dictionary<string, CRAttribute.AttributeList>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    }

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        ((KList<string, CRAttribute.Attribute>) this.Attributes).Clear();
        ((KList<string, CRAttribute.Attribute>) this.AttributesByDescr).Clear();
        foreach (PXDataRecord record in PXDatabase.SelectMulti<CSAttribute>(new PXDataField[8]
        {
          new PXDataField(typeof (CSAttribute.attributeID).Name),
          PXDBLocalizableStringAttribute.GetValueSelect(typeof (CSAttribute).Name, typeof (CSAttribute.description).Name, false),
          new PXDataField(typeof (CSAttribute.controlType).Name),
          new PXDataField(typeof (CSAttribute.entryMask).Name),
          new PXDataField(typeof (CSAttribute.regExp).Name),
          PXDBLocalizableStringAttribute.GetValueSelect(typeof (CSAttribute).Name, typeof (CSAttribute.list).Name, true),
          new PXDataField(typeof (CSAttribute.isInternal).Name),
          new PXDataField(typeof (CSAttribute.precision).Name)
        }))
        {
          CRAttribute.Attribute attribute = new CRAttribute.Attribute(record);
          ((KList<string, CRAttribute.Attribute>) this.Attributes).Add(attribute);
          ((KList<string, CRAttribute.Attribute>) this.AttributesByDescr).Add(attribute);
        }
        foreach (PXDataRecord record in PXDatabase.SelectMulti<CSAttributeDetail>(new PXDataField[6]
        {
          new PXDataField(typeof (CSAttributeDetail.attributeID).Name),
          new PXDataField(typeof (CSAttributeDetail.valueID).Name),
          PXDBLocalizableStringAttribute.GetValueSelect(typeof (CSAttributeDetail).Name, typeof (CSAttributeDetail.description).Name, false),
          new PXDataField(typeof (CSAttributeDetail.disabled).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeDetail.attributeID).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeDetail.sortOrder).Name)
        }))
        {
          CRAttribute.Attribute attribute;
          if (((KList<string, CRAttribute.Attribute>) this.Attributes).TryGetValue(record.GetString(0), ref attribute))
            attribute.AddValue(new CRAttribute.AttributeValue(record));
        }
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<CSAttributeGroup>(new PXDataField[11]
        {
          new PXDataField(typeof (CSAttributeGroup.entityType).Name),
          new PXDataField(typeof (CSAttributeGroup.entityClassID).Name),
          new PXDataField(typeof (CSAttributeGroup.attributeID).Name),
          new PXDataField(typeof (CSAttributeGroup.defaultValue).Name),
          new PXDataField(typeof (CSAttributeGroup.required).Name),
          new PXDataField(typeof (CSAttributeGroup.isActive).Name),
          new PXDataField(typeof (CSAttributeGroup.attributeCategory).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeGroup.entityType).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeGroup.entityClassID).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeGroup.sortOrder).Name),
          (PXDataField) new PXDataFieldOrder(typeof (CSAttributeGroup.attributeID).Name)
        }))
        {
          string key1 = pxDataRecord.GetString(0);
          string key2 = pxDataRecord.GetString(1);
          string str = pxDataRecord.GetString(2);
          CRAttribute.AttributeList attributeList;
          if (!this.EntityAttributes.TryGetValue(key1, out attributeList))
            this.EntityAttributes[key1] = attributeList = new CRAttribute.AttributeList();
          Dictionary<string, CRAttribute.ClassAttributeList> dictionary;
          if (!this.ClassAttributes.TryGetValue(key1, out dictionary))
            this.ClassAttributes[key1] = dictionary = new Dictionary<string, CRAttribute.ClassAttributeList>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
          CRAttribute.ClassAttributeList classAttributeList1;
          if (!dictionary.TryGetValue(key2, out classAttributeList1))
            dictionary[key2] = classAttributeList1 = new CRAttribute.ClassAttributeList();
          CRAttribute.Attribute attribute;
          if (((KList<string, CRAttribute.Attribute>) this.Attributes).TryGetValue(str, ref attribute))
          {
            ((KList<string, CRAttribute.Attribute>) attributeList).Add(attribute);
            CRAttribute.ClassAttributeList classAttributeList2 = classAttributeList1;
            CRAttribute.Attribute attr = attribute;
            string defaultValue = pxDataRecord.GetString(3);
            bool? boolean = pxDataRecord.GetBoolean(4);
            int num1 = boolean.GetValueOrDefault() ? 1 : 0;
            boolean = pxDataRecord.GetBoolean(5);
            int num2 = boolean ?? true ? 1 : 0;
            string attributeCategory = pxDataRecord.GetString(6);
            CRAttribute.AttributeExt attributeExt = new CRAttribute.AttributeExt(attr, defaultValue, num1 != 0, num2 != 0, attributeCategory);
            ((KList<string, CRAttribute.AttributeExt>) classAttributeList2).Add(attributeExt);
          }
        }
      }
    }
  }
}
