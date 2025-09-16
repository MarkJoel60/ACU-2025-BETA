// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailAccountPluginDetail
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXVirtual]
[PXHidden]
public class EMailAccountPluginDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXDBDefault(typeof (EMailAccount.emailAccountID))]
  public virtual int? EmailAccountID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? SortOrder { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Setting", Enabled = false)]
  public virtual 
  #nullable disable
  string SettingID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  public abstract class emailAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailAccountPluginDetail.emailAccountID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailAccountPluginDetail.sortOrder>
  {
  }

  public abstract class settingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccountPluginDetail.settingID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EMailAccountPluginDetail.description>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EMailAccountPluginDetail.value>
  {
  }
}
