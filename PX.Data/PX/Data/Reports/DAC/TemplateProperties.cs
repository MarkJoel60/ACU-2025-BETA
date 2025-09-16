// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.TemplateProperties
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
[Serializable]
public class TemplateProperties : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Template")]
  public 
  #nullable disable
  string Template { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  public bool? IsDefault { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Shared")]
  public bool? Shared { get; set; }

  public abstract class template : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TemplateProperties.template>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TemplateProperties.isDefault>
  {
  }

  public abstract class shared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TemplateProperties.shared>
  {
  }
}
