// Decompiled with JetBrains decompiler
// Type: PX.Api.SYWebServiceMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

public class SYWebServiceMaint : PXGraph<SYWebServiceMaint, SYWebService>
{
  public PXSelect<SYWebService> WebServices;
  public PXSelect<SYServiceSchema, Where<SYServiceSchema.serviceID, Equal<Current<SYWebService.serviceID>>>> Schemas;
  public PXSelectSiteMapTree<False, False, False, False, False> SiteMapTree;
  public PXAction<SYWebService> Generate;
  public PXAction<SYWebService> GeneratedSchema;
  public PXAction<SYWebService> AddToGrid;
  public PXAction<SYWebService> SingleSchema;
  public PXAction<SYWebService> UntypedSchema;
  public PXAction<SYWebService> ResetUsage;

  protected virtual IEnumerable schemas()
  {
    if (!(PXLongOperation.GetCustomInfo(this.UID) is SYServiceSchema[] customInfo))
      return (IEnumerable) null;
    foreach (SYServiceSchema row in customInfo)
    {
      if (!string.IsNullOrEmpty(row.ProcessMessage))
      {
        this.Schemas.Cache.RaiseExceptionHandling<SYServiceSchema.isGenerated>((object) row, (object) false, (Exception) new PXSetPropertyException(row.ProcessMessage, PXErrorLevel.RowError));
      }
      else
      {
        bool? isGenerated = row.IsGenerated;
        bool flag = true;
        if (isGenerated.GetValueOrDefault() == flag & isGenerated.HasValue)
          this.Schemas.Cache.RaiseExceptionHandling<SYServiceSchema.isGenerated>((object) row, (object) false, (Exception) new PXSetPropertyException("The record has been processed successfully.", PXErrorLevel.RowInfo));
      }
    }
    return (IEnumerable) customInfo;
  }

