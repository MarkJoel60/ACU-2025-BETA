// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.FSNotificationSource
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class FSNotificationSource
{
  public const 
  #nullable disable
  string Appointment = "Appt";
  public const string Contract = "Contract";

  public class appointment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSNotificationSource.appointment>
  {
    public appointment()
      : base("Appt")
    {
    }
  }

  public class contract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSNotificationSource.contract>
  {
    public contract()
      : base("Contract")
    {
    }
  }
}
