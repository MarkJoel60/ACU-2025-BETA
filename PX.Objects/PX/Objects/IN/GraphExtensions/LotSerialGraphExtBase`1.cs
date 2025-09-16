// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LotSerialGraphExtBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN.DAC.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class LotSerialGraphExtBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  protected const string LotSerialAttributesMaintenanceScreenID = "IN209600";
  protected const string ValueFieldName = "Value";
  protected Dictionary<string, KeyValueHelper.Attribute> _attributes;

  protected virtual KeyValueHelper.Attribute GetKeyValueAttribute(
    string attributeID,
    bool skipException = false)
  {
    if (this._attributes == null)
    {
      KeyValueHelper.ScreenAttribute[] attributes = KeyValueHelper.Def.GetAttributes("IN209600");
      this._attributes = attributes != null ? ((IEnumerable<KeyValueHelper.ScreenAttribute>) attributes).ToDictionary<KeyValueHelper.ScreenAttribute, string, KeyValueHelper.Attribute>((Func<KeyValueHelper.ScreenAttribute, string>) (a => a.AttributeID), (Func<KeyValueHelper.ScreenAttribute, KeyValueHelper.Attribute>) (a => a.Attribute), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : (Dictionary<string, KeyValueHelper.Attribute>) null;
    }
    Dictionary<string, KeyValueHelper.Attribute> attributes1 = this._attributes;
    KeyValueHelper.Attribute keyValueAttribute;
    // ISSUE: explicit non-virtual call
    if ((attributes1 != null ? (__nonvirtual (attributes1.TryGetValue(attributeID, out keyValueAttribute)) ? 1 : 0) : 0) != 0)
      return keyValueAttribute;
    if (!skipException)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<CSAttribute>((PXGraph) this.Base), new object[1]
      {
        (object) attributeID
      });
    return (KeyValueHelper.Attribute) null;
  }

  protected virtual void FieldSelecting(string attributeID, PXFieldSelectingEventArgs args)
  {
    KeyValueHelper.Attribute keyValueAttribute = this.GetKeyValueAttribute(attributeID);
    if (keyValueAttribute.ControlType != 7)
      return;
    PXCache cach = this.Base.Caches[PXBuildManager.GetType(keyValueAttribute.SchemaObject, true)];
    object obj = (object) null;
    string schemaField = keyValueAttribute.SchemaField;
    ref object local = ref obj;
    cach.RaiseFieldSelecting(schemaField, (object) null, ref local, true);
    if (!(obj is PXFieldState pxFieldState))
      return;
    PXFieldState instance = PXFieldState.CreateInstance((object) pxFieldState, pxFieldState.DataType, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    instance.Required = new bool?();
    object returnValue = args.ReturnValue;
    args.ReturnState = (object) instance;
    instance.Value = returnValue;
    if (string.IsNullOrEmpty(pxFieldState.ValueField))
      return;
    instance.ValueField = pxFieldState.ValueField;
  }

  protected virtual string ConvertAttributeIDToFieldName(string attributeID)
  {
    return "Attribute" + attributeID;
  }

  protected virtual string ConvertFieldNameToAttributeID(string fieldName)
  {
    return fieldName.RemoveFromStart("Attribute");
  }

  public virtual object GetValue<TAttributesHeader>(
    TAttributesHeader attributesHeader,
    string attributeID,
    PXCache cache = null)
    where TAttributesHeader : class, IBqlTable, new()
  {
    object obj = (object) null;
    ((PXCache) ((object) cache ?? (object) GraphHelper.Caches<TAttributesHeader>((PXGraph) this.Base))).RaiseFieldSelecting(this.ConvertAttributeIDToFieldName(attributeID), (object) attributesHeader, ref obj, false);
    if (obj is PXFieldState pxFieldState)
      obj = pxFieldState.Value;
    return obj;
  }

  public virtual TAttributesHeader SetValue<TAttributesHeader>(
    TAttributesHeader attributesHeader,
    string attributeID,
    object value,
    PXCache<TAttributesHeader> attributesHeaderCache = null)
    where TAttributesHeader : class, IBqlTable, ILotSerialAttributesHeader, new()
  {
    if (attributesHeaderCache == null)
      attributesHeaderCache = GraphHelper.Caches<TAttributesHeader>((PXGraph) this.Base);
    ((PXCache) attributesHeaderCache).SetValueExt((object) attributesHeader, this.ConvertAttributeIDToFieldName(attributeID), value);
    return attributesHeaderCache.Update(attributesHeader);
  }

  public virtual TAttributesHeaderDestination CopyAttributes<TAttributesHeaderSource, TAttributesHeaderDestination>(
    TAttributesHeaderSource source,
    TAttributesHeaderDestination destination,
    PXCache<TAttributesHeaderSource> sourceCache = null,
    PXCache<TAttributesHeaderDestination> destinationCache = null)
    where TAttributesHeaderSource : class, IBqlTable, ILotSerialAttributesHeader, new()
    where TAttributesHeaderDestination : class, IBqlTable, ILotSerialAttributesHeader, new()
  {
    foreach (LotSerialGraphExtBase<TGraph>.AttributeInformation attribute in this.GetAttributes(source.InventoryID))
    {
      object obj = this.GetValue<TAttributesHeaderSource>(source, attribute.AttributeID, (PXCache) sourceCache);
      if (obj == null && attribute.Required && attribute.IsActive)
      {
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, source.InventoryID);
        throw new PXException("At least one required attribute has not been specified for the {0} lot or serial number of the {1} item in the Line Details dialog box. Specify all required attributes to save the document.", new object[2]
        {
          (object) source.LotSerialNbr,
          (object) inventoryItem?.InventoryCD?.Trim()
        });
      }
      destination = this.SetValue<TAttributesHeaderDestination>(destination, attribute.AttributeID, obj, destinationCache);
    }
    return destination;
  }

  protected virtual IEnumerable<LotSerialGraphExtBase<TGraph>.AttributeInformation> GetAttributes(
    int? inventoryID)
  {
    List<LotSerialGraphExtBase<TGraph>.AttributeInformation> attributes = new List<LotSerialGraphExtBase<TGraph>.AttributeInformation>();
    if (!inventoryID.HasValue)
      return (IEnumerable<LotSerialGraphExtBase<TGraph>.AttributeInformation>) attributes;
    InventoryItemLotSerialAttributes serialAttributes = InventoryItemLotSerialAttributes.PK.Find((PXGraph) this.Base, inventoryID);
    if (serialAttributes == null)
      return (IEnumerable<LotSerialGraphExtBase<TGraph>.AttributeInformation>) attributes;
    int index = 0;
    while (true)
    {
      int num = index;
      int? length = serialAttributes.AttributeIdentifiers?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        attributes.Add(new LotSerialGraphExtBase<TGraph>.AttributeInformation()
        {
          AttributeID = serialAttributes.AttributeIdentifiers[index],
          Required = serialAttributes.AttributeRequired[index],
          IsActive = serialAttributes.AttributeIsActive[index],
          SortOrder = serialAttributes.AttributeSortOrder[index]
        });
        ++index;
      }
      else
        break;
    }
    return (IEnumerable<LotSerialGraphExtBase<TGraph>.AttributeInformation>) attributes;
  }

  protected virtual IEnumerable<LotSerialGraphExtBase<TGraph>.AttributeInformation> GetRequiredAttributes(
    int? inventoryID)
  {
    return this.GetAttributes(inventoryID).Where<LotSerialGraphExtBase<TGraph>.AttributeInformation>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, bool>) (a => a.Required && a.IsActive));
  }

  public class AttributeInformation
  {
    public string AttributeID { get; init; }

    public bool Required { get; init; }

    public bool IsActive { get; init; }

    public short SortOrder { get; init; }
  }
}
