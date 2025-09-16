// Decompiled with JetBrains decompiler
// Type: PX.SM.ReportUsers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class ReportUsers : Users
{
  [PXString]
  [PXUIField(DisplayName = "Username")]
  [SMReportSubstitute(typeof (ReportUsers.username), typeof (ReportUsers.username))]
  public virtual 
  #nullable disable
  string FriendlyName { get; set; }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReportUsers.username>
  {
  }

  public abstract class friendlyName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReportUsers.friendlyName>
  {
  }
}
