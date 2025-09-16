// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEventSubscriberAttributeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class PXEventSubscriberAttributeExtensions
{
  internal static void prepare(
    this PXEventSubscriberAttribute attr,
    string fieldName,
    int fieldOrdinal,
    System.Type itemType)
  {
    attr.FieldName = fieldName;
    attr.FieldOrdinal = fieldOrdinal;
    if (!(attr.BqlTable == (System.Type) null))
      return;
    attr.SetBqlTable(itemType);
  }

  internal static void prepare(
    this PXEventSubscriberAttribute attr,
    string fieldName,
    int fieldOrdinal,
    System.Type itemType,
    System.Type extensionType)
  {
    attr.FieldName = fieldName;
    attr.FieldOrdinal = fieldOrdinal;
    attr.CacheExtensionType = extensionType;
    if (!(attr.BqlTable == (System.Type) null))
      return;
    attr.SetBqlTable(itemType);
  }
}
