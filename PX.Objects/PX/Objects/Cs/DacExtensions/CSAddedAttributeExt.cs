// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DacExtensions.CSAddedAttributeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Descriptor.KeyValue.DAC;

#nullable enable
namespace PX.Objects.CS.DacExtensions;

/// <summary>
/// This cache extension is used to allow redirect to the Attributes screen by clicking on the AttributeID field in New UI
/// </summary>
/// &gt;
public sealed class CSAddedAttributeExt : PXCacheExtension<
#nullable disable
CSAddedAttribute>
{
  /// <inheritdoc cref="P:PX.Data.Descriptor.KeyValue.DAC.CSAddedAttribute.AttributeID" />
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXSelector(typeof (CSAttribute.attributeID))]
  public string AttributeID { get; set; }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAddedAttributeExt.attributeID>
  {
  }
}
