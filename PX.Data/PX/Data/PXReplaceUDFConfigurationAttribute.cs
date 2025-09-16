// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReplaceUDFConfigurationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// An attribute that replaces the configuration of user-defined fields
/// for the DAC with the configuration of user-defined fields for the specified DAC.
/// </summary>
/// <remarks>
/// The attribute must be assigned to a DAC field along with the <see cref="T:PX.Data.PXNoteAttribute" /> attribute.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXReplaceUDFConfigurationAttribute : PXEventSubscriberAttribute
{
  protected virtual System.Type TableName { get; set; }

  public PXReplaceUDFConfigurationAttribute(System.Type tableName) => this.TableName = tableName;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!sender.GetAttributesReadonly(this.FieldName).OfType<PXNoteAttribute>().Any<PXNoteAttribute>())
      throw new PXException("PXReplaceUDFConfigurationAttribute must be used along with PXNoteAttribute. The {0} field of the {1} DAC has PXReplaceUDFConfigurationAttribute, but has no PXNoteAttribute assigned.", new object[2]
      {
        (object) this.FieldName,
        (object) sender.GetItemType().FullName
      });
    sender._KeyValueAttributeConfigurationSource = this.TableName;
  }
}
