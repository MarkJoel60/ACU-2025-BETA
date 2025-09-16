// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

[Serializable]
public class TranslationEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TranslDefId;
  protected string _FinPeriodID;
  protected bool? _Unreleased;
  protected bool? _Released;
  protected bool? _Voided;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Translation ID", Required = false)]
  [PXSelector(typeof (TranslDef.translDefId), DescriptionField = typeof (TranslDef.description))]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  [ClosedPeriod]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include Unreleased")]
  public bool? Unreleased
  {
    get => this._Unreleased;
    set => this._Unreleased = value;
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Include Released")]
  public bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Voided")]
  public bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  public abstract class translDefId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationEnqFilter.translDefId>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationEnqFilter.finPeriodID>
  {
  }

  public abstract class unreleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationEnqFilter.unreleased>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationEnqFilter.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationEnqFilter.voided>
  {
  }
}
