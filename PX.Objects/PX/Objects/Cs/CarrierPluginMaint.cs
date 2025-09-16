// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierPluginMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Archiver;
using PX.CarrierService;
using PX.Data;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CS;

public class CarrierPluginMaint : PXGraph<CarrierPluginMaint, CarrierPlugin>
{
  public PXSelect<CarrierPlugin> Plugin;
  public PXSelect<CarrierPluginDetail> Details;
  public PXSelect<CarrierPluginDetail, Where<CarrierPluginDetail.carrierPluginID, Equal<Current<CarrierPlugin.carrierPluginID>>>> SelectDetails;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSelect<CarrierPluginCustomer, Where<CarrierPluginCustomer.carrierPluginID, Equal<Current<CarrierPlugin.carrierPluginID>>>> CustomerAccounts;
  public PXAction<CarrierPlugin> certify;
  public PXAction<CarrierPlugin> test;

  protected IEnumerable details()
  {
    this.ImportSettings();
    return (IEnumerable) ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Select(Array.Empty<object>());
  }

  public virtual void ImportSettings()
  {
    if (((PXSelectBase<CarrierPlugin>) this.Plugin).Current == null)
      return;
    CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService((PXGraph) this, ((PXSelectBase<CarrierPlugin>) this.Plugin).Current);
    if (!carrierService.IsSuccess)
      return;
    this.InsertDetails(carrierService.Result.ExportSettings());
  }

