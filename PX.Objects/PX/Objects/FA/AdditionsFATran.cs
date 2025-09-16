// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AdditionsFATran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.FA;

public class AdditionsFATran : PXGraph<AdditionsFATran>
{
  public PXSelect<FixedAsset> Assets;
  public PXSelect<FALocationHistory> Locations;
  public PXSelect<FADetails> Details;
  public PXSelect<FABookBalance> Balances;
  public PXSelect<FARegister> Register;
  public PXSelect<FAAccrualTran> Additions;
  public PXSelect<FATran> FATransactions;
  public PXSetup<FASetup> fasetup;
  public PXSetup<GLSetup> glsetup;
  public PXSetup<Company> company;

  public AdditionsFATran()
  {
    FASetup current1 = ((PXSelectBase<FASetup>) this.fasetup).Current;
    GLSetup current2 = ((PXSelectBase<GLSetup>) this.glsetup).Current;
  }

  protected virtual void InsertFATransactions(
    FixedAsset asset,
    DateTime? date,
    Decimal? amt,
    PX.Objects.GL.GLTran gltran)
  {
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      ((PXSelectBase<FATran>) this.FATransactions).Insert(new FATran()
      {
        AssetID = asset.AssetID,
        BookID = faBookBalance.BookID,
        TranAmt = amt,
        TranDate = date,
        TranType = "P+",
        CreditAccountID = asset.FAAccrualAcctID,
        CreditSubID = asset.FAAccrualSubID,
        DebitAccountID = asset.FAAccountID,
        DebitSubID = asset.FASubID,
        TranDesc = gltran.TranDesc
      });
      FATran faTran = new FATran();
      faTran.AssetID = asset.AssetID;
      faTran.BookID = faBookBalance.BookID;
      faTran.TranAmt = amt;
      faTran.TranDate = date;
      bool? nullable = faBookBalance.UpdateGL;
      faTran.GLTranID = nullable.GetValueOrDefault() ? gltran.TranID : new int?();
      faTran.TranType = "R+";
      faTran.CreditAccountID = gltran.AccountID;
      faTran.CreditSubID = gltran.SubID;
      faTran.DebitAccountID = asset.FAAccrualAcctID;
      faTran.DebitSubID = asset.FAAccrualSubID;
      faTran.TranDesc = gltran.TranDesc;
      ((PXSelectBase<FATran>) this.FATransactions).Insert(faTran);
      if (string.IsNullOrEmpty(faBookBalance.CurrDeprPeriod))
      {
        nullable = asset.Depreciable;
        if (nullable.GetValueOrDefault())
        {
          faBookBalance.CurrDeprPeriod = faBookBalance.DeprFromPeriod;
          ((PXSelectBase<FABookBalance>) this.Balances).Update(faBookBalance);
        }
      }
    }
  }

  public virtual void InsertNewComponent(
    FixedAsset parentAsset,
    FixedAsset cls,
    DateTime? date,
    Decimal? amt,
    Decimal? qty,
    IFALocation loc,
    AssetGLTransactions.GLTran gltran)
  {
    date = date ?? gltran.TranDate;
    FixedAsset asset = AssetGLTransactions.InsertAsset((PXGraph) this, cls.AssetID, parentAsset.AssetID, (string) null, cls.AssetTypeID, date, date, amt, cls.UsefulLife, qty, gltran, loc, out int? _);
    if (((PXSelectBase<FARegister>) this.Register).Current == null)
    {
      AssetGLTransactions.SetCurrentRegister(this.Register, asset.BranchID.Value);
      ((PXSelectBase<FARegister>) this.Register).Current.Origin = "P";
    }
    this.InsertFATransactions(asset, date, amt, (PX.Objects.GL.GLTran) gltran);
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseAsset.GetValueOrDefault() || ((PXSelectBase<FARegister>) this.Register).Current == null)
      return;
    ((PXGraph) this).SelectTimeStamp();
    AssetTranRelease.ReleaseDoc(new List<FARegister>()
    {
      ((PXSelectBase<FARegister>) this.Register).Current
    }, false);
  }

  protected virtual void FATran_AssetID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_BookID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }
}
