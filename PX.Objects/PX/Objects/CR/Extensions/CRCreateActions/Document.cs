// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.Document
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
public class Document : PXMappedCacheExtension, INotable
{
  public virtual int? ParentBAccountID { get; set; }

  public virtual int? WorkgroupID { get; set; }

  public virtual bool? OverrideSalesTerritory { get; set; }

  public virtual 
  #nullable disable
  string SalesTerritoryID { get; set; }

  public virtual int? OwnerID { get; set; }

  public virtual int? BAccountID { get; set; }

  public virtual int? ContactID { get; set; }

  public virtual int? RefContactID { get; set; }

  public virtual string ClassID { get; set; }

  public virtual Guid? NoteID { get; set; }

  public virtual string Source { get; set; }

  public virtual string CampaignID { get; set; }

  public virtual bool? OverrideRefContact { get; set; }

  public virtual string Description { get; set; }

  public virtual int? LocationID { get; set; }

  public virtual string TaxZoneID { get; set; }

  public virtual DateTime? QualificationDate { get; set; }

  public virtual Guid? ConvertedBy { get; set; }

  public virtual bool? IsActive { get; set; }

  public virtual string LanguageID { get; set; }

  public virtual string LocaleName { get; set; }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.parentBAccountID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.workgroupID>
  {
  }

  public abstract class overrideSalesTerritory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.overrideSalesTerritory>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Document.salesTerritoryID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.ownerID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.contactID>
  {
  }

  public abstract class refContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.refContactID>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.classID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Document.noteID>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.source>
  {
  }

  public abstract class campaignID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.campaignID>
  {
  }

  public abstract class overrideRefContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.overrideRefContact>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.description>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.locationID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.taxZoneID>
  {
  }

  public abstract class qualificationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Document.qualificationDate>
  {
  }

  public abstract class convertedBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Document.convertedBy>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.isActive>
  {
  }

  public abstract class languageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.languageID>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.localeName>
  {
  }
}
