// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageForReport
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
public class WikiPageForReport : WikiPage
{
  [PXDBGuid(false)]
  [PXWikiSelector(SubstituteKey = typeof (WikiPage.name))]
  public override Guid? WikiID
  {
    get => base.WikiID;
    set => base.WikiID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [WikiPageStatus.List]
  public override int? StatusID
  {
    get => this._StatusID;
    set => this._StatusID = value;
  }

  public new abstract class wikiID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageForReport.wikiID>
  {
  }
}
