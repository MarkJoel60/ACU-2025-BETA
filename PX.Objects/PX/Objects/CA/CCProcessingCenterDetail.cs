// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenterDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Collections.Generic;
using System.Text;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Credit Card Processing Center Detail")]
[Serializable]
public class CCProcessingCenterDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int ValueFieldLength = 1024 /*0x0400*/;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterDetail.processingCenterID>>>>))]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID")]
  public virtual string DetailID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsEncryptionRequired { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsEncrypted { get; set; }

  [PXRSACryptStringWithConditional(1024 /*0x0400*/, typeof (CCProcessingCenterDetail.isEncryptionRequired), typeof (CCProcessingCenterDetail.isEncrypted))]
  [PXDBDefault]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField]
  [ControlTypeDefintion.List]
  public virtual int? ControlType { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  public virtual string ComboValues { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public virtual ICollection<KeyValuePair<string, string>> ComboValuesCollection
  {
    get
    {
      return (ICollection<KeyValuePair<string, string>>) CCProcessingCenterDetail.GetComboValues(this);
    }
    set => CCProcessingCenterDetail.SetComboValues(this, value);
  }

  private static IList<KeyValuePair<string, string>> GetComboValues(CCProcessingCenterDetail detail)
  {
    List<KeyValuePair<string, string>> comboValues1 = new List<KeyValuePair<string, string>>();
    if (detail.ComboValues != null)
    {
      string comboValues2 = detail.ComboValues;
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
    }
    return (IList<KeyValuePair<string, string>>) comboValues1;
  }

  private static void SetComboValues(
    CCProcessingCenterDetail detail,
    ICollection<KeyValuePair<string, string>> collection)
  {
    if (collection != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) collection)
        stringBuilder.AppendFormat("{0}|{1};", (object) keyValuePair.Key, (object) keyValuePair.Value);
      detail.ComboValues = stringBuilder.ToString();
    }
    else
      detail.ComboValues = (string) null;
  }

  public static void Copy(PluginSettingDetail src, CCProcessingCenterDetail dst)
  {
    dst.DetailID = src.DetailID;
    dst.Value = src.Value;
    dst.Descr = src.Descr;
    dst.ControlType = src.ControlType;
    dst.IsEncryptionRequired = src.IsEncryptionRequired;
    dst.ComboValuesCollection = src.ComboValuesCollection;
  }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterDetail>.By<CCProcessingCenterDetail.processingCenterID, CCProcessingCenterDetail.detailID>
  {
    public static CCProcessingCenterDetail Find(
      PXGraph graph,
      string processingCenterID,
      string detailID,
      PKFindOptions options = 0)
    {
      return (CCProcessingCenterDetail) PrimaryKeyOf<CCProcessingCenterDetail>.By<CCProcessingCenterDetail.processingCenterID, CCProcessingCenterDetail.detailID>.FindBy(graph, (object) processingCenterID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCProcessingCenterDetail>.By<CCProcessingCenterDetail.processingCenterID>
    {
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterDetail.processingCenterID>
  {
  }

  public abstract class detailID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterDetail.detailID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcessingCenterDetail.descr>
  {
  }

  public abstract class isEncryptionRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenterDetail.isEncryptionRequired>
  {
  }

  public abstract class isEncrypted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenterDetail.isEncrypted>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcessingCenterDetail.value>
  {
  }

  public abstract class controlType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenterDetail.controlType>
  {
  }

  public abstract class comboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterDetail.comboValues>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CCProcessingCenterDetail.Tstamp>
  {
  }
}
