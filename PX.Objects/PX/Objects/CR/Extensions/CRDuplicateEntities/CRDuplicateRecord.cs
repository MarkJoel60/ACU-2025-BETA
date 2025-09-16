// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateRecord
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
[PXVirtual]
[PXHidden]
[Serializable]
public class CRDuplicateRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? ContactID { get; set; }

  [PXDBString(2, IsKey = true)]
  [PXUIField(DisplayName = "Entity Type")]
  [ValidationTypes]
  public virtual 
  #nullable disable
  string ValidationType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXVirtualSelector(typeof (Contact.contactID))]
  public virtual int? DuplicateContactID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXVirtualSelector(typeof (Contact.contactID), DescriptionField = typeof (Contact.displayName))]
  public virtual int? DuplicateRefContactID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXVirtualSelector(typeof (BAccount.bAccountID))]
  public virtual int? DuplicateBAccountID { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "1")]
  [PXUIField(DisplayName = "Score")]
  public virtual Decimal? Score { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Duplicate Contact Type", Visible = false)]
  public virtual string DuplicateContactType { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PhoneValidation]
  [PXPhone]
  public virtual string Phone1 { get; set; }

  [PXBool]
  [PXUIField]
  public bool? CanBeMerged { get; set; }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRDuplicateRecord.contactID>
  {
  }

  public abstract class validationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRDuplicateRecord.validationType>
  {
  }

  public abstract class duplicateContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRDuplicateRecord.duplicateContactID>
  {
  }

  public abstract class duplicateRefContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRDuplicateRecord.duplicateRefContactID>
  {
  }

  public abstract class duplicateBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRDuplicateRecord.duplicateBAccountID>
  {
  }

  public abstract class score : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRDuplicateRecord.score>
  {
  }

  public abstract class duplicateContactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRDuplicateRecord.duplicateContactType>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRDuplicateRecord.phone1>
  {
  }

  public abstract class canBeMerged : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRDuplicateRecord.canBeMerged>
  {
  }
}
