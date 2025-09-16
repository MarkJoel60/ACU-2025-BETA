// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSitePath
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
public class WikiSitePath : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public int? Number { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  public Guid? PageID { get; set; }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "Destination Physical Path")]
  public 
  #nullable disable
  string Path { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Source Article")]
  public string PageName { get; set; }

  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiSitePath.number>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiSitePath.pageID>
  {
  }

  public abstract class path : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSitePath.path>
  {
  }

  public abstract class pageName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiSitePath.pageName>
  {
  }
}
