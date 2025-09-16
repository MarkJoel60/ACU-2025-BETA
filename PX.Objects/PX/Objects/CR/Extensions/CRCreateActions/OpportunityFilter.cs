// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.OpportunityFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXHidden]
[Serializable]
public class OpportunityFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IClassIdFilter
{
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? CloseDate { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string Subject { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Opportunity Class")]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description))]
  public virtual string OpportunityClass { get; set; }

  string IClassIdFilter.ClassID => this.OpportunityClass;

  public abstract class closeDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OpportunityFilter.closeDate>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OpportunityFilter.subject>
  {
  }

  public abstract class opportunityClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OpportunityFilter.opportunityClass>
  {
  }
}
