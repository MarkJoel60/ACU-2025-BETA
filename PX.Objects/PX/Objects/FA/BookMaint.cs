// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.BookMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.FA;

public class BookMaint : PXGraph<BookMaint>
{
  public PXSavePerRow<FABook> Save;
  public PXCancel<FABook> Cancel;
  public PXSelect<FABook> Book;
  public PXSelect<FABookYearSetup> YearSetup;
  public PXSelect<FABookYear> Years;
  public PXSelect<FABookPeriodSetup> PeriodSetup;
  public PXSelect<FABookPeriod> PeriodSetups;
  public PXSetup<PX.Objects.FA.FASetup> FASetup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<FABook> ShowCalendar;

  public static FABook FindByID(PXGraph graph, int? bookID)
  {
    return PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Required<FABook.bookID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bookID
    }));
  }

  public static FABook FindByBookMarker(PXGraph graph, int marker)
  {
    if (marker == 1)
      return PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.updateGL, Equal<True>>>.Config>.Select(graph, Array.Empty<object>()));
    if (marker == 2)
      return PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelectOrderBy<FABook, OrderBy<Desc<FABook.updateGL>>>.Config>.SelectSingleBound(graph, (object[]) null, Array.Empty<object>()));
    throw new NotImplementedException();
  }

  public BookMaint()
  {
    PX.Objects.FA.FASetup current1 = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
    PX.Objects.GL.GLSetup current2 = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  [PXUIField]
  [PXButton]
  protected virtual void showCalendar()
  {
    if (((PXSelectBase<FABook>) this.Book).Current.UpdateGL.GetValueOrDefault())
    {
      FiscalYearSetupMaint instance = PXGraph.CreateInstance<FiscalYearSetupMaint>();
      ((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Current = PXResultset<FinYearSetup>.op_Implicit(((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Select(Array.Empty<object>()));
      if (((PXSelectBase<FinYearSetup>) instance.FiscalYearSetup).Current == null)
      {
        FinYearSetup finYearSetup = new FinYearSetup();
        ((PXSelectBase) instance.FiscalYearSetup).Cache.Insert((object) finYearSetup);
        ((PXSelectBase) instance.FiscalYearSetup).Cache.IsDirty = false;
      }
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
    FABookYearSetupMaint instance1 = PXGraph.CreateInstance<FABookYearSetupMaint>();
    ((PXSelectBase<FABookYearSetup>) instance1.FiscalYearSetup).Current = PXResultset<FABookYearSetup>.op_Implicit(((PXSelectBase<FABookYearSetup>) instance1.FiscalYearSetup).Search<FABookYearSetup.bookID>((object) ((PXSelectBase<FABook>) this.Book).Current.BookCode, Array.Empty<object>()));
    if (((PXSelectBase<FABookYearSetup>) instance1.FiscalYearSetup).Current == null)
    {
      FABookYearSetup faBookYearSetup = new FABookYearSetup()
      {
        BookID = ((PXSelectBase<FABook>) this.Book).Current.BookID
      };
      ((PXSelectBase) instance1.FiscalYearSetup).Cache.SetDefaultExt<FABookYearSetup.periodType>((object) faBookYearSetup);
      ((PXSelectBase) instance1.FiscalYearSetup).Cache.Insert((object) faBookYearSetup);
      ((PXSelectBase) instance1.FiscalYearSetup).Cache.IsDirty = false;
    }
    throw new PXRedirectRequiredException((PXGraph) instance1, (string) null);
  }

  protected virtual void FABook_UpdateGL_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is FABook row) || !object.Equals(e.NewValue, (object) true))
      return;
    if (PXResultset<FABookYearSetup>.op_Implicit(PXSelectBase<FABookYearSetup, PXSelect<FABookYearSetup, Where<FABookYearSetup.bookID, Equal<Current<FABook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
      throw new PXSetPropertyException("The Posting Book check box cannot be selected for the {0} book because a calendar has been configured for the book on the Book Calendars (FA206000) form.", (PXErrorLevel) 5, new object[1]
      {
        (object) row.BookCode
      });
  }

  protected virtual void FABook_UpdateGL_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    FABook row = (FABook) e.Row;
    if (row == null || !row.UpdateGL.GetValueOrDefault())
      return;
    foreach (PXResult<FABook> pxResult in PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookCode, NotEqual<Current<FABook.bookCode>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()))
    {
      FABook faBook = PXResult<FABook>.op_Implicit(pxResult);
      faBook.UpdateGL = new bool?(false);
      ((PXSelectBase<FABook>) this.Book).Update(faBook);
    }
    ((PXSelectBase) this.Book).View.RequestRefresh();
  }

  protected virtual void FABook_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FABook row = (FABook) e.Row;
    if (e.Operation != 3)
      return;
    if (PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.bookID, Equal<Current<FABook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXRowPersistingException("BookCode", (object) row.BookCode, "Book '{0}' cannot be deleted because it is used by Fixed Asset or Fixed Asset Class.", new object[1]
      {
        (object) row.BookCode
      });
    if (PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.bookID, Equal<Current<FABook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXRowPersistingException("BookCode", (object) row.BookCode, "Book '{0}' cannot be deleted because it is used by Fixed Asset or Fixed Asset Class.", new object[1]
      {
        (object) row.BookCode
      });
  }

  protected virtual void FABook_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FABook row = (FABook) e.Row;
    if (PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.bookID, Equal<Current<FABook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException("Book '{0}' cannot be deleted because it is used by Fixed Asset or Fixed Asset Class.", new object[1]
      {
        (object) row.BookCode
      });
    if (PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.bookID, Equal<Current<FABook.bookID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException("Book '{0}' cannot be deleted because it is used by Fixed Asset or Fixed Asset Class.", new object[1]
      {
        (object) row.BookCode
      });
  }
}
