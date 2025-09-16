// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Simple.EPEmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP.Simple;

/// <summary>Represents a simple version of an Employee class.</summary>
[PXHidden]
[Serializable]
public class EPEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;

  /// <summary>The identifier of the employee.</summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.SM.Users">Users</see> to be used for the employee to sign into the system.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Employee Login", Visibility = PXUIVisibility.Visible)]
  public virtual Guid? UserID { get; set; }

  public abstract class bAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.bAccountID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.userID>
  {
  }
}
