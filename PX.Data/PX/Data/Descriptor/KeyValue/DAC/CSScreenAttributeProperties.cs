// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.KeyValue.DAC.CSScreenAttributeProperties
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Descriptor.KeyValue.DAC;

[PXHidden]
public class CSScreenAttributeProperties : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  public virtual 
  #nullable disable
  string AttributeID { get; set; }

  [PXString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXStringList]
  [PXUnboundDefault("")]
  [PXUIField(DisplayName = "")]
  public virtual string TypeValue { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Hidden")]
  public virtual bool? Hidden { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? Required { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  public virtual string DefaultValue { get; set; }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttributeProperties.attributeID>
  {
  }

  public abstract class typeValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttributeProperties.typeValue>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSScreenAttributeProperties.hidden>
  {
  }

  public abstract class required : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CSScreenAttributeProperties.required>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSScreenAttributeProperties.defaultValue>
  {
  }
}
