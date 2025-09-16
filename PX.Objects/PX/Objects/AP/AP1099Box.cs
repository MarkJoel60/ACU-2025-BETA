// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099Box
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Represents a type of 1099 payment, which generally corresponds to a box on the 1099-MISC form.
/// This DAC is used by Acumatica ERP to track 1099-related payments.
/// 1099 boxes are configured through the Accounts Payable Preferences (AP101000) form.
/// </summary>
[PXCacheName("AP 1099 Box")]
[Serializable]
public class AP1099Box : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected short? _BoxNbr;
  protected 
  #nullable disable
  string _Descr;
  protected Decimal? _MinReportAmt;
  protected int? _AccountID;
  protected int? _OldAccountID;
  protected byte[] _tstamp;

  /// <summary>
  /// The line number, which is automatically added. A box is used for each payment made to a 1099 vendor.
  /// </summary>
  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "1099 Box", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
  public virtual short? BoxNbr
  {
    get => this._BoxNbr;
    set => this._BoxNbr = value;
  }

  [PXDBString(20, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "1099 Box", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string BoxCD { get; set; }

  /// <summary>
  /// The description of the 1099 type, which usually is based on the box name on the 1099-MISC form.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// The minimum payment amount for the 1099 type to be included for reporting.
  /// </summary>
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Minimum Report Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? MinReportAmt
  {
    get => this._MinReportAmt;
    set => this._MinReportAmt = value;
  }

  /// <summary>
  /// The optional default expense account associated with this type of 1099 payment.
  /// </summary>
  /// <value>
  /// Serves as a link to <see cref="T:PX.Objects.GL.Account" />.
  /// </value>
  [UnboundAccount(DisplayName = "Account", Visibility = PXUIVisibility.Visible)]
  [AvoidControlAccounts]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// System field used to perform two sided update of the 1099Box-<see cref="T:PX.Objects.GL.Account" /> relation.
  /// </summary>
  [PXInt]
  public virtual int? OldAccountID
  {
    get => this._OldAccountID;
    set => this._OldAccountID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<AP1099Box>.By<AP1099Box.boxNbr>
  {
    public static AP1099Box Find(PXGraph graph, short? boxNbr, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AP1099Box>.By<AP1099Box.boxNbr>.FindBy(graph, (object) boxNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<AP1099Box>.By<AP1099Box.boxCD>
  {
    public static AP1099Box Find(PXGraph graph, string boxCD, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AP1099Box>.By<AP1099Box.boxCD>.FindBy(graph, (object) boxCD, options);
    }
  }

  public static class FK
  {
    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<AP1099Box>.By<AP1099Box.accountID>
    {
    }
  }

  public abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AP1099Box.boxNbr>
  {
  }

  /// <summary>
  /// Key field.
  /// The user-friendly unique identifier of the 1099 Box ID.
  /// </summary>
  public abstract class boxCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Box.boxCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099Box.descr>
  {
  }

  public abstract class minReportAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AP1099Box.minReportAmt>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099Box.accountID>
  {
  }

  public abstract class oldAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099Box.oldAccountID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AP1099Box.Tstamp>
  {
  }
}
