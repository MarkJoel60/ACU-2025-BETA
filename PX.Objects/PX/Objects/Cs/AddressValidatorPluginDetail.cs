// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AddressValidatorPluginDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.AddressValidator;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Address Verification Service Details")]
[Serializable]
public class AddressValidatorPluginDetail : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAddressValidatorSetting
{
  public const int Text = 1;
  public const int Combo = 2;
  public const int CheckBox = 3;
  public const int Password = 4;
  public const int ValueFieldLength = 1024 /*0x0400*/;
  protected 
  #nullable disable
  string _AddressValidatorPluginID;
  protected string _SettingID;
  protected int? _SortOrder;
  protected string _Description;
  protected string _Value;
  protected int? _ControlTypeValue;
  protected string _ComboValuesStr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXParent(typeof (Select<AddressValidatorPlugin, Where<AddressValidatorPlugin.addressValidatorPluginID, Equal<Current<AddressValidatorPluginDetail.addressValidatorPluginID>>>>))]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (AddressValidatorPlugin.addressValidatorPluginID))]
  public virtual string AddressValidatorPluginID
  {
    get => this._AddressValidatorPluginID;
    set => this._AddressValidatorPluginID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Enabled = false)]
  public virtual string SettingID
  {
    get => this._SettingID;
    set => this._SettingID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
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
  public virtual int? ControlTypeValue
  {
    get => this._ControlTypeValue;
    set => this._ControlTypeValue = value;
  }

  [PXDBString(4000, IsUnicode = true)]
  public virtual string ComboValuesStr
  {
    get => this._ComboValuesStr;
    set => this._ComboValuesStr = value;
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

  public IDictionary<string, string> ComboValues
  {
    get
    {
      Dictionary<string, string> comboValues = new Dictionary<string, string>();
      string comboValuesStr = this._ComboValuesStr;
      char[] chArray = new char[1]{ ';' };
      foreach (string str in comboValuesStr.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split('|');
          if (strArray.Length == 2)
            comboValues.Add(strArray[0], strArray[1]);
        }
      }
      return (IDictionary<string, string>) comboValues;
    }
    set
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) value)
        stringBuilder.AppendFormat("{0}|{1};", (object) keyValuePair.Key, (object) keyValuePair.Value);
      this._ComboValuesStr = stringBuilder.ToString();
    }
  }

  public AddressValidatorSettingControlType ControlType
  {
    get
    {
      try
      {
        return this._ControlTypeValue.HasValue ? (AddressValidatorSettingControlType) this._ControlTypeValue.Value : (AddressValidatorSettingControlType) 0;
      }
      catch
      {
        return (AddressValidatorSettingControlType) 0;
      }
    }
    set => this._ControlTypeValue = new int?((int) value);
  }

  public abstract class addressValidatorPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.addressValidatorPluginID>
  {
  }

  public abstract class settingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.settingID>
  {
  }

  public abstract class sortOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AddressValidatorPluginDetail.sortOrder>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.description>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressValidatorPluginDetail.value>
  {
  }

  public abstract class controlTypeValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AddressValidatorPluginDetail.controlTypeValue>
  {
  }

  public abstract class comboValuesStr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.comboValuesStr>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AddressValidatorPluginDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AddressValidatorPluginDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AddressValidatorPluginDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressValidatorPluginDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AddressValidatorPluginDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    AddressValidatorPluginDetail.Tstamp>
  {
  }
}
