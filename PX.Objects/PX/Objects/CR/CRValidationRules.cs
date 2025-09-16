// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRValidationRules
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Duplicate Validation Rules")]
[Serializable]
public class CRValidationRules : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Validation Type", Visible = false)]
  [PXDefault(typeof (Current<CRValidation.type>))]
  [ValidationTypes]
  public virtual string ValidationType { get; set; }

  /// <summary>
  /// This field contains full name of the DAC of <see cref="P:PX.Objects.CR.CRValidationRules.MatchingField" /> field.
  /// </summary>
  /// <value>
  /// Full name of DAC (for example, <i>PX.Objects.CR.Contact</i>)
  /// </value>
  [PXDBString(40)]
  [PXDefault("PX.Objects.CR.Contact")]
  [PXUIField]
  public virtual string MatchingEntity { get; set; }

  [PXDBString(60)]
  [PXUIField]
  public virtual string MatchingField { get; set; }

  [PXString(100)]
  [PXDBCalced(typeof (BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<CRValidationRules.matchingEntity, DoubleUnderscore>>, IBqlString>.Concat<CRValidationRules.matchingField>), typeof (string))]
  [PXFormula(typeof (BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<CRValidationRules.matchingEntity, DoubleUnderscore>>, IBqlString>.Concat<CRValidationRules.matchingField>))]
  [PXUIField]
  public virtual string MatchingFieldUI { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "1")]
  [PXUIField(DisplayName = "Score Weight")]
  public virtual Decimal? ScoreWeight { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Transformation Rule")]
  [PXDefault("NO")]
  [TransformationRules]
  public virtual string TransformationRule { get; set; }

  [PXDBString(1)]
  [PXUIField(DisplayName = "Create on Entry")]
  [PXDefault("A")]
  [PX.Objects.CR.CreateOnEntry(true)]
  public virtual string CreateOnEntry { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXNote(IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  public abstract class validationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.validationType>
  {
  }

  public abstract class matchingEntity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.matchingEntity>
  {
  }

  public abstract class matchingField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.matchingField>
  {
  }

  public abstract class matchingFieldUI : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.matchingFieldUI>
  {
  }

  public abstract class scoreWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRValidationRules.scoreWeight>
  {
  }

  public abstract class transformationRule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.transformationRule>
  {
  }

  public abstract class createOnEntry : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.createOnEntry>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRValidationRules.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRValidationRules.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRValidationRules.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRValidationRules.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRValidationRules.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRValidationRules.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRValidationRules.noteID>
  {
  }
}
