// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.Matrix.Attributes;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Unbound;

[PXCacheName("Additional Attributes")]
public class AdditionalAttributes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Array to store values (CSAttributeDetail.valueID) of additional attributes which are not from matrix (columns)
  /// </summary>
  public virtual 
  #nullable disable
  string[] Values { get; set; }

  /// <summary>
  /// Array to store descriptions (CSAttributeDetail.description) of additional attributes which are not from matrix (columns)
  /// </summary>
  public virtual string[] Descriptions { get; set; }

  /// <summary>
  /// Array to store attribute identifiers (CSAttribute.attributeID) of additional attributes which are not from matrix (columns)
  /// </summary>
  public virtual string[] AttributeIdentifiers { get; set; }

  /// <summary>
  /// Array to store attribute descriptions (CSAttribute.description) of additional attributes which are not from matrix (columns)
  /// </summary>
  public virtual string[] AttributeDisplayNames { get; set; }

  /// <summary>ViewName for each attribute (to show PXSelector)</summary>
  public virtual string[] ViewNames { get; set; }

  /// <summary>
  /// The extra field is used to commit changes on a previous field. Should be hidden, last in a row.
  /// </summary>
  [PXUIField(DisplayName = "Template Item", Enabled = false)]
  [PXString]
  [MatrixAttributeValueSelector]
  public virtual string Extra { get; set; }

  public abstract class values : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AdditionalAttributes.values>
  {
  }

  public abstract class descriptions : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AdditionalAttributes.descriptions>
  {
  }

  public abstract class attributeIdentifiers : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AdditionalAttributes.attributeIdentifiers>
  {
  }

  public abstract class attributeDisplayNames : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AdditionalAttributes.attributeDisplayNames>
  {
  }

  public abstract class viewNames : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AdditionalAttributes.viewNames>
  {
  }

  public abstract class extra : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AdditionalAttributes.extra>
  {
  }
}