  [PXButton(Tooltip = "Add the selected node and all its valid children to the grid.")]
  [PXUIField(DisplayName = "Add to Grid", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  protected virtual void addToGrid()
  {
    PX.SM.SiteMap current = this.SiteMapTree.Current;
    if (current == null)
      return;
    if (this.WebServices.Current == null || string.IsNullOrEmpty(this.WebServices.Current.ServiceID))
      throw new PXException("Please select or enter a Service ID value.");
    this.AddNodeToGrid(PXSiteMap.Provider.FindSiteMapNodeFromKey(current.NodeID.Value));
    this.Schemas.View.RequestRefresh();
  }

  [PXButton(Tooltip = "View the schema of the selected screen.")]
  [PXUIField(DisplayName = "View Single", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected void singleSchema()
  {
    PX.SM.SiteMap current = this.SiteMapTree.Current;
    if (current == null)
      return;
    if (string.IsNullOrEmpty(current.ScreenID) || !this.IsValidGraphType(current.Graphtype))
      throw new PXException("The selected screen has an invalid screen ID or graph type. Navigation is aborted.");
    throw new PXRedirectToUrlException($"~/Soap/{current.ScreenID}.asmx", PXBaseRedirectException.WindowMode.New, true, "Soap");
  }

  [PXButton(Tooltip = "View the untyped schema.", Category = "Actions")]
  [PXUIField(DisplayName = "View Untyped", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected void untypedSchema()
  {
    throw new PXRedirectToUrlException("~/Soap/.asmx", PXBaseRedirectException.WindowMode.New, true, "Soap");
  }

  [PXButton(Tooltip = "Generate the WSDL for the screens marked as active.", Category = "Actions", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Generate", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  protected void generate()
  {
    if (this.WebServices.Current == null)
      return;
    this.Actions.PressSave();
    List<SYServiceSchema> list = new List<SYServiceSchema>();
    foreach (PXResult<SYServiceSchema> pxResult in this.Schemas.Select())
    {
      SYServiceSchema syServiceSchema = (SYServiceSchema) pxResult;
      list.Add(syServiceSchema);
    }
    if (list.Count <= 0)
      return;
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => SYWebServiceMaint.DoGenerate(this.WebServices.Current.ServiceID, list.ToArray())));
  }

  public static void DoGenerate(string serviceID, SYServiceSchema[] items)
  {
    SYWebServiceMaint instance = PXGraph.CreateInstance<SYWebServiceMaint>();
    instance.WebServices.Current = (SYWebService) instance.WebServices.Search<SYWebService.serviceID>((object) serviceID);
    if (instance.WebServices.Current == null)
      return;
    foreach (SYServiceSchema syServiceSchema in items)
    {
      syServiceSchema.ProcessMessage = (string) null;
      syServiceSchema.IsGenerated = new bool?(false);
      instance.Schemas.Cache.Update((object) syServiceSchema);
    }
    PXLongOperation.SetCustomInfo((object) items);
    WsdlMerger wsdlMerger1 = new WsdlMerger();
    bool flag1 = false;
    WsdlMerger wsdlMerger2 = wsdlMerger1;
    bool? nullable = instance.WebServices.Current.IncludeUntyped;
    bool flag2 = true;
    int num1 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    wsdlMerger2.Start(num1 != 0);
    foreach (SYServiceSchema syServiceSchema in items)
    {
      try
      {
        nullable = syServiceSchema.IsIncluded;
        bool flag3 = true;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          try
          {
            WsdlMerger wsdlMerger3 = wsdlMerger1;
            string screenId = syServiceSchema.ScreenID;
            nullable = syServiceSchema.IsExport;
            bool flag4 = true;
            int num2 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
            nullable = syServiceSchema.IsImport;
            bool flag5 = true;
            int num3 = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
            nullable = syServiceSchema.IsSubmit;
            bool flag6 = true;
            int num4 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
            wsdlMerger3.Append(screenId, num2 != 0, num3 != 0, num4 != 0);
          }
          catch
          {
            wsdlMerger1.Rollback();
            throw;
          }
        }
        syServiceSchema.IsGenerated = new bool?(true);
        instance.Schemas.Cache.Update((object) syServiceSchema);
      }
      catch (Exception ex)
      {
        flag1 = true;
        syServiceSchema.ProcessMessage = !(ex is TargetInvocationException) || ex.InnerException == null ? ex.Message : ex.InnerException.Message;
      }
    }
    SYWebService current = instance.WebServices.Current;
    WsdlMerger wsdlMerger4 = wsdlMerger1;
    string serviceID1 = serviceID;
    nullable = instance.WebServices.Current.IncludeUntyped;
    bool flag7 = true;
    int num5 = nullable.GetValueOrDefault() == flag7 & nullable.HasValue ? 1 : 0;
    string str = wsdlMerger4.Finish(serviceID1, num5 != 0);
    current.WSDL = str;
    instance.WebServices.Current.DateGenerated = new System.DateTime?(PXTimeZoneInfo.Now);
    instance.WebServices.Current.SysVerWhenGenerated = PXVersionInfo.Version;
    instance.WebServices.Current.IsGenerated = new bool?(true);
    instance.WebServices.Cache.Update((object) instance.WebServices.Current);
    instance.Save.Press();
    if (flag1)
      throw new PXException("At least one item has not been processed.");
  }

  [PXButton(Tooltip = "View the generated WSDL schema.", Category = "Actions")]
  [PXUIField(DisplayName = "View Generated", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected void generatedSchema()
  {
    if (this.WebServices.Current == null || string.IsNullOrEmpty(this.WebServices.Current.ServiceID))
      throw new PXException("The service ID is not selected or is null. Please select or enter the service ID.");
    throw new PXRedirectToUrlException($"~/Soap/{this.WebServices.Current.ServiceID.Trim()}.asmx", PXBaseRedirectException.WindowMode.New, true, "Soap");
  }

  [PXButton(Tooltip = "Set the values in the Import, Export, and Submit columns to the default values (which are defined for the web service in the summary area of the form).")]
  [PXUIField(DisplayName = "Reset Usage", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected void resetUsage()
  {
    if (this.WebServices.Current == null)
      throw new PXException("Please select or enter a Service ID value.");
    foreach (PXResult<SYServiceSchema> pxResult in this.Schemas.Select())
    {
      SYServiceSchema syServiceSchema = (SYServiceSchema) pxResult;
      syServiceSchema.IsImport = this.WebServices.Current.IsImport;
      syServiceSchema.IsExport = this.WebServices.Current.IsExport;
      syServiceSchema.IsSubmit = this.WebServices.Current.IsSubmit;
      this.Schemas.Update(syServiceSchema);
    }
    this.Schemas.View.RequestRefresh();
  }

  protected virtual void SYWebService_CurSysVer_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = (object) PXVersionInfo.Version;
  }

  protected virtual void SYWebService_IncludeUntyped_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    SYWebService row = (SYWebService) e.Row;
    if (row == null)
      return;
    row.IsGenerated = new bool?(false);
  }

  private void ValidateServiceId(PXCache sender, SYWebService row)
  {
    if (row.ServiceID == null || !row.ServiceID.Contains<char>(' '))
      return;
    sender.RaiseExceptionHandling<SYWebService.serviceID>((object) row, (object) row.ServiceID, (Exception) new PXSetPropertyException("Spaces are not allowed.", PXErrorLevel.Error));
  }

  protected virtual void SYWebService_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SYWebService row = (SYWebService) e.Row;
    this.ValidateServiceId(sender, row);
  }

  protected virtual void SYWebService_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    SYWebService row = (SYWebService) e.Row;
    this.ValidateServiceId(sender, row);
  }

  protected virtual void SYWebService_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PXLongRunStatus status = PXLongOperation.GetStatus(this.UID, out TimeSpan _, out Exception _);
    if (status != PXLongRunStatus.NotExists)
    {
      PXUIFieldAttribute.SetEnabled(cache, e.Row, false);
      PXUIFieldAttribute.SetEnabled<SYWebService.serviceID>(cache, e.Row, true);
      this.Save.SetEnabled(false);
      this.Insert.SetEnabled(false);
      this.Delete.SetEnabled(false);
      this.Generate.SetEnabled(false);
      this.GeneratedSchema.SetEnabled(status != PXLongRunStatus.InProcess);
      this.Schemas.Cache.AllowDelete = false;
      this.Schemas.Cache.AllowInsert = false;
      this.Schemas.Cache.AllowUpdate = false;
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<SYWebService.description>(cache, e.Row, true);
      this.Save.SetEnabled(true);
      this.Insert.SetEnabled(true);
      this.Delete.SetEnabled(true);
      this.Generate.SetEnabled(true);
      this.GeneratedSchema.SetEnabled(true);
      this.Schemas.Cache.AllowDelete = true;
      this.Schemas.Cache.AllowInsert = true;
      this.Schemas.Cache.AllowUpdate = true;
      SYWebService row = (SYWebService) e.Row;
      if (row == null)
        return;
      this.GeneratedSchema.SetEnabled(!string.IsNullOrEmpty(row.WSDL));
    }
  }

  protected virtual void SYServiceSchema_Title_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    SYServiceSchema row = (SYServiceSchema) e.Row;
    if (row == null || string.IsNullOrEmpty(row.ScreenID))
      return;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.ScreenID);
    if (screenIdUnsecure == null)
      return;
    e.ReturnValue = (object) screenIdUnsecure.Title;
  }

  protected void SYServiceSchema_ScreenID_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if (!string.IsNullOrEmpty(((SYServiceSchema) e.Row).ScreenID))
      throw new PXSetPropertyException("This field cannot be updated.");
  }

  protected void SYServiceSchema_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    SYServiceSchema row = (SYServiceSchema) e.Row;
    if (row == null || string.IsNullOrEmpty(row.ScreenID))
      return;
    PXUIFieldAttribute.SetEnabled<SYServiceSchema.screenID>(cache, (object) row, false);
  }

  protected void SYServiceSchema_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (this.WebServices.Current == null)
      return;
    this.WebServices.Current.IsGenerated = new bool?(false);
    this.WebServices.Update(this.WebServices.Current);
  }

  protected void SYServiceSchema_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (this.WebServices.Current == null)
      return;
    this.WebServices.Current.IsGenerated = new bool?(false);
    this.WebServices.Update(this.WebServices.Current);
  }

  protected void SYServiceSchema_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (this.WebServices.Current == null)
      return;
    this.WebServices.Current.IsGenerated = new bool?(false);
    this.WebServices.Update(this.WebServices.Current);
  }

  protected void AddNodeToGrid(PXSiteMapNode node)
  {
    if (node == null)
      return;
    if (!string.IsNullOrEmpty(node.ScreenID))
      this.Schemas.Cache.Insert((object) new SYServiceSchema()
      {
        ScreenID = node.ScreenID,
        Title = node.Title
      });
    foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) node.ChildNodes)
      this.AddNodeToGrid(childNode);
  }

  private bool IsValidGraphType(string graphtype)
  {
    return !string.IsNullOrEmpty(graphtype) && !(PXBuildManager.GetType(graphtype, false) == (System.Type) null);
  }
}
