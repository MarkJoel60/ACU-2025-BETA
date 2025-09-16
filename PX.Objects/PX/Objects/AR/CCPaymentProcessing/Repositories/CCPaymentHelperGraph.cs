// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Repositories.CCPaymentHelperGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.CC;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Repositories;

public class CCPaymentHelperGraph : PXGraph<CCPaymentHelperGraph>
{
  public PXSelect<PX.Objects.AR.ExternalTransaction> ExternalTrans;
  public PXSelect<CCProcTran> CCProcTrans;
  public PXSelect<PX.Objects.CA.PaymentMethod> PaymentMethod;
  public PXSelect<PaymentMethodDetail> PaymentMethodDet;
  public PXSelect<PX.Objects.AR.CustomerPaymentMethod> CustomerPaymentMethods;
  public PXSelect<PX.Objects.CA.CustomerProcessingCenterID> CustomerProcessingCenterID;
  public PXSelect<CustomerPaymentMethodDetail> CustomerPaymentMethodDetails;
  public PXSelect<CCPayLink> PayLink;

  [InjectDependency]
  public ICCDisplayMaskService CCDisplayMaskService { get; set; }

  protected virtual void CustomerPaymentMethodDetail_Value_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CustomerPaymentMethodDetail row = e.Row as CustomerPaymentMethodDetail;
    PaymentMethodDetail template = this.FindTemplate(row);
    if (template == null)
      return;
    bool? nullable = template.IsIdentifier;
    if (nullable.GetValueOrDefault())
    {
      string maskById = CustomerPaymentMethodMaint.IDObfuscator.GetMaskByID(this, row.Value, template.DisplayMask, ((PXSelectBase<CustomerPaymentMethodDetail>) this.CustomerPaymentMethodDetails).Current.PMInstanceID);
      if (((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethods).Current.Descr != maskById)
      {
        PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethods).Current;
        current.Descr = $"{current.PaymentMethodID}:{maskById}";
        current.Descr = CustomerPaymentMethodMaint.FormatDescription(current.CardType ?? "OTH", maskById);
        ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethods).Update(current);
      }
    }
    nullable = template.IsExpirationDate;
    if (!nullable.GetValueOrDefault())
      return;
    PX.Objects.AR.CustomerPaymentMethod current1 = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethods).Current;
    ((PXSelectBase) this.CustomerPaymentMethods).Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.expirationDate>((object) current1, (object) CustomerPaymentMethodMaint.ParseExpiryDate((PXGraph) this, current1, row.Value));
    ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.CustomerPaymentMethods).Update(current1);
  }

  protected virtual PaymentMethodDetail FindTemplate(CustomerPaymentMethodDetail aDet)
  {
    return PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aDet.PaymentMethodID,
      (object) aDet.DetailID
    }));
  }
}
