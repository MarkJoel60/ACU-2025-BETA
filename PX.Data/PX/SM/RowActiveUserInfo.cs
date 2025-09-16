// Decompiled with JetBrains decompiler
// Type: PX.SM.RowActiveUserInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class RowActiveUserInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "SessionId", Visible = false, IsReadOnly = true)]
  public 
  #nullable disable
  string SessionId { get; set; }

  [PXString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "User", IsReadOnly = true)]
  public string User { get; set; }

  [PXString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Company", IsReadOnly = true)]
  public string Company { get; set; }

  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Login Type", IsReadOnly = true)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"All", "User Interface", "API"})]
  public virtual int? LoginType { get; set; }

  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Time From Last Login", IsReadOnly = true)]
  public virtual int? LoginTimeSpan { get; set; }

  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Time From Last Activity", IsReadOnly = true)]
  public virtual int? LastActivity { get; set; }

  public abstract class sessionId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowActiveUserInfo.sessionId>
  {
  }

  public abstract class user : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowActiveUserInfo.user>
  {
  }

  public abstract class company : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowActiveUserInfo.company>
  {
  }

  public abstract class loginType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowActiveUserInfo.loginType>
  {
  }

  public abstract class loginTimeSpan : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowActiveUserInfo.loginTimeSpan>
  {
  }

  public abstract class lastActivity : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowActiveUserInfo.lastActivity>
  {
  }
}
