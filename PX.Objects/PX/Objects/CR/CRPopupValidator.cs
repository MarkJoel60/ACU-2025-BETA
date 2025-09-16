// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRPopupValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CRPopupValidator : ICRValidationFilter, ICRPreserveCachedRecordsFilter
{
  private readonly ICRValidationFilter[] _filters;

  private CRPopupValidator(ICRValidationFilter[] filters)
  {
    this._filters = filters ?? throw new ArgumentNullException(nameof (filters));
  }

  public void Validate()
  {
    List<PXOuterException> source1 = new List<PXOuterException>(this._filters.Length);
    foreach (ICRValidationFilter filter in this._filters)
    {
      try
      {
        filter.Validate();
      }
      catch (PXOuterException ex)
      {
        source1.Add(ex);
      }
    }
    if (source1.Count > 0)
    {
      IGrouping<(System.Type, object), PXOuterException> source2 = source1.GroupBy<PXOuterException, (System.Type, object)>((Func<PXOuterException, (System.Type, object)>) (e => (e.GraphType, e.Row))).First<IGrouping<(System.Type, object), PXOuterException>>();
      Dictionary<string, string> dict_ = new Dictionary<string, string>(17);
      foreach (PXOuterException ex_ in (IEnumerable<PXOuterException>) source2)
        FillDictionary(dict_, ex_);
      throw new PXOuterException(dict_, source2.Key.Item1, source2.Key.Item2, ((Exception) source2.First<PXOuterException>()).Message);
    }

    static void FillDictionary(Dictionary<string, string> dict_, PXOuterException ex_)
    {
      string[] innerFields = ex_.InnerFields;
      string[] innerMessages = ex_.InnerMessages;
      for (int index = 0; index < innerFields.Length; ++index)
        dict_[innerFields[index]] = innerMessages[index];
    }
  }

  public void Validate(params CRPopupValidator[] additionalValidators)
  {
    if (additionalValidators == null)
    {
      this.Validate();
    }
    else
    {
      PXOuterException pxOuterException = (PXOuterException) null;
      try
      {
        this.Validate();
      }
      catch (PXOuterException ex)
      {
        pxOuterException = ex;
      }
      foreach (CRPopupValidator additionalValidator in additionalValidators)
      {
        try
        {
          additionalValidator.Validate();
        }
        catch (PXOuterException ex)
        {
          pxOuterException = pxOuterException ?? ex;
        }
      }
      if (pxOuterException != null)
        throw pxOuterException;
    }
  }

  public bool TryValidate()
  {
    bool flag = true;
    foreach (ICRValidationFilter filter in this._filters)
      flag &= filter.TryValidate();
    return flag;
  }

  public bool TryValidate(params CRPopupValidator[] additionalValidators)
  {
    bool flag = this.TryValidate();
    if (additionalValidators == null)
      return flag;
    foreach (CRPopupValidator additionalValidator in additionalValidators)
      flag &= additionalValidator.TryValidate();
    return flag;
  }

  public void Reset()
  {
    foreach (ICRValidationFilter filter in this._filters)
      filter.Reset();
  }

  public void Reset(params CRPopupValidator[] additionalValidators)
  {
    this.Reset();
    if (additionalValidators == null)
      return;
    foreach (CRPopupValidator additionalValidator in additionalValidators)
      additionalValidator.Reset();
  }

  public IDisposable PreserveCachedRecords()
  {
    CompositeDisposable compositeDisposable = new CompositeDisposable(this._filters.Length);
    foreach (ICRPreserveCachedRecordsFilter cachedRecordsFilter in this._filters.OfType<ICRPreserveCachedRecordsFilter>())
      compositeDisposable.Add(cachedRecordsFilter.PreserveCachedRecords());
    return (IDisposable) compositeDisposable;
  }

  public static CRPopupValidator.Generic<TTable> Create<TTable>(
    CRValidationFilter<TTable> filter,
    params ICRValidationFilter[] filters)
    where TTable : class, IBqlTable, new()
  {
    return new CRPopupValidator.Generic<TTable>(filter, filters);
  }

  public class Generic<TTable> : CRPopupValidator where TTable : class, IBqlTable, new()
  {
    public Generic(CRValidationFilter<TTable> filter, params ICRValidationFilter[] filters)
      : base(((IEnumerable<ICRValidationFilter>) EnumerableExtensions.Prepend<ICRValidationFilter>(filters ?? Array.Empty<ICRValidationFilter>(), (ICRValidationFilter) filter)).ToArray<ICRValidationFilter>())
    {
      this.Filter = filter ?? throw new ArgumentNullException(nameof (filter));
    }

    public CRValidationFilter<TTable> Filter { get; }

    [Obsolete("Use AskExt() && TryValidate() instead")]
    public WebDialogResult? AskExtValidation(
      PXView.InitializePanel initializeHandler = null,
      bool reset = true,
      IEnumerable<CRPopupValidator> additionalValidators = null)
    {
      WebDialogResult? nullable = new WebDialogResult?(this.AskExt(initializeHandler, reset));
      return !this.TryValidate(additionalValidators != null ? additionalValidators.ToArray<CRPopupValidator>() : (CRPopupValidator[]) null) ? new WebDialogResult?() : nullable;
    }

    public WebDialogResult AskExt(PXView.InitializePanel initializeHandler = null, bool reset = true)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CRPopupValidator.Generic<TTable>.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new CRPopupValidator.Generic<TTable>.\u003C\u003Ec__DisplayClass5_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.reset = reset;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.initializeHandler = initializeHandler;
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      return ((PXSelectBase<TTable>) this.Filter).AskExt(new PXView.InitializePanel((object) cDisplayClass50, __methodptr(\u003CAskExt\u003Eg__Initializer\u007C0)), cDisplayClass50.reset);
    }
  }
}
