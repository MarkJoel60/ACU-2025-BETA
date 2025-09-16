// Decompiled with JetBrains decompiler
// Type: PX.SM.RowClipboardSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class RowClipboardSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Template Name")]
  public 
  #nullable disable
  string TemplateName { get; set; }

  /// <exclude />
  public abstract class templateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RowClipboardSettings.templateName>
  {
  }
}
