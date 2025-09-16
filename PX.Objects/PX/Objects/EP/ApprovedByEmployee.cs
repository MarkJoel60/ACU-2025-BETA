// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ApprovedByEmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class ApprovedByEmployee : EPEmployee
{
  /// <inheritdoc />
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Approved by (ID)")]
  public override 
  #nullable disable
  string AcctCD
  {
    get => base.AcctCD;
    set => base.AcctCD = value;
  }

  /// <inheritdoc />
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Approved By")]
  public override string AcctName
  {
    get => base.AcctName;
    set => base.AcctName = value;
  }

  public new abstract class defContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ApprovedByEmployee.defContactID>
  {
  }

  public new abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ApprovedByEmployee.userID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ApprovedByEmployee.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ApprovedByEmployee.acctName>
  {
  }
}
