// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Unbound.TemplateAttributes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Unbound;

[PXCacheName("Template Attributes")]
public class TemplateAttributes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>References to parent Inventory Item.</summary>
  [PXUIField(DisplayName = "Template Item")]
  [TemplateInventory(IsKey = true)]
  public virtual int? TemplateItemID { get; set; }

  /// <summary>
  /// Array to store attribute identifiers (CSAttribute.attributeID) of additional attributes which are not from matrix (columns)
  /// </summary>
  public virtual 
  #nullable disable
  string[] AttributeIdentifiers { get; set; }

  public abstract class templateItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TemplateAttributes.templateItemID>
  {
  }

  public abstract class attributeIdentifiers : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    TemplateAttributes.attributeIdentifiers>
  {
  }
}
