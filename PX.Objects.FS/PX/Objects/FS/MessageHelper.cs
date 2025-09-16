// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.MessageHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public static class MessageHelper
{
  public static int GetRowMessages(
    PXCache cache,
    object row,
    List<string> errors,
    List<string> warnings,
    bool includeRowInfo)
  {
    if (cache == null || row == null || !cache.IsDirty)
      return 0;
    int rowMessages = 0;
    foreach (MemberInfo bqlField in cache.BqlFields)
    {
      string name = bqlField.Name;
      if (!name.ToLower().StartsWith(typeof (FSAppointment.createdByID).Name.ToLower()))
      {
        if (!name.ToLower().StartsWith(typeof (FSAppointment.lastModifiedByID).Name.ToLower()))
        {
          PXFieldState pxFieldState;
          try
          {
            pxFieldState = (PXFieldState) cache.GetStateExt(row, name);
          }
          catch
          {
            pxFieldState = (PXFieldState) null;
          }
          if (pxFieldState != null && pxFieldState.Error != null)
          {
            if (errors != null && pxFieldState.ErrorLevel != 3 && pxFieldState.ErrorLevel != 2 && pxFieldState.ErrorLevel != 1)
            {
              errors.Add(pxFieldState.Error);
              ++rowMessages;
            }
            if (warnings != null && (pxFieldState.ErrorLevel == 3 || pxFieldState.ErrorLevel == 2 || pxFieldState.ErrorLevel == 1 && includeRowInfo))
              warnings.Add(pxFieldState.Error);
          }
        }
      }
    }
    return rowMessages;
  }

  public static string GetRowMessage(
    PXCache cache,
    IBqlTable row,
    bool getErrors,
    bool getWarnings)
  {
    List<string> errors = (List<string>) null;
    List<string> warnings = (List<string>) null;
    if (getErrors)
      errors = new List<string>();
    if (getWarnings)
      warnings = new List<string>();
    MessageHelper.GetRowMessages(cache, (object) row, errors, warnings, false);
    StringBuilder stringBuilder = new StringBuilder();
    if (errors != null)
    {
      foreach (string str in errors)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(str);
      }
    }
    if (warnings != null)
    {
      foreach (string str in warnings)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(str);
      }
    }
    return stringBuilder.ToString();
  }

  public static List<MessageHelper.ErrorInfo> GetErrorInfo<TranType>(
    PXCache headerCache,
    IBqlTable headerRow,
    PXSelectBase<TranType> detailView,
    Type extensionType)
    where TranType : class, IBqlTable, new()
  {
    List<MessageHelper.ErrorInfo> errorInfo1 = new List<MessageHelper.ErrorInfo>();
    string rowMessage1 = MessageHelper.GetRowMessage(headerCache, headerRow, true, false);
    if (!string.IsNullOrEmpty(rowMessage1))
    {
      MessageHelper.ErrorInfo errorInfo2 = new MessageHelper.ErrorInfo()
      {
        HeaderError = true,
        SOID = new int?(),
        SODetID = new int?(),
        AppointmentID = new int?(),
        AppDetID = new int?(),
        ErrorMessage = rowMessage1
      };
      errorInfo1.Add(errorInfo2);
    }
    foreach (object obj in detailView.Select(Array.Empty<object>()))
    {
      object row = obj;
      IFSRelatedDoc fsRelatedDoc = (IFSRelatedDoc) null;
      if (typeof (TranType) == typeof (PX.Objects.AR.ARTran))
      {
        row = (object) PXResult<PX.Objects.AR.ARTran>.op_Implicit((PXResult<PX.Objects.AR.ARTran>) obj);
        fsRelatedDoc = (IFSRelatedDoc) headerCache.Graph.Caches[typeof (PX.Objects.AR.ARTran)].GetExtension<FSxARTran>(row);
      }
      string rowMessage2 = MessageHelper.GetRowMessage(((PXSelectBase) detailView).Cache, (IBqlTable) row, true, false);
      if (!string.IsNullOrEmpty(rowMessage2) && extensionType != (Type) null)
      {
        if (extensionType == typeof (PX.Objects.SO.SOLine))
          fsRelatedDoc = (IFSRelatedDoc) ((PXSelectBase) detailView).Cache.GetExtension<FSxSOLine>(row);
        else if (extensionType == typeof (INTran))
          fsRelatedDoc = (IFSRelatedDoc) ((PXSelectBase) detailView).Cache.GetExtension<FSxINTran>(row);
        else if (extensionType == typeof (APTran))
          fsRelatedDoc = (IFSRelatedDoc) ((PXSelectBase) detailView).Cache.GetExtension<FSxAPTran>(row);
        else if (!(extensionType == typeof (PX.Objects.AR.ARTran)))
        {
          MessageHelper.ErrorInfo errorInfo3 = new MessageHelper.ErrorInfo()
          {
            HeaderError = false,
            SOID = new int?(),
            SODetID = new int?(),
            AppointmentID = new int?(),
            AppDetID = new int?(),
            ErrorMessage = rowMessage2 + ", "
          };
          errorInfo1.Add(errorInfo3);
        }
        if (fsRelatedDoc != null && !string.IsNullOrEmpty(fsRelatedDoc.SrvOrdType))
        {
          if (!string.IsNullOrEmpty(fsRelatedDoc.AppointmentRefNbr))
          {
            FSAppointment fsAppointment = FSAppointment.PK.Find(headerCache.Graph, fsRelatedDoc.SrvOrdType, fsRelatedDoc.AppointmentRefNbr);
            FSAppointmentDet fsAppointmentDet = FSAppointmentDet.PK.Find(headerCache.Graph, fsRelatedDoc.SrvOrdType, fsRelatedDoc.AppointmentRefNbr, fsRelatedDoc.AppointmentLineNbr);
            MessageHelper.ErrorInfo errorInfo4 = new MessageHelper.ErrorInfo()
            {
              HeaderError = false,
              SOID = fsAppointment.SOID,
              SODetID = new int?(),
              AppointmentID = (int?) fsAppointment?.AppointmentID,
              AppDetID = (int?) fsAppointmentDet?.AppDetID,
              ErrorMessage = rowMessage2 + ", "
            };
            errorInfo1.Add(errorInfo4);
          }
          else if (!string.IsNullOrEmpty(fsRelatedDoc.ServiceOrderRefNbr))
          {
            FSServiceOrder fsServiceOrder = FSServiceOrder.PK.Find(headerCache.Graph, fsRelatedDoc.SrvOrdType, fsRelatedDoc.ServiceOrderRefNbr);
            FSSODet fssoDet = FSSODet.PK.Find(headerCache.Graph, fsRelatedDoc.SrvOrdType, fsRelatedDoc.ServiceOrderRefNbr, fsRelatedDoc.ServiceOrderLineNbr);
            MessageHelper.ErrorInfo errorInfo5 = new MessageHelper.ErrorInfo()
            {
              HeaderError = false,
              SOID = (int?) fsServiceOrder?.SOID,
              SODetID = fssoDet.SODetID,
              AppointmentID = new int?(),
              AppDetID = new int?(),
              ErrorMessage = rowMessage2 + ", "
            };
            errorInfo1.Add(errorInfo5);
          }
        }
      }
    }
    return errorInfo1;
  }

  public static List<MessageHelper.ErrorInfo> GetErrorInfo<TranType>(
    PXCache headerCache,
    IBqlTable headerRow,
    PXSelectBase<TranType> detailView)
    where TranType : class, IBqlTable, new()
  {
    List<MessageHelper.ErrorInfo> errorInfo1 = new List<MessageHelper.ErrorInfo>();
    string rowMessage1 = MessageHelper.GetRowMessage(headerCache, headerRow, true, false);
    if (!string.IsNullOrEmpty(rowMessage1))
    {
      MessageHelper.ErrorInfo errorInfo2 = new MessageHelper.ErrorInfo()
      {
        HeaderError = true,
        SOID = new int?(),
        SODetID = new int?(),
        AppointmentID = new int?(),
        AppDetID = new int?(),
        ErrorMessage = rowMessage1
      };
      errorInfo1.Add(errorInfo2);
    }
    foreach (PXResult<TranType> pxResult in detailView.Select(Array.Empty<object>()))
    {
      TranType row = PXResult<TranType>.op_Implicit(pxResult);
      string rowMessage2 = MessageHelper.GetRowMessage(((PXSelectBase) detailView).Cache, (IBqlTable) row, true, false);
      if (!string.IsNullOrEmpty(rowMessage2))
      {
        MessageHelper.ErrorInfo errorInfo3 = new MessageHelper.ErrorInfo()
        {
          HeaderError = false,
          SOID = new int?(),
          SODetID = new int?(),
          AppointmentID = new int?(),
          AppDetID = new int?(),
          ErrorMessage = rowMessage2 + ", "
        };
        errorInfo1.Add(errorInfo3);
      }
    }
    return errorInfo1;
  }

  public static string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    string empty = string.Empty;
    if (string.IsNullOrEmpty(lineRefNbr))
      return empty;
    string lineDisplayHint = lineRefNbr;
    if (inventoryID.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID);
      if (inventoryItem != null)
        lineDisplayHint = lineDisplayHint + " - " + inventoryItem.InventoryCD.Trim();
    }
    if (!string.IsNullOrEmpty(lineDescr))
      lineDisplayHint = $"{lineDisplayHint} ({lineDescr.Trim()})";
    return lineDisplayHint;
  }

  public static string GetLineDisplayHint(
    PXGraph graph,
    string type,
    string srvOrdType,
    string refNbr,
    string lineRef)
  {
    string empty = string.Empty;
    if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(srvOrdType) || string.IsNullOrEmpty(refNbr))
      return empty;
    string lineDisplayHint = $"{(!(type == "AP") ? "Service Order: " : "Appointment: ")}{srvOrdType}-{refNbr}";
    if (lineRef != null)
      lineDisplayHint = $"{lineDisplayHint} Detail Ref. Nbr.: {lineRef}";
    return lineDisplayHint;
  }

  public class ErrorInfo
  {
    public int? SOID;
    public int? SODetID;
    public int? AppointmentID;
    public int? AppDetID;
    public string ErrorMessage;
    public bool HeaderError;
  }
}
