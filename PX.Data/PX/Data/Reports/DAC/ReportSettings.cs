// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.ReportSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class ReportSettings : PXSettingProvider.ReportSettings
{
  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "User")]
  [PXSelector(typeof (Users.username), DescriptionField = typeof (Users.displayName))]
  public virtual 
  #nullable disable
  string Username { get; set; }

  [PXDBBinary(500)]
  public byte[] MailValues { get; set; }

  public abstract class username : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUReportProcess.ReportSettings.username>
  {
  }

  public abstract class mailValues : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ReportSettings.mailValues>
  {
  }

  public class shared : BqlType<
  #nullable enable
  IBqlBool, bool>.Constant<
  #nullable disable
  ReportSettings.shared>
  {
    public shared()
      : base(true)
    {
    }
  }
}
