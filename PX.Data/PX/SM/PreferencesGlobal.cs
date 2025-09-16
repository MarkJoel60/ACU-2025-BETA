// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesGlobal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXHidden]
[PXPrimaryGraph(typeof (PreferencesGeneralMaint))]
[Serializable]
public class PreferencesGlobal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private const 
  #nullable disable
  string LogoutTimeoutDisplayName = "User Inactivity Timeout";

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  public int? SingleRowKey { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Send Diagnostics & Usage Data to Acumatica", Visible = false)]
  public bool? EnableTelemetry { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use WebConfig Value")]
  public bool? UsePreconfiguredTimeouts { get; set; }

  [PXDBInt]
  [PXDefault(60)]
  [PXUIField(DisplayName = "User Inactivity Timeout")]
  [PXIntList(new int[] {15, 30, 45, 60, 90, 120, 180, 240 /*0xF0*/, 300, 360, 420, 480}, new string[] {"15 min", "30 min", "45 min", "1 h", "1 h 30 min", "2 h", "3 h", "4 h", "5 h", "6 h", "7 h", "8 h"})]
  public int? LogoutTimeout { get; set; }

  [PXString]
  [PXUIField(DisplayName = "User Inactivity Timeout", Visible = false, Enabled = false)]
  public string PreconfiguredLogoutTimeout { get; set; }

  [PXDBBool]
  [PXUIField(Visible = false)]
  public bool? ModernUIForProduction { get; set; }

  public abstract class singleRowKey : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PreferencesGlobal.singleRowKey>
  {
  }

  public abstract class enableTelemetry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesGlobal.enableTelemetry>
  {
  }

  public abstract class usePreconfiguredTimeouts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesGlobal.usePreconfiguredTimeouts>
  {
  }

  public abstract class logoutTimeout : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PreferencesGlobal.logoutTimeout>
  {
  }

  public abstract class preconfiguredLogoutTimeout : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGlobal.preconfiguredLogoutTimeout>
  {
  }

  public abstract class modernUIForProduction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesGlobal.modernUIForProduction>
  {
  }
}
