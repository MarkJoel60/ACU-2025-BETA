// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRGramValidationDateTime
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
public abstract class CRGramValidationDateTime : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDateAndTime(BqlField = typeof (CRValidation.gramValidationDateTime))]
  public virtual DateTime? Value { get; set; }

  public abstract class value : BqlType<IBqlDateTime, DateTime>.Field<
  #nullable disable
  CRGramValidationDateTime.value>
  {
  }

  [PXHidden]
  [PXProjection(typeof (SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRValidation.type, IBqlString>.IsIn<ValidationTypesAttribute.leadToLead, ValidationTypesAttribute.leadToAccount, ValidationTypesAttribute.leadToContact, ValidationTypesAttribute.contactToLead>>.AggregateTo<Max<CRValidation.gramValidationDateTime>>))]
  public class ByLead : CRGramValidationDateTime
  {
    public new abstract class value : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRGramValidationDateTime.ByLead.value>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRValidation.type, IBqlString>.IsIn<ValidationTypesAttribute.contactToContact, ValidationTypesAttribute.contactToLead, ValidationTypesAttribute.contactToAccount, ValidationTypesAttribute.leadToContact>>.AggregateTo<Max<CRValidation.gramValidationDateTime>>))]
  public class ByContact : CRGramValidationDateTime
  {
    public new abstract class value : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRGramValidationDateTime.ByContact.value>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRValidation.type, IBqlString>.IsIn<ValidationTypesAttribute.accountToAccount, ValidationTypesAttribute.contactToAccount, ValidationTypesAttribute.leadToAccount>>.AggregateTo<Max<CRValidation.gramValidationDateTime>>))]
  public class ByBAccount : CRGramValidationDateTime
  {
    public new abstract class value : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CRGramValidationDateTime.ByBAccount.value>
    {
    }
  }
}
