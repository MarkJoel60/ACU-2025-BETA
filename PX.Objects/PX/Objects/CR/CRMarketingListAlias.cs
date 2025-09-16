// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[CRCacheIndependentPrimaryGraph(typeof (CRMarketingListMaint), typeof (Select<CRMarketingList, Where<CRMarketingList.marketingListID, Equal<Current<CRMarketingList.marketingListID>>>>))]
[PXCacheName("Marketing List")]
[PXBreakInheritance]
public class CRMarketingListAlias : CRMarketingList
{
  public new abstract class marketingListID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    CRMarketingListAlias.marketingListID>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRMarketingListAlias.selected>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingListAlias.name>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingListAlias.description>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingListAlias.status>
  {
  }

  public new abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingListAlias.workgroupID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRMarketingListAlias.ownerID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingListAlias.type>
  {
  }

  public new abstract class gIDesignID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingListAlias.gIDesignID>
  {
  }

  public new abstract class sharedGIFilter : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingListAlias.sharedGIFilter>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRMarketingListAlias.noteID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRMarketingListAlias.createdDateTime>
  {
  }
}
