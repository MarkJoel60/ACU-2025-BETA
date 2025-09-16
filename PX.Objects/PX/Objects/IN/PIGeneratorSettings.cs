// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIGeneratorSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class PIGeneratorSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PIClassID;
  protected int? _SiteID;
  protected string _Descr;
  protected string _Method;
  protected string _SelectedMethod;
  protected bool? _ByFrequency;
  protected string _CycleID;
  protected string _ABCCodeID;
  protected string _MovementClassID;
  protected short? _BlankLines;
  protected short? _RandomItemsLimit;
  protected short? _LastCountPeriod;
  public int RandomSeed;
  protected DateTime? _MaxLastCountDate;

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INPIClass.pIClassID>))]
  [PXDefault]
  public virtual string PIClassID
  {
    get => this._PIClassID;
    set => this._PIClassID = value;
  }

  [PXDefault]
  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDefault]
  [PXString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PIMethod.List]
  public virtual string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Selection Method", Enabled = false)]
  [PIInventoryMethod.List]
  public virtual string SelectedMethod
  {
    get => this._SelectedMethod;
    set => this._SelectedMethod = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "By Frequency")]
  public virtual bool? ByFrequency
  {
    get => this._ByFrequency;
    set => this._ByFrequency = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INPICycle.cycleID), DescriptionField = typeof (INPICycle.descr))]
  [PXUIField(DisplayName = "Cycle ID")]
  public virtual string CycleID
  {
    get => this._CycleID;
    set => this._CycleID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXSelector(typeof (INABCCode.aBCCodeID), DescriptionField = typeof (INABCCode.descr))]
  [PXUIField(DisplayName = "ABC Code")]
  public virtual string ABCCodeID
  {
    get => this._ABCCodeID;
    set => this._ABCCodeID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXSelector(typeof (INMovementClass.movementClassID), DescriptionField = typeof (INMovementClass.descr))]
  [PXUIField(DisplayName = "Movement Class ID")]
  public virtual string MovementClassID
  {
    get => this._MovementClassID;
    set => this._MovementClassID = value;
  }

  [PXDefault(0)]
  [PXDBShort(MinValue = 0, MaxValue = 10000)]
  [PXUIField(DisplayName = "Blank Lines To Append")]
  public virtual short? BlankLines
  {
    get => this._BlankLines;
    set => this._BlankLines = value;
  }

  [PXDefault(0)]
  [PXShort(MinValue = 0, MaxValue = 10000)]
  [PXUIField(DisplayName = "Max. Number of Items")]
  public virtual short? RandomItemsLimit
  {
    get => this._RandomItemsLimit;
    set => this._RandomItemsLimit = value;
  }

  [PXDefault(0)]
  [PXDBShort(MinValue = 0, MaxValue = 10000)]
  [PXUIField]
  public virtual short? LastCountPeriod
  {
    get => this._LastCountPeriod;
    set => this._LastCountPeriod = value;
  }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Last Count Not Later")]
  public virtual DateTime? MaxLastCountDate
  {
    get => this._MaxLastCountDate;
    set => this._MaxLastCountDate = value;
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIGeneratorSettings.pIClassID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PIGeneratorSettings.siteID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIGeneratorSettings.descr>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIGeneratorSettings.method>
  {
  }

  public abstract class selectedMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PIGeneratorSettings.selectedMethod>
  {
  }

  public abstract class byFrequency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PIGeneratorSettings.byFrequency>
  {
  }

  public abstract class cycleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIGeneratorSettings.cycleID>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PIGeneratorSettings.aBCCodeID>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PIGeneratorSettings.movementClassID>
  {
  }

  public abstract class blankLines : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PIGeneratorSettings.blankLines>
  {
  }

  public abstract class randomItemsLimit : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PIGeneratorSettings.randomItemsLimit>
  {
  }

  public abstract class lastCountPeriod : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PIGeneratorSettings.lastCountPeriod>
  {
  }

  public abstract class maxLastCountDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PIGeneratorSettings.maxLastCountDate>
  {
  }
}
