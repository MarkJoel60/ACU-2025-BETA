// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.AP.Services.JointCheckPrintService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.JointChecks.AP.Models;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.JointChecks.AP.Services;

internal class JointCheckPrintService
{
  private readonly PXGraph paymentEntry = PXGraph.CreateInstance<PXGraph>();

  public bool DoJointPayeePaymentsWithPositiveAmountExist(
    string documentType,
    string referenceNumber)
  {
    return ((IQueryable<PXResult<JointPayeePayment>>) ((PXSelectBase<JointPayeePayment>) new PXSelect<JointPayeePayment, Where<JointPayeePayment.paymentDocType, Equal<Required<JointPayeePayment.paymentDocType>>, And<JointPayeePayment.paymentRefNbr, Equal<Required<JointPayeePayment.paymentRefNbr>>, And<JointPayeePayment.jointAmountToPay, Greater<decimal0>>>>>(this.paymentEntry)).Select(new object[2]
    {
      (object) documentType,
      (object) referenceNumber
    })).Any<PXResult<JointPayeePayment>>();
  }

  public string GetJointPayeesSingleLine(string documentType, string referenceNumber)
  {
    return this.GetJointPayees(documentType, referenceNumber, false);
  }

  public string GetJointPayeesMultiline(string documentType, string referenceNumber)
  {
    return this.GetJointPayees(documentType, referenceNumber, true);
  }

  private string GetJointPayees(string documentType, string referenceNumber, bool isMultiline)
  {
    PXResultset<JointPayeePayment> jointPayeePayments = this.GetJointPayeePayments(documentType, referenceNumber);
    JointCheckPrintModel printModel = JointCheckPrintModel.Create(isMultiline);
    foreach (PXResult<JointPayeePayment> jointPayeePayment in jointPayeePayments)
      JointCheckPrintService.ProcessJointPayeePayment(printModel, (PXResult) jointPayeePayment);
    return printModel.JointPayeeNames;
  }

  private static void ProcessJointPayeePayment(
    JointCheckPrintModel printModel,
    PXResult jointPayeePayment)
  {
    JointPayee jointPayee = jointPayeePayment.GetItem<JointPayee>();
    Vendor vendor = jointPayeePayment.GetItem<Vendor>();
    PX.Objects.CR.Contact remitanceContact = jointPayeePayment.GetItem<PX.Objects.CR.Contact>();
    string jointPayeeNameIfNew = JointCheckPrintService.GetJointPayeeNameIfNew(printModel, jointPayee, (PX.Objects.CR.BAccount) vendor, remitanceContact);
    if (jointPayeeNameIfNew == string.Empty)
      return;
    JointCheckPrintService.UpdateJointCheckPrintModelWithNewPayee(printModel, jointPayee, (PX.Objects.CR.BAccount) vendor);
    JointCheckPrintService.AddJointPayeeName(printModel, jointPayeeNameIfNew);
  }

  private static void UpdateJointCheckPrintModelWithNewPayee(
    JointCheckPrintModel printModel,
    JointPayee jointPayee,
    PX.Objects.CR.BAccount vendor)
  {
    if (Str.IsNullOrEmpty(jointPayee.JointPayeeExternalName))
      printModel.InternalJointPayeeIds.Add(vendor.BAccountID);
    else
      printModel.ExternalJointPayeeNames.Add(jointPayee.JointPayeeExternalName);
  }

  private static string GetJointPayeeNameIfNew(
    JointCheckPrintModel printModel,
    JointPayee jointPayee,
    PX.Objects.CR.BAccount vendor,
    PX.Objects.CR.Contact remitanceContact)
  {
    return Str.IsNullOrEmpty(jointPayee.JointPayeeExternalName) ? (!printModel.InternalJointPayeeIds.Any<int?>((Func<int?, bool>) (id =>
    {
      int? nullable = id;
      int? baccountId = vendor.BAccountID;
      return nullable.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable.HasValue == baccountId.HasValue;
    })) ? remitanceContact.FullName : string.Empty) : (!printModel.ExternalJointPayeeNames.Any<string>((Func<string, bool>) (name => JointCheckPrintService.IsSameJointPayeeExternalName(name, jointPayee))) ? jointPayee.JointPayeeExternalName : string.Empty);
  }

  private static void AddJointPayeeName(JointCheckPrintModel printModel, string jointPayeeName)
  {
    string str = $"{printModel.JointPayeeNames}{jointPayeeName} And ";
    printModel.JointPayeeNames = printModel.IsMultilinePrintMode ? str + Environment.NewLine : str;
  }

  private static bool IsSameJointPayeeExternalName(string name, JointPayee jointPayee)
  {
    return string.Equals(name.Trim(), jointPayee.JointPayeeExternalName.Trim(), StringComparison.CurrentCultureIgnoreCase);
  }

  private PXResultset<JointPayeePayment> GetJointPayeePayments(
    string documentType,
    string referenceNumber)
  {
    this.paymentEntry.Caches[typeof (JointPayeePayment)].ClearQueryCache();
    return ((PXSelectBase<JointPayeePayment>) new PXSelectJoin<JointPayeePayment, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<JointPayeePayment.jointPayeeId>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<JointPayee.jointPayeeInternalId>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<Vendor.defLocationID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.vRemitContactID>>>>>>, Where<JointPayeePayment.paymentDocType, Equal<Required<JointPayeePayment.paymentDocType>>, And<JointPayeePayment.paymentRefNbr, Equal<Required<JointPayeePayment.paymentRefNbr>>, And<JointPayeePayment.jointAmountToPay, Greater<decimal0>, And<JointPayee.isMainPayee, Equal<False>>>>>>(this.paymentEntry)).Select(new object[2]
    {
      (object) documentType,
      (object) referenceNumber
    });
  }
}
