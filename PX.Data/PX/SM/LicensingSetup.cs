// Decompiled with JetBrains decompiler
// Type: PX.SM.LicensingSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.CSharp.RuntimeBinder;
using PX.Common;
using PX.Data;
using PX.Data.Process.Automation;
using PX.Data.Update;
using PX.Data.Update.Licensing;
using PX.Licensing;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.SM;

[PXDisableWorkflow]
public class LicensingSetup : PXGraph<LicensingSetup>
{
  [PXVirtualDAC]
  public PXFilter<LicenseInfo> License;
  [PXVirtualDAC]
  public PXSelect<LicenseFeature, Where<PX.Data.True>, OrderBy<Asc<LicenseFeature.name>>> Features;
  public PXCancel<LicenseInfo> Cancel;
  public PXAction<LicenseInfo> InstallLicense;
  [PXVirtualDAC]
  public PXFilter<LicensingKeyFilter> LicenseKeyPanel;
  [PXVirtualDAC]
  public PXFilter<LicensingKeyFilter> LicenseWarning;
  [PXVirtualDAC]
  public PXFilter<LicensingKeyFilter> LicenseAgreement;
  public PXAction<LicenseInfo> UploadLicense;
  public PXSelect<LicenseInfo> UploadDialogPanel;
  public PXAction<LicenseInfo> SaveLicense;
  public PXAction<LicenseInfo> UpdateLicense;
  public PXAction<LicenseInfo> DeleteLicense;

  [InjectDependency]
  private ILicensingManager _licensingManager { get; set; }

  [InjectDependency]
  private ILicenseLimitInfo[] _extendedLicensingRestrictions { get; set; }

  public LicensingSetup()
  {
    this.Features.Cache.AllowInsert = false;
    this.Features.Cache.AllowDelete = false;
  }

  protected IEnumerable license()
  {
    PXLicense license = this._licensingManager.License;
    if (this.License.Cache.Cached.Count() <= 0L)
      this.License.Cache.SetStatus((object) (this.License.Cache.Insert((object) this.ConvertLicense(license)) as LicenseInfo), PXEntryStatus.Held);
    foreach (LicenseInfo licenseInfo in this.License.Cache.Cached)
      yield return (object) licenseInfo;
    this.License.Cache.IsDirty = false;
  }

  protected IEnumerable features()
  {
    if (this.Features.Cache.Cached.Count() <= 0L)
    {
      PXLicense license = this._licensingManager.License;
      if (this.License.Cache.Cached.Count() > 0L && this.License.Current != null && this.License.Current.License != null)
        license = this.License.Current.License;
      if (license.State != PXLicenseState.Invalid && license.State != PXLicenseState.Bypass)
      {
        foreach (PXAccess.FeatureInfo featureInfo in PXAccess.GetAllFeaturesInfo())
        {
          if (featureInfo.Visible)
            this.Features.Cache.SetStatus((object) (this.Features.Cache.Insert((object) new LicenseFeature()
            {
              Id = featureInfo.ID,
              Name = featureInfo.Title,
              Description = featureInfo.Description,
              Enabled = new bool?(license.Features.Contains(featureInfo.ID)),
              Visible = new bool?(featureInfo.Visible)
            }) as LicenseFeature), PXEntryStatus.Held);
        }
      }
    }
    foreach (LicenseFeature licenseFeature in this.Features.Cache.Cached)
      yield return (object) licenseFeature;
    this.Features.Cache.IsDirty = false;
  }

