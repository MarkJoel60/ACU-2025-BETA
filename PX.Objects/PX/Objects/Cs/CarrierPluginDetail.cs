// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierPluginDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Carrier Plugin Detail")]
[Serializable]
public class CarrierPluginDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICarrierDetail
{
  public const int Text = 1;
  public const int Combo = 2;
  public const int CheckBox = 3;
  public const int Password = 4;
  public const int ValueFieldLength = 1024 /*0x0400*/;
  protected 
  #nullable disable
  string _CarrierPluginID;
  protected string _DetailID;
  protected string _Descr;
  protected string _Value;
  protected int? _ControlType;
  protected string _ComboValues;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXParent(typeof (Select<CarrierPlugin, Where<CarrierPlugin.carrierPluginID, Equal<Current<CarrierPluginDetail.carrierPluginID>>>>))]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (CarrierPlugin.carrierPluginID))]
  public virtual string CarrierPluginID
  {
    get => this._CarrierPluginID;
    set => this._CarrierPluginID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Enabled = false)]
  public virtual string DetailID
  {
    get => this._DetailID;
    set => this._DetailID = value;
  }

  [PXDBInt]
  [PXLineNbr(typeof (CarrierPlugin.detailLineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? DetailLineNbr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3}, new string[] {"Text", "Combo", "Checkbox"})]
  public virtual int? ControlType
  {
    get => this._ControlType;
    set => this._ControlType = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  public virtual string ComboValues
  {
    get => this._ComboValues;
    set => this._ComboValues = value;
  }

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

  public IList<KeyValuePair<string, string>> GetComboValues()
  {
    List<KeyValuePair<string, string>> comboValues1 = new List<KeyValuePair<string, string>>();
    string comboValues2 = this.ComboValues;
    char[] chArray = new char[1]{ ';' };
    foreach (string str in comboValues2.Split(chArray))
    {
      if (!string.IsNullOrEmpty(str))
      {
        string[] strArray = str.Split('|');
        if (strArray.Length == 2)
          comboValues1.Add(new KeyValuePair<string, string>(strArray[0], strArray[1]));
      }
    }
    return (IList<KeyValuePair<string, string>>) comboValues1;
  }

  public virtual void SetComboValues(IList<KeyValuePair<string, string>> list)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) list)
      stringBuilder.AppendFormat("{0}|{1};", (object) keyValuePair.Key, (object) keyValuePair.Value);
    this.ComboValues = stringBuilder.ToString();
  }

  public string CarrierID
  {
    get => this.CarrierPluginID;
    set => this.CarrierPluginID = value;
  }

  public abstract class carrierPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginDetail.carrierPluginID>
  {
  }

  public abstract class detailID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPluginDetail.detailID>
  {
  }

  public abstract class detailLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CarrierPluginDetail.detailLineNbr>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPluginDetail.descr>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CarrierPluginDetail.value>
  {
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierPluginDetail.controlType>
  {
  }

  public abstract class comboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginDetail.comboValues>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CarrierPluginDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPluginDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CarrierPluginDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPluginDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CarrierPluginDetail.Tstamp>
  {
  }
}
