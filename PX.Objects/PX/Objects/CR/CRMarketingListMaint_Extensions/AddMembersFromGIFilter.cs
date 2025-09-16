// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMaint_Extensions.AddMembersFromGIFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CR.CRMarketingListMaint_Extensions;

[PXHidden]
public class AddMembersFromGIFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXGuid]
  [PXDefault]
  [PXUIField(DisplayName = "Generic Inquiry")]
  [ContactGISelector]
  public Guid? GIDesignID { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "Shared Filter")]
  [FilterList(typeof (AddMembersFromGIFilter.gIDesignID), IsSiteMapIdentityScreenID = false, IsSiteMapIdentityGIDesignID = true)]
  [PXFormula(typeof (Default<AddMembersFromGIFilter.gIDesignID>))]
  public virtual Guid? SharedGIFilter { get; set; }

  public abstract class gIDesignID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  CRMarketingList.gIDesignID>
  {
  }

  public abstract class sharedGIFilter : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRMarketingList.sharedGIFilter>
  {
  }
}
