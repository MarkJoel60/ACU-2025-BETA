// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRGrams
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
[PXHidden]
[DebuggerDisplay("{GetType().Name,nq} of {EntityID}|{ValidationType}: {FieldName} = {FieldValue} ({Score})")]
[Serializable]
public class CRGrams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? GramID { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Entity Type")]
  [PXDefault]
  [ValidationTypes]
  public virtual 
  #nullable disable
  string ValidationType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Entity ID")]
  public virtual int? EntityID { get; set; }

  [PXDBString(40)]
  [PXDefault("PX.Objects.CR.Contact")]
  [PXUIField]
  public virtual string EntityName { get; set; }

  [PXDBString(60)]
  [PXDefault("")]
  [PXUIField]
  public virtual string FieldName { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault("")]
  [PXUIField]
  public virtual string FieldValue { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "1")]
  [PXUIField(DisplayName = "Score")]
  public virtual Decimal? Score { get; set; }

  [PXString(1)]
  [PXUIField(DisplayName = "Create on Entry")]
  [PXDefault("A")]
  [PX.Objects.CR.CreateOnEntry(true)]
  public virtual string CreateOnEntry { get; set; }

  public abstract class gramID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRGrams.gramID>
  {
  }

  public abstract class validationType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRGrams.validationType>
  {
  }

  public abstract class entityID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRGrams.entityID>
  {
  }

  public abstract class entityName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRGrams.entityName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRGrams.fieldName>
  {
  }

  public abstract class fieldValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRGrams.fieldValue>
  {
  }

  public abstract class score : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRGrams.score>
  {
  }

  public abstract class createOnEntry : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRGrams.createOnEntry>
  {
  }
}
