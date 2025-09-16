// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.KeyValue.DAC.CSAddedAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.CS;
using PX.Data.BQL;

#nullable enable
namespace PX.Data.Descriptor.KeyValue.DAC;

[PXHidden]
public class CSAddedAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Attribute ID")]
  [PXSelector(typeof (CSAttribute.attributeID))]
  public virtual 
  #nullable disable
  string AttributeID { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAddedAttribute.attributeID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAddedAttribute.description>
  {
  }
}
