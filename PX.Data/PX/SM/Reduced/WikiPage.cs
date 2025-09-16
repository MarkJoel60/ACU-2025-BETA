// Decompiled with JetBrains decompiler
// Type: PX.SM.Reduced.WikiPage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM.Reduced;

[Serializable]
public class WikiPage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  public abstract class pageID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage.pageID>
  {
  }
}
