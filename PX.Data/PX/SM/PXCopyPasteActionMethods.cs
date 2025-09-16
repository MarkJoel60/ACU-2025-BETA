// Decompiled with JetBrains decompiler
// Type: PX.SM.PXCopyPasteActionMethods
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

internal static class PXCopyPasteActionMethods
{
  public static PXCopyPasteAction<TNode> AddScreenIDAndNodeIdUniquenessImportValidation<TNode>(
    this PXCopyPasteAction<TNode> copyPasteAction,
    string withErrorMessageTemplate)
    where TNode : class, IBqlTable, new()
  {
    PXCopyPasteActionMethods.AddScreenIDUniquenessImportValidation<TNode>(copyPasteAction, withErrorMessageTemplate, PXCopyPasteActionMethods.FieldsToCheck.NodeId);
    return copyPasteAction;
  }

  public static PXCopyPasteAction<TNode> AddScreenIDAndUrlUniquenessImportValidation<TNode>(
    this PXCopyPasteAction<TNode> copyPasteAction,
    string withErrorMessageTemplate)
    where TNode : class, IBqlTable, new()
  {
    PXCopyPasteActionMethods.AddScreenIDUniquenessImportValidation<TNode>(copyPasteAction, withErrorMessageTemplate, PXCopyPasteActionMethods.FieldsToCheck.Url);
    return copyPasteAction;
  }

  private static void AddScreenIDUniquenessImportValidation<TNode>(
    PXCopyPasteAction<TNode> copyPaste,
    string withErrorMessageTemplate,
    PXCopyPasteActionMethods.FieldsToCheck fieldsToCheck)
    where TNode : class, IBqlTable, new()
  {
    // ISSUE: unable to decompile the method.
  }

  [Flags]
  private enum FieldsToCheck
  {
    Url = 1,
    NodeId = 2,
  }
}
