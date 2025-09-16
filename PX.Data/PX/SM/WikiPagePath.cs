// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPagePath
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPagePath : WikiPage, IPXSelectable
{
  protected 
  #nullable disable
  string _Path;

  [PXDBInt]
  [PXUIField(DisplayName = "Status")]
  [WikiPageStatus.List]
  public override int? StatusID
  {
    get => this._StatusID;
    set => this._StatusID = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Path")]
  public string Path
  {
    get => this._Path;
    set => this._Path = value;
  }

  [PXString(InputMask = "")]
  public override string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  public abstract class path : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPagePath.path>
  {
  }

  public new abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPagePath.content>
  {
  }
}
