// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.DuplicateDocument
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
[PXHidden]
public class DuplicateDocument : PXMappedCacheExtension
{
  public virtual int? ContactID { get; set; }

  public virtual int? RefContactID { get; set; }

  public virtual int? BAccountID { get; set; }

  public virtual 
  #nullable disable
  string ContactType { get; set; }

  public virtual string DuplicateStatus { get; set; }

  public virtual bool? DuplicateFound { get; set; }

  public virtual bool? IsActive { get; set; }

  public virtual string Email { get; set; }

  public virtual DateTime? GrammValidationDateTime { get; set; }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateDocument.contactID>
  {
  }

  public abstract class refContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateDocument.refContactID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DuplicateDocument.bAccountID>
  {
  }

  public abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DuplicateDocument.contactType>
  {
  }

  public abstract class duplicateStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DuplicateDocument.duplicateStatus>
  {
  }

  public abstract class duplicateFound : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DuplicateDocument.duplicateFound>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DuplicateDocument.isActive>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DuplicateDocument.email>
  {
  }

  public abstract class grammValidationDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DuplicateDocument.grammValidationDateTime>
  {
  }
}
