// Decompiled with JetBrains decompiler
// Type: PX.SM.UPLogFileFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Log File")]
[Serializable]
public class UPLogFileFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Text;

  [PXUIField(DisplayName = "Text", Enabled = false)]
  [PXDBString]
  public virtual string Text
  {
    get => this._Text;
    set => this._Text = value;
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPLogFileFilter.text>
  {
  }
}
