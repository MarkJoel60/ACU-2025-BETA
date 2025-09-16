// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.CommonActionCategories
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using System;

#nullable disable
namespace PX.Objects.Common;

/// <summary>Commonly used menu action categories.</summary>
public static class CommonActionCategories
{
  public const string ProcessingCategoryID = "Processing Category";
  public const string ApprovalCategoryID = "Approval Category";
  public const string CorrectionsCategoryID = "Corrections Category";
  public const string PrintingAndEmailingCategoryID = "Printing and Emailing Category";
  public const string IntercompanyCategoryID = "Intercompany Category";
  public const string OtherCategoryID = "Other Category";
  public const string AuthenticationCategoryID = "Authentication Category";

  public static CommonActionCategories.Categories<TGraph, TPrimary> Get<TGraph, TPrimary>(
    WorkflowContext<TGraph, TPrimary> context)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    return new CommonActionCategories.Categories<TGraph, TPrimary>(context);
  }

  [PXLocalizable]
  public static class DisplayNames
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string Corrections = "Corrections";
    public const string PrintingAndEmailing = "Printing and Emailing";
    public const string Intercompany = "Intercompany";
    public const string Other = "Other";
    public const string Authentication = "Authentication";
  }

  public struct Categories<TGraph, TPrimary>(WorkflowContext<TGraph, TPrimary> context)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    private readonly WorkflowContext<TGraph, TPrimary> _context = context;

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Processing
    {
      get
      {
        return this._context.Categories.Get("Processing Category") ?? this._context.Categories.CreateNew("Processing Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Processing))));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Approval
    {
      get
      {
        return this._context.Categories.Get("Approval Category") ?? this._context.Categories.CreateNew("Approval Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Approval))));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Corrections
    {
      get
      {
        return this._context.Categories.Get("Corrections Category") ?? this._context.Categories.CreateNew("Corrections Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Corrections))));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured PrintingAndEmailing
    {
      get
      {
        return this._context.Categories.Get("Printing and Emailing Category") ?? this._context.Categories.CreateNew("Printing and Emailing Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName("Printing and Emailing")));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Intercompany
    {
      get
      {
        return this._context.Categories.Get("Intercompany Category") ?? this._context.Categories.CreateNew("Intercompany Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Intercompany))));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Other
    {
      get
      {
        return this._context.Categories.Get("Other Category") ?? this._context.Categories.CreateNew("Other Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Other))));
      }
    }

    public BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured Authentication
    {
      get
      {
        return this._context.Categories.Get("Authentication Category") ?? this._context.Categories.CreateNew("Authentication Category", (Func<BoundedTo<TGraph, TPrimary>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured>) (c => (BoundedTo<TGraph, TPrimary>.ActionCategory.IConfigured) c.DisplayName(nameof (Authentication))));
      }
    }
  }
}
