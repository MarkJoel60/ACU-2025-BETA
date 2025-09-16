// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DisabledProcCenterAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.Extensions.PaymentTransaction;
using System;

#nullable disable
namespace PX.Objects.Common.Attributes;

/// <summary>The attribute that informs a user that the processing center is deactivated.</summary>
public class DisabledProcCenterAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
{
  public DisabledProcCenterAttribute.CheckFieldVal CheckFieldValue;
  private bool errorRised;

  /// <summary>The DAC field name that is used to display the error message.</summary>
  public string ErrorMappedFieldName { get; set; }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    string fieldName = this.FieldName;
    string str = this.ErrorMappedFieldName != null ? this.ErrorMappedFieldName : fieldName;
    object obj = sender.GetValue(e.Row, fieldName);
    int? pmInstanceId = obj as int?;
    if (this.CheckFieldValue == DisabledProcCenterAttribute.CheckFieldVal.PmInstanceId && pmInstanceId.HasValue)
    {
      if (this.IsDisabledProcCenter(sender, pmInstanceId))
      {
        sender.RaiseExceptionHandling(str, e.Row, (object) pmInstanceId, (Exception) new PXSetPropertyException("The customer payment method uses a deactivated processing center.", (PXErrorLevel) 2));
        this.errorRised = true;
      }
      else if (this.errorRised)
      {
        sender.RaiseExceptionHandling(str, e.Row, (object) null, (Exception) null);
        this.errorRised = false;
      }
    }
    string procCenterId = obj as string;
    if (this.CheckFieldValue != DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId || procCenterId == null)
      return;
    if (this.IsDisabledProcCenter(sender, procCenterId))
    {
      sender.RaiseExceptionHandling(str, e.Row, (object) procCenterId, (Exception) new PXSetPropertyException("The processing center is deactivated.", (PXErrorLevel) 2));
      this.errorRised = true;
    }
    else
    {
      if (!this.errorRised)
        return;
      sender.RaiseExceptionHandling(str, e.Row, (object) null, (Exception) null);
      this.errorRised = false;
    }
  }

  private bool IsDisabledProcCenter(PXCache sender, string procCenterId)
  {
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) new PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>(sender.Graph)).SelectSingle(new object[1]
    {
      (object) procCenterId
    });
    if (processingCenter != null)
    {
      bool? isActive = processingCenter.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
        return true;
    }
    return false;
  }

  private bool IsDisabledProcCenter(PXCache sender, int? pmInstanceId)
  {
    PXGraph graph = sender.Graph;
    int? nullable = pmInstanceId;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (nullable.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & nullable.HasValue == newPaymentProfile.HasValue)
      return false;
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) new PXSelectJoin<CCProcessingCenter, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>(graph)).SelectSingle(new object[1]
    {
      (object) pmInstanceId
    });
    if (processingCenter != null)
    {
      bool? isActive = processingCenter.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
        return true;
    }
    return false;
  }

  /// <summary>Defines the types of the values of the field that is annotated with the attribute.</summary>
  public enum CheckFieldVal
  {
    PmInstanceId,
    ProcessingCenterId,
  }
}
