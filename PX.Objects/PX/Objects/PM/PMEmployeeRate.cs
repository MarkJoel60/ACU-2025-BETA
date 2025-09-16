// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMEmployeeRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Item Employee")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMEmployeeRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RateDefinitionID;
  protected 
  #nullable disable
  string _RateCodeID;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateDefinitionID))]
  [PXParent(typeof (Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMEmployeeRate.rateDefinitionID>>, And<PMRateSequence.rateCodeID, Equal<Current<PMEmployeeRate.rateCodeID>>>>>))]
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

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID")]
  [PXEPEmployeeSelector]
  public virtual int? EmployeeID { get; set; }

  public abstract class rateDefinitionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMEmployeeRate.rateDefinitionID>
  {
  }

  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMEmployeeRate.rateCodeID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMEmployeeRate.employeeID>
  {
  }
}
