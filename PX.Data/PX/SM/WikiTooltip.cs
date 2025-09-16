// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiTooltip
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXHidden]
[Serializable]
public class WikiTooltip : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsKey = true)]
  [PXUIField(DisplayName = "Language")]
  public virtual 
  #nullable disable
  string Language { get; set; }

  [PXDBString(8, IsKey = true)]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXUIField(DisplayName = "View Name")]
  public virtual string ViewName { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXUIField(DisplayName = "Field ID")]
  public virtual string FieldID { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.language>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.screenID>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.viewName>
  {
  }

  public abstract class fieldID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.fieldID>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.fieldName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltip.description>
  {
  }
}
