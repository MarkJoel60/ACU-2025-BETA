// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Project Rate")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMProjectRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RateDefinitionID;
  protected 
  #nullable disable
  string _RateCodeID;
  protected string _ProjectCD;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateDefinitionID))]
  [PXParent(typeof (Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMProjectRate.rateDefinitionID>>, And<PMRateSequence.rateCodeID, Equal<Current<PMProjectRate.rateCodeID>>>>>))]
  public virtual int? RateDefinitionID
  {
    get => this._RateDefinitionID;
    set => this._RateDefinitionID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateCodeID))]
  public virtual string RateCodeID
  {
    get => this._RateCodeID;
    set => this._RateCodeID = value;
  }

  [PXDimensionWildcard("PROJECT", typeof (Search<PMProject.contractCD, Where<PMProject.baseType, Equal<CTPRType.project>>>), new Type[] {typeof (PMProject.contractCD), typeof (PMProject.description)})]
  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Project")]
  public virtual string ProjectCD
  {
    get => this._ProjectCD;
    set => this._ProjectCD = value;
  }

  public abstract class rateDefinitionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProjectRate.rateDefinitionID>
  {
  }

  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectRate.rateCodeID>
  {
  }

  public abstract class projectCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMProjectRate.projectCD>
  {
  }
}