  [PXButton(Tooltip = "Validate and activate a new license for your application. (An internet connection is required.)", CommitChanges = true)]
  [PXUIField(DisplayName = "Enter License Key", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable installLicense(PXAdapter adapter)
  {
    if (this.LicenseKeyPanel.AskExt(true) != WebDialogResult.OK)
      return adapter.Get();
    LicenseInfo license = this.GetLicense((this.LicenseKeyPanel.Current ?? throw new PXException("Please enter an appropriate license key.")).LicensingKey, PXLicenseReason.Preview);
    if (license != null && !PortalHelper.IsPortalContext() && license.License.IsPortal)
      throw new PXException("The license you are trying to apply is valid for Acumatica Self-Service Portal only.");
    return !this.agreeToLicense(license) || license == null ? adapter.Get() : (IEnumerable) EnumerableExtensions.AsSingleEnumerable<LicenseInfo>(this.PeviewLicense(license));
  }

  protected IEnumerable licenseKeyPanel()
  {
    if (this.LicenseKeyPanel.Current != null)
      yield return (object) this.LicenseKeyPanel.Current;
    else
      yield return (object) this.LicenseKeyPanel.Insert(new LicensingKeyFilter());
  }

  protected IEnumerable licenseWarning()
  {
    if (this.LicenseWarning.Current != null)
      yield return (object) this.LicenseWarning.Current;
    else
      yield return (object) this.LicenseWarning.Insert(new LicensingKeyFilter());
  }

  protected IEnumerable licenseAgreement()
  {
    if (this.LicenseAgreement.Current != null)
      yield return (object) this.LicenseAgreement.Current;
    else
      yield return (object) this.LicenseAgreement.Insert(new LicensingKeyFilter());
  }

  [PXButton(Tooltip = "Upload a custom license file (for standalone installation).", CommitChanges = true)]
  [PXUIField(DisplayName = "Upload License File", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable uploadLicense(PXAdapter adapter)
  {
    if (this.UploadDialogPanel.AskExt() != WebDialogResult.OK)
      return adapter.Get();
    PXLicense license = this._licensingManager.GetLicense(this._licensingManager.ParseLicenseFile(PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["UploadedLicenseKey"] ?? throw new PXException("The file is not found, or you don't have enough rights to see the file.")));
    LicenseInfo info = PortalHelper.IsPortalContext() || !license.IsPortal ? this.ConvertLicense(license) : throw new PXException("The license you are trying to apply is valid for Acumatica Self-Service Portal only.");
    info.License = license;
    info.Offline = new bool?(true);
    if (!this.agreeToLicense(info))
      return adapter.Get();
    PXContext.SessionTyped<PXSessionStatePXData>().Remove("UploadedLicenseKey");
    this.PeviewLicense(info);
    return adapter.Get();
  }

  private bool agreeToLicense(LicenseInfo info)
  {
    if (info == null)
      return false;
    PXContext.Session.SetString("LicenseType", string.Compare(info.Type, "PERP", StringComparison.OrdinalIgnoreCase) == 0 ? "PERP" : "PCS");
    if (this.LicenseAgreement.AskExt(true) != WebDialogResult.Yes)
    {
      PXContext.Session.Remove("LicenseType");
      return false;
    }
    PXContext.Session.Remove("LicenseType");
    return true;
  }

  [PXButton(Tooltip = "Apply the current license.", CommitChanges = true)]
  [PXUIField(DisplayName = "Apply License", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  protected IEnumerable saveLicense(PXAdapter adapter)
  {
    LicenseInfo licenseInfo = this.License.Current;
    if (licenseInfo == null || licenseInfo.License == null)
      return adapter.Get();
    if (!licenseInfo.License.Valid)
      throw new PXException("The received file is not a valid license.");
    try
    {
      licenseInfo = this.GetLicense(licenseInfo.License.LicenseKey, this._licensingManager.License.Licensed ? PXLicenseReason.Renewal : PXLicenseReason.Installation);
    }
    catch (Exception ex)
    {
      bool? offline = licenseInfo.Offline;
      bool flag = true;
      if (!(offline.GetValueOrDefault() == flag & offline.HasValue))
        throw ex;
    }
    if (licenseInfo == null || licenseInfo.License == null)
      return adapter.Get();
    if (!licenseInfo.License.Valid)
      throw new PXException("The received file is not a valid license.");
    LicenseBucket license = new LicenseBucket()
    {
      LicenseKey = licenseInfo.License.LicenseKey,
      Signature = licenseInfo.License.Signature,
      Restriction = licenseInfo.License.Restriction,
      Date = new System.DateTime?(System.DateTime.UtcNow)
    };
    if (this._licensingManager.ValidateLicense(license) && this.ValidateLicense(licenseInfo.License))
    {
      this.InstallInternalApp(licenseInfo.License);
      this._licensingManager.WriteLicense(license);
      this.Clear();
      object obj1 = (object) null;
      if (licenseInfo.License != null)
      {
        obj1 = (object) new ExpandoObject();
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Valid", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__0, obj1, licenseInfo.License.Valid);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Licensed", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__1.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__1, obj1, licenseInfo.License.Licensed);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Validated", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__2.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__2, obj1, licenseInfo.License.Validated);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Configured", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__3.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__3, obj1, licenseInfo.License.Configured);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, PXLicenseState, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "State", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__4.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__4, obj1, licenseInfo.License.State);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "UsersAllowed", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__5.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__5, obj1, licenseInfo.License.UsersAllowed);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaxApiUsersAllowed", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__6.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__6, obj1, licenseInfo.License.MaxApiUsersAllowed);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "CompaniesAllowed", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__7.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__7, obj1, licenseInfo.License.CompaniesAllowed);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "ProcessorsAllowed", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__8.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__8, obj1, licenseInfo.License.ProcessorsAllowed);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "PayrollEmployee", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__9.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__9, obj1, licenseInfo.License.PayrollEmployee);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaximumNumberOfAppointmentsPerMonth", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__10.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__10, obj1, licenseInfo.License.MaximumNumberOfAppointmentsPerMonth);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaximumNumberOfDocumentsRecognizedPerMonth", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__11.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__11, obj1, licenseInfo.License.MaximumNumberOfDocumentsRecognizedPerMonth);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaximumNumberOfStaffMembersAndVehicles", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj14 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__12.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__12, obj1, licenseInfo.License.MaximumNumberOfStaffMembersAndVehicles);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaximumNumberOfBusinessCardsRecognizedPerMonth", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__13.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__13, obj1, licenseInfo.License.MaximumNumberOfBusinessCardsRecognizedPerMonth);
        // ISSUE: reference to a compiler-generated field
        if (LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "MaximumNumberOfExpenseReceiptsRecognizedPerMonth", typeof (LicensingSetup), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__14.Target((CallSite) LicensingSetup.\u003C\u003Eo__27.\u003C\u003Ep__14, obj1, licenseInfo.License.MaximumNumberOfExpenseReceiptsRecognizedPerMonth);
        IDictionary<string, object> dictionary = obj1 as IDictionary<string, object>;
        foreach (ILicenseLimitInfo licensingRestriction in this._extendedLicensingRestrictions)
        {
          if (dictionary.ContainsKey(licensingRestriction.RestrictionName))
            dictionary[licensingRestriction.RestrictionName] = (object) licenseInfo.License.GetRestrictionValueOrDefault(licensingRestriction);
          else
            dictionary.Add(licensingRestriction.RestrictionName, (object) licenseInfo.License.GetRestrictionValueOrDefault(licensingRestriction));
        }
      }
      var data = new
      {
        Valid = licenseInfo.Valid,
        Preview = licenseInfo.Preview,
        Status = licenseInfo.Status,
        License = obj1,
        ValidFrom = licenseInfo.ValidFrom,
        ValidTo = licenseInfo.ValidTo,
        DomainName = licenseInfo.DomainName,
        CustomerName = licenseInfo.CustomerName,
        Users = licenseInfo.Users,
        Companies = licenseInfo.Companies,
        Processors = licenseInfo.Processors,
        Version = licenseInfo.Version,
        LicenseTypeCD = licenseInfo.LicenseTypeCD,
        Type = licenseInfo.Type,
        Offline = licenseInfo.Offline
      };
      PXTrace.Logger.ForSystemEvents("License", "License_LicenseActivatedEventId").ForContext("ContextScreenId", (object) "SM201510", false).ForContext("LicenseParams", (object) data, true).Information<string, string>("A license has been activated, CustomerName: {CustomerName}, DomainName: {DomainName}", licenseInfo.CustomerName, licenseInfo.DomainName);
      throw new PXRefreshException();
    }
    throw new PXException("License activation has failed.");
  }

  private LicenseInfo GetLicense(string licensingKey, PXLicenseReason reason)
  {
    if (string.IsNullOrEmpty(licensingKey))
      throw new PXException("Please enter an appropriate license key.");
    LicenseInfo license1 = (LicenseInfo) null;
    try
    {
      LicenseBucket bucket = this.RequestLicenseWithExceededCountWarning(licensingKey, reason);
      if (bucket != null)
      {
        PXLicense license2 = this._licensingManager.GetLicense(bucket);
        license1 = this.ConvertLicense(license2);
        license1.License = license2;
      }
    }
    catch (PXException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      throw new PXException("The license cannot be obtained.", ex);
    }
    return license1;
  }

  [PXButton(Tooltip = "Update the current license from the license server.", CommitChanges = true)]
  [PXUIField(DisplayName = "Update License", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  protected IEnumerable updateLicense(PXAdapter adapter)
  {
    LicenseBucket licenseBucket = this._licensingManager.RequestLicense(PXLicenseReason.Renewal, true);
    PXLicense license = this._licensingManager.GetLicense(licenseBucket);
    if (!license.Validated)
      throw new PXException("The received license has an invalid signature.");
    if (!PortalHelper.IsPortalContext() && license.IsPortal)
      throw new PXException("Requested license is valid for Acumatica Self-Service Portal only.");
    this.InstallInternalApp(license);
    this._licensingManager.WriteLicense(licenseBucket);
    this.Clear();
    throw new PXRefreshException();
  }

  [PXButton(Tooltip = "Delete the current license.", CommitChanges = true)]
  [PXUIField(DisplayName = "Delete License", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  protected IEnumerable deleteLicense(PXAdapter adapter)
  {
    if (this.License.Ask("License", "WARNING: This action will completely delete the current license. Are you sure you want to continue?", MessageButtons.OKCancel) == WebDialogResult.OK)
    {
      this._licensingManager.DeleteLicense();
      this.Clear();
      throw new PXRefreshException();
    }
    return adapter.Get();
  }

  public virtual void LicenseInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    bool isVisible = e.Row is LicenseInfo row && row.License != null;
    this.SaveLicense.SetVisible(isVisible);
    this.UploadLicense.SetEnabled(!isVisible);
    this.InstallLicense.SetEnabled(!isVisible);
    this.DeleteLicense.SetVisible(!isVisible && this._licensingManager.License.Configured);
    this.UpdateLicense.SetVisible(!isVisible && this._licensingManager.License.Configured);
    PXUIFieldAttribute.SetVisible<LicenseInfo.preview>(sender, (object) row, isVisible);
    bool flag = row != null && !string.IsNullOrEmpty(row.Type) && string.Compare(row.Type, "PERP", StringComparison.OrdinalIgnoreCase) == 0;
    PXUIFieldAttribute.SetVisible<LicenseInfo.validTo>(sender, (object) row, !flag);
    if (PXDatabase.Companies.Length > PXDatabase.AvailableCompanies.Length)
      sender.RaiseExceptionHandling<LicenseInfo.companies>((object) row, (object) row.Companies, (Exception) new PXSetPropertyException<LicenseInfo.companies>("More tenants exist than are allowed by the license. Tenant selection will be restricted on the login page.", PXErrorLevel.Warning));
    PXUIFieldAttribute.SetVisible<LicenseInfo.processors>(sender, (object) row, true);
    this.HideProcessorsCount(row.License ?? this._licensingManager.License, sender, (object) row);
  }

  private LicenseBucket RequestLicenseWithExceededCountWarning(
    string licenseKey,
    PXLicenseReason reason)
  {
    LicenseBucket licenseBucket = (LicenseBucket) null;
    try
    {
      licenseBucket = this._licensingManager.RequestLicense(licenseKey, reason, false, true);
    }
    catch (InstanceCountWarningException ex)
    {
      if (this.LicenseWarning.AskExt(true) == WebDialogResult.Yes)
        licenseBucket = this._licensingManager.RequestLicense(licenseKey, reason, true, false);
    }
    return licenseBucket;
  }

  protected bool ValidateLicense(PXLicense license)
  {
    IEnumerable<PXAccess.FeatureInfo> allFeaturesInfo = PXAccess.GetAllFeaturesInfo();
    bool flag = false;
    foreach (LicenseFeature feature1 in this.features())
    {
      LicenseFeature feature = feature1;
      try
      {
        PXAccess.FeatureInfo featureInfo = allFeaturesInfo.First<PXAccess.FeatureInfo>((Func<PXAccess.FeatureInfo, bool>) (f => f.ID == feature.Id));
        object newValue = (object) license.Features.Contains(feature.Id);
        try
        {
          foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies())
          {
            if (!string.IsNullOrEmpty(selectCompany.LoginName))
            {
              PXDatabase.ResetCredentials();
              using (new PXLoginScope("admin@" + selectCompany.LoginName, Array.Empty<string>()))
              {
                PXCache cach = PXGraph.CreateInstance<LicensingSetup>().Caches[featureInfo.FeatureSet];
                if (PXAccess.FeatureInstalled(featureInfo.ID))
                {
                  if (!cach.RaiseFieldVerifying(featureInfo.Name, cach.CreateInstance(), ref newValue))
                    throw new PXException("Feature '{0}' validation has failed. Your license does not include access to the used feature.", new object[1]
                    {
                      (object) feature.Name
                    });
                }
              }
            }
          }
        }
        finally
        {
          PXDatabase.ResetCredentials();
        }
      }
      catch (PXException ex)
      {
        flag = true;
        this.Features.Cache.RaiseExceptionHandling<LicenseFeature.enabled>((object) feature, (object) feature.Enabled, (Exception) new PXSetPropertyException<LicenseFeature.enabled>(ex.Message, PXErrorLevel.RowError));
      }
    }
    return !flag;
  }

  protected virtual bool InstallInternalApp(PXLicense info) => true;

  private LicenseInfo PeviewLicense(LicenseInfo info)
  {
    this.License.Cache.Clear();
    this.Features.Cache.Clear();
    info = this.License.Cache.Insert((object) info) as LicenseInfo;
    this.License.Cache.Current = (object) info;
    if (!info.Valid.GetValueOrDefault())
      this.License.Cache.RaiseExceptionHandling<LicenseInfo.status>((object) info, (object) info.Status, (Exception) new PXSetPropertyException<LicenseInfo.status>("The current license is not valid."));
    this.ValidateLicense(info.License);
    return info;
  }

  public virtual LicenseInfo ConvertLicense(PXLicense license)
  {
    LicenseInfo to = new LicenseInfo();
    if (license.State != PXLicenseState.Bypass)
    {
      to = PX.Data.Update.Tools.ConcatProperties((object) license, (object) to) as LicenseInfo;
      foreach (System.Reflection.FieldInfo field in license.GetType().GetFields())
      {
        if (!(field.DeclaringType != typeof (PXLicenseDefinition)))
        {
          object obj = field.GetValue((object) license);
          if (obj != null)
          {
            PropertyInfo property = to.GetType().GetProperty(field.Name);
            if (!(property == (PropertyInfo) null))
            {
              try
              {
                property.SetValue((object) to, obj, (object[]) null);
              }
              catch
              {
              }
            }
          }
        }
      }
    }
    to.Status = license.State != PXLicenseState.NotifyPeriod || string.Compare(license.Type, "PERP", StringComparison.OrdinalIgnoreCase) != 0 ? license.State.ToString() : PXLicenseState.NotifyPeriodPerpetual.ToString();
    to.Valid = new bool?(license.Licensed);
    return to;
  }

  private void HideProcessorsCount(PXLicense license, PXCache cache, object data)
  {
    if (license == null || license.Type == null || license.ResourceLevel == null || !license.Type.Equals("saas", StringComparison.InvariantCultureIgnoreCase) && (license.Type.Equals("saas", StringComparison.InvariantCultureIgnoreCase) || license.ResourceLevel.Equals("small", StringComparison.InvariantCultureIgnoreCase) || license.ResourceLevel.Equals("medium", StringComparison.InvariantCultureIgnoreCase) || license.ResourceLevel.Equals("large", StringComparison.InvariantCultureIgnoreCase) || license.ResourceLevel.Equals("extra large", StringComparison.InvariantCultureIgnoreCase)))
      return;
    PXUIFieldAttribute.SetVisible<LicenseInfo.processors>(cache, data, false);
  }
}
