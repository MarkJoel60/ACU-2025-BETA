// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceReferenceTypeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Abstractions;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;

public static class ComplianceReferenceTypeHelper
{
  public static string GetValueByKey(Type type, string key)
  {
    return ComplianceReferenceTypeHelper.GetTypes().Single<ComplianceReferenceType>((Func<ComplianceReferenceType, bool>) (x => x.Type == type && x.Code == key)).DisplayValue;
  }

  public static string GetKeyByValue(Type type, string value)
  {
    return ComplianceReferenceTypeHelper.GetTypes().Single<ComplianceReferenceType>((Func<ComplianceReferenceType, bool>) (x => x.Type == type && x.DisplayValue == value)).Code;
  }

  private static IEnumerable<ComplianceReferenceType> GetTypes()
  {
    List<ComplianceReferenceType> types = new List<ComplianceReferenceType>();
    ComplianceReferenceTypeHelper.FillTypes<POOrderType.ListAttribute, PX.Objects.PO.POOrder>(types);
    ComplianceReferenceTypeHelper.FillTypes<ARDocType.ListAttribute, PX.Objects.AR.ARInvoice>(types);
    ComplianceReferenceTypeHelper.FillTypes<APDocType.ListAttribute, PX.Objects.AP.APInvoice>(types);
    ComplianceReferenceTypeHelper.FillTypes<APDocType.ListAttribute, PX.Objects.AP.APPayment>(types);
    ComplianceReferenceTypeHelper.FillTypes<ARDocType.ListAttribute, PX.Objects.AR.ARPayment>(types);
    ComplianceReferenceTypeHelper.FillTypes<BatchModule.ListAttribute, PMRegister>(types);
    return (IEnumerable<ComplianceReferenceType>) types;
  }

  private static void FillTypes<T, TE>(List<ComplianceReferenceType> types) where T : PXStringListAttribute
  {
    types.AddRange(Activator.CreateInstance<T>().ValueLabelDic.Select<KeyValuePair<string, string>, ComplianceReferenceType>((Func<KeyValuePair<string, string>, ComplianceReferenceType>) (pair => ComplianceReferenceTypeHelper.GetComplianceReferenceType(typeof (TE), pair))));
  }

  private static ComplianceReferenceType GetComplianceReferenceType(
    Type type,
    KeyValuePair<string, string> pair)
  {
    return new ComplianceReferenceType()
    {
      Type = type,
      Code = pair.Key,
      DisplayValue = pair.Value
    };
  }

  public static DocumentKey ConvertToDocumentKey<T>(string clDisplayName)
  {
    string[] strArray = clDisplayName.Split(',');
    string str = strArray[0].Trim();
    string refNbr = strArray[1].Trim();
    return new DocumentKey(ComplianceReferenceTypeHelper.GetKeyByValue(typeof (T), str), refNbr);
  }
}
