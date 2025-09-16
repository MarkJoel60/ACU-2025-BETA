// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountGroupRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PM Account Group Rate")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAccountGroupRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RateDefinitionID;
  protected 
  #nullable disable
  string _RateCodeID;
  protected int? _AccountGroupID;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateDefinitionID))]
  [PXParent(typeof (Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMAccountGroupRate.rateDefinitionID>>, And<PMRateSequence.rateCodeID, Equal<Current<PMAccountGroupRate.rateCodeID>>>>>))]
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

  [PXDefault]
  [AccountGroup(IsKey = true)]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  public abstract class rateDefinitionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAccountGroupRate.rateDefinitionID>
  {
  }

  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAccountGroupRate.rateCodeID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMAccountGroupRate.accountGroupID>
  {
  }
}