  [PXUIField]
  [PXProcessButton]
  public virtual void Certify()
  {
    if (((PXSelectBase<CarrierPlugin>) this.Plugin).Current == null)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CarrierPluginMaint.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new CarrierPluginMaint.\u003C\u003Ec__DisplayClass8_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.currentRow = ((PXSelectBase<CarrierPlugin>) this.Plugin).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass80, __methodptr(\u003CCertify\u003Eb__0)));
  }

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual void Test()
  {
    if (((PXSelectBase<CarrierPlugin>) this.Plugin).Current == null)
      return;
    ((PXAction) this.Save).Press();
    CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService((PXGraph) this, ((PXSelectBase<CarrierPlugin>) this.Plugin).Current, true);
    if (!carrierService.IsSuccess)
      return;
    CarrierResult<string> carrierResult = carrierService.Result.Test();
    if (carrierResult.IsSuccess)
    {
      ((PXSelectBase<CarrierPlugin>) this.Plugin).Ask(((PXSelectBase<CarrierPlugin>) this.Plugin).Current, "Connection", "The connection to the carrier was successful.", (MessageButtons) 0, (MessageIcon) 4);
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Message message in (IEnumerable<Message>) carrierResult.Messages)
        stringBuilder.AppendLine(message.Description);
      if (stringBuilder.Length > 0)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The test has failed. Details: {0}.", new object[1]
        {
          (object) stringBuilder.ToString()
        }));
    }
  }

  private void PrepareCertificationData(CarrierPlugin cp)
  {
    CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService((PXGraph) this, cp, true);
    if (!carrierService.IsSuccess)
      return;
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    CarrierResult<IList<CarrierCertificationData>> certificationData1 = carrierService.Result.GetCertificationData();
    if (certificationData1 == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Message message in (IEnumerable<Message>) certificationData1.Messages)
      stringBuilder.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
    if (certificationData1.IsSuccess)
    {
      CarrierPlugin copy = (CarrierPlugin) ((PXSelectBase) this.Plugin).Cache.CreateCopy((object) cp);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (ZipArchiveWrapper zipArchiveWrapper = new ZipArchiveWrapper((Stream) memoryStream))
        {
          foreach (CarrierCertificationData certificationData2 in (IEnumerable<CarrierCertificationData>) certificationData1.Result)
            zipArchiveWrapper[$"{certificationData2.Description}.{certificationData2.Format}"] = certificationData2.File;
        }
        FileInfo fileInfo = new FileInfo("CertificationData.zip", (string) null, memoryStream.ToArray());
        instance.SaveFile(fileInfo, (FileExistsAction) 1);
        PXNoteAttribute.SetFileNotes(((PXSelectBase) this.Plugin).Cache, (object) copy, new Guid[1]
        {
          fileInfo.UID.Value
        });
      }
      ((PXSelectBase<CarrierPlugin>) this.Plugin).Update(copy);
      ((PXAction) this.Save).Press();
    }
    else
      throw new PXException("Carrier Service returned error. {0}", new object[1]
      {
        (object) stringBuilder.ToString()
      });
  }

  public virtual void InsertDetails(IList<ICarrierDetail> list)
  {
    Dictionary<string, CarrierPluginDetail> dictionary = new Dictionary<string, CarrierPluginDetail>();
    foreach (PXResult<CarrierPluginDetail> pxResult in ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
    {
      CarrierPluginDetail carrierPluginDetail = PXResult<CarrierPluginDetail>.op_Implicit(pxResult);
      dictionary.Add(carrierPluginDetail.DetailID.ToUpper(), carrierPluginDetail);
    }
    foreach (ICarrierDetail icarrierDetail in (IEnumerable<ICarrierDetail>) list)
    {
      if (!dictionary.ContainsKey(icarrierDetail.DetailID.ToUpper()))
      {
        CarrierPluginDetail carrierPluginDetail = ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Insert(new CarrierPluginDetail()
        {
          DetailID = icarrierDetail.DetailID
        });
        carrierPluginDetail.Descr = icarrierDetail.Descr;
        carrierPluginDetail.Value = icarrierDetail.Value;
        carrierPluginDetail.ControlType = icarrierDetail.ControlType;
        carrierPluginDetail.SetComboValues(icarrierDetail.GetComboValues());
      }
      else
      {
        CarrierPluginDetail carrierPluginDetail = dictionary[icarrierDetail.DetailID];
        CarrierPluginDetail copy = PXCache<CarrierPluginDetail>.CreateCopy(carrierPluginDetail);
        if (!string.IsNullOrEmpty(icarrierDetail.Descr))
          copy.Descr = icarrierDetail.Descr;
        copy.ControlType = icarrierDetail.ControlType;
        copy.SetComboValues(icarrierDetail.GetComboValues());
        if (!(carrierPluginDetail.Descr != copy.Descr))
        {
          int? controlType1 = carrierPluginDetail.ControlType;
          int? controlType2 = copy.ControlType;
          if (controlType1.GetValueOrDefault() == controlType2.GetValueOrDefault() & controlType1.HasValue == controlType2.HasValue && !(carrierPluginDetail.ComboValues != copy.ComboValues))
            continue;
        }
        ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Update(copy);
        ((PXSelectBase) this.SelectDetails).Cache.IsDirty = false;
      }
    }
  }

  public static CarrierResult<ICarrierService> CreateCarrierService(
    PXGraph graph,
    CarrierPlugin plugin,
    bool throwException = false)
  {
    ICarrierService icarrierService = (ICarrierService) null;
    string str = string.Empty;
    if (!string.IsNullOrEmpty(plugin.PluginTypeName))
    {
      Type type = (Type) null;
      try
      {
        type = PXBuildManager.GetType(plugin.PluginTypeName, true);
        icarrierService = (ICarrierService) Activator.CreateInstance(type);
        PXResultset<CarrierPluginDetail> pxResultset = ((PXSelectBase<CarrierPluginDetail>) new PXSelect<CarrierPluginDetail, Where<CarrierPluginDetail.carrierPluginID, Equal<Required<CarrierPluginDetail.carrierPluginID>>>>(graph)).Select(new object[1]
        {
          (object) plugin.CarrierPluginID
        });
        IList<ICarrierDetail> icarrierDetailList = (IList<ICarrierDetail>) new List<ICarrierDetail>(pxResultset.Count);
        foreach (PXResult<CarrierPluginDetail> pxResult in pxResultset)
        {
          CarrierPluginDetail carrierPluginDetail = PXResult<CarrierPluginDetail>.op_Implicit(pxResult);
          icarrierDetailList.Add((ICarrierDetail) carrierPluginDetail);
        }
        icarrierService.LoadSettings(icarrierDetailList);
      }
      catch (Exception ex)
      {
        str = type != (Type) null ? $"The carrier plug-in cannot be created. {ex.Message}" : $"The {plugin.CarrierPluginID} carrier references a missing plug-in.";
        if (throwException)
          throw new PXException(str);
      }
    }
    return new CarrierResult<ICarrierService>(icarrierService != null, icarrierService, new Message(string.Empty, str));
  }

  public static IList<string> GetCarrierPluginAttributes(PXGraph graph, string carrierPluginID)
  {
    CarrierPlugin carrierPlugin = CarrierPlugin.PK.Find(graph, carrierPluginID);
    if (carrierPlugin == null)
      throw new PXException("Failed to Find Carrier Plug-in with the given ID - {0}", new object[1]
      {
        (object) carrierPluginID
      });
    ICarrierService icarrierService = (ICarrierService) null;
    if (!string.IsNullOrEmpty(carrierPlugin.PluginTypeName))
    {
      Type type = PXBuildManager.GetType(carrierPlugin.PluginTypeName, false);
      icarrierService = type != (Type) null ? (ICarrierService) Activator.CreateInstance(type) : (ICarrierService) null;
    }
    return icarrierService != null ? icarrierService.Attributes : (IList<string>) new List<string>();
  }

  public static CarrierResult<ICarrierService> CreateCarrierService(
    PXGraph graph,
    string carrierPluginID,
    bool throwException = false)
  {
    return CarrierPluginMaint.CreateCarrierService(graph, CarrierPlugin.PK.Find(graph, carrierPluginID) ?? throw new PXException("Failed to Find Carrier Plug-in with the given ID - {0}", new object[1]
    {
      (object) carrierPluginID
    }), throwException);
  }

  public static bool IsValidType(string type) => PXBuildManager.GetType(type, false) != (Type) null;

  protected virtual void CarrierPlugin_PluginTypeName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CarrierPlugin))
      return;
    foreach (PXResult<CarrierPluginDetail> pxResult in ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Select(Array.Empty<object>()))
      ((PXSelectBase<CarrierPluginDetail>) this.SelectDetails).Delete(PXResult<CarrierPluginDetail>.op_Implicit(pxResult));
  }

  protected virtual void CarrierPluginDetail_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CarrierPluginDetail row))
      return;
    string name = typeof (CarrierPluginDetail.value).Name;
    int? controlType = row.ControlType;
    if (!controlType.HasValue)
      return;
    switch (controlType.GetValueOrDefault())
    {
      case 2:
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        foreach (KeyValuePair<string, string> comboValue in (IEnumerable<KeyValuePair<string, string>>) row.GetComboValues())
        {
          stringList2.Add(comboValue.Key);
          stringList1.Add(comboValue.Value);
        }
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1024 /*0x0400*/), new bool?(), name, new bool?(false), new int?(1), (string) null, stringList2.ToArray(), stringList1.ToArray(), new bool?(true), (string) null, (string[]) null);
        break;
      case 3:
        e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
      case 4:
        if (e.ReturnState == null)
          break;
        string str = new string('*', e.ReturnState.ToString().Length);
        e.ReturnState = (object) PXFieldState.CreateInstance((object) str, typeof (string), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, name, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
        break;
    }
  }

  protected virtual void CarrierPlugin_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is CarrierPlugin && PXResultset<Carrier>.op_Implicit(PXSelectBase<Carrier, PXSelect<Carrier, Where<Carrier.carrierPluginID, Equal<Current<CarrierPlugin.carrierPluginID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("Carrier cannot be deleted. One or more Ship Via is depends on this Carrier.");
  }

  protected virtual void CarrierPlugin_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CarrierPlugin row))
      return;
    CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService((PXGraph) this, row);
    PXUIFieldAttribute.SetWarning<CarrierPlugin.pluginTypeName>(sender, (object) row, (string) null);
    if (carrierService.IsSuccess)
    {
      ((PXAction) this.certify).SetVisible(carrierService.Result.Attributes.Contains("CERTIFICATE"));
    }
    else
    {
      IList<Message> messages = carrierService.Messages;
      string str = string.Format(messages != null ? messages.FirstOrDefault<Message>()?.Description : (string) null, (object) row?.CarrierPluginID);
      PXUIFieldAttribute.SetWarning<CarrierPlugin.pluginTypeName>(sender, (object) row, str);
    }
  }

  protected virtual void _(Events.RowSelected<CarrierPluginCustomer> e)
  {
    CarrierPluginCustomer row = e.Row;
    if (row == null)
      return;
    string carrierBillingType = row.CarrierBillingType;
    bool flag = carrierBillingType != null && carrierBillingType.Equals("T");
    PXUIFieldAttribute.SetEnabled<CarrierPluginCustomer.countryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CarrierPluginCustomer>>) e).Cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<CarrierPluginCustomer.postalCode>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CarrierPluginCustomer>>) e).Cache, (object) row, flag);
  }

  protected virtual void _(
    Events.FieldUpdated<CarrierPlugin, CarrierPlugin.unitType> args)
  {
    ((PXSelectBase) this.Plugin).Cache.SetValueExt<CarrierPlugin.kilogramUOM>((object) args.Row, (object) null);
    ((PXSelectBase) this.Plugin).Cache.SetValueExt<CarrierPlugin.poundUOM>((object) args.Row, (object) null);
    ((PXSelectBase) this.Plugin).Cache.SetValueExt<CarrierPlugin.centimeterUOM>((object) args.Row, (object) null);
    ((PXSelectBase) this.Plugin).Cache.SetValueExt<CarrierPlugin.inchUOM>((object) args.Row, (object) null);
  }
}
