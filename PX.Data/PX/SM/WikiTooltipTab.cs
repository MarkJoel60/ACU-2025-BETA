// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiTooltipTab
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
public class WikiTooltipTab : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Tab Name")]
  public virtual string TabName { get; set; }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltipTab.language>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltipTab.screenID>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltipTab.viewName>
  {
  }

  public abstract class tabName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiTooltipTab.tabName>
  {
  }
}
