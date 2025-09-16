// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[PXPrimaryGraph(typeof (CRDuplicateValidationSetupMaint))]
[Serializable]
public class CRValidation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public static readonly DateTime DefaultGramValidationDateTime = new DateTime(1901, 1, 1);

  [PXDBInt(IsKey = true)]
  public virtual int? ID { get; set; }

  [PXDBString(2)]
  [ValidationTypes]
  [PXUIField(DisplayName = "Type")]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Validation Score Threshold")]
  [PXDefault(TypeCode.Decimal, "5.0")]
  public virtual Decimal? ValidationThreshold { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Validate on Entry")]
  [PXDefault(false)]
  public virtual bool? ValidateOnEntry { get; set; }

  [PXDBDateAndTime]
  [PXDefault(typeof (CRValidation.defaultGramValidationDateTime))]
  public virtual DateTime? GramValidationDateTime { get; set; }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRValidation.iD>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRValidation.type>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRValidation.description>
  {
  }

  public abstract class validationThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRValidation.validationThreshold>
  {
  }

  public abstract class validateOnEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRValidation.validateOnEntry>
  {
  }

  public class defaultGramValidationDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Constant<
    #nullable disable
    CRValidation.defaultGramValidationDateTime>
  {
    public defaultGramValidationDateTime()
      : base(CRValidation.DefaultGramValidationDateTime)
    {
    }
  }

  public abstract class gramValidationDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRValidation.gramValidationDateTime>
  {
  }
}
