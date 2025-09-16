// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetGLTransactions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class AssetGLTransactions : PXGraph<
#nullable disable
AssetGLTransactions>
{
  private readonly Dictionary<int?, FixedAsset> _PersistedAssets = new Dictionary<int?, FixedAsset>();
  public PXCancel<GLTranFilter> Cancel;
  public PXFilter<GLTranFilter> Filter;
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  public PXSelect<EPEmployee> Employee;
  public PXSelect<FixedAsset> Assets;
  public PXSelect<FALocationHistory> Locations;
  public PXSelect<FADetails> Details;
  public PXSelect<FABookBalance> Balances;
  public PXSelect<FARegister> Register;
  public PXSelect<FATran, Where<FATran.gLtranID, Equal<Optional<FAAccrualTran.tranID>>, And<FATran.Tstamp, IsNull>>> FATransactions;
  public PXSelect<FABookHist> bookhist;
  public PXSelect<PX.Objects.GL.Sub> Subaccounts;
  public PXSetup<Company> company;
  public PXSetup<FASetup> fasetup;
  public PXSetup<GLSetup> glsetup;
  public PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>> assetNumbering;
  [PXHidden]
  public PXFilter<AssetGLTransactions.Error> StoredError;
  private const string keyPrefix = "*##@";

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodUtils FABookPeriodUtils { get; set; }

  public AssetGLTransactions()
  {
    ((PXSelectBase<FASetup>) this.fasetup).Current = (FASetup) null;
    FASetup current1 = ((PXSelectBase<FASetup>) this.fasetup).Current;
    GLSetup current2 = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      throw new PXSetupNotEnteredException<FASetup>("This operation is not available in initialization mode. To exit the initialization mode, select the '{1}' checkbox on the '{0}' screen.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FASetup.updateGL>(((PXSelectBase) this.fasetup).Cache)
      });
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Current<FASetup.registerNumberingID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (numbering == null)
      throw new PXSetPropertyException("Numbering ID is null.");
    if (numbering.UserNumbering.GetValueOrDefault())
      throw new PXSetPropertyException("Cannot generate the next number. Manual Numbering is activated for '{0}'", new object[1]
      {
        (object) numbering.NumberingID
      });
    PXUIFieldAttribute.SetEnabled<FATran.tranType>(((PXSelectBase) this.FATransactions).Cache, (object) null, false);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (GLTranFilter));
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (AssetGLTransactions.GLTran));
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (FATran));
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (FABookBalance));
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (FixedAsset));
  }

  protected virtual void GLTranFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<GLTranFilter.accountID, GLTranFilter.subID>(e.Row, e.OldRow) && (!PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() || sender.ObjectsEqual<GLTranFilter.branchID>(e.Row, e.OldRow)))
      return;
    this.ClearInserted();
  }

  protected virtual void GLTranFilter_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.ClearInserted();
  }

  protected virtual void GLTranFilter_EmployeeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLTranFilter.branchID>(e.Row);
    sender.SetDefaultExt<GLTranFilter.department>(e.Row);
  }

  protected virtual void FAAccrualTran_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    FAAccrualTran row = (FAAccrualTran) e.Row;
    if (row == null || ((PXSelectBase<GLTranFilter>) this.Filter).Current == null)
      return;
    row.BranchID = ((PXSelectBase<GLTranFilter>) this.Filter).Current.BranchID;
    row.EmployeeID = ((PXSelectBase<GLTranFilter>) this.Filter).Current.EmployeeID;
    row.Department = ((PXSelectBase<GLTranFilter>) this.Filter).Current.Department;
  }

  protected virtual void FAAccrualTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXSelectBase) this.FATransactions).Cache.AllowInsert = e.Row != null;
  }

  public static void SetCurrentRegister(PXSelect<FARegister> _Register, int BranchID)
  {
    FARegister faRegister1 = (FARegister) null;
    foreach (FARegister faRegister2 in ((PXSelectBase) _Register).Cache.Inserted)
    {
      int? branchId = faRegister2.BranchID;
      int num = BranchID;
      if (branchId.GetValueOrDefault() == num & branchId.HasValue)
      {
        faRegister1 = faRegister2;
        break;
      }
    }
    if (faRegister1 != null)
    {
      ((PXSelectBase<FARegister>) _Register).Current = faRegister1;
    }
    else
    {
      string tempKey = AssetGLTransactions.GetTempKey<FARegister.refNbr>(((PXSelectBase) _Register).Cache);
      ((PXSelectBase<FARegister>) _Register).Insert(new FARegister()
      {
        BranchID = new int?(BranchID),
        Origin = "R"
      }).RefNbr = tempKey;
      ((PXSelectBase) _Register).Cache.Normalize();
    }
  }

  protected virtual void FAAccrualTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    FAAccrualTran row = (FAAccrualTran) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    FAAccrualTran faAccrualTran = row;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> selectedAmt1 = (ValueType) row.SelectedAmt;
    bool? selected = row.Selected;
    int? classId;
    Decimal? nullable1;
    PXSetPropertyException propertyException;
    if (selected.GetValueOrDefault())
    {
      classId = row.ClassID;
      if (classId.HasValue)
      {
        Decimal? selectedAmt2 = row.SelectedAmt;
        if (selectedAmt2.HasValue)
        {
          selectedAmt2 = row.SelectedAmt;
          nullable1 = row.GLTranAmt;
          if (selectedAmt2.GetValueOrDefault() > nullable1.GetValueOrDefault() & selectedAmt2.HasValue & nullable1.HasValue)
          {
            propertyException = new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
            {
              (object) row.GLTranAmt
            });
            goto label_8;
          }
        }
      }
    }
    propertyException = (PXSetPropertyException) null;
