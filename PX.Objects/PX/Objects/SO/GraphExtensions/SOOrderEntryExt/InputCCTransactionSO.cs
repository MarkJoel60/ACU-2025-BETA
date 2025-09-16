// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.InputCCTransactionSO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Extensions.PaymentTransaction;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXHidden]
public class InputCCTransactionSO : InputCCTransaction
{
  protected int? _RecordID;

  [PXInt(IsKey = true)]
  [PXDBDefault(typeof (SOAdjust.recordID))]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  public abstract class recordID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  InputCCTransactionSO.recordID>
  {
  }
}
