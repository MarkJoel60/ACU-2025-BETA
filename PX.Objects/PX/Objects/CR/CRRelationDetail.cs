// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelationDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CRRelationDetail : PXEventSubscriberAttribute
{
  protected readonly System.Type StatusField;
  protected readonly System.Type DescriptionField;
  protected readonly System.Type OwnerField;
  protected readonly System.Type DocumentDateField;
  public string StatusFieldName;
  public string DescriptionFieldName;
  public string OwnerFieldName;
  public string DocumentDateFieldName;

  public CRRelationDetail(
    System.Type statusField,
    System.Type descriptionField,
    System.Type ownerField,
    System.Type documentDateField)
  {
    this.StatusField = statusField;
    this.DescriptionField = descriptionField;
    this.OwnerField = ownerField;
    this.DocumentDateField = documentDateField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this.StatusField != (System.Type) null)
      this.StatusFieldName = sender.GetField(this.StatusField);
    if (this.DescriptionField != (System.Type) null)
      this.DescriptionFieldName = sender.GetField(this.DescriptionField);
    if (this.OwnerField != (System.Type) null)
      this.OwnerFieldName = sender.GetField(this.OwnerField);
    if (!(this.DocumentDateField != (System.Type) null))
      return;
    this.DocumentDateFieldName = sender.GetField(this.DocumentDateField);
  }

  public (string, string, int?, DateTime?) GetValues(PXCache cache, object entity)
  {
    (string, string, int?, DateTime?) values = ((string) null, (string) null, new int?(), new DateTime?());
    if (this.StatusFieldName != null)
      values.Item1 = cache.GetValue(entity, this.StatusFieldName) as string;
    if (this.DescriptionFieldName != null)
      values.Item2 = cache.GetValue(entity, this.DescriptionFieldName) as string;
    if (this.OwnerFieldName != null)
      values.Item3 = cache.GetValue(entity, this.OwnerFieldName) as int?;
    if (this.DocumentDateFieldName != null)
      values.Item4 = cache.GetValue(entity, this.DocumentDateFieldName) as DateTime?;
    return values;
  }
}
