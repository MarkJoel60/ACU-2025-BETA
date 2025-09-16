// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskRate
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

[PXCacheName("PM Task Rate")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTaskRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RateDefinitionID;
  protected 
  #nullable disable
  string _RateCodeID;
  protected string _TaskCD;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMRateSequence.rateDefinitionID))]
  [PXParent(typeof (Select<PMRateSequence, Where<PMRateSequence.rateDefinitionID, Equal<Current<PMTaskRate.rateDefinitionID>>, And<PMRateSequence.rateCodeID, Equal<Current<PMTaskRate.rateCodeID>>>>>))]
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

  [PXDimensionWildcard("PROTASK", typeof (Search4<PMTask.taskCD, Aggregate<GroupBy<PMTask.taskCD>>>), new Type[] {typeof (PMTask.taskCD), typeof (PMTask.description)})]
  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Project Task")]
  public virtual string TaskCD
  {
    get => this._TaskCD;
    set => this._TaskCD = value;
  }

  public abstract class rateDefinitionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaskRate.rateDefinitionID>
  {
  }

  public abstract class rateCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaskRate.rateCodeID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTaskRate.taskCD>
  {
  }
}