label_8:
    pxCache.RaiseExceptionHandling<FAAccrualTran.selectedAmt>((object) faAccrualTran, (object) selectedAmt1, (Exception) propertyException);
    if (!sender.ObjectsEqual<FAAccrualTran.selected, FAAccrualTran.classID>(e.Row, e.OldRow))
    {
      selected = row.Selected;
      if (selected.GetValueOrDefault())
      {
        classId = row.ClassID;
        if (classId.HasValue)
        {
          nullable1 = row.OpenQty;
          Decimal num1 = 0M;
          Decimal? nullable2 = nullable1.GetValueOrDefault() > num1 & nullable1.HasValue ? row.OpenQty : new Decimal?(1M);
          int num2 = 0;
          while (true)
          {
            Decimal num3 = (Decimal) num2;
            nullable1 = nullable2;
            Decimal valueOrDefault = nullable1.GetValueOrDefault();
            if (num3 < valueOrDefault & nullable1.HasValue)
            {
              ((PXSelectBase<FATran>) this.FATransactions).Insert(new FATran());
              ++num2;
            }
            else
              break;
          }
          return;
        }
      }
      foreach (FATran faTran in GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.gLtranID, Equal<Current<FAAccrualTran.tranID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Where<FATran>((Func<FATran, bool>) (fatran => ((PXSelectBase) this.FATransactions).Cache.GetStatus((object) fatran) == 2)))
        ((PXSelectBase<FATran>) this.FATransactions).Delete(faTran);
    }
    else
    {
      if (sender.ObjectsEqual<FAAccrualTran.branchID, FAAccrualTran.employeeID, FAAccrualTran.department>(e.Row, e.OldRow))
        return;
      foreach (FATran faTran in GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.gLtranID, Equal<Current<FAAccrualTran.tranID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Where<FATran>((Func<FATran, bool>) (fatran => ((PXSelectBase) this.FATransactions).Cache.GetStatus((object) fatran) == 2)))
      {
        faTran.BranchID = row.BranchID;
        faTran.EmployeeID = row.EmployeeID;
        faTran.Department = row.Department;
        ((PXSelectBase<FATran>) this.FATransactions).Update(faTran);
      }
    }
  }

  protected virtual void FAAccrualTran_BranchID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) ((FAAccrualTran) e.Row).BranchID;
  }

  protected virtual void FAAccrualTran_EmployeeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) ((FAAccrualTran) e.Row).EmployeeID;
  }

  protected virtual void FAAccrualTran_Department_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) ((FAAccrualTran) e.Row).Department;
  }

  protected virtual void FixedAsset_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    this._PersistedAssets[row.AssetID] = row;
  }

  protected virtual void FADetails_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FADetails row = (FADetails) e.Row;
    if (row == null || !((PXSelectBase<FASetup>) this.fasetup).Current.CopyTagFromAssetID.GetValueOrDefault() || (e.Operation & 3) != 2)
      return;
    row.TagNbr = this._PersistedAssets[row.AssetID].AssetCD;
  }

  protected virtual void FASetup_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    FASetup row = (FASetup) e.Row;
    if (!row.CopyTagFromAssetID.GetValueOrDefault())
      return;
    row.TagNumberingID = (string) null;
  }

  protected virtual void FATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    PXCache pxCache1 = sender;
    FATran faTran1 = row;
    bool? nullable = row.NewAsset;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FATran.component>(pxCache1, (object) faTran1, num1 != 0);
    PXCache pxCache2 = sender;
    FATran faTran2 = row;
    nullable = row.Component;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FATran.newAsset>(pxCache2, (object) faTran2, num2 != 0);
    PXCache pxCache3 = sender;
    FATran faTran3 = row;
    nullable = row.NewAsset;
    int num3;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.Component;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.targetAssetID>(pxCache3, (object) faTran3, num3 != 0);
    PXCache pxCache4 = sender;
    FATran faTran4 = row;
    nullable = row.NewAsset;
    int num4;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Component;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.classID>(pxCache4, (object) faTran4, num4 != 0);
    PXCache pxCache5 = sender;
    FATran faTran5 = row;
    nullable = row.NewAsset;
    int num5;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Component;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.branchID>(pxCache5, (object) faTran5, num5 != 0);
    PXCache pxCache6 = sender;
    FATran faTran6 = row;
    nullable = row.NewAsset;
    int num6;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Component;
      num6 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.employeeID>(pxCache6, (object) faTran6, num6 != 0);
    PXCache pxCache7 = sender;
    FATran faTran7 = row;
    nullable = row.NewAsset;
    int num7;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Component;
      num7 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.department>(pxCache7, (object) faTran7, num7 != 0);
    PXCache pxCache8 = sender;
    FATran faTran8 = row;
    nullable = row.NewAsset;
    int num8 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FATran.receiptDate>(pxCache8, (object) faTran8, num8 != 0);
    PXCache pxCache9 = sender;
    FATran faTran9 = row;
    nullable = row.NewAsset;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FATran.deprFromDate>(pxCache9, (object) faTran9, num9 != 0);
    PXCache pxCache10 = sender;
    FATran faTran10 = row;
    nullable = row.NewAsset;
    int num10 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FATran.qty>(pxCache10, (object) faTran10, num10 != 0);
    Numbering numbering = PXResultset<Numbering>.op_Implicit(((PXSelectBase<Numbering>) this.assetNumbering).Select(Array.Empty<object>()));
    PXCache pxCache11 = sender;
    FATran faTran11 = row;
    int num11;
    if (numbering != null)
    {
      nullable = numbering.UserNumbering;
      num11 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num11 = 1;
    PXUIFieldAttribute.SetEnabled<FATran.assetCD>(pxCache11, (object) faTran11, num11 != 0);
  }

  protected virtual void FATran_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    FATran newRow = (FATran) e.NewRow;
    if (newRow == null)
      return;
    FixedAsset asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.AssetID
    }));
    AssetGLTransactions.GLTran glTran = PXResultset<AssetGLTransactions.GLTran>.op_Implicit(PXSelectBase<AssetGLTransactions.GLTran, PXSelect<AssetGLTransactions.GLTran, Where<PX.Objects.GL.GLTran.tranID, Equal<Required<FAAccrualTran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.GLTranID
    }));
    FAAccrualTran faAccrualTran = PXResultset<FAAccrualTran>.op_Implicit(PXSelectBase<FAAccrualTran, PXSelect<FAAccrualTran, Where<FAAccrualTran.tranID, Equal<Required<FAAccrualTran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.GLTranID
    }));
    FixedAsset faClass = this.GetFAClass(newRow);
    if (asset == null || faClass == null || glTran == null)
      return;
    if (!newRow.ClassID.HasValue)
      newRow.ClassID = faClass.AssetID;
    if (!string.IsNullOrEmpty(newRow.TranDesc))
      glTran.TranDesc = newRow.TranDesc;
    bool flag1 = false;
    foreach (string field in (List<string>) sender.Fields)
    {
      if (sender.GetStateExt((object) newRow, field) is PXFieldState stateExt && !string.IsNullOrEmpty(stateExt.Error))
        flag1 = true;
    }
    newRow.Selected = new bool?(!flag1);
    if (sender.ObjectsEqual<FATran.newAsset, FATran.component, FATran.classID, FATran.targetAssetID, FATran.branchID, FATran.employeeID, FATran.department, FATran.qty>(e.NewRow, e.Row) && sender.ObjectsEqual<FATran.receiptDate, FATran.deprFromDate, FATran.tranDesc, FATran.tranAmt, FATran.tranDate, FATran.finPeriodID, FATran.assetCD>(e.NewRow, e.Row))
      return;
    glTran.TranDesc = newRow.TranDesc;
    bool flag2 = ((PXSelectBase) this.Assets).Cache.GetStatus((object) asset) == 2;
    bool? nullable1;
    if (newRow.NewAsset.GetValueOrDefault())
    {
      if (flag2)
        this.DeleteAsset(asset);
      int? assetId = newRow.AssetID;
      try
      {
        int? classId = newRow.ClassID;
        nullable1 = newRow.Component;
        int? _parentID = nullable1.GetValueOrDefault() ? newRow.TargetAssetID : new int?();
        string assetCd = newRow.AssetCD;
        string assetTypeId = faClass.AssetTypeID;
        DateTime? nullable2 = newRow.ReceiptDate;
        DateTime? _recDate = nullable2 ?? glTran.TranDate;
        nullable1 = faClass.UnderConstruction;
        DateTime? _deprFromDate;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = newRow.DeprFromDate;
          _deprFromDate = nullable2 ?? glTran.TranDate;
        }
        else
        {
          nullable2 = new DateTime?();
          _deprFromDate = nullable2;
        }
        Decimal? _cost = flag2 ? newRow.TranAmt : faAccrualTran.UnitCost;
        Decimal? usefulLife = faClass.UsefulLife;
        Decimal? qty = newRow.Qty;
        AssetGLTransactions.GLTran _gltran = glTran;
        FATran _loc = newRow;
        ref int? local = ref assetId;
        AssetGLTransactions.InsertAsset((PXGraph) this, classId, _parentID, assetCd, assetTypeId, _recDate, _deprFromDate, _cost, usefulLife, qty, _gltran, (IFALocation) _loc, out local);
      }
      catch (PXException ex)
      {
        ((PXCache) GraphHelper.Caches<AssetGLTransactions.Error>((PXGraph) this)).Clear();
        GraphHelper.Caches<AssetGLTransactions.Error>((PXGraph) this).Insert(new AssetGLTransactions.Error()
        {
          ErrorMessage = ex.MessageNoPrefix,
          GLTranID = (int?) faAccrualTran?.GLTranID
        });
        throw;
      }
      finally
      {
        newRow.AssetID = assetId;
        newRow.TargetAssetID = newRow.Component.GetValueOrDefault() ? newRow.TargetAssetID : new int?();
      }
      ((PXCache) GraphHelper.Caches<AssetGLTransactions.Error>((PXGraph) this)).Clear();
    }
    else if (newRow.TargetAssetID.HasValue)
    {
      if (flag2)
        this.DeleteAsset(asset);
      newRow.AssetID = newRow.TargetAssetID;
      FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelectJoin<FABookBalance, LeftJoin<FABook, On<FABook.bookID, Equal<FABookBalance.bookID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) asset
      }, Array.Empty<object>()));
      FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) newRow.AssetID
      }));
      FALocationHistory faLocationHistory = PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) faDetails
      }, Array.Empty<object>()));
      newRow.EmployeeID = faLocationHistory.EmployeeID;
      newRow.Department = faLocationHistory.Department;
      if (faBookBalance != null && string.IsNullOrEmpty(faBookBalance.InitPeriod))
        newRow.TranAmt = faDetails.AcquisitionCost;
    }
    sender.SetDefaultExt<FATran.bookID>((object) newRow);
    sender.SetDefaultExt<FATran.finPeriodID>((object) newRow);
    sender.SetDefaultExt<FATran.debitAccountID>((object) newRow);
    sender.SetDefaultExt<FATran.debitSubID>((object) newRow);
    nullable1 = faClass.UnderConstruction;
    if (!nullable1.GetValueOrDefault())
      return;
    sender.SetDefaultExt<FATran.deprFromDate>((object) newRow);
  }

  protected virtual void FATran_AssetCD_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (row.NewAsset.GetValueOrDefault() && (numbering == null || numbering.UserNumbering.GetValueOrDefault()) && string.IsNullOrEmpty(e.NewValue as string))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[assetCD]"
      });
    if (PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelectReadonly<FixedAsset, Where<FixedAsset.assetCD, Equal<Required<FixedAsset.assetCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) != null)
      throw new PXSetPropertyException("A fixed asset with this ID already exists in the system.");
  }

  protected virtual void FATran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    FAAccrualTran current = (FAAccrualTran) ((PXGraph) this).Caches[typeof (FAAccrualTran)].Current;
    if (current == null)
      throw new PXException("GL Transaction must be selected.");
    int? nullable1;
    if (((PXSelectBase<FARegister>) this.Register).Current == null)
    {
      nullable1 = ((PXSelectBase<GLTranFilter>) this.Filter).Current.BranchID;
      if (!nullable1.HasValue)
        throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<GLTranFilter.branchID>(((PXSelectBase) this.Filter).Cache)
        });
      PXSelect<FARegister> register = this.Register;
      nullable1 = ((PXSelectBase<GLTranFilter>) this.Filter).Current.BranchID;
      int BranchID = nullable1.Value;
      AssetGLTransactions.SetCurrentRegister(register, BranchID);
    }
    current.Selected = new bool?(true);
    AssetGLTransactions.GLTran glTran = PXResultset<AssetGLTransactions.GLTran>.op_Implicit(PXSelectBase<AssetGLTransactions.GLTran, PXSelect<AssetGLTransactions.GLTran, Where<PX.Objects.GL.GLTran.tranID, Equal<Current<FAAccrualTran.tranID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new FAAccrualTran[1]
    {
      current
    }, Array.Empty<object>()));
    object[] objArray = new object[1];
    nullable1 = row.ClassID;
    objArray[0] = (object) (nullable1 ?? current.ClassID);
    FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FAAccrualTran.classID>>>>.Config>.Select((PXGraph) this, objArray));
    Decimal? nullable2 = row.TranAmt;
    Decimal num1 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable2 ?? this.GetGLRemainder(current));
    int? assetId = row.AssetID;
    try
    {
      int? _classID;
      if (fixedAsset == null)
      {
        nullable1 = new int?();
        _classID = nullable1;
      }
      else
        _classID = fixedAsset.AssetID;
      nullable1 = new int?();
      int? _parentID = nullable1;
      string assetCd = row.AssetCD;
      string assetTypeId = fixedAsset?.AssetTypeID;
      DateTime? tranDate1 = glTran.TranDate;
      DateTime? tranDate2 = glTran.TranDate;
      Decimal? _cost = new Decimal?(num1);
      Decimal? _usefulLife;
      if (fixedAsset == null)
      {
        nullable2 = new Decimal?();
        _usefulLife = nullable2;
      }
      else
        _usefulLife = fixedAsset.UsefulLife;
      Decimal? qty = row.Qty;
      AssetGLTransactions.GLTran _gltran = glTran;
      FALocationHistory _loc = new FALocationHistory();
      nullable1 = row.BranchID;
      _loc.BranchID = nullable1 ?? current.BranchID;
      nullable1 = row.EmployeeID;
      _loc.EmployeeID = nullable1 ?? current.EmployeeID;
      _loc.Department = row.Department ?? current.Department;
      ref int? local = ref assetId;
      AssetGLTransactions.InsertAsset((PXGraph) this, _classID, _parentID, assetCd, assetTypeId, tranDate1, tranDate2, _cost, _usefulLife, qty, _gltran, (IFALocation) _loc, out local);
    }
    finally
    {
      row.AssetID = assetId;
    }
    bool? nullable3 = current.Selected;
    bool flag = false;
    if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      sender.SetDefaultExt<FATran.bookID>((object) row);
      row.TranDate = glTran.TranDate;
      row.ReceiptDate = glTran.TranDate;
      int num2;
      if (fixedAsset == null)
      {
        num2 = 1;
      }
      else
      {
        nullable3 = fixedAsset.UnderConstruction;
        num2 = !nullable3.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
        row.DeprFromDate = row.ReceiptDate;
      sender.SetDefaultExt<FATran.finPeriodID>((object) row);
      row.TranAmt = new Decimal?(num1);
      row.GLTranID = glTran.TranID;
      row.CreditAccountID = glTran.AccountID;
      row.CreditSubID = glTran.SubID;
      row.TranDesc = glTran.TranDesc;
      row.Origin = ((PXSelectBase<FARegister>) this.Register).Current.Origin;
      if (fixedAsset == null)
        return;
      row.ClassID = fixedAsset.AssetID;
      sender.SetDefaultExt<FATran.debitAccountID>((object) row);
      sender.SetDefaultExt<FATran.debitSubID>((object) row);
    }
  }

  protected virtual void FATran_BookID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_TranDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<FATran.finPeriodID>(e.Row);
  }

  protected virtual void FATran_ReceiptDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    sender.SetDefaultExt<FATran.deprFromDate>((object) row);
    row.TranDate = row.ReceiptDate;
  }

  private FixedAsset GetFAClass(FATran transaction)
  {
    FixedAsset faClass;
    if (transaction.NewAsset.GetValueOrDefault())
      faClass = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) transaction.ClassID
      }));
    else
      faClass = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<BqlField<FixedAsset.classID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) transaction.TargetAssetID
        }))
      }, Array.Empty<object>()));
    return faClass;
  }

  protected virtual void FATran_DeprFromDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    DateTime? receiptDate = row.ReceiptDate;
    if (!receiptDate.HasValue || e.NewValue == null)
      return;
    receiptDate = row.ReceiptDate;
    if (receiptDate.Value.CompareTo(e.NewValue) > 0)
      throw new PXSetPropertyException("The {0} must be equal to or later than the {1}.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FATran.deprFromDate>(sender),
        (object) PXUIFieldAttribute.GetDisplayName<FATran.receiptDate>(sender)
      });
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FATran.tranDate> e)
  {
    FATran row = (FATran) e.Row;
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran.tranDate>, object, object>) e).NewValue == null || row == null)
      return;
    string bookPeriodIdOfDate1 = this.FABookPeriodRepository.GetFABookPeriodIDOfDate((DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran.tranDate>, object, object>) e).NewValue, row.BookID, row.AssetID);
    string bookPeriodIdOfDate2 = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(row.ReceiptDate, row.BookID, row.AssetID);
    string strB = (string) null;
    DateTime? nullable = row.DeprFromDate;
    if (nullable.HasValue)
      strB = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(row.DeprFromDate, row.BookID, row.AssetID);
    FABook faBook = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BookID
    }));
    FixedAsset faClass = this.GetFAClass(row);
    if (string.IsNullOrEmpty(bookPeriodIdOfDate1))
      throw new PXSetPropertyException("Financial period is not defined for the {0} book for {1}.", new object[2]
      {
        (object) faBook.BookCode ?? (object) row.BookID,
        (object) (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran.tranDate>, object, object>) e).NewValue
      });
    if (string.IsNullOrEmpty(bookPeriodIdOfDate2))
    {
      nullable = row.ReceiptDate;
      if (nullable.HasValue)
        throw new PXSetPropertyException("Financial period is not defined for the {0} book for {1}.", new object[2]
        {
          (object) faBook.BookCode ?? (object) row.BookID,
          (object) row.ReceiptDate
        });
    }
    if (faClass != null && faClass.UnderConstruction.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(strB))
    {
      nullable = row.DeprFromDate;
      if (nullable.HasValue)
        throw new PXSetPropertyException("Financial period is not defined for the {0} book for {1}.", new object[2]
        {
          (object) faBook.BookCode ?? (object) row.BookID,
          (object) row.DeprFromDate
        });
    }
    if (string.CompareOrdinal(bookPeriodIdOfDate2, strB) < 0 && string.CompareOrdinal(bookPeriodIdOfDate1, bookPeriodIdOfDate2) < 0 || string.CompareOrdinal(bookPeriodIdOfDate2, strB) >= 0 && string.CompareOrdinal(bookPeriodIdOfDate1, strB) < 0)
    {
      PXUIFieldAttribute.SetVisible<APTran.tranDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache, (object) null);
      throw new PXSetPropertyException("The financial period of the {0} purchasing transaction cannot be earlier than the period of {1} ({2}).", new object[3]
      {
        (object) PeriodIDAttribute.FormatForError(bookPeriodIdOfDate1),
        (object) PXUIFieldAttribute.GetDisplayName<FATran.receiptDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache),
        (object) PeriodIDAttribute.FormatForError(bookPeriodIdOfDate2)
      });
    }
    nullable = row.DeprFromDate;
    DateTime dateTime = nullable.Value;
    if (dateTime.CompareTo(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran.tranDate>, object, object>) e).NewValue) >= 0)
    {
      nullable = row.ReceiptDate;
      dateTime = nullable.Value;
      if (dateTime.CompareTo(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran.tranDate>, object, object>) e).NewValue) <= 0)
        return;
    }
    PXUIFieldAttribute.SetVisible<APTran.tranDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache, (object) null);
    throw new PXSetPropertyException("The {0} of the purchasing transaction must fall between or be equal to the {1} and the {2}.", new object[3]
    {
      (object) PXUIFieldAttribute.GetDisplayName<FATran.tranDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache),
      (object) PXUIFieldAttribute.GetDisplayName<FATran.receiptDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache),
      (object) PXUIFieldAttribute.GetDisplayName<FATran.deprFromDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran.tranDate>>) e).Cache)
    });
  }

  protected virtual void FATran_NewAsset_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    object targetAssetId = (object) row.TargetAssetID;
    try
    {
      sender.RaiseFieldVerifying<FATran.targetAssetID>((object) row, ref targetAssetId);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<FATran.targetAssetID>((object) row, targetAssetId, (Exception) ex);
    }
  }

  protected virtual void FATran_TargetAssetID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (e.NewValue == null && !row.NewAsset.GetValueOrDefault())
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FATran.targetAssetID>(sender)
      });
    if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelectReadonly<FATran, Where<FATran.assetID, Equal<Required<FATran.assetID>>, And<FATran.released, NotEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) != null)
    {
      FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FATran.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        e.NewValue
      }));
      e.NewValue = (object) fixedAsset.AssetCD;
      throw new PXSetPropertyException("The {0} fixed asset contains unreleased transactions. Release them to continue processing the asset.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FATran, FATran.targetAssetID> e)
  {
    FATran row = e.Row;
    if (row == null || e.NewValue == null || row.NewAsset.GetValueOrDefault())
      return;
    FixedAsset fixedAsset = PXSelectorAttribute.Select<FixedAsset.assetID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FATran, FATran.targetAssetID>>) e).Cache, (object) row, e.NewValue) as FixedAsset;
    if (!(row.TranType == "P+") || fixedAsset == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FATran, FATran.targetAssetID>>) e).Cache.SetValueExt<FATran.branchID>((object) row, (object) fixedAsset.BranchID);
  }

  protected virtual void FATran_ClassID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FATran.classID>(sender)
      });
  }

  protected virtual void FATran_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if ((row != null ? (!row.TranDate.HasValue ? 1 : 0) : 1) != 0 || !row.BookID.HasValue)
      return;
    e.NewValue = (object) PeriodIDAttribute.FormatPeriod(this.FABookPeriodRepository.GetFABookPeriodIDOfDate(row.TranDate, row.BookID, row.AssetID));
  }

  protected virtual void FATran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    this.DeleteAsset(PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    })));
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FATran, FATran.branchID> e)
  {
    FATran row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran, FATran.branchID>, FATran, object>) e).NewValue == null || row.NewAsset.GetValueOrDefault() || !(PXSelectorAttribute.Select<FixedAsset.assetID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FATran, FATran.branchID>>) e).Cache, (object) row, (object) row.TargetAssetID) is FixedAsset fixedAsset) || !(row.TranType == "P+"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FATran, FATran.branchID>, FATran, object>) e).NewValue = (object) fixedAsset.BranchID;
  }

  public static string GetTempKey<Field>(PXCache cache) where Field : IBqlField
  {
    int num1 = cache.Inserted.Cast<object>().Select<object, int>((Func<object, int>) (inserted => Convert.ToInt32(((string) cache.GetValue<Field>(inserted)).Substring(4)))).Concat<int>((IEnumerable<int>) new int[1]).Max();
    int num2;
    return "*##@" + Convert.ToString(num2 = num1 + 1);
  }

  public static bool IsTempKey(string key) => key != null && key.StartsWith("*##@");

  public static FixedAsset InsertAsset(
    PXGraph graph,
    int? _classID,
    int? _parentID,
    string _assetCD,
    string _assetTypeID,
    DateTime? _recDate,
    DateTime? _deprFromDate,
    Decimal? _cost,
    Decimal? _usefulLife,
    Decimal? _qty,
    AssetGLTransactions.GLTran _gltran,
    IFALocation _loc,
    out int? assetID)
  {
    if (_assetCD == null)
      _assetCD = AssetGLTransactions.GetTempKey<FixedAsset.assetCD>(graph.Caches[typeof (FixedAsset)]);
    FixedAsset asset = (FixedAsset) null;
    FALocationHistory faLocationHistory1 = (FALocationHistory) null;
    FADetails faDetails = (FADetails) null;
    assetID = new int?();
    try
    {
      asset = (FixedAsset) graph.Caches[typeof (FixedAsset)].Insert((object) new FixedAsset()
      {
        BranchID = _loc.BranchID,
        ClassID = _classID,
        ParentAssetID = _parentID,
        RecordType = "A",
        AssetTypeID = _assetTypeID,
        UsefulLife = _usefulLife,
        Description = (_gltran != null ? _gltran.TranDesc : string.Empty),
        Qty = _qty
      });
      asset.AssetCD = _assetCD;
      graph.Caches[typeof (FixedAsset)].Normalize();
      assetID = asset.AssetID;
      faLocationHistory1 = (FALocationHistory) graph.Caches[typeof (FALocationHistory)].Insert((object) new FALocationHistory()
      {
        AssetID = asset.AssetID,
        BranchID = _loc.BranchID,
        EmployeeID = _loc.EmployeeID,
        Department = _loc.Department,
        TransactionDate = _recDate
      });
      int? revisionId = faLocationHistory1.RevisionID;
      APTran apTran = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.refNbr, Equal<Current<PX.Objects.GL.GLTran.refNbr>>, And<APTran.lineNbr, Equal<Current<PX.Objects.GL.GLTran.tranLineNbr>>, And<APTran.tranType, Equal<Current<PX.Objects.GL.GLTran.tranType>>>>>>.Config>.SelectSingleBound(graph, new object[1]
      {
        (object) _gltran
      }, Array.Empty<object>()));
      faDetails = (FADetails) graph.Caches[typeof (FADetails)].Insert((object) new FADetails()
      {
        AssetID = asset.AssetID,
        ReceiptDate = _recDate,
        DepreciateFromDate = _deprFromDate,
        AcquisitionCost = _cost,
        LocationRevID = revisionId,
        BillNumber = _gltran?.RefNbr,
        PONumber = apTran?.PONbr,
        ReceiptNbr = apTran?.ReceiptNbr,
        ReceiptType = apTran?.ReceiptType
      });
    }
    catch (PXException ex)
    {
      FAAccrualTran current1 = (FAAccrualTran) graph.Caches[typeof (FAAccrualTran)].Current;
      if (current1 != null)
      {
        ((PXCache) GraphHelper.Caches<FAAccrualTran>(graph)).RaiseExceptionHandling((string) null, (object) current1, (object) null, (Exception) new PXSetPropertyException<FAAccrualTran.selected>(((Exception) ex).Message, (PXErrorLevel) 5));
        current1.Selected = new bool?(false);
      }
      FATran current2 = (FATran) graph.Caches[typeof (FATran)].Current;
      if (current2 != null)
      {
        object obj = ((PXCache) GraphHelper.Caches<FATran>(graph)).GetStateExt<FATran.assetID>((object) current2) is PXFieldState stateExt ? stateExt.Value : (object) null;
        ((PXCache) GraphHelper.Caches<FATran>(graph)).RaiseExceptionHandling<FATran.assetID>((object) current2, obj, (Exception) new PXSetPropertyException<FATran.assetID>(((Exception) ex).Message));
        current2.Selected = new bool?(false);
      }
    }
    int? bookID = new int?();
    try
    {
      PXCache cach = graph.Caches[typeof (FABookBalance)];
      foreach (PXResult<FABookSettings> pxResult in PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>>, OrderBy<Desc<FABookSettings.updateGL>>>.Config>.Select(graph, Array.Empty<object>()))
      {
        FABookSettings faBookSettings = PXResult<FABookSettings>.op_Implicit(pxResult);
        FABookBalance faBookBalance = (FABookBalance) cach.Insert((object) new FABookBalance()
        {
          AssetID = asset.AssetID,
          ClassID = _classID,
          BookID = faBookSettings.BookID,
          UsefulLife = faBookSettings.UsefulLife
        });
        object obj;
        cach.RaiseFieldDefaulting<FABookBalance.deprToPeriod>((object) faBookBalance, ref obj);
        faBookBalance.DeprToPeriod = (string) obj;
        bool? nullable;
        if (bookID.HasValue)
        {
          nullable = faBookBalance.UpdateGL;
          if (!nullable.GetValueOrDefault())
            goto label_14;
        }
        bookID = faBookBalance.BookID;
label_14:
        if (string.IsNullOrEmpty(faLocationHistory1.PeriodID))
        {
          FALocationHistory faLocationHistory2 = faLocationHistory1;
          nullable = asset.UnderConstruction;
          string str = nullable.GetValueOrDefault() ? graph.GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(faDetails.ReceiptDate, bookID, asset.AssetID, false) : faBookBalance.DeprFromPeriod;
          faLocationHistory2.PeriodID = str;
        }
      }
    }
    catch (PXException ex)
    {
      FAAccrualTran current3 = (FAAccrualTran) graph.Caches[typeof (FAAccrualTran)].Current;
      if (current3 != null)
      {
        ((PXCache) GraphHelper.Caches<FAAccrualTran>(graph)).RaiseExceptionHandling((string) null, (object) current3, (object) null, (Exception) new PXSetPropertyException<FAAccrualTran.selected>(((Exception) ex).Message, (PXErrorLevel) 5));
        current3.Selected = new bool?(false);
      }
      FATran current4 = (FATran) graph.Caches[typeof (FATran)].Current;
      if (current4 != null)
      {
        object obj = ((PXCache) GraphHelper.Caches<FATran>(graph)).GetStateExt<FATran.classID>((object) current4) is PXFieldState stateExt ? stateExt.Value : (object) null;
        ((PXCache) GraphHelper.Caches<FATran>(graph)).RaiseExceptionHandling<FATran.classID>((object) current4, obj, (Exception) new PXSetPropertyException<FATran.classID>(((Exception) ex).Message));
        current4.Selected = new bool?(false);
      }
    }
    if (asset == null || faLocationHistory1 == null || faDetails == null)
      return (FixedAsset) null;
    asset.FASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(graph.Caches[typeof (FixedAsset)], asset);
    asset.AccumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(graph.Caches[typeof (FixedAsset)], asset);
    asset.DepreciatedExpenseSubID = AssetMaint.MakeSubID<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>(graph.Caches[typeof (FixedAsset)], asset);
    asset.DisposalSubID = AssetMaint.MakeSubID<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>(graph.Caches[typeof (FixedAsset)], asset);
    asset.GainSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>(graph.Caches[typeof (FixedAsset)], asset);
    asset.LossSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>(graph.Caches[typeof (FixedAsset)], asset);
    faLocationHistory1.ClassID = asset.ClassID;
    faLocationHistory1.FAAccountID = asset.FAAccountID;
    faLocationHistory1.FASubID = asset.FASubID;
    faLocationHistory1.AccumulatedDepreciationAccountID = asset.AccumulatedDepreciationAccountID;
    faLocationHistory1.AccumulatedDepreciationSubID = asset.AccumulatedDepreciationSubID;
    faLocationHistory1.DepreciatedExpenseAccountID = asset.DepreciatedExpenseAccountID;
    faLocationHistory1.DepreciatedExpenseSubID = asset.DepreciatedExpenseSubID;
    faLocationHistory1.DisposalAccountID = asset.DisposalAccountID;
    faLocationHistory1.DisposalSubID = asset.DisposalSubID;
    faLocationHistory1.GainAcctID = asset.GainAcctID;
    faLocationHistory1.GainSubID = asset.GainSubID;
    faLocationHistory1.LossAcctID = asset.LossAcctID;
    faLocationHistory1.LossSubID = asset.LossSubID;
    faLocationHistory1.LocationID = asset.BranchID;
    faLocationHistory1.TransactionDate = faDetails.ReceiptDate;
    IFABookPeriodRepository service = graph.GetService<IFABookPeriodRepository>();
    if (bookID.HasValue)
      faLocationHistory1.PeriodID = service.FindFABookPeriodOfDate(faDetails.ReceiptDate, bookID, asset.AssetID)?.FinPeriodID;
    return asset;
  }

  protected virtual void DeleteAsset(FixedAsset asset)
  {
    int? assetId = asset.AssetID;
    int num = 0;
    if (!(assetId.GetValueOrDefault() < num & assetId.HasValue))
      return;
    ((PXSelectBase<FixedAsset>) this.Assets).Delete(asset);
  }

  protected virtual void ClearInserted()
  {
    ((PXSelectBase) this.FATransactions).Cache.Clear();
    ((PXSelectBase) this.Balances).Cache.Clear();
    ((PXSelectBase) this.Details).Cache.Clear();
    ((PXSelectBase) this.Locations).Cache.Clear();
    ((PXSelectBase) this.Assets).Cache.Clear();
  }

  protected virtual Decimal GetGLRemainder(FAAccrualTran ex)
  {
    Decimal? openAmt = ex.OpenAmt;
    Decimal valueOrDefault;
    if (!openAmt.HasValue)
    {
      Decimal? selectedAmt = ex.SelectedAmt;
      valueOrDefault = (selectedAmt.HasValue ? new Decimal?(0M - selectedAmt.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    }
    else
      valueOrDefault = openAmt.GetValueOrDefault();
    Decimal val1 = valueOrDefault;
    return !(val1 > 0M) ? 0M : Math.Min(val1, ex.UnitCost.GetValueOrDefault());
  }

  public virtual void Persist()
  {
    using (IEnumerator<AssetGLTransactions.Error> enumerator = ((PXCache) GraphHelper.Caches<AssetGLTransactions.Error>((PXGraph) this)).Inserted.Cast<AssetGLTransactions.Error>().Where<AssetGLTransactions.Error>((Func<AssetGLTransactions.Error, bool>) (err => !Str.IsNullOrEmpty(err.ErrorMessage))).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        AssetGLTransactions.Error current = enumerator.Current;
        if (current.GLTranID.HasValue)
        {
          FAAccrualTran faAccrualTran1 = new FAAccrualTran()
          {
            GLTranID = current.GLTranID
          };
          FAAccrualTran faAccrualTran2 = GraphHelper.Caches<FAAccrualTran>((PXGraph) this).Locate(faAccrualTran1);
          if (faAccrualTran2 != null)
          {
            PXProcessing<FAAccrualTran>.SetCurrentItem((object) faAccrualTran2);
            PXProcessing<FAAccrualTran>.SetError(current.ErrorMessage);
          }
        }
        throw new PXException(current.ErrorMessage);
      }
    }
    foreach (FATran faTran in ((PXSelectBase) this.FATransactions).Cache.Inserted)
    {
      bool? nullable = faTran.Selected;
      nullable = nullable.GetValueOrDefault() ? faTran.NewAsset : throw new PXException("At least one item has not been processed.");
      if (!nullable.GetValueOrDefault())
      {
        if (faTran.TargetAssetID.HasValue)
        {
          try
          {
            PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, faTran.TargetAssetID, "PC");
            PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, faTran.TargetAssetID, "ZL");
            PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, faTran.TargetAssetID, "LE");
          }
          catch (Exception ex)
          {
            PXProcessing<FAAccrualTran>.SetError(ex.Message);
            throw new PXException(ex.Message);
          }
        }
      }
      try
      {
        object assetCd = (object) faTran.AssetCD;
        ((PXSelectBase) this.FATransactions).Cache.RaiseFieldVerifying<FATran.assetCD>((object) faTran, ref assetCd);
      }
      catch (PXSetPropertyException ex)
      {
        throw new PXException("Cannot create the fixed asset. To create a fixed asset, specify the Asset ID or clear the Manual Numbering check box on the Numbering Sequences (CS201010) form for the fixed asset sequence.");
      }
    }
    foreach (FixedAsset asset in ((PXGraph) this).Caches[typeof (FixedAsset)].Cached)
    {
      if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) asset
      }, Array.Empty<object>())) == null)
        this.DeleteAsset(asset);
    }
    AssetGLTransactions.GLTransactionsViewExtension extension = ((PXGraph) this).GetExtension<AssetGLTransactions.GLTransactionsViewExtension>();
    foreach (PXResult<FAAccrualTran> pxResult1 in ((PXSelectBase<FAAccrualTran>) extension.GLTransactions).Select(Array.Empty<object>()))
    {
      FAAccrualTran faAccrualTran = PXResult<FAAccrualTran>.op_Implicit(pxResult1);
      PXProcessing<FAAccrualTran>.SetCurrentItem((object) faAccrualTran);
      foreach (PXResult<FATran> pxResult2 in ((PXSelectBase<FATran>) this.FATransactions).Select(new object[1]
      {
        (object) faAccrualTran.TranID
      }))
      {
        FATran faTran = PXResult<FATran>.op_Implicit(pxResult2);
        object targetAssetId = (object) faTran.TargetAssetID;
        ((PXSelectBase) this.FATransactions).Cache.RaiseFieldVerifying<FATran.targetAssetID>((object) faTran, ref targetAssetId);
        object classId = (object) faTran.ClassID;
        ((PXSelectBase) this.FATransactions).Cache.RaiseFieldVerifying<FATran.classID>((object) faTran, ref classId);
      }
      if (faAccrualTran.Selected.GetValueOrDefault())
      {
        Decimal? selectedAmt = faAccrualTran.SelectedAmt;
        Decimal? glTranAmt = faAccrualTran.GLTranAmt;
        if (selectedAmt.GetValueOrDefault() > glTranAmt.GetValueOrDefault() & selectedAmt.HasValue & glTranAmt.HasValue)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) faAccrualTran.GLTranAmt
          });
      }
    }
    foreach (FixedAsset fixedAsset in ((PXGraph) this).Caches[typeof (FixedAsset)].Inserted)
    {
      FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>.Order<By<Asc<FABookBalance.updateGL>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], new object[1]
      {
        (object) fixedAsset.AssetID
      }));
      FADetails faDetails = ((PXSelectBase<FADetails>) this.Details).Locate(new FADetails()
      {
        AssetID = fixedAsset.AssetID
      });
      if (faDetails != null)
      {
        FALocationHistory faLocationHistory = ((PXSelectBase<FALocationHistory>) this.Locations).Locate(new FALocationHistory()
        {
          AssetID = faDetails.AssetID,
          RevisionID = faDetails.LocationRevID
        });
        if (faLocationHistory != null)
        {
          faLocationHistory.FAAccountID = fixedAsset.FAAccountID;
          faLocationHistory.FASubID = fixedAsset.FASubID;
          faLocationHistory.AccumulatedDepreciationAccountID = fixedAsset.AccumulatedDepreciationAccountID;
          faLocationHistory.AccumulatedDepreciationSubID = fixedAsset.AccumulatedDepreciationSubID;
          faLocationHistory.DepreciatedExpenseAccountID = fixedAsset.DepreciatedExpenseAccountID;
          faLocationHistory.DepreciatedExpenseSubID = fixedAsset.DepreciatedExpenseSubID;
          faLocationHistory.DisposalAccountID = fixedAsset.DisposalAccountID;
          faLocationHistory.DisposalSubID = fixedAsset.DisposalSubID;
          faLocationHistory.GainAcctID = fixedAsset.GainAcctID;
          faLocationHistory.GainSubID = fixedAsset.GainSubID;
          faLocationHistory.LossAcctID = fixedAsset.LossAcctID;
          faLocationHistory.LossSubID = fixedAsset.LossSubID;
          faLocationHistory.LocationID = fixedAsset.BranchID;
          faLocationHistory.TransactionDate = faDetails.ReceiptDate;
          faLocationHistory.PeriodID = this.FABookPeriodRepository.FindFABookPeriodOfDate(faDetails.ReceiptDate, faBookBalance.BookID, fixedAsset.AssetID)?.FinPeriodID;
        }
      }
    }
    foreach (FAAccrualTran faAccrualTran3 in GraphHelper.RowCast<FAAccrualTran>((IEnumerable) ((PXSelectBase<FAAccrualTran>) extension.GLTransactions).Select(Array.Empty<object>())).Where<FAAccrualTran>((Func<FAAccrualTran, bool>) (ext => ext.Selected.GetValueOrDefault())))
    {
      PXProcessing<FAAccrualTran>.SetCurrentItem((object) faAccrualTran3);
      foreach (PXResult<FATran> pxResult3 in ((PXSelectBase<FATran>) this.FATransactions).Select(new object[1]
      {
        (object) faAccrualTran3.TranID
      }))
      {
        FATran faTran1 = PXResult<FATran>.op_Implicit(pxResult3);
        FADetails faDetails = ((PXSelectBase<FADetails>) this.Details).Locate(new FADetails()
        {
          AssetID = faTran1.AssetID
        });
        FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) faTran1
        }, Array.Empty<object>()));
        bool? nullable;
        if (faTran1.NewAsset.GetValueOrDefault())
        {
          FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>, And<FABookBalance.bookID, Equal<Current<FATran.bookID>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
          {
            (object) faTran1
          }, Array.Empty<object>()));
          FATran faTran2 = faTran1;
          nullable = fixedAsset.UnderConstruction;
          string str = nullable.GetValueOrDefault() ? ((PXGraph) this).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(faDetails.ReceiptDate, faBookBalance.BookID, faBookBalance.AssetID, false) : faBookBalance.DeprFromPeriod;
          faTran2.TranPeriodID = str;
        }
        foreach (PXResult<FABookBalance> pxResult4 in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>, And<FABookBalance.bookID, NotEqual<Current<FATran.bookID>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) faTran1
        }, Array.Empty<object>()))
        {
          FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult4);
          FATran copy = (FATran) ((PXSelectBase) this.FATransactions).Cache.CreateCopy((object) faTran1);
          ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.noteID>((object) copy);
          copy.BookID = faBookBalance.BookID;
          copy.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(((PXSelectBase) this.FATransactions).Cache, (object) ((PXSelectBase<FARegister>) this.Register).Current);
          ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.finPeriodID>((object) copy);
          nullable = faTran1.NewAsset;
          if (nullable.GetValueOrDefault())
          {
            FATran faTran3 = copy;
            nullable = fixedAsset.UnderConstruction;
            string str = nullable.GetValueOrDefault() ? ((PXGraph) this).GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(faDetails.ReceiptDate, faBookBalance.BookID, faBookBalance.AssetID, false) : faBookBalance.DeprFromPeriod;
            faTran3.TranPeriodID = str;
          }
          else
            ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.tranPeriodID>((object) copy);
          ((PXSelectBase) this.FATransactions).Cache.SetStatus((object) copy, (PXEntryStatus) 2);
        }
      }
      FAAccrualTran faAccrualTran4 = faAccrualTran3;
      Decimal? nullable1 = faAccrualTran3.GLTranAmt;
      Decimal? nullable2 = faAccrualTran3.OpenAmt;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      faAccrualTran4.ClosedAmt = nullable3;
      FAAccrualTran faAccrualTran5 = faAccrualTran3;
      nullable2 = faAccrualTran3.GLTranQty;
      nullable1 = faAccrualTran3.OpenQty;
      Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      faAccrualTran5.ClosedQty = nullable4;
      GraphHelper.MarkUpdated(((PXSelectBase) extension.GLTransactions).Cache, (object) faAccrualTran3);
    }
    List<FATran> faTranList = new List<FATran>((IEnumerable<FATran>) ((PXSelectBase) this.FATransactions).Cache.Inserted);
    foreach (FATran faTran in faTranList)
    {
      FATran copy1 = (FATran) ((PXSelectBase) this.FATransactions).Cache.CreateCopy((object) faTran);
      ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.noteID>((object) copy1);
      copy1.TranType = "R+";
      copy1.DebitAccountID = ((PXSelectBase<FASetup>) this.fasetup).Current.FAAccrualAcctID;
      copy1.DebitSubID = ((PXSelectBase<FASetup>) this.fasetup).Current.FAAccrualSubID;
      copy1.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(((PXSelectBase) this.FATransactions).Cache, (object) ((PXSelectBase<FARegister>) this.Register).Current);
      ((PXSelectBase) this.FATransactions).Cache.SetStatus((object) copy1, (PXEntryStatus) 2);
      faTran.CreditAccountID = copy1.DebitAccountID;
      faTran.CreditSubID = copy1.DebitSubID;
      faTran.GLTranID = new int?();
      AssetGLTransactions.GLTran glTran = PXResultset<AssetGLTransactions.GLTran>.op_Implicit(PXSelectBase<AssetGLTransactions.GLTran, PXSelect<AssetGLTransactions.GLTran, Where<PX.Objects.GL.GLTran.tranID, Equal<Current<FATran.gLtranID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) copy1
      }, Array.Empty<object>()));
      FABookBalance bookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>, And<FABookBalance.bookID, Equal<Current<FATran.bookID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) faTran
      }, Array.Empty<object>()));
      FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) bookBalance.DepreciationMethodID
      }));
      FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) faTran
      }, Array.Empty<object>()));
      OrganizationFinPeriod periodInSubledger = this.FABookPeriodUtils.GetNearestOpenOrganizationMappedFABookPeriodInSubledger<OrganizationFinPeriod.fAClosed>(bookBalance.BookID, glTran.BranchID, glTran.FinPeriodID, faTran.BranchID);
      copy1.FinPeriodID = periodInSubledger?.FinPeriodID;
      if (copy1.FinPeriodID == null)
        ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.finPeriodID>((object) copy1);
      bool? nullable = fixedAsset.UnderConstruction;
      copy1.TranPeriodID = !nullable.GetValueOrDefault() ? bookBalance.DeprFromPeriod : copy1.FinPeriodID;
      nullable = bookBalance.UpdateGL;
      if (!nullable.GetValueOrDefault())
        copy1.GLTranID = new int?();
      if (bookBalance.Status == "F" && depreciationMethod.IsPureStraightLine)
      {
        FATran copy2 = (FATran) ((PXSelectBase) this.FATransactions).Cache.CreateCopy((object) faTran);
        ((PXSelectBase) this.FATransactions).Cache.SetDefaultExt<FATran.noteID>((object) copy2);
        copy2.TranType = "C+";
        copy2.CreditAccountID = fixedAsset.AccumulatedDepreciationAccountID;
        copy2.CreditSubID = fixedAsset.AccumulatedDepreciationSubID;
        copy2.DebitAccountID = fixedAsset.DepreciatedExpenseAccountID;
        copy2.DebitSubID = fixedAsset.DepreciatedExpenseSubID;
        copy2.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(((PXSelectBase) this.FATransactions).Cache, (object) ((PXSelectBase<FARegister>) this.Register).Current);
        copy2.GLTranID = new int?();
        ((PXSelectBase) this.FATransactions).Cache.SetStatus((object) copy2, (PXEntryStatus) 2);
      }
      PXAccess.Organization parentOrganization = PXAccess.GetParentOrganization(fixedAsset.BranchID);
      if (!AssetMaint.IsPostingBookBalanceParametersValidInNonMigrationMode((PXGraph) this, (int?) parentOrganization?.OrganizationID, bookBalance))
        throw new PXSetPropertyException("The fixed asset cannot be created because the asset's Depr. to Period is closed in the posting book for the {0} company.", new object[1]
        {
          (object) parentOrganization?.OrganizationCD
        });
    }
    foreach (FATran faTran in ((PXSelectBase) this.FATransactions).Cache.Inserted)
    {
      AssetGLTransactions.SetCurrentRegister(this.Register, faTran.BranchID.Value);
      if (faTran.NewAsset.GetValueOrDefault())
        ((PXSelectBase<FARegister>) this.Register).Current.Origin = "P";
      if (faTran.RefNbr != ((PXSelectBase<FARegister>) this.Register).Current.RefNbr)
      {
        faTran.RefNbr = ((PXSelectBase<FARegister>) this.Register).Current.RefNbr;
        faTran.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(((PXSelectBase) this.FATransactions).Cache, (object) ((PXSelectBase<FARegister>) this.Register).Current);
        ((PXSelectBase) this.FATransactions).Cache.Normalize();
      }
    }
    List<FARegister> list = new List<FARegister>((IEnumerable<FARegister>) ((PXSelectBase) this.Register).Cache.Inserted);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      FARegister faRegister = list[index];
      if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) faRegister
      }, Array.Empty<object>())) == null)
      {
        ((PXSelectBase<FARegister>) this.Register).Delete(faRegister);
        list.RemoveAt(index);
      }
    }
    DocumentList<Batch> documentList = new DocumentList<Batch>((PXGraph) this);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      List<FALocationHistory> faLocationHistoryList = new List<FALocationHistory>((IEnumerable<FALocationHistory>) ((PXSelectBase) this.Locations).Cache.Inserted);
      ((PXGraph) this).Persist();
      foreach (FALocationHistory faLocationHistory in faLocationHistoryList)
      {
        if (((PXSelectBase<FARegister>) this.Register).Current == null)
          AssetGLTransactions.SetCurrentRegister(this.Register, faLocationHistory.BranchID.Value);
        faLocationHistory.RefNbr = ((PXSelectBase<FARegister>) this.Register).Current.RefNbr;
        GraphHelper.MarkUpdated(((PXSelectBase) this.Locations).Cache, (object) faLocationHistory);
      }
      foreach (FATran faTran in faTranList)
      {
        FABookBalance faBookBalance = new FABookBalance()
        {
          AssetID = faTran.AssetID,
          BookID = faTran.BookID
        };
        FABookBalance bookBalance;
        if ((bookBalance = ((PXSelectBase<FABookBalance>) this.Balances).Locate(faBookBalance)) != null)
        {
          FABookHist keyedHistory = new FABookHist();
          keyedHistory.AssetID = bookBalance.AssetID;
          keyedHistory.BookID = bookBalance.BookID;
          keyedHistory.FinPeriodID = bookBalance.DeprFromPeriod ?? faTran.FinPeriodID;
          FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref bookBalance);
          if (string.IsNullOrEmpty(bookBalance.CurrDeprPeriod) && string.IsNullOrEmpty(bookBalance.LastDeprPeriod))
          {
            bookBalance.CurrDeprPeriod = bookBalance.DeprFromPeriod ?? faTran.FinPeriodID;
            ((PXSelectBase<FABookBalance>) this.Balances).Update(bookBalance);
          }
        }
      }
      ((PXGraph) this).Persist();
      if (((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseAsset.GetValueOrDefault())
      {
        ((PXGraph) this).SelectTimeStamp();
        documentList = AssetTranRelease.ReleaseDoc(list, false, false);
      }
      transactionScope.Complete((PXGraph) this);
    }
    PostGraph instance = PXGraph.CreateInstance<PostGraph>();
    foreach (Batch b in (List<Batch>) documentList)
    {
      ((PXGraph) instance).Clear();
      instance.PostBatchProc(b);
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (FARegister.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FARegister, Where<FARegister.refNbr, Equal<Current<FATran.refNbr>>>>))]
  [PXUIField]
  [PXSelector(typeof (FARegister.refNbr))]
  protected virtual void FATran_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  [PXSelector(typeof (Search<FixedAsset.assetID, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description), DirtyRead = true)]
  [PXUIField]
  protected virtual void FATran_AssetID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "New Asset ID")]
  protected virtual void FATran_AssetCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Transaction Amount")]
  [PXFormula(null, typeof (AddCalc<FAAccrualTran.selectedAmt>))]
  protected virtual void FATran_TranAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  [PXFormula(null, typeof (AddCalc<FAAccrualTran.selectedQty>))]
  protected virtual void FATran_Qty_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  [PXCustomizeBaseAttribute]
  protected virtual void FATran_TranDate_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Tran. Period", Enabled = false)]
  [PeriodID(null, null, null, true)]
  protected virtual void FATran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PeriodID(null, null, null, true)]
  [PXFormula(typeof (RowExt<FATran.finPeriodID>))]
  protected virtual void FATran_TranPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (Search<FABookBalance.bookID, Where<FABookBalance.assetID, Equal<Current<FATran.assetID>>>, OrderBy<Desc<FABookBalance.updateGL>>>))]
  [PXUIField]
  protected virtual void FATran_BookID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<FixedAsset.fAAccountID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>))]
  protected virtual void FATran_DebitAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<FixedAsset.fASubID, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>))]
  protected virtual void FATran_DebitSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PXDefault("P+")]
  [FATran.tranType.List]
  protected virtual void FATran_TranType_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXSelector(typeof (Search2<FAClass.assetID, LeftJoin<FABookSettings, On<FAClass.assetID, Equal<FABookSettings.assetID>>, LeftJoin<FABook, On<FABookSettings.bookID, Equal<FABook.bookID>>>>, Where<FAClass.recordType, Equal<FARecordType.classType>, And<FABook.updateGL, Equal<True>>>>), SubstituteKey = typeof (FAClass.assetCD), DescriptionField = typeof (FAClass.description))]
  [PXDefault]
  [PXUIField(DisplayName = "Asset Class", Required = true)]
  protected virtual void FATran_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXSelector(typeof (Search2<FixedAsset.assetID, LeftJoin<FABookBalance, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>, LeftJoin<FABook, On<FABookBalance.bookID, Equal<FABook.bookID>>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>, And<FABook.updateGL, Equal<True>>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
  [PXRestrictor(typeof (Where<FixedAsset.status, NotEqual<FixedAssetStatus.disposed>, And<FixedAsset.status, NotEqual<FixedAssetStatus.reversed>>>), "Disposed and reversed fixed assets cannot be used on this form.", new System.Type[] {})]
  [PXUIField(DisplayName = "Asset ID")]
  protected virtual void FATran_TargetAssetID_CacheAttached(PXCache sender)
  {
  }

  [Branch(typeof (FAAccrualTran.branchID), null, true, true, true, Required = true)]
  protected virtual void FATran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXDefault(typeof (FAAccrualTran.employeeID))]
  [PXUIField(DisplayName = "Custodian")]
  protected virtual void FATran_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (FAAccrualTran.department))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField(DisplayName = "Department", Required = true)]
  protected virtual void FATran_Department_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FADetails.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<PX.Objects.FA.Standalone.FADetails.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.FA.Standalone.FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.depreciationMethodID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.averagingConvention> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<FABookBalance.deprFromDate> e)
  {
  }

  public class GLTransactionsViewExtension : AdditionsViewExtensionBase<AssetGLTransactions>
  {
    [PXFilterable(new System.Type[] {})]
    public PXFilteredProcessing<FAAccrualTran, GLTranFilter> GLTransactions;

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      PXUIFieldAttribute.SetEnabled<FAAccrualTran.classID>(((PXSelectBase) this.GLTransactions).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<FAAccrualTran.branchID>(((PXSelectBase) this.GLTransactions).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<FAAccrualTran.employeeID>(((PXSelectBase) this.GLTransactions).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<FAAccrualTran.department>(((PXSelectBase) this.GLTransactions).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<FAAccrualTran.reconciled>(((PXSelectBase) this.GLTransactions).Cache, (object) null, true);
    }

    public virtual IEnumerable gltransactions()
    {
      return this.GetFAAccrualTransactions(((PXSelectBase<GLTranFilter>) this.Base.Filter).Current, ((PXSelectBase) this.GLTransactions).Cache);
    }

    protected void SetProcessDelegate()
    {
      if (PXLongOperation.Exists(((PXGraph) this.Base).UID))
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      ((PXProcessingBase<FAAccrualTran>) this.GLTransactions).SetProcessDelegate(new PXProcessingBase<FAAccrualTran>.ProcessListDelegate((object) new AssetGLTransactions.GLTransactionsViewExtension.\u003C\u003Ec__DisplayClass3_0()
      {
        new_graph = (PXGraph) GraphHelper.Clone<AssetGLTransactions>(this.Base)
      }, __methodptr(\u003CSetProcessDelegate\u003Eb__0)));
    }

    protected virtual void GLTranFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      this.SetProcessDelegate();
      ((PXProcessing<FAAccrualTran>) this.GLTransactions).SetProcessAllVisible(false);
      PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<GLTranFilter.accountID>(sender, e.Row);
      sender.RaiseExceptionHandling<GLTranFilter.accountID>(e.Row, (object) account?.AccountCD, (Exception) null);
      try
      {
        AccountAttribute.VerifyAccountIsNotControl(account);
        ((PXProcessing<FAAccrualTran>) this.GLTransactions).SetProcessEnabled(true);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<GLTranFilter.accountID>(e.Row, (object) account?.AccountCD, (Exception) ex);
        ((PXProcessing<FAAccrualTran>) this.GLTransactions).SetProcessEnabled(ex.ErrorLevel < 4);
      }
    }

    protected virtual void GLTranFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (sender.ObjectsEqual<GLTranFilter.branchID, GLTranFilter.employeeID, GLTranFilter.department>(e.Row, e.OldRow))
        return;
      GLTranFilter row = (GLTranFilter) e.Row;
      foreach (FAAccrualTran faAccrualTran in ((PXSelectBase) this.GLTransactions).Cache.Cached)
      {
        FAAccrualTran copy = (FAAccrualTran) ((PXSelectBase) this.GLTransactions).Cache.CreateCopy((object) faAccrualTran);
        copy.BranchID = row.BranchID;
        copy.EmployeeID = row.EmployeeID;
        copy.Department = row.Department;
        ((PXSelectBase<FAAccrualTran>) this.GLTransactions).Update(copy);
      }
    }

    protected virtual void FATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      this.SetProcessDelegate();
    }

    protected virtual void FATran_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      this.SetProcessDelegate();
    }

    [PXOverride]
    public virtual void ClearInserted() => ((PXSelectBase) this.GLTransactions).Cache.Clear();
  }

  public class Error : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    public virtual string ErrorMessage { get; set; }

    [PXDBInt]
    public virtual int? GLTranID { get; set; }

    public abstract class errorMessage : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AssetGLTransactions.Error.errorMessage>
    {
    }

    public abstract class gLTranID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AssetGLTransactions.Error.gLTranID>
    {
    }
  }

  [Serializable]
  public class GLTran : PX.Objects.GL.GLTran
  {
    [Branch(null, null, true, true, true, IsDetail = false, DisplayName = "Transaction Branch", Enabled = false)]
    public override int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }
  }
}
