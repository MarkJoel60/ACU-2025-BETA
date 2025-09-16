// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents Voucher Entry Code, used to create a document of GL, AP, AR or CA module as a part of a <see cref="T:PX.Objects.GL.GLDocBatch">GL Document Batch</see>.
/// User can manage records of this type on the Voucher Entry Codes (GL106000) form.
/// </summary>
[PXCacheName("GL Transaction Code")]
[Serializable]
public class GLTranCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Module;
  protected string _TranType;
  protected string _TranCode;
  protected string _Descr;
  protected bool? _Active;

  /// <summary>
  /// The code of the module where a document or transaction will be generated according to this entry code.
  /// </summary>
  /// <value>
  /// Allowed values are: <c>"GL"</c>, <c>"AP"</c>, <c>"AR"</c> and <c>"CA"</c>.
  /// </value>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  [VoucherModule.List]
  [PXFieldDescription]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// The type of the document or transaction generated according to the code.
  /// </summary>
  /// <value>
  /// Allowed values set depends on the selected <see cref="P:PX.Objects.GL.GLTranCode.Module" />.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <summary>
  /// The unique voucher code for the selected <see cref="P:PX.Objects.GL.GLTranCode.Module" /> and <see cref="P:PX.Objects.GL.GLTranCode.TranType">Type</see> of the document.
  /// The code is selected by user on the Journal Vouchers (GL304000) form (<see cref="P:PX.Objects.GL.GLTranCode.TranCode" /> field)
  /// when entering the lines of the batch and determines the module and type of the document or transaction
  /// to be created from the corresponding line of the document batch.
  /// Identifies the record of this DAC associated with a <see cref="T:PX.Objects.GL.GLTranCode">line</see> of a <see cref="T:PX.Objects.GL.GLDocBatch">document batch</see>.
  /// Only one code can be created for any combination of <see cref="P:PX.Objects.GL.GLTranCode.Module" /> and <see cref="P:PX.Objects.GL.GLTranCode.TranType">Document/Transaction Type</see>.
  /// </summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">aaaaa")]
  [PXDefault]
  [PXCheckUnique(new Type[] {})]
  [PXUIField]
  public virtual string TranCode
  {
    get => this._TranCode;
    set => this._TranCode = value;
  }

  /// <summary>Description of the entry code.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// Indicates whether the entry code is active.
  /// Only active codes can be used to create documents or transactions.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// If set to <c>false</c>, the entry code won't appear in the list of available codes for the <see cref="P:PX.Objects.GL.GLTranDoc.TranCode" /> field.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  public class PK : PrimaryKeyOf<GLTranCode>.By<GLTranCode.module, GLTranCode.tranType>
  {
    public static GLTranCode Find(
      PXGraph graph,
      string module,
      string tranType,
      PKFindOptions options = 0)
    {
      return (GLTranCode) PrimaryKeyOf<GLTranCode>.By<GLTranCode.module, GLTranCode.tranType>.FindBy(graph, (object) module, (object) tranType, options);
    }
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranCode.module>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranCode.tranType>
  {
  }

  public abstract class tranCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranCode.tranCode>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranCode.descr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranCode.active>
  {
  }
}
